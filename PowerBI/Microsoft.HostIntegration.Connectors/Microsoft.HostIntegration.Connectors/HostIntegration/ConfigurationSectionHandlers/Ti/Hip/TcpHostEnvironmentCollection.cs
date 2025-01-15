using System;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.Ti.Hip
{
	// Token: 0x02000585 RID: 1413
	[ConfigurationCollection(typeof(TcpHostEnvironment), AddItemName = "tcpHostEnvironment", CollectionType = ConfigurationElementCollectionType.BasicMap)]
	public class TcpHostEnvironmentCollection : ConfigurationElementCollection
	{
		// Token: 0x17000A1D RID: 2589
		// (get) Token: 0x06003035 RID: 12341 RVA: 0x00006F04 File Offset: 0x00005104
		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.BasicMap;
			}
		}

		// Token: 0x17000A1E RID: 2590
		// (get) Token: 0x06003036 RID: 12342 RVA: 0x000A3554 File Offset: 0x000A1754
		protected override string ElementName
		{
			get
			{
				return "tcpHostEnvironment";
			}
		}

		// Token: 0x17000A1F RID: 2591
		// (get) Token: 0x06003037 RID: 12343 RVA: 0x000A355B File Offset: 0x000A175B
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return TcpHostEnvironmentCollection.s_properties;
			}
		}

		// Token: 0x17000A20 RID: 2592
		public TcpHostEnvironment this[int index]
		{
			get
			{
				return (TcpHostEnvironment)base.BaseGet(index);
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

		// Token: 0x17000A21 RID: 2593
		public TcpHostEnvironment this[string name]
		{
			get
			{
				return (TcpHostEnvironment)base.BaseGet(name);
			}
		}

		// Token: 0x0600303B RID: 12347 RVA: 0x000A357E File Offset: 0x000A177E
		protected override ConfigurationElement CreateNewElement()
		{
			return new TcpHostEnvironment();
		}

		// Token: 0x0600303C RID: 12348 RVA: 0x000A3585 File Offset: 0x000A1785
		protected override object GetElementKey(ConfigurationElement element)
		{
			return (element as TcpHostEnvironment).GetElementKey();
		}

		// Token: 0x0600303D RID: 12349 RVA: 0x00017EEC File Offset: 0x000160EC
		public void AddTcpHostEnvironment(TcpHostEnvironment tcpHostEnvironment)
		{
			base.BaseAdd(tcpHostEnvironment, true);
		}

		// Token: 0x0600303E RID: 12350 RVA: 0x000A3592 File Offset: 0x000A1792
		public void RemoveTcpHostEnvironment(TcpHostEnvironment tcpHostEnvironment)
		{
			base.BaseRemove(tcpHostEnvironment.GetElementKey());
		}

		// Token: 0x0600303F RID: 12351 RVA: 0x000A35A0 File Offset: 0x000A17A0
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

		// Token: 0x04001C40 RID: 7232
		private static ConfigurationPropertyCollection s_properties = new ConfigurationPropertyCollection();
	}
}
