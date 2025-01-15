using System;
using System.IO;
using System.Xml;

namespace Microsoft.InfoNav.Common
{
	// Token: 0x0200007E RID: 126
	public static class XmlUtil
	{
		// Token: 0x060004B7 RID: 1207 RVA: 0x0000C542 File Offset: 0x0000A742
		public static XmlReaderSettings CreateSafeXmlReaderSettings()
		{
			return new XmlReaderSettings
			{
				DtdProcessing = DtdProcessing.Prohibit,
				XmlResolver = null
			};
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x0000C557 File Offset: 0x0000A757
		public static XmlReader CreateSafeXmlReader(Stream stream)
		{
			return XmlReader.Create(stream, XmlUtil.CreateSafeXmlReaderSettings());
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x0000C564 File Offset: 0x0000A764
		public static XmlReader CreateSafeXmlReader(string xmlString)
		{
			return XmlReader.Create(new StringReader(xmlString), XmlUtil.CreateSafeXmlReaderSettings());
		}
	}
}
