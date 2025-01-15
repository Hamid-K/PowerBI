using System;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x02000025 RID: 37
	[OriginalName("DataSetRowSingle")]
	public class DataSetRowSingle : DataServiceQuerySingle<DataSetRow>
	{
		// Token: 0x0600018A RID: 394 RVA: 0x000046C7 File Offset: 0x000028C7
		public DataSetRowSingle(DataServiceContext context, string path)
			: base(context, path)
		{
		}

		// Token: 0x0600018B RID: 395 RVA: 0x000046D1 File Offset: 0x000028D1
		public DataSetRowSingle(DataServiceContext context, string path, bool isComposable)
			: base(context, path, isComposable)
		{
		}

		// Token: 0x0600018C RID: 396 RVA: 0x000046DC File Offset: 0x000028DC
		public DataSetRowSingle(DataServiceQuerySingle<DataSetRow> query)
			: base(query)
		{
		}
	}
}
