using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x020003C8 RID: 968
	public sealed class DataSets : List<DataSet>
	{
		// Token: 0x06001F38 RID: 7992 RVA: 0x0007E27C File Offset: 0x0007C47C
		internal DataSet Find(string dataSetName)
		{
			foreach (DataSet dataSet in this)
			{
				if (string.Compare(dataSet.Name, dataSetName, StringComparison.OrdinalIgnoreCase) == 0)
				{
					return dataSet;
				}
			}
			return null;
		}
	}
}
