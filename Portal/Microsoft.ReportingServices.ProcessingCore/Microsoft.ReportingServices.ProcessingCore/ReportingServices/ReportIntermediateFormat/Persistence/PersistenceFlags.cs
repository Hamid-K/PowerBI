using System;

namespace Microsoft.ReportingServices.ReportIntermediateFormat.Persistence
{
	// Token: 0x0200053A RID: 1338
	[Flags]
	internal enum PersistenceFlags
	{
		// Token: 0x04002074 RID: 8308
		None = 0,
		// Token: 0x04002075 RID: 8309
		Seekable = 1,
		// Token: 0x04002076 RID: 8310
		CompatVersioned = 2
	}
}
