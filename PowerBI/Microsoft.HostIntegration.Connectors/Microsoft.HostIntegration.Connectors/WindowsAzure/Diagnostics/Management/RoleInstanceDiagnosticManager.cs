using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Microsoft.WindowsAzure.Diagnostics.Management
{
	// Token: 0x02000476 RID: 1142
	[Obsolete("This API is deprecated.")]
	public class RoleInstanceDiagnosticManager
	{
		// Token: 0x170007D6 RID: 2006
		// (get) Token: 0x060027AF RID: 10159 RVA: 0x00077E53 File Offset: 0x00076053
		// (set) Token: 0x060027AE RID: 10158 RVA: 0x00077E4B File Offset: 0x0007604B
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static bool AllowInsecureRemoteConnections { get; set; }

		// Token: 0x170007D7 RID: 2007
		// (get) Token: 0x060027B0 RID: 10160 RVA: 0x00077E5A File Offset: 0x0007605A
		// (set) Token: 0x060027B1 RID: 10161 RVA: 0x00077E62 File Offset: 0x00076062
		public string DeploymentId { get; private set; }

		// Token: 0x170007D8 RID: 2008
		// (get) Token: 0x060027B2 RID: 10162 RVA: 0x00077E6B File Offset: 0x0007606B
		// (set) Token: 0x060027B3 RID: 10163 RVA: 0x00077E73 File Offset: 0x00076073
		public string RoleName { get; private set; }

		// Token: 0x170007D9 RID: 2009
		// (get) Token: 0x060027B4 RID: 10164 RVA: 0x00077E7C File Offset: 0x0007607C
		// (set) Token: 0x060027B5 RID: 10165 RVA: 0x00077E84 File Offset: 0x00076084
		public string RoleInstanceId { get; private set; }

		// Token: 0x060027B6 RID: 10166 RVA: 0x00002061 File Offset: 0x00000261
		public RoleInstanceDiagnosticManager(string connectionString, string deploymentId, string roleName, string roleInstanceId)
		{
		}

		// Token: 0x060027B7 RID: 10167 RVA: 0x00077BAA File Offset: 0x00075DAA
		public DiagnosticMonitorConfiguration GetCurrentConfiguration()
		{
			return new DiagnosticMonitorConfiguration();
		}

		// Token: 0x060027B8 RID: 10168 RVA: 0x000036A9 File Offset: 0x000018A9
		public void SetCurrentConfiguration(DiagnosticMonitorConfiguration newConfiguration)
		{
		}

		// Token: 0x060027B9 RID: 10169 RVA: 0x00077E8D File Offset: 0x0007608D
		public IDictionary<DataBufferName, OnDemandTransferInfo> GetActiveTransfers()
		{
			return new Dictionary<DataBufferName, OnDemandTransferInfo>();
		}

		// Token: 0x060027BA RID: 10170 RVA: 0x00077E94 File Offset: 0x00076094
		public Guid BeginOnDemandTransfer(DataBufferName sourceBufferName)
		{
			return Guid.NewGuid();
		}

		// Token: 0x060027BB RID: 10171 RVA: 0x00077E94 File Offset: 0x00076094
		public Guid BeginOnDemandTransfer(DataBufferName sourceBufferName, OnDemandTransferOptions onDemandTransferOptions)
		{
			return Guid.NewGuid();
		}

		// Token: 0x060027BC RID: 10172 RVA: 0x00077E9C File Offset: 0x0007609C
		public IEnumerable<Guid> CancelOnDemandTransfers(DataBufferName dataBufferName)
		{
			yield break;
		}

		// Token: 0x060027BD RID: 10173 RVA: 0x00002B16 File Offset: 0x00000D16
		public bool EndOnDemandTransfer(Guid requestId)
		{
			return true;
		}
	}
}
