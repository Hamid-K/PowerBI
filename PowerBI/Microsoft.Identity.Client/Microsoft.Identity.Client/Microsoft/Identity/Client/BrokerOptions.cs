using System;
using System.ComponentModel;
using Microsoft.Identity.Client.PlatformsCommon.Shared;

namespace Microsoft.Identity.Client
{
	// Token: 0x02000127 RID: 295
	public class BrokerOptions
	{
		// Token: 0x06000E86 RID: 3718 RVA: 0x000381E7 File Offset: 0x000363E7
		public BrokerOptions(BrokerOptions.OperatingSystems enabledOn)
		{
			this.EnabledOn = enabledOn;
		}

		// Token: 0x06000E87 RID: 3719 RVA: 0x000381F6 File Offset: 0x000363F6
		internal static BrokerOptions CreateFromWindowsOptions(WindowsBrokerOptions winOptions)
		{
			return new BrokerOptions(BrokerOptions.OperatingSystems.Windows)
			{
				Title = winOptions.HeaderText,
				MsaPassthrough = winOptions.MsaPassthrough,
				ListOperatingSystemAccounts = winOptions.ListWindowsWorkAndSchoolAccounts
			};
		}

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000E88 RID: 3720 RVA: 0x00038222 File Offset: 0x00036422
		public BrokerOptions.OperatingSystems EnabledOn { get; }

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000E89 RID: 3721 RVA: 0x0003822A File Offset: 0x0003642A
		// (set) Token: 0x06000E8A RID: 3722 RVA: 0x00038232 File Offset: 0x00036432
		public string Title { get; set; }

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06000E8B RID: 3723 RVA: 0x0003823B File Offset: 0x0003643B
		// (set) Token: 0x06000E8C RID: 3724 RVA: 0x00038243 File Offset: 0x00036443
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool MsaPassthrough { get; set; }

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06000E8D RID: 3725 RVA: 0x0003824C File Offset: 0x0003644C
		// (set) Token: 0x06000E8E RID: 3726 RVA: 0x00038254 File Offset: 0x00036454
		public bool ListOperatingSystemAccounts { get; set; }

		// Token: 0x06000E8F RID: 3727 RVA: 0x0003825D File Offset: 0x0003645D
		internal bool IsBrokerEnabledOnCurrentOs()
		{
			return this.EnabledOn.HasFlag(BrokerOptions.OperatingSystems.Windows) && DesktopOsHelper.IsWindows();
		}

		// Token: 0x020003BD RID: 957
		[Flags]
		public enum OperatingSystems
		{
			// Token: 0x0400109F RID: 4255
			None = 0,
			// Token: 0x040010A0 RID: 4256
			Windows = 1
		}
	}
}
