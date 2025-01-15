using System;
using System.Collections.Generic;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core
{
	// Token: 0x02000039 RID: 57
	internal abstract class ODataCollectionWriterCore : ODataCollectionWriter, IODataOutputInStreamErrorListener
	{
		// Token: 0x06000207 RID: 519 RVA: 0x00006F18 File Offset: 0x00005118
		protected ODataCollectionWriterCore(ODataOutputContext outputContext, IEdmTypeReference itemTypeReference)
			: this(outputContext, itemTypeReference, null)
		{
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00006F23 File Offset: 0x00005123
		protected ODataCollectionWriterCore(ODataOutputContext outputContext, IEdmTypeReference expectedItemType, IODataReaderWriterListener listener)
		{
			this.outputContext = outputContext;
			this.expectedItemType = expectedItemType;
			this.listener = listener;
			this.scopes.Push(new ODataCollectionWriterCore.Scope(ODataCollectionWriterCore.CollectionWriterState.Start, null));
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000209 RID: 521 RVA: 0x00006F5D File Offset: 0x0000515D
		protected ODataCollectionWriterCore.CollectionWriterState State
		{
			get
			{
				return this.scopes.Peek().State;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600020A RID: 522 RVA: 0x00006F70 File Offset: 0x00005170
		protected DuplicatePropertyNamesChecker DuplicatePropertyNamesChecker
		{
			get
			{
				if (this.duplicatePropertyNamesChecker == null)
				{
					this.duplicatePropertyNamesChecker = new DuplicatePropertyNamesChecker(this.outputContext.MessageWriterSettings.WriterBehavior.AllowDuplicatePropertyNames, this.outputContext.WritingResponse, !this.outputContext.MessageWriterSettings.EnableFullValidation);
				}
				return this.duplicatePropertyNamesChecker;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600020B RID: 523 RVA: 0x00006FC9 File Offset: 0x000051C9
		protected CollectionWithoutExpectedTypeValidator CollectionValidator
		{
			get
			{
				return this.collectionValidator;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600020C RID: 524 RVA: 0x00006FD1 File Offset: 0x000051D1
		protected IEdmTypeReference ItemTypeReference
		{
			get
			{
				return this.expectedItemType;
			}
		}

		// Token: 0x0600020D RID: 525 RVA: 0x00006FDC File Offset: 0x000051DC
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

		// Token: 0x0600020E RID: 526 RVA: 0x00007014 File Offset: 0x00005214
		public sealed override void WriteStart(ODataCollectionStart collectionStart)
		{
			this.VerifyCanWriteStart(true, collectionStart);
			this.WriteStartImplementation(collectionStart);
		}

		// Token: 0x0600020F RID: 527 RVA: 0x00007025 File Offset: 0x00005225
		public sealed override void WriteItem(object item)
		{
			this.VerifyCanWriteItem(true);
			this.WriteItemImplementation(item);
		}

		// Token: 0x06000210 RID: 528 RVA: 0x00007035 File Offset: 0x00005235
		public sealed override void WriteEnd()
		{
			this.VerifyCanWriteEnd(true);
			this.WriteEndImplementation();
			if (this.scopes.Peek().State == ODataCollectionWriterCore.CollectionWriterState.Completed)
			{
				this.Flush();
			}
		}

		// Token: 0x06000211 RID: 529 RVA: 0x00007060 File Offset: 0x00005260
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

		// Token: 0x06000212 RID: 530 RVA: 0x000070BF File Offset: 0x000052BF
		protected static bool IsErrorState(ODataCollectionWriterCore.CollectionWriterState state)
		{
			return state == ODataCollectionWriterCore.CollectionWriterState.Error;
		}

		// Token: 0x06000213 RID: 531
		protected abstract void VerifyNotDisposed();

		// Token: 0x06000214 RID: 532
		protected abstract void FlushSynchronously();

		// Token: 0x06000215 RID: 533
		protected abstract void StartPayload();

		// Token: 0x06000216 RID: 534
		protected abstract void EndPayload();

		// Token: 0x06000217 RID: 535
		protected abstract void StartCollection(ODataCollectionStart collectionStart);

		// Token: 0x06000218 RID: 536
		protected abstract void EndCollection();

		// Token: 0x06000219 RID: 537
		protected abstract void WriteCollectionItem(object item, IEdmTypeReference expectedItemTypeReference);

		// Token: 0x0600021A RID: 538 RVA: 0x000070C5 File Offset: 0x000052C5
		private void VerifyCanWriteStart(bool synchronousCall, ODataCollectionStart collectionStart)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataCollectionStart>(collectionStart, "collection");
			this.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
		}

		// Token: 0x0600021B RID: 539 RVA: 0x00007118 File Offset: 0x00005318
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

		// Token: 0x0600021C RID: 540 RVA: 0x0000715E File Offset: 0x0000535E
		private void VerifyCanWriteItem(bool synchronousCall)
		{
			this.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
		}

		// Token: 0x0600021D RID: 541 RVA: 0x000071B0 File Offset: 0x000053B0
		private void WriteItemImplementation(object item)
		{
			if (this.scopes.Peek().State != ODataCollectionWriterCore.CollectionWriterState.Item)
			{
				this.EnterScope(ODataCollectionWriterCore.CollectionWriterState.Item, item);
			}
			this.InterceptException(delegate
			{
				this.outputContext.WriterValidator.ValidateCollectionItem(item, true);
				this.WriteCollectionItem(item, this.expectedItemType);
			});
		}

		// Token: 0x0600021E RID: 542 RVA: 0x00007203 File Offset: 0x00005403
		private void VerifyCanWriteEnd(bool synchronousCall)
		{
			this.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00007299 File Offset: 0x00005499
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

		// Token: 0x06000220 RID: 544 RVA: 0x000072AD File Offset: 0x000054AD
		private void VerifyCanFlush(bool synchronousCall)
		{
			this.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
		}

		// Token: 0x06000221 RID: 545 RVA: 0x000072BC File Offset: 0x000054BC
		private void VerifyCallAllowed(bool synchronousCall)
		{
			if (synchronousCall && !this.outputContext.Synchronous)
			{
				throw new ODataException(Strings.ODataCollectionWriterCore_SyncCallOnAsyncWriter);
			}
		}

		// Token: 0x06000222 RID: 546 RVA: 0x000072DC File Offset: 0x000054DC
		private void StartPayloadInStartState()
		{
			ODataCollectionWriterCore.Scope scope = this.scopes.Peek();
			if (scope.State == ODataCollectionWriterCore.CollectionWriterState.Start)
			{
				this.InterceptException(new Action(this.StartPayload));
			}
		}

		// Token: 0x06000223 RID: 547 RVA: 0x00007310 File Offset: 0x00005510
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

		// Token: 0x06000224 RID: 548 RVA: 0x0000735C File Offset: 0x0000555C
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

		// Token: 0x06000225 RID: 549 RVA: 0x000073A4 File Offset: 0x000055A4
		private void EnterScope(ODataCollectionWriterCore.CollectionWriterState newState, object item)
		{
			this.InterceptException(delegate
			{
				this.ValidateTransition(newState);
			});
			this.scopes.Push(new ODataCollectionWriterCore.Scope(newState, item));
			this.NotifyListener(newState);
		}

		// Token: 0x06000226 RID: 550 RVA: 0x000073FC File Offset: 0x000055FC
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

		// Token: 0x06000227 RID: 551 RVA: 0x0000745B File Offset: 0x0000565B
		private void ReplaceScope(ODataCollectionWriterCore.CollectionWriterState newState, ODataItem item)
		{
			this.ValidateTransition(newState);
			this.scopes.Pop();
			this.scopes.Push(new ODataCollectionWriterCore.Scope(newState, item));
			this.NotifyListener(newState);
		}

		// Token: 0x06000228 RID: 552 RVA: 0x0000748C File Offset: 0x0000568C
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

		// Token: 0x0400012D RID: 301
		private readonly ODataOutputContext outputContext;

		// Token: 0x0400012E RID: 302
		private readonly IODataReaderWriterListener listener;

		// Token: 0x0400012F RID: 303
		private readonly Stack<ODataCollectionWriterCore.Scope> scopes = new Stack<ODataCollectionWriterCore.Scope>();

		// Token: 0x04000130 RID: 304
		private readonly IEdmTypeReference expectedItemType;

		// Token: 0x04000131 RID: 305
		private DuplicatePropertyNamesChecker duplicatePropertyNamesChecker;

		// Token: 0x04000132 RID: 306
		private CollectionWithoutExpectedTypeValidator collectionValidator;

		// Token: 0x0200003A RID: 58
		internal enum CollectionWriterState
		{
			// Token: 0x04000134 RID: 308
			Start,
			// Token: 0x04000135 RID: 309
			Collection,
			// Token: 0x04000136 RID: 310
			Item,
			// Token: 0x04000137 RID: 311
			Completed,
			// Token: 0x04000138 RID: 312
			Error
		}

		// Token: 0x0200003B RID: 59
		private sealed class Scope
		{
			// Token: 0x0600022A RID: 554 RVA: 0x000075CC File Offset: 0x000057CC
			public Scope(ODataCollectionWriterCore.CollectionWriterState state, object item)
			{
				this.state = state;
				this.item = item;
			}

			// Token: 0x17000097 RID: 151
			// (get) Token: 0x0600022B RID: 555 RVA: 0x000075E2 File Offset: 0x000057E2
			public ODataCollectionWriterCore.CollectionWriterState State
			{
				get
				{
					return this.state;
				}
			}

			// Token: 0x17000098 RID: 152
			// (get) Token: 0x0600022C RID: 556 RVA: 0x000075EA File Offset: 0x000057EA
			public object Item
			{
				get
				{
					return this.item;
				}
			}

			// Token: 0x04000139 RID: 313
			private readonly ODataCollectionWriterCore.CollectionWriterState state;

			// Token: 0x0400013A RID: 314
			private readonly object item;
		}
	}
}
