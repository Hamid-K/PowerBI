using System;
using System.Collections.Generic;
using System.Linq;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;
using AngleSharp.Extensions;
using AngleSharp.Html;

namespace AngleSharp.Dom.Html
{
	// Token: 0x0200038E RID: 910
	internal sealed class HtmlSlotElement : HtmlElement, IHtmlSlotElement, IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers
	{
		// Token: 0x06001C8B RID: 7307 RVA: 0x000548F7 File Offset: 0x00052AF7
		public HtmlSlotElement(Document owner, string prefix = null)
			: base(owner, TagNames.Slot, prefix, NodeFlags.None)
		{
		}

		// Token: 0x17000849 RID: 2121
		// (get) Token: 0x06001C8C RID: 7308 RVA: 0x0004FCAB File Offset: 0x0004DEAB
		// (set) Token: 0x06001C8D RID: 7309 RVA: 0x0004FCB8 File Offset: 0x0004DEB8
		public string Name
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Name);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Name, value, false);
			}
		}

		// Token: 0x06001C8E RID: 7310 RVA: 0x00054908 File Offset: 0x00052B08
		public IEnumerable<INode> GetDistributedNodes()
		{
			IShadowRoot ancestor = this.GetAncestor<IShadowRoot>();
			IElement element = ((ancestor != null) ? ancestor.Host : null);
			if (element != null)
			{
				List<INode> list = new List<INode>();
				foreach (INode node in element.ChildNodes)
				{
					if (HtmlSlotElement.GetAssignedSlot(node) == this)
					{
						HtmlSlotElement htmlSlotElement = node as HtmlSlotElement;
						if (htmlSlotElement != null)
						{
							list.AddRange(htmlSlotElement.GetDistributedNodes());
						}
						else
						{
							list.Add(node);
						}
					}
				}
				return list;
			}
			return Enumerable.Empty<INode>();
		}

		// Token: 0x06001C8F RID: 7311 RVA: 0x0005499C File Offset: 0x00052B9C
		private static IElement GetAssignedSlot(INode node)
		{
			NodeType nodeType = node.NodeType;
			if (nodeType == NodeType.Element)
			{
				return ((IElement)node).AssignedSlot;
			}
			if (nodeType == NodeType.Text)
			{
				return ((IText)node).AssignedSlot;
			}
			return null;
		}
	}
}
