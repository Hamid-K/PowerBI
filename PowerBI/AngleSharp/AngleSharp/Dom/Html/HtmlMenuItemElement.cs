using System;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;
using AngleSharp.Extensions;
using AngleSharp.Html;

namespace AngleSharp.Dom.Html
{
	// Token: 0x02000372 RID: 882
	internal sealed class HtmlMenuItemElement : HtmlElement, IHtmlMenuItemElement, IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers
	{
		// Token: 0x06001BC8 RID: 7112 RVA: 0x00053A1D File Offset: 0x00051C1D
		public HtmlMenuItemElement(Document owner, string prefix = null)
			: base(owner, TagNames.MenuItem, prefix, NodeFlags.SelfClosing | NodeFlags.Special)
		{
		}

		// Token: 0x170007F8 RID: 2040
		// (get) Token: 0x06001BC9 RID: 7113 RVA: 0x00053A2D File Offset: 0x00051C2D
		// (set) Token: 0x06001BCA RID: 7114 RVA: 0x00053A35 File Offset: 0x00051C35
		internal bool IsVisited { get; set; }

		// Token: 0x170007F9 RID: 2041
		// (get) Token: 0x06001BCB RID: 7115 RVA: 0x00053A3E File Offset: 0x00051C3E
		// (set) Token: 0x06001BCC RID: 7116 RVA: 0x00053A46 File Offset: 0x00051C46
		internal bool IsActive { get; set; }

		// Token: 0x170007FA RID: 2042
		// (get) Token: 0x06001BCD RID: 7117 RVA: 0x00053A50 File Offset: 0x00051C50
		public IHtmlElement Command
		{
			get
			{
				string ownAttribute = this.GetOwnAttribute(AttributeNames.Command);
				if (!string.IsNullOrEmpty(ownAttribute))
				{
					Document owner = base.Owner;
					return ((owner != null) ? owner.GetElementById(ownAttribute) : null) as IHtmlElement;
				}
				return null;
			}
		}

		// Token: 0x170007FB RID: 2043
		// (get) Token: 0x06001BCE RID: 7118 RVA: 0x00051A27 File Offset: 0x0004FC27
		// (set) Token: 0x06001BCF RID: 7119 RVA: 0x0004FF58 File Offset: 0x0004E158
		public string Type
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Type);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Type, value, false);
			}
		}

		// Token: 0x170007FC RID: 2044
		// (get) Token: 0x06001BD0 RID: 7120 RVA: 0x00053A01 File Offset: 0x00051C01
		// (set) Token: 0x06001BD1 RID: 7121 RVA: 0x00053A0E File Offset: 0x00051C0E
		public string Label
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Label);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Label, value, false);
			}
		}

		// Token: 0x170007FD RID: 2045
		// (get) Token: 0x06001BD2 RID: 7122 RVA: 0x00053A8B File Offset: 0x00051C8B
		// (set) Token: 0x06001BD3 RID: 7123 RVA: 0x00053A98 File Offset: 0x00051C98
		public string Icon
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Icon);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Icon, value, false);
			}
		}

		// Token: 0x170007FE RID: 2046
		// (get) Token: 0x06001BD4 RID: 7124 RVA: 0x00053260 File Offset: 0x00051460
		// (set) Token: 0x06001BD5 RID: 7125 RVA: 0x00051B70 File Offset: 0x0004FD70
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

		// Token: 0x170007FF RID: 2047
		// (get) Token: 0x06001BD6 RID: 7126 RVA: 0x000529D9 File Offset: 0x00050BD9
		// (set) Token: 0x06001BD7 RID: 7127 RVA: 0x000529E6 File Offset: 0x00050BE6
		public bool IsChecked
		{
			get
			{
				return this.GetBoolAttribute(AttributeNames.Checked);
			}
			set
			{
				this.SetBoolAttribute(AttributeNames.Checked, value);
			}
		}

		// Token: 0x17000800 RID: 2048
		// (get) Token: 0x06001BD8 RID: 7128 RVA: 0x00053AA7 File Offset: 0x00051CA7
		// (set) Token: 0x06001BD9 RID: 7129 RVA: 0x00053AB4 File Offset: 0x00051CB4
		public bool IsDefault
		{
			get
			{
				return this.GetBoolAttribute(AttributeNames.Default);
			}
			set
			{
				this.SetBoolAttribute(AttributeNames.Default, value);
			}
		}

		// Token: 0x17000801 RID: 2049
		// (get) Token: 0x06001BDA RID: 7130 RVA: 0x00053AC2 File Offset: 0x00051CC2
		// (set) Token: 0x06001BDB RID: 7131 RVA: 0x00053ACF File Offset: 0x00051CCF
		public string RadioGroup
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Radiogroup);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Radiogroup, value, false);
			}
		}
	}
}
