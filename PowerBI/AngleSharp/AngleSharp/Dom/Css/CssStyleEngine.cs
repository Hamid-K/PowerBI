using System;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp.Dom.Collections;
using AngleSharp.Dom.Events;
using AngleSharp.Extensions;
using AngleSharp.Network;
using AngleSharp.Parser.Css;
using AngleSharp.Services.Styling;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000201 RID: 513
	public class CssStyleEngine : ICssStyleEngine, IStyleEngine
	{
		// Token: 0x06001352 RID: 4946 RVA: 0x0004A42D File Offset: 0x0004862D
		public CssStyleEngine()
		{
			this._options = default(CssParserOptions);
		}

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x06001353 RID: 4947 RVA: 0x0004A441 File Offset: 0x00048641
		public string Type
		{
			get
			{
				return MimeTypeNames.Css;
			}
		}

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x06001354 RID: 4948 RVA: 0x0004A448 File Offset: 0x00048648
		public ICssStyleSheet Default
		{
			get
			{
				return this._default ?? this.SetDefault(CssStyleEngine.DefaultSource);
			}
		}

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x06001355 RID: 4949 RVA: 0x0004A45F File Offset: 0x0004865F
		// (set) Token: 0x06001356 RID: 4950 RVA: 0x0004A467 File Offset: 0x00048667
		public CssParserOptions Options
		{
			get
			{
				return this._options;
			}
			set
			{
				this._options = value;
			}
		}

		// Token: 0x06001357 RID: 4951 RVA: 0x0004A470 File Offset: 0x00048670
		public ICssStyleSheet SetDefault(string sourceCode)
		{
			ICssStyleSheet cssStyleSheet = new CssParser(this._options, Configuration.Default).ParseStylesheet(sourceCode);
			this._default = cssStyleSheet;
			return cssStyleSheet;
		}

		// Token: 0x06001358 RID: 4952 RVA: 0x0004A49C File Offset: 0x0004869C
		public async Task<IStyleSheet> ParseStylesheetAsync(IResponse response, StyleOptions options, CancellationToken cancel)
		{
			IBrowsingContext context = options.Context;
			IConfiguration configuration = context.Configuration;
			CssParser cssParser = new CssParser(this._options, configuration);
			Url address = response.Address;
			string text = ((address != null) ? address.Href : null);
			CssStyleSheet sheet = new CssStyleSheet(cssParser, text, options.Element)
			{
				IsDisabled = options.IsDisabled
			};
			TextSource textSource = new TextSource(response.Content, null);
			CssTokenizer cssTokenizer = new CssTokenizer(textSource);
			cssTokenizer.Error += delegate(object _, CssErrorEvent ev)
			{
				context.Fire(ev);
			};
			new CssBuilder(cssTokenizer, cssParser);
			context.Fire(new CssParseEvent(sheet, false));
			await cssParser.ParseStylesheetAsync(sheet, textSource).ConfigureAwait(false);
			context.Fire(new CssParseEvent(sheet, true));
			return sheet;
		}

		// Token: 0x06001359 RID: 4953 RVA: 0x0004A4F4 File Offset: 0x000486F4
		public ICssStyleDeclaration ParseDeclaration(string source, StyleOptions options)
		{
			IConfiguration configuration = options.Context.Configuration;
			CssStyleDeclaration cssStyleDeclaration = new CssStyleDeclaration(new CssParser(this._options, configuration));
			cssStyleDeclaration.Update(source);
			return cssStyleDeclaration;
		}

		// Token: 0x0600135A RID: 4954 RVA: 0x0004A528 File Offset: 0x00048728
		public IMediaList ParseMedia(string source, StyleOptions options)
		{
			IConfiguration configuration = options.Context.Configuration;
			return new MediaList(new CssParser(this._options, configuration))
			{
				MediaText = source
			};
		}

		// Token: 0x04000A99 RID: 2713
		private ICssStyleSheet _default;

		// Token: 0x04000A9A RID: 2714
		private CssParserOptions _options;

		// Token: 0x04000A9B RID: 2715
		public static readonly string DefaultSource = "\nhtml, address,\nblockquote,\nbody, dd, div,\ndl, dt, fieldset, form,\nframe, frameset,\nh1, h2, h3, h4,\nh5, h6, noframes,\nol, p, ul, center,\ndir, hr, menu, pre   { display: block; unicode-bidi: embed }\nli              { display: list-item }\nhead            { display: none }\ntable           { display: table }\ntr              { display: table-row }\nthead           { display: table-header-group }\ntbody           { display: table-row-group }\ntfoot           { display: table-footer-group }\ncol             { display: table-column }\ncolgroup        { display: table-column-group }\ntd, th          { display: table-cell }\ncaption         { display: table-caption }\nth              { font-weight: bolder; text-align: center }\ncaption         { text-align: center }\nbody            { margin: 8px }\nh1              { font-size: 2em; margin: .67em 0 }\nh2              { font-size: 1.5em; margin: .75em 0 }\nh3              { font-size: 1.17em; margin: .83em 0 }\nh4, p,\nblockquote, ul,\nfieldset, form,\nol, dl, dir,\nmenu            { margin: 1.12em 0 }\nh5              { font-size: .83em; margin: 1.5em 0 }\nh6              { font-size: .75em; margin: 1.67em 0 }\nh1, h2, h3, h4,\nh5, h6, b,\nstrong          { font-weight: bolder }\nblockquote      { margin-left: 40px; margin-right: 40px }\ni, cite, em,\nvar, address    { font-style: italic }\npre, tt, code,\nkbd, samp       { font-family: monospace }\npre             { white-space: pre }\nbutton, textarea,\ninput, select   { display: inline-block }\nbig             { font-size: 1.17em }\nsmall, sub, sup { font-size: .83em }\nsub             { vertical-align: sub }\nsup             { vertical-align: super }\ntable           { border-spacing: 2px; }\nthead, tbody,\ntfoot           { vertical-align: middle }\ntd, th, tr      { vertical-align: inherit }\ns, strike, del  { text-decoration: line-through }\nhr              { border: 1px inset }\nol, ul, dir,\nmenu, dd        { margin-left: 40px }\nol              { list-style-type: decimal }\nol ul, ul ol,\nul ul, ol ol    { margin-top: 0; margin-bottom: 0 }\nu, ins          { text-decoration: underline }\nbr:before       { content: '\\A'; white-space: pre-line }\ncenter          { text-align: center }\n:link, :visited { text-decoration: underline }\n:focus          { outline: thin dotted invert }\n\n/* Begin bidirectionality settings (do not change) */\nBDO[DIR='ltr']  { direction: ltr; unicode-bidi: bidi-override }\nBDO[DIR='rtl']  { direction: rtl; unicode-bidi: bidi-override }\n\n*[DIR='ltr']    { direction: ltr; unicode-bidi: embed }\n*[DIR='rtl']    { direction: rtl; unicode-bidi: embed }\n\n@media print {\n  h1            { page-break-before: always }\n  h1, h2, h3,\n  h4, h5, h6    { page-break-after: avoid }\n  ul, ol, dl    { page-break-before: avoid }\n}";
	}
}
