using System;

namespace Microsoft.Owin.Hosting.Utilities
{
	// Token: 0x02000009 RID: 9
	internal sealed class Disposable : MarshalByRefObject, IDisposable
	{
		// Token: 0x06000037 RID: 55 RVA: 0x00002EA8 File Offset: 0x000010A8
		public Disposable(Action dispose)
		{
			this._dispose = dispose;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002EB7 File Offset: 0x000010B7
		public void Dispose()
		{
			this._dispose();
		}

		// Token: 0x04000026 RID: 38
		private readonly Action _dispose;
	}
}
