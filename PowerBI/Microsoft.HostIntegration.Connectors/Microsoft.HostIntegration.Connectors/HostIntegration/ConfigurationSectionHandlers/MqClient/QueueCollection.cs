using System;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.MqClient
{
	// Token: 0x02000517 RID: 1303
	[ConfigurationCollection(typeof(Queue), AddItemName = "queue", CollectionType = ConfigurationElementCollectionType.BasicMap)]
	public class QueueCollection : ConfigurationElementCollection
	{
		// Token: 0x170008B1 RID: 2225
		// (get) Token: 0x06002BFF RID: 11263 RVA: 0x00006F04 File Offset: 0x00005104
		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.BasicMap;
			}
		}

		// Token: 0x170008B2 RID: 2226
		// (get) Token: 0x06002C00 RID: 11264 RVA: 0x000971ED File Offset: 0x000953ED
		protected override string ElementName
		{
			get
			{
				return "queue";
			}
		}

		// Token: 0x170008B3 RID: 2227
		// (get) Token: 0x06002C01 RID: 11265 RVA: 0x000971F4 File Offset: 0x000953F4
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return QueueCollection.s_properties;
			}
		}

		// Token: 0x170008B4 RID: 2228
		public Queue this[int index]
		{
			get
			{
				return (Queue)base.BaseGet(index);
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

		// Token: 0x170008B5 RID: 2229
		public Queue this[string name]
		{
			get
			{
				return (Queue)base.BaseGet(name);
			}
		}

		// Token: 0x06002C05 RID: 11269 RVA: 0x00097231 File Offset: 0x00095431
		protected override ConfigurationElement CreateNewElement()
		{
			return new Queue();
		}

		// Token: 0x06002C06 RID: 11270 RVA: 0x00097238 File Offset: 0x00095438
		protected override object GetElementKey(ConfigurationElement element)
		{
			return (element as Queue).GetElementKey();
		}

		// Token: 0x06002C07 RID: 11271 RVA: 0x00017EEC File Offset: 0x000160EC
		public void AddQueue(Queue queue)
		{
			base.BaseAdd(queue, true);
		}

		// Token: 0x06002C08 RID: 11272 RVA: 0x00097245 File Offset: 0x00095445
		public void RemoveQueue(Queue queue)
		{
			base.BaseRemove(queue.GetElementKey());
		}

		// Token: 0x04001B87 RID: 7047
		private static ConfigurationPropertyCollection s_properties = new ConfigurationPropertyCollection();
	}
}
