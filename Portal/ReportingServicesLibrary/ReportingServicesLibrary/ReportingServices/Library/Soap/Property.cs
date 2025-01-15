using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Xml;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library.Soap
{
	// Token: 0x0200031C RID: 796
	public class Property
	{
		// Token: 0x06001B40 RID: 6976 RVA: 0x0006EC08 File Offset: 0x0006CE08
		internal static string ThisArrayToXml(Property[] properties)
		{
			if (properties == null)
			{
				return null;
			}
			StringWriter stringWriter = new StringWriter(Localization.CatalogCulture);
			XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);
			xmlTextWriter.Formatting = Formatting.Indented;
			xmlTextWriter.WriteStartElement("Properties");
			for (int i = 0; i < properties.Length; i++)
			{
				if (properties[i] == null)
				{
					throw new MissingElementException("Property");
				}
				string name = properties[i].Name;
				if (name == null)
				{
					throw new MissingElementException("Name");
				}
				if (name.Length == 0)
				{
					throw new InvalidElementException("Name");
				}
				string value = properties[i].Value;
				if (value != null)
				{
					string text = XmlUtil.EncodePropertyName(name);
					RSTrace.CatalogTrace.Assert(!string.IsNullOrEmpty(text));
					xmlTextWriter.WriteStartElement(text);
					xmlTextWriter.WriteString(value);
					xmlTextWriter.WriteEndElement();
				}
			}
			xmlTextWriter.WriteEndElement();
			return stringWriter.ToString();
		}

		// Token: 0x06001B41 RID: 6977 RVA: 0x0006ECD8 File Offset: 0x0006CED8
		internal static Property[] XmlToThisArray(string properties)
		{
			if (properties == null)
			{
				return null;
			}
			SortedList sortedList = new SortedList();
			XmlTextReader xmlTextReader = XmlUtil.SafeCreateXmlTextReader(properties);
			try
			{
				xmlTextReader.MoveToContent();
				if (xmlTextReader.NodeType != XmlNodeType.Element)
				{
					throw new InternalCatalogException("invalid XML element");
				}
				if (xmlTextReader.Name != "Properties")
				{
					throw new InternalCatalogException(string.Format(CultureInfo.InvariantCulture, "invalid XML element name: {0}", xmlTextReader.Name));
				}
				while (xmlTextReader.Read())
				{
					if (xmlTextReader.IsStartElement())
					{
						bool isEmptyElement = xmlTextReader.IsEmptyElement;
						string text = xmlTextReader.Name;
						text = XmlUtil.DecodePropertyName(text);
						string text2 = xmlTextReader.ReadString();
						if (text2 == null || text2.Length == 0)
						{
							sortedList.Add(text, null);
						}
						else
						{
							sortedList.Add(text, text2);
						}
						if (!isEmptyElement && xmlTextReader.IsStartElement())
						{
							throw new InternalCatalogException("invalid XML element ");
						}
					}
				}
			}
			catch (XmlException ex)
			{
				throw new InternalCatalogException(ex, "malformed XML");
			}
			Property[] array = new Property[sortedList.Count];
			for (int i = 0; i < sortedList.Count; i++)
			{
				array[i] = new Property
				{
					Name = (string)sortedList.GetKey(i),
					Value = (string)sortedList.GetByIndex(i)
				};
			}
			return array;
		}

		// Token: 0x04000AB7 RID: 2743
		public string Name;

		// Token: 0x04000AB8 RID: 2744
		public string Value;
	}
}
