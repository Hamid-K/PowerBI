using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000018 RID: 24
	internal struct TIME_ZONE_INFORMATION_Introp
	{
		// Token: 0x04000053 RID: 83
		internal int bias;

		// Token: 0x04000054 RID: 84
		[FixedBuffer(typeof(char), 32)]
		internal TIME_ZONE_INFORMATION_Introp.<standardName>e__FixedBuffer standardName;

		// Token: 0x04000055 RID: 85
		internal SYSTEMTIME_Introp standardDate;

		// Token: 0x04000056 RID: 86
		internal int standardBias;

		// Token: 0x04000057 RID: 87
		[FixedBuffer(typeof(char), 32)]
		internal TIME_ZONE_INFORMATION_Introp.<daylightName>e__FixedBuffer daylightName;

		// Token: 0x04000058 RID: 88
		internal SYSTEMTIME_Introp daylightDate;

		// Token: 0x04000059 RID: 89
		internal int daylightBias;

		// Token: 0x02000092 RID: 146
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 64)]
		public struct <standardName>e__FixedBuffer
		{
			// Token: 0x04000381 RID: 897
			public char FixedElementField;
		}

		// Token: 0x02000093 RID: 147
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 64)]
		public struct <daylightName>e__FixedBuffer
		{
			// Token: 0x04000382 RID: 898
			public char FixedElementField;
		}
	}
}
