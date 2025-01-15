using System;
using System.Data;

namespace Microsoft.HostIntegration.DrdaClient
{
	// Token: 0x020009B0 RID: 2480
	internal class BulkCopyReaderInfo : IBulkCopySourceInfo
	{
		// Token: 0x06004CBA RID: 19642 RVA: 0x001329C5 File Offset: 0x00130BC5
		internal BulkCopyReaderInfo(IDataReader reader)
		{
			this._reader = reader;
		}

		// Token: 0x17001292 RID: 4754
		// (get) Token: 0x06004CBB RID: 19643 RVA: 0x001329D4 File Offset: 0x00130BD4
		public int FieldCount
		{
			get
			{
				return this._reader.FieldCount;
			}
		}

		// Token: 0x06004CBC RID: 19644 RVA: 0x001329E1 File Offset: 0x00130BE1
		public Type GetFieldType(int index)
		{
			return this._reader.GetFieldType(index);
		}

		// Token: 0x06004CBD RID: 19645 RVA: 0x001329EF File Offset: 0x00130BEF
		public string GetFieldName(int index)
		{
			return this._reader.GetName(index);
		}

		// Token: 0x04003CAE RID: 15534
		private IDataReader _reader;
	}
}
