using System;
using System.Buffers;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Azure.Core.Buffers;

namespace Azure.Core.Pipeline
{
	// Token: 0x0200009C RID: 156
	[NullableContext(1)]
	[Nullable(0)]
	internal class ResponseBodyPolicy : HttpPipelinePolicy
	{
		// Token: 0x060004F3 RID: 1267 RVA: 0x0000F327 File Offset: 0x0000D527
		public ResponseBodyPolicy(TimeSpan networkTimeout)
		{
			this._networkTimeout = networkTimeout;
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x0000F336 File Offset: 0x0000D536
		public override ValueTask ProcessAsync(HttpMessage message, [Nullable(new byte[] { 0, 1 })] ReadOnlyMemory<HttpPipelinePolicy> pipeline)
		{
			return this.ProcessAsync(message, pipeline, true);
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x0000F341 File Offset: 0x0000D541
		public override void Process(HttpMessage message, [Nullable(new byte[] { 0, 1 })] ReadOnlyMemory<HttpPipelinePolicy> pipeline)
		{
			this.ProcessAsync(message, pipeline, false).EnsureCompleted();
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x0000F354 File Offset: 0x0000D554
		private ValueTask ProcessAsync(HttpMessage message, [Nullable(new byte[] { 0, 1 })] ReadOnlyMemory<HttpPipelinePolicy> pipeline, bool async)
		{
			ResponseBodyPolicy.<ProcessAsync>d__5 <ProcessAsync>d__;
			<ProcessAsync>d__.<>t__builder = AsyncValueTaskMethodBuilder.Create();
			<ProcessAsync>d__.<>4__this = this;
			<ProcessAsync>d__.message = message;
			<ProcessAsync>d__.pipeline = pipeline;
			<ProcessAsync>d__.async = async;
			<ProcessAsync>d__.<>1__state = -1;
			<ProcessAsync>d__.<>t__builder.Start<ResponseBodyPolicy.<ProcessAsync>d__5>(ref <ProcessAsync>d__);
			return <ProcessAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x0000F3B0 File Offset: 0x0000D5B0
		private async Task CopyToAsync(Stream source, Stream destination, CancellationTokenSource cancellationTokenSource)
		{
			byte[] buffer = ArrayPool<byte>.Shared.Rent(81920);
			try
			{
				for (;;)
				{
					cancellationTokenSource.CancelAfter(this._networkTimeout);
					int num = await source.ReadAsync(buffer, 0, buffer.Length, cancellationTokenSource.Token).ConfigureAwait(false);
					if (num == 0)
					{
						break;
					}
					await destination.WriteAsync(new ReadOnlyMemory<byte>(buffer, 0, num), cancellationTokenSource.Token).ConfigureAwait(false);
				}
			}
			finally
			{
				cancellationTokenSource.CancelAfter(Timeout.InfiniteTimeSpan);
				ArrayPool<byte>.Shared.Return(buffer, false);
			}
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x0000F40C File Offset: 0x0000D60C
		private void CopyTo(Stream source, Stream destination, CancellationTokenSource cancellationTokenSource)
		{
			byte[] array = ArrayPool<byte>.Shared.Rent(81920);
			try
			{
				int num;
				while ((num = source.Read(array, 0, array.Length)) != 0)
				{
					cancellationTokenSource.Token.ThrowIfCancellationRequested();
					cancellationTokenSource.CancelAfter(this._networkTimeout);
					destination.Write(array, 0, num);
				}
			}
			finally
			{
				cancellationTokenSource.CancelAfter(Timeout.InfiniteTimeSpan);
				ArrayPool<byte>.Shared.Return(array, false);
			}
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x0000F488 File Offset: 0x0000D688
		[NullableContext(2)]
		internal static void ThrowIfCancellationRequestedOrTimeout(CancellationToken originalToken, CancellationToken timeoutToken, Exception inner, TimeSpan timeout)
		{
			CancellationHelper.ThrowIfCancellationRequested(originalToken);
			if (timeoutToken.IsCancellationRequested)
			{
				throw CancellationHelper.CreateOperationCanceledException(inner, timeoutToken, string.Format("The operation was cancelled because it exceeded the configured timeout of {0:g}. ", timeout) + "Network timeout can be adjusted in ClientOptions.Retry.NetworkTimeout.");
			}
		}

		// Token: 0x0400020D RID: 525
		private const int DefaultCopyBufferSize = 81920;

		// Token: 0x0400020E RID: 526
		private readonly TimeSpan _networkTimeout;
	}
}
