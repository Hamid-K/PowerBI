using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Microsoft.Owin.FileSystems
{
	// Token: 0x02000002 RID: 2
	public class EmbeddedResourceFileSystem : IFileSystem
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public EmbeddedResourceFileSystem()
			: this(Assembly.GetCallingAssembly())
		{
		}

		// Token: 0x06000002 RID: 2 RVA: 0x0000205D File Offset: 0x0000025D
		public EmbeddedResourceFileSystem(Assembly assembly)
			: this(assembly, string.Empty)
		{
		}

		// Token: 0x06000003 RID: 3 RVA: 0x0000206B File Offset: 0x0000026B
		public EmbeddedResourceFileSystem(string baseNamespace)
			: this(Assembly.GetCallingAssembly(), baseNamespace)
		{
		}

		// Token: 0x06000004 RID: 4 RVA: 0x0000207C File Offset: 0x0000027C
		public EmbeddedResourceFileSystem(Assembly assembly, string baseNamespace)
		{
			if (assembly == null)
			{
				throw new ArgumentNullException("assembly");
			}
			this._baseNamespace = (string.IsNullOrEmpty(baseNamespace) ? string.Empty : (baseNamespace + "."));
			this._assembly = assembly;
			this._lastModified = new FileInfo(assembly.Location).LastWriteTime;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020E0 File Offset: 0x000002E0
		public bool TryGetFileInfo(string subpath, out IFileInfo fileInfo)
		{
			if (string.IsNullOrEmpty(subpath) || subpath[0] != '/')
			{
				fileInfo = null;
				return false;
			}
			string fileName = subpath.Substring(1);
			string resourcePath = this._baseNamespace + fileName.Replace('/', '.');
			if (this._assembly.GetManifestResourceInfo(resourcePath) == null)
			{
				fileInfo = null;
				return false;
			}
			fileInfo = new EmbeddedResourceFileSystem.EmbeddedResourceFileInfo(this._assembly, resourcePath, fileName, this._lastModified);
			return true;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x0000214C File Offset: 0x0000034C
		public bool TryGetDirectoryContents(string subpath, out IEnumerable<IFileInfo> contents)
		{
			if (!subpath.Equals("/"))
			{
				contents = null;
				return false;
			}
			IList<IFileInfo> entries = new List<IFileInfo>();
			foreach (string resourceName in this._assembly.GetManifestResourceNames())
			{
				if (resourceName.StartsWith(this._baseNamespace))
				{
					entries.Add(new EmbeddedResourceFileSystem.EmbeddedResourceFileInfo(this._assembly, resourceName, resourceName.Substring(this._baseNamespace.Length), this._lastModified));
				}
			}
			contents = entries;
			return true;
		}

		// Token: 0x04000001 RID: 1
		private readonly Assembly _assembly;

		// Token: 0x04000002 RID: 2
		private readonly string _baseNamespace;

		// Token: 0x04000003 RID: 3
		private readonly DateTime _lastModified;

		// Token: 0x02000006 RID: 6
		private class EmbeddedResourceFileInfo : IFileInfo
		{
			// Token: 0x06000018 RID: 24 RVA: 0x000025A4 File Offset: 0x000007A4
			public EmbeddedResourceFileInfo(Assembly assembly, string resourcePath, string fileName, DateTime lastModified)
			{
				this._assembly = assembly;
				this._lastModified = lastModified;
				this._resourcePath = resourcePath;
				this._fileName = fileName;
			}

			// Token: 0x17000007 RID: 7
			// (get) Token: 0x06000019 RID: 25 RVA: 0x000025CC File Offset: 0x000007CC
			public long Length
			{
				get
				{
					if (this._length == null)
					{
						using (Stream stream = this._assembly.GetManifestResourceStream(this._resourcePath))
						{
							this._length = new long?(stream.Length);
						}
					}
					return this._length.Value;
				}
			}

			// Token: 0x17000008 RID: 8
			// (get) Token: 0x0600001A RID: 26 RVA: 0x00002630 File Offset: 0x00000830
			public string PhysicalPath
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17000009 RID: 9
			// (get) Token: 0x0600001B RID: 27 RVA: 0x00002633 File Offset: 0x00000833
			public string Name
			{
				get
				{
					return this._fileName;
				}
			}

			// Token: 0x1700000A RID: 10
			// (get) Token: 0x0600001C RID: 28 RVA: 0x0000263B File Offset: 0x0000083B
			public DateTime LastModified
			{
				get
				{
					return this._lastModified;
				}
			}

			// Token: 0x1700000B RID: 11
			// (get) Token: 0x0600001D RID: 29 RVA: 0x00002643 File Offset: 0x00000843
			public bool IsDirectory
			{
				get
				{
					return false;
				}
			}

			// Token: 0x0600001E RID: 30 RVA: 0x00002648 File Offset: 0x00000848
			public Stream CreateReadStream()
			{
				Stream stream = this._assembly.GetManifestResourceStream(this._resourcePath);
				if (this._length == null)
				{
					this._length = new long?(stream.Length);
				}
				return stream;
			}

			// Token: 0x04000006 RID: 6
			private readonly Assembly _assembly;

			// Token: 0x04000007 RID: 7
			private readonly DateTime _lastModified;

			// Token: 0x04000008 RID: 8
			private readonly string _resourcePath;

			// Token: 0x04000009 RID: 9
			private readonly string _fileName;

			// Token: 0x0400000A RID: 10
			private long? _length;
		}
	}
}
