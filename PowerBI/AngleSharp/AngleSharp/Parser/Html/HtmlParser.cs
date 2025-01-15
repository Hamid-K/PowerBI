using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp.Dom;
using AngleSharp.Dom.Html;
using AngleSharp.Extensions;
using AngleSharp.Services;

namespace AngleSharp.Parser.Html
{
	// Token: 0x0200006E RID: 110
	public class HtmlParser
	{
		// Token: 0x060002C5 RID: 709 RVA: 0x00013C54 File Offset: 0x00011E54
		public HtmlParser()
			: this(Configuration.Default)
		{
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x00013C61 File Offset: 0x00011E61
		public HtmlParser(HtmlParserOptions options)
			: this(options, Configuration.Default)
		{
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x00013C70 File Offset: 0x00011E70
		public HtmlParser(IConfiguration configuration)
			: this(new HtmlParserOptions
			{
				IsScripting = configuration.IsScripting()
			}, configuration)
		{
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x00013C9A File Offset: 0x00011E9A
		public HtmlParser(HtmlParserOptions options, IConfiguration configuration)
			: this(options, BrowsingContext.New(configuration))
		{
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x00013CA9 File Offset: 0x00011EA9
		public HtmlParser(HtmlParserOptions options, IBrowsingContext context)
		{
			this._options = options;
			this._context = context;
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060002CA RID: 714 RVA: 0x00013CBF File Offset: 0x00011EBF
		public HtmlParserOptions Options
		{
			get
			{
				return this._options;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060002CB RID: 715 RVA: 0x00013CC7 File Offset: 0x00011EC7
		public IBrowsingContext Context
		{
			get
			{
				return this._context;
			}
		}

		// Token: 0x060002CC RID: 716 RVA: 0x00013CCF File Offset: 0x00011ECF
		public IHtmlDocument Parse(string source)
		{
			return new HtmlDomBuilder(this.CreateDocument(source)).Parse(this._options);
		}

		// Token: 0x060002CD RID: 717 RVA: 0x00013CE8 File Offset: 0x00011EE8
		public INodeList ParseFragment(string source, IElement context)
		{
			HtmlDocument htmlDocument = this.CreateDocument(source);
			HtmlDomBuilder htmlDomBuilder = new HtmlDomBuilder(htmlDocument);
			if (context != null)
			{
				Element element = context as Element;
				if (element == null)
				{
					element = htmlDocument.Options.GetFactory<IElementFactory<HtmlElement>>().Create(htmlDocument, context.LocalName, context.Prefix);
				}
				return htmlDomBuilder.ParseFragment(this._options, element).DocumentElement.ChildNodes;
			}
			return htmlDomBuilder.Parse(this._options).ChildNodes;
		}

		// Token: 0x060002CE RID: 718 RVA: 0x00013D58 File Offset: 0x00011F58
		public IHtmlDocument Parse(Stream source)
		{
			return new HtmlDomBuilder(this.CreateDocument(source)).Parse(this._options);
		}

		// Token: 0x060002CF RID: 719 RVA: 0x00013D71 File Offset: 0x00011F71
		public Task<IHtmlDocument> ParseAsync(string source)
		{
			return this.ParseAsync(source, CancellationToken.None);
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x00013D7F File Offset: 0x00011F7F
		public Task<IHtmlDocument> ParseAsync(Stream source)
		{
			return this.ParseAsync(source, CancellationToken.None);
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x00013D90 File Offset: 0x00011F90
		public async Task<IHtmlDocument> ParseAsync(string source, CancellationToken cancel)
		{
			return await new HtmlDomBuilder(this.CreateDocument(source)).ParseAsync(this._options, cancel).ConfigureAwait(false);
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x00013DE8 File Offset: 0x00011FE8
		public async Task<IHtmlDocument> ParseAsync(Stream source, CancellationToken cancel)
		{
			return await new HtmlDomBuilder(this.CreateDocument(source)).ParseAsync(this._options, cancel).ConfigureAwait(false);
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x00013E40 File Offset: 0x00012040
		private HtmlDocument CreateDocument(string source)
		{
			TextSource textSource = new TextSource(source);
			return this.CreateDocument(textSource);
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x00013E5C File Offset: 0x0001205C
		private HtmlDocument CreateDocument(Stream source)
		{
			TextSource textSource = new TextSource(source, this._context.Configuration.DefaultEncoding());
			return this.CreateDocument(textSource);
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x00013E87 File Offset: 0x00012087
		private HtmlDocument CreateDocument(TextSource textSource)
		{
			return new HtmlDocument(this._context, textSource);
		}

		// Token: 0x0400029F RID: 671
		private readonly HtmlParserOptions _options;

		// Token: 0x040002A0 RID: 672
		private readonly IBrowsingContext _context;
	}
}
