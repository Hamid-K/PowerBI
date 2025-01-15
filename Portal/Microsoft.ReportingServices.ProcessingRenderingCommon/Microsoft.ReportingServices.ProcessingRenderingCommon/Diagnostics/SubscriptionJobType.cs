using System;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000092 RID: 146
	internal sealed class SubscriptionJobType : JobType
	{
		// Token: 0x0600048B RID: 1163 RVA: 0x0000DF60 File Offset: 0x0000C160
		public SubscriptionJobType(JobTypeEnum type)
			: base(type, JobSubTypeEnum.Subscription)
		{
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x0000DF6C File Offset: 0x0000C16C
		public static JobType CreateSubscriptionJobType(bool isDataDriven, JobTypeEnum type)
		{
			JobType jobType;
			if (isDataDriven)
			{
				jobType = new DataDrivenSubscriptionJobType(type);
			}
			else
			{
				jobType = new SubscriptionJobType(type);
			}
			return jobType;
		}
	}
}
