using System;
using System.Collections.Generic;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData
{
	// Token: 0x0200014C RID: 332
	internal abstract class ODataCollectionReaderCore : ODataCollectionReader
	{
		// Token: 0x060008C8 RID: 2248 RVA: 0x0001C0A8 File Offset: 0x0001A2A8
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

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x060008C9 RID: 2249 RVA: 0x0001C0F7 File Offset: 0x0001A2F7
		public sealed override ODataCollectionReaderState State
		{
			get
			{
				this.inputContext.VerifyNotDisposed();
				return this.scopes.Peek().State;
			}
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x060008CA RID: 2250 RVA: 0x0001C114 File Offset: 0x0001A314
		public sealed override object Item
		{
			get
			{
				this.inputContext.VerifyNotDisposed();
				return this.scopes.Peek().Item;
			}
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x060008CB RID: 2251 RVA: 0x0001C131 File Offset: 0x0001A331
		protected bool IsCollectionElementEmpty
		{
			get
			{
				return this.scopes.Peek().IsCollectionElementEmpty;
			}
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x060008CC RID: 2252 RVA: 0x0001C143 File Offset: 0x0001A343
		// (set) Token: 0x060008CD RID: 2253 RVA: 0x0001C14C File Offset: 0x0001A34C
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

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x060008CE RID: 2254 RVA: 0x0001C1A9 File Offset: 0x0001A3A9
		protected CollectionWithoutExpectedTypeValidator CollectionValidator
		{
			get
			{
				return this.collectionValidator;
			}
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x060008CF RID: 2255 RVA: 0x0001C1B1 File Offset: 0x0001A3B1
		protected bool IsReadingNestedPayload
		{
			get
			{
				return this.listener != null;
			}
		}

		// Token: 0x060008D0 RID: 2256 RVA: 0x0001C1BF File Offset: 0x0001A3BF
		public sealed override bool Read()
		{
			this.VerifyCanRead(true);
			return this.InterceptException<bool>(new Func<bool>(this.ReadSynchronously));
		}

		// Token: 0x060008D1 RID: 2257 RVA: 0x0001C1DC File Offset: 0x0001A3DC
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

		// Token: 0x060008D2 RID: 2258
		protected abstract bool ReadAtStartImplementation();

		// Token: 0x060008D3 RID: 2259
		protected abstract bool ReadAtCollectionStartImplementation();

		// Token: 0x060008D4 RID: 2260
		protected abstract bool ReadAtValueImplementation();

		// Token: 0x060008D5 RID: 2261
		protected abstract bool ReadAtCollectionEndImplementation();

		// Token: 0x060008D6 RID: 2262 RVA: 0x0001C259 File Offset: 0x0001A459
		protected bool ReadSynchronously()
		{
			return this.ReadImplementation();
		}

		// Token: 0x060008D7 RID: 2263 RVA: 0x0001C261 File Offset: 0x0001A461
		protected void EnterScope(ODataCollectionReaderState state, object item)
		{
			this.EnterScope(state, item, false);
		}

		// Token: 0x060008D8 RID: 2264 RVA: 0x0001C26C File Offset: 0x0001A46C
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

		// Token: 0x060008D9 RID: 2265 RVA: 0x0001C2BE File Offset: 0x0001A4BE
		protected void ReplaceScope(ODataCollectionReaderState state, object item)
		{
			if (state == ODataCollectionReaderState.Value)
			{
				ValidationUtils.ValidateCollectionItem(item, true);
			}
			this.scopes.Pop();
			this.EnterScope(state, item);
		}

		// Token: 0x060008DA RID: 2266 RVA: 0x0001C2DF File Offset: 0x0001A4DF
		protected void PopScope(ODataCollectionReaderState state)
		{
			this.scopes.Pop();
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x0001C2F0 File Offset: 0x0001A4F0
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

		// Token: 0x060008DC RID: 2268 RVA: 0x0001C32C File Offset: 0x0001A52C
		private void VerifyCanRead(bool synchronousCall)
		{
			this.inputContext.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
			if (this.State == ODataCollectionReaderState.Exception || this.State == ODataCollectionReaderState.Completed)
			{
				throw new ODataException(Strings.ODataCollectionReaderCore_ReadOrReadAsyncCalledInInvalidState(this.State));
			}
		}

		// Token: 0x060008DD RID: 2269 RVA: 0x0001C368 File Offset: 0x0001A568
		private void VerifyCallAllowed(bool synchronousCall)
		{
			if (synchronousCall)
			{
				this.VerifySynchronousCallAllowed();
			}
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x0001C373 File Offset: 0x0001A573
		private void VerifySynchronousCallAllowed()
		{
			if (!this.inputContext.Synchronous)
			{
				throw new ODataException(Strings.ODataCollectionReaderCore_SyncCallOnAsyncReader);
			}
		}

		// Token: 0x04000358 RID: 856
		private readonly ODataInputContext inputContext;

		// Token: 0x04000359 RID: 857
		private readonly Stack<ODataCollectionReaderCore.Scope> scopes = new Stack<ODataCollectionReaderCore.Scope>();

		// Token: 0x0400035A RID: 858
		private readonly IODataReaderWriterListener listener;

		// Token: 0x0400035B RID: 859
		private CollectionWithoutExpectedTypeValidator collectionValidator;

		// Token: 0x0400035C RID: 860
		private IEdmTypeReference expectedItemTypeReference;

		// Token: 0x0200014D RID: 333
		protected sealed class Scope
		{
			// Token: 0x060008DF RID: 2271 RVA: 0x0001C38D File Offset: 0x0001A58D
			public Scope(ODataCollectionReaderState state, object item)
				: this(state, item, false)
			{
			}

			// Token: 0x060008E0 RID: 2272 RVA: 0x0001C398 File Offset: 0x0001A598
			public Scope(ODataCollectionReaderState state, object item, bool isCollectionElementEmpty)
			{
				this.state = state;
				this.item = item;
				this.isCollectionElementEmpty = isCollectionElementEmpty;
				bool flag = this.isCollectionElementEmpty;
			}

			// Token: 0x17000226 RID: 550
			// (get) Token: 0x060008E1 RID: 2273 RVA: 0x0001C3BC File Offset: 0x0001A5BC
			public ODataCollectionReaderState State
			{
				get
				{
					return this.state;
				}
			}

			// Token: 0x17000227 RID: 551
			// (get) Token: 0x060008E2 RID: 2274 RVA: 0x0001C3C4 File Offset: 0x0001A5C4
			public object Item
			{
				get
				{
					return this.item;
				}
			}

			// Token: 0x17000228 RID: 552
			// (get) Token: 0x060008E3 RID: 2275 RVA: 0x0001C3CC File Offset: 0x0001A5CC
			public bool IsCollectionElementEmpty
			{
				get
				{
					return this.isCollectionElementEmpty;
				}
			}

			// Token: 0x0400035D RID: 861
			private readonly ODataCollectionReaderState state;

			// Token: 0x0400035E RID: 862
			private readonly object item;

			// Token: 0x0400035F RID: 863
			private readonly bool isCollectionElementEmpty;
		}
	}
}
