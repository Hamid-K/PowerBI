using System;
using System.Collections.Generic;
using System.Globalization;

namespace Microsoft.Data.OData
{
	// Token: 0x0200026D RID: 621
	public sealed class ODataBatchWriter : IODataBatchOperationListener, IODataOutputInStreamErrorListener
	{
		// Token: 0x06001359 RID: 4953 RVA: 0x00048A97 File Offset: 0x00046C97
		internal ODataBatchWriter(ODataRawOutputContext rawOutputContext, string batchBoundary)
		{
			ExceptionUtils.CheckArgumentNotNull<string>(batchBoundary, "batchBoundary");
			this.rawOutputContext = rawOutputContext;
			this.batchBoundary = batchBoundary;
			this.urlResolver = new ODataBatchUrlResolver(rawOutputContext.UrlResolver);
			this.rawOutputContext.InitializeRawValueWriter();
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x0600135A RID: 4954 RVA: 0x00048AD4 File Offset: 0x00046CD4
		// (set) Token: 0x0600135B RID: 4955 RVA: 0x00048ADC File Offset: 0x00046CDC
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

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x0600135C RID: 4956 RVA: 0x00048AE5 File Offset: 0x00046CE5
		// (set) Token: 0x0600135D RID: 4957 RVA: 0x00048AED File Offset: 0x00046CED
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

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x0600135E RID: 4958 RVA: 0x00048AF6 File Offset: 0x00046CF6
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

		// Token: 0x0600135F RID: 4959 RVA: 0x00048B21 File Offset: 0x00046D21
		public void WriteStartBatch()
		{
			this.VerifyCanWriteStartBatch(true);
			this.WriteStartBatchImplementation();
		}

		// Token: 0x06001360 RID: 4960 RVA: 0x00048B30 File Offset: 0x00046D30
		public void WriteEndBatch()
		{
			this.VerifyCanWriteEndBatch(true);
			this.WriteEndBatchImplementation();
			this.Flush();
		}

		// Token: 0x06001361 RID: 4961 RVA: 0x00048B45 File Offset: 0x00046D45
		public void WriteStartChangeset()
		{
			this.VerifyCanWriteStartChangeset(true);
			this.WriteStartChangesetImplementation();
		}

		// Token: 0x06001362 RID: 4962 RVA: 0x00048B54 File Offset: 0x00046D54
		public void WriteEndChangeset()
		{
			this.VerifyCanWriteEndChangeset(true);
			this.WriteEndChangesetImplementation();
		}

		// Token: 0x06001363 RID: 4963 RVA: 0x00048B63 File Offset: 0x00046D63
		public ODataBatchOperationRequestMessage CreateOperationRequestMessage(string method, Uri uri)
		{
			this.VerifyCanCreateOperationRequestMessage(true, method, uri);
			return this.CreateOperationRequestMessageImplementation(method, uri);
		}

		// Token: 0x06001364 RID: 4964 RVA: 0x00048B76 File Offset: 0x00046D76
		public ODataBatchOperationResponseMessage CreateOperationResponseMessage()
		{
			this.VerifyCanCreateOperationResponseMessage(true);
			return this.CreateOperationResponseMessageImplementation();
		}

		// Token: 0x06001365 RID: 4965 RVA: 0x00048B88 File Offset: 0x00046D88
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

		// Token: 0x06001366 RID: 4966 RVA: 0x00048BC4 File Offset: 0x00046DC4
		void IODataBatchOperationListener.BatchOperationContentStreamRequested()
		{
			this.StartBatchOperationContent();
			this.rawOutputContext.FlushBuffers();
			this.DisposeBatchWriterAndSetContentStreamRequestedState();
		}

		// Token: 0x06001367 RID: 4967 RVA: 0x00048BDD File Offset: 0x00046DDD
		void IODataBatchOperationListener.BatchOperationContentStreamDisposed()
		{
			this.SetState(ODataBatchWriter.BatchWriterState.OperationStreamDisposed);
			this.CurrentOperationRequestMessage = null;
			this.CurrentOperationResponseMessage = null;
			this.rawOutputContext.InitializeRawValueWriter();
		}

		// Token: 0x06001368 RID: 4968 RVA: 0x00048BFF File Offset: 0x00046DFF
		void IODataOutputInStreamErrorListener.OnInStreamError()
		{
			this.rawOutputContext.VerifyNotDisposed();
			this.SetState(ODataBatchWriter.BatchWriterState.Error);
			this.rawOutputContext.TextWriter.Flush();
			throw new ODataException(Strings.ODataBatchWriter_CannotWriteInStreamErrorForBatch);
		}

		// Token: 0x06001369 RID: 4969 RVA: 0x00048C2D File Offset: 0x00046E2D
		private static bool IsErrorState(ODataBatchWriter.BatchWriterState state)
		{
			return state == ODataBatchWriter.BatchWriterState.Error;
		}

		// Token: 0x0600136A RID: 4970 RVA: 0x00048C33 File Offset: 0x00046E33
		private void VerifyCanWriteStartBatch(bool synchronousCall)
		{
			this.ValidateWriterReady();
			this.VerifyCallAllowed(synchronousCall);
		}

		// Token: 0x0600136B RID: 4971 RVA: 0x00048C42 File Offset: 0x00046E42
		private void WriteStartBatchImplementation()
		{
			this.SetState(ODataBatchWriter.BatchWriterState.BatchStarted);
		}

		// Token: 0x0600136C RID: 4972 RVA: 0x00048C4B File Offset: 0x00046E4B
		private void VerifyCanWriteEndBatch(bool synchronousCall)
		{
			this.ValidateWriterReady();
			this.VerifyCallAllowed(synchronousCall);
		}

		// Token: 0x0600136D RID: 4973 RVA: 0x00048C5A File Offset: 0x00046E5A
		private void WriteEndBatchImplementation()
		{
			this.WritePendingMessageData(true);
			this.SetState(ODataBatchWriter.BatchWriterState.BatchCompleted);
			ODataBatchWriterUtils.WriteEndBoundary(this.rawOutputContext.TextWriter, this.batchBoundary, !this.batchStartBoundaryWritten);
			this.rawOutputContext.TextWriter.WriteLine();
		}

		// Token: 0x0600136E RID: 4974 RVA: 0x00048C99 File Offset: 0x00046E99
		private void VerifyCanWriteStartChangeset(bool synchronousCall)
		{
			this.ValidateWriterReady();
			this.VerifyCallAllowed(synchronousCall);
		}

		// Token: 0x0600136F RID: 4975 RVA: 0x00048CA8 File Offset: 0x00046EA8
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

		// Token: 0x06001370 RID: 4976 RVA: 0x00048D1E File Offset: 0x00046F1E
		private void VerifyCanWriteEndChangeset(bool synchronousCall)
		{
			this.ValidateWriterReady();
			this.VerifyCallAllowed(synchronousCall);
		}

		// Token: 0x06001371 RID: 4977 RVA: 0x00048D30 File Offset: 0x00046F30
		private void WriteEndChangesetImplementation()
		{
			this.WritePendingMessageData(true);
			string text = this.changeSetBoundary;
			this.SetState(ODataBatchWriter.BatchWriterState.ChangeSetCompleted);
			ODataBatchWriterUtils.WriteEndBoundary(this.rawOutputContext.TextWriter, text, !this.changesetStartBoundaryWritten);
			this.urlResolver.Reset();
			this.currentOperationContentId = null;
		}

		// Token: 0x06001372 RID: 4978 RVA: 0x00048D80 File Offset: 0x00046F80
		private void VerifyCanCreateOperationRequestMessage(bool synchronousCall, string method, Uri uri)
		{
			this.ValidateWriterReady();
			this.VerifyCallAllowed(synchronousCall);
			if (this.rawOutputContext.WritingResponse)
			{
				this.ThrowODataException(Strings.ODataBatchWriter_CannotCreateRequestOperationWhenWritingResponse);
			}
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(method, "method");
			if (this.changeSetBoundary == null)
			{
				if (!HttpUtils.IsQueryMethod(method))
				{
					this.ThrowODataException(Strings.ODataBatch_InvalidHttpMethodForQueryOperation(method));
				}
			}
			else if (HttpUtils.IsQueryMethod(method))
			{
				this.ThrowODataException(Strings.ODataBatch_InvalidHttpMethodForChangeSetRequest(method));
			}
			ExceptionUtils.CheckArgumentNotNull<Uri>(uri, "uri");
		}

		// Token: 0x06001373 RID: 4979 RVA: 0x00048E38 File Offset: 0x00047038
		private ODataBatchOperationRequestMessage CreateOperationRequestMessageImplementation(string method, Uri uri)
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
				uri = ODataBatchUtils.CreateOperationRequestUri(uri, this.rawOutputContext.MessageWriterSettings.BaseUri, this.urlResolver);
			});
			this.CurrentOperationRequestMessage = ODataBatchOperationRequestMessage.CreateWriteMessage(this.rawOutputContext.OutputStream, method, uri, this, this.urlResolver);
			this.SetState(ODataBatchWriter.BatchWriterState.OperationCreated);
			this.WriteStartBoundaryForOperation();
			ODataBatchWriterUtils.WriteRequestPreamble(this.rawOutputContext.TextWriter, method, uri);
			return this.CurrentOperationRequestMessage;
		}

		// Token: 0x06001374 RID: 4980 RVA: 0x00048F07 File Offset: 0x00047107
		private void VerifyCanCreateOperationResponseMessage(bool synchronousCall)
		{
			this.ValidateWriterReady();
			this.VerifyCallAllowed(synchronousCall);
			if (!this.rawOutputContext.WritingResponse)
			{
				this.ThrowODataException(Strings.ODataBatchWriter_CannotCreateResponseOperationWhenWritingRequest);
			}
		}

		// Token: 0x06001375 RID: 4981 RVA: 0x00048F30 File Offset: 0x00047130
		private ODataBatchOperationResponseMessage CreateOperationResponseMessageImplementation()
		{
			this.WritePendingMessageData(true);
			this.CurrentOperationResponseMessage = ODataBatchOperationResponseMessage.CreateWriteMessage(this.rawOutputContext.OutputStream, this, this.urlResolver.BatchMessageUrlResolver);
			this.SetState(ODataBatchWriter.BatchWriterState.OperationCreated);
			this.WriteStartBoundaryForOperation();
			ODataBatchWriterUtils.WriteResponsePreamble(this.rawOutputContext.TextWriter);
			return this.CurrentOperationResponseMessage;
		}

		// Token: 0x06001376 RID: 4982 RVA: 0x00048F89 File Offset: 0x00047189
		private void StartBatchOperationContent()
		{
			this.WritePendingMessageData(false);
			this.rawOutputContext.TextWriter.Flush();
		}

		// Token: 0x06001377 RID: 4983 RVA: 0x00048FA2 File Offset: 0x000471A2
		private void DisposeBatchWriterAndSetContentStreamRequestedState()
		{
			this.rawOutputContext.CloseWriter();
			this.SetState(ODataBatchWriter.BatchWriterState.OperationStreamRequested);
		}

		// Token: 0x06001378 RID: 4984 RVA: 0x00048FB6 File Offset: 0x000471B6
		private void RememberContentIdHeader(string contentId)
		{
			this.currentOperationContentId = contentId;
			if (contentId != null && this.urlResolver.ContainsContentId(contentId))
			{
				throw new ODataException(Strings.ODataBatchWriter_DuplicateContentIDsNotAllowed(contentId));
			}
		}

		// Token: 0x06001379 RID: 4985 RVA: 0x00048FDC File Offset: 0x000471DC
		private void VerifyCanFlush(bool synchronousCall)
		{
			this.rawOutputContext.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
			if (this.state == ODataBatchWriter.BatchWriterState.OperationStreamRequested)
			{
				this.ThrowODataException(Strings.ODataBatchWriter_FlushOrFlushAsyncCalledInStreamRequestedState);
			}
		}

		// Token: 0x0600137A RID: 4986 RVA: 0x00049004 File Offset: 0x00047204
		private void VerifyCallAllowed(bool synchronousCall)
		{
			if (synchronousCall && !this.rawOutputContext.Synchronous)
			{
				throw new ODataException(Strings.ODataBatchWriter_SyncCallOnAsyncWriter);
			}
		}

		// Token: 0x0600137B RID: 4987 RVA: 0x00049024 File Offset: 0x00047224
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

		// Token: 0x0600137C RID: 4988 RVA: 0x0004907C File Offset: 0x0004727C
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

		// Token: 0x0600137D RID: 4989 RVA: 0x000490F8 File Offset: 0x000472F8
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

		// Token: 0x0600137E RID: 4990 RVA: 0x0004929A File Offset: 0x0004749A
		private void ValidateWriterReady()
		{
			this.rawOutputContext.VerifyNotDisposed();
			if (this.state == ODataBatchWriter.BatchWriterState.OperationStreamRequested)
			{
				throw new ODataException(Strings.ODataBatchWriter_InvalidTransitionFromOperationContentStreamRequested);
			}
		}

		// Token: 0x0600137F RID: 4991 RVA: 0x000492BC File Offset: 0x000474BC
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
				bool flag = this.CurrentOperationRequestMessage != null && this.changeSetBoundary != null;
				string text = null;
				IEnumerable<KeyValuePair<string, string>> headers = this.CurrentOperationMessage.Headers;
				if (headers != null)
				{
					foreach (KeyValuePair<string, string> keyValuePair in headers)
					{
						string key = keyValuePair.Key;
						string value = keyValuePair.Value;
						this.rawOutputContext.TextWriter.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0}: {1}", new object[] { key, value }));
						if (flag && string.CompareOrdinal("Content-ID", key) == 0)
						{
							text = value;
						}
					}
				}
				if (flag)
				{
					this.RememberContentIdHeader(text);
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

		// Token: 0x06001380 RID: 4992 RVA: 0x0004940C File Offset: 0x0004760C
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

		// Token: 0x06001381 RID: 4993 RVA: 0x0004946E File Offset: 0x0004766E
		private void ThrowODataException(string errorMessage)
		{
			this.SetState(ODataBatchWriter.BatchWriterState.Error);
			throw new ODataException(errorMessage);
		}

		// Token: 0x06001382 RID: 4994 RVA: 0x00049480 File Offset: 0x00047680
		private void IncreaseBatchSize()
		{
			this.currentBatchSize += 1U;
			if ((ulong)this.currentBatchSize > (ulong)((long)this.rawOutputContext.MessageWriterSettings.MessageQuotas.MaxPartsPerBatch))
			{
				throw new ODataException(Strings.ODataBatchWriter_MaxBatchSizeExceeded(this.rawOutputContext.MessageWriterSettings.MessageQuotas.MaxPartsPerBatch));
			}
		}

		// Token: 0x06001383 RID: 4995 RVA: 0x000494E0 File Offset: 0x000476E0
		private void IncreaseChangeSetSize()
		{
			this.currentChangeSetSize += 1U;
			if ((ulong)this.currentChangeSetSize > (ulong)((long)this.rawOutputContext.MessageWriterSettings.MessageQuotas.MaxOperationsPerChangeset))
			{
				throw new ODataException(Strings.ODataBatchWriter_MaxChangeSetSizeExceeded(this.rawOutputContext.MessageWriterSettings.MessageQuotas.MaxOperationsPerChangeset));
			}
		}

		// Token: 0x06001384 RID: 4996 RVA: 0x0004953F File Offset: 0x0004773F
		private void ResetChangeSetSize()
		{
			this.currentChangeSetSize = 0U;
		}

		// Token: 0x04000730 RID: 1840
		private readonly ODataRawOutputContext rawOutputContext;

		// Token: 0x04000731 RID: 1841
		private readonly string batchBoundary;

		// Token: 0x04000732 RID: 1842
		private readonly ODataBatchUrlResolver urlResolver;

		// Token: 0x04000733 RID: 1843
		private ODataBatchWriter.BatchWriterState state;

		// Token: 0x04000734 RID: 1844
		private string changeSetBoundary;

		// Token: 0x04000735 RID: 1845
		private bool batchStartBoundaryWritten;

		// Token: 0x04000736 RID: 1846
		private bool changesetStartBoundaryWritten;

		// Token: 0x04000737 RID: 1847
		private ODataBatchOperationRequestMessage currentOperationRequestMessage;

		// Token: 0x04000738 RID: 1848
		private ODataBatchOperationResponseMessage currentOperationResponseMessage;

		// Token: 0x04000739 RID: 1849
		private string currentOperationContentId;

		// Token: 0x0400073A RID: 1850
		private uint currentBatchSize;

		// Token: 0x0400073B RID: 1851
		private uint currentChangeSetSize;

		// Token: 0x0200026E RID: 622
		private enum BatchWriterState
		{
			// Token: 0x0400073D RID: 1853
			Start,
			// Token: 0x0400073E RID: 1854
			BatchStarted,
			// Token: 0x0400073F RID: 1855
			ChangeSetStarted,
			// Token: 0x04000740 RID: 1856
			OperationCreated,
			// Token: 0x04000741 RID: 1857
			OperationStreamRequested,
			// Token: 0x04000742 RID: 1858
			OperationStreamDisposed,
			// Token: 0x04000743 RID: 1859
			ChangeSetCompleted,
			// Token: 0x04000744 RID: 1860
			BatchCompleted,
			// Token: 0x04000745 RID: 1861
			Error
		}
	}
}
