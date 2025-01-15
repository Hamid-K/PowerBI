using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.DataProcessing
{
	// Token: 0x02000031 RID: 49
	internal interface IRestartable
	{
		// Token: 0x060000BD RID: 189
		IDataParameter[] StartAt(List<ScopeValueFieldName> scopeValueFieldNameCollection);
	}
}
