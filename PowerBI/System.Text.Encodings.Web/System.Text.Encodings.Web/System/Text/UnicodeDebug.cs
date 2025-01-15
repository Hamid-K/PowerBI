using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace System.Text
{
	// Token: 0x0200001B RID: 27
	internal static class UnicodeDebug
	{
		// Token: 0x06000049 RID: 73 RVA: 0x0000280C File Offset: 0x00000A0C
		[Conditional("DEBUG")]
		internal static void AssertIsBmpCodePoint(uint codePoint)
		{
			UnicodeUtility.IsBmpCodePoint(codePoint);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002815 File Offset: 0x00000A15
		[Conditional("DEBUG")]
		internal static void AssertIsHighSurrogateCodePoint(uint codePoint)
		{
			UnicodeUtility.IsHighSurrogateCodePoint(codePoint);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x0000281E File Offset: 0x00000A1E
		[Conditional("DEBUG")]
		internal static void AssertIsLowSurrogateCodePoint(uint codePoint)
		{
			UnicodeUtility.IsLowSurrogateCodePoint(codePoint);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002827 File Offset: 0x00000A27
		[Conditional("DEBUG")]
		internal static void AssertIsValidCodePoint(uint codePoint)
		{
			UnicodeUtility.IsValidCodePoint(codePoint);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002830 File Offset: 0x00000A30
		[Conditional("DEBUG")]
		internal static void AssertIsValidScalar(uint scalarValue)
		{
			UnicodeUtility.IsValidUnicodeScalar(scalarValue);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002839 File Offset: 0x00000A39
		[Conditional("DEBUG")]
		internal static void AssertIsValidSupplementaryPlaneScalar(uint scalarValue)
		{
			if (UnicodeUtility.IsValidUnicodeScalar(scalarValue))
			{
				UnicodeUtility.IsBmpCodePoint(scalarValue);
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x0000284A File Offset: 0x00000A4A
		private static string ToHexString(uint codePoint)
		{
			return FormattableString.Invariant(FormattableStringFactory.Create("U+{0:X4}", new object[] { codePoint }));
		}
	}
}
