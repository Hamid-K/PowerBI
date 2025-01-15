using System;
using System.IO;
using System.Xml;

namespace Microsoft.Lucia.Xml
{
	// Token: 0x02000024 RID: 36
	public static class XmlReaderFactory
	{
		// Token: 0x0600009A RID: 154 RVA: 0x00003372 File Offset: 0x00001572
		public static XmlReader Create(TextReader reader, [Nullable] XmlReaderSettings settings = null)
		{
			return XmlReader.Create(reader, XmlReaderFactory.GetSafeSettings(settings));
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00003380 File Offset: 0x00001580
		public static XmlReader Create(Stream stream, [Nullable] XmlReaderSettings settings = null)
		{
			return XmlReaderFactory.Create(new StreamReader(stream), XmlReaderFactory.GetSafeSettings(settings));
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00003393 File Offset: 0x00001593
		public static XmlReader FromString(string xml, [Nullable] XmlReaderSettings settings = null)
		{
			return XmlReaderFactory.Create(new StringReader(xml), XmlReaderFactory.GetSafeSettings(settings));
		}

		// Token: 0x0600009D RID: 157 RVA: 0x000033A6 File Offset: 0x000015A6
		private static XmlReaderSettings GetSafeSettings([Nullable] XmlReaderSettings settings)
		{
			if (settings == null)
			{
				return XmlReaderFactory._defaultSafeSettings;
			}
			settings.DtdProcessing = DtdProcessing.Prohibit;
			settings.XmlResolver = null;
			return settings;
		}

		// Token: 0x0400004D RID: 77
		private static readonly XmlReaderSettings _defaultSafeSettings = XmlReaderFactory.GetSafeSettings(new XmlReaderSettings());
	}
}
