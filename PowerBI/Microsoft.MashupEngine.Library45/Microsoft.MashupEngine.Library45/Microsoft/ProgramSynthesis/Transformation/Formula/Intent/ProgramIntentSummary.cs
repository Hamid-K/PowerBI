using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Intent
{
	// Token: 0x020019B4 RID: 6580
	public class ProgramIntentSummary
	{
		// Token: 0x170023C3 RID: 9155
		// (get) Token: 0x0600D6DB RID: 55003 RVA: 0x002DACAB File Offset: 0x002D8EAB
		// (set) Token: 0x0600D6DC RID: 55004 RVA: 0x002DACB3 File Offset: 0x002D8EB3
		public ProgramIntent Intent { get; set; }

		// Token: 0x170023C4 RID: 9156
		// (get) Token: 0x0600D6DD RID: 55005 RVA: 0x002DACBC File Offset: 0x002D8EBC
		// (set) Token: 0x0600D6DE RID: 55006 RVA: 0x002DACC4 File Offset: 0x002D8EC4
		public ProgramIntent IntentFlags { get; set; }

		// Token: 0x0600D6DF RID: 55007 RVA: 0x002DACCD File Offset: 0x002D8ECD
		public override string ToString()
		{
			return string.Format("{0}{1}{2}", this.Intent, Environment.NewLine, this.IntentFlags);
		}
	}
}
