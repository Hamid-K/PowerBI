using System;

namespace Microsoft.BIServer.Configuration
{
	// Token: 0x0200000E RID: 14
	[Flags]
	public enum DumperFlags
	{
		// Token: 0x04000058 RID: 88
		Default = 0,
		// Token: 0x04000059 RID: 89
		NoMiniDump = 2,
		// Token: 0x0400005A RID: 90
		ReferencedMemory = 8,
		// Token: 0x0400005B RID: 91
		AllMemory = 16,
		// Token: 0x0400005C RID: 92
		AllThreads = 32,
		// Token: 0x0400005D RID: 93
		MatchFilename = 64,
		// Token: 0x0400005E RID: 94
		Verbose = 256,
		// Token: 0x0400005F RID: 95
		WaitAtExit = 512,
		// Token: 0x04000060 RID: 96
		SendToWatson = 1024,
		// Token: 0x04000061 RID: 97
		UseDefault = 2048,
		// Token: 0x04000062 RID: 98
		MaximumDump = 4096,
		// Token: 0x04000063 RID: 99
		DoubleDump = 8192,
		// Token: 0x04000064 RID: 100
		ForceWatson = 16384,
		// Token: 0x04000065 RID: 101
		Filtered = 32768,
		// Token: 0x04000066 RID: 102
		CriticalClr = 65536,
		// Token: 0x04000067 RID: 103
		NoRegistry = 131072,
		// Token: 0x04000068 RID: 104
		LocalOnly = 262144,
		// Token: 0x04000069 RID: 105
		DeleteFiles = 524288,
		// Token: 0x0400006A RID: 106
		ShowUI = 1048576,
		// Token: 0x0400006B RID: 107
		ForceUserThread = 2097152,
		// Token: 0x0400006C RID: 108
		MatchSignatureTime = 4194304,
		// Token: 0x0400006D RID: 109
		SyncSubmitToWatson = 67108864
	}
}
