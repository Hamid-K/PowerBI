using System;
using System.ComponentModel;
using System.Globalization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020001F1 RID: 497
	public class ReportSizeConverter : TypeConverter
	{
		// Token: 0x060010A0 RID: 4256 RVA: 0x000270E3 File Offset: 0x000252E3
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x060010A1 RID: 4257 RVA: 0x00027104 File Offset: 0x00025304
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value == null)
			{
				return null;
			}
			if (!(value is string))
			{
				return base.ConvertFrom(context, culture, value);
			}
			string text = ((string)value).Trim();
			if (text.Length == 0)
			{
				return new ReportSize(0.0, SizeTypes.Invalid);
			}
			return new ReportSize(text, culture);
		}

		// Token: 0x060010A2 RID: 4258 RVA: 0x00027160 File Offset: 0x00025360
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(string) && value is ReportSize)
			{
				return ((ReportSize)value).ToString(null, culture);
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
