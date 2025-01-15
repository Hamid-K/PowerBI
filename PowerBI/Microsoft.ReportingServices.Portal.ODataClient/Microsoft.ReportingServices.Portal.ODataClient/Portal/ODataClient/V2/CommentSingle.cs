using System;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x02000020 RID: 32
	[OriginalName("CommentSingle")]
	public class CommentSingle : DataServiceQuerySingle<Comment>
	{
		// Token: 0x06000144 RID: 324 RVA: 0x00003D58 File Offset: 0x00001F58
		public CommentSingle(DataServiceContext context, string path)
			: base(context, path)
		{
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00003D62 File Offset: 0x00001F62
		public CommentSingle(DataServiceContext context, string path, bool isComposable)
			: base(context, path, isComposable)
		{
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00003D6D File Offset: 0x00001F6D
		public CommentSingle(DataServiceQuerySingle<Comment> query)
			: base(query)
		{
		}
	}
}
