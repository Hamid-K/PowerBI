using System;
using System.Collections.Generic;
using System.Data.Entity.Resources;

namespace System.Data.Entity.Core.Objects
{
	// Token: 0x02000407 RID: 1031
	[Obsolete("This class has been replaced by System.Data.Entity.DbFunctions.")]
	public static class EntityFunctions
	{
		// Token: 0x06003090 RID: 12432 RVA: 0x0009BE94 File Offset: 0x0009A094
		[DbFunction("Edm", "StDev")]
		public static double? StandardDeviation(IEnumerable<decimal> collection)
		{
			return DbFunctions.StandardDeviation(collection);
		}

		// Token: 0x06003091 RID: 12433 RVA: 0x0009BE9C File Offset: 0x0009A09C
		[DbFunction("Edm", "StDev")]
		public static double? StandardDeviation(IEnumerable<decimal?> collection)
		{
			return DbFunctions.StandardDeviation(collection);
		}

		// Token: 0x06003092 RID: 12434 RVA: 0x0009BEA4 File Offset: 0x0009A0A4
		[DbFunction("Edm", "StDev")]
		public static double? StandardDeviation(IEnumerable<double> collection)
		{
			return DbFunctions.StandardDeviation(collection);
		}

		// Token: 0x06003093 RID: 12435 RVA: 0x0009BEAC File Offset: 0x0009A0AC
		[DbFunction("Edm", "StDev")]
		public static double? StandardDeviation(IEnumerable<double?> collection)
		{
			return DbFunctions.StandardDeviation(collection);
		}

		// Token: 0x06003094 RID: 12436 RVA: 0x0009BEB4 File Offset: 0x0009A0B4
		[DbFunction("Edm", "StDev")]
		public static double? StandardDeviation(IEnumerable<int> collection)
		{
			return DbFunctions.StandardDeviation(collection);
		}

		// Token: 0x06003095 RID: 12437 RVA: 0x0009BEBC File Offset: 0x0009A0BC
		[DbFunction("Edm", "StDev")]
		public static double? StandardDeviation(IEnumerable<int?> collection)
		{
			return DbFunctions.StandardDeviation(collection);
		}

		// Token: 0x06003096 RID: 12438 RVA: 0x0009BEC4 File Offset: 0x0009A0C4
		[DbFunction("Edm", "StDev")]
		public static double? StandardDeviation(IEnumerable<long> collection)
		{
			return DbFunctions.StandardDeviation(collection);
		}

		// Token: 0x06003097 RID: 12439 RVA: 0x0009BECC File Offset: 0x0009A0CC
		[DbFunction("Edm", "StDev")]
		public static double? StandardDeviation(IEnumerable<long?> collection)
		{
			return DbFunctions.StandardDeviation(collection);
		}

		// Token: 0x06003098 RID: 12440 RVA: 0x0009BED4 File Offset: 0x0009A0D4
		[DbFunction("Edm", "StDevP")]
		public static double? StandardDeviationP(IEnumerable<decimal> collection)
		{
			return DbFunctions.StandardDeviationP(collection);
		}

		// Token: 0x06003099 RID: 12441 RVA: 0x0009BEDC File Offset: 0x0009A0DC
		[DbFunction("Edm", "StDevP")]
		public static double? StandardDeviationP(IEnumerable<decimal?> collection)
		{
			return DbFunctions.StandardDeviationP(collection);
		}

		// Token: 0x0600309A RID: 12442 RVA: 0x0009BEE4 File Offset: 0x0009A0E4
		[DbFunction("Edm", "StDevP")]
		public static double? StandardDeviationP(IEnumerable<double> collection)
		{
			return DbFunctions.StandardDeviationP(collection);
		}

		// Token: 0x0600309B RID: 12443 RVA: 0x0009BEEC File Offset: 0x0009A0EC
		[DbFunction("Edm", "StDevP")]
		public static double? StandardDeviationP(IEnumerable<double?> collection)
		{
			return DbFunctions.StandardDeviationP(collection);
		}

