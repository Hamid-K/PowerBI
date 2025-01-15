using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.Ti.Wip
{
	// Token: 0x02000596 RID: 1430
	public class ReadOrderAppConfigDropDownList : EnumConverter
	{
		// Token: 0x0600316A RID: 12650 RVA: 0x000A5069 File Offset: 0x000A3269
		public ReadOrderAppConfigDropDownList()
			: base(typeof(WipConfigurationPriority))
		{
		}

		// Token: 0x0600316B RID: 12651 RVA: 0x000A1A1D File Offset: 0x0009FC1D
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x0600316C RID: 12652 RVA: 0x000A507C File Offset: 0x000A327C
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is string)
			{
				string text = (string)value;
				if (text != null)
				{
					if (text == "First")
					{
						return WipConfigurationPriority.First;
					}
					if (text == "Second")
					{
						return WipConfigurationPriority.Second;
					}
					if (text == "Third")
					{
						return WipConfigurationPriority.Third;
					}
				}
				return WipConfigurationPriority.Unused;
			}
			return base.ConvertFrom(context, culture, value);
		}

		// Token: 0x0600316D RID: 12653 RVA: 0x000A50EC File Offset: 0x000A32EC
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(string))
			{
				return ((WipConfigurationPriority)value).ToString();
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		// Token: 0x0600316E RID: 12654 RVA: 0x00006F04 File Offset: 0x00005104
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return false;
		}

		// Token: 0x0600316F RID: 12655 RVA: 0x00002B16 File Offset: 0x00000D16
		public override bool IsValid(ITypeDescriptorContext context, object value)
		{
			return true;
		}

		// Token: 0x06003170 RID: 12656 RVA: 0x00002B16 File Offset: 0x00000D16
		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x06003171 RID: 12657 RVA: 0x000A512C File Offset: 0x000A332C
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			ReadOrder readOrder = ((ReadOrder)context.Instance).GetReadOrder();
			ArrayList arrayList = new ArrayList();
			if (readOrder.Cache == WipConfigurationPriority.Unused && readOrder.Registry == WipConfigurationPriority.Unused)
			{
				arrayList.Add(WipConfigurationPriority.First);
			}
			else
			{
				arrayList.Add(WipConfigurationPriority.First);
				bool flag = true;
				bool flag2 = true;
				if (readOrder.Cache != WipConfigurationPriority.First && readOrder.AppConfig != WipConfigurationPriority.First && readOrder.Registry != WipConfigurationPriority.First)
				{
					flag = false;
				}
				if (readOrder.Cache != WipConfigurationPriority.Second && readOrder.AppConfig != WipConfigurationPriority.Second && readOrder.Registry != WipConfigurationPriority.Second)
				{
					flag2 = false;
				}
				if (readOrder.Cache != WipConfigurationPriority.First || readOrder.Registry != WipConfigurationPriority.First)
				{
					arrayList.Add(WipConfigurationPriority.Second);
				}
				if (flag && flag2)
				{
					arrayList.Add(WipConfigurationPriority.Third);
				}
				if (readOrder.AppConfig != WipConfigurationPriority.Unused)
				{
					arrayList.Add(WipConfigurationPriority.Unused);
				}
			}
			return new TypeConverter.StandardValuesCollection(arrayList);
		}
	}
}
