using System;
using System.Collections.Generic;
using Microsoft.PowerBI.Analytics.Contracts;

namespace Microsoft.DataShaping.Processing.Analytics
{
	// Token: 0x020000B1 RID: 177
	internal sealed class DataRowFactory : IDataRowFactory
	{
		// Token: 0x0600048F RID: 1167 RVA: 0x0000DEB0 File Offset: 0x0000C0B0
		private DataRowFactory()
		{
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x0000DEB8 File Offset: 0x0000C0B8
		public IDataRow CreateDataRow(IReadOnlyList<object> columns)
		{
			return new DataRow(columns);
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000491 RID: 1169 RVA: 0x0000DEC0 File Offset: 0x0000C0C0
		internal static DataRowFactory Instance
		{
			get
			{
				return DataRowFactory.PrivateInstance;
			}
		}

		// Token: 0x04000255 RID: 597
		private static readonly DataRowFactory PrivateInstance = new DataRowFactory();
	}
}