		// Token: 0x0600309C RID: 12444 RVA: 0x0009BEF4 File Offset: 0x0009A0F4
		[DbFunction("Edm", "StDevP")]
		public static double? StandardDeviationP(IEnumerable<int> collection)
		{
			return DbFunctions.StandardDeviationP(collection);
		}

		// Token: 0x0600309D RID: 12445 RVA: 0x0009BEFC File Offset: 0x0009A0FC
		[DbFunction("Edm", "StDevP")]
		public static double? StandardDeviationP(IEnumerable<int?> collection)
		{
			return DbFunctions.StandardDeviationP(collection);
		}

		// Token: 0x0600309E RID: 12446 RVA: 0x0009BF04 File Offset: 0x0009A104
		[DbFunction("Edm", "StDevP")]
		public static double? StandardDeviationP(IEnumerable<long> collection)
		{
			return DbFunctions.StandardDeviationP(collection);
		}

		// Token: 0x0600309F RID: 12447 RVA: 0x0009BF0C File Offset: 0x0009A10C
		[DbFunction("Edm", "StDevP")]
		public static double? StandardDeviationP(IEnumerable<long?> collection)
		{
			return DbFunctions.StandardDeviationP(collection);
		}

		// Token: 0x060030A0 RID: 12448 RVA: 0x0009BF14 File Offset: 0x0009A114
		[DbFunction("Edm", "Var")]
		public static double? Var(IEnumerable<decimal> collection)
		{
			return DbFunctions.Var(collection);
		}

		// Token: 0x060030A1 RID: 12449 RVA: 0x0009BF1C File Offset: 0x0009A11C
		[DbFunction("Edm", "Var")]
		public static double? Var(IEnumerable<decimal?> collection)
		{
			return DbFunctions.Var(collection);
		}

		// Token: 0x060030A2 RID: 12450 RVA: 0x0009BF24 File Offset: 0x0009A124
		[DbFunction("Edm", "Var")]
		public static double? Var(IEnumerable<double> collection)
		{
			return DbFunctions.Var(collection);
		}

		// Token: 0x060030A3 RID: 12451 RVA: 0x0009BF2C File Offset: 0x0009A12C
		[DbFunction("Edm", "Var")]
		public static double? Var(IEnumerable<double?> collection)
		{
			return DbFunctions.Var(collection);
		}

		// Token: 0x060030A4 RID: 12452 RVA: 0x0009BF34 File Offset: 0x0009A134
		[DbFunction("Edm", "Var")]
		public static double? Var(IEnumerable<int> collection)
		{
			return DbFunctions.Var(collection);
		}

		// Token: 0x060030A5 RID: 12453 RVA: 0x0009BF3C File Offset: 0x0009A13C
		[DbFunction("Edm", "Var")]
		public static double? Var(IEnumerable<int?> collection)
		{
			return DbFunctions.Var(collection);
		}

		// Token: 0x060030A6 RID: 12454 RVA: 0x0009BF44 File Offset: 0x0009A144
		[DbFunction("Edm", "Var")]
		public static double? Var(IEnumerable<long> collection)
		{
			return DbFunctions.Var(collection);
		}

		// Token: 0x060030A7 RID: 12455 RVA: 0x0009BF4C File Offset: 0x0009A14C
		[DbFunction("Edm", "Var")]
		public static double? Var(IEnumerable<long?> collection)
		{
			return DbFunctions.Var(collection);
		}

		// Token: 0x060030A8 RID: 12456 RVA: 0x0009BF54 File Offset: 0x0009A154
		[DbFunction("Edm", "VarP")]
		public static double? VarP(IEnumerable<decimal> collection)
		{
			return DbFunctions.VarP(collection);
		}

		// Token: 0x060030A9 RID: 12457 RVA: 0x0009BF5C File Offset: 0x0009A15C
		[DbFunction("Edm", "VarP")]
		public static double? VarP(IEnumerable<decimal?> collection)
		{
			return DbFunctions.VarP(collection);
		}

		// Token: 0x060030AA RID: 12458 RVA: 0x0009BF64 File Offset: 0x0009A164
		[DbFunction("Edm", "VarP")]
		public static double? VarP(IEnumerable<double> collection)
		{
			return DbFunctions.VarP(collection);
		}

