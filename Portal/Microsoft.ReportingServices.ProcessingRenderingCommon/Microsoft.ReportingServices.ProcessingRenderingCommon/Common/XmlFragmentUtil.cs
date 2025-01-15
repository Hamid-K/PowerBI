using System;
using System.IO;
using System.Text;
using System.Xml;

namespace Microsoft.ReportingServices.Common
{
	// Token: 0x0200000D RID: 13
	internal static class XmlFragmentUtil
	{
		// Token: 0x06000081 RID: 129 RVA: 0x0000452B File Offset: 0x0000272B
		public static XmlReader ReadXmlString(string xml)
		{
			return XmlRWFactory.CreateReader(new StringReader(xml));
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00004538 File Offset: 0x00002738
		public static string ToXmlString(Action<XmlWriter> writeTo)
		{
			return XmlFragmentUtil.ToXmlString(XmlFragmentUtil.m_xmlWriterSettings, writeTo);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00004548 File Offset: 0x00002748
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

		// Token: 0x06000084 RID: 132 RVA: 0x00004594 File Offset: 0x00002794
		private static XmlWriterSettings CreateXmlWriterSettings()
		{
			XmlWriterSettings writerSettings = XmlRWFactory.GetWriterSettings();
			writerSettings.OmitXmlDeclaration = true;
			return writerSettings;
		}

		// Token: 0x04000077 RID: 119
		private static XmlWriterSettings m_xmlWriterSettings = XmlFragmentUtil.CreateXmlWriterSettings();
	}
}
