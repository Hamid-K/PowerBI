using System;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Interfaces;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000104 RID: 260
	internal sealed class FireEventAction : RSSoapAction<FireEventActionParameters>
	{
		// Token: 0x06000A73 RID: 2675 RVA: 0x00027AAF File Offset: 0x00025CAF
		internal FireEventAction(RSService service)
			: base("FireEventAction", service)
		{
			Sku.ThrowIfFeatureNotEnabled(Globals.Configuration.InstanceID, RestrictedFeatures.EventGeneration);
		}

		// Token: 0x06000A74 RID: 2676 RVA: 0x00027AD0 File Offset: 0x00025CD0
		protected override void AddActionToBatch()
		{
			base.Service.Storage.AddBatchRecord(base.BatchID, base.Service.UserName, CatalogCommand.FireEvent, base.ActionParameters.EventType, "EventType", null, null, null, null, false, null, base.ActionParameters.EventData);
		}

		// Token: 0x06000A75 RID: 2677 RVA: 0x00027B21 File Offset: 0x00025D21
		internal override void PerformActionInBatch(CallParameters parameters)
		{
			base.ActionParameters.EventType = parameters.Item;
			base.ActionParameters.EventData = parameters.Properties;
			base.ActionParameters.Site = parameters.Item;
			this.PerformActionNow();
		}

		// Token: 0x06000A76 RID: 2678 RVA: 0x00027B5C File Offset: 0x00025D5C
		internal override void PerformActionNow()
		{
			ExternalItemPath externalItemPath = new ExternalItemPath(base.ActionParameters.Site);
			if (!base.Service.SecMgr.CheckAccess(base.Service.SecMgr.SystemSecDesc, CatalogOperation.GenerateEvents, externalItemPath))
			{
				throw new AccessDeniedException(base.Service.UserName, ErrorCode.rsAccessDenied);
			}
			base.Service.EventManager.FireEvent(base.ActionParameters.EventType, base.ActionParameters.EventData);
		}
	}
}
