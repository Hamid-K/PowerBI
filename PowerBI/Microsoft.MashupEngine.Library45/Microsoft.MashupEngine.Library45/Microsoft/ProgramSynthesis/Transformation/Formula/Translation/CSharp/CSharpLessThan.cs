using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.CSharp
{
	// Token: 0x02001962 RID: 6498
	internal class CSharpLessThan : FormulaBinaryOperator
	{
		// Token: 0x0600D3ED RID: 54253 RVA: 0x002B8C78 File Offset: 0x002B6E78
		public CSharpLessThan(FormulaExpression left, FormulaExpression right)
			: base(left, right, 0, false, false)
		{
		}

		// Token: 0x17002346 RID: 9030
		// (get) Token: 0x0600D3EE RID: 54254 RVA: 0x002B8F5E File Offset: 0x002B715E
		public override string Symbol
		{
			get
			{
				return "<";
			}
		}

		// Token: 0x0600D3EF RID: 54255 RVA: 0x002D2115 File Offset: 0x002D0315
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new CSharpLessThan(base.Left.Accept<FormulaExpression>(visitor), base.Right.Accept<FormulaExpression>(visitor));
		}
	}
}
