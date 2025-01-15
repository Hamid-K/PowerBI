using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.CSharp
{
	// Token: 0x02001971 RID: 6513
	internal class CSharpTernary : FormulaExpression
	{
		// Token: 0x0600D42C RID: 54316 RVA: 0x002D27E0 File Offset: 0x002D09E0
		public CSharpTernary(FormulaExpression condition, FormulaExpression trueBranch, FormulaExpression falseBranch)
		{
			if (trueBranch == null && falseBranch == null)
			{
				throw new Exception("Invalid CSharpTernary state, True and False branches are both null.");
			}
			this.Condition = condition;
			this.TrueBranch = trueBranch;
			this.FalseBranch = falseBranch;
			base.Children = new FormulaExpression[] { this.Condition, this.TrueBranch, this.FalseBranch };
		}

		// Token: 0x1700235C RID: 9052
		// (get) Token: 0x0600D42D RID: 54317 RVA: 0x002D284C File Offset: 0x002D0A4C
		public FormulaExpression Condition { get; }

		// Token: 0x1700235D RID: 9053
		// (get) Token: 0x0600D42E RID: 54318 RVA: 0x002D2854 File Offset: 0x002D0A54
		public FormulaExpression FalseBranch { get; }

		// Token: 0x1700235E RID: 9054
		// (get) Token: 0x0600D42F RID: 54319 RVA: 0x002D285C File Offset: 0x002D0A5C
		public FormulaExpression TrueBranch { get; }

		// Token: 0x0600D430 RID: 54320 RVA: 0x002D2864 File Offset: 0x002D0A64
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new CSharpTernary(this.Condition.Accept<FormulaExpression>(visitor), this.TrueBranch.Accept<FormulaExpression>(visitor), this.FalseBranch.Accept<FormulaExpression>(visitor));
		}

		// Token: 0x0600D431 RID: 54321 RVA: 0x002D288F File Offset: 0x002D0A8F
		protected override string ToCodeString()
		{
			return string.Format("{0} ? {1} : {2}", this.Condition, this.TrueBranch, this.FalseBranch);
		}
	}
}
