using System;
using System.Collections;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000E1 RID: 225
	public sealed class OlapInfoAxisCollection : ICollection, IEnumerable
	{
		// Token: 0x06000C6B RID: 3179 RVA: 0x0002EB38 File Offset: 0x0002CD38
		internal OlapInfoAxisCollection(MDDatasetFormatter formatter)
		{
			this.formatter = formatter;
			this.axesCach = new OlapInfoAxis[formatter.AxesList.Count];
			for (int i = 0; i < this.axesCach.Length; i++)
			{
				this.axesCach[i] = null;
			}
		}

		// Token: 0x170004B1 RID: 1201
		public OlapInfoAxis this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				if (this.axesCach[index] == null)
				{
					this.axesCach[index] = new OlapInfoAxis(this.formatter.AxesList[index]);
				}
				return this.axesCach[index];
			}
		}

		// Token: 0x170004B2 RID: 1202
		public OlapInfoAxis this[string name]
		{
			get
			{
				OlapInfoAxis olapInfoAxis = this.Find(name);
				if (olapInfoAxis == null)
				{
					throw new ArgumentException(SR.ICollection_ItemWithThisNameDoesNotExistInTheCollection, "name");
				}
				return olapInfoAxis;
			}
		}

		// Token: 0x06000C6E RID: 3182 RVA: 0x0002EBF8 File Offset: 0x0002CDF8
		public OlapInfoAxis Find(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (this.namesHash == null)
			{
				this.namesHash = new Hashtable(this.Count);
				for (int i = 0; i < this.formatter.AxesList.Count; i++)
				{
					string dataSetName = this.formatter.AxesList[i].DataSetName;
					if (this.namesHash[dataSetName] == null)
					{
						this.namesHash[dataSetName] = i;
					}
				}
			}
			if (!this.namesHash.ContainsKey(name))
			{
				return null;
			}
			int num = (int)this.namesHash[name];
			return this[num];
		}

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x06000C6F RID: 3183 RVA: 0x0002ECA7 File Offset: 0x0002CEA7
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x06000C70 RID: 3184 RVA: 0x0002ECAA File Offset: 0x0002CEAA
		public object SyncRoot
		{
			get
			{
				return this.axesCach.SyncRoot;
			}
		}

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x06000C71 RID: 3185 RVA: 0x0002ECB7 File Offset: 0x0002CEB7
		public int Count
		{
			get
			{
				return this.axesCach.Length;
			}
		}

		// Token: 0x06000C72 RID: 3186 RVA: 0x0002ECC1 File Offset: 0x0002CEC1
		public void CopyTo(OlapInfoAxis[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		// Token: 0x06000C73 RID: 3187 RVA: 0x0002ECCC File Offset: 0x0002CECC
		void ICollection.CopyTo(Array array, int index)
		{
			AdomdUtils.CheckCopyToParameters(array, index, this.Count);
			for (int i = 0; i < this.Count; i++)
			{
				array.SetValue(this[i], index + i);
			}
		}

		// Token: 0x06000C74 RID: 3188 RVA: 0x0002ED07 File Offset: 0x0002CF07
		public OlapInfoAxisCollection.Enumerator GetEnumerator()
		{
			return new OlapInfoAxisCollection.Enumerator(this);
		}

		// Token: 0x06000C75 RID: 3189 RVA: 0x0002ED0F File Offset: 0x0002CF0F
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x040007F9 RID: 2041
		private MDDatasetFormatter formatter;

		// Token: 0x040007FA RID: 2042
		private OlapInfoAxis[] axesCach;

		// Token: 0x040007FB RID: 2043
		private Hashtable namesHash;

		// Token: 0x020001C2 RID: 450
		public struct Enumerator : IEnumerator
		{
			// Token: 0x06001384 RID: 4996 RVA: 0x000446E4 File Offset: 0x000428E4
			internal Enumerator(OlapInfoAxisCollection axes)
			{
				this.axes = axes;
				this.currentIndex = -1;
			}

			// Token: 0x170006CF RID: 1743
			// (get) Token: 0x06001385 RID: 4997 RVA: 0x000446F4 File Offset: 0x000428F4
			public OlapInfoAxis Current
			{
				get
				{
					OlapInfoAxis olapInfoAxis;
					try
					{
						olapInfoAxis = this.axes[this.currentIndex];
					}
					catch (ArgumentException)
					{
						throw new InvalidOperationException();
					}
					return olapInfoAxis;
				}
			}

			// Token: 0x170006D0 RID: 1744
			// (get) Token: 0x06001386 RID: 4998 RVA: 0x00044730 File Offset: 0x00042930
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06001387 RID: 4999 RVA: 0x00044738 File Offset: 0x00042938
			public bool MoveNext()
			{
				int num = this.currentIndex + 1;
				this.currentIndex = num;
				return num < this.axes.Count;
			}

			// Token: 0x06001388 RID: 5000 RVA: 0x00044763 File Offset: 0x00042963
			public void Reset()
			{
				this.currentIndex = -1;
			}

			// Token: 0x04000CE1 RID: 3297
			private int currentIndex;

			// Token: 0x04000CE2 RID: 3298
			private OlapInfoAxisCollection axes;
		}
	}
}
