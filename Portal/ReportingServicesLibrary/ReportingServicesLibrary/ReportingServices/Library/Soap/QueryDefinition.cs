using System;
using System.Globalization;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.ReportingServices.Diagnostics;

namespace Microsoft.ReportingServices.Library.Soap
{
	// Token: 0x02000346 RID: 838
	public class QueryDefinition
	{
		// Token: 0x06001BFB RID: 7163 RVA: 0x00071753 File Offset: 0x0006F953
		public QueryDefinition()
		{
			this.CommandType = null;
			this.CommandText = null;
			this.Timeout = 0;
			this.TimeoutSpecified = false;
		}

		// Token: 0x06001BFC RID: 7164 RVA: 0x00071778 File Offset: 0x0006F978
		internal static void WriteToXml(QueryDefinition query, XmlTextWriter xml)
		{
			if (query == null)
			{
				return;
			}
			xml.WriteStartElement("Query");
			xml.WriteElementString("CommandType", query.CommandType);
			xml.WriteElementString("CommandText", query.CommandText);
			if (query.TimeoutSpecified)
			{
				xml.WriteElementString("Timeout", query.Timeout.ToString(Localization.CatalogCulture));
			}
			xml.WriteEndElement();
		}

		// Token: 0x06001BFD RID: 7165 RVA: 0x000717E0 File Offset: 0x0006F9E0
		internal static QueryDefinition XmlNodeToThis(XmlNode node)
		{
			if (node == null)
			{
				return null;
			}
			QueryDefinition queryDefinition = new QueryDefinition();
			XmlNode xmlNode = node.SelectSingleNode("CommandType");
			if (xmlNode != null)
			{
				queryDefinition.CommandType = xmlNode.InnerText;
			}
			XmlNode xmlNode2 = node.SelectSingleNode("CommandText");
			if (xmlNode2 != null)
			{
				queryDefinition.CommandText = xmlNode2.InnerText;
			}
			XmlNode xmlNode3 = node.SelectSingleNode("Timeout");
			if (xmlNode3 != null)
			{
				queryDefinition.Timeout = int.Parse(xmlNode3.InnerText, CultureInfo.InvariantCulture);
				queryDefinition.TimeoutSpecified = true;
			}
			return queryDefinition;
		}

		// Token: 0x04000B79 RID: 2937
		public string CommandType;

		// Token: 0x04000B7A RID: 2938
		public string CommandText;

		// Token: 0x04000B7B RID: 2939
		public int Timeout;

		// Token: 0x04000B7C RID: 2940
		[XmlIgnore]
		public bool TimeoutSpecified;
	}
}
