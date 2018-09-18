using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Controls;
using Wox.Plugin;

namespace WoxLiberKey
{
    public class Main : IPlugin, ISettingProvider
    {
        /// <summary>
        ///     LiberKey helper library.
        /// </summary>
        public LiberKey LiberKey { get; private set; }

        /// <summary>
        ///     Contains plugin options.
        /// </summary>
        public Options Options { get; set; }

        /// <summary>
        ///     Called when users enters some input
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<Result> Query(Query query)
        {
            return LiberKey.Apps
                .Where(game => game.Name.ToLower().Contains(query.RawQuery.ToLower()) || game.AutoKeyWords.ToLower().Contains(query.RawQuery.ToLower()))
                .Select(game => new Result
                {
                    Title = game.Name,
                    SubTitle = "LiberKey application: " + game.ExePath,
                    IcoPath = "Images/app.png",
                    Score = Score(query.RawQuery.ToLower(), game.Name, game.ExePath, game.AutoKeyWords),
                    Action = context =>
                    {
                        Process.Start(game.ExePath);
                        return true;
                    }
                })
                .ToList();
        }

        /// <summary>
        ///     Called to initialize the plugin.
        /// </summary>
        /// <param name="context"></param>
        public void Init(PluginInitContext context)
        {
            // Create library helper
            // Use plugin directory if possible
            LiberKey = new LiberKey();

            // Load options
            Options = new Options(context != null
                ? Path.Combine(context.CurrentPluginMetadata.PluginDirectory, "options.json")
                : "options.json");
            Options.Load();

            // Apply steam path option if somewhat correct
            if (Options.LiberKeyRootPath != null && Directory.Exists(Options.LiberKeyRootPath))
            {
                LiberKey.RootPath = Options.LiberKeyRootPath;
                if (!LiberKey.RootPath.EndsWith("\\")) LiberKey.RootPath += "\\";
            }


            // Loads game list
            LiberKey.Load();

            // Update options value, no matter the source
            Options.LiberKeyRootPath = LiberKey.RootPath;
            Options.Save();
        }

        public Control CreateSettingPanel()
        {
            return new SettingsControl(Options);
        }

        private int Score(string query, string name, string executableName, string keywords)
        {
            var score1 = StringMatcher.Score(name, query);
            var score2 = StringMatcher.Score(keywords, query);
            var score3 = StringMatcher.Score(executableName, query);
            var score = new[] {score1, score2, score3}.Max();
            return score;
        }
    }
}