using System;

namespace Microsoft.BIServer.Configuration
{
	// Token: 0x0200001C RID: 28
	public interface IRegistryStore
	{
		// Token: 0x060000DB RID: 219
		void DeleteSubKey(string parent, string subKeyName);

		// Token: 0x060000DC RID: 220
		void DeleteValue(string parent, string keyName);

		// Token: 0x060000DD RID: 221
		void SetValue(string key, string name, string value);

		// Token: 0x060000DE RID: 222
		void SetValue(string key, string name, int value);

		// Token: 0x060000DF RID: 223
		string GetValue(string key, string name);

		// Token: 0x060000E0 RID: 224
		int GetValueInt(string key, string name);

		// Token: 0x060000E1 RID: 225
		bool KeyExists(string key);
	}
}
