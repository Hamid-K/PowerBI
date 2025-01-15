using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200021C RID: 540
	internal sealed class DMMultiLevelHashTableEnumerator : IEnumerator
	{
		// Token: 0x060011FE RID: 4606 RVA: 0x00039709 File Offset: 0x00037909
		internal DMMultiLevelHashTableEnumerator(BaseHashTable root)
		{
			if (root != null)
			{
				this._stack.Push(root.GetEnumerator());
			}
		}

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x060011FF RID: 4607 RVA: 0x00039731 File Offset: 0x00037931
		public object Current
		{
			get
			{
				if (this._current != null)
				{
					return this._current;
				}
				throw new InvalidOperationException();
			}
		}

		// Token: 0x06001200 RID: 4608 RVA: 0x00039748 File Offset: 0x00037948
		public bool MoveNext()
		{
			this._current = null;
			while (this._stack.Count > 0)
			{
				IEnumerator enumerator = this._stack.Peek();
				if (enumerator.MoveNext())
				{
					if (!(enumerator.Current is BaseHashTable))
					{
						this._current = enumerator.Current;
						return true;
					}
					BaseHashTable baseHashTable = (BaseHashTable)enumerator.Current;
					this._stack.Push(baseHashTable.GetEnumerator());
				}
				else
				{
					this._stack.Pop();
				}
			}
			return false;
		}

		// Token: 0x06001201 RID: 4609 RVA: 0x00003CAB File Offset: 0x00001EAB
		public void Reset()
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000B09 RID: 2825
		private Stack<IEnumerator> _stack = new Stack<IEnumerator>(8);

		// Token: 0x04000B0A RID: 2826
		private object _current;
	}
}
