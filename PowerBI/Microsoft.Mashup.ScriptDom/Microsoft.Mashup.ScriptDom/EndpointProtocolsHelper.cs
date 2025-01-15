using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000134 RID: 308
	internal class EndpointProtocolsHelper : OptionsHelper<EndpointProtocol>
	{
		// Token: 0x060014C8 RID: 5320 RVA: 0x00090F52 File Offset: 0x0008F152
		private EndpointProtocolsHelper()
		{
			base.AddOptionMapping(EndpointProtocol.Tcp, "TCP");
			base.AddOptionMapping(EndpointProtocol.Http, "HTTP");
		}

		// Token: 0x04001175 RID: 4469
		internal static readonly EndpointProtocolsHelper Instance = new EndpointProtocolsHelper();
	}
}
