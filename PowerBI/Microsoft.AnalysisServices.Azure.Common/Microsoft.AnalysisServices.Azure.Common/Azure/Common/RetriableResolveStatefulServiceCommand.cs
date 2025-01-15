using System;
using System.Linq;
using Microsoft.Cloud.Platform.Azure.WindowsFabric.FabricClient;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x020000B0 RID: 176
	public sealed class RetriableResolveStatefulServiceCommand : ITryingRetriableCommand, IRetriableCommand
	{
		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x0600061B RID: 1563 RVA: 0x00010F56 File Offset: 0x0000F156
		// (set) Token: 0x0600061C RID: 1564 RVA: 0x00010F5E File Offset: 0x0000F15E
		public IResolvedServicePartition Result { get; private set; }

		// Token: 0x0600061D RID: 1565 RVA: 0x00010F68 File Offset: 0x0000F168
		public RetriableResolveStatefulServiceCommand(IFabricClient fabricClient, TimeSpan fabricTimeout, Uri serviceUri, int replicaCount, bool checkReplicaCount = false, bool forceRefresh = false)
		{
			this.fabricClient = fabricClient;
			this.fabricTimeout = fabricTimeout;
			this.serviceUri = serviceUri;
			this.replicaCount = replicaCount;
			this.checkReplicaCount = checkReplicaCount;
			this.forceRefresh = forceRefresh;
			this.previousRsp = null;
			this.Result = null;
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x00010FB6 File Offset: 0x0000F1B6
		public IAsyncResult BeginExecute(RetrierContext retrierContext, AsyncCallback asyncCallback, object asyncState)
		{
			return this.fabricClient.BeginResolveService(this.serviceUri, this.Result, this.fabricTimeout, asyncCallback, asyncState);
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x00010FD7 File Offset: 0x0000F1D7
		public void EndExecute(IAsyncResult ar)
		{
			this.previousRsp = this.Result;
			this.Result = this.fabricClient.EndResolveService(ar);
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x00010FF8 File Offset: 0x0000F1F8
		public bool IsRetryRequired()
		{
			if (this.Result != null && this.Result.Endpoints != null)
			{
				if (!this.Result.Endpoints.None((IResolvedServiceEndpoint e) => e.Role == 2) && (!this.checkReplicaCount || this.Result.Endpoints.Count<IResolvedServiceEndpoint>() == this.replicaCount))
				{
					return this.forceRefresh && this.previousRsp == null;
				}
			}
			return true;
		}

		// Token: 0x04000223 RID: 547
		private readonly IFabricClient fabricClient;

		// Token: 0x04000224 RID: 548
		private readonly Uri serviceUri;

		// Token: 0x04000225 RID: 549
		private readonly int replicaCount;

		// Token: 0x04000226 RID: 550
		private readonly TimeSpan fabricTimeout;

		// Token: 0x04000227 RID: 551
		private bool checkReplicaCount;

		// Token: 0x04000228 RID: 552
		private bool forceRefresh;

		// Token: 0x04000229 RID: 553
		private IResolvedServicePartition previousRsp;
	}
}
