using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Collections
{
	// Token: 0x02000403 RID: 1027
	internal sealed class StringList : IStringList, IEnumerable<string>, IEnumerable
	{
		// Token: 0x060020C9 RID: 8393 RVA: 0x0005804E File Offset: 0x0005624E
		internal StringList(IEnumerable<string> list)
		{
			this._list = list;
		}

		// Token: 0x17000A4B RID: 2635
		public string this[int index]
		{
			get
			{
				return this._list.GetItemByIndex(index);
			}
		}

		// Token: 0x17000A4C RID: 2636
		// (get) Token: 0x060020CB RID: 8395 RVA: 0x0005806B File Offset: 0x0005626B
		public int Length
		{
			get
			{
				return this._list.Count<string>();
			}
		}

		// Token: 0x060020CC RID: 8396 RVA: 0x00058078 File Offset: 0x00056278
		public bool Contains(string entry)
		{
			return this._list.Contains(entry);
		}

		// Token: 0x060020CD RID: 8397 RVA: 0x00058086 File Offset: 0x00056286
		public IEnumerator<string> GetEnumerator()
		{
			return this._list.GetEnumerator();
		}

		// Token: 0x060020CE RID: 8398 RVA: 0x00058086 File Offset: 0x00056286
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this._list.GetEnumerator();
		}

		// Token: 0x04000D20 RID: 3360
		private readonly IEnumerable<string> _list;
	}
}
