using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000136 RID: 310
	internal class EndpointStateHelper : OptionsHelper<EndpointState>
	{
		// Token: 0x060014CA RID: 5322 RVA: 0x00090F7E File Offset: 0x0008F17E
		private EndpointStateHelper()
		{
			base.AddOptionMapping(EndpointState.Disabled, "DISABLED");
			base.AddOptionMapping(EndpointState.Started, "STARTED");
			base.AddOptionMapping(EndpointState.Stopped, "STOPPED");
		}

		// Token: 0x0400117B RID: 4475
		internal static readonly EndpointStateHelper Instance = new EndpointStateHelper();
	}
}
