using System;
using System.ComponentModel;
using System.Globalization;

namespace NLog.Attributes
{
	// Token: 0x020001BE RID: 446
	public class LogLevelTypeConverter : TypeConverter
	{
		// Token: 0x060013F7 RID: 5111 RVA: 0x000364B0 File Offset: 0x000346B0
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || LogLevelTypeConverter.IsNumericType(sourceType) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x060013F8 RID: 5112 RVA: 0x000364D8 File Offset: 0x000346D8
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (((value != null) ? value.GetType() : null) == typeof(string))
			{
				return LogLevel.FromString(value.ToString());
			}
			if (LogLevelTypeConverter.IsNumericType((value != null) ? value.GetType() : null))
			{
				return LogLevel.FromOrdinal(Convert.ToInt32(value));
			}
			return base.ConvertFrom(context, culture, value);
		}

		// Token: 0x060013F9 RID: 5113 RVA: 0x00036536 File Offset: 0x00034736
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(string) || LogLevelTypeConverter.IsNumericType(destinationType) || base.CanConvertTo(context, destinationType);
		}

		// Token: 0x060013FA RID: 5114 RVA: 0x0003655C File Offset: 0x0003475C
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			LogLevel logLevel;
			if ((logLevel = value as LogLevel) != null)
			{
				if (destinationType == typeof(string))
				{
					return logLevel.ToString();
				}
				if (LogLevelTypeConverter.IsNumericType(destinationType))
				{
					return Convert.ChangeType(logLevel.Ordinal, destinationType, culture);
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		// Token: 0x060013FB RID: 5115 RVA: 0x000365B8 File Offset: 0x000347B8
		private static bool IsNumericType(Type sourceType)
		{
			return sourceType == typeof(int) || sourceType == typeof(uint) || sourceType == typeof(long) || sourceType == typeof(ulong) || sourceType == typeof(short) || sourceType == typeof(ushort);
		}
	}
}
