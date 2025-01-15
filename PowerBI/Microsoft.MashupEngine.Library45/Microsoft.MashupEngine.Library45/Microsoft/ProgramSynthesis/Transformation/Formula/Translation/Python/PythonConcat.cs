using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Python
{
	// Token: 0x02001867 RID: 6247
	internal class PythonConcat : FormulaConcat
	{
		// Token: 0x0600CBFF RID: 52223 RVA: 0x002B8CDC File Offset: 0x002B6EDC
		public PythonConcat(FormulaExpression left, FormulaExpression right)
			: base(left, right, 4)
		{
		}

		// Token: 0x1700226A RID: 8810
		// (get) Token: 0x0600CC00 RID: 52224 RVA: 0x002B3143 File Offset: 0x002B1343
		public override string Symbol
		{
			get
			{
				return "+";
			}
		}

		// Token: 0x0600CC01 RID: 52225 RVA: 0x002B8CE7 File Offset: 0x002B6EE7
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new PythonConcat(base.Left.Accept<FormulaExpression>(visitor), base.Right.Accept<FormulaExpression>(visitor));
		}
	}
}
