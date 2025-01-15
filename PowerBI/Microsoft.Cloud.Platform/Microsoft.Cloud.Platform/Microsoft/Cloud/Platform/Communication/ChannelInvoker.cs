using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x02000495 RID: 1173
	internal class ChannelInvoker<T> : IChannelInvoker<T>
	{
		// Token: 0x06002423 RID: 9251 RVA: 0x0008182D File Offset: 0x0007FA2D
		public ChannelInvoker(TimeSpan operationTimeout)
		{
			this.m_operationTimeout = operationTimeout;
		}

		// Token: 0x06002424 RID: 9252 RVA: 0x0008183C File Offset: 0x0007FA3C
		public IAsyncResult BeginInvoke(T channel, IEnumerable<EcfHttpMessageHeader> httpHeaders, IEnumerable<EcfSoapMessageHeader> soapHeaders, Sequencer.AsyncBeginFunction<T> beginExecuteOperation, Sequencer.AsyncEndFunction endExecuteOperation, AsyncCallback cb, object state)
		{
			IContextChannel contextChannel = (IContextChannel)((object)channel);
			OperationContext operationContext = new OperationContext(contextChannel);
			IAsyncResult asyncResult;
			using (new OperationContextScope(operationContext))
			{
				if (OperationContext.Current != operationContext)
				{
					OperationContext.Current = operationContext;
				}
				contextChannel.OperationTimeout = this.m_operationTimeout;
				ChannelChainedAsyncResult<T> channelChainedAsyncResult = new ChannelChainedAsyncResult<T>(cb, state, OperationContext.Current, endExecuteOperation);
				if (httpHeaders != null)
				{
					HttpRequestMessageProperty httpRequestMessageProperty = new HttpRequestMessageProperty();
					foreach (EcfHttpMessageHeader ecfHttpMessageHeader in httpHeaders)
					{
						httpRequestMessageProperty.Headers.Add(ecfHttpMessageHeader.Name, ecfHttpMessageHeader.Value);
					}
					OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = httpRequestMessageProperty;
				}
				if (soapHeaders != null)
				{
					foreach (EcfSoapMessageHeader ecfSoapMessageHeader in soapHeaders)
					{
						MessageHeader messageHeader = MessageHeader.CreateHeader(ecfSoapMessageHeader.Name, ecfSoapMessageHeader.Namespace, ecfSoapMessageHeader.Value);
						OperationContext.Current.OutgoingMessageHeaders.Add(messageHeader);
					}
				}
				channelChainedAsyncResult.InnerResult = beginExecuteOperation(channel, new AsyncCallback(channelChainedAsyncResult.BeginAsyncFunctionCallback), OperationContext.Current);
				asyncResult = channelChainedAsyncResult;
			}
			return asyncResult;
		}

		// Token: 0x06002425 RID: 9253 RVA: 0x000819C8 File Offset: 0x0007FBC8
		public void EndInvoke(IAsyncResult result)
		{
			ChannelChainedAsyncResult<T> channelChainedAsyncResult = (ChannelChainedAsyncResult<T>)result;
			channelChainedAsyncResult.End();
			using (new OperationContextScope(channelChainedAsyncResult.OperationContext))
			{
				if (OperationContext.Current != channelChainedAsyncResult.OperationContext)
				{
					OperationContext.Current = channelChainedAsyncResult.OperationContext;
				}
				channelChainedAsyncResult.EndExecuteOperation(channelChainedAsyncResult.InnerResult);
			}
		}

		// Token: 0x04000CBA RID: 3258
		private readonly TimeSpan m_operationTimeout;
	}
}
