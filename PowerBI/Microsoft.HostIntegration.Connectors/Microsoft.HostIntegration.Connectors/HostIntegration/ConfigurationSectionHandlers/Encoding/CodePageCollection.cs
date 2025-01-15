using System;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.Encoding
{
	// Token: 0x02000526 RID: 1318
	[ConfigurationCollection(typeof(CodePage), AddItemName = "codePage", CollectionType = ConfigurationElementCollectionType.BasicMap)]
	public class CodePageCollection : ConfigurationElementCollection
	{
		// Token: 0x170008F2 RID: 2290
		// (get) Token: 0x06002C99 RID: 11417 RVA: 0x00006F04 File Offset: 0x00005104
		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.BasicMap;
			}
		}

		// Token: 0x170008F3 RID: 2291
		// (get) Token: 0x06002C9A RID: 11418 RVA: 0x0009796E File Offset: 0x00095B6E
		protected override string ElementName
		{
			get
			{
				return "codePage";
			}
		}

		// Token: 0x170008F4 RID: 2292
		// (get) Token: 0x06002C9B RID: 11419 RVA: 0x00097975 File Offset: 0x00095B75
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return CodePageCollection.s_properties;
			}
		}

		// Token: 0x170008F5 RID: 2293
		public CodePage this[int index]
		{
			get
			{
				return (CodePage)base.BaseGet(index);
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

		// Token: 0x170008F6 RID: 2294
		public CodePage this[string name]
		{
			get
			{
				return (CodePage)base.BaseGet(name);
			}
		}

		// Token: 0x06002C9F RID: 11423 RVA: 0x00097998 File Offset: 0x00095B98
		protected override ConfigurationElement CreateNewElement()
		{
			return new CodePage();
		}

		// Token: 0x06002CA0 RID: 11424 RVA: 0x0009799F File Offset: 0x00095B9F
		protected override object GetElementKey(ConfigurationElement element)
		{
			return (element as CodePage).GetElementKey();
		}

		// Token: 0x06002CA1 RID: 11425 RVA: 0x00017EEC File Offset: 0x000160EC
		public void AddCodePage(CodePage codePage)
		{
			base.BaseAdd(codePage, true);
		}

		// Token: 0x06002CA2 RID: 11426 RVA: 0x000979AC File Offset: 0x00095BAC
		public void RemoveCodePage(CodePage codePage)
		{
			base.BaseRemove(codePage.GetElementKey());
		}

		// Token: 0x04001B8F RID: 7055
		private static ConfigurationPropertyCollection s_properties = new ConfigurationPropertyCollection();
	}
}
