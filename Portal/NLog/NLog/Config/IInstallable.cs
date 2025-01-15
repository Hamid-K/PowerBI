using System;

namespace NLog.Config
{
	// Token: 0x02000186 RID: 390
	public interface IInstallable
	{
		// Token: 0x060011C0 RID: 4544
		void Install(InstallationContext installationContext);

		// Token: 0x060011C1 RID: 4545
		void Uninstall(InstallationContext installationContext);

		// Token: 0x060011C2 RID: 4546
		bool? IsInstalled(InstallationContext installationContext);
	}
}
