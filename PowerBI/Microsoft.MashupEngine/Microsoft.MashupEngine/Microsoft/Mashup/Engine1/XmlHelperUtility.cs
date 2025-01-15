using System;
using System.IO;
using System.Xml;

namespace Microsoft.Mashup.Engine1
{
	// Token: 0x0200022E RID: 558
	public class XmlHelperUtility
	{
		// Token: 0x06000BC2 RID: 3010 RVA: 0x0001B8E8 File Offset: 0x00019AE8
		public static XmlDocument CreateXmlDocument()
		{
			return new XmlDocument
			{
				XmlResolver = null
			};
		}

		// Token: 0x06000BC3 RID: 3011 RVA: 0x0001B8F8 File Offset: 0x00019AF8
		public static XmlReader XmlReaderCreate(Stream stream)
		{
			return XmlHelperUtility.XmlReaderCreate(stream, new XmlReaderSettings
			{
				DtdProcessing = DtdProcessing.Prohibit
			});
		}

		// Token: 0x06000BC4 RID: 3012 RVA: 0x0001B91C File Offset: 0x00019B1C
		public static XmlReader XmlReaderCreate(TextReader reader)
		{
			return XmlHelperUtility.XmlReaderCreate(reader, new XmlReaderSettings
			{
				DtdProcessing = DtdProcessing.Prohibit
			});
		}

		// Token: 0x06000BC5 RID: 3013 RVA: 0x0001B93D File Offset: 0x00019B3D
		public static XmlReader XmlReaderCreate(Stream stream, XmlReaderSettings readerSettings)
		{
			readerSettings.XmlResolver = null;
			return XmlReader.Create(stream, readerSettings);
		}

		// Token: 0x06000BC6 RID: 3014 RVA: 0x0001B94D File Offset: 0x00019B4D
		public static XmlReader XmlReaderCreate(TextReader reader, XmlReaderSettings readerSettings)
		{
			readerSettings.XmlResolver = null;
			return XmlReader.Create(reader, readerSettings);
		}
	}
}
