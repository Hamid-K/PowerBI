using System;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Language.Query
{
	// Token: 0x02001867 RID: 6247
	internal struct ColumnSelectionBuilder
	{
		// Token: 0x170028E4 RID: 10468
		// (get) Token: 0x06009E7D RID: 40573 RVA: 0x0020C4A7 File Offset: 0x0020A6A7
		public int Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x06009E7E RID: 40574 RVA: 0x0020C4AF File Offset: 0x0020A6AF
		public void Add(string key)
		{
			this.Add(key, this.count);
		}

		// Token: 0x06009E7F RID: 40575 RVA: 0x0020C4C0 File Offset: 0x0020A6C0
		public void Add(string key, int column)
		{
			this.keys.Add(key);
			if (column != this.count && this.columns.Count == 0)
			{
				this.columns = default(ArrayBuilder<int>);
				for (int i = 0; i < this.count; i++)
				{
					this.columns.Add(i);
				}
			}
			if (column != this.count || this.columns.Count != 0)
			{
				this.columns.Add(column);
			}
			this.count++;
		}

		// Token: 0x06009E80 RID: 40576 RVA: 0x0020C548 File Offset: 0x0020A748
		public void Add(ColumnSelection columnSelection)
		{
			for (int i = 0; i < columnSelection.Keys.Length; i++)
			{
				this.Add(columnSelection.Keys[i], columnSelection.GetColumn(i));
			}
		}

		// Token: 0x06009E81 RID: 40577 RVA: 0x0020C584 File Offset: 0x0020A784
		public int IndexOf(string key)
		{
			return this.keys.IndexOf(key);
		}

		// Token: 0x06009E82 RID: 40578 RVA: 0x0020C592 File Offset: 0x0020A792
		public ColumnSelection ToColumnSelection()
		{
			return new ColumnSelection(this.keys.ToKeys(), (this.columns.Count == 0) ? null : this.columns.ToArray());
		}

		// Token: 0x04005336 RID: 21302
		private int count;

		// Token: 0x04005337 RID: 21303
		private KeysBuilder keys;

		// Token: 0x04005338 RID: 21304
		private ArrayBuilder<int> columns;
	}
}
