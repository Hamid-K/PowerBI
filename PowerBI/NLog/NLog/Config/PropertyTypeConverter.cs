using System;

namespace NLog.Config
{
	// Token: 0x0200019B RID: 411
	internal class PropertyTypeConverter : IPropertyTypeConverter
	{
		// Token: 0x060012BB RID: 4795 RVA: 0x00032E34 File Offset: 0x00031034
		public object Convert(object propertyValue, Type propertyType, string format, IFormatProvider formatProvider)
		{
			if (propertyType == null || propertyValue == null || propertyValue.GetType() == propertyType || propertyType == typeof(object))
			{
				return propertyValue;
			}
			string text;
			IFormattable formattable;
			if ((text = propertyValue as string) != null)
			{
				text = (propertyValue = text.Trim());
				if (propertyType == typeof(DateTime))
				{
					return PropertyTypeConverter.ConvertDateTime(format, formatProvider, text);
				}
				if (propertyType == typeof(DateTimeOffset))
				{
					return PropertyTypeConverter.ConvertDateTimeOffset(format, formatProvider, text);
				}
				if (propertyType == typeof(TimeSpan))
				{
					return PropertyTypeConverter.ConvertTimeSpan(format, formatProvider, text);
				}
				if (propertyType == typeof(Guid))
				{
					return PropertyTypeConverter.ConvertGuid(format, text);
				}
			}
			else if (!string.IsNullOrEmpty(format) && (formattable = propertyValue as IFormattable) != null)
			{
				propertyValue = formattable.ToString(format, formatProvider);
			}
			return global::System.Convert.ChangeType(propertyValue, propertyType, formatProvider);
		}

		// Token: 0x060012BC RID: 4796 RVA: 0x00032F17 File Offset: 0x00031117
		private static object ConvertGuid(string format, string propertyString)
		{
			return string.IsNullOrEmpty(format) ? Guid.Parse(propertyString) : Guid.ParseExact(propertyString, format);
		}

		// Token: 0x060012BD RID: 4797 RVA: 0x00032F35 File Offset: 0x00031135
		private static object ConvertTimeSpan(string format, IFormatProvider formatProvider, string propertyString)
		{
			if (!string.IsNullOrEmpty(format))
			{
				return TimeSpan.ParseExact(propertyString, format, formatProvider);
			}
			return TimeSpan.Parse(propertyString, formatProvider);
		}

		// Token: 0x060012BE RID: 4798 RVA: 0x00032F59 File Offset: 0x00031159
		private static object ConvertDateTimeOffset(string format, IFormatProvider formatProvider, string propertyString)
		{
			if (!string.IsNullOrEmpty(format))
			{
				return DateTimeOffset.ParseExact(propertyString, format, formatProvider);
			}
			return DateTimeOffset.Parse(propertyString, formatProvider);
		}

		// Token: 0x060012BF RID: 4799 RVA: 0x00032F7D File Offset: 0x0003117D
		private static object ConvertDateTime(string format, IFormatProvider formatProvider, string propertyString)
		{
			if (!string.IsNullOrEmpty(format))
			{
				return DateTime.ParseExact(propertyString, format, formatProvider);
			}
			return DateTime.Parse(propertyString, formatProvider);
		}
	}
}
