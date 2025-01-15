using System;
using Microsoft.SqlServer.ReportingServices2010;
using Model;

namespace Microsoft.ReportingServices.Portal.Services.ODataExtensions
{
	// Token: 0x02000041 RID: 65
	internal static class QueryExtensions
	{
		// Token: 0x06000268 RID: 616 RVA: 0x00010784 File Offset: 0x0000E984
		public static QueryDefinition ToSoapQueryDefinition(this Query query)
		{
			return new QueryDefinition
			{
				CommandType = "Text",
				CommandText = query.CommandText,
				Timeout = (query.Timeout ?? 0),
				TimeoutSpecified = (query.Timeout != null)
			};
		}

		// Token: 0x06000269 RID: 617 RVA: 0x000107E1 File Offset: 0x0000E9E1
		public static DataSetDefinition ToSoapDataSetDefinition(this Query query)
		{
			return new DataSetDefinition
			{
				Query = query.ToSoapQueryDefinition()
			};
		}

		// Token: 0x0600026A RID: 618 RVA: 0x000107F4 File Offset: 0x0000E9F4
		public static Query ToQueryDefinition(this DataSetDefinition datasetDefinition)
		{
			return new Query
			{
				CommandText = datasetDefinition.Query.CommandText,
				Timeout = (datasetDefinition.Query.TimeoutSpecified ? new int?(datasetDefinition.Query.Timeout) : null)
			};
		}
	}
}
