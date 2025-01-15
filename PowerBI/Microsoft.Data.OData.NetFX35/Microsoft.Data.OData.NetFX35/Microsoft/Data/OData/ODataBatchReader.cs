using System;
using System.Text;

namespace Microsoft.Data.OData
{
	// Token: 0x0200024D RID: 589
	public sealed class ODataBatchReader : IODataBatchOperationListener
	{
		// Token: 0x060011F0 RID: 4592 RVA: 0x00043DE5 File Offset: 0x00041FE5
		internal ODataBatchReader(ODataRawInputContext inputContext, string batchBoundary, Encoding batchEncoding, bool synchronous)
		{
			this.inputContext = inputContext;
			this.synchronous = synchronous;
			this.urlResolver = new ODataBatchUrlResolver(inputContext.UrlResolver);
			this.batchStream = new ODataBatchReaderStream(inputContext, batchBoundary, batchEncoding);
		}

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x060011F1 RID: 4593 RVA: 0x00043E1B File Offset: 0x0004201B
		// (set) Token: 0x060011F2 RID: 4594 RVA: 0x00043E2E File Offset: 0x0004202E
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

		// Token: 0x060011F3 RID: 4595 RVA: 0x00043E37 File Offset: 0x00042037
		public bool Read()
		{
			this.VerifyCanRead(true);
			return this.InterceptException<bool>(new Func<bool>(this.ReadSynchronously));
		}

		// Token: 0x060011F4 RID: 4596 RVA: 0x00043E52 File Offset: 0x00042052
		public ODataBatchOperationRequestMessage CreateOperationRequestMessage()
		{
			this.VerifyCanCreateOperationRequestMessage(true);
			return this.InterceptException<ODataBatchOperationRequestMessage>(new Func<ODataBatchOperationRequestMessage>(this.CreateOperationRequestMessageImplementation));
		}

		// Token: 0x060011F5 RID: 4597 RVA: 0x00043E6D File Offset: 0x0004206D
		public ODataBatchOperationResponseMessage CreateOperationResponseMessage()
		{
			this.VerifyCanCreateOperationResponseMessage(true);
			return this.InterceptException<ODataBatchOperationResponseMessage>(new Func<ODataBatchOperationResponseMessage>(this.CreateOperationResponseMessageImplementation));
		}

		// Token: 0x060011F6 RID: 4598 RVA: 0x00043E88 File Offset: 0x00042088
		void IODataBatchOperationListener.BatchOperationContentStreamRequested()
		{
			this.operationState = ODataBatchReader.OperationState.StreamRequested;
		}

		// Token: 0x060011F7 RID: 4599 RVA: 0x00043E91 File Offset: 0x00042091
		void IODataBatchOperationListener.BatchOperationContentStreamDisposed()
		{
			this.operationState = ODataBatchReader.OperationState.StreamDisposed;
		}

		// Token: 0x060011F8 RID: 4600 RVA: 0x00043E9C File Offset: 0x0004209C
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

		// Token: 0x060011F9 RID: 4601 RVA: 0x00043F1C File Offset: 0x0004211C
		private bool ReadSynchronously()
		{
			return this.ReadImplementation();
		}

		// Token: 0x060011FA RID: 4602 RVA: 0x00043F24 File Offset: 0x00042124
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
					this.urlResolver.AddContentId(this.contentIdToAddOnNextRead);
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

