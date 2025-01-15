using System;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Description
{
	// Token: 0x02001C99 RID: 7321
	[Flags]
	public enum TransformationKind
	{
		// Token: 0x04005BBA RID: 23482
		Unknown = 0,
		// Token: 0x04005BBB RID: 23483
		Constant = 1,
		// Token: 0x04005BBC RID: 23484
		Concat = 2,
		// Token: 0x04005BBD RID: 23485
		Substring = 4,
		// Token: 0x04005BBE RID: 23486
		WholeColumn = 8,
		// Token: 0x04005BBF RID: 23487
		Lookup = 16,
		// Token: 0x04005BC0 RID: 23488
		ParseNumber = 32,
		// Token: 0x04005BC1 RID: 23489
		ParseDateTime = 64,
		// Token: 0x04005BC2 RID: 23490
		RoundNumber = 128,
		// Token: 0x04005BC3 RID: 23491
		RoundDateTime = 256,
		// Token: 0x04005BC4 RID: 23492
		CaseTransformation = 512,
		// Token: 0x04005BC5 RID: 23493
		FormatNumber = 1024,
		// Token: 0x04005BC6 RID: 23494
		FormatNumericRange = 2048,
		// Token: 0x04005BC7 RID: 23495
		FormatDateTime = 4096,
		// Token: 0x04005BC8 RID: 23496
		FormatDateTimeRange = 8192,
		// Token: 0x04005BC9 RID: 23497
		InputNumber = 16384,
		// Token: 0x04005BCA RID: 23498
		InputDate = 32768,
		// Token: 0x04005BCB RID: 23499
		IfThenElse = 65536,
		// Token: 0x04005BCC RID: 23500
		RelativePosition = 131072,
		// Token: 0x04005BCD RID: 23501
		RegexPair = 262144,
		// Token: 0x04005BCE RID: 23502
		MultiTokenPositionalRegex = 524288
	}
}
