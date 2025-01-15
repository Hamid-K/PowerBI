using System;
using System.Collections.Generic;
using System.Configuration;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020000C4 RID: 196
	[Serializable]
	internal class AuthorizationElement : ConfigurationElementCollection, ISerializable
	{
		// Token: 0x0600050B RID: 1291 RVA: 0x00016D51 File Offset: 0x00014F51
		public AuthorizationElement()
		{
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x00016D59 File Offset: 0x00014F59
		protected override ConfigurationElement CreateNewElement()
		{
			return new AllowElement();
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x00016D60 File Offset: 0x00014F60
		protected sealed override object GetElementKey(ConfigurationElement element)
		{
			AllowElement allowElement = (AllowElement)element;
			return allowElement.Users;
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x00016D7A File Offset: 0x00014F7A
		internal AllowElement Get(string key)
		{
			return (AllowElement)base.BaseGet(key);
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x00016D88 File Offset: 0x00014F88
		internal bool Delete(string name)
		{
			if (base.BaseGet(name) != null)
			{
				base.BaseRemove(name);
				return base.BaseGet(name) == null;
			}
			return false;
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x00016DA8 File Offset: 0x00014FA8
		internal bool Add(AllowElement allowElement)
		{
			if (base.BaseGet(this.GetElementKey(allowElement)) == null)
			{
				base.BaseAdd(allowElement, true);
				return true;
			}
			return false;
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x00016DC4 File Offset: 0x00014FC4
		protected AuthorizationElement(SerializationInfo info, StreamingContext context)
		{
			List<AllowElement> list = (List<AllowElement>)info.GetValue("authorization", typeof(List<AllowElement>));
			for (int i = 0; i < list.Count; i++)
			{
				this.Add(list[i]);
			}
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x00016E14 File Offset: 0x00015014
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			List<AllowElement> list = new List<AllowElement>();
			for (int i = 0; i < base.Count; i++)
			{
				list.Add((AllowElement)base.BaseGet(i));
			}
			info.AddValue("authorization", list);
		}
	}
}
