using System;
using System.Collections.Generic;
using System.Data.Entity.Resources;
using System.Linq;
using System.Linq.Expressions;

namespace System.Data.Entity
{
	// Token: 0x0200005E RID: 94
	public static class DbFunctions
	{
		// Token: 0x060002AC RID: 684 RVA: 0x0000A620 File Offset: 0x00008820
		[DbFunction("Edm", "StDev")]
		public static double? StandardDeviation(IEnumerable<decimal> collection)
		{
			return DbFunctions.BootstrapFunction<decimal, double?>((IEnumerable<decimal> c) => DbFunctions.StandardDeviation(c), collection);
		}

		// Token: 0x060002AD RID: 685 RVA: 0x0000A678 File Offset: 0x00008878
		[DbFunction("Edm", "StDev")]
		public static double? StandardDeviation(IEnumerable<decimal?> collection)
		{
			return DbFunctions.BootstrapFunction<decimal?, double?>((IEnumerable<decimal?> c) => DbFunctions.StandardDeviation(c), collection);
		}

		// Token: 0x060002AE RID: 686 RVA: 0x0000A6D0 File Offset: 0x000088D0
		[DbFunction("Edm", "StDev")]
		public static double? StandardDeviation(IEnumerable<double> collection)
		{
			return DbFunctions.BootstrapFunction<double, double?>((IEnumerable<double> c) => DbFunctions.StandardDeviation(c), collection);
		}

