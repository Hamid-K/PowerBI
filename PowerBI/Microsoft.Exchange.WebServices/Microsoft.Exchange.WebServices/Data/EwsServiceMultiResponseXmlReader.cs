using System;
using System.IO;
using System.Xml;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020000CF RID: 207
	internal class EwsServiceMultiResponseXmlReader : EwsServiceXmlReader
	{
		// Token: 0x06000966 RID: 2406 RVA: 0x0001E6F0 File Offset: 0x0001D6F0
		private EwsServiceMultiResponseXmlReader(Stream stream, ExchangeService service)
			: base(stream, service)
		{
		}

		// Token: 0x06000967 RID: 2407 RVA: 0x0001E6FC File Offset: 0x0001D6FC
		internal static EwsServiceMultiResponseXmlReader Create(Stream stream, ExchangeService service)
		{
			return new EwsServiceMultiResponseXmlReader(stream, service);
		}

		// Token: 0x06000968 RID: 2408 RVA: 0x0001E714 File Offset: 0x0001D714
		private static XmlReader CreateXmlReader(Stream stream)
		{
			XmlReaderSettings xmlReaderSettings = new XmlReaderSettings
			{
				ConformanceLevel = 0,
				ProhibitDtd = true,
				IgnoreComments = true,
				IgnoreProcessingInstructions = true,
				IgnoreWhitespace = true,
				XmlResolver = null
			};
			return XmlReader.Create(stream, xmlReaderSettings);
		}

		// Token: 0x06000969 RID: 2409 RVA: 0x0001E75A File Offset: 0x0001D75A
		protected override XmlReader InitializeXmlReader(Stream stream)
		{
			return EwsServiceMultiResponseXmlReader.CreateXmlReader(stream);
		}
	}
}
