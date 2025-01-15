using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Fabric.Common;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200000F RID: 15
	internal class SimpleClientStrategy : IRoutingStrategy, IDisposable
	{
		// Token: 0x06000070 RID: 112 RVA: 0x00003BB3 File Offset: 0x00001DB3
		public SimpleClientStrategy(EndpointID destination, SimpleSendReceiveModule module, DataCacheFactoryConfiguration config)
		{
			this._destination = destination;
			this._sendReceiveModule = module;
			this._factoryConfig = config;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003BD0 File Offset: 0x00001DD0
		public virtual IEnumerable<EndpointID> GetHostEndpoints(string cacheName)
		{
			yield return this._destination;
			yield break;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003BF0 File Offset: 0x00001DF0
		public virtual ResponseBody SendMessageAndWait(RequestBody reqMsg, out IRequestTracker tracker)
		{
			reqMsg.Action = "http://schemas.microsoft.com/velocity/msgs/request";
			ResponseBody responseBody = this._sendReceiveModule.SendMsgAndWait(this._destination, reqMsg, this._factoryConfig.RequestTimeout, out tracker);
			if (responseBody.Ack == AckNack.Nack && responseBody.ResponseCode == ErrStatus.CACHE_REDIRECTED)
			{
				if (Provider.IsEnabled(TraceLevel.Warning))
				{
					EventLogWriter.WriteWarning("DataCache.SimpleClient", "{0}: SendMsgAndWait: Redirect SimpleSendReceiveModule Server Uri to: {1}", new object[]
					{
						reqMsg.RequestTrackingId,
						responseBody.RedirectUri.ToString()
					});
				}
				this._destination = new EndpointID(responseBody.RedirectUri.ToString());
				responseBody = this._sendReceiveModule.SendMsgAndWait(this._destination, reqMsg, this._factoryConfig.RequestTimeout, out tracker);
			}
			return responseBody;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003CAB File Offset: 0x00001EAB
		public ResponseBody SendMultiMessageAndWaitForAll(MultiRequest reqMsg, out IRequestTracker tracker)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003CB2 File Offset: 0x00001EB2
		public virtual void Dispose()
		{
			if (!this._factoryConfig.UseConnectionPool)
			{
				this._sendReceiveModule.Dispose();
			}
		}

		// Token: 0x04000055 RID: 85
		protected readonly SimpleSendReceiveModule _sendReceiveModule;

		// Token: 0x04000056 RID: 86
		protected readonly DataCacheFactoryConfiguration _factoryConfig;

		// Token: 0x04000057 RID: 87
		protected EndpointID _destination;
	}
}
