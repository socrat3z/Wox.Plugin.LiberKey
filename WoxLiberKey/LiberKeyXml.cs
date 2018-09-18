using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace WoxLiberKey
{
// NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks />
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false)]
    public class LiberKeyDB
    {
        /// <remarks />
        [XmlArrayItem("Software", IsNullable = false)]
        public LiberKeyDBSoftware[] Apps { get; set; }
    }

    /// <remarks />
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public class LiberKeyDBSoftware
    {
        /// <remarks />
        public string Name { get; set; }

        /// <remarks />
        public string ExePath { get; set; }

        /// <remarks />
        public string Platform { get; set; }

        /// <remarks />
        public string License { get; set; }

        /// <remarks />
        public string Description { get; set; }

        /// <remarks />
        public string Version { get; set; }

        /// <remarks />
        public string Developer { get; set; }

        /// <remarks />
        public string WebSite { get; set; }

        /// <remarks />
        public string LicenseFile { get; set; }

        /// <remarks />
        public string AutoKeyWords { get; set; }

        /// <remarks />
        [XmlAttribute]
        public string id { get; set; }

        /// <remarks />
        [XmlAttribute]
        public string type { get; set; }
    }
}