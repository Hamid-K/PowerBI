using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Microsoft.InfoNav
{
	// Token: 0x02000065 RID: 101
	[ImmutableObject(true)]
	public sealed class DateItemValue : DataValue<DateItem>, IDatePartValue
	{
		// Token: 0x06000203 RID: 515 RVA: 0x0000610F File Offset: 0x0000430F
		public DateItemValue(DateItem value)
			: base(value)
		{
			DateItemValue.GetKey(value).TryGetDataType(out this._type);
		}

		// Token: 0x06000204 RID: 516 RVA: 0x0000612A File Offset: 0x0000432A
		public DateItemValue(int? year = null, int? month = null, int? day = null, int? hour = null, int? minute = null, int? second = null, int? millisecond = null)
			: this(new DateItem(year, month, day, hour, minute, second, millisecond))
		{
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000205 RID: 517 RVA: 0x00006142 File Offset: 0x00004342
		internal override DataType Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x06000206 RID: 518 RVA: 0x0000614A File Offset: 0x0000434A
		internal bool LessAmbiguousThan(DateItemValue other)
		{
			return DateItemValue.GetKey(base.Value) > DateItemValue.GetKey(other.Value);
		}

		// Token: 0x06000207 RID: 519 RVA: 0x00006164 File Offset: 0x00004364
		internal static bool Cover(DataType type1, DataType type2)
		{
			TimePart timePart;
			TimePart timePart2;
			return type1.TryGetTimePart(out timePart) && type2.TryGetTimePart(out timePart2) && (timePart & timePart2) == timePart2;
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00006190 File Offset: 0x00004390
		internal bool TryConvertTo(DataType targetType, out DateItemValue result)
		{
			TimePart timePart;
			if (!targetType.TryGetTimePart(out timePart) || !DateItemValue.Cover(this._type, targetType))
			{
				result = null;
				return false;
			}
			result = new DateItemValue(((timePart & TimePart.Year) != TimePart.None) ? base.Value.Year : null, ((timePart & TimePart.Month) != TimePart.None) ? base.Value.Month : null, ((timePart & TimePart.Day) != TimePart.None) ? base.Value.Day : null, ((timePart & TimePart.Hour) != TimePart.None) ? base.Value.Hour : null, ((timePart & TimePart.Minute) != TimePart.None) ? base.Value.Minute : null, ((timePart & TimePart.Second) != TimePart.None) ? base.Value.Second : null, ((timePart & TimePart.Millisecond) != TimePart.None) ? base.Value.Millisecond : null);
			return true;
		}

		// Token: 0x06000209 RID: 521 RVA: 0x00006284 File Offset: 0x00004484
		internal static bool TryMerge(IList<DateItemValue> itemValues, out DateItemValue value)
		{
			if (itemValues.Count == 0)
			{
				value = null;
				return false;
			}
			if (itemValues.Count == 1)
			{
				value = itemValues[0];
				return true;
			}
			int? num;
			int? num2;
			int? num3;
			int? num4;
			int? num5;
			int? num6;
			int? num7;
			if (!DateItemValue.TryMerge(itemValues, DateItemValue._getYear, out num) || !DateItemValue.TryMerge(itemValues, DateItemValue._getMonth, out num2) || !DateItemValue.TryMerge(itemValues, DateItemValue._getDay, out num3) || !DateItemValue.TryMerge(itemValues, DateItemValue._getHour, out num4) || !DateItemValue.TryMerge(itemValues, DateItemValue._getMinute, out num5) || !DateItemValue.TryMerge(itemValues, DateItemValue._getSecond, out num6) || !DateItemValue.TryMerge(itemValues, DateItemValue._getMillisecond, out num7))
			{
				value = null;
				return false;
			}
			DataType dataType;
			if (!DateItemValue.GetKey(num, num2, num3, num4, num5, num6, num7).TryGetDataType(out dataType))
			{
				value = null;
				return false;
			}
			value = new DateItemValue(num, num2, num3, num4, num5, num6, num7);
			return true;
		}

		// Token: 0x0600020A RID: 522 RVA: 0x00006350 File Offset: 0x00004550
		internal static bool TryMerge(IList<DataType> dataTypes, out DataType value)
		{
			TimePart timePart = TimePart.None;
			TimePart timePart2 = TimePart.None;
			for (int i = 0; i < dataTypes.Count; i++)
			{
				TimePart timePart3;
				if (!dataTypes[i].TryGetTimePart(out timePart3))
				{
					value = DataType.None;
					return false;
				}
				timePart ^= timePart3;
				timePart2 |= timePart3;
			}
			if (timePart2 != timePart)
			{
				value = DataType.None;
				return false;
			}
			return timePart.TryGetDataType(out value);
		}

		// Token: 0x0600020B RID: 523 RVA: 0x0000639F File Offset: 0x0000459F
		public new static implicit operator DateItemValue(DateItem value)
		{
			return new DateItemValue(value);
		}

		// Token: 0x0600020C RID: 524 RVA: 0x000063A7 File Offset: 0x000045A7
		private static TimePart GetKey(DateItem value)
		{
			return DateItemValue.GetKey(value.Year, value.Month, value.Day, value.Hour, value.Minute, value.Second, value.Millisecond);
		}

		// Token: 0x0600020D RID: 525 RVA: 0x000063D8 File Offset: 0x000045D8
		private static TimePart GetKey(int? year, int? month, int? day, int? hour, int? minute, int? second, int? millisecond)
		{
			return ((year == null) ? TimePart.None : TimePart.Year) | ((month == null) ? TimePart.None : TimePart.Month) | ((day == null) ? TimePart.None : TimePart.Day) | ((hour == null) ? TimePart.None : TimePart.Hour) | ((minute == null) ? TimePart.None : TimePart.Minute) | ((second == null) ? TimePart.None : TimePart.Second) | ((millisecond == null) ? TimePart.None : TimePart.Millisecond);
		}

		// Token: 0x0600020E RID: 526 RVA: 0x00006449 File Offset: 0x00004649
		private static int GetBitMask(int? value, int bits)
		{
			if (value != null)
			{
				return 1 << bits;
			}
			return 0;
		}

		// Token: 0x0600020F RID: 527 RVA: 0x0000645C File Offset: 0x0000465C
		private static bool TryMerge(IList<DateItemValue> itemValues, Func<DateItemValue, int?> member, out int? value)
		{
			value = null;
			for (int i = 0; i < itemValues.Count; i++)
			{
				if (!DateItemValue.TryMerge(value, member(itemValues[i]), out value))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000210 RID: 528 RVA: 0x0000649F File Offset: 0x0000469F
		private static bool TryMerge(int? value1, int? value2, out int? value)
		{
			if (value1 == null)
			{
				value = value2;
				return true;
			}
			if (value2 == null)
			{
				value = value1;
				return true;
			}
			value = null;
			return false;
		}

		// Token: 0x04000140 RID: 320
		private static readonly Func<DateItemValue, int?> _getYear = (DateItemValue e) => e.Value.Year;

		// Token: 0x04000141 RID: 321
		private static readonly Func<DateItemValue, int?> _getMonth = (DateItemValue e) => e.Value.Month;

		// Token: 0x04000142 RID: 322
		private static readonly Func<DateItemValue, int?> _getDay = (DateItemValue e) => e.Value.Day;

		// Token: 0x04000143 RID: 323
		private static readonly Func<DateItemValue, int?> _getHour = (DateItemValue e) => e.Value.Hour;

		// Token: 0x04000144 RID: 324
		private static readonly Func<DateItemValue, int?> _getMinute = (DateItemValue e) => e.Value.Minute;

		// Token: 0x04000145 RID: 325
		private static readonly Func<DateItemValue, int?> _getSecond = (DateItemValue e) => e.Value.Second;

		// Token: 0x04000146 RID: 326
		private static readonly Func<DateItemValue, int?> _getMillisecond = (DateItemValue e) => e.Value.Millisecond;

		// Token: 0x04000147 RID: 327
		private readonly DataType _type;
	}
}
