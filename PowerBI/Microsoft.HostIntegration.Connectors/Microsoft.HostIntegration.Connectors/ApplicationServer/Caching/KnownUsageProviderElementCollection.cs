using System;
using System.Collections.Generic;
using System.Configuration;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000154 RID: 340
	[Serializable]
	internal class KnownUsageProviderElementCollection : ConfigurationElementCollection, ISerializable
	{
		// Token: 0x06000A8D RID: 2701 RVA: 0x00016D51 File Offset: 0x00014F51
		public KnownUsageProviderElementCollection()
		{
		}

		// Token: 0x06000A8E RID: 2702 RVA: 0x00023502 File Offset: 0x00021702
		protected override ConfigurationElement CreateNewElement()
		{
			return new KnownUsageProviderElement();
		}

		// Token: 0x06000A8F RID: 2703 RVA: 0x0002350C File Offset: 0x0002170C
		protected sealed override object GetElementKey(ConfigurationElement element)
		{
			KnownUsageProviderElement knownUsageProviderElement = (KnownUsageProviderElement)element;
			return knownUsageProviderElement.ToString();
		}

		// Token: 0x06000A90 RID: 2704 RVA: 0x00023526 File Offset: 0x00021726
		internal KnownUsageProviderElement Get(string key)
		{
			return (KnownUsageProviderElement)base.BaseGet(key);
		}

		// Token: 0x06000A91 RID: 2705 RVA: 0x00016D88 File Offset: 0x00014F88
		internal bool Delete(string name)
		{
			if (base.BaseGet(name) != null)
			{
				base.BaseRemove(name);
				return base.BaseGet(name) == null;
			}
			return false;
		}

		// Token: 0x06000A92 RID: 2706 RVA: 0x00016DA8 File Offset: 0x00014FA8
		internal bool Add(KnownUsageProviderElement providerElement)
		{
			if (base.BaseGet(this.GetElementKey(providerElement)) == null)
			{
				base.BaseAdd(providerElement, true);
				return true;
			}
			return false;
		}

		// Token: 0x06000A93 RID: 2707 RVA: 0x00023534 File Offset: 0x00021734
		protected KnownUsageProviderElementCollection(SerializationInfo info, StreamingContext context)
		{
			List<KnownUsageProviderElement> list = (List<KnownUsageProviderElement>)info.GetValue("providers", typeof(List<KnownUsageProviderElement>));
			for (int i = 0; i < list.Count; i++)
			{
				this.Add(list[i]);
			}
		}

		// Token: 0x06000A94 RID: 2708 RVA: 0x00023584 File Offset: 0x00021784
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			List<KnownUsageProviderElement> list = new List<KnownUsageProviderElement>();
			for (int i = 0; i < base.Count; i++)
			{
				list.Add((KnownUsageProviderElement)base.BaseGet(i));
			}
			info.AddValue("providers", list);
		}
	}
}
