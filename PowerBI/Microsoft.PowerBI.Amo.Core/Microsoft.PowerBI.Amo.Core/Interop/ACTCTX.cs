using System;
using System.Runtime.InteropServices;

namespace Microsoft.AnalysisServices.Interop
{
	// Token: 0x02000126 RID: 294
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 4)]
	internal struct ACTCTX
	{
		// Token: 0x04000A55 RID: 2645
		public int cbSize;

		// Token: 0x04000A56 RID: 2646
		public uint dwFlags;

		// Token: 0x04000A57 RID: 2647
		public string lpSource;

		// Token: 0x04000A58 RID: 2648
		public ushort wProcessorArchitecture;

		// Token: 0x04000A59 RID: 2649
		public ushort wLangId;

		// Token: 0x04000A5A RID: 2650
		public string lpAssemblyDirectory;

		// Token: 0x04000A5B RID: 2651
		public string lpResourceName;

		// Token: 0x04000A5C RID: 2652
		public string lpApplicationName;

		// Token: 0x04000A5D RID: 2653
		public IntPtr hModule;
	}
}
