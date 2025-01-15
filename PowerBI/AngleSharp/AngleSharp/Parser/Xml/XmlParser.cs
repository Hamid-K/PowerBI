using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp.Dom.Xml;
using AngleSharp.Extensions;

namespace AngleSharp.Parser.Xml
{
	// Token: 0x0200005A RID: 90
	public class XmlParser
	{
		// Token: 0x060001D2 RID: 466 RVA: 0x0000D809 File Offset: 0x0000BA09
		public XmlParser()
			: this(Configuration.Default)
		{
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x0000D816 File Offset: 0x0000BA16
		public XmlParser(XmlParserOptions options)
			: this(options, Configuration.Default)
		{
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x0000D824 File Offset: 0x0000BA24
		public XmlParser(IConfiguration configuration)
			: this(default(XmlParserOptions), configuration)
		{
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x0000D841 File Offset: 0x0000BA41
		public XmlParser(XmlParserOptions options, IConfiguration configuration)
			: this(options, BrowsingContext.New(configuration))
		{
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x0000D850 File Offset: 0x0000BA50
		public XmlParser(XmlParserOptions options, IBrowsingContext context)
		{
			this._options = options;
			this._context = context;
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x0000D866 File Offset: 0x0000BA66
		public XmlParserOptions Options
		{
			get
			{
				return this._options;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x0000D86E File Offset: 0x0000BA6E
		public IBrowsingContext Context
		{
			get
			{
				return this._context;
			}
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x0000D876 File Offset: 0x0000BA76
		public IXmlDocument Parse(string source)
		{
			XmlDocument xmlDocument = this.CreateDocument(source);
			new XmlDomBuilder(xmlDocument, null).Parse(this._options);
			return xmlDocument;
		}

		// Token: 0x060001DA RID: 474 RVA: 0x0000D892 File Offset: 0x0000BA92
		public IXmlDocument Parse(Stream source)
		{
			XmlDocument xmlDocument = this.CreateDocument(source);
			new XmlDomBuilder(xmlDocument, null).Parse(this._options);
			return xmlDocument;
		}

		// Token: 0x060001DB RID: 475 RVA: 0x0000D8AE File Offset: 0x0000BAAE
		public Task<IXmlDocument> ParseAsync(string source)
		{
			return this.ParseAsync(source, CancellationToken.None);
		}

		// Token: 0x060001DC RID: 476 RVA: 0x0000D8BC File Offset: 0x0000BABC
		public Task<IXmlDocument> ParseAsync(Stream source)
		{
			return this.ParseAsync(source, CancellationToken.None);
		}

		// Token: 0x060001DD RID: 477 RVA: 0x0000D8CC File Offset: 0x0000BACC
		public async Task<IXmlDocument> ParseAsync(string source, CancellationToken cancel)
		{
			XmlDocument document = this.CreateDocument(source);
			await new XmlDomBuilder(document, null).ParseAsync(this._options, cancel).ConfigureAwait(false);
			return document;
		}

		// Token: 0x060001DE RID: 478 RVA: 0x0000D924 File Offset: 0x0000BB24
		public async Task<IXmlDocument> ParseAsync(Stream source, CancellationToken cancel)
		{
			XmlDocument document = this.CreateDocument(source);
			await new XmlDomBuilder(document, null).ParseAsync(this._options, cancel).ConfigureAwait(false);
			return document;
		}

		// Token: 0x060001DF RID: 479 RVA: 0x0000D97C File Offset: 0x0000BB7C
		private XmlDocument CreateDocument(string source)
		{
			TextSource textSource = new TextSource(source);
			return this.CreateDocument(textSource);
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x0000D998 File Offset: 0x0000BB98
		private XmlDocument CreateDocument(Stream source)
		{
			TextSource textSource = new TextSource(source, this._context.Configuration.DefaultEncoding());
			return this.CreateDocument(textSource);
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x0000D9C3 File Offset: 0x0000BBC3
		private XmlDocument CreateDocument(TextSource textSource)
		{
			return new XmlDocument(this._context, textSource);
		}

		// Token: 0x04000207 RID: 519
		private readonly XmlParserOptions _options;

		// Token: 0x04000208 RID: 520
		private readonly IBrowsingContext _context;
	}
}
