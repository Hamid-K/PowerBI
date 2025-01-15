using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Microsoft.InfoNav
{
	// Token: 0x02000061 RID: 97
	public static class DataTypeExtensions
	{
		// Token: 0x06000186 RID: 390 RVA: 0x00003318 File Offset: 0x00001518
		static DataTypeExtensions()
		{
			IList<DataType> list = ((DataType[])Enum.GetValues(typeof(DataType))).Where(new Func<DataType, bool>(DataTypeExtensions.IsPrimitive)).Evaluate<DataType>();
			int count = list.Count;
			Dictionary<DataType, ReadOnlyCollection<DataType>> dictionary = new Dictionary<DataType, ReadOnlyCollection<DataType>>();
			for (int i = 0; i < count; i++)
			{
				DataType dataType = list[i];
				List<DataType> list2 = new List<DataType>();
				for (int j = 0; j < count; j++)
				{
					if (dataType.IsAssignable(list[j]))
					{
						list2.Add(list[j]);
					}
				}
				dictionary.Add(dataType, list2.AsReadOnlyCollection<DataType>());
			}
			DataTypeExtensions._assignableFromMap = dictionary.AsReadOnlyDictionary<DataType, ReadOnlyCollection<DataType>>();
			Dictionary<DataType, ReadOnlyDictionary<int, DataType>> dictionary2 = new Dictionary<DataType, ReadOnlyDictionary<int, DataType>>();
			for (int k = 0; k < count; k++)
			{
				DataType dataType2 = list[k];
				SortedList<int, DataType> sortedList = new SortedList<int, DataType>();
				for (int l = count - 1; l >= 0; l--)
				{
					DataType dataType3 = list[l];
					int assignablePreference = dataType3.GetAssignablePreference(dataType2);
					if (assignablePreference > 0)
					{
						sortedList.Add(assignablePreference, dataType3);
					}
				}
				sortedList.TrimExcess();
				dictionary2.Add(dataType2, sortedList.AsReadOnlyDictionary<int, DataType>());
			}
			DataTypeExtensions._assignableToMap = dictionary2.AsReadOnlyDictionary<DataType, ReadOnlyDictionary<int, DataType>>();
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00003451 File Offset: 0x00001651
		public static DataType GetPrimitiveType(this DataType type)
		{
			return type & DataTypeExtensions._primitiveTypes;
		}

		// Token: 0x06000188 RID: 392 RVA: 0x0000345A File Offset: 0x0000165A
		public static bool IsPrimitive(this DataType type)
		{
			return type != DataType.None && type.GetPrimitiveType() == type;
		}

		// Token: 0x06000189 RID: 393 RVA: 0x0000346A File Offset: 0x0000166A
		public static bool IsScalar(this DataType type)
		{
			return type.HasDataType(DataType.Scalar);
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00003473 File Offset: 0x00001673
		public static bool IsDecadeOrYear(this DataType type)
		{
			return type.IsYear() || type.IsDecade();
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00003485 File Offset: 0x00001685
		public static bool IsDecadeYearOrMonth(this DataType type)
		{
			return type.IsDecade() || type.IsYear() || type.IsMonth();
		}

		// Token: 0x0600018C RID: 396 RVA: 0x0000349F File Offset: 0x0000169F
		public static bool IsNumeric(this DataType type)
		{
			return type.IsNumber() || type.IsDecadeYearOrMonth();
		}

		// Token: 0x0600018D RID: 397 RVA: 0x000034B1 File Offset: 0x000016B1
		public static bool IsTime(this DataType type)
		{
			return type.HasDataType(DataType.Time);
		}

		// Token: 0x0600018E RID: 398 RVA: 0x000034BB File Offset: 0x000016BB
		public static bool IsSingleDate(this DataType type)
		{
			return type.HasDataType(DataType.SingleDate);
		}

		// Token: 0x0600018F RID: 399 RVA: 0x000034C8 File Offset: 0x000016C8
		public static bool IsDateSpan(this DataType type)
		{
			return type.HasDataType(DataType.DateSpan);
		}

		// Token: 0x06000190 RID: 400 RVA: 0x000034D2 File Offset: 0x000016D2
		public static bool IsTimeOfDay(this DataType type)
		{
			return type.HasDataType(DataType.TimeOfDay);
		}

		// Token: 0x06000191 RID: 401 RVA: 0x000034DF File Offset: 0x000016DF
		public static bool IsHourToSecond(this DataType type)
		{
			return type.HasDataType(DataType.HourToSecond);
		}

		// Token: 0x06000192 RID: 402 RVA: 0x000034EC File Offset: 0x000016EC
		public static bool IsMonthToMinute(this DataType type)
		{
			return type.HasDataType(DataType.MonthToMinute);
		}

		// Token: 0x06000193 RID: 403 RVA: 0x000034F9 File Offset: 0x000016F9
		public static bool IsMonthToSecond(this DataType type)
		{
			return type.HasDataType(DataType.MonthToSecond);
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00003506 File Offset: 0x00001706
		public static bool IsHourAndMinute(this DataType type)
		{
			return type.HasDataType(DataType.HourAndMinute);
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00003513 File Offset: 0x00001713
		public static bool IsTimePart(this DataType type)
		{
			return type.IsTimeOfDay() || type.IsHourToSecond() || type.IsHourAndMinute();
		}

		// Token: 0x06000196 RID: 406 RVA: 0x0000352D File Offset: 0x0000172D
		public static bool IsNumber(this DataType type)
		{
			return type.HasDataType(DataType.Number);
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00003537 File Offset: 0x00001737
		public static bool IsInteger(this DataType type)
		{
			return type.HasDataType(DataType.Integer);
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00003541 File Offset: 0x00001741
		public static bool IsText(this DataType type)
		{
			return type.HasDataType(DataType.Text);
		}

		// Token: 0x06000199 RID: 409 RVA: 0x0000354A File Offset: 0x0000174A
		public static bool IsDateTime(this DataType type)
		{
			return type.HasDataType(DataType.DateTime);
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00003557 File Offset: 0x00001757
		public static bool IsDate(this DataType type)
		{
			return type.HasDataType(DataType.Date);
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00003564 File Offset: 0x00001764
		public static bool IsDateTimeOrDate(this DataType type)
		{
			return type.IsDate() || type.IsDateTime();
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00003576 File Offset: 0x00001776
		public static bool IsMonth(this DataType type)
		{
			return type.HasDataType(DataType.Month);
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00003583 File Offset: 0x00001783
		public static bool IsYear(this DataType type)
		{
			return type.HasDataType(DataType.Year);
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00003590 File Offset: 0x00001790
		public static bool IsYearAndMonth(this DataType type)
		{
			return type.HasDataType(DataType.YearAndMonth);
		}

		// Token: 0x0600019F RID: 415 RVA: 0x0000359D File Offset: 0x0000179D
		public static bool IsMonthAndDay(this DataType type)
		{
			return type.HasDataType(DataType.MonthAndDay);
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x000035AA File Offset: 0x000017AA
		public static bool IsDecade(this DataType type)
		{
			return type.HasDataType(DataType.Decade);
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x000035B7 File Offset: 0x000017B7
		public static bool IsYearAndWeek(this DataType type)
		{
			return type.HasDataType(DataType.YearAndWeek);
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x000035C4 File Offset: 0x000017C4
		public static bool IsYearToHour(this DataType type)
		{
			return type.HasDataType(DataType.YearToHour);
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x000035D1 File Offset: 0x000017D1
		public static bool IsYearToMinute(this DataType type)
		{
			return type.HasDataType(DataType.YearToMinute);
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x000035DE File Offset: 0x000017DE
		public static bool IsYearToSecond(this DataType type)
		{
			return type.HasDataType(DataType.YearToSecond);
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x000035EB File Offset: 0x000017EB
		public static bool IsBoolean(this DataType type)
		{
			return type.HasDataType(DataType.Boolean);
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x000035F4 File Offset: 0x000017F4
		public static bool IsTable(this DataType type)
		{
			return type.HasDataType(DataType.Table);
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00003601 File Offset: 0x00001801
		public static bool IsRange(this DataType type)
		{
			return type.HasDataType(DataType.Range);
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x0000360E File Offset: 0x0000180E
		public static bool IsNull(this DataType type)
		{
			return type.HasDataType(DataType.Null);
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x0000361B File Offset: 0x0000181B
		public static bool IsPod(this DataType type)
		{
			return type.HasDataType(DataType.Pod);
		}

		// Token: 0x060001AA RID: 426 RVA: 0x00003628 File Offset: 0x00001828
		public static bool IsAssignable(this DataType argumentType, DataType valueType)
		{
			return argumentType.GetAssignablePreference(valueType) != 0;
		}

		// Token: 0x060001AB RID: 427 RVA: 0x00003634 File Offset: 0x00001834
		public static ReadOnlyCollection<DataType> AssignableFrom(this DataType argumentType)
		{
			if (!argumentType.IsPrimitive())
			{
				return Util.EmptyReadOnlyCollection<DataType>();
			}
			return DataTypeExtensions._assignableFromMap[argumentType];
		}

		// Token: 0x060001AC RID: 428 RVA: 0x0000364F File Offset: 0x0000184F
		public static ReadOnlyDictionary<int, DataType> AssignableTo(this DataType argumentType)
		{
			if (!argumentType.IsPrimitive())
			{
				return Util.EmptyReadOnlyDictionary<int, DataType>();
			}
			return DataTypeExtensions._assignableToMap[argumentType];
		}

		// Token: 0x060001AD RID: 429 RVA: 0x0000366A File Offset: 0x0000186A
		public static bool IsCompatible(this DataType argumentType, DataType valueType)
		{
			return (argumentType.IsNumber() && valueType.IsNumber()) || argumentType.IsAssignable(valueType) || argumentType.IsOfType(valueType);
		}

		// Token: 0x060001AE RID: 430 RVA: 0x0000368E File Offset: 0x0000188E
		public static bool IsOfType(this DataType value, DataType type)
		{
			return type != DataType.None && (value & type) == type;
		}

		// Token: 0x060001AF RID: 431 RVA: 0x0000369C File Offset: 0x0000189C
		public static bool IsValidDay(int day)
		{
			return day >= DateTime.MinValue.Day && day <= DateTime.MaxValue.Day;
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x000036D0 File Offset: 0x000018D0
		public static bool IsValidMonth(int month)
		{
			return month >= DateTime.MinValue.Month && month <= DateTime.MaxValue.Month;
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00003704 File Offset: 0x00001904
		public static bool IsValidYear(int year)
		{
			return year >= DateTime.MinValue.Year && year <= DateTime.MaxValue.Year;
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x00003736 File Offset: 0x00001936
		public static bool IsValidHour(int hour)
		{
			return hour >= 0 && hour < 24;
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00003743 File Offset: 0x00001943
		public static bool IsValidMinute(int minute)
		{
			return minute >= 0 && minute < 60;
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00003750 File Offset: 0x00001950
		public static bool IsValidSecond(int second)
		{
			return second >= 0 && second < 60;
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x0000375D File Offset: 0x0000195D
		public static bool IsValidMillisecond(int millisecond)
		{
			return millisecond >= 0 && millisecond < 1000;
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x0000376D File Offset: 0x0000196D
		public static bool IsValidDecade(int decade)
		{
			return decade % 10 == 0 && ((decade >= 0 && decade < 100) || (decade >= 1000 && decade < 10000));
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00003794 File Offset: 0x00001994
		internal static DataType GetTypeForPrimitive(ConceptualPrimitiveType primitiveType)
		{
			switch (primitiveType)
			{
			case ConceptualPrimitiveType.Null:
				return DataType.Null;
			case ConceptualPrimitiveType.Text:
				return DataType.Text;
			case ConceptualPrimitiveType.Decimal:
			case ConceptualPrimitiveType.Double:
				return DataType.Number;
			case ConceptualPrimitiveType.Integer:
				return DataType.Integer;
			case ConceptualPrimitiveType.Boolean:
				return DataType.Boolean;
			case ConceptualPrimitiveType.Date:
				return DataType.Date;
			case ConceptualPrimitiveType.DateTime:
			case ConceptualPrimitiveType.DateTimeZone:
				return DataType.DateTime;
			case ConceptualPrimitiveType.Time:
				return DataType.Time;
			case ConceptualPrimitiveType.Duration:
			case ConceptualPrimitiveType.Binary:
			case ConceptualPrimitiveType.Variant:
				return DataType.None;
			}
			throw new InvalidOperationException(StringUtil.FormatInvariant("Unknown ConceptualPrimitiveType: {0}", primitiveType));
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00003818 File Offset: 0x00001A18
		internal static bool EqualOrGreaterCardinality(this DataType valueType, DataType argumentType)
		{
			if (argumentType.IsTable() || argumentType.IsRange() || valueType.IsTable() || valueType.IsRange())
			{
				return false;
			}
			if (argumentType == valueType)
			{
				return true;
			}
			if (argumentType.IsNumber())
			{
				return valueType.IsNumber();
			}
			if (argumentType.IsDateTimeOrDate())
			{
				return valueType.IsDateTimeOrDate();
			}
			return valueType.IsAssignable(argumentType);
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00003874 File Offset: 0x00001A74
		internal static bool TryConvert(this DataType argumentType, DataValue value, out DataValue convertedValue)
		{
			if (argumentType == value.Type)
			{
				convertedValue = value;
				return true;
			}
			if (argumentType <= DataType.YearToHour)
			{
				if (argumentType <= DataType.Integer)
				{
					if (argumentType <= DataType.Boolean)
					{
						if (argumentType == DataType.Text)
						{
							return DataTypeExtensions.TryConvertToText(value, out convertedValue);
						}
						if (argumentType == DataType.Boolean)
						{
							return DataTypeExtensions.TryConvertToBoolean(value, out convertedValue);
						}
					}
					else
					{
						if (argumentType == DataType.Number)
						{
							return DataTypeExtensions.TryConvertToNumber(value, out convertedValue);
						}
						if (argumentType == DataType.Integer)
						{
							return DataTypeExtensions.TryConvertToInteger(value, out convertedValue);
						}
					}
				}
				else if (argumentType <= DataType.Year)
				{
					if (argumentType == DataType.Decade)
					{
						return DataTypeExtensions.TryConvertToDecade(value, out convertedValue);
					}
					if (argumentType == DataType.Year)
					{
						return DataTypeExtensions.TryConvertToYear(value, out convertedValue);
					}
				}
				else
				{
					if (argumentType == DataType.YearAndMonth)
					{
						return DataTypeExtensions.TryConvertToYearAndMonth(value, out convertedValue);
					}
					if (argumentType == DataType.Date)
					{
						return DataTypeExtensions.TryConvertToDate(value, out convertedValue);
					}
					if (argumentType == DataType.YearToHour)
					{
						return DataTypeExtensions.TryConvertToYearToHour(value, out convertedValue);
					}
				}
			}
			else if (argumentType <= DataType.MonthAndDay)
			{
				if (argumentType <= DataType.YearToSecond)
				{
					if (argumentType == DataType.YearToMinute)
					{
						return DataTypeExtensions.TryConvertToYearToMinute(value, out convertedValue);
					}
					if (argumentType == DataType.YearToSecond)
					{
						return DataTypeExtensions.TryConvertToYearToSecond(value, out convertedValue);
					}
				}
				else
				{
					if (argumentType == DataType.DateTime)
					{
						return DataTypeExtensions.TryConvertToDateTime(value, out convertedValue);
					}
					if (argumentType == DataType.Month)
					{
						return DataTypeExtensions.TryConvertToMonth(value, out convertedValue);
					}
					if (argumentType == DataType.MonthAndDay)
					{
						return DataTypeExtensions.TryConvertToMonthAndDay(value, out convertedValue);
					}
				}
			}
			else if (argumentType <= DataType.HourAndMinute)
			{
				if (argumentType == DataType.TimeOfDay)
				{
					return DataTypeExtensions.TryConvertToTimeOfDay(value, out convertedValue);
				}
				if (argumentType == DataType.HourAndMinute)
				{
					return DataTypeExtensions.TryConvertToHourAndMinute(value, out convertedValue);
				}
			}
			else
			{
				if (argumentType == DataType.HourToSecond)
				{
					return DataTypeExtensions.TryConvertToHourToSecond(value, out convertedValue);
				}
				if (argumentType == DataType.MonthToMinute)
				{
					return DataTypeExtensions.TryConvertToMonthToMinute(value, out convertedValue);
				}
				if (argumentType == DataType.MonthToSecond)
				{
					return DataTypeExtensions.TryConvertToMonthToSecond(value, out convertedValue);
				}
			}
			convertedValue = null;
			return false;
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00003A40 File Offset: 0x00001C40
		internal static int GetAssignablePreference(this DataType argumentType, DataType valueType)
		{
			if (argumentType == DataType.None || valueType == DataType.None)
			{
				return 0;
			}
			if (argumentType == valueType)
			{
				return 1;
			}
			long num = ((long)argumentType << 32) + (long)valueType;
			if (num <= 149963078107492L)
			{
				if (num <= 44409961840868L)
				{
					if (num <= 2628519985508L)
					{
						if (num <= 120326193152L)
						{
							if (num <= 8657043456L)
							{
								if (num != 4362076160L)
								{
									if (num != 8657043456L)
									{
										return 0;
									}
									return 21;
								}
							}
							else if (num != 51539607580L)
							{
								if (num == 51606716416L)
								{
									return 7;
								}
								if (num != 120326193152L)
								{
									return 0;
								}
								return 6;
							}
						}
						else if (num <= 1529008357604L)
						{
							if (num == 979319652352L)
							{
								return 12;
							}
							if (num != 1529008357604L)
							{
								return 0;
							}
						}
						else
						{
							if (num == 1529075466240L)
							{
								return 11;
							}
							if (num == 2628519985380L)
							{
								return 3;
							}
							if (num != 2628519985508L)
							{
								return 0;
							}
						}
					}
					else if (num <= 26817775796580L)
					{
						if (num <= 2628587094016L)
						{
							if (num != 2628520378404L)
							{
								if (num != 2628587094016L)
								{
									return 0;
								}
								return 9;
							}
						}
						else
						{
							if (num == 4827610349568L)
							{
								return 13;
							}
							if (num == 26817775796452L)
							{
								return 4;
							}
							if (num != 26817775796580L)
							{
								return 0;
							}
							return 3;
						}
					}
					else if (num <= 26817776189476L)
					{
						if (num != 26817775796836L && num != 26817775797348L)
						{
							if (num != 26817776189476L)
							{
								return 0;
							}
							return 4;
						}
					}
					else if (num != 26817776975908L)
					{
						if (num == 26817842905088L)
						{
							return 4;
						}
						if (num != 44409961840868L)
						{
							return 0;
						}
						return 6;
					}
				}
				else if (num <= 79594333930084L)
				{
					if (num <= 44409961908324L)
					{
						if (num <= 44409961841252L)
						{
							if (num == 44409961840996L)
							{
								return 5;
							}
							if (num != 44409961841252L)
							{
								return 0;
							}
							return 4;
						}
						else
						{
							if (num == 44409961841764L)
							{
								return 4;
							}
							if (num == 44409961846884L)
							{
								return 3;
							}
							if (num != 44409961908324L)
							{
								return 0;
							}
							return 4;
						}
					}
					else if (num <= 44410028949504L)
					{
						if (num == 44409962233892L)
						{
							return 6;
						}
						if (num == 44409963020324L)
						{
							return 4;
						}
						if (num != 44410028949504L)
						{
							return 0;
						}
						return 14;
					}
					else
					{
						if (num == 79594333929700L)
						{
							return 7;
						}
						if (num == 79594333929828L)
						{
							return 6;
						}
						if (num != 79594333930084L)
						{
							return 0;
						}
						return 5;
					}
				}
				else if (num <= 79594334322724L)
				{
					if (num <= 79594333935716L)
					{
						if (num == 79594333930596L)
						{
							return 5;
						}
						if (num != 79594333935716L)
						{
							return 0;
						}
						return 4;
					}
					else
					{
						if (num == 79594333939812L || num == 79594333997156L)
						{
							return 3;
						}
						if (num != 79594334322724L)
						{
							return 0;
						}
						return 7;
					}
				}
				else if (num <= 79594350837796L)
				{
					if (num == 79594335109156L || num == 79594340352036L)
					{
						return 5;
					}
					if (num != 79594350837796L)
					{
						return 0;
					}
					return 3;
				}
				else
				{
					if (num == 79594401038336L)
					{
						return 15;
					}
					if (num == 149963078107364L)
					{
						return 8;
					}
					if (num != 149963078107492L)
					{
						return 0;
					}
					return 7;
				}
			}
			else
			{
				if (num > 290700566497380L)
				{
					if (num <= 5066704266723328L)
					{
						if (num <= 290700577079332L)
						{
							if (num <= 290700567642148L)
							{
								if (num == 290700566855716L)
								{
									return 5;
								}
								if (num != 290700567642148L)
								{
									return 0;
								}
								return 3;
							}
							else
							{
								if (num == 290700568690724L)
								{
									return 2;
								}
								if (num == 290700572885028L)
								{
									return 4;
								}
								if (num != 290700577079332L)
								{
									return 0;
								}
								return 3;
							}
						}
						else if (num <= 290700633571328L)
						{
							if (num == 290700583370788L || num == 290700600148004L)
							{
								return 2;
							}
							if (num != 290700633571328L)
							{
								return 0;
							}
							return 3;
						}
						else if (num != 1689004546195456L)
						{
							if (num == 5066704200007716L)
							{
								return 3;
							}
							if (num != 5066704266723328L)
							{
								return 0;
							}
							return 8;
						}
					}
					else if (num <= 45599100913057792L)
					{
						if (num <= 9570303894093824L)
						{
							if (num == 9570303833407524L)
							{
								return 2;
							}
							if (num != 9570303894093824L)
							{
								return 0;
							}
							return 5;
						}
						else
						{
							if (num == 27584702403575808L)
							{
								return 20;
							}
							if (num == 45599100852371492L)
							{
								return 3;
							}
							if (num != 45599100913057792L)
							{
								return 0;
							}
							return 17;
						}
					}
					else if (num <= 72620698677280768L)
					{
						if (num == 72620698610565156L)
						{
							return 9;
						}
						if (num == 72620698611351588L)
						{
							return 7;
						}
						if (num != 72620698677280768L)
						{
							return 0;
						}
						return 18;
					}
					else if (num != 144678292648493092L)
					{
						if (num == 144678292649279524L)
						{
							return 8;
						}
						if (num != 144678292715208704L)
						{
							return 0;
						}
						return 19;
					}
					return 10;
				}
				if (num <= 149963088724004L)
				{
					if (num <= 149963078125668L)
					{
						if (num <= 149963078108260L)
						{
							if (num != 149963078107748L && num != 149963078108260L)
							{
								return 0;
							}
							return 6;
						}
						else
						{
							if (num == 149963078113380L)
							{
								return 5;
							}
							if (num == 149963078117476L)
							{
								return 4;
							}
							if (num != 149963078125668L)
							{
								return 0;
							}
							return 3;
						}
					}
					else if (num <= 149963079286820L)
					{
						if (num != 149963078174820L)
						{
							if (num == 149963078500388L)
							{
								return 8;
							}
							if (num != 149963079286820L)
							{
								return 0;
							}
							return 6;
						}
					}
					else
					{
						if (num == 149963080335396L)
						{
							return 5;
						}
						if (num == 149963084529700L)
						{
							return 6;
						}
						if (num != 149963088724004L)
						{
							return 0;
						}
						return 4;
					}
				}
				else if (num <= 290700566462820L)
				{
					if (num <= 149963111792676L)
					{
						if (num == 149963095015460L)
						{
							return 4;
						}
						if (num != 149963111792676L)
						{
							return 0;
						}
						return 3;
					}
					else
					{
						if (num == 149963145216000L)
						{
							return 16;
						}
						if (num == 290700566462692L)
						{
							return 5;
						}
						if (num != 290700566462820L)
						{
							return 0;
						}
						return 4;
					}
				}
				else if (num <= 290700566468708L)
				{
					if (num == 290700566463076L || num == 290700566463588L)
					{
						return 3;
					}
					if (num != 290700566468708L)
					{
						return 0;
					}
				}
				else if (num != 290700566472804L && num != 290700566480996L && num != 290700566497380L)
				{
					return 0;
				}
			}
			return 2;
		}

		// Token: 0x060001BB RID: 443 RVA: 0x000041C4 File Offset: 0x000023C4
		internal static bool TryConvertToDouble(this DataValue value, out double convertedValue)
		{
			convertedValue = 0.0;
			if (value == null)
			{
				return false;
			}
			DataType type = value.Type;
			if (type <= DataType.Integer)
			{
				if (type != DataType.Number)
				{
					if (type == DataType.Integer)
					{
						convertedValue = (double)((IntegerValue)value).Value;
						return true;
					}
				}
				else
				{
					NumberValue<decimal> numberValue = value as NumberValue<decimal>;
					if (numberValue == null)
					{
						return false;
					}
					convertedValue = (double)numberValue.Value;
					return true;
				}
			}
			else
			{
				if (type == DataType.Decade)
				{
					convertedValue = (double)((DecadeValue)value).Value;
					return true;
				}
				if (type == DataType.Year)
				{
					convertedValue = (double)((DateItemValue)value).Value.Year.Value;
					return true;
				}
				if (type == DataType.Month)
				{
					convertedValue = (double)((DateItemValue)value).Value.Month.Value;
					return true;
				}
			}
			return false;
		}

		// Token: 0x060001BC RID: 444 RVA: 0x00004290 File Offset: 0x00002490
		public static bool TryFindBaseValueTypeOfOperands(this DataType left, DataType right, out DataType resultDataType)
		{
			if (left == right)
			{
				resultDataType = left;
				return true;
			}
			resultDataType = DataType.None;
			if (left.IsTime() && right.IsTime())
			{
				if (left.IsDecadeOrYear() && right.IsDecadeOrYear())
				{
					resultDataType = DataType.Year;
				}
				else if (left.IsTimePart())
				{
					resultDataType = (right.IsTimePart() ? DataType.TimeOfDay : DataType.DateTime);
				}
				else if (right.IsTimePart())
				{
					resultDataType = DataType.DateTime;
				}
				else
				{
					resultDataType = DataType.Date;
				}
			}
			else if (left.IsNumber() && right.IsNumber())
			{
				resultDataType = DataType.Number;
			}
			else if (left == DataType.Null)
			{
				resultDataType = right;
			}
			else if (right == DataType.Null)
			{
				resultDataType = left;
			}
			return resultDataType > DataType.None;
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00004340 File Offset: 0x00002540
		private static bool TryConvertToNumber(DataValue value, out DataValue convertedValue)
		{
			DataType type = value.Type;
			if (type <= DataType.Integer)
			{
				if (type == DataType.Number)
				{
					convertedValue = (NumberValue<decimal>)value;
					return true;
				}
				if (type == DataType.Integer)
				{
					convertedValue = new NumberValue<decimal>(((IntegerValue)value).Value);
					return true;
				}
			}
			else
			{
				if (type == DataType.Decade)
				{
					convertedValue = new NumberValue<decimal>(((DecadeValue)value).Value);
					return true;
				}
				if (type == DataType.Year)
				{
					convertedValue = new NumberValue<decimal>(((DateItemValue)value).Value.Year.Value);
					return true;
				}
			}
			convertedValue = null;
			return false;
		}

		// Token: 0x060001BE RID: 446 RVA: 0x000043E0 File Offset: 0x000025E0
		private static bool TryConvertToInteger(DataValue value, out DataValue convertedValue)
		{
			if (value.Type == DataType.Integer)
			{
				convertedValue = value;
				return true;
			}
			DataType type = value.Type;
			long num;
			if (type != DataType.Number)
			{
				if (type == DataType.Decade)
				{
					convertedValue = new IntegerValue((long)((DecadeValue)value).Value);
					return true;
				}
				if (type == DataType.Year)
				{
					convertedValue = new IntegerValue((long)((DateItemValue)value).Value.Year.Value);
					return true;
				}
			}
			else if (DataTypeExtensions.TryGetInt64FromNumber((NumberValue<decimal>)value, out num))
			{
				convertedValue = new IntegerValue(num);
				return true;
			}
			convertedValue = null;
			return false;
		}

		// Token: 0x060001BF RID: 447 RVA: 0x00004470 File Offset: 0x00002670
		private static bool TryConvertToYear(DataValue value, out DataValue convertedValue)
		{
			if (value.Type == DataType.Year)
			{
				convertedValue = value;
				return true;
			}
			DataType type = value.Type;
			int num;
			if (type != DataType.Number)
			{
				if (type != DataType.Integer)
				{
					if (type == DataType.Decade)
					{
						convertedValue = new DateItemValue(new int?(((DecadeValue)value).Value), null, null, null, null, null, null);
						return true;
					}
				}
				else if (DataTypeExtensions.TryGetInt32FromInteger((IntegerValue)value, out num) && DataTypeExtensions.IsValidYear(num))
				{
					convertedValue = new DateItemValue(new int?(num), null, null, null, null, null, null);
					return true;
				}
			}
			else if (DataTypeExtensions.TryGetInt32FromNumber((NumberValue<decimal>)value, out num) && DataTypeExtensions.IsValidYear(num))
			{
				convertedValue = new DateItemValue(new int?(num), null, null, null, null, null, null);
				return true;
			}
			convertedValue = null;
			return false;
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x000045C7 File Offset: 0x000027C7
		private static bool TryConvertToYearAndMonth(DataValue value, out DataValue convertedValue)
		{
			if (value.Type == DataType.YearAndMonth)
			{
				convertedValue = value;
				return true;
			}
			convertedValue = null;
			return false;
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x000045E0 File Offset: 0x000027E0
		private static bool TryConvertToDate(DataValue value, out DataValue convertedValue)
		{
			if (value.Type == DataType.Date)
			{
				convertedValue = value;
				return true;
			}
			DecadeValue decadeValue = value as DecadeValue;
			if (decadeValue != null)
			{
				convertedValue = new DateItem(new int?(decadeValue.Value), new int?(1), new int?(1), null, null, null, null);
				return true;
			}
			DateItemValue dateItemValue = value as DateItemValue;
			if (dateItemValue != null)
			{
				DateItem value2 = dateItemValue.Value;
				DataType type = value.Type;
				if (type <= DataType.YearToHour)
				{
					if (type == DataType.Year || type == DataType.YearAndMonth)
					{
						convertedValue = new DateItemValue(value2.Year, new int?(value2.Month ?? 1), new int?(1), null, null, null, null);
						return true;
					}
					if (type != DataType.YearToHour)
					{
						goto IL_0195;
					}
				}
				else if (type != DataType.YearToMinute && type != DataType.YearToSecond && type != DataType.DateTime)
				{
					goto IL_0195;
				}
				if (DataTypeExtensions.IsNullOrZero(value2.Hour) && DataTypeExtensions.IsNullOrZero(value2.Minute) && DataTypeExtensions.IsNullOrZero(value2.Second) && DataTypeExtensions.IsNullOrZero(value2.Millisecond))
				{
					convertedValue = new DateItemValue(value2.Year, value2.Month, value2.Day, null, null, null, null);
					return true;
				}
			}
			IL_0195:
			convertedValue = null;
			return false;
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x00004788 File Offset: 0x00002988
		private static bool TryConvertToYearToHour(DataValue value, out DataValue convertedValue)
		{
			if (value.Type == DataType.YearToHour)
			{
				convertedValue = value;
				return true;
			}
			DecadeValue decadeValue = value as DecadeValue;
			if (decadeValue != null)
			{
				convertedValue = new DateItem(new int?(decadeValue.Value), new int?(1), new int?(1), new int?(0), null, null, null);
				return true;
			}
			DateItemValue dateItemValue = value as DateItemValue;
			if (dateItemValue != null)
			{
				DateItem value2 = dateItemValue.Value;
				DataType type = value.Type;
				if (type <= DataType.Date)
				{
					if (type == DataType.Year || type == DataType.YearAndMonth || type == DataType.Date)
					{
						convertedValue = new DateItemValue(value2.Year, new int?(value2.Month ?? 1), new int?(value2.Day ?? 1), new int?(0), null, null, null);
						return true;
					}
				}
				else if (type == DataType.YearToMinute || type == DataType.YearToSecond || type == DataType.DateTime)
				{
					if (DataTypeExtensions.IsNullOrZero(value2.Minute) && DataTypeExtensions.IsNullOrZero(value2.Second) && DataTypeExtensions.IsNullOrZero(value2.Millisecond))
					{
						convertedValue = new DateItemValue(value2.Year, value2.Month, value2.Day, value2.Hour, null, null, null);
						return true;
					}
				}
			}
			convertedValue = null;
			return false;
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00004934 File Offset: 0x00002B34
		private static bool TryConvertToYearToMinute(DataValue value, out DataValue convertedValue)
		{
			if (value.Type == DataType.YearToMinute)
			{
				convertedValue = value;
				return true;
			}
			DecadeValue decadeValue = value as DecadeValue;
			if (decadeValue != null)
			{
				convertedValue = new DateItem(new int?(decadeValue.Value), new int?(1), new int?(1), new int?(0), new int?(0), null, null);
				return true;
			}
			DateItemValue dateItemValue = value as DateItemValue;
			if (dateItemValue != null)
			{
				DateItem value2 = dateItemValue.Value;
				DataType type = value.Type;
				if (type <= DataType.Date)
				{
					if (type != DataType.Year && type != DataType.YearAndMonth && type != DataType.Date)
					{
						goto IL_0190;
					}
				}
				else if (type != DataType.YearToHour)
				{
					if (type != DataType.YearToSecond && type != DataType.DateTime)
					{
						goto IL_0190;
					}
					if (DataTypeExtensions.IsNullOrZero(value2.Second) && DataTypeExtensions.IsNullOrZero(value2.Millisecond))
					{
						convertedValue = new DateItemValue(value2.Year, value2.Month, value2.Day, value2.Hour, value2.Minute, null, null);
						goto IL_0190;
					}
					goto IL_0190;
				}
				convertedValue = new DateItemValue(value2.Year, new int?(value2.Month ?? 1), new int?(value2.Day ?? 1), new int?(value2.Hour.GetValueOrDefault()), new int?(0), null, null);
				return true;
			}
			IL_0190:
			convertedValue = null;
			return false;
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00004AD8 File Offset: 0x00002CD8
		private static bool TryConvertToYearToSecond(DataValue value, out DataValue convertedValue)
		{
			if (value.Type == DataType.YearToSecond)
			{
				convertedValue = value;
				return true;
			}
			DecadeValue decadeValue = value as DecadeValue;
			if (decadeValue != null)
			{
				convertedValue = new DateItem(new int?(decadeValue.Value), new int?(1), new int?(1), new int?(0), new int?(0), new int?(0), null);
				return true;
			}
			DateItemValue dateItemValue = value as DateItemValue;
			if (dateItemValue != null)
			{
				DateItem value2 = dateItemValue.Value;
				DataType type = value.Type;
				if (type <= DataType.Date)
				{
					if (type != DataType.Year && type != DataType.YearAndMonth && type != DataType.Date)
					{
						goto IL_0189;
					}
				}
				else if (type != DataType.YearToHour && type != DataType.YearToMinute)
				{
					if (type != DataType.DateTime)
					{
						goto IL_0189;
					}
					if (DataTypeExtensions.IsNullOrZero(value2.Millisecond))
					{
						convertedValue = new DateItemValue(value2.Year, value2.Month, value2.Day, value2.Hour, value2.Minute, value2.Second, null);
						return true;
					}
					goto IL_0189;
				}
				convertedValue = new DateItemValue(value2.Year, new int?(value2.Month ?? 1), new int?(value2.Day ?? 1), new int?(value2.Hour.GetValueOrDefault()), new int?(value2.Minute.GetValueOrDefault()), new int?(0), null);
				return true;
			}
			IL_0189:
			convertedValue = null;
			return false;
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00004C74 File Offset: 0x00002E74
		private static bool TryConvertToDateTime(DataValue value, out DataValue convertedValue)
		{
			if (value.Type == DataType.DateTime)
			{
				convertedValue = value;
				return true;
			}
			DecadeValue decadeValue = value as DecadeValue;
			if (decadeValue != null)
			{
				convertedValue = new DateItem(new int?(decadeValue.Value), new int?(1), new int?(1), new int?(0), new int?(0), new int?(0), new int?(0));
				return true;
			}
			DateItemValue dateItemValue = value as DateItemValue;
			if (dateItemValue != null)
			{
				DateItem value2 = dateItemValue.Value;
				DataType type = value.Type;
				if (type <= DataType.Date)
				{
					if (type != DataType.Year && type != DataType.YearAndMonth && type != DataType.Date)
					{
						goto IL_0145;
					}
				}
				else if (type != DataType.YearToHour && type != DataType.YearToMinute && type != DataType.YearToSecond)
				{
					goto IL_0145;
				}
				convertedValue = new DateItemValue(value2.Year, new int?(value2.Month ?? 1), new int?(value2.Day ?? 1), new int?(value2.Hour.GetValueOrDefault()), new int?(value2.Minute.GetValueOrDefault()), new int?(value2.Second.GetValueOrDefault()), new int?(0));
				return true;
			}
			IL_0145:
			convertedValue = null;
			return false;
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00004DCC File Offset: 0x00002FCC
		private static bool TryConvertToTimeOfDay(DataValue value, out DataValue convertedValue)
		{
			if (value.Type == DataType.TimeOfDay)
			{
				convertedValue = value;
				return true;
			}
			DateItemValue dateItemValue = value as DateItemValue;
			if (dateItemValue != null)
			{
				DateItem value2 = dateItemValue.Value;
				DataType type = value.Type;
				if (type == DataType.HourAndMinute || type == DataType.HourToSecond)
				{
					int? hour = value2.Hour;
					int? minute = value2.Minute;
					int? num = new int?(value2.Second.GetValueOrDefault());
					int? num2 = new int?(0);
					convertedValue = new DateItemValue(null, null, null, hour, minute, num, num2);
					return true;
				}
			}
			convertedValue = null;
			return false;
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00004E74 File Offset: 0x00003074
		private static bool TryConvertToHourToSecond(DataValue value, out DataValue convertedValue)
		{
			if (value.Type == DataType.HourToSecond)
			{
				convertedValue = value;
				return true;
			}
			DateItemValue dateItemValue = value as DateItemValue;
			if (dateItemValue != null)
			{
				DateItem value2 = dateItemValue.Value;
				if (value.Type == DataType.TimeOfDay && DataTypeExtensions.IsNullOrZero(value2.Millisecond))
				{
					int? hour = value2.Hour;
					int? minute = value2.Minute;
					int? second = value2.Second;
					convertedValue = new DateItemValue(null, null, null, hour, minute, second, null);
					return true;
				}
			}
			convertedValue = null;
			return false;
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x00004F10 File Offset: 0x00003110
		private static bool TryConvertToHourAndMinute(DataValue value, out DataValue convertedValue)
		{
			if (value.Type == DataType.HourAndMinute)
			{
				convertedValue = value;
				return true;
			}
			DateItemValue dateItemValue = value as DateItemValue;
			if (dateItemValue != null)
			{
				DateItem value2 = dateItemValue.Value;
				DataType type = value.Type;
				if ((type == DataType.TimeOfDay || type == DataType.HourToSecond) && DataTypeExtensions.IsNullOrZero(value2.Millisecond) && DataTypeExtensions.IsNullOrZero(value2.Second))
				{
					int? hour = value2.Hour;
					int? minute = value2.Minute;
					int? second = value2.Second;
					convertedValue = new DateItemValue(null, null, null, hour, minute, second, null);
					return true;
				}
			}
			convertedValue = null;
			return false;
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x00004FC8 File Offset: 0x000031C8
		private static bool TryConvertToMonth(DataValue value, out DataValue convertedValue)
		{
			if (value.Type == DataType.Month)
			{
				convertedValue = value;
				return true;
			}
			DataType type = value.Type;
			int num;
			if (type != DataType.Number)
			{
				if (type == DataType.Integer)
				{
					if (DataTypeExtensions.TryGetInt32FromInteger((IntegerValue)value, out num) && DataTypeExtensions.IsValidMonth(num))
					{
						int? num2 = new int?(num);
						convertedValue = new DateItemValue(null, num2, null, null, null, null, null);
						return true;
					}
				}
			}
			else if (DataTypeExtensions.TryGetInt32FromNumber((NumberValue<decimal>)value, out num) && DataTypeExtensions.IsValidMonth(num))
			{
				int? num2 = new int?(num);
				convertedValue = new DateItemValue(null, num2, null, null, null, null, null);
				return true;
			}
			convertedValue = null;
			return false;
		}

		// Token: 0x060001CA RID: 458 RVA: 0x000050C5 File Offset: 0x000032C5
		private static bool TryConvertToMonthAndDay(DataValue value, out DataValue convertedValue)
		{
			if (value.Type == DataType.MonthAndDay)
			{
				convertedValue = value;
				return true;
			}
			convertedValue = null;
			return false;
		}

		// Token: 0x060001CB RID: 459 RVA: 0x000050DD File Offset: 0x000032DD
		private static bool TryConvertToMonthToMinute(DataValue value, out DataValue convertedValue)
		{
			if (value.Type == DataType.MonthToMinute)
			{
				convertedValue = value;
				return true;
			}
			convertedValue = null;
			return false;
		}

		// Token: 0x060001CC RID: 460 RVA: 0x000050F5 File Offset: 0x000032F5
		private static bool TryConvertToMonthToSecond(DataValue value, out DataValue convertedValue)
		{
			if (value.Type == DataType.MonthToSecond)
			{
				convertedValue = value;
				return true;
			}
			convertedValue = null;
			return false;
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00005110 File Offset: 0x00003310
		private static bool TryConvertToDecade(DataValue value, out DataValue convertedValue)
		{
			if (value.Type == DataType.Decade)
			{
				convertedValue = value;
				return true;
			}
			DataType type = value.Type;
			int num;
			if (type != DataType.Number)
			{
				if (type != DataType.Integer)
				{
					if (type == DataType.Year)
					{
						DateItemValue dateItemValue = (DateItemValue)value;
						if (DataTypeExtensions.IsValidDecade(dateItemValue.Value.Year.Value))
						{
							convertedValue = new DecadeValue(dateItemValue.Value.Year.Value);
							return true;
						}
					}
				}
				else if (DataTypeExtensions.TryGetInt32FromInteger((IntegerValue)value, out num) && DataTypeExtensions.IsValidDecade(num))
				{
					convertedValue = new DecadeValue(num);
					return true;
				}
			}
			else if (DataTypeExtensions.TryGetInt32FromNumber((NumberValue<decimal>)value, out num) && DataTypeExtensions.IsValidDecade(num))
			{
				convertedValue = new DecadeValue(num);
				return true;
			}
			convertedValue = null;
			return false;
		}

		// Token: 0x060001CE RID: 462 RVA: 0x000051CD File Offset: 0x000033CD
		private static bool TryConvertToText(DataValue value, out DataValue convertedValue)
		{
			if (value.Type == DataType.Text)
			{
				convertedValue = value;
				return true;
			}
			convertedValue = null;
			return false;
		}

		// Token: 0x060001CF RID: 463 RVA: 0x000051E4 File Offset: 0x000033E4
		private static bool TryConvertToBoolean(DataValue value, out DataValue convertedValue)
		{
			if (value.Type == DataType.Boolean)
			{
				convertedValue = value;
				return true;
			}
			DataType type = value.Type;
			int num;
			if (type != DataType.Number)
			{
				if (type == DataType.Integer)
				{
					if (DataTypeExtensions.TryGetInt32FromInteger((IntegerValue)value, out num) && num >= 0 && num <= 1)
					{
						convertedValue = ((num != 0) ? BooleanValue.True : BooleanValue.False);
						return true;
					}
				}
			}
			else if (DataTypeExtensions.TryGetInt32FromNumber((NumberValue<decimal>)value, out num) && num >= 0 && num <= 1)
			{
				convertedValue = ((num != 0) ? BooleanValue.True : BooleanValue.False);
				return true;
			}
			convertedValue = null;
			return false;
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x0000526C File Offset: 0x0000346C
		private static bool TryGetInt64FromNumber(NumberValue<decimal> numberValue, out long convertedValue)
		{
			if (numberValue.Value % 1m == 0m && numberValue.Value >= -9223372036854775808m && numberValue.Value <= 9223372036854775807m)
			{
				convertedValue = (long)numberValue.Value;
				return true;
			}
			convertedValue = 0L;
			return false;
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x000052E0 File Offset: 0x000034E0
		private static bool TryGetInt32FromNumber(NumberValue<decimal> numberValue, out int convertedValue)
		{
			if (numberValue.Value % 1m == 0m && numberValue.Value >= -2147483648m && numberValue.Value <= 2147483647m)
			{
				convertedValue = (int)numberValue.Value;
				return true;
			}
			convertedValue = 0;
			return false;
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x0000534A File Offset: 0x0000354A
		private static bool TryGetInt32FromInteger(IntegerValue integerValue, out int convertedValue)
		{
			if (integerValue.Value >= -2147483648L && integerValue.Value <= 2147483647L)
			{
				convertedValue = (int)integerValue.Value;
				return true;
			}
			convertedValue = 0;
			return false;
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x00005378 File Offset: 0x00003578
		private static bool IsNullOrZero(int? value)
		{
			if (value != null)
			{
				int? num = value;
				int num2 = 0;
				return (num.GetValueOrDefault() == num2) & (num != null);
			}
			return true;
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x000053A6 File Offset: 0x000035A6
		public static bool HasDataType(this DataType type, DataType flag)
		{
			return (type & flag) == flag;
		}

		// Token: 0x04000135 RID: 309
		private static readonly ReadOnlyDictionary<DataType, ReadOnlyCollection<DataType>> _assignableFromMap;

		// Token: 0x04000136 RID: 310
		private static readonly ReadOnlyDictionary<DataType, ReadOnlyDictionary<int, DataType>> _assignableToMap;

		// Token: 0x04000137 RID: 311
		private static readonly DataType _primitiveTypes = (DataType)133693439;
	}
}
