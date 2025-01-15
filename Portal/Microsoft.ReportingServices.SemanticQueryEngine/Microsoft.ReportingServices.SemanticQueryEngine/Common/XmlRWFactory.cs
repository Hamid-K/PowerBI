using System;
using System.IO;
using System.Xml;

namespace Microsoft.ReportingServices.Common
{
	// Token: 0x0200007A RID: 122
	internal static class XmlRWFactory
	{
		// Token: 0x06000523 RID: 1315 RVA: 0x00015D01 File Offset: 0x00013F01
		internal static XmlReader CreateReader(Stream stream)
		{
			return XmlReader.Create(stream, XmlRWFactory.GetReaderSettings());
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x00015D0E File Offset: 0x00013F0E
		internal static XmlReader CreateReader(StringReader stringReader)
		{
			return XmlReader.Create(stringReader, XmlRWFactory.GetReaderSettings());
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x00015D1B File Offset: 0x00013F1B
		internal static XmlWriter CreateWriter(Stream stream)
		{
			return XmlWriter.Create(stream, XmlRWFactory.GetWriterSettings());
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x00015D28 File Offset: 0x00013F28
		internal static XmlReaderSettings GetReaderSettings()
		{
			return new XmlReaderSettings
			{
				CheckCharacters = false
			};
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x00015D36 File Offset: 0x00013F36
		internal static XmlWriterSettings GetWriterSettings()
		{
			return new XmlWriterSettings
			{
				CheckCharacters = false
			};
		}
	}
}
