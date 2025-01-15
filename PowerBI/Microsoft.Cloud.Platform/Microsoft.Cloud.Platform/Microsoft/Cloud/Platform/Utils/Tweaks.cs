using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using JetBrains.Annotations;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020002DC RID: 732
	public class Tweaks
	{
		// Token: 0x0600138B RID: 5003 RVA: 0x00043E30 File Offset: 0x00042030
		public Tweaks()
		{
			this.m_trace = new DefaultTraceListener();
			this.m_locker = new object();
			this.m_namesToTweaks = new Dictionary<string, Tweak>();
			this.m_appSettingsProviders = new Dictionary<string, IAppSettingsProvider>();
			this.m_defaults = new Tweaks.TweaksDefaults();
			this.m_appSettingsProviders.Add(this.m_defaults.Name, this.m_defaults);
			this.RegisterMandatoryTweaksFiles();
			this.RegisterEnvironmentTweaksProvider();
			this.m_programmaticAppSettings = new AppSettingsInMemoryProvider("programmatic", new AppSettingsBaseProvider.OnAppSettingsChanged(this.OnAppSettingsChanged));
			this.m_appSettingsProviders.Add(this.m_programmaticAppSettings.Name, this.m_programmaticAppSettings);
		}

		// Token: 0x0600138C RID: 5004 RVA: 0x00043EDC File Offset: 0x000420DC
		public void RegisterTweaksFile(string path, string file)
		{
			if (string.IsNullOrWhiteSpace(path))
			{
				throw new ArgumentNullException("path");
			}
			if (string.IsNullOrWhiteSpace(file))
			{
				throw new ArgumentNullException("file");
			}
			this.Trace("Registering tweaks file '{0}\\{1}'", new object[] { path, file });
			AppSettingsFileProvider appSettingsFileProvider = new AppSettingsFileProvider(new AppSettingsBaseProvider.OnAppSettingsChanged(this.OnAppSettingsChanged), path, file);
			bool flag = false;
			object locker = this.m_locker;
			lock (locker)
			{
				if (!this.m_appSettingsProviders.ContainsKey(appSettingsFileProvider.Name))
				{
					this.m_appSettingsProviders.Add(appSettingsFileProvider.Name, appSettingsFileProvider);
					flag = true;
				}
				else
				{
					this.Trace("Tweak file '{0}\\{1}' was not registered since it is already listed", new object[] { path, file });
				}
			}
			if (flag)
			{
				appSettingsFileProvider.StartWatching();
			}
		}

		// Token: 0x0600138D RID: 5005 RVA: 0x00043FB8 File Offset: 0x000421B8
		public void RegisterTweaksFile(string file)
		{
			string mainModuleDirectory = CurrentProcess.MainModuleDirectory;
			this.RegisterTweaksFile(mainModuleDirectory, file);
			string text = null;
			Assembly entryAssembly = Assembly.GetEntryAssembly();
			if (entryAssembly != null)
			{
				text = this.GetAssemblyPath(entryAssembly);
				if (text != mainModuleDirectory)
				{
					this.RegisterTweaksFile(text, file);
				}
			}
			Assembly callingAssembly = Assembly.GetCallingAssembly();
			string assemblyPath = this.GetAssemblyPath(callingAssembly);
			if (assemblyPath != mainModuleDirectory && assemblyPath != text)
			{
				this.RegisterTweaksFile(assemblyPath, file);
			}
		}

		// Token: 0x0600138E RID: 5006 RVA: 0x0004402C File Offset: 0x0004222C
		public void RegisterTweaksFile(Assembly assembly)
		{
			string assemblyPath = this.GetAssemblyPath(assembly);
			string fileName = Path.GetFileName(assembly.ManifestModule.FullyQualifiedName);
			this.RegisterTweaksFile(assemblyPath, Tweaks.GetFileNameWithTweakExtension(fileName));
		}

		// Token: 0x0600138F RID: 5007 RVA: 0x0004405F File Offset: 0x0004225F
		public Tweak<T> RegisterTweak<T>(string name, string description, T defaultValue)
		{
			return this.RegisterTweak<T>(name, description, null, defaultValue);
		}

		// Token: 0x06001390 RID: 5008 RVA: 0x0004406C File Offset: 0x0004226C
		public Tweak<T> RegisterTweak<T>(string name, string description, Action<Tweak> notifyChangesTo, T defaultValue)
		{
			Tweak<T> tweak = null;
			object locker = this.m_locker;
			lock (locker)
			{
				if (this.m_namesToTweaks.ContainsKey(name))
				{
					this.Trace("Registering tweak '{0}' (not actually doing anything, as the tweak was already registered)", new object[] { name });
					tweak = (Tweak<T>)this.m_namesToTweaks[name];
					return tweak;
				}
				this.Trace("Registering tweak '{0}' (type='{1}', description='{2}', default='{3}')", new object[]
				{
					name,
					typeof(T),
					description,
					defaultValue
				});
				tweak = new Tweak<T>(name, description, notifyChangesTo);
				this.m_namesToTweaks.Add(name, tweak);
				this.m_defaults.RegisterTweak(name, (defaultValue == null) ? null : defaultValue.ToString());
				string text = this.DetermineTweakValueUnderLock<T>(tweak);
				this.SetTweak(tweak, text);
			}
			return tweak;
		}

		// Token: 0x06001391 RID: 5009 RVA: 0x00044168 File Offset: 0x00042368
		public void SetProgrammaticAppSwitch(string name, string value)
		{
			this.m_programmaticAppSettings.SetNameValue(name, value);
		}

		// Token: 0x06001392 RID: 5010 RVA: 0x00044178 File Offset: 0x00042378
		public string GetProgrammaticAppSwitch(string name)
		{
			string text;
			if (this.m_programmaticAppSettings.GetAppSettings().TryGetValue(name, out text))
			{
				return text;
			}
			return null;
		}

		// Token: 0x06001393 RID: 5011 RVA: 0x0004419D File Offset: 0x0004239D
		public static string GetFileNameWithTweakExtension(string fileName)
		{
			return string.Format(CultureInfo.CurrentCulture, "{0}{1}", new object[] { fileName, ".tweaks" });
		}

		// Token: 0x06001394 RID: 5012 RVA: 0x000441C0 File Offset: 0x000423C0
		[StringFormatMethod("format")]
		private void Trace([NotNull] string format, params object[] args)
		{
			if (Debugger.IsAttached)
			{
				DefaultTraceListener trace = this.m_trace;
				lock (trace)
				{
					this.m_trace.WriteLine(string.Format("Tweaks: " + format, args));
				}
			}
		}

		// Token: 0x06001395 RID: 5013 RVA: 0x00044220 File Offset: 0x00042420
		private void RegisterEnvironmentTweaksProvider()
		{
			string environmentVariable = Environment.GetEnvironmentVariable("TWEAKS");
			if (string.IsNullOrWhiteSpace(environmentVariable))
			{
				this.Trace("Environment variable TWEAKS is empty, so there are no environment tweaks", new object[0]);
				return;
			}
			this.m_environmentAppSettings = new AppSettingsInMemoryProvider("environment", new AppSettingsBaseProvider.OnAppSettingsChanged(this.OnAppSettingsChanged));
			this.m_appSettingsProviders.Add(this.m_environmentAppSettings.Name, this.m_environmentAppSettings);
			char[] array = new char[] { '&' };
			char[] array2 = new char[] { '=' };
			foreach (string text in environmentVariable.Split(array, StringSplitOptions.RemoveEmptyEntries))
			{
				string[] array4 = text.Split(array2, StringSplitOptions.RemoveEmptyEntries);
				if (array4 == null || array4.Length != 2)
				{
					this.Trace("WARNING: Environment variable TWEAKS includes a name/value pair '{0}' which is not NAME=VALUE; ignoring", new object[] { text });
				}
				else
				{
					string text2 = array4[0].Trim();
					string text3 = array4[1].Trim();
					this.Trace("Environment variable TWEAKS: '{0}={1}'", new object[] { text2, text3 });
					this.m_environmentAppSettings.SetNameValue(text2, text3);
				}
			}
		}

		// Token: 0x06001396 RID: 5014 RVA: 0x00044334 File Offset: 0x00042534
		internal void OnAppSettingsChanged()
		{
			List<Tweak> list = null;
			object locker = this.m_locker;
			lock (locker)
			{
				NameValueDictionary nameValueDictionary = this.DetermineAppSettingsUnderLock();
				list = this.SetTweaksUnderLock(nameValueDictionary);
			}
			foreach (Tweak tweak in list)
			{
				Action<Tweak> notifyChangesTo = tweak.NotifyChangesTo;
				if (notifyChangesTo != null)
				{
					notifyChangesTo(tweak);
				}
			}
		}

		// Token: 0x06001397 RID: 5015 RVA: 0x000443D0 File Offset: 0x000425D0
		private void RegisterMandatoryTweaksFiles()
		{
			this.RegisterTweaksFile(Environment.GetFolderPath(Environment.SpecialFolder.Windows), Tweaks.GetFileNameWithTweakExtension("global"));
			string mainModuleDirectory = CurrentProcess.MainModuleDirectory;
			string mainModuleShortFileName = CurrentProcess.MainModuleShortFileName;
			this.RegisterTweaksFile(mainModuleDirectory, Tweaks.GetFileNameWithTweakExtension("dir"));
			this.RegisterTweaksFile(mainModuleDirectory, Tweaks.GetFileNameWithTweakExtension(mainModuleShortFileName));
			Assembly entryAssembly = Assembly.GetEntryAssembly();
			if (entryAssembly != null)
			{
				string assemblyPath = this.GetAssemblyPath(entryAssembly);
				string name = entryAssembly.ManifestModule.Name;
				if (assemblyPath != mainModuleDirectory)
				{
					this.RegisterTweaksFile(assemblyPath, Tweaks.GetFileNameWithTweakExtension("dir"));
				}
				if (assemblyPath != mainModuleDirectory || name != mainModuleShortFileName)
				{
					this.RegisterTweaksFile(assemblyPath, Tweaks.GetFileNameWithTweakExtension(name));
				}
			}
			else
			{
				this.Trace("Entry assembly not found, so will refrain reading data from corresponding .tweaks file", new object[0]);
			}
			Assembly executingAssembly = ExtendedAssembly.GetExecutingAssembly(typeof(Tweaks));
			string assemblyPath2 = this.GetAssemblyPath(executingAssembly);
			string fileName = Path.GetFileName(executingAssembly.ManifestModule.FullyQualifiedName);
			this.RegisterTweaksFile(assemblyPath2, Tweaks.GetFileNameWithTweakExtension(fileName));
		}

		// Token: 0x06001398 RID: 5016 RVA: 0x000444D0 File Offset: 0x000426D0
		private bool SetTweak(Tweak tweak, string value)
		{
			Type type = tweak.GetType();
			if (type == typeof(Tweak<bool>))
			{
				Tweak<bool> tweak2 = (Tweak<bool>)tweak;
				bool flag;
				if (!bool.TryParse(value, out flag))
				{
					this.Trace("Tweak<{0}> named '{1}' could not be parsed as a boolean ({2}) (value remains {3})", new object[] { type, tweak.Name, value, tweak2.Value });
					return false;
				}
				if (tweak2.Value != flag)
				{
					this.Trace("Tweak<bool> named '{0}' has been set to value {1} (was {2})", new object[] { tweak.Name, flag, tweak2.Value });
					tweak2.Value = flag;
					return true;
				}
				return false;
			}
			else if (type == typeof(Tweak<string>))
			{
				Tweak<string> tweak3 = (Tweak<string>)tweak;
				if (tweak3.Value != value)
				{
					this.Trace("Tweak<string> named '{0}' has been set to value {1} (was {2})", new object[] { tweak.Name, value, tweak3.Value });
					tweak3.Value = value;
					return true;
				}
				return false;
			}
			else
			{
				if (!(type == typeof(Tweak<int>)))
				{
					string text = "Unsupported tweak type: " + type.ToString();
					throw new InvalidCastException("Unsupported tweak type: " + text);
				}
				Tweak<int> tweak4 = (Tweak<int>)tweak;
				int num;
				if (!int.TryParse(value, out num))
				{
					this.Trace("Tweak<{0}> named '{1}' could not be parsed as an integer ({2}) (remains {3})", new object[] { type, tweak.Name, value, tweak4.Value });
					return false;
				}
				if (tweak4.Value != num)
				{
					this.Trace("Tweak<{0}> named '{1}' has been set to value {2} (was {3})", new object[] { type, tweak.Name, num, tweak4.Value });
					tweak4.Value = num;
					return true;
				}
				return false;
			}
		}

		// Token: 0x06001399 RID: 5017 RVA: 0x000446A8 File Offset: 0x000428A8
		private NameValueDictionary DetermineAppSettingsUnderLock()
		{
			this.AssertLockHeld();
			NameValueDictionary nameValueDictionary = new NameValueDictionary();
			foreach (IAppSettingsProvider appSettingsProvider in this.m_appSettingsProviders.Values)
			{
				NameValueDictionary appSettings = appSettingsProvider.GetAppSettings();
				nameValueDictionary.CopyFrom(appSettings);
			}
			return nameValueDictionary;
		}

		// Token: 0x0600139A RID: 5018 RVA: 0x00044714 File Offset: 0x00042914
		private List<Tweak> SetTweaksUnderLock(NameValueDictionary appSettings)
		{
			this.AssertLockHeld();
			List<Tweak> list = new List<Tweak>();
			foreach (KeyValuePair<string, Tweak> keyValuePair in this.m_namesToTweaks)
			{
				Tweak value = keyValuePair.Value;
				if (this.SetTweakUnderLock(value, appSettings))
				{
					list.Add(value);
				}
			}
			return list;
		}

		// Token: 0x0600139B RID: 5019 RVA: 0x00044788 File Offset: 0x00042988
		private bool SetTweakUnderLock([NotNull] Tweak tweak, NameValueDictionary appSettings)
		{
			this.AssertLockHeld();
			Ensure.ArgNotNull<Tweak>(tweak, "tweak");
			string name = tweak.Name;
			return appSettings.ContainsKey(name) && this.SetTweak(tweak, appSettings[name]);
		}

		// Token: 0x0600139C RID: 5020 RVA: 0x000447C8 File Offset: 0x000429C8
		private string DetermineTweakValueUnderLock<T>(Tweak<T> tweak)
		{
			this.AssertLockHeld();
			string text = null;
			foreach (IAppSettingsProvider appSettingsProvider in this.m_appSettingsProviders.Values)
			{
				string name = tweak.Name;
				NameValueDictionary appSettings = appSettingsProvider.GetAppSettings();
				if (appSettings.ContainsKey(name))
				{
					text = appSettings[name];
				}
			}
			return text;
		}

		// Token: 0x0600139D RID: 5021 RVA: 0x00044840 File Offset: 0x00042A40
		private string GetAssemblyPath(Assembly assembly)
		{
			return Path.GetDirectoryName(assembly.ManifestModule.FullyQualifiedName);
		}

		// Token: 0x0600139E RID: 5022 RVA: 0x00009B3B File Offset: 0x00007D3B
		private void AssertLockHeld()
		{
		}

		// Token: 0x04000759 RID: 1881
		private DefaultTraceListener m_trace;

		// Token: 0x0400075A RID: 1882
		private object m_locker;

		// Token: 0x0400075B RID: 1883
		private Dictionary<string, Tweak> m_namesToTweaks;

		// Token: 0x0400075C RID: 1884
		private Tweaks.TweaksDefaults m_defaults;

		// Token: 0x0400075D RID: 1885
		private AppSettingsInMemoryProvider m_environmentAppSettings;

		// Token: 0x0400075E RID: 1886
		private AppSettingsInMemoryProvider m_programmaticAppSettings;

		// Token: 0x0400075F RID: 1887
		private Dictionary<string, IAppSettingsProvider> m_appSettingsProviders;

		// Token: 0x04000760 RID: 1888
		private const string c_tweaksFileExtension = ".tweaks";

		// Token: 0x02000786 RID: 1926
		private class TweaksDefaults : IAppSettingsProvider, IIdentifiable
		{
			// Token: 0x060030A4 RID: 12452 RVA: 0x000A6A67 File Offset: 0x000A4C67
			public NameValueDictionary GetAppSettings()
			{
				return this.m_appSettings;
			}

			// Token: 0x060030A5 RID: 12453 RVA: 0x000A6A6F File Offset: 0x000A4C6F
			public void RegisterTweak(string name, string value)
			{
				this.m_appSettings.Add(name, value);
			}

			// Token: 0x1700075E RID: 1886
			// (get) Token: 0x060030A6 RID: 12454 RVA: 0x000A6A7E File Offset: 0x000A4C7E
			public string Name
			{
				get
				{
					return "defaults";
				}
			}

			// Token: 0x0400163E RID: 5694
			private NameValueDictionary m_appSettings = new NameValueDictionary();
		}
	}
}
