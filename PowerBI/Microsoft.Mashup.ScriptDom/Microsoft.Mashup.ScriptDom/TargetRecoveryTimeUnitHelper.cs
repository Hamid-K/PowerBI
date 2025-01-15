using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000178 RID: 376
	[Serializable]
	internal class TargetRecoveryTimeUnitHelper : OptionsHelper<TimeUnit>
	{
		// Token: 0x06002122 RID: 8482 RVA: 0x0015C960 File Offset: 0x0015AB60
		private TargetRecoveryTimeUnitHelper()
		{
			base.AddOptionMapping(TimeUnit.Minutes, "MINUTES");
			base.AddOptionMapping(TimeUnit.Seconds, "SECONDS");
		}

		// Token: 0x04001934 RID: 6452
		internal static readonly TargetRecoveryTimeUnitHelper Instance = new TargetRecoveryTimeUnitHelper();
	}
}
