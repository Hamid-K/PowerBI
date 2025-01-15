using System;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.HostIntegration
{
	// Token: 0x020005C8 RID: 1480
	[ConfigurationCollection(typeof(VsVersion), AddItemName = "vsVersion", CollectionType = ConfigurationElementCollectionType.BasicMap)]
	public class VsVersionCollection : ConfigurationElementCollection
	{
		// Token: 0x17000AFF RID: 2815
		// (get) Token: 0x0600335F RID: 13151 RVA: 0x00006F04 File Offset: 0x00005104
		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.BasicMap;
			}
		}

		// Token: 0x17000B00 RID: 2816
		// (get) Token: 0x06003360 RID: 13152 RVA: 0x000AD56B File Offset: 0x000AB76B
		protected override string ElementName
		{
			get
			{
				return "vsVersion";
			}
		}

		// Token: 0x17000B01 RID: 2817
		public VsVersion this[int index]
		{
			get
			{
				return (VsVersion)base.BaseGet(index);
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

		// Token: 0x06003363 RID: 13155 RVA: 0x000AD580 File Offset: 0x000AB780
		protected override ConfigurationElement CreateNewElement()
		{
			return new VsVersion();
		}

		// Token: 0x06003364 RID: 13156 RVA: 0x000AD587 File Offset: 0x000AB787
		protected override object GetElementKey(ConfigurationElement element)
		{
			return (element as VsVersion).GetElementKey();
		}

		// Token: 0x06003365 RID: 13157 RVA: 0x00017EEC File Offset: 0x000160EC
		public void AddVsVersion(VsVersion vsVersion)
		{
			base.BaseAdd(vsVersion, true);
		}

		// Token: 0x06003366 RID: 13158 RVA: 0x000AD594 File Offset: 0x000AB794
		public void RemoveVsVersion(VsVersion vsVersion)
		{
			base.BaseRemove(vsVersion.GetElementKey());
		}
	}
}
