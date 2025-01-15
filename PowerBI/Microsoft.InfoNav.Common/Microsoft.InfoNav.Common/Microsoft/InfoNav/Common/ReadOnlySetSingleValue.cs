using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace Microsoft.InfoNav.Common
{
	// Token: 0x0200006B RID: 107
	[ImmutableObject(true)]
	internal sealed class ReadOnlySetSingleValue<T> : ReadOnlySet<T>
	{
		// Token: 0x06000400 RID: 1024 RVA: 0x0000A5CA File Offset: 0x000087CA
		internal ReadOnlySetSingleValue(T item, IEqualityComparer<T> comparer = null)
		{
			this._item = item;
			this._comparer = comparer ?? EqualityComparer<T>.Default;
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000401 RID: 1025 RVA: 0x0000A5E9 File Offset: 0x000087E9
		public override int Count
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000402 RID: 1026 RVA: 0x0000A5EC File Offset: 0x000087EC
		internal IEqualityComparer<T> Comparer
		{
			get
			{
				return this._comparer;
			}
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x0000A5F4 File Offset: 0x000087F4
		public override bool Contains(T item)
		{
			return this.ItemEquals(item);
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x0000A600 File Offset: 0x00008800
		public override bool IsProperSubsetOf(IEnumerable<T> other)
		{
			HashSet<T> hashSet = other as HashSet<T>;
			if (hashSet != null && hashSet.Comparer.Equals(this._comparer))
			{
				return hashSet.Count > 1 && hashSet.Contains(this._item);
			}
			bool flag = false;
			bool flag2 = false;
			foreach (T t in other)
			{
				if (this.ItemEquals(t))
				{
					flag = true;
				}
				else
				{
					flag2 = true;
				}
				if (flag && flag2)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x0000A69C File Offset: 0x0000889C
		public override bool IsProperSupersetOf(IEnumerable<T> other)
		{
			ICollection<T> collection = other as ICollection<T>;
			if (collection != null)
			{
				return collection.Count == 0;
			}
			return !other.GetEnumerator().MoveNext();
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x0000A6CC File Offset: 0x000088CC
		public override bool IsSubsetOf(IEnumerable<T> other)
		{
			HashSet<T> hashSet = other as HashSet<T>;
			if (hashSet != null && hashSet.Comparer.Equals(this._comparer))
			{
				return hashSet.Contains(this._item);
			}
			foreach (T t in other)
			{
				if (this.ItemEquals(t))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x0000A748 File Offset: 0x00008948
		public override bool IsSupersetOf(IEnumerable<T> other)
		{
			foreach (T t in other)
			{
				if (!this.ItemEquals(t))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x0000A79C File Offset: 0x0000899C
		public override bool Overlaps(IEnumerable<T> other)
		{
			return this.IsSubsetOf(other);
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x0000A7A8 File Offset: 0x000089A8
		public override bool SetEquals(IEnumerable<T> other)
		{
			IEnumerator<T> enumerator = other.GetEnumerator();
			if (!enumerator.MoveNext())
			{
				return false;
			}
			while (this.ItemEquals(enumerator.Current))
			{
				if (!enumerator.MoveNext())
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x0000A7DF File Offset: 0x000089DF
		public override void CopyTo(T[] array, int arrayIndex)
		{
			array[arrayIndex] = this._item;
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x0000A7EE File Offset: 0x000089EE
		internal override IEqualityComparer<T> GetComparerOrNull()
		{
			return this._comparer;
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x0000A7F6 File Offset: 0x000089F6
		public ReadOnlySetSingleValue<T>.Enumerator GetEnumerator()
		{
			return new ReadOnlySetSingleValue<T>.Enumerator(this._item);
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x0000A803 File Offset: 0x00008A03
		protected override IEnumerator<T> GetEnumeratorCore()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x0000A810 File Offset: 0x00008A10
		private bool ItemEquals(T other)
		{
			return this._comparer.Equals(this._item, other);
		}

		// Token: 0x040000DC RID: 220
		private readonly T _item;

		// Token: 0x040000DD RID: 221
		private readonly IEqualityComparer<T> _comparer;

		// Token: 0x020000C7 RID: 199
		public struct Enumerator : IEnumerator<T>, IDisposable, IEnumerator
		{
			// Token: 0x060005F7 RID: 1527 RVA: 0x0000F9F8 File Offset: 0x0000DBF8
			internal Enumerator(T item)
			{
				this._item = item;
				this._started = false;
			}

			// Token: 0x170000D3 RID: 211
			// (get) Token: 0x060005F8 RID: 1528 RVA: 0x0000FA08 File Offset: 0x0000DC08
			public T Current
			{
				get
				{
					if (!this._started)
					{
						return default(T);
					}
					return this._item;
				}
			}

			// Token: 0x170000D4 RID: 212
			// (get) Token: 0x060005F9 RID: 1529 RVA: 0x0000FA30 File Offset: 0x0000DC30
			object IEnumerator.Current
			{
				get
				{
					return this._started ? this._item : default(T);
				}
			}

			// Token: 0x060005FA RID: 1530 RVA: 0x0000FA5B File Offset: 0x0000DC5B
			public void Dispose()
			{
			}

			// Token: 0x060005FB RID: 1531 RVA: 0x0000FA5D File Offset: 0x0000DC5D
			public bool MoveNext()
			{
				if (this._started)
				{
					return false;
				}
				this._started = true;
				return true;
			}

			// Token: 0x060005FC RID: 1532 RVA: 0x0000FA71 File Offset: 0x0000DC71
			void IEnumerator.Reset()
			{
				this._started = false;
			}

			// Token: 0x0400020B RID: 523
			private readonly T _item;

			// Token: 0x0400020C RID: 524
			private bool _started;
		}
	}
}
