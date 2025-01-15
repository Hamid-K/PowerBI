using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.CSharp
{
	// Token: 0x02001955 RID: 6485
	internal class CSharpModulo : FormulaBinaryOperator
	{
		// Token: 0x0600D3BF RID: 54207 RVA: 0x002B8C78 File Offset: 0x002B6E78
		public CSharpModulo(FormulaExpression left, FormulaExpression right)
			: base(left, right, 0, false, false)
		{
		}

		// Token: 0x17002334 RID: 9012
		// (get) Token: 0x0600D3C0 RID: 54208 RVA: 0x002B8C85 File Offset: 0x002B6E85
		public override string Symbol
		{
			get
			{
				return "%";
			}
		}

		// Token: 0x0600D3C1 RID: 54209 RVA: 0x002D1E1C File Offset: 0x002D001C
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new CSharpModulo(base.Left.Accept<FormulaExpression>(visitor), base.Right.Accept<FormulaExpression>(visitor));
		}
	}
}
