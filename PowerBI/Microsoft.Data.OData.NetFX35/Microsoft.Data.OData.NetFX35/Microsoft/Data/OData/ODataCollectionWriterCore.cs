using System;
using System.Collections.Generic;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData
{
	// Token: 0x02000187 RID: 391
	internal abstract class ODataCollectionWriterCore : ODataCollectionWriter, IODataOutputInStreamErrorListener
	{
		// Token: 0x06000AA7 RID: 2727 RVA: 0x00023D33 File Offset: 0x00021F33
		protected ODataCollectionWriterCore(ODataOutputContext outputContext, IEdmTypeReference itemTypeReference)
			: this(outputContext, itemTypeReference, null)
		{
		}

		// Token: 0x06000AA8 RID: 2728 RVA: 0x00023D3E File Offset: 0x00021F3E
		protected ODataCollectionWriterCore(ODataOutputContext outputContext, IEdmTypeReference expectedItemType, IODataReaderWriterListener listener)
		{
			this.outputContext = outputContext;
			this.expectedItemType = expectedItemType;
			this.listener = listener;
			this.scopes.Push(new ODataCollectionWriterCore.Scope(ODataCollectionWriterCore.CollectionWriterState.Start, null));
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06000AA9 RID: 2729 RVA: 0x00023D78 File Offset: 0x00021F78
		protected ODataCollectionWriterCore.CollectionWriterState State
		{
			get
			{
				return this.scopes.Peek().State;
			}
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000AAA RID: 2730 RVA: 0x00023D8A File Offset: 0x00021F8A
		protected DuplicatePropertyNamesChecker DuplicatePropertyNamesChecker
		{
			get
			{
				if (this.duplicatePropertyNamesChecker == null)
				{
					this.duplicatePropertyNamesChecker = new DuplicatePropertyNamesChecker(this.outputContext.MessageWriterSettings.WriterBehavior.AllowDuplicatePropertyNames, this.outputContext.WritingResponse);
				}
				return this.duplicatePropertyNamesChecker;
			}
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000AAB RID: 2731 RVA: 0x00023DC5 File Offset: 0x00021FC5
		protected CollectionWithoutExpectedTypeValidator CollectionValidator
		{
			get
			{
				return this.collectionValidator;
			}
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000AAC RID: 2732 RVA: 0x00023DCD File Offset: 0x00021FCD
		protected IEdmTypeReference ItemTypeReference
		{
			get
			{
				return this.expectedItemType;
			}
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x00023DD8 File Offset: 0x00021FD8
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

		// Token: 0x06000AAE RID: 2734 RVA: 0x00023E10 File Offset: 0x00022010
		public sealed override void WriteStart(ODataCollectionStart collectionStart)
		{
			this.VerifyCanWriteStart(true, collectionStart);
			this.WriteStartImplementation(collectionStart);
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x00023E21 File Offset: 0x00022021
		public sealed override void WriteItem(object item)
		{
			this.VerifyCanWriteItem(true);
			this.WriteItemImplementation(item);
		}

		// Token: 0x06000AB0 RID: 2736 RVA: 0x00023E31 File Offset: 0x00022031
		public sealed override void WriteEnd()
		{
			this.VerifyCanWriteEnd(true);
			this.WriteEndImplementation();
			if (this.scopes.Peek().State == ODataCollectionWriterCore.CollectionWriterState.Completed)
			{
				this.Flush();
			}
		}

		// Token: 0x06000AB1 RID: 2737 RVA: 0x00023E5C File Offset: 0x0002205C
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

		// Token: 0x06000AB2 RID: 2738 RVA: 0x00023EBB File Offset: 0x000220BB
		protected static bool IsErrorState(ODataCollectionWriterCore.CollectionWriterState state)
		{
			return state == ODataCollectionWriterCore.CollectionWriterState.Error;
		}

		// Token: 0x06000AB3 RID: 2739
		protected abstract void VerifyNotDisposed();

		// Token: 0x06000AB4 RID: 2740
		protected abstract void FlushSynchronously();

		// Token: 0x06000AB5 RID: 2741
		protected abstract void StartPayload();

		// Token: 0x06000AB6 RID: 2742
		protected abstract void EndPayload();

		// Token: 0x06000AB7 RID: 2743
		protected abstract void StartCollection(ODataCollectionStart collectionStart);

		// Token: 0x06000AB8 RID: 2744
		protected abstract void EndCollection();

		// Token: 0x06000AB9 RID: 2745
		protected abstract void WriteCollectionItem(object item, IEdmTypeReference expectedItemTypeReference);

		// Token: 0x06000ABA RID: 2746 RVA: 0x00023EC4 File Offset: 0x000220C4
		private void VerifyCanWriteStart(bool synchronousCall, ODataCollectionStart collectionStart)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataCollectionStart>(collectionStart, "collection");
			string name = collectionStart.Name;
			if (name != null && name.Length == 0)
			{
				throw new ODataException(Strings.ODataCollectionWriterCore_CollectionsMustNotHaveEmptyName);
			}
			this.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
		}

		// Token: 0x06000ABB RID: 2747 RVA: 0x00023F40 File Offset: 0x00022140
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

		// Token: 0x06000ABC RID: 2748 RVA: 0x00023F86 File Offset: 0x00022186
		private void VerifyCanWriteItem(bool synchronousCall)
		{
			this.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
		}

		// Token: 0x06000ABD RID: 2749 RVA: 0x00023FC8 File Offset: 0x000221C8
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

		// Token: 0x06000ABE RID: 2750 RVA: 0x0002401B File Offset: 0x0002221B
		private void VerifyCanWriteEnd(bool synchronousCall)
		{
			this.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
		}

		// Token: 0x06000ABF RID: 2751 RVA: 0x000240B1 File Offset: 0x000222B1
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

		// Token: 0x06000AC0 RID: 2752 RVA: 0x000240C5 File Offset: 0x000222C5
		private void VerifyCanFlush(bool synchronousCall)
		{
			this.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
		}

		// Token: 0x06000AC1 RID: 2753 RVA: 0x000240D4 File Offset: 0x000222D4
		private void VerifyCallAllowed(bool synchronousCall)
		{
			if (synchronousCall && !this.outputContext.Synchronous)
			{
				throw new ODataException(Strings.ODataCollectionWriterCore_SyncCallOnAsyncWriter);
			}
		}

		// Token: 0x06000AC2 RID: 2754 RVA: 0x000240F4 File Offset: 0x000222F4
		private void StartPayloadInStartState()
		{
			ODataCollectionWriterCore.Scope scope = this.scopes.Peek();
			if (scope.State == ODataCollectionWriterCore.CollectionWriterState.Start)
			{
				this.InterceptException(new Action(this.StartPayload));
			}
		}

		// Token: 0x06000AC3 RID: 2755 RVA: 0x00024128 File Offset: 0x00022328
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

		// Token: 0x06000AC4 RID: 2756 RVA: 0x00024174 File Offset: 0x00022374
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

		// Token: 0x06000AC5 RID: 2757 RVA: 0x000241BC File Offset: 0x000223BC
		private void EnterScope(ODataCollectionWriterCore.CollectionWriterState newState, object item)
		{
			this.InterceptException(delegate
			{
				this.ValidateTransition(newState);
			});
			this.scopes.Push(new ODataCollectionWriterCore.Scope(newState, item));
			this.NotifyListener(newState);
		}

		// Token: 0x06000AC6 RID: 2758 RVA: 0x00024214 File Offset: 0x00022414
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

		// Token: 0x06000AC7 RID: 2759 RVA: 0x00024273 File Offset: 0x00022473
		private void ReplaceScope(ODataCollectionWriterCore.CollectionWriterState newState, ODataItem item)
		{
			this.ValidateTransition(newState);
			this.scopes.Pop();
			this.scopes.Push(new ODataCollectionWriterCore.Scope(newState, item));
			this.NotifyListener(newState);
		}

		// Token: 0x06000AC8 RID: 2760 RVA: 0x000242A4 File Offset: 0x000224A4
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

		// Token: 0x04000406 RID: 1030
		private readonly ODataOutputContext outputContext;

		// Token: 0x04000407 RID: 1031
		private readonly IODataReaderWriterListener listener;

		// Token: 0x04000408 RID: 1032
		private readonly Stack<ODataCollectionWriterCore.Scope> scopes = new Stack<ODataCollectionWriterCore.Scope>();

		// Token: 0x04000409 RID: 1033
		private readonly IEdmTypeReference expectedItemType;

		// Token: 0x0400040A RID: 1034
		private DuplicatePropertyNamesChecker duplicatePropertyNamesChecker;

		// Token: 0x0400040B RID: 1035
		private CollectionWithoutExpectedTypeValidator collectionValidator;

		// Token: 0x02000188 RID: 392
		internal enum CollectionWriterState
		{
			// Token: 0x0400040D RID: 1037
			Start,
			// Token: 0x0400040E RID: 1038
			Collection,
			// Token: 0x0400040F RID: 1039
			Item,
			// Token: 0x04000410 RID: 1040
			Completed,
			// Token: 0x04000411 RID: 1041
			Error
		}

		// Token: 0x02000189 RID: 393
		private sealed class Scope
		{
			// Token: 0x06000ACA RID: 2762 RVA: 0x000243E4 File Offset: 0x000225E4
			public Scope(ODataCollectionWriterCore.CollectionWriterState state, object item)
			{
				this.state = state;
				this.item = item;
			}

			// Token: 0x1700029A RID: 666
			// (get) Token: 0x06000ACB RID: 2763 RVA: 0x000243FA File Offset: 0x000225FA
			public ODataCollectionWriterCore.CollectionWriterState State
			{
				get
				{
					return this.state;
				}
			}

			// Token: 0x1700029B RID: 667
			// (get) Token: 0x06000ACC RID: 2764 RVA: 0x00024402 File Offset: 0x00022602
			public object Item
			{
				get
				{
					return this.item;
				}
			}

			// Token: 0x04000412 RID: 1042
			private readonly ODataCollectionWriterCore.CollectionWriterState state;

			// Token: 0x04000413 RID: 1043
			private readonly object item;
		}
	}
}
