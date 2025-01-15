using System;

namespace Microsoft.SqlServer.SqlDumper
{
	// Token: 0x02000004 RID: 4
	[Flags]
	public enum DumperFlags
	{
		// Token: 0x04000046 RID: 70
		Default = 0,
		// Token: 0x04000047 RID: 71
		NoMiniDump = 2,
		// Token: 0x04000048 RID: 72
		ReferencedMemory = 8,
		// Token: 0x04000049 RID: 73
		AllMemory = 16,
		// Token: 0x0400004A RID: 74
		AllThreads = 32,
		// Token: 0x0400004B RID: 75
		MatchFilename = 64,
		// Token: 0x0400004C RID: 76
		Verbose = 256,
		// Token: 0x0400004D RID: 77
		WaitAtExit = 512,
		// Token: 0x0400004E RID: 78
		SendToWatson = 1024,
		// Token: 0x0400004F RID: 79
		UseDefault = 2048,
		// Token: 0x04000050 RID: 80
		MaximumDump = 4096,
		// Token: 0x04000051 RID: 81
		DoubleDump = 8192,
		// Token: 0x04000052 RID: 82
		ForceWatson = 16384,
		// Token: 0x04000053 RID: 83
		Filtered = 32768,
		// Token: 0x04000054 RID: 84
		CriticalClr = 65536,
		// Token: 0x04000055 RID: 85
		NoRegistry = 131072,
		// Token: 0x04000056 RID: 86
		LocalOnly = 262144,
		// Token: 0x04000057 RID: 87
		DeleteFiles = 524288,
		// Token: 0x04000058 RID: 88
		ShowUI = 1048576,
		// Token: 0x04000059 RID: 89
		ForceUserThread = 2097152,
		// Token: 0x0400005A RID: 90
		MatchSignatureTime = 4194304,
		// Token: 0x0400005B RID: 91
		SyncSubmitToWatson = 67108864
	}
}
