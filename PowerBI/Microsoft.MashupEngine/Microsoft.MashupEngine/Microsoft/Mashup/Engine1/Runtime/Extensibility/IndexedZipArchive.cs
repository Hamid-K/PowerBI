using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace Microsoft.Mashup.Engine1.Runtime.Extensibility
{
	// Token: 0x0200170F RID: 5903
	public class IndexedZipArchive : IDisposable
	{
		// Token: 0x06009605 RID: 38405 RVA: 0x001F1370 File Offset: 0x001EF570
		public IndexedZipArchive(Stream stream)
		{
			this.stream = stream;
			this.zip = new ZipArchive(this.stream);
			this.zipEntries = new Dictionary<string, ZipArchiveEntry>();
			foreach (ZipArchiveEntry zipArchiveEntry in this.zip.Entries)
			{
				if (!(zipArchiveEntry.Name == string.Empty))
				{
					this.zipEntries.Add(zipArchiveEntry.FullName, zipArchiveEntry);
				}
			}
		}

		// Token: 0x1700273C RID: 10044
		// (get) Token: 0x06009606 RID: 38406 RVA: 0x001F1408 File Offset: 0x001EF608
		public IEnumerable<string> Filenames
		{
			get
			{
				return this.zipEntries.Keys;
			}
		}

		// Token: 0x06009607 RID: 38407 RVA: 0x001F1415 File Offset: 0x001EF615
		public bool ContainsFile(string filename)
		{
			return this.zipEntries.ContainsKey(filename);
		}

		// Token: 0x06009608 RID: 38408 RVA: 0x001F1423 File Offset: 0x001EF623
		public Stream OpenFile(string filename)
		{
			return this.zipEntries[filename].Open();
		}

		// Token: 0x06009609 RID: 38409 RVA: 0x001F1436 File Offset: 0x001EF636
		public ZipArchiveEntry GetEntry(string filename)
		{
			return this.zipEntries[filename];
		}

		// Token: 0x0600960A RID: 38410 RVA: 0x001F1444 File Offset: 0x001EF644
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600960B RID: 38411 RVA: 0x001F1453 File Offset: 0x001EF653
		protected virtual void Dispose(bool disposing)
		{
			if (this.disposed)
			{
				return;
			}
			if (disposing)
			{
				this.zip.Dispose();
				this.stream.Dispose();
			}
			this.disposed = true;
		}

		// Token: 0x04004FC5 RID: 20421
		private readonly Stream stream;

		// Token: 0x04004FC6 RID: 20422
		private readonly ZipArchive zip;

		// Token: 0x04004FC7 RID: 20423
		private readonly Dictionary<string, ZipArchiveEntry> zipEntries;

		// Token: 0x04004FC8 RID: 20424
		private bool disposed;
	}
}
