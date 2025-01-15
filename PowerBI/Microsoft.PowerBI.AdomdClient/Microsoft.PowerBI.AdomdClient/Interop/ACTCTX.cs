using System;
using System.Runtime.InteropServices;

namespace Microsoft.AnalysisServices.AdomdClient.Interop
{
	// Token: 0x02000131 RID: 305
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 4)]
	internal struct ACTCTX
	{
		// Token: 0x04000A8F RID: 2703
		public int cbSize;

		// Token: 0x04000A90 RID: 2704
		public uint dwFlags;

		// Token: 0x04000A91 RID: 2705
		public string lpSource;

		// Token: 0x04000A92 RID: 2706
		public ushort wProcessorArchitecture;

		// Token: 0x04000A93 RID: 2707
		public ushort wLangId;

		// Token: 0x04000A94 RID: 2708
		public string lpAssemblyDirectory;

		// Token: 0x04000A95 RID: 2709
		public string lpResourceName;

		// Token: 0x04000A96 RID: 2710
		public string lpApplicationName;

		// Token: 0x04000A97 RID: 2711
		public IntPtr hModule;
	}
}
