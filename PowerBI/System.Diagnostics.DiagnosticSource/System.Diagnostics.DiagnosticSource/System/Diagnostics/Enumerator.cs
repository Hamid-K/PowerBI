using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace System.Diagnostics
{
	// Token: 0x0200002A RID: 42
	internal struct Enumerator<T> : IEnumerator<T>, IDisposable, IEnumerator
	{
		// Token: 0x0600016B RID: 363 RVA: 0x00005F93 File Offset: 0x00004193
		public Enumerator(DiagNode<T> head)
		{
			this._nextNode = head;
			this._currentItem = default(T);
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600016C RID: 364 RVA: 0x00005FA8 File Offset: 0x000041A8
		public T Current
		{
			get
			{
				return this._currentItem;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600016D RID: 365 RVA: 0x00005FB0 File Offset: 0x000041B0
		object IEnumerator.Current
		{
			get
			{
				return this.Current;
			}
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00005FBD File Offset: 0x000041BD
		public bool MoveNext()
		{
			if (this._nextNode == null)
			{
				this._currentItem = default(T);
				return false;
			}
			this._currentItem = this._nextNode.Value;
			this._nextNode = this._nextNode.Next;
			return true;
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00005FF8 File Offset: 0x000041F8
		public void Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00005FFF File Offset: 0x000041FF
		public void Dispose()
		{
		}

		// Token: 0x04000084 RID: 132
		private DiagNode<T> _nextNode;

		// Token: 0x04000085 RID: 133
		[AllowNull]
		[MaybeNull]
		private T _currentItem;
	}
}
