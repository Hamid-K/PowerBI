using System;
using System.Collections.Generic;
using Microsoft.Fabric.Common;
using Microsoft.Fabric.Data;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200002E RID: 46
	internal class ThickClient : ServiceResolver
	{
		// Token: 0x06000144 RID: 324 RVA: 0x00006F70 File Offset: 0x00005170
		internal ThickClient(IEnumerable<string> namedCaches, IEnumerable<string> endpointAddresses, IClientProtocol protocol, string id, CacheLookupTableTransfer initialLookupTable)
			: base(namedCaches, endpointAddresses)
		{
			this._protocolInUse = protocol;
			this._logSource = this._logSource + "." + id;
			if (initialLookupTable != null)
			{
				base.UpdateLookupTable(initialLookupTable.GetCasLookupTableTransfer());
			}
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00006FC0 File Offset: 0x000051C0
		protected override IAsyncResult BeginRequestLookupTable(string nodeAddress, LookupTableRequest request, TimeSpan timeout, AsyncCallback callback, object state)
		{
			if (request == null)
			{
				throw new ArgumentNullException("request");
			}
			CacheLookupTableTransfer lookupTable = this._protocolInUse.GetLookupTable(new EndpointID(nodeAddress), new CacheLookupTableRequest(request.Ranges, request.InterestedApps, request.GenerationNumber), timeout);
			ThickClient.LookupTableRefreshContext lookupTableRefreshContext;
			if (lookupTable != null)
			{
				lookupTableRefreshContext = new ThickClient.LookupTableRefreshContext(lookupTable.GetCasLookupTableTransfer(), callback, state);
			}
			else
			{
				lookupTableRefreshContext = new ThickClient.LookupTableRefreshContext(null, callback, state);
			}
			lookupTableRefreshContext.CompleteOperation(true, null);
			return lookupTableRefreshContext;
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00007030 File Offset: 0x00005230
		protected override LookupTableTransfer EndRequestLookupTable(IAsyncResult result)
		{
			ThickClient.LookupTableRefreshContext lookupTableRefreshContext = result as ThickClient.LookupTableRefreshContext;
			lookupTableRefreshContext.End();
			return lookupTableRefreshContext.LookupTable;
		}

		// Token: 0x040000B6 RID: 182
		private string _logSource = "DistributedCache.ThickClient";

		// Token: 0x040000B7 RID: 183
		private readonly IClientProtocol _protocolInUse;

		// Token: 0x0200002F RID: 47
		private sealed class LookupTableRefreshContext : OperationContext
		{
			// Token: 0x06000147 RID: 327 RVA: 0x00007050 File Offset: 0x00005250
			internal LookupTableRefreshContext(LookupTableTransfer transfer, AsyncCallback callback, object state)
				: base(callback, state)
			{
				this.LookupTable = transfer;
			}

			// Token: 0x1700003E RID: 62
			// (get) Token: 0x06000148 RID: 328 RVA: 0x00007061 File Offset: 0x00005261
			// (set) Token: 0x06000149 RID: 329 RVA: 0x00007069 File Offset: 0x00005269
			internal LookupTableTransfer LookupTable { get; private set; }
		}
	}
}
