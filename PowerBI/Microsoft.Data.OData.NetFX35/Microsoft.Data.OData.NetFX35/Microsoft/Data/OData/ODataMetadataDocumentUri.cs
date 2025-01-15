using System;

namespace Microsoft.Data.OData
{
	// Token: 0x02000150 RID: 336
	internal sealed class ODataMetadataDocumentUri
	{
		// Token: 0x060008E9 RID: 2281 RVA: 0x0001C48A File Offset: 0x0001A68A
		internal ODataMetadataDocumentUri(Uri baseUri)
		{
			ExceptionUtils.CheckArgumentNotNull<Uri>(baseUri, "baseUri");
			if (!baseUri.IsAbsoluteUri)
			{
				throw new ODataException(Strings.WriterValidationUtils_MessageWriterSettingsMetadataDocumentUriMustBeNullOrAbsolute(UriUtilsCommon.UriToString(baseUri)));
			}
			this.baseUri = baseUri;
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x060008EA RID: 2282 RVA: 0x0001C4BD File Offset: 0x0001A6BD
		internal Uri BaseUri
		{
			get
			{
				return this.baseUri;
			}
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x060008EB RID: 2283 RVA: 0x0001C4C5 File Offset: 0x0001A6C5
		// (set) Token: 0x060008EC RID: 2284 RVA: 0x0001C4CD File Offset: 0x0001A6CD
		internal string SelectClause
		{
			get
			{
				return this.selectClause;
			}
			set
			{
				this.selectClause = value;
			}
		}

		// Token: 0x04000364 RID: 868
		private readonly Uri baseUri;

		// Token: 0x04000365 RID: 869
		private string selectClause;
	}
}
