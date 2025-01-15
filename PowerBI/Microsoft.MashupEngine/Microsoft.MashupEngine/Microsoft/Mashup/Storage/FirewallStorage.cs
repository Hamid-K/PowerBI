using System;

namespace Microsoft.Mashup.Storage
{
	// Token: 0x02002074 RID: 8308
	public abstract class FirewallStorage
	{
		// Token: 0x0600CB53 RID: 52051
		public abstract FirewallRule[] GetFirewallRules();

		// Token: 0x0600CB54 RID: 52052
		public abstract void SetFirewallRules(FirewallRule[] firewallRules);

		// Token: 0x0600CB55 RID: 52053
		public abstract void ClearFirewallRules(params Resource[] resources);
	}
}
