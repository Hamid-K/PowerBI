using System;
using System.Collections.Generic;
using System.Data.EntityModel.SchemaObjectModel;
using System.Xml;

namespace Microsoft.Data.Metadata.Edm
{
	// Token: 0x020000BB RID: 187
	internal class MetadataArtifactLoaderFile : MetadataArtifactLoader, IComparable
	{
		// Token: 0x06000C14 RID: 3092 RVA: 0x0001F4D0 File Offset: 0x0001D6D0
		public MetadataArtifactLoaderFile(string path, ICollection<string> uriRegistry)
		{
			this._path = path;
			this._alreadyLoaded = uriRegistry.Contains(this._path);
			if (!this._alreadyLoaded)
			{
				uriRegistry.Add(this._path);
			}
		}

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x06000C15 RID: 3093 RVA: 0x0001F505 File Offset: 0x0001D705
		public override string Path
		{
			get
			{
				return this._path;
			}
		}

		// Token: 0x06000C16 RID: 3094 RVA: 0x0001F510 File Offset: 0x0001D710
		public int CompareTo(object obj)
		{
			MetadataArtifactLoaderFile metadataArtifactLoaderFile = obj as MetadataArtifactLoaderFile;
			if (metadataArtifactLoaderFile != null)
			{
				return string.Compare(this._path, metadataArtifactLoaderFile._path, StringComparison.OrdinalIgnoreCase);
			}
			return -1;
		}

		// Token: 0x06000C17 RID: 3095 RVA: 0x0001F53B File Offset: 0x0001D73B
		public override bool Equals(object obj)
		{
			return this.CompareTo(obj) == 0;
		}

		// Token: 0x06000C18 RID: 3096 RVA: 0x0001F547 File Offset: 0x0001D747
		public override int GetHashCode()
		{
			return this._path.GetHashCode();
		}

		// Token: 0x06000C19 RID: 3097 RVA: 0x0001F554 File Offset: 0x0001D754
		public override void CollectFilePermissionPaths(List<string> paths, DataSpace spaceToGet)
		{
			if (!this._alreadyLoaded && MetadataArtifactLoader.IsArtifactOfDataSpace(this._path, spaceToGet))
			{
				paths.Add(this._path);
			}
		}

		// Token: 0x06000C1A RID: 3098 RVA: 0x0001F578 File Offset: 0x0001D778
		public override List<string> GetPaths(DataSpace spaceToGet)
		{
			List<string> list = new List<string>();
			if (!this._alreadyLoaded && MetadataArtifactLoader.IsArtifactOfDataSpace(this._path, spaceToGet))
			{
				list.Add(this._path);
			}
			return list;
		}

		// Token: 0x06000C1B RID: 3099 RVA: 0x0001F5B0 File Offset: 0x0001D7B0
		public override List<string> GetPaths()
		{
			List<string> list = new List<string>();
			if (!this._alreadyLoaded)
			{
				list.Add(this._path);
			}
			return list;
		}

		// Token: 0x06000C1C RID: 3100 RVA: 0x0001F5D8 File Offset: 0x0001D7D8
		public override List<XmlReader> GetReaders(Dictionary<MetadataArtifactLoader, XmlReader> sourceDictionary)
		{
			List<XmlReader> list = new List<XmlReader>();
			if (!this._alreadyLoaded)
			{
				XmlReader xmlReader = this.CreateXmlReader();
				list.Add(xmlReader);
				if (sourceDictionary != null)
				{
					sourceDictionary.Add(this, xmlReader);
				}
			}
			return list;
		}

		// Token: 0x06000C1D RID: 3101 RVA: 0x0001F610 File Offset: 0x0001D810
		public override List<XmlReader> CreateReaders(DataSpace spaceToGet)
		{
			List<XmlReader> list = new List<XmlReader>();
			if (!this._alreadyLoaded && MetadataArtifactLoader.IsArtifactOfDataSpace(this._path, spaceToGet))
			{
				XmlReader xmlReader = this.CreateXmlReader();
				list.Add(xmlReader);
			}
			return list;
		}

		// Token: 0x06000C1E RID: 3102 RVA: 0x0001F648 File Offset: 0x0001D848
		private XmlReader CreateXmlReader()
		{
			XmlReaderSettings xmlReaderSettings = Schema.CreateEdmStandardXmlReaderSettings();
			xmlReaderSettings.ConformanceLevel = ConformanceLevel.Document;
			return XmlReader.Create(this._path, xmlReaderSettings);
		}

		// Token: 0x040008D1 RID: 2257
		private readonly bool _alreadyLoaded;

		// Token: 0x040008D2 RID: 2258
		private readonly string _path;
	}
}
