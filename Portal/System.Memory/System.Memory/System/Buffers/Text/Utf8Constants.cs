using System;

namespace System.Buffers.Text
{
	// Token: 0x0200002B RID: 43
	internal static class Utf8Constants
	{
		// Token: 0x04000091 RID: 145
		public const byte Colon = 58;

		// Token: 0x04000092 RID: 146
		public const byte Comma = 44;

		// Token: 0x04000093 RID: 147
		public const byte Minus = 45;

		// Token: 0x04000094 RID: 148
		public const byte Period = 46;

		// Token: 0x04000095 RID: 149
		public const byte Plus = 43;

		// Token: 0x04000096 RID: 150
		public const byte Slash = 47;

		// Token: 0x04000097 RID: 151
		public const byte Space = 32;

		// Token: 0x04000098 RID: 152
		public const byte Hyphen = 45;

		// Token: 0x04000099 RID: 153
		public const byte Separator = 44;

		// Token: 0x0400009A RID: 154
		public const int GroupSize = 3;

		// Token: 0x0400009B RID: 155
		public static readonly TimeSpan s_nullUtcOffset = TimeSpan.MinValue;

		// Token: 0x0400009C RID: 156
		public const int DateTimeMaxUtcOffsetHours = 14;

		// Token: 0x0400009D RID: 157
		public const int DateTimeNumFractionDigits = 7;

		// Token: 0x0400009E RID: 158
		public const int MaxDateTimeFraction = 9999999;

		// Token: 0x0400009F RID: 159
		public const ulong BillionMaxUIntValue = 4294967295000000000UL;

		// Token: 0x040000A0 RID: 160
		public const uint Billion = 1000000000U;
	}
}
