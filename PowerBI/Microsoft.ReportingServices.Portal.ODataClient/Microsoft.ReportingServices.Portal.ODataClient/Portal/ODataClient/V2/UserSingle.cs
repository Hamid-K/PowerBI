using System;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x02000060 RID: 96
	[OriginalName("UserSingle")]
	public class UserSingle : DataServiceQuerySingle<User>
	{
		// Token: 0x06000452 RID: 1106 RVA: 0x000095D9 File Offset: 0x000077D9
		public UserSingle(DataServiceContext context, string path)
			: base(context, path)
		{
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x000095E3 File Offset: 0x000077E3
		public UserSingle(DataServiceContext context, string path, bool isComposable)
			: base(context, path, isComposable)
		{
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x000095EE File Offset: 0x000077EE
		public UserSingle(DataServiceQuerySingle<User> query)
			: base(query)
		{
		}
	}
}
