using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.CSharp
{
	// Token: 0x02001956 RID: 6486
	internal class CSharpExponent : FormulaFunc
	{
		// Token: 0x0600D3C2 RID: 54210 RVA: 0x002D1E3B File Offset: 0x002D003B
		public CSharpExponent(FormulaExpression left, FormulaExpression right)
			: base("Math.Pow", new FormulaExpression[] { left, right })
		{
			this.Left = left;
			this.Right = right;
		}

		// Token: 0x17002335 RID: 9013
		// (get) Token: 0x0600D3C3 RID: 54211 RVA: 0x002D1E64 File Offset: 0x002D0064
		public FormulaExpression Left { get; }

		// Token: 0x17002336 RID: 9014
		// (get) Token: 0x0600D3C4 RID: 54212 RVA: 0x002D1E6C File Offset: 0x002D006C
		public FormulaExpression Right { get; }

		// Token: 0x0600D3C5 RID: 54213 RVA: 0x002D1E74 File Offset: 0x002D0074
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new CSharpExponent(this.Left.Accept<FormulaExpression>(visitor), this.Right.Accept<FormulaExpression>(visitor));
		}
	}
}
