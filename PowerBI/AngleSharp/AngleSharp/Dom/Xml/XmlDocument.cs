using System;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp.Dom.Events;
using AngleSharp.Extensions;
using AngleSharp.Network;
using AngleSharp.Parser.Xml;

namespace AngleSharp.Dom.Xml
{
	// Token: 0x020001AB RID: 427
	internal sealed class XmlDocument : Document, IXmlDocument, IDocument, INode, IEventTarget, IMarkupFormattable, IParentNode, IGlobalEventHandlers, IDocumentStyle, INonElementParentNode, IDisposable
	{
		// Token: 0x06000F12 RID: 3858 RVA: 0x00046B40 File Offset: 0x00044D40
		internal XmlDocument(IBrowsingContext context, TextSource source)
			: base(context ?? BrowsingContext.New(null), source)
		{
			base.ContentType = MimeTypeNames.Xml;
		}

		// Token: 0x06000F13 RID: 3859 RVA: 0x00046B5F File Offset: 0x00044D5F
		internal XmlDocument(IBrowsingContext context = null)
			: this(context, new TextSource(string.Empty))
		{
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06000F14 RID: 3860 RVA: 0x00046B72 File Offset: 0x00044D72
		public override IElement DocumentElement
		{
			get
			{
				return this.FindChild<IElement>();
			}
		}

		// Token: 0x06000F15 RID: 3861 RVA: 0x00046B7C File Offset: 0x00044D7C
		public override INode Clone(bool deep = true)
		{
			XmlDocument xmlDocument = new XmlDocument(base.Context, new TextSource(base.Source.Text));
			base.CloneDocument(xmlDocument, deep);
			return xmlDocument;
		}

		// Token: 0x06000F16 RID: 3862 RVA: 0x00046BB0 File Offset: 0x00044DB0
		internal static async Task<IDocument> LoadAsync(IBrowsingContext context, CreateDocumentOptions options, CancellationToken cancelToken)
		{
			XmlParserOptions xmlParserOptions = default(XmlParserOptions);
			XmlDocument document = new XmlDocument(context, options.Source);
			XmlDomBuilder xmlDomBuilder = new XmlDomBuilder(document, null);
			document.Setup(options);
			context.NavigateTo(document);
			context.Fire(new HtmlParseEvent(document, false));
			await xmlDomBuilder.ParseAsync(default(XmlParserOptions), cancelToken).ConfigureAwait(false);
			context.Fire(new HtmlParseEvent(document, true));
			return document;
		}

		// Token: 0x06000F17 RID: 3863 RVA: 0x00003C25 File Offset: 0x00001E25
		protected override void SetTitle(string value)
		{
		}
	}
}
