using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.CSharp
{
	// Token: 0x0200195F RID: 6495
	internal class CSharpIs : FormulaBinaryOperator
	{
		// Token: 0x0600D3E2 RID: 54242 RVA: 0x002D203D File Offset: 0x002D023D
		public CSharpIs(FormulaExpression left, FormulaExpression right, FormulaExpression variable)
			: base(left, right, 0, false, false)
		{
			this.Variable = variable;
		}

		// Token: 0x17002342 RID: 9026
		// (get) Token: 0x0600D3E3 RID: 54243 RVA: 0x002D2051 File Offset: 0x002D0251
		public FormulaExpression Variable { get; }

		// Token: 0x17002343 RID: 9027
		// (get) Token: 0x0600D3E4 RID: 54244 RVA: 0x002B8E73 File Offset: 0x002B7073
		public override string Symbol
		{
			get
			{
				return "is";
			}
		}

		// Token: 0x0600D3E5 RID: 54245 RVA: 0x002D2059 File Offset: 0x002D0259
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			FormulaExpression formulaExpression = base.Left.Accept<FormulaExpression>(visitor);
			FormulaExpression formulaExpression2 = base.Right.Accept<FormulaExpression>(visitor);
			FormulaExpression variable = this.Variable;
			return new CSharpIs(formulaExpression, formulaExpression2, (variable != null) ? variable.Accept<FormulaExpression>(visitor) : null);
		}

		// Token: 0x0600D3E6 RID: 54246 RVA: 0x002D208C File Offset: 0x002D028C
		protected override string ToCodeString()
		{
			string text = string.Format("{0} {1} {2}", base.Left, this.Symbol, base.Right);
			if (!(this.Variable == null))
			{
				return string.Format("{0} {1}", text, this.Variable);
			}
			return text;
		}
	}
}
