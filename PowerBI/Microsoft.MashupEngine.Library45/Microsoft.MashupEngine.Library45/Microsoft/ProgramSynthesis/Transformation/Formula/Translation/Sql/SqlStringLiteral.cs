using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Sql
{
	// Token: 0x02001817 RID: 6167
	internal class SqlStringLiteral : FormulaStringLiteral
	{
		// Token: 0x0600CA88 RID: 51848 RVA: 0x002B2B68 File Offset: 0x002B0D68
		public SqlStringLiteral(string value)
			: base(value)
		{
		}

		// Token: 0x0600CA89 RID: 51849 RVA: 0x00004FAE File Offset: 0x000031AE
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return this;
		}

		// Token: 0x0600CA8A RID: 51850 RVA: 0x002B477E File Offset: 0x002B297E
		protected override string ToCodeString()
		{
			if (base.Value != null)
			{
				return "'" + base.Value.Replace("'", "''").Replace("\n", "\\n") + "'";
			}
			return null;
		}
	}
}
