using System;
using System.Collections.Generic;
using System.Xml;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004D6 RID: 1238
	internal class MetadataArtifactLoaderXmlReaderWrapper : MetadataArtifactLoader, IComparable
	{
		// Token: 0x06003D57 RID: 15703 RVA: 0x000CB04D File Offset: 0x000C924D
		public MetadataArtifactLoaderXmlReaderWrapper(XmlReader xmlReader)
		{
			this._reader = xmlReader;
			this._resourceUri = xmlReader.BaseURI;
		}

		// Token: 0x17000C0E RID: 3086
		// (get) Token: 0x06003D58 RID: 15704 RVA: 0x000CB068 File Offset: 0x000C9268
		public override string Path
		{
			get
			{
				if (string.IsNullOrEmpty(this._resourceUri))
				{
					return string.Empty;
				}
				return this._resourceUri;
			}
		}

		// Token: 0x06003D59 RID: 15705 RVA: 0x000CB084 File Offset: 0x000C9284
		public int CompareTo(object obj)
		{
			MetadataArtifactLoaderXmlReaderWrapper metadataArtifactLoaderXmlReaderWrapper = obj as MetadataArtifactLoaderXmlReaderWrapper;
			if (metadataArtifactLoaderXmlReaderWrapper == null)
			{
				return -1;
			}
			if (this._reader == metadataArtifactLoaderXmlReaderWrapper._reader)
			{
				return 0;
			}
			return -1;
		}

		// Token: 0x06003D5A RID: 15706 RVA: 0x000CB0AE File Offset: 0x000C92AE
		public override bool Equals(object obj)
		{
			return this.CompareTo(obj) == 0;
		}

		// Token: 0x06003D5B RID: 15707 RVA: 0x000CB0BA File Offset: 0x000C92BA
		public override int GetHashCode()
		{
			return this._reader.GetHashCode();
		}

		// Token: 0x06003D5C RID: 15708 RVA: 0x000CB0C8 File Offset: 0x000C92C8
		public override List<string> GetPaths(DataSpace spaceToGet)
		{
			List<string> list = new List<string>();
			if (MetadataArtifactLoader.IsArtifactOfDataSpace(this.Path, spaceToGet))
			{
				list.Add(this.Path);
			}
			return list;
		}

		// Token: 0x06003D5D RID: 15709 RVA: 0x000CB0F6 File Offset: 0x000C92F6
		public override List<string> GetPaths()
		{
			return new List<string>(new string[] { this.Path });
		}

		// Token: 0x06003D5E RID: 15710 RVA: 0x000CB10C File Offset: 0x000C930C
		public override List<XmlReader> GetReaders(Dictionary<MetadataArtifactLoader, XmlReader> sourceDictionary)
		{
			List<XmlReader> list = new List<XmlReader>();
			list.Add(this._reader);
			if (sourceDictionary != null)
			{
				sourceDictionary.Add(this, this._reader);
			}
			return list;
		}

		// Token: 0x06003D5F RID: 15711 RVA: 0x000CB130 File Offset: 0x000C9330
		public override List<XmlReader> CreateReaders(DataSpace spaceToGet)
		{
			List<XmlReader> list = new List<XmlReader>();
			if (MetadataArtifactLoader.IsArtifactOfDataSpace(this.Path, spaceToGet))
			{
				list.Add(this._reader);
			}
			return list;
		}

		// Token: 0x040014F2 RID: 5362
		private readonly XmlReader _reader;

		// Token: 0x040014F3 RID: 5363
		private readonly string _resourceUri;
	}
}