		// Token: 0x060011FB RID: 4603 RVA: 0x0004401C File Offset: 0x0004221C
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
						this.urlResolver.Reset();
					}
				}
				else
				{
					bool flag3 = this.batchStream.ChangeSetBoundary != null;
					bool flag4 = this.batchStream.ProcessPartHeader();
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
				}
				return odataBatchReaderState;
			}
			if (this.batchStream.ChangeSetBoundary == null)
			{
				return ODataBatchReaderState.Completed;
			}
			return ODataBatchReaderState.ChangesetEnd;
		}

		// Token: 0x060011FC RID: 4604 RVA: 0x000440AC File Offset: 0x000422AC
		private ODataBatchOperationRequestMessage CreateOperationRequestMessageImplementation()
		{
			this.operationState = ODataBatchReader.OperationState.MessageCreated;
			string text = this.batchStream.ReadFirstNonEmptyLine();
			string text2;
			Uri uri;
			this.ParseRequestLine(text, out text2, out uri);
			ODataBatchOperationHeaders odataBatchOperationHeaders = this.batchStream.ReadHeaders();
			ODataBatchOperationRequestMessage odataBatchOperationRequestMessage = ODataBatchOperationRequestMessage.CreateReadMessage(this.batchStream, text2, uri, odataBatchOperationHeaders, this, this.urlResolver);
			string text3;
			if (odataBatchOperationHeaders.TryGetValue("Content-ID", out text3))
			{
				if (text3 != null && this.urlResolver.ContainsContentId(text3))
				{
					throw new ODataException(Strings.ODataBatchReader_DuplicateContentIDsNotAllowed(text3));
				}
				this.contentIdToAddOnNextRead = text3;
			}
			return odataBatchOperationRequestMessage;
		}

		// Token: 0x060011FD RID: 4605 RVA: 0x00044134 File Offset: 0x00042334
		private ODataBatchOperationResponseMessage CreateOperationResponseMessageImplementation()
		{
			this.operationState = ODataBatchReader.OperationState.MessageCreated;
			string text = this.batchStream.ReadFirstNonEmptyLine();
			int num = this.ParseResponseLine(text);
			ODataBatchOperationHeaders odataBatchOperationHeaders = this.batchStream.ReadHeaders();
			return ODataBatchOperationResponseMessage.CreateReadMessage(this.batchStream, num, odataBatchOperationHeaders, this, this.urlResolver.BatchMessageUrlResolver);
		}

		// Token: 0x060011FE RID: 4606 RVA: 0x00044184 File Offset: 0x00042384
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
			if (this.batchStream.ChangeSetBoundary == null)
			{
				if (!HttpUtils.IsQueryMethod(httpMethod))
				{
					throw new ODataException(Strings.ODataBatch_InvalidHttpMethodForQueryOperation(httpMethod));
				}
			}
			else if (HttpUtils.IsQueryMethod(httpMethod))
			{
				throw new ODataException(Strings.ODataBatch_InvalidHttpMethodForChangeSetRequest(httpMethod));
			}
			requestUri = new Uri(text, 0);
			requestUri = ODataBatchUtils.CreateOperationRequestUri(requestUri, this.inputContext.MessageReaderSettings.BaseUri, this.urlResolver);
		}

		// Token: 0x060011FF RID: 4607 RVA: 0x0004428C File Offset: 0x0004248C
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

		// Token: 0x06001200 RID: 4608 RVA: 0x0004433C File Offset: 0x0004253C
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

		// Token: 0x06001201 RID: 4609 RVA: 0x000443A0 File Offset: 0x000425A0
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

		// Token: 0x06001202 RID: 4610 RVA: 0x00044404 File Offset: 0x00042604
		private void VerifyCanRead(bool synchronousCall)
		{
			this.VerifyReaderReady();
			this.VerifyCallAllowed(synchronousCall);
			if (this.State == ODataBatchReaderState.Exception || this.State == ODataBatchReaderState.Completed)
			{
				throw new ODataException(Strings.ODataBatchReader_ReadOrReadAsyncCalledInInvalidState(this.State));
			}
		}

		// Token: 0x06001203 RID: 4611 RVA: 0x0004443B File Offset: 0x0004263B
		private void VerifyReaderReady()
		{
			this.inputContext.VerifyNotDisposed();
			if (this.operationState == ODataBatchReader.OperationState.StreamRequested)
			{
				throw new ODataException(Strings.ODataBatchReader_CannotUseReaderWhileOperationStreamActive);
			}
		}

		// Token: 0x06001204 RID: 4612 RVA: 0x0004445C File Offset: 0x0004265C
		private void VerifyCallAllowed(bool synchronousCall)
		{
			if (synchronousCall && !this.synchronous)
			{
				throw new ODataException(Strings.ODataBatchReader_SyncCallOnAsyncReader);
			}
		}

		// Token: 0x06001205 RID: 4613 RVA: 0x00044474 File Offset: 0x00042674
		private void IncreaseBatchSize()
		{
			this.currentBatchSize += 1U;
			if ((ulong)this.currentBatchSize > (ulong)((long)this.inputContext.MessageReaderSettings.MessageQuotas.MaxPartsPerBatch))
			{
				throw new ODataException(Strings.ODataBatchReader_MaxBatchSizeExceeded(this.inputContext.MessageReaderSettings.MessageQuotas.MaxPartsPerBatch));
			}
		}

		// Token: 0x06001206 RID: 4614 RVA: 0x000444D4 File Offset: 0x000426D4
		private void IncreaseChangeSetSize()
		{
			this.currentChangeSetSize += 1U;
			if ((ulong)this.currentChangeSetSize > (ulong)((long)this.inputContext.MessageReaderSettings.MessageQuotas.MaxOperationsPerChangeset))
			{
				throw new ODataException(Strings.ODataBatchReader_MaxChangeSetSizeExceeded(this.inputContext.MessageReaderSettings.MessageQuotas.MaxOperationsPerChangeset));
			}
		}

		// Token: 0x06001207 RID: 4615 RVA: 0x00044533 File Offset: 0x00042733
		private void ResetChangeSetSize()
		{
			this.currentChangeSetSize = 0U;
		}

		// Token: 0x06001208 RID: 4616 RVA: 0x0004453C File Offset: 0x0004273C
		private void ThrowODataException(string errorMessage)
		{
			this.State = ODataBatchReaderState.Exception;
			throw new ODataException(errorMessage);
		}

		// Token: 0x06001209 RID: 4617 RVA: 0x0004454C File Offset: 0x0004274C
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

		// Token: 0x040006C1 RID: 1729
		private readonly ODataRawInputContext inputContext;

		// Token: 0x040006C2 RID: 1730
		private readonly ODataBatchReaderStream batchStream;

		// Token: 0x040006C3 RID: 1731
		private readonly bool synchronous;

		// Token: 0x040006C4 RID: 1732
		private readonly ODataBatchUrlResolver urlResolver;

		// Token: 0x040006C5 RID: 1733
		private ODataBatchReaderState batchReaderState;

		// Token: 0x040006C6 RID: 1734
		private uint currentBatchSize;

		// Token: 0x040006C7 RID: 1735
		private uint currentChangeSetSize;

		// Token: 0x040006C8 RID: 1736
		private ODataBatchReader.OperationState operationState;

		// Token: 0x040006C9 RID: 1737
		private string contentIdToAddOnNextRead;

		// Token: 0x0200024E RID: 590
		private enum OperationState
		{
			// Token: 0x040006CB RID: 1739
			None,
			// Token: 0x040006CC RID: 1740
			MessageCreated,
			// Token: 0x040006CD RID: 1741
			StreamRequested,
			// Token: 0x040006CE RID: 1742
			StreamDisposed
		}
	}
}
