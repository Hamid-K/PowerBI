using System;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x02000064 RID: 100
	[OriginalName("PowerBIUserInfoSingle")]
	public class PowerBIUserInfoSingle : DataServiceQuerySingle<PowerBIUserInfo>
	{
		// Token: 0x06000470 RID: 1136 RVA: 0x00009811 File Offset: 0x00007A11
		public PowerBIUserInfoSingle(DataServiceContext context, string path)
			: base(context, path)
		{
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x0000981B File Offset: 0x00007A1B
		public PowerBIUserInfoSingle(DataServiceContext context, string path, bool isComposable)
			: base(context, path, isComposable)
		{
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x00009826 File Offset: 0x00007A26
		public PowerBIUserInfoSingle(DataServiceQuerySingle<PowerBIUserInfo> query)
			: base(query)
		{
		}
	}
}
