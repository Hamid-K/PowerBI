using System;
using System.Runtime.InteropServices;

namespace Microsoft.BIServer.Configuration.WMI
{
	// Token: 0x0200002D RID: 45
	[StructLayout(LayoutKind.Explicit)]
	internal struct PROCESSOR_INFO
	{
		// Token: 0x04000123 RID: 291
		[FieldOffset(0)]
		internal uint dwOemId;

		// Token: 0x04000124 RID: 292
		[FieldOffset(0)]
		internal ushort wProcessorArchitecture;

		// Token: 0x04000125 RID: 293
		[FieldOffset(2)]
		internal ushort wReserved;
	}
}
