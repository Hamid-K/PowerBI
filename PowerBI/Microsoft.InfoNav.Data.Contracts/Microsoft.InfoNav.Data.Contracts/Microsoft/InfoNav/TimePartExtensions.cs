using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.InfoNav
{
	// Token: 0x0200006E RID: 110
	public static class TimePartExtensions
	{
		// Token: 0x06000225 RID: 549 RVA: 0x0000662D File Offset: 0x0000482D
		public static bool TryGetDataType(this TimePart part, out DataType dataType)
		{
			return TimePartExtensions._timePartToDataTypeMap.TryGetValue(part, out dataType);
		}

		// Token: 0x06000226 RID: 550 RVA: 0x0000663B File Offset: 0x0000483B
		public static bool TryGetTimePart(this DataType dataType, out TimePart part)
		{
			return TimePartExtensions._dataTypeToTimePartMap.TryGetValue(dataType, out part);
		}

		// Token: 0x06000227 RID: 551 RVA: 0x0000664C File Offset: 0x0000484C
		private static IEnumerable<KeyValuePair<TimePart, DataType>> GetMappings()
		{
			return new KeyValuePair<TimePart, DataType>[]
			{
				Util.ToKeyValuePair<TimePart, DataType>(TimePart.Millisecond | TimePart.Second | TimePart.Minute | TimePart.Hour | TimePart.Day | TimePart.Month | TimePart.Year, DataType.DateTime),
				Util.ToKeyValuePair<TimePart, DataType>(TimePart.Second | TimePart.Minute | TimePart.Hour | TimePart.Day | TimePart.Month | TimePart.Year, DataType.YearToSecond),
				Util.ToKeyValuePair<TimePart, DataType>(TimePart.Minute | TimePart.Hour | TimePart.Day | TimePart.Month | TimePart.Year, DataType.YearToMinute),
				Util.ToKeyValuePair<TimePart, DataType>(TimePart.Hour | TimePart.Day | TimePart.Month | TimePart.Year, DataType.YearToHour),
				Util.ToKeyValuePair<TimePart, DataType>(TimePart.Day | TimePart.Month | TimePart.Year, DataType.Date),
				Util.ToKeyValuePair<TimePart, DataType>(TimePart.Month | TimePart.Year, DataType.YearAndMonth),
				Util.ToKeyValuePair<TimePart, DataType>(TimePart.Year, DataType.Year),
				Util.ToKeyValuePair<TimePart, DataType>(TimePart.Second | TimePart.Minute | TimePart.Hour | TimePart.Day | TimePart.Month, DataType.MonthToSecond),
				Util.ToKeyValuePair<TimePart, DataType>(TimePart.Minute | TimePart.Hour | TimePart.Day | TimePart.Month, DataType.MonthToMinute),
				Util.ToKeyValuePair<TimePart, DataType>(TimePart.Day | TimePart.Month, DataType.MonthAndDay),
				Util.ToKeyValuePair<TimePart, DataType>(TimePart.Month, DataType.Month),
				Util.ToKeyValuePair<TimePart, DataType>(TimePart.Day, DataType.DayOfMonth),
				Util.ToKeyValuePair<TimePart, DataType>(TimePart.Millisecond | TimePart.Second | TimePart.Minute | TimePart.Hour, DataType.TimeOfDay),
				Util.ToKeyValuePair<TimePart, DataType>(TimePart.Second | TimePart.Minute | TimePart.Hour, DataType.HourToSecond),
				Util.ToKeyValuePair<TimePart, DataType>(TimePart.Minute | TimePart.Hour, DataType.HourAndMinute)
			};
		}

		// Token: 0x0400016E RID: 366
		private static readonly IReadOnlyDictionary<TimePart, DataType> _timePartToDataTypeMap = TimePartExtensions.GetMappings().ToReadOnlyDictionary<TimePart, DataType>();

		// Token: 0x0400016F RID: 367
		private static readonly IReadOnlyDictionary<DataType, TimePart> _dataTypeToTimePartMap = (from e in TimePartExtensions.GetMappings()
			select Util.ToKeyValuePair<DataType, TimePart>(e.Value, e.Key)).ToReadOnlyDictionary<DataType, TimePart>();
	}
}
