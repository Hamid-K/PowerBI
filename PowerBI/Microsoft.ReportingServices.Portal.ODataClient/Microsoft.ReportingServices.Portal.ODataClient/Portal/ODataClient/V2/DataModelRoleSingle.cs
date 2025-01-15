using System;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x0200003F RID: 63
	[OriginalName("DataModelRoleSingle")]
	public class DataModelRoleSingle : DataServiceQuerySingle<DataModelRole>
	{
		// Token: 0x060002B0 RID: 688 RVA: 0x00006CCC File Offset: 0x00004ECC
		public DataModelRoleSingle(DataServiceContext context, string path)
			: base(context, path)
		{
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x00006CD6 File Offset: 0x00004ED6
		public DataModelRoleSingle(DataServiceContext context, string path, bool isComposable)
			: base(context, path, isComposable)
		{
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x00006CE1 File Offset: 0x00004EE1
		public DataModelRoleSingle(DataServiceQuerySingle<DataModelRole> query)
			: base(query)
		{
		}
	}
}
