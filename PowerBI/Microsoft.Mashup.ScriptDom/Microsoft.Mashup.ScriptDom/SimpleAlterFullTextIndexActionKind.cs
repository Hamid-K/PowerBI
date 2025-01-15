using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020000FC RID: 252
	internal enum SimpleAlterFullTextIndexActionKind
	{
		// Token: 0x04000B0B RID: 2827
		None,
		// Token: 0x04000B0C RID: 2828
		Enable,
		// Token: 0x04000B0D RID: 2829
		Disable,
		// Token: 0x04000B0E RID: 2830
		SetChangeTrackingManual,
		// Token: 0x04000B0F RID: 2831
		SetChangeTrackingAuto,
		// Token: 0x04000B10 RID: 2832
		SetChangeTrackingOff,
		// Token: 0x04000B11 RID: 2833
		StartFullPopulation,
		// Token: 0x04000B12 RID: 2834
		StartIncrementalPopulation,
		// Token: 0x04000B13 RID: 2835
		StartUpdatePopulation,
		// Token: 0x04000B14 RID: 2836
		StopPopulation,
		// Token: 0x04000B15 RID: 2837
		PausePopulation,
		// Token: 0x04000B16 RID: 2838
		ResumePopulation
	}
}
