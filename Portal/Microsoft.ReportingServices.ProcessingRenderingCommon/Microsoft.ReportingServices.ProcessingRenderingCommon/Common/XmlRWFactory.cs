using System;
using System.IO;
using System.Xml;

namespace Microsoft.ReportingServices.Common
{
	// Token: 0x0200000C RID: 12
	internal static class XmlRWFactory
	{
		// Token: 0x0600007C RID: 124 RVA: 0x000044E8 File Offset: 0x000026E8
		internal static XmlReader CreateReader(Stream stream)
		{
			return XmlReader.Create(stream, XmlRWFactory.GetReaderSettings());
		}

		// Token: 0x0600007D RID: 125 RVA: 0x000044F5 File Offset: 0x000026F5
		internal static XmlReader CreateReader(StringReader stringReader)
		{
			return XmlReader.Create(stringReader, XmlRWFactory.GetReaderSettings());
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00004502 File Offset: 0x00002702
		internal static XmlWriter CreateWriter(Stream stream)
		{
			return XmlWriter.Create(stream, XmlRWFactory.GetWriterSettings());
		}

		// Token: 0x0600007F RID: 127 RVA: 0x0000450F File Offset: 0x0000270F
		internal static XmlReaderSettings GetReaderSettings()
		{
			return new XmlReaderSettings
			{
				CheckCharacters = false
			};
		}

		// Token: 0x06000080 RID: 128 RVA: 0x0000451D File Offset: 0x0000271D
		internal static XmlWriterSettings GetWriterSettings()
		{
			return new XmlWriterSettings
			{
				CheckCharacters = false
			};
		}
	}
}
