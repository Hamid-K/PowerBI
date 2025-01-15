using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.PowerQuery
{
	// Token: 0x020018B7 RID: 6327
	internal class PowerQueryUnaryMinus : FormulaExpression, IFormulaUnaryOperator, IFormulaOperator
	{
		// Token: 0x0600CE3D RID: 52797 RVA: 0x002BFE69 File Offset: 0x002BE069
		public PowerQueryUnaryMinus(FormulaExpression subject)
		{
			this.Subject = subject;
			base.Children = new FormulaExpression[] { this.Subject };
		}

		// Token: 0x170022AB RID: 8875
		// (get) Token: 0x0600CE3E RID: 52798 RVA: 0x002BFE38 File Offset: 0x002BE038
		public int Precedence
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170022AC RID: 8876
		// (get) Token: 0x0600CE3F RID: 52799 RVA: 0x002BFE8D File Offset: 0x002BE08D
		public FormulaExpression Subject { get; }

		// Token: 0x170022AD RID: 8877
		// (get) Token: 0x0600CE40 RID: 52800 RVA: 0x002B312F File Offset: 0x002B132F
		public string Symbol
		{
			get
			{
				return "-";
			}
		}

		// Token: 0x0600CE41 RID: 52801 RVA: 0x002BFE95 File Offset: 0x002BE095
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new PowerQueryUnaryMinus(this.Subject.Accept<FormulaExpression>(visitor));
		}

		// Token: 0x0600CE42 RID: 52802 RVA: 0x002BFEA8 File Offset: 0x002BE0A8
		protected override string ToCodeString()
		{
			return string.Format("{0}{1}", this.Symbol, this.Subject);
		}
	}
}
