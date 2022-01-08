using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PS3Utils
{
    public class RPCS3
    {
        public RPCS3(string installPath)
        {
            this.AppPath= installPath;
        }

        public string AppPath = "";

        public string GetExeName()
        {
            return Path.Combine(AppPath, "Rpcs3.exe");
        }

        public List<PS3Game> GetDiscGames()
        {
            var discGames=new List<PS3Game>();

            var games = File.ReadAllLines(Path.Combine(AppPath, "games.yml"), Encoding.UTF8);
            foreach (var game in games)
            {
                if (String.IsNullOrWhiteSpace(game))
                    continue;

                if (!game.Contains(":"))
                    continue;

                var path = game.Substring(game.IndexOf(':') + 1).Trim().Replace("/", "\\");
                var basePath = Path.Combine(path, "PS3_GAME");
                var foundGame = new PS3Game(basePath);

                if (!foundGame.IsValid())
                    continue;

                discGames.Add(foundGame);
            }
            return discGames;
        }

        public List<PS3Game> GetHddGames()
        {
            var hddGames = new List<PS3Game>();

            var appBasePath = Path.Combine(AppPath, "dev_hdd0\\game");
            var appDirs = Directory.GetDirectories(appBasePath);
            foreach (var app in appDirs)
            {
                var foundGame = new PS3Game(app);

                if (!foundGame.IsValid())
                {
                    continue;
                }
                hddGames.Add(foundGame);
            }

            return hddGames;
        }

        public List<PS3Game> GetAllGames() {
            var processedIds = new List<string>();
            var allGames=new List<PS3Game>();

            GetDiscGames().ForEach(game =>
            {
                var id = game.GetId();
                if (processedIds.Contains(id))
                    return;

                processedIds.Add(id);
                allGames.Add(game);
            });

            GetHddGames().ForEach(game =>
            {
                var id = game.GetId();
                if (processedIds.Contains(id))
                    return;

                processedIds.Add(id);
                allGames.Add(game);
            });

            return allGames;
        }

        public ApplicationTarget GetApplicationTarget(PS3Game game,string stubDirectory,bool noGui=true)
        {
            return new ApplicationTarget()
            {
                OutName = Path.Combine(stubDirectory, game.GetCleanName() + ".exe"),
                Binary = GetExeName(),
                WorkingDirectory = AppPath,
                AppIcon = game.GetFormattedIcon(),
                Arguments = (noGui? "--no-gui ":"") +"\"" + game.EbootPath + "\""
            };
        }
    }
}
