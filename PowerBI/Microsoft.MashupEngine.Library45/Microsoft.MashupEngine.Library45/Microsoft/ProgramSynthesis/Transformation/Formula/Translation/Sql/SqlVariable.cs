using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Sql
{
	// Token: 0x02001827 RID: 6183
	internal class SqlVariable : FormulaVariable
	{
		// Token: 0x0600CAB2 RID: 51890 RVA: 0x002B4932 File Offset: 0x002B2B32
		public SqlVariable(string name)
			: base(name)
		{
			this.DataType = new SqlUnresolvedType();
		}

		// Token: 0x0600CAB3 RID: 51891 RVA: 0x002B4946 File Offset: 0x002B2B46
		public SqlVariable(string name, SqlType type)
			: base(name)
		{
			this.DataType = type;
		}

		// Token: 0x17002230 RID: 8752
		// (get) Token: 0x0600CAB4 RID: 51892 RVA: 0x002B4956 File Offset: 0x002B2B56
		public SqlType DataType { get; }

		// Token: 0x0600CAB5 RID: 51893 RVA: 0x00004FAE File Offset: 0x000031AE
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return this;
		}

		// Token: 0x0600CAB6 RID: 51894 RVA: 0x002B495E File Offset: 0x002B2B5E
		public override FormulaExpression CloneWith(string name)
		{
			return new SqlVariable(name);
		}
	}
}
