using System;
using Microsoft.ProgramSynthesis.DslLibrary.EntityDetectors;
using Microsoft.ProgramSynthesis.Learning.Logging;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Wrangling.Constraints
{
	// Token: 0x02000231 RID: 561
	public abstract class DSLOptions
	{
		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06000C03 RID: 3075 RVA: 0x0002486A File Offset: 0x00022A6A
		// (set) Token: 0x06000C04 RID: 3076 RVA: 0x00024872 File Offset: 0x00022A72
		public Optional<string> SynthesisLogFilenamePrefix { get; set; }

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06000C05 RID: 3077 RVA: 0x0002487B File Offset: 0x00022A7B
		// (set) Token: 0x06000C06 RID: 3078 RVA: 0x00024883 File Offset: 0x00022A83
		public LogInfo LogInfo { get; set; }

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06000C07 RID: 3079 RVA: 0x0002488C File Offset: 0x00022A8C
		// (set) Token: 0x06000C08 RID: 3080 RVA: 0x00024894 File Offset: 0x00022A94
		public bool UseDynamicSoundnessCheck { get; set; }

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06000C09 RID: 3081 RVA: 0x0002489D File Offset: 0x00022A9D
		// (set) Token: 0x06000C0A RID: 3082 RVA: 0x000248A5 File Offset: 0x00022AA5
		public EntityDetectorsMap EntityDetectorsMap { get; set; }
	}
}
