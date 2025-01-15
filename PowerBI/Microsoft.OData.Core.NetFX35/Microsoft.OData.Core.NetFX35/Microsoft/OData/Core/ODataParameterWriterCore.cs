using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Microsoft.OData.Core.Metadata;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core
{
	// Token: 0x020000EE RID: 238
	internal abstract class ODataParameterWriterCore : ODataParameterWriter, IODataReaderWriterListener, IODataOutputInStreamErrorListener
	{
		// Token: 0x06000901 RID: 2305 RVA: 0x00020CEC File Offset: 0x0001EEEC
		protected ODataParameterWriterCore(ODataOutputContext outputContext, IEdmOperation operation)
		{
			this.outputContext = outputContext;
			this.operation = operation;
			this.scopes.Push(ODataParameterWriterCore.ParameterWriterState.Start);
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06000902 RID: 2306 RVA: 0x00020D2C File Offset: 0x0001EF2C
		protected DuplicatePropertyNamesChecker DuplicatePropertyNamesChecker
		{
			get
			{
				DuplicatePropertyNamesChecker duplicatePropertyNamesChecker;
				if ((duplicatePropertyNamesChecker = this.duplicatePropertyNamesChecker) == null)
				{
					duplicatePropertyNamesChecker = (this.duplicatePropertyNamesChecker = new DuplicatePropertyNamesChecker(false, false, !this.outputContext.MessageWriterSettings.EnableFullValidation));
				}
				return duplicatePropertyNamesChecker;
			}
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000903 RID: 2307 RVA: 0x00020D66 File Offset: 0x0001EF66
		private ODataParameterWriterCore.ParameterWriterState State
		{
			get
			{
				return this.scopes.Peek();
			}
		}

		// Token: 0x06000904 RID: 2308 RVA: 0x00020D73 File Offset: 0x0001EF73
		public sealed override void Flush()
		{
			this.VerifyCanFlush(true);
			this.InterceptException(new Action(this.FlushSynchronously));
		}

		// Token: 0x06000905 RID: 2309 RVA: 0x00020D97 File Offset: 0x0001EF97
		public sealed override void WriteStart()
		{
			this.VerifyCanWriteStart(true);
			this.InterceptException(delegate
			{
				this.WriteStartImplementation();
			});
		}

		// Token: 0x06000906 RID: 2310 RVA: 0x00020DDC File Offset: 0x0001EFDC
		public sealed override void WriteValue(string parameterName, object parameterValue)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(parameterName, "parameterName");
			IEdmTypeReference expectedTypeReference = this.VerifyCanWriteValueParameter(true, parameterName, parameterValue);
			this.InterceptException(delegate
			{
				this.WriteValueImplementation(parameterName, parameterValue, expectedTypeReference);
			});
		}

		// Token: 0x06000907 RID: 2311 RVA: 0x00020E60 File Offset: 0x0001F060
		public sealed override ODataCollectionWriter CreateCollectionWriter(string parameterName)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(parameterName, "parameterName");
			IEdmTypeReference itemTypeReference = this.VerifyCanCreateCollectionWriter(true, parameterName);
			return this.InterceptException<ODataCollectionWriter>(() => this.CreateCollectionWriterImplementation(parameterName, itemTypeReference));
		}

		// Token: 0x06000908 RID: 2312 RVA: 0x00020ED8 File Offset: 0x0001F0D8
		public sealed override ODataWriter CreateEntryWriter(string parameterName)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(parameterName, "parameterName");
			IEdmTypeReference itemTypeReference = this.VerifyCanCreateEntryWriter(true, parameterName);
			return this.InterceptException<ODataWriter>(() => this.CreateEntryWriterImplementation(parameterName, itemTypeReference));
		}

		// Token: 0x06000909 RID: 2313 RVA: 0x00020F50 File Offset: 0x0001F150
		public sealed override ODataWriter CreateFeedWriter(string parameterName)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(parameterName, "parameterName");
			IEdmTypeReference itemTypeReference = this.VerifyCanCreateFeedWriter(true, parameterName);
			return this.InterceptException<ODataWriter>(() => this.CreateFeedWriterImplementation(parameterName, itemTypeReference));
		}

		// Token: 0x0600090A RID: 2314 RVA: 0x00020FAE File Offset: 0x0001F1AE
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

		// Token: 0x0600090B RID: 2315 RVA: 0x00020FD8 File Offset: 0x0001F1D8
		void IODataReaderWriterListener.OnException()
		{
			this.ReplaceScope(ODataParameterWriterCore.ParameterWriterState.Error);
		}

		// Token: 0x0600090C RID: 2316 RVA: 0x00020FE1 File Offset: 0x0001F1E1
		void IODataReaderWriterListener.OnCompleted()
		{
			this.ReplaceScope(ODataParameterWriterCore.ParameterWriterState.CanWriteParameter);
		}

		// Token: 0x0600090D RID: 2317 RVA: 0x00020FEA File Offset: 0x0001F1EA
		void IODataOutputInStreamErrorListener.OnInStreamError()
		{
			throw new ODataException(Strings.ODataParameterWriter_InStreamErrorNotSupported);
		}

		// Token: 0x0600090E RID: 2318
		protected abstract void VerifyNotDisposed();

		// Token: 0x0600090F RID: 2319
		protected abstract void FlushSynchronously();

		// Token: 0x06000910 RID: 2320
		protected abstract void StartPayload();

		// Token: 0x06000911 RID: 2321
		protected abstract void WriteValueParameter(string parameterName, object parameterValue, IEdmTypeReference expectedTypeReference);

		// Token: 0x06000912 RID: 2322
		protected abstract ODataCollectionWriter CreateFormatCollectionWriter(string parameterName, IEdmTypeReference expectedItemType);

		// Token: 0x06000913 RID: 2323
		protected abstract ODataWriter CreateFormatEntryWriter(string parameterName, IEdmTypeReference expectedItemType);

		// Token: 0x06000914 RID: 2324
		protected abstract ODataWriter CreateFormatFeedWriter(string parameterName, IEdmTypeReference expectedItemType);

		// Token: 0x06000915 RID: 2325
		protected abstract void EndPayload();

		// Token: 0x06000916 RID: 2326 RVA: 0x00020FF6 File Offset: 0x0001F1F6
		private void VerifyCanWriteStart(bool synchronousCall)
		{
			this.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
			if (this.State != ODataParameterWriterCore.ParameterWriterState.Start)
			{
				throw new ODataException(Strings.ODataParameterWriterCore_CannotWriteStart);
			}
		}

		// Token: 0x06000917 RID: 2327 RVA: 0x00021018 File Offset: 0x0001F218
		private void WriteStartImplementation()
		{
			this.InterceptException(new Action(this.StartPayload));
			this.EnterScope(ODataParameterWriterCore.ParameterWriterState.CanWriteParameter);
		}

		// Token: 0x06000918 RID: 2328 RVA: 0x00021034 File Offset: 0x0001F234
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

		// Token: 0x06000919 RID: 2329 RVA: 0x00021098 File Offset: 0x0001F298
		private IEdmTypeReference VerifyCanWriteValueParameter(bool synchronousCall, string parameterName, object parameterValue)
		{
			IEdmTypeReference edmTypeReference = this.VerifyCanWriteParameterAndGetTypeReference(synchronousCall, parameterName);
			if (edmTypeReference != null && !edmTypeReference.IsODataPrimitiveTypeKind() && !edmTypeReference.IsODataComplexTypeKind() && !edmTypeReference.IsODataEnumTypeKind() && !edmTypeReference.IsODataTypeDefinitionTypeKind())
			{
				throw new ODataException(Strings.ODataParameterWriterCore_CannotWriteValueOnNonValueTypeKind(parameterName, edmTypeReference.TypeKind()));
			}
			if (parameterValue != null && (!EdmLibraryExtensions.IsPrimitiveType(parameterValue.GetType()) || parameterValue is Stream) && !(parameterValue is ODataComplexValue) && !(parameterValue is ODataEnumValue))
			{
				throw new ODataException(Strings.ODataParameterWriterCore_CannotWriteValueOnNonSupportedValueType(parameterName, parameterValue.GetType()));
			}
			return edmTypeReference;
		}

		// Token: 0x0600091A RID: 2330 RVA: 0x00021124 File Offset: 0x0001F324
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

		// Token: 0x0600091B RID: 2331 RVA: 0x00021168 File Offset: 0x0001F368
		private IEdmTypeReference VerifyCanCreateEntryWriter(bool synchronousCall, string parameterName)
		{
			IEdmTypeReference edmTypeReference = this.VerifyCanWriteParameterAndGetTypeReference(synchronousCall, parameterName);
			if (edmTypeReference != null && !edmTypeReference.IsODataEntityTypeKind())
			{
				throw new ODataException(string.Format(CultureInfo.InvariantCulture, "The parameter '{0}' is of Edm type kind '{1}'. You cannot call CreateEntryWriter on a parameter that is not of Edm type kind 'Entity'.", new object[]
				{
					parameterName,
					edmTypeReference.TypeKind()
				}));
			}
			return edmTypeReference;
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x000211BC File Offset: 0x0001F3BC
		private IEdmTypeReference VerifyCanCreateFeedWriter(bool synchronousCall, string parameterName)
		{
			IEdmTypeReference edmTypeReference = this.VerifyCanWriteParameterAndGetTypeReference(synchronousCall, parameterName);
			if (edmTypeReference != null && !edmTypeReference.IsEntityCollectionType())
			{
				throw new ODataException(string.Format(CultureInfo.InvariantCulture, "The parameter '{0}' is of Edm type kind '{1}'. You cannot call CreateFeedWriter on a parameter that is not of Edm type kind 'Collection(Entity)'.", new object[]
				{
					parameterName,
					edmTypeReference.TypeKind()
				}));
			}
			return edmTypeReference;
		}

		// Token: 0x0600091D RID: 2333 RVA: 0x00021210 File Offset: 0x0001F410
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

		// Token: 0x0600091E RID: 2334 RVA: 0x00021288 File Offset: 0x0001F488
		private void WriteValueImplementation(string parameterName, object parameterValue, IEdmTypeReference expectedTypeReference)
		{
			this.InterceptException(delegate
			{
				this.WriteValueParameter(parameterName, parameterValue, expectedTypeReference);
			});
		}

		// Token: 0x0600091F RID: 2335 RVA: 0x000212CC File Offset: 0x0001F4CC
		private ODataCollectionWriter CreateCollectionWriterImplementation(string parameterName, IEdmTypeReference expectedItemType)
		{
			ODataCollectionWriter odataCollectionWriter = this.CreateFormatCollectionWriter(parameterName, expectedItemType);
			this.ReplaceScope(ODataParameterWriterCore.ParameterWriterState.ActiveSubWriter);
			return odataCollectionWriter;
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x000212EC File Offset: 0x0001F4EC
		private ODataWriter CreateEntryWriterImplementation(string parameterName, IEdmTypeReference expectedItemType)
		{
			ODataWriter odataWriter = this.CreateFormatEntryWriter(parameterName, expectedItemType);
			this.ReplaceScope(ODataParameterWriterCore.ParameterWriterState.ActiveSubWriter);
			return odataWriter;
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x0002130C File Offset: 0x0001F50C
		private ODataWriter CreateFeedWriterImplementation(string parameterName, IEdmTypeReference expectedItemType)
		{
			ODataWriter odataWriter = this.CreateFormatFeedWriter(parameterName, expectedItemType);
			this.ReplaceScope(ODataParameterWriterCore.ParameterWriterState.ActiveSubWriter);
			return odataWriter;
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x0002132A File Offset: 0x0001F52A
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

		// Token: 0x06000923 RID: 2339 RVA: 0x000213BC File Offset: 0x0001F5BC
		private void VerifyAllParametersWritten()
		{
			if (this.operation != null && this.operation.Parameters != null)
			{
				IEnumerable<IEdmOperationParameter> enumerable;
				if (this.operation.IsBound)
				{
					enumerable = Enumerable.Skip<IEdmOperationParameter>(this.operation.Parameters, 1);
				}
				else
				{
					enumerable = this.operation.Parameters;
				}
				IEnumerable<string> enumerable2 = Enumerable.Select<IEdmOperationParameter, string>(Enumerable.Where<IEdmOperationParameter>(enumerable, (IEdmOperationParameter p) => !this.parameterNamesWritten.Contains(p.Name) && !this.outputContext.EdmTypeResolver.GetParameterType(p).IsNullable), (IEdmOperationParameter p) => p.Name);
				if (Enumerable.Any<string>(enumerable2))
				{
					enumerable2 = Enumerable.Select<string, string>(enumerable2, (string name) => string.Format(CultureInfo.InvariantCulture, "'{0}'", new object[] { name }));
					throw new ODataException(Strings.ODataParameterWriterCore_MissingParameterInParameterPayload(string.Join(", ", Enumerable.ToArray<string>(enumerable2)), this.operation.Name));
				}
			}
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x000214A9 File Offset: 0x0001F6A9
		private void WriteEndImplementation()
		{
			this.InterceptException(delegate
			{
				this.EndPayload();
			});
			this.LeaveScope();
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x000214C3 File Offset: 0x0001F6C3
		private void VerifyNotInErrorOrCompletedState()
		{
			if (this.State == ODataParameterWriterCore.ParameterWriterState.Error || this.State == ODataParameterWriterCore.ParameterWriterState.Completed)
			{
				throw new ODataException(Strings.ODataParameterWriterCore_CannotWriteInErrorOrCompletedState);
			}
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x000214E2 File Offset: 0x0001F6E2
		private void VerifyCanFlush(bool synchronousCall)
		{
			this.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
		}

		// Token: 0x06000927 RID: 2343 RVA: 0x000214F1 File Offset: 0x0001F6F1
		private void VerifyCallAllowed(bool synchronousCall)
		{
			if (synchronousCall && !this.outputContext.Synchronous)
			{
				throw new ODataException(Strings.ODataParameterWriterCore_SyncCallOnAsyncWriter);
			}
		}

		// Token: 0x06000928 RID: 2344 RVA: 0x00021510 File Offset: 0x0001F710
		private void InterceptException(Action action)
		{
			try
			{
				action.Invoke();
			}
			catch
			{
				this.EnterErrorScope();
				throw;
			}
		}

		// Token: 0x06000929 RID: 2345 RVA: 0x00021540 File Offset: 0x0001F740
		private T InterceptException<T>(Func<T> function)
		{
			T t;
			try
			{
				t = function.Invoke();
			}
			catch
			{
				this.EnterErrorScope();
				throw;
			}
			return t;
		}

		// Token: 0x0600092A RID: 2346 RVA: 0x00021570 File Offset: 0x0001F770
		private void EnterErrorScope()
		{
			if (this.State != ODataParameterWriterCore.ParameterWriterState.Error)
			{
				this.EnterScope(ODataParameterWriterCore.ParameterWriterState.Error);
			}
		}

		// Token: 0x0600092B RID: 2347 RVA: 0x00021582 File Offset: 0x0001F782
		private void EnterScope(ODataParameterWriterCore.ParameterWriterState newState)
		{
			this.ValidateTransition(newState);
			this.scopes.Push(newState);
		}

		// Token: 0x0600092C RID: 2348 RVA: 0x00021597 File Offset: 0x0001F797
		private void LeaveScope()
		{
			this.ValidateTransition(ODataParameterWriterCore.ParameterWriterState.Completed);
			if (this.State == ODataParameterWriterCore.ParameterWriterState.CanWriteParameter)
			{
				this.scopes.Pop();
			}
			this.ReplaceScope(ODataParameterWriterCore.ParameterWriterState.Completed);
		}

		// Token: 0x0600092D RID: 2349 RVA: 0x000215BC File Offset: 0x0001F7BC
		private void ReplaceScope(ODataParameterWriterCore.ParameterWriterState newState)
		{
			this.ValidateTransition(newState);
			this.scopes.Pop();
			this.scopes.Push(newState);
		}

		// Token: 0x0600092E RID: 2350 RVA: 0x000215E0 File Offset: 0x0001F7E0
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

		// Token: 0x040003A4 RID: 932
		private readonly ODataOutputContext outputContext;

		// Token: 0x040003A5 RID: 933
		private readonly IEdmOperation operation;

		// Token: 0x040003A6 RID: 934
		private Stack<ODataParameterWriterCore.ParameterWriterState> scopes = new Stack<ODataParameterWriterCore.ParameterWriterState>();

		// Token: 0x040003A7 RID: 935
		private HashSet<string> parameterNamesWritten = new HashSet<string>(StringComparer.Ordinal);

		// Token: 0x040003A8 RID: 936
		private DuplicatePropertyNamesChecker duplicatePropertyNamesChecker;

		// Token: 0x020000EF RID: 239
		private enum ParameterWriterState
		{
			// Token: 0x040003AC RID: 940
			Start,
			// Token: 0x040003AD RID: 941
			CanWriteParameter,
			// Token: 0x040003AE RID: 942
			ActiveSubWriter,
			// Token: 0x040003AF RID: 943
			Completed,
			// Token: 0x040003B0 RID: 944
			Error
		}
	}
}
