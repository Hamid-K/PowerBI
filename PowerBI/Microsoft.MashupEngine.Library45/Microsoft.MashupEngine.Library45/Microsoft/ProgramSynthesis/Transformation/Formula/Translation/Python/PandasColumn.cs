using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Python
{
	// Token: 0x02001837 RID: 6199
	internal class PandasColumn : FormulaExpression
	{
		// Token: 0x0600CB31 RID: 52017 RVA: 0x002B62EA File Offset: 0x002B44EA
		public PandasColumn(FormulaExpression dataFrameName, FormulaExpression columnName)
		{
			this.DataFrameName = dataFrameName;
			this.ColumnName = columnName;
		}

		// Token: 0x17002247 RID: 8775
		// (get) Token: 0x0600CB32 RID: 52018 RVA: 0x002B6300 File Offset: 0x002B4500
		public FormulaExpression ColumnName { get; }

		// Token: 0x17002248 RID: 8776
		// (get) Token: 0x0600CB33 RID: 52019 RVA: 0x002B6308 File Offset: 0x002B4508
		public FormulaExpression DataFrameName { get; }

		// Token: 0x0600CB34 RID: 52020 RVA: 0x002B6310 File Offset: 0x002B4510
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new PandasColumn(this.DataFrameName.Accept<FormulaExpression>(visitor), this.ColumnName.Accept<FormulaExpression>(visitor));
		}

		// Token: 0x0600CB35 RID: 52021 RVA: 0x002B6330 File Offset: 0x002B4530
		protected override string ToCodeString()
		{
			FormulaVariable formulaVariable = this.ColumnName as FormulaVariable;
			if (formulaVariable == null)
			{
				return string.Format("{0}[{1}]", this.DataFrameName, this.ColumnName);
			}
			return string.Format("{0}.{1}", this.DataFrameName, formulaVariable);
		}
	}
}
