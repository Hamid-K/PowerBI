using System;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.Drda.As
{
	// Token: 0x02000542 RID: 1346
	[ConfigurationCollection(typeof(DateMask), AddItemName = "dateMask", CollectionType = ConfigurationElementCollectionType.BasicMap)]
	public class DateMaskCollection : ConfigurationElementCollection
	{
		// Token: 0x17000956 RID: 2390
		// (get) Token: 0x06002D82 RID: 11650 RVA: 0x00006F04 File Offset: 0x00005104
		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.BasicMap;
			}
		}

		// Token: 0x17000957 RID: 2391
		// (get) Token: 0x06002D83 RID: 11651 RVA: 0x00098898 File Offset: 0x00096A98
		protected override string ElementName
		{
			get
			{
				return "dateMask";
			}
		}

		// Token: 0x17000958 RID: 2392
		// (get) Token: 0x06002D84 RID: 11652 RVA: 0x0009889F File Offset: 0x00096A9F
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return DateMaskCollection.s_properties;
			}
		}

		// Token: 0x17000959 RID: 2393
		public DateMask this[int index]
		{
			get
			{
				return (DateMask)base.BaseGet(index);
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

		// Token: 0x1700095A RID: 2394
		public DateMask this[string name]
		{
			get
			{
				return (DateMask)base.BaseGet(name);
			}
		}

		// Token: 0x06002D88 RID: 11656 RVA: 0x000988C2 File Offset: 0x00096AC2
		protected override ConfigurationElement CreateNewElement()
		{
			return new DateMask();
		}

		// Token: 0x06002D89 RID: 11657 RVA: 0x000988C9 File Offset: 0x00096AC9
		protected override object GetElementKey(ConfigurationElement element)
		{
			return (element as DateMask).GetElementKey();
		}

		// Token: 0x06002D8A RID: 11658 RVA: 0x00017EEC File Offset: 0x000160EC
		public void AddDateMask(DateMask dateMask)
		{
			base.BaseAdd(dateMask, true);
		}

		// Token: 0x06002D8B RID: 11659 RVA: 0x000988D6 File Offset: 0x00096AD6
		public void RemoveDateMask(DateMask dateMask)
		{
			base.BaseRemove(dateMask.GetElementKey());
		}

		// Token: 0x04001BEF RID: 7151
		private static ConfigurationPropertyCollection s_properties = new ConfigurationPropertyCollection();
	}
}
