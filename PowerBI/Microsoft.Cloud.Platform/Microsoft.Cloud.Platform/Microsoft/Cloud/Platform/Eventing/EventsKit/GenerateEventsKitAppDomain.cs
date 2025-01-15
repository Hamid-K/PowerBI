using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.Cloud.Platform.Common;
using Microsoft.Cloud.Platform.EventsKit;

namespace Microsoft.Cloud.Platform.Eventing.EventsKit
{
	// Token: 0x020003AB RID: 939
	internal sealed class GenerateEventsKitAppDomain : MarshalByRefObject
	{
		// Token: 0x06001D09 RID: 7433 RVA: 0x0006E248 File Offset: 0x0006C448
		public int Execute(string targetAssembly, string[] references, string outputDirectory, bool logDebugOutput)
		{
			this.targetAssembly = targetAssembly;
			this.logDebugOutput = logDebugOutput;
			this.outputDir = outputDirectory;
			this.InitializeReferencePaths(references);
			Directory.CreateDirectory(this.outputDir);
			List<string> list = new List<string>();
			int num = 0;
			foreach (Type type in this.GetEventsKitInterfaces(targetAssembly))
			{
				EventsKitMetadata eventsKitMetadata = new EventsKitMetadata(type);
				string text = new EventsKitCodeGenerator(EventsKitFactoryOptions.All, eventsKitMetadata).CreateGeneratedEventsKitCode();
				string text2 = Path.Combine(this.outputDir, num.ToString(CultureInfo.InvariantCulture) + ".cs");
				using (StreamWriter streamWriter = new StreamWriter(text2, false))
				{
					streamWriter.Write(text);
				}
				list.Add(text2);
				num++;
			}
			if (num == 0)
			{
				this.LogError("No Event Kits found in assembly. Please set SqlCloudUseEventsKit to false.");
				throw new InvalidOperationException("No Event Kits found in assembly. Please set SqlCloudUseEventsKit to false.");
			}
			using (StreamWriter streamWriter2 = new StreamWriter(Path.Combine(this.outputDir, "FilesToBuildEventsKit.txt")))
			{
				streamWriter2.Write(string.Join(Environment.NewLine, list));
			}
			return 0;
		}

