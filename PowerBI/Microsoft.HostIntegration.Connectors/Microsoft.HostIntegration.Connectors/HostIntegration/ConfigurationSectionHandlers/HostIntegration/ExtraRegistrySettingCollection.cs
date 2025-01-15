using System;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.HostIntegration
{
	// Token: 0x020005CC RID: 1484
	[ConfigurationCollection(typeof(ExtraRegistrySetting), AddItemName = "extraRegistrySetting", CollectionType = ConfigurationElementCollectionType.BasicMap)]
	public class ExtraRegistrySettingCollection : ConfigurationElementCollection
	{
		// Token: 0x17000B18 RID: 2840
		// (get) Token: 0x0600339B RID: 13211 RVA: 0x00006F04 File Offset: 0x00005104
		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.BasicMap;
			}
		}

		// Token: 0x17000B19 RID: 2841
		// (get) Token: 0x0600339C RID: 13212 RVA: 0x000AD7ED File Offset: 0x000AB9ED
		protected override string ElementName
		{
			get
			{
				return "extraRegistrySetting";
			}
		}

		// Token: 0x17000B1A RID: 2842
		public ExtraRegistrySetting this[int index]
		{
			get
			{
				return (ExtraRegistrySetting)base.BaseGet(index);
			}
			set
			{
				if (base.BaseGet(index) != null)
				{
					base.BaseRemoveAt(index);
				}
				base.BaseAdd(index, value);
			}
		}

		// Token: 0x0600339F RID: 13215 RVA: 0x000AD802 File Offset: 0x000ABA02
		protected override ConfigurationElement CreateNewElement()
		{
			return new ExtraRegistrySetting();
		}

		// Token: 0x060033A0 RID: 13216 RVA: 0x000AD809 File Offset: 0x000ABA09
		protected override object GetElementKey(ConfigurationElement element)
		{
			return (element as ExtraRegistrySetting).GetElementKey();
		}

		// Token: 0x060033A1 RID: 13217 RVA: 0x00017EEC File Offset: 0x000160EC
		public void AddExtraRegistrySetting(ExtraRegistrySetting extraRegistrySetting)
		{
			base.BaseAdd(extraRegistrySetting, true);
		}

		// Token: 0x060033A2 RID: 13218 RVA: 0x000AD816 File Offset: 0x000ABA16
		public void RemoveExtraRegistrySetting(ExtraRegistrySetting extraRegistrySetting)
		{
			base.BaseRemove(extraRegistrySetting.GetElementKey());
		}
	}
}
