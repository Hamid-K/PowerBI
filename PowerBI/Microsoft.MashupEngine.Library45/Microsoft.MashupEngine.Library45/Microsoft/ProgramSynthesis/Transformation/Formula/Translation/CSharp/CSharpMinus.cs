using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.CSharp
{
	// Token: 0x02001951 RID: 6481
	internal class CSharpMinus : FormulaMinus
	{
		// Token: 0x0600D3B7 RID: 54199 RVA: 0x002B4835 File Offset: 0x002B2A35
		public CSharpMinus(FormulaExpression left, FormulaExpression right)
			: base(left, right, 4)
		{
		}

		// Token: 0x0600D3B8 RID: 54200 RVA: 0x002D1DA0 File Offset: 0x002CFFA0
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new CSharpMinus(base.Left.Accept<FormulaExpression>(visitor), base.Right.Accept<FormulaExpression>(visitor));
		}
	}
}
