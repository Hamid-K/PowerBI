using System;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.Drda.As
{
	// Token: 0x02000531 RID: 1329
	[ConfigurationCollection(typeof(ApplicationEncoding), AddItemName = "applicationEncoding", CollectionType = ConfigurationElementCollectionType.BasicMap)]
	public class ApplicationEncodingCollection : ConfigurationElementCollection
	{
		// Token: 0x1700090E RID: 2318
		// (get) Token: 0x06002CE1 RID: 11489 RVA: 0x00006F04 File Offset: 0x00005104
		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.BasicMap;
			}
		}

		// Token: 0x1700090F RID: 2319
		// (get) Token: 0x06002CE2 RID: 11490 RVA: 0x00097F34 File Offset: 0x00096134
		protected override string ElementName
		{
			get
			{
				return "applicationEncoding";
			}
		}

		// Token: 0x17000910 RID: 2320
		// (get) Token: 0x06002CE3 RID: 11491 RVA: 0x00097F3B File Offset: 0x0009613B
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return ApplicationEncodingCollection.s_properties;
			}
		}

		// Token: 0x17000911 RID: 2321
		public ApplicationEncoding this[int index]
		{
			get
			{
				return (ApplicationEncoding)base.BaseGet(index);
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

		// Token: 0x17000912 RID: 2322
		public ApplicationEncoding this[string name]
		{
			get
			{
				return (ApplicationEncoding)base.BaseGet(name);
			}
		}

		// Token: 0x06002CE7 RID: 11495 RVA: 0x00097F5E File Offset: 0x0009615E
		protected override ConfigurationElement CreateNewElement()
		{
			return new ApplicationEncoding();
		}

		// Token: 0x06002CE8 RID: 11496 RVA: 0x00097F65 File Offset: 0x00096165
		protected override object GetElementKey(ConfigurationElement element)
		{
			return (element as ApplicationEncoding).GetElementKey();
		}

		// Token: 0x06002CE9 RID: 11497 RVA: 0x00017EEC File Offset: 0x000160EC
		public void AddApplicationEncoding(ApplicationEncoding applicationEncoding)
		{
			base.BaseAdd(applicationEncoding, true);
		}

		// Token: 0x06002CEA RID: 11498 RVA: 0x00097F72 File Offset: 0x00096172
		public void RemoveApplicationEncoding(ApplicationEncoding applicationEncoding)
		{
			base.BaseRemove(applicationEncoding.GetElementKey());
		}

		// Token: 0x04001BC0 RID: 7104
		private static ConfigurationPropertyCollection s_properties = new ConfigurationPropertyCollection();
	}
}
