using System;
using System.Xml;

namespace Microsoft.ReportingServices.Library.Soap
{
	// Token: 0x02000345 RID: 837
	public class Field
	{
		// Token: 0x06001BF6 RID: 7158 RVA: 0x0007161C File Offset: 0x0006F81C
		internal static void WriteArrayToXml(Field[] fields, XmlTextWriter xml)
		{
			if (fields == null)
			{
				return;
			}
			xml.WriteStartElement("Fields");
			for (int i = 0; i < fields.Length; i++)
			{
				Field.WriteToXml(fields[i], xml);
			}
			xml.WriteEndElement();
		}

		// Token: 0x06001BF7 RID: 7159 RVA: 0x00071655 File Offset: 0x0006F855
		internal static void WriteToXml(Field field, XmlTextWriter xml)
		{
			if (field == null)
			{
				return;
			}
			xml.WriteStartElement("Field");
			xml.WriteElementString("Alias", field.Alias);
			xml.WriteElementString("Name", field.Name);
			xml.WriteEndElement();
		}

		// Token: 0x06001BF8 RID: 7160 RVA: 0x00071690 File Offset: 0x0006F890
		internal static Field[] XmlNodesToThisArray(XmlNodeList nodes)
		{
			if (nodes == null)
			{
				return null;
			}
			Field[] array = new Field[nodes.Count];
			int num = 0;
			foreach (object obj in nodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				array[num] = Field.XmlNodeToThis(xmlNode);
				num++;
			}
			return array;
		}

		// Token: 0x06001BF9 RID: 7161 RVA: 0x00071704 File Offset: 0x0006F904
		internal static Field XmlNodeToThis(XmlNode node)
		{
			if (node == null)
			{
				return null;
			}
			Field field = new Field();
			XmlNode xmlNode = node.SelectSingleNode("Alias");
			if (xmlNode != null)
			{
				field.Alias = xmlNode.InnerText;
			}
			XmlNode xmlNode2 = node.SelectSingleNode("Name");
			if (xmlNode2 != null)
			{
				field.Name = xmlNode2.InnerText;
			}
			return field;
		}

		// Token: 0x04000B77 RID: 2935
		public string Alias;

		// Token: 0x04000B78 RID: 2936
		public string Name;
	}
}
