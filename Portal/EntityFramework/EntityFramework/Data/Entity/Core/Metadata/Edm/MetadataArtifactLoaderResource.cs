using System;
using System.Collections.Generic;
using System.Data.Entity.Core.SchemaObjectModel;
using System.Data.Entity.Resources;
using System.IO;
using System.Reflection;
using System.Xml;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004D5 RID: 1237
	internal class MetadataArtifactLoaderResource : MetadataArtifactLoader, IComparable
	{
		// Token: 0x06003D4B RID: 15691 RVA: 0x000CAE68 File Offset: 0x000C9068
		internal MetadataArtifactLoaderResource(Assembly assembly, string resourceName, ICollection<string> uriRegistry)
		{
			this._assembly = assembly;
			this._resourceName = resourceName;
			string text = MetadataArtifactLoaderCompositeResource.CreateResPath(this._assembly, this._resourceName);
			this._alreadyLoaded = uriRegistry.Contains(text);
			if (!this._alreadyLoaded)
			{
				uriRegistry.Add(text);
			}
		}

		// Token: 0x17000C0D RID: 3085
		// (get) Token: 0x06003D4C RID: 15692 RVA: 0x000CAEB7 File Offset: 0x000C90B7
		public override string Path
		{
			get
			{
				return MetadataArtifactLoaderCompositeResource.CreateResPath(this._assembly, this._resourceName);
			}
		}

		// Token: 0x06003D4D RID: 15693 RVA: 0x000CAECC File Offset: 0x000C90CC
		public int CompareTo(object obj)
		{
			MetadataArtifactLoaderResource metadataArtifactLoaderResource = obj as MetadataArtifactLoaderResource;
			if (metadataArtifactLoaderResource != null)
			{
				return string.Compare(this.Path, metadataArtifactLoaderResource.Path, StringComparison.OrdinalIgnoreCase);
			}
			return -1;
		}

		// Token: 0x06003D4E RID: 15694 RVA: 0x000CAEF7 File Offset: 0x000C90F7
		public override bool Equals(object obj)
		{
			return this.CompareTo(obj) == 0;
		}

		// Token: 0x06003D4F RID: 15695 RVA: 0x000CAF03 File Offset: 0x000C9103
		public override int GetHashCode()
		{
			return this.Path.GetHashCode();
		}

		// Token: 0x06003D50 RID: 15696 RVA: 0x000CAF10 File Offset: 0x000C9110
		public override List<string> GetPaths(DataSpace spaceToGet)
		{
			List<string> list = new List<string>();
			if (!this._alreadyLoaded && MetadataArtifactLoader.IsArtifactOfDataSpace(this.Path, spaceToGet))
			{
				list.Add(this.Path);
			}
			return list;
		}

		// Token: 0x06003D51 RID: 15697 RVA: 0x000CAF48 File Offset: 0x000C9148
		public override List<string> GetPaths()
		{
			List<string> list = new List<string>();
			if (!this._alreadyLoaded)
			{
				list.Add(this.Path);
			}
			return list;
		}

		// Token: 0x06003D52 RID: 15698 RVA: 0x000CAF70 File Offset: 0x000C9170
		public override List<XmlReader> GetReaders(Dictionary<MetadataArtifactLoader, XmlReader> sourceDictionary)
		{
			List<XmlReader> list = new List<XmlReader>();
			if (!this._alreadyLoaded)
			{
				XmlReader xmlReader = this.CreateReader();
				list.Add(xmlReader);
				if (sourceDictionary != null)
				{
					sourceDictionary.Add(this, xmlReader);
				}
			}
			return list;
		}

		// Token: 0x06003D53 RID: 15699 RVA: 0x000CAFA8 File Offset: 0x000C91A8
		private XmlReader CreateReader()
		{
			Stream stream = this.LoadResource();
			XmlReaderSettings xmlReaderSettings = Schema.CreateEdmStandardXmlReaderSettings();
			xmlReaderSettings.CloseInput = true;
			xmlReaderSettings.ConformanceLevel = ConformanceLevel.Document;
			return XmlReader.Create(stream, xmlReaderSettings);
		}

		// Token: 0x06003D54 RID: 15700 RVA: 0x000CAFD8 File Offset: 0x000C91D8
		public override List<XmlReader> CreateReaders(DataSpace spaceToGet)
		{
			List<XmlReader> list = new List<XmlReader>();
			if (!this._alreadyLoaded && MetadataArtifactLoader.IsArtifactOfDataSpace(this.Path, spaceToGet))
			{
				XmlReader xmlReader = this.CreateReader();
				list.Add(xmlReader);
			}
			return list;
		}

		// Token: 0x06003D55 RID: 15701 RVA: 0x000CB010 File Offset: 0x000C9210
		private Stream LoadResource()
		{
			Stream stream;
			if (this.TryCreateResourceStream(out stream))
			{
				return stream;
			}
			throw new MetadataException(Strings.UnableToLoadResource);
		}

		// Token: 0x06003D56 RID: 15702 RVA: 0x000CB033 File Offset: 0x000C9233
		private bool TryCreateResourceStream(out Stream resourceStream)
		{
			resourceStream = this._assembly.GetManifestResourceStream(this._resourceName);
			return resourceStream != null;
		}

		// Token: 0x040014EF RID: 5359
		private readonly bool _alreadyLoaded;

		// Token: 0x040014F0 RID: 5360
		private readonly Assembly _assembly;

		// Token: 0x040014F1 RID: 5361
		private readonly string _resourceName;
	}
}
