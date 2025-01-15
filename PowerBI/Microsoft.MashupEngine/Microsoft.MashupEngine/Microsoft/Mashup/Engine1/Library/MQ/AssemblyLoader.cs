using System;
using System.IO;
using System.Reflection;
using Microsoft.Internal;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.MQ
{
	// Token: 0x0200093C RID: 2364
	public class AssemblyLoader
	{
		// Token: 0x17001562 RID: 5474
		// (get) Token: 0x0600434F RID: 17231 RVA: 0x000E3438 File Offset: 0x000E1638
		// (set) Token: 0x06004350 RID: 17232 RVA: 0x000E343F File Offset: 0x000E163F
		public static Assembly HisConnectors { get; protected set; }

		// Token: 0x06004351 RID: 17233 RVA: 0x000E3447 File Offset: 0x000E1647
		private static void MissingLibrary(IEngineHost engineHost, Exception exception)
		{
			throw DataSourceException.NewMissingClientLibraryError<Message0>(engineHost, new Message0(AssemblyLoader.GetClientSoftwareNotFoundExceptionMessage("Microsoft .NET Framework 4.6", "https://go.microsoft.com/fwlink/?LinkId=528259")), null, "Microsoft .NET Framework 4.6", "https://go.microsoft.com/fwlink/?LinkId=528259", exception);
		}

		// Token: 0x06004352 RID: 17234 RVA: 0x000E346F File Offset: 0x000E166F
		private static string GetClientSoftwareNotFoundExceptionMessage(string libraryName, string downloadLink)
		{
			if (X64Helper.Is64BitProcess)
			{
				return Strings.DatabaseClientMissingExceptionMessage64bit(libraryName, downloadLink);
			}
			return Strings.DatabaseClientMissingExceptionMessage32bit(libraryName, downloadLink);
		}

		// Token: 0x06004353 RID: 17235 RVA: 0x000E3494 File Offset: 0x000E1694
		public static void EnsureHisConnectorsLoaded(IEngineHost engineHost)
		{
			if (AssemblyLoader.HisConnectors == null)
			{
				try
				{
					AssemblyName assemblyName = new AssemblyName("Microsoft.HostIntegration.Connectors");
					assemblyName.SetPublicKeyToken(Assembly.GetExecutingAssembly().GetName().GetPublicKeyToken());
					AssemblyLoader.HisConnectors = Assembly.Load(assemblyName);
					if (!Utilities.IsNet46Installed)
					{
						AssemblyLoader.MissingLibrary(engineHost, new Exception());
					}
				}
				catch (FileLoadException ex)
				{
					AssemblyLoader.MissingLibrary(engineHost, ex);
				}
				catch (BadImageFormatException ex2)
				{
					AssemblyLoader.MissingLibrary(engineHost, ex2);
				}
				catch (FileNotFoundException ex3)
				{
					AssemblyLoader.MissingLibrary(engineHost, ex3);
				}
			}
		}

		// Token: 0x04002354 RID: 9044
		public static object[] EmptyArray = new object[0];

		// Token: 0x04002355 RID: 9045
		private const string DownloadLink = "https://go.microsoft.com/fwlink/?LinkId=528259";

		// Token: 0x04002356 RID: 9046
		private const string ClientLibraryName = "Microsoft .NET Framework 4.6";
	}
}
