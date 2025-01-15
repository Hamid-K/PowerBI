using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Microsoft.OData.Edm;

namespace Microsoft.OData
{
	// Token: 0x02000062 RID: 98
	internal abstract class ODataCollectionReaderCore : ODataCollectionReader
	{
		// Token: 0x0600036C RID: 876 RVA: 0x0000A248 File Offset: 0x00008448
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

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x0600036D RID: 877 RVA: 0x0000A297 File Offset: 0x00008497
		public sealed override ODataCollectionReaderState State
		{
			get
			{
				this.inputContext.VerifyNotDisposed();
				return this.scopes.Peek().State;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x0600036E RID: 878 RVA: 0x0000A2B4 File Offset: 0x000084B4
		public sealed override object Item
		{
			get
			{
				this.inputContext.VerifyNotDisposed();
				return this.scopes.Peek().Item;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x0600036F RID: 879 RVA: 0x0000A2D1 File Offset: 0x000084D1
		// (set) Token: 0x06000370 RID: 880 RVA: 0x0000A2D9 File Offset: 0x000084D9
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

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000371 RID: 881 RVA: 0x0000A2FE File Offset: 0x000084FE
		protected CollectionWithoutExpectedTypeValidator CollectionValidator
		{
			get
			{
				return this.collectionValidator;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000372 RID: 882 RVA: 0x0000A306 File Offset: 0x00008506
		protected bool IsReadingNestedPayload
		{
			get
			{
				return this.listener != null;
			}
		}

		// Token: 0x06000373 RID: 883 RVA: 0x0000A311 File Offset: 0x00008511
		public sealed override bool Read()
		{
			this.VerifyCanRead(true);
			return this.InterceptException<bool>(new Func<bool>(this.ReadSynchronously));
		}

		// Token: 0x06000374 RID: 884 RVA: 0x0000A32C File Offset: 0x0000852C
		public sealed override Task<bool> ReadAsync()
		{
			this.VerifyCanRead(false);
			return this.ReadAsynchronously().FollowOnFaultWith(delegate(Task<bool> t)
			{
				this.EnterScope(ODataCollectionReaderState.Exception, null);
			});
		}

		// Token: 0x06000375 RID: 885 RVA: 0x0000A34C File Offset: 0x0000854C
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

		// Token: 0x06000376 RID: 886
		protected abstract bool ReadAtStartImplementation();

		// Token: 0x06000377 RID: 887
		protected abstract bool ReadAtCollectionStartImplementation();

		// Token: 0x06000378 RID: 888
		protected abstract bool ReadAtValueImplementation();

		// Token: 0x06000379 RID: 889
		protected abstract bool ReadAtCollectionEndImplementation();

		// Token: 0x0600037A RID: 890 RVA: 0x0000A3AF File Offset: 0x000085AF
		protected bool ReadSynchronously()
		{
			return this.ReadImplementation();
		}

		// Token: 0x0600037B RID: 891 RVA: 0x0000A3B7 File Offset: 0x000085B7
		protected virtual Task<bool> ReadAsynchronously()
		{
			return TaskUtils.GetTaskForSynchronousOperation<bool>(new Func<bool>(this.ReadImplementation));
		}

		// Token: 0x0600037C RID: 892 RVA: 0x0000A3CA File Offset: 0x000085CA
		protected void EnterScope(ODataCollectionReaderState state, object item)
		{
			this.EnterScope(state, item, false);
		}

		// Token: 0x0600037D RID: 893 RVA: 0x0000A3D8 File Offset: 0x000085D8
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

		// Token: 0x0600037E RID: 894 RVA: 0x0000A42A File Offset: 0x0000862A
		protected void ReplaceScope(ODataCollectionReaderState state, object item)
		{
			if (state == ODataCollectionReaderState.Value)
			{
				ValidationUtils.ValidateCollectionItem(item, true);
			}
			this.scopes.Pop();
			this.EnterScope(state, item);
		}

		// Token: 0x0600037F RID: 895 RVA: 0x0000A44C File Offset: 0x0000864C
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "state", Justification = "Used in debug builds in assertions.")]
		[SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "scope", Justification = "Used in debug builds in assertions.")]
		protected void PopScope(ODataCollectionReaderState state)
		{
			ODataCollectionReaderCore.Scope scope = this.scopes.Pop();
		}

		// Token: 0x06000380 RID: 896 RVA: 0x0000A468 File Offset: 0x00008668
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
					this.EnterScope(ODataCollectionReaderState.Exception, null);
				}
				throw;
			}
			return t;
		}

