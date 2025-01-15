using System;
using System.Fabric;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x0200008E RID: 142
	public interface IRoleChangeUpdateSubscriber
	{
		// Token: 0x0600051B RID: 1307
		void OnChangeRole(ReplicaRole oldRole, ReplicaRole newRole);
	}
}
