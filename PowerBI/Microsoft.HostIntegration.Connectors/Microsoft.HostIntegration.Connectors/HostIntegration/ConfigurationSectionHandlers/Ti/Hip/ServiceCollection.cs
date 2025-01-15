using System;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.Ti.Hip
{
	// Token: 0x0200057C RID: 1404
	[ConfigurationCollection(typeof(Service), AddItemName = "hipService", CollectionType = ConfigurationElementCollectionType.BasicMap)]
	public class ServiceCollection : ConfigurationElementCollection
	{
		// Token: 0x170009FD RID: 2557
		// (get) Token: 0x06002FC9 RID: 12233 RVA: 0x00006F04 File Offset: 0x00005104
		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.BasicMap;
			}
		}

		// Token: 0x170009FE RID: 2558
		// (get) Token: 0x06002FCA RID: 12234 RVA: 0x0009AED7 File Offset: 0x000990D7
		protected override string ElementName
		{
			get
			{
				return "service";
			}
		}

		// Token: 0x170009FF RID: 2559
		// (get) Token: 0x06002FCB RID: 12235 RVA: 0x000A307C File Offset: 0x000A127C
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return ServiceCollection.s_properties;
			}
		}

		// Token: 0x17000A00 RID: 2560
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

		// Token: 0x17000A01 RID: 2561
		public Service this[string serviceName]
		{
			get
			{
				return (Service)base.BaseGet(serviceName);
			}
		}

		// Token: 0x06002FCF RID: 12239 RVA: 0x000A309F File Offset: 0x000A129F
		protected override ConfigurationElement CreateNewElement()
		{
			return new Service();
		}

		// Token: 0x06002FD0 RID: 12240 RVA: 0x000A30A6 File Offset: 0x000A12A6
		protected override object GetElementKey(ConfigurationElement element)
		{
			return (element as Service).GetElementKey();
		}

		// Token: 0x06002FD1 RID: 12241 RVA: 0x00017EEC File Offset: 0x000160EC
		public void AddService(Service service)
		{
			base.BaseAdd(service, true);
		}

		// Token: 0x06002FD2 RID: 12242 RVA: 0x000A30B3 File Offset: 0x000A12B3
		public void RemoveService(Service service)
		{
			base.BaseRemove(service.GetElementKey());
		}

		// Token: 0x04001C3B RID: 7227
		private static ConfigurationPropertyCollection s_properties = new ConfigurationPropertyCollection();
	}
}
