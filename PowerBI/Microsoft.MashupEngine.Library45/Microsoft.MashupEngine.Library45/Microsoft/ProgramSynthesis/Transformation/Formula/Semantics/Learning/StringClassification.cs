using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning
{
	// Token: 0x02001668 RID: 5736
	[Flags]
	public enum StringClassification : byte
	{
		// Token: 0x040049FE RID: 18942
		None = 0,
		// Token: 0x040049FF RID: 18943
		All = 248,
		// Token: 0x04004A00 RID: 18944
		FormattedNumber = 8,
		// Token: 0x04004A01 RID: 18945
		FormattedDateTime = 16,
		// Token: 0x04004A02 RID: 18946
		FormattedDateTimeNoNumbers = 32,
		// Token: 0x04004A03 RID: 18947
		PhoneNumber = 64,
		// Token: 0x04004A04 RID: 18948
		SocialSecurityNumber = 128,
		// Token: 0x04004A05 RID: 18949
		FormattedOther = 192
	}
}
