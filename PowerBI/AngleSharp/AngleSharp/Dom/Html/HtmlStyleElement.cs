using System;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;
using AngleSharp.Extensions;
using AngleSharp.Html;
using AngleSharp.Network;
using AngleSharp.Services.Styling;

namespace AngleSharp.Dom.Html
{
	// Token: 0x02000395 RID: 917
	internal sealed class HtmlStyleElement : HtmlElement, IHtmlStyleElement, IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers, ILinkStyle
	{
		// Token: 0x06001CA0 RID: 7328 RVA: 0x00054A41 File Offset: 0x00052C41
		public HtmlStyleElement(Document owner, string prefix = null)
			: base(owner, TagNames.Style, prefix, NodeFlags.Special | NodeFlags.LiteralText)
		{
		}

		// Token: 0x1700084F RID: 2127
		// (get) Token: 0x06001CA1 RID: 7329 RVA: 0x00054A51 File Offset: 0x00052C51
		// (set) Token: 0x06001CA2 RID: 7330 RVA: 0x00054A5E File Offset: 0x00052C5E
		public bool IsScoped
		{
			get
			{
				return this.GetBoolAttribute(AttributeNames.Scoped);
			}
			set
			{
				this.SetBoolAttribute(AttributeNames.Scoped, value);
			}
		}

		// Token: 0x17000850 RID: 2128
		// (get) Token: 0x06001CA3 RID: 7331 RVA: 0x00054A6C File Offset: 0x00052C6C
		public IStyleSheet Sheet
		{
			get
			{
				return this._sheet;
			}
		}

		// Token: 0x17000851 RID: 2129
		// (get) Token: 0x06001CA4 RID: 7332 RVA: 0x00053260 File Offset: 0x00051460
		// (set) Token: 0x06001CA5 RID: 7333 RVA: 0x00054A74 File Offset: 0x00052C74
		public bool IsDisabled
		{
			get
			{
				return this.GetBoolAttribute(AttributeNames.Disabled);
			}
			set
			{
				this.SetBoolAttribute(AttributeNames.Disabled, value);
				if (this._sheet != null)
				{
					this._sheet.IsDisabled = value;
				}
			}
		}

		// Token: 0x17000852 RID: 2130
		// (get) Token: 0x06001CA6 RID: 7334 RVA: 0x0005326D File Offset: 0x0005146D
		// (set) Token: 0x06001CA7 RID: 7335 RVA: 0x0005327A File Offset: 0x0005147A
		public string Media
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Media);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Media, value, false);
			}
		}

		// Token: 0x17000853 RID: 2131
		// (get) Token: 0x06001CA8 RID: 7336 RVA: 0x00051A27 File Offset: 0x0004FC27
		// (set) Token: 0x06001CA9 RID: 7337 RVA: 0x0004FF58 File Offset: 0x0004E158
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

		// Token: 0x06001CAA RID: 7338 RVA: 0x00054A96 File Offset: 0x00052C96
		internal override void SetupElement()
		{
			base.SetupElement();
			this.UpdateSheet();
		}

		// Token: 0x06001CAB RID: 7339 RVA: 0x00054AA4 File Offset: 0x00052CA4
		internal override void NodeIsInserted(Node newNode)
		{
			base.NodeIsInserted(newNode);
			this.UpdateSheet();
		}

		// Token: 0x06001CAC RID: 7340 RVA: 0x00054AB3 File Offset: 0x00052CB3
		internal override void NodeIsRemoved(Node removedNode, Node oldPreviousSibling)
		{
			base.NodeIsRemoved(removedNode, oldPreviousSibling);
			this.UpdateSheet();
		}

		// Token: 0x06001CAD RID: 7341 RVA: 0x00054AC3 File Offset: 0x00052CC3
		internal void UpdateMedia(string value)
		{
			if (this._sheet != null)
			{
				this._sheet.Media.MediaText = value;
			}
		}

		// Token: 0x06001CAE RID: 7342 RVA: 0x00054AE0 File Offset: 0x00052CE0
		private void UpdateSheet()
		{
			Document owner = base.Owner;
			if (owner != null)
			{
				IConfiguration options = owner.Options;
				IBrowsingContext context = owner.Context;
				string text = this.Type ?? MimeTypeNames.Css;
				IStyleEngine styleEngine = options.GetStyleEngine(text);
				if (styleEngine != null)
				{
					Task task = this.CreateSheetAsync(styleEngine, context);
					owner.DelayLoad(task);
				}
			}
		}

		// Token: 0x06001CAF RID: 7343 RVA: 0x00054B38 File Offset: 0x00052D38
		private async Task CreateSheetAsync(IStyleEngine engine, IBrowsingContext context)
		{
			CancellationToken none = CancellationToken.None;
			IResponse response = VirtualResponse.Create(delegate(VirtualResponse res)
			{
				res.Content(this.TextContent).Address(null);
			});
			StyleOptions styleOptions = new StyleOptions(context)
			{
				Element = this,
				IsDisabled = this.IsDisabled,
				IsAlternate = false
			};
			Task<IStyleSheet> task = engine.ParseStylesheetAsync(response, styleOptions, none);
			HtmlStyleElement htmlStyleElement = this;
			IStyleSheet sheet = htmlStyleElement._sheet;
			IStyleSheet styleSheet = await task.ConfigureAwait(false);
			htmlStyleElement._sheet = styleSheet;
			htmlStyleElement = null;
		}

		// Token: 0x04000CFB RID: 3323
		private IStyleSheet _sheet;
	}
}
