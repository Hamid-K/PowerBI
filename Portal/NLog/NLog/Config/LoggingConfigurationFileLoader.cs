using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Security;
using System.Xml;
using NLog.Common;
using NLog.Internal;
using NLog.Internal.Fakeables;
using NLog.Targets;

namespace NLog.Config
{
	// Token: 0x0200018F RID: 399
	internal class LoggingConfigurationFileLoader : ILoggingConfigurationLoader, IDisposable
	{
		// Token: 0x06001224 RID: 4644 RVA: 0x0002F853 File Offset: 0x0002DA53
		public LoggingConfigurationFileLoader()
			: this(LoggingConfigurationFileLoader.DefaultAppEnvironment)
		{
		}

		// Token: 0x06001225 RID: 4645 RVA: 0x0002F860 File Offset: 0x0002DA60
		public LoggingConfigurationFileLoader(IAppEnvironment appEnvironment)
		{
			this._appEnvironment = appEnvironment;
		}

		// Token: 0x06001226 RID: 4646 RVA: 0x0002F870 File Offset: 0x0002DA70
		public LoggingConfiguration Load(LogFactory logFactory, string filename)
		{
			string configFile = this.GetConfigFile(filename);
			return this.LoadXmlLoggingConfigurationFile(logFactory, configFile);
		}

		// Token: 0x06001227 RID: 4647 RVA: 0x0002F88D File Offset: 0x0002DA8D
		public virtual LoggingConfiguration Load(LogFactory logFactory)
		{
			return this.TryLoadFromFilePaths(logFactory);
		}

		// Token: 0x06001228 RID: 4648 RVA: 0x0002F896 File Offset: 0x0002DA96
		public virtual void Activated(LogFactory logFactory, LoggingConfiguration config)
		{
		}

		// Token: 0x06001229 RID: 4649 RVA: 0x0002F898 File Offset: 0x0002DA98
		internal string GetConfigFile(string configFile)
		{
			if (FilePathLayout.DetectFilePathKind(configFile, true) == FilePathKind.Relative)
			{
				foreach (string text in this.GetDefaultCandidateConfigFilePaths(configFile))
				{
					if (this._appEnvironment.FileExists(text))
					{
						configFile = text;
						break;
					}
				}
			}
			return configFile;
		}

		// Token: 0x0600122A RID: 4650 RVA: 0x0002F900 File Offset: 0x0002DB00
		private LoggingConfiguration TryLoadFromFilePaths(LogFactory logFactory)
		{
			foreach (string text in logFactory.GetCandidateConfigFilePaths())
			{
				LoggingConfiguration loggingConfiguration;
				if (this.TryLoadLoggingConfiguration(logFactory, text, out loggingConfiguration))
				{
					return loggingConfiguration;
				}
			}
			return null;
		}

		// Token: 0x0600122B RID: 4651 RVA: 0x0002F95C File Offset: 0x0002DB5C
		private bool TryLoadLoggingConfiguration(LogFactory logFactory, string configFile, out LoggingConfiguration config)
		{
			try
			{
				if (this._appEnvironment.FileExists(configFile))
				{
					config = this.LoadXmlLoggingConfigurationFile(logFactory, configFile);
					return true;
				}
			}
			catch (IOException ex)
			{
				InternalLogger.Warn(ex, "Skipping invalid config file location: {0}", new object[] { configFile });
			}
			catch (UnauthorizedAccessException ex2)
			{
				InternalLogger.Warn(ex2, "Skipping inaccessible config file location: {0}", new object[] { configFile });
			}
			catch (SecurityException ex3)
			{
				InternalLogger.Warn(ex3, "Skipping inaccessible config file location: {0}", new object[] { configFile });
			}
			catch (Exception ex4)
			{
				InternalLogger.Error(ex4, "Failed loading from config file location: {0}", new object[] { configFile });
				if (ex4.MustBeRethrown())
				{
					throw;
				}
			}
			config = null;
			return false;
		}

		// Token: 0x0600122C RID: 4652 RVA: 0x0002FA28 File Offset: 0x0002DC28
		private LoggingConfiguration LoadXmlLoggingConfigurationFile(LogFactory logFactory, string configFile)
		{
			InternalLogger.Debug<string>("Loading config from {0}", configFile);
			LoggingConfiguration loggingConfiguration;
			using (XmlReader xmlReader = this._appEnvironment.LoadXmlFile(configFile))
			{
				loggingConfiguration = this.LoadXmlLoggingConfiguration(xmlReader, configFile, logFactory);
			}
			return loggingConfiguration;
		}

		// Token: 0x0600122D RID: 4653 RVA: 0x0002FA74 File Offset: 0x0002DC74
		private LoggingConfiguration LoadXmlLoggingConfiguration(XmlReader xmlReader, string configFile, LogFactory logFactory)
		{
			XmlLoggingConfiguration xmlLoggingConfiguration = new XmlLoggingConfiguration(xmlReader, configFile, logFactory);
			bool? initializeSucceeded = xmlLoggingConfiguration.InitializeSucceeded;
			bool flag = true;
			if (!((initializeSucceeded.GetValueOrDefault() == flag) & (initializeSucceeded != null)))
			{
				InternalLogger.Warn<string>("Failed loading config from {0}. Invalid XML?", configFile);
			}
			return xmlLoggingConfiguration;
		}

