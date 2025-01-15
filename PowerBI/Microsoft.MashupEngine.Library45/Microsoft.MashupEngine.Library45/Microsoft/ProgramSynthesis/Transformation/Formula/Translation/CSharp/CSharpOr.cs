using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.CSharp
{
	// Token: 0x0200195D RID: 6493
	internal class CSharpOr : FormulaBinaryOperator
	{
		// Token: 0x0600D3DC RID: 54236 RVA: 0x002B8C78 File Offset: 0x002B6E78
		public CSharpOr(FormulaExpression left, FormulaExpression right)
			: base(left, right, 0, false, false)
		{
		}

		// Token: 0x17002340 RID: 9024
		// (get) Token: 0x0600D3DD RID: 54237 RVA: 0x002C43D4 File Offset: 0x002C25D4
		public override string Symbol
		{
			get
			{
				return "||";
			}
		}

		// Token: 0x0600D3DE RID: 54238 RVA: 0x002D1FFF File Offset: 0x002D01FF
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new CSharpOr(base.Left.Accept<FormulaExpression>(visitor), base.Right.Accept<FormulaExpression>(visitor));
		}
	}
}
