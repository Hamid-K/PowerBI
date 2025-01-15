using System;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x02000018 RID: 24
	[OriginalName("CacheOptionsSingle")]
	public class CacheOptionsSingle : DataServiceQuerySingle<CacheOptions>
	{
		// Token: 0x060000DB RID: 219 RVA: 0x00003355 File Offset: 0x00001555
		public CacheOptionsSingle(DataServiceContext context, string path)
			: base(context, path)
		{
		}

		// Token: 0x060000DC RID: 220 RVA: 0x0000335F File Offset: 0x0000155F
		public CacheOptionsSingle(DataServiceContext context, string path, bool isComposable)
			: base(context, path, isComposable)
		{
		}

		// Token: 0x060000DD RID: 221 RVA: 0x0000336A File Offset: 0x0000156A
		public CacheOptionsSingle(DataServiceQuerySingle<CacheOptions> query)
			: base(query)
		{
		}
	}
}
