using System;
using System.ComponentModel;
using System.Globalization;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.Ti.Hip
{
	// Token: 0x0200057E RID: 1406
	public class SnaHostEnvironmentNameValidation : TypeConverter
	{
		// Token: 0x06002FDD RID: 12253 RVA: 0x000A28DE File Offset: 0x000A0ADE
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x06002FDE RID: 12254 RVA: 0x000A30F2 File Offset: 0x000A12F2
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (!(value is string))
			{
				return base.ConvertFrom(context, culture, value);
			}
			if (string.IsNullOrEmpty((string)value))
			{
				throw new FormatException("SNA Host Environment Name cannot be blank");
			}
			return value;
		}

		// Token: 0x06002FDF RID: 12255 RVA: 0x000A2929 File Offset: 0x000A0B29
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
