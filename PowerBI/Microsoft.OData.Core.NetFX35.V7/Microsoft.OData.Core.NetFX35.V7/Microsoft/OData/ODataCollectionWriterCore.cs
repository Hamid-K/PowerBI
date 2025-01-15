using System;
using System.Collections.Generic;
using Microsoft.OData.Edm;

namespace Microsoft.OData
{
	// Token: 0x02000046 RID: 70
	internal abstract class ODataCollectionWriterCore : ODataCollectionWriter, IODataOutputInStreamErrorListener
	{
		// Token: 0x0600022F RID: 559 RVA: 0x000089E9 File Offset: 0x00006BE9
		protected ODataCollectionWriterCore(ODataOutputContext outputContext, IEdmTypeReference itemTypeReference)
			: this(outputContext, itemTypeReference, null)
		{
		}

		// Token: 0x06000230 RID: 560 RVA: 0x000089F4 File Offset: 0x00006BF4
		protected ODataCollectionWriterCore(ODataOutputContext outputContext, IEdmTypeReference expectedItemType, IODataReaderWriterListener listener)
		{
			this.outputContext = outputContext;
			this.expectedItemType = expectedItemType;
			this.listener = listener;
			this.scopes.Push(new ODataCollectionWriterCore.Scope(ODataCollectionWriterCore.CollectionWriterState.Start, null));
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000231 RID: 561 RVA: 0x00008A2E File Offset: 0x00006C2E
		protected ODataCollectionWriterCore.CollectionWriterState State
		{
			get
			{
				return this.scopes.Peek().State;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000232 RID: 562 RVA: 0x00008A40 File Offset: 0x00006C40
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

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000233 RID: 563 RVA: 0x00008A75 File Offset: 0x00006C75
		protected CollectionWithoutExpectedTypeValidator CollectionValidator
		{
			get
			{
				return this.collectionValidator;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000234 RID: 564 RVA: 0x00008A7D File Offset: 0x00006C7D
		protected IEdmTypeReference ItemTypeReference
		{
			get
			{
				return this.expectedItemType;
			}
		}

		// Token: 0x06000235 RID: 565 RVA: 0x00008A88 File Offset: 0x00006C88
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

		// Token: 0x06000236 RID: 566 RVA: 0x00008AC0 File Offset: 0x00006CC0
		public sealed override void WriteStart(ODataCollectionStart collectionStart)
		{
			this.VerifyCanWriteStart(true, collectionStart);
			this.WriteStartImplementation(collectionStart);
		}

		// Token: 0x06000237 RID: 567 RVA: 0x00008AD1 File Offset: 0x00006CD1
		public sealed override void WriteItem(object item)
		{
			this.VerifyCanWriteItem(true);
			this.WriteItemImplementation(item);
		}

		// Token: 0x06000238 RID: 568 RVA: 0x00008AE1 File Offset: 0x00006CE1
		public sealed override void WriteEnd()
		{
			this.VerifyCanWriteEnd(true);
			this.WriteEndImplementation();
			if (this.scopes.Peek().State == ODataCollectionWriterCore.CollectionWriterState.Completed)
			{
				this.Flush();
			}
		}

		// Token: 0x06000239 RID: 569 RVA: 0x00008B0C File Offset: 0x00006D0C
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

		// Token: 0x0600023A RID: 570 RVA: 0x00008B73 File Offset: 0x00006D73
		protected static bool IsErrorState(ODataCollectionWriterCore.CollectionWriterState state)
		{
			return state == ODataCollectionWriterCore.CollectionWriterState.Error;
		}

		// Token: 0x0600023B RID: 571
		protected abstract void VerifyNotDisposed();

		// Token: 0x0600023C RID: 572
		protected abstract void FlushSynchronously();

		// Token: 0x0600023D RID: 573
		protected abstract void StartPayload();

		// Token: 0x0600023E RID: 574
		protected abstract void EndPayload();

		// Token: 0x0600023F RID: 575
		protected abstract void StartCollection(ODataCollectionStart collectionStart);

		// Token: 0x06000240 RID: 576
		protected abstract void EndCollection();

		// Token: 0x06000241 RID: 577
		protected abstract void WriteCollectionItem(object item, IEdmTypeReference expectedItemTypeReference);

		// Token: 0x06000242 RID: 578 RVA: 0x00008B79 File Offset: 0x00006D79
		private void VerifyCanWriteStart(bool synchronousCall, ODataCollectionStart collectionStart)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataCollectionStart>(collectionStart, "collection");
			this.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
		}

		// Token: 0x06000243 RID: 579 RVA: 0x00008B94 File Offset: 0x00006D94
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

		// Token: 0x06000244 RID: 580 RVA: 0x00008BDA File Offset: 0x00006DDA
		private void VerifyCanWriteItem(bool synchronousCall)
		{
			this.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
		}

		// Token: 0x06000245 RID: 581 RVA: 0x00008BEC File Offset: 0x00006DEC
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

		// Token: 0x06000246 RID: 582 RVA: 0x00008BDA File Offset: 0x00006DDA
		private void VerifyCanWriteEnd(bool synchronousCall)
		{
			this.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
		}

		// Token: 0x06000247 RID: 583 RVA: 0x00008C3F File Offset: 0x00006E3F
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

		// Token: 0x06000248 RID: 584 RVA: 0x00008BDA File Offset: 0x00006DDA
		private void VerifyCanFlush(bool synchronousCall)
		{
			this.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
		}

		// Token: 0x06000249 RID: 585 RVA: 0x00008C53 File Offset: 0x00006E53
		private void VerifyCallAllowed(bool synchronousCall)
		{
			if (synchronousCall && !this.outputContext.Synchronous)
			{
				throw new ODataException(Strings.ODataCollectionWriterCore_SyncCallOnAsyncWriter);
			}
		}

		// Token: 0x0600024A RID: 586 RVA: 0x00008C70 File Offset: 0x00006E70
		private void StartPayloadInStartState()
		{
			ODataCollectionWriterCore.Scope scope = this.scopes.Peek();
			if (scope.State == ODataCollectionWriterCore.CollectionWriterState.Start)
			{
				this.InterceptException(new Action(this.StartPayload));
			}
		}

		// Token: 0x0600024B RID: 587 RVA: 0x00008CA4 File Offset: 0x00006EA4
		private void InterceptException(Action action)
		{
			try
			{
				action.Invoke();
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

		// Token: 0x0600024C RID: 588 RVA: 0x00008CF0 File Offset: 0x00006EF0
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

		// Token: 0x0600024D RID: 589 RVA: 0x00008D20 File Offset: 0x00006F20
		private void EnterScope(ODataCollectionWriterCore.CollectionWriterState newState, object item)
		{
			this.InterceptException(delegate
			{
				this.ValidateTransition(newState);
			});
			this.scopes.Push(new ODataCollectionWriterCore.Scope(newState, item));
			this.NotifyListener(newState);
		}

		// Token: 0x0600024E RID: 590 RVA: 0x00008D78 File Offset: 0x00006F78
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

		// Token: 0x0600024F RID: 591 RVA: 0x00008DD7 File Offset: 0x00006FD7
		private void ReplaceScope(ODataCollectionWriterCore.CollectionWriterState newState, ODataItem item)
		{
			this.ValidateTransition(newState);
			this.scopes.Pop();
			this.scopes.Push(new ODataCollectionWriterCore.Scope(newState, item));
			this.NotifyListener(newState);
		}

		// Token: 0x06000250 RID: 592 RVA: 0x00008E08 File Offset: 0x00007008
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

		// Token: 0x04000129 RID: 297
		private readonly ODataOutputContext outputContext;

		// Token: 0x0400012A RID: 298
		private readonly IODataReaderWriterListener listener;

		// Token: 0x0400012B RID: 299
		private readonly Stack<ODataCollectionWriterCore.Scope> scopes = new Stack<ODataCollectionWriterCore.Scope>();

		// Token: 0x0400012C RID: 300
		private readonly IEdmTypeReference expectedItemType;

		// Token: 0x0400012D RID: 301
		private IDuplicatePropertyNameChecker duplicatePropertyNameChecker;

		// Token: 0x0400012E RID: 302
		private CollectionWithoutExpectedTypeValidator collectionValidator;

		// Token: 0x02000259 RID: 601
		internal enum CollectionWriterState
		{
			// Token: 0x04000AED RID: 2797
			Start,
			// Token: 0x04000AEE RID: 2798
			Collection,
			// Token: 0x04000AEF RID: 2799
			Item,
			// Token: 0x04000AF0 RID: 2800
			Completed,
			// Token: 0x04000AF1 RID: 2801
			Error
		}

		// Token: 0x0200025A RID: 602
		private sealed class Scope
		{
			// Token: 0x06001772 RID: 6002 RVA: 0x000474EA File Offset: 0x000456EA
			public Scope(ODataCollectionWriterCore.CollectionWriterState state, object item)
			{
				this.state = state;
				this.item = item;
			}

			// Token: 0x17000541 RID: 1345
			// (get) Token: 0x06001773 RID: 6003 RVA: 0x00047500 File Offset: 0x00045700
			public ODataCollectionWriterCore.CollectionWriterState State
			{
				get
				{
					return this.state;
				}
			}

			// Token: 0x17000542 RID: 1346
			// (get) Token: 0x06001774 RID: 6004 RVA: 0x00047508 File Offset: 0x00045708
			public object Item
			{
				get
				{
					return this.item;
				}
			}

			// Token: 0x04000AF2 RID: 2802
			private readonly ODataCollectionWriterCore.CollectionWriterState state;

			// Token: 0x04000AF3 RID: 2803
			private readonly object item;
		}
	}
}
