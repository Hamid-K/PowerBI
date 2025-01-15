using System;
using System.Collections;
using System.Data;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000A7 RID: 167
	public sealed class MemberCollection : ICollection, IEnumerable
	{
		// Token: 0x060009AC RID: 2476 RVA: 0x0002958C File Offset: 0x0002778C
		internal MemberCollection(AdomdConnection connection, Tuple tuple, string cubeName)
		{
			this.memberCollectionInternal = new AxisTupleMemberCollection(connection, tuple, cubeName);
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x000295A2 File Offset: 0x000277A2
		internal MemberCollection(AdomdConnection connection, DataTable memberHierarchyDataTable, string cubeName, Level parentLevel, Member parentMember)
		{
			if (memberHierarchyDataTable == null)
			{
				this.memberCollectionInternal = new EmptyMembersCollection();
				return;
			}
			this.memberCollectionInternal = new AxisHierarchyMemberCollection(connection, memberHierarchyDataTable, cubeName, parentLevel, parentMember);
		}

		// Token: 0x1700032B RID: 811
		public Member this[int index]
		{
			get
			{
				return this.memberCollectionInternal[index];
			}
		}

		// Token: 0x1700032C RID: 812
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

		// Token: 0x060009B0 RID: 2480 RVA: 0x0002960C File Offset: 0x0002780C
		public Member Find(string index)
		{
			return this.memberCollectionInternal.Find(index);
		}

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x060009B1 RID: 2481 RVA: 0x0002961A File Offset: 0x0002781A
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x060009B2 RID: 2482 RVA: 0x0002961D File Offset: 0x0002781D
		public object SyncRoot
		{
			get
			{
				return this.memberCollectionInternal.SyncRoot;
			}
		}

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x060009B3 RID: 2483 RVA: 0x0002962A File Offset: 0x0002782A
		public int Count
		{
			get
			{
				return this.memberCollectionInternal.Count;
			}
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x00029637 File Offset: 0x00027837
		public void CopyTo(Member[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		// Token: 0x060009B5 RID: 2485 RVA: 0x00029644 File Offset: 0x00027844
		void ICollection.CopyTo(Array array, int index)
		{
			AdomdUtils.CheckCopyToParameters(array, index, this.Count);
			for (int i = 0; i < this.Count; i++)
			{
				array.SetValue(this[i], index + i);
			}
		}

		// Token: 0x060009B6 RID: 2486 RVA: 0x0002967F File Offset: 0x0002787F
		public MemberCollection.Enumerator GetEnumerator()
		{
			return new MemberCollection.Enumerator(this);
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x00029687 File Offset: 0x00027887
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0400065C RID: 1628
		private IMemberCollectionInternal memberCollectionInternal;

		// Token: 0x020001B4 RID: 436
		public struct Enumerator : IEnumerator
		{
			// Token: 0x06001333 RID: 4915 RVA: 0x00043E54 File Offset: 0x00042054
			internal Enumerator(MemberCollection members)
			{
				this.members = members;
				this.currentIndex = -1;
			}

			// Token: 0x170006B3 RID: 1715
			// (get) Token: 0x06001334 RID: 4916 RVA: 0x00043E64 File Offset: 0x00042064
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

			// Token: 0x170006B4 RID: 1716
			// (get) Token: 0x06001335 RID: 4917 RVA: 0x00043EA0 File Offset: 0x000420A0
			object IEnumerator.Current
			{
				get
				{
					return this.members[this.currentIndex];
				}
			}

			// Token: 0x06001336 RID: 4918 RVA: 0x00043EB4 File Offset: 0x000420B4
			public bool MoveNext()
			{
				int num = this.currentIndex + 1;
				this.currentIndex = num;
				return num < this.members.Count;
			}

			// Token: 0x06001337 RID: 4919 RVA: 0x00043EDF File Offset: 0x000420DF
			public void Reset()
			{
				this.currentIndex = -1;
			}

			// Token: 0x04000CC5 RID: 3269
			private int currentIndex;

			// Token: 0x04000CC6 RID: 3270
			private MemberCollection members;
		}
	}
}
