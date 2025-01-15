using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Microsoft.OData.Core
{
	// Token: 0x0200014E RID: 334
	public sealed class ODataBatchWriter : IODataBatchOperationListener, IODataOutputInStreamErrorListener
	{
		// Token: 0x06000C9E RID: 3230 RVA: 0x0002F858 File Offset: 0x0002DA58
		internal ODataBatchWriter(ODataRawOutputContext rawOutputContext, string batchBoundary)
		{
			ExceptionUtils.CheckArgumentNotNull<string>(batchBoundary, "batchBoundary");
			this.rawOutputContext = rawOutputContext;
			this.batchBoundary = batchBoundary;
			this.urlResolver = new ODataBatchUrlResolver(rawOutputContext.UrlResolver);
			this.rawOutputContext.InitializeRawValueWriter();
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000C9F RID: 3231 RVA: 0x0002F895 File Offset: 0x0002DA95
		// (set) Token: 0x06000CA0 RID: 3232 RVA: 0x0002F89D File Offset: 0x0002DA9D
		private ODataBatchOperationRequestMessage CurrentOperationRequestMessage
		{
			get
			{
				return this.currentOperationRequestMessage;
			}
			set
			{
				this.currentOperationRequestMessage = value;
			}
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000CA1 RID: 3233 RVA: 0x0002F8A6 File Offset: 0x0002DAA6
		// (set) Token: 0x06000CA2 RID: 3234 RVA: 0x0002F8AE File Offset: 0x0002DAAE
		private ODataBatchOperationResponseMessage CurrentOperationResponseMessage
		{
			get
			{
				return this.currentOperationResponseMessage;
			}
			set
			{
				this.currentOperationResponseMessage = value;
			}
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000CA3 RID: 3235 RVA: 0x0002F8B7 File Offset: 0x0002DAB7
		private ODataBatchOperationMessage CurrentOperationMessage
		{
			get
			{
				if (this.currentOperationRequestMessage != null)
				{
					return this.currentOperationRequestMessage.OperationMessage;
				}
				if (this.currentOperationResponseMessage != null)
				{
					return this.currentOperationResponseMessage.OperationMessage;
				}
				return null;
			}
		}

		// Token: 0x06000CA4 RID: 3236 RVA: 0x0002F8E2 File Offset: 0x0002DAE2
		public void WriteStartBatch()
		{
			this.VerifyCanWriteStartBatch(true);
			this.WriteStartBatchImplementation();
		}

		// Token: 0x06000CA5 RID: 3237 RVA: 0x0002F8F1 File Offset: 0x0002DAF1
		public void WriteEndBatch()
		{
			this.VerifyCanWriteEndBatch(true);
			this.WriteEndBatchImplementation();
			this.Flush();
		}

		// Token: 0x06000CA6 RID: 3238 RVA: 0x0002F906 File Offset: 0x0002DB06
		public void WriteStartChangeset()
		{
			this.VerifyCanWriteStartChangeset(true);
			this.WriteStartChangesetImplementation();
		}

		// Token: 0x06000CA7 RID: 3239 RVA: 0x0002F915 File Offset: 0x0002DB15
		public void WriteEndChangeset()
		{
			this.VerifyCanWriteEndChangeset(true);
			this.WriteEndChangesetImplementation();
		}

		// Token: 0x06000CA8 RID: 3240 RVA: 0x0002F924 File Offset: 0x0002DB24
		public ODataBatchOperationRequestMessage CreateOperationRequestMessage(string method, Uri uri, string contentId)
		{
			this.VerifyCanCreateOperationRequestMessage(true, method, uri, contentId);
			return this.CreateOperationRequestMessageImplementation(method, uri, contentId);
		}

		// Token: 0x06000CA9 RID: 3241 RVA: 0x0002F939 File Offset: 0x0002DB39
		public ODataBatchOperationResponseMessage CreateOperationResponseMessage(string contentId)
		{
			this.VerifyCanCreateOperationResponseMessage(true);
			return this.CreateOperationResponseMessageImplementation(contentId);
		}

		// Token: 0x06000CAA RID: 3242 RVA: 0x0002F94C File Offset: 0x0002DB4C
		public void Flush()
		{
			this.VerifyCanFlush(true);
			try
			{
				this.rawOutputContext.Flush();
			}
			catch
			{
				this.SetState(ODataBatchWriter.BatchWriterState.Error);
				throw;
			}
		}

		// Token: 0x06000CAB RID: 3243 RVA: 0x0002F988 File Offset: 0x0002DB88
		void IODataBatchOperationListener.BatchOperationContentStreamRequested()
		{
			this.StartBatchOperationContent();
			this.rawOutputContext.FlushBuffers();
			this.DisposeBatchWriterAndSetContentStreamRequestedState();
		}

		// Token: 0x06000CAC RID: 3244 RVA: 0x0002F9A1 File Offset: 0x0002DBA1
		void IODataBatchOperationListener.BatchOperationContentStreamDisposed()
		{
			this.SetState(ODataBatchWriter.BatchWriterState.OperationStreamDisposed);
			this.CurrentOperationRequestMessage = null;
			this.CurrentOperationResponseMessage = null;
			this.rawOutputContext.InitializeRawValueWriter();
		}

		// Token: 0x06000CAD RID: 3245 RVA: 0x0002F9C3 File Offset: 0x0002DBC3
		void IODataOutputInStreamErrorListener.OnInStreamError()
		{
			this.rawOutputContext.VerifyNotDisposed();
			this.SetState(ODataBatchWriter.BatchWriterState.Error);
			this.rawOutputContext.TextWriter.Flush();
			throw new ODataException(Strings.ODataBatchWriter_CannotWriteInStreamErrorForBatch);
		}

		// Token: 0x06000CAE RID: 3246 RVA: 0x0002F9F1 File Offset: 0x0002DBF1
		private static bool IsErrorState(ODataBatchWriter.BatchWriterState state)
		{
			return state == ODataBatchWriter.BatchWriterState.Error;
		}

		// Token: 0x06000CAF RID: 3247 RVA: 0x0002F9F7 File Offset: 0x0002DBF7
		private void VerifyCanWriteStartBatch(bool synchronousCall)
		{
			this.ValidateWriterReady();
			this.VerifyCallAllowed(synchronousCall);
		}

		// Token: 0x06000CB0 RID: 3248 RVA: 0x0002FA06 File Offset: 0x0002DC06
		private void WriteStartBatchImplementation()
		{
			this.SetState(ODataBatchWriter.BatchWriterState.BatchStarted);
		}

		// Token: 0x06000CB1 RID: 3249 RVA: 0x0002FA0F File Offset: 0x0002DC0F
		private void VerifyCanWriteEndBatch(bool synchronousCall)
		{
			this.ValidateWriterReady();
			this.VerifyCallAllowed(synchronousCall);
		}

		// Token: 0x06000CB2 RID: 3250 RVA: 0x0002FA1E File Offset: 0x0002DC1E
		private void WriteEndBatchImplementation()
		{
			this.WritePendingMessageData(true);
			this.SetState(ODataBatchWriter.BatchWriterState.BatchCompleted);
			ODataBatchWriterUtils.WriteEndBoundary(this.rawOutputContext.TextWriter, this.batchBoundary, !this.batchStartBoundaryWritten);
			this.rawOutputContext.TextWriter.WriteLine();
		}

		// Token: 0x06000CB3 RID: 3251 RVA: 0x0002FA5D File Offset: 0x0002DC5D
		private void VerifyCanWriteStartChangeset(bool synchronousCall)
		{
			this.ValidateWriterReady();
			this.VerifyCallAllowed(synchronousCall);
		}

		// Token: 0x06000CB4 RID: 3252 RVA: 0x0002FA6C File Offset: 0x0002DC6C
		private void WriteStartChangesetImplementation()
		{
			this.WritePendingMessageData(true);
			this.SetState(ODataBatchWriter.BatchWriterState.ChangeSetStarted);
			this.ResetChangeSetSize();
			this.InterceptException(new Action(this.IncreaseBatchSize));
			ODataBatchWriterUtils.WriteStartBoundary(this.rawOutputContext.TextWriter, this.batchBoundary, !this.batchStartBoundaryWritten);
			this.batchStartBoundaryWritten = true;
			ODataBatchWriterUtils.WriteChangeSetPreamble(this.rawOutputContext.TextWriter, this.changeSetBoundary);
			this.changesetStartBoundaryWritten = false;
		}

		// Token: 0x06000CB5 RID: 3253 RVA: 0x0002FAE2 File Offset: 0x0002DCE2
		private void VerifyCanWriteEndChangeset(bool synchronousCall)
		{
			this.ValidateWriterReady();
			this.VerifyCallAllowed(synchronousCall);
		}

		// Token: 0x06000CB6 RID: 3254 RVA: 0x0002FAF4 File Offset: 0x0002DCF4
		private void WriteEndChangesetImplementation()
		{
			this.WritePendingMessageData(true);
			string text = this.changeSetBoundary;
			this.SetState(ODataBatchWriter.BatchWriterState.ChangeSetCompleted);
			ODataBatchWriterUtils.WriteEndBoundary(this.rawOutputContext.TextWriter, text, !this.changesetStartBoundaryWritten);
			this.urlResolver.Reset();
			this.currentOperationContentId = null;
		}

		// Token: 0x06000CB7 RID: 3255 RVA: 0x0002FB44 File Offset: 0x0002DD44
		private void VerifyCanCreateOperationRequestMessage(bool synchronousCall, string method, Uri uri, string contentId)
		{
			this.ValidateWriterReady();
			this.VerifyCallAllowed(synchronousCall);
			if (this.rawOutputContext.WritingResponse)
			{
				this.ThrowODataException(Strings.ODataBatchWriter_CannotCreateRequestOperationWhenWritingResponse);
			}
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(method, "method");
			if (this.changeSetBoundary != null)
			{
				if (HttpUtils.IsQueryMethod(method))
				{
					this.ThrowODataException(Strings.ODataBatch_InvalidHttpMethodForChangeSetRequest(method));
				}
				if (string.IsNullOrEmpty(contentId))
				{
					this.ThrowODataException(Strings.ODataBatchOperationHeaderDictionary_KeyNotFound("Content-ID"));
				}
			}
			ExceptionUtils.CheckArgumentNotNull<Uri>(uri, "uri");
		}

		// Token: 0x06000CB8 RID: 3256 RVA: 0x0002FBFC File Offset: 0x0002DDFC
		private ODataBatchOperationRequestMessage CreateOperationRequestMessageImplementation(string method, Uri uri, string contentId)
		{
			if (this.changeSetBoundary == null)
			{
				this.InterceptException(new Action(this.IncreaseBatchSize));
			}
			else
			{
				this.InterceptException(new Action(this.IncreaseChangeSetSize));
			}
			this.WritePendingMessageData(true);
			if (this.currentOperationContentId != null)
			{
				this.urlResolver.AddContentId(this.currentOperationContentId);
			}
			this.InterceptException(delegate
			{
				uri = ODataBatchUtils.CreateOperationRequestUri(uri, this.rawOutputContext.MessageWriterSettings.PayloadBaseUri, this.urlResolver);
			});
			this.CurrentOperationRequestMessage = ODataBatchOperationRequestMessage.CreateWriteMessage(this.rawOutputContext.OutputStream, method, uri, this, this.urlResolver);
			if (this.changeSetBoundary != null)
			{
				this.RememberContentIdHeader(contentId);
			}
			this.SetState(ODataBatchWriter.BatchWriterState.OperationCreated);
			this.WriteStartBoundaryForOperation();
			ODataBatchWriterUtils.WriteRequestPreamble(this.rawOutputContext.TextWriter, method, uri, this.changeSetBoundary != null, contentId);
			return this.CurrentOperationRequestMessage;
		}

		// Token: 0x06000CB9 RID: 3257 RVA: 0x0002FCE7 File Offset: 0x0002DEE7
		private void VerifyCanCreateOperationResponseMessage(bool synchronousCall)
		{
			this.ValidateWriterReady();
			this.VerifyCallAllowed(synchronousCall);
			if (!this.rawOutputContext.WritingResponse)
			{
				this.ThrowODataException(Strings.ODataBatchWriter_CannotCreateResponseOperationWhenWritingRequest);
			}
		}

		// Token: 0x06000CBA RID: 3258 RVA: 0x0002FD10 File Offset: 0x0002DF10
		private ODataBatchOperationResponseMessage CreateOperationResponseMessageImplementation(string contentId)
		{
			this.WritePendingMessageData(true);
			this.CurrentOperationResponseMessage = ODataBatchOperationResponseMessage.CreateWriteMessage(this.rawOutputContext.OutputStream, this, this.urlResolver.BatchMessageUrlResolver);
			this.SetState(ODataBatchWriter.BatchWriterState.OperationCreated);
			this.WriteStartBoundaryForOperation();
			ODataBatchWriterUtils.WriteResponsePreamble(this.rawOutputContext.TextWriter, this.changeSetBoundary != null, contentId);
			return this.CurrentOperationResponseMessage;
		}

		// Token: 0x06000CBB RID: 3259 RVA: 0x0002FD76 File Offset: 0x0002DF76
		private void StartBatchOperationContent()
		{
			this.WritePendingMessageData(false);
			this.rawOutputContext.TextWriter.Flush();
		}

		// Token: 0x06000CBC RID: 3260 RVA: 0x0002FD8F File Offset: 0x0002DF8F
		private void DisposeBatchWriterAndSetContentStreamRequestedState()
		{
			this.rawOutputContext.CloseWriter();
			this.SetState(ODataBatchWriter.BatchWriterState.OperationStreamRequested);
		}

		// Token: 0x06000CBD RID: 3261 RVA: 0x0002FDA3 File Offset: 0x0002DFA3
		private void RememberContentIdHeader(string contentId)
		{
			this.currentOperationContentId = contentId;
			if (contentId != null && this.urlResolver.ContainsContentId(contentId))
			{
				throw new ODataException(Strings.ODataBatchWriter_DuplicateContentIDsNotAllowed(contentId));
			}
		}

		// Token: 0x06000CBE RID: 3262 RVA: 0x0002FDC9 File Offset: 0x0002DFC9
		private void VerifyCanFlush(bool synchronousCall)
		{
			this.rawOutputContext.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
			if (this.state == ODataBatchWriter.BatchWriterState.OperationStreamRequested)
			{
				this.ThrowODataException(Strings.ODataBatchWriter_FlushOrFlushAsyncCalledInStreamRequestedState);
			}
		}

		// Token: 0x06000CBF RID: 3263 RVA: 0x0002FDF1 File Offset: 0x0002DFF1
		private void VerifyCallAllowed(bool synchronousCall)
		{
			if (synchronousCall && !this.rawOutputContext.Synchronous)
			{
				throw new ODataException(Strings.ODataBatchWriter_SyncCallOnAsyncWriter);
			}
		}

		// Token: 0x06000CC0 RID: 3264 RVA: 0x0002FE10 File Offset: 0x0002E010
		private void InterceptException(Action action)
		{
			try
			{
				action.Invoke();
			}
			catch
			{
				if (!ODataBatchWriter.IsErrorState(this.state))
				{
					this.SetState(ODataBatchWriter.BatchWriterState.Error);
				}
				throw;
			}
		}

		// Token: 0x06000CC1 RID: 3265 RVA: 0x0002FE68 File Offset: 0x0002E068
		private void SetState(ODataBatchWriter.BatchWriterState newState)
		{
			this.InterceptException(delegate
			{
				this.ValidateTransition(newState);
			});
			ODataBatchWriter.BatchWriterState newState2 = newState;
			switch (newState2)
			{
			case ODataBatchWriter.BatchWriterState.BatchStarted:
				break;
			case ODataBatchWriter.BatchWriterState.ChangeSetStarted:
				this.changeSetBoundary = ODataBatchWriterUtils.CreateChangeSetBoundary(this.rawOutputContext.WritingResponse);
				break;
			default:
				if (newState2 == ODataBatchWriter.BatchWriterState.ChangeSetCompleted)
				{
					this.changeSetBoundary = null;
				}
				break;
			}
			this.state = newState;
		}

		// Token: 0x06000CC2 RID: 3266 RVA: 0x0002FEE4 File Offset: 0x0002E0E4
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Validating the transition in the state machine should stay in a single method.")]
		private void ValidateTransition(ODataBatchWriter.BatchWriterState newState)
		{
			if (!ODataBatchWriter.IsErrorState(this.state) && ODataBatchWriter.IsErrorState(newState))
			{
				return;
			}
			if (newState == ODataBatchWriter.BatchWriterState.ChangeSetStarted && this.changeSetBoundary != null)
			{
				throw new ODataException(Strings.ODataBatchWriter_CannotStartChangeSetWithActiveChangeSet);
			}
			if (newState == ODataBatchWriter.BatchWriterState.ChangeSetCompleted && this.changeSetBoundary == null)
			{
				throw new ODataException(Strings.ODataBatchWriter_CannotCompleteChangeSetWithoutActiveChangeSet);
			}
			if (newState == ODataBatchWriter.BatchWriterState.BatchCompleted && this.changeSetBoundary != null)
			{
				throw new ODataException(Strings.ODataBatchWriter_CannotCompleteBatchWithActiveChangeSet);
			}
			switch (this.state)
			{
			case ODataBatchWriter.BatchWriterState.Start:
				if (newState != ODataBatchWriter.BatchWriterState.BatchStarted)
				{
					throw new ODataException(Strings.ODataBatchWriter_InvalidTransitionFromStart);
				}
				break;
			case ODataBatchWriter.BatchWriterState.BatchStarted:
				if (newState != ODataBatchWriter.BatchWriterState.ChangeSetStarted && newState != ODataBatchWriter.BatchWriterState.OperationCreated && newState != ODataBatchWriter.BatchWriterState.BatchCompleted)
				{
					throw new ODataException(Strings.ODataBatchWriter_InvalidTransitionFromBatchStarted);
				}
				break;
			case ODataBatchWriter.BatchWriterState.ChangeSetStarted:
				if (newState != ODataBatchWriter.BatchWriterState.OperationCreated && newState != ODataBatchWriter.BatchWriterState.ChangeSetCompleted)
				{
					throw new ODataException(Strings.ODataBatchWriter_InvalidTransitionFromChangeSetStarted);
				}
				break;
			case ODataBatchWriter.BatchWriterState.OperationCreated:
				if (newState != ODataBatchWriter.BatchWriterState.OperationCreated && newState != ODataBatchWriter.BatchWriterState.OperationStreamRequested && newState != ODataBatchWriter.BatchWriterState.ChangeSetStarted && newState != ODataBatchWriter.BatchWriterState.ChangeSetCompleted && newState != ODataBatchWriter.BatchWriterState.BatchCompleted)
				{
					throw new ODataException(Strings.ODataBatchWriter_InvalidTransitionFromOperationCreated);
				}
				break;
			case ODataBatchWriter.BatchWriterState.OperationStreamRequested:
				if (newState != ODataBatchWriter.BatchWriterState.OperationStreamDisposed)
				{
					throw new ODataException(Strings.ODataBatchWriter_InvalidTransitionFromOperationContentStreamRequested);
				}
				break;
			case ODataBatchWriter.BatchWriterState.OperationStreamDisposed:
				if (newState != ODataBatchWriter.BatchWriterState.OperationCreated && newState != ODataBatchWriter.BatchWriterState.ChangeSetStarted && newState != ODataBatchWriter.BatchWriterState.ChangeSetCompleted && newState != ODataBatchWriter.BatchWriterState.BatchCompleted)
				{
					throw new ODataException(Strings.ODataBatchWriter_InvalidTransitionFromOperationContentStreamDisposed);
				}
				break;
			case ODataBatchWriter.BatchWriterState.ChangeSetCompleted:
				if (newState != ODataBatchWriter.BatchWriterState.BatchCompleted && newState != ODataBatchWriter.BatchWriterState.ChangeSetStarted && newState != ODataBatchWriter.BatchWriterState.OperationCreated)
				{
					throw new ODataException(Strings.ODataBatchWriter_InvalidTransitionFromChangeSetCompleted);
				}
				break;
			case ODataBatchWriter.BatchWriterState.BatchCompleted:
				throw new ODataException(Strings.ODataBatchWriter_InvalidTransitionFromBatchCompleted);
			case ODataBatchWriter.BatchWriterState.Error:
				if (newState != ODataBatchWriter.BatchWriterState.Error)
				{
					throw new ODataException(Strings.ODataWriterCore_InvalidTransitionFromError(this.state.ToString(), newState.ToString()));
				}
				break;
			default:
				throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataBatchWriter_ValidateTransition_UnreachableCodePath));
			}
		}

		// Token: 0x06000CC3 RID: 3267 RVA: 0x00030086 File Offset: 0x0002E286
		private void ValidateWriterReady()
		{
			this.rawOutputContext.VerifyNotDisposed();
			if (this.state == ODataBatchWriter.BatchWriterState.OperationStreamRequested)
			{
				throw new ODataException(Strings.ODataBatchWriter_InvalidTransitionFromOperationContentStreamRequested);
			}
		}

		// Token: 0x06000CC4 RID: 3268 RVA: 0x000300A8 File Offset: 0x0002E2A8
		private void WritePendingMessageData(bool reportMessageCompleted)
		{
			if (this.CurrentOperationMessage != null)
			{
				if (this.CurrentOperationResponseMessage != null)
				{
					int statusCode = this.CurrentOperationResponseMessage.StatusCode;
					string statusMessage = HttpUtils.GetStatusMessage(statusCode);
					this.rawOutputContext.TextWriter.WriteLine("{0} {1} {2}", "HTTP/1.1", statusCode, statusMessage);
				}
				IEnumerable<KeyValuePair<string, string>> headers = this.CurrentOperationMessage.Headers;
				if (headers != null)
				{
					foreach (KeyValuePair<string, string> keyValuePair in headers)
					{
						string key = keyValuePair.Key;
						string value = keyValuePair.Value;
						this.rawOutputContext.TextWriter.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0}: {1}", new object[] { key, value }));
					}
				}
				this.rawOutputContext.TextWriter.WriteLine();
				if (reportMessageCompleted)
				{
					this.CurrentOperationMessage.PartHeaderProcessingCompleted();
					this.CurrentOperationRequestMessage = null;
					this.CurrentOperationResponseMessage = null;
				}
			}
		}

		// Token: 0x06000CC5 RID: 3269 RVA: 0x000301B8 File Offset: 0x0002E3B8
		private void WriteStartBoundaryForOperation()
		{
			if (this.changeSetBoundary == null)
			{
				ODataBatchWriterUtils.WriteStartBoundary(this.rawOutputContext.TextWriter, this.batchBoundary, !this.batchStartBoundaryWritten);
				this.batchStartBoundaryWritten = true;
				return;
			}
			ODataBatchWriterUtils.WriteStartBoundary(this.rawOutputContext.TextWriter, this.changeSetBoundary, !this.changesetStartBoundaryWritten);
			this.changesetStartBoundaryWritten = true;
		}

		// Token: 0x06000CC6 RID: 3270 RVA: 0x0003021A File Offset: 0x0002E41A
		private void ThrowODataException(string errorMessage)
		{
			this.SetState(ODataBatchWriter.BatchWriterState.Error);
			throw new ODataException(errorMessage);
		}

		// Token: 0x06000CC7 RID: 3271 RVA: 0x0003022C File Offset: 0x0002E42C
		private void IncreaseBatchSize()
		{
			this.currentBatchSize += 1U;
			if ((ulong)this.currentBatchSize > (ulong)((long)this.rawOutputContext.MessageWriterSettings.MessageQuotas.MaxPartsPerBatch))
			{
				throw new ODataException(Strings.ODataBatchWriter_MaxBatchSizeExceeded(this.rawOutputContext.MessageWriterSettings.MessageQuotas.MaxPartsPerBatch));
			}
		}

		// Token: 0x06000CC8 RID: 3272 RVA: 0x0003028C File Offset: 0x0002E48C
		private void IncreaseChangeSetSize()
		{
			this.currentChangeSetSize += 1U;
			if ((ulong)this.currentChangeSetSize > (ulong)((long)this.rawOutputContext.MessageWriterSettings.MessageQuotas.MaxOperationsPerChangeset))
			{
				throw new ODataException(Strings.ODataBatchWriter_MaxChangeSetSizeExceeded(this.rawOutputContext.MessageWriterSettings.MessageQuotas.MaxOperationsPerChangeset));
			}
		}

		// Token: 0x06000CC9 RID: 3273 RVA: 0x000302EB File Offset: 0x0002E4EB
		private void ResetChangeSetSize()
		{
			this.currentChangeSetSize = 0U;
		}

		// Token: 0x04000540 RID: 1344
		private readonly ODataRawOutputContext rawOutputContext;

		// Token: 0x04000541 RID: 1345
		private readonly string batchBoundary;

		// Token: 0x04000542 RID: 1346
		private readonly ODataBatchUrlResolver urlResolver;

		// Token: 0x04000543 RID: 1347
		private ODataBatchWriter.BatchWriterState state;

		// Token: 0x04000544 RID: 1348
		private string changeSetBoundary;

		// Token: 0x04000545 RID: 1349
		private bool batchStartBoundaryWritten;

		// Token: 0x04000546 RID: 1350
		private bool changesetStartBoundaryWritten;

		// Token: 0x04000547 RID: 1351
		private ODataBatchOperationRequestMessage currentOperationRequestMessage;

		// Token: 0x04000548 RID: 1352
		private ODataBatchOperationResponseMessage currentOperationResponseMessage;

		// Token: 0x04000549 RID: 1353
		private string currentOperationContentId;

		// Token: 0x0400054A RID: 1354
		private uint currentBatchSize;

		// Token: 0x0400054B RID: 1355
		private uint currentChangeSetSize;

		// Token: 0x0200014F RID: 335
		private enum BatchWriterState
		{
			// Token: 0x0400054D RID: 1357
			Start,
			// Token: 0x0400054E RID: 1358
			BatchStarted,
			// Token: 0x0400054F RID: 1359
			ChangeSetStarted,
			// Token: 0x04000550 RID: 1360
			OperationCreated,
			// Token: 0x04000551 RID: 1361
			OperationStreamRequested,
			// Token: 0x04000552 RID: 1362
			OperationStreamDisposed,
			// Token: 0x04000553 RID: 1363
			ChangeSetCompleted,
			// Token: 0x04000554 RID: 1364
			BatchCompleted,
			// Token: 0x04000555 RID: 1365
			Error
		}
	}
}
