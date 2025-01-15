using System;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000079 RID: 121
	public interface IRdlSandboxTypeInfo
	{
		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000356 RID: 854
		string Namespace { get; }

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000357 RID: 855
		bool AllowNew { get; }

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000358 RID: 856
		string Name { get; }
	}
}
