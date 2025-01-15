using System;

namespace Microsoft.ProgramSynthesis.Detection.RichDataTypes
{
	// Token: 0x02000A94 RID: 2708
	[Flags]
	public enum NativeNumericType
	{
		// Token: 0x04001E90 RID: 7824
		Unsigned = 1,
		// Token: 0x04001E91 RID: 7825
		Integer8 = 2,
		// Token: 0x04001E92 RID: 7826
		Integer16 = 4,
		// Token: 0x04001E93 RID: 7827
		Integer32 = 8,
		// Token: 0x04001E94 RID: 7828
		Integer64 = 16,
		// Token: 0x04001E95 RID: 7829
		BigInteger = 32,
		// Token: 0x04001E96 RID: 7830
		Real = 64
	}
}
