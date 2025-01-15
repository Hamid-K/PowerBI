using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.CSharp
{
	// Token: 0x02001959 RID: 6489
	internal class CSharpAssign : FormulaBinaryOperator
	{
		// Token: 0x0600D3D0 RID: 54224 RVA: 0x002B8E3A File Offset: 0x002B703A
		public CSharpAssign(FormulaExpression left, FormulaExpression right, bool compact = false)
			: base(left, right, 0, compact, false)
		{
		}

		// Token: 0x1700233C RID: 9020
		// (get) Token: 0x0600D3D1 RID: 54225 RVA: 0x002B8E47 File Offset: 0x002B7047
		public override string Symbol
		{
			get
			{
				return "=";
			}
		}

		// Token: 0x0600D3D2 RID: 54226 RVA: 0x002D1F82 File Offset: 0x002D0182
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new CSharpAssign(base.Left.Accept<FormulaExpression>(visitor), base.Right.Accept<FormulaExpression>(visitor), false);
		}
	}
}
