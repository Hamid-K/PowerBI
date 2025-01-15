using System;
using System.IO;
using System.Xml;

namespace Microsoft.ReportingServices.Common
{
	// Token: 0x0200036F RID: 879
	internal static class XmlRWFactory
	{
		// Token: 0x06001CCF RID: 7375 RVA: 0x00074015 File Offset: 0x00072215
		internal static XmlReader CreateReader(Stream stream)
		{
			return XmlReader.Create(stream, XmlRWFactory.GetReaderSettings());
		}

		// Token: 0x06001CD0 RID: 7376 RVA: 0x00074022 File Offset: 0x00072222
		internal static XmlReader CreateReader(StringReader stringReader)
		{
			return XmlReader.Create(stringReader, XmlRWFactory.GetReaderSettings());
		}

		// Token: 0x06001CD1 RID: 7377 RVA: 0x0007402F File Offset: 0x0007222F
		internal static XmlWriter CreateWriter(Stream stream)
		{
			return XmlWriter.Create(stream, XmlRWFactory.GetWriterSettings());
		}

		// Token: 0x06001CD2 RID: 7378 RVA: 0x0007403C File Offset: 0x0007223C
		internal static XmlReaderSettings GetReaderSettings()
		{
			return new XmlReaderSettings
			{
				CheckCharacters = false
			};
		}

		// Token: 0x06001CD3 RID: 7379 RVA: 0x0007404A File Offset: 0x0007224A
		internal static XmlWriterSettings GetWriterSettings()
		{
			return new XmlWriterSettings
			{
				CheckCharacters = false
			};
		}
	}
}
