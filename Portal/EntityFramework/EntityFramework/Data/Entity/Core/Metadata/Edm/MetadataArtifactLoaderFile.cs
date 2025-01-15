using System;
using System.Collections.Generic;
using System.Data.Entity.Core.SchemaObjectModel;
using System.Xml;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004D4 RID: 1236
	internal class MetadataArtifactLoaderFile : MetadataArtifactLoader, IComparable
	{
		// Token: 0x06003D41 RID: 15681 RVA: 0x000CACEC File Offset: 0x000C8EEC
		public MetadataArtifactLoaderFile(string path, ICollection<string> uriRegistry)
		{
			this._path = path;
			this._alreadyLoaded = uriRegistry.Contains(this._path);
			if (!this._alreadyLoaded)
			{
				uriRegistry.Add(this._path);
			}
		}

		// Token: 0x17000C0C RID: 3084
		// (get) Token: 0x06003D42 RID: 15682 RVA: 0x000CAD21 File Offset: 0x000C8F21
		public override string Path
		{
			get
			{
				return this._path;
			}
		}

		// Token: 0x06003D43 RID: 15683 RVA: 0x000CAD2C File Offset: 0x000C8F2C
		public int CompareTo(object obj)
		{
			MetadataArtifactLoaderFile metadataArtifactLoaderFile = obj as MetadataArtifactLoaderFile;
			if (metadataArtifactLoaderFile != null)
			{
				return string.Compare(this._path, metadataArtifactLoaderFile._path, StringComparison.OrdinalIgnoreCase);
			}
			return -1;
		}

		// Token: 0x06003D44 RID: 15684 RVA: 0x000CAD57 File Offset: 0x000C8F57
		public override bool Equals(object obj)
		{
			return this.CompareTo(obj) == 0;
		}

		// Token: 0x06003D45 RID: 15685 RVA: 0x000CAD63 File Offset: 0x000C8F63
		public override int GetHashCode()
		{
			return this._path.GetHashCode();
		}

		// Token: 0x06003D46 RID: 15686 RVA: 0x000CAD70 File Offset: 0x000C8F70
		public override List<string> GetPaths(DataSpace spaceToGet)
		{
			List<string> list = new List<string>();
			if (!this._alreadyLoaded && MetadataArtifactLoader.IsArtifactOfDataSpace(this._path, spaceToGet))
			{
				list.Add(this._path);
			}
			return list;
		}

		// Token: 0x06003D47 RID: 15687 RVA: 0x000CADA8 File Offset: 0x000C8FA8
		public override List<string> GetPaths()
		{
			List<string> list = new List<string>();
			if (!this._alreadyLoaded)
			{
				list.Add(this._path);
			}
			return list;
		}

		// Token: 0x06003D48 RID: 15688 RVA: 0x000CADD0 File Offset: 0x000C8FD0
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

		// Token: 0x06003D49 RID: 15689 RVA: 0x000CAE08 File Offset: 0x000C9008
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

		// Token: 0x06003D4A RID: 15690 RVA: 0x000CAE40 File Offset: 0x000C9040
		private XmlReader CreateXmlReader()
		{
			XmlReaderSettings xmlReaderSettings = Schema.CreateEdmStandardXmlReaderSettings();
			xmlReaderSettings.ConformanceLevel = ConformanceLevel.Document;
			return XmlReader.Create(this._path, xmlReaderSettings);
		}

		// Token: 0x040014ED RID: 5357
		private readonly bool _alreadyLoaded;

		// Token: 0x040014EE RID: 5358
		private readonly string _path;
	}
}
