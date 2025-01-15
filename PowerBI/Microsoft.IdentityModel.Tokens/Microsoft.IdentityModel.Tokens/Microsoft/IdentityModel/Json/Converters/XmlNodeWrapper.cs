using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml;

namespace Microsoft.IdentityModel.Json.Converters
{
	// Token: 0x020000F4 RID: 244
	[NullableContext(2)]
	[Nullable(0)]
	internal class XmlNodeWrapper : IXmlNode
	{
		// Token: 0x06000CB5 RID: 3253 RVA: 0x0003343C File Offset: 0x0003163C
		[NullableContext(1)]
		public XmlNodeWrapper(XmlNode node)
		{
			this._node = node;
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000CB6 RID: 3254 RVA: 0x0003344B File Offset: 0x0003164B
		public object WrappedNode
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000CB7 RID: 3255 RVA: 0x00033453 File Offset: 0x00031653
		public XmlNodeType NodeType
		{
			get
			{
				return this._node.NodeType;
			}
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000CB8 RID: 3256 RVA: 0x00033460 File Offset: 0x00031660
		public virtual string LocalName
		{
			get
			{
				return this._node.LocalName;
			}
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06000CB9 RID: 3257 RVA: 0x00033470 File Offset: 0x00031670
		[Nullable(1)]
		public List<IXmlNode> ChildNodes
		{
			[NullableContext(1)]
			get
			{
				if (this._childNodes == null)
				{
					if (!this._node.HasChildNodes)
					{
						this._childNodes = XmlNodeConverter.EmptyChildNodes;
					}
					else
					{
						this._childNodes = new List<IXmlNode>(this._node.ChildNodes.Count);
						foreach (object obj in this._node.ChildNodes)
						{
							XmlNode xmlNode = (XmlNode)obj;
							this._childNodes.Add(XmlNodeWrapper.WrapNode(xmlNode));
						}
					}
				}
				return this._childNodes;
			}
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000CBA RID: 3258 RVA: 0x00033520 File Offset: 0x00031720
		protected virtual bool HasChildNodes
		{
			get
			{
				return this._node.HasChildNodes;
			}
		}

		// Token: 0x06000CBB RID: 3259 RVA: 0x00033530 File Offset: 0x00031730
		[NullableContext(1)]
		internal static IXmlNode WrapNode(XmlNode node)
		{
			XmlNodeType nodeType = node.NodeType;
			if (nodeType == XmlNodeType.Element)
			{
				return new XmlElementWrapper((XmlElement)node);
			}
			if (nodeType == XmlNodeType.DocumentType)
			{
				return new XmlDocumentTypeWrapper((XmlDocumentType)node);
			}
			if (nodeType != XmlNodeType.XmlDeclaration)
			{
				return new XmlNodeWrapper(node);
			}
			return new XmlDeclarationWrapper((XmlDeclaration)node);
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06000CBC RID: 3260 RVA: 0x00033580 File Offset: 0x00031780
		[Nullable(1)]
		public List<IXmlNode> Attributes
		{
			[NullableContext(1)]
			get
			{
				if (this._attributes == null)
				{
					if (!this.HasAttributes)
					{
						this._attributes = XmlNodeConverter.EmptyChildNodes;
					}
					else
					{
						this._attributes = new List<IXmlNode>(this._node.Attributes.Count);
						foreach (object obj in this._node.Attributes)
						{
							XmlAttribute xmlAttribute = (XmlAttribute)obj;
							this._attributes.Add(XmlNodeWrapper.WrapNode(xmlAttribute));
						}
					}
				}
				return this._attributes;
			}
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06000CBD RID: 3261 RVA: 0x00033628 File Offset: 0x00031828
		private bool HasAttributes
		{
			get
			{
				XmlElement xmlElement = this._node as XmlElement;
				if (xmlElement != null)
				{
					return xmlElement.HasAttributes;
				}
				XmlAttributeCollection attributes = this._node.Attributes;
				return attributes != null && attributes.Count > 0;
			}
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06000CBE RID: 3262 RVA: 0x00033664 File Offset: 0x00031864
		public IXmlNode ParentNode
		{
			get
			{
				XmlAttribute xmlAttribute = this._node as XmlAttribute;
				XmlNode xmlNode = ((xmlAttribute != null) ? xmlAttribute.OwnerElement : this._node.ParentNode);
				if (xmlNode == null)
				{
					return null;
				}
				return XmlNodeWrapper.WrapNode(xmlNode);
			}
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06000CBF RID: 3263 RVA: 0x0003369F File Offset: 0x0003189F
		// (set) Token: 0x06000CC0 RID: 3264 RVA: 0x000336AC File Offset: 0x000318AC
		public string Value
		{
			get
			{
				return this._node.Value;
			}
			set
			{
				this._node.Value = value;
			}
		}

		// Token: 0x06000CC1 RID: 3265 RVA: 0x000336BC File Offset: 0x000318BC
		[NullableContext(1)]
		public IXmlNode AppendChild(IXmlNode newChild)
		{
			XmlNodeWrapper xmlNodeWrapper = (XmlNodeWrapper)newChild;
			this._node.AppendChild(xmlNodeWrapper._node);
			this._childNodes = null;
			this._attributes = null;
			return newChild;
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06000CC2 RID: 3266 RVA: 0x000336F1 File Offset: 0x000318F1
		public string NamespaceUri
		{
			get
			{
				return this._node.NamespaceURI;
			}
		}

		// Token: 0x04000409 RID: 1033
		[Nullable(1)]
		private readonly XmlNode _node;

		// Token: 0x0400040A RID: 1034
		[Nullable(new byte[] { 2, 1 })]
		private List<IXmlNode> _childNodes;

		// Token: 0x0400040B RID: 1035
		[Nullable(new byte[] { 2, 1 })]
		private List<IXmlNode> _attributes;
	}
}
