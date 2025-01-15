using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Evaluator.Interface
{
	// Token: 0x02001E06 RID: 7686
	public interface IFirewallPartitionPlan
	{
		// Token: 0x17002EB0 RID: 11952
		// (get) Token: 0x0600BDC1 RID: 48577
		IPartitionKey PartitionKey { get; }

		// Token: 0x17002EB1 RID: 11953
		// (get) Token: 0x0600BDC2 RID: 48578
		bool IsCyclic { get; }

		// Token: 0x17002EB2 RID: 11954
		// (get) Token: 0x0600BDC3 RID: 48579
		int EvaluationOrder { get; }

		// Token: 0x17002EB3 RID: 11955
		// (get) Token: 0x0600BDC4 RID: 48580
		Exception Exception { get; }

		// Token: 0x17002EB4 RID: 11956
		// (get) Token: 0x0600BDC5 RID: 48581
		IEnumerable<IPartitionKey> Inputs { get; }

		// Token: 0x17002EB5 RID: 11957
		// (get) Token: 0x0600BDC6 RID: 48582
		IEnumerable<IResource> Resources { get; }

		// Token: 0x0600BDC7 RID: 48583
		void AddException(Exception exception);

		// Token: 0x0600BDC8 RID: 48584
		void AddResources(IEnumerable<IResource> resources);
	}
}
