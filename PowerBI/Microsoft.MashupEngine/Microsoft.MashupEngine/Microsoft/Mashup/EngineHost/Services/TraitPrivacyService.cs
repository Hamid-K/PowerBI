using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001B49 RID: 6985
	public class TraitPrivacyService : ITraitPrivacyService
	{
		// Token: 0x0600AEC4 RID: 44740 RVA: 0x0023C859 File Offset: 0x0023AA59
		public void VerifyPrivacyTrait(IResource resource, IValue trait)
		{
			throw new FirewallFlowException2(new IResource[] { resource }, Strings.TraitFirewallRuleRequired);
		}
	}
}
