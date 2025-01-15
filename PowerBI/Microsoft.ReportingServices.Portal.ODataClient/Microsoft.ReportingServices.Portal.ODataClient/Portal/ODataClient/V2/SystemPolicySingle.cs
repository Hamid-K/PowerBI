using System;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x02000049 RID: 73
	[OriginalName("SystemPolicySingle")]
	public class SystemPolicySingle : DataServiceQuerySingle<SystemPolicy>
	{
		// Token: 0x0600034C RID: 844 RVA: 0x00007DEB File Offset: 0x00005FEB
		public SystemPolicySingle(DataServiceContext context, string path)
			: base(context, path)
		{
		}

		// Token: 0x0600034D RID: 845 RVA: 0x00007DF5 File Offset: 0x00005FF5
		public SystemPolicySingle(DataServiceContext context, string path, bool isComposable)
			: base(context, path, isComposable)
		{
		}

		// Token: 0x0600034E RID: 846 RVA: 0x00007E00 File Offset: 0x00006000
		public SystemPolicySingle(DataServiceQuerySingle<SystemPolicy> query)
			: base(query)
		{
		}
	}
}
