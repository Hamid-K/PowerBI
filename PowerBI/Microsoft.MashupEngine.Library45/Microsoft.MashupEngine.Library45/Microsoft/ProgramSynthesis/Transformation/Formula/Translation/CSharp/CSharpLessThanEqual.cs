using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.CSharp
{
	// Token: 0x02001963 RID: 6499
	internal class CSharpLessThanEqual : FormulaBinaryOperator
	{
		// Token: 0x0600D3F0 RID: 54256 RVA: 0x002B8C78 File Offset: 0x002B6E78
		public CSharpLessThanEqual(FormulaExpression left, FormulaExpression right)
			: base(left, right, 0, false, false)
		{
		}

		// Token: 0x17002347 RID: 9031
		// (get) Token: 0x0600D3F1 RID: 54257 RVA: 0x002B8F84 File Offset: 0x002B7184
		public override string Symbol
		{
			get
			{
				return "<=";
			}
		}

		// Token: 0x0600D3F2 RID: 54258 RVA: 0x002D2134 File Offset: 0x002D0334
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new CSharpLessThanEqual(base.Left.Accept<FormulaExpression>(visitor), base.Right.Accept<FormulaExpression>(visitor));
		}
	}
}
