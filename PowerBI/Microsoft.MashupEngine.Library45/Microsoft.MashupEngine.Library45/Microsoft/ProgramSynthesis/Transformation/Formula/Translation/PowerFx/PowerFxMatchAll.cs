using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.PowerFx
{
	// Token: 0x020018F0 RID: 6384
	internal class PowerFxMatchAll : PowerFxFunc
	{
		// Token: 0x0600CFD1 RID: 53201 RVA: 0x002C46AC File Offset: 0x002C28AC
		public PowerFxMatchAll(FormulaExpression subject, FormulaExpression regex)
			: base("MatchAll", new FormulaExpression[] { subject, regex })
		{
			this.Subject = subject;
			this.Regex = regex;
		}

		// Token: 0x170022E0 RID: 8928
		// (get) Token: 0x0600CFD2 RID: 53202 RVA: 0x002C46D5 File Offset: 0x002C28D5
		public FormulaExpression Regex { get; }

		// Token: 0x170022E1 RID: 8929
		// (get) Token: 0x0600CFD3 RID: 53203 RVA: 0x002C46DD File Offset: 0x002C28DD
		public FormulaExpression Subject { get; }

		// Token: 0x0600CFD4 RID: 53204 RVA: 0x002C46E5 File Offset: 0x002C28E5
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new PowerFxMatchAll(this.Subject.Accept<FormulaExpression>(visitor), this.Regex.Accept<FormulaExpression>(visitor));
		}
	}
}
