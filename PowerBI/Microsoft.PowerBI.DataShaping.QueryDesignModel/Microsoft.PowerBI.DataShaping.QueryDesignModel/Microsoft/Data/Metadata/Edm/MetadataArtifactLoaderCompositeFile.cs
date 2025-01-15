using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading;
using System.Xml;

namespace Microsoft.Data.Metadata.Edm
{
	// Token: 0x020000B9 RID: 185
	internal class MetadataArtifactLoaderCompositeFile : MetadataArtifactLoader
	{
		// Token: 0x06000BF1 RID: 3057 RVA: 0x0001EB72 File Offset: 0x0001CD72
		public MetadataArtifactLoaderCompositeFile(string path, ICollection<string> uriRegistry)
		{
			this._path = path;
			this._uriRegistry = uriRegistry;
		}

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x06000BF2 RID: 3058 RVA: 0x0001EB88 File Offset: 0x0001CD88
		public override string Path
		{
			get
			{
				return this._path;
			}
		}

		// Token: 0x06000BF3 RID: 3059 RVA: 0x0001EB90 File Offset: 0x0001CD90
		public override void CollectFilePermissionPaths(List<string> paths, DataSpace spaceToGet)
		{
			IList<MetadataArtifactLoaderFile> list;
			if (this.TryGetListForSpace(spaceToGet, out list))
			{
				foreach (MetadataArtifactLoaderFile metadataArtifactLoaderFile in list)
				{
					metadataArtifactLoaderFile.CollectFilePermissionPaths(paths, spaceToGet);
				}
			}
		}

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x06000BF4 RID: 3060 RVA: 0x0001EBE4 File Offset: 0x0001CDE4
		public override bool IsComposite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x06000BF5 RID: 3061 RVA: 0x0001EBE7 File Offset: 0x0001CDE7
		internal ReadOnlyCollection<MetadataArtifactLoaderFile> CsdlChildren
		{
			get
			{
				this.LoadCollections();
				return this._csdlChildren;
			}
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x06000BF6 RID: 3062 RVA: 0x0001EBF5 File Offset: 0x0001CDF5
		internal ReadOnlyCollection<MetadataArtifactLoaderFile> SsdlChildren
		{
			get
			{
				this.LoadCollections();
				return this._ssdlChildren;
			}
		}

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x06000BF7 RID: 3063 RVA: 0x0001EC03 File Offset: 0x0001CE03
		internal ReadOnlyCollection<MetadataArtifactLoaderFile> MslChildren
		{
			get
			{
				this.LoadCollections();
				return this._mslChildren;
			}
		}

		// Token: 0x06000BF8 RID: 3064 RVA: 0x0001EC14 File Offset: 0x0001CE14
		private void LoadCollections()
		{
			if (this._csdlChildren == null)
			{
				ReadOnlyCollection<MetadataArtifactLoaderFile> readOnlyCollection = MetadataArtifactLoaderCompositeFile.GetArtifactsInDirectory(this._path, ".csdl", this._uriRegistry).AsReadOnly();
				Interlocked.CompareExchange<ReadOnlyCollection<MetadataArtifactLoaderFile>>(ref this._csdlChildren, readOnlyCollection, null);
			}
			if (this._ssdlChildren == null)
			{
				ReadOnlyCollection<MetadataArtifactLoaderFile> readOnlyCollection2 = MetadataArtifactLoaderCompositeFile.GetArtifactsInDirectory(this._path, ".ssdl", this._uriRegistry).AsReadOnly();
				Interlocked.CompareExchange<ReadOnlyCollection<MetadataArtifactLoaderFile>>(ref this._ssdlChildren, readOnlyCollection2, null);
			}
			if (this._mslChildren == null)
			{
				ReadOnlyCollection<MetadataArtifactLoaderFile> readOnlyCollection3 = MetadataArtifactLoaderCompositeFile.GetArtifactsInDirectory(this._path, ".msl", this._uriRegistry).AsReadOnly();
				Interlocked.CompareExchange<ReadOnlyCollection<MetadataArtifactLoaderFile>>(ref this._mslChildren, readOnlyCollection3, null);
			}
		}

		// Token: 0x06000BF9 RID: 3065 RVA: 0x0001ECB7 File Offset: 0x0001CEB7
		public override List<string> GetOriginalPaths(DataSpace spaceToGet)
		{
			return this.GetOriginalPaths();
		}

		// Token: 0x06000BFA RID: 3066 RVA: 0x0001ECC0 File Offset: 0x0001CEC0
		public override List<string> GetPaths(DataSpace spaceToGet)
		{
			List<string> list = new List<string>();
			IList<MetadataArtifactLoaderFile> list2;
			if (!this.TryGetListForSpace(spaceToGet, out list2))
			{
				return list;
			}
			foreach (MetadataArtifactLoaderFile metadataArtifactLoaderFile in list2)
			{
				list.AddRange(metadataArtifactLoaderFile.GetPaths(spaceToGet));
			}
			return list;
		}

		// Token: 0x06000BFB RID: 3067 RVA: 0x0001ED24 File Offset: 0x0001CF24
		private bool TryGetListForSpace(DataSpace spaceToGet, out IList<MetadataArtifactLoaderFile> files)
		{
			switch (spaceToGet)
			{
			case DataSpace.CSpace:
				files = this.CsdlChildren;
				return true;
			case DataSpace.SSpace:
				files = this.SsdlChildren;
				return true;
			case DataSpace.CSSpace:
				files = this.MslChildren;
				return true;
			}
			files = null;
			return false;
		}

		// Token: 0x06000BFC RID: 3068 RVA: 0x0001ED64 File Offset: 0x0001CF64
		public override List<string> GetPaths()
		{
			List<string> list = new List<string>();
			foreach (MetadataArtifactLoaderFile metadataArtifactLoaderFile in this.CsdlChildren)
			{
				list.AddRange(metadataArtifactLoaderFile.GetPaths());
			}
			foreach (MetadataArtifactLoaderFile metadataArtifactLoaderFile2 in this.SsdlChildren)
			{
				list.AddRange(metadataArtifactLoaderFile2.GetPaths());
			}
			foreach (MetadataArtifactLoaderFile metadataArtifactLoaderFile3 in this.MslChildren)
			{
				list.AddRange(metadataArtifactLoaderFile3.GetPaths());
			}
			return list;
		}

		// Token: 0x06000BFD RID: 3069 RVA: 0x0001EE44 File Offset: 0x0001D044
		public override List<XmlReader> GetReaders(Dictionary<MetadataArtifactLoader, XmlReader> sourceDictionary)
		{
			List<XmlReader> list = new List<XmlReader>();
			foreach (MetadataArtifactLoaderFile metadataArtifactLoaderFile in this.CsdlChildren)
			{
				list.AddRange(metadataArtifactLoaderFile.GetReaders(sourceDictionary));
			}
			foreach (MetadataArtifactLoaderFile metadataArtifactLoaderFile2 in this.SsdlChildren)
			{
				list.AddRange(metadataArtifactLoaderFile2.GetReaders(sourceDictionary));
			}
			foreach (MetadataArtifactLoaderFile metadataArtifactLoaderFile3 in this.MslChildren)
			{
				list.AddRange(metadataArtifactLoaderFile3.GetReaders(sourceDictionary));
			}
			return list;
		}

		// Token: 0x06000BFE RID: 3070 RVA: 0x0001EF24 File Offset: 0x0001D124
		public override List<XmlReader> CreateReaders(DataSpace spaceToGet)
		{
			List<XmlReader> list = new List<XmlReader>();
			IList<MetadataArtifactLoaderFile> list2;
			if (!this.TryGetListForSpace(spaceToGet, out list2))
			{
				return list;
			}
			foreach (MetadataArtifactLoaderFile metadataArtifactLoaderFile in list2)
			{
				list.AddRange(metadataArtifactLoaderFile.CreateReaders(spaceToGet));
			}
			return list;
		}

		// Token: 0x06000BFF RID: 3071 RVA: 0x0001EF88 File Offset: 0x0001D188
		private static List<MetadataArtifactLoaderFile> GetArtifactsInDirectory(string directory, string extension, ICollection<string> uriRegistry)
		{
			List<MetadataArtifactLoaderFile> list = new List<MetadataArtifactLoaderFile>();
			foreach (string text in ((IEnumerable<string>)Directory.GetFiles(directory, MetadataArtifactLoader.wildcard + extension, SearchOption.TopDirectoryOnly)))
			{
				string text2 = global::System.IO.Path.Combine(directory, text);
				if (!uriRegistry.Contains(text2) && text.EndsWith(extension, StringComparison.OrdinalIgnoreCase))
				{
					list.Add(new MetadataArtifactLoaderFile(text2, uriRegistry));
				}
			}
			return list;
		}

		// Token: 0x040008CA RID: 2250
		private ReadOnlyCollection<MetadataArtifactLoaderFile> _csdlChildren;

		// Token: 0x040008CB RID: 2251
		private ReadOnlyCollection<MetadataArtifactLoaderFile> _ssdlChildren;

		// Token: 0x040008CC RID: 2252
		private ReadOnlyCollection<MetadataArtifactLoaderFile> _mslChildren;

		// Token: 0x040008CD RID: 2253
		private readonly string _path;

		// Token: 0x040008CE RID: 2254
		private readonly ICollection<string> _uriRegistry;
	}
}
