using System;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.Drda.As
{
	// Token: 0x0200054F RID: 1359
	[ConfigurationCollection(typeof(Service), AddItemName = "service", CollectionType = ConfigurationElementCollectionType.BasicMap)]
	public class ServiceCollection : ConfigurationElementCollection
	{
		// Token: 0x1700098B RID: 2443
		// (get) Token: 0x06002DFC RID: 11772 RVA: 0x00006F04 File Offset: 0x00005104
		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.BasicMap;
			}
		}

		// Token: 0x1700098C RID: 2444
		// (get) Token: 0x06002DFD RID: 11773 RVA: 0x0009AED7 File Offset: 0x000990D7
		protected override string ElementName
		{
			get
			{
				return "service";
			}
		}

		// Token: 0x1700098D RID: 2445
		// (get) Token: 0x06002DFE RID: 11774 RVA: 0x0009AEDE File Offset: 0x000990DE
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return ServiceCollection.s_properties;
			}
		}

		// Token: 0x1700098E RID: 2446
		public Service this[int index]
		{
			get
			{
				return (Service)base.BaseGet(index);
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

		// Token: 0x1700098F RID: 2447
		public Service this[string name]
		{
			get
			{
				return (Service)base.BaseGet(name);
			}
		}

		// Token: 0x06002E02 RID: 11778 RVA: 0x0009AF01 File Offset: 0x00099101
		protected override ConfigurationElement CreateNewElement()
		{
			return new Service();
		}

		// Token: 0x06002E03 RID: 11779 RVA: 0x0009AF08 File Offset: 0x00099108
		protected override object GetElementKey(ConfigurationElement element)
		{
			return (element as Service).GetElementKey();
		}

		// Token: 0x06002E04 RID: 11780 RVA: 0x00017EEC File Offset: 0x000160EC
		public void AddService(Service service)
		{
			base.BaseAdd(service, true);
		}

		// Token: 0x06002E05 RID: 11781 RVA: 0x0009AF15 File Offset: 0x00099115
		public void RemoveService(Service service)
		{
			base.BaseRemove(service.GetElementKey());
		}

		// Token: 0x04001BFC RID: 7164
		private static ConfigurationPropertyCollection s_properties = new ConfigurationPropertyCollection();
	}
}
