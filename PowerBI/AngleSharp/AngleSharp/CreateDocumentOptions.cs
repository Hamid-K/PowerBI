using System;
using AngleSharp.Dom;
using AngleSharp.Extensions;
using AngleSharp.Html;
using AngleSharp.Network;

namespace AngleSharp
{
	// Token: 0x0200001A RID: 26
	public sealed class CreateDocumentOptions
	{
		// Token: 0x060000EE RID: 238 RVA: 0x00005964 File Offset: 0x00003B64
		public CreateDocumentOptions(IResponse response, IConfiguration configuration, IDocument ancestor = null)
		{
			MimeType contentType = response.GetContentType(MimeTypeNames.Html);
			string parameter = contentType.GetParameter(AttributeNames.Charset);
			TextSource textSource = new TextSource(response.Content, configuration.DefaultEncoding());
			if (!string.IsNullOrEmpty(parameter) && TextEncoding.IsSupported(parameter))
			{
				textSource.CurrentEncoding = TextEncoding.Resolve(parameter);
			}
			this._source = textSource;
			this._contentType = contentType;
			this._response = response;
			this._ancestor = ancestor;
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000EF RID: 239 RVA: 0x000059D9 File Offset: 0x00003BD9
		public IResponse Response
		{
			get
			{
				return this._response;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x000059E1 File Offset: 0x00003BE1
		public MimeType ContentType
		{
			get
			{
				return this._contentType;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x000059E9 File Offset: 0x00003BE9
		public TextSource Source
		{
			get
			{
				return this._source;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x000059F1 File Offset: 0x00003BF1
		public IDocument ImportAncestor
		{
			get
			{
				return this._ancestor;
			}
		}

		// Token: 0x0400004F RID: 79
		private readonly IResponse _response;

		// Token: 0x04000050 RID: 80
		private readonly MimeType _contentType;

		// Token: 0x04000051 RID: 81
		private readonly TextSource _source;

		// Token: 0x04000052 RID: 82
		private readonly IDocument _ancestor;
	}
}
