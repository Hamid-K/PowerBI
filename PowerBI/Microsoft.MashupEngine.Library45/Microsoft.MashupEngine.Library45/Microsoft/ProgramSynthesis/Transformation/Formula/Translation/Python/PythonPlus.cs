using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Python
{
	// Token: 0x02001861 RID: 6241
	internal class PythonPlus : FormulaPlus
	{
		// Token: 0x0600CBF0 RID: 52208 RVA: 0x002B485F File Offset: 0x002B2A5F
		public PythonPlus(FormulaExpression left, FormulaExpression right)
			: base(left, right, 4)
		{
		}

		// Token: 0x0600CBF1 RID: 52209 RVA: 0x002B8BF5 File Offset: 0x002B6DF5
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new PythonPlus(base.Left.Accept<FormulaExpression>(visitor), base.Right.Accept<FormulaExpression>(visitor));
		}
	}
}
