using System;
using Microsoft.OData.Core;

namespace Microsoft.Mashup.Engine1.Library.OData.V4
{
	// Token: 0x02000877 RID: 2167
	internal class ODataStreamReferenceValueWrapper : IODataStreamReferenceValueWrapper
	{
		// Token: 0x06003E56 RID: 15958 RVA: 0x000CB9FD File Offset: 0x000C9BFD
		public ODataStreamReferenceValueWrapper(ODataStreamReferenceValue streamReferenceValue)
		{
			this.streamReferenceValue = streamReferenceValue;
		}

		// Token: 0x17001478 RID: 5240
		// (get) Token: 0x06003E57 RID: 15959 RVA: 0x000CBA0C File Offset: 0x000C9C0C
		public string ContentType
		{
			get
			{
				return this.streamReferenceValue.ContentType;
			}
		}

		// Token: 0x17001479 RID: 5241
		// (get) Token: 0x06003E58 RID: 15960 RVA: 0x000CBA19 File Offset: 0x000C9C19
		public Uri EditLink
		{
			get
			{
				return this.streamReferenceValue.EditLink;
			}
		}

		// Token: 0x1700147A RID: 5242
		// (get) Token: 0x06003E59 RID: 15961 RVA: 0x000CBA26 File Offset: 0x000C9C26
		public string ETag
		{
			get
			{
				return this.streamReferenceValue.ETag;
			}
		}

		// Token: 0x1700147B RID: 5243
		// (get) Token: 0x06003E5A RID: 15962 RVA: 0x000CBA33 File Offset: 0x000C9C33
		public Uri ReadLink
		{
			get
			{
				return this.streamReferenceValue.ReadLink;
			}
		}

		// Token: 0x040020C6 RID: 8390
		private readonly ODataStreamReferenceValue streamReferenceValue;
	}
}
