using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.CSharp
{
	// Token: 0x0200195B RID: 6491
	internal class CSharpNotEqual : FormulaBinaryOperator
	{
		// Token: 0x0600D3D6 RID: 54230 RVA: 0x002B8C78 File Offset: 0x002B6E78
		public CSharpNotEqual(FormulaExpression left, FormulaExpression right)
			: base(left, right, 0, false, false)
		{
		}

		// Token: 0x1700233E RID: 9022
		// (get) Token: 0x0600D3D7 RID: 54231 RVA: 0x002B8EA0 File Offset: 0x002B70A0
		public override string Symbol
		{
			get
			{
				return "!=";
			}
		}

		// Token: 0x0600D3D8 RID: 54232 RVA: 0x002D1FC1 File Offset: 0x002D01C1
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new CSharpNotEqual(base.Left.Accept<FormulaExpression>(visitor), base.Right.Accept<FormulaExpression>(visitor));
		}
	}
}
