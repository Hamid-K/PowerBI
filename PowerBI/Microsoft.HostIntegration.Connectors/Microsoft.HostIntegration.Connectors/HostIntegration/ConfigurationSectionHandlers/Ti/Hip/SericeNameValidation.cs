using System;
using System.ComponentModel;
using System.Globalization;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.Ti.Hip
{
	// Token: 0x02000579 RID: 1401
	public class SericeNameValidation : TypeConverter
	{
		// Token: 0x06002FA0 RID: 12192 RVA: 0x000A28DE File Offset: 0x000A0ADE
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x06002FA1 RID: 12193 RVA: 0x000A2B5C File Offset: 0x000A0D5C
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (!(value is string))
			{
				return base.ConvertFrom(context, culture, value);
			}
			string text = (string)value;
			char[] array = new char[]
			{
				'~', '`', '!', '@', '#', '$', '%', '^', '&', '*',
				'(', ')', ':', ';', '"', '\'', '<', '>', ',', '.',
				'{', '}', '[', ']', '|', '\\', '+', '=', '?', '/'
			};
			if (string.IsNullOrEmpty(text))
			{
				throw new FormatException("Service Name cannot be blank");
			}
			if (text.Length > 15)
			{
				throw new FormatException("Service Name cannot be more than 15 characters in length");
			}
			if (text.Contains(" "))
			{
				throw new FormatException("Service Name cannot contain spaces");
			}
			if (text.IndexOfAny(array) >= 0)
			{
				throw new FormatException("Service Name cannot contain special character");
			}
			return value;
		}

		// Token: 0x06002FA2 RID: 12194 RVA: 0x000A2929 File Offset: 0x000A0B29
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(string))
			{
				return value;
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
