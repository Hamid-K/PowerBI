using System;
using System.Configuration;
using System.IO;
using System.Threading;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020002DB RID: 731
	internal class AppSettingsFileProvider : AppSettingsBaseProvider
	{
		// Token: 0x06001384 RID: 4996 RVA: 0x00043B1C File Offset: 0x00041D1C
		public AppSettingsFileProvider(AppSettingsBaseProvider.OnAppSettingsChanged onAppSettingsChanged, string path, string file)
			: base(AppSettingsFileProvider.CreateIdFromPathFile(path, file), onAppSettingsChanged)
		{
			if (string.IsNullOrWhiteSpace(path))
			{
				throw new ArgumentNullException("path");
			}
			if (string.IsNullOrWhiteSpace(file))
			{
				throw new ArgumentNullException("file");
			}
			this.m_path = path;
			this.m_file = file;
		}

		// Token: 0x06001385 RID: 4997 RVA: 0x00043B6C File Offset: 0x00041D6C
		public void StartWatching()
		{
			this.m_fileWatcher = new FileSystemWatcher(this.m_path, this.m_file);
			this.m_fileWatcher.NotifyFilter = NotifyFilters.LastWrite;
			this.m_fileWatcher.EnableRaisingEvents = true;
			this.m_fileWatcher.Created += this.OnFileChanged;
			this.m_fileWatcher.Changed += this.OnFileChanged;
			this.m_fileWatcher.Deleted += this.OnFileChanged;
			this.OnFileChanged(null, new FileSystemEventArgs(WatcherChangeTypes.All, this.m_fileWatcher.Path, this.m_fileWatcher.Filter));
		}

		// Token: 0x06001386 RID: 4998 RVA: 0x00043C14 File Offset: 0x00041E14
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"AppSettingsFileProvider(file=",
				this.m_fileWatcher.Filter,
				", path=",
				this.m_fileWatcher.Path,
				")"
			});
		}

		// Token: 0x06001387 RID: 4999 RVA: 0x00043C60 File Offset: 0x00041E60
		private static string CreateIdFromPathFile(string path, string file)
		{
			path = path.ToUpperInvariant();
			file = file.ToUpperInvariant();
			return Path.Combine(path, file);
		}

		// Token: 0x06001388 RID: 5000 RVA: 0x00043C7C File Offset: 0x00041E7C
		private void OnFileChanged(object sender, FileSystemEventArgs e)
		{
			if (sender == null)
			{
				base.Trace("Reading AppSettings file for the first time ({0})", new object[] { e.FullPath });
			}
			else
			{
				base.Trace("Processing AppSettings file change notification ({0})", new object[] { e.FullPath });
			}
			string fullPath = e.FullPath;
			this.ReadAppSettingsFromFile(fullPath);
			base.InvokedOnAppSettingsChanged();
		}

		// Token: 0x06001389 RID: 5001 RVA: 0x00043CD8 File Offset: 0x00041ED8
		private void ReadAppSettingsFromFile(string file)
		{
			ExeConfigurationFileMap exeConfigurationFileMap = new ExeConfigurationFileMap();
			exeConfigurationFileMap.ExeConfigFilename = file;
			int num = 0;
			for (;;)
			{
				num++;
				if (!File.Exists(file))
				{
					break;
				}
				try
				{
					Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(exeConfigurationFileMap, ConfigurationUserLevel.None);
					if (configuration.HasFile)
					{
						NameValueDictionary nameValueDictionary = AppSettingsFileProvider.ExtractAppSettingsFromConfigurationElement(configuration.AppSettings.Settings);
						base.SetAppSettings(nameValueDictionary);
					}
				}
				catch (ConfigurationErrorsException ex)
				{
					Exception innerException = ex.InnerException;
					if (innerException == null || !(innerException is IOException))
					{
						throw;
					}
					if (num <= 5)
					{
						Thread.Sleep(num * 1000);
						continue;
					}
					base.Trace("WARNING: Tweaks file '{0}' could not be read; existing tweaks (if any) won't be modified", new object[] { file });
				}
				return;
			}
			base.SetAppSettings(new NameValueDictionary());
		}

		// Token: 0x0600138A RID: 5002 RVA: 0x00043D8C File Offset: 0x00041F8C
		private static NameValueDictionary ExtractAppSettingsFromConfigurationElement(ConfigurationElementCollection cec)
		{
			NameValueDictionary nameValueDictionary = new NameValueDictionary();
			foreach (object obj in cec)
			{
				KeyValueConfigurationElement keyValueConfigurationElement = (KeyValueConfigurationElement)obj;
				nameValueDictionary.Add(keyValueConfigurationElement.Key, keyValueConfigurationElement.Value);
			}
			foreach (object obj2 in cec)
			{
				KeyValueConfigurationElement keyValueConfigurationElement2 = (KeyValueConfigurationElement)obj2;
			}
			return nameValueDictionary;
		}

		// Token: 0x04000756 RID: 1878
		private string m_path;

		// Token: 0x04000757 RID: 1879
		private string m_file;

		// Token: 0x04000758 RID: 1880
		private FileSystemWatcher m_fileWatcher;
	}
}
