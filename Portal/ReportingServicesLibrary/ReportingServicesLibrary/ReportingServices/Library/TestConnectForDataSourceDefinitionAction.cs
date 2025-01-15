using System;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Library.Soap2005;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020000F2 RID: 242
	internal sealed class TestConnectForDataSourceDefinitionAction : TestConnectAction<RSTestConnectForDSDefinitionActionParameters>
	{
		// Token: 0x06000A18 RID: 2584 RVA: 0x00026F26 File Offset: 0x00025126
		public TestConnectForDataSourceDefinitionAction(RSService service)
			: base("TestConnectForDataSourceDefinitionAction", service)
		{
		}

		// Token: 0x06000A19 RID: 2585 RVA: 0x00026F34 File Offset: 0x00025134
		protected override void InitDataSourceInfo()
		{
			ConnectionManager.Init();
			if (!WebRequestUtil.IsViaPortal())
			{
				Security.SafeCheckExecuteReportDefinitionPermission(base.Service, null, false);
			}
			base.ActionParameters.DSInfo = DataSourceDefinitionOrReference.ThisToDataSourceInfo(base.ActionParameters.DataSourceDefinition, null);
		}
	}
}
