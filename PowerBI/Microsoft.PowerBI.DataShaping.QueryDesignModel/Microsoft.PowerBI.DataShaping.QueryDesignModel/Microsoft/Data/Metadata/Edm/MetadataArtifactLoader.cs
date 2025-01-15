using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Xml;

namespace Microsoft.Data.Metadata.Edm
{
	// Token: 0x020000B7 RID: 183
	internal abstract class MetadataArtifactLoader
	{
		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x06000BCE RID: 3022
		public abstract string Path { get; }

		// Token: 0x06000BCF RID: 3023
		public abstract void CollectFilePermissionPaths(List<string> paths, DataSpace spaceToGet);

		// Token: 0x06000BD0 RID: 3024 RVA: 0x0001E590 File Offset: 0x0001C790
		public static MetadataArtifactLoader Create(List<MetadataArtifactLoader> allCollections)
		{
			return new MetadataArtifactLoaderComposite(allCollections);
		}

		// Token: 0x06000BD1 RID: 3025 RVA: 0x0001E598 File Offset: 0x0001C798
		public static MetadataArtifactLoader CreateCompositeFromXmlReaders(IEnumerable<XmlReader> xmlReaders)
		{
			List<MetadataArtifactLoader> list = new List<MetadataArtifactLoader>();
			foreach (XmlReader xmlReader in xmlReaders)
			{
				if (xmlReader == null)
				{
					throw EntityUtil.CollectionParameterElementIsNull("xmlReaders");
				}
				list.Add(new MetadataArtifactLoaderXmlReaderWrapper(xmlReader));
			}
			return MetadataArtifactLoader.Create(list);
		}

		// Token: 0x06000BD2 RID: 3026 RVA: 0x0001E600 File Offset: 0x0001C800
		internal static void CheckArtifactExtension(string path, string validExtension)
		{
			string extension = MetadataArtifactLoader.GetExtension(path);
			if (!extension.Equals(validExtension, StringComparison.OrdinalIgnoreCase))
			{
				throw EntityUtil.Metadata(Strings.InvalidFileExtension(path, extension, validExtension));
			}
		}

		// Token: 0x06000BD3 RID: 3027 RVA: 0x0001E62C File Offset: 0x0001C82C
		public virtual List<string> GetOriginalPaths()
		{
			return new List<string>(new string[] { this.Path });
		}

		// Token: 0x06000BD4 RID: 3028 RVA: 0x0001E644 File Offset: 0x0001C844
		public virtual List<string> GetOriginalPaths(DataSpace spaceToGet)
		{
			List<string> list = new List<string>();
			if (MetadataArtifactLoader.IsArtifactOfDataSpace(this.Path, spaceToGet))
			{
				list.Add(this.Path);
			}
			return list;
		}

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x06000BD5 RID: 3029 RVA: 0x0001E672 File Offset: 0x0001C872
		public virtual bool IsComposite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000BD6 RID: 3030
		public abstract List<string> GetPaths();

		// Token: 0x06000BD7 RID: 3031
		public abstract List<string> GetPaths(DataSpace spaceToGet);

		// Token: 0x06000BD8 RID: 3032 RVA: 0x0001E675 File Offset: 0x0001C875
		public List<XmlReader> GetReaders()
		{
			return this.GetReaders(null);
		}

		// Token: 0x06000BD9 RID: 3033
		public abstract List<XmlReader> GetReaders(Dictionary<MetadataArtifactLoader, XmlReader> sourceDictionary);

		// Token: 0x06000BDA RID: 3034
		public abstract List<XmlReader> CreateReaders(DataSpace spaceToGet);

		// Token: 0x06000BDB RID: 3035 RVA: 0x0001E67E File Offset: 0x0001C87E
		internal static bool PathStartsWithResPrefix(string path)
		{
			return path.StartsWith(MetadataArtifactLoader.resPathPrefix, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000BDC RID: 3036 RVA: 0x0001E68C File Offset: 0x0001C88C
		protected static bool IsCSpaceArtifact(string resource)
		{
			string extension = MetadataArtifactLoader.GetExtension(resource);
			return !string.IsNullOrEmpty(extension) && string.Compare(extension, ".csdl", StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x06000BDD RID: 3037 RVA: 0x0001E6BC File Offset: 0x0001C8BC
		protected static bool IsSSpaceArtifact(string resource)
		{
			string extension = MetadataArtifactLoader.GetExtension(resource);
			return !string.IsNullOrEmpty(extension) && string.Compare(extension, ".ssdl", StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x06000BDE RID: 3038 RVA: 0x0001E6EC File Offset: 0x0001C8EC
		protected static bool IsCSSpaceArtifact(string resource)
		{
			string extension = MetadataArtifactLoader.GetExtension(resource);
			return !string.IsNullOrEmpty(extension) && string.Compare(extension, ".msl", StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x06000BDF RID: 3039 RVA: 0x0001E71C File Offset: 0x0001C91C
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

		// Token: 0x06000BE0 RID: 3040 RVA: 0x0001E754 File Offset: 0x0001C954
		internal static bool IsValidArtifact(string resource)
		{
			string extension = MetadataArtifactLoader.GetExtension(resource);
			return !string.IsNullOrEmpty(extension) && (string.Compare(extension, ".csdl", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(extension, ".ssdl", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(extension, ".msl", StringComparison.OrdinalIgnoreCase) == 0);
		}

		// Token: 0x06000BE1 RID: 3041 RVA: 0x0001E79F File Offset: 0x0001C99F
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

		// Token: 0x06000BE2 RID: 3042 RVA: 0x0001E7C4 File Offset: 0x0001C9C4
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
				throw EntityUtil.Metadata(Strings.NotValidInputPath, ex);
			}
			catch (NotSupportedException ex2)
			{
				throw EntityUtil.Metadata(Strings.NotValidInputPath, ex2);
			}
			catch (PathTooLongException)
			{
				throw EntityUtil.Metadata(Strings.NotValidInputPath);
			}
			return path;
		}

		// Token: 0x040008C5 RID: 2245
		protected static readonly string resPathPrefix = "res://";

		// Token: 0x040008C6 RID: 2246
		protected static readonly string resPathSeparator = "/";

		// Token: 0x040008C7 RID: 2247
		protected static readonly string altPathSeparator = "\\";

		// Token: 0x040008C8 RID: 2248
		protected static readonly string wildcard = "*";

		// Token: 0x020002CB RID: 715
		public enum ExtensionCheck
		{
			// Token: 0x04000FFE RID: 4094
			None,
			// Token: 0x04000FFF RID: 4095
			Specific,
			// Token: 0x04001000 RID: 4096
			All
		}
	}
}
