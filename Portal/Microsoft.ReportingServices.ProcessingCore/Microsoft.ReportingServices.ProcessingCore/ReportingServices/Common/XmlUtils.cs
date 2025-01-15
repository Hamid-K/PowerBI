using System;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Schema;

namespace Microsoft.ReportingServices.Common
{
	// Token: 0x020005DC RID: 1500
	internal static class XmlUtils
	{
		// Token: 0x060053EF RID: 21487 RVA: 0x00161774 File Offset: 0x0015F974
		internal static XmlDocument CreateXmlDocumentWithNullResolver()
		{
			return new XmlDocument
			{
				XmlResolver = null
			};
		}

		// Token: 0x060053F0 RID: 21488 RVA: 0x00161782 File Offset: 0x0015F982
		internal static XmlSchema LoadSchemaFromResourceWithNullResolver(string resourceName)
		{
			return XmlSchema.Read(XmlUtils.CreateXmlReaderWithNullResolver(new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))), null);
		}

		// Token: 0x060053F1 RID: 21489 RVA: 0x001617A0 File Offset: 0x0015F9A0
		internal static void LoadWithNullResolver(this XmlDocument xmlDocument, Stream stream)
		{
			XmlReader xmlReader = XmlUtils.CreateXmlReaderWithNullResolver(new StreamReader(stream));
			xmlDocument.Load(xmlReader);
		}

		// Token: 0x060053F2 RID: 21490 RVA: 0x001617C0 File Offset: 0x0015F9C0
		internal static void LoadXmlWithNullResolver(this XmlDocument xmlDocument, string value)
		{
			XmlReader xmlReader = XmlUtils.CreateXmlReaderWithNullResolver(new StringReader(value));
			xmlDocument.Load(xmlReader);
		}

		// Token: 0x060053F3 RID: 21491 RVA: 0x001617E0 File Offset: 0x0015F9E0
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
