using System;
using System.Collections;
using System.Data;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000A7 RID: 167
	public sealed class MemberCollection : ICollection, IEnumerable
	{
		// Token: 0x060009B9 RID: 2489 RVA: 0x000298BC File Offset: 0x00027ABC
		internal MemberCollection(AdomdConnection connection, Tuple tuple, string cubeName)
		{
			this.memberCollectionInternal = new AxisTupleMemberCollection(connection, tuple, cubeName);
		}

		// Token: 0x060009BA RID: 2490 RVA: 0x000298D2 File Offset: 0x00027AD2
		internal MemberCollection(AdomdConnection connection, DataTable memberHierarchyDataTable, string cubeName, Level parentLevel, Member parentMember)
		{
			if (memberHierarchyDataTable == null)
			{
				this.memberCollectionInternal = new EmptyMembersCollection();
				return;
			}
			this.memberCollectionInternal = new AxisHierarchyMemberCollection(connection, memberHierarchyDataTable, cubeName, parentLevel, parentMember);
		}

		// Token: 0x17000331 RID: 817
		public Member this[int index]
		{
			get
			{
				return this.memberCollectionInternal[index];
			}
		}

		// Token: 0x17000332 RID: 818
		public Member this[string index]
		{
			get
			{
				Member member = this.Find(index);
				if (null == member)
				{
					throw new ArgumentException(SR.Indexer_ObjectNotFound(index), "index");
				}
				return member;
			}
		}

		// Token: 0x060009BD RID: 2493 RVA: 0x0002993C File Offset: 0x00027B3C
		public Member Find(string index)
		{
			return this.memberCollectionInternal.Find(index);
		}

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x060009BE RID: 2494 RVA: 0x0002994A File Offset: 0x00027B4A
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x060009BF RID: 2495 RVA: 0x0002994D File Offset: 0x00027B4D
		public object SyncRoot
		{
			get
			{
				return this.memberCollectionInternal.SyncRoot;
			}
		}

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x060009C0 RID: 2496 RVA: 0x0002995A File Offset: 0x00027B5A
		public int Count
		{
			get
			{
				return this.memberCollectionInternal.Count;
			}
		}

		// Token: 0x060009C1 RID: 2497 RVA: 0x00029967 File Offset: 0x00027B67
		public void CopyTo(Member[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		// Token: 0x060009C2 RID: 2498 RVA: 0x00029974 File Offset: 0x00027B74
		void ICollection.CopyTo(Array array, int index)
		{
			AdomdUtils.CheckCopyToParameters(array, index, this.Count);
			for (int i = 0; i < this.Count; i++)
			{
				array.SetValue(this[i], index + i);
			}
		}

		// Token: 0x060009C3 RID: 2499 RVA: 0x000299AF File Offset: 0x00027BAF
		public MemberCollection.Enumerator GetEnumerator()
		{
			return new MemberCollection.Enumerator(this);
		}

		// Token: 0x060009C4 RID: 2500 RVA: 0x000299B7 File Offset: 0x00027BB7
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04000669 RID: 1641
		private IMemberCollectionInternal memberCollectionInternal;

		// Token: 0x020001B4 RID: 436
		public struct Enumerator : IEnumerator
		{
			// Token: 0x06001340 RID: 4928 RVA: 0x00044390 File Offset: 0x00042590
			internal Enumerator(MemberCollection members)
			{
				this.members = members;
				this.currentIndex = -1;
			}

			// Token: 0x170006B9 RID: 1721
			// (get) Token: 0x06001341 RID: 4929 RVA: 0x000443A0 File Offset: 0x000425A0
			public Member Current
			{
				get
				{
					Member member;
					try
					{
						member = this.members[this.currentIndex];
					}
					catch (ArgumentException)
					{
						throw new InvalidOperationException();
					}
					return member;
				}
			}

			// Token: 0x170006BA RID: 1722
			// (get) Token: 0x06001342 RID: 4930 RVA: 0x000443DC File Offset: 0x000425DC
			object IEnumerator.Current
			{
				get
				{
					return this.members[this.currentIndex];
				}
			}

			// Token: 0x06001343 RID: 4931 RVA: 0x000443F0 File Offset: 0x000425F0
			public bool MoveNext()
			{
				int num = this.currentIndex + 1;
				this.currentIndex = num;
				return num < this.members.Count;
			}

			// Token: 0x06001344 RID: 4932 RVA: 0x0004441B File Offset: 0x0004261B
			public void Reset()
			{
				this.currentIndex = -1;
			}

			// Token: 0x04000CD6 RID: 3286
			private int currentIndex;

			// Token: 0x04000CD7 RID: 3287
			private MemberCollection members;
		}
	}
}
