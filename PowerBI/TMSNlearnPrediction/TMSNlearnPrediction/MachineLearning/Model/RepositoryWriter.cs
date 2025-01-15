using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using Microsoft.MachineLearning.Internal.Utilities;

namespace Microsoft.MachineLearning.Model
{
	// Token: 0x02000479 RID: 1145
	public sealed class RepositoryWriter : Repository
	{
		// Token: 0x060017F7 RID: 6135 RVA: 0x000897FD File Offset: 0x000879FD
		public static RepositoryWriter CreateNew(Stream stream, bool useFileSystem = true)
		{
			return new RepositoryWriter(stream, useFileSystem);
		}

		// Token: 0x060017F8 RID: 6136 RVA: 0x00089806 File Offset: 0x00087A06
		private RepositoryWriter(Stream stream, bool useFileSystem = true)
			: base(useFileSystem)
		{
			this._archive = new ZipArchive(stream, ZipArchiveMode.Create, true);
			this._closed = new Queue<KeyValuePair<string, Stream>>();
			this._paths = new HashSet<string>();
		}

		// Token: 0x060017F9 RID: 6137 RVA: 0x00089833 File Offset: 0x00087A33
		public Repository.Entry CreateEntry(string name)
		{
			return this.CreateEntry(null, name);
		}

		// Token: 0x060017FA RID: 6138 RVA: 0x00089840 File Offset: 0x00087A40
		public Repository.Entry CreateEntry(string dir, string name)
		{
			Contracts.Check(!base.Disposed);
			this.Flush();
			string text;
			string text2;
			base.GetPath(out text, out text2, dir, name, true);
			if (!this._paths.Add(text.ToLowerInvariant()))
			{
				throw Contracts.ExceptParam("name", "Duplicate entry: '{0}'", new object[] { text });
			}
			Stream stream;
			if (text2 != null)
			{
				stream = new FileStream(text2, FileMode.CreateNew);
			}
			else
			{
				stream = new MemoryStream();
			}
			return base.AddEntry(text, stream);
		}

		// Token: 0x060017FB RID: 6139 RVA: 0x000898B8 File Offset: 0x00087AB8
		protected override void OnDispose(Repository.Entry ent)
		{
			base.RemoveEntry(ent);
			if (this._closed != null)
			{
				this._closed.Enqueue(new KeyValuePair<string, Stream>(ent.Path, ent.Stream));
				return;
			}
			Utils.CloseEx(ent.Stream);
		}

		// Token: 0x060017FC RID: 6140 RVA: 0x000898F4 File Offset: 0x00087AF4
		protected override void Dispose(bool disposing)
		{
			if (this._closed != null)
			{
				while (this._closed.Count > 0)
				{
					Utils.CloseEx(this._closed.Dequeue().Value);
				}
				this._closed = null;
			}
			if (this._archive != null)
			{
				try
				{
					this._archive.Dispose();
				}
				catch
				{
				}
				this._archive = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x060017FD RID: 6141 RVA: 0x00089970 File Offset: 0x00087B70
		private void Flush()
		{
			while (this._closed.Count > 0)
			{
				string text = null;
				KeyValuePair<string, Stream> keyValuePair = this._closed.Dequeue();
				using (Stream value = keyValuePair.Value)
				{
					FileStream fileStream = value as FileStream;
					if (fileStream != null)
					{
						text = fileStream.Name;
					}
					ZipArchiveEntry zipArchiveEntry = this._archive.CreateEntry(keyValuePair.Key);
					using (Stream stream = zipArchiveEntry.Open())
					{
						value.Position = 0L;
						value.CopyTo(stream);
					}
				}
				if (!string.IsNullOrEmpty(text))
				{
					File.Delete(text);
				}
			}
		}

		// Token: 0x060017FE RID: 6142 RVA: 0x00089A28 File Offset: 0x00087C28
		public void Commit()
		{
			Contracts.Check(!base.Disposed);
			base.DisposeAllEntries();
			this.Flush();
			this.Dispose(true);
		}

		// Token: 0x04000E73 RID: 3699
		private ZipArchive _archive;

		// Token: 0x04000E74 RID: 3700
		private Queue<KeyValuePair<string, Stream>> _closed;

		// Token: 0x04000E75 RID: 3701
		private HashSet<string> _paths;
	}
}
