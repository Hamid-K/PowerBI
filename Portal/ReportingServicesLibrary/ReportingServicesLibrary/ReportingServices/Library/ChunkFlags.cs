using System;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000296 RID: 662
	[Flags]
	internal enum ChunkFlags
	{
		// Token: 0x040008B8 RID: 2232
		None = 0,
		// Token: 0x040008B9 RID: 2233
		Compressed = 1,
		// Token: 0x040008BA RID: 2234
		FileSystem = 2,
		// Token: 0x040008BB RID: 2235
		CrossDatabaseSharing = 4
	}
}
