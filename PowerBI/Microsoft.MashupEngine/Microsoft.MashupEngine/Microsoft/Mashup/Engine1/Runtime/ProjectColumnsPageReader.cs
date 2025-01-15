using System;
using System.Collections.Generic;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.OleDb.Serialization;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x020015DA RID: 5594
	internal sealed class ProjectColumnsPageReader : DelegatingPageReader
	{
		// Token: 0x06008CB5 RID: 36021 RVA: 0x001D7F52 File Offset: 0x001D6152
		public ProjectColumnsPageReader(IPageReader reader, ColumnSelection columnSelection)
			: base(reader)
		{
			this.columnSelection = columnSelection;
		}

		// Token: 0x170024E2 RID: 9442
		// (get) Token: 0x06008CB6 RID: 36022 RVA: 0x001D7F64 File Offset: 0x001D6164
		public override TableSchema Schema
		{
			get
			{
				if (this.schema == null)
				{
					TableSchema tableSchema = base.Schema;
					TableSchema tableSchema2 = new TableSchema(this.columnSelection.Keys.Length);
					for (int i = 0; i < this.columnSelection.Keys.Length; i++)
					{
						tableSchema2.AddColumn(tableSchema.GetColumn(this.columnSelection.GetColumn(i)).Clone(this.columnSelection.Keys[i]));
					}
					this.schema = tableSchema2;
				}
				return this.schema;
			}
		}

		// Token: 0x06008CB7 RID: 36023 RVA: 0x001D7FEC File Offset: 0x001D61EC
		public override IPage CreatePage()
		{
			return new ProjectColumnsPageReader.Page(this.columnSelection, base.CreatePage());
		}

		// Token: 0x06008CB8 RID: 36024 RVA: 0x001D7FFF File Offset: 0x001D61FF
		public override void Read(IPage page)
		{
			((ProjectColumnsPageReader.Page)page).ReadPage(base.PageReader);
		}

		// Token: 0x04004CC0 RID: 19648
		private readonly ColumnSelection columnSelection;

		// Token: 0x04004CC1 RID: 19649
		private TableSchema schema;

		// Token: 0x020015DB RID: 5595
		private sealed class Page : IPage, IDisposable
		{
			// Token: 0x06008CB9 RID: 36025 RVA: 0x001D8012 File Offset: 0x001D6212
			public Page(ColumnSelection columnSelection, IPage page)
			{
				this.columnSelection = columnSelection;
				this.page = page;
			}

			// Token: 0x170024E3 RID: 9443
			// (get) Token: 0x06008CBA RID: 36026 RVA: 0x001D8028 File Offset: 0x001D6228
			public int ColumnCount
			{
				get
				{
					return this.columnSelection.Keys.Length;
				}
			}

			// Token: 0x170024E4 RID: 9444
			// (get) Token: 0x06008CBB RID: 36027 RVA: 0x001D803A File Offset: 0x001D623A
			public int RowCount
			{
				get
				{
					return this.page.RowCount;
				}
			}

			// Token: 0x170024E5 RID: 9445
			// (get) Token: 0x06008CBC RID: 36028 RVA: 0x001D8047 File Offset: 0x001D6247
			public IDictionary<int, IExceptionRow> ExceptionRows
			{
				get
				{
					return this.exceptionRows;
				}
			}

			// Token: 0x170024E6 RID: 9446
			// (get) Token: 0x06008CBD RID: 36029 RVA: 0x001D804F File Offset: 0x001D624F
			public ISerializedException PageException
			{
				get
				{
					return this.page.PageException;
				}
			}

			// Token: 0x06008CBE RID: 36030 RVA: 0x001D805C File Offset: 0x001D625C
			public IColumn GetColumn(int ordinal)
			{
				return this.page.GetColumn(this.columnSelection.GetColumn(ordinal));
			}

			// Token: 0x06008CBF RID: 36031 RVA: 0x001D8078 File Offset: 0x001D6278
			public void ReadPage(IPageReader reader)
			{
				reader.Read(this.page);
				if (this.page.ExceptionRows != null && this.page.ExceptionRows.Count > 0)
				{
					ColumnSelection.SelectMap selectMap = this.columnSelection.CreateSelectMap(this.page.ColumnCount);
					this.exceptionRows = new Dictionary<int, IExceptionRow>(this.page.ExceptionRows.Count);
					using (IEnumerator<KeyValuePair<int, IExceptionRow>> enumerator = this.page.ExceptionRows.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							KeyValuePair<int, IExceptionRow> keyValuePair = enumerator.Current;
							Dictionary<int, ISerializedException> dictionary = new Dictionary<int, ISerializedException>(keyValuePair.Value.Exceptions.Count);
							foreach (KeyValuePair<int, ISerializedException> keyValuePair2 in keyValuePair.Value.Exceptions)
							{
								int num = selectMap.MapColumn(keyValuePair2.Key);
								if (num != -1)
								{
									dictionary.Add(num, keyValuePair2.Value);
								}
							}
							this.exceptionRows.Add(keyValuePair.Key, new ExceptionRow(dictionary));
						}
						return;
					}
				}
				this.exceptionRows = this.page.ExceptionRows;
			}

			// Token: 0x06008CC0 RID: 36032 RVA: 0x001D81D4 File Offset: 0x001D63D4
			public void Dispose()
			{
				this.page.Dispose();
			}

			// Token: 0x04004CC2 RID: 19650
			private readonly ColumnSelection columnSelection;

			// Token: 0x04004CC3 RID: 19651
			private readonly IPage page;

			// Token: 0x04004CC4 RID: 19652
			private IDictionary<int, IExceptionRow> exceptionRows;
		}
	}
}
