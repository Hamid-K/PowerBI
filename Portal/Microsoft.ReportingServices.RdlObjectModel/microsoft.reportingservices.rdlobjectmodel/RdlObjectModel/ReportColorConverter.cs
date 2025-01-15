using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020001D4 RID: 468
	internal sealed class ReportColorConverter : TypeConverter
	{
		// Token: 0x06000F4F RID: 3919 RVA: 0x00024D91 File Offset: 0x00022F91
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || sourceType == typeof(Color) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x06000F50 RID: 3920 RVA: 0x00024DC4 File Offset: 0x00022FC4
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is string)
			{
				return new ReportColor(((string)value).Trim());
			}
			if (value is Color)
			{
				return new ReportColor((Color)value);
			}
			return base.ConvertFrom(context, culture, value);
		}

		// Token: 0x06000F51 RID: 3921 RVA: 0x00024E11 File Offset: 0x00023011
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(string) || destinationType == typeof(Color);
		}

		// Token: 0x06000F52 RID: 3922 RVA: 0x00024E38 File Offset: 0x00023038
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (value is ReportColor)
			{
				if (destinationType == typeof(string))
				{
					return ((ReportColor)value).ToString();
				}
				if (destinationType == typeof(Color) && value is ReportColor)
				{
					return ((ReportColor)value).Color;
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
