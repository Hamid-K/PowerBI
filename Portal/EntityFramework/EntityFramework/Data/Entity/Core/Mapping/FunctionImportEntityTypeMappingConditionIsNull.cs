using System;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x02000534 RID: 1332
	public sealed class FunctionImportEntityTypeMappingConditionIsNull : FunctionImportEntityTypeMappingCondition
	{
		// Token: 0x0600419B RID: 16795 RVA: 0x000DD707 File Offset: 0x000DB907
		public FunctionImportEntityTypeMappingConditionIsNull(string columnName, bool isNull)
			: this(Check.NotNull<string>(columnName, "columnName"), isNull, LineInfo.Empty)
		{
		}

		// Token: 0x0600419C RID: 16796 RVA: 0x000DD720 File Offset: 0x000DB920
		internal FunctionImportEntityTypeMappingConditionIsNull(string columnName, bool isNull, LineInfo lineInfo)
			: base(columnName, lineInfo)
		{
			this._isNull = isNull;
		}

		// Token: 0x17000CFB RID: 3323
		// (get) Token: 0x0600419D RID: 16797 RVA: 0x000DD731 File Offset: 0x000DB931
		public bool IsNull
		{
			get
			{
				return this._isNull;
			}
		}

		// Token: 0x17000CFC RID: 3324
		// (get) Token: 0x0600419E RID: 16798 RVA: 0x000DD739 File Offset: 0x000DB939
		internal override ValueCondition ConditionValue
		{
			get
			{
				if (!this.IsNull)
				{
					return ValueCondition.IsNotNull;
				}
				return ValueCondition.IsNull;
			}
		}

		// Token: 0x0600419F RID: 16799 RVA: 0x000DD74E File Offset: 0x000DB94E
		internal override bool ColumnValueMatchesCondition(object columnValue)
		{
			return (columnValue == null || Convert.IsDBNull(columnValue)) == this.IsNull;
		}

		// Token: 0x040016C1 RID: 5825
		private readonly bool _isNull;
	}
}
