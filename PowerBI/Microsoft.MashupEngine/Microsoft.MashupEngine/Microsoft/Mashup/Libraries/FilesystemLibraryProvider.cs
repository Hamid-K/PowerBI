using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Microsoft.Mashup.Libraries
{
	// Token: 0x020020CC RID: 8396
	public sealed class FilesystemLibraryProvider : ILibraryProvider, IDisposable
	{
		// Token: 0x0600CDA7 RID: 52647 RVA: 0x0028DFD0 File Offset: 0x0028C1D0
		public FilesystemLibraryProvider(string directory, bool includeSubdirectories = false, bool watchForChanges = false, bool startWatching = true)
		{
			this.identifier = string.Format(CultureInfo.InvariantCulture, "Directory({0})", directory);
			this.directory = directory;
			this.includeSubdirectories = includeSubdirectories;
			if (watchForChanges)
			{
				this.watcher = new LibraryFileWatcher(directory, FilesystemLibraryProvider.libraryFileExtensions, includeSubdirectories);
				this.watcher.Changed += this.OnLibraryChanged;
				if (startWatching)
				{
					this.EnableRaisingEvents = true;
				}
			}
		}

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x0600CDA8 RID: 52648 RVA: 0x0028E040 File Offset: 0x0028C240
		// (remove) Token: 0x0600CDA9 RID: 52649 RVA: 0x0028E078 File Offset: 0x0028C278
		public event EventHandler<LibraryChangedEventArgs> Changed;

		// Token: 0x1700316D RID: 12653
		// (get) Token: 0x0600CDAA RID: 52650 RVA: 0x0028E0AD File Offset: 0x0028C2AD
		public string Identifier
		{
			get
			{
				return this.identifier;
			}
		}

		// Token: 0x1700316E RID: 12654
		// (get) Token: 0x0600CDAB RID: 52651 RVA: 0x0028E0B5 File Offset: 0x0028C2B5
		// (set) Token: 0x0600CDAC RID: 52652 RVA: 0x0028E0CC File Offset: 0x0028C2CC
		internal bool EnableRaisingEvents
		{
			get
			{
				return this.watcher != null && this.watcher.EnableRaisingEvents;
			}
			set
			{
				if (this.watcher == null)
				{
					if (value)
					{
						throw new InvalidOperationException();
					}
				}
				else
				{
					this.watcher.EnableRaisingEvents = value;
				}
			}
		}

		// Token: 0x0600CDAD RID: 52653 RVA: 0x0028E0EB File Offset: 0x0028C2EB
		public IEnumerable<ILibrary> GetLibraries()
		{
			IEnumerable<string> enumerable;
			if (this.watcher == null)
			{
				enumerable = LibraryFileWatcher.GetAllLibraryFiles(this.directory, this.includeSubdirectories, FilesystemLibraryProvider.libraryFileExtensions);
			}
			else
			{
				IEnumerable<string> libraryFiles = this.watcher.GetLibraryFiles();
				enumerable = libraryFiles;
			}
			IEnumerable<string> enumerable2 = enumerable;
			foreach (string text in enumerable2)
			{
				string text2 = this.StripProviderPath(text);
				ILibrary library;
				if (this.TryGetLibrary(text2, text, out library))
				{
					yield return library;
				}
			}
			IEnumerator<string> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0600CDAE RID: 52654 RVA: 0x0028E0FC File Offset: 0x0028C2FC
		public bool TryGetLibrary(string identifier, out ILibrary library)
		{
			string text = Path.Combine(this.directory, identifier);
			return this.TryGetLibrary(identifier, text, out library);
		}

		// Token: 0x0600CDAF RID: 52655 RVA: 0x0028E120 File Offset: 0x0028C320
		private bool TryGetLibrary(string identifier, string filename, out ILibrary library)
		{
			byte[] array;
			if (FilesystemLibraryProvider.TryReadFile(filename, out array))
			{
				library = new MemoryLibrary(this, identifier, array, false);
				return true;
			}
			library = null;
			return false;
		}

		// Token: 0x0600CDB0 RID: 52656 RVA: 0x0028E148 File Offset: 0x0028C348
		public void Dispose()
		{
			if (this.watcher != null)
			{
				this.watcher.Changed -= this.OnLibraryChanged;
				this.watcher.Dispose();
				this.watcher = null;
			}
		}

		// Token: 0x0600CDB1 RID: 52657 RVA: 0x0028E17C File Offset: 0x0028C37C
		private void OnLibraryChanged(object source, LibraryChangedEventArgs e)
		{
			EventHandler<LibraryChangedEventArgs> changed = this.Changed;
			if (changed != null)
			{
				changed(this, new LibraryChangedEventArgs(e.Added.Select((string identifier) => this.StripProviderPath(identifier)).ToArray<string>(), e.Changed.Select((string identifier) => this.StripProviderPath(identifier)).ToArray<string>(), e.Removed.Select((string identifier) => this.StripProviderPath(identifier)).ToArray<string>()));
			}
		}

		// Token: 0x0600CDB2 RID: 52658 RVA: 0x0028E1F3 File Offset: 0x0028C3F3
		private string StripProviderPath(string fullPath)
		{
			return fullPath.Substring(this.directory.Length + 1);
		}

		// Token: 0x0600CDB3 RID: 52659 RVA: 0x0028E208 File Offset: 0x0028C408
		private static bool TryReadFile(string filename, out byte[] contents)
		{
			bool flag;
			try
			{
				contents = File.ReadAllBytes(filename);
				flag = true;
			}
			catch (IOException)
			{
				contents = null;
				flag = false;
			}
			return flag;
		}

		// Token: 0x04006805 RID: 26629
		internal static readonly string[] libraryFileExtensions = new string[] { ".m", ".mez", ".pq", ".pqx" };

		// Token: 0x04006806 RID: 26630
		private readonly string identifier;

		// Token: 0x04006807 RID: 26631
		private readonly string directory;

		// Token: 0x04006808 RID: 26632
		private readonly bool includeSubdirectories;

		// Token: 0x04006809 RID: 26633
		private LibraryFileWatcher watcher;
	}
}
