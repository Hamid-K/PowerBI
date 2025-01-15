using System;
using Microsoft.Mashup.Common;

namespace Microsoft.Mashup.Engine1.Language.Query
{
	// Token: 0x02001F17 RID: 7959
	internal struct NestedColumnSelectionBuilder
	{
		// Token: 0x17002C6D RID: 11373
		// (get) Token: 0x06010C1B RID: 68635 RVA: 0x0039B8EC File Offset: 0x00399AEC
		public int Count
		{
			get
			{
				return this.columnSelectionBuilder.Count;
			}
		}

		// Token: 0x06010C1C RID: 68636 RVA: 0x0039B907 File Offset: 0x00399B07
		public void Add(string key)
		{
			this.Add(key, this.Count);
		}

		// Token: 0x06010C1D RID: 68637 RVA: 0x0039B916 File Offset: 0x00399B16
		public void Add(string key, int column)
		{
			this.Add(key, column, NestedColumnSelection.All);
		}

		// Token: 0x06010C1E RID: 68638 RVA: 0x0039B928 File Offset: 0x00399B28
		public void Add(string key, int column, NestedColumnSelection nestedColumnSelection)
		{
			this.columnSelectionBuilder.Add(key, column);
			this.nestedColumnSelectionsBuilder.Add(nestedColumnSelection);
		}

		// Token: 0x06010C1F RID: 68639 RVA: 0x0039B954 File Offset: 0x00399B54
		public void Add(ColumnSelection columnSelection)
		{
			for (int i = 0; i < columnSelection.Keys.Length; i++)
			{
				this.Add(columnSelection.Keys[i], columnSelection.GetColumn(i));
			}
		}

		// Token: 0x06010C20 RID: 68640 RVA: 0x0039B990 File Offset: 0x00399B90
		public void Add(NestedColumnSelection nestedColumnSelection)
		{
			for (int i = 0; i < nestedColumnSelection.ColumnSelection.Keys.Length; i++)
			{
				this.Add(nestedColumnSelection.ColumnSelection.Keys[i], nestedColumnSelection.ColumnSelection.GetColumn(i), nestedColumnSelection.GetNestedColumnSelection(i));
			}
		}

		// Token: 0x06010C21 RID: 68641 RVA: 0x0039B9E8 File Offset: 0x00399BE8
		public int IndexOf(string key)
		{
			return this.columnSelectionBuilder.IndexOf(key);
		}

		// Token: 0x06010C22 RID: 68642 RVA: 0x0039BA04 File Offset: 0x00399C04
		public NestedColumnSelection ToNestedColumnSelection()
		{
			return new NestedColumnSelection(this.columnSelectionBuilder.ToColumnSelection(), this.nestedColumnSelectionsBuilder.ToArray());
		}

		// Token: 0x04006461 RID: 25697
		private readonly ColumnSelectionBuilder columnSelectionBuilder;

		// Token: 0x04006462 RID: 25698
		private readonly ArrayBuilder<NestedColumnSelection> nestedColumnSelectionsBuilder;
	}
}
