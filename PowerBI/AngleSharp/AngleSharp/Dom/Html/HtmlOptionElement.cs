using System;
using System.Collections.Generic;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;
using AngleSharp.Extensions;
using AngleSharp.Html;

namespace AngleSharp.Dom.Html
{
	// Token: 0x0200037B RID: 891
	internal sealed class HtmlOptionElement : HtmlElement, IHtmlOptionElement, IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers
	{
		// Token: 0x06001C14 RID: 7188 RVA: 0x00053F02 File Offset: 0x00052102
		public HtmlOptionElement(Document owner, string prefix = null)
			: base(owner, TagNames.Option, prefix, NodeFlags.ImplicitelyClosed | NodeFlags.ImpliedEnd | NodeFlags.HtmlSelectScoped)
		{
		}

		// Token: 0x1700081B RID: 2075
		// (get) Token: 0x06001C15 RID: 7189 RVA: 0x00053260 File Offset: 0x00051460
		// (set) Token: 0x06001C16 RID: 7190 RVA: 0x00051B70 File Offset: 0x0004FD70
		public bool IsDisabled
		{
			get
			{
				return this.GetBoolAttribute(AttributeNames.Disabled);
			}
			set
			{
				this.SetBoolAttribute(AttributeNames.Disabled, value);
			}
		}

		// Token: 0x1700081C RID: 2076
		// (get) Token: 0x06001C17 RID: 7191 RVA: 0x00051B51 File Offset: 0x0004FD51
		public IHtmlFormElement Form
		{
			get
			{
				return base.GetAssignedForm();
			}
		}

		// Token: 0x1700081D RID: 2077
		// (get) Token: 0x06001C18 RID: 7192 RVA: 0x00053F16 File Offset: 0x00052116
		// (set) Token: 0x06001C19 RID: 7193 RVA: 0x00053A0E File Offset: 0x00051C0E
		public string Label
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Label) ?? this.Text;
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Label, value, false);
			}
		}

		// Token: 0x1700081E RID: 2078
		// (get) Token: 0x06001C1A RID: 7194 RVA: 0x00053F2D File Offset: 0x0005212D
		// (set) Token: 0x06001C1B RID: 7195 RVA: 0x000500CC File Offset: 0x0004E2CC
		public string Value
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Value) ?? this.Text;
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Value, value, false);
			}
		}

		// Token: 0x1700081F RID: 2079
		// (get) Token: 0x06001C1C RID: 7196 RVA: 0x00053F44 File Offset: 0x00052144
		public int Index
		{
			get
			{
				HtmlOptionsGroupElement htmlOptionsGroupElement = base.Parent as HtmlOptionsGroupElement;
				if (htmlOptionsGroupElement != null)
				{
					int num = 0;
					using (IEnumerator<INode> enumerator = htmlOptionsGroupElement.ChildNodes.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							if (enumerator.Current == this)
							{
								return num;
							}
							num++;
						}
					}
					return 0;
				}
				return 0;
			}
		}

		// Token: 0x17000820 RID: 2080
		// (get) Token: 0x06001C1D RID: 7197 RVA: 0x00053FA8 File Offset: 0x000521A8
		// (set) Token: 0x06001C1E RID: 7198 RVA: 0x0004FCCF File Offset: 0x0004DECF
		public string Text
		{
			get
			{
				return this.TextContent.CollapseAndStrip();
			}
			set
			{
				this.TextContent = value;
			}
		}

		// Token: 0x17000821 RID: 2081
		// (get) Token: 0x06001C1F RID: 7199 RVA: 0x00053FB5 File Offset: 0x000521B5
		// (set) Token: 0x06001C20 RID: 7200 RVA: 0x00053FC2 File Offset: 0x000521C2
		public bool IsDefaultSelected
		{
			get
			{
				return this.GetBoolAttribute(AttributeNames.Selected);
			}
			set
			{
				this.SetBoolAttribute(AttributeNames.Selected, value);
			}
		}

		// Token: 0x17000822 RID: 2082
		// (get) Token: 0x06001C21 RID: 7201 RVA: 0x00053FD0 File Offset: 0x000521D0
		// (set) Token: 0x06001C22 RID: 7202 RVA: 0x00053FF1 File Offset: 0x000521F1
		public bool IsSelected
		{
			get
			{
				if (this._selected == null)
				{
					return this.IsDefaultSelected;
				}
				return this._selected.Value;
			}
			set
			{
				this._selected = new bool?(value);
			}
		}

		// Token: 0x04000CF0 RID: 3312
		private bool? _selected;
	}
}
