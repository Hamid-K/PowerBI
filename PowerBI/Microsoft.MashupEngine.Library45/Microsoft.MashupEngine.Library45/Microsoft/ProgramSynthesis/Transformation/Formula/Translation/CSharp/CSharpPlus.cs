using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.CSharp
{
	// Token: 0x02001952 RID: 6482
	internal class CSharpPlus : FormulaPlus
	{
		// Token: 0x0600D3B9 RID: 54201 RVA: 0x002B485F File Offset: 0x002B2A5F
		public CSharpPlus(FormulaExpression left, FormulaExpression right)
			: base(left, right, 4)
		{
		}

		// Token: 0x0600D3BA RID: 54202 RVA: 0x002D1DBF File Offset: 0x002CFFBF
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new CSharpPlus(base.Left.Accept<FormulaExpression>(visitor), base.Right.Accept<FormulaExpression>(visitor));
		}
	}
}
