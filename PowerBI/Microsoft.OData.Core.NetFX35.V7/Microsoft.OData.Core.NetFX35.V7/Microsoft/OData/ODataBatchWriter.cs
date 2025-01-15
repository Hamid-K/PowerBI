using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Microsoft.OData
{
	// Token: 0x0200003C RID: 60
	public sealed class ODataBatchWriter : IODataBatchOperationListener, IODataOutputInStreamErrorListener
	{
		// Token: 0x060001C6 RID: 454 RVA: 0x000079F0 File Offset: 0x00005BF0
		internal ODataBatchWriter(ODataRawOutputContext rawOutputContext, string batchBoundary)
		{
			ExceptionUtils.CheckArgumentNotNull<string>(batchBoundary, "batchBoundary");
			this.rawOutputContext = rawOutputContext;
			this.container = rawOutputContext.Container;
			this.batchBoundary = batchBoundary;
			this.payloadUriConverter = new ODataBatchPayloadUriConverter(rawOutputContext.PayloadUriConverter);
			this.rawOutputContext.InitializeRawValueWriter();
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x00007A45 File Offset: 0x00005C45
		// (set) Token: 0x060001C8 RID: 456 RVA: 0x00007A4D File Offset: 0x00005C4D
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

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x00007A56 File Offset: 0x00005C56
		// (set) Token: 0x060001CA RID: 458 RVA: 0x00007A5E File Offset: 0x00005C5E
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

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060001CB RID: 459 RVA: 0x00007A67 File Offset: 0x00005C67
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

		// Token: 0x060001CC RID: 460 RVA: 0x00007A92 File Offset: 0x00005C92
		public void WriteStartBatch()
		{
			this.VerifyCanWriteStartBatch(true);
			this.WriteStartBatchImplementation();
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00007AA1 File Offset: 0x00005CA1
		public void WriteEndBatch()
		{
			this.VerifyCanWriteEndBatch(true);
			this.WriteEndBatchImplementation();
			this.Flush();
		}

		// Token: 0x060001CE RID: 462 RVA: 0x00007AB6 File Offset: 0x00005CB6
		public void WriteStartChangeset()
		{
			this.VerifyCanWriteStartChangeset(true);
			this.WriteStartChangesetImplementation();
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00007AC5 File Offset: 0x00005CC5
		public void WriteEndChangeset()
		{
			this.VerifyCanWriteEndChangeset(true);
			this.WriteEndChangesetImplementation();
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00007AD4 File Offset: 0x00005CD4
		public ODataBatchOperationRequestMessage CreateOperationRequestMessage(string method, Uri uri, string contentId)
		{
			return this.CreateOperationRequestMessage(method, uri, contentId, BatchPayloadUriOption.AbsoluteUri);
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00007AE0 File Offset: 0x00005CE0
		public ODataBatchOperationRequestMessage CreateOperationRequestMessage(string method, Uri uri, string contentId, BatchPayloadUriOption payloadUriOption)
		{
			this.VerifyCanCreateOperationRequestMessage(true, method, uri, contentId);
			return this.CreateOperationRequestMessageImplementation(method, uri, contentId, payloadUriOption);
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x00007AF7 File Offset: 0x00005CF7
		public ODataBatchOperationResponseMessage CreateOperationResponseMessage(string contentId)
		{
			this.VerifyCanCreateOperationResponseMessage(true);
			return this.CreateOperationResponseMessageImplementation(contentId);
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x00007B08 File Offset: 0x00005D08
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

		// Token: 0x060001D4 RID: 468 RVA: 0x00007B44 File Offset: 0x00005D44
		void IODataBatchOperationListener.BatchOperationContentStreamRequested()
		{
			this.StartBatchOperationContent();
			this.rawOutputContext.FlushBuffers();
			this.DisposeBatchWriterAndSetContentStreamRequestedState();
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x00007B5D File Offset: 0x00005D5D
		void IODataBatchOperationListener.BatchOperationContentStreamDisposed()
		{
			this.SetState(ODataBatchWriter.BatchWriterState.OperationStreamDisposed);
			this.CurrentOperationRequestMessage = null;
			this.CurrentOperationResponseMessage = null;
			this.rawOutputContext.InitializeRawValueWriter();
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x00007B7F File Offset: 0x00005D7F
		void IODataOutputInStreamErrorListener.OnInStreamError()
		{
			this.rawOutputContext.VerifyNotDisposed();
			this.SetState(ODataBatchWriter.BatchWriterState.Error);
			this.rawOutputContext.TextWriter.Flush();
			throw new ODataException(Strings.ODataBatchWriter_CannotWriteInStreamErrorForBatch);
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x00007BAD File Offset: 0x00005DAD
		private static bool IsErrorState(ODataBatchWriter.BatchWriterState state)
		{
			return state == ODataBatchWriter.BatchWriterState.Error;
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x00007BB3 File Offset: 0x00005DB3
		private void VerifyCanWriteStartBatch(bool synchronousCall)
		{
			this.ValidateWriterReady();
			this.VerifyCallAllowed(synchronousCall);
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x00007BC2 File Offset: 0x00005DC2
		private void WriteStartBatchImplementation()
		{
			this.SetState(ODataBatchWriter.BatchWriterState.BatchStarted);
		}

		// Token: 0x060001DA RID: 474 RVA: 0x00007BB3 File Offset: 0x00005DB3
		private void VerifyCanWriteEndBatch(bool synchronousCall)
		{
			this.ValidateWriterReady();
			this.VerifyCallAllowed(synchronousCall);
		}

		// Token: 0x060001DB RID: 475 RVA: 0x00007BCB File Offset: 0x00005DCB
		private void WriteEndBatchImplementation()
		{
			this.WritePendingMessageData(true);
			this.SetState(ODataBatchWriter.BatchWriterState.BatchCompleted);
			ODataBatchWriterUtils.WriteEndBoundary(this.rawOutputContext.TextWriter, this.batchBoundary, !this.batchStartBoundaryWritten);
			this.rawOutputContext.TextWriter.WriteLine();
		}

		// Token: 0x060001DC RID: 476 RVA: 0x00007BB3 File Offset: 0x00005DB3
		private void VerifyCanWriteStartChangeset(bool synchronousCall)
		{
			this.ValidateWriterReady();
			this.VerifyCallAllowed(synchronousCall);
		}

		// Token: 0x060001DD RID: 477 RVA: 0x00007C0C File Offset: 0x00005E0C
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

		// Token: 0x060001DE RID: 478 RVA: 0x00007BB3 File Offset: 0x00005DB3
		private void VerifyCanWriteEndChangeset(bool synchronousCall)
		{
			this.ValidateWriterReady();
			this.VerifyCallAllowed(synchronousCall);
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00007C84 File Offset: 0x00005E84
		private void WriteEndChangesetImplementation()
		{
			this.WritePendingMessageData(true);
			string text = this.changeSetBoundary;
			this.SetState(ODataBatchWriter.BatchWriterState.ChangeSetCompleted);
			ODataBatchWriterUtils.WriteEndBoundary(this.rawOutputContext.TextWriter, text, !this.changesetStartBoundaryWritten);
			this.payloadUriConverter.Reset();
			this.currentOperationContentId = null;
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00007CD4 File Offset: 0x00005ED4
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

		// Token: 0x060001E1 RID: 481 RVA: 0x00007D54 File Offset: 0x00005F54
		private ODataBatchOperationRequestMessage CreateOperationRequestMessageImplementation(string method, Uri uri, string contentId, BatchPayloadUriOption payloadUriOption)
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
				this.payloadUriConverter.AddContentId(this.currentOperationContentId);
			}
			this.InterceptException(delegate
			{
				uri = ODataBatchUtils.CreateOperationRequestUri(uri, this.rawOutputContext.MessageWriterSettings.BaseUri, this.payloadUriConverter);
			});
			this.CurrentOperationRequestMessage = ODataBatchOperationRequestMessage.CreateWriteMessage(this.rawOutputContext.OutputStream, method, uri, this, this.payloadUriConverter, this.container);
			if (this.changeSetBoundary != null)
			{
				this.RememberContentIdHeader(contentId);
			}
			this.SetState(ODataBatchWriter.BatchWriterState.OperationCreated);
			this.WriteStartBoundaryForOperation();
			ODataBatchWriterUtils.WriteRequestPreamble(this.rawOutputContext.TextWriter, method, uri, this.rawOutputContext.MessageWriterSettings.BaseUri, this.changeSetBoundary != null, contentId, payloadUriOption);
			return this.CurrentOperationRequestMessage;
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00007E54 File Offset: 0x00006054
		private void VerifyCanCreateOperationResponseMessage(bool synchronousCall)
		{
			this.ValidateWriterReady();
			this.VerifyCallAllowed(synchronousCall);
			if (!this.rawOutputContext.WritingResponse)
			{
				this.ThrowODataException(Strings.ODataBatchWriter_CannotCreateResponseOperationWhenWritingRequest);
			}
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00007E7C File Offset: 0x0000607C
		private ODataBatchOperationResponseMessage CreateOperationResponseMessageImplementation(string contentId)
		{
			this.WritePendingMessageData(true);
			this.CurrentOperationResponseMessage = ODataBatchOperationResponseMessage.CreateWriteMessage(this.rawOutputContext.OutputStream, this, this.payloadUriConverter.BatchMessagePayloadUriConverter, this.container);
			this.SetState(ODataBatchWriter.BatchWriterState.OperationCreated);
			this.WriteStartBoundaryForOperation();
			ODataBatchWriterUtils.WriteResponsePreamble(this.rawOutputContext.TextWriter, this.changeSetBoundary != null, contentId);
			return this.CurrentOperationResponseMessage;
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00007EE5 File Offset: 0x000060E5
		private void StartBatchOperationContent()
		{
			this.WritePendingMessageData(false);
			this.rawOutputContext.TextWriter.Flush();
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00007EFE File Offset: 0x000060FE
		private void DisposeBatchWriterAndSetContentStreamRequestedState()
		{
			this.rawOutputContext.CloseWriter();
			this.SetState(ODataBatchWriter.BatchWriterState.OperationStreamRequested);
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00007F12 File Offset: 0x00006112
		private void RememberContentIdHeader(string contentId)
		{
			this.currentOperationContentId = contentId;
			if (contentId != null && this.payloadUriConverter.ContainsContentId(contentId))
			{
				throw new ODataException(Strings.ODataBatchWriter_DuplicateContentIDsNotAllowed(contentId));
			}
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00007F38 File Offset: 0x00006138
		private void VerifyCanFlush(bool synchronousCall)
		{
			this.rawOutputContext.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
			if (this.state == ODataBatchWriter.BatchWriterState.OperationStreamRequested)
			{
				this.ThrowODataException(Strings.ODataBatchWriter_FlushOrFlushAsyncCalledInStreamRequestedState);
			}
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00007F60 File Offset: 0x00006160
		private void VerifyCallAllowed(bool synchronousCall)
		{
			if (synchronousCall && !this.rawOutputContext.Synchronous)
			{
				throw new ODataException(Strings.ODataBatchWriter_SyncCallOnAsyncWriter);
			}
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00007F80 File Offset: 0x00006180
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

		// Token: 0x060001EA RID: 490 RVA: 0x00007FBC File Offset: 0x000061BC
		private void SetState(ODataBatchWriter.BatchWriterState newState)
		{
			this.InterceptException(delegate
			{
				this.ValidateTransition(newState);
			});
			ODataBatchWriter.BatchWriterState newState2 = newState;
			if (newState2 != ODataBatchWriter.BatchWriterState.BatchStarted)
			{
				if (newState2 != ODataBatchWriter.BatchWriterState.ChangeSetStarted)
				{
					if (newState2 == ODataBatchWriter.BatchWriterState.ChangeSetCompleted)
					{
						this.changeSetBoundary = null;
					}
				}
				else
				{
					this.changeSetBoundary = ODataBatchWriterUtils.CreateChangeSetBoundary(this.rawOutputContext.WritingResponse);
				}
			}
			this.state = newState;
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00008030 File Offset: 0x00006230
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

		// Token: 0x060001EC RID: 492 RVA: 0x000081D5 File Offset: 0x000063D5
		private void ValidateWriterReady()
		{
			this.rawOutputContext.VerifyNotDisposed();
			if (this.state == ODataBatchWriter.BatchWriterState.OperationStreamRequested)
			{
				throw new ODataException(Strings.ODataBatchWriter_InvalidTransitionFromOperationContentStreamRequested);
			}
		}

		// Token: 0x060001ED RID: 493 RVA: 0x000081F8 File Offset: 0x000063F8
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

		// Token: 0x060001EE RID: 494 RVA: 0x00008300 File Offset: 0x00006500
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

		// Token: 0x060001EF RID: 495 RVA: 0x00008362 File Offset: 0x00006562
		private void ThrowODataException(string errorMessage)
		{
			this.SetState(ODataBatchWriter.BatchWriterState.Error);
			throw new ODataException(errorMessage);
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00008374 File Offset: 0x00006574
		private void IncreaseBatchSize()
		{
			this.currentBatchSize += 1U;
			if ((ulong)this.currentBatchSize > (ulong)((long)this.rawOutputContext.MessageWriterSettings.MessageQuotas.MaxPartsPerBatch))
			{
				throw new ODataException(Strings.ODataBatchWriter_MaxBatchSizeExceeded(this.rawOutputContext.MessageWriterSettings.MessageQuotas.MaxPartsPerBatch));
			}
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x000083D4 File Offset: 0x000065D4
		private void IncreaseChangeSetSize()
		{
			this.currentChangeSetSize += 1U;
			if ((ulong)this.currentChangeSetSize > (ulong)((long)this.rawOutputContext.MessageWriterSettings.MessageQuotas.MaxOperationsPerChangeset))
			{
				throw new ODataException(Strings.ODataBatchWriter_MaxChangeSetSizeExceeded(this.rawOutputContext.MessageWriterSettings.MessageQuotas.MaxOperationsPerChangeset));
			}
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00008433 File Offset: 0x00006633
		private void ResetChangeSetSize()
		{
			this.currentChangeSetSize = 0U;
		}

		// Token: 0x04000109 RID: 265
		private readonly ODataRawOutputContext rawOutputContext;

		// Token: 0x0400010A RID: 266
		private readonly string batchBoundary;

		// Token: 0x0400010B RID: 267
		private readonly ODataBatchPayloadUriConverter payloadUriConverter;

		// Token: 0x0400010C RID: 268
		private readonly IServiceProvider container;

		// Token: 0x0400010D RID: 269
		private ODataBatchWriter.BatchWriterState state;

		// Token: 0x0400010E RID: 270
		private string changeSetBoundary;

		// Token: 0x0400010F RID: 271
		private bool batchStartBoundaryWritten;

		// Token: 0x04000110 RID: 272
		private bool changesetStartBoundaryWritten;

		// Token: 0x04000111 RID: 273
		private ODataBatchOperationRequestMessage currentOperationRequestMessage;

		// Token: 0x04000112 RID: 274
		private ODataBatchOperationResponseMessage currentOperationResponseMessage;

		// Token: 0x04000113 RID: 275
		private string currentOperationContentId;

		// Token: 0x04000114 RID: 276
		private uint currentBatchSize;

		// Token: 0x04000115 RID: 277
		private uint currentChangeSetSize;

		// Token: 0x02000255 RID: 597
		private enum BatchWriterState
		{
			// Token: 0x04000ADC RID: 2780
			Start,
			// Token: 0x04000ADD RID: 2781
			BatchStarted,
			// Token: 0x04000ADE RID: 2782
			ChangeSetStarted,
			// Token: 0x04000ADF RID: 2783
			OperationCreated,
			// Token: 0x04000AE0 RID: 2784
			OperationStreamRequested,
			// Token: 0x04000AE1 RID: 2785
			OperationStreamDisposed,
			// Token: 0x04000AE2 RID: 2786
			ChangeSetCompleted,
			// Token: 0x04000AE3 RID: 2787
			BatchCompleted,
			// Token: 0x04000AE4 RID: 2788
			Error
		}
	}
}
