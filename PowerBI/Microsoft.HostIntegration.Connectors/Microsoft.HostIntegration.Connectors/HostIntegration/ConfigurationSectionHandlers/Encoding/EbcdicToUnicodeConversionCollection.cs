using System;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.Encoding
{
	// Token: 0x02000529 RID: 1321
	[ConfigurationCollection(typeof(EbcdicToUnicodeConversion), AddItemName = "ebcdicToUnicodeConversion", CollectionType = ConfigurationElementCollectionType.BasicMap)]
	public class EbcdicToUnicodeConversionCollection : ConfigurationElementCollection
	{
		// Token: 0x170008FA RID: 2298
		// (get) Token: 0x06002CAE RID: 11438 RVA: 0x00006F04 File Offset: 0x00005104
		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.BasicMap;
			}
		}

		// Token: 0x170008FB RID: 2299
		// (get) Token: 0x06002CAF RID: 11439 RVA: 0x00097A3E File Offset: 0x00095C3E
		protected override string ElementName
		{
			get
			{
				return "ebcdicToUnicodeConversion";
			}
		}

		// Token: 0x170008FC RID: 2300
		// (get) Token: 0x06002CB0 RID: 11440 RVA: 0x00097A45 File Offset: 0x00095C45
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return EbcdicToUnicodeConversionCollection.s_properties;
			}
		}

		// Token: 0x170008FD RID: 2301
		public EbcdicToUnicodeConversion this[int index]
		{
			get
			{
				return (EbcdicToUnicodeConversion)base.BaseGet(index);
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

		// Token: 0x170008FE RID: 2302
		public EbcdicToUnicodeConversion this[string name]
		{
			get
			{
				return (EbcdicToUnicodeConversion)base.BaseGet(name);
			}
		}

		// Token: 0x06002CB4 RID: 11444 RVA: 0x00097A68 File Offset: 0x00095C68
		protected override ConfigurationElement CreateNewElement()
		{
			return new EbcdicToUnicodeConversion();
		}

		// Token: 0x06002CB5 RID: 11445 RVA: 0x00097A6F File Offset: 0x00095C6F
		protected override object GetElementKey(ConfigurationElement element)
		{
			return (element as EbcdicToUnicodeConversion).GetElementKey();
		}

		// Token: 0x06002CB6 RID: 11446 RVA: 0x00017EEC File Offset: 0x000160EC
		public void AddEbcdicToUnicodeConversion(EbcdicToUnicodeConversion ebcdicToUnicodeConversion)
		{
			base.BaseAdd(ebcdicToUnicodeConversion, true);
		}

		// Token: 0x06002CB7 RID: 11447 RVA: 0x00097A7C File Offset: 0x00095C7C
		public void RemoveEbcdicToUnicodeConversion(EbcdicToUnicodeConversion ebcdicToUnicodeConversion)
		{
			base.BaseRemove(ebcdicToUnicodeConversion.GetElementKey());
		}

		// Token: 0x04001B93 RID: 7059
		private static ConfigurationPropertyCollection s_properties = new ConfigurationPropertyCollection();
	}
}
