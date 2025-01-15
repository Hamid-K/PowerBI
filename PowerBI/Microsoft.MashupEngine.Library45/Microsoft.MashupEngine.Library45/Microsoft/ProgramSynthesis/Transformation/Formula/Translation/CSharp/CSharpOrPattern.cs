using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.CSharp
{
	// Token: 0x0200195E RID: 6494
	internal class CSharpOrPattern : FormulaBinaryOperator
	{
		// Token: 0x0600D3DF RID: 54239 RVA: 0x002B8C78 File Offset: 0x002B6E78
		public CSharpOrPattern(FormulaExpression left, FormulaExpression right)
			: base(left, right, 0, false, false)
		{
		}

		// Token: 0x17002341 RID: 9025
		// (get) Token: 0x0600D3E0 RID: 54240 RVA: 0x002B8EEC File Offset: 0x002B70EC
		public override string Symbol
		{
			get
			{
				return "or";
			}
		}

		// Token: 0x0600D3E1 RID: 54241 RVA: 0x002D201E File Offset: 0x002D021E
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new CSharpOrPattern(base.Left.Accept<FormulaExpression>(visitor), base.Right.Accept<FormulaExpression>(visitor));
		}
	}
}
