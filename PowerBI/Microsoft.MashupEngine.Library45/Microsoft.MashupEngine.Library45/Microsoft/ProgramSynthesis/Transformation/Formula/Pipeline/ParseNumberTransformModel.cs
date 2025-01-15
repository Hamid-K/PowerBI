using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Pipeline
{
	// Token: 0x020019A7 RID: 6567
	public class ParseNumberTransformModel : PipelineModel
	{
		// Token: 0x170023BF RID: 9151
		// (get) Token: 0x0600D6AC RID: 54956 RVA: 0x002DA3B8 File Offset: 0x002D85B8
		// (set) Token: 0x0600D6AD RID: 54957 RVA: 0x002DA3C0 File Offset: 0x002D85C0
		public string Locale { get; set; }

		// Token: 0x0600D6AE RID: 54958 RVA: 0x002DA3CC File Offset: 0x002D85CC
		public override string ToOperatorString()
		{
			string text = ((this.Locale == "en-US") ? string.Empty : ("[" + this.Locale + "]"));
			return "ParseNumber(" + text + ")";
		}
	}
}
