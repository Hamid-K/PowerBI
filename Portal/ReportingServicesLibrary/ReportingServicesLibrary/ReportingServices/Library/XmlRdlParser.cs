using System;
using System.Globalization;
using System.IO;
using System.Xml;
using Microsoft.ReportingServices.Common;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000067 RID: 103
	internal static class XmlRdlParser
	{
		// Token: 0x06000429 RID: 1065 RVA: 0x00012574 File Offset: 0x00010774
		public static string[] GetValues(byte[] xmlDataSetbytes, string columnName, XmlRdlParserMode mode, int maxRows = 0)
		{
			XmlDocument xmlDocument = XmlRdlParser.CreateDataSetXmlDocument(xmlDataSetbytes);
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(xmlDocument.NameTable);
			xmlNamespaceManager.AddNamespace(XmlRdlParser.NamespaceString, xmlDocument.DocumentElement.NamespaceURI);
			if (mode == XmlRdlParserMode.Aggregation)
			{
				return new string[] { xmlDocument.ChildNodes[1].FirstChild.InnerText };
			}
			XmlNode xmlNode = xmlDocument.SelectSingleNode(XmlRdlParser.SelectFirstRowString, xmlNamespaceManager);
			int num = XmlRdlParser.FindColumn(xmlNode, columnName);
			if (num == -1)
			{
				return null;
			}
			string[] array = null;
			if (mode == XmlRdlParserMode.Downsample)
			{
				XmlNodeList xmlNodeList = xmlDocument.SelectNodes(string.Format(CultureInfo.InvariantCulture, XmlRdlParser.SelectColumnNodeTextStringFormat, num), xmlNamespaceManager);
				int num2 = ((xmlNodeList.Count % maxRows == 0) ? (xmlNodeList.Count / maxRows) : (xmlNodeList.Count / maxRows + 1));
				double?[] array2 = new double?[Math.Min(maxRows, xmlNodeList.Count)];
				for (int i = 0; i < xmlNodeList.Count; i += num2)
				{
					if (string.IsNullOrEmpty(xmlNodeList[i].Value))
					{
						array2[i] = null;
					}
					else
					{
						array2[i] = new double?(Convert.ToDouble(xmlNodeList[i].Value, CultureInfo.InvariantCulture));
					}
				}
				array = ResamplingUtils.LttbDownsampleToStrArray(array2, 30);
			}
			else if (mode == XmlRdlParserMode.First)
			{
				array = new string[] { xmlNode.SelectSingleNode(string.Format(CultureInfo.InvariantCulture, XmlRdlParser.SelectColumnFirstNodeTextStringFormat, num), xmlNamespaceManager).Value };
			}
			return array;
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x000126EC File Offset: 0x000108EC
		private static XmlDocument CreateDataSetXmlDocument(byte[] xmlBytes)
		{
			XmlDocument xmlDocument = new XmlDocument();
			using (MemoryStream memoryStream = new MemoryStream(xmlBytes, false))
			{
				using (XmlReader xmlReader = XmlReader.Create(memoryStream, null))
				{
					xmlDocument.Load(xmlReader);
				}
			}
			return xmlDocument;
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x0001274C File Offset: 0x0001094C
		private static int FindColumn(XmlNode firstRow, string columnName)
		{
			if (firstRow != null)
			{
				for (int i = 0; i < firstRow.ChildNodes.Count; i++)
				{
					string name = firstRow.ChildNodes[i].Name;
					if (name.Length >= 22 && string.Equals(name.Substring(13, name.Length - 22), columnName, StringComparison.Ordinal))
					{
						return i + 1;
					}
				}
			}
			return -1;
		}

		// Token: 0x04000200 RID: 512
		private static readonly string NamespaceString = "x";

		// Token: 0x04000201 RID: 513
		private static readonly string SelectFirstRowString = "/x:Report//x:Details[1]";

		// Token: 0x04000202 RID: 514
		private static readonly string SelectColumnNodeTextStringFormat = "/x:Report//x:Details/*[{0}]/text()";

		// Token: 0x04000203 RID: 515
		private static readonly string SelectColumnFirstNodeTextStringFormat = "./*[{0}]/text()";
	}
}
