using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Microsoft.OData.Client
{
	// Token: 0x02000059 RID: 89
	internal abstract class ODataRequestMessageWrapper
	{
		// Token: 0x060002C0 RID: 704 RVA: 0x0000AD27 File Offset: 0x00008F27
		protected ODataRequestMessageWrapper(DataServiceClientRequestMessage requestMessage, RequestInfo requestInfo, Descriptor descriptor)
		{
			this.requestMessage = requestMessage;
			this.requestInfo = requestInfo;
			this.Descriptor = descriptor;
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060002C1 RID: 705 RVA: 0x0000AD44 File Offset: 0x00008F44
		// (set) Token: 0x060002C2 RID: 706 RVA: 0x0000AD4C File Offset: 0x00008F4C
		internal Descriptor Descriptor { get; private set; }

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060002C3 RID: 707
		internal abstract ContentStream CachedRequestStream { get; }

		// Token: 0x1700009B RID: 155
		// (set) Token: 0x060002C4 RID: 708 RVA: 0x0000AD55 File Offset: 0x00008F55
		internal bool SendChunked
		{
			set
			{
				this.requestMessage.SendChunked = value;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060002C5 RID: 709
		internal abstract bool IsBatchPartRequest { get; }

		// Token: 0x060002C6 RID: 710 RVA: 0x0000AD64 File Offset: 0x00008F64
		internal static ODataRequestMessageWrapper CreateBatchPartRequestMessage(ODataBatchWriter batchWriter, BuildingRequestEventArgs requestMessageArgs, RequestInfo requestInfo, string contentId)
		{
			IODataRequestMessage iodataRequestMessage = batchWriter.CreateOperationRequestMessage(requestMessageArgs.Method, requestMessageArgs.RequestUri, contentId);
			foreach (KeyValuePair<string, string> keyValuePair in requestMessageArgs.Headers)
			{
				iodataRequestMessage.SetHeader(keyValuePair.Key, keyValuePair.Value);
			}
			InternalODataRequestMessage internalODataRequestMessage = new InternalODataRequestMessage(iodataRequestMessage, false);
			return new ODataRequestMessageWrapper.InnerBatchRequestMessageWrapper(internalODataRequestMessage, iodataRequestMessage, requestInfo, requestMessageArgs.Descriptor);
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0000ADEC File Offset: 0x00008FEC
		internal static ODataRequestMessageWrapper CreateRequestMessageWrapper(BuildingRequestEventArgs requestMessageArgs, RequestInfo requestInfo)
		{
			DataServiceClientRequestMessage dataServiceClientRequestMessage = requestInfo.CreateRequestMessage(requestMessageArgs);
			if (requestInfo.Credentials != null)
			{
				dataServiceClientRequestMessage.Credentials = requestInfo.Credentials;
			}
			if (requestInfo.Timeout != 0)
			{
				dataServiceClientRequestMessage.Timeout = requestInfo.Timeout;
			}
			return new ODataRequestMessageWrapper.TopLevelRequestMessageWrapper(dataServiceClientRequestMessage, requestInfo, requestMessageArgs.Descriptor);
		}

		// Token: 0x060002C8 RID: 712
		internal abstract ODataMessageWriter CreateWriter(ODataMessageWriterSettings writerSettings, bool isParameterPayload);

		// Token: 0x060002C9 RID: 713 RVA: 0x0000AE36 File Offset: 0x00009036
		internal void Abort()
		{
			this.requestMessage.Abort();
		}

		// Token: 0x060002CA RID: 714 RVA: 0x0000AE43 File Offset: 0x00009043
		internal void SetHeader(string headerName, string headerValue)
		{
			this.requestMessage.SetHeader(headerName, headerValue);
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0000AE52 File Offset: 0x00009052
		internal IAsyncResult BeginGetRequestStream(AsyncCallback callback, object state)
		{
			return this.requestMessage.BeginGetRequestStream(callback, state);
		}

		// Token: 0x060002CC RID: 716 RVA: 0x0000AE61 File Offset: 0x00009061
		internal Stream EndGetRequestStream(IAsyncResult asyncResult)
		{
			return this.requestMessage.EndGetRequestStream(asyncResult);
		}

		// Token: 0x060002CD RID: 717 RVA: 0x0000AE70 File Offset: 0x00009070
		internal void SetRequestStream(ContentStream requestStreamContent)
		{
			if (requestStreamContent.IsKnownMemoryStream)
			{
				this.SetContentLengthHeader();
			}
			using (Stream stream = this.requestMessage.GetStream())
			{
				if (requestStreamContent.IsKnownMemoryStream)
				{
					MemoryStream memoryStream = (MemoryStream)requestStreamContent.Stream;
					byte[] buffer = memoryStream.GetBuffer();
					int num = checked((int)memoryStream.Position);
					int num2 = checked((int)memoryStream.Length) - num;
					stream.Write(buffer, num, num2);
				}
				else
				{
					byte[] array = new byte[65536];
					WebUtil.CopyStream(requestStreamContent.Stream, stream, ref array);
				}
			}
		}

		// Token: 0x060002CE RID: 718 RVA: 0x0000AF08 File Offset: 0x00009108
		internal IAsyncResult BeginGetResponse(AsyncCallback callback, object state)
		{
			return this.requestMessage.BeginGetResponse(callback, state);
		}

		// Token: 0x060002CF RID: 719 RVA: 0x0000AF17 File Offset: 0x00009117
		internal IODataResponseMessage EndGetResponse(IAsyncResult asyncResult)
		{
			return this.requestMessage.EndGetResponse(asyncResult);
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x0000AF25 File Offset: 0x00009125
		internal IODataResponseMessage GetResponse()
		{
			return this.requestMessage.GetResponse();
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0000AF34 File Offset: 0x00009134
		internal void SetContentLengthHeader()
		{
			if (this.requestInfo.HasSendingRequest2EventHandlers)
			{
				this.SetHeader("Content-Length", this.CachedRequestStream.Stream.Length.ToString(CultureInfo.InvariantCulture));
			}
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x0000AF76 File Offset: 0x00009176
		internal void FireSendingEventHandlers(Descriptor descriptor)
		{
			this.FireSendingRequest2(descriptor);
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x0000AF80 File Offset: 0x00009180
		internal void FireSendingRequest2(Descriptor descriptor)
		{
			if (this.requestInfo.HasSendingRequest2EventHandlers)
			{
				HttpWebRequestMessage httpWebRequestMessage = this.requestMessage as HttpWebRequestMessage;
				if (httpWebRequestMessage != null)
				{
					httpWebRequestMessage.BeforeSendingRequest2Event();
				}
				try
				{
					this.requestInfo.FireSendingRequest2(new SendingRequest2EventArgs(this.requestMessage, descriptor, this.IsBatchPartRequest));
				}
				finally
				{
					if (httpWebRequestMessage != null)
					{
						httpWebRequestMessage.AfterSendingRequest2Event();
					}
				}
			}
		}

		// Token: 0x040000ED RID: 237
		private readonly DataServiceClientRequestMessage requestMessage;

		// Token: 0x040000EE RID: 238
		private readonly RequestInfo requestInfo;

		// Token: 0x02000166 RID: 358
		private class RequestMessageWithCachedStream : IODataRequestMessage
		{
			// Token: 0x06000D5C RID: 3420 RVA: 0x0002E98B File Offset: 0x0002CB8B
			internal RequestMessageWithCachedStream(DataServiceClientRequestMessage requestMessage)
			{
				this.requestMessage = requestMessage;
			}

			// Token: 0x1700034A RID: 842
			// (get) Token: 0x06000D5D RID: 3421 RVA: 0x0002E99A File Offset: 0x0002CB9A
			public IEnumerable<KeyValuePair<string, string>> Headers
			{
				get
				{
					return this.requestMessage.Headers;
				}
			}

			// Token: 0x1700034B RID: 843
			// (get) Token: 0x06000D5E RID: 3422 RVA: 0x0002E9A7 File Offset: 0x0002CBA7
			// (set) Token: 0x06000D5F RID: 3423 RVA: 0x00006FEF File Offset: 0x000051EF
			public Uri Url
			{
				get
				{
					return this.requestMessage.Url;
				}
				set
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x1700034C RID: 844
			// (get) Token: 0x06000D60 RID: 3424 RVA: 0x0002E9B4 File Offset: 0x0002CBB4
			// (set) Token: 0x06000D61 RID: 3425 RVA: 0x00006FEF File Offset: 0x000051EF
			public string Method
			{
				get
				{
					return this.requestMessage.Method;
				}
				set
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x1700034D RID: 845
			// (get) Token: 0x06000D62 RID: 3426 RVA: 0x0002E9C1 File Offset: 0x0002CBC1
			internal ContentStream CachedRequestStream
			{
				get
				{
					this.cachedRequestStream.Stream.Position = 0L;
					return this.cachedRequestStream;
				}
			}

			// Token: 0x06000D63 RID: 3427 RVA: 0x0002E9DB File Offset: 0x0002CBDB
			public string GetHeader(string headerName)
			{
				return this.requestMessage.GetHeader(headerName);
			}

			// Token: 0x06000D64 RID: 3428 RVA: 0x0002E9E9 File Offset: 0x0002CBE9
			public void SetHeader(string headerName, string headerValue)
			{
				this.requestMessage.SetHeader(headerName, headerValue);
			}

			// Token: 0x06000D65 RID: 3429 RVA: 0x0002E9F8 File Offset: 0x0002CBF8
			public Stream GetStream()
			{
				if (this.cachedRequestStream == null)
				{
					this.cachedRequestStream = new ContentStream(new MemoryStream(), true);
				}
				return this.cachedRequestStream.Stream;
			}

			// Token: 0x0400070C RID: 1804
			private readonly DataServiceClientRequestMessage requestMessage;

			// Token: 0x0400070D RID: 1805
			private ContentStream cachedRequestStream;
		}

		// Token: 0x02000167 RID: 359
		private class TopLevelRequestMessageWrapper : ODataRequestMessageWrapper
		{
			// Token: 0x06000D66 RID: 3430 RVA: 0x0002EA1E File Offset: 0x0002CC1E
			internal TopLevelRequestMessageWrapper(DataServiceClientRequestMessage requestMessage, RequestInfo requestInfo, Descriptor descriptor)
				: base(requestMessage, requestInfo, descriptor)
			{
				this.messageWithCachedStream = new ODataRequestMessageWrapper.RequestMessageWithCachedStream(this.requestMessage);
			}

			// Token: 0x1700034E RID: 846
			// (get) Token: 0x06000D67 RID: 3431 RVA: 0x00015066 File Offset: 0x00013266
			internal override bool IsBatchPartRequest
			{
				get
				{
					return false;
				}
			}

			// Token: 0x1700034F RID: 847
			// (get) Token: 0x06000D68 RID: 3432 RVA: 0x0002EA3A File Offset: 0x0002CC3A
			internal override ContentStream CachedRequestStream
			{
				get
				{
					return this.messageWithCachedStream.CachedRequestStream;
				}
			}

			// Token: 0x06000D69 RID: 3433 RVA: 0x0002EA47 File Offset: 0x0002CC47
			internal override ODataMessageWriter CreateWriter(ODataMessageWriterSettings writerSettings, bool isParameterPayload)
			{
				return this.requestInfo.WriteHelper.CreateWriter(this.messageWithCachedStream, writerSettings, isParameterPayload);
			}

			// Token: 0x0400070E RID: 1806
			private readonly ODataRequestMessageWrapper.RequestMessageWithCachedStream messageWithCachedStream;
		}

		// Token: 0x02000168 RID: 360
		private class InnerBatchRequestMessageWrapper : ODataRequestMessageWrapper
		{
			// Token: 0x06000D6A RID: 3434 RVA: 0x0002EA61 File Offset: 0x0002CC61
			internal InnerBatchRequestMessageWrapper(DataServiceClientRequestMessage clientRequestMessage, IODataRequestMessage odataRequestMessage, RequestInfo requestInfo, Descriptor descriptor)
				: base(clientRequestMessage, requestInfo, descriptor)
			{
				this.innerBatchRequestMessage = odataRequestMessage;
			}

			// Token: 0x17000350 RID: 848
			// (get) Token: 0x06000D6B RID: 3435 RVA: 0x00004A70 File Offset: 0x00002C70
			internal override bool IsBatchPartRequest
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000351 RID: 849
			// (get) Token: 0x06000D6C RID: 3436 RVA: 0x00006FEF File Offset: 0x000051EF
			internal override ContentStream CachedRequestStream
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x06000D6D RID: 3437 RVA: 0x0002EA74 File Offset: 0x0002CC74
			internal override ODataMessageWriter CreateWriter(ODataMessageWriterSettings writerSettings, bool isParameterPayload)
			{
				return this.requestInfo.WriteHelper.CreateWriter(this.innerBatchRequestMessage, writerSettings, isParameterPayload);
			}

			// Token: 0x0400070F RID: 1807
			private readonly IODataRequestMessage innerBatchRequestMessage;
		}
	}
}
