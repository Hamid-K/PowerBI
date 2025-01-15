using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Fabric.Common;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000011 RID: 17
	internal sealed class HybridClientStrategy : SimpleClientStrategy
	{
		// Token: 0x0600007D RID: 125 RVA: 0x00003D98 File Offset: 0x00001F98
		public HybridClientStrategy(EndpointID destination, SimpleSendReceiveModule module, DataCacheFactoryConfiguration config, IClientProtocol transport, CacheLookupTableTransfer initialLookupTable)
			: base(destination, module, config)
		{
			string cacheName = Utility.GetCacheName(destination.UriString);
			this._routingManager = new ClientRoutingManager(transport, cacheName, destination, initialLookupTable);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003CAB File Offset: 0x00001EAB
		public override IEnumerable<EndpointID> GetHostEndpoints(string cacheName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00003DCB File Offset: 0x00001FCB
		public ClientRoutingManager RoutingManager
		{
			get
			{
				return this._routingManager;
			}
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003DD4 File Offset: 0x00001FD4
		public override ResponseBody SendMessageAndWait(RequestBody reqMsg, out IRequestTracker tracker)
		{
			reqMsg.Fwd = ForwardingType.Routable;
			reqMsg.Action = "http://schemas.microsoft.com/velocity/msgs/request";
			ResponseBody responseBody;
			if (reqMsg.RegionName != null)
			{
				EndpointID endpointID;
				if (!this._operateInSimpleMode)
				{
					endpointID = this._routingManager.Lookup(reqMsg.RegionID);
				}
				else
				{
					endpointID = this._destination;
				}
				if (endpointID != null)
				{
					reqMsg.NodeId = Utility.GetCustomHashCode(endpointID.URI.Host);
					responseBody = this._sendReceiveModule.SendMsgAndWait(endpointID, reqMsg, this._factoryConfig.RequestTimeout, out tracker);
					this.PostProcessResponse(reqMsg, responseBody);
				}
				else
				{
					responseBody = this._sendReceiveModule.SendMsgAndWait(this._destination, reqMsg, this._factoryConfig.RequestTimeout, out tracker);
					this._routingManager.RefreshLookUpTableAsync(reqMsg);
				}
			}
			else
			{
				responseBody = this._sendReceiveModule.SendMsgAndWait(this._destination, reqMsg, this._factoryConfig.RequestTimeout, out tracker);
			}
			if (responseBody.Ack == AckNack.Nack)
			{
				if (responseBody.ResponseCode == ErrStatus.CACHE_REDIRECTED)
				{
					if (Provider.IsEnabled(TraceLevel.Warning))
					{
						EventLogWriter.WriteWarning("DataCache.HybridClient", "{0}: SendMsgAndWait: Redirect SimpleSendReceiveModule Server Uri to: {1}", new object[]
						{
							reqMsg.RequestTrackingId,
							responseBody.RedirectUri.ToString()
						});
					}
					this._destination = new EndpointID(responseBody.RedirectUri.ToString());
					responseBody = this._sendReceiveModule.SendMsgAndWait(this._destination, reqMsg, this._factoryConfig.RequestTimeout, out tracker);
					if (this.UpdateCacheRedirectionForHybridClient(this._destination))
					{
						this._routingManager.Server = this._destination;
						this._routingManager.RefreshLookUpTableAsync(reqMsg);
					}
				}
				else if (responseBody.ResponseCode == ErrStatus.CONVERT_SIMPLECLIENT)
				{
					this._operateInSimpleMode = true;
					this.ConvertChannelMode(DataCacheDeploymentMode.SimpleClient);
					responseBody = this._sendReceiveModule.SendMsgAndWait(this._destination, reqMsg, this._factoryConfig.RequestTimeout, out tracker);
				}
			}
			return responseBody;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003F98 File Offset: 0x00002198
		private void ConvertChannelMode(DataCacheDeploymentMode deploymentMode)
		{
			WcfClientChannel wcfClientChannel = this._sendReceiveModule.Channel as WcfClientChannel;
			if (wcfClientChannel != null)
			{
				wcfClientChannel.ConvertChannelMode(deploymentMode);
				return;
			}
			throw new NotImplementedException();
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00003FC8 File Offset: 0x000021C8
		private bool UpdateCacheRedirectionForHybridClient(EndpointID newVipEndpoint)
		{
			WcfClientChannel wcfClientChannel = this._sendReceiveModule.Channel as WcfClientChannel;
			if (wcfClientChannel != null)
			{
				return wcfClientChannel.UpdateCacheRedirectionForHybridClient(newVipEndpoint);
			}
			throw new NotImplementedException();
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003FF6 File Offset: 0x000021F6
		private void PostProcessResponse(RequestBody request, ResponseBody response)
		{
			if (response != null && response.Ack == AckNack.Ack && !this._operateInSimpleMode && response.IsClientRoutingTableStale)
			{
				this._routingManager.RefreshLookUpTableAsync(request);
			}
		}

		// Token: 0x0400005C RID: 92
		private bool _operateInSimpleMode;

		// Token: 0x0400005D RID: 93
		private readonly ClientRoutingManager _routingManager;
	}
}
