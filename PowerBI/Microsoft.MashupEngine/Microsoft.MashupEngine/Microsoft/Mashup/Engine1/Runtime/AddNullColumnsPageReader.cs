using System;
using System.Collections.Generic;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Common;
using Microsoft.OleDb;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x0200125E RID: 4702
	internal sealed class AddNullColumnsPageReader : DelegatingPageReader
	{
		// Token: 0x06007BE7 RID: 31719 RVA: 0x001AAC5F File Offset: 0x001A8E5F
		public AddNullColumnsPageReader(IPageReader reader, Keys newColumns)
			: base(reader)
		{
			this.newColumns = newColumns;
		}

		// Token: 0x170021CF RID: 8655
		// (get) Token: 0x06007BE8 RID: 31720 RVA: 0x001AAC70 File Offset: 0x001A8E70
		public override TableSchema Schema
		{
			get
			{
				if (this.schema == null)
				{
					TableSchema tableSchema = base.Schema.Copy();
					for (int i = 0; i < this.newColumns.Length; i++)
					{
						tableSchema.AddColumn(this.newColumns[i], typeof(object), true).Ordinal = new int?(tableSchema.ColumnCount);
					}
					this.schema = tableSchema;
				}
				return this.schema;
			}
		}

		// Token: 0x06007BE9 RID: 31721 RVA: 0x001AACE1 File Offset: 0x001A8EE1
		public override IPage CreatePage()
		{
			return new AddNullColumnsPageReader.Page(this.newColumns, base.CreatePage());
		}

		// Token: 0x06007BEA RID: 31722 RVA: 0x001AACF4 File Offset: 0x001A8EF4
		public override void Read(IPage page)
		{
			((AddNullColumnsPageReader.Page)page).ReadPage(base.PageReader);
		}

		// Token: 0x04004499 RID: 17561
		private readonly Keys newColumns;

		// Token: 0x0400449A RID: 17562
		private TableSchema schema;

		// Token: 0x0200125F RID: 4703
		private sealed class Page : IPage, IDisposable
		{
			// Token: 0x06007BEB RID: 31723 RVA: 0x001AAD07 File Offset: 0x001A8F07
			public Page(Keys newColumns, IPage page)
			{
				this.newColumns = newColumns;
				this.page = page;
			}

			// Token: 0x170021D0 RID: 8656
			// (get) Token: 0x06007BEC RID: 31724 RVA: 0x001AAD1D File Offset: 0x001A8F1D
			public int ColumnCount
			{
				get
				{
					return this.page.ColumnCount + this.newColumns.Length;
				}
			}

			// Token: 0x170021D1 RID: 8657
			// (get) Token: 0x06007BED RID: 31725 RVA: 0x001AAD36 File Offset: 0x001A8F36
			public int RowCount
			{
				get
				{
					return this.page.RowCount;
				}
			}

			// Token: 0x170021D2 RID: 8658
			// (get) Token: 0x06007BEE RID: 31726 RVA: 0x001AAD43 File Offset: 0x001A8F43
			public IDictionary<int, IExceptionRow> ExceptionRows
			{
				get
				{
					return this.page.ExceptionRows;
				}
			}

			// Token: 0x170021D3 RID: 8659
			// (get) Token: 0x06007BEF RID: 31727 RVA: 0x001AAD50 File Offset: 0x001A8F50
			public ISerializedException PageException
			{
				get
				{
					return this.page.PageException;
				}
			}

			// Token: 0x06007BF0 RID: 31728 RVA: 0x001AAD5D File Offset: 0x001A8F5D
			public IColumn GetColumn(int ordinal)
			{
				if (ordinal < this.page.ColumnCount)
				{
					return this.page.GetColumn(ordinal);
				}
				return this.nullColumn;
			}

			// Token: 0x06007BF1 RID: 31729 RVA: 0x001AAD80 File Offset: 0x001A8F80
			public void ReadPage(IPageReader reader)
			{
				reader.Read(this.page);
				if (this.nullColumn == null || this.nullColumn.RowCount != this.page.RowCount)
				{
					this.nullColumn = Column.Create(typeof(object), true, this.page.RowCount);
					for (int i = this.page.RowCount - 1; i >= 0; i--)
					{
						this.nullColumn.AddNull();
					}
				}
			}

			// Token: 0x06007BF2 RID: 31730 RVA: 0x001AADFD File Offset: 0x001A8FFD
			public void Dispose()
			{
				this.page.Dispose();
			}

			// Token: 0x0400449B RID: 17563
			private readonly Keys newColumns;

			// Token: 0x0400449C RID: 17564
			private readonly IPage page;

			// Token: 0x0400449D RID: 17565
			private Column nullColumn;
		}
	}
}
