using System;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.Encoding
{
	// Token: 0x0200052D RID: 1325
	[ConfigurationCollection(typeof(UnicodeToEbcdicConversion), AddItemName = "unicodeToEbcdicConversion", CollectionType = ConfigurationElementCollectionType.BasicMap)]
	public class UnicodeToEbcdicConversionCollection : ConfigurationElementCollection
	{
		// Token: 0x17000904 RID: 2308
		// (get) Token: 0x06002CC9 RID: 11465 RVA: 0x00006F04 File Offset: 0x00005104
		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.BasicMap;
			}
		}

		// Token: 0x17000905 RID: 2309
		// (get) Token: 0x06002CCA RID: 11466 RVA: 0x00097DFB File Offset: 0x00095FFB
		protected override string ElementName
		{
			get
			{
				return "unicodeToEbcdicConversion";
			}
		}

		// Token: 0x17000906 RID: 2310
		// (get) Token: 0x06002CCB RID: 11467 RVA: 0x00097E02 File Offset: 0x00096002
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return UnicodeToEbcdicConversionCollection.s_properties;
			}
		}

		// Token: 0x17000907 RID: 2311
		public UnicodeToEbcdicConversion this[int index]
		{
			get
			{
				return (UnicodeToEbcdicConversion)base.BaseGet(index);
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

		// Token: 0x17000908 RID: 2312
		public UnicodeToEbcdicConversion this[string name]
		{
			get
			{
				return (UnicodeToEbcdicConversion)base.BaseGet(name);
			}
		}

		// Token: 0x06002CCF RID: 11471 RVA: 0x00097E25 File Offset: 0x00096025
		protected override ConfigurationElement CreateNewElement()
		{
			return new UnicodeToEbcdicConversion();
		}

		// Token: 0x06002CD0 RID: 11472 RVA: 0x00097E2C File Offset: 0x0009602C
		protected override object GetElementKey(ConfigurationElement element)
		{
			return (element as UnicodeToEbcdicConversion).GetElementKey();
		}

		// Token: 0x06002CD1 RID: 11473 RVA: 0x00017EEC File Offset: 0x000160EC
		public void AddUnicodeToEbcdicConversion(UnicodeToEbcdicConversion unicodeToEbcdicConversion)
		{
			base.BaseAdd(unicodeToEbcdicConversion, true);
		}

		// Token: 0x06002CD2 RID: 11474 RVA: 0x00097E39 File Offset: 0x00096039
		public void RemoveUnicodeToEbcdicConversion(UnicodeToEbcdicConversion unicodeToEbcdicConversion)
		{
			base.BaseRemove(unicodeToEbcdicConversion.GetElementKey());
		}

		// Token: 0x04001B94 RID: 7060
		private static ConfigurationPropertyCollection s_properties = new ConfigurationPropertyCollection();
	}
}
