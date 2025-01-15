using System;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.Ti.Hip
{
	// Token: 0x02000573 RID: 1395
	[ConfigurationCollection(typeof(ResolutionEntry), AddItemName = "resolutionEntry", CollectionType = ConfigurationElementCollectionType.BasicMap)]
	public class ResolutionEntryCollection : ConfigurationElementCollection
	{
		// Token: 0x170009E6 RID: 2534
		// (get) Token: 0x06002F6E RID: 12142 RVA: 0x00006F04 File Offset: 0x00005104
		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.BasicMap;
			}
		}

		// Token: 0x170009E7 RID: 2535
		// (get) Token: 0x06002F6F RID: 12143 RVA: 0x000A27E8 File Offset: 0x000A09E8
		protected override string ElementName
		{
			get
			{
				return "resolutionEntry";
			}
		}

		// Token: 0x170009E8 RID: 2536
		// (get) Token: 0x06002F70 RID: 12144 RVA: 0x000A27EF File Offset: 0x000A09EF
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return ResolutionEntryCollection.s_properties;
			}
		}

		// Token: 0x170009E9 RID: 2537
		public ResolutionEntry this[int index]
		{
			get
			{
				return (ResolutionEntry)base.BaseGet(index);
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

		// Token: 0x170009EA RID: 2538
		public ResolutionEntry this[string interfaceMethod]
		{
			get
			{
				char[] array = new char[] { '.' };
				string[] array2 = interfaceMethod.Split(array);
				if (array2.Length == 3)
				{
					int num = 0;
					foreach (object obj in this)
					{
						ResolutionEntry resolutionEntry = (ResolutionEntry)obj;
						if (interfaceMethod.Contains(resolutionEntry.InterfaceName) && array2[2] == resolutionEntry.Method)
						{
							return (ResolutionEntry)base.BaseGet(num);
						}
						num++;
					}
				}
				return (ResolutionEntry)base.BaseGet(interfaceMethod);
			}
		}

		// Token: 0x06002F74 RID: 12148 RVA: 0x000A28B4 File Offset: 0x000A0AB4
		protected override ConfigurationElement CreateNewElement()
		{
			return new ResolutionEntry();
		}

		// Token: 0x06002F75 RID: 12149 RVA: 0x000A28BB File Offset: 0x000A0ABB
		protected override object GetElementKey(ConfigurationElement element)
		{
			return (element as ResolutionEntry).GetElementKey();
		}

		// Token: 0x06002F76 RID: 12150 RVA: 0x00017EEC File Offset: 0x000160EC
		public void AddResolutionEntry(ResolutionEntry resolutionEntry)
		{
			base.BaseAdd(resolutionEntry, true);
		}

		// Token: 0x06002F77 RID: 12151 RVA: 0x000A28C8 File Offset: 0x000A0AC8
		public void RemoveResolutionEntry(ResolutionEntry resolutionEntry)
		{
			base.BaseRemove(resolutionEntry.GetElementKey());
		}

		// Token: 0x04001C2D RID: 7213
		private static ConfigurationPropertyCollection s_properties = new ConfigurationPropertyCollection();
	}
}
