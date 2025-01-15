using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000204 RID: 516
	internal abstract class BaseHashTableEnumerator : IEnumerator
	{
		// Token: 0x060010E5 RID: 4325 RVA: 0x00037E6C File Offset: 0x0003606C
		internal BaseHashTableEnumerator(BaseHashTable parentHashTable)
		{
			this._parentHashTable = parentHashTable;
			this.Reset();
		}

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x060010E6 RID: 4326 RVA: 0x00037E81 File Offset: 0x00036081
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

		// Token: 0x060010E7 RID: 4327 RVA: 0x00037E98 File Offset: 0x00036098
		public bool MoveNext()
		{
			this._current = null;
			while (this._stateStack.Count > 0)
			{
				IEnumerator enumerator = this._stateStack.Peek();
				if (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					IBaseDataNode baseDataNode = obj as IBaseDataNode;
					if (baseDataNode != null)
					{
						this._current = this.GetCurrent(baseDataNode);
						return true;
					}
					BaseConflictNode baseConflictNode = obj as BaseConflictNode;
					if (baseConflictNode != null)
					{
						this._stateStack.Push(new BaseConflictEnumerator(baseConflictNode));
					}
					else
					{
						this._stateStack.Push(new BaseDirectoryEnumerator((MDHDirectoryNode)obj));
					}
				}
				else
				{
					this._stateStack.Pop();
				}
			}
			return false;
		}

		// Token: 0x060010E8 RID: 4328
		protected abstract object GetCurrent(IBaseDataNode baseDataNode);

		// Token: 0x060010E9 RID: 4329 RVA: 0x00037F37 File Offset: 0x00036137
		public void Reset()
		{
			this._stateStack = new Stack<IEnumerator>(16);
			this._stateStack.Push(new BaseDirectoryEnumerator(this._parentHashTable.RootDirectory));
		}

		// Token: 0x04000AD9 RID: 2777
		private Stack<IEnumerator> _stateStack;

		// Token: 0x04000ADA RID: 2778
		private object _current;

		// Token: 0x04000ADB RID: 2779
		private readonly BaseHashTable _parentHashTable;
	}
}
