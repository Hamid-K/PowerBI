using System;
using System.ComponentModel;
using System.Globalization;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.Ti.Hip
{
	// Token: 0x02000574 RID: 1396
	public class EssoSecurityPolicyNameValidation : TypeConverter
	{
		// Token: 0x06002F79 RID: 12153 RVA: 0x000A28DE File Offset: 0x000A0ADE
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x06002F7A RID: 12154 RVA: 0x000A28FC File Offset: 0x000A0AFC
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (!(value is string))
			{
				return base.ConvertFrom(context, culture, value);
			}
			if (string.IsNullOrEmpty((string)value))
			{
				throw new FormatException("ESSO Security Policy Name cannot be blank");
			}
			return value;
		}

		// Token: 0x06002F7B RID: 12155 RVA: 0x000A2929 File Offset: 0x000A0B29
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
