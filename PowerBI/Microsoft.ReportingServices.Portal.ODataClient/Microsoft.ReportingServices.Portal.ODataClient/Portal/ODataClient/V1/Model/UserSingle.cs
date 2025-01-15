using System;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x020000E3 RID: 227
	[OriginalName("UserSingle")]
	public class UserSingle : DataServiceQuerySingle<User>
	{
		// Token: 0x06000A21 RID: 2593 RVA: 0x0001469D File Offset: 0x0001289D
		public UserSingle(DataServiceContext context, string path)
			: base(context, path)
		{
		}

		// Token: 0x06000A22 RID: 2594 RVA: 0x000146A7 File Offset: 0x000128A7
		public UserSingle(DataServiceContext context, string path, bool isComposable)
			: base(context, path, isComposable)
		{
		}

		// Token: 0x06000A23 RID: 2595 RVA: 0x000146B2 File Offset: 0x000128B2
		public UserSingle(DataServiceQuerySingle<User> query)
			: base(query)
		{
		}
	}
}
