using System;

namespace Microsoft.Mashup.Engine.Interface.Tracing
{
	// Token: 0x0200012B RID: 299
	public interface IHostTraceValue : IDisposable
	{
		// Token: 0x06000532 RID: 1330
		void Add(object value);

		// Token: 0x06000533 RID: 1331
		void Begin();

		// Token: 0x06000534 RID: 1332
		void End();
	}
}
