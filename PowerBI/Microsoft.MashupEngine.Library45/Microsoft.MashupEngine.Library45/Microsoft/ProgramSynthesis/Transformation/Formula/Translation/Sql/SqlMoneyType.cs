using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Sql
{
	// Token: 0x02001824 RID: 6180
	internal class SqlMoneyType : SqlType
	{
		// Token: 0x0600CAA9 RID: 51881 RVA: 0x00004FAE File Offset: 0x000031AE
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return this;
		}

		// Token: 0x0600CAAA RID: 51882 RVA: 0x002B4924 File Offset: 0x002B2B24
		protected override string ToCodeString()
		{
			return "MONEY";
		}
	}
}
