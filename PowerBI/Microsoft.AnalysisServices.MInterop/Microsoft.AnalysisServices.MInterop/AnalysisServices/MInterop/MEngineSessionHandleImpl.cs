using System;
using Microsoft.Data.Mashup.Preview;

namespace Microsoft.AnalysisServices.MInterop
{
	// Token: 0x02000022 RID: 34
	internal sealed class MEngineSessionHandleImpl : IDisposable
	{
		// Token: 0x06000087 RID: 135 RVA: 0x000042C6 File Offset: 0x000024C6
		internal MEngineSessionHandleImpl(string sessionId)
		{
			this.handle = MashupSessionHandle.FromSession(sessionId);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x000042DA File Offset: 0x000024DA
		public MashupSessionHandle GetHandle()
		{
			return this.handle;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x000042E2 File Offset: 0x000024E2
		public void Dispose()
		{
			if (this.handle != null)
			{
				this.handle.Dispose();
				this.handle = null;
			}
		}

		// Token: 0x040000B7 RID: 183
		private MashupSessionHandle handle;
	}
}
