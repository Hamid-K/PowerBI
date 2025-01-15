using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.PowerFx
{
	// Token: 0x020018E7 RID: 6375
	internal class PowerFxNot : FormulaExpression, IFormulaUnaryOperator, IFormulaOperator
	{
		// Token: 0x0600CFAC RID: 53164 RVA: 0x002C4457 File Offset: 0x002C2657
		public PowerFxNot(FormulaExpression subject)
		{
			this.Subject = subject;
			base.Children = new FormulaExpression[] { this.Subject };
		}

		// Token: 0x170022D5 RID: 8917
		// (get) Token: 0x0600CFAD RID: 53165 RVA: 0x0000FA11 File Offset: 0x0000DC11
		public int Precedence
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170022D6 RID: 8918
		// (get) Token: 0x0600CFAE RID: 53166 RVA: 0x002C447B File Offset: 0x002C267B
		public FormulaExpression Subject { get; }

		// Token: 0x0600CFAF RID: 53167 RVA: 0x002C4483 File Offset: 0x002C2683
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new PowerFxNot(this.Subject.Accept<FormulaExpression>(visitor));
		}

		// Token: 0x0600CFB0 RID: 53168 RVA: 0x002C4496 File Offset: 0x002C2696
		protected override string ToCodeString()
		{
			return string.Format("!{0}", this.Subject);
		}
	}
}
