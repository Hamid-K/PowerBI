using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Microsoft.OData.Core
{
	// Token: 0x02000146 RID: 326
	public sealed class ODataBatchReader : IODataBatchOperationListener
	{
		// Token: 0x06000C59 RID: 3161 RVA: 0x0002DCE8 File Offset: 0x0002BEE8
		internal ODataBatchReader(ODataRawInputContext inputContext, string batchBoundary, Encoding batchEncoding, bool synchronous)
		{
			this.inputContext = inputContext;
			this.synchronous = synchronous;
			this.urlResolver = new ODataBatchUrlResolver(inputContext.UrlResolver);
			this.batchStream = new ODataBatchReaderStream(inputContext, batchBoundary, batchEncoding);
			this.allowLegacyContentIdBehaviour = true;
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000C5A RID: 3162 RVA: 0x0002DD25 File Offset: 0x0002BF25
		// (set) Token: 0x06000C5B RID: 3163 RVA: 0x0002DD38 File Offset: 0x0002BF38
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

		// Token: 0x06000C5C RID: 3164 RVA: 0x0002DD41 File Offset: 0x0002BF41
		public bool Read()
		{
			this.VerifyCanRead(true);
			return this.InterceptException<bool>(new Func<bool>(this.ReadSynchronously));
		}

		// Token: 0x06000C5D RID: 3165 RVA: 0x0002DD5C File Offset: 0x0002BF5C
		public ODataBatchOperationRequestMessage CreateOperationRequestMessage()
		{
			this.VerifyCanCreateOperationRequestMessage(true);
			return this.InterceptException<ODataBatchOperationRequestMessage>(new Func<ODataBatchOperationRequestMessage>(this.CreateOperationRequestMessageImplementation));
		}

		// Token: 0x06000C5E RID: 3166 RVA: 0x0002DD77 File Offset: 0x0002BF77
		public ODataBatchOperationResponseMessage CreateOperationResponseMessage()
		{
			this.VerifyCanCreateOperationResponseMessage(true);
			return this.InterceptException<ODataBatchOperationResponseMessage>(new Func<ODataBatchOperationResponseMessage>(this.CreateOperationResponseMessageImplementation));
		}

		// Token: 0x06000C5F RID: 3167 RVA: 0x0002DD92 File Offset: 0x0002BF92
		void IODataBatchOperationListener.BatchOperationContentStreamRequested()
		{
			this.operationState = ODataBatchReader.OperationState.StreamRequested;
		}

		// Token: 0x06000C60 RID: 3168 RVA: 0x0002DD9B File Offset: 0x0002BF9B
		void IODataBatchOperationListener.BatchOperationContentStreamDisposed()
		{
			this.operationState = ODataBatchReader.OperationState.StreamDisposed;
		}

		// Token: 0x06000C61 RID: 3169 RVA: 0x0002DDA4 File Offset: 0x0002BFA4
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

		// Token: 0x06000C62 RID: 3170 RVA: 0x0002DE24 File Offset: 0x0002C024
		private bool ReadSynchronously()
		{
			return this.ReadImplementation();
		}

		// Token: 0x06000C63 RID: 3171 RVA: 0x0002DE2C File Offset: 0x0002C02C
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

		// Token: 0x06000C64 RID: 3172 RVA: 0x0002DF24 File Offset: 0x0002C124
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
						if (text != null && this.urlResolver.ContainsContentId(text))
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

		// Token: 0x06000C65 RID: 3173 RVA: 0x0002DFE0 File Offset: 0x0002C1E0
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
					if (text3 != null && this.urlResolver.ContainsContentId(text3))
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
			return ODataBatchOperationRequestMessage.CreateReadMessage(this.batchStream, text2, uri, odataBatchOperationHeaders, this, this.contentIdToAddOnNextRead, this.urlResolver);
		}

		// Token: 0x06000C66 RID: 3174 RVA: 0x0002E0A4 File Offset: 0x0002C2A4
		private ODataBatchOperationResponseMessage CreateOperationResponseMessageImplementation()
		{
			this.operationState = ODataBatchReader.OperationState.MessageCreated;
			string text = this.batchStream.ReadFirstNonEmptyLine();
			int num = this.ParseResponseLine(text);
			ODataBatchOperationHeaders odataBatchOperationHeaders = this.batchStream.ReadHeaders();
			string text2;
			if (this.batchStream.ChangeSetBoundary != null && this.allowLegacyContentIdBehaviour && this.contentIdToAddOnNextRead == null && odataBatchOperationHeaders.TryGetValue("Content-ID", out text2))
			{
				if (text2 != null && this.urlResolver.ContainsContentId(text2))
				{
					throw new ODataException(Strings.ODataBatchReader_DuplicateContentIDsNotAllowed(text2));
				}
				this.contentIdToAddOnNextRead = text2;
			}
			return ODataBatchOperationResponseMessage.CreateReadMessage(this.batchStream, num, odataBatchOperationHeaders, this.contentIdToAddOnNextRead, this, this.urlResolver.BatchMessageUrlResolver);
		}

		// Token: 0x06000C67 RID: 3175 RVA: 0x0002E14C File Offset: 0x0002C34C
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
			requestUri = ODataBatchUtils.CreateOperationRequestUri(requestUri, this.inputContext.MessageReaderSettings.BaseUri, this.urlResolver);
		}

		// Token: 0x06000C68 RID: 3176 RVA: 0x0002E23C File Offset: 0x0002C43C
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

		// Token: 0x06000C69 RID: 3177 RVA: 0x0002E2EC File Offset: 0x0002C4EC
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

		// Token: 0x06000C6A RID: 3178 RVA: 0x0002E350 File Offset: 0x0002C550
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

		// Token: 0x06000C6B RID: 3179 RVA: 0x0002E3B4 File Offset: 0x0002C5B4
		private void VerifyCanRead(bool synchronousCall)
		{
			this.VerifyReaderReady();
			this.VerifyCallAllowed(synchronousCall);
			if (this.State == ODataBatchReaderState.Exception || this.State == ODataBatchReaderState.Completed)
			{
				throw new ODataException(Strings.ODataBatchReader_ReadOrReadAsyncCalledInInvalidState(this.State));
			}
		}

		// Token: 0x06000C6C RID: 3180 RVA: 0x0002E3EB File Offset: 0x0002C5EB
		private void VerifyReaderReady()
		{
			this.inputContext.VerifyNotDisposed();
			if (this.operationState == ODataBatchReader.OperationState.StreamRequested)
			{
				throw new ODataException(Strings.ODataBatchReader_CannotUseReaderWhileOperationStreamActive);
			}
		}

		// Token: 0x06000C6D RID: 3181 RVA: 0x0002E40C File Offset: 0x0002C60C
		private void VerifyCallAllowed(bool synchronousCall)
		{
			if (synchronousCall && !this.synchronous)
			{
				throw new ODataException(Strings.ODataBatchReader_SyncCallOnAsyncReader);
			}
		}

		// Token: 0x06000C6E RID: 3182 RVA: 0x0002E424 File Offset: 0x0002C624
		private void IncreaseBatchSize()
		{
			this.currentBatchSize += 1U;
			if ((ulong)this.currentBatchSize > (ulong)((long)this.inputContext.MessageReaderSettings.MessageQuotas.MaxPartsPerBatch))
			{
				throw new ODataException(Strings.ODataBatchReader_MaxBatchSizeExceeded(this.inputContext.MessageReaderSettings.MessageQuotas.MaxPartsPerBatch));
			}
		}

		// Token: 0x06000C6F RID: 3183 RVA: 0x0002E484 File Offset: 0x0002C684
		private void IncreaseChangeSetSize()
		{
			this.currentChangeSetSize += 1U;
			if ((ulong)this.currentChangeSetSize > (ulong)((long)this.inputContext.MessageReaderSettings.MessageQuotas.MaxOperationsPerChangeset))
			{
				throw new ODataException(Strings.ODataBatchReader_MaxChangeSetSizeExceeded(this.inputContext.MessageReaderSettings.MessageQuotas.MaxOperationsPerChangeset));
			}
		}

		// Token: 0x06000C70 RID: 3184 RVA: 0x0002E4E3 File Offset: 0x0002C6E3
		private void ResetChangeSetSize()
		{
			this.currentChangeSetSize = 0U;
		}

		// Token: 0x06000C71 RID: 3185 RVA: 0x0002E4EC File Offset: 0x0002C6EC
		private void ThrowODataException(string errorMessage)
		{
			this.State = ODataBatchReaderState.Exception;
			throw new ODataException(errorMessage);
		}

		// Token: 0x06000C72 RID: 3186 RVA: 0x0002E4FC File Offset: 0x0002C6FC
		[SuppressMessage("DataWeb.Usage", "AC0014", Justification = "Throws every time")]
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

		// Token: 0x04000515 RID: 1301
		private readonly ODataRawInputContext inputContext;

		// Token: 0x04000516 RID: 1302
		private readonly ODataBatchReaderStream batchStream;

		// Token: 0x04000517 RID: 1303
		private readonly bool synchronous;

		// Token: 0x04000518 RID: 1304
		private readonly ODataBatchUrlResolver urlResolver;

		// Token: 0x04000519 RID: 1305
		private ODataBatchReaderState batchReaderState;

		// Token: 0x0400051A RID: 1306
		private uint currentBatchSize;

		// Token: 0x0400051B RID: 1307
		private uint currentChangeSetSize;

		// Token: 0x0400051C RID: 1308
		private ODataBatchReader.OperationState operationState;

		// Token: 0x0400051D RID: 1309
		private string contentIdToAddOnNextRead;

		// Token: 0x0400051E RID: 1310
		private bool allowLegacyContentIdBehaviour;

		// Token: 0x02000147 RID: 327
		private enum OperationState
		{
			// Token: 0x04000520 RID: 1312
			None,
			// Token: 0x04000521 RID: 1313
			MessageCreated,
			// Token: 0x04000522 RID: 1314
			StreamRequested,
			// Token: 0x04000523 RID: 1315
			StreamDisposed
		}
	}
}
