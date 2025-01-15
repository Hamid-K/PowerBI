using System;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.Ti.Hip
{
	// Token: 0x02000576 RID: 1398
	[ConfigurationCollection(typeof(EssoSecurityPolicy), AddItemName = "essoSecurityPolicy", CollectionType = ConfigurationElementCollectionType.BasicMap)]
	public class EssoSecurityPolicyCollection : ConfigurationElementCollection
	{
		// Token: 0x170009EE RID: 2542
		// (get) Token: 0x06002F94 RID: 12180 RVA: 0x00006F04 File Offset: 0x00005104
		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.BasicMap;
			}
		}

		// Token: 0x170009EF RID: 2543
		// (get) Token: 0x06002F95 RID: 12181 RVA: 0x000A2AD8 File Offset: 0x000A0CD8
		protected override string ElementName
		{
			get
			{
				return "essoSecurityPolicy";
			}
		}

		// Token: 0x170009F0 RID: 2544
		// (get) Token: 0x06002F96 RID: 12182 RVA: 0x000A2ADF File Offset: 0x000A0CDF
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return EssoSecurityPolicyCollection.s_properties;
			}
		}

		// Token: 0x170009F1 RID: 2545
		public EssoSecurityPolicy this[int index]
		{
			get
			{
				return (EssoSecurityPolicy)base.BaseGet(index);
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

		// Token: 0x170009F2 RID: 2546
		public EssoSecurityPolicy this[string name]
		{
			get
			{
				return (EssoSecurityPolicy)base.BaseGet(name);
			}
		}

		// Token: 0x06002F9A RID: 12186 RVA: 0x000A2B02 File Offset: 0x000A0D02
		protected override ConfigurationElement CreateNewElement()
		{
			return new EssoSecurityPolicy();
		}

		// Token: 0x06002F9B RID: 12187 RVA: 0x000A2B09 File Offset: 0x000A0D09
		protected override object GetElementKey(ConfigurationElement element)
		{
			return (element as EssoSecurityPolicy).GetElementKey();
		}

		// Token: 0x06002F9C RID: 12188 RVA: 0x00017EEC File Offset: 0x000160EC
		public void AddEssoSecurityPolicy(EssoSecurityPolicy essoSecurityPolicy)
		{
			base.BaseAdd(essoSecurityPolicy, true);
		}

		// Token: 0x06002F9D RID: 12189 RVA: 0x000A2B16 File Offset: 0x000A0D16
		public void RemoveEssoSecurityPolicy(EssoSecurityPolicy essoSecurityPolicy)
		{
			base.BaseRemove(essoSecurityPolicy.GetElementKey());
		}

		// Token: 0x06002F9E RID: 12190 RVA: 0x000A2B24 File Offset: 0x000A0D24
		public bool Contains(string name)
		{
			object[] array = base.BaseGetAllKeys();
			for (int i = 0; i < array.Length; i++)
			{
				if ((string)array[i] == name)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04001C2F RID: 7215
		private static ConfigurationPropertyCollection s_properties = new ConfigurationPropertyCollection();
	}
}
