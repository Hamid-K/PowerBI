using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.OData.Edm;
using Microsoft.OData.Metadata;

namespace Microsoft.OData
{
	// Token: 0x020000A3 RID: 163
	internal abstract class ODataParameterReaderCore : ODataParameterReader, IODataReaderWriterListener
	{
		// Token: 0x060006F4 RID: 1780 RVA: 0x00010A1B File Offset: 0x0000EC1B
		protected ODataParameterReaderCore(ODataInputContext inputContext, IEdmOperation operation)
		{
			this.inputContext = inputContext;
			this.operation = operation;
			this.EnterScope(ODataParameterReaderState.Start, null, null);
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x060006F5 RID: 1781 RVA: 0x00010A55 File Offset: 0x0000EC55
		public sealed override ODataParameterReaderState State
		{
			get
			{
				this.inputContext.VerifyNotDisposed();
				return this.scopes.Peek().State;
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x060006F6 RID: 1782 RVA: 0x00010A72 File Offset: 0x0000EC72
		public override string Name
		{
			get
			{
				this.inputContext.VerifyNotDisposed();
				return this.scopes.Peek().Name;
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x060006F7 RID: 1783 RVA: 0x00010A8F File Offset: 0x0000EC8F
		public override object Value
		{
			get
			{
				this.inputContext.VerifyNotDisposed();
				return this.scopes.Peek().Value;
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x060006F8 RID: 1784 RVA: 0x00010AAC File Offset: 0x0000ECAC
		protected IEdmOperation Operation
		{
			get
			{
				return this.operation;
			}
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x00010AB4 File Offset: 0x0000ECB4
		public override ODataReader CreateResourceReader()
		{
			this.VerifyCanCreateSubReader(ODataParameterReaderState.Resource);
			this.subReaderState = ODataParameterReaderCore.SubReaderState.Active;
			IEdmStructuredType edmStructuredType = (IEdmStructuredType)this.GetParameterTypeReference(this.Name).Definition;
			return this.CreateResourceReader(edmStructuredType);
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x00010AF0 File Offset: 0x0000ECF0
		public override ODataReader CreateResourceSetReader()
		{
			this.VerifyCanCreateSubReader(ODataParameterReaderState.ResourceSet);
			this.subReaderState = ODataParameterReaderCore.SubReaderState.Active;
			IEdmStructuredType edmStructuredType = (IEdmStructuredType)((IEdmCollectionType)this.GetParameterTypeReference(this.Name).Definition).ElementType.Definition;
			return this.CreateResourceSetReader(edmStructuredType);
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x00010B38 File Offset: 0x0000ED38
		public override ODataCollectionReader CreateCollectionReader()
		{
			this.VerifyCanCreateSubReader(ODataParameterReaderState.Collection);
			this.subReaderState = ODataParameterReaderCore.SubReaderState.Active;
			IEdmTypeReference elementType = ((IEdmCollectionType)this.GetParameterTypeReference(this.Name).Definition).ElementType;
			return this.CreateCollectionReader(elementType);
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x00010B76 File Offset: 0x0000ED76
		public sealed override bool Read()
		{
			this.VerifyCanRead(true);
			return this.InterceptException<bool>(new Func<bool>(this.ReadSynchronously));
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x00010B91 File Offset: 0x0000ED91
		public sealed override Task<bool> ReadAsync()
		{
			this.VerifyCanRead(false);
			return this.ReadAsynchronously().FollowOnFaultWith(delegate(Task<bool> t)
			{
				this.EnterScope(ODataParameterReaderState.Exception, null, null);
			});
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x00010BB1 File Offset: 0x0000EDB1
		void IODataReaderWriterListener.OnException()
		{
			this.EnterScope(ODataParameterReaderState.Exception, null, null);
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x00010BBC File Offset: 0x0000EDBC
		void IODataReaderWriterListener.OnCompleted()
		{
			this.subReaderState = ODataParameterReaderCore.SubReaderState.Completed;
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x00010BC8 File Offset: 0x0000EDC8
		protected internal IEdmTypeReference GetParameterTypeReference(string parameterName)
		{
			IEdmOperationParameter edmOperationParameter = this.Operation.FindParameter(parameterName);
			if (edmOperationParameter == null)
			{
				throw new ODataException(Strings.ODataParameterReaderCore_ParameterNameNotInMetadata(parameterName, this.Operation.Name));
			}
			return this.inputContext.EdmTypeResolver.GetParameterType(edmOperationParameter);
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x00010C10 File Offset: 0x0000EE10
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
					foreach (IEdmOperationParameter edmOperationParameter in this.Operation.Parameters.Skip(this.Operation.IsBound ? 1 : 0))
					{
						if (!(edmOperationParameter is IEdmOptionalParameter) && !this.parametersRead.Contains(edmOperationParameter.Name) && !this.inputContext.EdmTypeResolver.GetParameterType(edmOperationParameter).IsNullable)
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

		// Token: 0x06000702 RID: 1794 RVA: 0x00010D84 File Offset: 0x0000EF84
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "state", Justification = "Used in debug builds in assertions.")]
		[SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "scope", Justification = "Used in debug builds in assertions.")]
		protected internal void PopScope(ODataParameterReaderState state)
		{
			ODataParameterReaderCore.Scope scope = this.scopes.Pop();
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x00010D9D File Offset: 0x0000EF9D
		protected void OnParameterCompleted()
		{
			this.subReaderState = ODataParameterReaderCore.SubReaderState.None;
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x00010DA8 File Offset: 0x0000EFA8
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

		// Token: 0x06000705 RID: 1797
		protected abstract bool ReadAtStartImplementation();

		// Token: 0x06000706 RID: 1798
		protected abstract bool ReadNextParameterImplementation();

		// Token: 0x06000707 RID: 1799
		protected abstract ODataReader CreateResourceReader(IEdmStructuredType expectedResourceType);

		// Token: 0x06000708 RID: 1800
		protected abstract ODataReader CreateResourceSetReader(IEdmStructuredType expectedResourceType);

		// Token: 0x06000709 RID: 1801
		protected abstract ODataCollectionReader CreateCollectionReader(IEdmTypeReference expectedItemTypeReference);

		// Token: 0x0600070A RID: 1802 RVA: 0x00010E1F File Offset: 0x0000F01F
		protected bool ReadSynchronously()
		{
			return this.ReadImplementation();
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x00010E27 File Offset: 0x0000F027
		protected virtual Task<bool> ReadAsynchronously()
		{
			return TaskUtils.GetTaskForSynchronousOperation<bool>(new Func<bool>(this.ReadImplementation));
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x00010E3A File Offset: 0x0000F03A
		private static string GetCreateReaderMethodName(ODataParameterReaderState state)
		{
			return "Create" + state.ToString() + "Reader";
		}

		// Token: 0x0600070D RID: 1805 RVA: 0x00010E58 File Offset: 0x0000F058
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

		// Token: 0x0600070E RID: 1806 RVA: 0x00010EB4 File Offset: 0x0000F0B4
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
					this.EnterScope(ODataParameterReaderState.Exception, null, null);
				}
				throw;
			}
			return t;
		}

		// Token: 0x0600070F RID: 1807 RVA: 0x00010EF0 File Offset: 0x0000F0F0
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

		// Token: 0x06000710 RID: 1808 RVA: 0x00010FA5 File Offset: 0x0000F1A5
		private void VerifyCallAllowed(bool synchronousCall)
		{
			if (synchronousCall)
			{
				this.VerifySynchronousCallAllowed();
				return;
			}
			this.VerifyAsynchronousCallAllowed();
		}

		// Token: 0x06000711 RID: 1809 RVA: 0x00010FB7 File Offset: 0x0000F1B7
		private void VerifySynchronousCallAllowed()
		{
			if (!this.inputContext.Synchronous)
			{
				throw new ODataException(Strings.ODataParameterReaderCore_SyncCallOnAsyncReader);
			}
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x00010FD1 File Offset: 0x0000F1D1
		private void VerifyAsynchronousCallAllowed()
		{
			if (this.inputContext.Synchronous)
			{
				throw new ODataException(Strings.ODataParameterReaderCore_AsyncCallOnSyncReader);
			}
		}

		// Token: 0x040002B5 RID: 693
		private readonly ODataInputContext inputContext;

		// Token: 0x040002B6 RID: 694
		private readonly IEdmOperation operation;

		// Token: 0x040002B7 RID: 695
		private readonly Stack<ODataParameterReaderCore.Scope> scopes = new Stack<ODataParameterReaderCore.Scope>();

		// Token: 0x040002B8 RID: 696
		private readonly HashSet<string> parametersRead = new HashSet<string>(StringComparer.Ordinal);

		// Token: 0x040002B9 RID: 697
		private ODataParameterReaderCore.SubReaderState subReaderState;

		// Token: 0x020002F3 RID: 755
		private enum SubReaderState
		{
			// Token: 0x04000CF9 RID: 3321
			None,
			// Token: 0x04000CFA RID: 3322
			Active,
			// Token: 0x04000CFB RID: 3323
			Completed
		}

		// Token: 0x020002F4 RID: 756
		protected sealed class Scope
		{
			// Token: 0x06001D89 RID: 7561 RVA: 0x00057BFE File Offset: 0x00055DFE
			public Scope(ODataParameterReaderState state, string name, object value)
			{
				this.state = state;
				this.name = name;
				this.value = value;
			}

			// Token: 0x170005F5 RID: 1525
			// (get) Token: 0x06001D8A RID: 7562 RVA: 0x00057C1B File Offset: 0x00055E1B
			public ODataParameterReaderState State
			{
				get
				{
					return this.state;
				}
			}

			// Token: 0x170005F6 RID: 1526
			// (get) Token: 0x06001D8B RID: 7563 RVA: 0x00057C23 File Offset: 0x00055E23
			public string Name
			{
				get
				{
					return this.name;
				}
			}

			// Token: 0x170005F7 RID: 1527
			// (get) Token: 0x06001D8C RID: 7564 RVA: 0x00057C2B File Offset: 0x00055E2B
			public object Value
			{
				get
				{
					return this.value;
				}
			}

			// Token: 0x04000CFC RID: 3324
			private readonly ODataParameterReaderState state;

			// Token: 0x04000CFD RID: 3325
			private readonly string name;

			// Token: 0x04000CFE RID: 3326
			private readonly object value;
		}
	}
}
