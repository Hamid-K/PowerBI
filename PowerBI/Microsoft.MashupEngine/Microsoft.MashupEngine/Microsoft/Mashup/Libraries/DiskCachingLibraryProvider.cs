using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Microsoft.Mashup.Libraries
{
	// Token: 0x020020C9 RID: 8393
	public sealed class DiskCachingLibraryProvider : ILibraryProvider, IDisposable
	{
		// Token: 0x0600CD8F RID: 52623 RVA: 0x0028DB68 File Offset: 0x0028BD68
		public DiskCachingLibraryProvider(ILibraryProvider baseProvider, string cachePath, string fileVersionSuffix = null)
		{
			this.baseProvider = baseProvider;
			this.cachePath = cachePath;
			this.fileVersionSuffix = fileVersionSuffix ?? string.Empty;
			this.baseProvider.Changed += this.OnLibraryChanged;
			try
			{
				Directory.CreateDirectory(cachePath);
			}
			catch (IOException)
			{
			}
		}

		// Token: 0x17003167 RID: 12647
		// (get) Token: 0x0600CD90 RID: 52624 RVA: 0x0028DBCC File Offset: 0x0028BDCC
		public string Identifier
		{
			get
			{
				return this.baseProvider.Identifier;
			}
		}

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x0600CD91 RID: 52625 RVA: 0x0028DBDC File Offset: 0x0028BDDC
		// (remove) Token: 0x0600CD92 RID: 52626 RVA: 0x0028DC14 File Offset: 0x0028BE14
		public event EventHandler<LibraryChangedEventArgs> Changed;

		// Token: 0x0600CD93 RID: 52627 RVA: 0x0028DC49 File Offset: 0x0028BE49
		public IEnumerable<ILibrary> GetLibraries()
		{
			return (from library in this.baseProvider.GetLibraries()
				select new DiskCachingLibraryProvider.CachingLibrary(this, library)).Cast<ILibrary>();
		}

		// Token: 0x0600CD94 RID: 52628 RVA: 0x0028DC6C File Offset: 0x0028BE6C
		public bool TryGetLibrary(string identifier, out ILibrary library)
		{
			if (this.baseProvider.TryGetLibrary(identifier, out library))
			{
				library = new DiskCachingLibraryProvider.CachingLibrary(this, library);
				return true;
			}
			return false;
		}

		// Token: 0x0600CD95 RID: 52629 RVA: 0x0028DC8A File Offset: 0x0028BE8A
		public void Dispose()
		{
			this.baseProvider.Changed -= this.OnLibraryChanged;
		}

		// Token: 0x0600CD96 RID: 52630 RVA: 0x0028DCA4 File Offset: 0x0028BEA4
		private void OnLibraryChanged(object source, LibraryChangedEventArgs e)
		{
			EventHandler<LibraryChangedEventArgs> changed = this.Changed;
			if (changed != null)
			{
				changed(this, e);
			}
		}

		// Token: 0x0600CD97 RID: 52631 RVA: 0x0028DCC3 File Offset: 0x0028BEC3
		private string MakeFilename(string identifier, string version)
		{
			return Path.Combine(this.cachePath, Utilities.CreateHash(new string[] { identifier, version }) + this.fileVersionSuffix);
		}

		// Token: 0x0600CD98 RID: 52632 RVA: 0x0028DCF0 File Offset: 0x0028BEF0
		private Dictionary<string, byte[]> ReadMetadata(string identifier, string version)
		{
			Dictionary<string, byte[]> dictionary = new Dictionary<string, byte[]>(StringComparer.OrdinalIgnoreCase);
			string text = this.MakeFilename(identifier, version);
			if (File.Exists(text))
			{
				try
				{
					using (FileStream fileStream = File.OpenRead(text))
					{
						using (BinaryReader binaryReader = new BinaryReader(fileStream))
						{
							int num = binaryReader.ReadInt32();
							for (int i = 0; i < num; i++)
							{
								string text2 = binaryReader.ReadString();
								int num2 = binaryReader.ReadInt32();
								dictionary[text2] = binaryReader.ReadBytes(num2);
							}
						}
					}
				}
				catch (Exception ex)
				{
					if (!Utilities.IsSafeException(ex))
					{
						throw;
					}
				}
			}
			return dictionary;
		}

		// Token: 0x0600CD99 RID: 52633 RVA: 0x0028DDB0 File Offset: 0x0028BFB0
		private bool WriteMetadata(string identifier, string version, Dictionary<string, byte[]> metadata)
		{
			try
			{
				using (FileStream fileStream = File.Open(this.MakeFilename(identifier, version), FileMode.Create))
				{
					using (BinaryWriter binaryWriter = new BinaryWriter(fileStream))
					{
						binaryWriter.Write(metadata.Count);
						foreach (KeyValuePair<string, byte[]> keyValuePair in metadata)
						{
							binaryWriter.Write(keyValuePair.Key);
							binaryWriter.Write(keyValuePair.Value.Length);
							binaryWriter.Write(keyValuePair.Value);
						}
					}
				}
				return true;
			}
			catch (Exception ex)
			{
				if (!Utilities.IsSafeException(ex))
				{
					throw;
				}
			}
			return false;
		}

		// Token: 0x040067FE RID: 26622
		private readonly ILibraryProvider baseProvider;

		// Token: 0x040067FF RID: 26623
		private readonly string cachePath;

		// Token: 0x04006800 RID: 26624
		private readonly string fileVersionSuffix;

		// Token: 0x020020CA RID: 8394
		private sealed class CachingLibrary : ILibrary
		{
			// Token: 0x0600CD9B RID: 52635 RVA: 0x0028DE9D File Offset: 0x0028C09D
			public CachingLibrary(DiskCachingLibraryProvider cache, ILibrary baseLibrary)
			{
				this.cache = cache;
				this.baseLibrary = baseLibrary;
			}

			// Token: 0x17003168 RID: 12648
			// (get) Token: 0x0600CD9C RID: 52636 RVA: 0x0028DEB3 File Offset: 0x0028C0B3
			public ILibraryProvider Provider
			{
				get
				{
					return this.cache;
				}
			}

			// Token: 0x17003169 RID: 12649
			// (get) Token: 0x0600CD9D RID: 52637 RVA: 0x0028DEBB File Offset: 0x0028C0BB
			public string Identifier
			{
				get
				{
					return this.baseLibrary.Identifier;
				}
			}

			// Token: 0x1700316A RID: 12650
			// (get) Token: 0x0600CD9E RID: 52638 RVA: 0x0028DEC8 File Offset: 0x0028C0C8
			public string Version
			{
				get
				{
					return this.baseLibrary.Version;
				}
			}

			// Token: 0x1700316B RID: 12651
			// (get) Token: 0x0600CD9F RID: 52639 RVA: 0x0028DED5 File Offset: 0x0028C0D5
			public byte[] Contents
			{
				get
				{
					return this.baseLibrary.Contents;
				}
			}

			// Token: 0x1700316C RID: 12652
			// (get) Token: 0x0600CDA0 RID: 52640 RVA: 0x0028DEE2 File Offset: 0x0028C0E2
			public IEnumerable<KeyValuePair<string, byte[]>> Metadata
			{
				get
				{
					return this.baseLibrary.Metadata.Concat(this.cache.ReadMetadata(this.Identifier, this.Version)).Distinct(DiskCachingLibraryProvider.MetadataComparer.Instance);
				}
			}

			// Token: 0x0600CDA1 RID: 52641 RVA: 0x0028DF15 File Offset: 0x0028C115
			public bool TryGetMetadata(string metadataName, out byte[] metadata)
			{
				return this.baseLibrary.TryGetMetadata(metadataName, out metadata) || this.cache.ReadMetadata(this.Identifier, this.Version).TryGetValue(metadataName, out metadata);
			}

			// Token: 0x0600CDA2 RID: 52642 RVA: 0x0028DF48 File Offset: 0x0028C148
			public bool TrySetMetadata(string metadataName, byte[] metadata)
			{
				if (this.baseLibrary.TrySetMetadata(metadataName, metadata))
				{
					return true;
				}
				Dictionary<string, byte[]> dictionary = this.cache.ReadMetadata(this.Identifier, this.Version);
				dictionary[metadataName] = metadata;
				return this.cache.WriteMetadata(this.Identifier, this.Version, dictionary);
			}

			// Token: 0x04006802 RID: 26626
			private readonly DiskCachingLibraryProvider cache;

			// Token: 0x04006803 RID: 26627
			private readonly ILibrary baseLibrary;
		}

		// Token: 0x020020CB RID: 8395
		private sealed class MetadataComparer : IEqualityComparer<KeyValuePair<string, byte[]>>
		{
			// Token: 0x0600CDA3 RID: 52643 RVA: 0x000020FD File Offset: 0x000002FD
			private MetadataComparer()
			{
			}

			// Token: 0x0600CDA4 RID: 52644 RVA: 0x0028DF9E File Offset: 0x0028C19E
			public bool Equals(KeyValuePair<string, byte[]> x, KeyValuePair<string, byte[]> y)
			{
				return x.Key.Equals(y.Key, StringComparison.OrdinalIgnoreCase);
			}

			// Token: 0x0600CDA5 RID: 52645 RVA: 0x0028DFB4 File Offset: 0x0028C1B4
			public int GetHashCode(KeyValuePair<string, byte[]> obj)
			{
				return obj.Key.GetHashCode();
			}

			// Token: 0x04006804 RID: 26628
			public static readonly IEqualityComparer<KeyValuePair<string, byte[]>> Instance = new DiskCachingLibraryProvider.MetadataComparer();
		}
	}
}
