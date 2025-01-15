using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.DocumentHost
{
	// Token: 0x02001936 RID: 6454
	internal static class FirewallPlanAnnotator
	{
		// Token: 0x0600A3FB RID: 41979 RVA: 0x0021EF08 File Offset: 0x0021D108
		public static void AnnotateFirewallPlan(IFirewallPlan firewallPlan, IDocumentAnalysisInfo documentInfo)
		{
			foreach (IFirewallPartitionPlan firewallPartitionPlan in firewallPlan.PartitionPlans)
			{
				IEnumerable<IResource> enumerable;
				if (documentInfo.GetPartitionInfo(firewallPartitionPlan.PartitionKey).TryGetResources(out enumerable) && !firewallPartitionPlan.Resources.Any<IResource>())
				{
					firewallPartitionPlan.AddResources(enumerable);
				}
			}
		}
	}
}
