using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Security;
using Microsoft.Win32;

namespace Microsoft.PowerBI.Telemetry.Utils
{
	// Token: 0x0200003D RID: 61
	public static class FileUtils
	{
		// Token: 0x0600018E RID: 398 RVA: 0x000059E4 File Offset: 0x00003BE4
		public static bool WriteToFile(string content, string filePath, bool create)
		{
			if (string.IsNullOrEmpty(filePath) || content == null)
			{
				return false;
			}
			try
			{
				if (!File.Exists(filePath) && !create)
				{
					return false;
				}
				File.WriteAllText(filePath, content);
			}
			catch (Exception ex)
			{
				if (ex is ArgumentNullException || ex is SecurityException || ex is ArgumentException || ex is UnauthorizedAccessException || ex is PathTooLongException || ex is NotSupportedException || ex is DirectoryNotFoundException || ex is IOException)
				{
					return false;
				}
				throw;
			}
			return true;
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00005A78 File Offset: 0x00003C78
		public static bool ReadFileContent(out string content, string filePath)
		{
			content = null;
			if (string.IsNullOrEmpty(filePath))
			{
				return false;
			}
			try
			{
				if (!File.Exists(filePath))
				{
					return false;
				}
				content = File.ReadAllText(filePath);
			}
			catch (Exception ex)
			{
				if (ex is ArgumentNullException || ex is SecurityException || ex is ArgumentException || ex is UnauthorizedAccessException || ex is PathTooLongException || ex is NotSupportedException || ex is IOException || ex is FileNotFoundException || ex is DirectoryNotFoundException)
				{
					return false;
				}
				throw;
			}
			return true;
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00005B14 File Offset: 0x00003D14
		public static bool TryGetRegistryKeyValue(RegistryHive registryHive, RegistryView registryView, string key, string name, out object value)
		{
			value = null;
			if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(name))
			{
				return false;
			}
			try
			{
				using (RegistryKey registryKey = RegistryKey.OpenBaseKey(registryHive, registryView))
				{
					using (RegistryKey registryKey2 = registryKey.OpenSubKey(key))
					{
						value = registryKey2.GetValue(name, null);
					}
				}
			}
			catch (Exception ex)
			{
				if (ex is ArgumentException || ex is UnauthorizedAccessException || ex is SecurityException || ex is ObjectDisposedException || ex is IOException)
				{
					return false;
				}
				throw;
			}
			return true;
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00005BD0 File Offset: 0x00003DD0
		public static bool TryCheckIfRegistryKeyExists(RegistryHive registryHive, string key, out bool keyExists, out RegistryView registryView)
		{
			keyExists = false;
			registryView = RegistryView.Default;
			if (string.IsNullOrEmpty(key) || !FileUtils.TryCheckIfRegistryKeyExists(registryHive, registryView, key, out keyExists))
			{
				return false;
			}
			if (!keyExists && Environment.Is64BitOperatingSystem)
			{
				registryView = (Environment.Is64BitProcess ? RegistryView.Registry32 : RegistryView.Registry64);
				return FileUtils.TryCheckIfRegistryKeyExists(registryHive, registryView, key, out keyExists);
			}
			return true;
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00005C28 File Offset: 0x00003E28
		public static bool TryCheckIfRegistryKeyExists(RegistryHive registryHive, RegistryView registryView, string key, out bool keyExists)
		{
			keyExists = false;
			if (string.IsNullOrEmpty(key))
			{
				return false;
			}
			try
			{
				using (RegistryKey registryKey = RegistryKey.OpenBaseKey(registryHive, registryView))
				{
					using (RegistryKey registryKey2 = registryKey.OpenSubKey(key))
					{
						keyExists = registryKey2 != null;
					}
				}
			}
			catch (Exception ex)
			{
				if (ex is ArgumentException || ex is UnauthorizedAccessException || ex is SecurityException || ex is ObjectDisposedException || ex is IOException)
				{
					return false;
				}
				throw;
			}
			return true;
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00005CD4 File Offset: 0x00003ED4
		public static string GetAssemblyVersion(Assembly assembly)
		{
			string text = null;
			if (assembly != null)
			{
				try
				{
					text = FileVersionInfo.GetVersionInfo(assembly.Location).ProductVersion;
				}
				catch (Exception ex)
				{
					if (!(ex is FileNotFoundException) && !(ex is NotSupportedException))
					{
						throw;
					}
					text = null;
				}
			}
			if (string.IsNullOrEmpty(text))
			{
				return "Unknown";
			}
			return text;
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00005D38 File Offset: 0x00003F38
		public static bool IsWow64Process()
		{
			return Environment.Is64BitOperatingSystem != Environment.Is64BitProcess;
		}
	}
}
