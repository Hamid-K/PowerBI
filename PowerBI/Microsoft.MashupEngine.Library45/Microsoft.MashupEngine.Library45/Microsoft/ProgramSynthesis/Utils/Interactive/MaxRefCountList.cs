using System;
using System.Collections.Generic;

namespace Microsoft.ProgramSynthesis.Utils.Interactive
{
	// Token: 0x0200069D RID: 1693
	internal class MaxRefCountList<T> : IRefCountList<T>
	{
		// Token: 0x06002463 RID: 9315 RVA: 0x000662DC File Offset: 0x000644DC
		public void Clear()
		{
			this._list.Clear();
		}

		// Token: 0x17000629 RID: 1577
		// (get) Token: 0x06002464 RID: 9316 RVA: 0x000662E9 File Offset: 0x000644E9
		public int Count
		{
			get
			{
				return this._list.Count;
			}
		}

		// Token: 0x1700062A RID: 1578
		public T this[int i]
		{
			get
			{
				return this._list[i];
			}
		}

		// Token: 0x06002466 RID: 9318 RVA: 0x00066304 File Offset: 0x00064504
		public void Add(T item)
		{
			this._list.Add(item);
		}

		// Token: 0x06002467 RID: 9319 RVA: 0x0000CC37 File Offset: 0x0000AE37
		public void Done(int index)
		{
		}

		// Token: 0x04001165 RID: 4453
		private IList<T> _list = new List<T>();
	}
}
