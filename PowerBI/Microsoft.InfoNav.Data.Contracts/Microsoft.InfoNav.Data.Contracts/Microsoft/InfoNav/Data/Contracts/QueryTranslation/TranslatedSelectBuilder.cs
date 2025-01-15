using System;
using System.Collections.Generic;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.Data.Contracts.QueryTranslation
{
	// Token: 0x020000B7 RID: 183
	public sealed class TranslatedSelectBuilder<TParent> : BaseBuilder<TranslatedSelect, TParent>
	{
		// Token: 0x060004D0 RID: 1232 RVA: 0x0000B9AC File Offset: 0x00009BAC
		public TranslatedSelectBuilder(TranslatedSelect select, TParent parent)
			: base(parent)
		{
			this._select = select;
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x0000B9BC File Offset: 0x00009BBC
		public TranslatedSelectBuilder<TParent> WithName(string name)
		{
			this._select.Name = name;
			return this;
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x0000B9CB File Offset: 0x00009BCB
		public TranslatedSelectBuilder<TParent> WithColumnName(string columnName)
		{
			this._select.ColumnName = columnName;
			return this;
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x0000B9DC File Offset: 0x00009BDC
		public TranslatedColumnBuilder<TranslatedSelectBuilder<TParent>> WithGroupColumn()
		{
			if (this._select.GroupColumns == null)
			{
				this._select.GroupColumns = new List<TranslatedColumn>();
			}
			TranslatedColumn translatedColumn = new TranslatedColumn();
			this._select.GroupColumns.Add(translatedColumn);
			return new TranslatedColumnBuilder<TranslatedSelectBuilder<TParent>>(translatedColumn, this);
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x0000BA24 File Offset: 0x00009C24
		public TranslatedColumnBuilder<TranslatedSelectBuilder<TParent>> WithSortColumn()
		{
			if (this._select.SortColumns == null)
			{
				this._select.SortColumns = new List<TranslatedColumn>();
			}
			TranslatedColumn translatedColumn = new TranslatedColumn();
			this._select.SortColumns.Add(translatedColumn);
			return new TranslatedColumnBuilder<TranslatedSelectBuilder<TParent>>(translatedColumn, this);
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x0000BA6C File Offset: 0x00009C6C
		public TranslatedSelectBuilder<TParent> WithDynamicFormat(string format, string culture)
		{
			this._select.DynamicFormat = new TranslatedDynamicFormat
			{
				Format = format,
				Culture = culture
			};
			return this;
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x0000BA8D File Offset: 0x00009C8D
		public override TranslatedSelect Build()
		{
			return this._select;
		}

		// Token: 0x04000211 RID: 529
		private readonly TranslatedSelect _select;
	}
}
