using System;
using System.Collections.Generic;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.DocumentHost
{
	// Token: 0x0200193E RID: 6462
	public class FirewallPlanMinimizer : IFirewallPlanMinimizer
	{
		// Token: 0x0600A41B RID: 42011 RVA: 0x0021FA65 File Offset: 0x0021DC65
		public FirewallPlanMinimizer(IFirewallPlanCreator creator)
		{
			this.creator = creator;
		}

		// Token: 0x0600A41C RID: 42012 RVA: 0x0021FA74 File Offset: 0x0021DC74
		public IFirewallPlan GroupPlanForPartitions(IFirewallPlan firewallPlan, IPartitionedDocument document, IEnumerable<IPartitionKey> partitionKeys)
		{
			IMemberLetPartitionedDocument memberLetPartitionedDocument = (IMemberLetPartitionedDocument)document;
			firewallPlan = new ParameterTrimmer(this.creator, firewallPlan, memberLetPartitionedDocument).TrimParameters();
			return new FirewallPlanGrouper(this.creator, firewallPlan, memberLetPartitionedDocument).GroupPlanForPartitions(partitionKeys);
		}

		// Token: 0x0600A41D RID: 42013 RVA: 0x0021FAB0 File Offset: 0x0021DCB0
		public IFirewallPlan TrimPlanForPartition(IFirewallPlan firewallPlan, IPartitionedDocument document, IPartitionKey partitionKey)
		{
			IMemberLetPartitionedDocument memberLetPartitionedDocument = (IMemberLetPartitionedDocument)document;
			FirewallPlanTrimmer firewallPlanTrimmer = new FirewallPlanTrimmer(this.creator);
			firewallPlan = firewallPlanTrimmer.RemovePartitionsWithoutDataSources(firewallPlan, partitionKey);
			firewallPlan = new FirewallPlanGrouper2(this.creator, firewallPlan, memberLetPartitionedDocument).GroupPlanForPartition(partitionKey);
			return firewallPlanTrimmer.RemoveUnreferencedPartitions(firewallPlan, partitionKey);
		}

		// Token: 0x0400557A RID: 21882
		private readonly IFirewallPlanCreator creator;
	}
}
