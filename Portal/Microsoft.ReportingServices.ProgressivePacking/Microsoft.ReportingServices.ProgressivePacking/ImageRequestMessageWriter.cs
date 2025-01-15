using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace Microsoft.ReportingServices.ProgressivePackaging
{
	// Token: 0x02000005 RID: 5
	internal static class ImageRequestMessageWriter
	{
		// Token: 0x06000010 RID: 16 RVA: 0x0000217C File Offset: 0x0000037C
		public static void WriteElementsToStream(IEnumerable<ImageRequestMessageElement> messageElements, Stream s)
		{
			using (XmlWriter xmlWriter = XmlWriter.Create(s, new XmlWriterSettings
			{
				CheckCharacters = false,
				Encoding = MessageUtil.StringEncoding
			}))
			{
				xmlWriter.WriteStartDocument();
				xmlWriter.WriteStartElement("ClientRequest", "http://schemas.microsoft.com/sqlserver/reporting/2011/01/getexternalimages");
				xmlWriter.WriteStartElement("ExternalImages");
				foreach (ImageRequestMessageElement imageRequestMessageElement in messageElements)
				{
					imageRequestMessageElement.Write(xmlWriter);
				}
				xmlWriter.WriteEndElement();
				xmlWriter.WriteEndElement();
				xmlWriter.WriteEndDocument();
			}
		}
	}
}
