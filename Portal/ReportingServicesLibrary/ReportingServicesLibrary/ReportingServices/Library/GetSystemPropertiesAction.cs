using System;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Interfaces;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020000BA RID: 186
	internal sealed class GetSystemPropertiesAction : RSSoapAction<GetSystemPropertiesActionParameters>
	{
		// Token: 0x06000844 RID: 2116 RVA: 0x0002194B File Offset: 0x0001FB4B
		public GetSystemPropertiesAction(RSService service)
			: base("GetSystemPropertiesAction", service)
		{
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06000845 RID: 2117 RVA: 0x000053DC File Offset: 0x000035DC
		internal override ConnectionTransactionType TransactionType
		{
			get
			{
				return ConnectionTransactionType.AutoCommit;
			}
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x0002195C File Offset: 0x0001FB5C
		internal override void PerformActionNow()
		{
			Property[] requestedProperties = base.ActionParameters.RequestedProperties;
			ExternalItemPath externalItemPath = new ExternalItemPath(base.Service.PropertyProvider.GetSystemUrl());
			if (requestedProperties != null && !SystemProperties.AreAllNonPermissionedProperties(requestedProperties) && !base.Service.SecMgr.CheckAccess(base.Service.SecMgr.SystemSecDesc, CatalogOperation.ReadSystemProperties, externalItemPath))
			{
				throw new AccessDeniedException(base.Service.UserName, ErrorCode.rsAccessDenied);
			}
			SystemProperties allConfigurationInfo = base.Service.Storage.GetAllConfigurationInfo();
			Property[] array;
			if (requestedProperties == null && !base.Service.SecMgr.CheckAccess(base.Service.SecMgr.SystemSecDesc, CatalogOperation.ReadSystemProperties, externalItemPath))
			{
				array = allConfigurationInfo.GetNonPermissionedProperties();
			}
			else
			{
				array = allConfigurationInfo.FilterProperties(requestedProperties);
			}
			if (requestedProperties == null)
			{
				array = SystemProperties.FilterSystemPropertiesOnMode(array, false);
			}
			base.ActionParameters.SystemProperties = array;
		}
	}
}
