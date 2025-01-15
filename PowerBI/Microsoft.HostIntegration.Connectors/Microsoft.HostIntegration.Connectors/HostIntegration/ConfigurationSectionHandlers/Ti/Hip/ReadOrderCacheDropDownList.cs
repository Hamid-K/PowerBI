using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.Ti.Hip
{
	// Token: 0x0200056C RID: 1388
	public class ReadOrderCacheDropDownList : EnumConverter
	{
		// Token: 0x06002F1D RID: 12061 RVA: 0x000A1A0B File Offset: 0x0009FC0B
		public ReadOrderCacheDropDownList()
			: base(typeof(HipConfigurationPriority))
		{
		}

		// Token: 0x06002F1E RID: 12062 RVA: 0x000A1A1D File Offset: 0x0009FC1D
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x06002F1F RID: 12063 RVA: 0x000A1B90 File Offset: 0x0009FD90
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is string)
			{
				string text = (string)value;
				if (text != null)
				{
					if (text == "First")
					{
						return HipConfigurationPriority.First;
					}
					if (text == "Second")
					{
						return HipConfigurationPriority.Second;
					}
				}
				return HipConfigurationPriority.Unused;
			}
			return base.ConvertFrom(context, culture, value);
		}

		// Token: 0x06002F20 RID: 12064 RVA: 0x000A1BEC File Offset: 0x0009FDEC
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(string))
			{
				return ((HipConfigurationPriority)value).ToString();
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		// Token: 0x06002F21 RID: 12065 RVA: 0x00006F04 File Offset: 0x00005104
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return false;
		}

		// Token: 0x06002F22 RID: 12066 RVA: 0x00002B16 File Offset: 0x00000D16
		public override bool IsValid(ITypeDescriptorContext context, object value)
		{
			return true;
		}

		// Token: 0x06002F23 RID: 12067 RVA: 0x00002B16 File Offset: 0x00000D16
		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x06002F24 RID: 12068 RVA: 0x000A1C2C File Offset: 0x0009FE2C
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			ReadOrder readOrder = ((ReadOrder)context.Instance).GetReadOrder();
			ArrayList arrayList = new ArrayList();
			if (readOrder.AppConfig == HipConfigurationPriority.Unused)
			{
				arrayList.Add(HipConfigurationPriority.First);
			}
			else if (readOrder.AppConfig == HipConfigurationPriority.First)
			{
				arrayList.Add(HipConfigurationPriority.First);
				arrayList.Add(HipConfigurationPriority.Second);
				arrayList.Add(HipConfigurationPriority.Unused);
			}
			else if (readOrder.AppConfig == HipConfigurationPriority.Second)
			{
				arrayList.Add(HipConfigurationPriority.First);
				arrayList.Add(HipConfigurationPriority.Second);
				arrayList.Add(HipConfigurationPriority.Unused);
			}
			else
			{
				arrayList.Add(HipConfigurationPriority.First);
			}
			arrayList.Sort();
			return new TypeConverter.StandardValuesCollection(arrayList);
		}
	}
}
