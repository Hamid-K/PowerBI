using System;
using Microsoft.BIServer.HostingEnvironment;
using Microsoft.Win32;

namespace Microsoft.BIServer.Configuration
{
	// Token: 0x0200001E RID: 30
	public class RegistryStore : IRegistryStore
	{
		// Token: 0x060000ED RID: 237 RVA: 0x000045EE File Offset: 0x000027EE
		public RegistryStore(RegistryKey top, string commonPath)
		{
			this._storeTop = top;
			if (!string.IsNullOrEmpty(commonPath))
			{
				this._storeTop = this.GetOrCreateSubKey(commonPath);
			}
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00004614 File Offset: 0x00002814
		public void DeleteSubKey(string parent, string subKeyName)
		{
			try
			{
				RegistryKey subKey = this.GetSubKey(parent);
				if (subKey != null)
				{
					Logger.Info("REGISTRY: Delete {0} {1}", new object[]
					{
						subKey.ToString(),
						subKeyName
					});
					subKey.DeleteSubKeyTree(subKeyName, false);
				}
			}
			catch (SystemException ex)
			{
				Logger.Warning(ex, "Could not remove registry key {0} from parent {1}", new object[] { subKeyName, parent });
			}
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00004680 File Offset: 0x00002880
		public void DeleteValue(string parent, string keyName)
		{
			try
			{
				RegistryKey subKey = this.GetSubKey(parent);
				if (subKey != null)
				{
					subKey.DeleteValue(keyName, false);
				}
			}
			catch (SystemException ex)
			{
				Logger.Warning(ex, "Could not remove registry key {0} from parent {1}", new object[] { keyName, parent });
			}
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x000046CC File Offset: 0x000028CC
		private RegistryKey GetSubKey(string subKeyName)
		{
			RegistryKey registryKey = this._storeTop;
			if (!string.IsNullOrEmpty(subKeyName))
			{
				foreach (string text in subKeyName.Split(new char[] { '\\' }))
				{
					registryKey = registryKey.OpenSubKey(text, RegistryKeyPermissionCheck.ReadWriteSubTree);
					if (registryKey == null)
					{
						return null;
					}
				}
			}
			return registryKey;
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x0000471C File Offset: 0x0000291C
		private RegistryKey GetOrCreateSubKey(string subKeyName)
		{
			RegistryKey registryKey = this._storeTop;
			if (!string.IsNullOrEmpty(subKeyName))
			{
				foreach (string text in subKeyName.Split(new char[] { '\\' }))
				{
					registryKey = registryKey.CreateSubKey(text);
				}
			}
			return registryKey;
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00004765 File Offset: 0x00002965
		public void SetValue(string key, string name, string value)
		{
			this.GetOrCreateSubKey(key).SetValue(name, value);
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00004775 File Offset: 0x00002975
		public void SetValue(string key, string name, int value)
		{
			this.GetOrCreateSubKey(key).SetValue(name, value, RegistryValueKind.DWord);
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x0000478C File Offset: 0x0000298C
		public string GetValue(string key, string name)
		{
			RegistryKey subKey = this.GetSubKey(key);
			if (subKey != null)
			{
				return (string)subKey.GetValue(name, null);
			}
			return null;
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x000047B4 File Offset: 0x000029B4
		public int GetValueInt(string key, string name)
		{
			RegistryKey subKey = this.GetSubKey(key);
			if (subKey != null)
			{
				return (int)subKey.GetValue(name, null);
			}
			return int.MinValue;
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x000047DF File Offset: 0x000029DF
		public bool KeyExists(string keyName)
		{
			return this.GetSubKey(keyName) != null;
		}

		// Token: 0x040000C9 RID: 201
		private RegistryKey _storeTop;

		// Token: 0x040000CA RID: 202
		private const char RegistryKeyDelimiter = '\\';
	}
}
