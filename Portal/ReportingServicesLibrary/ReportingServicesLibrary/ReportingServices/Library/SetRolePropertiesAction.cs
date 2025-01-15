using System;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Interfaces;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020001ED RID: 493
	internal sealed class SetRolePropertiesAction : RSSoapAction<SetRolePropertiesActionParameters>
	{
		// Token: 0x060010DD RID: 4317 RVA: 0x0003A8DD File Offset: 0x00038ADD
		internal SetRolePropertiesAction(RSService service)
			: base("SetRolePropertiesAction", service)
		{
			Sku.ThrowIfFeatureNotEnabled(Globals.Configuration.InstanceID, RestrictedFeatures.CustomRolesSecurity);
		}

		// Token: 0x060010DE RID: 4318 RVA: 0x0003A8FC File Offset: 0x00038AFC
		protected override void AddActionToBatch()
		{
			byte[] array = StringSupport.ToUnicodeArray(base.ActionParameters.Description);
			base.Service.Storage.AddBatchRecord(base.BatchID, base.Service.UserName, CatalogCommand.SetRoleProperties, base.ActionParameters.RoleName, "Name", null, null, null, null, false, array, Microsoft.ReportingServices.Library.Soap.Task.TaskArrayToXml(base.ActionParameters.TaskIDs));
		}

		// Token: 0x060010DF RID: 4319 RVA: 0x0003A964 File Offset: 0x00038B64
		internal override void PerformActionInBatch(CallParameters parameters)
		{
			base.ActionParameters.RoleName = parameters.Item;
			base.ActionParameters.Description = StringSupport.FromUnicodeArray(parameters.Content);
			base.ActionParameters.TaskIDs = Microsoft.ReportingServices.Library.Soap.Task.XmlToTaskArray(parameters.Properties);
			this.PerformActionNow();
		}

		// Token: 0x060010E0 RID: 4320 RVA: 0x0003A9B4 File Offset: 0x00038BB4
		internal override void PerformActionNow()
		{
			if (base.ActionParameters.RoleName.Length == 0 || base.ActionParameters.RoleName.Length > 260)
			{
				throw new InvalidParameterException("Name");
			}
			if (base.ActionParameters.Description != null && base.ActionParameters.Description.Length > 512)
			{
				throw new InvalidParameterException("Description");
			}
			if (!base.Service.SecMgr.CheckAccess(base.Service.SecMgr.SystemSecDesc, CatalogOperation.UpdateRoleProperties, null))
			{
				throw new AccessDeniedException(base.Service.UserName, ErrorCode.rsAccessDenied);
			}
			AuthzData.SecurityScope securityScope;
			string text = AuthzData.TaskList.TaskListToTaskMask(base.ActionParameters.TaskIDs, out securityScope);
			base.Service.SecMgr.SetRoleProperties(base.ActionParameters.RoleName, base.ActionParameters.Description, text, securityScope);
		}
	}
}
