using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Azure.Core.Pipeline;

namespace Azure.Core
{
	// Token: 0x02000078 RID: 120
	[NullableContext(1)]
	[Nullable(0)]
	internal class OperationInternal : OperationInternalBase
	{
		// Token: 0x060003F0 RID: 1008 RVA: 0x0000BF8C File Offset: 0x0000A18C
		public static OperationInternal Succeeded(Response rawResponse)
		{
			return new OperationInternal(OperationState.Success(rawResponse));
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x0000BF99 File Offset: 0x0000A199
		public static OperationInternal Failed(Response rawResponse, RequestFailedException operationFailedException)
		{
			return new OperationInternal(OperationState.Failure(rawResponse, operationFailedException));
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x0000BFA8 File Offset: 0x0000A1A8
		public OperationInternal(IOperation operation, ClientDiagnostics clientDiagnostics, Response rawResponse, [Nullable(2)] string operationTypeName = null, [Nullable(new byte[] { 2, 0, 1, 1 })] IEnumerable<KeyValuePair<string, string>> scopeAttributes = null, [Nullable(2)] DelayStrategy fallbackStrategy = null)
			: base(clientDiagnostics, operationTypeName ?? operation.GetType().Name, scopeAttributes, fallbackStrategy)
		{
			this._internalOperation = new OperationInternal<VoidValue>(new OperationInternal.OperationToOperationOfTProxy(operation), clientDiagnostics, rawResponse, operationTypeName ?? operation.GetType().Name, scopeAttributes, fallbackStrategy);
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x0000BFFC File Offset: 0x0000A1FC
		private OperationInternal(OperationState finalState)
			: base(finalState.RawResponse)
		{
			this._internalOperation = (finalState.HasSucceeded ? OperationInternal<VoidValue>.Succeeded(finalState.RawResponse, default(VoidValue)) : OperationInternal<VoidValue>.Failed(finalState.RawResponse, finalState.OperationFailedException));
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x060003F4 RID: 1012 RVA: 0x0000C04F File Offset: 0x0000A24F
		public override Response RawResponse
		{
			get
			{
				return this._internalOperation.RawResponse;
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x060003F5 RID: 1013 RVA: 0x0000C05C File Offset: 0x0000A25C
		public override bool HasCompleted
		{
			get
			{
				return this._internalOperation.HasCompleted;
			}
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x0000C06C File Offset: 0x0000A26C
		[return: Nullable(new byte[] { 0, 1 })]
		protected override async ValueTask<Response> UpdateStatusAsync(bool async, CancellationToken cancellationToken)
		{
			Response response;
			if (async)
			{
				response = await this._internalOperation.UpdateStatusAsync(cancellationToken).ConfigureAwait(false);
			}
			else
			{
				response = this._internalOperation.UpdateStatus(cancellationToken);
			}
			return response;
		}

		// Token: 0x040001A9 RID: 425
		private readonly OperationInternal<VoidValue> _internalOperation;

		// Token: 0x02000102 RID: 258
		[NullableContext(0)]
		private class OperationToOperationOfTProxy : IOperation<VoidValue>
		{
			// Token: 0x06000789 RID: 1929 RVA: 0x0001AA66 File Offset: 0x00018C66
			[NullableContext(1)]
			public OperationToOperationOfTProxy(IOperation operation)
			{
				this._operation = operation;
			}

			// Token: 0x0600078A RID: 1930 RVA: 0x0001AA78 File Offset: 0x00018C78
			public async ValueTask<OperationState<VoidValue>> UpdateStateAsync(bool async, CancellationToken cancellationToken)
			{
				OperationState operationState = await this._operation.UpdateStateAsync(async, cancellationToken).ConfigureAwait(false);
				OperationState<VoidValue> operationState2;
				if (!operationState.HasCompleted)
				{
					operationState2 = OperationState<VoidValue>.Pending(operationState.RawResponse);
				}
				else if (operationState.HasSucceeded)
				{
					operationState2 = OperationState<VoidValue>.Success(operationState.RawResponse, default(VoidValue));
				}
				else
				{
					operationState2 = OperationState<VoidValue>.Failure(operationState.RawResponse, operationState.OperationFailedException);
				}
				return operationState2;
			}

			// Token: 0x040003A6 RID: 934
			[Nullable(1)]
			private readonly IOperation _operation;
		}
	}
}
