using System;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Interfaces;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020001EB RID: 491
	internal sealed class GetRolePropertiesAction : RSSoapAction<GetRolePropertiesActionParameters>
	{
		// Token: 0x060010D1 RID: 4305 RVA: 0x0003A7C7 File Offset: 0x000389C7
		internal GetRolePropertiesAction(RSService service)
			: base("GetRolePropertiesAction", service)
		{
		}

		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x060010D2 RID: 4306 RVA: 0x000053DC File Offset: 0x000035DC
		internal override ConnectionTransactionType TransactionType
		{
			get
			{
				return ConnectionTransactionType.AutoCommit;
			}
		}

		// Token: 0x060010D3 RID: 4307 RVA: 0x0003A7D8 File Offset: 0x000389D8
		internal override void PerformActionNow()
		{
			ExternalItemPath externalItemPath = new ExternalItemPath(base.ActionParameters.SiteName);
			if (!base.Service.SecMgr.CheckAccess(base.Service.SecMgr.SystemSecDesc, CatalogOperation.ReadRoleProperties, externalItemPath))
			{
				throw new AccessDeniedException(base.Service.UserName, ErrorCode.rsAccessDenied);
			}
			string text;
			Microsoft.ReportingServices.Library.Soap.Task[] array;
			base.Service.SecMgr.GetRoleProperties(externalItemPath, base.ActionParameters.RoleName, out text, out array);
			base.ActionParameters.Description = text;
			base.ActionParameters.Tasks = array;
		}
	}
}
