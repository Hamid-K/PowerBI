using System;
using System.Fabric;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.Common.Utils
{
	// Token: 0x02000140 RID: 320
	public interface IServiceStateChangeSubscriber : IIdentifiable
	{
		// Token: 0x06001157 RID: 4439
		void OnOpen(Uri serviceUri, object Replica);

		// Token: 0x06001158 RID: 4440
		void OnChangeRole(Uri serviceUri, ReplicaRole oldRole, ReplicaRole newRole);

		// Token: 0x06001159 RID: 4441
		void OnClose(Uri serviceUri);
	}
}
