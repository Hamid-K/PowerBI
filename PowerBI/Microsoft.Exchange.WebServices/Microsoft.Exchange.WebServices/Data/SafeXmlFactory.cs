using System;
using System.IO;
using System.Xml;
using System.Xml.XPath;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000302 RID: 770
	internal class SafeXmlFactory
	{
		// Token: 0x06001B64 RID: 7012 RVA: 0x00049448 File Offset: 0x00048448
		public static XmlTextReader CreateSafeXmlTextReader(Stream stream)
		{
			return new XmlTextReader(stream)
			{
				ProhibitDtd = true,
				XmlResolver = null
			};
		}

		// Token: 0x06001B65 RID: 7013 RVA: 0x0004946C File Offset: 0x0004846C
		public static XmlTextReader CreateSafeXmlTextReader(string url)
		{
			return new XmlTextReader(url)
			{
				ProhibitDtd = true,
				XmlResolver = null
			};
		}

		// Token: 0x06001B66 RID: 7014 RVA: 0x00049490 File Offset: 0x00048490
		public static XmlTextReader CreateSafeXmlTextReader(TextReader input)
		{
			return new XmlTextReader(input)
			{
				ProhibitDtd = true,
				XmlResolver = null
			};
		}

		// Token: 0x06001B67 RID: 7015 RVA: 0x000494B4 File Offset: 0x000484B4
		public static XmlTextReader CreateSafeXmlTextReader(Stream input, XmlNameTable nt)
		{
			return new XmlTextReader(input, nt)
			{
				ProhibitDtd = true,
				XmlResolver = null
			};
		}

		// Token: 0x06001B68 RID: 7016 RVA: 0x000494D8 File Offset: 0x000484D8
		public static XmlTextReader CreateSafeXmlTextReader(string url, Stream input)
		{
			return new XmlTextReader(url, input)
			{
				ProhibitDtd = true,
				XmlResolver = null
			};
		}

		// Token: 0x06001B69 RID: 7017 RVA: 0x000494FC File Offset: 0x000484FC
		public static XmlTextReader CreateSafeXmlTextReader(string url, TextReader input)
		{
			return new XmlTextReader(url, input)
			{
				ProhibitDtd = true,
				XmlResolver = null
			};
		}

		// Token: 0x06001B6A RID: 7018 RVA: 0x00049520 File Offset: 0x00048520
		public static XmlTextReader CreateSafeXmlTextReader(string url, XmlNameTable nt)
		{
			return new XmlTextReader(url, nt)
			{
				ProhibitDtd = true,
				XmlResolver = null
			};
		}

		// Token: 0x06001B6B RID: 7019 RVA: 0x00049544 File Offset: 0x00048544
		public static XmlTextReader CreateSafeXmlTextReader(TextReader input, XmlNameTable nt)
		{
			return new XmlTextReader(input, nt)
			{
				ProhibitDtd = true,
				XmlResolver = null
			};
		}

		// Token: 0x06001B6C RID: 7020 RVA: 0x00049568 File Offset: 0x00048568
		public static XmlTextReader CreateSafeXmlTextReader(Stream xmlFragment, XmlNodeType fragType, XmlParserContext context)
		{
			return new XmlTextReader(xmlFragment, fragType, context)
			{
				ProhibitDtd = true,
				XmlResolver = null
			};
		}

		// Token: 0x06001B6D RID: 7021 RVA: 0x00049590 File Offset: 0x00048590
		public static XmlTextReader CreateSafeXmlTextReader(string url, Stream input, XmlNameTable nt)
		{
			return new XmlTextReader(url, input, nt)
			{
				ProhibitDtd = true,
				XmlResolver = null
			};
		}

		// Token: 0x06001B6E RID: 7022 RVA: 0x000495B8 File Offset: 0x000485B8
		public static XmlTextReader CreateSafeXmlTextReader(string url, TextReader input, XmlNameTable nt)
		{
			return new XmlTextReader(url, input, nt)
			{
				ProhibitDtd = true,
				XmlResolver = null
			};
		}

		// Token: 0x06001B6F RID: 7023 RVA: 0x000495E0 File Offset: 0x000485E0
		public static XmlTextReader CreateSafeXmlTextReader(string xmlFragment, XmlNodeType fragType, XmlParserContext context)
		{
			return new XmlTextReader(xmlFragment, fragType, context)
			{
				ProhibitDtd = true,
				XmlResolver = null
			};
		}

		// Token: 0x06001B70 RID: 7024 RVA: 0x00049608 File Offset: 0x00048608
		public static XPathDocument CreateXPathDocument(Stream stream)
		{
			XPathDocument xpathDocument;
			using (XmlReader xmlReader = XmlReader.Create(stream, SafeXmlFactory.defaultSettings))
			{
				xpathDocument = SafeXmlFactory.CreateXPathDocument(xmlReader);
			}
			return xpathDocument;
		}

		// Token: 0x06001B71 RID: 7025 RVA: 0x00049648 File Offset: 0x00048648
		public static XPathDocument CreateXPathDocument(string uri)
		{
			XPathDocument xpathDocument;
			using (XmlReader xmlReader = XmlReader.Create(uri, SafeXmlFactory.defaultSettings))
			{
				xpathDocument = SafeXmlFactory.CreateXPathDocument(xmlReader);
			}
			return xpathDocument;
		}

		// Token: 0x06001B72 RID: 7026 RVA: 0x00049688 File Offset: 0x00048688
		public static XPathDocument CreateXPathDocument(TextReader textReader)
		{
			XPathDocument xpathDocument;
			using (XmlReader xmlReader = XmlReader.Create(textReader, SafeXmlFactory.defaultSettings))
			{
				xpathDocument = SafeXmlFactory.CreateXPathDocument(xmlReader);
			}
			return xpathDocument;
		}

		// Token: 0x06001B73 RID: 7027 RVA: 0x000496C8 File Offset: 0x000486C8
		public static XPathDocument CreateXPathDocument(XmlReader reader)
		{
			if (reader.Settings != null && !reader.Settings.ProhibitDtd)
			{
				throw new XmlDtdException();
			}
			return new XPathDocument(reader);
		}

		// Token: 0x06001B74 RID: 7028 RVA: 0x000496EC File Offset: 0x000486EC
		public static XPathDocument CreateXPathDocument(string uri, XmlSpace space)
		{
			XPathDocument xpathDocument;
			using (XmlReader xmlReader = XmlReader.Create(uri, SafeXmlFactory.defaultSettings))
			{
				xpathDocument = SafeXmlFactory.CreateXPathDocument(xmlReader, space);
			}
			return xpathDocument;
		}

		// Token: 0x06001B75 RID: 7029 RVA: 0x0004972C File Offset: 0x0004872C
		public static XPathDocument CreateXPathDocument(XmlReader reader, XmlSpace space)
		{
			if (reader.Settings != null && !reader.Settings.ProhibitDtd)
			{
				throw new XmlDtdException();
			}
			return new XPathDocument(reader, space);
		}

		// Token: 0x04001455 RID: 5205
		private static XmlReaderSettings defaultSettings = new XmlReaderSettings
		{
			ProhibitDtd = true,
			XmlResolver = null
		};
	}
}
