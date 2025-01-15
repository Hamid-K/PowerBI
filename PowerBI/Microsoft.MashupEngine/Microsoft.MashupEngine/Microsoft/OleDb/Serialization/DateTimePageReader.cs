using System;
using System.Collections.Generic;
using Microsoft.Data.Serialization;

namespace Microsoft.OleDb.Serialization
{
	// Token: 0x02001FB3 RID: 8115
	public static class DateTimePageReader
	{
		// Token: 0x0600C5FD RID: 50685 RVA: 0x002772D8 File Offset: 0x002754D8
		public static IPageReader Create(IList<Type> expectedColumnTypes, IPageReader reader)
		{
			Dictionary<int, ColumnConversion> dictionary = new Dictionary<int, ColumnConversion>();
			TableSchema schema = reader.Schema;
			for (int i = 0; i < schema.ColumnCount; i++)
			{
				Type type = expectedColumnTypes[i];
				if (schema.GetColumn(i).DataType == typeof(string) && DateTimePageReader.IsDateTimeType(type))
				{
					dictionary.Add(i, new ColumnConversion(expectedColumnTypes[i], DateTimePageReader.GetAddValue(expectedColumnTypes[i])));
				}
			}
			return ColumnConvertingPageReader.New(reader, dictionary);
		}

		// Token: 0x0600C5FE RID: 50686 RVA: 0x00277358 File Offset: 0x00275558
		private static bool IsDateTimeType(Type type)
		{
			return type == typeof(Date) || type == typeof(Time) || type == typeof(DateTime) || type == typeof(DateTimeOffset);
		}

		// Token: 0x0600C5FF RID: 50687 RVA: 0x002773B0 File Offset: 0x002755B0
		private static Action<object, Column> GetAddValue(Type type)
		{
			if (type == typeof(Date))
			{
				return new Action<object, Column>(DateTimePageReader.AddDate);
			}
			if (type == typeof(Time))
			{
				return new Action<object, Column>(DateTimePageReader.AddTime);
			}
			if (type == typeof(DateTime))
			{
				return new Action<object, Column>(DateTimePageReader.AddDateTime);
			}
			if (type == typeof(DateTimeOffset))
			{
				return new Action<object, Column>(DateTimePageReader.AddDateTimeOffset);
			}
			throw new InvalidOperationException();
		}

		// Token: 0x0600C600 RID: 50688 RVA: 0x00277440 File Offset: 0x00275640
		private static void AddDate(object obj, Column column)
		{
			string text = (string)obj;
			if (text.Length < 19)
			{
				DateTimePageReader.CheckLength(text, 10, 10);
			}
			else
			{
				DateTimePageReader.CheckLength(text, 19, 27);
			}
			Date date = DateTimePageReader.DateTimeReader.ReadDate(text, 0);
			column.AddValue(date);
		}

		// Token: 0x0600C601 RID: 50689 RVA: 0x00277488 File Offset: 0x00275688
		private static void AddTime(object obj, Column column)
		{
			string text = (string)obj;
			DateTimePageReader.CheckLength(text, 8, 16);
			Time time = DateTimePageReader.DateTimeReader.ReadTime(text, 0, text.Length);
			column.AddValue(time);
		}

		// Token: 0x0600C602 RID: 50690 RVA: 0x002774C0 File Offset: 0x002756C0
		private static void AddDateTime(object obj, Column column)
		{
			string text = (string)obj;
			DateTimePageReader.CheckLength(text, 19, 27);
			Date date = DateTimePageReader.DateTimeReader.ReadDate(text, 0);
			Time time = DateTimePageReader.DateTimeReader.ReadTime(text, 11, text.Length);
			DateTime dateTime = date.DateTime.Add(time.TimeSpan);
			column.AddValue(dateTime);
		}

