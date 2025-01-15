using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.ProgramSynthesis.Utils.Text
{
	// Token: 0x02000526 RID: 1318
	public class TextTableBuilder : ITextRowBuilder
	{
		// Token: 0x06001D64 RID: 7524 RVA: 0x00057658 File Offset: 0x00055858
		public TextTableBuilder(TextTableBorder border = TextTableBorder.None, int? defaultColumnPadding = null, int? defaultColumnMaxWidth = null)
		{
			this._outerBorders = border.HasFlag(TextTableBorder.Outer);
			this._columnBorders = border.HasFlag(TextTableBorder.Column);
			this._rowBorders = border.HasFlag(TextTableBorder.Row);
			this._defaultColumnPadding = defaultColumnPadding ?? 1;
			this._defaultColumnMaxWidth = defaultColumnMaxWidth ?? int.MaxValue;
		}

		// Token: 0x06001D65 RID: 7525 RVA: 0x00057701 File Offset: 0x00055901
		public static TextTableBuilder Create(TextTableBorder border = TextTableBorder.None, int? defaultColumnPadding = null, int? defaultColumnMaxWidth = null)
		{
			return new TextTableBuilder(border, defaultColumnPadding, defaultColumnMaxWidth);
		}

		// Token: 0x06001D66 RID: 7526 RVA: 0x0005770B File Offset: 0x0005590B
		public TextTableBuilder AddBorderColumn()
		{
			this._columns.Add(new SingleBorderTextColumn
			{
				External = true
			});
			return this;
		}

		// Token: 0x06001D67 RID: 7527 RVA: 0x00057728 File Offset: 0x00055928
		public TextTableBuilder AddColumn(string heading = "", int minWidth = 0, int? maxWidth = null, bool alignRight = false, string format = null, string nullProjection = null, int? leftPadding = null, int? rightPadding = null)
		{
			List<ITextColumn> columns = this._columns;
			TextColumn textColumn = new TextColumn();
			textColumn.Heading = heading;
			textColumn.MinimumWidth = minWidth;
			textColumn.MaximumWidth = maxWidth;
			int currentDataColumnIndex = this._currentDataColumnIndex;
			this._currentDataColumnIndex = currentDataColumnIndex + 1;
			textColumn.DataColumnIndex = currentDataColumnIndex;
			textColumn.Format = format;
			textColumn.AlignRight = alignRight;
			textColumn.NullProjection = nullProjection;
			textColumn.LeftPadding = leftPadding;
			textColumn.RightPadding = rightPadding;
			columns.Add(textColumn);
			return this;
		}

		// Token: 0x06001D68 RID: 7528 RVA: 0x0005779A File Offset: 0x0005599A
		public TextTableBuilder AddDoubleBorderColumn()
		{
			this._columns.Add(new DoubleBorderTextColumn
			{
				External = true
			});
			return this;
		}

		// Token: 0x06001D69 RID: 7529 RVA: 0x000577B4 File Offset: 0x000559B4
		public TextTableBuilder AddIdentityColumn(string heading = "", int? leftPadding = null, int? rightPadding = null)
		{
			this._columns.Add(new IdentityTextColumn
			{
				Heading = heading,
				LeftPadding = leftPadding,
				RightPadding = rightPadding,
				AlignRight = true
			});
			return this;
		}

		// Token: 0x06001D6A RID: 7530 RVA: 0x000577E4 File Offset: 0x000559E4
		public TextTableBuilder AddNumberColumn(string heading = "", string format = "N0", int minWidth = 0, string nullProjection = "--", int? leftPadding = null, int? rightPadding = null)
		{
			return this.AddColumn(heading, minWidth, null, true, format, nullProjection, leftPadding, rightPadding);
		}

		// Token: 0x06001D6B RID: 7531 RVA: 0x00057813 File Offset: 0x00055A13
		public TextTableBuilder AddStaticColumn(string value, bool showInHeading = false, bool showInRow = true, bool showInRowExtraLines = false)
		{
			this._columns.Add(new StaticTextColumn
			{
				Text = value,
				ShowInHeading = showInHeading,
				ShowInRow = showInRow,
				ShowInRowExtraLines = showInRowExtraLines
			});
			return this;
		}

		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x06001D6C RID: 7532 RVA: 0x00057843 File Offset: 0x00055A43
		public IReadOnlyList<ITextRow> Rows
		{
			get
			{
				return this._rows;
			}
		}

		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x06001D6D RID: 7533 RVA: 0x0005784C File Offset: 0x00055A4C
		private ITextRowBuilder RowBuilder
		{
			get
			{
				TextRowBuilder textRowBuilder;
				if ((textRowBuilder = this._rowBuilder) == null)
				{
					textRowBuilder = (this._rowBuilder = new TextRowBuilder(this, this._rows, this._columns));
				}
				return textRowBuilder;
			}
		}

		// Token: 0x06001D6E RID: 7534 RVA: 0x0005787E File Offset: 0x00055A7E
		public ITextRowBuilder AddBorderRow()
		{
			return this.RowBuilder.AddBorderRow();
		}

		// Token: 0x06001D6F RID: 7535 RVA: 0x0005788C File Offset: 0x00055A8C
		public ITextRowBuilder AddDataRow(params object[] cells)
		{
			return this.RowBuilder.AddDataRow(cells, null);
		}

		// Token: 0x06001D70 RID: 7536 RVA: 0x000578AE File Offset: 0x00055AAE
		public ITextRowBuilder AddDataRow(IReadOnlyList<object> row, int? rowId = null)
		{
			return this.RowBuilder.AddDataRow(row, rowId);
		}

		// Token: 0x06001D71 RID: 7537 RVA: 0x000578BD File Offset: 0x00055ABD
		public ITextRowBuilder AddDataRows(IReadOnlyList<IReadOnlyList<object>> rows, int? startRowId = null)
		{
			return this.RowBuilder.AddDataRows(rows, startRowId);
		}

		// Token: 0x06001D72 RID: 7538 RVA: 0x000578CC File Offset: 0x00055ACC
		public ITextRowBuilder AddDoubleBorderRow()
		{
			return this.RowBuilder.AddDoubleBorderRow();
		}

		// Token: 0x06001D73 RID: 7539 RVA: 0x000578D9 File Offset: 0x00055AD9
		public ITextRowBuilder AddEllipsisRow(string indicator = "...", int indicatorColumnIndex = 0)
		{
			return this.RowBuilder.AddEllipsisRow(indicator, indicatorColumnIndex);
		}

		// Token: 0x06001D74 RID: 7540 RVA: 0x000578E8 File Offset: 0x00055AE8
		public ITextRowBuilder AddHeadingRow()
		{
			return this.RowBuilder.AddHeadingRow();
		}

		// Token: 0x06001D75 RID: 7541 RVA: 0x000578F5 File Offset: 0x00055AF5
		public ITextRowBuilder ClearRows()
		{
			return this.RowBuilder.ClearRows();
		}

		// Token: 0x06001D76 RID: 7542 RVA: 0x00057902 File Offset: 0x00055B02
		public ITextRowBuilder InsertRow(int index, ITextRow row)
		{
			return this.RowBuilder.InsertRow(index, row);
		}

		// Token: 0x06001D77 RID: 7543 RVA: 0x00057911 File Offset: 0x00055B11
		public ITextRowBuilder RemoveRow(int index)
		{
			return this.RowBuilder.RemoveRow(index);
		}

		// Token: 0x06001D78 RID: 7544 RVA: 0x00057920 File Offset: 0x00055B20
		public string Render()
		{
			if (!this._columns.Any<ITextColumn>() || !this._rows.Any<ITextRow>())
			{
				return string.Empty;
			}
			this.ComputeLayout();
			StringBuilder stringBuilder = new StringBuilder();
			foreach (ITextRow textRow in this._rows)
			{
				textRow.Render(stringBuilder, this._columns);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001D79 RID: 7545 RVA: 0x000579AC File Offset: 0x00055BAC
		public void ResetLayout()
		{
			this._layoutBorderComputed = false;
			this._columns.RemoveAll(delegate(ITextColumn c)
			{
				if (!(c is PaddingTextColumn))
				{
					IBorderTextColumn borderTextColumn = c as IBorderTextColumn;
					if (borderTextColumn == null || borderTextColumn.External)
					{
						return false;
					}
				}
				return true;
			});
			this._rows.RemoveAll(delegate(ITextRow c)
			{
				IBorderTextRow borderTextRow = c as IBorderTextRow;
				return borderTextRow != null && !borderTextRow.External;
			});
		}

		// Token: 0x06001D7A RID: 7546 RVA: 0x00057A16 File Offset: 0x00055C16
		public override string ToString()
		{
			return this.Render();
		}

		// Token: 0x06001D7B RID: 7547 RVA: 0x00057A1E File Offset: 0x00055C1E
		public int Width()
		{
			this.ComputeLayout();
			return this._columns.Select((ITextColumn c) => c.Width).Sum();
		}

		// Token: 0x06001D7C RID: 7548 RVA: 0x00057A58 File Offset: 0x00055C58
		private void ComputeLayout()
		{
			if (!this._layoutBorderComputed && this._columnBorders)
			{
				this.ComputeMeta();
				foreach (int num in (from col in this._columns
					where col is IDynamicWidthTextColumn && !col.First && !(this._columns[col.Index - 1] is IBorderTextColumn)
					orderby col.Index descending
					select col.Index).ToList<int>())
				{
					this._columns.Insert(num, new SingleBorderTextColumn());
				}
			}
			if (!this._layoutBorderComputed && this._rowBorders)
			{
				this.ComputeMeta();
				foreach (int num2 in (from row in this._rows
					where row is DataTextRow && !row.First && !(this._rows[row.Index - 1] is IBorderTextRow)
					orderby row.Index descending
					select row.Index).ToList<int>())
				{
					this._rows.Insert(num2, new SingleBorderTextRow());
				}
			}
			this.ComputeMeta();
			if (!this._layoutBorderComputed)
			{
				IReadOnlyList<IDynamicWidthTextColumn> readOnlyList = (from col in this._columns
					where col is IDynamicWidthTextColumn
					let dynamicCol = (IDynamicWidthTextColumn)col
					orderby dynamicCol.Index descending
					select dynamicCol).ToList<IDynamicWidthTextColumn>();
				foreach (IDynamicWidthTextColumn dynamicWidthTextColumn in readOnlyList.Where((IDynamicWidthTextColumn col) => col.LeftPadding == null).ToList<IDynamicWidthTextColumn>())
				{
					dynamicWidthTextColumn.LeftPadding = new int?((dynamicWidthTextColumn.First && !this._outerBorders) ? 0 : this._defaultColumnPadding);
				}
				foreach (IDynamicWidthTextColumn dynamicWidthTextColumn2 in readOnlyList.Where((IDynamicWidthTextColumn col) => col.RightPadding == null).ToList<IDynamicWidthTextColumn>())
				{
					dynamicWidthTextColumn2.RightPadding = new int?((dynamicWidthTextColumn2.Last && !this._outerBorders) ? 0 : this._defaultColumnPadding);
				}
				foreach (IDynamicWidthTextColumn dynamicWidthTextColumn3 in readOnlyList)
				{
					int? num3 = dynamicWidthTextColumn3.RightPadding;
					if (num3 != null && num3.GetValueOrDefault() > 0)
					{
						this._columns.Insert(dynamicWidthTextColumn3.Index + 1, new PaddingTextColumn(dynamicWidthTextColumn3.RightPadding.Value));
					}
					num3 = dynamicWidthTextColumn3.LeftPadding;
					int num4 = 0;
					if ((num3.GetValueOrDefault() > num4) & (num3 != null))
					{
						this._columns.Insert(dynamicWidthTextColumn3.Index, new PaddingTextColumn(dynamicWidthTextColumn3.LeftPadding.Value));
					}
				}
			}
			this.ComputeMeta();
			List<DataTextRow> list = this._rows.OfType<DataTextRow>().ToList<DataTextRow>();
			using (List<ITextColumn>.Enumerator enumerator4 = this._columns.GetEnumerator())
			{
				while (enumerator4.MoveNext())
				{
					ITextColumn col = enumerator4.Current;
					IReadOnlyList<int> readOnlyList2 = (from row in list
						from partition in col.Lines(row)
						select partition.Length).ToList<int>();
					int num5 = (readOnlyList2.Any<int>() ? readOnlyList2.Max() : 0);
					bool flag = this.Rows.Any((ITextRow r) => r is HeadingTextRow);
					IDynamicWidthTextColumn dynamicWidthTextColumn4 = col as IDynamicWidthTextColumn;
					if (dynamicWidthTextColumn4 != null)
					{
						IDynamicWidthTextColumn dynamicWidthTextColumn5 = dynamicWidthTextColumn4;
						int? num3 = dynamicWidthTextColumn5.MaximumWidth;
						int num4 = num3.GetValueOrDefault();
						if (num3 == null)
						{
							num4 = this._defaultColumnMaxWidth;
							dynamicWidthTextColumn5.MaximumWidth = new int?(num4);
						}
						int num6;
						if (!flag)
						{
							num6 = 0;
						}
						else
						{
							string heading = dynamicWidthTextColumn4.Heading;
							num6 = ((heading != null) ? heading.Length : 0);
						}
						int num7 = num6;
						dynamicWidthTextColumn4.Width = Math.Max(dynamicWidthTextColumn4.MinimumWidth, Math.Max(num7, num5));
					}
					else
					{
						col.Width = num5;
					}
				}
			}
			this.ComputeMeta();
			if (!this._layoutBorderComputed && this._outerBorders)
			{
				if (!(this._columns[0] is IBorderTextColumn))
				{
					this._columns.Insert(0, new SingleBorderTextColumn());
				}
				if (!(this._columns[this._columns.Count - 1] is IBorderTextColumn))
				{
					this._columns.Add(new SingleBorderTextColumn());
				}
				if (!(this._rows[0] is IBorderTextRow))
				{
					this._rows.Insert(0, new SingleBorderTextRow());
				}
				if (!(this._rows[this._rows.Count - 1] is IBorderTextRow))
				{
					this._rows.Add(new SingleBorderTextRow());
				}
			}
			this.ComputeMeta();
			this._layoutBorderComputed = true;
		}

		// Token: 0x06001D7D RID: 7549 RVA: 0x000580FC File Offset: 0x000562FC
		private void ComputeMeta()
		{
			int num = 0;
			foreach (ITextColumn textColumn in this._columns)
			{
				textColumn.First = num == 0;
				textColumn.Last = num == this._columns.Count - 1;
				textColumn.Index = num++;
			}
			num = 0;
			foreach (ITextRow textRow in this._rows)
			{
				textRow.First = num == 0;
				textRow.Last = num == this._rows.Count - 1;
				textRow.Index = num++;
			}
		}

		// Token: 0x04000E3F RID: 3647
		private readonly bool _columnBorders;

		// Token: 0x04000E40 RID: 3648
		private readonly List<ITextColumn> _columns = new List<ITextColumn>();

		// Token: 0x04000E41 RID: 3649
		private int _currentDataColumnIndex;

		// Token: 0x04000E42 RID: 3650
		private readonly int _defaultColumnMaxWidth;

		// Token: 0x04000E43 RID: 3651
		private readonly int _defaultColumnPadding;

		// Token: 0x04000E44 RID: 3652
		private bool _layoutBorderComputed;

		// Token: 0x04000E45 RID: 3653
		private readonly bool _outerBorders;

		// Token: 0x04000E46 RID: 3654
		private readonly bool _rowBorders;

		// Token: 0x04000E47 RID: 3655
		private readonly List<ITextRow> _rows = new List<ITextRow>();

		// Token: 0x04000E48 RID: 3656
		private TextRowBuilder _rowBuilder;
	}
}
