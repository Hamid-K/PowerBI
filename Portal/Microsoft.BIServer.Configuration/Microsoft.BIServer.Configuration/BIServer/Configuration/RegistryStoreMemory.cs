using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.BIServer.Configuration
{
	// Token: 0x0200001D RID: 29
	public class RegistryStoreMemory : IRegistryStore
	{
		// Token: 0x060000E2 RID: 226 RVA: 0x0000443B File Offset: 0x0000263B
		private string GetKey(string parent, string keyName)
		{
			return parent + "$" + keyName;
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x0000444C File Offset: 0x0000264C
		public void DeleteSubKey(string parent, string subKeyName)
		{
			string path = ((parent != null) ? (parent + "\\" + subKeyName) : subKeyName);
			foreach (KeyValuePair<string, object> keyValuePair in this._items.Where((KeyValuePair<string, object> kvp) => kvp.Key.StartsWith(path)).ToArray<KeyValuePair<string, object>>())
			{
				this._items.Remove(keyValuePair.Key);
			}
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x000044C0 File Offset: 0x000026C0
		public void DeleteValue(string parent, string keyName)
		{
			string key = this.GetKey(parent, keyName);
			if (this._items.ContainsKey(key))
			{
				this._items.Remove(key);
			}
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x000044F1 File Offset: 0x000026F1
		public void SetValue(string key, string name, string value)
		{
			this.SetTypeHelper(key, name, value);
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x000044FC File Offset: 0x000026FC
		private void SetTypeHelper(string key, string name, object value)
		{
			string key2 = this.GetKey(key, name);
			if (!this._items.ContainsKey(key2))
			{
				this._items.Add(key2, value);
				return;
			}
			this._items[key2] = value;
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x0000453B File Offset: 0x0000273B
		public void SetValue(string key, string name, int value)
		{
			this.SetTypeHelper(key, name, value);
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x0000454B File Offset: 0x0000274B
		public string GetValue(string key, string name)
		{
			return (string)this.GetValueHelper(key, name);
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x0000455C File Offset: 0x0000275C
		private object GetValueHelper(string key, string name)
		{
			string key2 = this.GetKey(key, name);
			if (this._items.ContainsKey(key2))
			{
				return this._items[key2];
			}
			return null;
		}

		// Token: 0x060000EA RID: 234 RVA: 0x0000458E File Offset: 0x0000278E
		public int GetValueInt(string key, string name)
		{
			return (int)this.GetValueHelper(key, name);
		}

		// Token: 0x060000EB RID: 235 RVA: 0x000045A0 File Offset: 0x000027A0
		public bool KeyExists(string keyName)
		{
			return this._items.Where((KeyValuePair<string, object> kvp) => kvp.Key.StartsWith(keyName)).ToArray<KeyValuePair<string, object>>().Any<KeyValuePair<string, object>>();
		}

		// Token: 0x040000C8 RID: 200
		private readonly Dictionary<string, object> _items = new Dictionary<string, object>();
	}
}
