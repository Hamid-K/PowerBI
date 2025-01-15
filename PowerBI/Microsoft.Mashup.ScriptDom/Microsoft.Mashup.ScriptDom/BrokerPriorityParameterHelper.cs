using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000044 RID: 68
	[Serializable]
	internal class BrokerPriorityParameterHelper : OptionsHelper<BrokerPriorityParameterType>
	{
		// Token: 0x060001BB RID: 443 RVA: 0x00005F22 File Offset: 0x00004122
		private BrokerPriorityParameterHelper()
		{
			base.AddOptionMapping(BrokerPriorityParameterType.ContractName, "CONTRACT_NAME");
			base.AddOptionMapping(BrokerPriorityParameterType.LocalServiceName, "LOCAL_SERVICE_NAME");
			base.AddOptionMapping(BrokerPriorityParameterType.PriorityLevel, "PRIORITY_LEVEL");
			base.AddOptionMapping(BrokerPriorityParameterType.RemoteServiceName, "REMOTE_SERVICE_NAME");
		}

		// Token: 0x04000127 RID: 295
		internal static readonly BrokerPriorityParameterHelper Instance = new BrokerPriorityParameterHelper();
	}
}
