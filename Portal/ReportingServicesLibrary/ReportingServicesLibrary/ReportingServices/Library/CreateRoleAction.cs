using System;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Interfaces;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020001E7 RID: 487
	internal sealed class CreateRoleAction : RSSoapAction<CreateRoleActionParameters>
	{
		// Token: 0x060010B8 RID: 4280 RVA: 0x0003A435 File Offset: 0x00038635
		internal CreateRoleAction(RSService service)
			: base("CreateRoleAction", service)
		{
			Sku.ThrowIfFeatureNotEnabled(Globals.Configuration.InstanceID, RestrictedFeatures.CustomRolesSecurity);
		}

		// Token: 0x060010B9 RID: 4281 RVA: 0x0003A454 File Offset: 0x00038654
		protected override void AddActionToBatch()
		{
			byte[] array = StringSupport.ToUnicodeArray(base.ActionParameters.Description);
			base.Service.Storage.AddBatchRecord(base.BatchID, base.Service.UserName, CatalogCommand.CreateRole, base.ActionParameters.RoleName, "Name", null, null, null, null, false, array, Microsoft.ReportingServices.Library.Soap.Task.TaskArrayToXml(base.ActionParameters.TaskIDs));
		}

		// Token: 0x060010BA RID: 4282 RVA: 0x0003A4BC File Offset: 0x000386BC
		internal override void PerformActionInBatch(CallParameters parameters)
		{
			string text = StringSupport.FromUnicodeArray(parameters.Content);
			base.ActionParameters.RoleName = parameters.Item;
			base.ActionParameters.Description = text;
			base.ActionParameters.TaskIDs = Microsoft.ReportingServices.Library.Soap.Task.XmlToTaskArray(parameters.Properties);
			this.PerformActionNow();
		}

		// Token: 0x060010BB RID: 4283 RVA: 0x0003A510 File Offset: 0x00038710
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
			if (!base.Service.SecMgr.CheckAccess(base.Service.SecMgr.SystemSecDesc, CatalogOperation.CreateRoles, null))
			{
				throw new AccessDeniedException(base.Service.UserName, ErrorCode.rsAccessDenied);
			}
			AuthzData.SecurityScope securityScope;
			string text = AuthzData.TaskList.TaskListToTaskMask(base.ActionParameters.TaskIDs, out securityScope);
			base.Service.SecMgr.CreateRole(base.ActionParameters.RoleName, base.ActionParameters.Description, text, securityScope);
		}
	}
}
