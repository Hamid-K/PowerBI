using System;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Interfaces;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020000BC RID: 188
	internal sealed class GetUserSettingsAction : RSSoapAction<GetUserSettingsActionParameters>
	{
		// Token: 0x0600084D RID: 2125 RVA: 0x00021A51 File Offset: 0x0001FC51
		public GetUserSettingsAction(RSService service)
			: base("GetUserSettingsAction", service)
		{
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x0600084E RID: 2126 RVA: 0x000053DC File Offset: 0x000035DC
		internal override ConnectionTransactionType TransactionType
		{
			get
			{
				return ConnectionTransactionType.AutoCommit;
			}
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x00021A60 File Offset: 0x0001FC60
		internal override void PerformActionNow()
		{
			Property[] requestedProperties = base.ActionParameters.RequestedProperties;
			ExternalItemPath externalItemPath = new ExternalItemPath(base.Service.PropertyProvider.GetSystemUrl());
			if (!base.Service.SecMgr.CheckAccess(base.Service.SecMgr.SystemSecDesc, CatalogOperation.ExecuteReportDefinition, externalItemPath))
			{
				throw new AccessDeniedException(base.Service.UserName, ErrorCode.rsAccessDenied);
			}
			UserProperties userSettings = base.Service.SecMgr.GetUserSettings();
			if (base.Service.SecMgr.GetServiceTokenFromCatalog() != null)
			{
				string latestAADToken = base.Service.SecMgr.GetLatestAADToken();
				if (userSettings == null)
				{
					base.ActionParameters.UserProperties = new UserProperties().AddReservedProperties(latestAADToken);
					return;
				}
				base.ActionParameters.UserProperties = userSettings.AddReservedProperties(latestAADToken);
			}
		}
	}
}
