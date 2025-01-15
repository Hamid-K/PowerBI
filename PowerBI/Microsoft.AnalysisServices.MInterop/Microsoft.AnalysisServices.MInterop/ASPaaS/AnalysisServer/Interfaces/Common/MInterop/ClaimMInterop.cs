using System;
using System.Runtime.InteropServices;

namespace Microsoft.ASPaaS.AnalysisServer.Interfaces.Common.MInterop
{
	// Token: 0x02000004 RID: 4
	[ComVisible(true)]
	[Guid("FBDBAA85-A008-4DAC-96CC-F263EE3D1692")]
	[CLSCompliant(true)]
	public struct ClaimMInterop
	{
		// Token: 0x04000008 RID: 8
		[MarshalAs(UnmanagedType.BStr)]
		public string Type;

		// Token: 0x04000009 RID: 9
		[MarshalAs(UnmanagedType.BStr)]
		public string Value;

		// Token: 0x0400000A RID: 10
		[MarshalAs(UnmanagedType.BStr)]
		public string ValueType;
	}
}
