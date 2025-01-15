using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData
{
	// Token: 0x02000198 RID: 408
	internal abstract class ODataParameterWriterCore : ODataParameterWriter, IODataReaderWriterListener, IODataOutputInStreamErrorListener
	{
		// Token: 0x06000BA2 RID: 2978 RVA: 0x00028AA8 File Offset: 0x00026CA8
		protected ODataParameterWriterCore(ODataOutputContext outputContext, IEdmFunctionImport functionImport)
		{
			this.outputContext = outputContext;
			this.functionImport = functionImport;
			this.scopes.Push(ODataParameterWriterCore.ParameterWriterState.Start);
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06000BA3 RID: 2979 RVA: 0x00028AE8 File Offset: 0x00026CE8
		protected DuplicatePropertyNamesChecker DuplicatePropertyNamesChecker
		{
			get
			{
				DuplicatePropertyNamesChecker duplicatePropertyNamesChecker;
				if ((duplicatePropertyNamesChecker = this.duplicatePropertyNamesChecker) == null)
				{
					duplicatePropertyNamesChecker = (this.duplicatePropertyNamesChecker = new DuplicatePropertyNamesChecker(false, false));
				}
				return duplicatePropertyNamesChecker;
			}
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06000BA4 RID: 2980 RVA: 0x00028B0F File Offset: 0x00026D0F
		private ODataParameterWriterCore.ParameterWriterState State
		{
			get
			{
				return this.scopes.Peek();
			}
		}

		// Token: 0x06000BA5 RID: 2981 RVA: 0x00028B1C File Offset: 0x00026D1C
		public sealed override void Flush()
		{
			this.VerifyCanFlush(true);
			this.InterceptException(new Action(this.FlushSynchronously));
		}

		// Token: 0x06000BA6 RID: 2982 RVA: 0x00028B40 File Offset: 0x00026D40
		public sealed override void WriteStart()
		{
			this.VerifyCanWriteStart(true);
			this.InterceptException(delegate
			{
				this.WriteStartImplementation();
			});
		}

		// Token: 0x06000BA7 RID: 2983 RVA: 0x00028B84 File Offset: 0x00026D84
		public sealed override void WriteValue(string parameterName, object parameterValue)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(parameterName, "parameterName");
			IEdmTypeReference expectedTypeReference = this.VerifyCanWriteValueParameter(true, parameterName, parameterValue);
			this.InterceptException(delegate
			{
				this.WriteValueImplementation(parameterName, parameterValue, expectedTypeReference);
			});
		}

		// Token: 0x06000BA8 RID: 2984 RVA: 0x00028C08 File Offset: 0x00026E08
		public sealed override ODataCollectionWriter CreateCollectionWriter(string parameterName)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(parameterName, "parameterName");
			IEdmTypeReference itemTypeReference = this.VerifyCanCreateCollectionWriter(true, parameterName);
			return this.InterceptException<ODataCollectionWriter>(() => this.CreateCollectionWriterImplementation(parameterName, itemTypeReference));
		}

		// Token: 0x06000BA9 RID: 2985 RVA: 0x00028C66 File Offset: 0x00026E66
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

		// Token: 0x06000BAA RID: 2986 RVA: 0x00028C90 File Offset: 0x00026E90
		void IODataReaderWriterListener.OnException()
		{
			this.ReplaceScope(ODataParameterWriterCore.ParameterWriterState.Error);
		}

		// Token: 0x06000BAB RID: 2987 RVA: 0x00028C99 File Offset: 0x00026E99
		void IODataReaderWriterListener.OnCompleted()
		{
			this.ReplaceScope(ODataParameterWriterCore.ParameterWriterState.CanWriteParameter);
		}

		// Token: 0x06000BAC RID: 2988 RVA: 0x00028CA2 File Offset: 0x00026EA2
		void IODataOutputInStreamErrorListener.OnInStreamError()
		{
			throw new ODataException(Strings.ODataParameterWriter_InStreamErrorNotSupported);
		}

		// Token: 0x06000BAD RID: 2989
		protected abstract void VerifyNotDisposed();

		// Token: 0x06000BAE RID: 2990
		protected abstract void FlushSynchronously();

		// Token: 0x06000BAF RID: 2991
		protected abstract void StartPayload();

		// Token: 0x06000BB0 RID: 2992
		protected abstract void WriteValueParameter(string parameterName, object parameterValue, IEdmTypeReference expectedTypeReference);

		// Token: 0x06000BB1 RID: 2993
		protected abstract ODataCollectionWriter CreateFormatCollectionWriter(string parameterName, IEdmTypeReference expectedItemType);

		// Token: 0x06000BB2 RID: 2994
		protected abstract void EndPayload();

		// Token: 0x06000BB3 RID: 2995 RVA: 0x00028CAE File Offset: 0x00026EAE
		private void VerifyCanWriteStart(bool synchronousCall)
		{
			this.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
			if (this.State != ODataParameterWriterCore.ParameterWriterState.Start)
			{
				throw new ODataException(Strings.ODataParameterWriterCore_CannotWriteStart);
			}
		}

		// Token: 0x06000BB4 RID: 2996 RVA: 0x00028CD0 File Offset: 0x00026ED0
		private void WriteStartImplementation()
		{
			this.InterceptException(new Action(this.StartPayload));
			this.EnterScope(ODataParameterWriterCore.ParameterWriterState.CanWriteParameter);
		}

		// Token: 0x06000BB5 RID: 2997 RVA: 0x00028CEC File Offset: 0x00026EEC
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

		// Token: 0x06000BB6 RID: 2998 RVA: 0x00028D50 File Offset: 0x00026F50
		private IEdmTypeReference VerifyCanWriteValueParameter(bool synchronousCall, string parameterName, object parameterValue)
		{
			IEdmTypeReference edmTypeReference = this.VerifyCanWriteParameterAndGetTypeReference(synchronousCall, parameterName);
			if (edmTypeReference != null && !edmTypeReference.IsODataPrimitiveTypeKind() && !edmTypeReference.IsODataComplexTypeKind())
			{
				throw new ODataException(Strings.ODataParameterWriterCore_CannotWriteValueOnNonValueTypeKind(parameterName, edmTypeReference.TypeKind()));
			}
			if (parameterValue != null && (!EdmLibraryExtensions.IsPrimitiveType(parameterValue.GetType()) || parameterValue is Stream) && !(parameterValue is ODataComplexValue))
			{
				throw new ODataException(Strings.ODataParameterWriterCore_CannotWriteValueOnNonSupportedValueType(parameterName, parameterValue.GetType()));
			}
			return edmTypeReference;
		}

		// Token: 0x06000BB7 RID: 2999 RVA: 0x00028DC4 File Offset: 0x00026FC4
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

		// Token: 0x06000BB8 RID: 3000 RVA: 0x00028E08 File Offset: 0x00027008
		private IEdmTypeReference GetParameterTypeReference(string parameterName)
		{
			if (this.functionImport == null)
			{
				return null;
			}
			IEdmFunctionParameter edmFunctionParameter = this.functionImport.FindParameter(parameterName);
			if (edmFunctionParameter == null)
			{
				throw new ODataException(Strings.ODataParameterWriterCore_ParameterNameNotFoundInFunctionImport(parameterName, this.functionImport.Name));
			}
			return this.outputContext.EdmTypeResolver.GetParameterType(edmFunctionParameter);
		}

		// Token: 0x06000BB9 RID: 3001 RVA: 0x00028E80 File Offset: 0x00027080
		private void WriteValueImplementation(string parameterName, object parameterValue, IEdmTypeReference expectedTypeReference)
		{
			this.InterceptException(delegate
			{
				this.WriteValueParameter(parameterName, parameterValue, expectedTypeReference);
			});
		}

		// Token: 0x06000BBA RID: 3002 RVA: 0x00028EC4 File Offset: 0x000270C4
		private ODataCollectionWriter CreateCollectionWriterImplementation(string parameterName, IEdmTypeReference expectedItemType)
		{
			ODataCollectionWriter odataCollectionWriter = this.CreateFormatCollectionWriter(parameterName, expectedItemType);
			this.ReplaceScope(ODataParameterWriterCore.ParameterWriterState.ActiveSubWriter);
			return odataCollectionWriter;
		}

		// Token: 0x06000BBB RID: 3003 RVA: 0x00028EE2 File Offset: 0x000270E2
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

		// Token: 0x06000BBC RID: 3004 RVA: 0x00028F74 File Offset: 0x00027174
		private void VerifyAllParametersWritten()
		{
			if (this.functionImport != null && this.functionImport.Parameters != null)
			{
				IEnumerable<IEdmFunctionParameter> enumerable;
				if (this.functionImport.IsBindable)
				{
					enumerable = Enumerable.Skip<IEdmFunctionParameter>(this.functionImport.Parameters, 1);
				}
				else
				{
					enumerable = this.functionImport.Parameters;
				}
				IEnumerable<string> enumerable2 = Enumerable.Select<IEdmFunctionParameter, string>(Enumerable.Where<IEdmFunctionParameter>(enumerable, (IEdmFunctionParameter p) => !this.parameterNamesWritten.Contains(p.Name) && !this.outputContext.EdmTypeResolver.GetParameterType(p).IsNullable), (IEdmFunctionParameter p) => p.Name);
				if (Enumerable.Any<string>(enumerable2))
				{
					enumerable2 = Enumerable.Select<string, string>(enumerable2, (string name) => string.Format(CultureInfo.InvariantCulture, "'{0}'", new object[] { name }));
					throw new ODataException(Strings.ODataParameterWriterCore_MissingParameterInParameterPayload(string.Join(", ", Enumerable.ToArray<string>(enumerable2)), this.functionImport.Name));
				}
			}
		}

		// Token: 0x06000BBD RID: 3005 RVA: 0x00029061 File Offset: 0x00027261
		private void WriteEndImplementation()
		{
			this.InterceptException(delegate
			{
				this.EndPayload();
			});
			this.LeaveScope();
		}

		// Token: 0x06000BBE RID: 3006 RVA: 0x0002907B File Offset: 0x0002727B
		private void VerifyNotInErrorOrCompletedState()
		{
			if (this.State == ODataParameterWriterCore.ParameterWriterState.Error || this.State == ODataParameterWriterCore.ParameterWriterState.Completed)
			{
				throw new ODataException(Strings.ODataParameterWriterCore_CannotWriteInErrorOrCompletedState);
			}
		}

		// Token: 0x06000BBF RID: 3007 RVA: 0x0002909A File Offset: 0x0002729A
		private void VerifyCanFlush(bool synchronousCall)
		{
			this.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
		}

		// Token: 0x06000BC0 RID: 3008 RVA: 0x000290A9 File Offset: 0x000272A9
		private void VerifyCallAllowed(bool synchronousCall)
		{
			if (synchronousCall && !this.outputContext.Synchronous)
			{
				throw new ODataException(Strings.ODataParameterWriterCore_SyncCallOnAsyncWriter);
			}
		}

		// Token: 0x06000BC1 RID: 3009 RVA: 0x000290C8 File Offset: 0x000272C8
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

		// Token: 0x06000BC2 RID: 3010 RVA: 0x000290F8 File Offset: 0x000272F8
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

		// Token: 0x06000BC3 RID: 3011 RVA: 0x00029128 File Offset: 0x00027328
		private void EnterErrorScope()
		{
			if (this.State != ODataParameterWriterCore.ParameterWriterState.Error)
			{
				this.EnterScope(ODataParameterWriterCore.ParameterWriterState.Error);
			}
		}

		// Token: 0x06000BC4 RID: 3012 RVA: 0x0002913A File Offset: 0x0002733A
		private void EnterScope(ODataParameterWriterCore.ParameterWriterState newState)
		{
			this.ValidateTransition(newState);
			this.scopes.Push(newState);
		}

		// Token: 0x06000BC5 RID: 3013 RVA: 0x0002914F File Offset: 0x0002734F
		private void LeaveScope()
		{
			this.ValidateTransition(ODataParameterWriterCore.ParameterWriterState.Completed);
			if (this.State == ODataParameterWriterCore.ParameterWriterState.CanWriteParameter)
			{
				this.scopes.Pop();
			}
			this.ReplaceScope(ODataParameterWriterCore.ParameterWriterState.Completed);
		}

		// Token: 0x06000BC6 RID: 3014 RVA: 0x00029174 File Offset: 0x00027374
		private void ReplaceScope(ODataParameterWriterCore.ParameterWriterState newState)
		{
			this.ValidateTransition(newState);
			this.scopes.Pop();
			this.scopes.Push(newState);
		}

		// Token: 0x06000BC7 RID: 3015 RVA: 0x00029198 File Offset: 0x00027398
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

		// Token: 0x04000431 RID: 1073
		private readonly ODataOutputContext outputContext;

		// Token: 0x04000432 RID: 1074
		private readonly IEdmFunctionImport functionImport;

		// Token: 0x04000433 RID: 1075
		private Stack<ODataParameterWriterCore.ParameterWriterState> scopes = new Stack<ODataParameterWriterCore.ParameterWriterState>();

		// Token: 0x04000434 RID: 1076
		private HashSet<string> parameterNamesWritten = new HashSet<string>(StringComparer.Ordinal);

		// Token: 0x04000435 RID: 1077
		private DuplicatePropertyNamesChecker duplicatePropertyNamesChecker;

		// Token: 0x02000199 RID: 409
		private enum ParameterWriterState
		{
			// Token: 0x04000439 RID: 1081
			Start,
			// Token: 0x0400043A RID: 1082
			CanWriteParameter,
			// Token: 0x0400043B RID: 1083
			ActiveSubWriter,
			// Token: 0x0400043C RID: 1084
			Completed,
			// Token: 0x0400043D RID: 1085
			Error
		}
	}
}
