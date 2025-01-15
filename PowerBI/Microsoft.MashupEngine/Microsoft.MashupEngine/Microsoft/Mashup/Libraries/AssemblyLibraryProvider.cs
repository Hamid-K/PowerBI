using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace Microsoft.Mashup.Libraries
{
	// Token: 0x020020C4 RID: 8388
	public sealed class AssemblyLibraryProvider : ILibraryProvider, IDisposable
	{
		// Token: 0x0600CD62 RID: 52578 RVA: 0x0028D4C3 File Offset: 0x0028B6C3
		public AssemblyLibraryProvider(AssemblyName assemblyName, Func<string, bool> isEnabled = null)
			: this(Assembly.Load(assemblyName), isEnabled)
		{
		}

		// Token: 0x0600CD63 RID: 52579 RVA: 0x0028D4D2 File Offset: 0x0028B6D2
		public AssemblyLibraryProvider(string fileName, Func<string, bool> isEnabled = null)
			: this(Assembly.LoadFrom(fileName), isEnabled)
		{
		}

		// Token: 0x0600CD64 RID: 52580 RVA: 0x0028D4E4 File Offset: 0x0028B6E4
		public AssemblyLibraryProvider(Assembly assembly, Func<string, bool> isEnabled = null)
		{
			this.assembly = assembly;
			this.assemblyName = assembly.GetName().Name;
			this.identifier = string.Format(CultureInfo.InvariantCulture, "Assembly({0})", this.assembly.GetName().FullName);
			assembly.GetManifestResourceNames();
			Func<string, bool> func;
			if ((func = isEnabled) == null && (func = AssemblyLibraryProvider.<>c.<>9__7_0) == null)
			{
				func = (AssemblyLibraryProvider.<>c.<>9__7_0 = (string name) => true);
			}
			isEnabled = func;
			this.resources = (from name in assembly.GetManifestResourceNames()
				where name.EndsWith(".mez", StringComparison.OrdinalIgnoreCase) || name.EndsWith(".pqx", StringComparison.OrdinalIgnoreCase)
				select name).Where(isEnabled).Select(new Func<string, string>(this.TrimAssembly)).ToArray<string>();
			this.version = string.Format(CultureInfo.InvariantCulture, "{0}.{1}", FileVersionInfo.GetVersionInfo(this.assembly.Location).ProductVersion, File.GetCreationTimeUtc(this.assembly.Location).Ticks);
		}

		// Token: 0x1700315D RID: 12637
		// (get) Token: 0x0600CD65 RID: 52581 RVA: 0x0028D5F4 File Offset: 0x0028B7F4
		public string Identifier
		{
			get
			{
				return this.identifier;
			}
		}

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x0600CD66 RID: 52582 RVA: 0x0000336E File Offset: 0x0000156E
		// (remove) Token: 0x0600CD67 RID: 52583 RVA: 0x0000336E File Offset: 0x0000156E
		event EventHandler<LibraryChangedEventArgs> ILibraryProvider.Changed
		{
			add
			{
			}
			remove
			{
			}
		}

		// Token: 0x0600CD68 RID: 52584 RVA: 0x0028D5FC File Offset: 0x0028B7FC
		public IEnumerable<ILibrary> GetLibraries()
		{
			foreach (string text in this.resources)
			{
				yield return new AssemblyLibraryProvider.Library(this, text);
			}
			string[] array = null;
			yield break;
		}

		// Token: 0x0600CD69 RID: 52585 RVA: 0x0028D60C File Offset: 0x0028B80C
		public bool TryGetLibrary(string identifier, out ILibrary library)
		{
			if (this.resources.Contains(identifier))
			{
				library = new AssemblyLibraryProvider.Library(this, identifier);
				return true;
			}
			library = null;
			return false;
		}

		// Token: 0x0600CD6A RID: 52586 RVA: 0x0000336E File Offset: 0x0000156E
		public void Dispose()
		{
		}

		// Token: 0x0600CD6B RID: 52587 RVA: 0x0028D62C File Offset: 0x0028B82C
		public static void CreateAssembly(string filePath, params ILibrary[] libraries)
		{
			string fileName = Path.GetFileName(filePath);
			string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filePath);
			string directoryName = Path.GetDirectoryName(filePath);
			string currentDirectory = Directory.GetCurrentDirectory();
			try
			{
				if (!Directory.Exists(directoryName))
				{
					Directory.CreateDirectory(directoryName);
				}
				Directory.SetCurrentDirectory(directoryName);
				AssemblyName assemblyName = new AssemblyName(fileNameWithoutExtension);
				AssemblyBuilder assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Save);
				ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule(assemblyName.Name, fileName);
				foreach (ILibrary library in libraries)
				{
					moduleBuilder.DefineManifestResource(AssemblyLibraryProvider.Source(fileNameWithoutExtension, library.Identifier), new MemoryStream(library.Contents), ResourceAttributes.Public);
					foreach (KeyValuePair<string, byte[]> keyValuePair in library.Metadata)
					{
						moduleBuilder.DefineManifestResource(AssemblyLibraryProvider.MetadataRoot(fileNameWithoutExtension, library.Identifier) + keyValuePair.Key, new MemoryStream(keyValuePair.Value), ResourceAttributes.Public);
					}
				}
				assemblyBuilder.Save(fileName);
			}
			finally
			{
				Directory.SetCurrentDirectory(currentDirectory);
			}
		}

		// Token: 0x0600CD6C RID: 52588 RVA: 0x0028D760 File Offset: 0x0028B960
		private string TrimAssembly(string resourceName)
		{
			return resourceName.Substring(this.assemblyName.Length + 1);
		}

		// Token: 0x0600CD6D RID: 52589 RVA: 0x0028D775 File Offset: 0x0028B975
		private IEnumerable<KeyValuePair<string, byte[]>> GetMetadata(string resourceName)
		{
			string prefix = AssemblyLibraryProvider.MetadataRoot(this.assemblyName, resourceName);
			foreach (string text in this.assembly.GetManifestResourceNames())
			{
				byte[] array2;
				if (text.StartsWith(prefix, StringComparison.OrdinalIgnoreCase) && this.TryGetResource(text, out array2))
				{
					yield return new KeyValuePair<string, byte[]>(text.Substring(prefix.Length), array2);
				}
			}
			string[] array = null;
			yield break;
		}

		// Token: 0x0600CD6E RID: 52590 RVA: 0x0028D78C File Offset: 0x0028B98C
		private bool TryGetSource(string resourceName, out byte[] resource)
		{
			return this.TryGetResource(AssemblyLibraryProvider.Source(this.assemblyName, resourceName), out resource);
		}

		// Token: 0x0600CD6F RID: 52591 RVA: 0x0028D7A1 File Offset: 0x0028B9A1
		private bool TryGetMetadata(string resourceName, string metadataName, out byte[] resource)
		{
			return this.TryGetResource(AssemblyLibraryProvider.MetadataRoot(this.assemblyName, resourceName) + metadataName, out resource);
		}

		// Token: 0x0600CD70 RID: 52592 RVA: 0x0028D7BC File Offset: 0x0028B9BC
		private bool TryGetResource(string resourceName, out byte[] resource)
		{
			Stream manifestResourceStream = this.assembly.GetManifestResourceStream(resourceName);
			if (manifestResourceStream == null)
			{
				resource = null;
				return false;
			}
			bool flag;
			using (manifestResourceStream)
			{
				manifestResourceStream.Position = 0L;
				resource = new byte[manifestResourceStream.Length];
				int num = 0;
				while ((long)num < manifestResourceStream.Length)
				{
					num += manifestResourceStream.Read(resource, num, resource.Length - num);
				}
				flag = true;
			}
			return flag;
		}

		// Token: 0x0600CD71 RID: 52593 RVA: 0x0028D838 File Offset: 0x0028BA38
		private static string Source(string assemblyName, string resourceName)
		{
			return assemblyName + "." + resourceName;
		}

		// Token: 0x0600CD72 RID: 52594 RVA: 0x0028D846 File Offset: 0x0028BA46
		private static string MetadataRoot(string assemblyName, string resourceName)
		{
			return assemblyName + ".PreCompileCache." + resourceName + "$";
		}

		// Token: 0x040067E5 RID: 26597
		private readonly string identifier;

		// Token: 0x040067E6 RID: 26598
		private readonly string assemblyName;

		// Token: 0x040067E7 RID: 26599
		private readonly Assembly assembly;

		// Token: 0x040067E8 RID: 26600
		private readonly string[] resources;

		// Token: 0x040067E9 RID: 26601
		private readonly string version;

		// Token: 0x020020C5 RID: 8389
		private sealed class Library : ILibrary
		{
			// Token: 0x0600CD73 RID: 52595 RVA: 0x0028D859 File Offset: 0x0028BA59
			public Library(AssemblyLibraryProvider provider, string resourceName)
			{
				this.provider = provider;
				this.resourceName = resourceName;
			}

			// Token: 0x1700315E RID: 12638
			// (get) Token: 0x0600CD74 RID: 52596 RVA: 0x0028D86F File Offset: 0x0028BA6F
			public ILibraryProvider Provider
			{
				get
				{
					return this.provider;
				}
			}

			// Token: 0x1700315F RID: 12639
			// (get) Token: 0x0600CD75 RID: 52597 RVA: 0x0028D877 File Offset: 0x0028BA77
			public string Identifier
			{
				get
				{
					return this.resourceName;
				}
			}

			// Token: 0x17003160 RID: 12640
			// (get) Token: 0x0600CD76 RID: 52598 RVA: 0x0028D87F File Offset: 0x0028BA7F
			public string Version
			{
				get
				{
					return this.provider.version;
				}
			}

			// Token: 0x17003161 RID: 12641
			// (get) Token: 0x0600CD77 RID: 52599 RVA: 0x0028D88C File Offset: 0x0028BA8C
			public byte[] Contents
			{
				get
				{
					byte[] array;
					this.provider.TryGetSource(this.resourceName, out array);
					return array;
				}
			}

			// Token: 0x17003162 RID: 12642
			// (get) Token: 0x0600CD78 RID: 52600 RVA: 0x0028D8AE File Offset: 0x0028BAAE
			public IEnumerable<KeyValuePair<string, byte[]>> Metadata
			{
				get
				{
					return this.provider.GetMetadata(this.resourceName);
				}
			}

			// Token: 0x0600CD79 RID: 52601 RVA: 0x0028D8C1 File Offset: 0x0028BAC1
			public bool TryGetMetadata(string metadataName, out byte[] metadata)
			{
				return this.provider.TryGetMetadata(this.resourceName, metadataName, out metadata);
			}

			// Token: 0x0600CD7A RID: 52602 RVA: 0x00002105 File Offset: 0x00000305
			public bool TrySetMetadata(string metadataName, byte[] metadata)
			{
				return false;
			}

			// Token: 0x040067EA RID: 26602
			private readonly AssemblyLibraryProvider provider;

			// Token: 0x040067EB RID: 26603
			private readonly string resourceName;
		}
	}
}
