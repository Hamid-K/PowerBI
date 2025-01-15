using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Microsoft.OData.Client.Materialization;
using Microsoft.OData.Client.Metadata;

namespace Microsoft.OData.Client
{
	// Token: 0x020000DD RID: 221
	[SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable", Justification = "Pending")]
	internal class SaveResult : BaseSaveResult
	{
		// Token: 0x0600075C RID: 1884 RVA: 0x0001DC42 File Offset: 0x0001BE42
		internal SaveResult(DataServiceContext context, string method, SaveChangesOptions options, AsyncCallback callback, object state)
			: base(context, method, null, options, callback, state)
		{
			this.cachedResponses = new List<SaveResult.CachedResponse>();
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x0600075D RID: 1885 RVA: 0x00015066 File Offset: 0x00013266
		internal override bool IsBatchRequest
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x0600075E RID: 1886 RVA: 0x0001DC5D File Offset: 0x0001BE5D
		protected override bool ProcessResponsePayload
		{
			get
			{
				return this.cachedResponse.MaterializerEntry != null;
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x0600075F RID: 1887 RVA: 0x0001DC6D File Offset: 0x0001BE6D
		protected override Stream ResponseStream
		{
			get
			{
				return this.inMemoryResponseStream;
			}
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x0001DC78 File Offset: 0x0001BE78
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Pending")]
		internal void BeginCreateNextChange()
		{
			this.inMemoryResponseStream = new MemoryStream();
			BaseAsyncResult.PerRequest perRequest = null;
			for (;;)
			{
				IODataResponseMessage iodataResponseMessage = null;
				try
				{
					if (this.perRequest != null)
					{
						base.SetCompleted();
						Error.ThrowInternalError(InternalError.InvalidBeginNextChange);
					}
					ODataRequestMessageWrapper odataRequestMessageWrapper = this.CreateNextRequest();
					if (odataRequestMessageWrapper == null)
					{
						base.Abortable = null;
					}
					if (odataRequestMessageWrapper != null || this.entryIndex < this.ChangedEntries.Count)
					{
						if (this.ChangedEntries[this.entryIndex].ContentGeneratedForSave)
						{
							goto IL_0181;
						}
						base.Abortable = odataRequestMessageWrapper;
						ContentStream contentStream = this.CreateNonBatchChangeData(this.entryIndex, odataRequestMessageWrapper);
						perRequest = (this.perRequest = new BaseAsyncResult.PerRequest());
						perRequest.Request = odataRequestMessageWrapper;
						BaseAsyncResult.AsyncStateBag asyncStateBag = new BaseAsyncResult.AsyncStateBag(perRequest);
						IAsyncResult asyncResult;
						if (contentStream == null || contentStream.Stream == null)
						{
							asyncResult = BaseAsyncResult.InvokeAsync(new Func<AsyncCallback, object, IAsyncResult>(odataRequestMessageWrapper.BeginGetResponse), new AsyncCallback(this.AsyncEndGetResponse), asyncStateBag);
						}
						else
						{
							if (contentStream.IsKnownMemoryStream)
							{
								odataRequestMessageWrapper.SetContentLengthHeader();
							}
							perRequest.RequestContentStream = contentStream;
							asyncResult = BaseAsyncResult.InvokeAsync(new Func<AsyncCallback, object, IAsyncResult>(odataRequestMessageWrapper.BeginGetRequestStream), new AsyncCallback(base.AsyncEndGetRequestStream), asyncStateBag);
						}
						perRequest.SetRequestCompletedSynchronously(asyncResult.CompletedSynchronously);
						base.SetCompletedSynchronously(perRequest.RequestCompletedSynchronously);
					}
					else
					{
						base.SetCompleted();
						if (base.CompletedSynchronously)
						{
							this.HandleCompleted(perRequest);
						}
					}
				}
				catch (InvalidOperationException httpWebResponse)
				{
					httpWebResponse = WebUtil.GetHttpWebResponse(httpWebResponse, ref iodataResponseMessage);
					this.HandleOperationException(httpWebResponse, iodataResponseMessage);
					this.HandleCompleted(perRequest);
				}
				finally
				{
					WebUtil.DisposeMessage(iodataResponseMessage);
				}
				goto IL_015F;
				IL_0181:
				if ((perRequest != null && (!perRequest.RequestCompleted || !perRequest.RequestCompletedSynchronously)) || base.IsCompletedInternally)
				{
					break;
				}
				continue;
				IL_015F:
				if (perRequest != null && perRequest.RequestCompleted && perRequest.RequestCompletedSynchronously && !base.IsCompletedInternally)
				{
					this.FinishCurrentChange(perRequest);
					goto IL_0181;
				}
				goto IL_0181;
			}
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x0001DE58 File Offset: 0x0001C058
		internal void CreateNextChange()
		{
			do
			{
				IODataResponseMessage iodataResponseMessage = null;
				try
				{
					ODataRequestMessageWrapper odataRequestMessageWrapper = this.CreateNextRequest();
					if (odataRequestMessageWrapper != null || this.entryIndex < this.ChangedEntries.Count)
					{
						if (!this.ChangedEntries[this.entryIndex].ContentGeneratedForSave)
						{
							ContentStream contentStream = this.CreateNonBatchChangeData(this.entryIndex, odataRequestMessageWrapper);
							if (contentStream != null && contentStream.Stream != null)
							{
								odataRequestMessageWrapper.SetRequestStream(contentStream);
							}
							iodataResponseMessage = this.RequestInfo.GetSyncronousResponse(odataRequestMessageWrapper, false);
							this.HandleOperationResponse(iodataResponseMessage);
							base.HandleOperationResponseHeaders((HttpStatusCode)iodataResponseMessage.StatusCode, new HeaderCollection(iodataResponseMessage));
							this.HandleOperationResponseData(iodataResponseMessage);
							this.perRequest = null;
						}
					}
				}
				catch (InvalidOperationException httpWebResponse)
				{
					httpWebResponse = WebUtil.GetHttpWebResponse(httpWebResponse, ref iodataResponseMessage);
					this.HandleOperationException(httpWebResponse, iodataResponseMessage);
				}
				finally
				{
					WebUtil.DisposeMessage(iodataResponseMessage);
				}
			}
			while (this.entryIndex < this.ChangedEntries.Count && !base.IsCompletedInternally);
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x0001DF4C File Offset: 0x0001C14C
		protected override void FinishCurrentChange(BaseAsyncResult.PerRequest pereq)
		{
			base.FinishCurrentChange(pereq);
			if (this.ResponseStream.Position != 0L)
			{
				this.ResponseStream.Position = 0L;
				this.HandleOperationResponseData(this.responseMessage, this.ResponseStream);
			}
			else
			{
				this.HandleOperationResponseData(this.responseMessage, null);
			}
			pereq.Dispose();
			this.perRequest = null;
			if (!pereq.RequestCompletedSynchronously && !base.IsCompletedInternally)
			{
				this.BeginCreateNextChange();
			}
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x0001DFBE File Offset: 0x0001C1BE
		protected override void HandleOperationResponse(IODataResponseMessage responseMsg)
		{
			this.responseMessage = responseMsg;
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x0001DFC8 File Offset: 0x0001C1C8
		protected override DataServiceResponse HandleResponse()
		{
			List<OperationResponse> list = new List<OperationResponse>((this.cachedResponses != null) ? this.cachedResponses.Count : 0);
			DataServiceResponse dataServiceResponse = new DataServiceResponse(null, -1, list, false);
			Exception ex = null;
			try
			{
				foreach (SaveResult.CachedResponse cachedResponse in this.cachedResponses)
				{
					Descriptor descriptor = cachedResponse.Descriptor;
					base.SaveResultProcessed(descriptor);
					OperationResponse operationResponse = new ChangeOperationResponse(cachedResponse.Headers, descriptor);
					operationResponse.StatusCode = (int)cachedResponse.StatusCode;
					if (cachedResponse.Exception != null)
					{
						operationResponse.Error = cachedResponse.Exception;
						if (ex == null)
						{
							ex = cachedResponse.Exception;
						}
					}
					else
					{
						this.cachedResponse = cachedResponse;
						base.HandleOperationResponse(descriptor, cachedResponse.Headers);
					}
					list.Add(operationResponse);
				}
			}
			catch (InvalidOperationException ex2)
			{
				ex = ex2;
			}
			if (ex != null)
			{
				throw new DataServiceRequestException(Strings.DataServiceException_GeneralError, ex, dataServiceResponse);
			}
			return dataServiceResponse;
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x0001E0DC File Offset: 0x0001C2DC
		protected override MaterializeAtom GetMaterializer(EntityDescriptor entityDescriptor, ResponseInfo responseInfo)
		{
			ODataResource odataResource = ((this.cachedResponse.MaterializerEntry == null) ? null : this.cachedResponse.MaterializerEntry.Entry);
			return new MaterializeAtom(responseInfo, new ODataResource[] { odataResource }, entityDescriptor.Entity.GetType(), this.cachedResponse.MaterializerEntry.Format);
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x0001E135 File Offset: 0x0001C335
		protected override ODataRequestMessageWrapper CreateRequestMessage(string method, Uri requestUri, HeaderCollection headers, HttpStack httpStack, Descriptor descriptor, string contentId)
		{
			return base.CreateTopLevelRequest(method, requestUri, headers, httpStack, descriptor);
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x0001E144 File Offset: 0x0001C344
		protected ContentStream CreateNonBatchChangeData(int index, ODataRequestMessageWrapper requestMessage)
		{
			Descriptor descriptor = this.ChangedEntries[index];
			if (descriptor.DescriptorKind == DescriptorKind.Entity && this.streamRequestKind != BaseSaveResult.StreamRequestKind.None)
			{
				if (this.streamRequestKind != BaseSaveResult.StreamRequestKind.None)
				{
					return new ContentStream(this.mediaResourceRequestStream, false);
				}
			}
			else
			{
				if (descriptor.DescriptorKind == DescriptorKind.NamedStream)
				{
					descriptor.ContentGeneratedForSave = true;
					return new ContentStream(this.mediaResourceRequestStream, false);
				}
				if (base.CreateChangeData(index, requestMessage))
				{
					return requestMessage.CachedRequestStream;
				}
			}
			return null;
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x0001E1B4 File Offset: 0x0001C3B4
		private ODataRequestMessageWrapper CreateNextRequest()
		{
			bool flag = this.streamRequestKind == BaseSaveResult.StreamRequestKind.None;
			if (this.entryIndex < this.ChangedEntries.Count)
			{
				Descriptor descriptor = this.ChangedEntries[this.entryIndex];
				if (descriptor.DescriptorKind == DescriptorKind.Entity)
				{
					EntityDescriptor entityDescriptor = (EntityDescriptor)descriptor;
					entityDescriptor.CloseSaveStream();
					if (this.streamRequestKind == BaseSaveResult.StreamRequestKind.PutMediaResource && EntityStates.Unchanged == entityDescriptor.State)
					{
						entityDescriptor.ContentGeneratedForSave = true;
						flag = true;
					}
				}
				else if (descriptor.DescriptorKind == DescriptorKind.NamedStream)
				{
					((StreamDescriptor)descriptor).CloseSaveStream();
				}
			}
			if (flag)
			{
				this.entryIndex++;
			}
			ODataRequestMessageWrapper odataRequestMessageWrapper = null;
			if (this.entryIndex < this.ChangedEntries.Count)
			{
				Descriptor descriptor2 = this.ChangedEntries[this.entryIndex];
				Descriptor descriptor3 = descriptor2;
				if (descriptor2.DescriptorKind == DescriptorKind.Entity)
				{
					EntityDescriptor entityDescriptor2 = (EntityDescriptor)descriptor2;
					if ((EntityStates.Unchanged == descriptor2.State || EntityStates.Modified == descriptor2.State) && (odataRequestMessageWrapper = this.CheckAndProcessMediaEntryPut(entityDescriptor2)) != null)
					{
						this.streamRequestKind = BaseSaveResult.StreamRequestKind.PutMediaResource;
						descriptor3 = entityDescriptor2.DefaultStreamDescriptor;
					}
					else if (EntityStates.Added == descriptor2.State && (odataRequestMessageWrapper = this.CheckAndProcessMediaEntryPost(entityDescriptor2)) != null)
					{
						this.streamRequestKind = BaseSaveResult.StreamRequestKind.PostMediaResource;
						entityDescriptor2.StreamState = EntityStates.Added;
					}
					else
					{
						this.streamRequestKind = BaseSaveResult.StreamRequestKind.None;
						odataRequestMessageWrapper = base.CreateRequest(entityDescriptor2);
					}
				}
				else if (descriptor2.DescriptorKind == DescriptorKind.NamedStream)
				{
					odataRequestMessageWrapper = this.CreateNamedStreamRequest((StreamDescriptor)descriptor2);
				}
				else
				{
					odataRequestMessageWrapper = base.CreateRequest((LinkDescriptor)descriptor2);
				}
				if (odataRequestMessageWrapper != null)
				{
					odataRequestMessageWrapper.FireSendingEventHandlers(descriptor3);
				}
			}
			return odataRequestMessageWrapper;
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x0001E328 File Offset: 0x0001C528
		private ODataRequestMessageWrapper CheckAndProcessMediaEntryPost(EntityDescriptor entityDescriptor)
		{
			ClientEdmModel model = this.RequestInfo.Model;
			ClientTypeAnnotation clientTypeAnnotation = model.GetClientTypeAnnotation(model.GetOrCreateEdmType(entityDescriptor.Entity.GetType()));
			if (!clientTypeAnnotation.IsMediaLinkEntry && !entityDescriptor.IsMediaLinkEntry)
			{
				return null;
			}
			if (clientTypeAnnotation.MediaDataMember == null && entityDescriptor.SaveStream == null)
			{
				throw Error.InvalidOperation(Strings.Context_MLEWithoutSaveStream(clientTypeAnnotation.ElementTypeName));
			}
			ODataRequestMessageWrapper odataRequestMessageWrapper;
			if (clientTypeAnnotation.MediaDataMember != null)
			{
				int num = 0;
				string text;
				if (clientTypeAnnotation.MediaDataMember.MimeTypeProperty == null)
				{
					text = "application/octet-stream";
				}
				else
				{
					object value = clientTypeAnnotation.MediaDataMember.MimeTypeProperty.GetValue(entityDescriptor.Entity);
					string text2 = ((value != null) ? value.ToString() : null);
					if (string.IsNullOrEmpty(text2))
					{
						throw Error.InvalidOperation(Strings.Context_NoContentTypeForMediaLink(clientTypeAnnotation.ElementTypeName, clientTypeAnnotation.MediaDataMember.MimeTypeProperty.PropertyName));
					}
					text = text2;
				}
				object value2 = clientTypeAnnotation.MediaDataMember.GetValue(entityDescriptor.Entity);
				if (value2 == null)
				{
					this.mediaResourceRequestStream = null;
				}
				else
				{
					byte[] array = value2 as byte[];
					if (array == null)
					{
						string text3;
						Encoding utf;
						ContentTypeUtil.ReadContentType(text, out text3, out utf);
						if (utf == null)
						{
							utf = Encoding.UTF8;
							text += ";charset=UTF-8";
						}
						array = utf.GetBytes(ClientConvert.ToString(value2));
					}
					num = array.Length;
					this.mediaResourceRequestStream = new MemoryStream(array, 0, array.Length, false, true);
				}
				HeaderCollection headerCollection = new HeaderCollection();
				headerCollection.SetHeader("Content-Length", num.ToString(CultureInfo.InvariantCulture));
				headerCollection.SetHeader("Content-Type", text);
				odataRequestMessageWrapper = this.CreateMediaResourceRequest(entityDescriptor.GetResourceUri(this.RequestInfo.BaseUriResolver, false), "POST", Util.ODataVersion4, clientTypeAnnotation.MediaDataMember == null, true, headerCollection, entityDescriptor);
			}
			else
			{
				HeaderCollection headerCollection2 = new HeaderCollection();
				this.SetupMediaResourceRequest(headerCollection2, entityDescriptor.SaveStream, null);
				odataRequestMessageWrapper = this.CreateMediaResourceRequest(entityDescriptor.GetResourceUri(this.RequestInfo.BaseUriResolver, false), "POST", Util.ODataVersion4, clientTypeAnnotation.MediaDataMember == null, true, headerCollection2, entityDescriptor);
			}
			entityDescriptor.State = EntityStates.Modified;
			return odataRequestMessageWrapper;
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x0001E530 File Offset: 0x0001C730
		private ODataRequestMessageWrapper CheckAndProcessMediaEntryPut(EntityDescriptor entityDescriptor)
		{
			if (entityDescriptor.SaveStream == null)
			{
				return null;
			}
			Uri latestEditStreamUri = entityDescriptor.GetLatestEditStreamUri();
			if (latestEditStreamUri == null)
			{
				throw Error.InvalidOperation(Strings.Context_SetSaveStreamWithoutEditMediaLink);
			}
			HeaderCollection headerCollection = new HeaderCollection();
			this.SetupMediaResourceRequest(headerCollection, entityDescriptor.SaveStream, entityDescriptor.GetLatestStreamETag());
			return this.CreateMediaResourceRequest(latestEditStreamUri, "PUT", Util.ODataVersion4, true, false, headerCollection, entityDescriptor.DefaultStreamDescriptor);
		}

		// Token: 0x0600076B RID: 1899 RVA: 0x0001E598 File Offset: 0x0001C798
		private ODataRequestMessageWrapper CreateMediaResourceRequest(Uri requestUri, string method, Version version, bool sendChunked, bool applyResponsePreference, HeaderCollection headers, Descriptor descriptor)
		{
			headers.SetHeaderIfUnset("Content-Type", "*/*");
			if (applyResponsePreference)
			{
				BaseSaveResult.ApplyPreferences(headers, method, this.RequestInfo.AddAndUpdateResponsePreference, ref version);
			}
			headers.SetRequestVersion(version, this.RequestInfo.MaxProtocolVersionAsVersion);
			this.RequestInfo.Format.SetRequestAcceptHeader(headers);
			ODataRequestMessageWrapper odataRequestMessageWrapper = this.CreateRequestMessage(method, requestUri, headers, this.RequestInfo.HttpStack, descriptor, null);
			odataRequestMessageWrapper.SendChunked = sendChunked;
			return odataRequestMessageWrapper;
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x0001E618 File Offset: 0x0001C818
		private void SetupMediaResourceRequest(HeaderCollection headers, DataServiceSaveStream saveStream, string etag)
		{
			this.mediaResourceRequestStream = saveStream.Stream;
			headers.SetHeaders(saveStream.Args.Headers.Where((KeyValuePair<string, string> h) => !string.Equals(h.Key, "Accept", StringComparison.OrdinalIgnoreCase)));
			if (etag != null)
			{
				headers.SetHeader("If-Match", etag);
			}
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x0001E678 File Offset: 0x0001C878
		private void HandleOperationException(InvalidOperationException e, IODataResponseMessage response)
		{
			Descriptor descriptor = this.ChangedEntries[this.entryIndex];
			HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError;
			Version version = null;
			HeaderCollection headerCollection;
			if (response != null)
			{
				headerCollection = new HeaderCollection(response);
				httpStatusCode = (HttpStatusCode)response.StatusCode;
				base.HandleOperationResponseHeaders(httpStatusCode, headerCollection);
				e = BaseSaveResult.HandleResponse(this.RequestInfo, httpStatusCode, response.GetHeader("OData-Version"), new Func<Stream>(response.GetStream), false, out version);
			}
			else
			{
				headerCollection = new HeaderCollection();
				headerCollection.SetHeader("Content-Type", "text/plain");
				if (e.GetType() != typeof(DataServiceClientException))
				{
					e = new DataServiceClientException(e.Message, e);
				}
			}
			this.cachedResponses.Add(new SaveResult.CachedResponse(descriptor, headerCollection, httpStatusCode, version, null, e));
			this.perRequest = null;
			this.CheckContinueOnError();
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x0001E743 File Offset: 0x0001C943
		private void CheckContinueOnError()
		{
			if (!Util.IsFlagSet(this.Options, SaveChangesOptions.ContinueOnError))
			{
				base.SetCompleted();
				return;
			}
			this.streamRequestKind = BaseSaveResult.StreamRequestKind.None;
			this.ChangedEntries[this.entryIndex].ContentGeneratedForSave = true;
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x0001E778 File Offset: 0x0001C978
		private void HandleOperationResponseData(IODataResponseMessage response)
		{
			using (Stream stream = response.GetStream())
			{
				if (stream != null)
				{
					using (MemoryStream memoryStream = new MemoryStream())
					{
						if (WebUtil.CopyStream(stream, memoryStream, ref this.buildBatchBuffer) != 0L)
						{
							memoryStream.Position = 0L;
							this.HandleOperationResponseData(response, memoryStream);
						}
						else
						{
							this.HandleOperationResponseData(response, null);
						}
					}
				}
			}
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x0001E7F4 File Offset: 0x0001C9F4
		private void HandleOperationResponseData(IODataResponseMessage responseMsg, Stream responseStream)
		{
			Descriptor descriptor = this.ChangedEntries[this.entryIndex];
			MaterializerEntry materializerEntry = null;
			Version version;
			Exception ex = BaseSaveResult.HandleResponse(this.RequestInfo, (HttpStatusCode)responseMsg.StatusCode, responseMsg.GetHeader("OData-Version"), () => responseStream, false, out version);
			HeaderCollection headerCollection = new HeaderCollection(responseMsg);
			if (responseStream != null && descriptor.DescriptorKind == DescriptorKind.Entity && ex == null)
			{
				EntityDescriptor entityDescriptor = (EntityDescriptor)descriptor;
				if (entityDescriptor.State == EntityStates.Added || entityDescriptor.StreamState == EntityStates.Added || entityDescriptor.State == EntityStates.Modified || entityDescriptor.StreamState == EntityStates.Modified)
				{
					try
					{
						ResponseInfo responseInfo = base.CreateResponseInfo(entityDescriptor);
						HttpWebResponseMessage httpWebResponseMessage = new HttpWebResponseMessage(headerCollection, responseMsg.StatusCode, () => responseStream);
						materializerEntry = ODataReaderEntityMaterializer.ParseSingleEntityPayload(httpWebResponseMessage, responseInfo, entityDescriptor.Entity.GetType());
						entityDescriptor.TransientEntityDescriptor = materializerEntry.EntityDescriptor;
					}
					catch (Exception ex2)
					{
						ex = ex2;
						if (!CommonUtil.IsCatchableExceptionType(ex2))
						{
							throw;
						}
					}
				}
			}
			this.cachedResponses.Add(new SaveResult.CachedResponse(descriptor, headerCollection, (HttpStatusCode)responseMsg.StatusCode, version, materializerEntry, ex));
			if (ex != null)
			{
				descriptor.SaveError = ex;
			}
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x0001E940 File Offset: 0x0001CB40
		private ODataRequestMessageWrapper CreateNamedStreamRequest(StreamDescriptor namedStreamInfo)
		{
			Uri latestEditLink = namedStreamInfo.GetLatestEditLink();
			if (latestEditLink == null)
			{
				throw Error.InvalidOperation(Strings.Context_SetSaveStreamWithoutNamedStreamEditLink(namedStreamInfo.Name));
			}
			HeaderCollection headerCollection = new HeaderCollection();
			this.SetupMediaResourceRequest(headerCollection, namedStreamInfo.SaveStream, namedStreamInfo.GetLatestETag());
			return this.CreateMediaResourceRequest(latestEditLink, "PUT", Util.ODataVersion4, true, false, headerCollection, namedStreamInfo);
		}

		// Token: 0x04000354 RID: 852
		private readonly List<SaveResult.CachedResponse> cachedResponses;

		// Token: 0x04000355 RID: 853
		private MemoryStream inMemoryResponseStream;

		// Token: 0x04000356 RID: 854
		private IODataResponseMessage responseMessage;

		// Token: 0x04000357 RID: 855
		private SaveResult.CachedResponse cachedResponse;

		// Token: 0x020001A4 RID: 420
		private struct CachedResponse
		{
			// Token: 0x06000EA9 RID: 3753 RVA: 0x00031963 File Offset: 0x0002FB63
			internal CachedResponse(Descriptor descriptor, HeaderCollection headers, HttpStatusCode statusCode, Version responseVersion, MaterializerEntry entry, Exception exception)
			{
				this.Descriptor = descriptor;
				this.MaterializerEntry = entry;
				this.Exception = exception;
				this.Headers = headers;
				this.StatusCode = statusCode;
				this.Version = responseVersion;
			}

			// Token: 0x040007AC RID: 1964
			public readonly HeaderCollection Headers;

			// Token: 0x040007AD RID: 1965
			public readonly HttpStatusCode StatusCode;

			// Token: 0x040007AE RID: 1966
			public readonly Version Version;

			// Token: 0x040007AF RID: 1967
			public readonly MaterializerEntry MaterializerEntry;

			// Token: 0x040007B0 RID: 1968
			public readonly Exception Exception;

			// Token: 0x040007B1 RID: 1969
			public readonly Descriptor Descriptor;
		}
	}
}
