using System;
using System.Text;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;
using AngleSharp.Extensions;
using AngleSharp.Html;
using AngleSharp.Network;

namespace AngleSharp.Dom.Html
{
	// Token: 0x02000373 RID: 883
	internal sealed class HtmlMetaElement : HtmlElement, IHtmlMetaElement, IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers
	{
		// Token: 0x06001BDC RID: 7132 RVA: 0x00053ADE File Offset: 0x00051CDE
		public HtmlMetaElement(Document owner, string prefix = null)
			: base(owner, TagNames.Meta, prefix, NodeFlags.SelfClosing | NodeFlags.Special)
		{
		}

		// Token: 0x17000802 RID: 2050
		// (get) Token: 0x06001BDD RID: 7133 RVA: 0x00053AEE File Offset: 0x00051CEE
		// (set) Token: 0x06001BDE RID: 7134 RVA: 0x00053AFB File Offset: 0x00051CFB
		public string Content
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Content);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Content, value, false);
			}
		}

		// Token: 0x17000803 RID: 2051
		// (get) Token: 0x06001BDF RID: 7135 RVA: 0x0004FC8F File Offset: 0x0004DE8F
		// (set) Token: 0x06001BE0 RID: 7136 RVA: 0x0004FC9C File Offset: 0x0004DE9C
		public string Charset
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Charset);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Charset, value, false);
			}
		}

		// Token: 0x17000804 RID: 2052
		// (get) Token: 0x06001BE1 RID: 7137 RVA: 0x00053B0A File Offset: 0x00051D0A
		// (set) Token: 0x06001BE2 RID: 7138 RVA: 0x00053B17 File Offset: 0x00051D17
		public string HttpEquivalent
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.HttpEquiv);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.HttpEquiv, value, false);
			}
		}

		// Token: 0x17000805 RID: 2053
		// (get) Token: 0x06001BE3 RID: 7139 RVA: 0x00053B26 File Offset: 0x00051D26
		// (set) Token: 0x06001BE4 RID: 7140 RVA: 0x00053B33 File Offset: 0x00051D33
		public string Scheme
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Scheme);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Scheme, value, false);
			}
		}

		// Token: 0x17000806 RID: 2054
		// (get) Token: 0x06001BE5 RID: 7141 RVA: 0x0004FCAB File Offset: 0x0004DEAB
		// (set) Token: 0x06001BE6 RID: 7142 RVA: 0x0004FCB8 File Offset: 0x0004DEB8
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

		// Token: 0x06001BE7 RID: 7143 RVA: 0x00053B44 File Offset: 0x00051D44
		public Encoding GetEncoding()
		{
			string text = this.Charset;
			if (text != null)
			{
				text = text.Trim();
				if (TextEncoding.IsSupported(text))
				{
					return TextEncoding.Resolve(text);
				}
			}
			string httpEquivalent = this.HttpEquivalent;
			if (httpEquivalent == null || !httpEquivalent.Isi(HeaderNames.ContentType))
			{
				return null;
			}
			return TextEncoding.Parse(this.Content ?? string.Empty);
		}
	}
}
