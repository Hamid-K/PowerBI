using System;
using System.Collections.Generic;

namespace Microsoft.PowerBI.Analytics.Contracts
{
	// Token: 0x0200000F RID: 15
	public interface IDataRowFactory
	{
		// Token: 0x06000029 RID: 41
		IDataRow CreateDataRow(IReadOnlyList<object> columns);
	}
}
