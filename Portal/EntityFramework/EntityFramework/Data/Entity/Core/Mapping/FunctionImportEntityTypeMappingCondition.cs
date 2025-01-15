using System;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x02000533 RID: 1331
	public abstract class FunctionImportEntityTypeMappingCondition : MappingItem
	{
		// Token: 0x06004196 RID: 16790 RVA: 0x000DD6DC File Offset: 0x000DB8DC
		internal FunctionImportEntityTypeMappingCondition(string columnName, LineInfo lineInfo)
		{
			this._columnName = columnName;
			this.LineInfo = lineInfo;
		}

		// Token: 0x17000CF9 RID: 3321
		// (get) Token: 0x06004197 RID: 16791 RVA: 0x000DD6F2 File Offset: 0x000DB8F2
		public string ColumnName
		{
			get
			{
				return this._columnName;
			}
		}

		// Token: 0x17000CFA RID: 3322
		// (get) Token: 0x06004198 RID: 16792
		internal abstract ValueCondition ConditionValue { get; }

		// Token: 0x06004199 RID: 16793
		internal abstract bool ColumnValueMatchesCondition(object columnValue);

		// Token: 0x0600419A RID: 16794 RVA: 0x000DD6FA File Offset: 0x000DB8FA
		public override string ToString()
		{
			return this.ConditionValue.ToString();
		}

		// Token: 0x040016BF RID: 5823
		private readonly string _columnName;

		// Token: 0x040016C0 RID: 5824
		internal readonly LineInfo LineInfo;
	}
}
