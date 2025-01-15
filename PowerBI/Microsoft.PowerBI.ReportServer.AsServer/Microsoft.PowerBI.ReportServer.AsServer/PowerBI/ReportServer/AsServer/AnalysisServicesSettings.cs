using System;
using Microsoft.BIServer.HostingEnvironment;
using Microsoft.BIServer.HostingEnvironment.HostingInfo;

namespace Microsoft.PowerBI.ReportServer.AsServer
{
	// Token: 0x02000018 RID: 24
	public class AnalysisServicesSettings
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000080 RID: 128 RVA: 0x000041B5 File Offset: 0x000023B5
		// (set) Token: 0x06000081 RID: 129 RVA: 0x000041BD File Offset: 0x000023BD
		public TimeSpan ModelCleanupCycle { get; private set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000082 RID: 130 RVA: 0x000041C6 File Offset: 0x000023C6
		// (set) Token: 0x06000083 RID: 131 RVA: 0x000041CE File Offset: 0x000023CE
		public TimeSpan ModelExpiration { get; private set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000084 RID: 132 RVA: 0x000041D7 File Offset: 0x000023D7
		// (set) Token: 0x06000085 RID: 133 RVA: 0x000041DF File Offset: 0x000023DF
		public TimeSpan ScheduleRefreshTimeout { get; private set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000086 RID: 134 RVA: 0x000041E8 File Offset: 0x000023E8
		// (set) Token: 0x06000087 RID: 135 RVA: 0x000041F0 File Offset: 0x000023F0
		public int Port { get; private set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000088 RID: 136 RVA: 0x000041F9 File Offset: 0x000023F9
		// (set) Token: 0x06000089 RID: 137 RVA: 0x00004201 File Offset: 0x00002401
		public string ServerAddress { get; private set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600008A RID: 138 RVA: 0x0000420A File Offset: 0x0000240A
		// (set) Token: 0x0600008B RID: 139 RVA: 0x00004212 File Offset: 0x00002412
		public TimeSpan EvictorInitialDelay { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600008C RID: 140 RVA: 0x0000421B File Offset: 0x0000241B
		// (set) Token: 0x0600008D RID: 141 RVA: 0x00004223 File Offset: 0x00002423
		public bool isWinUser { get; set; }

		// Token: 0x0600008E RID: 142 RVA: 0x0000422C File Offset: 0x0000242C
		public AnalysisServicesSettings(int port, int modelCleanupCycleMinutes, int modelExpirationMinutes, int scheduleRefreshTimeoutMinutes, string serverAddress, TimeSpan evictorInitialDelay, bool WinUser)
			: this(port, modelCleanupCycleMinutes, TimeSpan.FromMinutes((double)modelExpirationMinutes), scheduleRefreshTimeoutMinutes, serverAddress, evictorInitialDelay, WinUser)
		{
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00004248 File Offset: 0x00002448
		internal AnalysisServicesSettings(int port, int modelCleanupCycleMinutes, TimeSpan modelExpiration, int scheduleRefreshTimeoutMinutes, string serverAddress, TimeSpan evictorInitialDelay, bool WinUser)
		{
			this.ModelCleanupCycle = TimeSpan.FromMinutes((double)modelCleanupCycleMinutes);
			this.ModelExpiration = modelExpiration;
			this.ScheduleRefreshTimeout = TimeSpan.FromMinutes((double)scheduleRefreshTimeoutMinutes);
			this.Port = port;
			this.ServerAddress = serverAddress;
			this.EvictorInitialDelay = evictorInitialDelay;
			this.isWinUser = WinUser;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x0000429C File Offset: 0x0000249C
		public AnalysisServicesSettings()
			: this(StaticConfig.Current.GetIntOrException(ConfigSettings.ASPort.ToString()), StaticConfig.Current.GetIntOrException(ConfigSettings.ModelCleanupCycleMinutes.ToString()), StaticConfig.Current.GetIntOrException(ConfigSettings.ModelExpirationMinutes.ToString()), StaticConfig.Current.GetIntOrException(ConfigSettings.ScheduleRefreshTimeoutMinutes.ToString()), "localhost", TimeSpan.FromSeconds((double)StaticConfig.Current.GetIntOrException(ConfigSettings.TimerInitialDelaySeconds.ToString())), false)
		{
		}

		// Token: 0x04000052 RID: 82
		private const string DefaultServerAddress = "localhost";

		// Token: 0x0400005A RID: 90
		private const bool DefaultWinUser = false;
	}
}
