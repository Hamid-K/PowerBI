using System;
using System.Data;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x0200010B RID: 267
	internal sealed class _SqlMetaDataSet
	{
		// Token: 0x0600158E RID: 5518 RVA: 0x0005E654 File Offset: 0x0005C854
		internal _SqlMetaDataSet(int count, SqlTceCipherInfoTable cipherTable)
		{
			this._hiddenColumnCount = -1;
			this.cekTable = cipherTable;
			this._metaDataArray = new _SqlMetaData[count];
			for (int i = 0; i < this._metaDataArray.Length; i++)
			{
				this._metaDataArray[i] = new _SqlMetaData(i);
			}
		}

		// Token: 0x0600158F RID: 5519 RVA: 0x0005E6A4 File Offset: 0x0005C8A4
		private _SqlMetaDataSet(_SqlMetaDataSet original)
		{
			this.id = original.id;
			this._hiddenColumnCount = original._hiddenColumnCount;
			this._visibleColumnMap = original._visibleColumnMap;
			this._schemaTable = original._schemaTable;
			if (original._metaDataArray == null)
			{
				this._metaDataArray = null;
				return;
			}
			this._metaDataArray = new _SqlMetaData[original._metaDataArray.Length];
			for (int i = 0; i < this._metaDataArray.Length; i++)
			{
				this._metaDataArray[i] = (_SqlMetaData)original._metaDataArray[i].Clone();
			}
		}

		// Token: 0x170008FD RID: 2301
		// (get) Token: 0x06001590 RID: 5520 RVA: 0x0005E737 File Offset: 0x0005C937
		internal int VisibleColumnCount
		{
			get
			{
				if (this._hiddenColumnCount == -1)
				{
					this.SetupHiddenColumns();
				}
				return this.Length - this._hiddenColumnCount;
			}
		}

		// Token: 0x170008FE RID: 2302
		// (get) Token: 0x06001591 RID: 5521 RVA: 0x0005E755 File Offset: 0x0005C955
		internal int Length
		{
			get
			{
				return this._metaDataArray.Length;
			}
		}

		// Token: 0x170008FF RID: 2303
		internal _SqlMetaData this[int index]
		{
			get
			{
				return this._metaDataArray[index];
			}
			set
			{
				this._metaDataArray[index] = value;
			}
		}

		// Token: 0x06001594 RID: 5524 RVA: 0x0005E774 File Offset: 0x0005C974
		public int GetVisibleColumnIndex(int index)
		{
			if (this._hiddenColumnCount == -1)
			{
				this.SetupHiddenColumns();
			}
			if (this._visibleColumnMap == null)
			{
				return index;
			}
			return this._visibleColumnMap[index];
		}

		// Token: 0x06001595 RID: 5525 RVA: 0x0005E797 File Offset: 0x0005C997
		public _SqlMetaDataSet Clone()
		{
			return new _SqlMetaDataSet(this);
		}

		// Token: 0x06001596 RID: 5526 RVA: 0x0005E7A0 File Offset: 0x0005C9A0
		private void SetupHiddenColumns()
		{
			int num = 0;
			for (int i = 0; i < this.Length; i++)
			{
				if (this._metaDataArray[i].IsHidden)
				{
					num++;
				}
			}
			if (num > 0)
			{
				int[] array = new int[this.Length - num];
				int num2 = 0;
				for (int j = 0; j < this.Length; j++)
				{
					if (!this._metaDataArray[j].IsHidden)
					{
						array[num2] = j;
						num2++;
					}
				}
				this._visibleColumnMap = array;
			}
			this._hiddenColumnCount = num;
		}

		// Token: 0x04000887 RID: 2183
		internal ushort id;

		// Token: 0x04000888 RID: 2184
		internal DataTable _schemaTable;

		// Token: 0x04000889 RID: 2185
		internal readonly SqlTceCipherInfoTable cekTable;

		// Token: 0x0400088A RID: 2186
		internal readonly _SqlMetaData[] _metaDataArray;

		// Token: 0x0400088B RID: 2187
		private int _hiddenColumnCount;

		// Token: 0x0400088C RID: 2188
		private int[] _visibleColumnMap;
	}
}
