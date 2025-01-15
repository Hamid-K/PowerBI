using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Sql
{
	// Token: 0x02001821 RID: 6177
	internal class SqlStringType : SqlType
	{
		// Token: 0x0600CAA0 RID: 51872 RVA: 0x00004FAE File Offset: 0x000031AE
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return this;
		}

		// Token: 0x0600CAA1 RID: 51873 RVA: 0x002B4907 File Offset: 0x002B2B07
		protected override string ToCodeString()
		{
			return "VARCHAR(MAX)";
		}
	}
}
