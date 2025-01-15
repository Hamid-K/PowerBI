using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.MachineLearning.Internal.Utilities;

namespace Microsoft.MachineLearning.Model
{
	// Token: 0x02000477 RID: 1143
	public abstract class Repository : IDisposable
	{
		// Token: 0x1700024D RID: 589
		// (get) Token: 0x060017E7 RID: 6119 RVA: 0x000894A8 File Offset: 0x000876A8
		protected bool Disposed
		{
			get
			{
				return this._disposed;
			}
		}

		// Token: 0x060017E8 RID: 6120 RVA: 0x000894B0 File Offset: 0x000876B0
		internal Repository(bool needDir)
		{
			this._open = new List<Repository.Entry>();
			if (needDir)
			{
				this._dirTemp = Repository.GetTempPath();
				Directory.CreateDirectory(this._dirTemp);
				return;
			}
			GC.SuppressFinalize(this);
		}

		// Token: 0x060017E9 RID: 6121 RVA: 0x000894E4 File Offset: 0x000876E4
		private static string GetTempPath()
		{
			Guid guid = Guid.NewGuid();
			return Path.GetFullPath(Path.Combine(Path.GetTempPath(), "TLC_" + guid.ToString()));
		}

		// Token: 0x060017EA RID: 6122 RVA: 0x00089520 File Offset: 0x00087720
		~Repository()
		{
			if (!this.Disposed)
			{
				this.Dispose(false);
			}
		}

		// Token: 0x060017EB RID: 6123 RVA: 0x00089558 File Offset: 0x00087758
		public void Dispose()
		{
			if (!this.Disposed)
			{
				this.Dispose(true);
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x060017EC RID: 6124 RVA: 0x00089570 File Offset: 0x00087770
		protected virtual void Dispose(bool disposing)
		{
			try
			{
				this.DisposeAllEntries();
			}
			catch
			{
			}
			if (this._dirTemp != null)
			{
				try
				{
					Directory.Delete(this._dirTemp, true);
				}
				catch
				{
				}
			}
			this._disposed = true;
		}

		// Token: 0x060017ED RID: 6125 RVA: 0x000895C4 File Offset: 0x000877C4
		protected void DisposeAllEntries()
		{
			while (this._open.Count > 0)
			{
				Repository.Entry entry = this._open[this._open.Count - 1];
				entry.Dispose();
			}
		}

		// Token: 0x060017EE RID: 6126 RVA: 0x00089600 File Offset: 0x00087800
		protected void RemoveEntry(Repository.Entry ent)
		{
			int num = this._open.Count;
			while (--num >= 0)
			{
				if (this._open[num] == ent)
				{
					this._open.RemoveAt(num);
					return;
				}
			}
		}

		// Token: 0x060017EF RID: 6127
		protected abstract void OnDispose(Repository.Entry ent);

		// Token: 0x060017F0 RID: 6128 RVA: 0x0008963F File Offset: 0x0008783F
		protected string Normalize(string path)
		{
			if (path == null)
			{
				return null;
			}
			return path.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
		}

		// Token: 0x060017F1 RID: 6129 RVA: 0x00089658 File Offset: 0x00087858
		protected void GetPath(out string pathEnt, out string pathTemp, string dir, string name, bool createDir)
		{
			Contracts.CheckParam(dir == null || !dir.Contains(".."), "dir");
			Contracts.CheckParam(!string.IsNullOrWhiteSpace(name), "name");
			Contracts.CheckParam(!name.Contains(".."), "name");
			string fullPath = Path.GetFullPath(this._dirTemp ?? "x:\\dummy");
			string text = Path.Combine(fullPath, dir ?? "", name);
			text = Path.GetFullPath(text);
			string directoryName = Path.GetDirectoryName(text);
			Contracts.Check(directoryName != null);
			Contracts.Check(directoryName.StartsWith(fullPath));
			int length = fullPath.Length;
			Contracts.Check(text.Length > length && text[length] == Path.DirectorySeparatorChar);
			if (createDir && this._dirTemp != null && directoryName.Length > length)
			{
				Directory.CreateDirectory(directoryName);
			}
			pathEnt = text.Substring(length + 1);
			Contracts.Check(Utils.Size(pathEnt) > 0);
			Contracts.Check(text == Path.Combine(fullPath, pathEnt));
			pathTemp = ((this._dirTemp != null) ? text : null);
			pathEnt = this.Normalize(pathEnt);
			pathTemp = this.Normalize(pathTemp);
		}

		// Token: 0x060017F2 RID: 6130 RVA: 0x00089790 File Offset: 0x00087990
		protected Repository.Entry AddEntry(string pathEnt, Stream stream)
		{
			Repository.Entry entry = new Repository.Entry(this, pathEnt, stream);
			this._open.Add(entry);
			return entry;
		}

		// Token: 0x04000E6D RID: 3693
		protected readonly string _dirTemp;

		// Token: 0x04000E6E RID: 3694
		private List<Repository.Entry> _open;

		// Token: 0x04000E6F RID: 3695
		private bool _disposed;

		// Token: 0x02000478 RID: 1144
		public sealed class Entry : IDisposable
		{
			// Token: 0x1700024E RID: 590
			// (get) Token: 0x060017F3 RID: 6131 RVA: 0x000897B3 File Offset: 0x000879B3
			public string Path
			{
				get
				{
					return this._path;
				}
			}

			// Token: 0x1700024F RID: 591
			// (get) Token: 0x060017F4 RID: 6132 RVA: 0x000897BB File Offset: 0x000879BB
			public Stream Stream
			{
				get
				{
					return this._stream;
				}
			}

			// Token: 0x060017F5 RID: 6133 RVA: 0x000897C3 File Offset: 0x000879C3
			internal Entry(Repository rep, string path, Stream stream)
			{
				this._rep = rep;
				this._path = path;
				this._stream = stream;
			}

			// Token: 0x060017F6 RID: 6134 RVA: 0x000897E0 File Offset: 0x000879E0
			public void Dispose()
			{
				if (this._rep != null)
				{
					this._rep.OnDispose(this);
					this._rep = null;
				}
			}

			// Token: 0x04000E70 RID: 3696
			private Repository _rep;

			// Token: 0x04000E71 RID: 3697
			private readonly string _path;

			// Token: 0x04000E72 RID: 3698
			private readonly Stream _stream;
		}
	}
}
