using System;
using System.IO;
using System.Xml;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Xml
{
	// Token: 0x0200028E RID: 654
	internal static class XmlModuleHelper
	{
		// Token: 0x06001A9A RID: 6810 RVA: 0x000359F0 File Offset: 0x00033BF0
		public static XmlDocument OpenContents(Value contents, Value encoding)
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.XmlResolver = null;
			XmlDocument xmlDocument2;
			using (XmlReader xmlReader = XmlModuleHelper.CreateReader(contents, encoding))
			{
				xmlDocument.Load(xmlReader);
				xmlDocument2 = xmlDocument;
			}
			return xmlDocument2;
		}

		// Token: 0x06001A9B RID: 6811 RVA: 0x00035A38 File Offset: 0x00033C38
		public static XmlReader CreateReader(Value contents, Value encoding)
		{
			long num;
			return XmlModuleHelper.CreateReader(contents, encoding, out num);
		}

		// Token: 0x06001A9C RID: 6812 RVA: 0x00035A4E File Offset: 0x00033C4E
		public static XmlReader OpenContentsForReading(Value contents, Value encoding, out long contentLength)
		{
			XmlReader xmlReader = XmlModuleHelper.CreateReader(contents, encoding, out contentLength);
			xmlReader.MoveToContent();
			return xmlReader;
		}

		// Token: 0x06001A9D RID: 6813 RVA: 0x00035A60 File Offset: 0x00033C60
		private static XmlReader CreateReader(Value contents, Value encoding, out long contentLength)
		{
			XmlReaderSettings defaultXmlReaderSettings = XmlModuleHelper.DefaultXmlReaderSettings;
			defaultXmlReaderSettings.XmlResolver = null;
			if (contents.IsBinary)
			{
				BinaryValue asBinary = contents.AsBinary;
				if (!asBinary.TryGetLength(out contentLength))
				{
					contentLength = long.MaxValue;
				}
				return XmlReader.Create(asBinary.OpenText(encoding), defaultXmlReaderSettings);
			}
			string asString = contents.AsString;
			contentLength = (long)(asString.Length * 2);
			return XmlReader.Create(new StringReader(asString), defaultXmlReaderSettings);
		}

		// Token: 0x06001A9E RID: 6814 RVA: 0x00035AC8 File Offset: 0x00033CC8
		public static string XmlDocumentKey(Value contents, Value encoding)
		{
			string text;
			string sourceKeyOrDigest = DataSource.GetSourceKeyOrDigest(contents, out text);
			string text2 = (encoding.IsNull ? "*" : encoding.AsNumber.ToString());
			return PersistentCacheKey.XmlText.Qualify(text, text2, sourceKeyOrDigest);
		}

		// Token: 0x040007EA RID: 2026
		public static readonly XmlReaderSettings DefaultXmlReaderSettings = new XmlReaderSettings
		{
			CloseInput = true,
			IgnoreWhitespace = true,
			IgnoreComments = true,
			IgnoreProcessingInstructions = true,
			DtdProcessing = DtdProcessing.Parse,
			XmlResolver = null,
			MaxCharactersFromEntities = 1024L
		};
	}
}
