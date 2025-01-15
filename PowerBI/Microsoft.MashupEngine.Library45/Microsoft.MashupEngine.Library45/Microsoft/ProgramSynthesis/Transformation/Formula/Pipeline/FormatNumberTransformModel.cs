using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Pipeline
{
	// Token: 0x02001999 RID: 6553
	public class FormatNumberTransformModel : PipelineModel
	{
		// Token: 0x17002384 RID: 9092
		// (get) Token: 0x0600D62C RID: 54828 RVA: 0x002D9B3B File Offset: 0x002D7D3B
		// (set) Token: 0x0600D62D RID: 54829 RVA: 0x002D9B43 File Offset: 0x002D7D43
		public string FormatMask { get; set; }

		// Token: 0x17002385 RID: 9093
		// (get) Token: 0x0600D62E RID: 54830 RVA: 0x002D9B4C File Offset: 0x002D7D4C
		// (set) Token: 0x0600D62F RID: 54831 RVA: 0x002D9B54 File Offset: 0x002D7D54
		public string SimplifiedFormatMask { get; set; }

		// Token: 0x17002386 RID: 9094
		// (get) Token: 0x0600D630 RID: 54832 RVA: 0x002D9B5D File Offset: 0x002D7D5D
		// (set) Token: 0x0600D631 RID: 54833 RVA: 0x002D9B65 File Offset: 0x002D7D65
		public string Locale { get; set; }

		// Token: 0x0600D632 RID: 54834 RVA: 0x002D9B70 File Offset: 0x002D7D70
		public override string ToOperatorString()
		{
			string text = ((this.Locale == "en-US") ? string.Empty : ("[" + this.Locale + "]"));
			return "Format(" + text + this.SimplifiedFormatMask + ")";
		}
	}
}
