using System;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.Drda.As
{
	// Token: 0x02000556 RID: 1366
	[ConfigurationCollection(typeof(TimeMask), AddItemName = "timeMask", CollectionType = ConfigurationElementCollectionType.BasicMap)]
	public class TimeMaskCollection : ConfigurationElementCollection
	{
		// Token: 0x1700099C RID: 2460
		// (get) Token: 0x06002E24 RID: 11812 RVA: 0x00006F04 File Offset: 0x00005104
		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.BasicMap;
			}
		}

		// Token: 0x1700099D RID: 2461
		// (get) Token: 0x06002E25 RID: 11813 RVA: 0x0009B14C File Offset: 0x0009934C
		protected override string ElementName
		{
			get
			{
				return "timeMask";
			}
		}

		// Token: 0x1700099E RID: 2462
		// (get) Token: 0x06002E26 RID: 11814 RVA: 0x0009B153 File Offset: 0x00099353
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return TimeMaskCollection.s_properties;
			}
		}

		// Token: 0x1700099F RID: 2463
		public TimeMask this[int index]
		{
			get
			{
				return (TimeMask)base.BaseGet(index);
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

		// Token: 0x170009A0 RID: 2464
		public TimeMask this[string name]
		{
			get
			{
				return (TimeMask)base.BaseGet(name);
			}
		}

		// Token: 0x06002E2A RID: 11818 RVA: 0x0009B176 File Offset: 0x00099376
		protected override ConfigurationElement CreateNewElement()
		{
			return new TimeMask();
		}

		// Token: 0x06002E2B RID: 11819 RVA: 0x0009B17D File Offset: 0x0009937D
		protected override object GetElementKey(ConfigurationElement element)
		{
			return (element as TimeMask).GetElementKey();
		}

		// Token: 0x06002E2C RID: 11820 RVA: 0x00017EEC File Offset: 0x000160EC
		public void AddTimeMask(TimeMask timeMask)
		{
			base.BaseAdd(timeMask, true);
		}

		// Token: 0x06002E2D RID: 11821 RVA: 0x0009B18A File Offset: 0x0009938A
		public void RemoveTimeMask(TimeMask timeMask)
		{
			base.BaseRemove(timeMask.GetElementKey());
		}

		// Token: 0x04001C07 RID: 7175
		private static ConfigurationPropertyCollection s_properties = new ConfigurationPropertyCollection();
	}
}
