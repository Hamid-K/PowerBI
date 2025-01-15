using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.OData.Edm;

namespace Microsoft.AspNet.OData.Query.Expressions
{
	// Token: 0x020000F9 RID: 249
	internal class ClrCanonicalFunctions
	{
		// Token: 0x06000867 RID: 2151 RVA: 0x00020A6E File Offset: 0x0001EC6E
		private static MethodInfo MethodOf<TReturn>(Expression<Func<object, TReturn>> expression)
		{
			return ClrCanonicalFunctions.MethodOf(expression);
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x00020A76 File Offset: 0x0001EC76
		private static MethodInfo MethodOf(Expression expression)
		{
			return ((expression as LambdaExpression).Body as MethodCallExpression).Method;
		}

		// Token: 0x0400027E RID: 638
		private static string _defaultString = null;

		// Token: 0x0400027F RID: 639
		private static Enum _defaultEnum = null;

		// Token: 0x04000280 RID: 640
		internal const string StartswithFunctionName = "startswith";

		// Token: 0x04000281 RID: 641
		internal const string EndswithFunctionName = "endswith";

		// Token: 0x04000282 RID: 642
		internal const string ContainsFunctionName = "contains";

		// Token: 0x04000283 RID: 643
		internal const string SubstringFunctionName = "substring";

		// Token: 0x04000284 RID: 644
		internal const string LengthFunctionName = "length";

		// Token: 0x04000285 RID: 645
		internal const string IndexofFunctionName = "indexof";

		// Token: 0x04000286 RID: 646
		internal const string TolowerFunctionName = "tolower";

		// Token: 0x04000287 RID: 647
		internal const string ToupperFunctionName = "toupper";

		// Token: 0x04000288 RID: 648
		internal const string TrimFunctionName = "trim";

		// Token: 0x04000289 RID: 649
		internal const string ConcatFunctionName = "concat";

		// Token: 0x0400028A RID: 650
		internal const string YearFunctionName = "year";

		// Token: 0x0400028B RID: 651
		internal const string MonthFunctionName = "month";

		// Token: 0x0400028C RID: 652
		internal const string DayFunctionName = "day";

		// Token: 0x0400028D RID: 653
		internal const string HourFunctionName = "hour";

		// Token: 0x0400028E RID: 654
		internal const string MinuteFunctionName = "minute";

		// Token: 0x0400028F RID: 655
		internal const string SecondFunctionName = "second";

		// Token: 0x04000290 RID: 656
		internal const string MillisecondFunctionName = "millisecond";

		// Token: 0x04000291 RID: 657
		internal const string FractionalSecondsFunctionName = "fractionalseconds";

		// Token: 0x04000292 RID: 658
		internal const string RoundFunctionName = "round";

		// Token: 0x04000293 RID: 659
		internal const string FloorFunctionName = "floor";

		// Token: 0x04000294 RID: 660
		internal const string CeilingFunctionName = "ceiling";

		// Token: 0x04000295 RID: 661
		internal const string CastFunctionName = "cast";

		// Token: 0x04000296 RID: 662
		internal const string IsofFunctionName = "isof";

		// Token: 0x04000297 RID: 663
		internal const string DateFunctionName = "date";

		// Token: 0x04000298 RID: 664
		internal const string TimeFunctionName = "time";

		// Token: 0x04000299 RID: 665
		internal const string NowFunctionName = "now";

		// Token: 0x0400029A RID: 666
		public static readonly MethodInfo StartsWith = ClrCanonicalFunctions.MethodOf<bool>((object _) => ClrCanonicalFunctions._defaultString.StartsWith(null));

		// Token: 0x0400029B RID: 667
		public static readonly MethodInfo EndsWith = ClrCanonicalFunctions.MethodOf<bool>((object _) => ClrCanonicalFunctions._defaultString.EndsWith(null));

		// Token: 0x0400029C RID: 668
		public static readonly MethodInfo Contains = ClrCanonicalFunctions.MethodOf<bool>((object _) => ClrCanonicalFunctions._defaultString.Contains(null));

		// Token: 0x0400029D RID: 669
		public static readonly MethodInfo SubstringStart = ClrCanonicalFunctions.MethodOf<string>((object _) => ClrCanonicalFunctions._defaultString.Substring(0));

		// Token: 0x0400029E RID: 670
		public static readonly MethodInfo SubstringStartAndLength = ClrCanonicalFunctions.MethodOf<string>((object _) => ClrCanonicalFunctions._defaultString.Substring(0, 0));

		// Token: 0x0400029F RID: 671
		public static readonly MethodInfo SubstringStartNoThrow = ClrCanonicalFunctions.MethodOf<string>((object _) => ClrSafeFunctions.SubstringStart(null, 0));

		// Token: 0x040002A0 RID: 672
		public static readonly MethodInfo SubstringStartAndLengthNoThrow = ClrCanonicalFunctions.MethodOf<string>((object _) => ClrSafeFunctions.SubstringStartAndLength(null, 0, 0));

		// Token: 0x040002A1 RID: 673
		public static readonly MethodInfo IndexOf = ClrCanonicalFunctions.MethodOf<int>((object _) => ClrCanonicalFunctions._defaultString.IndexOf(null));

		// Token: 0x040002A2 RID: 674
		public static readonly MethodInfo ToLower = ClrCanonicalFunctions.MethodOf<string>((object _) => ClrCanonicalFunctions._defaultString.ToLower());

		// Token: 0x040002A3 RID: 675
		public static readonly MethodInfo ToUpper = ClrCanonicalFunctions.MethodOf<string>((object _) => ClrCanonicalFunctions._defaultString.ToUpper());

		// Token: 0x040002A4 RID: 676
		public static readonly MethodInfo Trim = ClrCanonicalFunctions.MethodOf<string>((object _) => ClrCanonicalFunctions._defaultString.Trim());

		// Token: 0x040002A5 RID: 677
		public static readonly MethodInfo Concat = ClrCanonicalFunctions.MethodOf<string>((object _) => null + null);

		// Token: 0x040002A6 RID: 678
		public static readonly MethodInfo CeilingOfDouble = ClrCanonicalFunctions.MethodOf<double>((object _) => Math.Ceiling(0.0));

		// Token: 0x040002A7 RID: 679
		public static readonly MethodInfo RoundOfDouble = ClrCanonicalFunctions.MethodOf<double>((object _) => Math.Round(0.0));

		// Token: 0x040002A8 RID: 680
		public static readonly MethodInfo FloorOfDouble = ClrCanonicalFunctions.MethodOf<double>((object _) => Math.Floor(0.0));

		// Token: 0x040002A9 RID: 681
		public static readonly MethodInfo CeilingOfDecimal = ClrCanonicalFunctions.MethodOf<decimal>((object _) => Math.Ceiling(0m));

		// Token: 0x040002AA RID: 682
		public static readonly MethodInfo RoundOfDecimal = ClrCanonicalFunctions.MethodOf<decimal>((object _) => Math.Round(0m));

		// Token: 0x040002AB RID: 683
		public static readonly MethodInfo FloorOfDecimal = ClrCanonicalFunctions.MethodOf<decimal>((object _) => Math.Floor(0m));

		// Token: 0x040002AC RID: 684
		public static readonly MethodInfo HasFlag = ClrCanonicalFunctions.MethodOf<bool>((object _) => ClrCanonicalFunctions._defaultEnum.HasFlag(null));

		// Token: 0x040002AD RID: 685
		public static readonly Dictionary<string, PropertyInfo> DateProperties = new KeyValuePair<string, PropertyInfo>[]
		{
			new KeyValuePair<string, PropertyInfo>("year", typeof(Date).GetProperty("Year")),
			new KeyValuePair<string, PropertyInfo>("month", typeof(Date).GetProperty("Month")),
			new KeyValuePair<string, PropertyInfo>("day", typeof(Date).GetProperty("Day"))
		}.ToDictionary((KeyValuePair<string, PropertyInfo> kvp) => kvp.Key, (KeyValuePair<string, PropertyInfo> kvp) => kvp.Value);

		// Token: 0x040002AE RID: 686
		public static readonly Dictionary<string, PropertyInfo> DateTimeProperties = new KeyValuePair<string, PropertyInfo>[]
		{
			new KeyValuePair<string, PropertyInfo>("year", typeof(DateTime).GetProperty("Year")),
			new KeyValuePair<string, PropertyInfo>("month", typeof(DateTime).GetProperty("Month")),
			new KeyValuePair<string, PropertyInfo>("day", typeof(DateTime).GetProperty("Day")),
			new KeyValuePair<string, PropertyInfo>("hour", typeof(DateTime).GetProperty("Hour")),
			new KeyValuePair<string, PropertyInfo>("minute", typeof(DateTime).GetProperty("Minute")),
			new KeyValuePair<string, PropertyInfo>("second", typeof(DateTime).GetProperty("Second")),
			new KeyValuePair<string, PropertyInfo>("millisecond", typeof(DateTime).GetProperty("Millisecond"))
		}.ToDictionary((KeyValuePair<string, PropertyInfo> kvp) => kvp.Key, (KeyValuePair<string, PropertyInfo> kvp) => kvp.Value);

		// Token: 0x040002AF RID: 687
		public static readonly Dictionary<string, PropertyInfo> DateTimeOffsetProperties = new KeyValuePair<string, PropertyInfo>[]
		{
			new KeyValuePair<string, PropertyInfo>("year", typeof(DateTimeOffset).GetProperty("Year")),
			new KeyValuePair<string, PropertyInfo>("month", typeof(DateTimeOffset).GetProperty("Month")),
			new KeyValuePair<string, PropertyInfo>("day", typeof(DateTimeOffset).GetProperty("Day")),
			new KeyValuePair<string, PropertyInfo>("hour", typeof(DateTimeOffset).GetProperty("Hour")),
			new KeyValuePair<string, PropertyInfo>("minute", typeof(DateTimeOffset).GetProperty("Minute")),
			new KeyValuePair<string, PropertyInfo>("second", typeof(DateTimeOffset).GetProperty("Second")),
			new KeyValuePair<string, PropertyInfo>("millisecond", typeof(DateTimeOffset).GetProperty("Millisecond"))
		}.ToDictionary((KeyValuePair<string, PropertyInfo> kvp) => kvp.Key, (KeyValuePair<string, PropertyInfo> kvp) => kvp.Value);

		// Token: 0x040002B0 RID: 688
		public static readonly Dictionary<string, PropertyInfo> TimeOfDayProperties = new KeyValuePair<string, PropertyInfo>[]
		{
			new KeyValuePair<string, PropertyInfo>("hour", typeof(TimeOfDay).GetProperty("Hours")),
			new KeyValuePair<string, PropertyInfo>("minute", typeof(TimeOfDay).GetProperty("Minutes")),
			new KeyValuePair<string, PropertyInfo>("second", typeof(TimeOfDay).GetProperty("Seconds")),
			new KeyValuePair<string, PropertyInfo>("millisecond", typeof(TimeOfDay).GetProperty("Milliseconds"))
		}.ToDictionary((KeyValuePair<string, PropertyInfo> kvp) => kvp.Key, (KeyValuePair<string, PropertyInfo> kvp) => kvp.Value);

		// Token: 0x040002B1 RID: 689
		public static readonly Dictionary<string, PropertyInfo> TimeSpanProperties = new KeyValuePair<string, PropertyInfo>[]
		{
			new KeyValuePair<string, PropertyInfo>("hour", typeof(TimeSpan).GetProperty("Hours")),
			new KeyValuePair<string, PropertyInfo>("minute", typeof(TimeSpan).GetProperty("Minutes")),
			new KeyValuePair<string, PropertyInfo>("second", typeof(TimeSpan).GetProperty("Seconds")),
			new KeyValuePair<string, PropertyInfo>("millisecond", typeof(TimeSpan).GetProperty("Milliseconds"))
		}.ToDictionary((KeyValuePair<string, PropertyInfo> kvp) => kvp.Key, (KeyValuePair<string, PropertyInfo> kvp) => kvp.Value);

		// Token: 0x040002B2 RID: 690
		public static readonly PropertyInfo Length = typeof(string).GetProperty("Length");

		// Token: 0x040002B3 RID: 691
		public static readonly PropertyInfo DateTimeKindPropertyInfo = typeof(DateTime).GetProperty("Kind");

		// Token: 0x040002B4 RID: 692
		public static readonly MethodInfo ToUniversalTimeDateTime = typeof(DateTime).GetMethod("ToUniversalTime", BindingFlags.Instance | BindingFlags.Public);

		// Token: 0x040002B5 RID: 693
		public static readonly MethodInfo ToUniversalTimeDateTimeOffset = typeof(DateTimeOffset).GetMethod("ToUniversalTime", BindingFlags.Instance | BindingFlags.Public);

		// Token: 0x040002B6 RID: 694
		public static readonly MethodInfo ToOffsetFunction = typeof(DateTimeOffset).GetMethod("ToOffset", BindingFlags.Instance | BindingFlags.Public);

		// Token: 0x040002B7 RID: 695
		public static readonly MethodInfo GetUtcOffset = typeof(TimeZoneInfo).GetMethod("GetUtcOffset", new Type[] { typeof(DateTime) });
	}
}
