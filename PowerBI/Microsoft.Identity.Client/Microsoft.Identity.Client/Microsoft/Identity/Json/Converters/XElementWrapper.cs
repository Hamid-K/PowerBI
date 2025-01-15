using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using Microsoft.Identity.Json.Utilities;

namespace Microsoft.Identity.Json.Converters
{
	// Token: 0x02000102 RID: 258
	internal class XElementWrapper : XContainerWrapper, IXmlElement, IXmlNode
	{
		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000D1E RID: 3358 RVA: 0x000334D6 File Offset: 0x000316D6
		private XElement Element
		{
			get
			{
				return (XElement)base.WrappedNode;
			}
		}

		// Token: 0x06000D1F RID: 3359 RVA: 0x000334E3 File Offset: 0x000316E3
		public XElementWrapper(XElement element)
			: base(element)
		{
		}

		// Token: 0x06000D20 RID: 3360 RVA: 0x000334EC File Offset: 0x000316EC
		public void SetAttributeNode(IXmlNode attribute)
		{
			XObjectWrapper xobjectWrapper = (XObjectWrapper)attribute;
			this.Element.Add(xobjectWrapper.WrappedNode);
			this._attributes = null;
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000D21 RID: 3361 RVA: 0x00033518 File Offset: 0x00031718
		public override List<IXmlNode> Attributes
		{
			get
			{
				if (this._attributes == null)
				{
					if (!this.Element.HasAttributes && !this.HasImplicitNamespaceAttribute(this.NamespaceUri))
					{
						this._attributes = XmlNodeConverter.EmptyChildNodes;
					}
					else
					{
						this._attributes = new List<IXmlNode>();
						foreach (XAttribute xattribute in this.Element.Attributes())
						{
							this._attributes.Add(new XAttributeWrapper(xattribute));
						}
						string namespaceUri = this.NamespaceUri;
						if (this.HasImplicitNamespaceAttribute(namespaceUri))
						{
							this._attributes.Insert(0, new XAttributeWrapper(new XAttribute("xmlns", namespaceUri)));
						}
					}
				}
				return this._attributes;
			}
		}

		// Token: 0x06000D22 RID: 3362 RVA: 0x000335EC File Offset: 0x000317EC
		private bool HasImplicitNamespaceAttribute(string namespaceUri)
		{
			if (!StringUtils.IsNullOrEmpty(namespaceUri))
			{
				IXmlNode parentNode = this.ParentNode;
				if (namespaceUri != ((parentNode != null) ? parentNode.NamespaceUri : null) && StringUtils.IsNullOrEmpty(this.GetPrefixOfNamespace(namespaceUri)))
				{
					bool flag = false;
					if (this.Element.HasAttributes)
					{
						foreach (XAttribute xattribute in this.Element.Attributes())
						{
							if (xattribute.Name.LocalName == "xmlns" && StringUtils.IsNullOrEmpty(xattribute.Name.NamespaceName) && xattribute.Value == namespaceUri)
							{
								flag = true;
							}
						}
					}
					if (!flag)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000D23 RID: 3363 RVA: 0x000336BC File Offset: 0x000318BC
		public override IXmlNode AppendChild(IXmlNode newChild)
		{
			IXmlNode xmlNode = base.AppendChild(newChild);
			this._attributes = null;
			return xmlNode;
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000D24 RID: 3364 RVA: 0x000336CC File Offset: 0x000318CC
		// (set) Token: 0x06000D25 RID: 3365 RVA: 0x000336D9 File Offset: 0x000318D9
		[Nullable(2)]
		public override string Value
		{
			[NullableContext(2)]
			get
			{
				return this.Element.Value;
			}
			[NullableContext(2)]
			set
			{
				this.Element.Value = value;
			}
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000D26 RID: 3366 RVA: 0x000336E7 File Offset: 0x000318E7
		[Nullable(2)]
		public override string LocalName
		{
			[NullableContext(2)]
			get
			{
				return this.Element.Name.LocalName;
			}
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000D27 RID: 3367 RVA: 0x000336F9 File Offset: 0x000318F9
		[Nullable(2)]
		public override string NamespaceUri
		{
			[NullableContext(2)]
			get
			{
				return this.Element.Name.NamespaceName;
			}
		}

		// Token: 0x06000D28 RID: 3368 RVA: 0x0003370B File Offset: 0x0003190B
		public string GetPrefixOfNamespace(string namespaceUri)
		{
			return this.Element.GetPrefixOfNamespace(namespaceUri);
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06000D29 RID: 3369 RVA: 0x0003371E File Offset: 0x0003191E
		public bool IsEmpty
		{
			get
			{
				return this.Element.IsEmpty;
			}
		}

		// Token: 0x040003F3 RID: 1011
		[Nullable(new byte[] { 2, 0 })]
		private List<IXmlNode> _attributes;
	}
}
