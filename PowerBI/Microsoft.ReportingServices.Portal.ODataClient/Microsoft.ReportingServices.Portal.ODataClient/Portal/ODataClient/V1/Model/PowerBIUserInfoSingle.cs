using System;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x020000E7 RID: 231
	[OriginalName("PowerBIUserInfoSingle")]
	public class PowerBIUserInfoSingle : DataServiceQuerySingle<PowerBIUserInfo>
	{
		// Token: 0x06000A3F RID: 2623 RVA: 0x000148D5 File Offset: 0x00012AD5
		public PowerBIUserInfoSingle(DataServiceContext context, string path)
			: base(context, path)
		{
		}

		// Token: 0x06000A40 RID: 2624 RVA: 0x000148DF File Offset: 0x00012ADF
		public PowerBIUserInfoSingle(DataServiceContext context, string path, bool isComposable)
			: base(context, path, isComposable)
		{
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x000148EA File Offset: 0x00012AEA
		public PowerBIUserInfoSingle(DataServiceQuerySingle<PowerBIUserInfo> query)
			: base(query)
		{
		}
	}
}
