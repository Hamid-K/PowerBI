using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x0200003D RID: 61
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public abstract class IndirectReadOnlyCollection<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable, IList, ICollection
	{
		// Token: 0x17000038 RID: 56
		public abstract T this[int index] { get; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600025A RID: 602
		public abstract int Count { get; }

		// Token: 0x0600025B RID: 603 RVA: 0x0000971C File Offset: 0x0000791C
		public bool Contains(T item)
		{
			return this.IndexOf(item) != -1;
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0000972C File Offset: 0x0000792C
		public void CopyTo(T[] array, int arrayIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException();
			}
			if (arrayIndex < 0 || arrayIndex > array.Length - this.Count)
			{
				throw new ArgumentOutOfRangeException();
			}
			for (int i = 0; i < this.Count; i++)
			{
				array[i + arrayIndex] = this[i];
			}
		}

		// Token: 0x0600025D RID: 605 RVA: 0x0000977C File Offset: 0x0000797C
		public int IndexOf(T item)
		{
			for (int i = 0; i < this.Count; i++)
			{
				if (object.Equals(this[i], item))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0600025E RID: 606 RVA: 0x000097B6 File Offset: 0x000079B6
		public IndirectReadOnlyCollection<T>.Enumerator GetEnumerator()
		{
			return new IndirectReadOnlyCollection<T>.Enumerator(this);
		}

		// Token: 0x1700003A RID: 58
		T IList<T>.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000261 RID: 609 RVA: 0x000097CE File Offset: 0x000079CE
		bool ICollection<T>.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000262 RID: 610 RVA: 0x000097D1 File Offset: 0x000079D1
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000263 RID: 611 RVA: 0x000097DE File Offset: 0x000079DE
		void ICollection<T>.Add(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000264 RID: 612 RVA: 0x000097E5 File Offset: 0x000079E5
		void ICollection<T>.Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000265 RID: 613 RVA: 0x000097EC File Offset: 0x000079EC
		void IList<T>.Insert(int index, T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000266 RID: 614 RVA: 0x000097F3 File Offset: 0x000079F3
		bool ICollection<T>.Remove(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000267 RID: 615 RVA: 0x000097FA File Offset: 0x000079FA
		void IList<T>.RemoveAt(int index)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000268 RID: 616 RVA: 0x00009801 File Offset: 0x00007A01
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000269 RID: 617 RVA: 0x00009810 File Offset: 0x00007A10
		void ICollection.CopyTo(Array array, int arrayIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException();
			}
			if (arrayIndex < 0 || arrayIndex > array.Length - this.Count)
			{
				throw new ArgumentOutOfRangeException();
			}
			for (int i = 0; i < this.Count; i++)
			{
				array.SetValue(this[i], i + arrayIndex);
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600026A RID: 618 RVA: 0x00009866 File Offset: 0x00007A66
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600026B RID: 619 RVA: 0x00009869 File Offset: 0x00007A69
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x1700003E RID: 62
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600026E RID: 622 RVA: 0x00009881 File Offset: 0x00007A81
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x0600026F RID: 623 RVA: 0x00009884 File Offset: 0x00007A84
		bool IList.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000270 RID: 624 RVA: 0x00009887 File Offset: 0x00007A87
		int IList.Add(object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0000988E File Offset: 0x00007A8E
		void IList.Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000272 RID: 626 RVA: 0x00009895 File Offset: 0x00007A95
		bool IList.Contains(object value)
		{
			return value is T && this.Contains((T)((object)value));
		}

		// Token: 0x06000273 RID: 627 RVA: 0x000098AD File Offset: 0x00007AAD
		int IList.IndexOf(object value)
		{
			if (value is T)
			{
				return this.IndexOf((T)((object)value));
			}
			return -1;
		}

		// Token: 0x06000274 RID: 628 RVA: 0x000098C5 File Offset: 0x00007AC5
		void IList.Insert(int index, object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000275 RID: 629 RVA: 0x000098CC File Offset: 0x00007ACC
		void IList.Remove(object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000276 RID: 630 RVA: 0x000098D3 File Offset: 0x00007AD3
		void IList.RemoveAt(int index)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0200011B RID: 283
		public struct Enumerator : IEnumerator<T>, IDisposable, IEnumerator
		{
			// Token: 0x06000D99 RID: 3481 RVA: 0x0002CED6 File Offset: 0x0002B0D6
			internal Enumerator(IndirectReadOnlyCollection<T> coll)
			{
				this.m_coll = coll;
				this.m_endIndex = this.m_coll.Count - 1;
				this.m_index = -1;
				this.m_current = default(T);
			}

			// Token: 0x17000318 RID: 792
			// (get) Token: 0x06000D9A RID: 3482 RVA: 0x0002CF05 File Offset: 0x0002B105
			public T Current
			{
				get
				{
					return this.m_current;
				}
			}

			// Token: 0x06000D9B RID: 3483 RVA: 0x0002CF0D File Offset: 0x0002B10D
			public bool MoveNext()
			{
				if (this.m_index == this.m_endIndex)
				{
					return false;
				}
				this.m_index++;
				this.m_current = this.m_coll[this.m_index];
				return true;
			}

			// Token: 0x06000D9C RID: 3484 RVA: 0x0002CF45 File Offset: 0x0002B145
			void IDisposable.Dispose()
			{
			}

			// Token: 0x17000319 RID: 793
			// (get) Token: 0x06000D9D RID: 3485 RVA: 0x0002CF47 File Offset: 0x0002B147
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06000D9E RID: 3486 RVA: 0x0002CF54 File Offset: 0x0002B154
			void IEnumerator.Reset()
			{
				this.m_index = -1;
				this.m_current = default(T);
			}

			// Token: 0x040005A8 RID: 1448
			private readonly IndirectReadOnlyCollection<T> m_coll;

			// Token: 0x040005A9 RID: 1449
			private readonly int m_endIndex;

			// Token: 0x040005AA RID: 1450
			private int m_index;

			// Token: 0x040005AB RID: 1451
			private T m_current;
		}
	}
}
