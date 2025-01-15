using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Azure.Core.Pipeline;

namespace Azure.Core
{
	// Token: 0x02000076 RID: 118
	[NullableContext(1)]
	[Nullable(0)]
	internal abstract class OperationInternalBase
	{
		// Token: 0x060003E3 RID: 995 RVA: 0x0000BC54 File Offset: 0x00009E54
		protected OperationInternalBase(Response rawResponse)
		{
			this._diagnostics = new ClientDiagnostics(ClientOptions.Default, null);
			this._updateStatusScopeName = string.Empty;
			this._waitForCompletionResponseScopeName = string.Empty;
			this._waitForCompletionScopeName = string.Empty;
			this._scopeAttributes = null;
			this._fallbackStrategy = null;
			this._responseLock = new AsyncLockWithValue<Response>(rawResponse);
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x0000BCBC File Offset: 0x00009EBC
		protected OperationInternalBase(ClientDiagnostics clientDiagnostics, string operationTypeName, [Nullable(new byte[] { 2, 0, 1, 1 })] IEnumerable<KeyValuePair<string, string>> scopeAttributes = null, [Nullable(2)] DelayStrategy fallbackStrategy = null)
		{
			this._diagnostics = clientDiagnostics;
			this._updateStatusScopeName = operationTypeName + ".UpdateStatus";
			this._waitForCompletionResponseScopeName = operationTypeName + ".WaitForCompletionResponse";
			this._waitForCompletionScopeName = operationTypeName + ".WaitForCompletion";
			IReadOnlyDictionary<string, string> readOnlyDictionary;
			if (scopeAttributes == null)
			{
				readOnlyDictionary = null;
			}
			else
			{
				readOnlyDictionary = scopeAttributes.ToDictionary((KeyValuePair<string, string> kvp) => kvp.Key, (KeyValuePair<string, string> kvp) => kvp.Value);
			}
			this._scopeAttributes = readOnlyDictionary;
			this._fallbackStrategy = fallbackStrategy;
			this._responseLock = new AsyncLockWithValue<Response>();
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x060003E5 RID: 997
		public abstract Response RawResponse { get; }

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x060003E6 RID: 998
		public abstract bool HasCompleted { get; }

		// Token: 0x060003E7 RID: 999 RVA: 0x0000BD6C File Offset: 0x00009F6C
		[return: Nullable(new byte[] { 0, 1 })]
		public async ValueTask<Response> UpdateStatusAsync(CancellationToken cancellationToken)
		{
			return await this.UpdateStatusAsync(true, cancellationToken).ConfigureAwait(false);
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x0000BDB7 File Offset: 0x00009FB7
		public Response UpdateStatus(CancellationToken cancellationToken)
		{
			return this.UpdateStatusAsync(false, cancellationToken).EnsureCompleted<Response>();
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x0000BDC8 File Offset: 0x00009FC8
		[return: Nullable(new byte[] { 0, 1 })]
		public async ValueTask<Response> WaitForCompletionResponseAsync(CancellationToken cancellationToken)
		{
			return await this.WaitForCompletionResponseAsync(true, null, this._waitForCompletionResponseScopeName, cancellationToken).ConfigureAwait(false);
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x0000BE14 File Offset: 0x0000A014
		[return: Nullable(new byte[] { 0, 1 })]
		public async ValueTask<Response> WaitForCompletionResponseAsync(TimeSpan pollingInterval, CancellationToken cancellationToken)
		{
			return await this.WaitForCompletionResponseAsync(true, new TimeSpan?(pollingInterval), this._waitForCompletionResponseScopeName, cancellationToken).ConfigureAwait(false);
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x0000BE68 File Offset: 0x0000A068
		public Response WaitForCompletionResponse(CancellationToken cancellationToken)
		{
			return this.WaitForCompletionResponseAsync(false, null, this._waitForCompletionResponseScopeName, cancellationToken).EnsureCompleted<Response>();
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x0000BE91 File Offset: 0x0000A091
		public Response WaitForCompletionResponse(TimeSpan pollingInterval, CancellationToken cancellationToken)
		{
			return this.WaitForCompletionResponseAsync(false, new TimeSpan?(pollingInterval), this._waitForCompletionResponseScopeName, cancellationToken).EnsureCompleted<Response>();
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x0000BEAC File Offset: 0x0000A0AC
		[return: Nullable(new byte[] { 0, 1 })]
		protected ValueTask<Response> WaitForCompletionResponseAsync(bool async, TimeSpan? pollingInterval, string scopeName, CancellationToken cancellationToken)
		{
			OperationInternalBase.<WaitForCompletionResponseAsync>d__19 <WaitForCompletionResponseAsync>d__;
			<WaitForCompletionResponseAsync>d__.<>t__builder = AsyncValueTaskMethodBuilder<Response>.Create();
			<WaitForCompletionResponseAsync>d__.<>4__this = this;
			<WaitForCompletionResponseAsync>d__.async = async;
			<WaitForCompletionResponseAsync>d__.pollingInterval = pollingInterval;
			<WaitForCompletionResponseAsync>d__.scopeName = scopeName;
			<WaitForCompletionResponseAsync>d__.cancellationToken = cancellationToken;
			<WaitForCompletionResponseAsync>d__.<>1__state = -1;
			<WaitForCompletionResponseAsync>d__.<>t__builder.Start<OperationInternalBase.<WaitForCompletionResponseAsync>d__19>(ref <WaitForCompletionResponseAsync>d__);
			return <WaitForCompletionResponseAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060003EE RID: 1006
		[return: Nullable(new byte[] { 0, 1 })]
		protected abstract ValueTask<Response> UpdateStatusAsync(bool async, CancellationToken cancellationToken);

		// Token: 0x060003EF RID: 1007 RVA: 0x0000BF10 File Offset: 0x0000A110
		protected DiagnosticScope CreateScope(string scopeName)
		{
			DiagnosticScope diagnosticScope = this._diagnostics.CreateScope(scopeName, ActivityKind.Internal);
			if (this._scopeAttributes != null)
			{
				foreach (KeyValuePair<string, string> keyValuePair in this._scopeAttributes)
				{
					diagnosticScope.AddAttribute(keyValuePair.Key, keyValuePair.Value);
				}
			}
			diagnosticScope.Start();
			return diagnosticScope;
		}

		// Token: 0x040001A2 RID: 418
		private readonly ClientDiagnostics _diagnostics;

		// Token: 0x040001A3 RID: 419
		[Nullable(new byte[] { 2, 1, 1 })]
		private readonly IReadOnlyDictionary<string, string> _scopeAttributes;

		// Token: 0x040001A4 RID: 420
		[Nullable(2)]
		private readonly DelayStrategy _fallbackStrategy;

		// Token: 0x040001A5 RID: 421
		private readonly AsyncLockWithValue<Response> _responseLock;

		// Token: 0x040001A6 RID: 422
		private readonly string _waitForCompletionResponseScopeName;

		// Token: 0x040001A7 RID: 423
		protected readonly string _updateStatusScopeName;

		// Token: 0x040001A8 RID: 424
		protected readonly string _waitForCompletionScopeName;
	}
}