		// Token: 0x06001D0A RID: 7434 RVA: 0x0006E390 File Offset: 0x0006C590
		private void AddAssemblyToCache(string path)
		{
			string name = AssemblyName.GetAssemblyName(path).Name;
			this.LogVerbose(string.Format(CultureInfo.InvariantCulture, "Add Assembly {0} to the cache with path {1}", new object[] { name, path }));
			if (!this.references.ContainsKey(name))
			{
				this.references.Add(name, path);
				return;
			}
			if (!string.Equals(this.references[name], path, StringComparison.OrdinalIgnoreCase))
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "The reference path contained the same dll twice with different source paths. First at: {0} Second at: {1}", new object[]
				{
					path,
					this.references[name]
				}));
			}
		}

		// Token: 0x06001D0B RID: 7435 RVA: 0x0006E42C File Offset: 0x0006C62C
		private bool InitializeReferencePaths(string[] refs)
		{
			foreach (string text in refs)
			{
				this.AddAssemblyToCache(text);
			}
			if (!this.references.ContainsKey(AssemblyName.GetAssemblyName(this.targetAssembly).Name))
			{
				this.AddAssemblyToCache(this.targetAssembly);
			}
			AppDomain.CurrentDomain.AssemblyResolve += this.ResolveFailedAssemblyLoad;
			return true;
		}

		// Token: 0x06001D0C RID: 7436 RVA: 0x0006E494 File Offset: 0x0006C694
		private IEnumerable<Type> GetEventsKitInterfaces(string path)
		{
			if (!EventsKitExplorerFactory.AssemblyHasEventKits(path))
			{
				return new List<Type>();
			}
			Assembly assembly = this.LoadAssembly(AssemblyName.GetAssemblyName(path));
			IEnumerable<Type> enumerable;
			try
			{
				List<Type> list = new List<Type>();
				foreach (Type type in assembly.GetTypes())
				{
					this.LogVerbose(string.Format(CultureInfo.InvariantCulture, "About to load type '{0}' for analysis of EventsKit", new object[] { type.FullName }));
					IList<CustomAttributeData> customAttributes = CustomAttributeData.GetCustomAttributes(type);
					if (customAttributes.Any((CustomAttributeData a) => a.Constructor.DeclaringType.FullName.Equals(typeof(EventsKitAttribute).FullName)))
					{
						if (!customAttributes.Any((CustomAttributeData a) => a.Constructor.DeclaringType.FullName.Equals(typeof(ObsoleteAttribute).FullName)))
						{
							list.Add(type);
						}
					}
				}
				enumerable = list.ToArray();
			}
			catch (ReflectionTypeLoadException ex)
			{
				if (ex.LoaderExceptions == null || ex.LoaderExceptions.Length == 0)
				{
					throw;
				}
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("Could not load a type required for EventsKit generation. Please add the following references as either a project reference or reference to the csproj in the project that this error occurs: ");
				foreach (Exception ex2 in ex.LoaderExceptions)
				{
					stringBuilder.Append(ex2.Message + "\n");
				}
				this.LogError(stringBuilder.ToString());
				throw;
			}
			return enumerable;
		}

		// Token: 0x06001D0D RID: 7437 RVA: 0x0006E5F8 File Offset: 0x0006C7F8
		private Assembly LoadAssembly(AssemblyName assemblyName)
		{
			Assembly assembly = null;
			if (this.assemblies.TryGetValue(assemblyName.Name, out assembly))
			{
				this.LogVerbose(string.Format(CultureInfo.InvariantCulture, "Found assembly {0} in cache", new object[] { assemblyName.FullName }));
				return assembly;
			}
			string text = null;
			if (this.references.TryGetValue(assemblyName.Name, out text))
			{
				this.LogVerbose(string.Format(CultureInfo.InvariantCulture, "Attempt to load assembly {0} from references with path {1}", new object[] { assemblyName.FullName, text }));
				assembly = Assembly.LoadFrom(text);
				this.assemblies.Add(assemblyName.Name, assembly);
				return assembly;
			}
			this.LogVerbose(string.Format(CultureInfo.InvariantCulture, "Load Assembly {0} from system path", new object[] { assemblyName.FullName }));
			assembly = Assembly.Load(assemblyName.Name);
			this.assemblies.Add(assemblyName.Name, assembly);
			return assembly;
		}

		// Token: 0x06001D0E RID: 7438 RVA: 0x0006E6E0 File Offset: 0x0006C8E0
		private Assembly ResolveFailedAssemblyLoad(object sender, ResolveEventArgs args)
		{
			this.LogVerbose(string.Format(CultureInfo.InvariantCulture, "Attempting to load assembly {0}", new object[] { args.Name }));
			AssemblyName assemblyName = new AssemblyName(args.Name);
			return this.LoadAssembly(assemblyName);
		}

		// Token: 0x06001D0F RID: 7439 RVA: 0x0006E724 File Offset: 0x0006C924
		private void LogVerbose(string message)
		{
			if (this.logDebugOutput)
			{
				Console.WriteLine(message);
			}
		}

		// Token: 0x06001D10 RID: 7440 RVA: 0x0006E734 File Offset: 0x0006C934
		private void LogError(string message)
		{
			Console.Error.WriteLine(message);
		}

		// Token: 0x040009B4 RID: 2484
		private readonly Dictionary<string, string> references = new Dictionary<string, string>();

		// Token: 0x040009B5 RID: 2485
		private readonly Dictionary<string, Assembly> assemblies = new Dictionary<string, Assembly>();

		// Token: 0x040009B6 RID: 2486
		private string outputDir;

		// Token: 0x040009B7 RID: 2487
		private string targetAssembly;

		// Token: 0x040009B8 RID: 2488
		private bool logDebugOutput;
	}
}
