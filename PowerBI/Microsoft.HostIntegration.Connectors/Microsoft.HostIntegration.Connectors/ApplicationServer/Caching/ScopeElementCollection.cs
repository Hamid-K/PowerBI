using System;
using System.Collections.Generic;
using System.Configuration;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000150 RID: 336
	[Serializable]
	internal class ScopeElementCollection : ConfigurationElementCollection, ISerializable
	{
		// Token: 0x06000A67 RID: 2663 RVA: 0x00016D51 File Offset: 0x00014F51
		public ScopeElementCollection()
		{
		}

		// Token: 0x06000A68 RID: 2664 RVA: 0x000230AA File Offset: 0x000212AA
		protected override ConfigurationElement CreateNewElement()
		{
			return new ScopeElement();
		}

		// Token: 0x06000A69 RID: 2665 RVA: 0x000230B4 File Offset: 0x000212B4
		protected sealed override object GetElementKey(ConfigurationElement element)
		{
			ScopeElement scopeElement = (ScopeElement)element;
			return scopeElement.ToString();
		}

		// Token: 0x06000A6A RID: 2666 RVA: 0x000230CE File Offset: 0x000212CE
		internal ScopeElement Get(string key)
		{
			return (ScopeElement)base.BaseGet(key);
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x00016D88 File Offset: 0x00014F88
		internal bool Delete(string name)
		{
			if (base.BaseGet(name) != null)
			{
				base.BaseRemove(name);
				return base.BaseGet(name) == null;
			}
			return false;
		}

		// Token: 0x06000A6C RID: 2668 RVA: 0x00016DA8 File Offset: 0x00014FA8
		internal bool Add(ScopeElement scopeElement)
		{
			if (base.BaseGet(this.GetElementKey(scopeElement)) == null)
			{
				base.BaseAdd(scopeElement, true);
				return true;
			}
			return false;
		}

		// Token: 0x06000A6D RID: 2669 RVA: 0x000230DC File Offset: 0x000212DC
		protected ScopeElementCollection(SerializationInfo info, StreamingContext context)
		{
			List<ScopeElement> list = (List<ScopeElement>)info.GetValue("scopes", typeof(List<ScopeElement>));
			for (int i = 0; i < list.Count; i++)
			{
				this.Add(list[i]);
			}
		}

		// Token: 0x06000A6E RID: 2670 RVA: 0x0002312C File Offset: 0x0002132C
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			List<ScopeElement> list = new List<ScopeElement>();
			for (int i = 0; i < base.Count; i++)
			{
				list.Add((ScopeElement)base.BaseGet(i));
			}
			info.AddValue("scopes", list);
		}
	}
}
