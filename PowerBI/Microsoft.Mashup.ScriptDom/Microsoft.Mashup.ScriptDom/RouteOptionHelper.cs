using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000122 RID: 290
	[Serializable]
	internal class RouteOptionHelper : OptionsHelper<RouteOptionKind>
	{
		// Token: 0x060014B4 RID: 5300 RVA: 0x00090A74 File Offset: 0x0008EC74
		private RouteOptionHelper()
		{
			base.AddOptionMapping(RouteOptionKind.Address, "ADDRESS");
			base.AddOptionMapping(RouteOptionKind.BrokerInstance, "BROKER_INSTANCE");
			base.AddOptionMapping(RouteOptionKind.Lifetime, "LIFETIME");
			base.AddOptionMapping(RouteOptionKind.MirrorAddress, "MIRROR_ADDRESS");
			base.AddOptionMapping(RouteOptionKind.ServiceName, "SERVICE_NAME");
		}

		// Token: 0x0400112C RID: 4396
		internal static readonly RouteOptionHelper Instance = new RouteOptionHelper();
	}
}
