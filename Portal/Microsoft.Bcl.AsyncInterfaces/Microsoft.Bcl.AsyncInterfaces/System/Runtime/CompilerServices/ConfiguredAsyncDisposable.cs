using System;
using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
	// Token: 0x0200001A RID: 26
	[StructLayout(LayoutKind.Auto)]
	public readonly struct ConfiguredAsyncDisposable
	{
		// Token: 0x06000031 RID: 49 RVA: 0x00002293 File Offset: 0x00000493
		internal ConfiguredAsyncDisposable(IAsyncDisposable source, bool continueOnCapturedContext)
		{
			this._source = source;
			this._continueOnCapturedContext = continueOnCapturedContext;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000022A4 File Offset: 0x000004A4
		public ConfiguredValueTaskAwaitable DisposeAsync()
		{
			return this._source.DisposeAsync().ConfigureAwait(this._continueOnCapturedContext);
		}

		// Token: 0x04000017 RID: 23
		private readonly IAsyncDisposable _source;

		// Token: 0x04000018 RID: 24
		private readonly bool _continueOnCapturedContext;
	}
}
