using System;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.Ti.Wip
{
	// Token: 0x02000594 RID: 1428
	[ConfigurationCollection(typeof(WipObject), AddItemName = "object", CollectionType = ConfigurationElementCollectionType.BasicMap)]
	public class WipObjectCollection : ConfigurationElementCollection
	{
		// Token: 0x17000A71 RID: 2673
		// (get) Token: 0x06003160 RID: 12640 RVA: 0x00006F04 File Offset: 0x00005104
		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.BasicMap;
			}
		}

		// Token: 0x17000A72 RID: 2674
		// (get) Token: 0x06003161 RID: 12641 RVA: 0x000A1988 File Offset: 0x0009FB88
		protected override string ElementName
		{
			get
			{
				return "object";
			}
		}

		// Token: 0x17000A73 RID: 2675
		// (get) Token: 0x06003162 RID: 12642 RVA: 0x000A5024 File Offset: 0x000A3224
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return WipObjectCollection.s_properties;
			}
		}

		// Token: 0x17000A74 RID: 2676
		public WipObject this[int index]
		{
			get
			{
				return (WipObject)base.BaseGet(index);
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

		// Token: 0x17000A75 RID: 2677
		public WipObject this[string name]
		{
			get
			{
				return (WipObject)base.BaseGet(name);
			}
		}

		// Token: 0x06003166 RID: 12646 RVA: 0x000A5047 File Offset: 0x000A3247
		protected override ConfigurationElement CreateNewElement()
		{
			return new WipObject();
		}

		// Token: 0x06003167 RID: 12647 RVA: 0x000A504E File Offset: 0x000A324E
		protected override object GetElementKey(ConfigurationElement element)
		{
			return (element as WipObject).Name;
		}

		// Token: 0x06003168 RID: 12648 RVA: 0x00017EEC File Offset: 0x000160EC
		public void AddWipObject(WipObject wipObject)
		{
			base.BaseAdd(wipObject, true);
		}

		// Token: 0x06003169 RID: 12649 RVA: 0x000A505B File Offset: 0x000A325B
		public void RemoveWipObject(WipObject wipObject)
		{
			base.BaseRemove(wipObject.GetElementKey());
		}

		// Token: 0x04001C57 RID: 7255
		private static ConfigurationPropertyCollection s_properties = new ConfigurationPropertyCollection();
	}
}
