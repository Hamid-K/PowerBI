using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using Microsoft.MachineLearning.Internal.Utilities;

namespace Microsoft.MachineLearning.Model
{
	// Token: 0x0200047A RID: 1146
	public sealed class RepositoryReader : Repository
	{
		// Token: 0x060017FF RID: 6143 RVA: 0x00089A4B File Offset: 0x00087C4B
		public static RepositoryReader Open(Stream stream, bool useFileSystem = true)
		{
			return new RepositoryReader(stream, useFileSystem);
		}

		// Token: 0x06001800 RID: 6144 RVA: 0x00089A54 File Offset: 0x00087C54
		private RepositoryReader(Stream stream, bool useFileSystem)
			: base(useFileSystem)
		{
			this._archive = new ZipArchive(stream, ZipArchiveMode.Read, true);
			this._entries = new Dictionary<string, ZipArchiveEntry>();
			foreach (ZipArchiveEntry zipArchiveEntry in this._archive.Entries)
			{
				string text = base.Normalize(zipArchiveEntry.FullName);
				this._entries[text] = zipArchiveEntry;
			}
			this._pathMap = new Dictionary<string, string>();
		}

		// Token: 0x06001801 RID: 6145 RVA: 0x00089AE4 File Offset: 0x00087CE4
		public Repository.Entry OpenEntry(string name)
		{
			return this.OpenEntry(null, name);
		}

		// Token: 0x06001802 RID: 6146 RVA: 0x00089AF0 File Offset: 0x00087CF0
		public Repository.Entry OpenEntry(string dir, string name)
		{
			Repository.Entry entry = this.OpenEntryOrNull(dir, name);
			if (entry != null)
			{
				return entry;
			}
			string text;
			string text2;
			base.GetPath(out text, out text2, dir, name, false);
			throw Contracts.Except("Repository doesn't contain entry {0}", new object[] { text });
		}

		// Token: 0x06001803 RID: 6147 RVA: 0x00089B2E File Offset: 0x00087D2E
		public Repository.Entry OpenEntryOrNull(string name)
		{
			return this.OpenEntryOrNull(null, name);
		}

		// Token: 0x06001804 RID: 6148 RVA: 0x00089B38 File Offset: 0x00087D38
		public Repository.Entry OpenEntryOrNull(string dir, string name)
		{
			Contracts.Check(!base.Disposed);
			string text;
			string text2;
			base.GetPath(out text, out text2, dir, name, false);
			string text3 = text.ToLowerInvariant();
			string text4;
			Stream stream;
			if (this._pathMap.TryGetValue(text3, out text4))
			{
				stream = new FileStream(text4, FileMode.Open, FileAccess.Read);
			}
			else
			{
				ZipArchiveEntry zipArchiveEntry;
				if (!this._entries.TryGetValue(text, out zipArchiveEntry))
				{
					return null;
				}
				if (text2 != null)
				{
					Directory.CreateDirectory(Path.GetDirectoryName(text2));
					zipArchiveEntry.ExtractToFile(text2);
					this._pathMap.Add(text3, text2);
					stream = new FileStream(text2, FileMode.Open, FileAccess.Read);
				}
				else
				{
					Contracts.CheckDecode(zipArchiveEntry.Length < 2147483647L, "Repository stream too large to read into memory");
					stream = new MemoryStream((int)zipArchiveEntry.Length);
					using (Stream stream2 = zipArchiveEntry.Open())
					{
						stream2.CopyTo(stream);
					}
					stream.Position = 0L;
				}
			}
			return base.AddEntry(text, stream);
		}

		// Token: 0x06001805 RID: 6149 RVA: 0x00089C2C File Offset: 0x00087E2C
		protected override void OnDispose(Repository.Entry ent)
		{
			base.RemoveEntry(ent);
			Utils.CloseEx(ent.Stream);
		}

		// Token: 0x04000E76 RID: 3702
		private ZipArchive _archive;

		// Token: 0x04000E77 RID: 3703
		private Dictionary<string, ZipArchiveEntry> _entries;

		// Token: 0x04000E78 RID: 3704
		private Dictionary<string, string> _pathMap;
	}
}
