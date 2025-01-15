using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Azure.Core.Pipeline;

namespace Azure.Core
{
	// Token: 0x02000081 RID: 129
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class OperationPoller
	{
		// Token: 0x06000422 RID: 1058 RVA: 0x0000C658 File Offset: 0x0000A858
		[NullableContext(2)]
		public OperationPoller(DelayStrategy strategy = null)
		{
			this._delayStrategy = strategy ?? new FixedDelayWithNoJitterStrategy(null);
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x0000C684 File Offset: 0x0000A884
		[return: Nullable(new byte[] { 0, 1 })]
		public ValueTask<Response> WaitForCompletionResponseAsync(Operation operation, TimeSpan? delayHint, CancellationToken cancellationToken)
		{
			return this.WaitForCompletionAsync(true, operation, delayHint, cancellationToken);
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x0000C690 File Offset: 0x0000A890
		public Response WaitForCompletionResponse(Operation operation, TimeSpan? delayHint, CancellationToken cancellationToken)
		{
			return this.WaitForCompletionAsync(false, operation, delayHint, cancellationToken).EnsureCompleted<Response>();
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x0000C6A1 File Offset: 0x0000A8A1
		[return: Nullable(new byte[] { 0, 1 })]
		public ValueTask<Response> WaitForCompletionResponseAsync(OperationInternalBase operation, TimeSpan? delayHint, CancellationToken cancellationToken)
		{
			return this.WaitForCompletionAsync(true, operation, delayHint, cancellationToken);
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x0000C6AD File Offset: 0x0000A8AD
		public Response WaitForCompletionResponse(OperationInternalBase operation, TimeSpan? delayHint, CancellationToken cancellationToken)
		{
			return this.WaitForCompletionAsync(false, operation, delayHint, cancellationToken).EnsureCompleted<Response>();
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x0000C6C0 File Offset: 0x0000A8C0
		[return: Nullable(new byte[] { 0, 1, 1 })]
		public async ValueTask<Response<T>> WaitForCompletionAsync<T>(Operation<T> operation, TimeSpan? delayHint, CancellationToken cancellationToken)
		{
			Response response = await this.WaitForCompletionAsync(true, operation, delayHint, cancellationToken).ConfigureAwait(false);
			return Response.FromValue<T>(operation.Value, response);
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x0000C71C File Offset: 0x0000A91C
		public Response<T> WaitForCompletion<T>(Operation<T> operation, TimeSpan? delayHint, CancellationToken cancellationToken)
		{
			Response response = this.WaitForCompletionAsync(false, operation, delayHint, cancellationToken).EnsureCompleted<Response>();
			return Response.FromValue<T>(operation.Value, response);
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x0000C748 File Offset: 0x0000A948
		[return: Nullable(new byte[] { 0, 1, 1 })]
		public async ValueTask<Response<T>> WaitForCompletionAsync<T>(OperationInternal<T> operation, TimeSpan? delayHint, CancellationToken cancellationToken)
		{
			Response response = await this.WaitForCompletionAsync(true, operation, delayHint, cancellationToken).ConfigureAwait(false);
			return Response.FromValue<T>(operation.Value, response);
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x0000C7A4 File Offset: 0x0000A9A4
		public Response<T> WaitForCompletion<T>(OperationInternal<T> operation, TimeSpan? delayHint, CancellationToken cancellationToken)
		{
			Response response = this.WaitForCompletionAsync(false, operation, delayHint, cancellationToken).EnsureCompleted<Response>();
			return Response.FromValue<T>(operation.Value, response);
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x0000C7D0 File Offset: 0x0000A9D0
		[return: Nullable(new byte[] { 0, 1 })]
		private async ValueTask<Response> WaitForCompletionAsync(bool async, Operation operation, TimeSpan? delayHint, CancellationToken cancellationToken)
		{
			int retryNumber = 0;
			for (;;)
			{
				Response response;
				if (async)
				{
					response = await operation.UpdateStatusAsync(cancellationToken).ConfigureAwait(false);
				}
				else
				{
					response = operation.UpdateStatus(cancellationToken);
				}
				Response response2 = response;
				if (operation.HasCompleted)
				{
					break;
				}
				DelayStrategy delayStrategy = ((delayHint != null) ? new FixedDelayWithNoJitterStrategy(new TimeSpan?(delayHint.Value)) : this._delayStrategy);
				await OperationPoller.Delay(async, delayStrategy.GetNextDelay(response2, ++retryNumber), cancellationToken).ConfigureAwait(false);
			}
			return operation.GetRawResponse();
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x0000C834 File Offset: 0x0000AA34
		[return: Nullable(new byte[] { 0, 1 })]
		private async ValueTask<Response> WaitForCompletionAsync(bool async, OperationInternalBase operation, TimeSpan? delayHint, CancellationToken cancellationToken)
		{
			int retryNumber = 0;
			for (;;)
			{
				Response response;
				if (async)
				{
					response = await operation.UpdateStatusAsync(cancellationToken).ConfigureAwait(false);
				}
				else
				{
					response = operation.UpdateStatus(cancellationToken);
				}
				Response response2 = response;
				if (operation.HasCompleted)
				{
					break;
				}
				DelayStrategy delayStrategy = ((delayHint != null) ? new FixedDelayWithNoJitterStrategy(new TimeSpan?(delayHint.Value)) : this._delayStrategy);
				await OperationPoller.Delay(async, delayStrategy.GetNextDelay(response2, ++retryNumber), cancellationToken).ConfigureAwait(false);
			}
			return operation.RawResponse;
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x0000C898 File Offset: 0x0000AA98
		private static async ValueTask Delay(bool async, TimeSpan delay, CancellationToken cancellationToken)
		{
			if (async)
			{
				await Task.Delay(delay, cancellationToken).ConfigureAwait(false);
			}
			else if (cancellationToken.CanBeCanceled)
			{
				if (cancellationToken.WaitHandle.WaitOne(delay))
				{
					cancellationToken.ThrowIfCancellationRequested();
				}
			}
			else
			{
				Thread.Sleep(delay);
			}
		}

		// Token: 0x040001B9 RID: 441
		private readonly DelayStrategy _delayStrategy;
	}
}
