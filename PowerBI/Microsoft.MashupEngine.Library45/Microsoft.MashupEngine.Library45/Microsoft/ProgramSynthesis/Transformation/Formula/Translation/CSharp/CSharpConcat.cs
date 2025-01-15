using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.CSharp
{
	// Token: 0x02001957 RID: 6487
	internal class CSharpConcat : FormulaConcat
	{
		// Token: 0x0600D3C6 RID: 54214 RVA: 0x002B8CDC File Offset: 0x002B6EDC
		public CSharpConcat(FormulaExpression left, FormulaExpression right)
			: base(left, right, 4)
		{
		}

		// Token: 0x17002337 RID: 9015
		// (get) Token: 0x0600D3C7 RID: 54215 RVA: 0x002B3143 File Offset: 0x002B1343
		public override string Symbol
		{
			get
			{
				return "+";
			}
		}

		// Token: 0x0600D3C8 RID: 54216 RVA: 0x002D1E93 File Offset: 0x002D0093
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new CSharpConcat(base.Left.Accept<FormulaExpression>(visitor), base.Right.Accept<FormulaExpression>(visitor));
		}
	}
}
