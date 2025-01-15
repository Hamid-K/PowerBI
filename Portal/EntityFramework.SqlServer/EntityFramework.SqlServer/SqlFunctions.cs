using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer.Resources;
using System.Linq;
using System.Linq.Expressions;

namespace System.Data.Entity.SqlServer
{
	// Token: 0x0200000D RID: 13
	public static class SqlFunctions
	{
		// Token: 0x0600005D RID: 93 RVA: 0x00003394 File Offset: 0x00001594
		[DbFunction("SqlServer", "CHECKSUM_AGG")]
		public static int? ChecksumAggregate(IEnumerable<int> arg)
		{
			return SqlFunctions.BootstrapFunction<int, int?>((IEnumerable<int> a) => SqlFunctions.ChecksumAggregate(a), arg);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x000033EC File Offset: 0x000015EC
		[DbFunction("SqlServer", "CHECKSUM_AGG")]
		public static int? ChecksumAggregate(IEnumerable<int?> arg)
		{
			return SqlFunctions.BootstrapFunction<int?, int?>((IEnumerable<int?> a) => SqlFunctions.ChecksumAggregate(a), arg);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00003444 File Offset: 0x00001644
		private static TOut BootstrapFunction<TIn, TOut>(Expression<Func<IEnumerable<TIn>, TOut>> methodExpression, IEnumerable<TIn> arg)
		{
			IQueryable queryable = arg as IQueryable;
			if (queryable != null)
			{
				return queryable.Provider.Execute<TOut>(Expression.Call(((MethodCallExpression)methodExpression.Body).Method, Expression.Constant(arg)));
			}
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x0000348C File Offset: 0x0000168C
		[DbFunction("SqlServer", "ASCII")]
		public static int? Ascii(string arg)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00003498 File Offset: 0x00001698
		[DbFunction("SqlServer", "CHAR")]
		public static string Char(int? arg)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x000034A4 File Offset: 0x000016A4
		[DbFunction("SqlServer", "CHARINDEX")]
		public static int? CharIndex(string toFind, string toSearch)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x000034B0 File Offset: 0x000016B0
		[DbFunction("SqlServer", "CHARINDEX")]
		public static int? CharIndex(byte[] toFind, byte[] toSearch)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x000034BC File Offset: 0x000016BC
		[DbFunction("SqlServer", "CHARINDEX")]
		public static int? CharIndex(string toFind, string toSearch, int? startLocation)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x000034C8 File Offset: 0x000016C8
		[DbFunction("SqlServer", "CHARINDEX")]
		public static int? CharIndex(byte[] toFind, byte[] toSearch, int? startLocation)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x000034D4 File Offset: 0x000016D4
		[DbFunction("SqlServer", "CHARINDEX")]
		public static long? CharIndex(string toFind, string toSearch, long? startLocation)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x000034E0 File Offset: 0x000016E0
		[DbFunction("SqlServer", "CHARINDEX")]
		public static long? CharIndex(byte[] toFind, byte[] toSearch, long? startLocation)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x000034EC File Offset: 0x000016EC
		[DbFunction("SqlServer", "DIFFERENCE")]
		public static int? Difference(string string1, string string2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000034F8 File Offset: 0x000016F8
		[DbFunction("SqlServer", "NCHAR")]
		public static string NChar(int? arg)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003504 File Offset: 0x00001704
		[DbFunction("SqlServer", "PATINDEX")]
		public static int? PatIndex(string stringPattern, string target)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003510 File Offset: 0x00001710
		[DbFunction("SqlServer", "QUOTENAME")]
		public static string QuoteName(string stringArg)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x0000351C File Offset: 0x0000171C
		[DbFunction("SqlServer", "QUOTENAME")]
		public static string QuoteName(string stringArg, string quoteCharacter)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003528 File Offset: 0x00001728
		[DbFunction("SqlServer", "REPLICATE")]
		public static string Replicate(string target, int? count)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003534 File Offset: 0x00001734
		[DbFunction("SqlServer", "SOUNDEX")]
		public static string SoundCode(string arg)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003540 File Offset: 0x00001740
		[DbFunction("SqlServer", "SPACE")]
		public static string Space(int? arg1)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x0000354C File Offset: 0x0000174C
		[DbFunction("SqlServer", "STR")]
		public static string StringConvert(double? number)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003558 File Offset: 0x00001758
		[DbFunction("SqlServer", "STR")]
		public static string StringConvert(decimal? number)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003564 File Offset: 0x00001764
		[DbFunction("SqlServer", "STR")]
		public static string StringConvert(double? number, int? length)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003570 File Offset: 0x00001770
		[DbFunction("SqlServer", "STR")]
		public static string StringConvert(decimal? number, int? length)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x0000357C File Offset: 0x0000177C
		[DbFunction("SqlServer", "STR")]
		public static string StringConvert(double? number, int? length, int? decimalArg)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003588 File Offset: 0x00001788
		[DbFunction("SqlServer", "STR")]
		public static string StringConvert(decimal? number, int? length, int? decimalArg)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003594 File Offset: 0x00001794
		[DbFunction("SqlServer", "STUFF")]
		public static string Stuff(string stringInput, int? start, int? length, string stringReplacement)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000035A0 File Offset: 0x000017A0
		[DbFunction("SqlServer", "UNICODE")]
		public static int? Unicode(string arg)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000035AC File Offset: 0x000017AC
		[DbFunction("SqlServer", "ACOS")]
		public static double? Acos(double? arg1)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000035B8 File Offset: 0x000017B8
		[DbFunction("SqlServer", "ACOS")]
		public static double? Acos(decimal? arg1)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x000035C4 File Offset: 0x000017C4
		[DbFunction("SqlServer", "ASIN")]
		public static double? Asin(double? arg)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x000035D0 File Offset: 0x000017D0
		[DbFunction("SqlServer", "ASIN")]
		public static double? Asin(decimal? arg)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x000035DC File Offset: 0x000017DC
		[DbFunction("SqlServer", "ATAN")]
		public static double? Atan(double? arg)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x000035E8 File Offset: 0x000017E8
		[DbFunction("SqlServer", "ATAN")]
		public static double? Atan(decimal? arg)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000035F4 File Offset: 0x000017F4
		[DbFunction("SqlServer", "ATN2")]
		public static double? Atan2(double? arg1, double? arg2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00003600 File Offset: 0x00001800
		[DbFunction("SqlServer", "ATN2")]
		public static double? Atan2(decimal? arg1, decimal? arg2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x0000360C File Offset: 0x0000180C
		[DbFunction("SqlServer", "COS")]
		public static double? Cos(double? arg)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003618 File Offset: 0x00001818
		[DbFunction("SqlServer", "COS")]
		public static double? Cos(decimal? arg)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00003624 File Offset: 0x00001824
		[DbFunction("SqlServer", "COT")]
		public static double? Cot(double? arg)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003630 File Offset: 0x00001830
		[DbFunction("SqlServer", "COT")]
		public static double? Cot(decimal? arg)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x0000363C File Offset: 0x0000183C
		[DbFunction("SqlServer", "DEGREES")]
		public static int? Degrees(int? arg1)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003648 File Offset: 0x00001848
		[DbFunction("SqlServer", "DEGREES")]
		public static long? Degrees(long? arg1)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003654 File Offset: 0x00001854
		[DbFunction("SqlServer", "DEGREES")]
		public static decimal? Degrees(decimal? arg1)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00003660 File Offset: 0x00001860
		[DbFunction("SqlServer", "DEGREES")]
		public static double? Degrees(double? arg1)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x0000366C File Offset: 0x0000186C
		[DbFunction("SqlServer", "EXP")]
		public static double? Exp(double? arg)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00003678 File Offset: 0x00001878
		[DbFunction("SqlServer", "EXP")]
		public static double? Exp(decimal? arg)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003684 File Offset: 0x00001884
		[DbFunction("SqlServer", "LOG")]
		public static double? Log(double? arg)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003690 File Offset: 0x00001890
		[DbFunction("SqlServer", "LOG")]
		public static double? Log(decimal? arg)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x0000369C File Offset: 0x0000189C
		[DbFunction("SqlServer", "LOG10")]
		public static double? Log10(double? arg)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x000036A8 File Offset: 0x000018A8
		[DbFunction("SqlServer", "LOG10")]
		public static double? Log10(decimal? arg)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x000036B4 File Offset: 0x000018B4
		[DbFunction("SqlServer", "PI")]
		public static double? Pi()
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000036C0 File Offset: 0x000018C0
		[DbFunction("SqlServer", "RADIANS")]
		public static int? Radians(int? arg)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000036CC File Offset: 0x000018CC
		[DbFunction("SqlServer", "RADIANS")]
		public static long? Radians(long? arg)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x000036D8 File Offset: 0x000018D8
		[DbFunction("SqlServer", "RADIANS")]
		public static decimal? Radians(decimal? arg)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x000036E4 File Offset: 0x000018E4
		[DbFunction("SqlServer", "RADIANS")]
		public static double? Radians(double? arg)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x000036F0 File Offset: 0x000018F0
		[DbFunction("SqlServer", "RAND")]
		public static double? Rand()
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x000036FC File Offset: 0x000018FC
		[DbFunction("SqlServer", "RAND")]
		public static double? Rand(int? seed)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003708 File Offset: 0x00001908
		[DbFunction("SqlServer", "SIGN")]
		public static int? Sign(int? arg)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00003714 File Offset: 0x00001914
		[DbFunction("SqlServer", "SIGN")]
		public static long? Sign(long? arg)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00003720 File Offset: 0x00001920
		[DbFunction("SqlServer", "SIGN")]
		public static decimal? Sign(decimal? arg)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x0000372C File Offset: 0x0000192C
		[DbFunction("SqlServer", "SIGN")]
		public static double? Sign(double? arg)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00003738 File Offset: 0x00001938
		[DbFunction("SqlServer", "SIN")]
		public static double? Sin(decimal? arg)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00003744 File Offset: 0x00001944
		[DbFunction("SqlServer", "SIN")]
		public static double? Sin(double? arg)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00003750 File Offset: 0x00001950
		[DbFunction("SqlServer", "SQRT")]
		public static double? SquareRoot(double? arg)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x0000375C File Offset: 0x0000195C
		[DbFunction("SqlServer", "SQRT")]
		public static double? SquareRoot(decimal? arg)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00003768 File Offset: 0x00001968
		[DbFunction("SqlServer", "SQUARE")]
		public static double? Square(double? arg1)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00003774 File Offset: 0x00001974
		[DbFunction("SqlServer", "SQUARE")]
		public static double? Square(decimal? arg1)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00003780 File Offset: 0x00001980
		[DbFunction("SqlServer", "TAN")]
		public static double? Tan(double? arg)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x0000378C File Offset: 0x0000198C
		[DbFunction("SqlServer", "TAN")]
		public static double? Tan(decimal? arg)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00003798 File Offset: 0x00001998
		[DbFunction("SqlServer", "DATEADD")]
		public static DateTime? DateAdd(string datePartArg, double? number, DateTime? date)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x000037A4 File Offset: 0x000019A4
		[DbFunction("SqlServer", "DATEADD")]
		public static TimeSpan? DateAdd(string datePartArg, double? number, TimeSpan? time)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x000037B0 File Offset: 0x000019B0
		[DbFunction("SqlServer", "DATEADD")]
		public static DateTimeOffset? DateAdd(string datePartArg, double? number, DateTimeOffset? dateTimeOffsetArg)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x000037BC File Offset: 0x000019BC
		[DbFunction("SqlServer", "DATEADD")]
		public static DateTime? DateAdd(string datePartArg, double? number, string date)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x000037C8 File Offset: 0x000019C8
		[DbFunction("SqlServer", "DATEDIFF")]
		public static int? DateDiff(string datePartArg, DateTime? startDate, DateTime? endDate)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x000037D4 File Offset: 0x000019D4
		[DbFunction("SqlServer", "DATEDIFF")]
		public static int? DateDiff(string datePartArg, DateTimeOffset? startDate, DateTimeOffset? endDate)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x000037E0 File Offset: 0x000019E0
		[DbFunction("SqlServer", "DATEDIFF")]
		public static int? DateDiff(string datePartArg, TimeSpan? startDate, TimeSpan? endDate)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x000037EC File Offset: 0x000019EC
		[DbFunction("SqlServer", "DATEDIFF")]
		public static int? DateDiff(string datePartArg, string startDate, DateTime? endDate)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x000037F8 File Offset: 0x000019F8
		[DbFunction("SqlServer", "DATEDIFF")]
		public static int? DateDiff(string datePartArg, string startDate, DateTimeOffset? endDate)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00003804 File Offset: 0x00001A04
		[DbFunction("SqlServer", "DATEDIFF")]
		public static int? DateDiff(string datePartArg, string startDate, TimeSpan? endDate)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00003810 File Offset: 0x00001A10
		[DbFunction("SqlServer", "DATEDIFF")]
		public static int? DateDiff(string datePartArg, TimeSpan? startDate, string endDate)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x0000381C File Offset: 0x00001A1C
		[DbFunction("SqlServer", "DATEDIFF")]
		public static int? DateDiff(string datePartArg, DateTime? startDate, string endDate)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00003828 File Offset: 0x00001A28
		[DbFunction("SqlServer", "DATEDIFF")]
		public static int? DateDiff(string datePartArg, DateTimeOffset? startDate, string endDate)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00003834 File Offset: 0x00001A34
		[DbFunction("SqlServer", "DATEDIFF")]
		public static int? DateDiff(string datePartArg, string startDate, string endDate)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00003840 File Offset: 0x00001A40
		[DbFunction("SqlServer", "DATEDIFF")]
		public static int? DateDiff(string datePartArg, TimeSpan? startDate, DateTime? endDate)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x0000384C File Offset: 0x00001A4C
		[DbFunction("SqlServer", "DATEDIFF")]
		public static int? DateDiff(string datePartArg, TimeSpan? startDate, DateTimeOffset? endDate)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00003858 File Offset: 0x00001A58
		[DbFunction("SqlServer", "DATEDIFF")]
		public static int? DateDiff(string datePartArg, DateTime? startDate, TimeSpan? endDate)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00003864 File Offset: 0x00001A64
		[DbFunction("SqlServer", "DATEDIFF")]
		public static int? DateDiff(string datePartArg, DateTimeOffset? startDate, TimeSpan? endDate)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00003870 File Offset: 0x00001A70
		[DbFunction("SqlServer", "DATEDIFF")]
		public static int? DateDiff(string datePartArg, DateTime? startDate, DateTimeOffset? endDate)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x0000387C File Offset: 0x00001A7C
		[DbFunction("SqlServer", "DATEDIFF")]
		public static int? DateDiff(string datePartArg, DateTimeOffset? startDate, DateTime? endDate)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00003888 File Offset: 0x00001A88
		[DbFunction("SqlServer", "DATENAME")]
		public static string DateName(string datePartArg, DateTime? date)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00003894 File Offset: 0x00001A94
		[DbFunction("SqlServer", "DATENAME")]
		public static string DateName(string datePartArg, string date)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x000038A0 File Offset: 0x00001AA0
		[DbFunction("SqlServer", "DATENAME")]
		public static string DateName(string datePartArg, TimeSpan? date)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x000038AC File Offset: 0x00001AAC
		[DbFunction("SqlServer", "DATENAME")]
		public static string DateName(string datePartArg, DateTimeOffset? date)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x000038B8 File Offset: 0x00001AB8
		[DbFunction("SqlServer", "DATEPART")]
		public static int? DatePart(string datePartArg, DateTime? date)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060000BA RID: 186 RVA: 0x000038C4 File Offset: 0x00001AC4
		[DbFunction("SqlServer", "DATEPART")]
		public static int? DatePart(string datePartArg, DateTimeOffset? date)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x000038D0 File Offset: 0x00001AD0
		[DbFunction("SqlServer", "DATEPART")]
		public static int? DatePart(string datePartArg, string date)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060000BC RID: 188 RVA: 0x000038DC File Offset: 0x00001ADC
		[DbFunction("SqlServer", "DATEPART")]
		public static int? DatePart(string datePartArg, TimeSpan? date)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060000BD RID: 189 RVA: 0x000038E8 File Offset: 0x00001AE8
		[DbFunction("SqlServer", "GETDATE")]
		public static DateTime? GetDate()
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060000BE RID: 190 RVA: 0x000038F4 File Offset: 0x00001AF4
		[DbFunction("SqlServer", "GETUTCDATE")]
		public static DateTime? GetUtcDate()
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00003900 File Offset: 0x00001B00
		[DbFunction("SqlServer", "DATALENGTH")]
		public static int? DataLength(bool? arg)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x0000390C File Offset: 0x00001B0C
		[DbFunction("SqlServer", "DATALENGTH")]
		public static int? DataLength(double? arg)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00003918 File Offset: 0x00001B18
		[DbFunction("SqlServer", "DATALENGTH")]
		public static int? DataLength(decimal? arg)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00003924 File Offset: 0x00001B24
		[DbFunction("SqlServer", "DATALENGTH")]
		public static int? DataLength(DateTime? arg)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00003930 File Offset: 0x00001B30
		[DbFunction("SqlServer", "DATALENGTH")]
		public static int? DataLength(TimeSpan? arg)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x0000393C File Offset: 0x00001B3C
		[DbFunction("SqlServer", "DATALENGTH")]
		public static int? DataLength(DateTimeOffset? arg)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00003948 File Offset: 0x00001B48
		[DbFunction("SqlServer", "DATALENGTH")]
		public static int? DataLength(string arg)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00003954 File Offset: 0x00001B54
		[DbFunction("SqlServer", "DATALENGTH")]
		public static int? DataLength(byte[] arg)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00003960 File Offset: 0x00001B60
		[DbFunction("SqlServer", "DATALENGTH")]
		public static int? DataLength(Guid? arg)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x0000396C File Offset: 0x00001B6C
		[DbFunction("SqlServer", "CHECKSUM")]
		public static int? Checksum(bool? arg1)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00003978 File Offset: 0x00001B78
		[DbFunction("SqlServer", "CHECKSUM")]
		public static int? Checksum(double? arg1)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00003984 File Offset: 0x00001B84
		[DbFunction("SqlServer", "CHECKSUM")]
		public static int? Checksum(decimal? arg1)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00003990 File Offset: 0x00001B90
		[DbFunction("SqlServer", "CHECKSUM")]
		public static int? Checksum(string arg1)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060000CC RID: 204 RVA: 0x0000399C File Offset: 0x00001B9C
		[DbFunction("SqlServer", "CHECKSUM")]
		public static int? Checksum(DateTime? arg1)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060000CD RID: 205 RVA: 0x000039A8 File Offset: 0x00001BA8
		[DbFunction("SqlServer", "CHECKSUM")]
		public static int? Checksum(TimeSpan? arg1)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060000CE RID: 206 RVA: 0x000039B4 File Offset: 0x00001BB4
		[DbFunction("SqlServer", "CHECKSUM")]
		public static int? Checksum(DateTimeOffset? arg1)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060000CF RID: 207 RVA: 0x000039C0 File Offset: 0x00001BC0
		[DbFunction("SqlServer", "CHECKSUM")]
		public static int? Checksum(byte[] arg1)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x000039CC File Offset: 0x00001BCC
		[DbFunction("SqlServer", "CHECKSUM")]
		public static int? Checksum(Guid? arg1)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x000039D8 File Offset: 0x00001BD8
		[DbFunction("SqlServer", "CHECKSUM")]
		public static int? Checksum(bool? arg1, bool? arg2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x000039E4 File Offset: 0x00001BE4
		[DbFunction("SqlServer", "CHECKSUM")]
		public static int? Checksum(double? arg1, double? arg2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x000039F0 File Offset: 0x00001BF0
		[DbFunction("SqlServer", "CHECKSUM")]
		public static int? Checksum(decimal? arg1, decimal? arg2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x000039FC File Offset: 0x00001BFC
		[DbFunction("SqlServer", "CHECKSUM")]
		public static int? Checksum(string arg1, string arg2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00003A08 File Offset: 0x00001C08
		[DbFunction("SqlServer", "CHECKSUM")]
		public static int? Checksum(DateTime? arg1, DateTime? arg2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00003A14 File Offset: 0x00001C14
		[DbFunction("SqlServer", "CHECKSUM")]
		public static int? Checksum(TimeSpan? arg1, TimeSpan? arg2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00003A20 File Offset: 0x00001C20
		[DbFunction("SqlServer", "CHECKSUM")]
		public static int? Checksum(DateTimeOffset? arg1, DateTimeOffset? arg2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00003A2C File Offset: 0x00001C2C
		[DbFunction("SqlServer", "CHECKSUM")]
		public static int? Checksum(byte[] arg1, byte[] arg2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00003A38 File Offset: 0x00001C38
		[DbFunction("SqlServer", "CHECKSUM")]
		public static int? Checksum(Guid? arg1, Guid? arg2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00003A44 File Offset: 0x00001C44
		[DbFunction("SqlServer", "CHECKSUM")]
		public static int? Checksum(bool? arg1, bool? arg2, bool? arg3)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00003A50 File Offset: 0x00001C50
		[DbFunction("SqlServer", "CHECKSUM")]
		public static int? Checksum(double? arg1, double? arg2, double? arg3)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00003A5C File Offset: 0x00001C5C
		[DbFunction("SqlServer", "CHECKSUM")]
		public static int? Checksum(decimal? arg1, decimal? arg2, decimal? arg3)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00003A68 File Offset: 0x00001C68
		[DbFunction("SqlServer", "CHECKSUM")]
		public static int? Checksum(string arg1, string arg2, string arg3)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00003A74 File Offset: 0x00001C74
		[DbFunction("SqlServer", "CHECKSUM")]
		public static int? Checksum(DateTime? arg1, DateTime? arg2, DateTime? arg3)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00003A80 File Offset: 0x00001C80
		[DbFunction("SqlServer", "CHECKSUM")]
		public static int? Checksum(DateTimeOffset? arg1, DateTimeOffset? arg2, DateTimeOffset? arg3)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00003A8C File Offset: 0x00001C8C
		[DbFunction("SqlServer", "CHECKSUM")]
		public static int? Checksum(TimeSpan? arg1, TimeSpan? arg2, TimeSpan? arg3)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00003A98 File Offset: 0x00001C98
		[DbFunction("SqlServer", "CHECKSUM")]
		public static int? Checksum(byte[] arg1, byte[] arg2, byte[] arg3)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00003AA4 File Offset: 0x00001CA4
		[DbFunction("SqlServer", "CHECKSUM")]
		public static int? Checksum(Guid? arg1, Guid? arg2, Guid? arg3)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00003AB0 File Offset: 0x00001CB0
		[DbFunction("SqlServer", "CURRENT_TIMESTAMP")]
		public static DateTime? CurrentTimestamp()
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00003ABC File Offset: 0x00001CBC
		[DbFunction("SqlServer", "CURRENT_USER")]
		public static string CurrentUser()
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00003AC8 File Offset: 0x00001CC8
		[DbFunction("SqlServer", "HOST_NAME")]
		public static string HostName()
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00003AD4 File Offset: 0x00001CD4
		[DbFunction("SqlServer", "USER_NAME")]
		public static string UserName(int? arg)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00003AE0 File Offset: 0x00001CE0
		[DbFunction("SqlServer", "USER_NAME")]
		public static string UserName()
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00003AEC File Offset: 0x00001CEC
		[DbFunction("SqlServer", "ISNUMERIC")]
		public static int? IsNumeric(string arg)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00003AF8 File Offset: 0x00001CF8
		[DbFunction("SqlServer", "ISDATE")]
		public static int? IsDate(string arg)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}
	}
}
