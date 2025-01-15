using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using System.Xml;

namespace Microsoft.Data.Metadata.Edm
{
	// Token: 0x020000BA RID: 186
	internal class MetadataArtifactLoaderCompositeResource : MetadataArtifactLoader
	{
		// Token: 0x06000C00 RID: 3072 RVA: 0x0001F00C File Offset: 0x0001D20C
		internal MetadataArtifactLoaderCompositeResource(string originalPath, string assemblyName, string resourceName, ICollection<string> uriRegistry, MetadataArtifactAssemblyResolver resolver)
		{
			this._originalPath = originalPath;
			this._children = MetadataArtifactLoaderCompositeResource.LoadResources(assemblyName, resourceName, uriRegistry, resolver).AsReadOnly();
		}

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x06000C01 RID: 3073 RVA: 0x0001F031 File Offset: 0x0001D231
		public override string Path
		{
			get
			{
				return this._originalPath;
			}
		}

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x06000C02 RID: 3074 RVA: 0x0001F039 File Offset: 0x0001D239
		public override bool IsComposite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000C03 RID: 3075 RVA: 0x0001F03C File Offset: 0x0001D23C
		public override void CollectFilePermissionPaths(List<string> paths, DataSpace spaceToGet)
		{
			foreach (MetadataArtifactLoaderResource metadataArtifactLoaderResource in this._children)
			{
				metadataArtifactLoaderResource.CollectFilePermissionPaths(paths, spaceToGet);
			}
		}

		// Token: 0x06000C04 RID: 3076 RVA: 0x0001F088 File Offset: 0x0001D288
		public override List<string> GetOriginalPaths(DataSpace spaceToGet)
		{
			return this.GetOriginalPaths();
		}

		// Token: 0x06000C05 RID: 3077 RVA: 0x0001F090 File Offset: 0x0001D290
		public override List<string> GetPaths(DataSpace spaceToGet)
		{
			List<string> list = new List<string>();
			foreach (MetadataArtifactLoaderResource metadataArtifactLoaderResource in this._children)
			{
				list.AddRange(metadataArtifactLoaderResource.GetPaths(spaceToGet));
			}
			return list;
		}

		// Token: 0x06000C06 RID: 3078 RVA: 0x0001F0EC File Offset: 0x0001D2EC
		public override List<string> GetPaths()
		{
			List<string> list = new List<string>();
			foreach (MetadataArtifactLoaderResource metadataArtifactLoaderResource in this._children)
			{
				list.AddRange(metadataArtifactLoaderResource.GetPaths());
			}
			return list;
		}

		// Token: 0x06000C07 RID: 3079 RVA: 0x0001F148 File Offset: 0x0001D348
		public override List<XmlReader> GetReaders(Dictionary<MetadataArtifactLoader, XmlReader> sourceDictionary)
		{
			List<XmlReader> list = new List<XmlReader>();
			foreach (MetadataArtifactLoaderResource metadataArtifactLoaderResource in this._children)
			{
				list.AddRange(metadataArtifactLoaderResource.GetReaders(sourceDictionary));
			}
			return list;
		}

		// Token: 0x06000C08 RID: 3080 RVA: 0x0001F1A4 File Offset: 0x0001D3A4
		public override List<XmlReader> CreateReaders(DataSpace spaceToGet)
		{
			List<XmlReader> list = new List<XmlReader>();
			foreach (MetadataArtifactLoaderResource metadataArtifactLoaderResource in this._children)
			{
				list.AddRange(metadataArtifactLoaderResource.CreateReaders(spaceToGet));
			}
			return list;
		}

