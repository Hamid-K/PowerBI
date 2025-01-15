using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Sql
{
	// Token: 0x02001826 RID: 6182
	internal class SqlUnresolvedType : SqlType
	{
		// Token: 0x0600CAAF RID: 51887 RVA: 0x00004FAE File Offset: 0x000031AE
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return this;
		}

		// Token: 0x0600CAB0 RID: 51888 RVA: 0x000170F6 File Offset: 0x000152F6
		protected override string ToCodeString()
		{
			throw new NotImplementedException();
		}
	}
}
