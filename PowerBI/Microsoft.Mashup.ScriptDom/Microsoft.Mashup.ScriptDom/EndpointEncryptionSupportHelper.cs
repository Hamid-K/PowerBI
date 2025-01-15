using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000130 RID: 304
	internal class EndpointEncryptionSupportHelper : OptionsHelper<EndpointEncryptionSupport>
	{
		// Token: 0x060014C4 RID: 5316 RVA: 0x00090E63 File Offset: 0x0008F063
		private EndpointEncryptionSupportHelper()
		{
			base.AddOptionMapping(EndpointEncryptionSupport.Disabled, "DISABLED");
			base.AddOptionMapping(EndpointEncryptionSupport.Required, "REQUIRED");
			base.AddOptionMapping(EndpointEncryptionSupport.Supported, "SUPPORTED");
		}

		// Token: 0x04001160 RID: 4448
		internal static readonly EndpointEncryptionSupportHelper Instance = new EndpointEncryptionSupportHelper();
	}
}
