using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.PowerFx
{
	// Token: 0x020018EE RID: 6382
	internal class PowerFxFind : PowerFxFunc
	{
		// Token: 0x0600CFC7 RID: 53191 RVA: 0x002C45AD File Offset: 0x002C27AD
		public PowerFxFind(FormulaExpression delimiter, FormulaExpression subject)
			: base("Find", new FormulaExpression[] { delimiter, subject })
		{
			this.Delimiter = delimiter;
			this.Subject = subject;
		}

		// Token: 0x0600CFC8 RID: 53192 RVA: 0x002C45D6 File Offset: 0x002C27D6
		public PowerFxFind(FormulaExpression delimiter, FormulaExpression subject, FormulaExpression startAt)
			: base("Find", new FormulaExpression[] { delimiter, subject, startAt })
		{
			this.Delimiter = delimiter;
			this.Subject = subject;
			this.StartAt = startAt;
		}

		// Token: 0x170022DB RID: 8923
		// (get) Token: 0x0600CFC9 RID: 53193 RVA: 0x002C460A File Offset: 0x002C280A
		public FormulaExpression Delimiter { get; }

		// Token: 0x170022DC RID: 8924
		// (get) Token: 0x0600CFCA RID: 53194 RVA: 0x002C4612 File Offset: 0x002C2812
		public FormulaExpression StartAt { get; }

		// Token: 0x170022DD RID: 8925
		// (get) Token: 0x0600CFCB RID: 53195 RVA: 0x002C461A File Offset: 0x002C281A
		public FormulaExpression Subject { get; }

		// Token: 0x0600CFCC RID: 53196 RVA: 0x002C4622 File Offset: 0x002C2822
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			FormulaExpression formulaExpression = this.Delimiter.Accept<FormulaExpression>(visitor);
			FormulaExpression formulaExpression2 = this.Subject.Accept<FormulaExpression>(visitor);
			FormulaExpression startAt = this.StartAt;
			return new PowerFxFind(formulaExpression, formulaExpression2, (startAt != null) ? startAt.Accept<FormulaExpression>(visitor) : null);
		}
	}
}
