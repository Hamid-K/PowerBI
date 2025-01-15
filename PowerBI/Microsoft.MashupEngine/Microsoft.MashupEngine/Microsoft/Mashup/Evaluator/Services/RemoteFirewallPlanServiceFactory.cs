using System;
using System.IO;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.Evaluator.Services
{
	// Token: 0x02001D8B RID: 7563
	public class RemoteFirewallPlanServiceFactory : IRemoteServiceFactory
	{
		// Token: 0x0600BBD0 RID: 48080 RVA: 0x0026018C File Offset: 0x0025E38C
		public IRemoteServiceStub CreateStub(IEngineHost engineHost, IMessenger messenger, BinaryWriter proxyInitArgs)
		{
			IFirewallPlanService firewallPlanService = engineHost.QueryService<IFirewallPlanService>();
			IFirewallPlan firewallPlan = ((firewallPlanService != null) ? firewallPlanService.FirewallPlan : null);
			proxyInitArgs.WriteBool(firewallPlan != null);
			if (firewallPlan != null)
			{
				proxyInitArgs.WriteIFirewallPlan(firewallPlan);
			}
			return EmptyStub.Instance;
		}

		// Token: 0x0600BBD1 RID: 48081 RVA: 0x002601C6 File Offset: 0x0025E3C6
		public IRemoteServiceProxy CreateProxy(IEngineHost engineHost, IMessenger messenger, BinaryReader proxyInitArgs)
		{
			if (proxyInitArgs.ReadBool())
			{
				return new EngineHostServiceProxy(new SimpleEngineHost<IFirewallPlanService>(new FirewallPlanService(proxyInitArgs.ReadIFirewallPlan())));
			}
			return EmptyProxy.Instance;
		}
	}
}
