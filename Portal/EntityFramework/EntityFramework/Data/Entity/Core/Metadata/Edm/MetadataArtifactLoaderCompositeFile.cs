using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading;
using System.Xml;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004D2 RID: 1234
	internal class MetadataArtifactLoaderCompositeFile : MetadataArtifactLoader
	{
		// Token: 0x06003D20 RID: 15648 RVA: 0x000CA452 File Offset: 0x000C8652
		public MetadataArtifactLoaderCompositeFile(string path, ICollection<string> uriRegistry)
		{
			this._path = path;
			this._uriRegistry = uriRegistry;
		}

		// Token: 0x17000C05 RID: 3077
		// (get) Token: 0x06003D21 RID: 15649 RVA: 0x000CA468 File Offset: 0x000C8668
		public override string Path
		{
			get
			{
				return this._path;
			}
		}

		// Token: 0x17000C06 RID: 3078
		// (get) Token: 0x06003D22 RID: 15650 RVA: 0x000CA470 File Offset: 0x000C8670
		public override bool IsComposite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000C07 RID: 3079
		// (get) Token: 0x06003D23 RID: 15651 RVA: 0x000CA473 File Offset: 0x000C8673
		internal ReadOnlyCollection<MetadataArtifactLoaderFile> CsdlChildren
		{
			get
			{
				this.LoadCollections();
				return this._csdlChildren;
			}
		}

		// Token: 0x17000C08 RID: 3080
		// (get) Token: 0x06003D24 RID: 15652 RVA: 0x000CA481 File Offset: 0x000C8681
		internal ReadOnlyCollection<MetadataArtifactLoaderFile> SsdlChildren
		{
			get
			{
				this.LoadCollections();
				return this._ssdlChildren;
			}
		}

		// Token: 0x17000C09 RID: 3081
		// (get) Token: 0x06003D25 RID: 15653 RVA: 0x000CA48F File Offset: 0x000C868F
		internal ReadOnlyCollection<MetadataArtifactLoaderFile> MslChildren
		{
			get
			{
				this.LoadCollections();
				return this._mslChildren;
			}
		}

		// Token: 0x06003D26 RID: 15654 RVA: 0x000CA4A0 File Offset: 0x000C86A0
		private void LoadCollections()
		{
			if (this._csdlChildren == null)
			{
				ReadOnlyCollection<MetadataArtifactLoaderFile> readOnlyCollection = new ReadOnlyCollection<MetadataArtifactLoaderFile>(MetadataArtifactLoaderCompositeFile.GetArtifactsInDirectory(this._path, ".csdl", this._uriRegistry));
				Interlocked.CompareExchange<ReadOnlyCollection<MetadataArtifactLoaderFile>>(ref this._csdlChildren, readOnlyCollection, null);
			}
			if (this._ssdlChildren == null)
			{
				ReadOnlyCollection<MetadataArtifactLoaderFile> readOnlyCollection2 = new ReadOnlyCollection<MetadataArtifactLoaderFile>(MetadataArtifactLoaderCompositeFile.GetArtifactsInDirectory(this._path, ".ssdl", this._uriRegistry));
				Interlocked.CompareExchange<ReadOnlyCollection<MetadataArtifactLoaderFile>>(ref this._ssdlChildren, readOnlyCollection2, null);
			}
			if (this._mslChildren == null)
			{
				ReadOnlyCollection<MetadataArtifactLoaderFile> readOnlyCollection3 = new ReadOnlyCollection<MetadataArtifactLoaderFile>(MetadataArtifactLoaderCompositeFile.GetArtifactsInDirectory(this._path, ".msl", this._uriRegistry));
				Interlocked.CompareExchange<ReadOnlyCollection<MetadataArtifactLoaderFile>>(ref this._mslChildren, readOnlyCollection3, null);
			}
		}

		// Token: 0x06003D27 RID: 15655 RVA: 0x000CA543 File Offset: 0x000C8743
		public override List<string> GetOriginalPaths(DataSpace spaceToGet)
		{
			return this.GetOriginalPaths();
		}

		// Token: 0x06003D28 RID: 15656 RVA: 0x000CA54C File Offset: 0x000C874C
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

		// Token: 0x06003D29 RID: 15657 RVA: 0x000CA5B0 File Offset: 0x000C87B0
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

		// Token: 0x06003D2A RID: 15658 RVA: 0x000CA5F0 File Offset: 0x000C87F0
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

		// Token: 0x06003D2B RID: 15659 RVA: 0x000CA6D0 File Offset: 0x000C88D0
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

		// Token: 0x06003D2C RID: 15660 RVA: 0x000CA7B0 File Offset: 0x000C89B0
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

		// Token: 0x06003D2D RID: 15661 RVA: 0x000CA814 File Offset: 0x000C8A14
		private static List<MetadataArtifactLoaderFile> GetArtifactsInDirectory(string directory, string extension, ICollection<string> uriRegistry)
		{
			List<MetadataArtifactLoaderFile> list = new List<MetadataArtifactLoaderFile>();
			foreach (string text in Directory.GetFiles(directory, MetadataArtifactLoader.wildcard + extension, SearchOption.TopDirectoryOnly))
			{
				string text2 = global::System.IO.Path.Combine(directory, text);
				if (!uriRegistry.Contains(text2) && text.EndsWith(extension, StringComparison.OrdinalIgnoreCase))
				{
					list.Add(new MetadataArtifactLoaderFile(text2, uriRegistry));
				}
			}
			return list;
		}

		// Token: 0x040014E6 RID: 5350
		private ReadOnlyCollection<MetadataArtifactLoaderFile> _csdlChildren;

		// Token: 0x040014E7 RID: 5351
		private ReadOnlyCollection<MetadataArtifactLoaderFile> _ssdlChildren;

		// Token: 0x040014E8 RID: 5352
		private ReadOnlyCollection<MetadataArtifactLoaderFile> _mslChildren;

		// Token: 0x040014E9 RID: 5353
		private readonly string _path;

		// Token: 0x040014EA RID: 5354
		private readonly ICollection<string> _uriRegistry;
	}
}
