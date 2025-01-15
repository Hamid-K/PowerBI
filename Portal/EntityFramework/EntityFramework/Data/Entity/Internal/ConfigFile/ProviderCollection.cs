using System;
using System.Configuration;
using System.Data.Entity.Resources;

namespace System.Data.Entity.Internal.ConfigFile
{
	// Token: 0x02000152 RID: 338
	internal class ProviderCollection : ConfigurationElementCollection
	{
		// Token: 0x060015BA RID: 5562 RVA: 0x0003872B File Offset: 0x0003692B
		protected override ConfigurationElement CreateNewElement()
		{
			return new ProviderElement();
		}

		// Token: 0x060015BB RID: 5563 RVA: 0x00038732 File Offset: 0x00036932
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((ProviderElement)element).InvariantName;
		}

		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x060015BC RID: 5564 RVA: 0x0003873F File Offset: 0x0003693F
		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.BasicMap;
			}
		}

		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x060015BD RID: 5565 RVA: 0x00038742 File Offset: 0x00036942
		protected override string ElementName
		{
			get
			{
				return "provider";
			}
		}

		// Token: 0x060015BE RID: 5566 RVA: 0x00038749 File Offset: 0x00036949
		protected override void BaseAdd(ConfigurationElement element)
		{
			if (!this.ValidateProviderElement(element))
			{
				base.BaseAdd(element);
			}
		}

		// Token: 0x060015BF RID: 5567 RVA: 0x0003875B File Offset: 0x0003695B
		protected override void BaseAdd(int index, ConfigurationElement element)
		{
			if (!this.ValidateProviderElement(element))
			{
				base.BaseAdd(index, element);
			}
		}

		// Token: 0x060015C0 RID: 5568 RVA: 0x00038770 File Offset: 0x00036970
		private bool ValidateProviderElement(ConfigurationElement element)
		{
			object elementKey = this.GetElementKey(element);
			ProviderElement providerElement = (ProviderElement)base.BaseGet(elementKey);
			if (providerElement != null && providerElement.ProviderTypeName != ((ProviderElement)element).ProviderTypeName)
			{
				throw new InvalidOperationException(Strings.ProviderInvariantRepeatedInConfig(elementKey));
			}
			return providerElement != null;
		}

		// Token: 0x060015C1 RID: 5569 RVA: 0x000387C0 File Offset: 0x000369C0
		public ProviderElement AddProvider(string invariantName, string providerTypeName)
		{
			ProviderElement providerElement = (ProviderElement)this.CreateNewElement();
			base.BaseAdd(providerElement);
			providerElement.InvariantName = invariantName;
			providerElement.ProviderTypeName = providerTypeName;
			return providerElement;
		}

		// Token: 0x040009EE RID: 2542
		private const string ProviderKey = "provider";
	}
}
