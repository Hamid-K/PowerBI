using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.CSharp
{
	// Token: 0x02001961 RID: 6497
	internal class CSharpGreaterThanEqual : FormulaBinaryOperator
	{
		// Token: 0x0600D3EA RID: 54250 RVA: 0x002B8C78 File Offset: 0x002B6E78
		public CSharpGreaterThanEqual(FormulaExpression left, FormulaExpression right)
			: base(left, right, 0, false, false)
		{
		}

		// Token: 0x17002345 RID: 9029
		// (get) Token: 0x0600D3EB RID: 54251 RVA: 0x002B8F38 File Offset: 0x002B7138
		public override string Symbol
		{
			get
			{
				return ">=";
			}
		}

		// Token: 0x0600D3EC RID: 54252 RVA: 0x002D20F6 File Offset: 0x002D02F6
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new CSharpGreaterThanEqual(base.Left.Accept<FormulaExpression>(visitor), base.Right.Accept<FormulaExpression>(visitor));
		}
	}
}
