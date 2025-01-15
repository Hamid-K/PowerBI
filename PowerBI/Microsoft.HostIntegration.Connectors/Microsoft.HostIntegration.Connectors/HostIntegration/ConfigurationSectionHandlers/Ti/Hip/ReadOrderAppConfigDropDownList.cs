using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.Ti.Hip
{
	// Token: 0x0200056B RID: 1387
	public class ReadOrderAppConfigDropDownList : EnumConverter
	{
		// Token: 0x06002F15 RID: 12053 RVA: 0x000A1A0B File Offset: 0x0009FC0B
		public ReadOrderAppConfigDropDownList()
			: base(typeof(HipConfigurationPriority))
		{
		}

		// Token: 0x06002F16 RID: 12054 RVA: 0x000A1A1D File Offset: 0x0009FC1D
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x06002F17 RID: 12055 RVA: 0x000A1A3C File Offset: 0x0009FC3C
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

		// Token: 0x06002F18 RID: 12056 RVA: 0x000A1A98 File Offset: 0x0009FC98
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(string))
			{
				return ((HipConfigurationPriority)value).ToString();
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		// Token: 0x06002F19 RID: 12057 RVA: 0x00006F04 File Offset: 0x00005104
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return false;
		}

		// Token: 0x06002F1A RID: 12058 RVA: 0x00002B16 File Offset: 0x00000D16
		public override bool IsValid(ITypeDescriptorContext context, object value)
		{
			return true;
		}

		// Token: 0x06002F1B RID: 12059 RVA: 0x00002B16 File Offset: 0x00000D16
		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x06002F1C RID: 12060 RVA: 0x000A1AD8 File Offset: 0x0009FCD8
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			ReadOrder readOrder = ((ReadOrder)context.Instance).GetReadOrder();
			ArrayList arrayList = new ArrayList();
			if (readOrder.Cache == HipConfigurationPriority.Unused)
			{
				arrayList.Add(HipConfigurationPriority.First);
			}
			else if (readOrder.Cache == HipConfigurationPriority.First)
			{
				arrayList.Add(HipConfigurationPriority.First);
				arrayList.Add(HipConfigurationPriority.Second);
				arrayList.Add(HipConfigurationPriority.Unused);
			}
			else if (readOrder.Cache == HipConfigurationPriority.Second)
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
