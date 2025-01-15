using System;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.Drda.As
{
	// Token: 0x02000533 RID: 1331
	[ConfigurationCollection(typeof(CollationName), AddItemName = "collationName", CollectionType = ConfigurationElementCollectionType.BasicMap)]
	public class CollationNameCollection : ConfigurationElementCollection
	{
		// Token: 0x17000915 RID: 2325
		// (get) Token: 0x06002CF3 RID: 11507 RVA: 0x00006F04 File Offset: 0x00005104
		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.BasicMap;
			}
		}

		// Token: 0x17000916 RID: 2326
		// (get) Token: 0x06002CF4 RID: 11508 RVA: 0x00097F9F File Offset: 0x0009619F
		protected override string ElementName
		{
			get
			{
				return "collationName";
			}
		}

		// Token: 0x17000917 RID: 2327
		// (get) Token: 0x06002CF5 RID: 11509 RVA: 0x00097FA6 File Offset: 0x000961A6
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return CollationNameCollection.s_properties;
			}
		}

		// Token: 0x17000918 RID: 2328
		public CollationName this[int index]
		{
			get
			{
				return (CollationName)base.BaseGet(index);
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

		// Token: 0x17000919 RID: 2329
		public CollationName this[string name]
		{
			get
			{
				return (CollationName)base.BaseGet(name);
			}
		}

		// Token: 0x06002CF9 RID: 11513 RVA: 0x00097FC9 File Offset: 0x000961C9
		protected override ConfigurationElement CreateNewElement()
		{
			return new CollationName();
		}

		// Token: 0x06002CFA RID: 11514 RVA: 0x00097FD0 File Offset: 0x000961D0
		protected override object GetElementKey(ConfigurationElement element)
		{
			return (element as CollationName).GetElementKey();
		}

		// Token: 0x06002CFB RID: 11515 RVA: 0x00017EEC File Offset: 0x000160EC
		public void AddCollationName(CollationName collationName)
		{
			base.BaseAdd(collationName, true);
		}

		// Token: 0x06002CFC RID: 11516 RVA: 0x00097FDD File Offset: 0x000961DD
		public void RemoveCollationName(CollationName collationName)
		{
			base.BaseRemove(collationName.GetElementKey());
		}

		// Token: 0x04001BC1 RID: 7105
		private static ConfigurationPropertyCollection s_properties = new ConfigurationPropertyCollection();
	}
}
