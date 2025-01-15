using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020000F4 RID: 244
	[NullableContext(2)]
	[Nullable(0)]
	internal class XmlNodeWrapper : IXmlNode
	{
		// Token: 0x06000CBF RID: 3263 RVA: 0x00033594 File Offset: 0x00031794
		[NullableContext(1)]
		public XmlNodeWrapper(XmlNode node)
		{
			this._node = node;
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000CC0 RID: 3264 RVA: 0x000335A3 File Offset: 0x000317A3
		public object WrappedNode
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000CC1 RID: 3265 RVA: 0x000335AB File Offset: 0x000317AB
		public XmlNodeType NodeType
		{
			get
			{
				return this._node.NodeType;
			}
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06000CC2 RID: 3266 RVA: 0x000335B8 File Offset: 0x000317B8
		public virtual string LocalName
		{
			get
			{
				return this._node.LocalName;
			}
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000CC3 RID: 3267 RVA: 0x000335C8 File Offset: 0x000317C8
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

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06000CC4 RID: 3268 RVA: 0x00033678 File Offset: 0x00031878
		protected virtual bool HasChildNodes
		{
			get
			{
				return this._node.HasChildNodes;
			}
		}

		// Token: 0x06000CC5 RID: 3269 RVA: 0x00033688 File Offset: 0x00031888
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

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06000CC6 RID: 3270 RVA: 0x000336D8 File Offset: 0x000318D8
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

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06000CC7 RID: 3271 RVA: 0x00033780 File Offset: 0x00031980
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

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06000CC8 RID: 3272 RVA: 0x000337BC File Offset: 0x000319BC
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

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06000CC9 RID: 3273 RVA: 0x000337F7 File Offset: 0x000319F7
		// (set) Token: 0x06000CCA RID: 3274 RVA: 0x00033804 File Offset: 0x00031A04
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

		// Token: 0x06000CCB RID: 3275 RVA: 0x00033814 File Offset: 0x00031A14
		[NullableContext(1)]
		public IXmlNode AppendChild(IXmlNode newChild)
		{
			XmlNodeWrapper xmlNodeWrapper = (XmlNodeWrapper)newChild;
			this._node.AppendChild(xmlNodeWrapper._node);
			this._childNodes = null;
			this._attributes = null;
			return newChild;
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06000CCC RID: 3276 RVA: 0x00033849 File Offset: 0x00031A49
		public string NamespaceUri
		{
			get
			{
				return this._node.NamespaceURI;
			}
		}

		// Token: 0x0400040A RID: 1034
		[Nullable(1)]
		private readonly XmlNode _node;

		// Token: 0x0400040B RID: 1035
		[Nullable(new byte[] { 2, 1 })]
		private List<IXmlNode> _childNodes;

		// Token: 0x0400040C RID: 1036
		[Nullable(new byte[] { 2, 1 })]
		private List<IXmlNode> _attributes;
	}
}
