using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading.Tasks;

namespace Microsoft.OData
{
	// Token: 0x02000059 RID: 89
	public abstract class ODataBatchReader : IODataStreamListener
	{
		// Token: 0x060002DC RID: 732 RVA: 0x000088ED File Offset: 0x00006AED
		protected ODataBatchReader(ODataInputContext inputContext, bool synchronous)
		{
			this.inputContext = inputContext;
			this.container = inputContext.Container;
			this.synchronous = synchronous;
			this.PayloadUriConverter = new ODataBatchPayloadUriConverter(inputContext.PayloadUriConverter);
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060002DD RID: 733 RVA: 0x00008920 File Offset: 0x00006B20
		// (set) Token: 0x060002DE RID: 734 RVA: 0x00008933 File Offset: 0x00006B33
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

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060002DF RID: 735 RVA: 0x0000893C File Offset: 0x00006B3C
		public string CurrentGroupId
		{
			get
			{
				return this.GetCurrentGroupIdImplementation();
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060002E0 RID: 736 RVA: 0x00008944 File Offset: 0x00006B44
		protected ODataInputContext InputContext
		{
			get
			{
				return this.inputContext;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060002E1 RID: 737 RVA: 0x0000894C File Offset: 0x00006B4C
		// (set) Token: 0x060002E2 RID: 738 RVA: 0x00008954 File Offset: 0x00006B54
		private ODataBatchReader.OperationState ReaderOperationState
		{
			get
			{
				return this.operationState;
			}
			set
			{
				this.operationState = value;
			}
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x0000895D File Offset: 0x00006B5D
		public bool Read()
		{
			this.VerifyCanRead(true);
			return this.InterceptException<bool>(new Func<bool>(this.ReadSynchronously));
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x00008978 File Offset: 0x00006B78
		public Task<bool> ReadAsync()
		{
			this.VerifyCanRead(false);
			return this.ReadAsynchronously().FollowOnFaultWith(delegate(Task<bool> t)
			{
				this.State = ODataBatchReaderState.Exception;
			});
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x00008998 File Offset: 0x00006B98
		public ODataBatchOperationRequestMessage CreateOperationRequestMessage()
		{
			this.VerifyCanCreateOperationRequestMessage(true);
			ODataBatchOperationRequestMessage odataBatchOperationRequestMessage = this.InterceptException<ODataBatchOperationRequestMessage>(new Func<ODataBatchOperationRequestMessage>(this.CreateOperationRequestMessageImplementation));
			this.ReaderOperationState = ODataBatchReader.OperationState.MessageCreated;
			this.contentIdToAddOnNextRead = odataBatchOperationRequestMessage.ContentId;
			return odataBatchOperationRequestMessage;
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x000089D4 File Offset: 0x00006BD4
		public Task<ODataBatchOperationRequestMessage> CreateOperationRequestMessageAsync()
		{
			this.VerifyCanCreateOperationRequestMessage(false);
			return TaskUtils.GetTaskForSynchronousOperation<ODataBatchOperationRequestMessage>(new Func<ODataBatchOperationRequestMessage>(this.CreateOperationRequestMessageImplementation)).FollowOnSuccessWithTask(delegate(Task<ODataBatchOperationRequestMessage> t)
			{
				this.ReaderOperationState = ODataBatchReader.OperationState.MessageCreated;
				this.contentIdToAddOnNextRead = t.Result.ContentId;
				return t;
			}).FollowOnFaultWith(delegate(Task<ODataBatchOperationRequestMessage> t)
			{
				this.State = ODataBatchReaderState.Exception;
			});
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x00008A14 File Offset: 0x00006C14
		public ODataBatchOperationResponseMessage CreateOperationResponseMessage()
		{
			this.VerifyCanCreateOperationResponseMessage(true);
			ODataBatchOperationResponseMessage odataBatchOperationResponseMessage = this.InterceptException<ODataBatchOperationResponseMessage>(new Func<ODataBatchOperationResponseMessage>(this.CreateOperationResponseMessageImplementation));
			this.ReaderOperationState = ODataBatchReader.OperationState.MessageCreated;
			return odataBatchOperationResponseMessage;
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x00008A44 File Offset: 0x00006C44
		public Task<ODataBatchOperationResponseMessage> CreateOperationResponseMessageAsync()
		{
			this.VerifyCanCreateOperationResponseMessage(false);
			return TaskUtils.GetTaskForSynchronousOperation<ODataBatchOperationResponseMessage>(new Func<ODataBatchOperationResponseMessage>(this.CreateOperationResponseMessageImplementation)).FollowOnSuccessWithTask(delegate(Task<ODataBatchOperationResponseMessage> t)
			{
				this.ReaderOperationState = ODataBatchReader.OperationState.MessageCreated;
				return t;
			}).FollowOnFaultWith(delegate(Task<ODataBatchOperationResponseMessage> t)
			{
				this.State = ODataBatchReaderState.Exception;
			});
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x00008A81 File Offset: 0x00006C81
		void IODataStreamListener.StreamRequested()
		{
			this.operationState = ODataBatchReader.OperationState.StreamRequested;
		}

		// Token: 0x060002EA RID: 746 RVA: 0x00008A8A File Offset: 0x00006C8A
		Task IODataStreamListener.StreamRequestedAsync()
		{
			this.operationState = ODataBatchReader.OperationState.StreamRequested;
			return TaskUtils.CompletedTask;
		}

		// Token: 0x060002EB RID: 747 RVA: 0x00008A98 File Offset: 0x00006C98
		void IODataStreamListener.StreamDisposed()
		{
			this.operationState = ODataBatchReader.OperationState.StreamDisposed;
		}

		// Token: 0x060002EC RID: 748 RVA: 0x00008AA1 File Offset: 0x00006CA1
		protected void ThrowODataException(string errorMessage)
		{
			this.State = ODataBatchReaderState.Exception;
			throw new ODataException(errorMessage);
		}

		// Token: 0x060002ED RID: 749 RVA: 0x0000360D File Offset: 0x0000180D
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "A method for consistency with the rest of the API.")]
		protected virtual string GetCurrentGroupIdImplementation()
		{
			return null;
		}

		// Token: 0x060002EE RID: 750
		protected abstract ODataBatchOperationRequestMessage CreateOperationRequestMessageImplementation();

		// Token: 0x060002EF RID: 751
		protected abstract ODataBatchOperationResponseMessage CreateOperationResponseMessageImplementation();

		// Token: 0x060002F0 RID: 752
		protected abstract ODataBatchReaderState ReadAtStartImplementation();

		// Token: 0x060002F1 RID: 753
		protected abstract ODataBatchReaderState ReadAtOperationImplementation();

		// Token: 0x060002F2 RID: 754
		protected abstract ODataBatchReaderState ReadAtChangesetStartImplementation();

		// Token: 0x060002F3 RID: 755
		protected abstract ODataBatchReaderState ReadAtChangesetEndImplementation();

		// Token: 0x060002F4 RID: 756 RVA: 0x00008AB0 File Offset: 0x00006CB0
		protected ODataBatchOperationRequestMessage BuildOperationRequestMessage(Func<Stream> streamCreatorFunc, string method, Uri requestUri, ODataBatchOperationHeaders headers, string contentId, string groupId, IEnumerable<string> dependsOnRequestIds, bool dependsOnIdsValidationRequired)
		{
			if (dependsOnRequestIds != null && dependsOnIdsValidationRequired)
			{
				foreach (string text in dependsOnRequestIds)
				{
					if (!this.PayloadUriConverter.ContainsContentId(text))
					{
						throw new ODataException(Strings.ODataBatchReader_DependsOnIdNotFound(text, contentId));
					}
				}
			}
			Uri uri = ODataBatchUtils.CreateOperationRequestUri(requestUri, this.inputContext.MessageReaderSettings.BaseUri, this.PayloadUriConverter);
			ODataBatchUtils.ValidateReferenceUri(requestUri, dependsOnRequestIds, this.inputContext.MessageReaderSettings.BaseUri);
			return new ODataBatchOperationRequestMessage(streamCreatorFunc, method, uri, headers, this, contentId, this.PayloadUriConverter, false, this.container, dependsOnRequestIds, groupId);
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x00008B6C File Offset: 0x00006D6C
		protected ODataBatchOperationResponseMessage BuildOperationResponseMessage(Func<Stream> streamCreatorFunc, int statusCode, ODataBatchOperationHeaders headers, string contentId, string groupId)
		{
			return new ODataBatchOperationResponseMessage(streamCreatorFunc, headers, this, contentId, this.PayloadUriConverter.BatchMessagePayloadUriConverter, false, this.container, groupId)
			{
				StatusCode = statusCode
			};
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x00008BA0 File Offset: 0x00006DA0
		private void IncreaseBatchSize()
		{
			if ((ulong)this.currentBatchSize == (ulong)((long)this.inputContext.MessageReaderSettings.MessageQuotas.MaxPartsPerBatch))
			{
				throw new ODataException(Strings.ODataBatchReader_MaxBatchSizeExceeded(this.inputContext.MessageReaderSettings.MessageQuotas.MaxPartsPerBatch));
			}
			this.currentBatchSize += 1U;
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x00008C00 File Offset: 0x00006E00
		private void IncreaseChangesetSize()
		{
			if ((ulong)this.currentChangeSetSize == (ulong)((long)this.inputContext.MessageReaderSettings.MessageQuotas.MaxOperationsPerChangeset))
			{
				throw new ODataException(Strings.ODataBatchReader_MaxChangeSetSizeExceeded(this.inputContext.MessageReaderSettings.MessageQuotas.MaxOperationsPerChangeset));
			}
			this.currentChangeSetSize += 1U;
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x00008C5F File Offset: 0x00006E5F
		private void ResetChangesetSize()
		{
			this.currentChangeSetSize = 0U;
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x00008C68 File Offset: 0x00006E68
		private bool ReadSynchronously()
		{
			return this.ReadImplementation();
		}

		// Token: 0x060002FA RID: 762 RVA: 0x00008C70 File Offset: 0x00006E70
		[SuppressMessage("Microsoft.MSInternal", "CA908:AvoidTypesThatRequireJitCompilationInPrecompiledAssemblies", Justification = "API design calls for a bool being returned from the task here.")]
		private Task<bool> ReadAsynchronously()
		{
			return TaskUtils.GetTaskForSynchronousOperation<bool>(new Func<bool>(this.ReadImplementation));
		}

		// Token: 0x060002FB RID: 763 RVA: 0x00008C84 File Offset: 0x00006E84
		private bool ReadImplementation()
		{
			switch (this.State)
			{
			case ODataBatchReaderState.Initial:
				this.State = this.ReadAtStartImplementation();
				break;
			case ODataBatchReaderState.Operation:
				if (this.ReaderOperationState == ODataBatchReader.OperationState.None)
				{
					throw new ODataException(Strings.ODataBatchReader_NoMessageWasCreatedForOperation);
				}
				this.ReaderOperationState = ODataBatchReader.OperationState.None;
				if (this.contentIdToAddOnNextRead != null)
				{
					if (this.PayloadUriConverter.ContainsContentId(this.contentIdToAddOnNextRead))
					{
						throw new ODataException(Strings.ODataBatchReader_DuplicateContentIDsNotAllowed(this.contentIdToAddOnNextRead));
					}
					this.PayloadUriConverter.AddContentId(this.contentIdToAddOnNextRead);
					this.contentIdToAddOnNextRead = null;
				}
				if (this.isInChangeset)
				{
					this.IncreaseChangesetSize();
				}
				else
				{
					this.IncreaseBatchSize();
				}
				this.State = this.ReadAtOperationImplementation();
				break;
			case ODataBatchReaderState.ChangesetStart:
				if (this.isInChangeset)
				{
					this.ThrowODataException(Strings.ODataBatchReaderStream_NestedChangesetsAreNotSupported);
				}
				this.IncreaseBatchSize();
				this.State = this.ReadAtChangesetStartImplementation();
				if (this.inputContext.MessageReaderSettings.Version <= ODataVersion.V4)
				{
					this.PayloadUriConverter.Reset();
				}
				this.isInChangeset = true;
				break;
			case ODataBatchReaderState.ChangesetEnd:
				this.ResetChangesetSize();
				this.isInChangeset = false;
				this.State = this.ReadAtChangesetEndImplementation();
				break;
			case ODataBatchReaderState.Completed:
			case ODataBatchReaderState.Exception:
				throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataBatchReader_ReadImplementation));
			default:
				throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataBatchReader_ReadImplementation));
			}
			return this.State != ODataBatchReaderState.Completed && this.State != ODataBatchReaderState.Exception;
		}

		// Token: 0x060002FC RID: 764 RVA: 0x00008E0C File Offset: 0x0000700C
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

		// Token: 0x060002FD RID: 765 RVA: 0x00008E70 File Offset: 0x00007070
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

		// Token: 0x060002FE RID: 766 RVA: 0x00008ED4 File Offset: 0x000070D4
		private void VerifyCanRead(bool synchronousCall)
		{
			this.VerifyReaderReady();
			this.VerifyCallAllowed(synchronousCall);
			if (this.State == ODataBatchReaderState.Exception || this.State == ODataBatchReaderState.Completed)
			{
				throw new ODataException(Strings.ODataBatchReader_ReadOrReadAsyncCalledInInvalidState(this.State));
			}
		}

		// Token: 0x060002FF RID: 767 RVA: 0x00008F0B File Offset: 0x0000710B
		private void VerifyReaderReady()
		{
			this.inputContext.VerifyNotDisposed();
			if (this.operationState == ODataBatchReader.OperationState.StreamRequested)
			{
				throw new ODataException(Strings.ODataBatchReader_CannotUseReaderWhileOperationStreamActive);
			}
		}

		// Token: 0x06000300 RID: 768 RVA: 0x00008F2C File Offset: 0x0000712C
		private void VerifyCallAllowed(bool synchronousCall)
		{
			if (synchronousCall)
			{
				if (!this.synchronous)
				{
					throw new ODataException(Strings.ODataBatchReader_SyncCallOnAsyncReader);
				}
			}
			else if (this.synchronous)
			{
				throw new ODataException(Strings.ODataBatchReader_AsyncCallOnSyncReader);
			}
		}

		// Token: 0x06000301 RID: 769 RVA: 0x00008F58 File Offset: 0x00007158
		private T InterceptException<T>(Func<T> action)
		{
			T t;
			try
			{
				t = action();
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

		// Token: 0x0400014B RID: 331
		internal readonly ODataBatchPayloadUriConverter PayloadUriConverter;

		// Token: 0x0400014C RID: 332
		private readonly ODataInputContext inputContext;

		// Token: 0x0400014D RID: 333
		private readonly bool synchronous;

		// Token: 0x0400014E RID: 334
		private readonly IServiceProvider container;

		// Token: 0x0400014F RID: 335
		private ODataBatchReaderState batchReaderState;

		// Token: 0x04000150 RID: 336
		private uint currentBatchSize;

		// Token: 0x04000151 RID: 337
		private uint currentChangeSetSize;

		// Token: 0x04000152 RID: 338
		private ODataBatchReader.OperationState operationState;

		// Token: 0x04000153 RID: 339
		private bool isInChangeset;

		// Token: 0x04000154 RID: 340
		private string contentIdToAddOnNextRead;

		// Token: 0x0200029B RID: 667
		private enum OperationState
		{
			// Token: 0x04000C33 RID: 3123
			None,
			// Token: 0x04000C34 RID: 3124
			MessageCreated,
			// Token: 0x04000C35 RID: 3125
			StreamRequested,
			// Token: 0x04000C36 RID: 3126
			StreamDisposed
		}
	}
}
