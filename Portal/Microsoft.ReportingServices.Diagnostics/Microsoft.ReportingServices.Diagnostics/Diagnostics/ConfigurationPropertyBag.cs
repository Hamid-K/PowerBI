using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000022 RID: 34
	[Serializable]
	internal class ConfigurationPropertyBag : Dictionary<ConfigurationProperty, ConfigurationPropertyInfo>
	{
		// Token: 0x06000082 RID: 130 RVA: 0x00003258 File Offset: 0x00001458
		public void Add(ConfigurationProperty propertyId, string value)
		{
			this.Add(propertyId, value, null);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003264 File Offset: 0x00001464
		public void Add(ConfigurationProperty propertyId, string value, string name)
		{
			if (base.ContainsKey(propertyId))
			{
				if (name == null)
				{
					name = propertyId.ToString();
				}
				throw new Exception(ErrorStringsWrapper.DuplicateConfigElement(name));
			}
			ConfigurationPropertyInfo configurationPropertyInfo = new ConfigurationPropertyInfo();
			configurationPropertyInfo.SpecifiedValue = value;
			base.Add(propertyId, configurationPropertyInfo);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x000032AD File Offset: 0x000014AD
		public ConfigurationPropertyBag()
		{
		}

		// Token: 0x06000085 RID: 133 RVA: 0x000032B5 File Offset: 0x000014B5
		protected ConfigurationPropertyBag(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x06000086 RID: 134 RVA: 0x000032C0 File Offset: 0x000014C0
		public void Merge(ConfigurationPropertyBag newProperties)
		{
			foreach (KeyValuePair<ConfigurationProperty, ConfigurationPropertyInfo> keyValuePair in newProperties)
			{
				base[keyValuePair.Key] = keyValuePair.Value;
			}
		}
	}
}
