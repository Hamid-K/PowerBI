using System;
using Microsoft.Cloud.Platform.Azure.WindowsFabric.FabricClient;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x020000AF RID: 175
	public sealed class ResolveCommand : IFailingRetriableCommand, IRetriableCommand
	{
		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000615 RID: 1557 RVA: 0x00010EA7 File Offset: 0x0000F0A7
		// (set) Token: 0x06000616 RID: 1558 RVA: 0x00010EAF File Offset: 0x0000F0AF
		public IResolvedServicePartition Result { get; private set; }

		// Token: 0x06000617 RID: 1559 RVA: 0x00010EB8 File Offset: 0x0000F0B8
		public ResolveCommand(IFabricClient fabricClient, Uri serviceUri, TimeSpan resolveTimeout, IResolvedServicePartition previous)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<IFabricClient>(fabricClient, "fabricClient");
			ExtendedDiagnostics.EnsureArgumentNotNull<Uri>(serviceUri, "serviceUri");
			ExtendedDiagnostics.EnsureArgumentNotNull<TimeSpan>(resolveTimeout, "resolveTimeout");
			this.fabricClient = fabricClient;
			this.serviceUri = serviceUri;
			this.resolveTimeout = resolveTimeout;
			this.Result = previous;
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x00010F09 File Offset: 0x0000F109
		public bool IsPermanentError(Exception ex)
		{
			return !(ex is FabricClientTransientException) && !(ex is FabricClientTimeoutException);
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x00010F21 File Offset: 0x0000F121
		public IAsyncResult BeginExecute(RetrierContext retrierContext, AsyncCallback callback, object asyncState)
		{
			return this.fabricClient.BeginGetService(this.serviceUri, this.Result, this.resolveTimeout, callback, asyncState);
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x00010F42 File Offset: 0x0000F142
		public void EndExecute(IAsyncResult asyncResult)
		{
			this.Result = this.fabricClient.EndGetService(asyncResult);
		}

		// Token: 0x0400021F RID: 543
		private readonly TimeSpan resolveTimeout;

		// Token: 0x04000220 RID: 544
		private readonly IFabricClient fabricClient;

		// Token: 0x04000221 RID: 545
		private readonly Uri serviceUri;
	}
}
