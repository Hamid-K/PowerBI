using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.PowerFx
{
	// Token: 0x020018EF RID: 6383
	internal class PowerFxMatch : PowerFxFunc
	{
		// Token: 0x0600CFCD RID: 53197 RVA: 0x002C4654 File Offset: 0x002C2854
		public PowerFxMatch(FormulaExpression subject, FormulaExpression regex)
			: base("Match", new FormulaExpression[] { subject, regex })
		{
			this.Subject = subject;
			this.Regex = regex;
		}

		// Token: 0x170022DE RID: 8926
		// (get) Token: 0x0600CFCE RID: 53198 RVA: 0x002C467D File Offset: 0x002C287D
		public FormulaExpression Regex { get; }

		// Token: 0x170022DF RID: 8927
		// (get) Token: 0x0600CFCF RID: 53199 RVA: 0x002C4685 File Offset: 0x002C2885
		public FormulaExpression Subject { get; }

		// Token: 0x0600CFD0 RID: 53200 RVA: 0x002C468D File Offset: 0x002C288D
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new PowerFxMatch(this.Subject.Accept<FormulaExpression>(visitor), this.Regex.Accept<FormulaExpression>(visitor));
		}
	}
}
