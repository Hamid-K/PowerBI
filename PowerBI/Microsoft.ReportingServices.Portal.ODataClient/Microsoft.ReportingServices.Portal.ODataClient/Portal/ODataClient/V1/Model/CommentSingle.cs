using System;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x020000CC RID: 204
	[OriginalName("CommentSingle")]
	public class CommentSingle : DataServiceQuerySingle<Comment>
	{
		// Token: 0x06000910 RID: 2320 RVA: 0x000129D5 File Offset: 0x00010BD5
		public CommentSingle(DataServiceContext context, string path)
			: base(context, path)
		{
		}

		// Token: 0x06000911 RID: 2321 RVA: 0x000129DF File Offset: 0x00010BDF
		public CommentSingle(DataServiceContext context, string path, bool isComposable)
			: base(context, path, isComposable)
		{
		}

		// Token: 0x06000912 RID: 2322 RVA: 0x000129EA File Offset: 0x00010BEA
		public CommentSingle(DataServiceQuerySingle<Comment> query)
			: base(query)
		{
		}
	}
}
