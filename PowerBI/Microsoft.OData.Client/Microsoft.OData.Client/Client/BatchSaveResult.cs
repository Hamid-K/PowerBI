using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Microsoft.OData.Client.Metadata;

namespace Microsoft.OData.Client
{
	// Token: 0x02000022 RID: 34
	[SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable", Justification = "The response stream is disposed by the message reader we create over it which we dispose inside the enumerator.")]
	internal class BatchSaveResult : BaseSaveResult
	{
		// Token: 0x0600010A RID: 266 RVA: 0x000063DE File Offset: 0x000045DE
		internal BatchSaveResult(DataServiceContext context, string method, DataServiceRequest[] queries, SaveChangesOptions options, AsyncCallback callback, object state)
			: base(context, method, queries, options, callback, state)
		{
			this.Queries = queries;
			this.streamCopyBuffer = new byte[4000];
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600010B RID: 267 RVA: 0x00004A70 File Offset: 0x00002C70
		internal override bool IsBatchRequest
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600010C RID: 268 RVA: 0x00006406 File Offset: 0x00004606
		protected override Stream ResponseStream
		{
			get
			{
				return this.responseStream;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600010D RID: 269 RVA: 0x0000640E File Offset: 0x0000460E
		protected override bool ProcessResponsePayload
		{
			get
			{
				return !this.currentOperationResponse.HasEmptyContent;
			}
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00006420 File Offset: 0x00004620
		internal void BatchBeginRequest()
		{
			BaseAsyncResult.PerRequest perRequest = null;
			try
			{
				ODataRequestMessageWrapper odataRequestMessageWrapper = this.GenerateBatchRequest();
				base.Abortable = odataRequestMessageWrapper;
				if (odataRequestMessageWrapper != null)
				{
					odataRequestMessageWrapper.SetContentLengthHeader();
					perRequest = (this.perRequest = new BaseAsyncResult.PerRequest());
					perRequest.Request = odataRequestMessageWrapper;
					perRequest.RequestContentStream = odataRequestMessageWrapper.CachedRequestStream;
					BaseAsyncResult.AsyncStateBag asyncStateBag = new BaseAsyncResult.AsyncStateBag(perRequest);
					this.responseStream = new MemoryStream();
					IAsyncResult asyncResult = BaseAsyncResult.InvokeAsync(new Func<AsyncCallback, object, IAsyncResult>(odataRequestMessageWrapper.BeginGetRequestStream), new AsyncCallback(base.AsyncEndGetRequestStream), asyncStateBag);
					perRequest.SetRequestCompletedSynchronously(asyncResult.CompletedSynchronously);
				}
			}
			catch (Exception ex)
			{
				base.HandleFailure(perRequest, ex);
				throw;
			}
			finally
			{
				this.HandleCompleted(perRequest);
			}
		}

		// Token: 0x0600010F RID: 271 RVA: 0x000064D8 File Offset: 0x000046D8
		internal void BatchRequest()
		{
			ODataRequestMessageWrapper odataRequestMessageWrapper = this.GenerateBatchRequest();
			if (odataRequestMessageWrapper != null)
			{
				odataRequestMessageWrapper.SetRequestStream(odataRequestMessageWrapper.CachedRequestStream);
				try
				{
					this.batchResponseMessage = this.RequestInfo.GetSyncronousResponse(odataRequestMessageWrapper, false);
				}
				catch (DataServiceTransportException ex)
				{
					InvalidOperationException httpWebResponse = WebUtil.GetHttpWebResponse(ex, ref this.batchResponseMessage);
					throw httpWebResponse;
				}
				finally
				{
					if (this.batchResponseMessage != null)
					{
						this.responseStream = this.batchResponseMessage.GetStream();
					}
				}
			}
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00006558 File Offset: 0x00004758
		protected override void FinishCurrentChange(BaseAsyncResult.PerRequest pereq)
		{
			base.FinishCurrentChange(pereq);
			this.ResponseStream.Position = 0L;
			this.perRequest = null;
			base.SetCompleted();
		}

		// Token: 0x06000111 RID: 273 RVA: 0x0000657B File Offset: 0x0000477B
		protected override void HandleOperationResponse(IODataResponseMessage responseMessage)
		{
			Error.ThrowInternalError(InternalError.InvalidHandleOperationResponse);
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00006584 File Offset: 0x00004784
		protected override DataServiceResponse HandleResponse()
		{
			if (this.ResponseStream != null)
			{
				return this.HandleBatchResponse();
			}
			return new DataServiceResponse(null, 0, new List<OperationResponse>(0), true);
		}

		// Token: 0x06000113 RID: 275 RVA: 0x000065A4 File Offset: 0x000047A4
		protected override MaterializeAtom GetMaterializer(EntityDescriptor entityDescriptor, ResponseInfo responseInfo)
		{
			QueryComponents queryComponents = new QueryComponents(null, Util.ODataVersionEmpty, entityDescriptor.Entity.GetType(), null, null);
			return new MaterializeAtom(responseInfo, queryComponents, null, this.currentOperationResponse.CreateResponseMessage(), ODataPayloadKind.Resource);
		}

		// Token: 0x06000114 RID: 276 RVA: 0x000065E0 File Offset: 0x000047E0
		protected override ODataRequestMessageWrapper CreateRequestMessage(string method, Uri requestUri, HeaderCollection headers, HttpStack httpStack, Descriptor descriptor, string contentId)
		{
			BuildingRequestEventArgs buildingRequestEventArgs = this.RequestInfo.CreateRequestArgsAndFireBuildingRequest(method, requestUri, headers, this.RequestInfo.HttpStack, descriptor);
			return ODataRequestMessageWrapper.CreateBatchPartRequestMessage(this.batchWriter, buildingRequestEventArgs, this.RequestInfo, contentId);
		}

		// Token: 0x06000115 RID: 277 RVA: 0x0000661D File Offset: 0x0000481D
		private static string CreateMultiPartMimeContentType()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}; {1}={2}_{3}", new object[]
			{
				"multipart/mixed",
				"boundary",
				"batch",
				Guid.NewGuid()
			});
		}

		// Token: 0x06000116 RID: 278 RVA: 0x0000665C File Offset: 0x0000485C
		private ODataRequestMessageWrapper CreateBatchRequest()
		{
			Uri uri = UriUtil.CreateUri(this.RequestInfo.BaseUriResolver.GetBaseUriWithSlash(), UriUtil.CreateUri("$batch", UriKind.Relative));
			HeaderCollection headerCollection = new HeaderCollection();
			headerCollection.SetRequestVersion(Util.ODataVersion4, this.RequestInfo.MaxProtocolVersionAsVersion);
			headerCollection.SetHeader("Content-Type", BatchSaveResult.CreateMultiPartMimeContentType());
			this.RequestInfo.Format.SetRequestAcceptHeaderForBatch(headerCollection);
			return base.CreateTopLevelRequest("POST", uri, headerCollection, this.RequestInfo.HttpStack, null);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x000066E0 File Offset: 0x000048E0
		private ODataRequestMessageWrapper GenerateBatchRequest()
		{
			if (this.ChangedEntries.Count == 0 && this.Queries == null)
			{
				base.SetCompleted();
				return null;
			}
			ODataRequestMessageWrapper odataRequestMessageWrapper = this.CreateBatchRequest();
			odataRequestMessageWrapper.FireSendingRequest2(null);
			using (ODataMessageWriter odataMessageWriter = Serializer.CreateMessageWriter(odataRequestMessageWrapper, this.RequestInfo, false))
			{
				this.batchWriter = odataMessageWriter.CreateODataBatchWriter();
				this.batchWriter.WriteStartBatch();
				if (this.Queries != null)
				{
					foreach (DataServiceRequest dataServiceRequest in this.Queries)
					{
						QueryComponents queryComponents = dataServiceRequest.QueryComponents(this.RequestInfo.Model);
						Uri orCreateAbsoluteUri = this.RequestInfo.BaseUriResolver.GetOrCreateAbsoluteUri(queryComponents.Uri);
						HeaderCollection headerCollection = new HeaderCollection();
						headerCollection.SetRequestVersion(queryComponents.Version, this.RequestInfo.MaxProtocolVersionAsVersion);
						this.RequestInfo.Format.SetRequestAcceptHeaderForQuery(headerCollection, queryComponents);
						ODataRequestMessageWrapper odataRequestMessageWrapper2 = this.CreateRequestMessage("GET", orCreateAbsoluteUri, headerCollection, this.RequestInfo.HttpStack, null, null);
						odataRequestMessageWrapper2.FireSendingEventHandlers(null);
					}
				}
				else if (0 < this.ChangedEntries.Count)
				{
					if (Util.IsBatchWithSingleChangeset(this.Options))
					{
						this.batchWriter.WriteStartChangeset();
					}
					ClientEdmModel model = this.RequestInfo.Model;
					for (int j = 0; j < this.ChangedEntries.Count; j++)
					{
						if (Util.IsBatchWithIndependentOperations(this.Options))
						{
							this.batchWriter.WriteStartChangeset();
						}
						Descriptor descriptor = this.ChangedEntries[j];
						if (!descriptor.ContentGeneratedForSave)
						{
							EntityDescriptor entityDescriptor = descriptor as EntityDescriptor;
							if (descriptor.DescriptorKind == DescriptorKind.Entity)
							{
								if (entityDescriptor.State == EntityStates.Added)
								{
									ClientTypeAnnotation clientTypeAnnotation = model.GetClientTypeAnnotation(model.GetOrCreateEdmType(entityDescriptor.Entity.GetType()));
									if (clientTypeAnnotation.IsMediaLinkEntry || entityDescriptor.IsMediaLinkEntry)
									{
										throw Error.NotSupported(Strings.Context_BatchNotSupportedForMediaLink);
									}
								}
								else if ((entityDescriptor.State == EntityStates.Unchanged || entityDescriptor.State == EntityStates.Modified) && entityDescriptor.SaveStream != null)
								{
									throw Error.NotSupported(Strings.Context_BatchNotSupportedForMediaLink);
								}
							}
							else if (descriptor.DescriptorKind == DescriptorKind.NamedStream)
							{
								throw Error.NotSupported(Strings.Context_BatchNotSupportedForNamedStreams);
							}
							ODataRequestMessageWrapper odataRequestMessageWrapper3;
							if (descriptor.DescriptorKind == DescriptorKind.Entity)
							{
								odataRequestMessageWrapper3 = base.CreateRequest(entityDescriptor);
							}
							else
							{
								odataRequestMessageWrapper3 = base.CreateRequest((LinkDescriptor)descriptor);
							}
							odataRequestMessageWrapper3.FireSendingRequest2(descriptor);
							base.CreateChangeData(j, odataRequestMessageWrapper3);
							if (Util.IsBatchWithIndependentOperations(this.Options))
							{
								this.batchWriter.WriteEndChangeset();
							}
						}
					}
					if (Util.IsBatchWithSingleChangeset(this.Options))
					{
						this.batchWriter.WriteEndChangeset();
					}
				}
				this.batchWriter.WriteEndBatch();
				this.batchWriter.Flush();
			}
			return odataRequestMessageWrapper;
		}

		// Token: 0x06000118 RID: 280 RVA: 0x000069B8 File Offset: 0x00004BB8
		private DataServiceResponse HandleBatchResponse()
		{
			bool flag = true;
			DataServiceResponse dataServiceResponse2;
			try
			{
				if (this.batchResponseMessage == null || this.batchResponseMessage.StatusCode == 204)
				{
					throw Error.InvalidOperation(Strings.Batch_ExpectedResponse(1));
				}
				Func<Stream> func = () => this.ResponseStream;
				Version version;
				BaseSaveResult.HandleResponse(this.RequestInfo, (HttpStatusCode)this.batchResponseMessage.StatusCode, this.batchResponseMessage.GetHeader("OData-Version"), func, true, out version);
				if (this.ResponseStream == null)
				{
					Error.ThrowBatchExpectedResponse(InternalError.NullResponseStream);
				}
				this.batchResponseMessage = new HttpWebResponseMessage(new HeaderCollection(this.batchResponseMessage), this.batchResponseMessage.StatusCode, func);
				ODataMessageReaderSettings odataMessageReaderSettings = this.RequestInfo.GetDeserializationInfo(null).ReadHelper.CreateSettings();
				this.batchMessageReader = new ODataMessageReader(this.batchResponseMessage, odataMessageReaderSettings);
				ODataBatchReader odataBatchReader;
				try
				{
					odataBatchReader = this.batchMessageReader.CreateODataBatchReader();
				}
				catch (ODataContentTypeException ex)
				{
					Exception ex2 = ex;
					string text;
					Encoding encoding;
					ContentTypeUtil.ReadContentType(this.batchResponseMessage.GetHeader("Content-Type"), out text, out encoding);
					if (string.Equals("text/plain", text, StringComparison.Ordinal))
					{
						ex2 = BaseSaveResult.GetResponseText(new Func<Stream>(this.batchResponseMessage.GetStream), (HttpStatusCode)this.batchResponseMessage.StatusCode);
					}
					throw Error.InvalidOperation(Strings.Batch_ExpectedContentType(this.batchResponseMessage.GetHeader("Content-Type")), ex2);
				}
				DataServiceResponse dataServiceResponse = this.HandleBatchResponseInternal(odataBatchReader);
				flag = false;
				dataServiceResponse2 = dataServiceResponse;
			}
			catch (DataServiceRequestException)
			{
				throw;
			}
			catch (InvalidOperationException ex3)
			{
				HeaderCollection headerCollection = new HeaderCollection(this.batchResponseMessage);
				int num = ((this.batchResponseMessage == null) ? 500 : this.batchResponseMessage.StatusCode);
				DataServiceResponse dataServiceResponse3 = new DataServiceResponse(headerCollection, num, new OperationResponse[0], this.IsBatchRequest);
				throw new DataServiceRequestException(Strings.DataServiceException_GeneralError, ex3, dataServiceResponse3);
			}
			finally
			{
				if (flag)
				{
					Util.Dispose<ODataMessageReader>(ref this.batchMessageReader);
				}
			}
			return dataServiceResponse2;
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00006BE8 File Offset: 0x00004DE8
		private DataServiceResponse HandleBatchResponseInternal(ODataBatchReader batchReader)
		{
			HeaderCollection headerCollection = new HeaderCollection(this.batchResponseMessage);
			IEnumerable<OperationResponse> enumerable = this.HandleBatchResponse(batchReader);
			DataServiceResponse dataServiceResponse;
			if (this.Queries != null)
			{
				dataServiceResponse = new DataServiceResponse(headerCollection, this.batchResponseMessage.StatusCode, enumerable, true);
			}
			else
			{
				List<OperationResponse> list = new List<OperationResponse>();
				dataServiceResponse = new DataServiceResponse(headerCollection, this.batchResponseMessage.StatusCode, list, true);
				Exception ex = null;
				foreach (OperationResponse operationResponse in enumerable)
				{
					ChangeOperationResponse changeOperationResponse = (ChangeOperationResponse)operationResponse;
					list.Add(changeOperationResponse);
					if (Util.IsBatchWithSingleChangeset(this.Options) && ex == null && changeOperationResponse.Error != null)
					{
						ex = changeOperationResponse.Error;
					}
				}
				if (ex != null)
				{
					throw new DataServiceRequestException(Strings.DataServiceException_GeneralError, ex, dataServiceResponse);
				}
			}
			return dataServiceResponse;
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00006CC4 File Offset: 0x00004EC4
		private IEnumerable<OperationResponse> HandleBatchResponse(ODataBatchReader batchReader)
		{
			try
			{
				if (this.batchMessageReader == null)
				{
					yield break;
				}
				bool changesetFound = false;
				bool insideChangeset = false;
				int queryCount = 0;
				int operationCount = 0;
				this.entryIndex = 0;
				while (batchReader.Read())
				{
					Exception exception;
					switch (batchReader.State)
					{
					case ODataBatchReaderState.Operation:
						exception = this.ProcessCurrentOperationResponse(batchReader, insideChangeset);
						if (!insideChangeset)
						{
							QueryOperationResponse queryOperationResponse = null;
							try
							{
								if (exception == null)
								{
									DataServiceRequest dataServiceRequest = this.Queries[queryCount];
									ResponseInfo deserializationInfo = this.RequestInfo.GetDeserializationInfo(null);
									MaterializeAtom materializeAtom = DataServiceRequest.Materialize(deserializationInfo, dataServiceRequest.QueryComponents(this.RequestInfo.Model), null, this.currentOperationResponse.Headers.GetHeader("Content-Type"), this.currentOperationResponse.CreateResponseMessage(), dataServiceRequest.PayloadKind);
									queryOperationResponse = QueryOperationResponse.GetInstance(dataServiceRequest.ElementType, this.currentOperationResponse.Headers, dataServiceRequest, materializeAtom);
								}
							}
							catch (ArgumentException ex)
							{
								exception = ex;
							}
							catch (FormatException ex2)
							{
								exception = ex2;
							}
							catch (InvalidOperationException ex3)
							{
								exception = ex3;
							}
							if (queryOperationResponse == null)
							{
								if (this.Queries == null)
								{
									throw exception;
								}
								DataServiceRequest dataServiceRequest2 = this.Queries[queryCount];
								if (this.RequestInfo.IgnoreResourceNotFoundException && this.currentOperationResponse.StatusCode == HttpStatusCode.NotFound)
								{
									queryOperationResponse = QueryOperationResponse.GetInstance(dataServiceRequest2.ElementType, this.currentOperationResponse.Headers, dataServiceRequest2, MaterializeAtom.EmptyResults);
								}
								else
								{
									queryOperationResponse = QueryOperationResponse.GetInstance(dataServiceRequest2.ElementType, this.currentOperationResponse.Headers, dataServiceRequest2, MaterializeAtom.EmptyResults);
									queryOperationResponse.Error = exception;
								}
							}
							queryOperationResponse.StatusCode = (int)this.currentOperationResponse.StatusCode;
							int num = queryCount;
							queryCount = num + 1;
							yield return queryOperationResponse;
						}
						else
						{
							try
							{
								Descriptor descriptor = this.ChangedEntries[this.entryIndex];
								operationCount += this.SaveResultProcessed(descriptor);
								if (exception != null)
								{
									throw exception;
								}
								this.HandleOperationResponseHeaders(this.currentOperationResponse.StatusCode, this.currentOperationResponse.Headers);
								this.HandleOperationResponse(descriptor, this.currentOperationResponse.Headers);
							}
							catch (Exception ex4)
							{
								this.ChangedEntries[this.entryIndex].SaveError = ex4;
								exception = ex4;
								if (!CommonUtil.IsCatchableExceptionType(ex4))
								{
									throw;
								}
							}
							ChangeOperationResponse changeOperationResponse = new ChangeOperationResponse(this.currentOperationResponse.Headers, this.ChangedEntries[this.entryIndex]);
							changeOperationResponse.StatusCode = (int)this.currentOperationResponse.StatusCode;
							if (exception != null)
							{
								changeOperationResponse.Error = exception;
							}
							int num = operationCount;
							operationCount = num + 1;
							this.entryIndex++;
							yield return changeOperationResponse;
						}
						break;
					case ODataBatchReaderState.ChangesetStart:
						if ((Util.IsBatchWithSingleChangeset(this.Options) && changesetFound) || operationCount != 0)
						{
							Error.ThrowBatchUnexpectedContent(InternalError.UnexpectedBeginChangeSet);
						}
						insideChangeset = true;
						break;
					case ODataBatchReaderState.ChangesetEnd:
						changesetFound = true;
						operationCount = 0;
						insideChangeset = false;
						break;
					default:
						Error.ThrowBatchExpectedResponse(InternalError.UnexpectedBatchState);
						break;
					}
					exception = null;
				}
				if (this.Queries == null)
				{
					if (!changesetFound || 0 < queryCount)
					{
						goto IL_0525;
					}
					if (this.ChangedEntries.Any((Descriptor o) => o.ContentGeneratedForSave && o.SaveResultWasProcessed == (EntityStates)0))
					{
						if (!this.IsBatchRequest)
						{
							goto IL_0525;
						}
						if (this.ChangedEntries.FirstOrDefault((Descriptor o) => o.SaveError != null) == null)
						{
							goto IL_0525;
						}
					}
				}
				if (this.Queries == null || queryCount == this.Queries.Length)
				{
					goto JumpOutOfTryFinally-3;
				}
				IL_0525:
				throw Error.InvalidOperation(Strings.Batch_IncompleteResponseCount);
			}
			finally
			{
				Util.Dispose<ODataMessageReader>(ref this.batchMessageReader);
			}
			JumpOutOfTryFinally-3:
			yield break;
			yield break;
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00006CDC File Offset: 0x00004EDC
		private Exception ProcessCurrentOperationResponse(ODataBatchReader batchReader, bool isChangesetOperation)
		{
			ODataBatchOperationResponseMessage odataBatchOperationResponseMessage = batchReader.CreateOperationResponseMessage();
			Descriptor descriptor = null;
			if (isChangesetOperation)
			{
				this.entryIndex = this.ValidateContentID(odataBatchOperationResponseMessage.ContentId);
				descriptor = this.ChangedEntries[this.entryIndex];
			}
			if (!WebUtil.SuccessStatusCode((HttpStatusCode)odataBatchOperationResponseMessage.StatusCode))
			{
				descriptor = null;
			}
			this.RequestInfo.Context.FireReceivingResponseEvent(new ReceivingResponseEventArgs(odataBatchOperationResponseMessage, descriptor, true));
			Stream stream = odataBatchOperationResponseMessage.GetStream();
			if (stream == null)
			{
				Error.ThrowBatchExpectedResponse(InternalError.NullResponseStream);
			}
			MemoryStream memoryStream;
			try
			{
				memoryStream = new MemoryStream();
				WebUtil.CopyStream(stream, memoryStream, ref this.streamCopyBuffer);
				memoryStream.Position = 0L;
			}
			finally
			{
				stream.Dispose();
			}
			this.currentOperationResponse = new BatchSaveResult.CurrentOperationResponse((HttpStatusCode)odataBatchOperationResponseMessage.StatusCode, odataBatchOperationResponseMessage.Headers, memoryStream);
			string text = "OData-Version";
			Version version;
			return BaseSaveResult.HandleResponse(this.RequestInfo, this.currentOperationResponse.StatusCode, this.currentOperationResponse.Headers.GetHeader(text), () => this.currentOperationResponse.ContentStream, false, out version);
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00006DDC File Offset: 0x00004FDC
		private int ValidateContentID(string contentIdStr)
		{
			int num = 0;
			if (string.IsNullOrEmpty(contentIdStr) || !int.TryParse(contentIdStr, NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out num))
			{
				Error.ThrowBatchUnexpectedContent(InternalError.ChangeResponseMissingContentID);
			}
			for (int i = 0; i < this.ChangedEntries.Count; i++)
			{
				if ((ulong)this.ChangedEntries[i].ChangeOrder == (ulong)((long)num))
				{
					return i;
				}
			}
			Error.ThrowBatchUnexpectedContent(InternalError.ChangeResponseUnknownContentID);
			return -1;
		}

		// Token: 0x04000057 RID: 87
		private const int StreamCopyBufferSize = 4000;

		// Token: 0x04000058 RID: 88
		private readonly DataServiceRequest[] Queries;

		// Token: 0x04000059 RID: 89
		private Stream responseStream;

		// Token: 0x0400005A RID: 90
		private ODataBatchWriter batchWriter;

		// Token: 0x0400005B RID: 91
		private ODataMessageReader batchMessageReader;

		// Token: 0x0400005C RID: 92
		private BatchSaveResult.CurrentOperationResponse currentOperationResponse;

		// Token: 0x0400005D RID: 93
		private byte[] streamCopyBuffer;

		// Token: 0x02000153 RID: 339
		private sealed class CurrentOperationResponse
		{
			// Token: 0x06000D1B RID: 3355 RVA: 0x0002DD3C File Offset: 0x0002BF3C
			public CurrentOperationResponse(HttpStatusCode statusCode, IEnumerable<KeyValuePair<string, string>> headers, MemoryStream contentStream)
			{
				this.statusCode = statusCode;
				this.contentStream = contentStream;
				this.headers = new HeaderCollection();
				foreach (KeyValuePair<string, string> keyValuePair in headers)
				{
					this.headers.SetHeader(keyValuePair.Key, keyValuePair.Value);
				}
			}

			// Token: 0x17000340 RID: 832
			// (get) Token: 0x06000D1C RID: 3356 RVA: 0x0002DDB8 File Offset: 0x0002BFB8
			public HttpStatusCode StatusCode
			{
				get
				{
					return this.statusCode;
				}
			}

			// Token: 0x17000341 RID: 833
			// (get) Token: 0x06000D1D RID: 3357 RVA: 0x0002DDC0 File Offset: 0x0002BFC0
			public Stream ContentStream
			{
				get
				{
					return this.contentStream;
				}
			}

			// Token: 0x17000342 RID: 834
			// (get) Token: 0x06000D1E RID: 3358 RVA: 0x0002DDC8 File Offset: 0x0002BFC8
			public bool HasEmptyContent
			{
				get
				{
					return this.contentStream.Length == 0L;
				}
			}

			// Token: 0x17000343 RID: 835
			// (get) Token: 0x06000D1F RID: 3359 RVA: 0x0002DDD9 File Offset: 0x0002BFD9
			public HeaderCollection Headers
			{
				get
				{
					return this.headers;
				}
			}

			// Token: 0x06000D20 RID: 3360 RVA: 0x0002DDE1 File Offset: 0x0002BFE1
			public IODataResponseMessage CreateResponseMessage()
			{
				if (!this.HasEmptyContent)
				{
					return new HttpWebResponseMessage(this.headers, (int)this.statusCode, () => this.contentStream);
				}
				return null;
			}

			// Token: 0x040006DD RID: 1757
			private readonly HttpStatusCode statusCode;

			// Token: 0x040006DE RID: 1758
			private readonly HeaderCollection headers;

			// Token: 0x040006DF RID: 1759
			private readonly MemoryStream contentStream;
		}
	}
}
