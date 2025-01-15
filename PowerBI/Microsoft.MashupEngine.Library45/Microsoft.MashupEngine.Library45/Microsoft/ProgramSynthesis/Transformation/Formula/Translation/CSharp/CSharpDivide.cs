using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.CSharp
{
	// Token: 0x02001954 RID: 6484
	internal class CSharpDivide : FormulaDivide
	{
		// Token: 0x0600D3BD RID: 54205 RVA: 0x002B48B3 File Offset: 0x002B2AB3
		public CSharpDivide(FormulaExpression left, FormulaExpression right)
			: base(left, right, 6)
		{
		}

		// Token: 0x0600D3BE RID: 54206 RVA: 0x002D1DFD File Offset: 0x002CFFFD
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new CSharpDivide(base.Left.Accept<FormulaExpression>(visitor), base.Right.Accept<FormulaExpression>(visitor));
		}
	}
}
