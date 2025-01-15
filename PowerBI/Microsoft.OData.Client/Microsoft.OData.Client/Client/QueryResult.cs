using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Net;
using System.Threading;

namespace Microsoft.OData.Client
{
	// Token: 0x02000095 RID: 149
	internal class QueryResult : BaseAsyncResult
	{
		// Token: 0x06000473 RID: 1139 RVA: 0x0000FBD4 File Offset: 0x0000DDD4
		internal QueryResult(object source, string method, DataServiceRequest serviceRequest, ODataRequestMessageWrapper request, RequestInfo requestInfo, AsyncCallback callback, object state)
			: base(source, method, callback, state)
		{
			this.ServiceRequest = serviceRequest;
			this.Request = request;
			this.RequestInfo = requestInfo;
			base.Abortable = request;
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x0000FC01 File Offset: 0x0000DE01
		internal QueryResult(object source, string method, DataServiceRequest serviceRequest, ODataRequestMessageWrapper request, RequestInfo requestInfo, AsyncCallback callback, object state, ContentStream requestContentStream)
			: this(source, method, serviceRequest, request, requestInfo, callback, state)
		{
			this.requestContentStream = requestContentStream;
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000475 RID: 1141 RVA: 0x0000FC1C File Offset: 0x0000DE1C
		internal long ContentLength
		{
			get
			{
				return this.contentLength;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000476 RID: 1142 RVA: 0x0000FC24 File Offset: 0x0000DE24
		internal string ContentType
		{
			get
			{
				return this.contentType;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000477 RID: 1143 RVA: 0x0000FC2C File Offset: 0x0000DE2C
		internal HttpStatusCode StatusCode
		{
			get
			{
				return this.statusCode;
			}
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x0000FC34 File Offset: 0x0000DE34
		internal static QueryResult EndExecuteQuery<TElement>(object source, string method, IAsyncResult asyncResult)
		{
			QueryResult queryResult = null;
			try
			{
				queryResult = BaseAsyncResult.EndExecute<QueryResult>(source, method, asyncResult);
			}
			catch (InvalidOperationException ex)
			{
				queryResult = asyncResult as QueryResult;
				QueryOperationResponse response = queryResult.GetResponse<TElement>(MaterializeAtom.EmptyResults);
				if (response != null)
				{
					response.Error = ex;
					throw new DataServiceQueryException(Strings.DataServiceException_GeneralError, ex, response);
				}
				throw;
			}
			return queryResult;
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x0000FC8C File Offset: 0x0000DE8C
		internal Stream GetResponseStream()
		{
			return this.outputResponseStream;
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x0000FC94 File Offset: 0x0000DE94
		internal void BeginExecuteQuery()
		{
			BaseAsyncResult.PerRequest perRequest = new BaseAsyncResult.PerRequest();
			BaseAsyncResult.AsyncStateBag asyncStateBag = new BaseAsyncResult.AsyncStateBag(perRequest);
			perRequest.Request = this.Request;
			this.perRequest = perRequest;
			try
			{
				IAsyncResult asyncResult;
				if (this.requestContentStream != null && this.requestContentStream.Stream != null)
				{
					if (this.requestContentStream.IsKnownMemoryStream)
					{
						this.Request.SetContentLengthHeader();
					}
					this.perRequest.RequestContentStream = this.requestContentStream;
					asyncResult = BaseAsyncResult.InvokeAsync(new Func<AsyncCallback, object, IAsyncResult>(this.Request.BeginGetRequestStream), new AsyncCallback(base.AsyncEndGetRequestStream), asyncStateBag);
				}
				else
				{
					asyncResult = BaseAsyncResult.InvokeAsync(new Func<AsyncCallback, object, IAsyncResult>(this.Request.BeginGetResponse), new AsyncCallback(this.AsyncEndGetResponse), asyncStateBag);
				}
				perRequest.SetRequestCompletedSynchronously(asyncResult.CompletedSynchronously);
				base.SetCompletedSynchronously(asyncResult.CompletedSynchronously);
			}
			catch (Exception ex)
			{
				base.HandleFailure(ex);
				throw;
			}
			finally
			{
				this.HandleCompleted(perRequest);
			}
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x0000FD98 File Offset: 0x0000DF98
		internal void ExecuteQuery()
		{
			try
			{
				if (this.requestContentStream != null && this.requestContentStream.Stream != null)
				{
					this.Request.SetRequestStream(this.requestContentStream);
				}
				IODataResponseMessage syncronousResponse = this.RequestInfo.GetSyncronousResponse(this.Request, true);
				this.SetHttpWebResponse(Util.NullCheck<IODataResponseMessage>(syncronousResponse, InternalError.InvalidGetResponse));
				if (HttpStatusCode.NoContent != this.StatusCode)
				{
					using (Stream stream = this.responseMessage.GetStream())
					{
						if (stream != null)
						{
							Stream asyncResponseStreamCopy = this.GetAsyncResponseStreamCopy();
							this.outputResponseStream = asyncResponseStreamCopy;
							byte[] asyncResponseStreamCopyBuffer = this.GetAsyncResponseStreamCopyBuffer();
							long num = WebUtil.CopyStream(stream, asyncResponseStreamCopy, ref asyncResponseStreamCopyBuffer);
							if (this.responseStreamOwner)
							{
								if (num == 0L)
								{
									this.outputResponseStream = null;
								}
								else if (asyncResponseStreamCopy.Position < asyncResponseStreamCopy.Length)
								{
									((MemoryStream)asyncResponseStreamCopy).SetLength(asyncResponseStreamCopy.Position);
								}
							}
							this.PutAsyncResponseStreamCopyBuffer(asyncResponseStreamCopyBuffer);
						}
					}
				}
			}
			catch (Exception ex)
			{
				base.HandleFailure(ex);
				throw;
			}
			finally
			{
				base.SetCompleted();
				this.CompletedRequest();
			}
			if (base.Failure != null)
			{
				throw base.Failure;
			}
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x0000FEC4 File Offset: 0x0000E0C4
		internal QueryOperationResponse<TElement> GetResponse<TElement>(MaterializeAtom results)
		{
			if (this.responseMessage != null)
			{
				HeaderCollection headerCollection = new HeaderCollection(this.responseMessage);
				return new QueryOperationResponse<TElement>(headerCollection, this.ServiceRequest, results)
				{
					StatusCode = this.responseMessage.StatusCode
				};
			}
			return null;
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x0000FF08 File Offset: 0x0000E108
		internal QueryOperationResponse GetResponseWithType(MaterializeAtom results, Type elementType)
		{
			if (this.responseMessage != null)
			{
				HeaderCollection headerCollection = new HeaderCollection(this.responseMessage);
				QueryOperationResponse instance = QueryOperationResponse.GetInstance(elementType, headerCollection, this.ServiceRequest, results);
				instance.StatusCode = this.responseMessage.StatusCode;
				return instance;
			}
			return null;
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x0000FF4C File Offset: 0x0000E14C
		internal MaterializeAtom GetMaterializer(ProjectionPlan plan)
		{
			MaterializeAtom materializeAtom;
			if (HttpStatusCode.NoContent != this.StatusCode)
			{
				materializeAtom = this.CreateMaterializer(plan, ODataPayloadKind.Unsupported);
			}
			else
			{
				materializeAtom = MaterializeAtom.EmptyResults;
			}
			return materializeAtom;
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x0000FF7C File Offset: 0x0000E17C
		internal QueryOperationResponse<TElement> ProcessResult<TElement>(ProjectionPlan plan)
		{
			MaterializeAtom materializeAtom = this.CreateMaterializer(plan, this.ServiceRequest.PayloadKind);
			QueryOperationResponse<TElement> response = this.GetResponse<TElement>(materializeAtom);
			materializeAtom.SetInstanceAnnotations = delegate(IDictionary<string, object> instanceAnnotations)
			{
				if (!this.responseInfo.Context.InstanceAnnotations.ContainsKey(response) && instanceAnnotations != null && instanceAnnotations.Count > 0)
				{
					this.responseInfo.Context.InstanceAnnotations.Add(response, instanceAnnotations);
				}
			};
			return response;
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x0000FFD0 File Offset: 0x0000E1D0
		protected override void CompletedRequest()
		{
			byte[] array = this.asyncStreamCopyBuffer;
			this.asyncStreamCopyBuffer = null;
			if (array != null && !this.usingBuffer)
			{
				this.PutAsyncResponseStreamCopyBuffer(array);
			}
			if (this.responseStreamOwner && this.outputResponseStream != null)
			{
				this.outputResponseStream.Position = 0L;
			}
			if (this.responseMessage != null)
			{
				WebUtil.DisposeMessage(this.responseMessage);
				Version version;
				Exception ex = BaseSaveResult.HandleResponse(this.RequestInfo, this.StatusCode, this.responseMessage.GetHeader("OData-Version"), new Func<Stream>(this.GetResponseStream), false, out version);
				if (ex != null)
				{
					base.HandleFailure(ex);
					return;
				}
				this.responseInfo = this.CreateResponseInfo();
			}
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x00010078 File Offset: 0x0000E278
		protected virtual ResponseInfo CreateResponseInfo()
		{
			return this.RequestInfo.GetDeserializationInfo(null);
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x0001009C File Offset: 0x0000E29C
		protected virtual Stream GetAsyncResponseStreamCopy()
		{
			this.responseStreamOwner = true;
			long num = this.contentLength;
			if (0L < num && num <= 2147483647L)
			{
				return new MemoryStream((int)num);
			}
			return new MemoryStream();
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x000100D2 File Offset: 0x0000E2D2
		protected virtual byte[] GetAsyncResponseStreamCopyBuffer()
		{
			return Interlocked.Exchange<byte[]>(ref QueryResult.reusableAsyncCopyBuffer, null) ?? new byte[8000];
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x000100ED File Offset: 0x0000E2ED
		protected virtual void PutAsyncResponseStreamCopyBuffer(byte[] buffer)
		{
			QueryResult.reusableAsyncCopyBuffer = buffer;
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x000100F8 File Offset: 0x0000E2F8
		protected virtual void SetHttpWebResponse(IODataResponseMessage response)
		{
			this.responseMessage = response;
			this.statusCode = (HttpStatusCode)response.StatusCode;
			string header = response.GetHeader("Content-Length");
			if (header != null)
			{
				this.contentLength = (long)int.Parse(header, CultureInfo.InvariantCulture);
			}
			else
			{
				this.contentLength = -1L;
			}
			this.contentType = response.GetHeader("Content-Type");
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x00010154 File Offset: 0x0000E354
		protected override void HandleCompleted(BaseAsyncResult.PerRequest pereq)
		{
			if (pereq != null)
			{
				base.SetCompletedSynchronously(pereq.RequestCompletedSynchronously);
				if (pereq.RequestCompleted)
				{
					Interlocked.CompareExchange<BaseAsyncResult.PerRequest>(ref this.perRequest, null, pereq);
					pereq.Dispose();
				}
			}
			base.HandleCompleted();
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x00010188 File Offset: 0x0000E388
		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "required for this feature")]
		protected override void AsyncEndGetResponse(IAsyncResult asyncResult)
		{
			BaseAsyncResult.AsyncStateBag asyncStateBag = asyncResult.AsyncState as BaseAsyncResult.AsyncStateBag;
			BaseAsyncResult.PerRequest perRequest = ((asyncStateBag == null) ? null : asyncStateBag.PerRequest);
			try
			{
				if (base.IsAborted)
				{
					if (perRequest != null)
					{
						perRequest.SetComplete();
					}
					base.SetCompleted();
				}
				else
				{
					this.CompleteCheck(perRequest, InternalError.InvalidEndGetResponseCompleted);
					perRequest.SetRequestCompletedSynchronously(asyncResult.CompletedSynchronously);
					base.SetCompletedSynchronously(asyncResult.CompletedSynchronously);
					ODataRequestMessageWrapper odataRequestMessageWrapper = Util.NullCheck<ODataRequestMessageWrapper>(perRequest.Request, InternalError.InvalidEndGetResponseRequest);
					IODataResponseMessage iodataResponseMessage = this.RequestInfo.EndGetResponse(odataRequestMessageWrapper, asyncResult);
					perRequest.ResponseMessage = Util.NullCheck<IODataResponseMessage>(iodataResponseMessage, InternalError.InvalidEndGetResponseResponse);
					this.SetHttpWebResponse(perRequest.ResponseMessage);
					Stream stream = null;
					if (204 != iodataResponseMessage.StatusCode)
					{
						stream = iodataResponseMessage.GetStream();
						perRequest.ResponseStream = stream;
					}
					if (stream != null && stream.CanRead)
					{
						if (this.outputResponseStream == null)
						{
							this.outputResponseStream = Util.NullCheck<Stream>(this.GetAsyncResponseStreamCopy(), InternalError.InvalidAsyncResponseStreamCopy);
						}
						if (this.asyncStreamCopyBuffer == null)
						{
							this.asyncStreamCopyBuffer = Util.NullCheck<byte[]>(this.GetAsyncResponseStreamCopyBuffer(), InternalError.InvalidAsyncResponseStreamCopyBuffer);
						}
						this.ReadResponseStream(asyncStateBag);
					}
					else
					{
						perRequest.SetComplete();
						base.SetCompleted();
					}
				}
			}
			catch (Exception ex)
			{
				if (base.HandleFailure(ex))
				{
					throw;
				}
			}
			finally
			{
				this.HandleCompleted(perRequest);
			}
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x000102D0 File Offset: 0x0000E4D0
		protected override void CompleteCheck(BaseAsyncResult.PerRequest pereq, InternalError errorcode)
		{
			if (pereq == null || ((pereq.RequestCompleted || base.IsCompletedInternally) && !base.IsAborted && !pereq.RequestAborted))
			{
				Error.ThrowInternalError(errorcode);
			}
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x000102FC File Offset: 0x0000E4FC
		private void ReadResponseStream(BaseAsyncResult.AsyncStateBag asyncStateBag)
		{
			BaseAsyncResult.PerRequest perRequest = asyncStateBag.PerRequest;
			byte[] array = this.asyncStreamCopyBuffer;
			Stream responseStream = perRequest.ResponseStream;
			IAsyncResult asyncResult;
			do
			{
				int num = 0;
				int num2 = array.Length;
				this.usingBuffer = true;
				asyncResult = BaseAsyncResult.InvokeAsync(new BaseAsyncResult.AsyncAction(responseStream.BeginRead), array, num, num2, new AsyncCallback(this.AsyncEndRead), asyncStateBag);
				perRequest.SetRequestCompletedSynchronously(asyncResult.CompletedSynchronously);
				base.SetCompletedSynchronously(asyncResult.CompletedSynchronously);
			}
			while (asyncResult.CompletedSynchronously && !perRequest.RequestCompleted && !base.IsCompletedInternally && responseStream.CanRead);
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x0001038C File Offset: 0x0000E58C
		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "required for this feature")]
		private void AsyncEndRead(IAsyncResult asyncResult)
		{
			BaseAsyncResult.AsyncStateBag asyncStateBag = asyncResult.AsyncState as BaseAsyncResult.AsyncStateBag;
			BaseAsyncResult.PerRequest perRequest = ((asyncStateBag == null) ? null : asyncStateBag.PerRequest);
			try
			{
				this.CompleteCheck(perRequest, InternalError.InvalidEndReadCompleted);
				perRequest.SetRequestCompletedSynchronously(asyncResult.CompletedSynchronously);
				base.SetCompletedSynchronously(asyncResult.CompletedSynchronously);
				Stream stream = Util.NullCheck<Stream>(perRequest.ResponseStream, InternalError.InvalidEndReadStream);
				Stream stream2 = Util.NullCheck<Stream>(this.outputResponseStream, InternalError.InvalidEndReadCopy);
				byte[] array = Util.NullCheck<byte[]>(this.asyncStreamCopyBuffer, InternalError.InvalidEndReadBuffer);
				int num = stream.EndRead(asyncResult);
				this.usingBuffer = false;
				if (0 < num)
				{
					stream2.Write(array, 0, num);
				}
				if (0 < num && array.Length != 0 && stream.CanRead)
				{
					if (!asyncResult.CompletedSynchronously)
					{
						this.ReadResponseStream(asyncStateBag);
					}
				}
				else
				{
					if (stream2.Position < stream2.Length)
					{
						((MemoryStream)stream2).SetLength(stream2.Position);
					}
					perRequest.SetComplete();
					base.SetCompleted();
				}
			}
			catch (Exception ex)
			{
				if (base.HandleFailure(ex))
				{
					throw;
				}
			}
			finally
			{
				this.HandleCompleted(perRequest);
			}
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x000104A8 File Offset: 0x0000E6A8
		private MaterializeAtom CreateMaterializer(ProjectionPlan plan, ODataPayloadKind payloadKind)
		{
			QueryComponents queryComponents = this.ServiceRequest.QueryComponents(this.responseInfo.Model);
			if (plan != null || queryComponents.Projection != null)
			{
				this.RequestInfo.TypeResolver.IsProjectionRequest();
			}
			HttpWebResponseMessage httpWebResponseMessage = new HttpWebResponseMessage(new HeaderCollection(this.responseMessage), this.responseMessage.StatusCode, new Func<Stream>(this.GetResponseStream));
			return DataServiceRequest.Materialize(this.responseInfo, queryComponents, plan, this.ContentType, httpWebResponseMessage, payloadKind);
		}

		// Token: 0x04000145 RID: 325
		internal readonly DataServiceRequest ServiceRequest;

		// Token: 0x04000146 RID: 326
		internal readonly RequestInfo RequestInfo;

		// Token: 0x04000147 RID: 327
		internal readonly ODataRequestMessageWrapper Request;

		// Token: 0x04000148 RID: 328
		private static byte[] reusableAsyncCopyBuffer;

		// Token: 0x04000149 RID: 329
		private ContentStream requestContentStream;

		// Token: 0x0400014A RID: 330
		private IODataResponseMessage responseMessage;

		// Token: 0x0400014B RID: 331
		private ResponseInfo responseInfo;

		// Token: 0x0400014C RID: 332
		private byte[] asyncStreamCopyBuffer;

		// Token: 0x0400014D RID: 333
		private Stream outputResponseStream;

		// Token: 0x0400014E RID: 334
		private string contentType;

		// Token: 0x0400014F RID: 335
		private long contentLength;

		// Token: 0x04000150 RID: 336
		private HttpStatusCode statusCode;

		// Token: 0x04000151 RID: 337
		private bool responseStreamOwner;

		// Token: 0x04000152 RID: 338
		private bool usingBuffer;
	}
}
