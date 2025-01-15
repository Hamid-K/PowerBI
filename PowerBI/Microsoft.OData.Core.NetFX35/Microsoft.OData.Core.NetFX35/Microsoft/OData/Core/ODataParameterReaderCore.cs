using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.OData.Core.Metadata;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core
{
	// Token: 0x020000E8 RID: 232
	internal abstract class ODataParameterReaderCore : ODataParameterReader, IODataReaderWriterListener
	{
		// Token: 0x060008CD RID: 2253 RVA: 0x0002063A File Offset: 0x0001E83A
		protected ODataParameterReaderCore(ODataInputContext inputContext, IEdmOperation operation)
		{
			this.inputContext = inputContext;
			this.operation = operation;
			this.EnterScope(ODataParameterReaderState.Start, null, null);
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x060008CE RID: 2254 RVA: 0x00020674 File Offset: 0x0001E874
		public sealed override ODataParameterReaderState State
		{
			get
			{
				this.inputContext.VerifyNotDisposed();
				return this.scopes.Peek().State;
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x060008CF RID: 2255 RVA: 0x00020691 File Offset: 0x0001E891
		public override string Name
		{
			get
			{
				this.inputContext.VerifyNotDisposed();
				return this.scopes.Peek().Name;
			}
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x060008D0 RID: 2256 RVA: 0x000206AE File Offset: 0x0001E8AE
		public override object Value
		{
			get
			{
				this.inputContext.VerifyNotDisposed();
				return this.scopes.Peek().Value;
			}
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x060008D1 RID: 2257 RVA: 0x000206CB File Offset: 0x0001E8CB
		protected IEdmOperation Operation
		{
			get
			{
				return this.operation;
			}
		}

		// Token: 0x060008D2 RID: 2258 RVA: 0x000206D4 File Offset: 0x0001E8D4
		public override ODataReader CreateEntryReader()
		{
			this.VerifyCanCreateSubReader(ODataParameterReaderState.Entry);
			this.subReaderState = ODataParameterReaderCore.SubReaderState.Active;
			IEdmEntityType edmEntityType = (IEdmEntityType)this.GetParameterTypeReference(this.Name).Definition;
			return this.CreateEntryReader(edmEntityType);
		}

		// Token: 0x060008D3 RID: 2259 RVA: 0x00020710 File Offset: 0x0001E910
		public override ODataReader CreateFeedReader()
		{
			this.VerifyCanCreateSubReader(ODataParameterReaderState.Feed);
			this.subReaderState = ODataParameterReaderCore.SubReaderState.Active;
			IEdmEntityType edmEntityType = (IEdmEntityType)((IEdmCollectionType)this.GetParameterTypeReference(this.Name).Definition).ElementType.Definition;
			return this.CreateFeedReader(edmEntityType);
		}

		// Token: 0x060008D4 RID: 2260 RVA: 0x00020758 File Offset: 0x0001E958
		public override ODataCollectionReader CreateCollectionReader()
		{
			this.VerifyCanCreateSubReader(ODataParameterReaderState.Collection);
			this.subReaderState = ODataParameterReaderCore.SubReaderState.Active;
			IEdmTypeReference elementType = ((IEdmCollectionType)this.GetParameterTypeReference(this.Name).Definition).ElementType;
			return this.CreateCollectionReader(elementType);
		}

		// Token: 0x060008D5 RID: 2261 RVA: 0x00020796 File Offset: 0x0001E996
		public sealed override bool Read()
		{
			this.VerifyCanRead(true);
			return this.InterceptException<bool>(new Func<bool>(this.ReadSynchronously));
		}

		// Token: 0x060008D6 RID: 2262 RVA: 0x000207B1 File Offset: 0x0001E9B1
		void IODataReaderWriterListener.OnException()
		{
			this.EnterScope(ODataParameterReaderState.Exception, null, null);
		}

		// Token: 0x060008D7 RID: 2263 RVA: 0x000207BC File Offset: 0x0001E9BC
		void IODataReaderWriterListener.OnCompleted()
		{
			this.subReaderState = ODataParameterReaderCore.SubReaderState.Completed;
		}

		// Token: 0x060008D8 RID: 2264 RVA: 0x000207C8 File Offset: 0x0001E9C8
		protected internal IEdmTypeReference GetParameterTypeReference(string parameterName)
		{
			IEdmOperationParameter edmOperationParameter = this.Operation.FindParameter(parameterName);
			if (edmOperationParameter == null)
			{
				throw new ODataException(Strings.ODataParameterReaderCore_ParameterNameNotInMetadata(parameterName, this.Operation.Name));
			}
			return this.inputContext.EdmTypeResolver.GetParameterType(edmOperationParameter);
		}

		// Token: 0x060008D9 RID: 2265 RVA: 0x00020810 File Offset: 0x0001EA10
		protected internal void EnterScope(ODataParameterReaderState state, string name, object value)
		{
			if (state == ODataParameterReaderState.Value && value != null && !(value is ODataComplexValue) && !EdmLibraryExtensions.IsPrimitiveType(value.GetType()) && !(value is ODataEnumValue))
			{
				throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataParameterReaderCore_ValueMustBePrimitiveOrComplexOrNull));
			}
			if (this.scopes.Count == 0 || this.State != ODataParameterReaderState.Exception)
			{
				if (state == ODataParameterReaderState.Completed)
				{
					List<string> list = new List<string>();
					foreach (IEdmOperationParameter edmOperationParameter in Enumerable.Skip<IEdmOperationParameter>(this.Operation.Parameters, this.Operation.IsBound ? 1 : 0))
					{
						if (!this.parametersRead.Contains(edmOperationParameter.Name) && !this.inputContext.EdmTypeResolver.GetParameterType(edmOperationParameter).IsNullable)
						{
							list.Add(edmOperationParameter.Name);
						}
					}
					if (list.Count > 0)
					{
						this.scopes.Push(new ODataParameterReaderCore.Scope(ODataParameterReaderState.Exception, null, null));
						throw new ODataException(Strings.ODataParameterReaderCore_ParametersMissingInPayload(this.Operation.Name, string.Join(",", list.ToArray())));
					}
				}
				else if (name != null)
				{
					if (this.parametersRead.Contains(name))
					{
						throw new ODataException(Strings.ODataParameterReaderCore_DuplicateParametersInPayload(name));
					}
					this.parametersRead.Add(name);
				}
				this.scopes.Push(new ODataParameterReaderCore.Scope(state, name, value));
			}
		}

		// Token: 0x060008DA RID: 2266 RVA: 0x00020984 File Offset: 0x0001EB84
		[SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "scope", Justification = "Used in debug builds in assertions.")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "state", Justification = "Used in debug builds in assertions.")]
		protected internal void PopScope(ODataParameterReaderState state)
		{
			this.scopes.Pop();
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x00020992 File Offset: 0x0001EB92
		protected void OnParameterCompleted()
		{
			this.subReaderState = ODataParameterReaderCore.SubReaderState.None;
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x0002099C File Offset: 0x0001EB9C
		protected bool ReadImplementation()
		{
			bool flag;
			switch (this.State)
			{
			case ODataParameterReaderState.Start:
				flag = this.ReadAtStartImplementation();
				break;
			case ODataParameterReaderState.Value:
			case ODataParameterReaderState.Collection:
			case ODataParameterReaderState.Entry:
			case ODataParameterReaderState.Feed:
				this.OnParameterCompleted();
				flag = this.ReadNextParameterImplementation();
				break;
			case ODataParameterReaderState.Exception:
			case ODataParameterReaderState.Completed:
				throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataParameterReaderCore_ReadImplementation));
			default:
				throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataParameterReaderCore_ReadImplementation));
			}
			return flag;
		}

		// Token: 0x060008DD RID: 2269
		protected abstract bool ReadAtStartImplementation();

		// Token: 0x060008DE RID: 2270
		protected abstract bool ReadNextParameterImplementation();

		// Token: 0x060008DF RID: 2271
		protected abstract ODataReader CreateEntryReader(IEdmEntityType expectedEntityType);

		// Token: 0x060008E0 RID: 2272
		protected abstract ODataReader CreateFeedReader(IEdmEntityType expectedEntityType);

		// Token: 0x060008E1 RID: 2273
		protected abstract ODataCollectionReader CreateCollectionReader(IEdmTypeReference expectedItemTypeReference);

		// Token: 0x060008E2 RID: 2274 RVA: 0x00020A13 File Offset: 0x0001EC13
		protected bool ReadSynchronously()
		{
			return this.ReadImplementation();
		}

		// Token: 0x060008E3 RID: 2275 RVA: 0x00020A1B File Offset: 0x0001EC1B
		private static string GetCreateReaderMethodName(ODataParameterReaderState state)
		{
			return "Create" + state.ToString() + "Reader";
		}

		// Token: 0x060008E4 RID: 2276 RVA: 0x00020A38 File Offset: 0x0001EC38
		private void VerifyCanCreateSubReader(ODataParameterReaderState expectedState)
		{
			this.inputContext.VerifyNotDisposed();
			if (this.State != expectedState)
			{
				throw new ODataException(Strings.ODataParameterReaderCore_InvalidCreateReaderMethodCalledForState(ODataParameterReaderCore.GetCreateReaderMethodName(expectedState), this.State));
			}
			if (this.subReaderState != ODataParameterReaderCore.SubReaderState.None)
			{
				throw new ODataException(Strings.ODataParameterReaderCore_CreateReaderAlreadyCalled(ODataParameterReaderCore.GetCreateReaderMethodName(expectedState), this.Name));
			}
		}

		// Token: 0x060008E5 RID: 2277 RVA: 0x00020A94 File Offset: 0x0001EC94
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
					this.EnterScope(ODataParameterReaderState.Exception, null, null);
				}
				throw;
			}
			return t;
		}

		// Token: 0x060008E6 RID: 2278 RVA: 0x00020AD0 File Offset: 0x0001ECD0
		private void VerifyCanRead(bool synchronousCall)
		{
			this.inputContext.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
			if (this.State == ODataParameterReaderState.Exception || this.State == ODataParameterReaderState.Completed)
			{
				throw new ODataException(Strings.ODataParameterReaderCore_ReadOrReadAsyncCalledInInvalidState(this.State));
			}
			if (this.State == ODataParameterReaderState.Entry || this.State == ODataParameterReaderState.Feed || this.State == ODataParameterReaderState.Collection)
			{
				if (this.subReaderState == ODataParameterReaderCore.SubReaderState.None)
				{
					throw new ODataException(Strings.ODataParameterReaderCore_SubReaderMustBeCreatedAndReadToCompletionBeforeTheNextReadOrReadAsyncCall(this.State, ODataParameterReaderCore.GetCreateReaderMethodName(this.State)));
				}
				if (this.subReaderState == ODataParameterReaderCore.SubReaderState.Active)
				{
					throw new ODataException(Strings.ODataParameterReaderCore_SubReaderMustBeInCompletedStateBeforeTheNextReadOrReadAsyncCall(this.State, ODataParameterReaderCore.GetCreateReaderMethodName(this.State)));
				}
			}
		}

		// Token: 0x060008E7 RID: 2279 RVA: 0x00020B85 File Offset: 0x0001ED85
		private void VerifyCallAllowed(bool synchronousCall)
		{
			if (synchronousCall)
			{
				this.VerifySynchronousCallAllowed();
			}
		}

		// Token: 0x060008E8 RID: 2280 RVA: 0x00020B90 File Offset: 0x0001ED90
		private void VerifySynchronousCallAllowed()
		{
			if (!this.inputContext.Synchronous)
			{
				throw new ODataException(Strings.ODataParameterReaderCore_SyncCallOnAsyncReader);
			}
		}

		// Token: 0x04000395 RID: 917
		private readonly ODataInputContext inputContext;

		// Token: 0x04000396 RID: 918
		private readonly IEdmOperation operation;

		// Token: 0x04000397 RID: 919
		private readonly Stack<ODataParameterReaderCore.Scope> scopes = new Stack<ODataParameterReaderCore.Scope>();

		// Token: 0x04000398 RID: 920
		private readonly HashSet<string> parametersRead = new HashSet<string>(StringComparer.Ordinal);

		// Token: 0x04000399 RID: 921
		private ODataParameterReaderCore.SubReaderState subReaderState;

		// Token: 0x020000E9 RID: 233
		private enum SubReaderState
		{
			// Token: 0x0400039B RID: 923
			None,
			// Token: 0x0400039C RID: 924
			Active,
			// Token: 0x0400039D RID: 925
			Completed
		}

		// Token: 0x020000EA RID: 234
		protected sealed class Scope
		{
			// Token: 0x060008E9 RID: 2281 RVA: 0x00020BAA File Offset: 0x0001EDAA
			public Scope(ODataParameterReaderState state, string name, object value)
			{
				this.state = state;
				this.name = name;
				this.value = value;
			}

			// Token: 0x170001FB RID: 507
			// (get) Token: 0x060008EA RID: 2282 RVA: 0x00020BC7 File Offset: 0x0001EDC7
			public ODataParameterReaderState State
			{
				get
				{
					return this.state;
				}
			}

			// Token: 0x170001FC RID: 508
			// (get) Token: 0x060008EB RID: 2283 RVA: 0x00020BCF File Offset: 0x0001EDCF
			public string Name
			{
				get
				{
					return this.name;
				}
			}

			// Token: 0x170001FD RID: 509
			// (get) Token: 0x060008EC RID: 2284 RVA: 0x00020BD7 File Offset: 0x0001EDD7
			public object Value
			{
				get
				{
					return this.value;
				}
			}

			// Token: 0x0400039E RID: 926
			private readonly ODataParameterReaderState state;

			// Token: 0x0400039F RID: 927
			private readonly string name;

			// Token: 0x040003A0 RID: 928
			private readonly object value;
		}
	}
}
