using System;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.MqClient
{
	// Token: 0x0200051B RID: 1307
	[ConfigurationCollection(typeof(QueueManager), AddItemName = "queueManager", CollectionType = ConfigurationElementCollectionType.BasicMap)]
	public class QueueManagerCollection : ConfigurationElementCollection
	{
		// Token: 0x170008C9 RID: 2249
		// (get) Token: 0x06002C36 RID: 11318 RVA: 0x00006F04 File Offset: 0x00005104
		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.BasicMap;
			}
		}

		// Token: 0x170008CA RID: 2250
		// (get) Token: 0x06002C37 RID: 11319 RVA: 0x0009746B File Offset: 0x0009566B
		protected override string ElementName
		{
			get
			{
				return "queueManager";
			}
		}

		// Token: 0x170008CB RID: 2251
		// (get) Token: 0x06002C38 RID: 11320 RVA: 0x00097472 File Offset: 0x00095672
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return QueueManagerCollection.s_properties;
			}
		}

		// Token: 0x170008CC RID: 2252
		public QueueManager this[int index]
		{
			get
			{
				return (QueueManager)base.BaseGet(index);
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

		// Token: 0x170008CD RID: 2253
		public QueueManager this[string name]
		{
			get
			{
				return (QueueManager)base.BaseGet(name);
			}
		}

		// Token: 0x06002C3C RID: 11324 RVA: 0x00097495 File Offset: 0x00095695
		protected override ConfigurationElement CreateNewElement()
		{
			return new QueueManager();
		}

		// Token: 0x06002C3D RID: 11325 RVA: 0x0009749C File Offset: 0x0009569C
		protected override object GetElementKey(ConfigurationElement element)
		{
			return (element as QueueManager).GetElementKey();
		}

		// Token: 0x06002C3E RID: 11326 RVA: 0x00017EEC File Offset: 0x000160EC
		public void AddQueueManager(QueueManager queueManager)
		{
			base.BaseAdd(queueManager, true);
		}

		// Token: 0x06002C3F RID: 11327 RVA: 0x000974A9 File Offset: 0x000956A9
		public void RemoveQueueManager(QueueManager queueManager)
		{
			base.BaseRemove(queueManager.GetElementKey());
		}

		// Token: 0x04001B88 RID: 7048
		private static ConfigurationPropertyCollection s_properties = new ConfigurationPropertyCollection();
	}
}
