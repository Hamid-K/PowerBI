using System;

namespace Microsoft.Lucia
{
	// Token: 0x02000012 RID: 18
	public static class FormattableStringUtil
	{
		// Token: 0x06000048 RID: 72 RVA: 0x000029CD File Offset: 0x00000BCD
		public static string Format(IFormatProvider formatProvider, FormattableString formattable)
		{
			return formattable.ToString(formatProvider);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000029D6 File Offset: 0x00000BD6
		public static FormattableString Formattable(FormattableString formattable)
		{
			return formattable;
		}
	}
}
