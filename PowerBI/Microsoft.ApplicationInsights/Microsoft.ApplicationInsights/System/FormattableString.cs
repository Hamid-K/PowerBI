using System;
using System.Globalization;

namespace System
{
	// Token: 0x02000019 RID: 25
	internal class FormattableString
	{
		// Token: 0x060000D1 RID: 209 RVA: 0x0000620C File Offset: 0x0000440C
		public FormattableString(string format, object[] args)
		{
			this.format = format;
			this.args = args;
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00006222 File Offset: 0x00004422
		public static string Invariant(global::System.FormattableString formattableString)
		{
			return formattableString.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x0000622F File Offset: 0x0000442F
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, this.format, this.args);
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00006247 File Offset: 0x00004447
		public string ToString(IFormatProvider formatProvider)
		{
			if (formatProvider == null)
			{
				throw new ArgumentNullException("formatProvider");
			}
			return string.Format(formatProvider, this.format, this.args);
		}

		// Token: 0x0400007B RID: 123
		private readonly string format;

		// Token: 0x0400007C RID: 124
		private readonly object[] args;
	}
}
