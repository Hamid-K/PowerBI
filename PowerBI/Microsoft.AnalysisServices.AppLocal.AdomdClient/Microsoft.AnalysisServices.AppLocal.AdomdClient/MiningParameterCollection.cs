using System;
using System.Collections;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000C4 RID: 196
	public sealed class MiningParameterCollection : ICollection, IEnumerable
	{
		// Token: 0x06000B08 RID: 2824 RVA: 0x0002C585 File Offset: 0x0002A785
		internal MiningParameterCollection(string parameters)
		{
			this.miningParameterCollectionInternal = new MiningParameterCollectionInternal(parameters);
		}

		// Token: 0x170003E5 RID: 997
		public MiningParameter this[int index]
		{
			get
			{
				return this.miningParameterCollectionInternal[index];
			}
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x0002C5A8 File Offset: 0x0002A7A8
		public MiningParameter Find(string name)
		{
			foreach (MiningParameter miningParameter in this)
			{
				if (string.Compare(miningParameter.Name, name, StringComparison.Ordinal) == 0)
				{
					return miningParameter;
				}
			}
			return null;
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x06000B0B RID: 2827 RVA: 0x0002C5E1 File Offset: 0x0002A7E1
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x06000B0C RID: 2828 RVA: 0x0002C5E4 File Offset: 0x0002A7E4
		public object SyncRoot
		{
			get
			{
				return this.miningParameterCollectionInternal.SyncRoot;
			}
		}

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x06000B0D RID: 2829 RVA: 0x0002C5F1 File Offset: 0x0002A7F1
		public int Count
		{
			get
			{
				return this.miningParameterCollectionInternal.Count;
			}
		}

		// Token: 0x06000B0E RID: 2830 RVA: 0x0002C5FE File Offset: 0x0002A7FE
		public void CopyTo(MiningParameter[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		// Token: 0x06000B0F RID: 2831 RVA: 0x0002C608 File Offset: 0x0002A808
		void ICollection.CopyTo(Array array, int index)
		{
			AdomdUtils.CheckCopyToParameters(array, index, this.Count);
			for (int i = 0; i < this.Count; i++)
			{
				array.SetValue(this[i], index + i);
			}
		}

		// Token: 0x06000B10 RID: 2832 RVA: 0x0002C643 File Offset: 0x0002A843
		public MiningParameterCollection.Enumerator GetEnumerator()
		{
			return new MiningParameterCollection.Enumerator(this);
		}

		// Token: 0x06000B11 RID: 2833 RVA: 0x0002C64B File Offset: 0x0002A84B
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x06000B12 RID: 2834 RVA: 0x0002C658 File Offset: 0x0002A858
		internal MiningParameterCollectionInternal CollectionInternal
		{
			get
			{
				return this.miningParameterCollectionInternal;
			}
		}

		// Token: 0x04000758 RID: 1880
		private MiningParameterCollectionInternal miningParameterCollectionInternal;

		// Token: 0x020001BB RID: 443
		public struct Enumerator : IEnumerator
		{
			// Token: 0x06001367 RID: 4967 RVA: 0x000447C0 File Offset: 0x000429C0
			internal Enumerator(MiningParameterCollection miningParameters)
			{
				this.miningParameters = miningParameters.CollectionInternal;
				this.currentIndex = -1;
			}

			// Token: 0x06001368 RID: 4968 RVA: 0x000447D5 File Offset: 0x000429D5
			internal Enumerator(MiningParameterCollectionInternal miningParameters)
			{
				this.miningParameters = miningParameters;
				this.currentIndex = -1;
			}

			// Token: 0x170006C7 RID: 1735
			// (get) Token: 0x06001369 RID: 4969 RVA: 0x000447E8 File Offset: 0x000429E8
			public MiningParameter Current
			{
				get
				{
					MiningParameter miningParameter;
					try
					{
						miningParameter = this.miningParameters[this.currentIndex];
					}
					catch (ArgumentException)
					{
						throw new InvalidOperationException();
					}
					return miningParameter;
				}
			}

			// Token: 0x170006C8 RID: 1736
			// (get) Token: 0x0600136A RID: 4970 RVA: 0x00044824 File Offset: 0x00042A24
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x0600136B RID: 4971 RVA: 0x0004482C File Offset: 0x00042A2C
			public bool MoveNext()
			{
				int num = this.currentIndex + 1;
				this.currentIndex = num;
				return num < this.miningParameters.Count;
			}

			// Token: 0x0600136C RID: 4972 RVA: 0x00044857 File Offset: 0x00042A57
			public void Reset()
			{
				this.currentIndex = -1;
			}

			// Token: 0x04000CE4 RID: 3300
			private MiningParameterCollectionInternal miningParameters;

			// Token: 0x04000CE5 RID: 3301
			private int currentIndex;
		}
	}
}
