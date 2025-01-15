using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core
{
	// Token: 0x0200002F RID: 47
	internal abstract class ODataCollectionReaderCore : ODataCollectionReader
	{
		// Token: 0x060001AE RID: 430 RVA: 0x00005CF4 File Offset: 0x00003EF4
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

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060001AF RID: 431 RVA: 0x00005D43 File Offset: 0x00003F43
		public sealed override ODataCollectionReaderState State
		{
			get
			{
				this.inputContext.VerifyNotDisposed();
				return this.scopes.Peek().State;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x00005D60 File Offset: 0x00003F60
		public sealed override object Item
		{
			get
			{
				this.inputContext.VerifyNotDisposed();
				return this.scopes.Peek().Item;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x00005D7D File Offset: 0x00003F7D
		protected bool IsCollectionElementEmpty
		{
			get
			{
				return this.scopes.Peek().IsCollectionElementEmpty;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x00005D8F File Offset: 0x00003F8F
		// (set) Token: 0x060001B3 RID: 435 RVA: 0x00005D98 File Offset: 0x00003F98
		protected IEdmTypeReference ExpectedItemTypeReference
		{
			get
			{
				return this.expectedItemTypeReference;
			}
			set
			{
				ExceptionUtils.CheckArgumentNotNull<IEdmTypeReference>(value, "value");
				if (this.State != ODataCollectionReaderState.Start)
				{
					throw new ODataException(Strings.ODataCollectionReaderCore_ExpectedItemTypeSetInInvalidState(this.State.ToString(), ODataCollectionReaderState.Start.ToString()));
				}
				if (this.expectedItemTypeReference != value)
				{
					this.expectedItemTypeReference = value;
					this.collectionValidator = null;
				}
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x00005DF5 File Offset: 0x00003FF5
		protected CollectionWithoutExpectedTypeValidator CollectionValidator
		{
			get
			{
				return this.collectionValidator;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x00005DFD File Offset: 0x00003FFD
		protected bool IsReadingNestedPayload
		{
			get
			{
				return this.listener != null;
			}
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00005E0B File Offset: 0x0000400B
		public sealed override bool Read()
		{
			this.VerifyCanRead(true);
			return this.InterceptException<bool>(new Func<bool>(this.ReadSynchronously));
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00005E28 File Offset: 0x00004028
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
			case ODataCollectionReaderState.Exception:
			case ODataCollectionReaderState.Completed:
				throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataCollectionReaderCore_ReadImplementation));
			default:
				throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataCollectionReaderCore_ReadImplementation));
			}
			return flag;
		}

		// Token: 0x060001B8 RID: 440
		protected abstract bool ReadAtStartImplementation();

		// Token: 0x060001B9 RID: 441
		protected abstract bool ReadAtCollectionStartImplementation();

		// Token: 0x060001BA RID: 442
		protected abstract bool ReadAtValueImplementation();

		// Token: 0x060001BB RID: 443
		protected abstract bool ReadAtCollectionEndImplementation();

		// Token: 0x060001BC RID: 444 RVA: 0x00005EA5 File Offset: 0x000040A5
		protected bool ReadSynchronously()
		{
			return this.ReadImplementation();
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00005EAD File Offset: 0x000040AD
		protected void EnterScope(ODataCollectionReaderState state, object item)
		{
			this.EnterScope(state, item, false);
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00005EB8 File Offset: 0x000040B8
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

		// Token: 0x060001BF RID: 447 RVA: 0x00005F0A File Offset: 0x0000410A
		protected void ReplaceScope(ODataCollectionReaderState state, object item)
		{
			if (state == ODataCollectionReaderState.Value)
			{
				ValidationUtils.ValidateCollectionItem(item, true);
			}
			this.scopes.Pop();
			this.EnterScope(state, item);
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00005F2B File Offset: 0x0000412B
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "state", Justification = "Used in debug builds in assertions.")]
		[SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "scope", Justification = "Used in debug builds in assertions.")]
		protected void PopScope(ODataCollectionReaderState state)
		{
			this.scopes.Pop();
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00005F3C File Offset: 0x0000413C
		[SuppressMessage("DataWeb.Usage", "AC0014", Justification = "Throws every time")]
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

		// Token: 0x060001C2 RID: 450 RVA: 0x00005F78 File Offset: 0x00004178
		private void VerifyCanRead(bool synchronousCall)
		{
			this.inputContext.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
			if (this.State == ODataCollectionReaderState.Exception || this.State == ODataCollectionReaderState.Completed)
			{
				throw new ODataException(Strings.ODataCollectionReaderCore_ReadOrReadAsyncCalledInInvalidState(this.State));
			}
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00005FB4 File Offset: 0x000041B4
		private void VerifyCallAllowed(bool synchronousCall)
		{
			if (synchronousCall)
			{
				this.VerifySynchronousCallAllowed();
			}
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00005FBF File Offset: 0x000041BF
		private void VerifySynchronousCallAllowed()
		{
			if (!this.inputContext.Synchronous)
			{
				throw new ODataException(Strings.ODataCollectionReaderCore_SyncCallOnAsyncReader);
			}
		}

		// Token: 0x04000117 RID: 279
		private readonly ODataInputContext inputContext;

		// Token: 0x04000118 RID: 280
		private readonly Stack<ODataCollectionReaderCore.Scope> scopes = new Stack<ODataCollectionReaderCore.Scope>();

		// Token: 0x04000119 RID: 281
		private readonly IODataReaderWriterListener listener;

		// Token: 0x0400011A RID: 282
		private CollectionWithoutExpectedTypeValidator collectionValidator;

		// Token: 0x0400011B RID: 283
		private IEdmTypeReference expectedItemTypeReference;

		// Token: 0x02000030 RID: 48
		protected sealed class Scope
		{
			// Token: 0x060001C5 RID: 453 RVA: 0x00005FD9 File Offset: 0x000041D9
			[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily", Justification = "Debug.Assert check only.")]
			public Scope(ODataCollectionReaderState state, object item)
				: this(state, item, false)
			{
			}

			// Token: 0x060001C6 RID: 454 RVA: 0x00005FE4 File Offset: 0x000041E4
			[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily", Justification = "Debug.Assert check only.")]
			public Scope(ODataCollectionReaderState state, object item, bool isCollectionElementEmpty)
			{
				this.state = state;
				this.item = item;
				this.isCollectionElementEmpty = isCollectionElementEmpty;
				bool flag = this.isCollectionElementEmpty;
			}

			// Token: 0x17000088 RID: 136
			// (get) Token: 0x060001C7 RID: 455 RVA: 0x00006008 File Offset: 0x00004208
			public ODataCollectionReaderState State
			{
				get
				{
					return this.state;
				}
			}

			// Token: 0x17000089 RID: 137
			// (get) Token: 0x060001C8 RID: 456 RVA: 0x00006010 File Offset: 0x00004210
			public object Item
			{
				get
				{
					return this.item;
				}
			}

			// Token: 0x1700008A RID: 138
			// (get) Token: 0x060001C9 RID: 457 RVA: 0x00006018 File Offset: 0x00004218
			public bool IsCollectionElementEmpty
			{
				get
				{
					return this.isCollectionElementEmpty;
				}
			}

			// Token: 0x0400011C RID: 284
			private readonly ODataCollectionReaderState state;

			// Token: 0x0400011D RID: 285
			private readonly object item;

			// Token: 0x0400011E RID: 286
			private readonly bool isCollectionElementEmpty;
		}
	}
}
