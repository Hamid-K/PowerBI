using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000168 RID: 360
	[Serializable]
	internal class RaiseErrorOptionsHelper : OptionsHelper<RaiseErrorOptions>
	{
		// Token: 0x06002118 RID: 8472 RVA: 0x0015C7B3 File Offset: 0x0015A9B3
		private RaiseErrorOptionsHelper()
		{
			base.AddOptionMapping(RaiseErrorOptions.Log, "LOG");
			base.AddOptionMapping(RaiseErrorOptions.NoWait, "NOWAIT");
			base.AddOptionMapping(RaiseErrorOptions.SetError, "SETERROR");
		}

		// Token: 0x040018C0 RID: 6336
		internal static readonly RaiseErrorOptionsHelper Instance = new RaiseErrorOptionsHelper();
	}
}
