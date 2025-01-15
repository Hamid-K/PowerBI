using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Evaluator.Interface
{
	// Token: 0x02001E0A RID: 7690
	public interface IFirewallRuleService
	{
		// Token: 0x0600BDCF RID: 48591
		FirewallGroup2 CreateFirewallGroup(IResource resource);

		// Token: 0x0600BDD0 RID: 48592
		FirewallGroup2 UpdateFirewallGroup(IResource resource, FirewallGroup2 originalGroup, IValue traits);
	}
}