		// Token: 0x06000381 RID: 897 RVA: 0x0000A4A4 File Offset: 0x000086A4
		private void VerifyCanRead(bool synchronousCall)
		{
			this.inputContext.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
			if (this.State == ODataCollectionReaderState.Exception || this.State == ODataCollectionReaderState.Completed)
			{
				throw new ODataException(Strings.ODataCollectionReaderCore_ReadOrReadAsyncCalledInInvalidState(this.State));
			}
		}

		// Token: 0x06000382 RID: 898 RVA: 0x0000A4E0 File Offset: 0x000086E0
		private void VerifyCallAllowed(bool synchronousCall)
		{
			if (synchronousCall)
			{
				this.VerifySynchronousCallAllowed();
				return;
			}
			this.VerifyAsynchronousCallAllowed();
		}

		// Token: 0x06000383 RID: 899 RVA: 0x0000A4F2 File Offset: 0x000086F2
		private void VerifySynchronousCallAllowed()
		{
			if (!this.inputContext.Synchronous)
			{
				throw new ODataException(Strings.ODataCollectionReaderCore_SyncCallOnAsyncReader);
			}
		}

		// Token: 0x06000384 RID: 900 RVA: 0x0000A50C File Offset: 0x0000870C
		private void VerifyAsynchronousCallAllowed()
		{
			if (this.inputContext.Synchronous)
			{
				throw new ODataException(Strings.ODataCollectionReaderCore_AsyncCallOnSyncReader);
			}
		}

		// Token: 0x04000174 RID: 372
		private readonly ODataInputContext inputContext;

		// Token: 0x04000175 RID: 373
		private readonly Stack<ODataCollectionReaderCore.Scope> scopes = new Stack<ODataCollectionReaderCore.Scope>();

		// Token: 0x04000176 RID: 374
		private readonly IODataReaderWriterListener listener;

		// Token: 0x04000177 RID: 375
		private CollectionWithoutExpectedTypeValidator collectionValidator;

		// Token: 0x04000178 RID: 376
		private IEdmTypeReference expectedItemTypeReference;

		// Token: 0x020002A4 RID: 676
		protected sealed class Scope
		{
			// Token: 0x06001CBC RID: 7356 RVA: 0x00056F77 File Offset: 0x00055177
			[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily", Justification = "Debug.Assert check only.")]
			public Scope(ODataCollectionReaderState state, object item)
				: this(state, item, false)
			{
			}

			// Token: 0x06001CBD RID: 7357 RVA: 0x00056F82 File Offset: 0x00055182
			[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily", Justification = "Debug.Assert check only.")]
			public Scope(ODataCollectionReaderState state, object item, bool isCollectionElementEmpty)
			{
				this.state = state;
				this.item = item;
				this.isCollectionElementEmpty = isCollectionElementEmpty;
			}

			// Token: 0x170005DD RID: 1501
			// (get) Token: 0x06001CBE RID: 7358 RVA: 0x00056F9F File Offset: 0x0005519F
			public ODataCollectionReaderState State
			{
				get
				{
					return this.state;
				}
			}

			// Token: 0x170005DE RID: 1502
			// (get) Token: 0x06001CBF RID: 7359 RVA: 0x00056FA7 File Offset: 0x000551A7
			public object Item
			{
				get
				{
					return this.item;
				}
			}

			// Token: 0x04000C53 RID: 3155
			private readonly ODataCollectionReaderState state;

			// Token: 0x04000C54 RID: 3156
			private readonly object item;

			// Token: 0x04000C55 RID: 3157
			[SuppressMessage("Microsoft.Performance", "CA1823", Justification = "isCollectionElementEmpty is used in debug.")]
			private readonly bool isCollectionElementEmpty;
		}
	}
}
