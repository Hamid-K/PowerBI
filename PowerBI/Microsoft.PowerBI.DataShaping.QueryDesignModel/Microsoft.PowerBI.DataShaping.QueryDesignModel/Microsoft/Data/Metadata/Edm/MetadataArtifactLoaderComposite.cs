using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml;

namespace Microsoft.Data.Metadata.Edm
{
	// Token: 0x020000B8 RID: 184
	internal class MetadataArtifactLoaderComposite : MetadataArtifactLoader, IEnumerable<MetadataArtifactLoader>, IEnumerable
	{
		// Token: 0x06000BE5 RID: 3045 RVA: 0x0001E8BE File Offset: 0x0001CABE
		public MetadataArtifactLoaderComposite(List<MetadataArtifactLoader> children)
		{
			this._children = new List<MetadataArtifactLoader>(children).AsReadOnly();
		}

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x06000BE6 RID: 3046 RVA: 0x0001E8D7 File Offset: 0x0001CAD7
		public override string Path
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x06000BE7 RID: 3047 RVA: 0x0001E8E0 File Offset: 0x0001CAE0
		public override void CollectFilePermissionPaths(List<string> paths, DataSpace spaceToGet)
		{
			foreach (MetadataArtifactLoader metadataArtifactLoader in this._children)
			{
				metadataArtifactLoader.CollectFilePermissionPaths(paths, spaceToGet);
			}
		}

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x06000BE8 RID: 3048 RVA: 0x0001E92C File Offset: 0x0001CB2C
		public override bool IsComposite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000BE9 RID: 3049 RVA: 0x0001E930 File Offset: 0x0001CB30
		public override List<string> GetOriginalPaths()
		{
			List<string> list = new List<string>();
			foreach (MetadataArtifactLoader metadataArtifactLoader in this._children)
			{
				list.AddRange(metadataArtifactLoader.GetOriginalPaths());
			}
			return list;
		}

		// Token: 0x06000BEA RID: 3050 RVA: 0x0001E98C File Offset: 0x0001CB8C
		public override List<string> GetOriginalPaths(DataSpace spaceToGet)
		{
			List<string> list = new List<string>();
			foreach (MetadataArtifactLoader metadataArtifactLoader in this._children)
			{
				list.AddRange(metadataArtifactLoader.GetOriginalPaths(spaceToGet));
			}
			return list;
		}

		// Token: 0x06000BEB RID: 3051 RVA: 0x0001E9E8 File Offset: 0x0001CBE8
		public override List<string> GetPaths(DataSpace spaceToGet)
		{
			List<string> list = new List<string>();
			foreach (MetadataArtifactLoader metadataArtifactLoader in this._children)
			{
				list.AddRange(metadataArtifactLoader.GetPaths(spaceToGet));
			}
			return list;
		}

		// Token: 0x06000BEC RID: 3052 RVA: 0x0001EA44 File Offset: 0x0001CC44
		public override List<string> GetPaths()
		{
			List<string> list = new List<string>();
			foreach (MetadataArtifactLoader metadataArtifactLoader in this._children)
			{
				list.AddRange(metadataArtifactLoader.GetPaths());
			}
			return list;
		}

		// Token: 0x06000BED RID: 3053 RVA: 0x0001EAA0 File Offset: 0x0001CCA0
		public override List<XmlReader> GetReaders(Dictionary<MetadataArtifactLoader, XmlReader> sourceDictionary)
		{
			List<XmlReader> list = new List<XmlReader>();
			foreach (MetadataArtifactLoader metadataArtifactLoader in this._children)
			{
				list.AddRange(metadataArtifactLoader.GetReaders(sourceDictionary));
			}
			return list;
		}

		// Token: 0x06000BEE RID: 3054 RVA: 0x0001EAFC File Offset: 0x0001CCFC
		public override List<XmlReader> CreateReaders(DataSpace spaceToGet)
		{
			List<XmlReader> list = new List<XmlReader>();
			foreach (MetadataArtifactLoader metadataArtifactLoader in this._children)
			{
				list.AddRange(metadataArtifactLoader.CreateReaders(spaceToGet));
			}
			return list;
		}

		// Token: 0x06000BEF RID: 3055 RVA: 0x0001EB58 File Offset: 0x0001CD58
		public IEnumerator<MetadataArtifactLoader> GetEnumerator()
		{
			return this._children.GetEnumerator();
		}

		// Token: 0x06000BF0 RID: 3056 RVA: 0x0001EB65 File Offset: 0x0001CD65
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this._children.GetEnumerator();
		}

		// Token: 0x040008C9 RID: 2249
		private readonly ReadOnlyCollection<MetadataArtifactLoader> _children;
	}
}
