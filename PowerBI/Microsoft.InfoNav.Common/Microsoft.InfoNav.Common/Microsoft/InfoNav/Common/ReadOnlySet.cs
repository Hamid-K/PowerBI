using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Microsoft.InfoNav.Common
{
	// Token: 0x02000069 RID: 105
	[DebuggerDisplay("Count = {Count}")]
	[DebuggerTypeProxy(typeof(EnumerableDebugView<>))]
	public abstract class ReadOnlySet<T> : ISet<T>, ICollection<T>, IEnumerable<T>, IEnumerable, IReadOnlyCollection<T>
	{
		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060003DB RID: 987
		public abstract int Count { get; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060003DC RID: 988 RVA: 0x0000A470 File Offset: 0x00008670
		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060003DD RID: 989
		public abstract bool IsProperSubsetOf(IEnumerable<T> other);

		// Token: 0x060003DE RID: 990
		public abstract bool IsProperSupersetOf(IEnumerable<T> other);

		// Token: 0x060003DF RID: 991
		public abstract bool IsSubsetOf(IEnumerable<T> other);

		// Token: 0x060003E0 RID: 992
		public abstract bool IsSupersetOf(IEnumerable<T> other);

		// Token: 0x060003E1 RID: 993
		public abstract bool Overlaps(IEnumerable<T> other);

		// Token: 0x060003E2 RID: 994
		public abstract bool SetEquals(IEnumerable<T> other);

		// Token: 0x060003E3 RID: 995
		public abstract bool Contains(T item);

		// Token: 0x060003E4 RID: 996
		public abstract void CopyTo(T[] array, int arrayIndex);

		// Token: 0x060003E5 RID: 997
		internal abstract IEqualityComparer<T> GetComparerOrNull();

		// Token: 0x060003E6 RID: 998
		protected abstract IEnumerator<T> GetEnumeratorCore();

		// Token: 0x060003E7 RID: 999 RVA: 0x0000A473 File Offset: 0x00008673
		public bool Add(T item)
		{
			throw new InvalidOperationException("This set is read-only and cannot be modified");
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x0000A47F File Offset: 0x0000867F
		public void ExceptWith(IEnumerable<T> other)
		{
			throw new InvalidOperationException("This set is read-only and cannot be modified");
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x0000A48B File Offset: 0x0000868B
		public void IntersectWith(IEnumerable<T> other)
		{
			throw new InvalidOperationException("This set is read-only and cannot be modified");
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x0000A497 File Offset: 0x00008697
		void ICollection<T>.Add(T item)
		{
			throw new InvalidOperationException("This set is read-only and cannot be modified");
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x0000A4A3 File Offset: 0x000086A3
		public void Clear()
		{
			throw new InvalidOperationException("This set is read-only and cannot be modified");
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x0000A4AF File Offset: 0x000086AF
		public bool Remove(T item)
		{
			throw new InvalidOperationException("This set is read-only and cannot be modified");
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x0000A4BB File Offset: 0x000086BB
		public void UnionWith(IEnumerable<T> other)
		{
			throw new InvalidOperationException("This set is read-only and cannot be modified");
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x0000A4C7 File Offset: 0x000086C7
		public void SymmetricExceptWith(IEnumerable<T> other)
		{
			throw new InvalidOperationException("This set is read-only and cannot be modified");
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x0000A4D3 File Offset: 0x000086D3
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return this.GetEnumeratorCore();
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x0000A4DB File Offset: 0x000086DB
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumeratorCore();
		}

		// Token: 0x040000D8 RID: 216
		private const string ReadOnlyErrorMessage = "This set is read-only and cannot be modified";

		// Token: 0x040000D9 RID: 217
		internal static readonly ReadOnlySet<T> Empty = new ReadOnlySetGeneric<T>(new HashSet<T>());
	}
}
