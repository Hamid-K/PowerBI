using System;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x02000117 RID: 279
	[OriginalName("SystemPolicySingle")]
	public class SystemPolicySingle : DataServiceQuerySingle<SystemPolicy>
	{
		// Token: 0x06000C16 RID: 3094 RVA: 0x0001764D File Offset: 0x0001584D
		public SystemPolicySingle(DataServiceContext context, string path)
			: base(context, path)
		{
		}

		// Token: 0x06000C17 RID: 3095 RVA: 0x00017657 File Offset: 0x00015857
		public SystemPolicySingle(DataServiceContext context, string path, bool isComposable)
			: base(context, path, isComposable)
		{
		}

		// Token: 0x06000C18 RID: 3096 RVA: 0x00017662 File Offset: 0x00015862
		public SystemPolicySingle(DataServiceQuerySingle<SystemPolicy> query)
			: base(query)
		{
		}
	}
}
