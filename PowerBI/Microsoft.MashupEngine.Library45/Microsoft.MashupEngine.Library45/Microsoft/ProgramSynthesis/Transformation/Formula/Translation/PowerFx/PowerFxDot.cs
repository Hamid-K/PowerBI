using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.PowerFx
{
	// Token: 0x020018DF RID: 6367
	internal class PowerFxDot : FormulaExpression
	{
		// Token: 0x0600CF92 RID: 53138 RVA: 0x002C4312 File Offset: 0x002C2512
		public PowerFxDot(FormulaExpression subject, FormulaExpression accessor)
		{
			this.Subject = ((subject is IFormulaBinaryOperator) ? new FormulaParenthesis(subject) : subject);
			this.Accessor = accessor;
			base.Children = new FormulaExpression[] { subject };
		}

		// Token: 0x170022CC RID: 8908
		// (get) Token: 0x0600CF93 RID: 53139 RVA: 0x002C4348 File Offset: 0x002C2548
		public FormulaExpression Accessor { get; }

		// Token: 0x170022CD RID: 8909
		// (get) Token: 0x0600CF94 RID: 53140 RVA: 0x002C4350 File Offset: 0x002C2550
		public FormulaExpression Subject { get; }

		// Token: 0x0600CF95 RID: 53141 RVA: 0x002C4358 File Offset: 0x002C2558
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new PowerFxDot(this.Subject.Accept<FormulaExpression>(visitor), this.Accessor.Accept<FormulaExpression>(visitor));
		}

		// Token: 0x0600CF96 RID: 53142 RVA: 0x002C4377 File Offset: 0x002C2577
		protected override string ToCodeString()
		{
			return string.Format("{0}.{1}", this.Subject, this.Accessor);
		}
	}
}
