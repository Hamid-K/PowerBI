using System;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x02000074 RID: 116
	[OriginalName("DataModelParameterSingle")]
	public class DataModelParameterSingle : DataServiceQuerySingle<DataModelParameter>
	{
		// Token: 0x06000524 RID: 1316 RVA: 0x0000A95A File Offset: 0x00008B5A
		public DataModelParameterSingle(DataServiceContext context, string path)
			: base(context, path)
		{
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x0000A964 File Offset: 0x00008B64
		public DataModelParameterSingle(DataServiceContext context, string path, bool isComposable)
			: base(context, path, isComposable)
		{
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x0000A96F File Offset: 0x00008B6F
		public DataModelParameterSingle(DataServiceQuerySingle<DataModelParameter> query)
			: base(query)
		{
		}
	}
}
