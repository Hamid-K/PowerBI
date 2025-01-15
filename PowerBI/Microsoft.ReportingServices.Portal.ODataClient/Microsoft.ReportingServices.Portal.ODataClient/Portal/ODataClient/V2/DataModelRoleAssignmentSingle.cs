using System;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x02000041 RID: 65
	[OriginalName("DataModelRoleAssignmentSingle")]
	public class DataModelRoleAssignmentSingle : DataServiceQuerySingle<DataModelRoleAssignment>
	{
		// Token: 0x060002BE RID: 702 RVA: 0x00006DDD File Offset: 0x00004FDD
		public DataModelRoleAssignmentSingle(DataServiceContext context, string path)
			: base(context, path)
		{
		}

		// Token: 0x060002BF RID: 703 RVA: 0x00006DE7 File Offset: 0x00004FE7
		public DataModelRoleAssignmentSingle(DataServiceContext context, string path, bool isComposable)
			: base(context, path, isComposable)
		{
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x00006DF2 File Offset: 0x00004FF2
		public DataModelRoleAssignmentSingle(DataServiceQuerySingle<DataModelRoleAssignment> query)
			: base(query)
		{
		}
	}
}
