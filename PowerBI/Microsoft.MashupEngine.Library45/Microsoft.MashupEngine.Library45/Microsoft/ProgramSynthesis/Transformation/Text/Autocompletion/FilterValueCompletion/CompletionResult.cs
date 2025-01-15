using System;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Autocompletion.FilterValueCompletion
{
	// Token: 0x02001E1E RID: 7710
	public class CompletionResult
	{
		// Token: 0x17002ACA RID: 10954
		// (get) Token: 0x060101C5 RID: 65989 RVA: 0x00374ED1 File Offset: 0x003730D1
		public string Value { get; }

		// Token: 0x17002ACB RID: 10955
		// (get) Token: 0x060101C6 RID: 65990 RVA: 0x00374ED9 File Offset: 0x003730D9
		public double PercentagePresence { get; }

		// Token: 0x060101C7 RID: 65991 RVA: 0x00374EE1 File Offset: 0x003730E1
		public CompletionResult(string value, double percentagePresence)
		{
			this.Value = value;
			this.PercentagePresence = percentagePresence;
		}

		// Token: 0x060101C8 RID: 65992 RVA: 0x00374EF7 File Offset: 0x003730F7
		public override string ToString()
		{
			return this.Value;
		}
	}
}
