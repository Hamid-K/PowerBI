using System;
using System.ComponentModel;

namespace Microsoft.WindowsAzure.Diagnostics
{
	// Token: 0x0200046F RID: 1135
	[Obsolete("This API is deprecated.")]
	public class DiagnosticMonitor
	{
		// Token: 0x170007C9 RID: 1993
		// (get) Token: 0x0600277A RID: 10106 RVA: 0x00077B9B File Offset: 0x00075D9B
		// (set) Token: 0x0600277B RID: 10107 RVA: 0x00077BA2 File Offset: 0x00075DA2
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static bool AllowInsecureRemoteConnections { get; set; }

		// Token: 0x0600277C RID: 10108 RVA: 0x00077BAA File Offset: 0x00075DAA
		public static DiagnosticMonitorConfiguration GetDefaultInitialConfiguration()
		{
			return new DiagnosticMonitorConfiguration();
		}

		// Token: 0x0600277D RID: 10109 RVA: 0x00077BB1 File Offset: 0x00075DB1
		public static DiagnosticMonitor Start(string diagnosticsStorageAccountConfigurationSettingName)
		{
			return new DiagnosticMonitor();
		}

		// Token: 0x0600277E RID: 10110 RVA: 0x00077BB1 File Offset: 0x00075DB1
		public static DiagnosticMonitor Start(string diagnosticsStorageAccountConfigurationSettingName, DiagnosticMonitorConfiguration initialConfiguration)
		{
			return new DiagnosticMonitor();
		}

		// Token: 0x0600277F RID: 10111 RVA: 0x00077BB1 File Offset: 0x00075DB1
		public static DiagnosticMonitor StartWithConnectionString(string connectionString, DiagnosticMonitorConfiguration initialConfiguration)
		{
			return new DiagnosticMonitor();
		}

		// Token: 0x06002780 RID: 10112 RVA: 0x000036A9 File Offset: 0x000018A9
		public void UpdateStorageAccount(string connectionString)
		{
		}

		// Token: 0x170007CA RID: 1994
		// (get) Token: 0x06002781 RID: 10113 RVA: 0x00077BB8 File Offset: 0x00075DB8
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string LocalDataDirectory
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x06002782 RID: 10114 RVA: 0x000036A9 File Offset: 0x000018A9
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public void Shutdown()
		{
		}
	}
}
