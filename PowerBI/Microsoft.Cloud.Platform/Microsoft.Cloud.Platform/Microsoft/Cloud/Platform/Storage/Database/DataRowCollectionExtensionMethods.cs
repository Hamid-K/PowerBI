using System;
using System.Collections.Generic;
using System.Data;

namespace Microsoft.Cloud.Platform.Storage.Database
{
	// Token: 0x02000054 RID: 84
	public static class DataRowCollectionExtensionMethods
	{
		// Token: 0x060001F8 RID: 504 RVA: 0x000068B0 File Offset: 0x00004AB0
		public static void AddRows(this DataRowCollection rowsCollection, IEnumerable<DataRow> rows)
		{
			foreach (DataRow dataRow in rows)
			{
				rowsCollection.Add(dataRow);
			}
		}
	}
}
