using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Sql
{
	// Token: 0x02001823 RID: 6179
	internal class SqlFloatType : SqlType
	{
		// Token: 0x0600CAA6 RID: 51878 RVA: 0x00004FAE File Offset: 0x000031AE
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return this;
		}

		// Token: 0x0600CAA7 RID: 51879 RVA: 0x002B491D File Offset: 0x002B2B1D
		protected override string ToCodeString()
		{
			return "FLOAT";
		}
	}
}
