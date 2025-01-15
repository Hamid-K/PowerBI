using System;
using System.Collections;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000BC RID: 188
	public sealed class MiningModelCollection : ICollection, IEnumerable
	{
		// Token: 0x06000AA3 RID: 2723 RVA: 0x0002BA27 File Offset: 0x00029C27
		internal MiningModelCollection(AdomdConnection connection)
		{
			this.miningModelCollectionInternal = new MiningModelCollectionInternal(connection);
		}

		// Token: 0x06000AA4 RID: 2724 RVA: 0x0002BA3B File Offset: 0x00029C3B
		internal MiningModelCollection(MiningStructure structure)
		{
			this.miningModelCollectionInternal = new MiningModelCollectionInternal(structure);
		}

		// Token: 0x170003AC RID: 940
		public MiningModel this[int index]
		{
			get
			{
				return this.miningModelCollectionInternal[index];
			}
		}

		// Token: 0x170003AD RID: 941
		public MiningModel this[string index]
		{
			get
			{
				return this.miningModelCollectionInternal[index];
			}
		}

		// Token: 0x06000AA7 RID: 2727 RVA: 0x0002BA6B File Offset: 0x00029C6B
		public MiningModel Find(string index)
		{
			return this.miningModelCollectionInternal.Find(index);
		}

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x06000AA8 RID: 2728 RVA: 0x0002BA79 File Offset: 0x00029C79
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x06000AA9 RID: 2729 RVA: 0x0002BA7C File Offset: 0x00029C7C
		public object SyncRoot
		{
			get
			{
				return this.miningModelCollectionInternal.SyncRoot;
			}
		}

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x06000AAA RID: 2730 RVA: 0x0002BA89 File Offset: 0x00029C89
		public int Count
		{
			get
			{
				return this.miningModelCollectionInternal.Count;
			}
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x0002BA96 File Offset: 0x00029C96
		public void CopyTo(MiningModel[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		// Token: 0x06000AAC RID: 2732 RVA: 0x0002BAA0 File Offset: 0x00029CA0
		void ICollection.CopyTo(Array array, int index)
		{
			AdomdUtils.CheckCopyToParameters(array, index, this.Count);
			for (int i = 0; i < this.Count; i++)
			{
				array.SetValue(this[i], index + i);
			}
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x0002BADB File Offset: 0x00029CDB
		public MiningModelCollection.Enumerator GetEnumerator()
		{
			return new MiningModelCollection.Enumerator(this);
		}

		// Token: 0x06000AAE RID: 2734 RVA: 0x0002BAE3 File Offset: 0x00029CE3
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06000AAF RID: 2735 RVA: 0x0002BAF0 File Offset: 0x00029CF0
		internal MiningModelCollectionInternal CollectionInternal
		{
			get
			{
				return this.miningModelCollectionInternal;
			}
		}

		// Token: 0x04000711 RID: 1809
		private MiningModelCollectionInternal miningModelCollectionInternal;

		// Token: 0x020001B9 RID: 441
		public struct Enumerator : IEnumerator
		{
			// Token: 0x0600135B RID: 4955 RVA: 0x00044680 File Offset: 0x00042880
			internal Enumerator(MiningModelCollection miningModels)
			{
				this.miningModels = miningModels.CollectionInternal;
				this.currentIndex = -1;
			}

			// Token: 0x0600135C RID: 4956 RVA: 0x00044695 File Offset: 0x00042895
			internal Enumerator(MiningModelCollectionInternal miningModels)
			{
				this.miningModels = miningModels;
				this.currentIndex = -1;
			}

			// Token: 0x170006C3 RID: 1731
			// (get) Token: 0x0600135D RID: 4957 RVA: 0x000446A8 File Offset: 0x000428A8
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

			// Token: 0x170006C4 RID: 1732
			// (get) Token: 0x0600135E RID: 4958 RVA: 0x000446E4 File Offset: 0x000428E4
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x0600135F RID: 4959 RVA: 0x000446EC File Offset: 0x000428EC
			public bool MoveNext()
			{
				int num = this.currentIndex + 1;
				this.currentIndex = num;
				return num < this.miningModels.Count;
			}

			// Token: 0x06001360 RID: 4960 RVA: 0x00044717 File Offset: 0x00042917
			public void Reset()
			{
				this.currentIndex = -1;
			}

			// Token: 0x04000CE0 RID: 3296
			private MiningModelCollectionInternal miningModels;

			// Token: 0x04000CE1 RID: 3297
			private int currentIndex;
		}
	}
}
