using System;
using Microsoft.PowerBI.ReportServer.AsServer.Artifacts;
using Microsoft.ReportingServices.Portal.ODataClient.V2;

namespace Microsoft.PowerBI.ReportServer.WebApi.Pbix
{
	// Token: 0x02000013 RID: 19
	public static class PbixModelRoleExtensions
	{
		// Token: 0x06000043 RID: 67 RVA: 0x00002F24 File Offset: 0x00001124
		public static DataModelRole ToDataModelRole(this PbixModelRole pbixModelRole)
		{
			return new DataModelRole
			{
				Id = pbixModelRole.Id,
				ModelRoleId = pbixModelRole.ModelRoleId,
				ModelRoleName = pbixModelRole.ModelRoleName
			};
		}
	}
}
