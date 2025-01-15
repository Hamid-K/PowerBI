using System;

namespace Microsoft.ProgramSynthesis.Learning.Logging
{
	// Token: 0x02000760 RID: 1888
	[Flags]
	public enum LogInfo
	{
		// Token: 0x0400139D RID: 5021
		None = 0,
		// Token: 0x0400139E RID: 5022
		Branch = 1,
		// Token: 0x0400139F RID: 5023
		WitnessingPlan = 2,
		// Token: 0x040013A0 RID: 5024
		Witness = 4,
		// Token: 0x040013A1 RID: 5025
		Cluster = 8,
		// Token: 0x040013A2 RID: 5026
		ProgramSets = 16,
		// Token: 0x040013A3 RID: 5027
		BestProgram = 32,
		// Token: 0x040013A4 RID: 5028
		TopK = 64,
		// Token: 0x040013A5 RID: 5029
		MLTraining = 41,
		// Token: 0x040013A6 RID: 5030
		AllAbbreviated = -17,
		// Token: 0x040013A7 RID: 5031
		All = -1
	}
}
