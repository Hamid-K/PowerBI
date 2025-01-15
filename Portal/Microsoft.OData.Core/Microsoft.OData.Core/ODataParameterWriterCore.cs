using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.OData.Edm;
using Microsoft.OData.Metadata;

namespace Microsoft.OData
{
	// Token: 0x020000A7 RID: 167
	internal abstract class ODataParameterWriterCore : ODataParameterWriter, IODataReaderWriterListener, IODataOutputInStreamErrorListener
	{
		// Token: 0x0600072A RID: 1834 RVA: 0x00011067 File Offset: 0x0000F267
		protected ODataParameterWriterCore(ODataOutputContext outputContext, IEdmOperation operation)
		{
			this.outputContext = outputContext;
			this.operation = operation;
			this.scopes.Push(ODataParameterWriterCore.ParameterWriterState.Start);
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x0600072B RID: 1835 RVA: 0x000110A4 File Offset: 0x0000F2A4
		protected IDuplicatePropertyNameChecker DuplicatePropertyNameChecker
		{
			get
			{
				IDuplicatePropertyNameChecker duplicatePropertyNameChecker;
				if ((duplicatePropertyNameChecker = this.duplicatePropertyNameChecker) == null)
				{
					duplicatePropertyNameChecker = (this.duplicatePropertyNameChecker = this.outputContext.MessageWriterSettings.Validator.CreateDuplicatePropertyNameChecker());
				}
				return duplicatePropertyNameChecker;
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x0600072C RID: 1836 RVA: 0x000110D9 File Offset: 0x0000F2D9
		private ODataParameterWriterCore.ParameterWriterState State
		{
			get
			{
				return this.scopes.Peek();
			}
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x000110E6 File Offset: 0x0000F2E6
		public sealed override void Flush()
		{
			this.VerifyCanFlush(true);
			this.InterceptException(new Action(this.FlushSynchronously));
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x00011102 File Offset: 0x0000F302
		public sealed override Task FlushAsync()
		{
			this.VerifyCanFlush(false);
			return this.FlushAsynchronously().FollowOnFaultWith(delegate(Task t)
			{
				this.EnterErrorScope();
			});
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x00011122 File Offset: 0x0000F322
		public sealed override void WriteStart()
		{
			this.VerifyCanWriteStart(true);
			this.InterceptException(delegate
			{
				this.WriteStartImplementation();
			});
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x0001113D File Offset: 0x0000F33D
		public sealed override Task WriteStartAsync()
		{
			this.VerifyCanWriteStart(false);
			return TaskUtils.GetTaskForSynchronousOperation(delegate
			{
				this.InterceptException(delegate
				{
					this.WriteStartImplementation();
				});
			});
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x00011158 File Offset: 0x0000F358
		public sealed override void WriteValue(string parameterName, object parameterValue)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(parameterName, "parameterName");
			IEdmTypeReference expectedTypeReference = this.VerifyCanWriteValueParameter(true, parameterName, parameterValue);
			this.InterceptException(delegate
			{
				this.WriteValueImplementation(parameterName, parameterValue, expectedTypeReference);
			});
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x000111BC File Offset: 0x0000F3BC
		public sealed override Task WriteValueAsync(string parameterName, object parameterValue)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(parameterName, "parameterName");
			IEdmTypeReference expectedTypeReference = this.VerifyCanWriteValueParameter(false, parameterName, parameterValue);
			Action <>9__1;
			return TaskUtils.GetTaskForSynchronousOperation(delegate
			{
				ODataParameterWriterCore <>4__this = this;
				Action action;
				if ((action = <>9__1) == null)
				{
					action = (<>9__1 = delegate
					{
						this.WriteValueImplementation(parameterName, parameterValue, expectedTypeReference);
					});
				}
				<>4__this.InterceptException(action);
			});
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x00011220 File Offset: 0x0000F420
		public sealed override ODataCollectionWriter CreateCollectionWriter(string parameterName)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(parameterName, "parameterName");
			IEdmTypeReference itemTypeReference = this.VerifyCanCreateCollectionWriter(true, parameterName);
			return this.InterceptException<ODataCollectionWriter>(() => this.CreateCollectionWriterImplementation(parameterName, itemTypeReference));
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x00011278 File Offset: 0x0000F478
		public sealed override Task<ODataCollectionWriter> CreateCollectionWriterAsync(string parameterName)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(parameterName, "parameterName");
			IEdmTypeReference itemTypeReference = this.VerifyCanCreateCollectionWriter(false, parameterName);
			Func<ODataCollectionWriter> <>9__1;
			return TaskUtils.GetTaskForSynchronousOperation<ODataCollectionWriter>(delegate
			{
				ODataParameterWriterCore <>4__this = this;
				Func<ODataCollectionWriter> func;
				if ((func = <>9__1) == null)
				{
					func = (<>9__1 = () => this.CreateCollectionWriterImplementation(parameterName, itemTypeReference));
				}
				return <>4__this.InterceptException<ODataCollectionWriter>(func);
			});
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x000112D0 File Offset: 0x0000F4D0
		public sealed override ODataWriter CreateResourceWriter(string parameterName)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(parameterName, "parameterName");
			IEdmTypeReference itemTypeReference = this.VerifyCanCreateResourceWriter(true, parameterName);
			return this.InterceptException<ODataWriter>(() => this.CreateResourceWriterImplementation(parameterName, itemTypeReference));
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x00011328 File Offset: 0x0000F528
		public sealed override Task<ODataWriter> CreateResourceWriterAsync(string parameterName)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(parameterName, "parameterName");
			IEdmTypeReference itemTypeReference = this.VerifyCanCreateResourceWriter(false, parameterName);
			Func<ODataWriter> <>9__1;
			return TaskUtils.GetTaskForSynchronousOperation<ODataWriter>(delegate
			{
				ODataParameterWriterCore <>4__this = this;
				Func<ODataWriter> func;
				if ((func = <>9__1) == null)
				{
					func = (<>9__1 = () => this.CreateResourceWriterImplementation(parameterName, itemTypeReference));
				}
				return <>4__this.InterceptException<ODataWriter>(func);
			});
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x00011380 File Offset: 0x0000F580
		public sealed override ODataWriter CreateResourceSetWriter(string parameterName)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(parameterName, "parameterName");
			IEdmTypeReference itemTypeReference = this.VerifyCanCreateResourceSetWriter(true, parameterName);
			return this.InterceptException<ODataWriter>(() => this.CreateResourceSetWriterImplementation(parameterName, itemTypeReference));
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x000113D8 File Offset: 0x0000F5D8
		public sealed override Task<ODataWriter> CreateResourceSetWriterAsync(string parameterName)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(parameterName, "parameterName");
			IEdmTypeReference itemTypeReference = this.VerifyCanCreateResourceSetWriter(false, parameterName);
			Func<ODataWriter> <>9__1;
			return TaskUtils.GetTaskForSynchronousOperation<ODataWriter>(delegate
			{
				ODataParameterWriterCore <>4__this = this;
				Func<ODataWriter> func;
				if ((func = <>9__1) == null)
				{
					func = (<>9__1 = () => this.CreateResourceSetWriterImplementation(parameterName, itemTypeReference));
				}
				return <>4__this.InterceptException<ODataWriter>(func);
			});
		}

		// Token: 0x06000739 RID: 1849 RVA: 0x0001142D File Offset: 0x0000F62D
		public sealed override void WriteEnd()
		{
			this.VerifyCanWriteEnd(true);
			this.InterceptException(delegate
			{
				this.WriteEndImplementation();
			});
			if (this.State == ODataParameterWriterCore.ParameterWriterState.Completed)
			{
				this.Flush();
			}
		}

		// Token: 0x0600073A RID: 1850 RVA: 0x00011457 File Offset: 0x0000F657
		public sealed override Task WriteEndAsync()
		{
			this.VerifyCanWriteEnd(false);
			return TaskUtils.GetTaskForSynchronousOperation(delegate
			{
				this.InterceptException(delegate
				{
					this.WriteEndImplementation();
				});
			}).FollowOnSuccessWithTask(delegate(Task task)
			{
				if (this.State == ODataParameterWriterCore.ParameterWriterState.Completed)
				{
					return this.FlushAsync();
				}
				return TaskUtils.CompletedTask;
			});
		}

		// Token: 0x0600073B RID: 1851 RVA: 0x00011482 File Offset: 0x0000F682
		void IODataReaderWriterListener.OnException()
		{
			this.ReplaceScope(ODataParameterWriterCore.ParameterWriterState.Error);
		}

		// Token: 0x0600073C RID: 1852 RVA: 0x0001148B File Offset: 0x0000F68B
		void IODataReaderWriterListener.OnCompleted()
		{
			this.ReplaceScope(ODataParameterWriterCore.ParameterWriterState.CanWriteParameter);
		}

		// Token: 0x0600073D RID: 1853 RVA: 0x00011494 File Offset: 0x0000F694
		void IODataOutputInStreamErrorListener.OnInStreamError()
		{
			throw new ODataException(Strings.ODataParameterWriter_InStreamErrorNotSupported);
		}

		// Token: 0x0600073E RID: 1854
		protected abstract void VerifyNotDisposed();

		// Token: 0x0600073F RID: 1855
		protected abstract void FlushSynchronously();

		// Token: 0x06000740 RID: 1856
		protected abstract Task FlushAsynchronously();

		// Token: 0x06000741 RID: 1857
		protected abstract void StartPayload();

		// Token: 0x06000742 RID: 1858
		protected abstract void WriteValueParameter(string parameterName, object parameterValue, IEdmTypeReference expectedTypeReference);

		// Token: 0x06000743 RID: 1859
		protected abstract ODataCollectionWriter CreateFormatCollectionWriter(string parameterName, IEdmTypeReference expectedItemType);

		// Token: 0x06000744 RID: 1860
		protected abstract ODataWriter CreateFormatResourceWriter(string parameterName, IEdmTypeReference expectedItemType);

		// Token: 0x06000745 RID: 1861
		protected abstract ODataWriter CreateFormatResourceSetWriter(string parameterName, IEdmTypeReference expectedItemType);

		// Token: 0x06000746 RID: 1862
		protected abstract void EndPayload();

		// Token: 0x06000747 RID: 1863 RVA: 0x000114A0 File Offset: 0x0000F6A0
		private void VerifyCanWriteStart(bool synchronousCall)
		{
			this.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
			if (this.State != ODataParameterWriterCore.ParameterWriterState.Start)
			{
				throw new ODataException(Strings.ODataParameterWriterCore_CannotWriteStart);
			}
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x000114C2 File Offset: 0x0000F6C2
		private void WriteStartImplementation()
		{
			this.InterceptException(new Action(this.StartPayload));
			this.EnterScope(ODataParameterWriterCore.ParameterWriterState.CanWriteParameter);
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x000114E0 File Offset: 0x0000F6E0
		private IEdmTypeReference VerifyCanWriteParameterAndGetTypeReference(bool synchronousCall, string parameterName)
		{
			this.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
			this.VerifyNotInErrorOrCompletedState();
			if (this.State != ODataParameterWriterCore.ParameterWriterState.CanWriteParameter)
			{
				throw new ODataException(Strings.ODataParameterWriterCore_CannotWriteParameter);
			}
			if (this.parameterNamesWritten.Contains(parameterName))
			{
				throw new ODataException(Strings.ODataParameterWriterCore_DuplicatedParameterNameNotAllowed(parameterName));
			}
			this.parameterNamesWritten.Add(parameterName);
			return this.GetParameterTypeReference(parameterName);
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x00011544 File Offset: 0x0000F744
		private IEdmTypeReference VerifyCanWriteValueParameter(bool synchronousCall, string parameterName, object parameterValue)
		{
			IEdmTypeReference edmTypeReference = this.VerifyCanWriteParameterAndGetTypeReference(synchronousCall, parameterName);
			if (edmTypeReference != null && !edmTypeReference.IsODataPrimitiveTypeKind() && !edmTypeReference.IsODataEnumTypeKind() && !edmTypeReference.IsODataTypeDefinitionTypeKind())
			{
				throw new ODataException(Strings.ODataParameterWriterCore_CannotWriteValueOnNonValueTypeKind(parameterName, edmTypeReference.TypeKind()));
			}
			if (parameterValue != null && (!EdmLibraryExtensions.IsPrimitiveType(parameterValue.GetType()) || parameterValue is Stream) && !(parameterValue is ODataEnumValue))
			{
				throw new ODataException(Strings.ODataParameterWriterCore_CannotWriteValueOnNonSupportedValueType(parameterName, parameterValue.GetType()));
			}
			return edmTypeReference;
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x000115C0 File Offset: 0x0000F7C0
		private IEdmTypeReference VerifyCanCreateCollectionWriter(bool synchronousCall, string parameterName)
		{
			IEdmTypeReference edmTypeReference = this.VerifyCanWriteParameterAndGetTypeReference(synchronousCall, parameterName);
			if (edmTypeReference != null && !edmTypeReference.IsNonEntityCollectionType())
			{
				throw new ODataException(Strings.ODataParameterWriterCore_CannotCreateCollectionWriterOnNonCollectionTypeKind(parameterName, edmTypeReference.TypeKind()));
			}
			if (edmTypeReference != null)
			{
				return edmTypeReference.GetCollectionItemType();
			}
			return null;
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x00011604 File Offset: 0x0000F804
		private IEdmTypeReference VerifyCanCreateResourceWriter(bool synchronousCall, string parameterName)
		{
			IEdmTypeReference edmTypeReference = this.VerifyCanWriteParameterAndGetTypeReference(synchronousCall, parameterName);
			if (edmTypeReference != null && !edmTypeReference.IsStructured())
			{
				throw new ODataException(Strings.ODataParameterWriterCore_CannotCreateResourceWriterOnNonEntityOrComplexTypeKind(parameterName, edmTypeReference.TypeKind()));
			}
			return edmTypeReference;
		}

		// Token: 0x0600074D RID: 1869 RVA: 0x00011640 File Offset: 0x0000F840
		private IEdmTypeReference VerifyCanCreateResourceSetWriter(bool synchronousCall, string parameterName)
		{
			IEdmTypeReference edmTypeReference = this.VerifyCanWriteParameterAndGetTypeReference(synchronousCall, parameterName);
			if (edmTypeReference != null && !edmTypeReference.IsStructuredCollectionType())
			{
				throw new ODataException(Strings.ODataParameterWriterCore_CannotCreateResourceSetWriterOnNonStructuredCollectionTypeKind(parameterName, edmTypeReference.TypeKind()));
			}
			return edmTypeReference;
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x0001167C File Offset: 0x0000F87C
		private IEdmTypeReference GetParameterTypeReference(string parameterName)
		{
			if (this.operation == null)
			{
				return null;
			}
			IEdmOperationParameter edmOperationParameter = this.operation.FindParameter(parameterName);
			if (edmOperationParameter == null)
			{
				throw new ODataException(Strings.ODataParameterWriterCore_ParameterNameNotFoundInOperation(parameterName, this.operation.Name));
			}
			return this.outputContext.EdmTypeResolver.GetParameterType(edmOperationParameter);
		}

		// Token: 0x0600074F RID: 1871 RVA: 0x000116CC File Offset: 0x0000F8CC
		private void WriteValueImplementation(string parameterName, object parameterValue, IEdmTypeReference expectedTypeReference)
		{
			this.InterceptException(delegate
			{
				this.WriteValueParameter(parameterName, parameterValue, expectedTypeReference);
			});
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x00011710 File Offset: 0x0000F910
		private ODataCollectionWriter CreateCollectionWriterImplementation(string parameterName, IEdmTypeReference expectedItemType)
		{
			ODataCollectionWriter odataCollectionWriter = this.CreateFormatCollectionWriter(parameterName, expectedItemType);
			this.ReplaceScope(ODataParameterWriterCore.ParameterWriterState.ActiveSubWriter);
			return odataCollectionWriter;
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x00011730 File Offset: 0x0000F930
		private ODataWriter CreateResourceWriterImplementation(string parameterName, IEdmTypeReference expectedItemType)
		{
			ODataWriter odataWriter = this.CreateFormatResourceWriter(parameterName, expectedItemType);
			this.ReplaceScope(ODataParameterWriterCore.ParameterWriterState.ActiveSubWriter);
			return odataWriter;
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x00011750 File Offset: 0x0000F950
		private ODataWriter CreateResourceSetWriterImplementation(string parameterName, IEdmTypeReference expectedItemType)
		{
			ODataWriter odataWriter = this.CreateFormatResourceSetWriter(parameterName, expectedItemType);
			this.ReplaceScope(ODataParameterWriterCore.ParameterWriterState.ActiveSubWriter);
			return odataWriter;
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x0001176E File Offset: 0x0000F96E
		private void VerifyCanWriteEnd(bool synchronousCall)
		{
			this.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
			this.VerifyNotInErrorOrCompletedState();
			if (this.State != ODataParameterWriterCore.ParameterWriterState.CanWriteParameter)
			{
				throw new ODataException(Strings.ODataParameterWriterCore_CannotWriteEnd);
			}
			this.VerifyAllParametersWritten();
		}

		// Token: 0x06000754 RID: 1876 RVA: 0x000117A0 File Offset: 0x0000F9A0
		private void VerifyAllParametersWritten()
		{
			if (this.operation != null && this.operation.Parameters != null)
			{
				IEnumerable<IEdmOperationParameter> enumerable;
				if (this.operation.IsBound)
				{
					enumerable = this.operation.Parameters.Skip(1);
				}
				else
				{
					enumerable = this.operation.Parameters;
				}
				IEnumerable<string> enumerable2 = from p in enumerable
					where !this.parameterNamesWritten.Contains(p.Name) && !this.outputContext.EdmTypeResolver.GetParameterType(p).IsNullable
					select p.Name;
				if (enumerable2.Any<string>())
				{
					enumerable2 = enumerable2.Select((string name) => string.Format(CultureInfo.InvariantCulture, "'{0}'", new object[] { name }));
					throw new ODataException(Strings.ODataParameterWriterCore_MissingParameterInParameterPayload(string.Join(", ", enumerable2.ToArray<string>()), this.operation.Name));
				}
			}
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x00011882 File Offset: 0x0000FA82
		private void WriteEndImplementation()
		{
			this.InterceptException(delegate
			{
				this.EndPayload();
			});
			this.LeaveScope();
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x0001189C File Offset: 0x0000FA9C
		private void VerifyNotInErrorOrCompletedState()
		{
			if (this.State == ODataParameterWriterCore.ParameterWriterState.Error || this.State == ODataParameterWriterCore.ParameterWriterState.Completed)
			{
				throw new ODataException(Strings.ODataParameterWriterCore_CannotWriteInErrorOrCompletedState);
			}
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x000118BB File Offset: 0x0000FABB
		private void VerifyCanFlush(bool synchronousCall)
		{
			this.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x000118CA File Offset: 0x0000FACA
		private void VerifyCallAllowed(bool synchronousCall)
		{
			if (synchronousCall)
			{
				if (!this.outputContext.Synchronous)
				{
					throw new ODataException(Strings.ODataParameterWriterCore_SyncCallOnAsyncWriter);
				}
			}
			else if (this.outputContext.Synchronous)
			{
				throw new ODataException(Strings.ODataParameterWriterCore_AsyncCallOnSyncWriter);
			}
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x00011900 File Offset: 0x0000FB00
		private void InterceptException(Action action)
		{
			try
			{
				action();
			}
			catch
			{
				this.EnterErrorScope();
				throw;
			}
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x00011930 File Offset: 0x0000FB30
		private T InterceptException<T>(Func<T> function)
		{
			T t;
			try
			{
				t = function();
			}
			catch
			{
				this.EnterErrorScope();
				throw;
			}
			return t;
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x00011960 File Offset: 0x0000FB60
		private void EnterErrorScope()
		{
			if (this.State != ODataParameterWriterCore.ParameterWriterState.Error)
			{
				this.EnterScope(ODataParameterWriterCore.ParameterWriterState.Error);
			}
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x00011972 File Offset: 0x0000FB72
		private void EnterScope(ODataParameterWriterCore.ParameterWriterState newState)
		{
			this.ValidateTransition(newState);
			this.scopes.Push(newState);
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x00011987 File Offset: 0x0000FB87
		private void LeaveScope()
		{
			this.ValidateTransition(ODataParameterWriterCore.ParameterWriterState.Completed);
			if (this.State == ODataParameterWriterCore.ParameterWriterState.CanWriteParameter)
			{
				this.scopes.Pop();
			}
			this.ReplaceScope(ODataParameterWriterCore.ParameterWriterState.Completed);
		}

		// Token: 0x0600075E RID: 1886 RVA: 0x000119AC File Offset: 0x0000FBAC
		private void ReplaceScope(ODataParameterWriterCore.ParameterWriterState newState)
		{
			this.ValidateTransition(newState);
			this.scopes.Pop();
			this.scopes.Push(newState);
		}

		// Token: 0x0600075F RID: 1887 RVA: 0x000119D0 File Offset: 0x0000FBD0
		private void ValidateTransition(ODataParameterWriterCore.ParameterWriterState newState)
		{
			if (this.State != ODataParameterWriterCore.ParameterWriterState.Error && newState == ODataParameterWriterCore.ParameterWriterState.Error)
			{
				return;
			}
			switch (this.State)
			{
			case ODataParameterWriterCore.ParameterWriterState.Start:
				if (newState != ODataParameterWriterCore.ParameterWriterState.CanWriteParameter && newState != ODataParameterWriterCore.ParameterWriterState.Completed)
				{
					throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataParameterWriterCore_ValidateTransition_InvalidTransitionFromStart));
				}
				break;
			case ODataParameterWriterCore.ParameterWriterState.CanWriteParameter:
				if (newState != ODataParameterWriterCore.ParameterWriterState.CanWriteParameter && newState != ODataParameterWriterCore.ParameterWriterState.ActiveSubWriter && newState != ODataParameterWriterCore.ParameterWriterState.Completed)
				{
					throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataParameterWriterCore_ValidateTransition_InvalidTransitionFromCanWriteParameter));
				}
				break;
			case ODataParameterWriterCore.ParameterWriterState.ActiveSubWriter:
				if (newState != ODataParameterWriterCore.ParameterWriterState.CanWriteParameter)
				{
					throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataParameterWriterCore_ValidateTransition_InvalidTransitionFromActiveSubWriter));
				}
				break;
			case ODataParameterWriterCore.ParameterWriterState.Completed:
				throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataParameterWriterCore_ValidateTransition_InvalidTransitionFromCompleted));
			case ODataParameterWriterCore.ParameterWriterState.Error:
				if (newState != ODataParameterWriterCore.ParameterWriterState.Error)
				{
					throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataParameterWriterCore_ValidateTransition_InvalidTransitionFromError));
				}
				break;
			default:
				throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataParameterWriterCore_ValidateTransition_UnreachableCodePath));
			}
		}

		// Token: 0x040002C2 RID: 706
		private readonly ODataOutputContext outputContext;

		// Token: 0x040002C3 RID: 707
		private readonly IEdmOperation operation;

		// Token: 0x040002C4 RID: 708
		private Stack<ODataParameterWriterCore.ParameterWriterState> scopes = new Stack<ODataParameterWriterCore.ParameterWriterState>();

		// Token: 0x040002C5 RID: 709
		private HashSet<string> parameterNamesWritten = new HashSet<string>(StringComparer.Ordinal);

		// Token: 0x040002C6 RID: 710
		private IDuplicatePropertyNameChecker duplicatePropertyNameChecker;

		// Token: 0x020002F5 RID: 757
		private enum ParameterWriterState
		{
			// Token: 0x04000D00 RID: 3328
			Start,
			// Token: 0x04000D01 RID: 3329
			CanWriteParameter,
			// Token: 0x04000D02 RID: 3330
			ActiveSubWriter,
			// Token: 0x04000D03 RID: 3331
			Completed,
			// Token: 0x04000D04 RID: 3332
			Error
		}
	}
}
