using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000303 RID: 771
	internal class SafeXmlSchema : XmlSchema
	{
		// Token: 0x06001B78 RID: 7032 RVA: 0x00049780 File Offset: 0x00048780
		public static XmlSchema Read(Stream stream, ValidationEventHandler validationEventHandler)
		{
			XmlSchema xmlSchema;
			using (XmlReader xmlReader = XmlReader.Create(stream, SafeXmlSchema.defaultSettings))
			{
				xmlSchema = XmlSchema.Read(xmlReader, validationEventHandler);
			}
			return xmlSchema;
		}

		// Token: 0x06001B79 RID: 7033 RVA: 0x000497C0 File Offset: 0x000487C0
		public static XmlSchema Read(TextReader reader, ValidationEventHandler validationEventHandler)
		{
			XmlSchema xmlSchema;
			using (XmlReader xmlReader = XmlReader.Create(reader, SafeXmlSchema.defaultSettings))
			{
				xmlSchema = XmlSchema.Read(xmlReader, validationEventHandler);
			}
			return xmlSchema;
		}

		// Token: 0x06001B7A RID: 7034 RVA: 0x00049800 File Offset: 0x00048800
		public static XmlSchema Read(XmlReader reader, ValidationEventHandler validationEventHandler)
		{
			if (reader.Settings != null && !reader.Settings.ProhibitDtd)
			{
				throw new XmlDtdException();
			}
			return XmlSchema.Read(reader, validationEventHandler);
		}

		// Token: 0x04001456 RID: 5206
		private static XmlReaderSettings defaultSettings = new XmlReaderSettings
		{
			ProhibitDtd = true,
			XmlResolver = null
		};
	}
}
