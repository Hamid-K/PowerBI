using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004D1 RID: 1233
	internal class MetadataArtifactLoaderComposite : MetadataArtifactLoader, IEnumerable<MetadataArtifactLoader>, IEnumerable
	{
		// Token: 0x06003D15 RID: 15637 RVA: 0x000CA1EA File Offset: 0x000C83EA
		public MetadataArtifactLoaderComposite(List<MetadataArtifactLoader> children)
		{
			this._children = new ReadOnlyCollection<MetadataArtifactLoader>(new List<MetadataArtifactLoader>(children));
		}

		// Token: 0x17000C03 RID: 3075
		// (get) Token: 0x06003D16 RID: 15638 RVA: 0x000CA203 File Offset: 0x000C8403
		public override string Path
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17000C04 RID: 3076
		// (get) Token: 0x06003D17 RID: 15639 RVA: 0x000CA20A File Offset: 0x000C840A
		public override bool IsComposite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003D18 RID: 15640 RVA: 0x000CA210 File Offset: 0x000C8410
		public override List<string> GetOriginalPaths()
		{
			List<string> list = new List<string>();
			foreach (MetadataArtifactLoader metadataArtifactLoader in this._children)
			{
				list.AddRange(metadataArtifactLoader.GetOriginalPaths());
			}
			return list;
		}

		// Token: 0x06003D19 RID: 15641 RVA: 0x000CA26C File Offset: 0x000C846C
		public override List<string> GetOriginalPaths(DataSpace spaceToGet)
		{
			List<string> list = new List<string>();
			foreach (MetadataArtifactLoader metadataArtifactLoader in this._children)
			{
				list.AddRange(metadataArtifactLoader.GetOriginalPaths(spaceToGet));
			}
			return list;
		}

		// Token: 0x06003D1A RID: 15642 RVA: 0x000CA2C8 File Offset: 0x000C84C8
		public override List<string> GetPaths(DataSpace spaceToGet)
		{
			List<string> list = new List<string>();
			foreach (MetadataArtifactLoader metadataArtifactLoader in this._children)
			{
				list.AddRange(metadataArtifactLoader.GetPaths(spaceToGet));
			}
			return list;
		}

		// Token: 0x06003D1B RID: 15643 RVA: 0x000CA324 File Offset: 0x000C8524
		public override List<string> GetPaths()
		{
			List<string> list = new List<string>();
			foreach (MetadataArtifactLoader metadataArtifactLoader in this._children)
			{
				list.AddRange(metadataArtifactLoader.GetPaths());
			}
			return list;
		}

		// Token: 0x06003D1C RID: 15644 RVA: 0x000CA380 File Offset: 0x000C8580
		public override List<XmlReader> GetReaders(Dictionary<MetadataArtifactLoader, XmlReader> sourceDictionary)
		{
			List<XmlReader> list = new List<XmlReader>();
			foreach (MetadataArtifactLoader metadataArtifactLoader in this._children)
			{
				list.AddRange(metadataArtifactLoader.GetReaders(sourceDictionary));
			}
			return list;
		}

		// Token: 0x06003D1D RID: 15645 RVA: 0x000CA3DC File Offset: 0x000C85DC
		public override List<XmlReader> CreateReaders(DataSpace spaceToGet)
		{
			List<XmlReader> list = new List<XmlReader>();
			foreach (MetadataArtifactLoader metadataArtifactLoader in this._children)
			{
				list.AddRange(metadataArtifactLoader.CreateReaders(spaceToGet));
			}
			return list;
		}

		// Token: 0x06003D1E RID: 15646 RVA: 0x000CA438 File Offset: 0x000C8638
		public IEnumerator<MetadataArtifactLoader> GetEnumerator()
		{
			return this._children.GetEnumerator();
		}

		// Token: 0x06003D1F RID: 15647 RVA: 0x000CA445 File Offset: 0x000C8645
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this._children.GetEnumerator();
		}

		// Token: 0x040014E5 RID: 5349
		private readonly ReadOnlyCollection<MetadataArtifactLoader> _children;
	}
}
