using System;
using System.Globalization;
using System.Text;
using System.Xml;

namespace Microsoft.ReportingServices.Common
{
	// Token: 0x020000D3 RID: 211
	internal static class SoapUtil
	{
		// Token: 0x06000497 RID: 1175 RVA: 0x00008314 File Offset: 0x00006514
		internal static string RemoveInvalidXmlChars(string origText)
		{
			if (string.IsNullOrEmpty(origText))
			{
				return origText;
			}
			string text = origText;
			try
			{
				bool flag = false;
				StringBuilder stringBuilder = new StringBuilder(origText);
				for (int i = 0; i < stringBuilder.Length; i++)
				{
					char c = stringBuilder[i];
					if (c > '\ufffd' || (c < ' ' && c != '\t' && c != '\n' && c != '\r'))
					{
						stringBuilder[i] = ' ';
						flag = true;
					}
				}
				if (flag)
				{
					text = stringBuilder.ToString();
				}
			}
			catch (Exception)
			{
			}
			return text;
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x0000839C File Offset: 0x0000659C
		internal static XmlNode CreateExceptionDetailsNode(XmlDocument doc, string code, string detailedMsg, string helpLink, string productName, string productVersion, int productLocaleId, string operatingSystem, int countryLocaleId)
		{
			XmlNode xmlNode = doc.CreateNode(XmlNodeType.Element, "detail", "");
			XmlNode xmlNode2 = SoapUtil.CreateNode(doc, "ErrorCode");
			xmlNode2.InnerText = code;
			xmlNode.AppendChild(xmlNode2);
			XmlNode xmlNode3 = SoapUtil.CreateNode(doc, "HttpStatus");
			xmlNode3.InnerText = "400";
			xmlNode.AppendChild(xmlNode3);
			XmlNode xmlNode4 = SoapUtil.CreateNode(doc, "Message");
			xmlNode4.InnerText = detailedMsg;
			xmlNode.AppendChild(xmlNode4);
			XmlNode xmlNode5 = SoapUtil.CreateNode(doc, "HelpLink");
			xmlNode5.InnerText = helpLink;
			xmlNode.AppendChild(xmlNode5);
			XmlNode xmlNode6 = SoapUtil.CreateNode(doc, "ProductName");
			xmlNode6.InnerText = productName;
			xmlNode.AppendChild(xmlNode6);
			XmlNode xmlNode7 = SoapUtil.CreateNode(doc, "ProductVersion");
			xmlNode7.InnerText = productVersion;
			xmlNode.AppendChild(xmlNode7);
			XmlNode xmlNode8 = SoapUtil.CreateNode(doc, "ProductLocaleId");
			xmlNode8.InnerText = productLocaleId.ToString(CultureInfo.InvariantCulture);
			xmlNode.AppendChild(xmlNode8);
			XmlNode xmlNode9 = SoapUtil.CreateNode(doc, "OperatingSystem");
			xmlNode9.InnerText = operatingSystem;
			xmlNode.AppendChild(xmlNode9);
			XmlNode xmlNode10 = SoapUtil.CreateNode(doc, "CountryLocaleId");
			xmlNode10.InnerText = countryLocaleId.ToString(CultureInfo.InvariantCulture);
			xmlNode.AppendChild(xmlNode10);
			return xmlNode;
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x000084D9 File Offset: 0x000066D9
		internal static XmlNode CreateNode(XmlDocument doc, string name)
		{
			return doc.CreateNode(XmlNodeType.Element, name, "http://www.microsoft.com/sql/reportingservices");
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x000084E8 File Offset: 0x000066E8
		internal static XmlNode CreateWarningNode(XmlDocument doc)
		{
			return SoapUtil.CreateNode(doc, "Warnings");
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x000084F5 File Offset: 0x000066F5
		internal static XmlNode CreateWarningCodeNode(XmlDocument doc)
		{
			return SoapUtil.CreateNode(doc, "Code");
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x00008502 File Offset: 0x00006702
		internal static XmlNode CreateWarningSeverityNode(XmlDocument doc)
		{
			return SoapUtil.CreateNode(doc, "Severity");
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x0000850F File Offset: 0x0000670F
		internal static XmlNode CreateWarningObjectNameNode(XmlDocument doc)
		{
			return SoapUtil.CreateNode(doc, "ObjectName");
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x0000851C File Offset: 0x0000671C
		internal static XmlNode CreateWarningObjectTypeNode(XmlDocument doc)
		{
			return SoapUtil.CreateNode(doc, "ObjectType");
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x00008529 File Offset: 0x00006729
		internal static XmlNode CreateWarningMessageNode(XmlDocument doc)
		{
			return SoapUtil.CreateNode(doc, "Message");
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x00008536 File Offset: 0x00006736
		internal static XmlNode CreateMoreInfoNode(XmlDocument doc)
		{
			return SoapUtil.CreateNode(doc, "MoreInformation");
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x00008543 File Offset: 0x00006743
		internal static XmlNode CreateMoreInfoSourceNode(XmlDocument doc)
		{
			return SoapUtil.CreateNode(doc, "Source");
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x00008550 File Offset: 0x00006750
		internal static XmlNode CreateMoreInfoMessageNode(XmlDocument doc)
		{
			return SoapUtil.CreateNode(doc, "Message");
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x0000855D File Offset: 0x0000675D
		internal static XmlAttribute CreateErrorCodeAttr(XmlDocument doc)
		{
			return doc.CreateAttribute("msrs", "ErrorCode", "http://www.microsoft.com/sql/reportingservices");
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x00008574 File Offset: 0x00006774
		internal static XmlAttribute CreateHelpLinkTagAttr(XmlDocument doc)
		{
			return doc.CreateAttribute("msrs", "HelpLink", "http://www.microsoft.com/sql/reportingservices");
		}

		// Token: 0x04000061 RID: 97
		internal const string DefaultServerErrorNamespace = "http://www.microsoft.com/sql/reportingservices";

		// Token: 0x04000062 RID: 98
		internal const string DefaultNamespacePrefix = "msrs";

		// Token: 0x04000063 RID: 99
		internal const string HelpLinkFormat = "https://go.microsoft.com/fwlink/?LinkId=20476&EvtSrc={0}&EvtID={1}&ProdName=Microsoft%20SQL%20Server%20Reporting%20Services&ProdVer={2}";

		// Token: 0x04000064 RID: 100
		internal const string HelpLinkTag = "HelpLink";

		// Token: 0x04000065 RID: 101
		internal const string XmlMoreInfoElement = "MoreInformation";

		// Token: 0x04000066 RID: 102
		internal const string XmlMoreInfoSource = "Source";

		// Token: 0x04000067 RID: 103
		internal const string XmlMoreInfoMessage = "Message";

		// Token: 0x04000068 RID: 104
		internal const string XmlErrorCode = "ErrorCode";

		// Token: 0x04000069 RID: 105
		internal const string XmlWarningsElement = "Warnings";

		// Token: 0x0400006A RID: 106
		internal const string XmlWarningElement = "Warning";

		// Token: 0x0400006B RID: 107
		internal const string XmlWarningCodeElement = "Code";

		// Token: 0x0400006C RID: 108
		internal const string XmlWarningSeverityElement = "Severity";

		// Token: 0x0400006D RID: 109
		internal const string XmlWarningObjectNameElement = "ObjectName";

		// Token: 0x0400006E RID: 110
		internal const string XmlWarningObjectTypeElement = "ObjectType";

		// Token: 0x0400006F RID: 111
		internal const string XmlWarningMessageElement = "Message";

		// Token: 0x04000070 RID: 112
		private const string SoapExceptionDetailElementNameName = "detail";

		// Token: 0x04000071 RID: 113
		private const string SoapExceptionDetailElementNameNamespace = "";

		// Token: 0x04000072 RID: 114
		internal const string SoapExceptionHttpStatus = "400";
	}
}
