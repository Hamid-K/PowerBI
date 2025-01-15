using System;
using System.Collections.Generic;
using System.Configuration;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000155 RID: 341
	[Serializable]
	internal class ProviderPropertyElementCollection : ConfigurationElementCollection, ISerializable
	{
		// Token: 0x06000A95 RID: 2709 RVA: 0x00016D51 File Offset: 0x00014F51
		public ProviderPropertyElementCollection()
		{
		}

		// Token: 0x06000A96 RID: 2710 RVA: 0x000235C6 File Offset: 0x000217C6
		protected override ConfigurationElement CreateNewElement()
		{
			return new ProviderPropertyElement();
		}

		// Token: 0x06000A97 RID: 2711 RVA: 0x000235D0 File Offset: 0x000217D0
		protected sealed override object GetElementKey(ConfigurationElement element)
		{
			ProviderPropertyElement providerPropertyElement = (ProviderPropertyElement)element;
			return providerPropertyElement.Name;
		}

		// Token: 0x06000A98 RID: 2712 RVA: 0x000235EA File Offset: 0x000217EA
		internal ProviderPropertyElement Get(string key)
		{
			return (ProviderPropertyElement)base.BaseGet(key);
		}

		// Token: 0x06000A99 RID: 2713 RVA: 0x00016D88 File Offset: 0x00014F88
		internal bool Delete(string name)
		{
			if (base.BaseGet(name) != null)
			{
				base.BaseRemove(name);
				return base.BaseGet(name) == null;
			}
			return false;
		}

		// Token: 0x06000A9A RID: 2714 RVA: 0x00016DA8 File Offset: 0x00014FA8
		internal bool Add(ProviderPropertyElement providerPropertyElement)
		{
			if (base.BaseGet(this.GetElementKey(providerPropertyElement)) == null)
			{
				base.BaseAdd(providerPropertyElement, true);
				return true;
			}
			return false;
		}

		// Token: 0x06000A9B RID: 2715 RVA: 0x000235F8 File Offset: 0x000217F8
		protected ProviderPropertyElementCollection(SerializationInfo info, StreamingContext context)
		{
			List<ProviderPropertyElement> list = (List<ProviderPropertyElement>)info.GetValue("properties", typeof(List<ProviderPropertyElement>));
			for (int i = 0; i < list.Count; i++)
			{
				this.Add(list[i]);
			}
		}

		// Token: 0x06000A9C RID: 2716 RVA: 0x00023648 File Offset: 0x00021848
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			List<ProviderPropertyElement> list = new List<ProviderPropertyElement>();
			for (int i = 0; i < base.Count; i++)
			{
				list.Add((ProviderPropertyElement)base.BaseGet(i));
			}
			info.AddValue("properties", list);
		}
	}
}
