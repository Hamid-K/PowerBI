using System;

namespace Microsoft.BIServer.Configuration
{
	// Token: 0x02000010 RID: 16
	public sealed class DumpInstructions
	{
		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000061 RID: 97 RVA: 0x000029B4 File Offset: 0x00000BB4
		// (set) Token: 0x06000062 RID: 98 RVA: 0x000029BC File Offset: 0x00000BBC
		public string ServiceName { get; set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000063 RID: 99 RVA: 0x000029C5 File Offset: 0x00000BC5
		// (set) Token: 0x06000064 RID: 100 RVA: 0x000029CD File Offset: 0x00000BCD
		public string InstanceName { get; set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000065 RID: 101 RVA: 0x000029D6 File Offset: 0x00000BD6
		// (set) Token: 0x06000066 RID: 102 RVA: 0x000029DE File Offset: 0x00000BDE
		public string Location { get; set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000067 RID: 103 RVA: 0x000029E7 File Offset: 0x00000BE7
		// (set) Token: 0x06000068 RID: 104 RVA: 0x000029EF File Offset: 0x00000BEF
		public string ErrorText { get; set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000069 RID: 105 RVA: 0x000029F8 File Offset: 0x00000BF8
		// (set) Token: 0x0600006A RID: 106 RVA: 0x00002A00 File Offset: 0x00000C00
		public DumperFlags Flags { get; set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00002A09 File Offset: 0x00000C09
		// (set) Token: 0x0600006C RID: 108 RVA: 0x00002A11 File Offset: 0x00000C11
		public string LogFileToInclude { get; set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00002A1A File Offset: 0x00000C1A
		// (set) Token: 0x0600006E RID: 110 RVA: 0x00002A22 File Offset: 0x00000C22
		public int SizeOfLogFileToInclude { get; set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600006F RID: 111 RVA: 0x00002A2B File Offset: 0x00000C2B
		// (set) Token: 0x06000070 RID: 112 RVA: 0x00002A33 File Offset: 0x00000C33
		public int FramesToInclude { get; set; }
	}
}
