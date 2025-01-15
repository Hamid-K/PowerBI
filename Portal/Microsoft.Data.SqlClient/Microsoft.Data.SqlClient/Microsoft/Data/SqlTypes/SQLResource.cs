using System;

namespace Microsoft.Data.SqlTypes
{
	// Token: 0x02000015 RID: 21
	internal sealed class SQLResource
	{
		// Token: 0x0600061B RID: 1563 RVA: 0x000027D1 File Offset: 0x000009D1
		private SQLResource()
		{
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x0000B2C2 File Offset: 0x000094C2
		internal static string InvalidOpStreamClosed(string method)
		{
			return StringsHelper.GetString(Strings.SqlMisc_InvalidOpStreamClosed, new object[] { method });
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x0000B2D8 File Offset: 0x000094D8
		internal static string InvalidOpStreamNonWritable(string method)
		{
			return StringsHelper.GetString(Strings.SqlMisc_InvalidOpStreamNonWritable, new object[] { method });
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x0000B2EE File Offset: 0x000094EE
		internal static string InvalidOpStreamNonReadable(string method)
		{
			return StringsHelper.GetString(Strings.SqlMisc_InvalidOpStreamNonReadable, new object[] { method });
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x0000B304 File Offset: 0x00009504
		internal static string InvalidOpStreamNonSeekable(string method)
		{
			return StringsHelper.GetString(Strings.SqlMisc_InvalidOpStreamNonSeekable, new object[] { method });
		}

		// Token: 0x0400001F RID: 31
		internal static readonly string NullString = StringsHelper.GetString(Strings.SqlMisc_NullString, Array.Empty<object>());

		// Token: 0x04000020 RID: 32
		internal static readonly string MessageString = StringsHelper.GetString(Strings.SqlMisc_MessageString, Array.Empty<object>());

		// Token: 0x04000021 RID: 33
		internal static readonly string ArithOverflowMessage = StringsHelper.GetString(Strings.SqlMisc_ArithOverflowMessage, Array.Empty<object>());

		// Token: 0x04000022 RID: 34
		internal static readonly string DivideByZeroMessage = StringsHelper.GetString(Strings.SqlMisc_DivideByZeroMessage, Array.Empty<object>());

		// Token: 0x04000023 RID: 35
		internal static readonly string NullValueMessage = StringsHelper.GetString(Strings.SqlMisc_NullValueMessage, Array.Empty<object>());

		// Token: 0x04000024 RID: 36
		internal static readonly string TruncationMessage = StringsHelper.GetString(Strings.SqlMisc_TruncationMessage, Array.Empty<object>());

		// Token: 0x04000025 RID: 37
		internal static readonly string DateTimeOverflowMessage = StringsHelper.GetString(Strings.SqlMisc_DateTimeOverflowMessage, Array.Empty<object>());

		// Token: 0x04000026 RID: 38
		internal static readonly string ConcatDiffCollationMessage = StringsHelper.GetString(Strings.SqlMisc_ConcatDiffCollationMessage, Array.Empty<object>());

		// Token: 0x04000027 RID: 39
		internal static readonly string CompareDiffCollationMessage = StringsHelper.GetString(Strings.SqlMisc_CompareDiffCollationMessage, Array.Empty<object>());

		// Token: 0x04000028 RID: 40
		internal static readonly string InvalidFlagMessage = StringsHelper.GetString(Strings.SqlMisc_InvalidFlagMessage, Array.Empty<object>());

		// Token: 0x04000029 RID: 41
		internal static readonly string NumeToDecOverflowMessage = StringsHelper.GetString(Strings.SqlMisc_NumeToDecOverflowMessage, Array.Empty<object>());

		// Token: 0x0400002A RID: 42
		internal static readonly string ConversionOverflowMessage = StringsHelper.GetString(Strings.SqlMisc_ConversionOverflowMessage, Array.Empty<object>());

		// Token: 0x0400002B RID: 43
		internal static readonly string InvalidDateTimeMessage = StringsHelper.GetString(Strings.SqlMisc_InvalidDateTimeMessage, Array.Empty<object>());

		// Token: 0x0400002C RID: 44
		internal static readonly string TimeZoneSpecifiedMessage = StringsHelper.GetString(Strings.SqlMisc_TimeZoneSpecifiedMessage, Array.Empty<object>());

		// Token: 0x0400002D RID: 45
		internal static readonly string InvalidArraySizeMessage = StringsHelper.GetString(Strings.SqlMisc_InvalidArraySizeMessage, Array.Empty<object>());

		// Token: 0x0400002E RID: 46
		internal static readonly string InvalidPrecScaleMessage = StringsHelper.GetString(Strings.SqlMisc_InvalidPrecScaleMessage, Array.Empty<object>());

		// Token: 0x0400002F RID: 47
		internal static readonly string FormatMessage = StringsHelper.GetString(Strings.SqlMisc_FormatMessage, Array.Empty<object>());

		// Token: 0x04000030 RID: 48
		internal static readonly string NotFilledMessage = StringsHelper.GetString(Strings.SqlMisc_NotFilledMessage, Array.Empty<object>());

		// Token: 0x04000031 RID: 49
		internal static readonly string AlreadyFilledMessage = StringsHelper.GetString(Strings.SqlMisc_AlreadyFilledMessage, Array.Empty<object>());

		// Token: 0x04000032 RID: 50
		internal static readonly string ClosedXmlReaderMessage = StringsHelper.GetString(Strings.SqlMisc_ClosedXmlReaderMessage, Array.Empty<object>());
	}
}
