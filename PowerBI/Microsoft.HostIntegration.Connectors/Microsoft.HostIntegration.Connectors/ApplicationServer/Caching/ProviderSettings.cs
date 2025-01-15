using System;
using System.Collections.Generic;
using System.Configuration;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200013E RID: 318
	[Serializable]
	internal sealed class ProviderSettings : ConfigurationElementCollection, ISerializable, IDeserializationCallback
	{
		// Token: 0x06000980 RID: 2432 RVA: 0x00016D51 File Offset: 0x00014F51
		private ProviderSettings()
		{
		}

		// Token: 0x06000981 RID: 2433 RVA: 0x000204D2 File Offset: 0x0001E6D2
		protected override ConfigurationElement CreateNewElement()
		{
			return new ProviderSetting();
		}

		// Token: 0x06000982 RID: 2434 RVA: 0x000204DC File Offset: 0x0001E6DC
		protected override object GetElementKey(ConfigurationElement element)
		{
			ProviderSetting providerSetting = (ProviderSetting)element;
			return providerSetting.Key;
		}

		// Token: 0x06000983 RID: 2435 RVA: 0x000204F6 File Offset: 0x0001E6F6
		internal ProviderSetting Get(string key)
		{
			return (ProviderSetting)base.BaseGet(key);
		}

		// Token: 0x06000984 RID: 2436 RVA: 0x00016D88 File Offset: 0x00014F88
		internal bool Delete(string name)
		{
			if (base.BaseGet(name) != null)
			{
				base.BaseRemove(name);
				return base.BaseGet(name) == null;
			}
			return false;
		}

		// Token: 0x06000985 RID: 2437 RVA: 0x00020504 File Offset: 0x0001E704
		internal bool Add(ProviderSetting setting)
		{
			if (base.BaseGet(this.GetElementKey(setting)) == null)
			{
				base.BaseAdd(setting, true);
				return true;
			}
			return false;
		}

		// Token: 0x06000986 RID: 2438 RVA: 0x00020530 File Offset: 0x0001E730
		public void OnDeserialization(object sender)
		{
			foreach (ProviderSetting providerSetting in this.tempSettings)
			{
				this.Add(providerSetting);
			}
			this.tempSettings = null;
		}

		// Token: 0x06000987 RID: 2439 RVA: 0x0002058C File Offset: 0x0001E78C
		public ProviderSettings(SerializationInfo info, StreamingContext context)
		{
			this.tempSettings = (List<ProviderSetting>)info.GetValue("Settings", typeof(List<ProviderSetting>));
		}

		// Token: 0x06000988 RID: 2440 RVA: 0x000205B4 File Offset: 0x0001E7B4
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			List<ProviderSetting> list = new List<ProviderSetting>();
			for (int i = 0; i < base.Count; i++)
			{
				list.Add((ProviderSetting)base.BaseGet(i));
			}
			info.AddValue("Settings", list, typeof(List<ProviderSetting>));
		}

		// Token: 0x040006DA RID: 1754
		internal const string SETTINGS = "Settings";

		// Token: 0x040006DB RID: 1755
		[NonSerialized]
		private List<ProviderSetting> tempSettings;
	}
}
