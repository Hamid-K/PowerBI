using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Language.Query
{
	// Token: 0x02001865 RID: 6245
	public class ColumnSelection
	{
		// Token: 0x06009E69 RID: 40553 RVA: 0x0020C045 File Offset: 0x0020A245
		public ColumnSelection(Keys keys)
			: this(keys, null)
		{
		}

		// Token: 0x06009E6A RID: 40554 RVA: 0x0020C04F File Offset: 0x0020A24F
		public ColumnSelection(Keys keys, int[] columns)
		{
			this.keys = keys;
			this.columns = columns;
		}

		// Token: 0x170028E2 RID: 10466
		// (get) Token: 0x06009E6B RID: 40555 RVA: 0x0020C065 File Offset: 0x0020A265
		public Keys Keys
		{
			get
			{
				return this.keys;
			}
		}

		// Token: 0x06009E6C RID: 40556 RVA: 0x0020C06D File Offset: 0x0020A26D
		public int GetColumn(int index)
		{
			if (this.columns == null)
			{
				return index;
			}
			return this.columns[index];
		}

		// Token: 0x170028E3 RID: 10467
		// (get) Token: 0x06009E6D RID: 40557 RVA: 0x0020C081 File Offset: 0x0020A281
		public bool Ordered
		{
			get
			{
				return this.columns == null;
			}
		}

		// Token: 0x06009E6E RID: 40558 RVA: 0x0020C08C File Offset: 0x0020A28C
		public int[] GetColumns(int[] indices)
		{
			if (this.columns == null)
			{
				return indices;
			}
			int[] array = new int[indices.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = this.columns[indices[i]];
			}
			return array;
		}

		// Token: 0x06009E6F RID: 40559 RVA: 0x0020C0C8 File Offset: 0x0020A2C8
		public ColumnSelection SelectColumns(ColumnSelection columnSelection)
		{
			ColumnSelectionBuilder columnSelectionBuilder = default(ColumnSelectionBuilder);
			for (int i = 0; i < columnSelection.Keys.Length; i++)
			{
				columnSelectionBuilder.Add(columnSelection.Keys[i], this.GetColumn(columnSelection.GetColumn(i)));
			}
			return columnSelectionBuilder.ToColumnSelection();
		}

		// Token: 0x06009E70 RID: 40560 RVA: 0x0020C11C File Offset: 0x0020A31C
		public void Split(Keys columns, out ColumnSelection select, out ColumnSelection rename)
		{
			bool[] array = new bool[columns.Length];
			for (int i = 0; i < this.Keys.Length; i++)
			{
				array[this.GetColumn(i)] = true;
			}
			ColumnSelectionBuilder columnSelectionBuilder = default(ColumnSelectionBuilder);
			for (int j = 0; j < columns.Length; j++)
			{
				if (array[j])
				{
					columnSelectionBuilder.Add(columns[j], j);
				}
			}
			select = columnSelectionBuilder.ToColumnSelection();
			ColumnSelectionBuilder columnSelectionBuilder2 = default(ColumnSelectionBuilder);
			for (int k = 0; k < this.Keys.Length; k++)
			{
				string text = this.Keys[k];
				int num;
				if (!select.Keys.TryGetKeyIndex(columns[this.GetColumn(k)], out num))
				{
					throw new InvalidOperationException();
				}
				columnSelectionBuilder2.Add(text, num);
			}
			rename = columnSelectionBuilder2.ToColumnSelection();
		}

		// Token: 0x06009E71 RID: 40561 RVA: 0x0020C202 File Offset: 0x0020A402
		public ColumnSelection Add(string key)
		{
			return this.Add(key, this.keys.Length);
		}

		// Token: 0x06009E72 RID: 40562 RVA: 0x0020C218 File Offset: 0x0020A418
		public ColumnSelection Add(string key, int column)
		{
			ColumnSelectionBuilder columnSelectionBuilder = default(ColumnSelectionBuilder);
			for (int i = 0; i < this.keys.Length; i++)
			{
				columnSelectionBuilder.Add(this.keys[i], this.GetColumn(i));
			}
			columnSelectionBuilder.Add(key, column);
			return columnSelectionBuilder.ToColumnSelection();
		}

		// Token: 0x06009E73 RID: 40563 RVA: 0x0020C26D File Offset: 0x0020A46D
		public ColumnSelection Remove(int index)
		{
			return this.Remove(index, 1);
		}

		// Token: 0x06009E74 RID: 40564 RVA: 0x0020C278 File Offset: 0x0020A478
		public ColumnSelection Remove(int index, int count)
		{
			ColumnSelectionBuilder columnSelectionBuilder = default(ColumnSelectionBuilder);
			for (int i = 0; i < this.keys.Length; i++)
			{
				if (i < index || i >= index + count)
				{
					columnSelectionBuilder.Add(this.keys[i], this.GetColumn(i));
				}
			}
			return columnSelectionBuilder.ToColumnSelection();
		}

		// Token: 0x06009E75 RID: 40565 RVA: 0x0020C2D0 File Offset: 0x0020A4D0
		public ColumnSelection RemoveInner(int innerIndex)
		{
			bool flag = true;
			int[] array = new int[this.keys.Length];
			for (int i = 0; i < array.Length; i++)
			{
				int column = this.GetColumn(i);
				array[i] = ((column > innerIndex) ? (column - 1) : column);
				flag &= i == array[i];
			}
			return new ColumnSelection(this.keys, flag ? null : array);
		}

		// Token: 0x06009E76 RID: 40566 RVA: 0x0020C330 File Offset: 0x0020A530
		public ColumnSelection Move(int from, int to)
		{
			if (from < to)
			{
				return this.Move(to, from);
			}
			ColumnSelectionBuilder columnSelectionBuilder = default(ColumnSelectionBuilder);
			for (int i = 0; i < this.keys.Length; i++)
			{
				if (i == to)
				{
					columnSelectionBuilder.Add(this.keys[from], this.GetColumn(from));
				}
				if (i != from)
				{
					columnSelectionBuilder.Add(this.keys[i], this.GetColumn(i));
				}
			}
			return columnSelectionBuilder.ToColumnSelection();
		}

		// Token: 0x06009E77 RID: 40567 RVA: 0x0020C3AC File Offset: 0x0020A5AC
		public ColumnSelection Rename(int column, string key)
		{
			string[] array = new string[this.keys.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = ((i == column) ? key : this.keys[i]);
			}
			return new ColumnSelection(Keys.New(array), this.columns);
		}

		// Token: 0x06009E78 RID: 40568 RVA: 0x0020C3FF File Offset: 0x0020A5FF
		public ColumnSelection.SelectMap CreateSelectMap(Keys columns)
		{
			return this.CreateSelectMap(columns.Length);
		}

		// Token: 0x06009E79 RID: 40569 RVA: 0x0020C40D File Offset: 0x0020A60D
		public ColumnSelection.SelectMap CreateSelectMap(int columnCount)
		{
			return new ColumnSelection.SelectMap(columnCount, this);
		}

		// Token: 0x04005333 RID: 21299
		private Keys keys;

		// Token: 0x04005334 RID: 21300
		private int[] columns;

		// Token: 0x02001866 RID: 6246
		public class SelectMap
		{
			// Token: 0x06009E7A RID: 40570 RVA: 0x0020C418 File Offset: 0x0020A618
			public SelectMap(int columnCount, ColumnSelection columnSelection)
			{
				this.map = new int[columnCount];
				for (int i = 0; i < columnSelection.Keys.Length; i++)
				{
					this.map[columnSelection.GetColumn(i)] = i + 1;
				}
			}

			// Token: 0x06009E7B RID: 40571 RVA: 0x0020C45E File Offset: 0x0020A65E
			public int MapColumn(int column)
			{
				return this.map[column] - 1;
			}

			// Token: 0x06009E7C RID: 40572 RVA: 0x0020C46C File Offset: 0x0020A66C
			public int[] MapColumns(int[] columns)
			{
				int[] array = new int[columns.Length];
				for (int i = 0; i < array.Length; i++)
				{
					int num = this.map[columns[i]];
					if (num == 0)
					{
						return null;
					}
					array[i] = num - 1;
				}
				return array;
			}

			// Token: 0x04005335 RID: 21301
			private int[] map;
		}
	}
}
