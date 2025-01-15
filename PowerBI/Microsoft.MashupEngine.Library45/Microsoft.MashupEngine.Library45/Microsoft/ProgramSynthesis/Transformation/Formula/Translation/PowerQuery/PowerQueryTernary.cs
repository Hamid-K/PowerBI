using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.PowerQuery
{
	// Token: 0x020018BC RID: 6332
	internal class PowerQueryTernary : FormulaExpression, IFormulaTyped
	{
		// Token: 0x0600CE59 RID: 52825 RVA: 0x002C0198 File Offset: 0x002BE398
		public PowerQueryTernary(FormulaExpression condition, FormulaExpression trueExpression, FormulaExpression falseExpression)
		{
			this.Condition = condition;
			this.TrueExpression = trueExpression;
			this.FalseExpression = falseExpression;
			base.Children = new FormulaExpression[] { this.Condition, this.TrueExpression, this.FalseExpression };
		}

		// Token: 0x170022B3 RID: 8883
		// (get) Token: 0x0600CE5A RID: 52826 RVA: 0x002C01E7 File Offset: 0x002BE3E7
		public FormulaExpression Condition { get; }

		// Token: 0x170022B4 RID: 8884
		// (get) Token: 0x0600CE5B RID: 52827 RVA: 0x002C01EF File Offset: 0x002BE3EF
		public FormulaExpression FalseExpression { get; }

		// Token: 0x170022B5 RID: 8885
		// (get) Token: 0x0600CE5C RID: 52828 RVA: 0x002C01F7 File Offset: 0x002BE3F7
		public FormulaExpression TrueExpression { get; }

		// Token: 0x170022B6 RID: 8886
		// (get) Token: 0x0600CE5D RID: 52829 RVA: 0x002C01FF File Offset: 0x002BE3FF
		public Type Type
		{
			get
			{
				return typeof(object);
			}
		}

		// Token: 0x0600CE5E RID: 52830 RVA: 0x002C020B File Offset: 0x002BE40B
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new PowerQueryTernary(this.Condition.Accept<FormulaExpression>(visitor), this.TrueExpression.Accept<FormulaExpression>(visitor), this.FalseExpression.Accept<FormulaExpression>(visitor));
		}

		// Token: 0x0600CE5F RID: 52831 RVA: 0x002C0236 File Offset: 0x002BE436
		protected override string ToCodeString()
		{
			return string.Format("if {0} then {1} else {2}", this.Condition, this.TrueExpression, this.FalseExpression);
		}
	}
}
