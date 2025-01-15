using System;
using AngleSharp.Attributes;

namespace AngleSharp.Dom.Html
{
	// Token: 0x020003F8 RID: 1016
	[DomName("ValidityState")]
	public interface IValidityState
	{
		// Token: 0x17000A19 RID: 2585
		// (get) Token: 0x0600203A RID: 8250
		[DomName("valueMissing")]
		bool IsValueMissing { get; }

		// Token: 0x17000A1A RID: 2586
		// (get) Token: 0x0600203B RID: 8251
		[DomName("typeMismatch")]
		bool IsTypeMismatch { get; }

		// Token: 0x17000A1B RID: 2587
		// (get) Token: 0x0600203C RID: 8252
		[DomName("patternMismatch")]
		bool IsPatternMismatch { get; }

		// Token: 0x17000A1C RID: 2588
		// (get) Token: 0x0600203D RID: 8253
		[DomName("tooLong")]
		bool IsTooLong { get; }

		// Token: 0x17000A1D RID: 2589
		// (get) Token: 0x0600203E RID: 8254
		[DomName("tooShort")]
		bool IsTooShort { get; }

		// Token: 0x17000A1E RID: 2590
		// (get) Token: 0x0600203F RID: 8255
		[DomName("badInput")]
		bool IsBadInput { get; }

		// Token: 0x17000A1F RID: 2591
		// (get) Token: 0x06002040 RID: 8256
		[DomName("rangeUnderflow")]
		bool IsRangeUnderflow { get; }

		// Token: 0x17000A20 RID: 2592
		// (get) Token: 0x06002041 RID: 8257
		[DomName("rangeOverflow")]
		bool IsRangeOverflow { get; }

		// Token: 0x17000A21 RID: 2593
		// (get) Token: 0x06002042 RID: 8258
		[DomName("stepMismatch")]
		bool IsStepMismatch { get; }

		// Token: 0x17000A22 RID: 2594
		// (get) Token: 0x06002043 RID: 8259
		[DomName("customError")]
		bool IsCustomError { get; }

		// Token: 0x17000A23 RID: 2595
		// (get) Token: 0x06002044 RID: 8260
		[DomName("valid")]
		bool IsValid { get; }
	}
}
