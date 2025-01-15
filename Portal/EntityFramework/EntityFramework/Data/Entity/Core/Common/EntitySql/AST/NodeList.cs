using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Data.Entity.Core.Common.EntitySql.AST
{
	// Token: 0x02000675 RID: 1653
	internal sealed class NodeList<T> : Node, IEnumerable<T>, IEnumerable where T : Node
	{
		// Token: 0x06004EFA RID: 20218 RVA: 0x0011F62D File Offset: 0x0011D82D
		internal NodeList()
		{
		}

		// Token: 0x06004EFB RID: 20219 RVA: 0x0011F640 File Offset: 0x0011D840
		internal NodeList(T item)
		{
			this._list.Add(item);
		}

		// Token: 0x06004EFC RID: 20220 RVA: 0x0011F65F File Offset: 0x0011D85F
		internal NodeList<T> Add(T item)
		{
			this._list.Add(item);
			return this;
		}

		// Token: 0x17000F38 RID: 3896
		// (get) Token: 0x06004EFD RID: 20221 RVA: 0x0011F66E File Offset: 0x0011D86E
		internal int Count
		{
			get
			{
				return this._list.Count;
			}
		}

		// Token: 0x17000F39 RID: 3897
		internal T this[int index]
		{
			get
			{
				return this._list[index];
			}
		}

		// Token: 0x06004EFF RID: 20223 RVA: 0x0011F689 File Offset: 0x0011D889
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return this._list.GetEnumerator();
		}

		// Token: 0x06004F00 RID: 20224 RVA: 0x0011F69B File Offset: 0x0011D89B
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this._list.GetEnumerator();
		}

		// Token: 0x04001C90 RID: 7312
		private readonly List<T> _list = new List<T>();
	}
}
