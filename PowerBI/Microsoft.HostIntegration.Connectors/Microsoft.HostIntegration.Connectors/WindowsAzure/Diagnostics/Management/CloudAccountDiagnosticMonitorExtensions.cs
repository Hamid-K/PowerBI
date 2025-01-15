using System;
using System.ComponentModel;

namespace Microsoft.WindowsAzure.Diagnostics.Management
{
	// Token: 0x02000462 RID: 1122
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Obsolete("This API is deprecated.")]
	public static class CloudAccountDiagnosticMonitorExtensions
	{
		// Token: 0x0600273C RID: 10044 RVA: 0x000779DC File Offset: 0x00075BDC
		public static DeploymentDiagnosticManager CreateDeploymentDiagnosticManager(string connectionString, string deploymentId)
		{
			return new DeploymentDiagnosticManager(connectionString, deploymentId);
		}

		// Token: 0x0600273D RID: 10045 RVA: 0x000779E5 File Offset: 0x00075BE5
		public static RoleInstanceDiagnosticManager CreateRoleInstanceDiagnosticManager(string connectionString, string deploymentId, string roleName, string roleInstanceId)
		{
			return new RoleInstanceDiagnosticManager(connectionString, deploymentId, roleName, roleInstanceId);
		}
	}
}
