using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Pipeline
{
	// Token: 0x0200199D RID: 6557
	public class FormatDateTimeTransformModel : PipelineModel
	{
		// Token: 0x17002391 RID: 9105
		// (get) Token: 0x0600D64B RID: 54859 RVA: 0x002D9D17 File Offset: 0x002D7F17
		// (set) Token: 0x0600D64C RID: 54860 RVA: 0x002D9D1F File Offset: 0x002D7F1F
		public string FormatMask { get; set; }

		// Token: 0x17002392 RID: 9106
		// (get) Token: 0x0600D64D RID: 54861 RVA: 0x002D9D28 File Offset: 0x002D7F28
		// (set) Token: 0x0600D64E RID: 54862 RVA: 0x002D9D30 File Offset: 0x002D7F30
		public string Locale { get; set; }

		// Token: 0x0600D64F RID: 54863 RVA: 0x002D9D3C File Offset: 0x002D7F3C
		public override string ToOperatorString()
		{
			string text = ((this.Locale == "en-US") ? string.Empty : ("[" + this.Locale + "]"));
			return "Format(" + text + this.FormatMask + ")";
		}
	}
}
