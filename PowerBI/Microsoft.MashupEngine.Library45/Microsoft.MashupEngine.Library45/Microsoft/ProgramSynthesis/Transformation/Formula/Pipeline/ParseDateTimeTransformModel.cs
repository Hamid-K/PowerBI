using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Pipeline
{
	// Token: 0x020019A8 RID: 6568
	public class ParseDateTimeTransformModel : PipelineModel
	{
		// Token: 0x170023C0 RID: 9152
		// (get) Token: 0x0600D6B0 RID: 54960 RVA: 0x002DA418 File Offset: 0x002D8618
		// (set) Token: 0x0600D6B1 RID: 54961 RVA: 0x002DA420 File Offset: 0x002D8620
		public string FormatMask { get; set; }

		// Token: 0x170023C1 RID: 9153
		// (get) Token: 0x0600D6B2 RID: 54962 RVA: 0x002DA429 File Offset: 0x002D8629
		// (set) Token: 0x0600D6B3 RID: 54963 RVA: 0x002DA431 File Offset: 0x002D8631
		public string Locale { get; set; }

		// Token: 0x0600D6B4 RID: 54964 RVA: 0x002DA43C File Offset: 0x002D863C
		public override string ToOperatorString()
		{
			string text = ((this.Locale == "en-US") ? string.Empty : ("[" + this.Locale + "]"));
			return "ParseDateTime(" + text + this.FormatMask + ")";
		}
	}
}
