using System;
using System.Runtime.InteropServices;

namespace Microsoft.ReportingServices.HostingInterfaces
{
	// Token: 0x02000005 RID: 5
	[ComVisible(true)]
	public enum RsMemoryPressureLevel
	{
		// Token: 0x04000036 RID: 54
		Unknown = -1,
		// Token: 0x04000037 RID: 55
		NoPressure,
		// Token: 0x04000038 RID: 56
		LowPressure,
		// Token: 0x04000039 RID: 57
		MediumPressure,
		// Token: 0x0400003A RID: 58
		HighPressure,
		// Token: 0x0400003B RID: 59
		ExceedingLimit
	}
}
