using System;

namespace Microsoft.ProgramSynthesis.Extraction.Json.Learning
{
	// Token: 0x02000B85 RID: 2949
	public class MultiTargetCode
	{
		// Token: 0x06004AE8 RID: 19176 RVA: 0x000EB556 File Offset: 0x000E9756
		public MultiTargetCode()
		{
			this.PowerQueryM = null;
			this.Pandas = null;
			this.PySpark = null;
		}

		// Token: 0x06004AE9 RID: 19177 RVA: 0x000EB573 File Offset: 0x000E9773
		public MultiTargetCode(TargetCode powerQueryM, TargetCode pandas, TargetCode pyspark)
		{
			this.PowerQueryM = powerQueryM;
			this.Pandas = pandas;
			this.PySpark = pyspark;
		}

		// Token: 0x17000D6D RID: 3437
		// (get) Token: 0x06004AEA RID: 19178 RVA: 0x000EB590 File Offset: 0x000E9790
		public TargetCode PowerQueryM { get; }

		// Token: 0x17000D6E RID: 3438
		// (get) Token: 0x06004AEB RID: 19179 RVA: 0x000EB598 File Offset: 0x000E9798
		public TargetCode Pandas { get; }

		// Token: 0x17000D6F RID: 3439
		// (get) Token: 0x06004AEC RID: 19180 RVA: 0x000EB5A0 File Offset: 0x000E97A0
		public TargetCode PySpark { get; }
	}
}
