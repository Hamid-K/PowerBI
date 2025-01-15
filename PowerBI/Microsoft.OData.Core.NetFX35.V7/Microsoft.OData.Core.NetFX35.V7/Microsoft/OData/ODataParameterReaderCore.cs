using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.OData.Edm;
using Microsoft.OData.Metadata;

namespace Microsoft.OData
{
	// Token: 0x0200007E RID: 126
	internal abstract class ODataParameterReaderCore : ODataParameterReader, IODataReaderWriterListener
	{
		// Token: 0x060004D3 RID: 1235 RVA: 0x0000D5FB File Offset: 0x0000B7FB
		protected ODataParameterReaderCore(ODataInputContext inputContext, IEdmOperation operation)
		{
			this.inputContext = inputContext;
			this.operation = operation;
			this.EnterScope(ODataParameterReaderState.Start, null, null);
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x060004D4 RID: 1236 RVA: 0x0000D635 File Offset: 0x0000B835
		public sealed override ODataParameterReaderState State
		{
			get
			{
				this.inputContext.VerifyNotDisposed();
				return this.scopes.Peek().State;
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x060004D5 RID: 1237 RVA: 0x0000D652 File Offset: 0x0000B852
		public override string Name
		{
			get
			{
				this.inputContext.VerifyNotDisposed();
				return this.scopes.Peek().Name;
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x060004D6 RID: 1238 RVA: 0x0000D66F File Offset: 0x0000B86F
		public override object Value
		{
			get
			{
				this.inputContext.VerifyNotDisposed();
				return this.scopes.Peek().Value;
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x060004D7 RID: 1239 RVA: 0x0000D68C File Offset: 0x0000B88C
		protected IEdmOperation Operation
		{
			get
			{
				return this.operation;
			}
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x0000D694 File Offset: 0x0000B894
		public override ODataReader CreateResourceReader()
		{
			this.VerifyCanCreateSubReader(ODataParameterReaderState.Resource);
			this.subReaderState = ODataParameterReaderCore.SubReaderState.Active;
			IEdmStructuredType edmStructuredType = (IEdmStructuredType)this.GetParameterTypeReference(this.Name).Definition;
			return this.CreateResourceReader(edmStructuredType);
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x0000D6D0 File Offset: 0x0000B8D0
		public override ODataReader CreateResourceSetReader()
		{
			this.VerifyCanCreateSubReader(ODataParameterReaderState.ResourceSet);
			this.subReaderState = ODataParameterReaderCore.SubReaderState.Active;
			IEdmStructuredType edmStructuredType = (IEdmStructuredType)((IEdmCollectionType)this.GetParameterTypeReference(this.Name).Definition).ElementType.Definition;
			return this.CreateResourceSetReader(edmStructuredType);
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x0000D718 File Offset: 0x0000B918
		public override ODataCollectionReader CreateCollectionReader()
		{
			this.VerifyCanCreateSubReader(ODataParameterReaderState.Collection);
			this.subReaderState = ODataParameterReaderCore.SubReaderState.Active;
			IEdmTypeReference elementType = ((IEdmCollectionType)this.GetParameterTypeReference(this.Name).Definition).ElementType;
			return this.CreateCollectionReader(elementType);
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x0000D756 File Offset: 0x0000B956
		public sealed override bool Read()
		{
			this.VerifyCanRead(true);
			return this.InterceptException<bool>(new Func<bool>(this.ReadSynchronously));
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x0000D771 File Offset: 0x0000B971
		void IODataReaderWriterListener.OnException()
		{
			this.EnterScope(ODataParameterReaderState.Exception, null, null);
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x0000D77C File Offset: 0x0000B97C
		void IODataReaderWriterListener.OnCompleted()
		{
			this.subReaderState = ODataParameterReaderCore.SubReaderState.Completed;
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x0000D788 File Offset: 0x0000B988
		protected internal IEdmTypeReference GetParameterTypeReference(string parameterName)
		{
			IEdmOperationParameter edmOperationParameter = this.Operation.FindParameter(parameterName);
			if (edmOperationParameter == null)
			{
				throw new ODataException(Strings.ODataParameterReaderCore_ParameterNameNotInMetadata(parameterName, this.Operation.Name));
			}
			return this.inputContext.EdmTypeResolver.GetParameterType(edmOperationParameter);
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x0000D7D0 File Offset: 0x0000B9D0
		protected internal void EnterScope(ODataParameterReaderState state, string name, object value)
		{
			if (state == ODataParameterReaderState.Value && value != null && !EdmLibraryExtensions.IsPrimitiveType(value.GetType()) && !(value is ODataEnumValue))
			{
				throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataParameterReaderCore_ValueMustBePrimitiveOrNull));
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

		// Token: 0x060004E0 RID: 1248 RVA: 0x0000D93C File Offset: 0x0000BB3C
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "state", Justification = "Used in debug builds in assertions.")]
		[SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "scope", Justification = "Used in debug builds in assertions.")]
		protected internal void PopScope(ODataParameterReaderState state)
		{
			ODataParameterReaderCore.Scope scope = this.scopes.Pop();
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x0000D955 File Offset: 0x0000BB55
		protected void OnParameterCompleted()
		{
			this.subReaderState = ODataParameterReaderCore.SubReaderState.None;
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x0000D960 File Offset: 0x0000BB60
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
			case ODataParameterReaderState.Resource:
			case ODataParameterReaderState.ResourceSet:
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

		// Token: 0x060004E3 RID: 1251
		protected abstract bool ReadAtStartImplementation();

		// Token: 0x060004E4 RID: 1252
		protected abstract bool ReadNextParameterImplementation();

		// Token: 0x060004E5 RID: 1253
		protected abstract ODataReader CreateResourceReader(IEdmStructuredType expectedResourceType);

		// Token: 0x060004E6 RID: 1254
		protected abstract ODataReader CreateResourceSetReader(IEdmStructuredType expectedResourceType);

		// Token: 0x060004E7 RID: 1255
		protected abstract ODataCollectionReader CreateCollectionReader(IEdmTypeReference expectedItemTypeReference);

		// Token: 0x060004E8 RID: 1256 RVA: 0x0000D9D7 File Offset: 0x0000BBD7
		protected bool ReadSynchronously()
		{
			return this.ReadImplementation();
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x0000D9DF File Offset: 0x0000BBDF
		private static string GetCreateReaderMethodName(ODataParameterReaderState state)
		{
			return "Create" + state.ToString() + "Reader";
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x0000DA00 File Offset: 0x0000BC00
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

		// Token: 0x060004EB RID: 1259 RVA: 0x0000DA5C File Offset: 0x0000BC5C
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

		// Token: 0x060004EC RID: 1260 RVA: 0x0000DA98 File Offset: 0x0000BC98
		private void VerifyCanRead(bool synchronousCall)
		{
			this.inputContext.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
			if (this.State == ODataParameterReaderState.Exception || this.State == ODataParameterReaderState.Completed)
			{
				throw new ODataException(Strings.ODataParameterReaderCore_ReadOrReadAsyncCalledInInvalidState(this.State));
			}
			if (this.State == ODataParameterReaderState.Resource || this.State == ODataParameterReaderState.ResourceSet || this.State == ODataParameterReaderState.Collection)
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

		// Token: 0x060004ED RID: 1261 RVA: 0x0000DB4D File Offset: 0x0000BD4D
		private void VerifyCallAllowed(bool synchronousCall)
		{
			if (synchronousCall)
			{
				this.VerifySynchronousCallAllowed();
			}
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x0000DB58 File Offset: 0x0000BD58
		private void VerifySynchronousCallAllowed()
		{
			if (!this.inputContext.Synchronous)
			{
				throw new ODataException(Strings.ODataParameterReaderCore_SyncCallOnAsyncReader);
			}
		}

		// Token: 0x0400024F RID: 591
		private readonly ODataInputContext inputContext;

		// Token: 0x04000250 RID: 592
		private readonly IEdmOperation operation;

		// Token: 0x04000251 RID: 593
		private readonly Stack<ODataParameterReaderCore.Scope> scopes = new Stack<ODataParameterReaderCore.Scope>();

		// Token: 0x04000252 RID: 594
		private readonly HashSet<string> parametersRead = new HashSet<string>(StringComparer.Ordinal);

		// Token: 0x04000253 RID: 595
		private ODataParameterReaderCore.SubReaderState subReaderState;

		// Token: 0x0200027C RID: 636
		private enum SubReaderState
		{
			// Token: 0x04000B33 RID: 2867
			None,
			// Token: 0x04000B34 RID: 2868
			Active,
			// Token: 0x04000B35 RID: 2869
			Completed
		}

		// Token: 0x0200027D RID: 637
		protected sealed class Scope
		{
			// Token: 0x060017CF RID: 6095 RVA: 0x0004793D File Offset: 0x00045B3D
			public Scope(ODataParameterReaderState state, string name, object value)
			{
				this.state = state;
				this.name = name;
				this.value = value;
			}

			// Token: 0x17000555 RID: 1365
			// (get) Token: 0x060017D0 RID: 6096 RVA: 0x0004795A File Offset: 0x00045B5A
			public ODataParameterReaderState State
			{
				get
				{
					return this.state;
				}
			}

			// Token: 0x17000556 RID: 1366
			// (get) Token: 0x060017D1 RID: 6097 RVA: 0x00047962 File Offset: 0x00045B62
			public string Name
			{
				get
				{
					return this.name;
				}
			}

			// Token: 0x17000557 RID: 1367
			// (get) Token: 0x060017D2 RID: 6098 RVA: 0x0004796A File Offset: 0x00045B6A
			public object Value
			{
				get
				{
					return this.value;
				}
			}

			// Token: 0x04000B36 RID: 2870
			private readonly ODataParameterReaderState state;

			// Token: 0x04000B37 RID: 2871
			private readonly string name;

			// Token: 0x04000B38 RID: 2872
			private readonly object value;
		}
	}
}
