using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.PowerQuery
{
	// Token: 0x020018BB RID: 6331
	internal class PowerQueryItemAccess : FormulaExpression
	{
		// Token: 0x0600CE54 RID: 52820 RVA: 0x002C0100 File Offset: 0x002BE300
		public PowerQueryItemAccess(FormulaExpression subject, FormulaExpression index)
		{
			this.Subject = ((subject is IFormulaOperator) ? new PowerQueryParenthesis(subject) : subject);
			this.Index = index;
			base.Children = new FormulaExpression[] { this.Subject, this.Index };
		}

		// Token: 0x170022B1 RID: 8881
		// (get) Token: 0x0600CE55 RID: 52821 RVA: 0x002C014F File Offset: 0x002BE34F
		public FormulaExpression Index { get; }

		// Token: 0x170022B2 RID: 8882
		// (get) Token: 0x0600CE56 RID: 52822 RVA: 0x002C0157 File Offset: 0x002BE357
		public FormulaExpression Subject { get; }

		// Token: 0x0600CE57 RID: 52823 RVA: 0x002C015F File Offset: 0x002BE35F
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new PowerQueryItemAccess(this.Subject.Accept<FormulaExpression>(visitor), this.Index.Accept<FormulaExpression>(visitor));
		}

		// Token: 0x0600CE58 RID: 52824 RVA: 0x002C017E File Offset: 0x002BE37E
		protected override string ToCodeString()
		{
			return string.Format("{0}{{{1}}}", this.Subject, this.Index);
		}
	}
}
