using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace ZeroApp.ZeroCore
{
    public class ConfigObjectModel
    {
		[XmlRoot(ElementName = "Key")]
		public class Key
		{
			[XmlAttribute(AttributeName = "Name")]
			public string Name { get; set; }
			[XmlAttribute(AttributeName = "Value")]
			public string Value { get; set; }
		}

		[XmlRoot(ElementName = "Keys")]
		public class Keys
		{
			[XmlElement(ElementName = "Key")]
			public List<Key> Key = new List<Key>();
		}
	}
}
