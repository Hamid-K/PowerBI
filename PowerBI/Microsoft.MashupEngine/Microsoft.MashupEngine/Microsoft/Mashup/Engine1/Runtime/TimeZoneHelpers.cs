using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x02001674 RID: 5748
	internal static class TimeZoneHelpers
	{
		// Token: 0x0600917D RID: 37245 RVA: 0x001E352E File Offset: 0x001E172E
		public static Value GetDefaultTimeZoneName(IEngineHost host)
		{
			return TextValue.New(TimeZoneHelpers.GetDefaultTimeZone(host).Name);
		}

		// Token: 0x0600917E RID: 37246 RVA: 0x001E3540 File Offset: 0x001E1740
		public static ITimeZone GetDefaultTimeZone(IEngineHost host)
		{
			return host.QueryService<ITimeZoneService>().DefaultTimeZone;
		}

		// Token: 0x0600917F RID: 37247 RVA: 0x001E354D File Offset: 0x001E174D
		public static DateTimeOffset AddDefaultTimeZone(this DateTime dateTime, IEngineHost host)
		{
			return dateTime.AddTimeZone(TimeZoneHelpers.GetDefaultTimeZone(host), null);
		}

		// Token: 0x06009180 RID: 37248 RVA: 0x001E355C File Offset: 0x001E175C
		public static DateTimeOffset AddTimeZone(this DateTime dateTime, ITimeZone timeZone, Value originalValue = null)
		{
			DateTimeOffset dateTimeOffset;
			try
			{
				dateTimeOffset = new DateTimeOffset(dateTime.Ticks, timeZone.Value.GetUtcOffset(dateTime));
			}
			catch (ArgumentException)
			{
				throw ValueException.NewDataFormatError<Message0>(Strings.DateTimeZone_NotConvertibleToDateTimeZone, originalValue ?? DateTimeValue.New(dateTime), null);
			}
			return dateTimeOffset;
		}

		// Token: 0x06009181 RID: 37249 RVA: 0x001E35B0 File Offset: 0x001E17B0
		public static DateTime AdjustForTimeZone(this DateTime utcDateTime, ITimeZone timeZone)
		{
			return new DateTime(TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, timeZone.Value).Ticks, DateTimeKind.Unspecified);
		}
	}
}
