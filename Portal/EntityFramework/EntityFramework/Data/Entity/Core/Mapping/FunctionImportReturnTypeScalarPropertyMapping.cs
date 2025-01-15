using System;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x0200053E RID: 1342
	public sealed class FunctionImportReturnTypeScalarPropertyMapping : FunctionImportReturnTypePropertyMapping
	{
		// Token: 0x060041DF RID: 16863 RVA: 0x000DF233 File Offset: 0x000DD433
		public FunctionImportReturnTypeScalarPropertyMapping(string propertyName, string columnName)
			: this(Check.NotNull<string>(propertyName, "propertyName"), Check.NotNull<string>(columnName, "columnName"), LineInfo.Empty)
		{
		}

		// Token: 0x060041E0 RID: 16864 RVA: 0x000DF256 File Offset: 0x000DD456
		internal FunctionImportReturnTypeScalarPropertyMapping(string propertyName, string columnName, LineInfo lineInfo)
			: base(lineInfo)
		{
			this._propertyName = propertyName;
			this._columnName = columnName;
		}

		// Token: 0x17000D0A RID: 3338
		// (get) Token: 0x060041E1 RID: 16865 RVA: 0x000DF26D File Offset: 0x000DD46D
		public string PropertyName
		{
			get
			{
				return this._propertyName;
			}
		}

		// Token: 0x17000D0B RID: 3339
		// (get) Token: 0x060041E2 RID: 16866 RVA: 0x000DF275 File Offset: 0x000DD475
		internal override string CMember
		{
			get
			{
				return this.PropertyName;
			}
		}

		// Token: 0x17000D0C RID: 3340
		// (get) Token: 0x060041E3 RID: 16867 RVA: 0x000DF27D File Offset: 0x000DD47D
		public string ColumnName
		{
			get
			{
				return this._columnName;
			}
		}

		// Token: 0x17000D0D RID: 3341
		// (get) Token: 0x060041E4 RID: 16868 RVA: 0x000DF285 File Offset: 0x000DD485
		internal override string SColumn
		{
			get
			{
				return this.ColumnName;
			}
		}

		// Token: 0x040016D9 RID: 5849
		private readonly string _propertyName;

		// Token: 0x040016DA RID: 5850
		private readonly string _columnName;
	}
}
