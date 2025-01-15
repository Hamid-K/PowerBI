using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Runtime.CompilerServices
{
	// Token: 0x0200001B RID: 27
	[StructLayout(LayoutKind.Auto)]
	public readonly struct ConfiguredCancelableAsyncEnumerable<[Nullable(2)] T>
	{
		// Token: 0x06000033 RID: 51 RVA: 0x000022CA File Offset: 0x000004CA
		internal ConfiguredCancelableAsyncEnumerable(IAsyncEnumerable<T> enumerable, bool continueOnCapturedContext, CancellationToken cancellationToken)
		{
			this._enumerable = enumerable;
			this._continueOnCapturedContext = continueOnCapturedContext;
			this._cancellationToken = cancellationToken;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000022E1 File Offset: 0x000004E1
		[return: Nullable(new byte[] { 0, 1 })]
		public ConfiguredCancelableAsyncEnumerable<T> ConfigureAwait(bool continueOnCapturedContext)
		{
			return new ConfiguredCancelableAsyncEnumerable<T>(this._enumerable, continueOnCapturedContext, this._cancellationToken);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000022F5 File Offset: 0x000004F5
		[return: Nullable(new byte[] { 0, 1 })]
		public ConfiguredCancelableAsyncEnumerable<T> WithCancellation(CancellationToken cancellationToken)
		{
			return new ConfiguredCancelableAsyncEnumerable<T>(this._enumerable, this._continueOnCapturedContext, cancellationToken);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002309 File Offset: 0x00000509
		public ConfiguredCancelableAsyncEnumerable<T>.Enumerator GetAsyncEnumerator()
		{
			return new ConfiguredCancelableAsyncEnumerable<T>.Enumerator(this._enumerable.GetAsyncEnumerator(this._cancellationToken), this._continueOnCapturedContext);
		}

		// Token: 0x04000019 RID: 25
		private readonly IAsyncEnumerable<T> _enumerable;

		// Token: 0x0400001A RID: 26
		private readonly CancellationToken _cancellationToken;

		// Token: 0x0400001B RID: 27
		private readonly bool _continueOnCapturedContext;

		// Token: 0x02000020 RID: 32
		[StructLayout(LayoutKind.Auto)]
		public readonly struct Enumerator
		{
			// Token: 0x06000049 RID: 73 RVA: 0x000026F0 File Offset: 0x000008F0
			internal Enumerator(IAsyncEnumerator<T> enumerator, bool continueOnCapturedContext)
			{
				this._enumerator = enumerator;
				this._continueOnCapturedContext = continueOnCapturedContext;
			}

			// Token: 0x0600004A RID: 74 RVA: 0x00002700 File Offset: 0x00000900
			public ConfiguredValueTaskAwaitable<bool> MoveNextAsync()
			{
				return this._enumerator.MoveNextAsync().ConfigureAwait(this._continueOnCapturedContext);
			}

			// Token: 0x17000011 RID: 17
			// (get) Token: 0x0600004B RID: 75 RVA: 0x00002726 File Offset: 0x00000926
			[Nullable(1)]
			public T Current
			{
				[NullableContext(1)]
				get
				{
					return this._enumerator.Current;
				}
			}

			// Token: 0x0600004C RID: 76 RVA: 0x00002734 File Offset: 0x00000934
			public ConfiguredValueTaskAwaitable DisposeAsync()
			{
				return this._enumerator.DisposeAsync().ConfigureAwait(this._continueOnCapturedContext);
			}

			// Token: 0x04000026 RID: 38
			private readonly IAsyncEnumerator<T> _enumerator;

			// Token: 0x04000027 RID: 39
			private readonly bool _continueOnCapturedContext;
		}
	}
}
