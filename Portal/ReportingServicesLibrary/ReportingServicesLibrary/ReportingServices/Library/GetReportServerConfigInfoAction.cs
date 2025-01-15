using System;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020000BE RID: 190
	internal sealed class GetReportServerConfigInfoAction : RSSoapAction<GetReportServerConfigInfoActionParameters>
	{
		// Token: 0x06000856 RID: 2134 RVA: 0x00021B49 File Offset: 0x0001FD49
		public GetReportServerConfigInfoAction(RSService service)
			: base("GetReportServerConfigInfoAction", service)
		{
		}

		// Token: 0x06000857 RID: 2135 RVA: 0x00021B58 File Offset: 0x0001FD58
		internal override void PerformActionNow()
		{
			if (!RSService.IsRequestFromMemberOfAdministratorsGroup())
			{
				throw new AccessDeniedException(base.Service.UserName, ErrorCode.rsAccessDenied);
			}
			string[] array = null;
			string[] array2 = null;
			string[] array3 = null;
			string[] array4 = null;
			base.Service.GetReportServerConfigInfo(base.ActionParameters.ScaleOut, out array, out array2, out array3, out array4);
			base.ActionParameters.ServerConfigInfo = new ServerConfigInfo[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				base.ActionParameters.ServerConfigInfo[i] = new ServerConfigInfo();
				base.ActionParameters.ServerConfigInfo[i].MachineName = array[i];
				base.ActionParameters.ServerConfigInfo[i].InstanceName = array2[i];
				base.ActionParameters.ServerConfigInfo[i].ServiceAccountName = array3[i];
				base.ActionParameters.ServerConfigInfo[i].ReportServerUrlItem = array4[i];
			}
		}
	}
}
