using System;
using System.IO;
using System.Text;
using System.Xml;

namespace Microsoft.ReportingServices.Common
{
	// Token: 0x02000370 RID: 880
	internal static class XmlFragmentUtil
	{
		// Token: 0x06001CD4 RID: 7380 RVA: 0x00074058 File Offset: 0x00072258
		public static XmlReader ReadXmlString(string xml)
		{
			return XmlRWFactory.CreateReader(new StringReader(xml));
		}

		// Token: 0x06001CD5 RID: 7381 RVA: 0x00074065 File Offset: 0x00072265
		public static string ToXmlString(Action<XmlWriter> writeTo)
		{
			return XmlFragmentUtil.ToXmlString(XmlFragmentUtil.m_xmlWriterSettings, writeTo);
		}

		// Token: 0x06001CD6 RID: 7382 RVA: 0x00074074 File Offset: 0x00072274
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

		// Token: 0x06001CD7 RID: 7383 RVA: 0x000740C0 File Offset: 0x000722C0
		private static XmlWriterSettings CreateXmlWriterSettings()
		{
			XmlWriterSettings writerSettings = XmlRWFactory.GetWriterSettings();
			writerSettings.OmitXmlDeclaration = true;
			return writerSettings;
		}

		// Token: 0x04000BDD RID: 3037
		private static XmlWriterSettings m_xmlWriterSettings = XmlFragmentUtil.CreateXmlWriterSettings();
	}
}
