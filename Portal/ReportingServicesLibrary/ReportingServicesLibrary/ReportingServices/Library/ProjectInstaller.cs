using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration.Install;
using System.Security.Permissions;
using Microsoft.ReportingServices.Common;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200005C RID: 92
	[RunInstaller(true)]
	[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
	public sealed class ProjectInstaller : Installer
	{
		// Token: 0x060003F7 RID: 1015 RVA: 0x000112EC File Offset: 0x0000F4EC
		public override void Install(IDictionary stateSaver)
		{
			base.Install(stateSaver);
			Microsoft.ReportingServices.Common.RSPerfCounterInstallUtil.InstallWebServicePerfCounters(false, false);
			Microsoft.ReportingServices.Common.RSPerfCounterInstallUtil.InstallWindowsServicePerfCounters(false, false);
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x00011303 File Offset: 0x0000F503
		public override void Uninstall(IDictionary savedState)
		{
			base.Uninstall(savedState);
			Microsoft.ReportingServices.Common.RSPerfCounterInstallUtil.Uninstall(false);
		}
	}
}
