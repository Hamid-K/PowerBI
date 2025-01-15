using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Azure.Core;

namespace Azure
{
	// Token: 0x02000026 RID: 38
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class Operation
	{
		// Token: 0x0600008B RID: 139 RVA: 0x00002D68 File Offset: 0x00000F68
		public virtual RehydrationToken? GetRehydrationToken()
		{
			return null;
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600008C RID: 140
		public abstract string Id { get; }

		// Token: 0x0600008D RID: 141
		public abstract Response GetRawResponse();

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600008E RID: 142
		public abstract bool HasCompleted { get; }

		// Token: 0x0600008F RID: 143
		[return: Nullable(new byte[] { 0, 1 })]
		public abstract ValueTask<Response> UpdateStatusAsync(CancellationToken cancellationToken = default(CancellationToken));

		// Token: 0x06000090 RID: 144
		public abstract Response UpdateStatus(CancellationToken cancellationToken = default(CancellationToken));

		// Token: 0x06000091 RID: 145 RVA: 0x00002D80 File Offset: 0x00000F80
		[return: Nullable(new byte[] { 0, 1 })]
		public virtual async ValueTask<Response> WaitForCompletionResponseAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			return await new OperationPoller(null).WaitForCompletionResponseAsync(this, null, cancellationToken).ConfigureAwait(false);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00002DCC File Offset: 0x00000FCC
		[return: Nullable(new byte[] { 0, 1 })]
		public virtual async ValueTask<Response> WaitForCompletionResponseAsync(TimeSpan pollingInterval, CancellationToken cancellationToken = default(CancellationToken))
		{
			return await new OperationPoller(null).WaitForCompletionResponseAsync(this, new TimeSpan?(pollingInterval), cancellationToken).ConfigureAwait(false);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00002E20 File Offset: 0x00001020
		[return: Nullable(new byte[] { 0, 1 })]
		public virtual async ValueTask<Response> WaitForCompletionResponseAsync(DelayStrategy delayStrategy, CancellationToken cancellationToken = default(CancellationToken))
		{
			return await new OperationPoller(delayStrategy).WaitForCompletionResponseAsync(this, null, cancellationToken).ConfigureAwait(false);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00002E74 File Offset: 0x00001074
		public virtual Response WaitForCompletionResponse(CancellationToken cancellationToken = default(CancellationToken))
		{
			return new OperationPoller(null).WaitForCompletionResponse(this, null, cancellationToken);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00002E97 File Offset: 0x00001097
		public virtual Response WaitForCompletionResponse(TimeSpan pollingInterval, CancellationToken cancellationToken = default(CancellationToken))
		{
			return new OperationPoller(null).WaitForCompletionResponse(this, new TimeSpan?(pollingInterval), cancellationToken);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00002EAC File Offset: 0x000010AC
		public virtual Response WaitForCompletionResponse(DelayStrategy delayStrategy, CancellationToken cancellationToken = default(CancellationToken))
		{
			return new OperationPoller(delayStrategy).WaitForCompletionResponse(this, null, cancellationToken);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00002ECF File Offset: 0x000010CF
		internal static T GetValue<T>([Nullable(2)] ref T value) where T : class
		{
			if (value == null)
			{
				throw new InvalidOperationException("The operation has not completed yet.");
			}
			return value;
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00002EEF File Offset: 0x000010EF
		[NullableContext(0)]
		internal static T GetValue<T>(ref T? value) where T : struct
		{
			if (value == null)
			{
				throw new InvalidOperationException("The operation has not completed yet.");
			}
			return value.Value;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00002F0A File Offset: 0x0000110A
		[NullableContext(2)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00002F13 File Offset: 0x00001113
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00002F1B File Offset: 0x0000111B
		[NullableContext(2)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}
	}
}
