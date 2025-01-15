using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Sql
{
	// Token: 0x02001822 RID: 6178
	internal class SqlIntType : SqlType
	{
		// Token: 0x0600CAA3 RID: 51875 RVA: 0x00004FAE File Offset: 0x000031AE
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return this;
		}

		// Token: 0x0600CAA4 RID: 51876 RVA: 0x002B4916 File Offset: 0x002B2B16
		protected override string ToCodeString()
		{
			return "INT";
		}
	}
}
