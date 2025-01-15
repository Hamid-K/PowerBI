using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.CSharp
{
	// Token: 0x0200195C RID: 6492
	internal class CSharpAnd : FormulaBinaryOperator
	{
		// Token: 0x0600D3D9 RID: 54233 RVA: 0x002B8C78 File Offset: 0x002B6E78
		public CSharpAnd(FormulaExpression left, FormulaExpression right)
			: base(left, right, 0, false, false)
		{
		}

		// Token: 0x1700233F RID: 9023
		// (get) Token: 0x0600D3DA RID: 54234 RVA: 0x002C43AE File Offset: 0x002C25AE
		public override string Symbol
		{
			get
			{
				return "&&";
			}
		}

		// Token: 0x0600D3DB RID: 54235 RVA: 0x002D1FE0 File Offset: 0x002D01E0
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new CSharpAnd(base.Left.Accept<FormulaExpression>(visitor), base.Right.Accept<FormulaExpression>(visitor));
		}
	}
}
