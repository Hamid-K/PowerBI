using System;
using System.Runtime.InteropServices;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000022 RID: 34
	internal static class NativeDumperMethods
	{
		// Token: 0x06000110 RID: 272
		[DllImport("ReportingServicesService.exe", CharSet = CharSet.Unicode, EntryPoint = "RsDumpDump")]
		public static extern void Dump();

		// Token: 0x06000111 RID: 273
		[DllImport("ReportingServicesService.exe", CharSet = CharSet.Unicode, EntryPoint = "RsDumpSetErrorText")]
		public static extern void SetErrorText(string errorText);

		// Token: 0x06000112 RID: 274
		[DllImport("ReportingServicesService.exe", CharSet = CharSet.Unicode, EntryPoint = "RsDumpSetErrorDetails")]
		public static extern void SetErrorDetails(string errorDetails);

		// Token: 0x06000113 RID: 275
		[DllImport("ReportingServicesService.exe", CharSet = CharSet.Unicode, EntryPoint = "RsDumpSetErrorSignature")]
		public static extern void SetErrorSignature(int signature);

		// Token: 0x06000114 RID: 276
		[DllImport("ReportingServicesService.exe", CharSet = CharSet.Unicode, EntryPoint = "RsDumpAddMemory")]
		public static extern void AddMemory(IntPtr pData, int size);

		// Token: 0x04000095 RID: 149
		private const string ReportingServiceExe = "ReportingServicesService.exe";

		// Token: 0x020000DE RID: 222
		[Flags]
		public enum DumperFlags
		{
			// Token: 0x0400048C RID: 1164
			Default = 0,
			// Token: 0x0400048D RID: 1165
			NoMiniDump = 2,
			// Token: 0x0400048E RID: 1166
			ReferencedMemory = 8,
			// Token: 0x0400048F RID: 1167
			AllMemory = 16,
			// Token: 0x04000490 RID: 1168
			AllThreads = 32,
			// Token: 0x04000491 RID: 1169
			MatchFilename = 64,
			// Token: 0x04000492 RID: 1170
			Verbose = 256,
			// Token: 0x04000493 RID: 1171
			WaitAtExit = 512,
			// Token: 0x04000494 RID: 1172
			SendToWatson = 1024,
			// Token: 0x04000495 RID: 1173
			UseDefault = 2048,
			// Token: 0x04000496 RID: 1174
			MaximumDump = 4096,
			// Token: 0x04000497 RID: 1175
			DoubleDump = 8192,
			// Token: 0x04000498 RID: 1176
			ForceWatson = 16384
		}
	}
}
