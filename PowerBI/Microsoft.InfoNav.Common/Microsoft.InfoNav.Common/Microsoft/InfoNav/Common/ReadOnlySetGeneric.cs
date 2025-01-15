using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.InfoNav.Common
{
	// Token: 0x0200006A RID: 106
	internal sealed class ReadOnlySetGeneric<T> : ReadOnlySet<T>
	{
		// Token: 0x060003F3 RID: 1011 RVA: 0x0000A4FC File Offset: 0x000086FC
		internal ReadOnlySetGeneric(ISet<T> innerSet)
		{
			this._innerSet = innerSet;
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060003F4 RID: 1012 RVA: 0x0000A50B File Offset: 0x0000870B
		public override int Count
		{
			get
			{
				return this._innerSet.Count;
			}
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x0000A518 File Offset: 0x00008718
		public override bool IsProperSubsetOf(IEnumerable<T> other)
		{
			return this._innerSet.IsProperSubsetOf(other);
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x0000A526 File Offset: 0x00008726
		public override bool IsProperSupersetOf(IEnumerable<T> other)
		{
			return this._innerSet.IsProperSupersetOf(other);
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x0000A534 File Offset: 0x00008734
		public override bool IsSubsetOf(IEnumerable<T> other)
		{
			return this._innerSet.IsSubsetOf(other);
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x0000A542 File Offset: 0x00008742
		public override bool IsSupersetOf(IEnumerable<T> other)
		{
			return this._innerSet.IsSupersetOf(other);
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x0000A550 File Offset: 0x00008750
		public override bool Overlaps(IEnumerable<T> other)
		{
			return this._innerSet.Overlaps(other);
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x0000A55E File Offset: 0x0000875E
		public override bool SetEquals(IEnumerable<T> other)
		{
			return this._innerSet.SetEquals(other);
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x0000A56C File Offset: 0x0000876C
		public override bool Contains(T item)
		{
			return this._innerSet.Contains(item);
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x0000A57A File Offset: 0x0000877A
		public override void CopyTo(T[] array, int arrayIndex)
		{
			this._innerSet.CopyTo(array, arrayIndex);
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x0000A58C File Offset: 0x0000878C
		internal override IEqualityComparer<T> GetComparerOrNull()
		{
			HashSet<T> hashSet = this._innerSet as HashSet<T>;
			if (hashSet != null)
			{
				return hashSet.Comparer;
			}
			return null;
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x0000A5B0 File Offset: 0x000087B0
		public ReadOnlySetGeneric<T>.Enumerator GetEnumerator()
		{
			return new ReadOnlySetGeneric<T>.Enumerator(this._innerSet);
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x0000A5BD File Offset: 0x000087BD
		protected override IEnumerator<T> GetEnumeratorCore()
		{
			return this._innerSet.GetEnumerator();
		}

		// Token: 0x040000DA RID: 218
		private const string ReadOnlyErrorMessage = "This set is read-only and cannot be modified";

		// Token: 0x040000DB RID: 219
		private readonly ISet<T> _innerSet;

		// Token: 0x020000C6 RID: 198
		public struct Enumerator : IEnumerator<T>, IDisposable, IEnumerator
		{
			// Token: 0x060005F1 RID: 1521 RVA: 0x0000F924 File Offset: 0x0000DB24
			internal Enumerator(ISet<T> innerSet)
			{
				HashSet<T> hashSet = innerSet as HashSet<T>;
				if (hashSet != null)
				{
					this._hashSetEnumerator = hashSet.GetEnumerator();
					this._enumerator = null;
					return;
				}
				this._enumerator = innerSet.GetEnumerator();
				this._hashSetEnumerator = default(HashSet<T>.Enumerator);
			}

			// Token: 0x170000D1 RID: 209
			// (get) Token: 0x060005F2 RID: 1522 RVA: 0x0000F967 File Offset: 0x0000DB67
			public T Current
			{
				get
				{
					if (this._enumerator == null)
					{
						return this._hashSetEnumerator.Current;
					}
					return this._enumerator.Current;
				}
			}

			// Token: 0x170000D2 RID: 210
			// (get) Token: 0x060005F3 RID: 1523 RVA: 0x0000F988 File Offset: 0x0000DB88
			object IEnumerator.Current
			{
				get
				{
					return (this._enumerator != null) ? this._enumerator.Current : this._hashSetEnumerator.Current;
				}
			}

			// Token: 0x060005F4 RID: 1524 RVA: 0x0000F9AF File Offset: 0x0000DBAF
			public void Dispose()
			{
			}

			// Token: 0x060005F5 RID: 1525 RVA: 0x0000F9B1 File Offset: 0x0000DBB1
			public bool MoveNext()
			{
				if (this._enumerator == null)
				{
					return this._hashSetEnumerator.MoveNext();
				}
				return this._enumerator.MoveNext();
			}

			// Token: 0x060005F6 RID: 1526 RVA: 0x0000F9D2 File Offset: 0x0000DBD2
			void IEnumerator.Reset()
			{
				if (this._enumerator != null)
				{
					this._enumerator.Reset();
					return;
				}
				((IEnumerator)this._hashSetEnumerator).Reset();
			}

			// Token: 0x04000209 RID: 521
			private IEnumerator<T> _enumerator;

			// Token: 0x0400020A RID: 522
			private HashSet<T>.Enumerator _hashSetEnumerator;
		}
	}
}
