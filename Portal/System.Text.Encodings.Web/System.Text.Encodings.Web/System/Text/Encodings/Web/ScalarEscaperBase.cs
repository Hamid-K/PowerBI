using System;

namespace System.Text.Encodings.Web
{
	// Token: 0x02000028 RID: 40
	internal abstract class ScalarEscaperBase
	{
		// Token: 0x0600016E RID: 366
		internal abstract int EncodeUtf16(Rune value, Span<char> destination);

		// Token: 0x0600016F RID: 367
		internal abstract int EncodeUtf8(Rune value, Span<byte> destination);
	}
}
