using System;
using System.Configuration;
using System.Data.Entity.Resources;

namespace System.Data.Entity.Internal.ConfigFile
{
	// Token: 0x02000149 RID: 329
	internal class ContextCollection : ConfigurationElementCollection
	{
		// Token: 0x0600157A RID: 5498 RVA: 0x00038293 File Offset: 0x00036493
		protected override ConfigurationElement CreateNewElement()
		{
			return new ContextElement();
		}

		// Token: 0x0600157B RID: 5499 RVA: 0x0003829A File Offset: 0x0003649A
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((ContextElement)element).ContextTypeName;
		}

		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x0600157C RID: 5500 RVA: 0x000382A7 File Offset: 0x000364A7
		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.BasicMap;
			}
		}

		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x0600157D RID: 5501 RVA: 0x000382AA File Offset: 0x000364AA
		protected override string ElementName
		{
			get
			{
				return "context";
			}
		}

		// Token: 0x0600157E RID: 5502 RVA: 0x000382B4 File Offset: 0x000364B4
		protected override void BaseAdd(ConfigurationElement element)
		{
			object elementKey = this.GetElementKey(element);
			if (base.BaseGet(elementKey) != null)
			{
				throw Error.ContextConfiguredMultipleTimes(elementKey);
			}
			base.BaseAdd(element);
		}

		// Token: 0x0600157F RID: 5503 RVA: 0x000382E0 File Offset: 0x000364E0
		protected override void BaseAdd(int index, ConfigurationElement element)
		{
			object elementKey = this.GetElementKey(element);
			if (base.BaseGet(elementKey) != null)
			{
				throw Error.ContextConfiguredMultipleTimes(elementKey);
			}
			base.BaseAdd(index, element);
		}

		// Token: 0x040009D5 RID: 2517
		private const string ContextKey = "context";
	}
}
