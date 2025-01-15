using System;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.Ti.Hip
{
	// Token: 0x02000567 RID: 1383
	[ConfigurationCollection(typeof(HttpEndpoint), AddItemName = "endpoint", CollectionType = ConfigurationElementCollectionType.BasicMap)]
	public class HttpEndpointCollection : ConfigurationElementCollection
	{
		// Token: 0x170009C8 RID: 2504
		// (get) Token: 0x06002EE6 RID: 12006 RVA: 0x00006F04 File Offset: 0x00005104
		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.BasicMap;
			}
		}

		// Token: 0x170009C9 RID: 2505
		// (get) Token: 0x06002EE7 RID: 12007 RVA: 0x000A1740 File Offset: 0x0009F940
		protected override string ElementName
		{
			get
			{
				return "endpoint";
			}
		}

		// Token: 0x170009CA RID: 2506
		// (get) Token: 0x06002EE8 RID: 12008 RVA: 0x000A1747 File Offset: 0x0009F947
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return HttpEndpointCollection.s_properties;
			}
		}

		// Token: 0x170009CB RID: 2507
		public HttpEndpoint this[int index]
		{
			get
			{
				return (HttpEndpoint)base.BaseGet(index);
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

		// Token: 0x170009CC RID: 2508
		public HttpEndpoint this[string name]
		{
			get
			{
				return (HttpEndpoint)base.BaseGet(name);
			}
		}

		// Token: 0x06002EEC RID: 12012 RVA: 0x000A176A File Offset: 0x0009F96A
		protected override ConfigurationElement CreateNewElement()
		{
			return new HttpEndpoint();
		}

		// Token: 0x06002EED RID: 12013 RVA: 0x000A1771 File Offset: 0x0009F971
		protected override object GetElementKey(ConfigurationElement element)
		{
			return (element as HttpEndpoint).GetElementKey();
		}

		// Token: 0x06002EEE RID: 12014 RVA: 0x00017EEC File Offset: 0x000160EC
		public void AddEndpoint(HttpEndpoint httpEndpoint)
		{
			base.BaseAdd(httpEndpoint, true);
		}

		// Token: 0x06002EEF RID: 12015 RVA: 0x000A177E File Offset: 0x0009F97E
		public void RemoveEndpoint(HttpEndpoint httpEndpoint)
		{
			base.BaseRemove(httpEndpoint.GetElementKey());
		}

		// Token: 0x04001C1F RID: 7199
		private static ConfigurationPropertyCollection s_properties = new ConfigurationPropertyCollection();
	}
}
