using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Cloud.Platform.Configuration;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.ConfigurationManagement
{
	// Token: 0x02000410 RID: 1040
	public sealed class LocalConfigurationProvider : FileBasedConfigurationProvider
	{
		// Token: 0x06001F93 RID: 8083 RVA: 0x000766C8 File Offset: 0x000748C8
		public LocalConfigurationProvider(string specification)
		{
			string text = (Directory.Exists(specification) ? Path.GetFullPath(specification) : Path.Combine(Path.GetDirectoryName(ExtendedAssembly.GetExecutingAssembly(base.GetType()).Location), specification));
			if (!Directory.Exists(text))
			{
				throw new ConfigurationException("The given configuration files path '{0}' does not exist. Please verify you have the right location".FormatWithInvariantCulture(new object[] { text }));
			}
			this.m_absolutePathToConfigurationFiles = text;
		}

		// Token: 0x06001F94 RID: 8084 RVA: 0x00076734 File Offset: 0x00074934
		public override Dictionary<Type, IConfigurationClass> Start(IConfigurationProviderOwner owner)
		{
			base.Start(owner);
			Dictionary<Type, IConfigurationClass> configuration = base.GetConfiguration(this.m_absolutePathToConfigurationFiles, null);
			this.m_fileSystemWatcher = new FileSystemWatcher(this.m_absolutePathToConfigurationFiles)
			{
				NotifyFilter = NotifyFilters.LastWrite,
				Filter = "*Configuration.xml",
				EnableRaisingEvents = true
			};
			this.m_fileSystemWatcher.Changed += delegate(object s, FileSystemEventArgs e)
			{
				base.Update(this.m_absolutePathToConfigurationFiles, null);
			};
			this.m_fileSystemWatcher.Deleted += delegate(object s, FileSystemEventArgs e)
			{
				base.Update(this.m_absolutePathToConfigurationFiles, null);
			};
			return configuration;
		}

		// Token: 0x06001F95 RID: 8085 RVA: 0x000767AF File Offset: 0x000749AF
		public override void WaitForStopToComplete()
		{
			this.m_fileSystemWatcher.Dispose();
			base.WaitForStopToComplete();
		}

		// Token: 0x06001F96 RID: 8086 RVA: 0x000767C2 File Offset: 0x000749C2
		public override void Shutdown()
		{
			this.m_absolutePathToConfigurationFiles = null;
			this.m_fileSystemWatcher = null;
			base.Shutdown();
		}

		// Token: 0x04000B0F RID: 2831
		private FileSystemWatcher m_fileSystemWatcher;

		// Token: 0x04000B10 RID: 2832
		private string m_absolutePathToConfigurationFiles;
	}
}
