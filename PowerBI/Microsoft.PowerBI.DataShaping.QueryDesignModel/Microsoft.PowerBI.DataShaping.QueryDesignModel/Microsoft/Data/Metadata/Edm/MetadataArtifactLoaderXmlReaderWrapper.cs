using System;
using System.Collections.Generic;
using System.Xml;

namespace Microsoft.Data.Metadata.Edm
{
	// Token: 0x020000BD RID: 189
	internal class MetadataArtifactLoaderXmlReaderWrapper : MetadataArtifactLoader, IComparable
	{
		// Token: 0x06000C2C RID: 3116 RVA: 0x0001F859 File Offset: 0x0001DA59
		public MetadataArtifactLoaderXmlReaderWrapper(XmlReader xmlReader)
		{
			this._reader = xmlReader;
			this._resourceUri = xmlReader.BaseURI;
		}

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x06000C2D RID: 3117 RVA: 0x0001F874 File Offset: 0x0001DA74
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

		// Token: 0x06000C2E RID: 3118 RVA: 0x0001F890 File Offset: 0x0001DA90
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

		// Token: 0x06000C2F RID: 3119 RVA: 0x0001F8BA File Offset: 0x0001DABA
		public override bool Equals(object obj)
		{
			return this.CompareTo(obj) == 0;
		}

		// Token: 0x06000C30 RID: 3120 RVA: 0x0001F8C6 File Offset: 0x0001DAC6
		public override int GetHashCode()
		{
			return this._reader.GetHashCode();
		}

		// Token: 0x06000C31 RID: 3121 RVA: 0x0001F8D3 File Offset: 0x0001DAD3
		public override void CollectFilePermissionPaths(List<string> paths, DataSpace spaceToGet)
		{
		}

		// Token: 0x06000C32 RID: 3122 RVA: 0x0001F8D8 File Offset: 0x0001DAD8
		public override List<string> GetPaths(DataSpace spaceToGet)
		{
			List<string> list = new List<string>();
			if (MetadataArtifactLoader.IsArtifactOfDataSpace(this.Path, spaceToGet))
			{
				list.Add(this.Path);
			}
			return list;
		}

		// Token: 0x06000C33 RID: 3123 RVA: 0x0001F906 File Offset: 0x0001DB06
		public override List<string> GetPaths()
		{
			return new List<string>(new string[] { this.Path });
		}

		// Token: 0x06000C34 RID: 3124 RVA: 0x0001F91C File Offset: 0x0001DB1C
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

		// Token: 0x06000C35 RID: 3125 RVA: 0x0001F940 File Offset: 0x0001DB40
		public override List<XmlReader> CreateReaders(DataSpace spaceToGet)
		{
			List<XmlReader> list = new List<XmlReader>();
			if (MetadataArtifactLoader.IsArtifactOfDataSpace(this.Path, spaceToGet))
			{
				list.Add(this._reader);
			}
			return list;
		}

		// Token: 0x040008D6 RID: 2262
		private readonly XmlReader _reader;

		// Token: 0x040008D7 RID: 2263
		private readonly string _resourceUri;
	}
}
