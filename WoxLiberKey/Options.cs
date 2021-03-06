﻿using System.IO;
using Newtonsoft.Json;

namespace WoxLiberKey
{
    /// <summary>
    ///     Contains plugin options.
    /// </summary>
    public class Options
    {
        private readonly string _filePath;

        public Options(string path)
        {
            _filePath = path;
        }

        [JsonProperty] public string LiberKeyRootPath { get; set; }

        /// <summary>
        ///     Loads options saved to file if exists.
        /// </summary>
        public void Load()
        {
            // Skip loading if nothing was saved
            if (!File.Exists(_filePath)) return;

            // Load stored options
            var loaded = JsonConvert.DeserializeObject<Options>(File.ReadAllText(_filePath));

            // Save them here
            LiberKeyRootPath = loaded.LiberKeyRootPath;
        }

        /// <summary>
        ///     Saves current values to file.
        /// </summary>
        public void Save()
        {
            File.WriteAllText(_filePath, JsonConvert.SerializeObject(this));
        }
    }
}