		// Token: 0x060002AF RID: 687 RVA: 0x0000A728 File Offset: 0x00008928
		[DbFunction("Edm", "StDev")]
		public static double? StandardDeviation(IEnumerable<double?> collection)
		{
			return DbFunctions.BootstrapFunction<double?, double?>((IEnumerable<double?> c) => DbFunctions.StandardDeviation(c), collection);
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x0000A780 File Offset: 0x00008980
		[DbFunction("Edm", "StDev")]
		public static double? StandardDeviation(IEnumerable<int> collection)
		{
			return DbFunctions.BootstrapFunction<int, double?>((IEnumerable<int> c) => DbFunctions.StandardDeviation(c), collection);
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x0000A7D8 File Offset: 0x000089D8
		[DbFunction("Edm", "StDev")]
		public static double? StandardDeviation(IEnumerable<int?> collection)
		{
			return DbFunctions.BootstrapFunction<int?, double?>((IEnumerable<int?> c) => DbFunctions.StandardDeviation(c), collection);
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x0000A830 File Offset: 0x00008A30
		[DbFunction("Edm", "StDev")]
		public static double? StandardDeviation(IEnumerable<long> collection)
		{
			return DbFunctions.BootstrapFunction<long, double?>((IEnumerable<long> c) => DbFunctions.StandardDeviation(c), collection);
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x0000A888 File Offset: 0x00008A88
		[DbFunction("Edm", "StDev")]
		public static double? StandardDeviation(IEnumerable<long?> collection)
		{
			return DbFunctions.BootstrapFunction<long?, double?>((IEnumerable<long?> c) => DbFunctions.StandardDeviation(c), collection);
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x0000A8E0 File Offset: 0x00008AE0
		[DbFunction("Edm", "StDevP")]
		public static double? StandardDeviationP(IEnumerable<decimal> collection)
		{
			return DbFunctions.BootstrapFunction<decimal, double?>((IEnumerable<decimal> c) => DbFunctions.StandardDeviationP(c), collection);
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x0000A938 File Offset: 0x00008B38
		[DbFunction("Edm", "StDevP")]
		public static double? StandardDeviationP(IEnumerable<decimal?> collection)
		{
			return DbFunctions.BootstrapFunction<decimal?, double?>((IEnumerable<decimal?> c) => DbFunctions.StandardDeviationP(c), collection);
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x0000A990 File Offset: 0x00008B90
		[DbFunction("Edm", "StDevP")]
		public static double? StandardDeviationP(IEnumerable<double> collection)
		{
			return DbFunctions.BootstrapFunction<double, double?>((IEnumerable<double> c) => DbFunctions.StandardDeviationP(c), collection);
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x0000A9E8 File Offset: 0x00008BE8
		[DbFunction("Edm", "StDevP")]
		public static double? StandardDeviationP(IEnumerable<double?> collection)
		{
			return DbFunctions.BootstrapFunction<double?, double?>((IEnumerable<double?> c) => DbFunctions.StandardDeviationP(c), collection);
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x0000AA40 File Offset: 0x00008C40
		[DbFunction("Edm", "StDevP")]
		public static double? StandardDeviationP(IEnumerable<int> collection)
		{
			return DbFunctions.BootstrapFunction<int, double?>((IEnumerable<int> c) => DbFunctions.StandardDeviationP(c), collection);
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x0000AA98 File Offset: 0x00008C98
		[DbFunction("Edm", "StDevP")]
		public static double? StandardDeviationP(IEnumerable<int?> collection)
		{
			return DbFunctions.BootstrapFunction<int?, double?>((IEnumerable<int?> c) => DbFunctions.StandardDeviationP(c), collection);
		}

		// Token: 0x060002BA RID: 698 RVA: 0x0000AAF0 File Offset: 0x00008CF0
		[DbFunction("Edm", "StDevP")]
		public static double? StandardDeviationP(IEnumerable<long> collection)
		{
			return DbFunctions.BootstrapFunction<long, double?>((IEnumerable<long> c) => DbFunctions.StandardDeviationP(c), collection);
		}

		// Token: 0x060002BB RID: 699 RVA: 0x0000AB48 File Offset: 0x00008D48
		[DbFunction("Edm", "StDevP")]
		public static double? StandardDeviationP(IEnumerable<long?> collection)
		{
			return DbFunctions.BootstrapFunction<long?, double?>((IEnumerable<long?> c) => DbFunctions.StandardDeviationP(c), collection);
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0000ABA0 File Offset: 0x00008DA0
		[DbFunction("Edm", "Var")]
		public static double? Var(IEnumerable<decimal> collection)
		{
			return DbFunctions.BootstrapFunction<decimal, double?>((IEnumerable<decimal> c) => DbFunctions.Var(c), collection);
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0000ABF8 File Offset: 0x00008DF8
		[DbFunction("Edm", "Var")]
		public static double? Var(IEnumerable<decimal?> collection)
		{
			return DbFunctions.BootstrapFunction<decimal?, double?>((IEnumerable<decimal?> c) => DbFunctions.Var(c), collection);
		}

		// Token: 0x060002BE RID: 702 RVA: 0x0000AC50 File Offset: 0x00008E50
		[DbFunction("Edm", "Var")]
		public static double? Var(IEnumerable<double> collection)
		{
			return DbFunctions.BootstrapFunction<double, double?>((IEnumerable<double> c) => DbFunctions.Var(c), collection);
		}

		// Token: 0x060002BF RID: 703 RVA: 0x0000ACA8 File Offset: 0x00008EA8
		[DbFunction("Edm", "Var")]
		public static double? Var(IEnumerable<double?> collection)
		{
			return DbFunctions.BootstrapFunction<double?, double?>((IEnumerable<double?> c) => DbFunctions.Var(c), collection);
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x0000AD00 File Offset: 0x00008F00
		[DbFunction("Edm", "Var")]
		public static double? Var(IEnumerable<int> collection)
		{
			return DbFunctions.BootstrapFunction<int, double?>((IEnumerable<int> c) => DbFunctions.Var(c), collection);
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x0000AD58 File Offset: 0x00008F58
		[DbFunction("Edm", "Var")]
		public static double? Var(IEnumerable<int?> collection)
		{
			return DbFunctions.BootstrapFunction<int?, double?>((IEnumerable<int?> c) => DbFunctions.Var(c), collection);
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x0000ADB0 File Offset: 0x00008FB0
		[DbFunction("Edm", "Var")]
		public static double? Var(IEnumerable<long> collection)
		{
			return DbFunctions.BootstrapFunction<long, double?>((IEnumerable<long> c) => DbFunctions.Var(c), collection);
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x0000AE08 File Offset: 0x00009008
		[DbFunction("Edm", "Var")]
		public static double? Var(IEnumerable<long?> collection)
		{
			return DbFunctions.BootstrapFunction<long?, double?>((IEnumerable<long?> c) => DbFunctions.Var(c), collection);
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0000AE60 File Offset: 0x00009060
		[DbFunction("Edm", "VarP")]
		public static double? VarP(IEnumerable<decimal> collection)
		{
			return DbFunctions.BootstrapFunction<decimal, double?>((IEnumerable<decimal> c) => DbFunctions.VarP(c), collection);
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x0000AEB8 File Offset: 0x000090B8
		[DbFunction("Edm", "VarP")]
		public static double? VarP(IEnumerable<decimal?> collection)
		{
			return DbFunctions.BootstrapFunction<decimal?, double?>((IEnumerable<decimal?> c) => DbFunctions.VarP(c), collection);
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x0000AF10 File Offset: 0x00009110
		[DbFunction("Edm", "VarP")]
		public static double? VarP(IEnumerable<double> collection)
		{
			return DbFunctions.BootstrapFunction<double, double?>((IEnumerable<double> c) => DbFunctions.VarP(c), collection);
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0000AF68 File Offset: 0x00009168
		[DbFunction("Edm", "VarP")]
		public static double? VarP(IEnumerable<double?> collection)
		{
			return DbFunctions.BootstrapFunction<double?, double?>((IEnumerable<double?> c) => DbFunctions.VarP(c), collection);
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0000AFC0 File Offset: 0x000091C0
		[DbFunction("Edm", "VarP")]
		public static double? VarP(IEnumerable<int> collection)
		{
			return DbFunctions.BootstrapFunction<int, double?>((IEnumerable<int> c) => DbFunctions.VarP(c), collection);
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x0000B018 File Offset: 0x00009218
		[DbFunction("Edm", "VarP")]
		public static double? VarP(IEnumerable<int?> collection)
		{
			return DbFunctions.BootstrapFunction<int?, double?>((IEnumerable<int?> c) => DbFunctions.VarP(c), collection);
		}

		// Token: 0x060002CA RID: 714 RVA: 0x0000B070 File Offset: 0x00009270
		[DbFunction("Edm", "VarP")]
		public static double? VarP(IEnumerable<long> collection)
		{
			return DbFunctions.BootstrapFunction<long, double?>((IEnumerable<long> c) => DbFunctions.VarP(c), collection);
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0000B0C8 File Offset: 0x000092C8
		[DbFunction("Edm", "VarP")]
		public static double? VarP(IEnumerable<long?> collection)
		{
			return DbFunctions.BootstrapFunction<long?, double?>((IEnumerable<long?> c) => DbFunctions.VarP(c), collection);
		}

		// Token: 0x060002CC RID: 716 RVA: 0x0000B11E File Offset: 0x0000931E
		[DbFunction("Edm", "Left")]
		public static string Left(string stringArgument, long? length)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060002CD RID: 717 RVA: 0x0000B12A File Offset: 0x0000932A
		[DbFunction("Edm", "Right")]
		public static string Right(string stringArgument, long? length)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060002CE RID: 718 RVA: 0x0000B136 File Offset: 0x00009336
		[DbFunction("Edm", "Reverse")]
		public static string Reverse(string stringArgument)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060002CF RID: 719 RVA: 0x0000B142 File Offset: 0x00009342
		[DbFunction("Edm", "GetTotalOffsetMinutes")]
		public static int? GetTotalOffsetMinutes(DateTimeOffset? dateTimeOffsetArgument)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x0000B14E File Offset: 0x0000934E
		[DbFunction("Edm", "TruncateTime")]
		public static DateTimeOffset? TruncateTime(DateTimeOffset? dateValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0000B15A File Offset: 0x0000935A
		[DbFunction("Edm", "TruncateTime")]
		public static DateTime? TruncateTime(DateTime? dateValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x0000B166 File Offset: 0x00009366
		[DbFunction("Edm", "CreateDateTime")]
		public static DateTime? CreateDateTime(int? year, int? month, int? day, int? hour, int? minute, double? second)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x0000B172 File Offset: 0x00009372
		[DbFunction("Edm", "CreateDateTimeOffset")]
		public static DateTimeOffset? CreateDateTimeOffset(int? year, int? month, int? day, int? hour, int? minute, double? second, int? timeZoneOffset)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x0000B17E File Offset: 0x0000937E
		[DbFunction("Edm", "CreateTime")]
		public static TimeSpan? CreateTime(int? hour, int? minute, double? second)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x0000B18A File Offset: 0x0000938A
		[DbFunction("Edm", "AddYears")]
		public static DateTimeOffset? AddYears(DateTimeOffset? dateValue, int? addValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x0000B196 File Offset: 0x00009396
		[DbFunction("Edm", "AddYears")]
		public static DateTime? AddYears(DateTime? dateValue, int? addValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x0000B1A2 File Offset: 0x000093A2
		[DbFunction("Edm", "AddMonths")]
		public static DateTimeOffset? AddMonths(DateTimeOffset? dateValue, int? addValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x0000B1AE File Offset: 0x000093AE
		[DbFunction("Edm", "AddMonths")]
		public static DateTime? AddMonths(DateTime? dateValue, int? addValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0000B1BA File Offset: 0x000093BA
		[DbFunction("Edm", "AddDays")]
		public static DateTimeOffset? AddDays(DateTimeOffset? dateValue, int? addValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060002DA RID: 730 RVA: 0x0000B1C6 File Offset: 0x000093C6
		[DbFunction("Edm", "AddDays")]
		public static DateTime? AddDays(DateTime? dateValue, int? addValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0000B1D2 File Offset: 0x000093D2
		[DbFunction("Edm", "AddHours")]
		public static DateTimeOffset? AddHours(DateTimeOffset? timeValue, int? addValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0000B1DE File Offset: 0x000093DE
		[DbFunction("Edm", "AddHours")]
		public static DateTime? AddHours(DateTime? timeValue, int? addValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0000B1EA File Offset: 0x000093EA
		[DbFunction("Edm", "AddHours")]
		public static TimeSpan? AddHours(TimeSpan? timeValue, int? addValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0000B1F6 File Offset: 0x000093F6
		[DbFunction("Edm", "AddMinutes")]
		public static DateTimeOffset? AddMinutes(DateTimeOffset? timeValue, int? addValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060002DF RID: 735 RVA: 0x0000B202 File Offset: 0x00009402
		[DbFunction("Edm", "AddMinutes")]
		public static DateTime? AddMinutes(DateTime? timeValue, int? addValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x0000B20E File Offset: 0x0000940E
		[DbFunction("Edm", "AddMinutes")]
		public static TimeSpan? AddMinutes(TimeSpan? timeValue, int? addValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x0000B21A File Offset: 0x0000941A
		[DbFunction("Edm", "AddSeconds")]
		public static DateTimeOffset? AddSeconds(DateTimeOffset? timeValue, int? addValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x0000B226 File Offset: 0x00009426
		[DbFunction("Edm", "AddSeconds")]
		public static DateTime? AddSeconds(DateTime? timeValue, int? addValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x0000B232 File Offset: 0x00009432
		[DbFunction("Edm", "AddSeconds")]
		public static TimeSpan? AddSeconds(TimeSpan? timeValue, int? addValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x0000B23E File Offset: 0x0000943E
		[DbFunction("Edm", "AddMilliseconds")]
		public static DateTimeOffset? AddMilliseconds(DateTimeOffset? timeValue, int? addValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x0000B24A File Offset: 0x0000944A
		[DbFunction("Edm", "AddMilliseconds")]
		public static DateTime? AddMilliseconds(DateTime? timeValue, int? addValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x0000B256 File Offset: 0x00009456
		[DbFunction("Edm", "AddMilliseconds")]
		public static TimeSpan? AddMilliseconds(TimeSpan? timeValue, int? addValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x0000B262 File Offset: 0x00009462
		[DbFunction("Edm", "AddMicroseconds")]
		public static DateTimeOffset? AddMicroseconds(DateTimeOffset? timeValue, int? addValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x0000B26E File Offset: 0x0000946E
		[DbFunction("Edm", "AddMicroseconds")]
		public static DateTime? AddMicroseconds(DateTime? timeValue, int? addValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x0000B27A File Offset: 0x0000947A
		[DbFunction("Edm", "AddMicroseconds")]
		public static TimeSpan? AddMicroseconds(TimeSpan? timeValue, int? addValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060002EA RID: 746 RVA: 0x0000B286 File Offset: 0x00009486
		[DbFunction("Edm", "AddNanoseconds")]
		public static DateTimeOffset? AddNanoseconds(DateTimeOffset? timeValue, int? addValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060002EB RID: 747 RVA: 0x0000B292 File Offset: 0x00009492
		[DbFunction("Edm", "AddNanoseconds")]
		public static DateTime? AddNanoseconds(DateTime? timeValue, int? addValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060002EC RID: 748 RVA: 0x0000B29E File Offset: 0x0000949E
		[DbFunction("Edm", "AddNanoseconds")]
		public static TimeSpan? AddNanoseconds(TimeSpan? timeValue, int? addValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060002ED RID: 749 RVA: 0x0000B2AA File Offset: 0x000094AA
		[DbFunction("Edm", "DiffYears")]
		public static int? DiffYears(DateTimeOffset? dateValue1, DateTimeOffset? dateValue2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060002EE RID: 750 RVA: 0x0000B2B6 File Offset: 0x000094B6
		[DbFunction("Edm", "DiffYears")]
		public static int? DiffYears(DateTime? dateValue1, DateTime? dateValue2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060002EF RID: 751 RVA: 0x0000B2C2 File Offset: 0x000094C2
		[DbFunction("Edm", "DiffMonths")]
		public static int? DiffMonths(DateTimeOffset? dateValue1, DateTimeOffset? dateValue2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x0000B2CE File Offset: 0x000094CE
		[DbFunction("Edm", "DiffMonths")]
		public static int? DiffMonths(DateTime? dateValue1, DateTime? dateValue2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x0000B2DA File Offset: 0x000094DA
		[DbFunction("Edm", "DiffDays")]
		public static int? DiffDays(DateTimeOffset? dateValue1, DateTimeOffset? dateValue2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x0000B2E6 File Offset: 0x000094E6
		[DbFunction("Edm", "DiffDays")]
		public static int? DiffDays(DateTime? dateValue1, DateTime? dateValue2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x0000B2F2 File Offset: 0x000094F2
		[DbFunction("Edm", "DiffHours")]
		public static int? DiffHours(DateTimeOffset? timeValue1, DateTimeOffset? timeValue2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x0000B2FE File Offset: 0x000094FE
		[DbFunction("Edm", "DiffHours")]
		public static int? DiffHours(DateTime? timeValue1, DateTime? timeValue2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x0000B30A File Offset: 0x0000950A
		[DbFunction("Edm", "DiffHours")]
		public static int? DiffHours(TimeSpan? timeValue1, TimeSpan? timeValue2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x0000B316 File Offset: 0x00009516
		[DbFunction("Edm", "DiffMinutes")]
		public static int? DiffMinutes(DateTimeOffset? timeValue1, DateTimeOffset? timeValue2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x0000B322 File Offset: 0x00009522
		[DbFunction("Edm", "DiffMinutes")]
		public static int? DiffMinutes(DateTime? timeValue1, DateTime? timeValue2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x0000B32E File Offset: 0x0000952E
		[DbFunction("Edm", "DiffMinutes")]
		public static int? DiffMinutes(TimeSpan? timeValue1, TimeSpan? timeValue2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x0000B33A File Offset: 0x0000953A
		[DbFunction("Edm", "DiffSeconds")]
		public static int? DiffSeconds(DateTimeOffset? timeValue1, DateTimeOffset? timeValue2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060002FA RID: 762 RVA: 0x0000B346 File Offset: 0x00009546
		[DbFunction("Edm", "DiffSeconds")]
		public static int? DiffSeconds(DateTime? timeValue1, DateTime? timeValue2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060002FB RID: 763 RVA: 0x0000B352 File Offset: 0x00009552
		[DbFunction("Edm", "DiffSeconds")]
		public static int? DiffSeconds(TimeSpan? timeValue1, TimeSpan? timeValue2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060002FC RID: 764 RVA: 0x0000B35E File Offset: 0x0000955E
		[DbFunction("Edm", "DiffMilliseconds")]
		public static int? DiffMilliseconds(DateTimeOffset? timeValue1, DateTimeOffset? timeValue2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060002FD RID: 765 RVA: 0x0000B36A File Offset: 0x0000956A
		[DbFunction("Edm", "DiffMilliseconds")]
		public static int? DiffMilliseconds(DateTime? timeValue1, DateTime? timeValue2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060002FE RID: 766 RVA: 0x0000B376 File Offset: 0x00009576
		[DbFunction("Edm", "DiffMilliseconds")]
		public static int? DiffMilliseconds(TimeSpan? timeValue1, TimeSpan? timeValue2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060002FF RID: 767 RVA: 0x0000B382 File Offset: 0x00009582
		[DbFunction("Edm", "DiffMicroseconds")]
		public static int? DiffMicroseconds(DateTimeOffset? timeValue1, DateTimeOffset? timeValue2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000300 RID: 768 RVA: 0x0000B38E File Offset: 0x0000958E
		[DbFunction("Edm", "DiffMicroseconds")]
		public static int? DiffMicroseconds(DateTime? timeValue1, DateTime? timeValue2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000301 RID: 769 RVA: 0x0000B39A File Offset: 0x0000959A
		[DbFunction("Edm", "DiffMicroseconds")]
		public static int? DiffMicroseconds(TimeSpan? timeValue1, TimeSpan? timeValue2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000302 RID: 770 RVA: 0x0000B3A6 File Offset: 0x000095A6
		[DbFunction("Edm", "DiffNanoseconds")]
		public static int? DiffNanoseconds(DateTimeOffset? timeValue1, DateTimeOffset? timeValue2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000303 RID: 771 RVA: 0x0000B3B2 File Offset: 0x000095B2
		[DbFunction("Edm", "DiffNanoseconds")]
		public static int? DiffNanoseconds(DateTime? timeValue1, DateTime? timeValue2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000304 RID: 772 RVA: 0x0000B3BE File Offset: 0x000095BE
		[DbFunction("Edm", "DiffNanoseconds")]
		public static int? DiffNanoseconds(TimeSpan? timeValue1, TimeSpan? timeValue2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000305 RID: 773 RVA: 0x0000B3CA File Offset: 0x000095CA
		[DbFunction("Edm", "Truncate")]
		public static double? Truncate(double? value, int? digits)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000306 RID: 774 RVA: 0x0000B3D6 File Offset: 0x000095D6
		[DbFunction("Edm", "Truncate")]
		public static decimal? Truncate(decimal? value, int? digits)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000307 RID: 775 RVA: 0x0000B3E2 File Offset: 0x000095E2
		public static bool Like(string searchString, string likeExpression)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000308 RID: 776 RVA: 0x0000B3EE File Offset: 0x000095EE
		public static bool Like(string searchString, string likeExpression, string escapeCharacter)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000309 RID: 777 RVA: 0x0000B3FA File Offset: 0x000095FA
		public static string AsUnicode(string value)
		{
			return value;
		}

		// Token: 0x0600030A RID: 778 RVA: 0x0000B3FD File Offset: 0x000095FD
		public static string AsNonUnicode(string value)
		{
			return value;
		}

		// Token: 0x0600030B RID: 779 RVA: 0x0000B400 File Offset: 0x00009600
		private static TOut BootstrapFunction<TIn, TOut>(Expression<Func<IEnumerable<TIn>, TOut>> methodExpression, IEnumerable<TIn> collection)
		{
			IQueryable queryable = collection as IQueryable;
			if (queryable != null)
			{
				return queryable.Provider.Execute<TOut>(Expression.Call(((MethodCallExpression)methodExpression.Body).Method, Expression.Constant(collection)));
			}
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}
	}
}
