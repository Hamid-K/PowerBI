using System;

namespace System.Buffers.Text
{
	// Token: 0x020000E9 RID: 233
	internal static class Utf8Constants
	{
		// Token: 0x04000269 RID: 617
		public const byte Colon = 58;

		// Token: 0x0400026A RID: 618
		public const byte Comma = 44;

		// Token: 0x0400026B RID: 619
		public const byte Minus = 45;

		// Token: 0x0400026C RID: 620
		public const byte Period = 46;

		// Token: 0x0400026D RID: 621
		public const byte Plus = 43;

		// Token: 0x0400026E RID: 622
		public const byte Slash = 47;

		// Token: 0x0400026F RID: 623
		public const byte Space = 32;

		// Token: 0x04000270 RID: 624
		public const byte Hyphen = 45;

		// Token: 0x04000271 RID: 625
		public const byte Separator = 44;

		// Token: 0x04000272 RID: 626
		public const int GroupSize = 3;

		// Token: 0x04000273 RID: 627
		public static readonly TimeSpan s_nullUtcOffset = TimeSpan.MinValue;

		// Token: 0x04000274 RID: 628
		public const int DateTimeMaxUtcOffsetHours = 14;

		// Token: 0x04000275 RID: 629
		public const int DateTimeNumFractionDigits = 7;

		// Token: 0x04000276 RID: 630
		public const int MaxDateTimeFraction = 9999999;

		// Token: 0x04000277 RID: 631
		public const ulong BillionMaxUIntValue = 4294967295000000000UL;

		// Token: 0x04000278 RID: 632
		public const uint Billion = 1000000000U;
	}
}
