using System;
using System.ComponentModel;

namespace Microsoft.WindowsAzure.Diagnostics
{
	// Token: 0x0200046C RID: 1132
	[Obsolete("This API is deprecated.")]
	public class DiagnosticMonitorConfiguration
	{
		// Token: 0x170007BB RID: 1979
		// (get) Token: 0x0600275A RID: 10074 RVA: 0x00077AA6 File Offset: 0x00075CA6
		// (set) Token: 0x0600275B RID: 10075 RVA: 0x00077AAE File Offset: 0x00075CAE
		public int OverallQuotaInMB { get; set; }

		// Token: 0x170007BC RID: 1980
		// (get) Token: 0x0600275C RID: 10076 RVA: 0x00077AB7 File Offset: 0x00075CB7
		// (set) Token: 0x0600275D RID: 10077 RVA: 0x00077ABF File Offset: 0x00075CBF
		public BasicLogsBufferConfiguration Logs { get; set; }

		// Token: 0x170007BD RID: 1981
		// (get) Token: 0x0600275E RID: 10078 RVA: 0x00077AC8 File Offset: 0x00075CC8
		// (set) Token: 0x0600275F RID: 10079 RVA: 0x00077AD0 File Offset: 0x00075CD0
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public BasicLogsBufferConfiguration DiagnosticInfrastructureLogs { get; set; }

		// Token: 0x170007BE RID: 1982
		// (get) Token: 0x06002760 RID: 10080 RVA: 0x00077AD9 File Offset: 0x00075CD9
		// (set) Token: 0x06002761 RID: 10081 RVA: 0x00077AE1 File Offset: 0x00075CE1
		public PerformanceCountersBufferConfiguration PerformanceCounters { get; set; }

		// Token: 0x170007BF RID: 1983
		// (get) Token: 0x06002762 RID: 10082 RVA: 0x00077AEA File Offset: 0x00075CEA
		// (set) Token: 0x06002763 RID: 10083 RVA: 0x00077AF2 File Offset: 0x00075CF2
		public WindowsEventLogsBufferConfiguration WindowsEventLog { get; set; }

		// Token: 0x170007C0 RID: 1984
		// (get) Token: 0x06002764 RID: 10084 RVA: 0x00077AFB File Offset: 0x00075CFB
		// (set) Token: 0x06002765 RID: 10085 RVA: 0x00077B03 File Offset: 0x00075D03
		public DirectoriesBufferConfiguration Directories { get; set; }

		// Token: 0x170007C1 RID: 1985
		// (get) Token: 0x06002766 RID: 10086 RVA: 0x00077B0C File Offset: 0x00075D0C
		// (set) Token: 0x06002767 RID: 10087 RVA: 0x00077B14 File Offset: 0x00075D14
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public TimeSpan ConfigurationChangePollInterval { get; set; }
	}
}
