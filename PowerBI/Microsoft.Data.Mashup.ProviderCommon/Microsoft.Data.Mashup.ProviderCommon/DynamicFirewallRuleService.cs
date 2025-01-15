using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.EngineHost.Services;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Data.Mashup.ProviderCommon
{
	// Token: 0x02000008 RID: 8
	internal class DynamicFirewallRuleService : IFirewallRuleService
	{
		// Token: 0x06000025 RID: 37 RVA: 0x000027EA File Offset: 0x000009EA
		public DynamicFirewallRuleService(IFirewallRuleService firewallRuleService, ConnectionContext connection, IEvaluationConstants evaluationConstants = null)
		{
			this.firewallRuleService = firewallRuleService;
			this.connectionContext = connection;
			this.evaluationConstants = evaluationConstants;
			this.cachedFirewallGroups = new Dictionary<IResource, FirewallGroup2>(ResourceComparer.Instance);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002818 File Offset: 0x00000A18
		public FirewallGroup2 CreateFirewallGroup(IResource resource)
		{
			Dictionary<IResource, FirewallGroup2> dictionary = this.cachedFirewallGroups;
			lock (dictionary)
			{
				FirewallGroup2 firewallGroup;
				if (this.cachedFirewallGroups.TryGetValue(resource, out firewallGroup))
				{
					return firewallGroup;
				}
			}
			return this.firewallRuleService.CreateFirewallGroup(resource);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002874 File Offset: 0x00000A74
		public FirewallGroup2 UpdateFirewallGroup(IResource resource, FirewallGroup2 originalGroup, IValue traits)
		{
			try
			{
				FirewallGroup2 firewallGroup;
				if (this.connectionContext.TryUpdateFirewallGroup(resource, originalGroup, traits, out firewallGroup))
				{
					Dictionary<IResource, FirewallGroup2> dictionary = this.cachedFirewallGroups;
					lock (dictionary)
					{
						this.cachedFirewallGroups[resource] = firewallGroup;
					}
					return firewallGroup;
				}
			}
			catch (Exception ex) when (ProviderTracing.TraceIsSafeException("DynamicFirewallRuleService/CreateFirewallGroup", ex, this.evaluationConstants, resource))
			{
			}
			return new FirewallGroup2(FirewallGroupType2.SingleUnclassified, false, resource);
		}

		// Token: 0x04000012 RID: 18
		private readonly ConnectionContext connectionContext;

		// Token: 0x04000013 RID: 19
		private readonly IFirewallRuleService firewallRuleService;

		// Token: 0x04000014 RID: 20
		private readonly IEvaluationConstants evaluationConstants;

		// Token: 0x04000015 RID: 21
		private readonly Dictionary<IResource, FirewallGroup2> cachedFirewallGroups;
	}
}
