using System;
using System.ComponentModel;
using System.Globalization;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.Ti.Wip
{
	// Token: 0x02000592 RID: 1426
	public class WipObjectNameValidation : TypeConverter
	{
		// Token: 0x06003143 RID: 12611 RVA: 0x000A28DE File Offset: 0x000A0ADE
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x06003144 RID: 12612 RVA: 0x000A4C42 File Offset: 0x000A2E42
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (!(value is string))
			{
				return base.ConvertFrom(context, culture, value);
			}
			if (string.IsNullOrEmpty((string)value))
			{
				throw new FormatException("Object Name cannot be blank");
			}
			return value;
		}

		// Token: 0x06003145 RID: 12613 RVA: 0x000A2929 File Offset: 0x000A0B29
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
