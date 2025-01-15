using System;
using System.Collections;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000BC RID: 188
	public sealed class MiningModelCollection : ICollection, IEnumerable
	{
		// Token: 0x06000A96 RID: 2710 RVA: 0x0002B6F7 File Offset: 0x000298F7
		internal MiningModelCollection(AdomdConnection connection)
		{
			this.miningModelCollectionInternal = new MiningModelCollectionInternal(connection);
		}

		// Token: 0x06000A97 RID: 2711 RVA: 0x0002B70B File Offset: 0x0002990B
		internal MiningModelCollection(MiningStructure structure)
		{
			this.miningModelCollectionInternal = new MiningModelCollectionInternal(structure);
		}

		// Token: 0x170003A6 RID: 934
		public MiningModel this[int index]
		{
			get
			{
				return this.miningModelCollectionInternal[index];
			}
		}

		// Token: 0x170003A7 RID: 935
		public MiningModel this[string index]
		{
			get
			{
				return this.miningModelCollectionInternal[index];
			}
		}

		// Token: 0x06000A9A RID: 2714 RVA: 0x0002B73B File Offset: 0x0002993B
		public MiningModel Find(string index)
		{
			return this.miningModelCollectionInternal.Find(index);
		}

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06000A9B RID: 2715 RVA: 0x0002B749 File Offset: 0x00029949
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x06000A9C RID: 2716 RVA: 0x0002B74C File Offset: 0x0002994C
		public object SyncRoot
		{
			get
			{
				return this.miningModelCollectionInternal.SyncRoot;
			}
		}

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x06000A9D RID: 2717 RVA: 0x0002B759 File Offset: 0x00029959
		public int Count
		{
			get
			{
				return this.miningModelCollectionInternal.Count;
			}
		}

		// Token: 0x06000A9E RID: 2718 RVA: 0x0002B766 File Offset: 0x00029966
		public void CopyTo(MiningModel[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		// Token: 0x06000A9F RID: 2719 RVA: 0x0002B770 File Offset: 0x00029970
		void ICollection.CopyTo(Array array, int index)
		{
			AdomdUtils.CheckCopyToParameters(array, index, this.Count);
			for (int i = 0; i < this.Count; i++)
			{
				array.SetValue(this[i], index + i);
			}
		}

		// Token: 0x06000AA0 RID: 2720 RVA: 0x0002B7AB File Offset: 0x000299AB
		public MiningModelCollection.Enumerator GetEnumerator()
		{
			return new MiningModelCollection.Enumerator(this);
		}

		// Token: 0x06000AA1 RID: 2721 RVA: 0x0002B7B3 File Offset: 0x000299B3
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06000AA2 RID: 2722 RVA: 0x0002B7C0 File Offset: 0x000299C0
		internal MiningModelCollectionInternal CollectionInternal
		{
			get
			{
				return this.miningModelCollectionInternal;
			}
		}

		// Token: 0x04000704 RID: 1796
		private MiningModelCollectionInternal miningModelCollectionInternal;

		// Token: 0x020001B9 RID: 441
		public struct Enumerator : IEnumerator
		{
			// Token: 0x0600134E RID: 4942 RVA: 0x00044144 File Offset: 0x00042344
			internal Enumerator(MiningModelCollection miningModels)
			{
				this.miningModels = miningModels.CollectionInternal;
				this.currentIndex = -1;
			}

			// Token: 0x0600134F RID: 4943 RVA: 0x00044159 File Offset: 0x00042359
			internal Enumerator(MiningModelCollectionInternal miningModels)
			{
				this.miningModels = miningModels;
				this.currentIndex = -1;
			}

			// Token: 0x170006BD RID: 1725
			// (get) Token: 0x06001350 RID: 4944 RVA: 0x0004416C File Offset: 0x0004236C
			public MiningModel Current
			{
				get
				{
					MiningModel miningModel;
					try
					{
						miningModel = this.miningModels[this.currentIndex];
					}
					catch (ArgumentException)
					{
						throw new InvalidOperationException();
					}
					return miningModel;
				}
			}

			// Token: 0x170006BE RID: 1726
			// (get) Token: 0x06001351 RID: 4945 RVA: 0x000441A8 File Offset: 0x000423A8
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06001352 RID: 4946 RVA: 0x000441B0 File Offset: 0x000423B0
			public bool MoveNext()
			{
				int num = this.currentIndex + 1;
				this.currentIndex = num;
				return num < this.miningModels.Count;
			}

			// Token: 0x06001353 RID: 4947 RVA: 0x000441DB File Offset: 0x000423DB
			public void Reset()
			{
				this.currentIndex = -1;
			}

			// Token: 0x04000CCF RID: 3279
			private MiningModelCollectionInternal miningModels;

			// Token: 0x04000CD0 RID: 3280
			private int currentIndex;
		}
	}
}
