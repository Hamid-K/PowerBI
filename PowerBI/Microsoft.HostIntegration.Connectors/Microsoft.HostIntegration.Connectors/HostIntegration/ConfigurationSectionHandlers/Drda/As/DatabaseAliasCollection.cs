using System;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.Drda.As
{
	// Token: 0x02000540 RID: 1344
	[ConfigurationCollection(typeof(DatabaseAlias), AddItemName = "databaseAlias", CollectionType = ConfigurationElementCollectionType.BasicMap)]
	public class DatabaseAliasCollection : ConfigurationElementCollection
	{
		// Token: 0x1700094D RID: 2381
		// (get) Token: 0x06002D6E RID: 11630 RVA: 0x00006F04 File Offset: 0x00005104
		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.BasicMap;
			}
		}

		// Token: 0x1700094E RID: 2382
		// (get) Token: 0x06002D6F RID: 11631 RVA: 0x0009863D File Offset: 0x0009683D
		protected override string ElementName
		{
			get
			{
				return "databaseAlias";
			}
		}

		// Token: 0x1700094F RID: 2383
		// (get) Token: 0x06002D70 RID: 11632 RVA: 0x00098644 File Offset: 0x00096844
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return DatabaseAliasCollection.s_properties;
			}
		}

		// Token: 0x17000950 RID: 2384
		public DatabaseAlias this[int index]
		{
			get
			{
				return (DatabaseAlias)base.BaseGet(index);
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

		// Token: 0x17000951 RID: 2385
		public DatabaseAlias this[string name]
		{
			get
			{
				return (DatabaseAlias)base.BaseGet(name);
			}
		}

		// Token: 0x06002D74 RID: 11636 RVA: 0x00098667 File Offset: 0x00096867
		protected override ConfigurationElement CreateNewElement()
		{
			return new DatabaseAlias();
		}

		// Token: 0x06002D75 RID: 11637 RVA: 0x0009866E File Offset: 0x0009686E
		protected override object GetElementKey(ConfigurationElement element)
		{
			return (element as DatabaseAlias).GetElementKey();
		}

		// Token: 0x06002D76 RID: 11638 RVA: 0x00017EEC File Offset: 0x000160EC
		public void AddDatabaseAlias(DatabaseAlias databaseAlias)
		{
			base.BaseAdd(databaseAlias, true);
		}

		// Token: 0x06002D77 RID: 11639 RVA: 0x0009867B File Offset: 0x0009687B
		public void RemoveDatabaseAlias(DatabaseAlias databaseAlias)
		{
			base.BaseRemove(databaseAlias.GetElementKey());
		}

		// Token: 0x04001BED RID: 7149
		private static ConfigurationPropertyCollection s_properties = new ConfigurationPropertyCollection();
	}
}
