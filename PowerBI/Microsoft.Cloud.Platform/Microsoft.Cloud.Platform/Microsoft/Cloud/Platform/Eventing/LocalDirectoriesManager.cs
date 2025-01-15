using System;
using System.IO;
using Microsoft.Cloud.Platform.ConfigurationClasses.Eventing;
using Microsoft.Cloud.Platform.ConfigurationManagement;
using Microsoft.Cloud.Platform.Eventing.Base;
using Microsoft.Cloud.Platform.Modularization;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Eventing
{
	// Token: 0x02000388 RID: 904
	[BlockServiceProvider(typeof(IEventingDirectoriesManager))]
	public class LocalDirectoriesManager : Block, IEventingDirectoriesManager
	{
		// Token: 0x06001C08 RID: 7176 RVA: 0x00010777 File Offset: 0x0000E977
		public LocalDirectoriesManager(string name)
			: base(name)
		{
		}

		// Token: 0x06001C09 RID: 7177 RVA: 0x0006B10E File Offset: 0x0006930E
		public LocalDirectoriesManager()
			: this(typeof(LocalDirectoriesManager).Name)
		{
		}

		// Token: 0x06001C0A RID: 7178 RVA: 0x0006B128 File Offset: 0x00069328
		protected override BlockInitializationStatus OnInitialize()
		{
			if (base.OnInitialize() == BlockInitializationStatus.PartiallyDone)
			{
				return BlockInitializationStatus.PartiallyDone;
			}
			IConfigurationManager configurationManager = this.m_configurationFactory.GetConfigurationManager();
			configurationManager.Subscribe(new Type[] { typeof(EtwConfiguration) }, new CcsEventHandler(this.ConfigurationHandler));
			configurationManager.Unsubscribe(new CcsEventHandler(this.ConfigurationHandler));
			this.m_directories = this.GetEventingDirectories();
			LocalDirectoriesManager.CreateDirectory(this.m_directories.EventsSourceDirectory);
			LocalDirectoriesManager.CreateDirectory(this.m_directories.EventsTargetDirectory);
			LocalDirectoriesManager.CreateDirectory(this.m_directories.ProvidersManifestDirectory);
			return BlockInitializationStatus.Done;
		}

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x06001C0B RID: 7179 RVA: 0x0006B1C0 File Offset: 0x000693C0
		public string EventingFilesSourceDirectory
		{
			get
			{
				return this.m_directories.EventsSourceDirectory;
			}
		}

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x06001C0C RID: 7180 RVA: 0x0006B1CD File Offset: 0x000693CD
		public string EventingFilesTargetDirectory
		{
			get
			{
				return this.m_directories.EventsTargetDirectory;
			}
		}

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x06001C0D RID: 7181 RVA: 0x0006B1DA File Offset: 0x000693DA
		public string ProvidersManifestDirectory
		{
			get
			{
				return this.m_directories.ProvidersManifestDirectory;
			}
		}

		// Token: 0x06001C0E RID: 7182 RVA: 0x0006B1E7 File Offset: 0x000693E7
		protected virtual LocalDirectoriesManager.EventingDirectories GetEventingDirectories()
		{
			return this.m_directories;
		}

		// Token: 0x06001C0F RID: 7183 RVA: 0x0006B1F0 File Offset: 0x000693F0
		private static void CreateDirectory(string directory)
		{
			if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
			{
				try
				{
					Directory.CreateDirectory(directory);
				}
				catch (IOException ex)
				{
					if (!Directory.Exists(directory))
					{
						TraceSourceBase<EventingTrace>.Tracer.TraceError("Failed creating eventing directory. Exception: {0}", new object[] { ex });
						throw;
					}
				}
			}
		}

		// Token: 0x06001C10 RID: 7184 RVA: 0x0006B24C File Offset: 0x0006944C
		private void ConfigurationHandler(IConfigurationContainer container)
		{
			EtwConfiguration configuration = container.GetConfiguration<EtwConfiguration>();
			this.m_directories = new LocalDirectoriesManager.EventingDirectories(configuration.EventFilesSourceDirectoryPath, configuration.EventFilesTargetDirectoryPath, configuration.ProvidersManifestSessionDirectoryPath);
		}

		// Token: 0x04000978 RID: 2424
		[BlockServiceDependency]
		private IConfigurationManagerFactory m_configurationFactory;

		// Token: 0x04000979 RID: 2425
		private LocalDirectoriesManager.EventingDirectories m_directories;

		// Token: 0x020007BB RID: 1979
		protected class EventingDirectories
		{
			// Token: 0x17000768 RID: 1896
			// (get) Token: 0x06003162 RID: 12642 RVA: 0x000A7D18 File Offset: 0x000A5F18
			// (set) Token: 0x06003163 RID: 12643 RVA: 0x000A7D20 File Offset: 0x000A5F20
			public string EventsSourceDirectory { get; private set; }

			// Token: 0x17000769 RID: 1897
			// (get) Token: 0x06003164 RID: 12644 RVA: 0x000A7D29 File Offset: 0x000A5F29
			// (set) Token: 0x06003165 RID: 12645 RVA: 0x000A7D31 File Offset: 0x000A5F31
			public string EventsTargetDirectory { get; private set; }

			// Token: 0x1700076A RID: 1898
			// (get) Token: 0x06003166 RID: 12646 RVA: 0x000A7D3A File Offset: 0x000A5F3A
			// (set) Token: 0x06003167 RID: 12647 RVA: 0x000A7D42 File Offset: 0x000A5F42
			public string ProvidersManifestDirectory { get; private set; }

			// Token: 0x06003168 RID: 12648 RVA: 0x000A7D4B File Offset: 0x000A5F4B
			public EventingDirectories(string eventsSource, string eventsTarget, string providersManifest)
			{
				this.EventsSourceDirectory = eventsSource;
				this.EventsTargetDirectory = eventsTarget;
				this.ProvidersManifestDirectory = providersManifest;
			}
		}
	}
}
