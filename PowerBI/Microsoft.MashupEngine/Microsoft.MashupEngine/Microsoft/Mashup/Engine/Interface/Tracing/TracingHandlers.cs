using System;

namespace Microsoft.Mashup.Engine.Interface.Tracing
{
	// Token: 0x02000136 RID: 310
	public class TracingHandlers
	{
		// Token: 0x170001DD RID: 477
		// (get) Token: 0x0600055B RID: 1371 RVA: 0x00008102 File Offset: 0x00006302
		// (set) Token: 0x0600055C RID: 1372 RVA: 0x00008109 File Offset: 0x00006309
		public static Func<Exception, string> GetExceptionDetails { get; set; }
	}
}
