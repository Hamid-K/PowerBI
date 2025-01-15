using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Resources;
using System.IO;
using System.Xml;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004D0 RID: 1232
	internal abstract class MetadataArtifactLoader
	{
		// Token: 0x17000C01 RID: 3073
		// (get) Token: 0x06003CFB RID: 15611
		public abstract string Path { get; }

		// Token: 0x06003CFC RID: 15612 RVA: 0x000C9D59 File Offset: 0x000C7F59
		public static MetadataArtifactLoader Create(string path, MetadataArtifactLoader.ExtensionCheck extensionCheck, string validExtension, ICollection<string> uriRegistry)
		{
			return MetadataArtifactLoader.Create(path, extensionCheck, validExtension, uriRegistry, new DefaultAssemblyResolver());
		}

		// Token: 0x06003CFD RID: 15613 RVA: 0x000C9D6C File Offset: 0x000C7F6C
		internal static MetadataArtifactLoader Create(string path, MetadataArtifactLoader.ExtensionCheck extensionCheck, string validExtension, ICollection<string> uriRegistry, MetadataArtifactAssemblyResolver resolver)
		{
			if (MetadataArtifactLoader.PathStartsWithResPrefix(path))
			{
				return MetadataArtifactLoaderCompositeResource.CreateResourceLoader(path, extensionCheck, validExtension, uriRegistry, resolver);
			}
			string text = MetadataArtifactLoader.NormalizeFilePaths(path);
			if (Directory.Exists(text))
			{
				return new MetadataArtifactLoaderCompositeFile(text, uriRegistry);
			}
			if (File.Exists(text))
			{
				if (extensionCheck != MetadataArtifactLoader.ExtensionCheck.Specific)
				{
					if (extensionCheck == MetadataArtifactLoader.ExtensionCheck.All)
					{
						if (!MetadataArtifactLoader.IsValidArtifact(text))
						{
							throw new MetadataException(Strings.InvalidMetadataPath);
						}
					}
				}
				else
				{
					MetadataArtifactLoader.CheckArtifactExtension(text, validExtension);
				}
				return new MetadataArtifactLoaderFile(text, uriRegistry);
			}
			throw new MetadataException(Strings.InvalidMetadataPath);
		}

		// Token: 0x06003CFE RID: 15614 RVA: 0x000C9DE4 File Offset: 0x000C7FE4
		public static MetadataArtifactLoader Create(List<MetadataArtifactLoader> allCollections)
		{
			return new MetadataArtifactLoaderComposite(allCollections);
		}

		// Token: 0x06003CFF RID: 15615 RVA: 0x000C9DEC File Offset: 0x000C7FEC
		public static MetadataArtifactLoader CreateCompositeFromFilePaths(IEnumerable<string> filePaths, string validExtension)
		{
			return MetadataArtifactLoader.CreateCompositeFromFilePaths(filePaths, validExtension, new DefaultAssemblyResolver());
		}

		// Token: 0x06003D00 RID: 15616 RVA: 0x000C9DFC File Offset: 0x000C7FFC
		internal static MetadataArtifactLoader CreateCompositeFromFilePaths(IEnumerable<string> filePaths, string validExtension, MetadataArtifactAssemblyResolver resolver)
		{
			MetadataArtifactLoader.ExtensionCheck extensionCheck;
			if (string.IsNullOrEmpty(validExtension))
			{
				extensionCheck = MetadataArtifactLoader.ExtensionCheck.All;
			}
			else
			{
				extensionCheck = MetadataArtifactLoader.ExtensionCheck.Specific;
			}
			List<MetadataArtifactLoader> list = new List<MetadataArtifactLoader>();
			HashSet<string> hashSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			foreach (string text in filePaths)
			{
				if (string.IsNullOrEmpty(text))
				{
					throw new MetadataException(Strings.NotValidInputPath, new ArgumentException(Strings.ADP_CollectionParameterElementIsNullOrEmpty("filePaths")));
				}
				string text2 = text.Trim();
				if (text2.Length > 0)
				{
					list.Add(MetadataArtifactLoader.Create(text2, extensionCheck, validExtension, hashSet, resolver));
				}
			}
			return MetadataArtifactLoader.Create(list);
		}

		// Token: 0x06003D01 RID: 15617 RVA: 0x000C9EA8 File Offset: 0x000C80A8
		public static MetadataArtifactLoader CreateCompositeFromXmlReaders(IEnumerable<XmlReader> xmlReaders)
		{
			List<MetadataArtifactLoader> list = new List<MetadataArtifactLoader>();
			foreach (XmlReader xmlReader in xmlReaders)
			{
				if (xmlReader == null)
				{
					throw new ArgumentException(Strings.ADP_CollectionParameterElementIsNull("xmlReaders"));
				}
				list.Add(new MetadataArtifactLoaderXmlReaderWrapper(xmlReader));
			}
			return MetadataArtifactLoader.Create(list);
		}

		// Token: 0x06003D02 RID: 15618 RVA: 0x000C9F14 File Offset: 0x000C8114
		internal static void CheckArtifactExtension(string path, string validExtension)
		{
			string extension = MetadataArtifactLoader.GetExtension(path);
			if (!extension.Equals(validExtension, StringComparison.OrdinalIgnoreCase))
			{
				throw new MetadataException(Strings.InvalidFileExtension(path, extension, validExtension));
			}
		}

		// Token: 0x06003D03 RID: 15619 RVA: 0x000C9F40 File Offset: 0x000C8140
		public virtual List<string> GetOriginalPaths()
		{
			return new List<string>(new string[] { this.Path });
		}

		// Token: 0x06003D04 RID: 15620 RVA: 0x000C9F58 File Offset: 0x000C8158
		public virtual List<string> GetOriginalPaths(DataSpace spaceToGet)
		{
			List<string> list = new List<string>();
			if (MetadataArtifactLoader.IsArtifactOfDataSpace(this.Path, spaceToGet))
			{
				list.Add(this.Path);
			}
			return list;
		}

		// Token: 0x17000C02 RID: 3074
		// (get) Token: 0x06003D05 RID: 15621 RVA: 0x000C9F86 File Offset: 0x000C8186
		public virtual bool IsComposite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06003D06 RID: 15622
		public abstract List<string> GetPaths();

		// Token: 0x06003D07 RID: 15623
		public abstract List<string> GetPaths(DataSpace spaceToGet);

		// Token: 0x06003D08 RID: 15624 RVA: 0x000C9F89 File Offset: 0x000C8189
		public List<XmlReader> GetReaders()
		{
			return this.GetReaders(null);
		}

		// Token: 0x06003D09 RID: 15625
		public abstract List<XmlReader> GetReaders(Dictionary<MetadataArtifactLoader, XmlReader> sourceDictionary);

		// Token: 0x06003D0A RID: 15626
		public abstract List<XmlReader> CreateReaders(DataSpace spaceToGet);

		// Token: 0x06003D0B RID: 15627 RVA: 0x000C9F92 File Offset: 0x000C8192
		internal static bool PathStartsWithResPrefix(string path)
		{
			return path.StartsWith(MetadataArtifactLoader.resPathPrefix, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06003D0C RID: 15628 RVA: 0x000C9FA0 File Offset: 0x000C81A0
		protected static bool IsCSpaceArtifact(string resource)
		{
			string extension = MetadataArtifactLoader.GetExtension(resource);
			return !string.IsNullOrEmpty(extension) && string.Compare(extension, ".csdl", StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x06003D0D RID: 15629 RVA: 0x000C9FD0 File Offset: 0x000C81D0
		protected static bool IsSSpaceArtifact(string resource)
		{
			string extension = MetadataArtifactLoader.GetExtension(resource);
			return !string.IsNullOrEmpty(extension) && string.Compare(extension, ".ssdl", StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x06003D0E RID: 15630 RVA: 0x000CA000 File Offset: 0x000C8200
		protected static bool IsCSSpaceArtifact(string resource)
		{
			string extension = MetadataArtifactLoader.GetExtension(resource);
			return !string.IsNullOrEmpty(extension) && string.Compare(extension, ".msl", StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x06003D0F RID: 15631 RVA: 0x000CA030 File Offset: 0x000C8230
		private static string GetExtension(string resource)
		{
			if (string.IsNullOrEmpty(resource))
			{
				return string.Empty;
			}
			int num = resource.LastIndexOf('.');
			if (num < 0)
			{
				return string.Empty;
			}
			return resource.Substring(num);
		}

		// Token: 0x06003D10 RID: 15632 RVA: 0x000CA068 File Offset: 0x000C8268
		internal static bool IsValidArtifact(string resource)
		{
			string extension = MetadataArtifactLoader.GetExtension(resource);
			return !string.IsNullOrEmpty(extension) && (string.Compare(extension, ".csdl", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(extension, ".ssdl", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(extension, ".msl", StringComparison.OrdinalIgnoreCase) == 0);
		}

		// Token: 0x06003D11 RID: 15633 RVA: 0x000CA0B3 File Offset: 0x000C82B3
		protected static bool IsArtifactOfDataSpace(string resource, DataSpace dataSpace)
		{
			if (dataSpace == DataSpace.CSpace)
			{
				return MetadataArtifactLoader.IsCSpaceArtifact(resource);
			}
			if (dataSpace == DataSpace.SSpace)
			{
				return MetadataArtifactLoader.IsSSpaceArtifact(resource);
			}
			return dataSpace == DataSpace.CSSpace && MetadataArtifactLoader.IsCSSpaceArtifact(resource);
		}

		// Token: 0x06003D12 RID: 15634 RVA: 0x000CA0D8 File Offset: 0x000C82D8
		internal static string NormalizeFilePaths(string path)
		{
			bool flag = true;
			if (!string.IsNullOrEmpty(path))
			{
				path = path.Trim();
				if (path.StartsWith("~", StringComparison.Ordinal))
				{
					path = new AspProxy().MapWebPath(path);
					flag = false;
				}
				if (path.Length == 2 && path[1] == global::System.IO.Path.VolumeSeparatorChar)
				{
					path += global::System.IO.Path.DirectorySeparatorChar.ToString();
				}
				else
				{
					string text = DbProviderServices.ExpandDataDirectory(path);
					if (!path.Equals(text, StringComparison.Ordinal))
					{
						path = text;
						flag = false;
					}
				}
			}
			try
			{
				if (flag)
				{
					path = global::System.IO.Path.GetFullPath(path);
				}
			}
			catch (ArgumentException ex)
			{
				throw new MetadataException(Strings.NotValidInputPath, ex);
			}
			catch (NotSupportedException ex2)
			{
				throw new MetadataException(Strings.NotValidInputPath, ex2);
			}
			catch (PathTooLongException)
			{
				throw new MetadataException(Strings.NotValidInputPath);
			}
			return path;
		}

		// Token: 0x040014E1 RID: 5345
		protected static readonly string resPathPrefix = "res://";

		// Token: 0x040014E2 RID: 5346
		protected static readonly string resPathSeparator = "/";

		// Token: 0x040014E3 RID: 5347
		protected static readonly string altPathSeparator = "\\";

		// Token: 0x040014E4 RID: 5348
		protected static readonly string wildcard = "*";

		// Token: 0x02000AF2 RID: 2802
		public enum ExtensionCheck
		{
			// Token: 0x04002C4B RID: 11339
			None,
			// Token: 0x04002C4C RID: 11340
			Specific,
			// Token: 0x04002C4D RID: 11341
			All
		}
	}
}
