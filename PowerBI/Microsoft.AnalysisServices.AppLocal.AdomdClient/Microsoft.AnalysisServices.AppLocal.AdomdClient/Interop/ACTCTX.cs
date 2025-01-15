using System;
using System.Runtime.InteropServices;

namespace Microsoft.AnalysisServices.AdomdClient.Interop
{
	// Token: 0x02000131 RID: 305
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 4)]
	internal struct ACTCTX
	{
		// Token: 0x04000A9C RID: 2716
		public int cbSize;

		// Token: 0x04000A9D RID: 2717
		public uint dwFlags;

		// Token: 0x04000A9E RID: 2718
		public string lpSource;

		// Token: 0x04000A9F RID: 2719
		public ushort wProcessorArchitecture;

		// Token: 0x04000AA0 RID: 2720
		public ushort wLangId;

		// Token: 0x04000AA1 RID: 2721
		public string lpAssemblyDirectory;

		// Token: 0x04000AA2 RID: 2722
		public string lpResourceName;

		// Token: 0x04000AA3 RID: 2723
		public string lpApplicationName;

		// Token: 0x04000AA4 RID: 2724
		public IntPtr hModule;
	}
}
