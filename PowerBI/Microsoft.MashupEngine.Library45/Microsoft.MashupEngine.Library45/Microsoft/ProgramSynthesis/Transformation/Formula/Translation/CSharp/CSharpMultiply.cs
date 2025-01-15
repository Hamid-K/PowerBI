using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.CSharp
{
	// Token: 0x02001953 RID: 6483
	internal class CSharpMultiply : FormulaMultiply
	{
		// Token: 0x0600D3BB RID: 54203 RVA: 0x002B4889 File Offset: 0x002B2A89
		public CSharpMultiply(FormulaExpression left, FormulaExpression right)
			: base(left, right, 6)
		{
		}

		// Token: 0x0600D3BC RID: 54204 RVA: 0x002D1DDE File Offset: 0x002CFFDE
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new CSharpMultiply(base.Left.Accept<FormulaExpression>(visitor), base.Right.Accept<FormulaExpression>(visitor));
		}
	}
}
