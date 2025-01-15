using System;

namespace Microsoft.SqlServer.SqlDumper
{
	// Token: 0x02000005 RID: 5
	[Flags]
	public enum MiniDumpFlags
	{
		// Token: 0x0400005D RID: 93
		Normal = 0,
		// Token: 0x0400005E RID: 94
		DataSegs = 1,
		// Token: 0x0400005F RID: 95
		FullMemory = 2,
		// Token: 0x04000060 RID: 96
		HandleData = 4,
		// Token: 0x04000061 RID: 97
		FilterMemory = 8,
		// Token: 0x04000062 RID: 98
		ScanMemory = 16,
		// Token: 0x04000063 RID: 99
		UnloadedModules = 32,
		// Token: 0x04000064 RID: 100
		IndirectlyReferencedMemory = 64,
		// Token: 0x04000065 RID: 101
		FilterModulePaths = 128,
		// Token: 0x04000066 RID: 102
		ProcessThreadData = 256,
		// Token: 0x04000067 RID: 103
		PrivateReadWriteMemory = 512,
		// Token: 0x04000068 RID: 104
		outOptionalData = 1024,
		// Token: 0x04000069 RID: 105
		FullMemoryInfo = 2048,
		// Token: 0x0400006A RID: 106
		ThreadInfo = 4096,
		// Token: 0x0400006B RID: 107
		CodeSegs = 8192,
		// Token: 0x0400006C RID: 108
		WithoutManagedState = 16384
	}
}
