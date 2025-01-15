using System;
using System.Runtime.InteropServices;

namespace Microsoft.AnalysisServices.MInterop
{
	// Token: 0x02000026 RID: 38
	[ComVisible(true)]
	[Guid("5C49AE96-82EF-4CFD-B0F0-AB0C14FE54C8")]
	public struct MEngineSettingsForContainerPool
	{
		// Token: 0x040000C2 RID: 194
		[MarshalAs(UnmanagedType.I4)]
		public int MaxContainerCount;

		// Token: 0x040000C3 RID: 195
		[MarshalAs(UnmanagedType.I4)]
		public int MinContainerCount;
	}
}
