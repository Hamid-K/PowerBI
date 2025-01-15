using System;

namespace Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql
{
	// Token: 0x020011C8 RID: 4552
	internal enum ConstantType
	{
		// Token: 0x0400416C RID: 16748
		InvalidValue,
		// Token: 0x0400416D RID: 16749
		AnsiString,
		// Token: 0x0400416E RID: 16750
		UnicodeString,
		// Token: 0x0400416F RID: 16751
		Binary,
		// Token: 0x04004170 RID: 16752
		Boolean,
		// Token: 0x04004171 RID: 16753
		DateTime,
		// Token: 0x04004172 RID: 16754
		Time,
		// Token: 0x04004173 RID: 16755
		DateTimeOffset,
		// Token: 0x04004174 RID: 16756
		Integer,
		// Token: 0x04004175 RID: 16757
		Decimal,
		// Token: 0x04004176 RID: 16758
		Float,
		// Token: 0x04004177 RID: 16759
		Interval,
		// Token: 0x04004178 RID: 16760
		Null = 13,
		// Token: 0x04004179 RID: 16761
		DoubleQuotesString = 15,
		// Token: 0x0400417A RID: 16762
		Enum,
		// Token: 0x0400417B RID: 16763
		Date
	}
}
