using System;
using System.IO;
using System.Text;
using System.Xml;

namespace Microsoft.ReportingServices.Common
{
	// Token: 0x0200007B RID: 123
	internal static class XmlFragmentUtil
	{
		// Token: 0x06000528 RID: 1320 RVA: 0x00015D44 File Offset: 0x00013F44
		public static XmlReader ReadXmlString(string xml)
		{
			return XmlRWFactory.CreateReader(new StringReader(xml));
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x00015D51 File Offset: 0x00013F51
		public static string ToXmlString(Action<XmlWriter> writeTo)
		{
			return XmlFragmentUtil.ToXmlString(XmlFragmentUtil.m_xmlWriterSettings, writeTo);
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x00015D60 File Offset: 0x00013F60
		public static string ToXmlString(XmlWriterSettings settings, Action<XmlWriter> writeTo)
		{
			StringBuilder stringBuilder = new StringBuilder();
			using (XmlWriter xmlWriter = XmlWriter.Create(stringBuilder, settings))
			{
				writeTo(xmlWriter);
				xmlWriter.Flush();
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x00015DAC File Offset: 0x00013FAC
		private static XmlWriterSettings CreateXmlWriterSettings()
		{
			XmlWriterSettings writerSettings = XmlRWFactory.GetWriterSettings();
			writerSettings.OmitXmlDeclaration = true;
			return writerSettings;
		}

		// Token: 0x0400020E RID: 526
		private static XmlWriterSettings m_xmlWriterSettings = XmlFragmentUtil.CreateXmlWriterSettings();
	}
}