		// Token: 0x06000C09 RID: 3081 RVA: 0x0001F200 File Offset: 0x0001D400
		private static List<MetadataArtifactLoaderResource> LoadResources(string assemblyName, string resourceName, ICollection<string> uriRegistry, MetadataArtifactAssemblyResolver resolver)
		{
			List<MetadataArtifactLoaderResource> list = new List<MetadataArtifactLoaderResource>();
			if (assemblyName == MetadataArtifactLoader.wildcard)
			{
				using (IEnumerator<Assembly> enumerator = resolver.GetWildcardAssemblies().GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Assembly assembly = enumerator.Current;
						if (MetadataArtifactLoaderCompositeResource.AssemblyContainsResource(assembly, ref resourceName))
						{
							MetadataArtifactLoaderCompositeResource.LoadResourcesFromAssembly(assembly, resourceName, uriRegistry, list, resolver);
						}
					}
					goto IL_0060;
				}
			}
			MetadataArtifactLoaderCompositeResource.LoadResourcesFromAssembly(MetadataArtifactLoaderCompositeResource.ResolveAssemblyName(assemblyName, resolver), resourceName, uriRegistry, list, resolver);
			IL_0060:
			if (resourceName != null && list.Count == 0)
			{
				throw EntityUtil.Metadata(Strings.UnableToLoadResource);
			}
			return list;
		}

		// Token: 0x06000C0A RID: 3082 RVA: 0x0001F294 File Offset: 0x0001D494
		private static bool AssemblyContainsResource(Assembly assembly, ref string resourceName)
		{
			if (resourceName == null)
			{
				return true;
			}
			foreach (string text in MetadataArtifactLoaderCompositeResource.GetManifestResourceNamesForAssembly(assembly))
			{
				if (string.Equals(resourceName, text, StringComparison.OrdinalIgnoreCase))
				{
					resourceName = text;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000C0B RID: 3083 RVA: 0x0001F2D1 File Offset: 0x0001D4D1
		private static void LoadResourcesFromAssembly(Assembly assembly, string resourceName, ICollection<string> uriRegistry, List<MetadataArtifactLoaderResource> loaders, MetadataArtifactAssemblyResolver resolver)
		{
			if (resourceName == null)
			{
				MetadataArtifactLoaderCompositeResource.LoadAllResourcesFromAssembly(assembly, uriRegistry, loaders, resolver);
				return;
			}
			if (MetadataArtifactLoaderCompositeResource.AssemblyContainsResource(assembly, ref resourceName))
			{
				MetadataArtifactLoaderCompositeResource.CreateAndAddSingleResourceLoader(assembly, resourceName, uriRegistry, loaders);
				return;
			}
			throw EntityUtil.Metadata(Strings.UnableToLoadResource);
		}

		// Token: 0x06000C0C RID: 3084 RVA: 0x0001F300 File Offset: 0x0001D500
		private static void LoadAllResourcesFromAssembly(Assembly assembly, ICollection<string> uriRegistry, List<MetadataArtifactLoaderResource> loaders, MetadataArtifactAssemblyResolver resolver)
		{
			foreach (string text in MetadataArtifactLoaderCompositeResource.GetManifestResourceNamesForAssembly(assembly))
			{
				MetadataArtifactLoaderCompositeResource.CreateAndAddSingleResourceLoader(assembly, text, uriRegistry, loaders);
			}
		}

		// Token: 0x06000C0D RID: 3085 RVA: 0x0001F330 File Offset: 0x0001D530
		private static void CreateAndAddSingleResourceLoader(Assembly assembly, string resourceName, ICollection<string> uriRegistry, List<MetadataArtifactLoaderResource> loaders)
		{
			string text = MetadataArtifactLoaderCompositeResource.CreateResPath(assembly, resourceName);
			if (!uriRegistry.Contains(text))
			{
				loaders.Add(new MetadataArtifactLoaderResource(assembly, resourceName, uriRegistry));
			}
		}

		// Token: 0x06000C0E RID: 3086 RVA: 0x0001F35C File Offset: 0x0001D55C
		internal static string CreateResPath(Assembly assembly, string resourceName)
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}{1}{2}{3}", new object[]
			{
				MetadataArtifactLoader.resPathPrefix,
				assembly.FullName,
				MetadataArtifactLoader.resPathSeparator,
				resourceName
			});
		}