		// Token: 0x0600122E RID: 4654 RVA: 0x0002FAB1 File Offset: 0x0002DCB1
		public IEnumerable<string> GetDefaultCandidateConfigFilePaths()
		{
			return this.GetDefaultCandidateConfigFilePaths(null);
		}

		// Token: 0x0600122F RID: 4655 RVA: 0x0002FABA File Offset: 0x0002DCBA
		public IEnumerable<string> GetDefaultCandidateConfigFilePaths(string fileName)
		{
			string nlogConfigFile = fileName ?? "NLog.config";
			string baseDirectory = PathHelpers.TrimDirectorySeparators(this._appEnvironment.AppDomainBaseDirectory);
			if (!string.IsNullOrEmpty(baseDirectory))
			{
				yield return Path.Combine(baseDirectory, nlogConfigFile);
			}
			string nLogConfigFileLowerCase = nlogConfigFile.ToLower();
			bool platformFileSystemCaseInsensitive = nlogConfigFile == nLogConfigFileLowerCase || PlatformDetector.IsWin32;
			if (!platformFileSystemCaseInsensitive && !string.IsNullOrEmpty(baseDirectory))
			{
				yield return Path.Combine(baseDirectory, nLogConfigFileLowerCase);
			}
			string entryAssemblyLocation = PathHelpers.TrimDirectorySeparators(this._appEnvironment.EntryAssemblyLocation);
			if (!string.IsNullOrEmpty(entryAssemblyLocation) && !string.Equals(entryAssemblyLocation, baseDirectory, StringComparison.OrdinalIgnoreCase))
			{
				yield return Path.Combine(entryAssemblyLocation, nlogConfigFile);
				if (!platformFileSystemCaseInsensitive)
				{
					yield return Path.Combine(entryAssemblyLocation, nLogConfigFileLowerCase);
				}
			}
			if (string.IsNullOrEmpty(baseDirectory))
			{
				yield return nlogConfigFile;
				if (!platformFileSystemCaseInsensitive)
				{
					yield return nLogConfigFileLowerCase;
				}
			}
			IEnumerator<string> enumerator;
			if (fileName == null)
			{
				foreach (string text in this.GetAppSpecificNLogLocations(entryAssemblyLocation))
				{
					yield return text;
				}
				enumerator = null;
			}
			foreach (string text2 in this.GetPrivateBinPathNLogLocations(baseDirectory, nlogConfigFile, platformFileSystemCaseInsensitive ? nLogConfigFileLowerCase : string.Empty))
			{
				yield return text2;
			}
			enumerator = null;
			if (fileName == null)
			{
				Assembly assembly = typeof(LogFactory).GetAssembly();
				if (!string.IsNullOrEmpty((assembly != null) ? assembly.Location : null) && !assembly.GlobalAssemblyCache)
				{
					yield return assembly.Location + ".nlog";
				}
			}
			yield break;
			yield break;
		}

		// Token: 0x06001230 RID: 4656 RVA: 0x0002FAD1 File Offset: 0x0002DCD1
		public IEnumerable<string> GetAppSpecificNLogLocations(string entryAssemblyLocation)
		{
			string configurationFile = this._appEnvironment.AppDomainConfigurationFile;
			if (!StringHelpers.IsNullOrWhiteSpace(configurationFile))
			{
				yield return Path.ChangeExtension(configurationFile, ".nlog");
				if (configurationFile.Contains(".vshost."))
				{
					yield return Path.ChangeExtension(configurationFile.Replace(".vshost.", "."), ".nlog");
				}
			}
			yield break;
		}

		// Token: 0x06001231 RID: 4657 RVA: 0x0002FAE1 File Offset: 0x0002DCE1
		public IEnumerable<string> GetPrivateBinPathNLogLocations(string baseDirectory, string nlogConfigFile, string nLogConfigFileLowerCase)
		{
			IEnumerable<string> privateBinPath = this._appEnvironment.PrivateBinPath;
			if (privateBinPath != null)
			{
				foreach (string text in privateBinPath)
				{
					string path = PathHelpers.TrimDirectorySeparators(text);
					if (!StringHelpers.IsNullOrWhiteSpace(path) && !string.Equals(path, baseDirectory, StringComparison.OrdinalIgnoreCase))
					{
						yield return Path.Combine(path, nlogConfigFile);
						if (!string.IsNullOrEmpty(nLogConfigFileLowerCase))
						{
							yield return Path.Combine(path, nLogConfigFileLowerCase);
						}
					}
					path = null;
				}
				IEnumerator<string> enumerator = null;
			}
			yield break;
			yield break;
		}

		// Token: 0x06001232 RID: 4658 RVA: 0x0002FB06 File Offset: 0x0002DD06
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x06001233 RID: 4659 RVA: 0x0002FB08 File Offset: 0x0002DD08
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x040004EC RID: 1260
		private static readonly AppEnvironmentWrapper DefaultAppEnvironment = new AppEnvironmentWrapper();

		// Token: 0x040004ED RID: 1261
		private readonly IAppEnvironment _appEnvironment;
	}
}
