using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Azure.Core.Pipeline;

namespace Azure.Core
{
	// Token: 0x0200007B RID: 123
	[NullableContext(1)]
	[Nullable(0)]
	internal class OperationInternal<[Nullable(2)] T> : OperationInternalBase
	{
		// Token: 0x06000400 RID: 1024 RVA: 0x0000C140 File Offset: 0x0000A340
		public static OperationInternal<T> Succeeded(Response rawResponse, T value)
		{
			return new OperationInternal<T>(OperationState<T>.Success(rawResponse, value));
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x0000C14E File Offset: 0x0000A34E
		public static OperationInternal<T> Failed(Response rawResponse, RequestFailedException operationFailedException)
		{
			return new OperationInternal<T>(OperationState<T>.Failure(rawResponse, operationFailedException));
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x0000C15C File Offset: 0x0000A35C
		public OperationInternal(IOperation<T> operation, ClientDiagnostics clientDiagnostics, Response rawResponse, [Nullable(2)] string operationTypeName = null, [Nullable(new byte[] { 2, 0, 1, 1 })] IEnumerable<KeyValuePair<string, string>> scopeAttributes = null, [Nullable(2)] DelayStrategy fallbackStrategy = null)
			: base(clientDiagnostics, operationTypeName ?? operation.GetType().Name, scopeAttributes, fallbackStrategy)
		{
			this._operation = operation;
			this._rawResponse = rawResponse;
			this._stateLock = new AsyncLockWithValue<OperationState<T>>();
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x0000C193 File Offset: 0x0000A393
		private OperationInternal([Nullable(new byte[] { 0, 1 })] OperationState<T> finalState)
			: base(finalState.RawResponse)
		{
			this._operation = new OperationInternal<T>.FinalOperation();
			this._rawResponse = finalState.RawResponse;
			this._stateLock = new AsyncLockWithValue<OperationState<T>>(finalState);
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000404 RID: 1028 RVA: 0x0000C1C8 File Offset: 0x0000A3C8
		public override Response RawResponse
		{
			get
			{
				OperationState<T> operationState;
				if (!this._stateLock.TryGetValue(out operationState))
				{
					return this._rawResponse;
				}
				return operationState.RawResponse;
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000405 RID: 1029 RVA: 0x0000C1F2 File Offset: 0x0000A3F2
		public override bool HasCompleted
		{
			get
			{
				return this._stateLock.HasValue;
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000406 RID: 1030 RVA: 0x0000C200 File Offset: 0x0000A400
		public bool HasValue
		{
			get
			{
				OperationState<T> operationState;
				return this._stateLock.TryGetValue(out operationState) && operationState.HasSucceeded;
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000407 RID: 1031 RVA: 0x0000C228 File Offset: 0x0000A428
		public T Value
		{
			get
			{
				OperationState<T> operationState;
				if (!this._stateLock.TryGetValue(out operationState))
				{
					throw new InvalidOperationException("The operation has not completed yet.");
				}
				if (operationState.HasSucceeded)
				{
					return operationState.Value;
				}
				throw operationState.OperationFailedException;
			}
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x0000C268 File Offset: 0x0000A468
		[return: Nullable(new byte[] { 0, 1, 1 })]
		public async ValueTask<Response<T>> WaitForCompletionAsync(CancellationToken cancellationToken)
		{
			return await this.WaitForCompletionAsync(true, null, cancellationToken).ConfigureAwait(false);
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x0000C2B4 File Offset: 0x0000A4B4
		[return: Nullable(new byte[] { 0, 1, 1 })]
		public async ValueTask<Response<T>> WaitForCompletionAsync(TimeSpan pollingInterval, CancellationToken cancellationToken)
		{
			return await this.WaitForCompletionAsync(true, new TimeSpan?(pollingInterval), cancellationToken).ConfigureAwait(false);
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x0000C308 File Offset: 0x0000A508
		public Response<T> WaitForCompletion(CancellationToken cancellationToken)
		{
			return this.WaitForCompletionAsync(false, null, cancellationToken).EnsureCompleted<Response<T>>();
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x0000C32B File Offset: 0x0000A52B
		public Response<T> WaitForCompletion(TimeSpan pollingInterval, CancellationToken cancellationToken)
		{
			return this.WaitForCompletionAsync(false, new TimeSpan?(pollingInterval), cancellationToken).EnsureCompleted<Response<T>>();
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x0000C340 File Offset: 0x0000A540
		[return: Nullable(new byte[] { 0, 1, 1 })]
		private async ValueTask<Response<T>> WaitForCompletionAsync(bool async, TimeSpan? pollingInterval, CancellationToken cancellationToken)
		{
			Response response = await base.WaitForCompletionResponseAsync(async, pollingInterval, this._waitForCompletionScopeName, cancellationToken).ConfigureAwait(false);
			return Response.FromValue<T>(this.Value, response);
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x0000C39C File Offset: 0x0000A59C
		[return: Nullable(new byte[] { 0, 1 })]
		protected override ValueTask<Response> UpdateStatusAsync(bool async, CancellationToken cancellationToken)
		{
			OperationInternal<T>.<UpdateStatusAsync>d__20 <UpdateStatusAsync>d__;
			<UpdateStatusAsync>d__.<>t__builder = AsyncValueTaskMethodBuilder<Response>.Create();
			<UpdateStatusAsync>d__.<>4__this = this;
			<UpdateStatusAsync>d__.async = async;
			<UpdateStatusAsync>d__.cancellationToken = cancellationToken;
			<UpdateStatusAsync>d__.<>1__state = -1;
			<UpdateStatusAsync>d__.<>t__builder.Start<OperationInternal<T>.<UpdateStatusAsync>d__20>(ref <UpdateStatusAsync>d__);
			return <UpdateStatusAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x0000C3EF File Offset: 0x0000A5EF
		private static Response GetResponseFromState([Nullable(new byte[] { 0, 1 })] OperationState<T> state)
		{
			if (state.HasSucceeded)
			{
				return state.RawResponse;
			}
			throw state.OperationFailedException;
		}

		// Token: 0x040001AE RID: 430
		private readonly IOperation<T> _operation;

		// Token: 0x040001AF RID: 431
		[Nullable(new byte[] { 1, 0, 1 })]
		private readonly AsyncLockWithValue<OperationState<T>> _stateLock;

		// Token: 0x040001B0 RID: 432
		private Response _rawResponse;

		// Token: 0x02000104 RID: 260
		[NullableContext(0)]
		private class FinalOperation : IOperation<T>
		{
			// Token: 0x0600078D RID: 1933 RVA: 0x0001ABCC File Offset: 0x00018DCC
			[return: Nullable(new byte[] { 0, 0, 1 })]
			public ValueTask<OperationState<T>> UpdateStateAsync(bool async, CancellationToken cancellationToken)
			{
				throw new NotSupportedException("The operation has already completed");
			}
		}
	}
}
