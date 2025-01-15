using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.CSharp
{
	// Token: 0x0200195A RID: 6490
	internal class CSharpEqual : FormulaBinaryOperator
	{
		// Token: 0x0600D3D3 RID: 54227 RVA: 0x002B8C78 File Offset: 0x002B6E78
		public CSharpEqual(FormulaExpression left, FormulaExpression right)
			: base(left, right, 0, false, false)
		{
		}

		// Token: 0x1700233D RID: 9021
		// (get) Token: 0x0600D3D4 RID: 54228 RVA: 0x002B8E99 File Offset: 0x002B7099
		public override string Symbol
		{
			get
			{
				return "==";
			}
		}

		// Token: 0x0600D3D5 RID: 54229 RVA: 0x002D1FA2 File Offset: 0x002D01A2
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new CSharpEqual(base.Left.Accept<FormulaExpression>(visitor), base.Right.Accept<FormulaExpression>(visitor));
		}
	}
}
