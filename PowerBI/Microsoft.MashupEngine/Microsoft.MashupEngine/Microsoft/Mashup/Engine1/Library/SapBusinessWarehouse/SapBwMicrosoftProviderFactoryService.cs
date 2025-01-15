using System;
using System.Data.Common;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.Internal;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.SapBusinessWarehouse
{
	// Token: 0x020004C2 RID: 1218
	internal class SapBwMicrosoftProviderFactoryService : ISapBwDbProviderFactoryService, IDbProviderFactoryService
	{
		// Token: 0x060027F9 RID: 10233 RVA: 0x00075E9F File Offset: 0x0007409F
		private SapBwMicrosoftProviderFactoryService()
		{
			this.factory = SapBwMicrosoftProviderFactoryService.AssemblyLoader.GetFactoryInstance();
			this.destinationCleanup = SapBwMicrosoftProviderFactoryService.AssemblyLoader.DestinationCleanup;
			this.SapConnectorVersion = SapBwMicrosoftProviderFactoryService.AssemblyLoader.SapConnectorVersion;
			this.SapBwProviderVersion = SapBwMicrosoftProviderFactoryService.AssemblyLoader.SapBwProviderVersion;
		}

		// Token: 0x17000F94 RID: 3988
		// (get) Token: 0x060027FA RID: 10234 RVA: 0x00075ED3 File Offset: 0x000740D3
		public static SapBwMicrosoftProviderFactoryService Instance
		{
			get
			{
				if (SapBwMicrosoftProviderFactoryService.instance == null)
				{
					SapBwMicrosoftProviderFactoryService.instance = SapBwMicrosoftProviderFactoryService.InitializeService();
				}
				return SapBwMicrosoftProviderFactoryService.instance;
			}
		}

		// Token: 0x17000F95 RID: 3989
		// (get) Token: 0x060027FB RID: 10235 RVA: 0x00075EEB File Offset: 0x000740EB
		// (set) Token: 0x060027FC RID: 10236 RVA: 0x00075EF3 File Offset: 0x000740F3
		public string SapConnectorVersion { get; private set; }

		// Token: 0x17000F96 RID: 3990
		// (get) Token: 0x060027FD RID: 10237 RVA: 0x00075EFC File Offset: 0x000740FC
		// (set) Token: 0x060027FE RID: 10238 RVA: 0x00075F04 File Offset: 0x00074104
		public string SapBwProviderVersion { get; private set; }

		// Token: 0x060027FF RID: 10239 RVA: 0x00075F0D File Offset: 0x0007410D
		public DbProviderFactory GetProviderFactory()
		{
			return this.factory;
		}

		// Token: 0x06002800 RID: 10240 RVA: 0x00075F15 File Offset: 0x00074115
		public SapBwDestinationTracker GetEvaluationCleanup()
		{
			return new SapBwDestinationTracker(this.destinationCleanup);
		}

		// Token: 0x06002801 RID: 10241 RVA: 0x00075F24 File Offset: 0x00074124
		private static SapBwMicrosoftProviderFactoryService InitializeService()
		{
			SapBwMicrosoftProviderFactoryService sapBwMicrosoftProviderFactoryService;
			try
			{
				sapBwMicrosoftProviderFactoryService = new SapBwMicrosoftProviderFactoryService();
			}
			catch (TypeInitializationException ex)
			{
				throw ex.InnerException;
			}
			catch (Exception ex2) when (SafeExceptions.IsSafeException(ex2) && !(ex2 is ValueException))
			{
				throw SapBwMicrosoftProviderFactoryService.NewMissingLibraryException(ex2, "SAP .NET Connector 3.0.0.42 / SAP .NET Connector 3.1.0.42", "https://go.microsoft.com/fwlink/?linkid=872300");
			}
			return sapBwMicrosoftProviderFactoryService;
		}

		// Token: 0x06002802 RID: 10242 RVA: 0x00075F98 File Offset: 0x00074198
		private static Exception NewMissingLibraryException(Exception exception, string libraryName, string downloadLink)
		{
			return DataSourceException.NewMissingClientLibraryError<Message0>(null, new Message0(SapBwMicrosoftProviderFactoryService.GetClientSoftwareNotFoundExceptionMessage(libraryName, downloadLink)), null, libraryName, downloadLink, exception);
		}

		// Token: 0x06002803 RID: 10243 RVA: 0x00075FB0 File Offset: 0x000741B0
		private static string GetClientSoftwareNotFoundExceptionMessage(string libraryName, string downloadLink)
		{
			return X64Helper.Is64BitProcess ? Strings.DatabaseClientMissingExceptionMessage64bit(libraryName, downloadLink) : Strings.DatabaseClientMissingExceptionMessage32bit(libraryName, downloadLink);
		}

		// Token: 0x040010EF RID: 4335
		private const string SapConnectorDownloadLink = "https://go.microsoft.com/fwlink/?linkid=872300";

		// Token: 0x040010F0 RID: 4336
		private const string SapConnectorLibraryName = "SAP .NET Connector 3.0.0.42 / SAP .NET Connector 3.1.0.42";

		// Token: 0x040010F1 RID: 4337
		private const string SapBwProviderAssemblyName300 = "Microsoft.Mashup.SapBwProvider";

		// Token: 0x040010F2 RID: 4338
		private const string SapBwProviderAssemblyName310 = "Microsoft.Mashup.SapBwProvider31";

		// Token: 0x040010F3 RID: 4339
		private const string SapConnectorAssemblyName300 = "sapnco, Version=3.0.0.42, Culture=neutral, PublicKeyToken=50436dca5c7f7d23";

		// Token: 0x040010F4 RID: 4340
		private const string SapConnectorAssemblyName310 = "sapnco, Version=3.1.0.42, Culture=neutral, PublicKeyToken=50436dca5c7f7d23";

		// Token: 0x040010F5 RID: 4341
		private static SapBwMicrosoftProviderFactoryService instance;

		// Token: 0x040010F6 RID: 4342
		private readonly DbProviderFactory factory;

		// Token: 0x040010F7 RID: 4343
		private readonly Action<string> destinationCleanup;

		// Token: 0x020004C3 RID: 1219
		private static class AssemblyLoader
		{
			// Token: 0x17000F97 RID: 3991
			// (get) Token: 0x06002804 RID: 10244 RVA: 0x00075FCE File Offset: 0x000741CE
			// (set) Token: 0x06002805 RID: 10245 RVA: 0x00075FD5 File Offset: 0x000741D5
			public static Action<string> DestinationCleanup { get; private set; }

			// Token: 0x17000F98 RID: 3992
			// (get) Token: 0x06002806 RID: 10246 RVA: 0x00075FDD File Offset: 0x000741DD
			// (set) Token: 0x06002807 RID: 10247 RVA: 0x00075FE4 File Offset: 0x000741E4
			public static string SapConnectorVersion { get; private set; }

			// Token: 0x17000F99 RID: 3993
			// (get) Token: 0x06002808 RID: 10248 RVA: 0x00075FEC File Offset: 0x000741EC
			// (set) Token: 0x06002809 RID: 10249 RVA: 0x00075FF3 File Offset: 0x000741F3
			public static string SapBwProviderVersion { get; private set; }

			// Token: 0x0600280A RID: 10250 RVA: 0x00075FFC File Offset: 0x000741FC
			public static DbProviderFactory GetFactoryInstance()
			{
				if (SapBwMicrosoftProviderFactoryService.AssemblyLoader.factoryInstance == null)
				{
					if (FxVersionDetector.InstalledFxVersion < ClrVersion.Net45)
					{
						throw SapBwMicrosoftProviderFactoryService.NewMissingLibraryException(new Exception(), "Microsoft .NET Framework 4.5", "https://go.microsoft.com/fwlink/?LinkId=528259");
					}
					Assembly assembly = null;
					if (FxVersionDetector.InstalledFxBuild >= 394802)
					{
						SapBwMicrosoftProviderFactoryService.AssemblyLoader.LoadSapNetConnectorAndSapBwProvider("sapnco, Version=3.1.0.42, Culture=neutral, PublicKeyToken=50436dca5c7f7d23", "Microsoft.Mashup.SapBwProvider31", out assembly);
					}
					if (assembly == null)
					{
						Exception ex = SapBwMicrosoftProviderFactoryService.AssemblyLoader.LoadSapNetConnectorAndSapBwProvider("sapnco, Version=3.0.0.42, Culture=neutral, PublicKeyToken=50436dca5c7f7d23", "Microsoft.Mashup.SapBwProvider", out assembly);
						if (ex != null)
						{
							throw ex;
						}
					}
					if (assembly != null)
					{
						Type type = assembly.GetType("Microsoft.Mashup.SapBwProvider.SapBwProviderFactory");
						try
						{
							object obj = type.InvokeMember("Instance", BindingFlags.Static | BindingFlags.Public | BindingFlags.GetField | BindingFlags.GetProperty, null, type, null, CultureInfo.InvariantCulture);
							SapBwMicrosoftProviderFactoryService.AssemblyLoader.factoryInstance = obj as DbProviderFactory;
							MethodInfo method = type.GetMethod("RemoveDestination", BindingFlags.Instance | BindingFlags.Public);
							SapBwMicrosoftProviderFactoryService.AssemblyLoader.DestinationCleanup = (Action<string>)Delegate.CreateDelegate(typeof(Action<string>), obj, method);
						}
						catch (Exception ex2)
						{
							if (!SafeExceptions.IsSafeException(ex2))
							{
								throw;
							}
							throw new InvalidOperationException("Could not get provider factory instance from SapBwProvider.");
						}
					}
				}
				return SapBwMicrosoftProviderFactoryService.AssemblyLoader.factoryInstance;
			}

			// Token: 0x0600280B RID: 10251 RVA: 0x00076100 File Offset: 0x00074300
			private static Exception LoadSapNetConnectorAndSapBwProvider(string sapConnectorAssemblyName, string sapBwProviderAssemblyName, out Assembly sapBwProviderAssembly)
			{
				sapBwProviderAssembly = null;
				string text = "SAP .NET Connector 3.0.0.42 / SAP .NET Connector 3.1.0.42";
				string text2 = null;
				Exception ex;
				try
				{
					Assembly assembly = Assembly.Load(sapConnectorAssemblyName);
					if (!Assembly.ReflectionOnlyLoad(sapConnectorAssemblyName).GlobalAssemblyCache)
					{
						ex = SapBwMicrosoftProviderFactoryService.NewMissingLibraryException(new FileLoadException(Strings.SapBwAssemblyNotInGac("sapnco, Version=3.0.0.42, Culture=neutral, PublicKeyToken=50436dca5c7f7d23", "sapnco, Version=3.1.0.42, Culture=neutral, PublicKeyToken=50436dca5c7f7d23")), text, text2);
					}
					else
					{
						text = sapBwProviderAssemblyName;
						text2 = string.Empty;
						AssemblyName assemblyName = new AssemblyName(text);
						assemblyName.SetPublicKeyToken(Assembly.GetExecutingAssembly().GetName().GetPublicKeyToken());
						sapBwProviderAssembly = Assembly.Load(assemblyName);
						AssemblyFileVersionAttribute assemblyFileVersionAttribute = assembly.GetCustomAttributes(true).OfType<AssemblyFileVersionAttribute>().FirstOrDefault<AssemblyFileVersionAttribute>();
						SapBwMicrosoftProviderFactoryService.AssemblyLoader.SapConnectorVersion = assembly.FullName + "; FileVersion: " + ((assemblyFileVersionAttribute != null) ? assemblyFileVersionAttribute.Version : null);
						SapBwMicrosoftProviderFactoryService.AssemblyLoader.SapBwProviderVersion = sapBwProviderAssembly.FullName;
						ex = null;
					}
				}
				catch (FileLoadException ex2)
				{
					ex = SapBwMicrosoftProviderFactoryService.NewMissingLibraryException(ex2, text, text2);
				}
				catch (BadImageFormatException ex3)
				{
					ex = SapBwMicrosoftProviderFactoryService.NewMissingLibraryException(ex3, text, text2);
				}
				catch (FileNotFoundException ex4)
				{
					ex = SapBwMicrosoftProviderFactoryService.NewMissingLibraryException(ex4, text, text2);
				}
				return ex;
			}

			// Token: 0x040010FA RID: 4346
			private static DbProviderFactory factoryInstance;
		}
	}
}
