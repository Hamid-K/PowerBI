using System;
using System.Collections.Generic;
using System.Configuration;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200014E RID: 334
	[Serializable]
	internal class ResourceElementCollection : ConfigurationElementCollection, ISerializable
	{
		// Token: 0x06000A53 RID: 2643 RVA: 0x00016D51 File Offset: 0x00014F51
		public ResourceElementCollection()
		{
		}

		// Token: 0x06000A54 RID: 2644 RVA: 0x00022D81 File Offset: 0x00020F81
		protected override ConfigurationElement CreateNewElement()
		{
			return new ResourceElement();
		}

		// Token: 0x06000A55 RID: 2645 RVA: 0x00022D88 File Offset: 0x00020F88
		protected sealed override object GetElementKey(ConfigurationElement element)
		{
			ResourceElement resourceElement = (ResourceElement)element;
			return resourceElement.Type;
		}

		// Token: 0x06000A56 RID: 2646 RVA: 0x00022DA7 File Offset: 0x00020FA7
		internal ResourceElement Get(string key)
		{
			return (ResourceElement)base.BaseGet(key);
		}

		// Token: 0x06000A57 RID: 2647 RVA: 0x00016D88 File Offset: 0x00014F88
		internal bool Delete(string name)
		{
			if (base.BaseGet(name) != null)
			{
				base.BaseRemove(name);
				return base.BaseGet(name) == null;
			}
			return false;
		}

		// Token: 0x06000A58 RID: 2648 RVA: 0x00016DA8 File Offset: 0x00014FA8
		internal bool Add(ResourceElement resourceElement)
		{
			if (base.BaseGet(this.GetElementKey(resourceElement)) == null)
			{
				base.BaseAdd(resourceElement, true);
				return true;
			}
			return false;
		}

		// Token: 0x06000A59 RID: 2649 RVA: 0x00022DB8 File Offset: 0x00020FB8
		protected ResourceElementCollection(SerializationInfo info, StreamingContext context)
		{
			List<ResourceElement> list = (List<ResourceElement>)info.GetValue("resources", typeof(List<ResourceElement>));
			for (int i = 0; i < list.Count; i++)
			{
				this.Add(list[i]);
			}
		}

		// Token: 0x06000A5A RID: 2650 RVA: 0x00022E08 File Offset: 0x00021008
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			List<ResourceElement> list = new List<ResourceElement>();
			for (int i = 0; i < base.Count; i++)
			{
				list.Add((ResourceElement)base.BaseGet(i));
			}
			info.AddValue("resources", list);
		}
	}
}
