using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.OData.Edm;

namespace Microsoft.OData
{
	// Token: 0x02000068 RID: 104
	internal abstract class ODataCollectionWriterCore : ODataCollectionWriter, IODataOutputInStreamErrorListener
	{
		// Token: 0x060003A2 RID: 930 RVA: 0x0000A625 File Offset: 0x00008825
		protected ODataCollectionWriterCore(ODataOutputContext outputContext, IEdmTypeReference itemTypeReference)
			: this(outputContext, itemTypeReference, null)
		{
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x0000A630 File Offset: 0x00008830
		protected ODataCollectionWriterCore(ODataOutputContext outputContext, IEdmTypeReference expectedItemType, IODataReaderWriterListener listener)
		{
			this.outputContext = outputContext;
			this.expectedItemType = expectedItemType;
			this.listener = listener;
			this.scopes.Push(new ODataCollectionWriterCore.Scope(ODataCollectionWriterCore.CollectionWriterState.Start, null));
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060003A4 RID: 932 RVA: 0x0000A66A File Offset: 0x0000886A
		protected ODataCollectionWriterCore.CollectionWriterState State
		{
			get
			{
				return this.scopes.Peek().State;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060003A5 RID: 933 RVA: 0x0000A67C File Offset: 0x0000887C
		protected IDuplicatePropertyNameChecker DuplicatePropertyNameChecker
		{
			get
			{
				IDuplicatePropertyNameChecker duplicatePropertyNameChecker;
				if ((duplicatePropertyNameChecker = this.duplicatePropertyNameChecker) == null)
				{
					duplicatePropertyNameChecker = (this.duplicatePropertyNameChecker = this.outputContext.MessageWriterSettings.Validator.CreateDuplicatePropertyNameChecker());
				}
				return duplicatePropertyNameChecker;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060003A6 RID: 934 RVA: 0x0000A6B1 File Offset: 0x000088B1
		protected CollectionWithoutExpectedTypeValidator CollectionValidator
		{
			get
			{
				return this.collectionValidator;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060003A7 RID: 935 RVA: 0x0000A6B9 File Offset: 0x000088B9
		protected IEdmTypeReference ItemTypeReference
		{
			get
			{
				return this.expectedItemType;
			}
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x0000A6C4 File Offset: 0x000088C4
		public sealed override void Flush()
		{
			this.VerifyCanFlush(true);
			try
			{
				this.FlushSynchronously();
			}
			catch
			{
				this.ReplaceScope(ODataCollectionWriterCore.CollectionWriterState.Error, null);
				throw;
			}
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x0000A6FC File Offset: 0x000088FC
		public sealed override Task FlushAsync()
		{
			this.VerifyCanFlush(false);
			return this.FlushAsynchronously().FollowOnFaultWith(delegate(Task t)
			{
				this.ReplaceScope(ODataCollectionWriterCore.CollectionWriterState.Error, null);
			});
		}

		// Token: 0x060003AA RID: 938 RVA: 0x0000A71C File Offset: 0x0000891C
		public sealed override void WriteStart(ODataCollectionStart collectionStart)
		{
			this.VerifyCanWriteStart(true, collectionStart);
			this.WriteStartImplementation(collectionStart);
		}

		// Token: 0x060003AB RID: 939 RVA: 0x0000A730 File Offset: 0x00008930
		public sealed override Task WriteStartAsync(ODataCollectionStart collection)
		{
			this.VerifyCanWriteStart(false, collection);
			return TaskUtils.GetTaskForSynchronousOperation(delegate
			{
				this.WriteStartImplementation(collection);
			});
		}

		// Token: 0x060003AC RID: 940 RVA: 0x0000A76F File Offset: 0x0000896F
		public sealed override void WriteItem(object item)
		{
			this.VerifyCanWriteItem(true);
			this.WriteItemImplementation(item);
		}

		// Token: 0x060003AD RID: 941 RVA: 0x0000A780 File Offset: 0x00008980
		public sealed override Task WriteItemAsync(object item)
		{
			this.VerifyCanWriteItem(false);
			return TaskUtils.GetTaskForSynchronousOperation(delegate
			{
				this.WriteItemImplementation(item);
			});
		}

		// Token: 0x060003AE RID: 942 RVA: 0x0000A7B9 File Offset: 0x000089B9
		public sealed override void WriteEnd()
		{
			this.VerifyCanWriteEnd(true);
			this.WriteEndImplementation();
			if (this.scopes.Peek().State == ODataCollectionWriterCore.CollectionWriterState.Completed)
			{
				this.Flush();
			}
		}

		// Token: 0x060003AF RID: 943 RVA: 0x0000A7E1 File Offset: 0x000089E1
		public sealed override Task WriteEndAsync()
		{
			this.VerifyCanWriteEnd(false);
			return TaskUtils.GetTaskForSynchronousOperation(new Action(this.WriteEndImplementation)).FollowOnSuccessWithTask(delegate(Task task)
			{
				if (this.scopes.Peek().State == ODataCollectionWriterCore.CollectionWriterState.Completed)
				{
					return this.FlushAsync();
				}
				return TaskUtils.CompletedTask;
			});
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x0000A80C File Offset: 0x00008A0C
		void IODataOutputInStreamErrorListener.OnInStreamError()
		{
			this.VerifyNotDisposed();
			if (this.State == ODataCollectionWriterCore.CollectionWriterState.Completed)
			{
				throw new ODataException(Strings.ODataWriterCore_InvalidTransitionFromCompleted(this.State.ToString(), ODataCollectionWriterCore.CollectionWriterState.Error.ToString()));
			}
			this.StartPayloadInStartState();
			this.EnterScope(ODataCollectionWriterCore.CollectionWriterState.Error, this.scopes.Peek().Item);
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x0000A873 File Offset: 0x00008A73
		protected static bool IsErrorState(ODataCollectionWriterCore.CollectionWriterState state)
		{
			return state == ODataCollectionWriterCore.CollectionWriterState.Error;
		}

		// Token: 0x060003B2 RID: 946
		protected abstract void VerifyNotDisposed();

		// Token: 0x060003B3 RID: 947
		protected abstract void FlushSynchronously();

		// Token: 0x060003B4 RID: 948
		protected abstract Task FlushAsynchronously();

		// Token: 0x060003B5 RID: 949
		protected abstract void StartPayload();

		// Token: 0x060003B6 RID: 950
		protected abstract void EndPayload();

		// Token: 0x060003B7 RID: 951
		protected abstract void StartCollection(ODataCollectionStart collectionStart);

		// Token: 0x060003B8 RID: 952
		protected abstract void EndCollection();

		// Token: 0x060003B9 RID: 953
		protected abstract void WriteCollectionItem(object item, IEdmTypeReference expectedItemTypeReference);

		// Token: 0x060003BA RID: 954 RVA: 0x0000A879 File Offset: 0x00008A79
		private void VerifyCanWriteStart(bool synchronousCall, ODataCollectionStart collectionStart)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataCollectionStart>(collectionStart, "collection");
			this.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
		}

		// Token: 0x060003BB RID: 955 RVA: 0x0000A894 File Offset: 0x00008A94
		private void WriteStartImplementation(ODataCollectionStart collectionStart)
		{
			this.StartPayloadInStartState();
			this.EnterScope(ODataCollectionWriterCore.CollectionWriterState.Collection, collectionStart);
			this.InterceptException(delegate
			{
				if (this.expectedItemType == null)
				{
					this.collectionValidator = new CollectionWithoutExpectedTypeValidator(null);
				}
				this.StartCollection(collectionStart);
			});
		}

		// Token: 0x060003BC RID: 956 RVA: 0x0000A8DA File Offset: 0x00008ADA
		private void VerifyCanWriteItem(bool synchronousCall)
		{
			this.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
		}

		// Token: 0x060003BD RID: 957 RVA: 0x0000A8EC File Offset: 0x00008AEC
		private void WriteItemImplementation(object item)
		{
			if (this.scopes.Peek().State != ODataCollectionWriterCore.CollectionWriterState.Item)
			{
				this.EnterScope(ODataCollectionWriterCore.CollectionWriterState.Item, item);
			}
			this.InterceptException(delegate
			{
				ValidationUtils.ValidateCollectionItem(item, true);
				this.WriteCollectionItem(item, this.expectedItemType);
			});
		}

		// Token: 0x060003BE RID: 958 RVA: 0x0000A8DA File Offset: 0x00008ADA
		private void VerifyCanWriteEnd(bool synchronousCall)
		{
			this.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
		}

		// Token: 0x060003BF RID: 959 RVA: 0x0000A93F File Offset: 0x00008B3F
		private void WriteEndImplementation()
		{
			this.InterceptException(delegate
			{
				ODataCollectionWriterCore.Scope scope = this.scopes.Peek();
				switch (scope.State)
				{
				case ODataCollectionWriterCore.CollectionWriterState.Start:
				case ODataCollectionWriterCore.CollectionWriterState.Completed:
				case ODataCollectionWriterCore.CollectionWriterState.Error:
					throw new ODataException(Strings.ODataCollectionWriterCore_WriteEndCalledInInvalidState(scope.State.ToString()));
				case ODataCollectionWriterCore.CollectionWriterState.Collection:
					this.EndCollection();
					break;
				case ODataCollectionWriterCore.CollectionWriterState.Item:
					this.LeaveScope();
					this.EndCollection();
					break;
				default:
					throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataCollectionWriterCore_WriteEnd_UnreachableCodePath));
				}
				this.LeaveScope();
			});
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x0000A8DA File Offset: 0x00008ADA
		private void VerifyCanFlush(bool synchronousCall)
		{
			this.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x0000A953 File Offset: 0x00008B53
		private void VerifyCallAllowed(bool synchronousCall)
		{
			if (synchronousCall)
			{
				if (!this.outputContext.Synchronous)
				{
					throw new ODataException(Strings.ODataCollectionWriterCore_SyncCallOnAsyncWriter);
				}
			}
			else if (this.outputContext.Synchronous)
			{
				throw new ODataException(Strings.ODataCollectionWriterCore_AsyncCallOnSyncWriter);
			}
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x0000A988 File Offset: 0x00008B88
		private void StartPayloadInStartState()
		{
			ODataCollectionWriterCore.Scope scope = this.scopes.Peek();
			if (scope.State == ODataCollectionWriterCore.CollectionWriterState.Start)
			{
				this.InterceptException(new Action(this.StartPayload));
			}
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x0000A9BC File Offset: 0x00008BBC
		private void InterceptException(Action action)
		{
			try
			{
				action();
			}
			catch
			{
				if (!ODataCollectionWriterCore.IsErrorState(this.State))
				{
					this.EnterScope(ODataCollectionWriterCore.CollectionWriterState.Error, this.scopes.Peek().Item);
				}
				throw;
			}
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x0000AA08 File Offset: 0x00008C08
		private void NotifyListener(ODataCollectionWriterCore.CollectionWriterState newState)
		{
			if (this.listener != null)
			{
				if (ODataCollectionWriterCore.IsErrorState(newState))
				{
					this.listener.OnException();
					return;
				}
				if (newState == ODataCollectionWriterCore.CollectionWriterState.Completed)
				{
					this.listener.OnCompleted();
				}
			}
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x0000AA38 File Offset: 0x00008C38
		private void EnterScope(ODataCollectionWriterCore.CollectionWriterState newState, object item)
		{
			this.InterceptException(delegate
			{
				this.ValidateTransition(newState);
			});
			this.scopes.Push(new ODataCollectionWriterCore.Scope(newState, item));
			this.NotifyListener(newState);
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x0000AA90 File Offset: 0x00008C90
		private void LeaveScope()
		{
			this.scopes.Pop();
			if (this.scopes.Count == 1)
			{
				this.scopes.Pop();
				this.scopes.Push(new ODataCollectionWriterCore.Scope(ODataCollectionWriterCore.CollectionWriterState.Completed, null));
				this.InterceptException(new Action(this.EndPayload));
				this.NotifyListener(ODataCollectionWriterCore.CollectionWriterState.Completed);
			}
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x0000AAEF File Offset: 0x00008CEF
		private void ReplaceScope(ODataCollectionWriterCore.CollectionWriterState newState, ODataItem item)
		{
			this.ValidateTransition(newState);
			this.scopes.Pop();
			this.scopes.Push(new ODataCollectionWriterCore.Scope(newState, item));
			this.NotifyListener(newState);
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x0000AB20 File Offset: 0x00008D20
		private void ValidateTransition(ODataCollectionWriterCore.CollectionWriterState newState)
		{
			if (!ODataCollectionWriterCore.IsErrorState(this.State) && ODataCollectionWriterCore.IsErrorState(newState))
			{
				return;
			}
			switch (this.State)
			{
			case ODataCollectionWriterCore.CollectionWriterState.Start:
				if (newState != ODataCollectionWriterCore.CollectionWriterState.Collection && newState != ODataCollectionWriterCore.CollectionWriterState.Completed)
				{
					throw new ODataException(Strings.ODataCollectionWriterCore_InvalidTransitionFromStart(this.State.ToString(), newState.ToString()));
				}
				break;
			case ODataCollectionWriterCore.CollectionWriterState.Collection:
				if (newState != ODataCollectionWriterCore.CollectionWriterState.Item && newState != ODataCollectionWriterCore.CollectionWriterState.Completed)
				{
					throw new ODataException(Strings.ODataCollectionWriterCore_InvalidTransitionFromCollection(this.State.ToString(), newState.ToString()));
				}
				break;
			case ODataCollectionWriterCore.CollectionWriterState.Item:
				if (newState != ODataCollectionWriterCore.CollectionWriterState.Completed)
				{
					throw new ODataException(Strings.ODataCollectionWriterCore_InvalidTransitionFromItem(this.State.ToString(), newState.ToString()));
				}
				break;
			case ODataCollectionWriterCore.CollectionWriterState.Completed:
				throw new ODataException(Strings.ODataWriterCore_InvalidTransitionFromCompleted(this.State.ToString(), newState.ToString()));
			case ODataCollectionWriterCore.CollectionWriterState.Error:
				if (newState != ODataCollectionWriterCore.CollectionWriterState.Error)
				{
					throw new ODataException(Strings.ODataWriterCore_InvalidTransitionFromError(this.State.ToString(), newState.ToString()));
				}
				break;
			default:
				throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataCollectionWriterCore_ValidateTransition_UnreachableCodePath));
			}
		}

		// Token: 0x04000185 RID: 389
		private readonly ODataOutputContext outputContext;

		// Token: 0x04000186 RID: 390
		private readonly IODataReaderWriterListener listener;

		// Token: 0x04000187 RID: 391
		private readonly Stack<ODataCollectionWriterCore.Scope> scopes = new Stack<ODataCollectionWriterCore.Scope>();

		// Token: 0x04000188 RID: 392
		private readonly IEdmTypeReference expectedItemType;

		// Token: 0x04000189 RID: 393
		private IDuplicatePropertyNameChecker duplicatePropertyNameChecker;

		// Token: 0x0400018A RID: 394
		private CollectionWithoutExpectedTypeValidator collectionValidator;

		// Token: 0x020002A5 RID: 677
		internal enum CollectionWriterState
		{
			// Token: 0x04000C57 RID: 3159
			Start,
			// Token: 0x04000C58 RID: 3160
			Collection,
			// Token: 0x04000C59 RID: 3161
			Item,
			// Token: 0x04000C5A RID: 3162
			Completed,
			// Token: 0x04000C5B RID: 3163
			Error
		}

		// Token: 0x020002A6 RID: 678
		private sealed class Scope
		{
			// Token: 0x06001CC0 RID: 7360 RVA: 0x00056FAF File Offset: 0x000551AF
			public Scope(ODataCollectionWriterCore.CollectionWriterState state, object item)
			{
				this.state = state;
				this.item = item;
			}

			// Token: 0x170005DF RID: 1503
			// (get) Token: 0x06001CC1 RID: 7361 RVA: 0x00056FC5 File Offset: 0x000551C5
			public ODataCollectionWriterCore.CollectionWriterState State
			{
				get
				{
					return this.state;
				}
			}

			// Token: 0x170005E0 RID: 1504
			// (get) Token: 0x06001CC2 RID: 7362 RVA: 0x00056FCD File Offset: 0x000551CD
			public object Item
			{
				get
				{
					return this.item;
				}
			}

			// Token: 0x04000C5C RID: 3164
			private readonly ODataCollectionWriterCore.CollectionWriterState state;

			// Token: 0x04000C5D RID: 3165
			private readonly object item;
		}
	}
}
