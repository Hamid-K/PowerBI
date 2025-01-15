﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace Microsoft.Identity.Json.Converters
{
	// Token: 0x020000FF RID: 255
	internal class XContainerWrapper : XObjectWrapper
	{
		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06000D05 RID: 3333 RVA: 0x00033273 File Offset: 0x00031473
		private XContainer Container
		{
			get
			{
				return (XContainer)base.WrappedNode;
			}
		}

		// Token: 0x06000D06 RID: 3334 RVA: 0x00033280 File Offset: 0x00031480
		public XContainerWrapper(XContainer container)
			: base(container)
		{
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06000D07 RID: 3335 RVA: 0x0003328C File Offset: 0x0003148C
		public override List<IXmlNode> ChildNodes
		{
			get
			{
				if (this._childNodes == null)
				{
					if (!this.HasChildNodes)
					{
						this._childNodes = XmlNodeConverter.EmptyChildNodes;
					}
					else
					{
						this._childNodes = new List<IXmlNode>();
						foreach (XNode xnode in this.Container.Nodes())
						{
							this._childNodes.Add(XContainerWrapper.WrapNode(xnode));
						}
					}
				}
				return this._childNodes;
			}
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06000D08 RID: 3336 RVA: 0x00033318 File Offset: 0x00031518
		protected virtual bool HasChildNodes
		{
			get
			{
				return this.Container.LastNode != null;
			}
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06000D09 RID: 3337 RVA: 0x00033328 File Offset: 0x00031528
		[Nullable(2)]
		public override IXmlNode ParentNode
		{
			[NullableContext(2)]
			get
			{
				if (this.Container.Parent == null)
				{
					return null;
				}
				return XContainerWrapper.WrapNode(this.Container.Parent);
			}
		}

		// Token: 0x06000D0A RID: 3338 RVA: 0x0003334C File Offset: 0x0003154C
		internal static IXmlNode WrapNode(XObject node)
		{
			XDocument xdocument = node as XDocument;
			if (xdocument != null)
			{
				return new XDocumentWrapper(xdocument);
			}
			XElement xelement = node as XElement;
			if (xelement != null)
			{
				return new XElementWrapper(xelement);
			}
			XContainer xcontainer = node as XContainer;
			if (xcontainer != null)
			{
				return new XContainerWrapper(xcontainer);
			}
			XProcessingInstruction xprocessingInstruction = node as XProcessingInstruction;
			if (xprocessingInstruction != null)
			{
				return new XProcessingInstructionWrapper(xprocessingInstruction);
			}
			XText xtext = node as XText;
			if (xtext != null)
			{
				return new XTextWrapper(xtext);
			}
			XComment xcomment = node as XComment;
			if (xcomment != null)
			{
				return new XCommentWrapper(xcomment);
			}
			XAttribute xattribute = node as XAttribute;
			if (xattribute != null)
			{
				return new XAttributeWrapper(xattribute);
			}
			XDocumentType xdocumentType = node as XDocumentType;
			if (xdocumentType != null)
			{
				return new XDocumentTypeWrapper(xdocumentType);
			}
			return new XObjectWrapper(node);
		}

		// Token: 0x06000D0B RID: 3339 RVA: 0x000333F3 File Offset: 0x000315F3
		public override IXmlNode AppendChild(IXmlNode newChild)
		{
			this.Container.Add(newChild.WrappedNode);
			this._childNodes = null;
			return newChild;
		}

		// Token: 0x040003F1 RID: 1009
		[Nullable(new byte[] { 2, 0 })]
		private List<IXmlNode> _childNodes;
	}
}
