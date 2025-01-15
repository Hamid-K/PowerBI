using System;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp.Dom.Events;
using AngleSharp.Extensions;
using AngleSharp.Html;
using AngleSharp.Network;
using AngleSharp.Parser.Html;

namespace AngleSharp.Dom.Html
{
	// Token: 0x02000353 RID: 851
	internal sealed class HtmlDocument : Document, IHtmlDocument, IDocument, INode, IEventTarget, IMarkupFormattable, IParentNode, IGlobalEventHandlers, IDocumentStyle, INonElementParentNode, IDisposable
	{
		// Token: 0x06001998 RID: 6552 RVA: 0x000504ED File Offset: 0x0004E6ED
		internal HtmlDocument(IBrowsingContext context, TextSource source)
			: base(context ?? BrowsingContext.New(null), source)
		{
			base.ContentType = MimeTypeNames.Html;
		}

		// Token: 0x06001999 RID: 6553 RVA: 0x0005050C File Offset: 0x0004E70C
		internal HtmlDocument(IBrowsingContext context = null)
			: this(context, new TextSource(string.Empty))
		{
		}

		// Token: 0x1700073D RID: 1853
		// (get) Token: 0x0600199A RID: 6554 RVA: 0x0005051F File Offset: 0x0004E71F
		public override IElement DocumentElement
		{
			get
			{
				return this.FindChild<HtmlHtmlElement>();
			}
		}

		// Token: 0x0600199B RID: 6555 RVA: 0x00050528 File Offset: 0x0004E728
		public override INode Clone(bool deep = true)
		{
			TextSource textSource = new TextSource(base.Source.Text);
			HtmlDocument htmlDocument = new HtmlDocument(base.Context, textSource);
			base.CloneDocument(htmlDocument, deep);
			return htmlDocument;
		}

		// Token: 0x0600199C RID: 6556 RVA: 0x0005055C File Offset: 0x0004E75C
		internal static async Task<IDocument> LoadAsync(IBrowsingContext context, CreateDocumentOptions options, CancellationToken cancelToken)
		{
			bool flag = context.Configuration.IsScripting();
			HtmlParserOptions htmlParserOptions = new HtmlParserOptions
			{
				IsScripting = flag
			};
			HtmlDocument document = new HtmlDocument(context, options.Source);
			HtmlDomBuilder htmlDomBuilder = new HtmlDomBuilder(document);
			document.Setup(options);
			context.NavigateTo(document);
			context.Fire(new HtmlParseEvent(document, false));
			await htmlDomBuilder.ParseAsync(htmlParserOptions, cancelToken).ConfigureAwait(false);
			context.Fire(new HtmlParseEvent(document, true));
			return document;
		}

		// Token: 0x0600199D RID: 6557 RVA: 0x000505B4 File Offset: 0x0004E7B4
		internal static async Task<IDocument> LoadTextAsync(IBrowsingContext context, CreateDocumentOptions options, CancellationToken cancelToken)
		{
			bool flag = context.Configuration.IsScripting();
			default(HtmlParserOptions).IsScripting = flag;
			HtmlDocument document = new HtmlDocument(context, options.Source);
			document.Setup(options);
			context.NavigateTo(document);
			IElement element = document.CreateElement(TagNames.Html);
			IElement element2 = document.CreateElement(TagNames.Head);
			IElement element3 = document.CreateElement(TagNames.Body);
			IElement pre = document.CreateElement(TagNames.Pre);
			document.AppendChild(element);
			element.AppendChild(element2);
			element.AppendChild(element3);
			element3.AppendChild(pre);
			pre.SetAttribute(AttributeNames.Style, "word-wrap: break-word; white-space: pre-wrap;");
			await options.Source.PrefetchAllAsync(cancelToken).ConfigureAwait(false);
			pre.TextContent = options.Source.Text;
			return document;
		}

		// Token: 0x0600199E RID: 6558 RVA: 0x00050609 File Offset: 0x0004E809
		protected override string GetTitle()
		{
			IHtmlTitleElement htmlTitleElement = this.DocumentElement.FindDescendant<IHtmlTitleElement>();
			return ((htmlTitleElement != null) ? htmlTitleElement.TextContent.CollapseAndStrip() : null) ?? base.GetTitle();
		}

		// Token: 0x0600199F RID: 6559 RVA: 0x00050634 File Offset: 0x0004E834
		protected override void SetTitle(string value)
		{
			IHtmlTitleElement htmlTitleElement = this.DocumentElement.FindDescendant<IHtmlTitleElement>();
			if (htmlTitleElement == null)
			{
				IHtmlHeadElement head = base.Head;
				if (head == null)
				{
					return;
				}
				htmlTitleElement = new HtmlTitleElement(this, null);
				head.AppendChild(htmlTitleElement);
			}
			htmlTitleElement.TextContent = value;
		}
	}
}
