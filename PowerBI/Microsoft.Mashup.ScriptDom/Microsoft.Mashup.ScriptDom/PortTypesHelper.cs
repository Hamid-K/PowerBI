using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200013F RID: 319
	[Serializable]
	internal class PortTypesHelper : OptionsHelper<PortTypes>
	{
		// Token: 0x060014D0 RID: 5328 RVA: 0x0009103E File Offset: 0x0008F23E
		private PortTypesHelper()
		{
			base.AddOptionMapping(PortTypes.Clear, "CLEAR");
			base.AddOptionMapping(PortTypes.Ssl, "SSL");
		}

		// Token: 0x040011AA RID: 4522
		internal static readonly PortTypesHelper Instance = new PortTypesHelper();
	}
}
