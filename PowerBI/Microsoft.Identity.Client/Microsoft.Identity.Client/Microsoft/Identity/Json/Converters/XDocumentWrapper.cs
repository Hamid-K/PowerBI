using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Identity.Json.Utilities;

namespace Microsoft.Identity.Json.Converters
{
	// Token: 0x020000FB RID: 251
	internal class XDocumentWrapper : XContainerWrapper, IXmlDocument, IXmlNode
	{
		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06000CE4 RID: 3300 RVA: 0x00032FF4 File Offset: 0x000311F4
		private XDocument Document
		{
			get
			{
				return (XDocument)base.WrappedNode;
			}
		}

		// Token: 0x06000CE5 RID: 3301 RVA: 0x00033001 File Offset: 0x00031201
		public XDocumentWrapper(XDocument document)
			: base(document)
		{
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06000CE6 RID: 3302 RVA: 0x0003300C File Offset: 0x0003120C
		public override List<IXmlNode> ChildNodes
		{
			get
			{
				List<IXmlNode> childNodes = base.ChildNodes;
				if (this.Document.Declaration != null && (childNodes.Count == 0 || childNodes[0].NodeType != XmlNodeType.XmlDeclaration))
				{
					childNodes.Insert(0, new XDeclarationWrapper(this.Document.Declaration));
				}
				return childNodes;
			}
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06000CE7 RID: 3303 RVA: 0x0003305D File Offset: 0x0003125D
		protected override bool HasChildNodes
		{
			get
			{
				return base.HasChildNodes || this.Document.Declaration != null;
			}
		}

		// Token: 0x06000CE8 RID: 3304 RVA: 0x00033077 File Offset: 0x00031277
		public IXmlNode CreateComment([Nullable(2)] string text)
		{
			return new XObjectWrapper(new XComment(text));
		}

		// Token: 0x06000CE9 RID: 3305 RVA: 0x00033084 File Offset: 0x00031284
		public IXmlNode CreateTextNode([Nullable(2)] string text)
		{
			return new XObjectWrapper(new XText(text));
		}

		// Token: 0x06000CEA RID: 3306 RVA: 0x00033091 File Offset: 0x00031291
		public IXmlNode CreateCDataSection([Nullable(2)] string data)
		{
			return new XObjectWrapper(new XCData(data));
		}

		// Token: 0x06000CEB RID: 3307 RVA: 0x0003309E File Offset: 0x0003129E
		public IXmlNode CreateWhitespace([Nullable(2)] string text)
		{
			return new XObjectWrapper(new XText(text));
		}

		// Token: 0x06000CEC RID: 3308 RVA: 0x000330AB File Offset: 0x000312AB
		public IXmlNode CreateSignificantWhitespace([Nullable(2)] string text)
		{
			return new XObjectWrapper(new XText(text));
		}

		// Token: 0x06000CED RID: 3309 RVA: 0x000330B8 File Offset: 0x000312B8
		[NullableContext(2)]
		[return: Nullable(0)]
		public IXmlNode CreateXmlDeclaration(string version, string encoding, string standalone)
		{
			return new XDeclarationWrapper(new XDeclaration(version, encoding, standalone));
		}

		// Token: 0x06000CEE RID: 3310 RVA: 0x000330C7 File Offset: 0x000312C7
		[NullableContext(2)]
		[return: Nullable(0)]
		public IXmlNode CreateXmlDocumentType(string name, string publicId, string systemId, string internalSubset)
		{
			return new XDocumentTypeWrapper(new XDocumentType(name, publicId, systemId, internalSubset));
		}

		// Token: 0x06000CEF RID: 3311 RVA: 0x000330D8 File Offset: 0x000312D8
		public IXmlNode CreateProcessingInstruction(string target, [Nullable(2)] string data)
		{
			return new XProcessingInstructionWrapper(new XProcessingInstruction(target, data));
		}

		// Token: 0x06000CF0 RID: 3312 RVA: 0x000330E6 File Offset: 0x000312E6
		public IXmlElement CreateElement(string elementName)
		{
			return new XElementWrapper(new XElement(elementName));
		}

		// Token: 0x06000CF1 RID: 3313 RVA: 0x000330F8 File Offset: 0x000312F8
		public IXmlElement CreateElement(string qualifiedName, string namespaceUri)
		{
			return new XElementWrapper(new XElement(XName.Get(MiscellaneousUtils.GetLocalName(qualifiedName), namespaceUri)));
		}

		// Token: 0x06000CF2 RID: 3314 RVA: 0x00033110 File Offset: 0x00031310
		public IXmlNode CreateAttribute(string name, [Nullable(2)] string value)
		{
			return new XAttributeWrapper(new XAttribute(name, value));
		}

		// Token: 0x06000CF3 RID: 3315 RVA: 0x00033123 File Offset: 0x00031323
		public IXmlNode CreateAttribute(string qualifiedName, string namespaceUri, [Nullable(2)] string value)
		{
			return new XAttributeWrapper(new XAttribute(XName.Get(MiscellaneousUtils.GetLocalName(qualifiedName), namespaceUri), value));
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06000CF4 RID: 3316 RVA: 0x0003313C File Offset: 0x0003133C
		[Nullable(2)]
		public IXmlElement DocumentElement
		{
			[NullableContext(2)]
			get
			{
				if (this.Document.Root == null)
				{
					return null;
				}
				return new XElementWrapper(this.Document.Root);
			}
		}

		// Token: 0x06000CF5 RID: 3317 RVA: 0x00033160 File Offset: 0x00031360
		public override IXmlNode AppendChild(IXmlNode newChild)
		{
			XDeclarationWrapper xdeclarationWrapper = newChild as XDeclarationWrapper;
			if (xdeclarationWrapper != null)
			{
				this.Document.Declaration = xdeclarationWrapper.Declaration;
				return xdeclarationWrapper;
			}
			return base.AppendChild(newChild);
		}
	}
}
