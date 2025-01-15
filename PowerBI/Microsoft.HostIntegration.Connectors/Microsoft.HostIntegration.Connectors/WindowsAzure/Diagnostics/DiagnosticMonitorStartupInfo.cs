using System;
using System.ComponentModel;

namespace Microsoft.WindowsAzure.Diagnostics
{
	// Token: 0x0200046D RID: 1133
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Obsolete("This API is deprecated.")]
	public class DiagnosticMonitorStartupInfo
	{
		// Token: 0x170007C2 RID: 1986
		// (get) Token: 0x06002769 RID: 10089 RVA: 0x00077B1D File Offset: 0x00075D1D
		// (set) Token: 0x0600276A RID: 10090 RVA: 0x00077B25 File Offset: 0x00075D25
		public string StorageAccountConnectionString { get; set; }

		// Token: 0x170007C3 RID: 1987
		// (get) Token: 0x0600276B RID: 10091 RVA: 0x00077B2E File Offset: 0x00075D2E
		// (set) Token: 0x0600276C RID: 10092 RVA: 0x00077B36 File Offset: 0x00075D36
		public string DeploymentId { get; set; }

		// Token: 0x170007C4 RID: 1988
		// (get) Token: 0x0600276D RID: 10093 RVA: 0x00077B3F File Offset: 0x00075D3F
		// (set) Token: 0x0600276E RID: 10094 RVA: 0x00077B47 File Offset: 0x00075D47
		public string RoleInstanceId { get; set; }

		// Token: 0x170007C5 RID: 1989
		// (get) Token: 0x0600276F RID: 10095 RVA: 0x00077B50 File Offset: 0x00075D50
		// (set) Token: 0x06002770 RID: 10096 RVA: 0x00077B58 File Offset: 0x00075D58
		public string RoleName { get; set; }

		// Token: 0x170007C6 RID: 1990
		// (get) Token: 0x06002771 RID: 10097 RVA: 0x00077B61 File Offset: 0x00075D61
		// (set) Token: 0x06002772 RID: 10098 RVA: 0x00077B69 File Offset: 0x00075D69
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string LocalDataDirectory { get; set; }

		// Token: 0x170007C7 RID: 1991
		// (get) Token: 0x06002773 RID: 10099 RVA: 0x00077B72 File Offset: 0x00075D72
		// (set) Token: 0x06002774 RID: 10100 RVA: 0x00077B7A File Offset: 0x00075D7A
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string DiagnosticMonitorToolsDirectory { get; set; }

		// Token: 0x170007C8 RID: 1992
		// (get) Token: 0x06002775 RID: 10101 RVA: 0x00077B83 File Offset: 0x00075D83
		// (set) Token: 0x06002776 RID: 10102 RVA: 0x00077B8B File Offset: 0x00075D8B
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public TimeSpan MonitorPollInterval { get; set; }
	}
}
