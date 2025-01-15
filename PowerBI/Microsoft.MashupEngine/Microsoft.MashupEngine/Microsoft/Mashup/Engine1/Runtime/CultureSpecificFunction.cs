using System;
using Microsoft.Mashup.Engine.Host;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x020012C2 RID: 4802
	internal static class CultureSpecificFunction
	{
		// Token: 0x04004550 RID: 17744
		public static FunctionValue TextFrom = new Library.Text.FromFunctionValue(EngineHost.Empty, null);

		// Token: 0x04004551 RID: 17745
		public static FunctionValue TextLower = new Library.Text.LowerFunctionValue(EngineHost.Empty);

		// Token: 0x04004552 RID: 17746
		public static FunctionValue TextUpper = new Library.Text.UpperFunctionValue(EngineHost.Empty);

		// Token: 0x04004553 RID: 17747
		public static FunctionValue DateFrom = new Library.Date.FromFunctionValue(EngineHost.Empty, null);

		// Token: 0x04004554 RID: 17748
		public static FunctionValue DateDayOfWeek = new Library.Date.DayOfWeekFunctionValue(EngineHost.Empty);

		// Token: 0x04004555 RID: 17749
		public static FunctionValue DateWeekOfYear = new Library.Date.WeekOfYearFunctionValue(EngineHost.Empty);

		// Token: 0x04004556 RID: 17750
		public static FunctionValue DateStartOfWeek = new Library.Date.StartOfWeekFunctionValue(EngineHost.Empty);

		// Token: 0x04004557 RID: 17751
		public static FunctionValue DateEndOfWeek = new Library.Date.EndOfWeekFunctionValue(EngineHost.Empty);

		// Token: 0x04004558 RID: 17752
		public static FunctionValue DateTimeFrom = new Library.DateTime.FromFunctionValue(EngineHost.Empty, null);

		// Token: 0x04004559 RID: 17753
		public static FunctionValue DateTimeFromText = new Library.DateTime.FromTextFunctionValue(EngineHost.Empty);

		// Token: 0x0400455A RID: 17754
		public static FunctionValue DateTimeZoneFrom = new Library.DateTimeZone.FromFunctionValue(EngineHost.Empty, null);

		// Token: 0x0400455B RID: 17755
		public static FunctionValue DateTimeZoneFromText = new Library.DateTimeZone.FromTextFunctionValue(EngineHost.Empty);

		// Token: 0x0400455C RID: 17756
		public static FunctionValue TimeFrom = new Library.Time.FromFunctionValue(EngineHost.Empty, null);

		// Token: 0x0400455D RID: 17757
		public static FunctionValue TimeFromText = new Library.Time.FromTextFunctionValue(EngineHost.Empty);

		// Token: 0x0400455E RID: 17758
		public static FunctionValue NumberFrom = new Library.Number.FromFunctionValue(EngineHost.Empty, null);

		// Token: 0x0400455F RID: 17759
		public static FunctionValue NumberFromText = new Library.Number.FromTextFunctionValue(EngineHost.Empty, null);

		// Token: 0x04004560 RID: 17760
		public static FunctionValue NumberToText = new Library.Number.ToTextFunctionValue(EngineHost.Empty);

		// Token: 0x04004561 RID: 17761
		public static FunctionValue CurrencyFrom = new Library.Currency.FromFunctionValue(EngineHost.Empty, null);

		// Token: 0x04004562 RID: 17762
		public static FunctionValue PercentageFrom = new Library.Percentage.FromFunctionValue(EngineHost.Empty, null);

		// Token: 0x04004563 RID: 17763
		public static FunctionValue ByteFrom = new Library.NumberAliasTypes.FromIntType(EngineHost.Empty, TypeCode.Byte, null);

		// Token: 0x04004564 RID: 17764
		public static FunctionValue Int8From = new Library.NumberAliasTypes.FromIntType(EngineHost.Empty, TypeCode.SByte, null);

		// Token: 0x04004565 RID: 17765
		public static FunctionValue Int16From = new Library.NumberAliasTypes.FromIntType(EngineHost.Empty, TypeCode.Int16, null);

		// Token: 0x04004566 RID: 17766
		public static FunctionValue Int32From = new Library.NumberAliasTypes.FromIntType(EngineHost.Empty, TypeCode.Int32, null);

		// Token: 0x04004567 RID: 17767
		public static FunctionValue Int64From = new Library.NumberAliasTypes.FromIntType(EngineHost.Empty, TypeCode.Int64, null);

		// Token: 0x04004568 RID: 17768
		public static FunctionValue SingleFrom = new Library.NumberAliasTypes.FromNonIntType(EngineHost.Empty, TypeCode.Single, null);

		// Token: 0x04004569 RID: 17769
		public static FunctionValue DoubleFrom = new Library.NumberAliasTypes.FromNonIntType(EngineHost.Empty, TypeCode.Double, null);

		// Token: 0x0400456A RID: 17770
		public static FunctionValue DecimalFrom = new Library.NumberAliasTypes.FromNonIntType(EngineHost.Empty, TypeCode.Decimal, null);
	}
}
