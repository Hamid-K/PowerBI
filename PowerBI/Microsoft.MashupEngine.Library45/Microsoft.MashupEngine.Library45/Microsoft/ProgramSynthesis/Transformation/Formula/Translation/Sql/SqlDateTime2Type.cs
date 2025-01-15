using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Sql
{
	// Token: 0x02001825 RID: 6181
	internal class SqlDateTime2Type : SqlType
	{
		// Token: 0x0600CAAC RID: 51884 RVA: 0x00004FAE File Offset: 0x000031AE
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return this;
		}

		// Token: 0x0600CAAD RID: 51885 RVA: 0x002B492B File Offset: 0x002B2B2B
		protected override string ToCodeString()
		{
			return "DATETIME2";
		}
	}
}
