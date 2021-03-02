using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace ZeroApp.ZeroCore
{
    public class ParametersObjectModel
    {
		[XmlRoot(ElementName = "Parameter")]
		public class Parameter
		{
			[XmlAttribute(AttributeName = "Active")]
			public string Active { get; set; }
			[XmlAttribute(AttributeName = "Key")]
			public string Key { get; set; }
			[XmlAttribute(AttributeName = "Name")]
			public string Name { get; set; }
			[XmlAttribute(AttributeName = "Value")]
			public string Value { get; set; }
		}

		[XmlRoot(ElementName = "Parameters")]
		public class Parameters
		{
			[XmlElement(ElementName = "Parameter")]
			public List<Parameter> Parameter = new List<Parameter>(); 
		}
	}
}
