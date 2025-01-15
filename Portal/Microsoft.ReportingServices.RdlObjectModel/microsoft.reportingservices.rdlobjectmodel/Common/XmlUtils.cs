using System;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Schema;

namespace Microsoft.ReportingServices.Common
{
	// Token: 0x02000067 RID: 103
	internal static class XmlUtils
	{
		// Token: 0x060003DE RID: 990 RVA: 0x000163A4 File Offset: 0x000145A4
		internal static XmlDocument CreateXmlDocumentWithNullResolver()
		{
			return new XmlDocument
			{
				XmlResolver = null
			};
		}

		// Token: 0x060003DF RID: 991 RVA: 0x000163B2 File Offset: 0x000145B2
		internal static XmlSchema LoadSchemaFromResourceWithNullResolver(string resourceName)
		{
			return XmlSchema.Read(XmlUtils.CreateXmlReaderWithNullResolver(new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))), null);
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x000163D0 File Offset: 0x000145D0
		internal static void LoadWithNullResolver(this XmlDocument xmlDocument, Stream stream)
		{
			XmlReader xmlReader = XmlUtils.CreateXmlReaderWithNullResolver(new StreamReader(stream));
			xmlDocument.Load(xmlReader);
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x000163F0 File Offset: 0x000145F0
		internal static void LoadXmlWithNullResolver(this XmlDocument xmlDocument, string value)
		{
			XmlReader xmlReader = XmlUtils.CreateXmlReaderWithNullResolver(new StringReader(value));
			xmlDocument.Load(xmlReader);
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x00016410 File Offset: 0x00014610
		private static XmlReader CreateXmlReaderWithNullResolver(TextReader textReader)
		{
			return XmlReader.Create(textReader, new XmlReaderSettings
			{
				XmlResolver = null,
				DtdProcessing = DtdProcessing.Prohibit
			});
		}
	}
}
