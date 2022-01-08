using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

namespace PS3Utils
{
    public class PS3Game
    {
        public PS3Game(string basePath)
        {
            GamePath= basePath;
            EbootPath = Path.Combine(GamePath, "USRDIR\\EBOOT.BIN");
            SfoPath = Path.Combine(GamePath, "PARAM.SFO");
        }
        public readonly string GamePath = "";
        public readonly string EbootPath = "";
        public readonly string SfoPath = "";
        public Dictionary<string, string> Sfo = null;

        public string GetId()
        {
            if (Sfo == null)
                ReadSFO();

            if (Sfo == null || !Sfo.ContainsKey("TITLE_ID"))
                return "";

            return Sfo["TITLE_ID"].ToUpper();
        }

        public Image GetIconImage()
        {
            return Image.FromFile(Path.Combine(GamePath, "ICON0.PNG"));
        }

        public Icon GetIcon(Image img)
        {
            var ms = new MemoryStream();
            var bw = new BinaryWriter(ms);
            bw.Write((short)0);
            bw.Write((short)1);
            bw.Write((short)1);
            bw.Write((byte)(img.Width >= 256 ? 0 : img.Width));
            bw.Write((byte)(img.Height >= 256 ? 0 : img.Height));
            bw.Write((byte)0);
            bw.Write((byte)0);
            bw.Write((short)0);
            bw.Write((short)0);
            bw.Write((int)0);
            bw.Write((int)22);
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            var imageSize = (int)ms.Position - 22;
            ms.Seek(14, SeekOrigin.Begin);
            bw.Write(imageSize);
            ms.Seek(0, SeekOrigin.Begin);

            return new Icon(ms);
        }

        public Icon GetIcon()
        {
            return GetIcon(GetIconImage());
        }

        public Image GetImage(Image image, int width, int height)
        {
            var sourceAspect=(float)image.Width / (float)image.Height;
            var destAspect = (float)width / (float)height;
            var unpaddedWidth = width;
            var unpaddedHeight = height;

            if (sourceAspect > destAspect) //dest is more vertical than source
            {
                unpaddedHeight = (int)((float)width / sourceAspect);
            }
            else
            {
                unpaddedWidth = (int)((float)height * sourceAspect);
            }

            Bitmap b = new Bitmap(width, height);
            Graphics g = Graphics.FromImage((Image)b);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.DrawImage(image, (width-unpaddedWidth)/2, (height-unpaddedHeight)/2, unpaddedWidth, unpaddedHeight);
            g.Dispose();
            return (Image)b;
        }
        public Icon GetFormattedIcon()
        {
            var img = GetIconImage();
            return GetIcon(GetImage(img,48,48));
        }

        public string GetName()
        {
            if (Sfo == null)
                ReadSFO();

            if (Sfo == null || !Sfo.ContainsKey("TITLE_ID"))
                return "";

            return Sfo["TITLE"].ToUpper();
        }

        public string GetCleanName()
        {
            return Regex.Replace(GetName(), "[^a-zA-Z0-9\\-_', .!()]", "").ToUpper();
        }

        public bool HasEboot()
        {
            return File.Exists(EbootPath);
        }

        public bool HasSfo()
        {
            return File.Exists(SfoPath);
        }
        public bool IsValid()
        {
            return HasEboot() && GetId()!="";
        }


        [StructLayout(LayoutKind.Explicit)]
        private struct SfoHeader
        {
            [FieldOffset(0)]
            public UInt32 magic;
            [FieldOffset(4)]
            public UInt32 version;
            [FieldOffset(8)]
            public UInt32 key_table_start;
            [FieldOffset(12)]
            public UInt32 data_table_start;
            [FieldOffset(16)]
            public UInt32 tables_entries;
        };

        [StructLayout(LayoutKind.Explicit)]
        private struct SfoTableEntry
        {
            [FieldOffset(0)]
            public UInt16 key_offset;
            [FieldOffset(2)]
            public UInt16 data_fmt; // 04 00 utf8-S (no null) , 04 02 utf8, 04 04 uint32
            [FieldOffset(4)]
            public UInt32 data_len;
            [FieldOffset(8)]
            public UInt32 data_max_len;
            [FieldOffset(12)]
            public UInt32 data_offset;
        };

        internal void ReadSFO()
        {

            if (!HasSfo())
                return;

            var r = new Dictionary<string, string>();
            var headerSize = Marshal.SizeOf(typeof(SfoHeader));
            var indexSize = Marshal.SizeOf(typeof(SfoTableEntry));

            var sfo = File.ReadAllBytes(SfoPath);
            SfoHeader sfoHeader;
            SfoTableEntry sfoTableEntry;

            {
                GCHandle handle = GCHandle.Alloc(sfo, GCHandleType.Pinned);
                sfoHeader = (SfoHeader)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(SfoHeader));
                handle.Free();
            }

            var indexOffset = headerSize;
            var keyOffset = sfoHeader.key_table_start;
            var valueOffset = sfoHeader.data_table_start;
            for (var i = 0; i < sfoHeader.tables_entries; i++)
            {
                var sfoEntry = new byte[indexSize];
                Array.Copy(sfo, indexOffset + i * indexSize, sfoEntry, 0, indexSize);
                GCHandle handle = GCHandle.Alloc(sfoEntry, GCHandleType.Pinned);
                sfoTableEntry = (SfoTableEntry)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(SfoTableEntry));
                handle.Free();
                var entryValueOffset = valueOffset + sfoTableEntry.data_offset;
                var entryKeyOffset = keyOffset + sfoTableEntry.key_offset;
                var val = "";
                var keyBytes = Encoding.UTF8.GetString(sfo.Skip((int)entryKeyOffset).TakeWhile(b => !b.Equals(0)).ToArray());
                switch (sfoTableEntry.data_fmt)
                {
                    case 0x0004: //non-null string
                    case 0x0204: //null string
                        var strBytes = new byte[sfoTableEntry.data_len];
                        Array.Copy(sfo, entryValueOffset, strBytes, 0, sfoTableEntry.data_len);
                        val = Encoding.UTF8.GetString(strBytes);
                        break;
                    case 0x0404: //uint32
                        val = BitConverter.ToUInt32(sfo, (int)entryValueOffset).ToString();
                        break;
                }
                r.Add(keyBytes, val);
            }
            Sfo=r;

        }

        public override string ToString()
        {
            return GetName();
        }
    }
}
