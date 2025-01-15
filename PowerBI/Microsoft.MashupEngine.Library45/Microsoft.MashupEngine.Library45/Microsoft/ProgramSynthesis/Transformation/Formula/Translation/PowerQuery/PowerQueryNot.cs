using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.PowerQuery
{
	// Token: 0x020018B6 RID: 6326
	internal class PowerQueryNot : FormulaExpression, IFormulaUnaryOperator, IFormulaOperator
	{
		// Token: 0x0600CE38 RID: 52792 RVA: 0x002BFE14 File Offset: 0x002BE014
		public PowerQueryNot(FormulaExpression subject)
		{
			this.Subject = subject;
			base.Children = new FormulaExpression[] { this.Subject };
		}

		// Token: 0x170022A9 RID: 8873
		// (get) Token: 0x0600CE39 RID: 52793 RVA: 0x002BFE38 File Offset: 0x002BE038
		public int Precedence
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170022AA RID: 8874
		// (get) Token: 0x0600CE3A RID: 52794 RVA: 0x002BFE3C File Offset: 0x002BE03C
		public FormulaExpression Subject { get; }

		// Token: 0x0600CE3B RID: 52795 RVA: 0x002BFE44 File Offset: 0x002BE044
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new PowerQueryNot(this.Subject.Accept<FormulaExpression>(visitor));
		}

		// Token: 0x0600CE3C RID: 52796 RVA: 0x002BFE57 File Offset: 0x002BE057
		protected override string ToCodeString()
		{
			return string.Format("not {0}", this.Subject);
		}
	}
}
