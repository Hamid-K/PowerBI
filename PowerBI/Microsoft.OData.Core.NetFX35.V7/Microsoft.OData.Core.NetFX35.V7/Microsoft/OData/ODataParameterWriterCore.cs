using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Microsoft.OData.Edm;
using Microsoft.OData.Metadata;

namespace Microsoft.OData
{
	// Token: 0x02000082 RID: 130
	internal abstract class ODataParameterWriterCore : ODataParameterWriter, IODataReaderWriterListener, IODataOutputInStreamErrorListener
	{
		// Token: 0x060004F8 RID: 1272 RVA: 0x0000DB7C File Offset: 0x0000BD7C
		protected ODataParameterWriterCore(ODataOutputContext outputContext, IEdmOperation operation)
		{
			this.outputContext = outputContext;
			this.operation = operation;
			this.scopes.Push(ODataParameterWriterCore.ParameterWriterState.Start);
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x060004F9 RID: 1273 RVA: 0x0000DBBC File Offset: 0x0000BDBC
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

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x060004FA RID: 1274 RVA: 0x0000DBF1 File Offset: 0x0000BDF1
		private ODataParameterWriterCore.ParameterWriterState State
		{
			get
			{
				return this.scopes.Peek();
			}
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x0000DBFE File Offset: 0x0000BDFE
		public sealed override void Flush()
		{
			this.VerifyCanFlush(true);
			this.InterceptException(new Action(this.FlushSynchronously));
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x0000DC1A File Offset: 0x0000BE1A
		public sealed override void WriteStart()
		{
			this.VerifyCanWriteStart(true);
			this.InterceptException(delegate
			{
				this.WriteStartImplementation();
			});
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x0000DC38 File Offset: 0x0000BE38
		public sealed override void WriteValue(string parameterName, object parameterValue)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(parameterName, "parameterName");
			IEdmTypeReference expectedTypeReference = this.VerifyCanWriteValueParameter(true, parameterName, parameterValue);
			this.InterceptException(delegate
			{
				this.WriteValueImplementation(parameterName, parameterValue, expectedTypeReference);
			});
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x0000DC9C File Offset: 0x0000BE9C
		public sealed override ODataCollectionWriter CreateCollectionWriter(string parameterName)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(parameterName, "parameterName");
			IEdmTypeReference itemTypeReference = this.VerifyCanCreateCollectionWriter(true, parameterName);
			return this.InterceptException<ODataCollectionWriter>(() => this.CreateCollectionWriterImplementation(parameterName, itemTypeReference));
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x0000DCF4 File Offset: 0x0000BEF4
		public sealed override ODataWriter CreateResourceWriter(string parameterName)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(parameterName, "parameterName");
			IEdmTypeReference itemTypeReference = this.VerifyCanCreateResourceWriter(true, parameterName);
			return this.InterceptException<ODataWriter>(() => this.CreateResourceWriterImplementation(parameterName, itemTypeReference));
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x0000DD4C File Offset: 0x0000BF4C
		public sealed override ODataWriter CreateResourceSetWriter(string parameterName)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(parameterName, "parameterName");
			IEdmTypeReference itemTypeReference = this.VerifyCanCreateResourceSetWriter(true, parameterName);
			return this.InterceptException<ODataWriter>(() => this.CreateResourceSetWriterImplementation(parameterName, itemTypeReference));
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x0000DDA2 File Offset: 0x0000BFA2
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

		// Token: 0x06000502 RID: 1282 RVA: 0x0000DDCC File Offset: 0x0000BFCC
		void IODataReaderWriterListener.OnException()
		{
			this.ReplaceScope(ODataParameterWriterCore.ParameterWriterState.Error);
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x0000DDD5 File Offset: 0x0000BFD5
		void IODataReaderWriterListener.OnCompleted()
		{
			this.ReplaceScope(ODataParameterWriterCore.ParameterWriterState.CanWriteParameter);
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x0000DDDE File Offset: 0x0000BFDE
		void IODataOutputInStreamErrorListener.OnInStreamError()
		{
			throw new ODataException(Strings.ODataParameterWriter_InStreamErrorNotSupported);
		}

		// Token: 0x06000505 RID: 1285
		protected abstract void VerifyNotDisposed();

		// Token: 0x06000506 RID: 1286
		protected abstract void FlushSynchronously();

		// Token: 0x06000507 RID: 1287
		protected abstract void StartPayload();

		// Token: 0x06000508 RID: 1288
		protected abstract void WriteValueParameter(string parameterName, object parameterValue, IEdmTypeReference expectedTypeReference);

		// Token: 0x06000509 RID: 1289
		protected abstract ODataCollectionWriter CreateFormatCollectionWriter(string parameterName, IEdmTypeReference expectedItemType);

		// Token: 0x0600050A RID: 1290
		protected abstract ODataWriter CreateFormatResourceWriter(string parameterName, IEdmTypeReference expectedItemType);

		// Token: 0x0600050B RID: 1291
		protected abstract ODataWriter CreateFormatResourceSetWriter(string parameterName, IEdmTypeReference expectedItemType);

		// Token: 0x0600050C RID: 1292
		protected abstract void EndPayload();

		// Token: 0x0600050D RID: 1293 RVA: 0x0000DDEA File Offset: 0x0000BFEA
		private void VerifyCanWriteStart(bool synchronousCall)
		{
			this.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
			if (this.State != ODataParameterWriterCore.ParameterWriterState.Start)
			{
				throw new ODataException(Strings.ODataParameterWriterCore_CannotWriteStart);
			}
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x0000DE0C File Offset: 0x0000C00C
		private void WriteStartImplementation()
		{
			this.InterceptException(new Action(this.StartPayload));
			this.EnterScope(ODataParameterWriterCore.ParameterWriterState.CanWriteParameter);
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x0000DE28 File Offset: 0x0000C028
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

		// Token: 0x06000510 RID: 1296 RVA: 0x0000DE8C File Offset: 0x0000C08C
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

		// Token: 0x06000511 RID: 1297 RVA: 0x0000DF08 File Offset: 0x0000C108
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

		// Token: 0x06000512 RID: 1298 RVA: 0x0000DF4C File Offset: 0x0000C14C
		private IEdmTypeReference VerifyCanCreateResourceWriter(bool synchronousCall, string parameterName)
		{
			IEdmTypeReference edmTypeReference = this.VerifyCanWriteParameterAndGetTypeReference(synchronousCall, parameterName);
			if (edmTypeReference != null && !edmTypeReference.IsStructured())
			{
				throw new ODataException(Strings.ODataParameterWriterCore_CannotCreateResourceWriterOnNonEntityOrComplexTypeKind(parameterName, edmTypeReference.TypeKind()));
			}
			return edmTypeReference;
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x0000DF88 File Offset: 0x0000C188
		private IEdmTypeReference VerifyCanCreateResourceSetWriter(bool synchronousCall, string parameterName)
		{
			IEdmTypeReference edmTypeReference = this.VerifyCanWriteParameterAndGetTypeReference(synchronousCall, parameterName);
			if (edmTypeReference != null && !edmTypeReference.IsStructuredCollectionType())
			{
				throw new ODataException(Strings.ODataParameterWriterCore_CannotCreateResourceSetWriterOnNonStructuredCollectionTypeKind(parameterName, edmTypeReference.TypeKind()));
			}
			return edmTypeReference;
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x0000DFC4 File Offset: 0x0000C1C4
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

		// Token: 0x06000515 RID: 1301 RVA: 0x0000E014 File Offset: 0x0000C214
		private void WriteValueImplementation(string parameterName, object parameterValue, IEdmTypeReference expectedTypeReference)
		{
			this.InterceptException(delegate
			{
				this.WriteValueParameter(parameterName, parameterValue, expectedTypeReference);
			});
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x0000E058 File Offset: 0x0000C258
		private ODataCollectionWriter CreateCollectionWriterImplementation(string parameterName, IEdmTypeReference expectedItemType)
		{
			ODataCollectionWriter odataCollectionWriter = this.CreateFormatCollectionWriter(parameterName, expectedItemType);
			this.ReplaceScope(ODataParameterWriterCore.ParameterWriterState.ActiveSubWriter);
			return odataCollectionWriter;
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x0000E078 File Offset: 0x0000C278
		private ODataWriter CreateResourceWriterImplementation(string parameterName, IEdmTypeReference expectedItemType)
		{
			ODataWriter odataWriter = this.CreateFormatResourceWriter(parameterName, expectedItemType);
			this.ReplaceScope(ODataParameterWriterCore.ParameterWriterState.ActiveSubWriter);
			return odataWriter;
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x0000E098 File Offset: 0x0000C298
		private ODataWriter CreateResourceSetWriterImplementation(string parameterName, IEdmTypeReference expectedItemType)
		{
			ODataWriter odataWriter = this.CreateFormatResourceSetWriter(parameterName, expectedItemType);
			this.ReplaceScope(ODataParameterWriterCore.ParameterWriterState.ActiveSubWriter);
			return odataWriter;
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x0000E0B6 File Offset: 0x0000C2B6
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

		// Token: 0x0600051A RID: 1306 RVA: 0x0000E0E8 File Offset: 0x0000C2E8
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

		// Token: 0x0600051B RID: 1307 RVA: 0x0000E1CA File Offset: 0x0000C3CA
		private void WriteEndImplementation()
		{
			this.InterceptException(delegate
			{
				this.EndPayload();
			});
			this.LeaveScope();
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x0000E1E4 File Offset: 0x0000C3E4
		private void VerifyNotInErrorOrCompletedState()
		{
			if (this.State == ODataParameterWriterCore.ParameterWriterState.Error || this.State == ODataParameterWriterCore.ParameterWriterState.Completed)
			{
				throw new ODataException(Strings.ODataParameterWriterCore_CannotWriteInErrorOrCompletedState);
			}
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x0000E203 File Offset: 0x0000C403
		private void VerifyCanFlush(bool synchronousCall)
		{
			this.VerifyNotDisposed();
			this.VerifyCallAllowed(synchronousCall);
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x0000E212 File Offset: 0x0000C412
		private void VerifyCallAllowed(bool synchronousCall)
		{
			if (synchronousCall && !this.outputContext.Synchronous)
			{
				throw new ODataException(Strings.ODataParameterWriterCore_SyncCallOnAsyncWriter);
			}
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x0000E230 File Offset: 0x0000C430
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

		// Token: 0x06000520 RID: 1312 RVA: 0x0000E260 File Offset: 0x0000C460
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

		// Token: 0x06000521 RID: 1313 RVA: 0x0000E290 File Offset: 0x0000C490
		private void EnterErrorScope()
		{
			if (this.State != ODataParameterWriterCore.ParameterWriterState.Error)
			{
				this.EnterScope(ODataParameterWriterCore.ParameterWriterState.Error);
			}
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x0000E2A2 File Offset: 0x0000C4A2
		private void EnterScope(ODataParameterWriterCore.ParameterWriterState newState)
		{
			this.ValidateTransition(newState);
			this.scopes.Push(newState);
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x0000E2B7 File Offset: 0x0000C4B7
		private void LeaveScope()
		{
			this.ValidateTransition(ODataParameterWriterCore.ParameterWriterState.Completed);
			if (this.State == ODataParameterWriterCore.ParameterWriterState.CanWriteParameter)
			{
				this.scopes.Pop();
			}
			this.ReplaceScope(ODataParameterWriterCore.ParameterWriterState.Completed);
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x0000E2DC File Offset: 0x0000C4DC
		private void ReplaceScope(ODataParameterWriterCore.ParameterWriterState newState)
		{
			this.ValidateTransition(newState);
			this.scopes.Pop();
			this.scopes.Push(newState);
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x0000E300 File Offset: 0x0000C500
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

		// Token: 0x0400025C RID: 604
		private readonly ODataOutputContext outputContext;

		// Token: 0x0400025D RID: 605
		private readonly IEdmOperation operation;

		// Token: 0x0400025E RID: 606
		private Stack<ODataParameterWriterCore.ParameterWriterState> scopes = new Stack<ODataParameterWriterCore.ParameterWriterState>();

		// Token: 0x0400025F RID: 607
		private HashSet<string> parameterNamesWritten = new HashSet<string>(StringComparer.Ordinal);

		// Token: 0x04000260 RID: 608
		private IDuplicatePropertyNameChecker duplicatePropertyNameChecker;

		// Token: 0x0200027E RID: 638
		private enum ParameterWriterState
		{
			// Token: 0x04000B3A RID: 2874
			Start,
			// Token: 0x04000B3B RID: 2875
			CanWriteParameter,
			// Token: 0x04000B3C RID: 2876
			ActiveSubWriter,
			// Token: 0x04000B3D RID: 2877
			Completed,
			// Token: 0x04000B3E RID: 2878
			Error
		}
	}
}
