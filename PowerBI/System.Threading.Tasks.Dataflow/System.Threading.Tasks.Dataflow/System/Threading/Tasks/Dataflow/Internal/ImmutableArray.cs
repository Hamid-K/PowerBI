using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace System.Threading.Tasks.Dataflow.Internal
{
	// Token: 0x0200003F RID: 63
	[DebuggerDisplay("Count={Count}")]
	internal readonly struct ImmutableArray<T>
	{
		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000228 RID: 552 RVA: 0x00009085 File Offset: 0x00007285
		public static ImmutableArray<T> Empty
		{
			get
			{
				return ImmutableArray<T>.s_empty;
			}
		}

		// Token: 0x06000229 RID: 553 RVA: 0x0000908C File Offset: 0x0000728C
		private ImmutableArray(T[] elements)
		{
			this._array = elements;
		}

		// Token: 0x0600022A RID: 554 RVA: 0x00009098 File Offset: 0x00007298
		public ImmutableArray<T> Add(T item)
		{
			T[] array = new T[this._array.Length + 1];
			Array.Copy(this._array, array, this._array.Length);
			array[array.Length - 1] = item;
			return new ImmutableArray<T>(array);
		}

		// Token: 0x0600022B RID: 555 RVA: 0x000090DC File Offset: 0x000072DC
		public ImmutableArray<T> Remove(T item)
		{
			int num = Array.IndexOf<T>(this._array, item);
			if (num < 0)
			{
				return this;
			}
			if (this._array.Length == 1)
			{
				return ImmutableArray<T>.Empty;
			}
			T[] array = new T[this._array.Length - 1];
			Array.Copy(this._array, array, num);
			Array.Copy(this._array, num + 1, array, num, this._array.Length - num - 1);
			return new ImmutableArray<T>(array);
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x0600022C RID: 556 RVA: 0x00009151 File Offset: 0x00007351
		public int Count
		{
			get
			{
				return this._array.Length;
			}
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0000915B File Offset: 0x0000735B
		public bool Contains(T item)
		{
			return Array.IndexOf<T>(this._array, item) >= 0;
		}

		// Token: 0x0600022E RID: 558 RVA: 0x0000916F File Offset: 0x0000736F
		public IEnumerator<T> GetEnumerator()
		{
			return this._array.GetEnumerator();
		}

		// Token: 0x0600022F RID: 559 RVA: 0x0000917C File Offset: 0x0000737C
		public T[] ToArray()
		{
			if (this._array.Length != 0)
			{
				return (T[])this._array.Clone();
			}
			return ImmutableArray<T>.s_empty._array;
		}

		// Token: 0x04000099 RID: 153
		private static readonly ImmutableArray<T> s_empty = new ImmutableArray<T>(new T[0]);

		// Token: 0x0400009A RID: 154
		private readonly T[] _array;
	}
}
