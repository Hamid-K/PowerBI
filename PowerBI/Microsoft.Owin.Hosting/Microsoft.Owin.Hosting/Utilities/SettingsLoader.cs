using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Reflection;
using System.Threading;

namespace Microsoft.Owin.Hosting.Utilities
{
	// Token: 0x0200000B RID: 11
	public static class SettingsLoader
	{
		// Token: 0x0600003B RID: 59 RVA: 0x00002F4E File Offset: 0x0000114E
		public static IDictionary<string, string> LoadFromConfig()
		{
			return LazyInitializer.EnsureInitialized<IDictionary<string, string>>(ref SettingsLoader._fromConfigImplementation, () => new SettingsLoader.FromConfigImplementation());
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002F7C File Offset: 0x0000117C
		public static void LoadFromConfig(IDictionary<string, string> settings)
		{
			if (settings == null)
			{
				throw new ArgumentNullException("settings");
			}
			IDictionary<string, string> config = SettingsLoader.LoadFromConfig();
			foreach (KeyValuePair<string, string> pair in config)
			{
				string ignored;
				if (!settings.TryGetValue(pair.Key, out ignored))
				{
					settings.Add(pair);
				}
			}
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002FEC File Offset: 0x000011EC
		public static IDictionary<string, string> LoadFromSettingsFile(string settingsFile)
		{
			Dictionary<string, string> settings = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
			SettingsLoader.LoadFromSettingsFile(settingsFile, settings);
			return settings;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x0000300C File Offset: 0x0000120C
		public static void LoadFromSettingsFile(string settingsFile, IDictionary<string, string> settings)
		{
			if (settingsFile == null)
			{
				throw new ArgumentNullException("settingsFile");
			}
			if (settings == null)
			{
				throw new ArgumentNullException("settings");
			}
			using (StreamReader streamReader = new StreamReader(settingsFile))
			{
				for (;;)
				{
					string line = streamReader.ReadLine();
					if (line == null)
					{
						break;
					}
					if (!line.StartsWith("#", StringComparison.Ordinal) && !string.IsNullOrWhiteSpace(line))
					{
						int delimiterIndex = line.IndexOf('=');
						if (delimiterIndex <= 0)
						{
							goto Block_7;
						}
						string name = line.Substring(0, delimiterIndex).Trim();
						string value = line.Substring(delimiterIndex + 1).Trim();
						if (string.IsNullOrWhiteSpace(name))
						{
							goto Block_8;
						}
						string ignored;
						if (!settings.TryGetValue(name, out ignored))
						{
							settings[name] = value;
						}
					}
				}
				return;
				Block_7:
				throw new ArgumentException(Resources.Exception_ImproperlyFormattedSettingsFile);
				Block_8:
				throw new ArgumentException(Resources.Exception_ImproperlyFormattedSettingsFile);
			}
		}

		// Token: 0x04000029 RID: 41
		private static IDictionary<string, string> _fromConfigImplementation;

		// Token: 0x02000039 RID: 57
		private class FromConfigImplementation : IDictionary<string, string>, ICollection<KeyValuePair<string, string>>, IEnumerable<KeyValuePair<string, string>>, IEnumerable
		{
			// Token: 0x060000FE RID: 254 RVA: 0x00004F98 File Offset: 0x00003198
			public FromConfigImplementation()
			{
				Type configurationManagerType = Type.GetType("System.Configuration.ConfigurationManager, System.Configuration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
				PropertyInfo appSettingsProperty = configurationManagerType.GetProperty("AppSettings");
				this._appSettings = (NameValueCollection)appSettingsProperty.GetValue(null, new object[0]);
			}

			// Token: 0x1700002C RID: 44
			// (get) Token: 0x060000FF RID: 255 RVA: 0x00004FDA File Offset: 0x000031DA
			public int Count
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x1700002D RID: 45
			// (get) Token: 0x06000100 RID: 256 RVA: 0x00004FE1 File Offset: 0x000031E1
			public bool IsReadOnly
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x1700002E RID: 46
			// (get) Token: 0x06000101 RID: 257 RVA: 0x00004FE8 File Offset: 0x000031E8
			public ICollection<string> Keys
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x1700002F RID: 47
			// (get) Token: 0x06000102 RID: 258 RVA: 0x00004FEF File Offset: 0x000031EF
			public ICollection<string> Values
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x17000030 RID: 48
			public string this[string key]
			{
				get
				{
					return this._appSettings[key];
				}
				set
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x06000105 RID: 261 RVA: 0x0000500B File Offset: 0x0000320B
			public bool TryGetValue(string key, out string value)
			{
				value = this._appSettings[key];
				return value != null;
			}

			// Token: 0x06000106 RID: 262 RVA: 0x00005020 File Offset: 0x00003220
			public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
			{
				foreach (object obj in this._appSettings.Keys)
				{
					string key = (string)obj;
					yield return new KeyValuePair<string, string>(key, this._appSettings[key]);
				}
				IEnumerator enumerator = null;
				yield break;
				yield break;
			}

			// Token: 0x06000107 RID: 263 RVA: 0x0000502F File Offset: 0x0000322F
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x06000108 RID: 264 RVA: 0x00005037 File Offset: 0x00003237
			public void Add(KeyValuePair<string, string> item)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000109 RID: 265 RVA: 0x0000503E File Offset: 0x0000323E
			public void Clear()
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600010A RID: 266 RVA: 0x00005045 File Offset: 0x00003245
			public bool Contains(KeyValuePair<string, string> item)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600010B RID: 267 RVA: 0x0000504C File Offset: 0x0000324C
			public void CopyTo(KeyValuePair<string, string>[] array, int arrayIndex)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600010C RID: 268 RVA: 0x00005053 File Offset: 0x00003253
			public bool Remove(KeyValuePair<string, string> item)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600010D RID: 269 RVA: 0x0000505A File Offset: 0x0000325A
			public bool ContainsKey(string key)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600010E RID: 270 RVA: 0x00005061 File Offset: 0x00003261
			public void Add(string key, string value)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600010F RID: 271 RVA: 0x00005068 File Offset: 0x00003268
			public bool Remove(string key)
			{
				throw new NotImplementedException();
			}

			// Token: 0x04000067 RID: 103
			private readonly NameValueCollection _appSettings;
		}
	}
}
