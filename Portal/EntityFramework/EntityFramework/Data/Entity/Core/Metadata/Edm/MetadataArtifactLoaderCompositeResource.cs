using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Resources;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Xml;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004D3 RID: 1235
	internal class MetadataArtifactLoaderCompositeResource : MetadataArtifactLoader
	{
		// Token: 0x06003D2E RID: 15662 RVA: 0x000CA878 File Offset: 0x000C8A78
		internal MetadataArtifactLoaderCompositeResource(string originalPath, string assemblyName, string resourceName, ICollection<string> uriRegistry, MetadataArtifactAssemblyResolver resolver)
		{
			this._originalPath = originalPath;
			this._children = new ReadOnlyCollection<MetadataArtifactLoaderResource>(MetadataArtifactLoaderCompositeResource.LoadResources(assemblyName, resourceName, uriRegistry, resolver));
		}

		// Token: 0x17000C0A RID: 3082
		// (get) Token: 0x06003D2F RID: 15663 RVA: 0x000CA89D File Offset: 0x000C8A9D
		public override string Path
		{
			get
			{
				return this._originalPath;
			}
		}

		// Token: 0x17000C0B RID: 3083
		// (get) Token: 0x06003D30 RID: 15664 RVA: 0x000CA8A5 File Offset: 0x000C8AA5
		public override bool IsComposite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003D31 RID: 15665 RVA: 0x000CA8A8 File Offset: 0x000C8AA8
		public override List<string> GetOriginalPaths(DataSpace spaceToGet)
		{
			return this.GetOriginalPaths();
		}

		// Token: 0x06003D32 RID: 15666 RVA: 0x000CA8B0 File Offset: 0x000C8AB0
		public override List<string> GetPaths(DataSpace spaceToGet)
		{
			List<string> list = new List<string>();
			foreach (MetadataArtifactLoaderResource metadataArtifactLoaderResource in this._children)
			{
				list.AddRange(metadataArtifactLoaderResource.GetPaths(spaceToGet));
			}
			return list;
		}

		// Token: 0x06003D33 RID: 15667 RVA: 0x000CA90C File Offset: 0x000C8B0C
		public override List<string> GetPaths()
		{
			List<string> list = new List<string>();
			foreach (MetadataArtifactLoaderResource metadataArtifactLoaderResource in this._children)
			{
				list.AddRange(metadataArtifactLoaderResource.GetPaths());
			}
			return list;
		}

		// Token: 0x06003D34 RID: 15668 RVA: 0x000CA968 File Offset: 0x000C8B68
		public override List<XmlReader> GetReaders(Dictionary<MetadataArtifactLoader, XmlReader> sourceDictionary)
		{
			List<XmlReader> list = new List<XmlReader>();
			foreach (MetadataArtifactLoaderResource metadataArtifactLoaderResource in this._children)
			{
				list.AddRange(metadataArtifactLoaderResource.GetReaders(sourceDictionary));
			}
			return list;
		}

		// Token: 0x06003D35 RID: 15669 RVA: 0x000CA9C4 File Offset: 0x000C8BC4
		public override List<XmlReader> CreateReaders(DataSpace spaceToGet)
		{
			List<XmlReader> list = new List<XmlReader>();
			foreach (MetadataArtifactLoaderResource metadataArtifactLoaderResource in this._children)
			{
				list.AddRange(metadataArtifactLoaderResource.CreateReaders(spaceToGet));
			}
			return list;
		}

		// Token: 0x06003D36 RID: 15670 RVA: 0x000CAA20 File Offset: 0x000C8C20
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
							MetadataArtifactLoaderCompositeResource.LoadResourcesFromAssembly(assembly, resourceName, uriRegistry, list);
						}
					}
					goto IL_005E;
				}
			}
			MetadataArtifactLoaderCompositeResource.LoadResourcesFromAssembly(MetadataArtifactLoaderCompositeResource.ResolveAssemblyName(assemblyName, resolver), resourceName, uriRegistry, list);
			IL_005E:
			if (resourceName != null && list.Count == 0)
			{
				throw new MetadataException(Strings.UnableToLoadResource);
			}
			return list;
		}

		// Token: 0x06003D37 RID: 15671 RVA: 0x000CAAB4 File Offset: 0x000C8CB4
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

		// Token: 0x06003D38 RID: 15672 RVA: 0x000CAAF1 File Offset: 0x000C8CF1
		private static void LoadResourcesFromAssembly(Assembly assembly, string resourceName, ICollection<string> uriRegistry, List<MetadataArtifactLoaderResource> loaders)
		{
			if (resourceName == null)
			{
				MetadataArtifactLoaderCompositeResource.LoadAllResourcesFromAssembly(assembly, uriRegistry, loaders);
				return;
			}
			if (MetadataArtifactLoaderCompositeResource.AssemblyContainsResource(assembly, ref resourceName))
			{
				MetadataArtifactLoaderCompositeResource.CreateAndAddSingleResourceLoader(assembly, resourceName, uriRegistry, loaders);
				return;
			}
			throw new MetadataException(Strings.UnableToLoadResource);
		}

		// Token: 0x06003D39 RID: 15673 RVA: 0x000CAB20 File Offset: 0x000C8D20
		private static void LoadAllResourcesFromAssembly(Assembly assembly, ICollection<string> uriRegistry, List<MetadataArtifactLoaderResource> loaders)
		{
			foreach (string text in MetadataArtifactLoaderCompositeResource.GetManifestResourceNamesForAssembly(assembly))
			{
				MetadataArtifactLoaderCompositeResource.CreateAndAddSingleResourceLoader(assembly, text, uriRegistry, loaders);
			}
		}

		// Token: 0x06003D3A RID: 15674 RVA: 0x000CAB50 File Offset: 0x000C8D50
		private static void CreateAndAddSingleResourceLoader(Assembly assembly, string resourceName, ICollection<string> uriRegistry, List<MetadataArtifactLoaderResource> loaders)
		{
			string text = MetadataArtifactLoaderCompositeResource.CreateResPath(assembly, resourceName);
			if (!uriRegistry.Contains(text))
			{
				loaders.Add(new MetadataArtifactLoaderResource(assembly, resourceName, uriRegistry));
			}
		}

		// Token: 0x06003D3B RID: 15675 RVA: 0x000CAB7C File Offset: 0x000C8D7C
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

		// Token: 0x06003D3C RID: 15676 RVA: 0x000CABB0 File Offset: 0x000C8DB0
		internal static string[] GetManifestResourceNamesForAssembly(Assembly assembly)
		{
			if (assembly.IsDynamic)
			{
				return new string[0];
			}
			return assembly.GetManifestResourceNames();
		}

		// Token: 0x06003D3D RID: 15677 RVA: 0x000CABC8 File Offset: 0x000C8DC8
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

		// Token: 0x06003D3E RID: 15678 RVA: 0x000CABF4 File Offset: 0x000C8DF4
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

		// Token: 0x06003D3F RID: 15679 RVA: 0x000CAC50 File Offset: 0x000C8E50
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
				throw new MetadataException(Strings.InvalidMetadataPath);
			}
		}

		// Token: 0x06003D40 RID: 15680 RVA: 0x000CAC7C File Offset: 0x000C8E7C
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
				throw new MetadataException(Strings.InvalidMetadataPath);
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

		// Token: 0x040014EB RID: 5355
		private readonly ReadOnlyCollection<MetadataArtifactLoaderResource> _children;

		// Token: 0x040014EC RID: 5356
		private readonly string _originalPath;
	}
}
