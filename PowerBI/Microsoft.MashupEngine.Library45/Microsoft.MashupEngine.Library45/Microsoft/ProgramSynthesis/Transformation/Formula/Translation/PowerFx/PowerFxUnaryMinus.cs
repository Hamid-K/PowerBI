using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.PowerFx
{
	// Token: 0x020018E8 RID: 6376
	internal class PowerFxUnaryMinus : FormulaExpression, IFormulaUnaryOperator, IFormulaOperator
	{
		// Token: 0x0600CFB1 RID: 53169 RVA: 0x002C44A8 File Offset: 0x002C26A8
		public PowerFxUnaryMinus(FormulaExpression subject)
		{
			this.Subject = subject;
			base.Children = new FormulaExpression[] { this.Subject };
		}

		// Token: 0x170022D7 RID: 8919
		// (get) Token: 0x0600CFB2 RID: 53170 RVA: 0x0000FA11 File Offset: 0x0000DC11
		public int Precedence
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170022D8 RID: 8920
		// (get) Token: 0x0600CFB3 RID: 53171 RVA: 0x002C44CC File Offset: 0x002C26CC
		public FormulaExpression Subject { get; }

		// Token: 0x170022D9 RID: 8921
		// (get) Token: 0x0600CFB4 RID: 53172 RVA: 0x002B312F File Offset: 0x002B132F
		public string Symbol
		{
			get
			{
				return "-";
			}
		}

		// Token: 0x0600CFB5 RID: 53173 RVA: 0x002C44D4 File Offset: 0x002C26D4
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new PowerFxUnaryMinus(this.Subject.Accept<FormulaExpression>(visitor));
		}

		// Token: 0x0600CFB6 RID: 53174 RVA: 0x002C44E7 File Offset: 0x002C26E7
		protected override string ToCodeString()
		{
			return string.Format("{0}{1}", this.Symbol, this.Subject);
		}
	}
}
