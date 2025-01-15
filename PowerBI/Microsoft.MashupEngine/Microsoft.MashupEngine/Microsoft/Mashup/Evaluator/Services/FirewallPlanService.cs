using System;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.Evaluator.Services
{
	// Token: 0x02001D83 RID: 7555
	public class FirewallPlanService : IFirewallPlanService
	{
		// Token: 0x0600BBB2 RID: 48050 RVA: 0x0025FB20 File Offset: 0x0025DD20
		public FirewallPlanService(IFirewallPlan firewallPlan)
		{
			this.firewallPlan = firewallPlan;
		}

		// Token: 0x17002E5B RID: 11867
		// (get) Token: 0x0600BBB3 RID: 48051 RVA: 0x0025FB2F File Offset: 0x0025DD2F
		public IFirewallPlan FirewallPlan
		{
			get
			{
				return this.firewallPlan;
			}
		}

		// Token: 0x04005F7A RID: 24442
		private readonly IFirewallPlan firewallPlan;
	}
}
