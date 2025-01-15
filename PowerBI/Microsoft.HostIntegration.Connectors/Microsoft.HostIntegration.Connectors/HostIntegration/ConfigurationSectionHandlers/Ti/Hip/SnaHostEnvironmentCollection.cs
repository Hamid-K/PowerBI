using System;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.Ti.Hip
{
	// Token: 0x02000580 RID: 1408
	[ConfigurationCollection(typeof(SnaHostEnvironment), AddItemName = "snaHostEnvironment", CollectionType = ConfigurationElementCollectionType.BasicMap)]
	public class SnaHostEnvironmentCollection : ConfigurationElementCollection
	{
		// Token: 0x17000A0B RID: 2571
		// (get) Token: 0x06002FFA RID: 12282 RVA: 0x00006F04 File Offset: 0x00005104
		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.BasicMap;
			}
		}

		// Token: 0x17000A0C RID: 2572
		// (get) Token: 0x06002FFB RID: 12283 RVA: 0x000A3308 File Offset: 0x000A1508
		protected override string ElementName
		{
			get
			{
				return "snaHostEnvironment";
			}
		}

		// Token: 0x17000A0D RID: 2573
		// (get) Token: 0x06002FFC RID: 12284 RVA: 0x000A330F File Offset: 0x000A150F
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return SnaHostEnvironmentCollection.s_properties;
			}
		}

		// Token: 0x17000A0E RID: 2574
		public SnaHostEnvironment this[int index]
		{
			get
			{
				return (SnaHostEnvironment)base.BaseGet(index);
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

		// Token: 0x17000A0F RID: 2575
		public SnaHostEnvironment this[string name]
		{
			get
			{
				return (SnaHostEnvironment)base.BaseGet(name);
			}
		}

		// Token: 0x06003000 RID: 12288 RVA: 0x000A3332 File Offset: 0x000A1532
		protected override ConfigurationElement CreateNewElement()
		{
			return new SnaHostEnvironment();
		}

		// Token: 0x06003001 RID: 12289 RVA: 0x000A3339 File Offset: 0x000A1539
		protected override object GetElementKey(ConfigurationElement element)
		{
			return (element as SnaHostEnvironment).GetElementKey();
		}

		// Token: 0x06003002 RID: 12290 RVA: 0x00017EEC File Offset: 0x000160EC
		public void AddSnaHostEnvironment(SnaHostEnvironment snaHostEnvironment)
		{
			base.BaseAdd(snaHostEnvironment, true);
		}

		// Token: 0x06003003 RID: 12291 RVA: 0x000A3346 File Offset: 0x000A1546
		public void RemoveSnaHostEnvironment(SnaHostEnvironment snaHostEnvironment)
		{
			base.BaseRemove(snaHostEnvironment.GetElementKey());
		}

		// Token: 0x06003004 RID: 12292 RVA: 0x000A3354 File Offset: 0x000A1554
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

		// Token: 0x04001C3D RID: 7229
		private static ConfigurationPropertyCollection s_properties = new ConfigurationPropertyCollection();
	}
}
