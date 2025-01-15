using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000138 RID: 312
	internal class EndpointTypesHelper : OptionsHelper<EndpointType>
	{
		// Token: 0x060014CC RID: 5324 RVA: 0x00090FB6 File Offset: 0x0008F1B6
		private EndpointTypesHelper()
		{
			base.AddOptionMapping(EndpointType.DatabaseMirroring, "DATABASE_MIRRORING");
			base.AddOptionMapping(EndpointType.Soap, "SOAP");
			base.AddOptionMapping(EndpointType.ServiceBroker, "SERVICE_BROKER");
			base.AddOptionMapping(EndpointType.TSql, "TSQL");
		}

		// Token: 0x04001182 RID: 4482
		internal static readonly EndpointTypesHelper Instance = new EndpointTypesHelper();
	}
}
