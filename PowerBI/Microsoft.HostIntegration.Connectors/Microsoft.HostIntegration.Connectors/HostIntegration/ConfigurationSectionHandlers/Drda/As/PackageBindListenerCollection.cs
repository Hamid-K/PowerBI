using System;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.Drda.As
{
	// Token: 0x0200054C RID: 1356
	[ConfigurationCollection(typeof(PackageBindListener), AddItemName = "packageBindListener", CollectionType = ConfigurationElementCollectionType.BasicMap)]
	public class PackageBindListenerCollection : ConfigurationElementCollection
	{
		// Token: 0x17000971 RID: 2417
		// (get) Token: 0x06002DC3 RID: 11715 RVA: 0x00006F04 File Offset: 0x00005104
		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.BasicMap;
			}
		}

		// Token: 0x17000972 RID: 2418
		// (get) Token: 0x06002DC4 RID: 11716 RVA: 0x0009AC4A File Offset: 0x00098E4A
		protected override string ElementName
		{
			get
			{
				return "packageBindListener";
			}
		}

		// Token: 0x17000973 RID: 2419
		// (get) Token: 0x06002DC5 RID: 11717 RVA: 0x0009AC51 File Offset: 0x00098E51
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return PackageBindListenerCollection.s_properties;
			}
		}

		// Token: 0x17000974 RID: 2420
		public PackageBindListener this[int index]
		{
			get
			{
				return (PackageBindListener)base.BaseGet(index);
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

		// Token: 0x17000975 RID: 2421
		public PackageBindListener this[string name]
		{
			get
			{
				return (PackageBindListener)base.BaseGet(name);
			}
		}

		// Token: 0x06002DC9 RID: 11721 RVA: 0x0009AC74 File Offset: 0x00098E74
		protected override ConfigurationElement CreateNewElement()
		{
			return new PackageBindListener();
		}

		// Token: 0x06002DCA RID: 11722 RVA: 0x0009AC7B File Offset: 0x00098E7B
		protected override object GetElementKey(ConfigurationElement element)
		{
			return (element as PackageBindListener).GetElementKey();
		}

		// Token: 0x06002DCB RID: 11723 RVA: 0x00017EEC File Offset: 0x000160EC
		public void AddPackageBindListener(PackageBindListener packageBindListener)
		{
			base.BaseAdd(packageBindListener, true);
		}

		// Token: 0x06002DCC RID: 11724 RVA: 0x0009AC88 File Offset: 0x00098E88
		public void RemovePackageBindListener(PackageBindListener packageBindListener)
		{
			base.BaseRemove(packageBindListener.GetElementKey());
		}

		// Token: 0x04001BFB RID: 7163
		private static ConfigurationPropertyCollection s_properties = new ConfigurationPropertyCollection();
	}
}
