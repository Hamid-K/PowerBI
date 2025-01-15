using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using dotless.Core.Input;

namespace dotless.Core.Importers
{
	// Token: 0x020000BB RID: 187
	internal class ResourceLoader : MarshalByRefObject
	{
		// Token: 0x06000564 RID: 1380 RVA: 0x00017A7C File Offset: 0x00015C7C
		public static string GetResource(string file, IFileReader fileReader, out string fileDependency)
		{
			fileDependency = null;
			Match match = Importer.EmbeddedResourceRegex.Match(file);
			if (!match.Success)
			{
				return null;
			}
			ResourceLoader resourceLoader = new ResourceLoader
			{
				_resourceName = match.Groups["Resource"].Value
			};
			try
			{
				fileDependency = match.Groups["Assembly"].Value;
				ResourceLoader.LoadFromCurrentAppDomain(resourceLoader, fileDependency);
				if (string.IsNullOrEmpty(resourceLoader._resourceContent))
				{
					ResourceLoader.LoadFromNewAppDomain(resourceLoader, fileReader, fileDependency);
				}
			}
			catch (Exception)
			{
				throw new FileNotFoundException(string.Concat(new string[] { "Unable to load resource [", resourceLoader._resourceName, "] in assembly [", fileDependency, "]" }));
			}
			finally
			{
				resourceLoader._fileContents = null;
			}
			return resourceLoader._resourceContent;
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x00017B5C File Offset: 0x00015D5C
		private static void LoadFromCurrentAppDomain(ResourceLoader loader, string assemblyName)
		{
			IEnumerable<Assembly> assemblies = AppDomain.CurrentDomain.GetAssemblies();
			Func<Assembly, bool> <>9__0;
			Func<Assembly, bool> func;
			if ((func = <>9__0) == null)
			{
				func = (<>9__0 = (Assembly x) => !ResourceLoader.IsDynamicAssembly(x) && x.Location.EndsWith(assemblyName, StringComparison.InvariantCultureIgnoreCase));
			}
			foreach (Assembly assembly in assemblies.Where(func))
			{
				if (assembly.GetManifestResourceNames().Contains(loader._resourceName))
				{
					using (Stream manifestResourceStream = assembly.GetManifestResourceStream(loader._resourceName))
					{
						using (StreamReader streamReader = new StreamReader(manifestResourceStream))
						{
							loader._resourceContent = streamReader.ReadToEnd();
							if (!string.IsNullOrEmpty(loader._resourceContent))
							{
								break;
							}
						}
					}
				}
			}
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x00017C50 File Offset: 0x00015E50
		private static bool IsDynamicAssembly(Assembly assembly)
		{
			bool flag;
			try
			{
				string location = assembly.Location;
				flag = false;
			}
			catch (NotSupportedException)
			{
				flag = true;
			}
			return flag;
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x00017C80 File Offset: 0x00015E80
		private static void LoadFromNewAppDomain(ResourceLoader loader, IFileReader fileReader, string assemblyName)
		{
			if (!fileReader.DoesFileExist(assemblyName))
			{
				throw new FileNotFoundException("Unable to locate assembly file [" + assemblyName + "]");
			}
			loader._fileContents = fileReader.GetBinaryFileContents(assemblyName);
			AppDomain appDomain = AppDomain.CreateDomain("LoaderDomain");
			using (Stream manifestResourceStream = appDomain.Load(loader._fileContents).GetManifestResourceStream(loader._resourceName))
			{
				using (StreamReader streamReader = new StreamReader(manifestResourceStream))
				{
					loader._resourceContent = streamReader.ReadToEnd();
				}
			}
			AppDomain.Unload(appDomain);
		}

		// Token: 0x0400010E RID: 270
		private byte[] _fileContents;

		// Token: 0x0400010F RID: 271
		private string _resourceName;

		// Token: 0x04000110 RID: 272
		private string _resourceContent;
	}
}
