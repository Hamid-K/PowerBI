using System;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000066 RID: 102
	internal sealed class DataDrivenSubscriptionJobType : JobType
	{
		// Token: 0x060002FF RID: 767 RVA: 0x0000AC2C File Offset: 0x00008E2C
		public DataDrivenSubscriptionJobType(JobTypeEnum type)
			: base(type, JobSubTypeEnum.DataDrivenSubscription)
		{
		}
	}
}
