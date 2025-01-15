using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x02000043 RID: 67
	public enum FunctionHandle
	{
		// Token: 0x040000A4 RID: 164
		CubeAttributeMemberId,
		// Token: 0x040000A5 RID: 165
		CubeAttributeMemberProperty,
		// Token: 0x040000A6 RID: 166
		CubePropertyKey,
		// Token: 0x040000A7 RID: 167
		CubeMeasureProperty,
		// Token: 0x040000A8 RID: 168
		ListDistinct,
		// Token: 0x040000A9 RID: 169
		ListFirstN,
		// Token: 0x040000AA RID: 170
		ListSort,
		// Token: 0x040000AB RID: 171
		RecordRemoveFields,
		// Token: 0x040000AC RID: 172
		TableSelectRows,
		// Token: 0x040000AD RID: 173
		ValueFromText,
		// Token: 0x040000AE RID: 174
		DateTimeFrom,
		// Token: 0x040000AF RID: 175
		DateTimeToText,
		// Token: 0x040000B0 RID: 176
		TablePartitionValues,
		// Token: 0x040000B1 RID: 177
		LaxOrdinalIgnoreCaseComparer,
		// Token: 0x040000B2 RID: 178
		TableRemoveColumns,
		// Token: 0x040000B3 RID: 179
		TableRenameColumns,
		// Token: 0x040000B4 RID: 180
		TableReorderColumns,
		// Token: 0x040000B5 RID: 181
		TypeIs,
		// Token: 0x040000B6 RID: 182
		ValueAndTypeFromText,
		// Token: 0x040000B7 RID: 183
		AddBinaryOperator,
		// Token: 0x040000B8 RID: 184
		SubtractBinaryOperator,
		// Token: 0x040000B9 RID: 185
		MultiplyBinaryOperator,
		// Token: 0x040000BA RID: 186
		DivideBinaryOperator,
		// Token: 0x040000BB RID: 187
		ModBinaryOperator,
		// Token: 0x040000BC RID: 188
		NumberIntegerDivide,
		// Token: 0x040000BD RID: 189
		DateTimeZoneFrom,
		// Token: 0x040000BE RID: 190
		DateTimeZoneToText,
		// Token: 0x040000BF RID: 191
		TextFrom,
		// Token: 0x040000C0 RID: 192
		TextTrim,
		// Token: 0x040000C1 RID: 193
		TextUpper,
		// Token: 0x040000C2 RID: 194
		TextLower,
		// Token: 0x040000C3 RID: 195
		TextMiddle,
		// Token: 0x040000C4 RID: 196
		TextSplit,
		// Token: 0x040000C5 RID: 197
		TextPositionOf,
		// Token: 0x040000C6 RID: 198
		TextLength,
		// Token: 0x040000C7 RID: 199
		ConcatenateBinaryOperator,
		// Token: 0x040000C8 RID: 200
		TextReplace,
		// Token: 0x040000C9 RID: 201
		TextProper,
		// Token: 0x040000CA RID: 202
		DateDay,
		// Token: 0x040000CB RID: 203
		DateDayOfWeek,
		// Token: 0x040000CC RID: 204
		DateDayOfYear,
		// Token: 0x040000CD RID: 205
		DateMonth,
		// Token: 0x040000CE RID: 206
		DateQuarterOfYear,
		// Token: 0x040000CF RID: 207
		DateWeekOfMonth,
		// Token: 0x040000D0 RID: 208
		DateWeekOfYear,
		// Token: 0x040000D1 RID: 209
		DateYear,
		// Token: 0x040000D2 RID: 210
		TimeHour,
		// Token: 0x040000D3 RID: 211
		TimeMinute,
		// Token: 0x040000D4 RID: 212
		TimeSecond,
		// Token: 0x040000D5 RID: 213
		TimeFrom,
		// Token: 0x040000D6 RID: 214
		DateFrom,
		// Token: 0x040000D7 RID: 215
		DateToText,
		// Token: 0x040000D8 RID: 216
		TextCombine,
		// Token: 0x040000D9 RID: 217
		JsonFromValue,
		// Token: 0x040000DA RID: 218
		TextFromBinary,
		// Token: 0x040000DB RID: 219
		TextEnd,
		// Token: 0x040000DC RID: 220
		TextStart,
		// Token: 0x040000DD RID: 221
		TextAfterDelimiter,
		// Token: 0x040000DE RID: 222
		TextBeforeDelimiter,
		// Token: 0x040000DF RID: 223
		TextBetweenDelimiters,
		// Token: 0x040000E0 RID: 224
		NumberFrom,
		// Token: 0x040000E1 RID: 225
		TextClean,
		// Token: 0x040000E2 RID: 226
		DateDaysInMonth,
		// Token: 0x040000E3 RID: 227
		DateStartOfDay,
		// Token: 0x040000E4 RID: 228
		DateEndOfDay,
		// Token: 0x040000E5 RID: 229
		DateStartOfMonth,
		// Token: 0x040000E6 RID: 230
		DateEndOfMonth,
		// Token: 0x040000E7 RID: 231
		DateStartOfQuarter,
		// Token: 0x040000E8 RID: 232
		DateEndOfQuarter,
		// Token: 0x040000E9 RID: 233
		DateStartOfWeek,
		// Token: 0x040000EA RID: 234
		DateEndOfWeek,
		// Token: 0x040000EB RID: 235
		DateStartOfYear,
		// Token: 0x040000EC RID: 236
		DateEndOfYear,
		// Token: 0x040000ED RID: 237
		TimeStartOfHour,
		// Token: 0x040000EE RID: 238
		TimeEndOfHour,
		// Token: 0x040000EF RID: 239
		DateTimeLocalNow,
		// Token: 0x040000F0 RID: 240
		DateDayOfWeekName,
		// Token: 0x040000F1 RID: 241
		DateMonthName,
		// Token: 0x040000F2 RID: 242
		DateTimeZoneToLocal,
		// Token: 0x040000F3 RID: 243
		NumberAbs,
		// Token: 0x040000F4 RID: 244
		NumberAcos,
		// Token: 0x040000F5 RID: 245
		NumberAsin,
		// Token: 0x040000F6 RID: 246
		NumberAtan,
		// Token: 0x040000F7 RID: 247
		NumberCos,
		// Token: 0x040000F8 RID: 248
		NumberExp,
		// Token: 0x040000F9 RID: 249
		NumberFactorial,
		// Token: 0x040000FA RID: 250
		NumberIsEven,
		// Token: 0x040000FB RID: 251
		NumberIsOdd,
		// Token: 0x040000FC RID: 252
		NumberLn,
		// Token: 0x040000FD RID: 253
		NumberLog10,
		// Token: 0x040000FE RID: 254
		NumberPower,
		// Token: 0x040000FF RID: 255
		NumberRoundDown,
		// Token: 0x04000100 RID: 256
		NumberRoundUp,
		// Token: 0x04000101 RID: 257
		NumberSign,
		// Token: 0x04000102 RID: 258
		NumberSin,
		// Token: 0x04000103 RID: 259
		NumberSqrt,
		// Token: 0x04000104 RID: 260
		NumberTan,
		// Token: 0x04000105 RID: 261
		ListSum,
		// Token: 0x04000106 RID: 262
		ListProduct,
		// Token: 0x04000107 RID: 263
		NumberMod,
		// Token: 0x04000108 RID: 264
		ListAverage,
		// Token: 0x04000109 RID: 265
		ListMax,
		// Token: 0x0400010A RID: 266
		ListMedian,
		// Token: 0x0400010B RID: 267
		ListMin,
		// Token: 0x0400010C RID: 268
		ListStandardDeviation,
		// Token: 0x0400010D RID: 269
		NumberRound,
		// Token: 0x0400010E RID: 270
		ListNonNullCount,
		// Token: 0x0400010F RID: 271
		DurationDays,
		// Token: 0x04000110 RID: 272
		ValueCompare,
		// Token: 0x04000111 RID: 273
		TextRemove,
		// Token: 0x04000112 RID: 274
		TextSelect,
		// Token: 0x04000113 RID: 275
		ResourceAccess,
		// Token: 0x04000114 RID: 276
		Date,
		// Token: 0x04000115 RID: 277
		TextFormat
	}
}