		// Token: 0x06000C0F RID: 3087 RVA: 0x0001F390 File Offset: 0x0001D590
		internal static string[] GetManifestResourceNamesForAssembly(Assembly assembly)
		{
			if (assembly.ManifestModule is ModuleBuilder)
			{
				return new string[0];
			}
			return assembly.GetManifestResourceNames();
		}

		// Token: 0x06000C10 RID: 3088 RVA: 0x0001F3AC File Offset: 0x0001D5AC
		private static Assembly ResolveAssemblyName(string assemblyName, MetadataArtifactAssemblyResolver resolver)
		{
			AssemblyName assemblyName2 = new AssemblyName(assemblyName);
			Assembly assembly;
			if (!resolver.TryResolveAssemblyReference(assemblyName2, out assembly))
			{
				throw new FileNotFoundException(Strings.UnableToResolveAssembly(assemblyName));
			}
			return assembly;
		}

		// Token: 0x06000C11 RID: 3089 RVA: 0x0001F3D8 File Offset: 0x0001D5D8
		internal static MetadataArtifactLoader CreateResourceLoader(string path, MetadataArtifactLoader.ExtensionCheck extensionCheck, string validExtension, ICollection<string> uriRegistry, MetadataArtifactAssemblyResolver resolver)
		{
			string text = null;
			string text2 = null;
			MetadataArtifactLoaderCompositeResource.ParseResourcePath(path, out text, out text2);
			bool flag = text != null && (text2 == null || text.Trim() == MetadataArtifactLoader.wildcard);
			MetadataArtifactLoaderCompositeResource.ValidateExtension(extensionCheck, validExtension, text2);
			if (flag)
			{
				return new MetadataArtifactLoaderCompositeResource(path, text, text2, uriRegistry, resolver);
			}
			return new MetadataArtifactLoaderResource(MetadataArtifactLoaderCompositeResource.ResolveAssemblyName(text, resolver), text2, uriRegistry);
		}

		// Token: 0x06000C12 RID: 3090 RVA: 0x0001F434 File Offset: 0x0001D634
		private static void ValidateExtension(MetadataArtifactLoader.ExtensionCheck extensionCheck, string validExtension, string resourceName)
		{
			if (resourceName == null)
			{
				return;
			}
			if (extensionCheck == MetadataArtifactLoader.ExtensionCheck.Specific)
			{
				MetadataArtifactLoader.CheckArtifactExtension(resourceName, validExtension);
				return;
			}
			if (extensionCheck != MetadataArtifactLoader.ExtensionCheck.All)
			{
				return;
			}
			if (!MetadataArtifactLoader.IsValidArtifact(resourceName))
			{
				throw EntityUtil.Metadata(Strings.InvalidMetadataPath);
			}
		}

		// Token: 0x06000C13 RID: 3091 RVA: 0x0001F460 File Offset: 0x0001D660
		private static void ParseResourcePath(string path, out string assemblyName, out string resourceName)
		{
			int length = MetadataArtifactLoader.resPathPrefix.Length;
			string[] array = path.Substring(length).Split(new string[]
			{
				MetadataArtifactLoader.resPathSeparator,
				MetadataArtifactLoader.altPathSeparator
			}, StringSplitOptions.RemoveEmptyEntries);
			if (array.Length == 0 || array.Length > 2)
			{
				throw EntityUtil.Metadata(Strings.InvalidMetadataPath);
			}
			if (array.Length >= 1)
			{
				assemblyName = array[0];
			}
			else
			{
				assemblyName = null;
			}
			if (array.Length == 2)
			{
				resourceName = array[1];
				return;
			}
			resourceName = null;
		}

		// Token: 0x040008CF RID: 2255
		private readonly ReadOnlyCollection<MetadataArtifactLoaderResource> _children;

		// Token: 0x040008D0 RID: 2256
		private readonly string _originalPath;
	}
}
