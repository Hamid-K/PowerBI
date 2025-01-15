using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp.Dom;
using AngleSharp.Dom.Html;
using AngleSharp.Dom.Svg;
using AngleSharp.Dom.Xml;
using AngleSharp.Network;

namespace AngleSharp.Services.Default
{
	// Token: 0x02000048 RID: 72
	public class DocumentFactory : IDocumentFactory
	{
		// Token: 0x06000179 RID: 377 RVA: 0x000097B0 File Offset: 0x000079B0
		public void Register(string contentType, DocumentFactory.Creator creator)
		{
			this._creators.Add(contentType, creator);
		}

		// Token: 0x0600017A RID: 378 RVA: 0x000097C0 File Offset: 0x000079C0
		public DocumentFactory.Creator Unregister(string contentType)
		{
			DocumentFactory.Creator creator = null;
			if (this._creators.TryGetValue(contentType, out creator))
			{
				this._creators.Remove(contentType);
			}
			return creator;
		}

		// Token: 0x0600017B RID: 379 RVA: 0x000097ED File Offset: 0x000079ED
		protected virtual Task<IDocument> CreateDefaultAsync(IBrowsingContext context, CreateDocumentOptions options, CancellationToken cancellationToken)
		{
			return HtmlDocument.LoadAsync(context, options, cancellationToken);
		}

		// Token: 0x0600017C RID: 380 RVA: 0x000097F8 File Offset: 0x000079F8
		public Task<IDocument> CreateAsync(IBrowsingContext context, CreateDocumentOptions options, CancellationToken cancellationToken)
		{
			MimeType contentType = options.ContentType;
			foreach (KeyValuePair<string, DocumentFactory.Creator> keyValuePair in this._creators)
			{
				if (contentType.Represents(keyValuePair.Key))
				{
					return keyValuePair.Value(context, options, cancellationToken);
				}
			}
			return this.CreateDefaultAsync(context, options, cancellationToken);
		}

		// Token: 0x040001C6 RID: 454
		private readonly Dictionary<string, DocumentFactory.Creator> _creators = new Dictionary<string, DocumentFactory.Creator>
		{
			{
				MimeTypeNames.Xml,
				new DocumentFactory.Creator(XmlDocument.LoadAsync)
			},
			{
				MimeTypeNames.ApplicationXml,
				new DocumentFactory.Creator(XmlDocument.LoadAsync)
			},
			{
				MimeTypeNames.Svg,
				new DocumentFactory.Creator(SvgDocument.LoadAsync)
			},
			{
				MimeTypeNames.Html,
				new DocumentFactory.Creator(HtmlDocument.LoadAsync)
			},
			{
				MimeTypeNames.ApplicationXHtml,
				new DocumentFactory.Creator(HtmlDocument.LoadAsync)
			},
			{
				MimeTypeNames.Plain,
				new DocumentFactory.Creator(HtmlDocument.LoadTextAsync)
			},
			{
				MimeTypeNames.ApplicationJson,
				new DocumentFactory.Creator(HtmlDocument.LoadTextAsync)
			},
			{
				MimeTypeNames.DefaultJavaScript,
				new DocumentFactory.Creator(HtmlDocument.LoadTextAsync)
			},
			{
				MimeTypeNames.Css,
				new DocumentFactory.Creator(HtmlDocument.LoadTextAsync)
			}
		};

		// Token: 0x02000427 RID: 1063
		// (Invoke) Token: 0x060021FA RID: 8698
		public delegate Task<IDocument> Creator(IBrowsingContext context, CreateDocumentOptions options, CancellationToken cancellationToken);
	}
}
