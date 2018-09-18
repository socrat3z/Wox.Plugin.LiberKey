using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace WoxLiberKey
{
    /// <summary>
    ///     LiberKey helper class.
    /// </summary>
    public class LiberKey
    {
        /// <summary>
        ///     Path to liberkey installation root.
        /// </summary>
        public string RootPath { get; set; }

        /// <summary>
        ///     List of all currently installed apps.
        /// </summary>
        public List<LiberKeyDBSoftware> Apps { get; } = new List<LiberKeyDBSoftware>();

        /// <summary>
        ///     Loads list of installed apps.
        /// </summary>
        public void Load()
        {
            LoadApps();
        }

        /// <summary>
        ///     Loads list of installed apps.
        /// </summary>
        private void LoadApps()
        {
            Apps.Clear();

            try
            {
                LiberKeyDB db = null;
                var path = Path.Combine(RootPath, @"LiberKeyTools\LiberKeyMenu\data\localapps.db.xml");
                var serializer = new XmlSerializer(typeof(LiberKeyDB));
                using (var reader = new StreamReader(path))
                {
                    db = (LiberKeyDB) serializer.Deserialize(reader);
                    reader.Close();
                }

                foreach (var app in db.Apps)
                {
                    if (app.ExePath.StartsWith(@"%APPS%"))
                        app.ExePath = app.ExePath.Replace(@"%APPS%", "Apps");
                    else if (app.ExePath.StartsWith(@"%MYAPPS%"))
                        app.ExePath = app.ExePath.Replace(@"%MYAPPS%", "MyApps");

                    app.ExePath = Path.Combine(RootPath, app.ExePath);
                }

                Apps.AddRange(db.Apps);
            }
            finally
            {
                if (!Directory.Exists(RootPath)) throw new Exception("Failed to locate LiberKey installation.");
            }
        }
    }
}