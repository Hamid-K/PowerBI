using System;
using System.Collections.Generic;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x0200010C RID: 268
	internal sealed class _SqlMetaDataSetCollection : ICloneable
	{
		// Token: 0x06001597 RID: 5527 RVA: 0x0005E823 File Offset: 0x0005CA23
		internal _SqlMetaDataSetCollection()
		{
			this.altMetaDataSetArray = new List<_SqlMetaDataSet>();
		}

		// Token: 0x06001598 RID: 5528 RVA: 0x0005E838 File Offset: 0x0005CA38
		internal void SetAltMetaData(_SqlMetaDataSet altMetaDataSet)
		{
			int id = (int)altMetaDataSet.id;
			for (int i = 0; i < this.altMetaDataSetArray.Count; i++)
			{
				if ((int)this.altMetaDataSetArray[i].id == id)
				{
					this.altMetaDataSetArray[i] = altMetaDataSet;
					return;
				}
			}
			this.altMetaDataSetArray.Add(altMetaDataSet);
		}

		// Token: 0x06001599 RID: 5529 RVA: 0x0005E890 File Offset: 0x0005CA90
		internal _SqlMetaDataSet GetAltMetaData(int id)
		{
			foreach (_SqlMetaDataSet sqlMetaDataSet in this.altMetaDataSetArray)
			{
				if ((int)sqlMetaDataSet.id == id)
				{
					return sqlMetaDataSet;
				}
			}
			return null;
		}

		// Token: 0x0600159A RID: 5530 RVA: 0x0005E8EC File Offset: 0x0005CAEC
		public object Clone()
		{
			_SqlMetaDataSetCollection sqlMetaDataSetCollection = new _SqlMetaDataSetCollection();
			sqlMetaDataSetCollection.metaDataSet = ((this.metaDataSet == null) ? null : this.metaDataSet.Clone());
			foreach (_SqlMetaDataSet sqlMetaDataSet in this.altMetaDataSetArray)
			{
				sqlMetaDataSetCollection.altMetaDataSetArray.Add(sqlMetaDataSet.Clone());
			}
			return sqlMetaDataSetCollection;
		}

		// Token: 0x0400088D RID: 2189
		private readonly List<_SqlMetaDataSet> altMetaDataSetArray;

		// Token: 0x0400088E RID: 2190
		internal _SqlMetaDataSet metaDataSet;
	}
}
