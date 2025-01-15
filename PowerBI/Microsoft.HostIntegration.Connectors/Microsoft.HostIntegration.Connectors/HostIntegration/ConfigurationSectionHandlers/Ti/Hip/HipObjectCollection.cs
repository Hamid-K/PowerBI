using System;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.Ti.Hip
{
	// Token: 0x02000569 RID: 1385
	[ConfigurationCollection(typeof(HipObject), AddItemName = "object", CollectionType = ConfigurationElementCollectionType.BasicMap)]
	public class HipObjectCollection : ConfigurationElementCollection
	{
		// Token: 0x170009D2 RID: 2514
		// (get) Token: 0x06002F0A RID: 12042 RVA: 0x00006F04 File Offset: 0x00005104
		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.BasicMap;
			}
		}

		// Token: 0x170009D3 RID: 2515
		// (get) Token: 0x06002F0B RID: 12043 RVA: 0x000A1988 File Offset: 0x0009FB88
		protected override string ElementName
		{
			get
			{
				return "object";
			}
		}

		// Token: 0x170009D4 RID: 2516
		// (get) Token: 0x06002F0C RID: 12044 RVA: 0x000A198F File Offset: 0x0009FB8F
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return HipObjectCollection.s_properties;
			}
		}

		// Token: 0x170009D5 RID: 2517
		public HipObject this[int index]
		{
			get
			{
				return (HipObject)base.BaseGet(index);
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

		// Token: 0x170009D6 RID: 2518
		public HipObject this[string name]
		{
			get
			{
				return (HipObject)base.BaseGet(name);
			}
		}

		// Token: 0x06002F10 RID: 12048 RVA: 0x000A19B4 File Offset: 0x0009FBB4
		public bool Contains(string name)
		{
			object[] array = base.BaseGetAllKeys();
			for (int i = 0; i < array.Length; i++)
			{
				if ((string)array[i] == name)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002F11 RID: 12049 RVA: 0x000A19E9 File Offset: 0x0009FBE9
		protected override ConfigurationElement CreateNewElement()
		{
			return new HipObject();
		}

		// Token: 0x06002F12 RID: 12050 RVA: 0x000A19F0 File Offset: 0x0009FBF0
		protected override object GetElementKey(ConfigurationElement element)
		{
			return (element as HipObject).GetElementKey();
		}

		// Token: 0x06002F13 RID: 12051 RVA: 0x00017EEC File Offset: 0x000160EC
		public void AddHipObject(HipObject hipObject)
		{
			base.BaseAdd(hipObject, true);
		}

		// Token: 0x06002F14 RID: 12052 RVA: 0x000A19FD File Offset: 0x0009FBFD
		public void RemoveHipObject(HipObject hipObject)
		{
			base.BaseRemove(hipObject.GetElementKey());
		}

		// Token: 0x04001C20 RID: 7200
		private static ConfigurationPropertyCollection s_properties = new ConfigurationPropertyCollection();
	}
}
