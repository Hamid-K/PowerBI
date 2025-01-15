using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Intent
{
	// Token: 0x020019B5 RID: 6581
	[Flags]
	public enum ProgramIntent : long
	{
		// Token: 0x04005253 RID: 21075
		FromStr = 2L,
		// Token: 0x04005254 RID: 21076
		FromNumber = 4L,
		// Token: 0x04005255 RID: 21077
		FromNumbers = 8L,
		// Token: 0x04005256 RID: 21078
		FromDateTime = 16L,
		// Token: 0x04005257 RID: 21079
		FromTime = 32L,
		// Token: 0x04005258 RID: 21080
		FromRowNumber = 64L,
		// Token: 0x04005259 RID: 21081
		ConstantString = 128L,
		// Token: 0x0400525A RID: 21082
		ConstantNumber = 256L,
		// Token: 0x0400525B RID: 21083
		NumberFormat = 4096L,
		// Token: 0x0400525C RID: 21084
		NumberRound = 8192L,
		// Token: 0x0400525D RID: 21085
		ForwardFillLinear = 16384L,
		// Token: 0x0400525E RID: 21086
		DateTimeFormat = 2097152L,
		// Token: 0x0400525F RID: 21087
		DateTimeRound = 4194304L,
		// Token: 0x04005260 RID: 21088
		DateTimePart = 8388608L,
		// Token: 0x04005261 RID: 21089
		TimePart = 16777216L,
		// Token: 0x04005262 RID: 21090
		Case = 67108864L,
		// Token: 0x04005263 RID: 21091
		Trim = 134217728L,
		// Token: 0x04005264 RID: 21092
		ParseDateTime = 268435456L,
		// Token: 0x04005265 RID: 21093
		ParseNumber = 536870912L,
		// Token: 0x04005266 RID: 21094
		Replace = 1073741824L,
		// Token: 0x04005267 RID: 21095
		Substring = 2147483648L,
		// Token: 0x04005268 RID: 21096
		Length = 4294967296L,
		// Token: 0x04005269 RID: 21097
		Concat = 2199023255552L,
		// Token: 0x0400526A RID: 21098
		If = 4398046511104L,
		// Token: 0x0400526B RID: 21099
		Arithmetic = 8796093022208L,
		// Token: 0x0400526C RID: 21100
		ArithmeticAggregate = 17592186044416L,
		// Token: 0x0400526D RID: 21101
		WholeColumn = 35184372088832L,
		// Token: 0x0400526E RID: 21102
		MultiColumn = 70368744177664L,
		// Token: 0x0400526F RID: 21103
		SingleColumn = 140737488355328L,
		// Token: 0x04005270 RID: 21104
		MultiSubstring = 281474976710656L,
		// Token: 0x04005271 RID: 21105
		ToStr = 288230376151711744L,
		// Token: 0x04005272 RID: 21106
		ToInt = 576460752303423488L,
		// Token: 0x04005273 RID: 21107
		ToDouble = 1152921504606846976L,
		// Token: 0x04005274 RID: 21108
		ToDecimal = 2305843009213693952L,
		// Token: 0x04005275 RID: 21109
		ToDateTime = 4611686018427387904L,
		// Token: 0x04005276 RID: 21110
		Unknown = 0L,
		// Token: 0x04005277 RID: 21111
		CompositeRoots = 68177990021504L,
		// Token: 0x04005278 RID: 21112
		SubstringCase = 2214592512L,
		// Token: 0x04005279 RID: 21113
		SubstringTrim = 2281701376L,
		// Token: 0x0400527A RID: 21114
		SubstringTrimCase = 2348810240L,
		// Token: 0x0400527B RID: 21115
		ConcatStr = 2199023255680L,
		// Token: 0x0400527C RID: 21116
		ConcatWholeColumn = 37383395344384L,
		// Token: 0x0400527D RID: 21117
		ConcatWholeColumnStr = 37383395344512L,
		// Token: 0x0400527E RID: 21118
		ConcatWholeColumnSubstring = 37385542828032L,
		// Token: 0x0400527F RID: 21119
		ConcatWholeColumnSubstringStr = 37385542828160L,
		// Token: 0x04005280 RID: 21120
		ConcatSubstring = 2201170739200L,
		// Token: 0x04005281 RID: 21121
		ConcatSubstringsStr = 2201170739328L,
		// Token: 0x04005282 RID: 21122
		ArithmeticFormat = 8796093026304L,
		// Token: 0x04005283 RID: 21123
		ArithmeticConstant = 8796093022464L,
		// Token: 0x04005284 RID: 21124
		ArithmeticConstantFormat = 8796093026560L,
		// Token: 0x04005285 RID: 21125
		ArithmeticAggregateFormat = 17592186048512L,
		// Token: 0x04005286 RID: 21126
		NumberRoundFormat = 12288L,
		// Token: 0x04005287 RID: 21127
		ParseNumberFormat = 536875008L,
		// Token: 0x04005288 RID: 21128
		ParseNumberRound = 536879104L,
		// Token: 0x04005289 RID: 21129
		ParseNumberRoundFormat = 536883200L,
		// Token: 0x0400528A RID: 21130
		ExtractNumber = 2684354560L,
		// Token: 0x0400528B RID: 21131
		ExtractNumberFormat = 2684358656L,
		// Token: 0x0400528C RID: 21132
		ExtractNumberRound = 2684362752L,
		// Token: 0x0400528D RID: 21133
		ExtractNumberRoundFormat = 2684366848L,
		// Token: 0x0400528E RID: 21134
		ParseDateTimeFormat = 270532608L,
		// Token: 0x0400528F RID: 21135
		ParseDateTimeRound = 272629760L,
		// Token: 0x04005290 RID: 21136
		ParseDateTimeRoundFormat = 274726912L,
		// Token: 0x04005291 RID: 21137
		ExtractDateTime = 2415919104L,
		// Token: 0x04005292 RID: 21138
		ExtractDateTimeFormat = 2418016256L,
		// Token: 0x04005293 RID: 21139
		ExtractDateTimeRound = 2420113408L,
		// Token: 0x04005294 RID: 21140
		ExtractDateTimeRoundFormat = 2422210560L,
		// Token: 0x04005295 RID: 21141
		DateTimeRoundFormat = 6291456L,
		// Token: 0x04005296 RID: 21142
		ParseDateTimePart = 276824064L,
		// Token: 0x04005297 RID: 21143
		ParseDateTimePartFormat = 276828160L,
		// Token: 0x04005298 RID: 21144
		ExtractDateTimePart = 2424307712L
	}
}
