using System;
using System.Runtime.CompilerServices;
using System.Xml;

namespace Microsoft.Identity.Json.Converters
{
	// Token: 0x020000EF RID: 239
	internal class XmlDocumentWrapper : XmlNodeWrapper, IXmlDocument, IXmlNode
	{
		// Token: 0x06000C87 RID: 3207 RVA: 0x00032A66 File Offset: 0x00030C66
		public XmlDocumentWrapper(XmlDocument document)
			: base(document)
		{
			this._document = document;
		}

		// Token: 0x06000C88 RID: 3208 RVA: 0x00032A76 File Offset: 0x00030C76
		public IXmlNode CreateComment([Nullable(2)] string data)
		{
			return new XmlNodeWrapper(this._document.CreateComment(data));
		}

		// Token: 0x06000C89 RID: 3209 RVA: 0x00032A89 File Offset: 0x00030C89
		public IXmlNode CreateTextNode([Nullable(2)] string text)
		{
			return new XmlNodeWrapper(this._document.CreateTextNode(text));
		}

		// Token: 0x06000C8A RID: 3210 RVA: 0x00032A9C File Offset: 0x00030C9C
		public IXmlNode CreateCDataSection([Nullable(2)] string data)
		{
			return new XmlNodeWrapper(this._document.CreateCDataSection(data));
		}

		// Token: 0x06000C8B RID: 3211 RVA: 0x00032AAF File Offset: 0x00030CAF
		public IXmlNode CreateWhitespace([Nullable(2)] string text)
		{
			return new XmlNodeWrapper(this._document.CreateWhitespace(text));
		}

		// Token: 0x06000C8C RID: 3212 RVA: 0x00032AC2 File Offset: 0x00030CC2
		public IXmlNode CreateSignificantWhitespace([Nullable(2)] string text)
		{
			return new XmlNodeWrapper(this._document.CreateSignificantWhitespace(text));
		}

		// Token: 0x06000C8D RID: 3213 RVA: 0x00032AD5 File Offset: 0x00030CD5
		[NullableContext(2)]
		[return: Nullable(0)]
		public IXmlNode CreateXmlDeclaration(string version, string encoding, string standalone)
		{
			return new XmlDeclarationWrapper(this._document.CreateXmlDeclaration(version, encoding, standalone));
		}

		// Token: 0x06000C8E RID: 3214 RVA: 0x00032AEA File Offset: 0x00030CEA
		[NullableContext(2)]
		[return: Nullable(0)]
		public IXmlNode CreateXmlDocumentType(string name, string publicId, string systemId, string internalSubset)
		{
			return new XmlDocumentTypeWrapper(this._document.CreateDocumentType(name, publicId, systemId, null));
		}

		// Token: 0x06000C8F RID: 3215 RVA: 0x00032B00 File Offset: 0x00030D00
		public IXmlNode CreateProcessingInstruction(string target, [Nullable(2)] string data)
		{
			return new XmlNodeWrapper(this._document.CreateProcessingInstruction(target, data));
		}

		// Token: 0x06000C90 RID: 3216 RVA: 0x00032B14 File Offset: 0x00030D14
		public IXmlElement CreateElement(string elementName)
		{
			return new XmlElementWrapper(this._document.CreateElement(elementName));
		}

		// Token: 0x06000C91 RID: 3217 RVA: 0x00032B27 File Offset: 0x00030D27
		public IXmlElement CreateElement(string qualifiedName, string namespaceUri)
		{
			return new XmlElementWrapper(this._document.CreateElement(qualifiedName, namespaceUri));
		}

		// Token: 0x06000C92 RID: 3218 RVA: 0x00032B3B File Offset: 0x00030D3B
		public IXmlNode CreateAttribute(string name, [Nullable(2)] string value)
		{
			return new XmlNodeWrapper(this._document.CreateAttribute(name))
			{
				Value = value
			};
		}

		// Token: 0x06000C93 RID: 3219 RVA: 0x00032B55 File Offset: 0x00030D55
		public IXmlNode CreateAttribute(string qualifiedName, string namespaceUri, [Nullable(2)] string value)
		{
			return new XmlNodeWrapper(this._document.CreateAttribute(qualifiedName, namespaceUri))
			{
				Value = value
			};
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06000C94 RID: 3220 RVA: 0x00032B70 File Offset: 0x00030D70
		[Nullable(2)]
		public IXmlElement DocumentElement
		{
			[NullableContext(2)]
			get
			{
				if (this._document.DocumentElement == null)
				{
					return null;
				}
				return new XmlElementWrapper(this._document.DocumentElement);
			}
		}

		// Token: 0x040003E8 RID: 1000
		private readonly XmlDocument _document;
	}
}
