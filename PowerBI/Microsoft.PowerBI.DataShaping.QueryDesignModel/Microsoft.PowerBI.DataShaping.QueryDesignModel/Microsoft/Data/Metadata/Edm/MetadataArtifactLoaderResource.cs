using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.EntityModel.SchemaObjectModel;
using System.IO;
using System.Reflection;
using System.Xml;

namespace Microsoft.Data.Metadata.Edm
{
	// Token: 0x020000BC RID: 188
	internal class MetadataArtifactLoaderResource : MetadataArtifactLoader, IComparable
	{
		// Token: 0x06000C1F RID: 3103 RVA: 0x0001F670 File Offset: 0x0001D870
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

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x06000C20 RID: 3104 RVA: 0x0001F6BF File Offset: 0x0001D8BF
		public override string Path
		{
			get
			{
				return MetadataArtifactLoaderCompositeResource.CreateResPath(this._assembly, this._resourceName);
			}
		}

		// Token: 0x06000C21 RID: 3105 RVA: 0x0001F6D4 File Offset: 0x0001D8D4
		public int CompareTo(object obj)
		{
			MetadataArtifactLoaderResource metadataArtifactLoaderResource = obj as MetadataArtifactLoaderResource;
			if (metadataArtifactLoaderResource != null)
			{
				return string.Compare(this.Path, metadataArtifactLoaderResource.Path, StringComparison.OrdinalIgnoreCase);
			}
			return -1;
		}

		// Token: 0x06000C22 RID: 3106 RVA: 0x0001F6FF File Offset: 0x0001D8FF
		public override bool Equals(object obj)
		{
			return this.CompareTo(obj) == 0;
		}

		// Token: 0x06000C23 RID: 3107 RVA: 0x0001F70B File Offset: 0x0001D90B
		public override int GetHashCode()
		{
			return this.Path.GetHashCode();
		}

		// Token: 0x06000C24 RID: 3108 RVA: 0x0001F718 File Offset: 0x0001D918
		public override void CollectFilePermissionPaths(List<string> paths, DataSpace spaceToGet)
		{
		}

		// Token: 0x06000C25 RID: 3109 RVA: 0x0001F71C File Offset: 0x0001D91C
		public override List<string> GetPaths(DataSpace spaceToGet)
		{
			List<string> list = new List<string>();
			if (!this._alreadyLoaded && MetadataArtifactLoader.IsArtifactOfDataSpace(this.Path, spaceToGet))
			{
				list.Add(this.Path);
			}
			return list;
		}

		// Token: 0x06000C26 RID: 3110 RVA: 0x0001F754 File Offset: 0x0001D954
		public override List<string> GetPaths()
		{
			List<string> list = new List<string>();
			if (!this._alreadyLoaded)
			{
				list.Add(this.Path);
			}
			return list;
		}

		// Token: 0x06000C27 RID: 3111 RVA: 0x0001F77C File Offset: 0x0001D97C
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

		// Token: 0x06000C28 RID: 3112 RVA: 0x0001F7B4 File Offset: 0x0001D9B4
		private XmlReader CreateReader()
		{
			Stream stream = this.LoadResource();
			XmlReaderSettings xmlReaderSettings = Schema.CreateEdmStandardXmlReaderSettings();
			xmlReaderSettings.CloseInput = true;
			xmlReaderSettings.ConformanceLevel = ConformanceLevel.Document;
			return XmlReader.Create(stream, xmlReaderSettings);
		}

		// Token: 0x06000C29 RID: 3113 RVA: 0x0001F7E4 File Offset: 0x0001D9E4
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

		// Token: 0x06000C2A RID: 3114 RVA: 0x0001F81C File Offset: 0x0001DA1C
		private Stream LoadResource()
		{
			Stream stream;
			if (this.TryCreateResourceStream(out stream))
			{
				return stream;
			}
			throw EntityUtil.Metadata(Strings.UnableToLoadResource);
		}

		// Token: 0x06000C2B RID: 3115 RVA: 0x0001F83F File Offset: 0x0001DA3F
		private bool TryCreateResourceStream(out Stream resourceStream)
		{
			resourceStream = this._assembly.GetManifestResourceStream(this._resourceName);
			return resourceStream != null;
		}

		// Token: 0x040008D3 RID: 2259
		private readonly bool _alreadyLoaded;

		// Token: 0x040008D4 RID: 2260
		private readonly Assembly _assembly;

		// Token: 0x040008D5 RID: 2261
		private readonly string _resourceName;
	}
}
