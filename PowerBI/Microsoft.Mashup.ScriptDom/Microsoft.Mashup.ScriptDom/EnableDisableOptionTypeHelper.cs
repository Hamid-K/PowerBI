using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000098 RID: 152
	internal class EnableDisableOptionTypeHelper : OptionsHelper<EnableDisableOptionType>
	{
		// Token: 0x060002B0 RID: 688 RVA: 0x0000B8F3 File Offset: 0x00009AF3
		private EnableDisableOptionTypeHelper()
		{
			base.AddOptionMapping(EnableDisableOptionType.Enable, "ENABLE");
			base.AddOptionMapping(EnableDisableOptionType.Disable, "DISABLE");
		}

		// Token: 0x040003AC RID: 940
		internal static readonly EnableDisableOptionTypeHelper Instance = new EnableDisableOptionTypeHelper();
	}
}
