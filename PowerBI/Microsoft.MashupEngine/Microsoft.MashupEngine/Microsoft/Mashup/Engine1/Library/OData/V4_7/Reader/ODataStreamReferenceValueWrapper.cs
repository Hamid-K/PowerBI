using System;
using Microsoft.OData;

namespace Microsoft.Mashup.Engine1.Library.OData.V4_7.Reader
{
	// Token: 0x0200079C RID: 1948
	internal class ODataStreamReferenceValueWrapper : IODataStreamReferenceValueWrapper
	{
		// Token: 0x06003914 RID: 14612 RVA: 0x000B7BD5 File Offset: 0x000B5DD5
		public ODataStreamReferenceValueWrapper(ODataStreamReferenceValue streamReferenceValue)
		{
			this.streamReferenceValue = streamReferenceValue;
		}

		// Token: 0x1700135C RID: 4956
		// (get) Token: 0x06003915 RID: 14613 RVA: 0x000B7BE4 File Offset: 0x000B5DE4
		public string ContentType
		{
			get
			{
				return this.streamReferenceValue.ContentType;
			}
		}

		// Token: 0x1700135D RID: 4957
		// (get) Token: 0x06003916 RID: 14614 RVA: 0x000B7BF1 File Offset: 0x000B5DF1
		public Uri EditLink
		{
			get
			{
				return this.streamReferenceValue.EditLink;
			}
		}

		// Token: 0x1700135E RID: 4958
		// (get) Token: 0x06003917 RID: 14615 RVA: 0x000B7BFE File Offset: 0x000B5DFE
		public string ETag
		{
			get
			{
				return this.streamReferenceValue.ETag;
			}
		}

		// Token: 0x1700135F RID: 4959
		// (get) Token: 0x06003918 RID: 14616 RVA: 0x000B7C0B File Offset: 0x000B5E0B
		public Uri ReadLink
		{
			get
			{
				return this.streamReferenceValue.ReadLink;
			}
		}

		// Token: 0x04001D6E RID: 7534
		private readonly ODataStreamReferenceValue streamReferenceValue;
	}
}
