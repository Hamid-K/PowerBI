using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Pipeline
{
	// Token: 0x0200198B RID: 6539
	public class MatchModel : PipelineModel
	{
		// Token: 0x17002377 RID: 9079
		// (get) Token: 0x0600D5F8 RID: 54776 RVA: 0x002D9858 File Offset: 0x002D7A58
		// (set) Token: 0x0600D5F9 RID: 54777 RVA: 0x002D9860 File Offset: 0x002D7A60
		public bool End { get; set; }

		// Token: 0x17002378 RID: 9080
		// (get) Token: 0x0600D5FA RID: 54778 RVA: 0x002D9869 File Offset: 0x002D7A69
		// (set) Token: 0x0600D5FB RID: 54779 RVA: 0x002D9871 File Offset: 0x002D7A71
		public bool Full { get; set; }

		// Token: 0x17002379 RID: 9081
		// (get) Token: 0x0600D5FC RID: 54780 RVA: 0x002D987A File Offset: 0x002D7A7A
		// (set) Token: 0x0600D5FD RID: 54781 RVA: 0x002D9882 File Offset: 0x002D7A82
		public string Pattern { get; set; }

		// Token: 0x0600D5FE RID: 54782 RVA: 0x002D988C File Offset: 0x002D7A8C
		public override string ToOperatorString()
		{
			string text = (this.End ? "MatchEnd" : (this.Full ? "MatchFull" : "Match"));
			return string.Concat(new string[] { text, "(", base.ColumnName, ", ", this.Pattern, ")" });
		}
	}
}
