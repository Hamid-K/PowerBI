using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase.Serialization
{
	// Token: 0x020000D3 RID: 211
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public static class DateTimePageReader
	{
		// Token: 0x060003D7 RID: 983 RVA: 0x0000B7F4 File Offset: 0x000099F4
		public static IPageReader Create(IList<Type> expectedColumnTypes, IPageReader reader)
		{
			Dictionary<int, ColumnConversion> dictionary = new Dictionary<int, ColumnConversion>();
			DataTable schemaTable = reader.SchemaTable;
			for (int i = 0; i < schemaTable.Rows.Count; i++)
			{
				Type type = expectedColumnTypes[i];
				if ((Type)schemaTable.Rows[i]["DataType"] == typeof(string) && DateTimePageReader.IsDateTimeType(type))
				{
					dictionary.Add(i, new ColumnConversion(expectedColumnTypes[i], DateTimePageReader.GetAddValue(expectedColumnTypes[i])));
				}
			}
			return ColumnConvertingPageReader.New(reader, dictionary);
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x0000B888 File Offset: 0x00009A88
		private static bool IsDateTimeType(Type type)
		{
			return type == typeof(Date) || type == typeof(Time) || type == typeof(DateTime) || type == typeof(DateTimeOffset);
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x0000B8E0 File Offset: 0x00009AE0
		private static Action<object, Column> GetAddValue(Type type)
		{
			if (type == typeof(Date))
			{
				Action<object, Column> action;
				if ((action = DateTimePageReader.<>O.<0>__AddDate) == null)
				{
					action = (DateTimePageReader.<>O.<0>__AddDate = new Action<object, Column>(DateTimePageReader.AddDate));
				}
				return action;
			}
			if (type == typeof(Time))
			{
				Action<object, Column> action2;
				if ((action2 = DateTimePageReader.<>O.<1>__AddTime) == null)
				{
					action2 = (DateTimePageReader.<>O.<1>__AddTime = new Action<object, Column>(DateTimePageReader.AddTime));
				}
				return action2;
			}
			if (type == typeof(DateTime))
			{
				Action<object, Column> action3;
				if ((action3 = DateTimePageReader.<>O.<2>__AddDateTime) == null)
				{
					action3 = (DateTimePageReader.<>O.<2>__AddDateTime = new Action<object, Column>(DateTimePageReader.AddDateTime));
				}
				return action3;
			}
			if (type == typeof(DateTimeOffset))
			{
				Action<object, Column> action4;
				if ((action4 = DateTimePageReader.<>O.<3>__AddDateTimeOffset) == null)
				{
					action4 = (DateTimePageReader.<>O.<3>__AddDateTimeOffset = new Action<object, Column>(DateTimePageReader.AddDateTimeOffset));
				}
				return action4;
			}
			throw new InvalidOperationException();
		}

		// Token: 0x060003DA RID: 986 RVA: 0x0000B9AC File Offset: 0x00009BAC
		private static void AddDate(object obj, Column column)
		{
			string text = (string)obj;
			DateTimePageReader.CheckLength(text, 10, 10);
			Date date = DateTimePageReader.DateTimeReader.ReadDate(text, 0);
			column.AddValue(date);
		}

		// Token: 0x060003DB RID: 987 RVA: 0x0000B9DC File Offset: 0x00009BDC
		private static void AddTime(object obj, Column column)
		{
			string text = (string)obj;
			DateTimePageReader.CheckLength(text, 8, 16);
			Time time = DateTimePageReader.DateTimeReader.ReadTime(text, 0, text.Length);
			column.AddValue(time);
		}

		// Token: 0x060003DC RID: 988 RVA: 0x0000BA14 File Offset: 0x00009C14
		private static void AddDateTime(object obj, Column column)
		{
			string text = (string)obj;
			DateTimePageReader.CheckLength(text, 19, 27);
			Date date = DateTimePageReader.DateTimeReader.ReadDate(text, 0);
			Time time = DateTimePageReader.DateTimeReader.ReadTime(text, 11, text.Length);
			DateTime dateTime = date.DateTime.Add(time.TimeSpan);
			column.AddValue(dateTime);
		}

		// Token: 0x060003DD RID: 989 RVA: 0x0000BA70 File Offset: 0x00009C70
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

		// Token: 0x060003DE RID: 990 RVA: 0x0000BAE6 File Offset: 0x00009CE6
		private static void CheckLength(string value, int minLength, int maxLength)
		{
			if (value.Length < minLength || value.Length > maxLength)
			{
				throw new FormatException("Invalid date/time: " + value);
			}
		}

		// Token: 0x020000F9 RID: 249
		[global::System.Runtime.CompilerServices.Nullable(0)]
		internal static class DateTimeReader
		{
			// Token: 0x0600050C RID: 1292 RVA: 0x0000F630 File Offset: 0x0000D830
			public static Date ReadDate(string value, int offset)
			{
				int num = DateTimePageReader.DateTimeReader.ReadD4(value, offset);
				int num2 = DateTimePageReader.DateTimeReader.ReadD2(value, offset + 5);
				int num3 = DateTimePageReader.DateTimeReader.ReadD2(value, offset + 8);
				return new Date(new DateTime(num, num2, num3));
			}

			// Token: 0x0600050D RID: 1293 RVA: 0x0000F664 File Offset: 0x0000D864
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

			// Token: 0x0600050E RID: 1294 RVA: 0x0000F6BC File Offset: 0x0000D8BC
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

			// Token: 0x0600050F RID: 1295 RVA: 0x0000F6FC File Offset: 0x0000D8FC
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

			// Token: 0x06000510 RID: 1296 RVA: 0x0000F738 File Offset: 0x0000D938
			private static int ReadD4(string value, int offset)
			{
				int num = DateTimePageReader.DateTimeReader.ReadD2(value, offset) * 100;
				int num2 = DateTimePageReader.DateTimeReader.ReadD2(value, offset + 2);
				return num + num2;
			}

			// Token: 0x06000511 RID: 1297 RVA: 0x0000F75C File Offset: 0x0000D95C
			private static int ReadD2(string value, int offset)
			{
				int num = (int)(value[offset] - '0');
				int num2 = (int)(value[offset + 1] - '0');
				return num * 10 + num2;
			}

			// Token: 0x06000512 RID: 1298 RVA: 0x0000F785 File Offset: 0x0000D985
			private static int ReadD1(string value, int offset)
			{
				return (int)(value[offset] - '0');
			}
		}

		// Token: 0x020000FA RID: 250
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04000428 RID: 1064
			[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1, 1 })]
			public static Action<object, Column> <0>__AddDate;

			// Token: 0x04000429 RID: 1065
			[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1, 1 })]
			public static Action<object, Column> <1>__AddTime;

			// Token: 0x0400042A RID: 1066
			[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1, 1 })]
			public static Action<object, Column> <2>__AddDateTime;

			// Token: 0x0400042B RID: 1067
			[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1, 1 })]
			public static Action<object, Column> <3>__AddDateTimeOffset;
		}
	}
}
