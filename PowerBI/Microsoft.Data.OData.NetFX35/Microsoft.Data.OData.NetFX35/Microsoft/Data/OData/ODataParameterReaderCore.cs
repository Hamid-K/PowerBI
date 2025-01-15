using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData
{
	// Token: 0x02000154 RID: 340
	internal abstract class ODataParameterReaderCore : ODataParameterReader, IODataReaderWriterListener
	{
		// Token: 0x060008F7 RID: 2295 RVA: 0x0001C4E9 File Offset: 0x0001A6E9
		protected ODataParameterReaderCore(ODataInputContext inputContext, IEdmFunctionImport functionImport)
		{
			this.inputContext = inputContext;
			this.functionImport = functionImport;
			this.EnterScope(ODataParameterReaderState.Start, null, null);
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x060008F8 RID: 2296 RVA: 0x0001C523 File Offset: 0x0001A723
		public sealed override ODataParameterReaderState State
		{
			get
			{
				this.inputContext.VerifyNotDisposed();
				return this.scopes.Peek().State;
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x060008F9 RID: 2297 RVA: 0x0001C540 File Offset: 0x0001A740
		public override string Name
		{
			get
			{
				this.inputContext.VerifyNotDisposed();
				return this.scopes.Peek().Name;
			}
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x060008FA RID: 2298 RVA: 0x0001C55D File Offset: 0x0001A75D
		public override object Value
		{
			get
			{
				this.inputContext.VerifyNotDisposed();
				return this.scopes.Peek().Value;
			}
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x060008FB RID: 2299 RVA: 0x0001C57A File Offset: 0x0001A77A
		protected IEdmFunctionImport FunctionImport
		{
			get
			{
				return this.functionImport;
			}
		}

		// Token: 0x060008FC RID: 2300 RVA: 0x0001C584 File Offset: 0x0001A784
		public override ODataCollectionReader CreateCollectionReader()
		{
			this.VerifyCanCreateSubReader(ODataParameterReaderState.Collection);
			this.subReaderState = ODataParameterReaderCore.SubReaderState.Active;
			IEdmTypeReference elementType = ((IEdmCollectionType)this.GetParameterTypeReference(this.Name).Definition).ElementType;
			return this.CreateCollectionReader(elementType);
		}

		// Token: 0x060008FD RID: 2301 RVA: 0x0001C5C2 File Offset: 0x0001A7C2
		public sealed override bool Read()
		{
			this.VerifyCanRead(true);
			return this.InterceptException<bool>(new Func<bool>(this.ReadSynchronously));
		}

		// Token: 0x060008FE RID: 2302 RVA: 0x0001C5DD File Offset: 0x0001A7DD
		void IODataReaderWriterListener.OnException()
		{
			this.EnterScope(ODataParameterReaderState.Exception, null, null);
		}

		// Token: 0x060008FF RID: 2303 RVA: 0x0001C5E8 File Offset: 0x0001A7E8
		void IODataReaderWriterListener.OnCompleted()
		{
			this.subReaderState = ODataParameterReaderCore.SubReaderState.Completed;
		}

		// Token: 0x06000900 RID: 2304 RVA: 0x0001C5F4 File Offset: 0x0001A7F4
		protected internal IEdmTypeReference GetParameterTypeReference(string parameterName)
		{
			IEdmFunctionParameter edmFunctionParameter = this.FunctionImport.FindParameter(parameterName);
			if (edmFunctionParameter == null)
			{
				throw new ODataException(Strings.ODataParameterReaderCore_ParameterNameNotInMetadata(parameterName, this.FunctionImport.Name));
			}
			return this.inputContext.EdmTypeResolver.GetParameterType(edmFunctionParameter);
		}

		// Token: 0x06000901 RID: 2305 RVA: 0x0001C63C File Offset: 0x0001A83C
		protected internal void EnterScope(ODataParameterReaderState state, string name, object value)
		{
			if (state == ODataParameterReaderState.Value && value != null && !(value is ODataComplexValue) && !EdmLibraryExtensions.IsPrimitiveType(value.GetType()))
			{
				throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataParameterReaderCore_ValueMustBePrimitiveOrComplexOrNull));
			}
			if (this.scopes.Count == 0 || this.State != ODataParameterReaderState.Exception)
			{
				if (state == ODataParameterReaderState.Completed)
				{
					List<string> list = new List<string>();
					foreach (IEdmFunctionParameter edmFunctionParameter in Enumerable.Skip<IEdmFunctionParameter>(this.FunctionImport.Parameters, this.FunctionImport.IsBindable ? 1 : 0))
					{
						if (!this.parametersRead.Contains(edmFunctionParameter.Name) && !this.inputContext.EdmTypeResolver.GetParameterType(edmFunctionParameter).IsNullable)
						{
							list.Add(edmFunctionParameter.Name);
						}
					}
					if (list.Count > 0)
					{
						this.scopes.Push(new ODataParameterReaderCore.Scope(ODataParameterReaderState.Exception, null, null));
						throw new ODataException(Strings.ODataParameterReaderCore_ParametersMissingInPayload(this.FunctionImport.Name, string.Join(",", list.ToArray())));
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

		// Token: 0x06000902 RID: 2306 RVA: 0x0001C7A8 File Offset: 0x0001A9A8
		protected internal void PopScope(ODataParameterReaderState state)
		{
			this.scopes.Pop();
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x0001C7B6 File Offset: 0x0001A9B6
		protected void OnParameterCompleted()
		{
			this.subReaderState = ODataParameterReaderCore.SubReaderState.None;
		}

		// Token: 0x06000904 RID: 2308 RVA: 0x0001C7C0 File Offset: 0x0001A9C0
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

		// Token: 0x06000905 RID: 2309
		protected abstract bool ReadAtStartImplementation();

		// Token: 0x06000906 RID: 2310
		protected abstract bool ReadNextParameterImplementation();

		// Token: 0x06000907 RID: 2311
		protected abstract ODataCollectionReader CreateCollectionReader(IEdmTypeReference expectedItemTypeReference);

		// Token: 0x06000908 RID: 2312 RVA: 0x0001C82F File Offset: 0x0001AA2F
		protected bool ReadSynchronously()
		{
			return this.ReadImplementation();
		}

		// Token: 0x06000909 RID: 2313 RVA: 0x0001C837 File Offset: 0x0001AA37
		private static string GetCreateReaderMethodName(ODataParameterReaderState state)
		{
			return "Create" + state.ToString() + "Reader";
		}

		// Token: 0x0600090A RID: 2314 RVA: 0x0001C854 File Offset: 0x0001AA54
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

		// Token: 0x0600090B RID: 2315 RVA: 0x0001C8B0 File Offset: 0x0001AAB0
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

		// Token: 0x0600090C RID: 2316 RVA: 0x0001C8EC File Offset: 0x0001AAEC
		private void VerifyCanRead(bool synchronousCall)
		{
			this.inputContext.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
			if (this.State == ODataParameterReaderState.Exception || this.State == ODataParameterReaderState.Completed)
			{
				throw new ODataException(Strings.ODataParameterReaderCore_ReadOrReadAsyncCalledInInvalidState(this.State));
			}
			if (this.State == ODataParameterReaderState.Collection)
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

		// Token: 0x0600090D RID: 2317 RVA: 0x0001C98F File Offset: 0x0001AB8F
		private void VerifyCallAllowed(bool synchronousCall)
		{
			if (synchronousCall)
			{
				this.VerifySynchronousCallAllowed();
			}
		}

		// Token: 0x0600090E RID: 2318 RVA: 0x0001C99A File Offset: 0x0001AB9A
		private void VerifySynchronousCallAllowed()
		{
			if (!this.inputContext.Synchronous)
			{
				throw new ODataException(Strings.ODataParameterReaderCore_SyncCallOnAsyncReader);
			}
		}

		// Token: 0x04000366 RID: 870
		private readonly ODataInputContext inputContext;

		// Token: 0x04000367 RID: 871
		private readonly IEdmFunctionImport functionImport;

		// Token: 0x04000368 RID: 872
		private readonly Stack<ODataParameterReaderCore.Scope> scopes = new Stack<ODataParameterReaderCore.Scope>();

		// Token: 0x04000369 RID: 873
		private readonly HashSet<string> parametersRead = new HashSet<string>(StringComparer.Ordinal);

		// Token: 0x0400036A RID: 874
		private ODataParameterReaderCore.SubReaderState subReaderState;

		// Token: 0x02000155 RID: 341
		private enum SubReaderState
		{
			// Token: 0x0400036C RID: 876
			None,
			// Token: 0x0400036D RID: 877
			Active,
			// Token: 0x0400036E RID: 878
			Completed
		}

		// Token: 0x02000156 RID: 342
		protected sealed class Scope
		{
			// Token: 0x0600090F RID: 2319 RVA: 0x0001C9B4 File Offset: 0x0001ABB4
			public Scope(ODataParameterReaderState state, string name, object value)
			{
				this.state = state;
				this.name = name;
				this.value = value;
			}

			// Token: 0x17000234 RID: 564
			// (get) Token: 0x06000910 RID: 2320 RVA: 0x0001C9D1 File Offset: 0x0001ABD1
			public ODataParameterReaderState State
			{
				get
				{
					return this.state;
				}
			}

			// Token: 0x17000235 RID: 565
			// (get) Token: 0x06000911 RID: 2321 RVA: 0x0001C9D9 File Offset: 0x0001ABD9
			public string Name
			{
				get
				{
					return this.name;
				}
			}

			// Token: 0x17000236 RID: 566
			// (get) Token: 0x06000912 RID: 2322 RVA: 0x0001C9E1 File Offset: 0x0001ABE1
			public object Value
			{
				get
				{
					return this.value;
				}
			}

			// Token: 0x0400036F RID: 879
			private readonly ODataParameterReaderState state;

			// Token: 0x04000370 RID: 880
			private readonly string name;

			// Token: 0x04000371 RID: 881
			private readonly object value;
		}
	}
}
