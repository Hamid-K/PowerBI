using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Microsoft.OData.Edm;

namespace Microsoft.OData
{
	// Token: 0x0200003F RID: 63
	internal abstract class ODataCollectionReaderCore : ODataCollectionReader
	{
		// Token: 0x06000201 RID: 513 RVA: 0x000086A0 File Offset: 0x000068A0
		protected ODataCollectionReaderCore(ODataInputContext inputContext, IEdmTypeReference expectedItemTypeReference, IODataReaderWriterListener listener)
		{
			this.inputContext = inputContext;
			this.expectedItemTypeReference = expectedItemTypeReference;
			if (this.expectedItemTypeReference == null)
			{
				this.collectionValidator = new CollectionWithoutExpectedTypeValidator(null);
			}
			this.listener = listener;
			this.EnterScope(ODataCollectionReaderState.Start, null);
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000202 RID: 514 RVA: 0x000086EF File Offset: 0x000068EF
		public sealed override ODataCollectionReaderState State
		{
			get
			{
				this.inputContext.VerifyNotDisposed();
				return this.scopes.Peek().State;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000203 RID: 515 RVA: 0x0000870C File Offset: 0x0000690C
		public sealed override object Item
		{
			get
			{
				this.inputContext.VerifyNotDisposed();
				return this.scopes.Peek().Item;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000204 RID: 516 RVA: 0x00008729 File Offset: 0x00006929
		// (set) Token: 0x06000205 RID: 517 RVA: 0x00008731 File Offset: 0x00006931
		protected IEdmTypeReference ExpectedItemTypeReference
		{
			get
			{
				return this.expectedItemTypeReference;
			}
			set
			{
				ExceptionUtils.CheckArgumentNotNull<IEdmTypeReference>(value, "value");
				if (this.expectedItemTypeReference != value)
				{
					this.expectedItemTypeReference = value;
					this.collectionValidator = null;
				}
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000206 RID: 518 RVA: 0x00008756 File Offset: 0x00006956
		protected CollectionWithoutExpectedTypeValidator CollectionValidator
		{
			get
			{
				return this.collectionValidator;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000207 RID: 519 RVA: 0x0000875E File Offset: 0x0000695E
		protected bool IsReadingNestedPayload
		{
			get
			{
				return this.listener != null;
			}
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00008769 File Offset: 0x00006969
		public sealed override bool Read()
		{
			this.VerifyCanRead(true);
			return this.InterceptException<bool>(new Func<bool>(this.ReadSynchronously));
		}

		// Token: 0x06000209 RID: 521 RVA: 0x00008784 File Offset: 0x00006984
		protected bool ReadImplementation()
		{
			bool flag;
			switch (this.State)
			{
			case ODataCollectionReaderState.Start:
				flag = this.ReadAtStartImplementation();
				break;
			case ODataCollectionReaderState.CollectionStart:
				flag = this.ReadAtCollectionStartImplementation();
				break;
			case ODataCollectionReaderState.Value:
				flag = this.ReadAtValueImplementation();
				break;
			case ODataCollectionReaderState.CollectionEnd:
				flag = this.ReadAtCollectionEndImplementation();
				break;
			default:
				throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataCollectionReaderCore_ReadImplementation));
			}
			return flag;
		}

		// Token: 0x0600020A RID: 522
		protected abstract bool ReadAtStartImplementation();

		// Token: 0x0600020B RID: 523
		protected abstract bool ReadAtCollectionStartImplementation();

		// Token: 0x0600020C RID: 524
		protected abstract bool ReadAtValueImplementation();

		// Token: 0x0600020D RID: 525
		protected abstract bool ReadAtCollectionEndImplementation();

		// Token: 0x0600020E RID: 526 RVA: 0x000087E7 File Offset: 0x000069E7
		protected bool ReadSynchronously()
		{
			return this.ReadImplementation();
		}

		// Token: 0x0600020F RID: 527 RVA: 0x000087EF File Offset: 0x000069EF
		protected void EnterScope(ODataCollectionReaderState state, object item)
		{
			this.EnterScope(state, item, false);
		}

		// Token: 0x06000210 RID: 528 RVA: 0x000087FC File Offset: 0x000069FC
		protected void EnterScope(ODataCollectionReaderState state, object item, bool isCollectionElementEmpty)
		{
			if (state == ODataCollectionReaderState.Value)
			{
				ValidationUtils.ValidateCollectionItem(item, true);
			}
			this.scopes.Push(new ODataCollectionReaderCore.Scope(state, item, isCollectionElementEmpty));
			if (this.listener != null)
			{
				if (state == ODataCollectionReaderState.Exception)
				{
					this.listener.OnException();
					return;
				}
				if (state == ODataCollectionReaderState.Completed)
				{
					this.listener.OnCompleted();
				}
			}
		}

		// Token: 0x06000211 RID: 529 RVA: 0x0000884E File Offset: 0x00006A4E
		protected void ReplaceScope(ODataCollectionReaderState state, object item)
		{
			if (state == ODataCollectionReaderState.Value)
			{
				ValidationUtils.ValidateCollectionItem(item, true);
			}
			this.scopes.Pop();
			this.EnterScope(state, item);
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00008870 File Offset: 0x00006A70
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "state", Justification = "Used in debug builds in assertions.")]
		[SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "scope", Justification = "Used in debug builds in assertions.")]
		protected void PopScope(ODataCollectionReaderState state)
		{
			ODataCollectionReaderCore.Scope scope = this.scopes.Pop();
		}

		// Token: 0x06000213 RID: 531 RVA: 0x0000888C File Offset: 0x00006A8C
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
					this.EnterScope(ODataCollectionReaderState.Exception, null);
				}
				throw;
			}
			return t;
		}

		// Token: 0x06000214 RID: 532 RVA: 0x000088C8 File Offset: 0x00006AC8
		private void VerifyCanRead(bool synchronousCall)
		{
			this.inputContext.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
			if (this.State == ODataCollectionReaderState.Exception || this.State == ODataCollectionReaderState.Completed)
			{
				throw new ODataException(Strings.ODataCollectionReaderCore_ReadOrReadAsyncCalledInInvalidState(this.State));
			}
		}

		// Token: 0x06000215 RID: 533 RVA: 0x00008904 File Offset: 0x00006B04
		private void VerifyCallAllowed(bool synchronousCall)
		{
			if (synchronousCall)
			{
				this.VerifySynchronousCallAllowed();
			}
		}

		// Token: 0x06000216 RID: 534 RVA: 0x0000890F File Offset: 0x00006B0F
		private void VerifySynchronousCallAllowed()
		{
			if (!this.inputContext.Synchronous)
			{
				throw new ODataException(Strings.ODataCollectionReaderCore_SyncCallOnAsyncReader);
			}
		}

		// Token: 0x04000116 RID: 278
		private readonly ODataInputContext inputContext;

		// Token: 0x04000117 RID: 279
		private readonly Stack<ODataCollectionReaderCore.Scope> scopes = new Stack<ODataCollectionReaderCore.Scope>();

		// Token: 0x04000118 RID: 280
		private readonly IODataReaderWriterListener listener;

		// Token: 0x04000119 RID: 281
		private CollectionWithoutExpectedTypeValidator collectionValidator;

		// Token: 0x0400011A RID: 282
		private IEdmTypeReference expectedItemTypeReference;

		// Token: 0x02000258 RID: 600
		protected sealed class Scope
		{
			// Token: 0x0600176E RID: 5998 RVA: 0x000474B2 File Offset: 0x000456B2
			[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily", Justification = "Debug.Assert check only.")]
			public Scope(ODataCollectionReaderState state, object item)
				: this(state, item, false)
			{
			}

			// Token: 0x0600176F RID: 5999 RVA: 0x000474BD File Offset: 0x000456BD
			[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily", Justification = "Debug.Assert check only.")]
			public Scope(ODataCollectionReaderState state, object item, bool isCollectionElementEmpty)
			{
				this.state = state;
				this.item = item;
				this.isCollectionElementEmpty = isCollectionElementEmpty;
			}

			// Token: 0x1700053F RID: 1343
			// (get) Token: 0x06001770 RID: 6000 RVA: 0x000474DA File Offset: 0x000456DA
			public ODataCollectionReaderState State
			{
				get
				{
					return this.state;
				}
			}

			// Token: 0x17000540 RID: 1344
			// (get) Token: 0x06001771 RID: 6001 RVA: 0x000474E2 File Offset: 0x000456E2
			public object Item
			{
				get
				{
					return this.item;
				}
			}

			// Token: 0x04000AE9 RID: 2793
			private readonly ODataCollectionReaderState state;

			// Token: 0x04000AEA RID: 2794
			private readonly object item;

			// Token: 0x04000AEB RID: 2795
			[SuppressMessage("Microsoft.Performance", "CA1823", Justification = "isCollectionElementEmpty is used in debug.")]
			private readonly bool isCollectionElementEmpty;
		}
	}
}
