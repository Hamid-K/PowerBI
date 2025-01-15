using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Microsoft.OData
{
	// Token: 0x02000035 RID: 53
	public sealed class ODataBatchReader : IODataBatchOperationListener
	{
		// Token: 0x06000181 RID: 385 RVA: 0x00005F60 File Offset: 0x00004160
		internal ODataBatchReader(ODataRawInputContext inputContext, string batchBoundary, Encoding batchEncoding, bool synchronous)
		{
			this.inputContext = inputContext;
			this.container = inputContext.Container;
			this.synchronous = synchronous;
			this.payloadUriConverter = new ODataBatchPayloadUriConverter(inputContext.PayloadUriConverter);
			this.batchStream = new ODataBatchReaderStream(inputContext, batchBoundary, batchEncoding);
			this.allowLegacyContentIdBehaviour = true;
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000182 RID: 386 RVA: 0x00005FB4 File Offset: 0x000041B4
		// (set) Token: 0x06000183 RID: 387 RVA: 0x00005FC7 File Offset: 0x000041C7
		public ODataBatchReaderState State
		{
			get
			{
				this.inputContext.VerifyNotDisposed();
				return this.batchReaderState;
			}
			private set
			{
				this.batchReaderState = value;
			}
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00005FD0 File Offset: 0x000041D0
		public bool Read()
		{
			this.VerifyCanRead(true);
			return this.InterceptException<bool>(new Func<bool>(this.ReadSynchronously));
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00005FEB File Offset: 0x000041EB
		public ODataBatchOperationRequestMessage CreateOperationRequestMessage()
		{
			this.VerifyCanCreateOperationRequestMessage(true);
			return this.InterceptException<ODataBatchOperationRequestMessage>(new Func<ODataBatchOperationRequestMessage>(this.CreateOperationRequestMessageImplementation));
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00006006 File Offset: 0x00004206
		public ODataBatchOperationResponseMessage CreateOperationResponseMessage()
		{
			this.VerifyCanCreateOperationResponseMessage(true);
			return this.InterceptException<ODataBatchOperationResponseMessage>(new Func<ODataBatchOperationResponseMessage>(this.CreateOperationResponseMessageImplementation));
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00006021 File Offset: 0x00004221
		void IODataBatchOperationListener.BatchOperationContentStreamRequested()
		{
			this.operationState = ODataBatchReader.OperationState.StreamRequested;
		}

		// Token: 0x06000188 RID: 392 RVA: 0x0000602A File Offset: 0x0000422A
		void IODataBatchOperationListener.BatchOperationContentStreamDisposed()
		{
			this.operationState = ODataBatchReader.OperationState.StreamDisposed;
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00006034 File Offset: 0x00004234
		private ODataBatchReaderState GetEndBoundaryState()
		{
			switch (this.batchReaderState)
			{
			case ODataBatchReaderState.Initial:
				return ODataBatchReaderState.Completed;
			case ODataBatchReaderState.Operation:
				if (this.batchStream.ChangeSetBoundary != null)
				{
					return ODataBatchReaderState.ChangesetEnd;
				}
				return ODataBatchReaderState.Completed;
			case ODataBatchReaderState.ChangesetStart:
				return ODataBatchReaderState.ChangesetEnd;
			case ODataBatchReaderState.ChangesetEnd:
				return ODataBatchReaderState.Completed;
			case ODataBatchReaderState.Completed:
				throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataBatchReader_GetEndBoundary_Completed));
			case ODataBatchReaderState.Exception:
				throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataBatchReader_GetEndBoundary_Exception));
			default:
				throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataBatchReader_GetEndBoundary_UnknownValue));
			}
		}

		// Token: 0x0600018A RID: 394 RVA: 0x000060B4 File Offset: 0x000042B4
		private bool ReadSynchronously()
		{
			return this.ReadImplementation();
		}

		// Token: 0x0600018B RID: 395 RVA: 0x000060BC File Offset: 0x000042BC
		private bool ReadImplementation()
		{
			switch (this.State)
			{
			case ODataBatchReaderState.Initial:
				this.batchReaderState = this.SkipToNextPartAndReadHeaders();
				break;
			case ODataBatchReaderState.Operation:
				if (this.operationState == ODataBatchReader.OperationState.None)
				{
					throw new ODataException(Strings.ODataBatchReader_NoMessageWasCreatedForOperation);
				}
				this.operationState = ODataBatchReader.OperationState.None;
				if (this.contentIdToAddOnNextRead != null)
				{
					this.payloadUriConverter.AddContentId(this.contentIdToAddOnNextRead);
					this.contentIdToAddOnNextRead = null;
				}
				this.batchReaderState = this.SkipToNextPartAndReadHeaders();
				break;
			case ODataBatchReaderState.ChangesetStart:
				this.batchReaderState = this.SkipToNextPartAndReadHeaders();
				break;
			case ODataBatchReaderState.ChangesetEnd:
				this.ResetChangeSetSize();
				this.batchStream.ResetChangeSetBoundary();
				this.batchReaderState = this.SkipToNextPartAndReadHeaders();
				break;
			case ODataBatchReaderState.Completed:
			case ODataBatchReaderState.Exception:
				throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataBatchReader_ReadImplementation));
			default:
				throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataBatchReader_ReadImplementation));
			}
			return this.batchReaderState != ODataBatchReaderState.Completed && this.batchReaderState != ODataBatchReaderState.Exception;
		}

		// Token: 0x0600018C RID: 396 RVA: 0x000061B4 File Offset: 0x000043B4
		private ODataBatchReaderState SkipToNextPartAndReadHeaders()
		{
			bool flag;
			bool flag2;
			if (this.batchStream.SkipToBoundary(out flag, out flag2))
			{
				ODataBatchReaderState odataBatchReaderState;
				if (flag || flag2)
				{
					odataBatchReaderState = this.GetEndBoundaryState();
					if (odataBatchReaderState == ODataBatchReaderState.ChangesetEnd)
					{
						this.payloadUriConverter.Reset();
					}
				}
				else
				{
					bool flag3 = this.batchStream.ChangeSetBoundary != null;
					string text;
					bool flag4 = this.batchStream.ProcessPartHeader(out text);
					if (flag3)
					{
						odataBatchReaderState = ODataBatchReaderState.Operation;
						this.IncreaseChangeSetSize();
					}
					else
					{
						odataBatchReaderState = (flag4 ? ODataBatchReaderState.ChangesetStart : ODataBatchReaderState.Operation);
						this.IncreaseBatchSize();
					}
					if (!flag4)
					{
						if (text != null && this.payloadUriConverter.ContainsContentId(text))
						{
							throw new ODataException(Strings.ODataBatchReader_DuplicateContentIDsNotAllowed(text));
						}
						this.contentIdToAddOnNextRead = text;
					}
				}
				return odataBatchReaderState;
			}
			if (this.batchStream.ChangeSetBoundary == null)
			{
				return ODataBatchReaderState.Completed;
			}
			return ODataBatchReaderState.ChangesetEnd;
		}

		// Token: 0x0600018D RID: 397 RVA: 0x0000626C File Offset: 0x0000446C
		private ODataBatchOperationRequestMessage CreateOperationRequestMessageImplementation()
		{
			this.operationState = ODataBatchReader.OperationState.MessageCreated;
			string text = this.batchStream.ReadFirstNonEmptyLine();
			string text2;
			Uri uri;
			this.ParseRequestLine(text, out text2, out uri);
			ODataBatchOperationHeaders odataBatchOperationHeaders = this.batchStream.ReadHeaders();
			if (this.batchStream.ChangeSetBoundary != null)
			{
				string text3;
				if (this.allowLegacyContentIdBehaviour && this.contentIdToAddOnNextRead == null && odataBatchOperationHeaders.TryGetValue("Content-ID", out text3))
				{
					if (text3 != null && this.payloadUriConverter.ContainsContentId(text3))
					{
						throw new ODataException(Strings.ODataBatchReader_DuplicateContentIDsNotAllowed(text3));
					}
					this.contentIdToAddOnNextRead = text3;
				}
				if (this.contentIdToAddOnNextRead == null)
				{
					throw new ODataException(Strings.ODataBatchOperationHeaderDictionary_KeyNotFound("Content-ID"));
				}
			}
			return ODataBatchOperationRequestMessage.CreateReadMessage(this.batchStream, text2, uri, odataBatchOperationHeaders, this, this.contentIdToAddOnNextRead, this.payloadUriConverter, this.container);
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00006334 File Offset: 0x00004534
		private ODataBatchOperationResponseMessage CreateOperationResponseMessageImplementation()
		{
			this.operationState = ODataBatchReader.OperationState.MessageCreated;
			string text = this.batchStream.ReadFirstNonEmptyLine();
			int num = this.ParseResponseLine(text);
			ODataBatchOperationHeaders odataBatchOperationHeaders = this.batchStream.ReadHeaders();
			string text2;
			if (this.batchStream.ChangeSetBoundary != null && this.allowLegacyContentIdBehaviour && this.contentIdToAddOnNextRead == null && odataBatchOperationHeaders.TryGetValue("Content-ID", out text2))
			{
				if (text2 != null && this.payloadUriConverter.ContainsContentId(text2))
				{
					throw new ODataException(Strings.ODataBatchReader_DuplicateContentIDsNotAllowed(text2));
				}
				this.contentIdToAddOnNextRead = text2;
			}
			return ODataBatchOperationResponseMessage.CreateReadMessage(this.batchStream, num, odataBatchOperationHeaders, this.contentIdToAddOnNextRead, this, this.payloadUriConverter.BatchMessagePayloadUriConverter, this.container);
		}

		// Token: 0x0600018F RID: 399 RVA: 0x000063E4 File Offset: 0x000045E4
		private void ParseRequestLine(string requestLine, out string httpMethod, out Uri requestUri)
		{
			int num = requestLine.IndexOf(' ');
			if (num <= 0 || requestLine.Length - 3 <= num)
			{
				throw new ODataException(Strings.ODataBatchReaderStream_InvalidRequestLine(requestLine));
			}
			int num2 = requestLine.LastIndexOf(' ');
			if (num2 < 0 || num2 - num - 1 <= 0 || requestLine.Length - 1 <= num2)
			{
				throw new ODataException(Strings.ODataBatchReaderStream_InvalidRequestLine(requestLine));
			}
			httpMethod = requestLine.Substring(0, num);
			string text = requestLine.Substring(num + 1, num2 - num - 1);
			string text2 = requestLine.Substring(num2 + 1);
			if (string.CompareOrdinal("HTTP/1.1", text2) != 0)
			{
				throw new ODataException(Strings.ODataBatchReaderStream_InvalidHttpVersionSpecified(text2, "HTTP/1.1"));
			}
			HttpUtils.ValidateHttpMethod(httpMethod);
			if (this.batchStream.ChangeSetBoundary != null && HttpUtils.IsQueryMethod(httpMethod))
			{
				throw new ODataException(Strings.ODataBatch_InvalidHttpMethodForChangeSetRequest(httpMethod));
			}
			requestUri = new Uri(text, 0);
			requestUri = ODataBatchUtils.CreateOperationRequestUri(requestUri, this.inputContext.MessageReaderSettings.BaseUri, this.payloadUriConverter);
		}

		// Token: 0x06000190 RID: 400 RVA: 0x000064D4 File Offset: 0x000046D4
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "'this' is used when built in debug")]
		private int ParseResponseLine(string responseLine)
		{
			int num = responseLine.IndexOf(' ');
			if (num <= 0 || responseLine.Length - 3 <= num)
			{
				throw new ODataException(Strings.ODataBatchReaderStream_InvalidResponseLine(responseLine));
			}
			int num2 = responseLine.IndexOf(' ', num + 1);
			if (num2 < 0 || num2 - num - 1 <= 0 || responseLine.Length - 1 <= num2)
			{
				throw new ODataException(Strings.ODataBatchReaderStream_InvalidResponseLine(responseLine));
			}
			string text = responseLine.Substring(0, num);
			string text2 = responseLine.Substring(num + 1, num2 - num - 1);
			if (string.CompareOrdinal("HTTP/1.1", text) != 0)
			{
				throw new ODataException(Strings.ODataBatchReaderStream_InvalidHttpVersionSpecified(text, "HTTP/1.1"));
			}
			int num3;
			if (!int.TryParse(text2, ref num3))
			{
				throw new ODataException(Strings.ODataBatchReaderStream_NonIntegerHttpStatusCode(text2));
			}
			return num3;
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00006584 File Offset: 0x00004784
		private void VerifyCanCreateOperationRequestMessage(bool synchronousCall)
		{
			this.VerifyReaderReady();
			this.VerifyCallAllowed(synchronousCall);
			if (this.inputContext.ReadingResponse)
			{
				this.ThrowODataException(Strings.ODataBatchReader_CannotCreateRequestOperationWhenReadingResponse);
			}
			if (this.State != ODataBatchReaderState.Operation)
			{
				this.ThrowODataException(Strings.ODataBatchReader_InvalidStateForCreateOperationRequestMessage(this.State));
			}
			if (this.operationState != ODataBatchReader.OperationState.None)
			{
				this.ThrowODataException(Strings.ODataBatchReader_OperationRequestMessageAlreadyCreated);
			}
		}

		// Token: 0x06000192 RID: 402 RVA: 0x000065E8 File Offset: 0x000047E8
		private void VerifyCanCreateOperationResponseMessage(bool synchronousCall)
		{
			this.VerifyReaderReady();
			this.VerifyCallAllowed(synchronousCall);
			if (!this.inputContext.ReadingResponse)
			{
				this.ThrowODataException(Strings.ODataBatchReader_CannotCreateResponseOperationWhenReadingRequest);
			}
			if (this.State != ODataBatchReaderState.Operation)
			{
				this.ThrowODataException(Strings.ODataBatchReader_InvalidStateForCreateOperationResponseMessage(this.State));
			}
			if (this.operationState != ODataBatchReader.OperationState.None)
			{
				this.ThrowODataException(Strings.ODataBatchReader_OperationResponseMessageAlreadyCreated);
			}
		}

		// Token: 0x06000193 RID: 403 RVA: 0x0000664C File Offset: 0x0000484C
		private void VerifyCanRead(bool synchronousCall)
		{
			this.VerifyReaderReady();
			this.VerifyCallAllowed(synchronousCall);
			if (this.State == ODataBatchReaderState.Exception || this.State == ODataBatchReaderState.Completed)
			{
				throw new ODataException(Strings.ODataBatchReader_ReadOrReadAsyncCalledInInvalidState(this.State));
			}
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00006683 File Offset: 0x00004883
		private void VerifyReaderReady()
		{
			this.inputContext.VerifyNotDisposed();
			if (this.operationState == ODataBatchReader.OperationState.StreamRequested)
			{
				throw new ODataException(Strings.ODataBatchReader_CannotUseReaderWhileOperationStreamActive);
			}
		}

		// Token: 0x06000195 RID: 405 RVA: 0x000066A4 File Offset: 0x000048A4
		private void VerifyCallAllowed(bool synchronousCall)
		{
			if (synchronousCall && !this.synchronous)
			{
				throw new ODataException(Strings.ODataBatchReader_SyncCallOnAsyncReader);
			}
		}

		// Token: 0x06000196 RID: 406 RVA: 0x000066BC File Offset: 0x000048BC
		private void IncreaseBatchSize()
		{
			this.currentBatchSize += 1U;
			if ((ulong)this.currentBatchSize > (ulong)((long)this.inputContext.MessageReaderSettings.MessageQuotas.MaxPartsPerBatch))
			{
				throw new ODataException(Strings.ODataBatchReader_MaxBatchSizeExceeded(this.inputContext.MessageReaderSettings.MessageQuotas.MaxPartsPerBatch));
			}
		}

		// Token: 0x06000197 RID: 407 RVA: 0x0000671C File Offset: 0x0000491C
		private void IncreaseChangeSetSize()
		{
			this.currentChangeSetSize += 1U;
			if ((ulong)this.currentChangeSetSize > (ulong)((long)this.inputContext.MessageReaderSettings.MessageQuotas.MaxOperationsPerChangeset))
			{
				throw new ODataException(Strings.ODataBatchReader_MaxChangeSetSizeExceeded(this.inputContext.MessageReaderSettings.MessageQuotas.MaxOperationsPerChangeset));
			}
		}

		// Token: 0x06000198 RID: 408 RVA: 0x0000677B File Offset: 0x0000497B
		private void ResetChangeSetSize()
		{
			this.currentChangeSetSize = 0U;
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00006784 File Offset: 0x00004984
		private void ThrowODataException(string errorMessage)
		{
			this.State = ODataBatchReaderState.Exception;
			throw new ODataException(errorMessage);
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00006794 File Offset: 0x00004994
		private T InterceptException<T>(Func<T> action)
		{
			T t;
			try
			{
				t = action.Invoke();
			}
			catch (Exception ex)
			{
				if (ExceptionUtils.IsCatchableExceptionType(ex))
				{
					this.State = ODataBatchReaderState.Exception;
				}
				throw;
			}
			return t;
		}

		// Token: 0x040000E1 RID: 225
		private readonly ODataRawInputContext inputContext;

		// Token: 0x040000E2 RID: 226
		private readonly ODataBatchReaderStream batchStream;

		// Token: 0x040000E3 RID: 227
		private readonly bool synchronous;

		// Token: 0x040000E4 RID: 228
		private readonly ODataBatchPayloadUriConverter payloadUriConverter;

		// Token: 0x040000E5 RID: 229
		private readonly IServiceProvider container;

		// Token: 0x040000E6 RID: 230
		private ODataBatchReaderState batchReaderState;

		// Token: 0x040000E7 RID: 231
		private uint currentBatchSize;

		// Token: 0x040000E8 RID: 232
		private uint currentChangeSetSize;

		// Token: 0x040000E9 RID: 233
		private ODataBatchReader.OperationState operationState;

		// Token: 0x040000EA RID: 234
		private string contentIdToAddOnNextRead;

		// Token: 0x040000EB RID: 235
		private bool allowLegacyContentIdBehaviour;

		// Token: 0x02000253 RID: 595
		private enum OperationState
		{
			// Token: 0x04000AD3 RID: 2771
			None,
			// Token: 0x04000AD4 RID: 2772
			MessageCreated,
			// Token: 0x04000AD5 RID: 2773
			StreamRequested,
			// Token: 0x04000AD6 RID: 2774
			StreamDisposed
		}
	}
}
