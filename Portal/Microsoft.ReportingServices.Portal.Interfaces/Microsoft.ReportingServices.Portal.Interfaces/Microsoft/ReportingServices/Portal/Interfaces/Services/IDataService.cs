using System;
using System.Collections.Generic;
using System.Security.Principal;
using Model;

namespace Microsoft.ReportingServices.Portal.Interfaces.Services
{
	// Token: 0x0200008B RID: 139
	public interface IDataService
	{
		// Token: 0x06000449 RID: 1097
		DataSetSchema GetDataSetSchema(IPrincipal userPrincipal, Guid key);

		// Token: 0x0600044A RID: 1098
		string GetDataSetTableJson(IPrincipal userPrincipal, Guid key, int? maxRows, out bool fromJsonCache);

		// Token: 0x0600044B RID: 1099
		string GetDataSetTableJson(IPrincipal userPrincipal, Guid key, IEnumerable<DataSetParameter> parameterOverrides, int? maxRows, out bool fromJsonCache);

		// Token: 0x0600044C RID: 1100
		byte[] GetCompressedDataSetTableJson(IPrincipal userPrincipal, Guid key, IEnumerable<DataSetParameter> parameterOverrides, int? maxRows, out bool fromJsonCache);

		// Token: 0x0600044D RID: 1101
		string GetDataSetColumnJson(IPrincipal userPrincipal, Guid key, IEnumerable<DataSetParameter> parameterOverrides, string columnName, int sampledRows);

		// Token: 0x0600044E RID: 1102
		string GetDataSetAggregatedValuesJson(IPrincipal userPrincipal, Guid key, IEnumerable<DataSetParameter> parameterOverrides, string columnName, KpiSharedDataItemAggregation aggregation);
	}
}
