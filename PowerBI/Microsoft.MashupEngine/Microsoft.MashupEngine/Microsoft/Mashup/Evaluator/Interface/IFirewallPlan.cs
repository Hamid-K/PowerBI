using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Evaluator.Interface
{
	// Token: 0x02001E05 RID: 7685
	public interface IFirewallPlan
	{
		// Token: 0x17002EAF RID: 11951
		// (get) Token: 0x0600BDC0 RID: 48576
		IEnumerable<IFirewallPartitionPlan> PartitionPlans { get; }
	}
}
