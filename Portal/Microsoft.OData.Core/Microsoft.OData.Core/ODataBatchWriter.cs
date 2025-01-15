using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading.Tasks;

namespace Microsoft.OData
{
	// Token: 0x02000060 RID: 96
	public abstract class ODataBatchWriter : IODataStreamListener, IODataOutputInStreamErrorListener
	{
		// Token: 0x06000324 RID: 804 RVA: 0x000097F6 File Offset: 0x000079F6
		internal ODataBatchWriter(ODataOutputContext outputContext)
		{
			this.outputContext = outputContext;
			this.container = outputContext.Container;
			this.payloadUriConverter = new ODataBatchPayloadUriConverter(outputContext.PayloadUriConverter);
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000325 RID: 805 RVA: 0x00009822 File Offset: 0x00007A22
		// (set) Token: 0x06000326 RID: 806 RVA: 0x0000982A File Offset: 0x00007A2A
		protected ODataBatchOperationRequestMessage CurrentOperationRequestMessage
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

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000327 RID: 807 RVA: 0x00009833 File Offset: 0x00007A33
		// (set) Token: 0x06000328 RID: 808 RVA: 0x0000983B File Offset: 0x00007A3B
		protected ODataBatchOperationResponseMessage CurrentOperationResponseMessage
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

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000329 RID: 809 RVA: 0x00009844 File Offset: 0x00007A44
		protected ODataOutputContext OutputContext
		{
			get
			{
				return this.outputContext;
			}
		}

		// Token: 0x0600032A RID: 810 RVA: 0x0000984C File Offset: 0x00007A4C
		public void WriteStartBatch()
		{
			this.VerifyCanWriteStartBatch(true);
			this.WriteStartBatchImplementation();
		}

		// Token: 0x0600032B RID: 811 RVA: 0x0000985B File Offset: 0x00007A5B
		public Task WriteStartBatchAsync()
		{
			this.VerifyCanWriteStartBatch(false);
			return TaskUtils.GetTaskForSynchronousOperation(new Action(this.WriteStartBatchImplementation));
		}

		// Token: 0x0600032C RID: 812 RVA: 0x00009876 File Offset: 0x00007A76
		public void WriteEndBatch()
		{
			this.VerifyCanWriteEndBatch(true);
			this.WriteEndBatchImplementation();
			this.Flush();
		}

		// Token: 0x0600032D RID: 813 RVA: 0x0000988B File Offset: 0x00007A8B
		public Task WriteEndBatchAsync()
		{
			this.VerifyCanWriteEndBatch(false);
			return TaskUtils.GetTaskForSynchronousOperation(new Action(this.WriteEndBatchImplementation)).FollowOnSuccessWithTask((Task task) => this.FlushAsync());
		}

		// Token: 0x0600032E RID: 814 RVA: 0x000098B8 File Offset: 0x00007AB8
		public void WriteStartChangeset()
		{
			this.WriteStartChangeset(Guid.NewGuid().ToString());
		}

		// Token: 0x0600032F RID: 815 RVA: 0x000098DE File Offset: 0x00007ADE
		public void WriteStartChangeset(string changesetId)
		{
			ExceptionUtils.CheckArgumentNotNull<string>(changesetId, "changesetId");
			this.VerifyCanWriteStartChangeset(true);
			this.WriteStartChangesetImplementation(changesetId);
			this.FinishWriteStartChangeset();
		}

		// Token: 0x06000330 RID: 816 RVA: 0x00009900 File Offset: 0x00007B00
		public Task WriteStartChangesetAsync()
		{
			return this.WriteStartChangesetAsync(Guid.NewGuid().ToString());
		}

		// Token: 0x06000331 RID: 817 RVA: 0x00009928 File Offset: 0x00007B28
		public Task WriteStartChangesetAsync(string changesetId)
		{
			ExceptionUtils.CheckArgumentNotNull<string>(changesetId, "changesetId");
			this.VerifyCanWriteStartChangeset(false);
			return TaskUtils.GetTaskForSynchronousOperation(delegate
			{
				this.WriteStartChangesetImplementation(changesetId);
			}).FollowOnSuccessWith(delegate(Task t)
			{
				this.FinishWriteStartChangeset();
			});
		}

		// Token: 0x06000332 RID: 818 RVA: 0x00009983 File Offset: 0x00007B83
		public void WriteEndChangeset()
		{
			this.VerifyCanWriteEndChangeset(true);
			this.WriteEndChangesetImplementation();
			this.FinishWriteEndChangeset();
		}

		// Token: 0x06000333 RID: 819 RVA: 0x00009998 File Offset: 0x00007B98
		public Task WriteEndChangesetAsync()
		{
			this.VerifyCanWriteEndChangeset(false);
			return TaskUtils.GetTaskForSynchronousOperation(new Action(this.WriteEndChangesetImplementation)).FollowOnSuccessWith(delegate(Task t)
			{
				this.FinishWriteEndChangeset();
			});
		}

		// Token: 0x06000334 RID: 820 RVA: 0x000099C4 File Offset: 0x00007BC4
		public ODataBatchOperationRequestMessage CreateOperationRequestMessage(string method, Uri uri, string contentId)
		{
			return this.CreateOperationRequestMessage(method, uri, contentId, BatchPayloadUriOption.AbsoluteUri);
		}

		// Token: 0x06000335 RID: 821 RVA: 0x000099D0 File Offset: 0x00007BD0
		public ODataBatchOperationRequestMessage CreateOperationRequestMessage(string method, Uri uri, string contentId, BatchPayloadUriOption payloadUriOption)
		{
			return this.CreateOperationRequestMessage(method, uri, contentId, payloadUriOption, null);
		}

		// Token: 0x06000336 RID: 822 RVA: 0x000099DE File Offset: 0x00007BDE
		public ODataBatchOperationRequestMessage CreateOperationRequestMessage(string method, Uri uri, string contentId, BatchPayloadUriOption payloadUriOption, IEnumerable<string> dependsOnIds)
		{
			this.VerifyCanCreateOperationRequestMessage(true, method, uri, contentId);
			return this.CreateOperationRequestMessageInternal(method, uri, contentId, payloadUriOption, dependsOnIds);
		}

		// Token: 0x06000337 RID: 823 RVA: 0x000099F7 File Offset: 0x00007BF7
		public Task<ODataBatchOperationRequestMessage> CreateOperationRequestMessageAsync(string method, Uri uri, string contentId)
		{
			return this.CreateOperationRequestMessageAsync(method, uri, contentId, BatchPayloadUriOption.AbsoluteUri);
		}

		// Token: 0x06000338 RID: 824 RVA: 0x00009A03 File Offset: 0x00007C03
		public Task<ODataBatchOperationRequestMessage> CreateOperationRequestMessageAsync(string method, Uri uri, string contentId, BatchPayloadUriOption payloadUriOption)
		{
			return this.CreateOperationRequestMessageAsync(method, uri, contentId, payloadUriOption, null);
		}

		// Token: 0x06000339 RID: 825 RVA: 0x00009A14 File Offset: 0x00007C14
		public Task<ODataBatchOperationRequestMessage> CreateOperationRequestMessageAsync(string method, Uri uri, string contentId, BatchPayloadUriOption payloadUriOption, IList<string> dependsOnIds)
		{
			this.VerifyCanCreateOperationRequestMessage(false, method, uri, contentId);
			return TaskUtils.GetTaskForSynchronousOperation<ODataBatchOperationRequestMessage>(() => this.CreateOperationRequestMessageInternal(method, uri, contentId, payloadUriOption, dependsOnIds));
		}

		// Token: 0x0600033A RID: 826 RVA: 0x00009A7D File Offset: 0x00007C7D
		public ODataBatchOperationResponseMessage CreateOperationResponseMessage(string contentId)
		{
			this.VerifyCanCreateOperationResponseMessage(true);
			return this.CreateOperationResponseMessageImplementation(contentId);
		}

		// Token: 0x0600033B RID: 827 RVA: 0x00009A90 File Offset: 0x00007C90
		public Task<ODataBatchOperationResponseMessage> CreateOperationResponseMessageAsync(string contentId)
		{
			this.VerifyCanCreateOperationResponseMessage(false);
			return TaskUtils.GetTaskForSynchronousOperation<ODataBatchOperationResponseMessage>(() => this.CreateOperationResponseMessageImplementation(contentId));
		}

		// Token: 0x0600033C RID: 828 RVA: 0x00009ACC File Offset: 0x00007CCC
		public void Flush()
		{
			this.VerifyCanFlush(true);
			try
			{
				this.FlushSynchronously();
			}
			catch
			{
				this.SetState(ODataBatchWriter.BatchWriterState.Error);
				throw;
			}
		}

		// Token: 0x0600033D RID: 829 RVA: 0x00009B04 File Offset: 0x00007D04
		public Task FlushAsync()
		{
			this.VerifyCanFlush(false);
			return this.FlushAsynchronously().FollowOnFaultWith(delegate(Task t)
			{
				this.SetState(ODataBatchWriter.BatchWriterState.Error);
			});
		}

		// Token: 0x0600033E RID: 830
		public abstract void StreamRequested();

		// Token: 0x0600033F RID: 831
		public abstract Task StreamRequestedAsync();

		// Token: 0x06000340 RID: 832
		public abstract void StreamDisposed();

		// Token: 0x06000341 RID: 833
		public abstract void OnInStreamError();

		// Token: 0x06000342 RID: 834
		protected abstract void FlushSynchronously();

		// Token: 0x06000343 RID: 835
		protected abstract Task FlushAsynchronously();

		// Token: 0x06000344 RID: 836
		protected abstract void WriteEndBatchImplementation();

		// Token: 0x06000345 RID: 837
		protected abstract void WriteStartChangesetImplementation(string groupOrChangesetId);

		// Token: 0x06000346 RID: 838
		protected abstract void WriteEndChangesetImplementation();

		// Token: 0x06000347 RID: 839
		protected abstract ODataBatchOperationResponseMessage CreateOperationResponseMessageImplementation(string contentId);

		// Token: 0x06000348 RID: 840
		protected abstract ODataBatchOperationRequestMessage CreateOperationRequestMessageImplementation(string method, Uri uri, string contentId, BatchPayloadUriOption payloadUriOption, IEnumerable<string> dependsOnIds);

		// Token: 0x06000349 RID: 841 RVA: 0x00009B24 File Offset: 0x00007D24
		protected void SetState(ODataBatchWriter.BatchWriterState newState)
		{
			this.InterceptException(delegate
			{
				this.ValidateTransition(newState);
			});
			this.state = newState;
		}

		// Token: 0x0600034A RID: 842
		protected abstract void VerifyNotDisposed();

		// Token: 0x0600034B RID: 843
		protected abstract void WriteStartBatchImplementation();

		// Token: 0x0600034C RID: 844
		protected abstract IEnumerable<string> GetDependsOnRequestIds(IEnumerable<string> dependsOnIds);

		// Token: 0x0600034D RID: 845 RVA: 0x00009B64 File Offset: 0x00007D64
		protected ODataBatchOperationRequestMessage BuildOperationRequestMessage(Stream outputStream, string method, Uri uri, string contentId, string groupId, IEnumerable<string> dependsOnIds)
		{
			IEnumerable<string> dependsOnRequestIds = this.GetDependsOnRequestIds(dependsOnIds);
			if (dependsOnIds != null)
			{
				foreach (string text in dependsOnRequestIds)
				{
					if (!this.payloadUriConverter.ContainsContentId(text))
					{
						throw new ODataException(Strings.ODataBatchReader_DependsOnIdNotFound(text, contentId));
					}
				}
			}
			IEnumerable<string> enumerable = ((dependsOnIds == null) ? this.payloadUriConverter.ContentIdCache : dependsOnRequestIds);
			ODataBatchUtils.ValidateReferenceUri(uri, enumerable, this.outputContext.MessageWriterSettings.BaseUri);
			Func<Stream> func = () => ODataBatchUtils.CreateBatchOperationWriteStream(outputStream, this);
			return new ODataBatchOperationRequestMessage(func, method, uri, null, this, contentId, this.payloadUriConverter, true, this.container, dependsOnIds, groupId);
		}

		// Token: 0x0600034E RID: 846 RVA: 0x00009C44 File Offset: 0x00007E44
		protected ODataBatchOperationResponseMessage BuildOperationResponseMessage(Stream outputStream, string contentId, string groupId)
		{
			Func<Stream> func = () => ODataBatchUtils.CreateBatchOperationWriteStream(outputStream, this);
			return new ODataBatchOperationResponseMessage(func, null, this, contentId, this.payloadUriConverter.BatchMessagePayloadUriConverter, true, this.container, groupId);
		}

		// Token: 0x0600034F RID: 847 RVA: 0x00009C90 File Offset: 0x00007E90
		private void InterceptException(Action action)
		{
			try
			{
				action();
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

		// Token: 0x06000350 RID: 848 RVA: 0x00009CCC File Offset: 0x00007ECC
		private void ThrowODataException(string errorMessage)
		{
			this.SetState(ODataBatchWriter.BatchWriterState.Error);
			throw new ODataException(errorMessage);
		}

		// Token: 0x06000351 RID: 849 RVA: 0x00009CDC File Offset: 0x00007EDC
		private ODataBatchOperationRequestMessage CreateOperationRequestMessageInternal(string method, Uri uri, string contentId, BatchPayloadUriOption payloadUriOption, IEnumerable<string> dependsOnIds)
		{
			if (!this.isInChangset)
			{
				this.InterceptException(new Action(this.IncreaseBatchSize));
			}
			else
			{
				this.InterceptException(new Action(this.IncreaseChangeSetSize));
			}
			if (this.currentOperationContentId != null)
			{
				this.payloadUriConverter.AddContentId(this.currentOperationContentId);
			}
			this.InterceptException(delegate
			{
				uri = ODataBatchUtils.CreateOperationRequestUri(uri, this.outputContext.MessageWriterSettings.BaseUri, this.payloadUriConverter);
			});
			this.CurrentOperationRequestMessage = this.CreateOperationRequestMessageImplementation(method, uri, contentId, payloadUriOption, dependsOnIds);
			if (this.isInChangset || this.outputContext.MessageWriterSettings.Version > ODataVersion.V4)
			{
				this.RememberContentIdHeader(this.CurrentOperationRequestMessage.ContentId);
			}
			return this.CurrentOperationRequestMessage;
		}

		// Token: 0x06000352 RID: 850 RVA: 0x00009DB8 File Offset: 0x00007FB8
		private void FinishWriteStartChangeset()
		{
			if (this.outputContext.MessageWriterSettings.Version <= ODataVersion.V4)
			{
				this.payloadUriConverter.Reset();
			}
			this.ResetChangeSetSize();
			this.InterceptException(new Action(this.IncreaseBatchSize));
			this.isInChangset = true;
		}

		// Token: 0x06000353 RID: 851 RVA: 0x00009E18 File Offset: 0x00008018
		private void FinishWriteEndChangeset()
		{
			if (this.outputContext.MessageWriterSettings.Version <= ODataVersion.V4)
			{
				this.currentOperationContentId = null;
			}
			this.isInChangset = false;
		}

		// Token: 0x06000354 RID: 852 RVA: 0x00009E5C File Offset: 0x0000805C
		private void RememberContentIdHeader(string contentId)
		{
			if (contentId != null && this.payloadUriConverter.ContainsContentId(contentId))
			{
				throw new ODataException(Strings.ODataBatchWriter_DuplicateContentIDsNotAllowed(contentId));
			}
			this.currentOperationContentId = contentId;
		}

		// Token: 0x06000355 RID: 853 RVA: 0x00009E84 File Offset: 0x00008084
		private void IncreaseBatchSize()
		{
			this.currentBatchSize += 1U;
			if ((ulong)this.currentBatchSize > (ulong)((long)this.outputContext.MessageWriterSettings.MessageQuotas.MaxPartsPerBatch))
			{
				throw new ODataException(Strings.ODataBatchWriter_MaxBatchSizeExceeded(this.outputContext.MessageWriterSettings.MessageQuotas.MaxPartsPerBatch));
			}
		}

		// Token: 0x06000356 RID: 854 RVA: 0x00009EE4 File Offset: 0x000080E4
		private void IncreaseChangeSetSize()
		{
			this.currentChangeSetSize += 1U;
			if ((ulong)this.currentChangeSetSize > (ulong)((long)this.outputContext.MessageWriterSettings.MessageQuotas.MaxOperationsPerChangeset))
			{
				throw new ODataException(Strings.ODataBatchWriter_MaxChangeSetSizeExceeded(this.outputContext.MessageWriterSettings.MessageQuotas.MaxOperationsPerChangeset));
			}
		}

		// Token: 0x06000357 RID: 855 RVA: 0x00009F43 File Offset: 0x00008143
		private void ResetChangeSetSize()
		{
			this.currentChangeSetSize = 0U;
		}

		// Token: 0x06000358 RID: 856 RVA: 0x00009F4C File Offset: 0x0000814C
		private void VerifyCallAllowed(bool synchronousCall)
		{
			if (synchronousCall)
			{
				if (!this.outputContext.Synchronous)
				{
					throw new ODataException(Strings.ODataBatchWriter_SyncCallOnAsyncWriter);
				}
			}
			else if (this.outputContext.Synchronous)
			{
				throw new ODataException(Strings.ODataBatchWriter_AsyncCallOnSyncWriter);
			}
		}

		// Token: 0x06000359 RID: 857 RVA: 0x00009F84 File Offset: 0x00008184
		private void VerifyCanCreateOperationRequestMessage(bool synchronousCall, string method, Uri uri, string contentId)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(method, "method");
			ExceptionUtils.CheckArgumentNotNull<Uri>(uri, "uri");
			if (this.isInChangset)
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
			this.ValidateWriterReady();
			this.VerifyCallAllowed(synchronousCall);
			if (this.outputContext.WritingResponse)
			{
				this.ThrowODataException(Strings.ODataBatchWriter_CannotCreateRequestOperationWhenWritingResponse);
			}
		}

		// Token: 0x0600035A RID: 858 RVA: 0x0000A002 File Offset: 0x00008202
		private void VerifyCanFlush(bool synchronousCall)
		{
			this.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
			if (this.state == ODataBatchWriter.BatchWriterState.OperationStreamRequested)
			{
				this.ThrowODataException(Strings.ODataBatchWriter_FlushOrFlushAsyncCalledInStreamRequestedState);
			}
		}

		// Token: 0x0600035B RID: 859 RVA: 0x0000A025 File Offset: 0x00008225
		private void ValidateWriterReady()
		{
			this.VerifyNotDisposed();
			if (this.state == ODataBatchWriter.BatchWriterState.OperationStreamRequested)
			{
				throw new ODataException(Strings.ODataBatchWriter_InvalidTransitionFromOperationContentStreamRequested);
			}
		}

		// Token: 0x0600035C RID: 860 RVA: 0x0000A041 File Offset: 0x00008241
		private static bool IsErrorState(ODataBatchWriter.BatchWriterState state)
		{
			return state == ODataBatchWriter.BatchWriterState.Error;
		}

		// Token: 0x0600035D RID: 861 RVA: 0x0000A047 File Offset: 0x00008247
		private void VerifyCanWriteStartBatch(bool synchronousCall)
		{
			this.ValidateWriterReady();
			this.VerifyCallAllowed(synchronousCall);
		}

		// Token: 0x0600035E RID: 862 RVA: 0x0000A047 File Offset: 0x00008247
		private void VerifyCanWriteEndBatch(bool synchronousCall)
		{
			this.ValidateWriterReady();
			this.VerifyCallAllowed(synchronousCall);
		}

		// Token: 0x0600035F RID: 863 RVA: 0x0000A047 File Offset: 0x00008247
		private void VerifyCanWriteStartChangeset(bool synchronousCall)
		{
			this.ValidateWriterReady();
			this.VerifyCallAllowed(synchronousCall);
		}

		// Token: 0x06000360 RID: 864 RVA: 0x0000A047 File Offset: 0x00008247
		private void VerifyCanWriteEndChangeset(bool synchronousCall)
		{
			this.ValidateWriterReady();
			this.VerifyCallAllowed(synchronousCall);
		}

		// Token: 0x06000361 RID: 865 RVA: 0x0000A056 File Offset: 0x00008256
		private void VerifyCanCreateOperationResponseMessage(bool synchronousCall)
		{
			this.ValidateWriterReady();
			this.VerifyCallAllowed(synchronousCall);
			if (!this.outputContext.WritingResponse)
			{
				this.ThrowODataException(Strings.ODataBatchWriter_CannotCreateResponseOperationWhenWritingRequest);
			}
		}

		// Token: 0x06000362 RID: 866 RVA: 0x0000A080 File Offset: 0x00008280
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Validating the transition in the state machine should stay in a single method.")]
		private void ValidateTransition(ODataBatchWriter.BatchWriterState newState)
		{
			if (!ODataBatchWriter.IsErrorState(this.state) && ODataBatchWriter.IsErrorState(newState))
			{
				return;
			}
			if (newState != ODataBatchWriter.BatchWriterState.ChangesetStarted)
			{
				if (newState != ODataBatchWriter.BatchWriterState.ChangesetCompleted)
				{
					if (newState == ODataBatchWriter.BatchWriterState.BatchCompleted)
					{
						if (this.isInChangset)
						{
							throw new ODataException(Strings.ODataBatchWriter_CannotCompleteBatchWithActiveChangeSet);
						}
					}
				}
				else if (!this.isInChangset)
				{
					throw new ODataException(Strings.ODataBatchWriter_CannotCompleteChangeSetWithoutActiveChangeSet);
				}
			}
			else if (this.isInChangset)
			{
				throw new ODataException(Strings.ODataBatchWriter_CannotStartChangeSetWithActiveChangeSet);
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
				if (newState != ODataBatchWriter.BatchWriterState.ChangesetStarted && newState != ODataBatchWriter.BatchWriterState.OperationCreated && newState != ODataBatchWriter.BatchWriterState.BatchCompleted)
				{
					throw new ODataException(Strings.ODataBatchWriter_InvalidTransitionFromBatchStarted);
				}
				break;
			case ODataBatchWriter.BatchWriterState.ChangesetStarted:
				if (newState != ODataBatchWriter.BatchWriterState.OperationCreated && newState != ODataBatchWriter.BatchWriterState.ChangesetCompleted)
				{
					throw new ODataException(Strings.ODataBatchWriter_InvalidTransitionFromChangeSetStarted);
				}
				break;
			case ODataBatchWriter.BatchWriterState.OperationCreated:
				if (newState != ODataBatchWriter.BatchWriterState.OperationCreated && newState != ODataBatchWriter.BatchWriterState.OperationStreamRequested && newState != ODataBatchWriter.BatchWriterState.ChangesetStarted && newState != ODataBatchWriter.BatchWriterState.ChangesetCompleted && newState != ODataBatchWriter.BatchWriterState.BatchCompleted)
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
				if (newState != ODataBatchWriter.BatchWriterState.OperationCreated && newState != ODataBatchWriter.BatchWriterState.ChangesetStarted && newState != ODataBatchWriter.BatchWriterState.ChangesetCompleted && newState != ODataBatchWriter.BatchWriterState.BatchCompleted)
				{
					throw new ODataException(Strings.ODataBatchWriter_InvalidTransitionFromOperationContentStreamDisposed);
				}
				break;
			case ODataBatchWriter.BatchWriterState.ChangesetCompleted:
				if (newState != ODataBatchWriter.BatchWriterState.BatchCompleted && newState != ODataBatchWriter.BatchWriterState.ChangesetStarted && newState != ODataBatchWriter.BatchWriterState.OperationCreated)
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

		// Token: 0x0400016A RID: 362
		private readonly ODataOutputContext outputContext;

		// Token: 0x0400016B RID: 363
		private readonly ODataBatchPayloadUriConverter payloadUriConverter;

		// Token: 0x0400016C RID: 364
		private readonly IServiceProvider container;

		// Token: 0x0400016D RID: 365
		private ODataBatchWriter.BatchWriterState state;

		// Token: 0x0400016E RID: 366
		private ODataBatchOperationRequestMessage currentOperationRequestMessage;

		// Token: 0x0400016F RID: 367
		private ODataBatchOperationResponseMessage currentOperationResponseMessage;

		// Token: 0x04000170 RID: 368
		private string currentOperationContentId;

		// Token: 0x04000171 RID: 369
		private uint currentBatchSize;

		// Token: 0x04000172 RID: 370
		private uint currentChangeSetSize;

		// Token: 0x04000173 RID: 371
		private bool isInChangset;

		// Token: 0x0200029C RID: 668
		protected enum BatchWriterState
		{
			// Token: 0x04000C38 RID: 3128
			Start,
			// Token: 0x04000C39 RID: 3129
			BatchStarted,
			// Token: 0x04000C3A RID: 3130
			ChangesetStarted,
			// Token: 0x04000C3B RID: 3131
			OperationCreated,
			// Token: 0x04000C3C RID: 3132
			OperationStreamRequested,
			// Token: 0x04000C3D RID: 3133
			OperationStreamDisposed,
			// Token: 0x04000C3E RID: 3134
			ChangesetCompleted,
			// Token: 0x04000C3F RID: 3135
			BatchCompleted,
			// Token: 0x04000C40 RID: 3136
			Error
		}
	}
}
