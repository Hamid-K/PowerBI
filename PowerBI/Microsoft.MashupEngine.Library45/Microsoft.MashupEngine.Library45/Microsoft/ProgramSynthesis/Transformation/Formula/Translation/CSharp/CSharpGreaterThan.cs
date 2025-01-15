using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.CSharp
{
	// Token: 0x02001960 RID: 6496
	internal class CSharpGreaterThan : FormulaBinaryOperator
	{
		// Token: 0x0600D3E7 RID: 54247 RVA: 0x002B8C78 File Offset: 0x002B6E78
		public CSharpGreaterThan(FormulaExpression left, FormulaExpression right)
			: base(left, right, 0, false, false)
		{
		}

		// Token: 0x17002344 RID: 9028
		// (get) Token: 0x0600D3E8 RID: 54248 RVA: 0x002B8F12 File Offset: 0x002B7112
		public override string Symbol
		{
			get
			{
				return ">";
			}
		}

		// Token: 0x0600D3E9 RID: 54249 RVA: 0x002D20D7 File Offset: 0x002D02D7
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new CSharpGreaterThan(base.Left.Accept<FormulaExpression>(visitor), base.Right.Accept<FormulaExpression>(visitor));
		}
	}
}
