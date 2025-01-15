using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.PowerQuery
{
	// Token: 0x020018A8 RID: 6312
	internal class PowerQueryConcat : FormulaConcat
	{
		// Token: 0x0600CE0A RID: 52746 RVA: 0x002BF951 File Offset: 0x002BDB51
		public PowerQueryConcat(FormulaExpression left, FormulaExpression right)
			: base(left, right, 16)
		{
		}

		// Token: 0x1700229F RID: 8863
		// (get) Token: 0x0600CE0B RID: 52747 RVA: 0x002BF95D File Offset: 0x002BDB5D
		public override string Symbol
		{
			get
			{
				return "&";
			}
		}

		// Token: 0x0600CE0C RID: 52748 RVA: 0x002BF964 File Offset: 0x002BDB64
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new PowerQueryConcat(base.Left.Accept<FormulaExpression>(visitor), base.Right.Accept<FormulaExpression>(visitor));
		}
	}
}
