using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x020003C5 RID: 965
	public sealed class DataSources : List<DataSource>
	{
		// Token: 0x06001F28 RID: 7976 RVA: 0x0007E120 File Offset: 0x0007C320
		internal DataSource Find(string dataSourceName)
		{
			foreach (DataSource dataSource in this)
			{
				if (string.Compare(dataSource.Name, dataSourceName, StringComparison.OrdinalIgnoreCase) == 0)
				{
					return dataSource;
				}
			}
			return null;
		}
	}
}
