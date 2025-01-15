using System;
using System.Collections.Generic;
using System.Configuration;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000152 RID: 338
	[Serializable]
	internal class PersistenceProviderElementCollection : ConfigurationElementCollection, ISerializable
	{
		// Token: 0x06000A7F RID: 2687 RVA: 0x00016D51 File Offset: 0x00014F51
		public PersistenceProviderElementCollection()
		{
		}

		// Token: 0x06000A80 RID: 2688 RVA: 0x0002340B File Offset: 0x0002160B
		protected override ConfigurationElement CreateNewElement()
		{
			return new PersistenceProviderElement();
		}

		// Token: 0x06000A81 RID: 2689 RVA: 0x00023414 File Offset: 0x00021614
		protected sealed override object GetElementKey(ConfigurationElement element)
		{
			PersistenceProviderElement persistenceProviderElement = (PersistenceProviderElement)element;
			return persistenceProviderElement.ToString();
		}

		// Token: 0x06000A82 RID: 2690 RVA: 0x0002342E File Offset: 0x0002162E
		internal PersistenceProviderElement Get(string key)
		{
			return (PersistenceProviderElement)base.BaseGet(key);
		}

		// Token: 0x06000A83 RID: 2691 RVA: 0x00016D88 File Offset: 0x00014F88
		internal bool Delete(string name)
		{
			if (base.BaseGet(name) != null)
			{
				base.BaseRemove(name);
				return base.BaseGet(name) == null;
			}
			return false;
		}

		// Token: 0x06000A84 RID: 2692 RVA: 0x00016DA8 File Offset: 0x00014FA8
		internal bool Add(PersistenceProviderElement providerElement)
		{
			if (base.BaseGet(this.GetElementKey(providerElement)) == null)
			{
				base.BaseAdd(providerElement, true);
				return true;
			}
			return false;
		}

		// Token: 0x06000A85 RID: 2693 RVA: 0x0002343C File Offset: 0x0002163C
		protected PersistenceProviderElementCollection(SerializationInfo info, StreamingContext context)
		{
			List<PersistenceProviderElement> list = (List<PersistenceProviderElement>)info.GetValue("persistenceProviders", typeof(List<PersistenceProviderElement>));
			for (int i = 0; i < list.Count; i++)
			{
				this.Add(list[i]);
			}
		}

		// Token: 0x06000A86 RID: 2694 RVA: 0x0002348C File Offset: 0x0002168C
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			List<PersistenceProviderElement> list = new List<PersistenceProviderElement>();
			for (int i = 0; i < base.Count; i++)
			{
				list.Add((PersistenceProviderElement)base.BaseGet(i));
			}
			info.AddValue("persistenceProviders", list);
		}
	}
}
