using System;
using Microsoft.ProgramSynthesis.DslLibrary.Numbers;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Models;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Pipeline
{
	// Token: 0x0200199F RID: 6559
	public class RoundDateTimeTransformModel : PipelineModel
	{
		// Token: 0x17002396 RID: 9110
		// (get) Token: 0x0600D659 RID: 54873 RVA: 0x002D9DD8 File Offset: 0x002D7FD8
		// (set) Token: 0x0600D65A RID: 54874 RVA: 0x002D9DE0 File Offset: 0x002D7FE0
		public RoundDatePeriodCeiling Ceiling { get; set; }

		// Token: 0x17002397 RID: 9111
		// (get) Token: 0x0600D65B RID: 54875 RVA: 0x002D9DE9 File Offset: 0x002D7FE9
		// (set) Token: 0x0600D65C RID: 54876 RVA: 0x002D9DF1 File Offset: 0x002D7FF1
		public RoundingMode Mode { get; set; }

		// Token: 0x17002398 RID: 9112
		// (get) Token: 0x0600D65D RID: 54877 RVA: 0x002D9DFA File Offset: 0x002D7FFA
		// (set) Token: 0x0600D65E RID: 54878 RVA: 0x002D9E02 File Offset: 0x002D8002
		public RoundDateTimePeriod Period { get; set; }

		// Token: 0x0600D65F RID: 54879 RVA: 0x002D9E0C File Offset: 0x002D800C
		public override string ToOperatorString()
		{
			string text = ((this.Ceiling == RoundDatePeriodCeiling.Ceiling) ? string.Empty : (", " + this.Ceiling.ToString()));
			return string.Format("Round({0}, {1}{2})", this.Mode, this.Period, text);
		}
	}
}
