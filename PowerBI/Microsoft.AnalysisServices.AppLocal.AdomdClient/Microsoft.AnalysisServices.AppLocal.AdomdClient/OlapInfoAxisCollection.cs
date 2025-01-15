using System;
using System.Collections;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000E1 RID: 225
	public sealed class OlapInfoAxisCollection : ICollection, IEnumerable
	{
		// Token: 0x06000C78 RID: 3192 RVA: 0x0002EE68 File Offset: 0x0002D068
		internal OlapInfoAxisCollection(MDDatasetFormatter formatter)
		{
			this.formatter = formatter;
			this.axesCach = new OlapInfoAxis[formatter.AxesList.Count];
			for (int i = 0; i < this.axesCach.Length; i++)
			{
				this.axesCach[i] = null;
			}
		}

		// Token: 0x170004B7 RID: 1207
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

		// Token: 0x170004B8 RID: 1208
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

		// Token: 0x06000C7B RID: 3195 RVA: 0x0002EF28 File Offset: 0x0002D128
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

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x06000C7C RID: 3196 RVA: 0x0002EFD7 File Offset: 0x0002D1D7
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x06000C7D RID: 3197 RVA: 0x0002EFDA File Offset: 0x0002D1DA
		public object SyncRoot
		{
			get
			{
				return this.axesCach.SyncRoot;
			}
		}

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x06000C7E RID: 3198 RVA: 0x0002EFE7 File Offset: 0x0002D1E7
		public int Count
		{
			get
			{
				return this.axesCach.Length;
			}
		}

		// Token: 0x06000C7F RID: 3199 RVA: 0x0002EFF1 File Offset: 0x0002D1F1
		public void CopyTo(OlapInfoAxis[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		// Token: 0x06000C80 RID: 3200 RVA: 0x0002EFFC File Offset: 0x0002D1FC
		void ICollection.CopyTo(Array array, int index)
		{
			AdomdUtils.CheckCopyToParameters(array, index, this.Count);
			for (int i = 0; i < this.Count; i++)
			{
				array.SetValue(this[i], index + i);
			}
		}

		// Token: 0x06000C81 RID: 3201 RVA: 0x0002F037 File Offset: 0x0002D237
		public OlapInfoAxisCollection.Enumerator GetEnumerator()
		{
			return new OlapInfoAxisCollection.Enumerator(this);
		}

		// Token: 0x06000C82 RID: 3202 RVA: 0x0002F03F File Offset: 0x0002D23F
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04000806 RID: 2054
		private MDDatasetFormatter formatter;

		// Token: 0x04000807 RID: 2055
		private OlapInfoAxis[] axesCach;

		// Token: 0x04000808 RID: 2056
		private Hashtable namesHash;

		// Token: 0x020001C2 RID: 450
		public struct Enumerator : IEnumerator
		{
			// Token: 0x06001391 RID: 5009 RVA: 0x00044C20 File Offset: 0x00042E20
			internal Enumerator(OlapInfoAxisCollection axes)
			{
				this.axes = axes;
				this.currentIndex = -1;
			}

			// Token: 0x170006D5 RID: 1749
			// (get) Token: 0x06001392 RID: 5010 RVA: 0x00044C30 File Offset: 0x00042E30
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

			// Token: 0x170006D6 RID: 1750
			// (get) Token: 0x06001393 RID: 5011 RVA: 0x00044C6C File Offset: 0x00042E6C
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06001394 RID: 5012 RVA: 0x00044C74 File Offset: 0x00042E74
			public bool MoveNext()
			{
				int num = this.currentIndex + 1;
				this.currentIndex = num;
				return num < this.axes.Count;
			}

			// Token: 0x06001395 RID: 5013 RVA: 0x00044C9F File Offset: 0x00042E9F
			public void Reset()
			{
				this.currentIndex = -1;
			}

			// Token: 0x04000CF2 RID: 3314
			private int currentIndex;

			// Token: 0x04000CF3 RID: 3315
			private OlapInfoAxisCollection axes;
		}
	}
}
