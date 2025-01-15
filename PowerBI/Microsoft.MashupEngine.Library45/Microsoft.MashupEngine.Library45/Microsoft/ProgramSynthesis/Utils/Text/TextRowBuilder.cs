using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Utils.Interactive;

namespace Microsoft.ProgramSynthesis.Utils.Text
{
	// Token: 0x02000529 RID: 1321
	public class TextRowBuilder : ITextRowBuilder
	{
		// Token: 0x06001D93 RID: 7571 RVA: 0x00058344 File Offset: 0x00056544
		public TextRowBuilder(TextTableBuilder table, List<ITextRow> rows, IReadOnlyList<ITextColumn> columns)
		{
			this._table = table;
			this._rows = rows;
			List<TextColumn> list = columns.OfType<TextColumn>().ToList<TextColumn>();
			int num;
			if (!list.Any<TextColumn>())
			{
				num = 0;
			}
			else
			{
				num = list.Max((TextColumn i) => i.DataColumnIndex) + 1;
			}
			this._dataColumnCount = num;
		}

		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x06001D94 RID: 7572 RVA: 0x000583B0 File Offset: 0x000565B0
		public IReadOnlyList<ITextRow> Rows
		{
			get
			{
				return this._rows;
			}
		}

		// Token: 0x06001D95 RID: 7573 RVA: 0x000583B8 File Offset: 0x000565B8
		public ITextRowBuilder AddBorderRow()
		{
			this._rows.Add(new SingleBorderTextRow
			{
				External = true
			});
			return this;
		}

		// Token: 0x06001D96 RID: 7574 RVA: 0x000583D4 File Offset: 0x000565D4
		public ITextRowBuilder AddDataRow(params object[] cells)
		{
			return this.AddDataRow(cells, null);
		}

		// Token: 0x06001D97 RID: 7575 RVA: 0x000583F4 File Offset: 0x000565F4
		public ITextRowBuilder AddDataRow(IReadOnlyList<object> row, int? rowId = null)
		{
			if (row.Count != this._dataColumnCount)
			{
				throw new Exception(string.Format("row columns ({0}) does not match scheme columns ({1})", row.Count, this._dataColumnCount));
			}
			List<ITextRow> rows = this._rows;
			int? num = rowId;
			int num2;
			if (num == null)
			{
				int rowId2 = this._rowId;
				this._rowId = rowId2 + 1;
				num2 = rowId2;
			}
			else
			{
				num2 = num.GetValueOrDefault();
			}
			rows.Add(new DataTextRow(num2, row));
			return this;
		}

		// Token: 0x06001D98 RID: 7576 RVA: 0x0005846C File Offset: 0x0005666C
		public ITextRowBuilder AddDataRows(IReadOnlyList<IReadOnlyList<object>> rows, int? startRowId = null)
		{
			foreach (IReadOnlyList<object> readOnlyList in rows)
			{
				IReadOnlyList<object> readOnlyList2 = readOnlyList;
				int? num;
				if (startRowId != null)
				{
					int? num2;
					num = (num2 = startRowId);
					startRowId = num2 + 1;
				}
				else
				{
					num = null;
				}
				this.AddDataRow(readOnlyList2, num);
			}
			return this;
		}

		// Token: 0x06001D99 RID: 7577 RVA: 0x000584F4 File Offset: 0x000566F4
		public ITextRowBuilder AddDoubleBorderRow()
		{
			this._rows.Add(new DoubleBorderTextRow
			{
				External = true
			});
			return this;
		}

		// Token: 0x06001D9A RID: 7578 RVA: 0x0005850E File Offset: 0x0005670E
		public ITextRowBuilder AddEllipsisRow(string indicator = "...", int indicatorColumnIndex = 0)
		{
			this._rows.Add(new EllipsisTextRow
			{
				Indicator = indicator,
				ColumnIndex = indicatorColumnIndex
			});
			return this;
		}

		// Token: 0x06001D9B RID: 7579 RVA: 0x0005852F File Offset: 0x0005672F
		public ITextRowBuilder AddHeadingRow()
		{
			this._rows.Add(new HeadingTextRow());
			return this;
		}

		// Token: 0x06001D9C RID: 7580 RVA: 0x00058542 File Offset: 0x00056742
		public ITextRowBuilder ClearRows()
		{
			this._rows.RemoveAll((ITextRow r) => r is DataTextRow);
			this.ResetLayout();
			return this;
		}

		// Token: 0x06001D9D RID: 7581 RVA: 0x00058576 File Offset: 0x00056776
		public ITextRowBuilder InsertRow(int index, ITextRow row)
		{
			this._rows.Insert(index, row);
			this.ResetLayout();
			return this;
		}

		// Token: 0x06001D9E RID: 7582 RVA: 0x0005858C File Offset: 0x0005678C
		public ITextRowBuilder RemoveRow(int index)
		{
			this._rows.RemoveAt(index);
			this.ResetLayout();
			return this;
		}

		// Token: 0x06001D9F RID: 7583 RVA: 0x000585A1 File Offset: 0x000567A1
		public string Render()
		{
			return this._table.Render();
		}

		// Token: 0x06001DA0 RID: 7584 RVA: 0x000585AE File Offset: 0x000567AE
		public override string ToString()
		{
			return this._table.ToString();
		}

		// Token: 0x06001DA1 RID: 7585 RVA: 0x000585BC File Offset: 0x000567BC
		private void ResetLayout()
		{
			this._table.ResetLayout();
			this._rowId = 1;
			this._rows.Where((ITextRow r) => r is DataTextRow).ForEach(delegate(ITextRow r)
			{
				int rowId = this._rowId;
				this._rowId = rowId + 1;
				r.Id = new int?(rowId);
			});
		}

		// Token: 0x04000E5A RID: 3674
		private readonly int _dataColumnCount;

		// Token: 0x04000E5B RID: 3675
		private int _rowId = 1;

		// Token: 0x04000E5C RID: 3676
		private readonly List<ITextRow> _rows;

		// Token: 0x04000E5D RID: 3677
		private readonly TextTableBuilder _table;
	}
}
