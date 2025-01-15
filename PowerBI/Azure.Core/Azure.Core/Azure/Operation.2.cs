using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Azure.Core;

namespace Azure
{
	// Token: 0x02000027 RID: 39
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class Operation<T> : Operation
	{
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600009D RID: 157
		public abstract T Value { get; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600009E RID: 158
		public abstract bool HasValue { get; }

		// Token: 0x0600009F RID: 159 RVA: 0x00002F2C File Offset: 0x0000112C
		public virtual Response<T> WaitForCompletion(CancellationToken cancellationToken = default(CancellationToken))
		{
			return new OperationPoller(null).WaitForCompletion<T>(this, null, cancellationToken);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00002F4F File Offset: 0x0000114F
		public virtual Response<T> WaitForCompletion(TimeSpan pollingInterval, CancellationToken cancellationToken)
		{
			return new OperationPoller(null).WaitForCompletion<T>(this, new TimeSpan?(pollingInterval), cancellationToken);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00002F64 File Offset: 0x00001164
		[return: Nullable(new byte[] { 0, 1, 1 })]
		public virtual async ValueTask<Response<T>> WaitForCompletionAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			return await new OperationPoller(null).WaitForCompletionAsync<T>(this, null, cancellationToken).ConfigureAwait(false);
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00002FB0 File Offset: 0x000011B0
		[return: Nullable(new byte[] { 0, 1, 1 })]
		public virtual async ValueTask<Response<T>> WaitForCompletionAsync(TimeSpan pollingInterval, CancellationToken cancellationToken)
		{
			return await new OperationPoller(null).WaitForCompletionAsync<T>(this, new TimeSpan?(pollingInterval), cancellationToken).ConfigureAwait(false);
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00003004 File Offset: 0x00001204
		public virtual Response<T> WaitForCompletion(DelayStrategy delayStrategy, CancellationToken cancellationToken)
		{
			return new OperationPoller(delayStrategy).WaitForCompletion<T>(this, null, cancellationToken);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00003028 File Offset: 0x00001228
		[return: Nullable(new byte[] { 0, 1, 1 })]
		public virtual async ValueTask<Response<T>> WaitForCompletionAsync(DelayStrategy delayStrategy, CancellationToken cancellationToken)
		{
			return await new OperationPoller(delayStrategy).WaitForCompletionAsync<T>(this, null, cancellationToken).ConfigureAwait(false);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x0000307C File Offset: 0x0000127C
		[EditorBrowsable(EditorBrowsableState.Never)]
		[return: Nullable(new byte[] { 0, 1 })]
		public override async ValueTask<Response> WaitForCompletionResponseAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			return (await this.WaitForCompletionAsync(cancellationToken).ConfigureAwait(false)).GetRawResponse();
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x000030C8 File Offset: 0x000012C8
		[EditorBrowsable(EditorBrowsableState.Never)]
		[return: Nullable(new byte[] { 0, 1 })]
		public override async ValueTask<Response> WaitForCompletionResponseAsync(TimeSpan pollingInterval, CancellationToken cancellationToken = default(CancellationToken))
		{
			return (await this.WaitForCompletionAsync(pollingInterval, cancellationToken).ConfigureAwait(false)).GetRawResponse();
		}
	}
}
