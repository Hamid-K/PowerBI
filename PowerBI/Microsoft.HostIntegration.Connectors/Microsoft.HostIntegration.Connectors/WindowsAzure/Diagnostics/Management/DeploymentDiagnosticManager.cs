using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Microsoft.WindowsAzure.Diagnostics.Management
{
	// Token: 0x02000471 RID: 1137
	[Obsolete("This API is deprecated.")]
	public class DeploymentDiagnosticManager
	{
		// Token: 0x170007CB RID: 1995
		// (get) Token: 0x06002787 RID: 10119 RVA: 0x00077BC7 File Offset: 0x00075DC7
		// (set) Token: 0x06002786 RID: 10118 RVA: 0x00077BBF File Offset: 0x00075DBF
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static bool AllowInsecureRemoteConnections { get; set; }

		// Token: 0x06002788 RID: 10120 RVA: 0x00002061 File Offset: 0x00000261
		public DeploymentDiagnosticManager(string connectionString, string deploymentId)
		{
		}

		// Token: 0x06002789 RID: 10121 RVA: 0x00077BD0 File Offset: 0x00075DD0
		public IEnumerable<string> GetRoleNames()
		{
			yield break;
		}

		// Token: 0x0600278A RID: 10122 RVA: 0x00077BF0 File Offset: 0x00075DF0
		public IEnumerable<string> GetRoleInstanceIdsForRole(string roleName)
		{
			yield break;
		}

		// Token: 0x0600278B RID: 10123 RVA: 0x00077C10 File Offset: 0x00075E10
		public IEnumerable<RoleInstanceDiagnosticManager> GetRoleInstanceDiagnosticManagersForRole(string roleName)
		{
			yield break;
		}

		// Token: 0x0600278C RID: 10124 RVA: 0x00077C2D File Offset: 0x00075E2D
		public RoleInstanceDiagnosticManager GetRoleInstanceDiagnosticManager(string roleName, string roleInstanceId)
		{
			return new RoleInstanceDiagnosticManager(string.Empty, string.Empty, roleName, roleInstanceId);
		}
	}
}
