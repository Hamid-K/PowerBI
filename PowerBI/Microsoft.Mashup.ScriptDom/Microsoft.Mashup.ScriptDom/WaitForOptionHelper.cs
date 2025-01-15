using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000184 RID: 388
	[Serializable]
	internal class WaitForOptionHelper : OptionsHelper<WaitForOption>
	{
		// Token: 0x0600214C RID: 8524 RVA: 0x0015D08A File Offset: 0x0015B28A
		private WaitForOptionHelper()
		{
			base.AddOptionMapping(WaitForOption.Delay, "DELAY");
			base.AddOptionMapping(WaitForOption.Time, "TIME");
		}

		// Token: 0x04001980 RID: 6528
		internal static readonly WaitForOptionHelper Instance = new WaitForOptionHelper();
	}
}
