using System;
using System.ComponentModel;
using System.Globalization;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.Ti.Wip
{
	// Token: 0x0200059B RID: 1435
	public class RemoteEnvironmentNameValidation : TypeConverter
	{
		// Token: 0x0600319E RID: 12702 RVA: 0x000A28DE File Offset: 0x000A0ADE
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x0600319F RID: 12703 RVA: 0x000A6A28 File Offset: 0x000A4C28
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (!(value is string))
			{
				return base.ConvertFrom(context, culture, value);
			}
			if (string.IsNullOrEmpty((string)value))
			{
				throw new FormatException("Remote Environment Name cannot be blank");
			}
			return value;
		}

		// Token: 0x060031A0 RID: 12704 RVA: 0x000A2929 File Offset: 0x000A0B29
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
