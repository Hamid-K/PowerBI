using System;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Cube
{
	// Token: 0x02000D76 RID: 3446
	internal struct View
	{
		// Token: 0x06005DE4 RID: 24036 RVA: 0x001446AD File Offset: 0x001428AD
		public View(Keys keys)
		{
			this = new View(keys, null);
		}

		// Token: 0x06005DE5 RID: 24037 RVA: 0x001446B7 File Offset: 0x001428B7
		public View(Keys keys, int[] columns)
		{
			this = new View(new ColumnSelection(keys, columns));
		}

		// Token: 0x06005DE6 RID: 24038 RVA: 0x001446C6 File Offset: 0x001428C6
		private View(ColumnSelection innerSelection)
		{
			this.innerSelection = innerSelection;
		}

		// Token: 0x17001BAB RID: 7083
		// (get) Token: 0x06005DE7 RID: 24039 RVA: 0x001446CF File Offset: 0x001428CF
		public ColumnSelection InnerSelection
		{
			get
			{
				return this.innerSelection;
			}
		}

		// Token: 0x17001BAC RID: 7084
		// (get) Token: 0x06005DE8 RID: 24040 RVA: 0x001446D7 File Offset: 0x001428D7
		public Keys Keys
		{
			get
			{
				return this.innerSelection.Keys;
			}
		}

		// Token: 0x06005DE9 RID: 24041 RVA: 0x001446E4 File Offset: 0x001428E4
		public int GetColumn(int index)
		{
			return this.innerSelection.GetColumn(index);
		}

		// Token: 0x06005DEA RID: 24042 RVA: 0x001446F2 File Offset: 0x001428F2
		public View SelectColumns(ColumnSelection columnSelection)
		{
			return new View(this.innerSelection.SelectColumns(columnSelection));
		}

		// Token: 0x06005DEB RID: 24043 RVA: 0x00144705 File Offset: 0x00142905
		public View Remove(int index)
		{
			return new View(this.innerSelection.Remove(index));
		}

		// Token: 0x06005DEC RID: 24044 RVA: 0x00144718 File Offset: 0x00142918
		public View RemoveInner(int column)
		{
			return new View(this.innerSelection.RemoveInner(column));
		}

		// Token: 0x06005DED RID: 24045 RVA: 0x0014472B File Offset: 0x0014292B
		public View.Map CreateMap(int columnCount)
		{
			return new View.Map(this.innerSelection.CreateSelectMap(columnCount));
		}

		// Token: 0x0400337C RID: 13180
		private readonly ColumnSelection innerSelection;

		// Token: 0x02000D77 RID: 3447
		public struct Map
		{
			// Token: 0x06005DEE RID: 24046 RVA: 0x0014473E File Offset: 0x0014293E
			public Map(ColumnSelection.SelectMap innerMap)
			{
				this.innerMap = innerMap;
			}

			// Token: 0x06005DEF RID: 24047 RVA: 0x00144747 File Offset: 0x00142947
			public int MapColumnToSomeKey(int column)
			{
				return this.innerMap.MapColumn(column);
			}

			// Token: 0x0400337D RID: 13181
			private ColumnSelection.SelectMap innerMap;
		}

		// Token: 0x02000D78 RID: 3448
		public struct Builder
		{
			// Token: 0x06005DF0 RID: 24048 RVA: 0x00144755 File Offset: 0x00142955
			public void Add(View view)
			{
				this.innerBuilder.Add(view.innerSelection);
			}

			// Token: 0x06005DF1 RID: 24049 RVA: 0x00144768 File Offset: 0x00142968
			public void Add(string key, int column)
			{
				this.innerBuilder.Add(key, column);
			}

			// Token: 0x06005DF2 RID: 24050 RVA: 0x00144777 File Offset: 0x00142977
			public View ToView()
			{
				return new View(this.innerBuilder.ToColumnSelection());
			}

			// Token: 0x0400337E RID: 13182
			private ColumnSelectionBuilder innerBuilder;
		}
	}
}
