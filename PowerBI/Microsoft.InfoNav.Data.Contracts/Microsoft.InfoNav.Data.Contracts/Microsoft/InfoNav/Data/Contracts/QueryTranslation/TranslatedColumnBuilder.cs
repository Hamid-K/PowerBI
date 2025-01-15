using System;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.Data.Contracts.QueryTranslation
{
	// Token: 0x020000B8 RID: 184
	public sealed class TranslatedColumnBuilder<TParent> : BaseBuilder<TranslatedColumn, TParent>
	{
		// Token: 0x060004D7 RID: 1239 RVA: 0x0000BA95 File Offset: 0x00009C95
		public TranslatedColumnBuilder(TranslatedColumn column, TParent parent)
			: base(parent)
		{
			this._column = column;
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x0000BAA5 File Offset: 0x00009CA5
		public TranslatedColumnBuilder<TParent> WithColumnName(string columnName)
		{
			this._column.ColumnName = columnName;
			return this;
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x0000BAB4 File Offset: 0x00009CB4
		public TranslatedColumnBuilder<TParent> WithSource(QueryExpressionContainer source)
		{
			this._column.Source = source;
			return this;
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x0000BAC3 File Offset: 0x00009CC3
		public override TranslatedColumn Build()
		{
			return this._column;
		}

		// Token: 0x04000212 RID: 530
		private readonly TranslatedColumn _column;
	}
}
