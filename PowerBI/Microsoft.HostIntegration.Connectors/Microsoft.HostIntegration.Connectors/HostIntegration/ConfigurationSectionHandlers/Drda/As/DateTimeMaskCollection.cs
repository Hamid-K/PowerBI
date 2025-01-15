using System;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.Drda.As
{
	// Token: 0x02000546 RID: 1350
	[ConfigurationCollection(typeof(DateTimeMask), AddItemName = "dateTimeMask", CollectionType = ConfigurationElementCollectionType.BasicMap)]
	public class DateTimeMaskCollection : ConfigurationElementCollection
	{
		// Token: 0x17000961 RID: 2401
		// (get) Token: 0x06002D9B RID: 11675 RVA: 0x00006F04 File Offset: 0x00005104
		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.BasicMap;
			}
		}

		// Token: 0x17000962 RID: 2402
		// (get) Token: 0x06002D9C RID: 11676 RVA: 0x00098A64 File Offset: 0x00096C64
		protected override string ElementName
		{
			get
			{
				return "dateTimeMask";
			}
		}

		// Token: 0x17000963 RID: 2403
		// (get) Token: 0x06002D9D RID: 11677 RVA: 0x00098A6B File Offset: 0x00096C6B
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return DateTimeMaskCollection.s_properties;
			}
		}

		// Token: 0x17000964 RID: 2404
		public DateTimeMask this[int index]
		{
			get
			{
				return (DateTimeMask)base.BaseGet(index);
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

		// Token: 0x17000965 RID: 2405
		public DateTimeMask this[string name]
		{
			get
			{
				return (DateTimeMask)base.BaseGet(name);
			}
		}

		// Token: 0x06002DA1 RID: 11681 RVA: 0x00098A8E File Offset: 0x00096C8E
		protected override ConfigurationElement CreateNewElement()
		{
			return new DateTimeMask();
		}

		// Token: 0x06002DA2 RID: 11682 RVA: 0x00098A95 File Offset: 0x00096C95
		protected override object GetElementKey(ConfigurationElement element)
		{
			return (element as DateTimeMask).GetElementKey();
		}

		// Token: 0x06002DA3 RID: 11683 RVA: 0x00017EEC File Offset: 0x000160EC
		public void AddDateTimeMask(DateTimeMask dateTimeMask)
		{
			base.BaseAdd(dateTimeMask, true);
		}

		// Token: 0x06002DA4 RID: 11684 RVA: 0x00098AA2 File Offset: 0x00096CA2
		public void RemoveDateTimeMask(DateTimeMask dateTimeMask)
		{
			base.BaseRemove(dateTimeMask.GetElementKey());
		}

		// Token: 0x04001BF7 RID: 7159
		private static ConfigurationPropertyCollection s_properties = new ConfigurationPropertyCollection();
	}
}
