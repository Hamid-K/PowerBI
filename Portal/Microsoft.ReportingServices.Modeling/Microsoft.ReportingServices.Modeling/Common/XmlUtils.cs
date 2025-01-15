using System;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Schema;

namespace Microsoft.ReportingServices.Common
{
	// Token: 0x02000010 RID: 16
	internal static class XmlUtils
	{
		// Token: 0x0600005A RID: 90 RVA: 0x00002D7C File Offset: 0x00000F7C
		internal static XmlDocument CreateXmlDocumentWithNullResolver()
		{
			return new XmlDocument
			{
				XmlResolver = null
			};
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002D8A File Offset: 0x00000F8A
		internal static XmlSchema LoadSchemaFromResourceWithNullResolver(string resourceName)
		{
			return XmlSchema.Read(XmlUtils.CreateXmlReaderWithNullResolver(new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))), null);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002DA8 File Offset: 0x00000FA8
		internal static void LoadWithNullResolver(this XmlDocument xmlDocument, Stream stream)
		{
			XmlReader xmlReader = XmlUtils.CreateXmlReaderWithNullResolver(new StreamReader(stream));
			xmlDocument.Load(xmlReader);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002DC8 File Offset: 0x00000FC8
		internal static void LoadXmlWithNullResolver(this XmlDocument xmlDocument, string value)
		{
			XmlReader xmlReader = XmlUtils.CreateXmlReaderWithNullResolver(new StringReader(value));
			xmlDocument.Load(xmlReader);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002DE8 File Offset: 0x00000FE8
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
