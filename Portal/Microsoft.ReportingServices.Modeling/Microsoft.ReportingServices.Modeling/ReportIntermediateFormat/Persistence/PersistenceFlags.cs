using System;

namespace Microsoft.ReportingServices.ReportIntermediateFormat.Persistence
{
	// Token: 0x0200001D RID: 29
	[Flags]
	internal enum PersistenceFlags
	{
		// Token: 0x040000F4 RID: 244
		None = 0,
		// Token: 0x040000F5 RID: 245
		Seekable = 1,
		// Token: 0x040000F6 RID: 246
		CompatVersioned = 2
	}
}