		// Token: 0x060030AB RID: 12459 RVA: 0x0009BF6C File Offset: 0x0009A16C
		[DbFunction("Edm", "VarP")]
		public static double? VarP(IEnumerable<double?> collection)
		{
			return DbFunctions.VarP(collection);
		}

		// Token: 0x060030AC RID: 12460 RVA: 0x0009BF74 File Offset: 0x0009A174
		[DbFunction("Edm", "VarP")]
		public static double? VarP(IEnumerable<int> collection)
		{
			return DbFunctions.VarP(collection);
		}

		// Token: 0x060030AD RID: 12461 RVA: 0x0009BF7C File Offset: 0x0009A17C
		[DbFunction("Edm", "VarP")]
		public static double? VarP(IEnumerable<int?> collection)
		{
			return DbFunctions.VarP(collection);
		}

		// Token: 0x060030AE RID: 12462 RVA: 0x0009BF84 File Offset: 0x0009A184
		[DbFunction("Edm", "VarP")]
		public static double? VarP(IEnumerable<long> collection)
		{
			return DbFunctions.VarP(collection);
		}

		// Token: 0x060030AF RID: 12463 RVA: 0x0009BF8C File Offset: 0x0009A18C
		[DbFunction("Edm", "VarP")]
		public static double? VarP(IEnumerable<long?> collection)
		{
			return DbFunctions.VarP(collection);
		}

