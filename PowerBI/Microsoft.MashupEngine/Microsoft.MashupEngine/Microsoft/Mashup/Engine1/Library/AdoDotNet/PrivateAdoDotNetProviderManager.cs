using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Reflection;
using System.Xml;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Extensibility;

namespace Microsoft.Mashup.Engine1.Library.AdoDotNet
{
	// Token: 0x02000F4F RID: 3919
	internal class PrivateAdoDotNetProviderManager
	{
		// Token: 0x06006786 RID: 26502 RVA: 0x00164790 File Offset: 0x00162990
		public PrivateAdoDotNetProviderManager()
		{
			this.privateProvidersMap = new Dictionary<string, PrivateAdoDotNetProviderManager.DbProviderFactoryLoadInfo>();
			this.foldersMap = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			this.isDotNet35 = FxVersionDetector.InstalledFxVersion == ClrVersion.Net35;
			if (!this.isDotNet35)
			{
				this.requestingAssemblyPropertyInfo = typeof(ResolveEventArgs).GetProperty("RequestingAssembly");
			}
			this.loadedAssemblies = new Dictionary<string, Assembly>(StringComparer.OrdinalIgnoreCase);
			this.Initialize();
		}

		// Token: 0x06006787 RID: 26503 RVA: 0x00164804 File Offset: 0x00162A04
		public bool TryCreateFactory(IEngineHost host, string providerName, out DbProviderFactory factory, bool extensibilityOnly)
		{
			factory = null;
			bool flag = host.QueryService<ExtensionModule>() != null;
			PrivateAdoDotNetProviderManager.DbProviderFactoryLoadInfo dbProviderFactoryLoadInfo;
			if ((!extensibilityOnly || flag) && this.privateProvidersMap.TryGetValue(providerName, out dbProviderFactoryLoadInfo))
			{
				if (this.isDotNet35)
				{
					throw DataSourceException.NewMissingClientLibraryError<Message2>(host, DbEnvironment.GetClientSoftwareNotFoundExceptionMessage("Microsoft .NET Framework 4.5.2", "https://go.microsoft.com/fwlink/?LinkId=328856"), null, "Microsoft .NET Framework 4.5.2", "https://go.microsoft.com/fwlink/?LinkId=328856", null);
				}
				Assembly assembly = this.LoadAssembly(host, dbProviderFactoryLoadInfo.AssemblyLocation);
				factory = assembly.GetType(dbProviderFactoryLoadInfo.FactoryClass).GetField("Instance").GetValue(null) as DbProviderFactory;
				if (factory != null)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06006788 RID: 26504 RVA: 0x0016489C File Offset: 0x00162A9C
		private void Initialize()
		{
			string text = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "ADO.NET Providers");
			DirectoryInfo directoryInfo = new DirectoryInfo(text);
			if (directoryInfo.Exists)
			{
				foreach (DirectoryInfo directoryInfo2 in directoryInfo.GetDirectories())
				{
					string text2 = Path.Combine(text, directoryInfo2.Name + ".config");
					if (File.Exists(text2))
					{
						string text3 = Path.Combine(text, directoryInfo2.Name);
						using (Stream stream = File.OpenRead(text2))
						{
							using (XmlReader xmlReader = XmlHelperUtility.XmlReaderCreate(stream))
							{
								XmlDocument xmlDocument = XmlHelperUtility.CreateXmlDocument();
								xmlDocument.Load(xmlReader);
								foreach (object obj in xmlDocument.SelectNodes("/configuration/system.data/DbProviderFactories/add"))
								{
									XmlNode xmlNode = (XmlNode)obj;
									string innerText = xmlNode.Attributes["invariant"].InnerText;
									string innerText2 = xmlNode.Attributes["type"].InnerText;
									this.privateProvidersMap.Add(innerText, new PrivateAdoDotNetProviderManager.DbProviderFactoryLoadInfo(innerText2, text3));
								}
							}
						}
						if (!this.isDotNet35)
						{
							this.foldersMap.Add(text3);
						}
					}
				}
				if (!this.isDotNet35 && this.foldersMap.Count > 0)
				{
					AppDomain.CurrentDomain.AssemblyResolve += this.PrivateDriverManagerResolveEventHandler;
				}
			}
		}

		// Token: 0x06006789 RID: 26505 RVA: 0x00164A4C File Offset: 0x00162C4C
		private Assembly PrivateDriverManagerResolveEventHandler(object sender, ResolveEventArgs args)
		{
			AssemblyName assemblyName = new AssemblyName(args.Name);
			Assembly assembly;
			if (this.loadedAssemblies.TryGetValue(assemblyName.Name, out assembly))
			{
				return assembly;
			}
			Assembly assembly2 = this.requestingAssemblyPropertyInfo.GetValue(args, null) as Assembly;
			if (assembly2 != null)
			{
				string directoryName = Path.GetDirectoryName(assembly2.Location);
				if (this.foldersMap.Contains(directoryName))
				{
					string text = Path.Combine(directoryName, assemblyName.Name + ".dll");
					if (File.Exists(text))
					{
						return this.LoadAssembly(null, text);
					}
				}
			}
			return null;
		}

		// Token: 0x0600678A RID: 26506 RVA: 0x00164ADE File Offset: 0x00162CDE
		private Exception MissingDependency(IEngineHost engineHost, Exception exception, string assemblyLocation)
		{
			return DataSourceException.NewMissingClientLibraryError<Message1>(engineHost, Strings.PrivateAdoNetDriverMissingAssemblyMessage(assemblyLocation), null, null, null, exception);
		}

		// Token: 0x0600678B RID: 26507 RVA: 0x00164AF0 File Offset: 0x00162CF0
		public Assembly LoadAssembly(IEngineHost engineHost, string assemblyLocation)
		{
			Assembly assembly2;
			try
			{
				Assembly assembly = Assembly.LoadFile(assemblyLocation);
				this.loadedAssemblies[Path.GetFileNameWithoutExtension(assemblyLocation)] = assembly;
				assembly2 = assembly;
			}
			catch (FileLoadException ex)
			{
				throw this.MissingDependency(engineHost, ex, assemblyLocation);
			}
			catch (BadImageFormatException ex2)
			{
				throw this.MissingDependency(engineHost, ex2, assemblyLocation);
			}
			catch (FileNotFoundException ex3)
			{
				throw this.MissingDependency(engineHost, ex3, assemblyLocation);
			}
			return assembly2;
		}

		// Token: 0x040038F9 RID: 14585
		private const string net45DownloadLink = "https://go.microsoft.com/fwlink/?LinkId=328856";

		// Token: 0x040038FA RID: 14586
		private const string net45FriendlyName = "Microsoft .NET Framework 4.5.2";

		// Token: 0x040038FB RID: 14587
		private const string privateDriverFolderName = "ADO.NET Providers";

		// Token: 0x040038FC RID: 14588
		private const string privateDriverNodePath = "/configuration/system.data/DbProviderFactories/add";

		// Token: 0x040038FD RID: 14589
		private readonly Dictionary<string, PrivateAdoDotNetProviderManager.DbProviderFactoryLoadInfo> privateProvidersMap;

		// Token: 0x040038FE RID: 14590
		private readonly HashSet<string> foldersMap;

		// Token: 0x040038FF RID: 14591
		private readonly bool isDotNet35;

		// Token: 0x04003900 RID: 14592
		private readonly PropertyInfo requestingAssemblyPropertyInfo;

		// Token: 0x04003901 RID: 14593
		private readonly Dictionary<string, Assembly> loadedAssemblies;

		// Token: 0x02000F50 RID: 3920
		private class DbProviderFactoryLoadInfo
		{
			// Token: 0x0600678C RID: 26508 RVA: 0x00164B68 File Offset: 0x00162D68
			public DbProviderFactoryLoadInfo(string loadInfo, string assemblyFolderPath)
			{
				this.loadInfo = loadInfo;
				this.assemblyFolderPath = assemblyFolderPath;
				this.AnalyzeLoadInfo();
			}

			// Token: 0x17001DEF RID: 7663
			// (get) Token: 0x0600678D RID: 26509 RVA: 0x00164B84 File Offset: 0x00162D84
			public string FactoryClass
			{
				get
				{
					return this.factoryClass;
				}
			}

			// Token: 0x17001DF0 RID: 7664
			// (get) Token: 0x0600678E RID: 26510 RVA: 0x00164B8C File Offset: 0x00162D8C
			public string AssemblyLocation
			{
				get
				{
					return this.assemblyLocation;
				}
			}

			// Token: 0x0600678F RID: 26511 RVA: 0x00164B94 File Offset: 0x00162D94
			private void AnalyzeLoadInfo()
			{
				int num = this.loadInfo.IndexOf(',');
				this.factoryClass = this.loadInfo.Substring(0, num);
				AssemblyName assemblyName = new AssemblyName(this.loadInfo.Substring(num + 1).Trim());
				this.assemblyLocation = Path.Combine(this.assemblyFolderPath, assemblyName.Name + ".dll");
			}

			// Token: 0x04003902 RID: 14594
			private readonly string loadInfo;

			// Token: 0x04003903 RID: 14595
			private readonly string assemblyFolderPath;

			// Token: 0x04003904 RID: 14596
			private string factoryClass;

			// Token: 0x04003905 RID: 14597
			private string assemblyLocation;
		}
	}
}
