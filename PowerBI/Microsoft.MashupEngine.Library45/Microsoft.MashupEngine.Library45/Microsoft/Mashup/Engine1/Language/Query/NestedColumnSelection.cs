using System;

namespace Microsoft.Mashup.Engine1.Language.Query
{
	// Token: 0x02001F16 RID: 7958
	internal struct NestedColumnSelection
	{
		// Token: 0x06010C10 RID: 68624 RVA: 0x0039B6F2 File Offset: 0x003998F2
		public NestedColumnSelection(ColumnSelection columnSelection, NestedColumnSelection[] nestedColumnSelections = null)
		{
			this.columnSelection = columnSelection;
			this.nestedColumnSelections = nestedColumnSelections;
		}

		// Token: 0x17002C69 RID: 11369
		// (get) Token: 0x06010C11 RID: 68625 RVA: 0x0039B704 File Offset: 0x00399904
		public static NestedColumnSelection All
		{
			get
			{
				return default(NestedColumnSelection);
			}
		}

		// Token: 0x17002C6A RID: 11370
		// (get) Token: 0x06010C12 RID: 68626 RVA: 0x0039B71A File Offset: 0x0039991A
		public ColumnSelection ColumnSelection
		{
			get
			{
				return this.columnSelection;
			}
		}

		// Token: 0x17002C6B RID: 11371
		// (get) Token: 0x06010C13 RID: 68627 RVA: 0x0039B722 File Offset: 0x00399922
		public bool IsAll
		{
			get
			{
				return this.columnSelection == null;
			}
		}

		// Token: 0x17002C6C RID: 11372
		// (get) Token: 0x06010C14 RID: 68628 RVA: 0x0039B72D File Offset: 0x0039992D
		public bool IsFlat
		{
			get
			{
				return this.columnSelection == null || this.nestedColumnSelections == null;
			}
		}

		// Token: 0x06010C15 RID: 68629 RVA: 0x0039B742 File Offset: 0x00399942
		public int GetColumn(int index)
		{
			if (this.IsAll)
			{
				return index;
			}
			return this.ColumnSelection.GetColumn(index);
		}

		// Token: 0x06010C16 RID: 68630 RVA: 0x0039B75A File Offset: 0x0039995A
		public NestedColumnSelection GetNestedColumnSelection(int index)
		{
			if (!this.IsFlat)
			{
				return this.nestedColumnSelections[index];
			}
			return NestedColumnSelection.All;
		}

		// Token: 0x06010C17 RID: 68631 RVA: 0x0039B778 File Offset: 0x00399978
		public NestedColumnSelection SelectColumns(ColumnSelection columnSelection)
		{
			if (this.IsAll)
			{
				return new NestedColumnSelection(columnSelection, null);
			}
			if (this.IsFlat)
			{
				return new NestedColumnSelection(this.columnSelection.SelectColumns(columnSelection), null);
			}
			NestedColumnSelection[] array = new NestedColumnSelection[columnSelection.Keys.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = this.GetNestedColumnSelection(columnSelection.GetColumn(i));
			}
			return new NestedColumnSelection(columnSelection, array);
		}

		// Token: 0x06010C18 RID: 68632 RVA: 0x0039B7EA File Offset: 0x003999EA
		public NestedColumnSelection Rename(int column, string key)
		{
			if (this.IsAll)
			{
				throw new InvalidOperationException();
			}
			return new NestedColumnSelection(this.columnSelection.Rename(column, key), this.nestedColumnSelections);
		}

		// Token: 0x06010C19 RID: 68633 RVA: 0x0039B814 File Offset: 0x00399A14
		public NestedColumnSelection SelectColumns(NestedColumnSelection columnSelection)
		{
			if (columnSelection.IsAll)
			{
				return this;
			}
			return this.SelectNestedColumns(columnSelection).SelectColumns(columnSelection.ColumnSelection);
		}

		// Token: 0x06010C1A RID: 68634 RVA: 0x0039B848 File Offset: 0x00399A48
		public NestedColumnSelection SelectNestedColumns(NestedColumnSelection columnSelection)
		{
			if (columnSelection.IsFlat)
			{
				return this;
			}
			NestedColumnSelection[] array = new NestedColumnSelection[columnSelection.ColumnSelection.Keys.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = this.GetNestedColumnSelection(columnSelection.ColumnSelection.GetColumn(i));
				if (array[i].IsAll)
				{
					array[i] = columnSelection.GetNestedColumnSelection(i);
				}
				else
				{
					array[i] = array[i].SelectColumns(columnSelection.GetNestedColumnSelection(i));
				}
			}
			return new NestedColumnSelection(columnSelection.ColumnSelection, array);
		}

		// Token: 0x0400645F RID: 25695
		private readonly ColumnSelection columnSelection;

		// Token: 0x04006460 RID: 25696
		private readonly NestedColumnSelection[] nestedColumnSelections;
	}
}
