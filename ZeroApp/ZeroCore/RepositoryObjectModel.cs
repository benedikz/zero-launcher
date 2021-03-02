using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ZeroApp
{   
    /// <summary>
    /// Repository Object Model
    /// </summary>
    public class RepositoryObjectModel
    {
		[XmlRoot(ElementName = "Mods")]
		public class Mods
		{

			[XmlElement(ElementName = "string")]
			public List<string> @string { get; set; }
		}

		[XmlRoot(ElementName = "Modpack")]
		public class Modpack
		{

			[XmlElement(ElementName = "Mods")]
			public Mods Mods { get; set; }

			[XmlAttribute(AttributeName = "ID")]
			public string ID { get; set; }

			[XmlAttribute(AttributeName = "Name")]
			public string Name { get; set; }

			[XmlAttribute(AttributeName = "Source")]
			public string Source { get; set; }

			[XmlAttribute(AttributeName = "IP")]
			public string IP { get; set; }

			[XmlAttribute(AttributeName = "Port")]
			public string Port { get; set; }

			[XmlAttribute(AttributeName = "Password")]
			public string Password { get; set; }

			[XmlText]
			public string text { get; set; }
		}

		[XmlRoot(ElementName = "Modpacks")]
		public class Modpacks
		{

			[XmlElement(ElementName = "Modpack")]
			public Modpack Modpack { get; set; }
		}

		[XmlRoot(ElementName = "File")]
		public class File
		{

			[XmlAttribute(AttributeName = "LastChange")]
			public int LastChange { get; set; }

			[XmlAttribute(AttributeName = "Path")]
			public string Path { get; set; }

			[XmlAttribute(AttributeName = "Size")]
			public int Size { get; set; }
		}

		[XmlRoot(ElementName = "Files")]
		public class Files
		{

			[XmlElement(ElementName = "File")]
			public List<File> File { get; set; }
		}

		[XmlRoot(ElementName = "Addon")]
		public class Addon
		{

			[XmlElement(ElementName = "Files")]
			public Files Files { get; set; }

			[XmlAttribute(AttributeName = "Name")]
			public string Name { get; set; }
		}

		[XmlRoot(ElementName = "Addons")]
		public class Addons
		{

			[XmlElement(ElementName = "Addon")]
			public List<Addon> Addon { get; set; }
		}

		[XmlRoot(ElementName = "Repository")]
		public class Repository
		{

			[XmlElement(ElementName = "Modpacks")]
			public Modpacks Modpacks { get; set; }

			[XmlElement(ElementName = "Addons")]
			public Addons Addons { get; set; }
		}

	}
}
