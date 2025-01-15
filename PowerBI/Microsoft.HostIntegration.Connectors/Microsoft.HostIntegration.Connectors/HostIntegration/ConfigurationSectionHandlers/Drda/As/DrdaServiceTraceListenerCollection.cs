using System;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.Drda.As
{
	// Token: 0x02000558 RID: 1368
	[ConfigurationCollection(typeof(DrdaServiceTraceListener), AddItemName = "drdaServiceTraceListener", CollectionType = ConfigurationElementCollectionType.BasicMap)]
	public class DrdaServiceTraceListenerCollection : ConfigurationElementCollection
	{
		// Token: 0x170009A4 RID: 2468
		// (get) Token: 0x06002E38 RID: 11832 RVA: 0x00006F04 File Offset: 0x00005104
		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.BasicMap;
			}
		}

		// Token: 0x170009A5 RID: 2469
		// (get) Token: 0x06002E39 RID: 11833 RVA: 0x0009B1E3 File Offset: 0x000993E3
		protected override string ElementName
		{
			get
			{
				return "drdaServiceTraceListener";
			}
		}

		// Token: 0x170009A6 RID: 2470
		// (get) Token: 0x06002E3A RID: 11834 RVA: 0x0009B1EA File Offset: 0x000993EA
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return DrdaServiceTraceListenerCollection.s_properties;
			}
		}

		// Token: 0x170009A7 RID: 2471
		public DrdaServiceTraceListener this[int index]
		{
			get
			{
				return (DrdaServiceTraceListener)base.BaseGet(index);
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

		// Token: 0x170009A8 RID: 2472
		public DrdaServiceTraceListener this[string name]
		{
			get
			{
				return (DrdaServiceTraceListener)base.BaseGet(name);
			}
		}

		// Token: 0x06002E3E RID: 11838 RVA: 0x0009B20D File Offset: 0x0009940D
		protected override ConfigurationElement CreateNewElement()
		{
			return new DrdaServiceTraceListener();
		}

		// Token: 0x06002E3F RID: 11839 RVA: 0x0009B214 File Offset: 0x00099414
		protected override object GetElementKey(ConfigurationElement element)
		{
			return (element as DrdaServiceTraceListener).GetElementKey();
		}

		// Token: 0x06002E40 RID: 11840 RVA: 0x00017EEC File Offset: 0x000160EC
		public void AddDrdaServiceTraceListener(DrdaServiceTraceListener drdaServiceTraceListener)
		{
			base.BaseAdd(drdaServiceTraceListener, true);
		}

		// Token: 0x06002E41 RID: 11841 RVA: 0x0009B221 File Offset: 0x00099421
		public void RemoveDrdaServiceTraceListener(DrdaServiceTraceListener drdaServiceTraceListener)
		{
			base.BaseRemove(drdaServiceTraceListener.GetElementKey());
		}

		// Token: 0x04001C08 RID: 7176
		private static ConfigurationPropertyCollection s_properties = new ConfigurationPropertyCollection();
	}
}