		// Token: 0x0600C603 RID: 50691 RVA: 0x0027751C File Offset: 0x0027571C
		private static void AddDateTimeOffset(object obj, Column column)
		{
			string text = (string)obj;
			DateTimePageReader.CheckLength(text, 26, 34);
			Date date = DateTimePageReader.DateTimeReader.ReadDate(text, 0);
			Time time = DateTimePageReader.DateTimeReader.ReadTime(text, 11, text.Length - 7);
			TimeSpan timeSpan = DateTimePageReader.DateTimeReader.ReadOffset(text, text.Length - 6);
			DateTime dateTime = date.DateTime.Add(time.TimeSpan);
			DateTimeOffset dateTimeOffset = new DateTimeOffset(dateTime, timeSpan);
			column.AddValue(dateTimeOffset);
		}

		// Token: 0x0600C604 RID: 50692 RVA: 0x00277592 File Offset: 0x00275792
		private static void CheckLength(string value, int minLength, int maxLength)
		{
			if (value.Length < minLength || value.Length > maxLength)
			{
				throw new FormatException("Invalid date/time: " + value);
			}
		}

		// Token: 0x02001FB4 RID: 8116
		private static class DateTimeReader
		{
			// Token: 0x0600C605 RID: 50693 RVA: 0x002775B8 File Offset: 0x002757B8
			public static Date ReadDate(string value, int offset)
			{
				int num = DateTimePageReader.DateTimeReader.ReadD4(value, offset);
				int num2 = DateTimePageReader.DateTimeReader.ReadD2(value, offset + 5);
				int num3 = DateTimePageReader.DateTimeReader.ReadD2(value, offset + 8);
				return new Date(new DateTime(num, num2, num3));
			}

			// Token: 0x0600C606 RID: 50694 RVA: 0x002775EC File Offset: 0x002757EC
			public static Time ReadTime(string value, int offset, int offsetMax)
			{
				int num = DateTimePageReader.DateTimeReader.ReadD2(value, offset);
				int num2 = DateTimePageReader.DateTimeReader.ReadD2(value, offset + 3);
				int num3 = DateTimePageReader.DateTimeReader.ReadD2(value, offset + 6);
				int num4 = 0;
				if (offset + 9 < offsetMax)
				{
					num4 = DateTimePageReader.DateTimeReader.ReadD7(value, offset + 9, offsetMax);
				}
				return new Time(new TimeSpan(num, num2, num3) + new TimeSpan((long)num4));
			}

			// Token: 0x0600C607 RID: 50695 RVA: 0x00277644 File Offset: 0x00275844
			public static TimeSpan ReadOffset(string value, int offset)
			{
				int num = DateTimePageReader.DateTimeReader.ReadD2(value, offset + 1);
				int num2 = DateTimePageReader.DateTimeReader.ReadD2(value, offset + 4);
				TimeSpan timeSpan = new TimeSpan(num, num2, 0);
				if (value[offset] == '-')
				{
					timeSpan = -timeSpan;
				}
				return timeSpan;
			}

			// Token: 0x0600C608 RID: 50696 RVA: 0x00277684 File Offset: 0x00275884
			private static int ReadD7(string value, int offset, int offsetMax)
			{
				int num = 0;
				for (int i = offset; i < offsetMax; i++)
				{
					num = num * 10 + DateTimePageReader.DateTimeReader.ReadD1(value, i);
				}
				for (int j = offsetMax - offset; j < 7; j++)
				{
					num *= 10;
				}
				return num;
			}

			// Token: 0x0600C609 RID: 50697 RVA: 0x002776C0 File Offset: 0x002758C0
			private static int ReadD4(string value, int offset)
			{
				int num = DateTimePageReader.DateTimeReader.ReadD2(value, offset) * 100;
				int num2 = DateTimePageReader.DateTimeReader.ReadD2(value, offset + 2);
				return num + num2;
			}

			// Token: 0x0600C60A RID: 50698 RVA: 0x002776E4 File Offset: 0x002758E4
			private static int ReadD2(string value, int offset)
			{
				int num = (int)(value[offset] - '0');
				int num2 = (int)(value[offset + 1] - '0');
				return num * 10 + num2;
			}

			// Token: 0x0600C60B RID: 50699 RVA: 0x0027770D File Offset: 0x0027590D
			private static int ReadD1(string value, int offset)
			{
				return (int)(value[offset] - '0');
			}
		}
	}
}
