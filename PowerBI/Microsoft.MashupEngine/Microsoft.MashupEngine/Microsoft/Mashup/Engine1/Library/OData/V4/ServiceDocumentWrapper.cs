using System;
using Microsoft.OData.Core;

namespace Microsoft.Mashup.Engine1.Library.OData.V4
{
	// Token: 0x0200089D RID: 2205
	internal sealed class ServiceDocumentWrapper
	{
		// Token: 0x06003F33 RID: 16179 RVA: 0x000CFB9C File Offset: 0x000CDD9C
		public ServiceDocumentWrapper(Uri serviceLocation)
			: this(new ODataServiceDocument(), serviceLocation)
		{
		}

		// Token: 0x06003F34 RID: 16180 RVA: 0x000CFBAA File Offset: 0x000CDDAA
		public ServiceDocumentWrapper(ODataServiceDocument serviceDocument, Uri serviceLocation)
		{
			this.serviceDocument = serviceDocument;
			this.serviceLocation = ODataUriNormalizer.NormalizeServiceDocumentUri(serviceLocation);
		}

		// Token: 0x17001496 RID: 5270
		// (get) Token: 0x06003F35 RID: 16181 RVA: 0x000CFBC5 File Offset: 0x000CDDC5
		public ODataServiceDocument Document
		{
			get
			{
				return this.serviceDocument;
			}
		}

		// Token: 0x17001497 RID: 5271
		// (get) Token: 0x06003F36 RID: 16182 RVA: 0x000CFBCD File Offset: 0x000CDDCD
		public Uri ServiceUri
		{
			get
			{
				return this.serviceLocation;
			}
		}

		// Token: 0x04002137 RID: 8503
		private readonly ODataServiceDocument serviceDocument;

		// Token: 0x04002138 RID: 8504
		private readonly Uri serviceLocation;
	}
}
