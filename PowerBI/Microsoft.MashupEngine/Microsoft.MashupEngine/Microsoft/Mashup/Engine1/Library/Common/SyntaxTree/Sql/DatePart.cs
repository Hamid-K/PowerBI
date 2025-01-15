using System;

namespace Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql
{
	// Token: 0x020011CA RID: 4554
	internal sealed class DatePart : SqlExpression
	{
		// Token: 0x06007852 RID: 30802 RVA: 0x001A1155 File Offset: 0x0019F355
		private DatePart(string datePart)
		{
			this.datePart = datePart;
		}

		// Token: 0x170020F0 RID: 8432
		// (get) Token: 0x06007853 RID: 30803 RVA: 0x00002105 File Offset: 0x00000305
		public override int Precedence
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06007854 RID: 30804 RVA: 0x001A1164 File Offset: 0x0019F364
		public override void WriteCreateScript(ScriptWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			new SqlConstant(ConstantType.DoubleQuotesString, this.datePart).WriteCreateScript(writer);
		}

		// Token: 0x0400417F RID: 16767
		private const string YearName = "yyyy";

		// Token: 0x04004180 RID: 16768
		private const string QuarterName = "qq";

		// Token: 0x04004181 RID: 16769
		private const string MonthName = "m";

		// Token: 0x04004182 RID: 16770
		private const string WeekName = "wk";

		// Token: 0x04004183 RID: 16771
		private const string DayOfYearName = "dy";

		// Token: 0x04004184 RID: 16772
		private const string WeekDayName = "dw";

		// Token: 0x04004185 RID: 16773
		private const string DayName = "d";

		// Token: 0x04004186 RID: 16774
		private const string HourName = "hh";

		// Token: 0x04004187 RID: 16775
		private const string MinuteName = "mi";

		// Token: 0x04004188 RID: 16776
		private const string SecondsName = "ss";

		// Token: 0x04004189 RID: 16777
		private const string MillisecondName = "ms";

		// Token: 0x0400418A RID: 16778
		private const string NanosecondName = "ns";

		// Token: 0x0400418B RID: 16779
		private const string TzOffsetName = "tz";

		// Token: 0x0400418C RID: 16780
		private const string AccessHourName = "h";

		// Token: 0x0400418D RID: 16781
		private const string AccessMinuteName = "n";

		// Token: 0x0400418E RID: 16782
		private const string AccessSecondName = "s";

		// Token: 0x0400418F RID: 16783
		private const string SybaseYearName = "yy";

		// Token: 0x04004190 RID: 16784
		private const string SybaseMonthName = "mm";

		// Token: 0x04004191 RID: 16785
		private const string SybaseDayName = "dd";

		// Token: 0x04004192 RID: 16786
		private const string SybaseMicrosecondName = "us";

		// Token: 0x04004193 RID: 16787
		public static readonly DatePart Year = new DatePart("yyyy");

		// Token: 0x04004194 RID: 16788
		public static readonly DatePart Quarter = new DatePart("qq");

		// Token: 0x04004195 RID: 16789
		public static readonly DatePart Month = new DatePart("m");

		// Token: 0x04004196 RID: 16790
		public static readonly DatePart Week = new DatePart("wk");

		// Token: 0x04004197 RID: 16791
		public static readonly DatePart DayOfYear = new DatePart("dy");

		// Token: 0x04004198 RID: 16792
		public static readonly DatePart WeekDay = new DatePart("dw");

		// Token: 0x04004199 RID: 16793
		public static readonly DatePart Day = new DatePart("d");

		// Token: 0x0400419A RID: 16794
		public static readonly DatePart Hour = new DatePart("hh");

		// Token: 0x0400419B RID: 16795
		public static readonly DatePart Minute = new DatePart("mi");

		// Token: 0x0400419C RID: 16796
		public static readonly DatePart Second = new DatePart("ss");

		// Token: 0x0400419D RID: 16797
		public static readonly DatePart Millisecond = new DatePart("ms");

		// Token: 0x0400419E RID: 16798
		public static readonly DatePart Nanosecond = new DatePart("ns");

		// Token: 0x0400419F RID: 16799
		public static readonly DatePart TzOffset = new DatePart("tz");

		// Token: 0x040041A0 RID: 16800
		public static readonly DatePart AccessHour = new DatePart("h");

		// Token: 0x040041A1 RID: 16801
		public static readonly DatePart AccessMinute = new DatePart("n");

		// Token: 0x040041A2 RID: 16802
		public static readonly DatePart AccessSecond = new DatePart("s");

		// Token: 0x040041A3 RID: 16803
		public static readonly DatePart SybaseYear = new DatePart("yy");

		// Token: 0x040041A4 RID: 16804
		public static readonly DatePart SybaseMonth = new DatePart("mm");

		// Token: 0x040041A5 RID: 16805
		public static readonly DatePart SybaseDay = new DatePart("dd");

		// Token: 0x040041A6 RID: 16806
		public static readonly DatePart SybaseMicrosecond = new DatePart("us");

		// Token: 0x040041A7 RID: 16807
		private readonly string datePart;
	}
}
