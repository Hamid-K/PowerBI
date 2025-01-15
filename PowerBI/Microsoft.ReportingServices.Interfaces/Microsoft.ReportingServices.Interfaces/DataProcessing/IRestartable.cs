using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.DataProcessing
{
	// Token: 0x0200001F RID: 31
	internal interface IRestartable
	{
		// Token: 0x06000047 RID: 71
		IDataParameter[] StartAt(List<ScopeValueFieldName> scopeValueFieldNameCollection);
	}
}
