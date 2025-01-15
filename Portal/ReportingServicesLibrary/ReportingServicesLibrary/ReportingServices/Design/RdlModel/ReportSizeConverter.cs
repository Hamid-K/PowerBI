using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x0200040A RID: 1034
	internal sealed class ReportSizeConverter : TypeConverter
	{
		// Token: 0x060020E6 RID: 8422 RVA: 0x0007FD42 File Offset: 0x0007DF42
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x060020E7 RID: 8423 RVA: 0x0007FD60 File Offset: 0x0007DF60
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(InstanceDescriptor) || base.CanConvertTo(context, destinationType);
		}

		// Token: 0x060020E8 RID: 8424 RVA: 0x0007FD80 File Offset: 0x0007DF80
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (!(value is string))
			{
				return base.ConvertFrom(context, culture, value);
			}
			string text = ((string)value).Trim();
			if (text.Length == 0)
			{
				return new ReportSize(new Unit(0), new Unit(0));
			}
			if (culture == null)
			{
				culture = CultureInfo.CurrentCulture;
			}
			char c = culture.TextInfo.ListSeparator[0];
			string[] array = Utils.ReportExpressionSplit(text, c);
			if (array.Length == 2)
			{
				return new ReportSize(new Unit(array[0], culture), new Unit(array[1], culture));
			}
			throw new ArgumentException(SRErrors.TextParseFailedFormat(text, "Width, Height"));
		}

		// Token: 0x060020E9 RID: 8425 RVA: 0x0007FE24 File Offset: 0x0007E024
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == typeof(string) && value is ReportSize)
			{
				ReportSize reportSize = (ReportSize)value;
				if (culture == null)
				{
					culture = CultureInfo.CurrentCulture;
				}
				string text = culture.TextInfo.ListSeparator + " ";
				string[] array = new string[2];
				int num = 0;
				array[num++] = reportSize.Width.ToString();
				array[num++] = reportSize.Height.ToString();
				return string.Join(text, array);
			}
			if (destinationType == typeof(InstanceDescriptor) && value is ReportSize)
			{
				ReportSize reportSize2 = (ReportSize)value;
				ConstructorInfo constructor = typeof(ReportSize).GetConstructor(new Type[]
				{
					typeof(int),
					typeof(int)
				});
				if (constructor != null)
				{
					return new InstanceDescriptor(constructor, new object[] { reportSize2.Width, reportSize2.Height });
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		// Token: 0x060020EA RID: 8426 RVA: 0x0007FF64 File Offset: 0x0007E164
		public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
		{
			return new ReportSize((Unit)propertyValues["Width"], (Unit)propertyValues["Height"]);
		}

		// Token: 0x060020EB RID: 8427 RVA: 0x000053DC File Offset: 0x000035DC
		public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x060020EC RID: 8428 RVA: 0x0007FF90 File Offset: 0x0007E190
		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			return TypeDescriptor.GetProperties(typeof(ReportSize), attributes).Sort(new string[] { "Width", "Height" });
		}

		// Token: 0x060020ED RID: 8429 RVA: 0x000053DC File Offset: 0x000035DC
		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return true;
		}
	}
}
