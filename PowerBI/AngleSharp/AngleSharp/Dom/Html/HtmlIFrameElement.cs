using System;
using AngleSharp.Dom.Collections;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;
using AngleSharp.Extensions;
using AngleSharp.Html;

namespace AngleSharp.Dom.Html
{
	// Token: 0x02000364 RID: 868
	internal sealed class HtmlIFrameElement : HtmlFrameElementBase, IHtmlInlineFrameElement, IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers, ILoadableElement
	{
		// Token: 0x06001AC5 RID: 6853 RVA: 0x0005273E File Offset: 0x0005093E
		public HtmlIFrameElement(Document owner, string prefix = null)
			: base(owner, TagNames.Iframe, prefix, NodeFlags.LiteralText)
		{
		}

		// Token: 0x1700077E RID: 1918
		// (get) Token: 0x06001AC6 RID: 6854 RVA: 0x0005274E File Offset: 0x0005094E
		// (set) Token: 0x06001AC7 RID: 6855 RVA: 0x00052761 File Offset: 0x00050961
		public Alignment Align
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Align).ToEnum(Alignment.Bottom);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Align, value.ToString(), false);
			}
		}

		// Token: 0x1700077F RID: 1919
		// (get) Token: 0x06001AC8 RID: 6856 RVA: 0x0005277C File Offset: 0x0005097C
		// (set) Token: 0x06001AC9 RID: 6857 RVA: 0x00052789 File Offset: 0x00050989
		public string ContentHtml
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.SrcDoc);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.SrcDoc, value, false);
			}
		}

		// Token: 0x17000780 RID: 1920
		// (get) Token: 0x06001ACA RID: 6858 RVA: 0x00052798 File Offset: 0x00050998
		public ISettableTokenList Sandbox
		{
			get
			{
				if (this._sandbox == null)
				{
					this._sandbox = new SettableTokenList(this.GetOwnAttribute(AttributeNames.Sandbox));
					this._sandbox.Changed += delegate(string value)
					{
						base.UpdateAttribute(AttributeNames.Sandbox, value);
					};
				}
				return this._sandbox;
			}
		}

		// Token: 0x17000781 RID: 1921
		// (get) Token: 0x06001ACB RID: 6859 RVA: 0x000527D5 File Offset: 0x000509D5
		// (set) Token: 0x06001ACC RID: 6860 RVA: 0x000527E2 File Offset: 0x000509E2
		public bool IsSeamless
		{
			get
			{
				return this.GetBoolAttribute(AttributeNames.SrcDoc);
			}
			set
			{
				this.SetBoolAttribute(AttributeNames.SrcDoc, value);
			}
		}

		// Token: 0x17000782 RID: 1922
		// (get) Token: 0x06001ACD RID: 6861 RVA: 0x000527F0 File Offset: 0x000509F0
		// (set) Token: 0x06001ACE RID: 6862 RVA: 0x000527FD File Offset: 0x000509FD
		public bool IsFullscreenAllowed
		{
			get
			{
				return this.GetBoolAttribute(AttributeNames.AllowFullscreen);
			}
			set
			{
				this.SetBoolAttribute(AttributeNames.AllowFullscreen, value);
			}
		}

		// Token: 0x17000783 RID: 1923
		// (get) Token: 0x06001ACF RID: 6863 RVA: 0x0005280B File Offset: 0x00050A0B
		public IWindow ContentWindow
		{
			get
			{
				return base.NestedContext.Current;
			}
		}

		// Token: 0x06001AD0 RID: 6864 RVA: 0x00052818 File Offset: 0x00050A18
		internal override string GetContentHtml()
		{
			return this.ContentHtml;
		}

		// Token: 0x06001AD1 RID: 6865 RVA: 0x00052820 File Offset: 0x00050A20
		internal override void SetupElement()
		{
			base.SetupElement();
			if (this.GetOwnAttribute(AttributeNames.SrcDoc) != null)
			{
				base.UpdateSource();
			}
		}

		// Token: 0x06001AD2 RID: 6866 RVA: 0x0005283B File Offset: 0x00050A3B
		internal void UpdateSandbox(string value)
		{
			SettableTokenList sandbox = this._sandbox;
			if (sandbox == null)
			{
				return;
			}
			sandbox.Update(value);
		}

		// Token: 0x04000CD5 RID: 3285
		private SettableTokenList _sandbox;
	}
}
