using System;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x020019DB RID: 6619
	[Flags]
	internal enum FileMappingAccess : uint
	{
		// Token: 0x04005748 RID: 22344
		FILE_MAP_WRITE = 2U,
		// Token: 0x04005749 RID: 22345
		FILE_MAP_READ = 4U,
		// Token: 0x0400574A RID: 22346
		FILE_MAP_ALL_ACCESS = 31U
	}
}
