using System;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x020000F8 RID: 248
	[OriginalName("DataModelDataSourceSingle")]
	public class DataModelDataSourceSingle : DataServiceQuerySingle<DataModelDataSource>
	{
		// Token: 0x06000AEA RID: 2794 RVA: 0x000159B5 File Offset: 0x00013BB5
		public DataModelDataSourceSingle(DataServiceContext context, string path)
			: base(context, path)
		{
		}

		// Token: 0x06000AEB RID: 2795 RVA: 0x000159BF File Offset: 0x00013BBF
		public DataModelDataSourceSingle(DataServiceContext context, string path, bool isComposable)
			: base(context, path, isComposable)
		{
		}

		// Token: 0x06000AEC RID: 2796 RVA: 0x000159CA File Offset: 0x00013BCA
		public DataModelDataSourceSingle(DataServiceQuerySingle<DataModelDataSource> query)
			: base(query)
		{
		}
	}
}