		// Token: 0x060030B0 RID: 12464 RVA: 0x0009BF94 File Offset: 0x0009A194
		[DbFunction("Edm", "Left")]
		public static string Left(string stringArgument, long? length)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060030B1 RID: 12465 RVA: 0x0009BFA0 File Offset: 0x0009A1A0
		[DbFunction("Edm", "Right")]
		public static string Right(string stringArgument, long? length)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060030B2 RID: 12466 RVA: 0x0009BFAC File Offset: 0x0009A1AC
		[DbFunction("Edm", "Reverse")]
		public static string Reverse(string stringArgument)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060030B3 RID: 12467 RVA: 0x0009BFB8 File Offset: 0x0009A1B8
		[DbFunction("Edm", "GetTotalOffsetMinutes")]
		public static int? GetTotalOffsetMinutes(DateTimeOffset? dateTimeOffsetArgument)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060030B4 RID: 12468 RVA: 0x0009BFC4 File Offset: 0x0009A1C4
		[DbFunction("Edm", "TruncateTime")]
		public static DateTimeOffset? TruncateTime(DateTimeOffset? dateValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060030B5 RID: 12469 RVA: 0x0009BFD0 File Offset: 0x0009A1D0
		[DbFunction("Edm", "TruncateTime")]
		public static DateTime? TruncateTime(DateTime? dateValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060030B6 RID: 12470 RVA: 0x0009BFDC File Offset: 0x0009A1DC
		[DbFunction("Edm", "CreateDateTime")]
		public static DateTime? CreateDateTime(int? year, int? month, int? day, int? hour, int? minute, double? second)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060030B7 RID: 12471 RVA: 0x0009BFE8 File Offset: 0x0009A1E8
		[DbFunction("Edm", "CreateDateTimeOffset")]
		public static DateTimeOffset? CreateDateTimeOffset(int? year, int? month, int? day, int? hour, int? minute, double? second, int? timeZoneOffset)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060030B8 RID: 12472 RVA: 0x0009BFF4 File Offset: 0x0009A1F4
		[DbFunction("Edm", "CreateTime")]
		public static TimeSpan? CreateTime(int? hour, int? minute, double? second)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060030B9 RID: 12473 RVA: 0x0009C000 File Offset: 0x0009A200
		[DbFunction("Edm", "AddYears")]
		public static DateTimeOffset? AddYears(DateTimeOffset? dateValue, int? addValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060030BA RID: 12474 RVA: 0x0009C00C File Offset: 0x0009A20C
		[DbFunction("Edm", "AddYears")]
		public static DateTime? AddYears(DateTime? dateValue, int? addValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060030BB RID: 12475 RVA: 0x0009C018 File Offset: 0x0009A218
		[DbFunction("Edm", "AddMonths")]
		public static DateTimeOffset? AddMonths(DateTimeOffset? dateValue, int? addValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060030BC RID: 12476 RVA: 0x0009C024 File Offset: 0x0009A224
		[DbFunction("Edm", "AddMonths")]
		public static DateTime? AddMonths(DateTime? dateValue, int? addValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060030BD RID: 12477 RVA: 0x0009C030 File Offset: 0x0009A230
		[DbFunction("Edm", "AddDays")]
		public static DateTimeOffset? AddDays(DateTimeOffset? dateValue, int? addValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060030BE RID: 12478 RVA: 0x0009C03C File Offset: 0x0009A23C
		[DbFunction("Edm", "AddDays")]
		public static DateTime? AddDays(DateTime? dateValue, int? addValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060030BF RID: 12479 RVA: 0x0009C048 File Offset: 0x0009A248
		[DbFunction("Edm", "AddHours")]
		public static DateTimeOffset? AddHours(DateTimeOffset? timeValue, int? addValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060030C0 RID: 12480 RVA: 0x0009C054 File Offset: 0x0009A254
		[DbFunction("Edm", "AddHours")]
		public static DateTime? AddHours(DateTime? timeValue, int? addValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060030C1 RID: 12481 RVA: 0x0009C060 File Offset: 0x0009A260
		[DbFunction("Edm", "AddHours")]
		public static TimeSpan? AddHours(TimeSpan? timeValue, int? addValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060030C2 RID: 12482 RVA: 0x0009C06C File Offset: 0x0009A26C
		[DbFunction("Edm", "AddMinutes")]
		public static DateTimeOffset? AddMinutes(DateTimeOffset? timeValue, int? addValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060030C3 RID: 12483 RVA: 0x0009C078 File Offset: 0x0009A278
		[DbFunction("Edm", "AddMinutes")]
		public static DateTime? AddMinutes(DateTime? timeValue, int? addValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060030C4 RID: 12484 RVA: 0x0009C084 File Offset: 0x0009A284
		[DbFunction("Edm", "AddMinutes")]
		public static TimeSpan? AddMinutes(TimeSpan? timeValue, int? addValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060030C5 RID: 12485 RVA: 0x0009C090 File Offset: 0x0009A290
		[DbFunction("Edm", "AddSeconds")]
		public static DateTimeOffset? AddSeconds(DateTimeOffset? timeValue, int? addValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060030C6 RID: 12486 RVA: 0x0009C09C File Offset: 0x0009A29C
		[DbFunction("Edm", "AddSeconds")]
		public static DateTime? AddSeconds(DateTime? timeValue, int? addValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060030C7 RID: 12487 RVA: 0x0009C0A8 File Offset: 0x0009A2A8
		[DbFunction("Edm", "AddSeconds")]
		public static TimeSpan? AddSeconds(TimeSpan? timeValue, int? addValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060030C8 RID: 12488 RVA: 0x0009C0B4 File Offset: 0x0009A2B4
		[DbFunction("Edm", "AddMilliseconds")]
		public static DateTimeOffset? AddMilliseconds(DateTimeOffset? timeValue, int? addValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060030C9 RID: 12489 RVA: 0x0009C0C0 File Offset: 0x0009A2C0
		[DbFunction("Edm", "AddMilliseconds")]
		public static DateTime? AddMilliseconds(DateTime? timeValue, int? addValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060030CA RID: 12490 RVA: 0x0009C0CC File Offset: 0x0009A2CC
		[DbFunction("Edm", "AddMilliseconds")]
		public static TimeSpan? AddMilliseconds(TimeSpan? timeValue, int? addValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060030CB RID: 12491 RVA: 0x0009C0D8 File Offset: 0x0009A2D8
		[DbFunction("Edm", "AddMicroseconds")]
		public static DateTimeOffset? AddMicroseconds(DateTimeOffset? timeValue, int? addValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060030CC RID: 12492 RVA: 0x0009C0E4 File Offset: 0x0009A2E4
		[DbFunction("Edm", "AddMicroseconds")]
		public static DateTime? AddMicroseconds(DateTime? timeValue, int? addValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060030CD RID: 12493 RVA: 0x0009C0F0 File Offset: 0x0009A2F0
		[DbFunction("Edm", "AddMicroseconds")]
		public static TimeSpan? AddMicroseconds(TimeSpan? timeValue, int? addValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060030CE RID: 12494 RVA: 0x0009C0FC File Offset: 0x0009A2FC
		[DbFunction("Edm", "AddNanoseconds")]
		public static DateTimeOffset? AddNanoseconds(DateTimeOffset? timeValue, int? addValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060030CF RID: 12495 RVA: 0x0009C108 File Offset: 0x0009A308
		[DbFunction("Edm", "AddNanoseconds")]
		public static DateTime? AddNanoseconds(DateTime? timeValue, int? addValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060030D0 RID: 12496 RVA: 0x0009C114 File Offset: 0x0009A314
		[DbFunction("Edm", "AddNanoseconds")]
		public static TimeSpan? AddNanoseconds(TimeSpan? timeValue, int? addValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060030D1 RID: 12497 RVA: 0x0009C120 File Offset: 0x0009A320
		[DbFunction("Edm", "DiffYears")]
		public static int? DiffYears(DateTimeOffset? dateValue1, DateTimeOffset? dateValue2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060030D2 RID: 12498 RVA: 0x0009C12C File Offset: 0x0009A32C
		[DbFunction("Edm", "DiffYears")]
		public static int? DiffYears(DateTime? dateValue1, DateTime? dateValue2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060030D3 RID: 12499 RVA: 0x0009C138 File Offset: 0x0009A338
		[DbFunction("Edm", "DiffMonths")]
		public static int? DiffMonths(DateTimeOffset? dateValue1, DateTimeOffset? dateValue2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060030D4 RID: 12500 RVA: 0x0009C144 File Offset: 0x0009A344
		[DbFunction("Edm", "DiffMonths")]
		public static int? DiffMonths(DateTime? dateValue1, DateTime? dateValue2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060030D5 RID: 12501 RVA: 0x0009C150 File Offset: 0x0009A350
		[DbFunction("Edm", "DiffDays")]
		public static int? DiffDays(DateTimeOffset? dateValue1, DateTimeOffset? dateValue2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060030D6 RID: 12502 RVA: 0x0009C15C File Offset: 0x0009A35C
		[DbFunction("Edm", "DiffDays")]
		public static int? DiffDays(DateTime? dateValue1, DateTime? dateValue2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060030D7 RID: 12503 RVA: 0x0009C168 File Offset: 0x0009A368
		[DbFunction("Edm", "DiffHours")]
		public static int? DiffHours(DateTimeOffset? timeValue1, DateTimeOffset? timeValue2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060030D8 RID: 12504 RVA: 0x0009C174 File Offset: 0x0009A374
		[DbFunction("Edm", "DiffHours")]
		public static int? DiffHours(DateTime? timeValue1, DateTime? timeValue2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060030D9 RID: 12505 RVA: 0x0009C180 File Offset: 0x0009A380
		[DbFunction("Edm", "DiffHours")]
		public static int? DiffHours(TimeSpan? timeValue1, TimeSpan? timeValue2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060030DA RID: 12506 RVA: 0x0009C18C File Offset: 0x0009A38C
		[DbFunction("Edm", "DiffMinutes")]
		public static int? DiffMinutes(DateTimeOffset? timeValue1, DateTimeOffset? timeValue2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060030DB RID: 12507 RVA: 0x0009C198 File Offset: 0x0009A398
		[DbFunction("Edm", "DiffMinutes")]
		public static int? DiffMinutes(DateTime? timeValue1, DateTime? timeValue2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060030DC RID: 12508 RVA: 0x0009C1A4 File Offset: 0x0009A3A4
		[DbFunction("Edm", "DiffMinutes")]
		public static int? DiffMinutes(TimeSpan? timeValue1, TimeSpan? timeValue2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060030DD RID: 12509 RVA: 0x0009C1B0 File Offset: 0x0009A3B0
		[DbFunction("Edm", "DiffSeconds")]
		public static int? DiffSeconds(DateTimeOffset? timeValue1, DateTimeOffset? timeValue2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060030DE RID: 12510 RVA: 0x0009C1BC File Offset: 0x0009A3BC
		[DbFunction("Edm", "DiffSeconds")]
		public static int? DiffSeconds(DateTime? timeValue1, DateTime? timeValue2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060030DF RID: 12511 RVA: 0x0009C1C8 File Offset: 0x0009A3C8
		[DbFunction("Edm", "DiffSeconds")]
		public static int? DiffSeconds(TimeSpan? timeValue1, TimeSpan? timeValue2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060030E0 RID: 12512 RVA: 0x0009C1D4 File Offset: 0x0009A3D4
		[DbFunction("Edm", "DiffMilliseconds")]
		public static int? DiffMilliseconds(DateTimeOffset? timeValue1, DateTimeOffset? timeValue2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060030E1 RID: 12513 RVA: 0x0009C1E0 File Offset: 0x0009A3E0
		[DbFunction("Edm", "DiffMilliseconds")]
		public static int? DiffMilliseconds(DateTime? timeValue1, DateTime? timeValue2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060030E2 RID: 12514 RVA: 0x0009C1EC File Offset: 0x0009A3EC
		[DbFunction("Edm", "DiffMilliseconds")]
		public static int? DiffMilliseconds(TimeSpan? timeValue1, TimeSpan? timeValue2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060030E3 RID: 12515 RVA: 0x0009C1F8 File Offset: 0x0009A3F8
		[DbFunction("Edm", "DiffMicroseconds")]
		public static int? DiffMicroseconds(DateTimeOffset? timeValue1, DateTimeOffset? timeValue2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060030E4 RID: 12516 RVA: 0x0009C204 File Offset: 0x0009A404
		[DbFunction("Edm", "DiffMicroseconds")]
		public static int? DiffMicroseconds(DateTime? timeValue1, DateTime? timeValue2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060030E5 RID: 12517 RVA: 0x0009C210 File Offset: 0x0009A410
		[DbFunction("Edm", "DiffMicroseconds")]
		public static int? DiffMicroseconds(TimeSpan? timeValue1, TimeSpan? timeValue2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060030E6 RID: 12518 RVA: 0x0009C21C File Offset: 0x0009A41C
		[DbFunction("Edm", "DiffNanoseconds")]
		public static int? DiffNanoseconds(DateTimeOffset? timeValue1, DateTimeOffset? timeValue2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060030E7 RID: 12519 RVA: 0x0009C228 File Offset: 0x0009A428
		[DbFunction("Edm", "DiffNanoseconds")]
		public static int? DiffNanoseconds(DateTime? timeValue1, DateTime? timeValue2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060030E8 RID: 12520 RVA: 0x0009C234 File Offset: 0x0009A434
		[DbFunction("Edm", "DiffNanoseconds")]
		public static int? DiffNanoseconds(TimeSpan? timeValue1, TimeSpan? timeValue2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060030E9 RID: 12521 RVA: 0x0009C240 File Offset: 0x0009A440
		[DbFunction("Edm", "Truncate")]
		public static double? Truncate(double? value, int? digits)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060030EA RID: 12522 RVA: 0x0009C24C File Offset: 0x0009A44C
		[DbFunction("Edm", "Truncate")]
		public static decimal? Truncate(decimal? value, int? digits)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060030EB RID: 12523 RVA: 0x0009C258 File Offset: 0x0009A458
		public static bool Like(string searchString, string likeExpression)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060030EC RID: 12524 RVA: 0x0009C264 File Offset: 0x0009A464
		public static bool Like(string searchString, string likeExpression, string escapeCharacter)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060030ED RID: 12525 RVA: 0x0009C270 File Offset: 0x0009A470
		public static string AsUnicode(string value)
		{
			return value;
		}

		// Token: 0x060030EE RID: 12526 RVA: 0x0009C273 File Offset: 0x0009A473
		public static string AsNonUnicode(string value)
		{
			return value;
		}
	}
}
