using System;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp.Dom.Events;
using AngleSharp.Extensions;
using AngleSharp.Network;
using AngleSharp.Parser.Xml;
using AngleSharp.Services;

namespace AngleSharp.Dom.Svg
{
	// Token: 0x020001B0 RID: 432
	internal sealed class SvgDocument : Document, ISvgDocument, IDocument, INode, IEventTarget, IMarkupFormattable, IParentNode, IGlobalEventHandlers, IDocumentStyle, INonElementParentNode, IDisposable
	{
		// Token: 0x06000F1E RID: 3870 RVA: 0x00046C86 File Offset: 0x00044E86
		internal SvgDocument(IBrowsingContext context, TextSource source)
			: base(context ?? BrowsingContext.New(null), source)
		{
			base.ContentType = MimeTypeNames.Svg;
		}

		// Token: 0x06000F1F RID: 3871 RVA: 0x00046CA5 File Offset: 0x00044EA5
		internal SvgDocument(IBrowsingContext context = null)
			: this(context, new TextSource(string.Empty))
		{
		}

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06000F20 RID: 3872 RVA: 0x00046CB8 File Offset: 0x00044EB8
		public override IElement DocumentElement
		{
			get
			{
				return this.RootElement;
			}
		}

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06000F21 RID: 3873 RVA: 0x00046CC0 File Offset: 0x00044EC0
		public ISvgSvgElement RootElement
		{
			get
			{
				return this.FindChild<ISvgSvgElement>();
			}
		}

		// Token: 0x06000F22 RID: 3874 RVA: 0x00046CC8 File Offset: 0x00044EC8
		public override INode Clone(bool deep = true)
		{
			SvgDocument svgDocument = new SvgDocument(base.Context, new TextSource(base.Source.Text));
			base.CloneDocument(svgDocument, deep);
			return svgDocument;
		}

		// Token: 0x06000F23 RID: 3875 RVA: 0x00046CFC File Offset: 0x00044EFC
		internal static async Task<IDocument> LoadAsync(IBrowsingContext context, CreateDocumentOptions options, CancellationToken cancelToken)
		{
			XmlParserOptions xmlParserOptions = default(XmlParserOptions);
			SvgDocument document = new SvgDocument(context, options.Source);
			IElementFactory<SvgElement> factory = context.Configuration.GetFactory<IElementFactory<SvgElement>>();
			XmlDomBuilder xmlDomBuilder = new XmlDomBuilder(document, new Func<Document, string, string, Element>(factory.Create));
			document.Setup(options);
			context.NavigateTo(document);
			context.Fire(new HtmlParseEvent(document, false));
			await xmlDomBuilder.ParseAsync(xmlParserOptions, cancelToken).ConfigureAwait(false);
			context.Fire(new HtmlParseEvent(document, true));
			return document;
		}

		// Token: 0x06000F24 RID: 3876 RVA: 0x00046D51 File Offset: 0x00044F51
		protected override string GetTitle()
		{
			ISvgTitleElement svgTitleElement = this.RootElement.FindChild<ISvgTitleElement>();
			return ((svgTitleElement != null) ? svgTitleElement.TextContent.CollapseAndStrip() : null) ?? base.GetTitle();
		}

		// Token: 0x06000F25 RID: 3877 RVA: 0x00046D7C File Offset: 0x00044F7C
		protected override void SetTitle(string value)
		{
			ISvgTitleElement svgTitleElement = this.RootElement.FindChild<ISvgTitleElement>();
			if (svgTitleElement == null)
			{
				svgTitleElement = new SvgTitleElement(this, null);
				this.RootElement.AppendChild(svgTitleElement);
			}
			svgTitleElement.TextContent = value;
		}
	}
}
