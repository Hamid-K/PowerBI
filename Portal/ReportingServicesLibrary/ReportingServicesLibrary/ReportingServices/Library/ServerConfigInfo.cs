using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml;
using Microsoft.ReportingServices.Diagnostics;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200000F RID: 15
	[Serializable]
	internal class ServerConfigInfo
	{
		// Token: 0x06000015 RID: 21 RVA: 0x000023FC File Offset: 0x000005FC
		internal static string ThisArrayToXml(ServerConfigInfo[] serverConfigInfo)
		{
			if (serverConfigInfo == null)
			{
				return null;
			}
			StringWriter stringWriter = new StringWriter(Localization.CatalogCulture);
			XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);
			xmlTextWriter.WriteStartElement("ServerConfigInfo");
			for (int i = 0; i < serverConfigInfo.Length; i++)
			{
				if (serverConfigInfo[i] != null)
				{
					xmlTextWriter.WriteStartElement("Server");
					if (serverConfigInfo[i].MachineName != null)
					{
						xmlTextWriter.WriteElementString("MachineName", serverConfigInfo[i].MachineName);
					}
					if (serverConfigInfo[i].InstanceName != null)
					{
						xmlTextWriter.WriteElementString("InstanceName", serverConfigInfo[i].InstanceName);
					}
					if (serverConfigInfo[i].ServiceAccountName != null)
					{
						xmlTextWriter.WriteElementString("ServiceAccountName", serverConfigInfo[i].ServiceAccountName);
					}
					if (serverConfigInfo[i].ReportServerUrlItem != null)
					{
						xmlTextWriter.WriteElementString("ReportServerUrlItem", serverConfigInfo[i].ReportServerUrlItem);
					}
					xmlTextWriter.WriteEndElement();
				}
			}
			xmlTextWriter.WriteEndElement();
			xmlTextWriter.Flush();
			xmlTextWriter.Close();
			return stringWriter.ToString();
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000024E4 File Offset: 0x000006E4
		internal static IList<ServerConfigInfo> XmlToList(string serverConfigInfoXml)
		{
			if (string.IsNullOrEmpty(serverConfigInfoXml))
			{
				return null;
			}
			XmlDocument xmlDocument = new XmlDocument();
			XmlUtil.SafeOpenXmlDocumentString(xmlDocument, serverConfigInfoXml);
			string text = string.Format(CultureInfo.InvariantCulture, "/{0}/{1}", "ServerConfigInfo", "Server");
			List<ServerConfigInfo> list = new List<ServerConfigInfo>();
			XmlNodeList xmlNodeList = xmlDocument.SelectNodes(text);
			if (xmlNodeList != null)
			{
				foreach (object obj in xmlNodeList)
				{
					XmlNode xmlNode = (XmlNode)obj;
					list.Add(new ServerConfigInfo
					{
						MachineName = ServerConfigInfo.GetServerNodeValue(xmlNode, "MachineName"),
						InstanceName = ServerConfigInfo.GetServerNodeValue(xmlNode, "InstanceName"),
						ServiceAccountName = ServerConfigInfo.GetServerNodeValue(xmlNode, "ServiceAccountName"),
						ReportServerUrlItem = ServerConfigInfo.GetServerNodeValue(xmlNode, "ReportServerUrlItem")
					});
				}
			}
			return list;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000025D4 File Offset: 0x000007D4
		private static string GetServerNodeValue(XmlNode serverNode, string requestedElement)
		{
			XmlNode xmlNode = serverNode.SelectSingleNode(requestedElement);
			if (xmlNode == null)
			{
				return null;
			}
			return xmlNode.InnerText;
		}

		// Token: 0x04000079 RID: 121
		private const string RootElementName = "ServerConfigInfo";

		// Token: 0x0400007A RID: 122
		private const string ServerElementName = "Server";

		// Token: 0x0400007B RID: 123
		private const string MachineElementName = "MachineName";

		// Token: 0x0400007C RID: 124
		private const string InstanceElementName = "InstanceName";

		// Token: 0x0400007D RID: 125
		private const string ServiceAccountElementName = "ServiceAccountName";

		// Token: 0x0400007E RID: 126
		private const string ReportServerUrlElementName = "ReportServerUrlItem";

		// Token: 0x0400007F RID: 127
		public string MachineName;

		// Token: 0x04000080 RID: 128
		public string InstanceName;

		// Token: 0x04000081 RID: 129
		public string ServiceAccountName;

		// Token: 0x04000082 RID: 130
		public string ReportServerUrlItem;
	}
}
