using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace Microsoft.Mashup.Libraries
{
	// Token: 0x020020D2 RID: 8402
	public class LibraryFileWatcher : IDisposable
	{
		// Token: 0x1400000A RID: 10
		// (add) Token: 0x0600CDD5 RID: 52693 RVA: 0x0028E4A0 File Offset: 0x0028C6A0
		// (remove) Token: 0x0600CDD6 RID: 52694 RVA: 0x0028E4D8 File Offset: 0x0028C6D8
		public event EventHandler<LibraryChangedEventArgs> Changed;

		// Token: 0x0600CDD7 RID: 52695 RVA: 0x0028E510 File Offset: 0x0028C710
		public LibraryFileWatcher(string directory, string[] libraryFileExtensions, bool includeSubdirectories)
		{
			this.lockObject = new object();
			this.libraryFileExtensions = libraryFileExtensions;
			this.includeSubdirectories = includeSubdirectories;
			this.incomingFileChangeEvents = new Queue<FileSystemEventArgs>();
			this.directories = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			this.libraryFiles = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
			this.fileWatcher = new FileSystemWatcher(directory);
			this.fileWatcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.DirectoryName | NotifyFilters.LastWrite;
			this.fileWatcher.Filter = "*.*";
			this.fileWatcher.IncludeSubdirectories = includeSubdirectories;
			this.fileWatcher.Changed += this.QueueIncomingFileSystemEvent;
			this.fileWatcher.Created += this.QueueIncomingFileSystemEvent;
			this.fileWatcher.Deleted += this.QueueIncomingFileSystemEvent;
			this.fileWatcher.Renamed += new RenamedEventHandler(this.QueueIncomingFileSystemEvent);
		}

		// Token: 0x1700317B RID: 12667
		// (get) Token: 0x0600CDD8 RID: 52696 RVA: 0x0028E5F8 File Offset: 0x0028C7F8
		// (set) Token: 0x0600CDD9 RID: 52697 RVA: 0x0028E640 File Offset: 0x0028C840
		public bool EnableRaisingEvents
		{
			get
			{
				object obj = this.lockObject;
				bool enableRaisingEvents;
				lock (obj)
				{
					enableRaisingEvents = this.fileWatcher.EnableRaisingEvents;
				}
				return enableRaisingEvents;
			}
			set
			{
				object obj = this.lockObject;
				lock (obj)
				{
					if (this.fileWatcher.EnableRaisingEvents != value)
					{
						this.fileWatcher.EnableRaisingEvents = value;
						if (!value)
						{
							this.Stop();
						}
						else
						{
							this.Start();
						}
					}
				}
			}
		}

		// Token: 0x0600CDDA RID: 52698 RVA: 0x0028E6A8 File Offset: 0x0028C8A8
		public static IEnumerable<string> GetAllLibraryFiles(string root, bool includeSubdirectories, string[] libraryFileExtensions)
		{
			Stack<string> stack = new Stack<string>(20);
			List<string> list = new List<string>();
			stack.Push(root);
			Func<string, bool> <>9__0;
			while (stack.Count > 0)
			{
				string text = stack.Pop();
				try
				{
					List<string> list2 = list;
					IEnumerable<string> files = Directory.GetFiles(text);
					Func<string, bool> func;
					if ((func = <>9__0) == null)
					{
						func = (<>9__0 = (string path) => libraryFileExtensions.Contains(Path.GetExtension(path), StringComparer.OrdinalIgnoreCase));
					}
					list2.AddRange(files.Where(func));
					if (includeSubdirectories)
					{
						foreach (string text2 in Directory.GetDirectories(text))
						{
							stack.Push(text2);
						}
					}
				}
				catch (UnauthorizedAccessException)
				{
				}
				catch (DirectoryNotFoundException)
				{
				}
			}
			return list;
		}

		// Token: 0x0600CDDB RID: 52699 RVA: 0x0028E770 File Offset: 0x0028C970
		public string[] GetLibraryFiles()
		{
			object obj = this.lockObject;
			string[] array;
			lock (obj)
			{
				array = this.libraryFiles.Keys.ToArray<string>();
			}
			return array;
		}

		// Token: 0x0600CDDC RID: 52700 RVA: 0x0028E7BC File Offset: 0x0028C9BC
		public void Dispose()
		{
			object obj = this.lockObject;
			lock (obj)
			{
				if (this.fileWatcher != null)
				{
					this.fileWatcher.EnableRaisingEvents = false;
					this.fileWatcher.Changed -= this.QueueIncomingFileSystemEvent;
					this.fileWatcher.Created -= this.QueueIncomingFileSystemEvent;
					this.fileWatcher.Deleted -= this.QueueIncomingFileSystemEvent;
					this.fileWatcher.Renamed -= new RenamedEventHandler(this.QueueIncomingFileSystemEvent);
					this.incomingFileChangeEvents.Clear();
					this.directories.Clear();
					this.libraryFiles.Clear();
					this.fileWatcher.Dispose();
					this.fileWatcher = null;
				}
			}
		}

		// Token: 0x0600CDDD RID: 52701 RVA: 0x0028E8A0 File Offset: 0x0028CAA0
		private bool HasLibraryExtension(string filename)
		{
			return this.libraryFileExtensions.Contains(Path.GetExtension(filename), StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x0600CDDE RID: 52702 RVA: 0x0028E8B8 File Offset: 0x0028CAB8
		private void Start()
		{
			this.directories.Clear();
			this.libraryFiles.Clear();
			this.incomingFileChangeEvents.Clear();
			DirectoryInfo directoryInfo = new DirectoryInfo(this.fileWatcher.Path);
			this.ProcessOneEvent(new FileSystemEventArgs(WatcherChangeTypes.Created, directoryInfo.Parent.FullName, directoryInfo.Name));
			this.fileWatcher.EnableRaisingEvents = true;
		}

		// Token: 0x0600CDDF RID: 52703 RVA: 0x0028E920 File Offset: 0x0028CB20
		private void Stop()
		{
			this.fileWatcher.EnableRaisingEvents = false;
			DirectoryInfo directoryInfo = new DirectoryInfo(this.fileWatcher.Path);
			FileSystemEventArgs fileSystemEventArgs = new FileSystemEventArgs(WatcherChangeTypes.Deleted, directoryInfo.Parent.FullName, directoryInfo.Name);
			this.OnDeleted(fileSystemEventArgs);
			this.directories.Clear();
			this.libraryFiles.Clear();
		}

		// Token: 0x0600CDE0 RID: 52704 RVA: 0x0028E980 File Offset: 0x0028CB80
		private void OnLibraryChanged(LibraryChangedEventArgs eventArgs)
		{
			EventHandler<LibraryChangedEventArgs> changed = this.Changed;
			if (changed != null)
			{
				try
				{
					changed(this, eventArgs);
				}
				catch (Exception ex)
				{
					if (!Utilities.IsSafeException(ex))
					{
						throw;
					}
				}
			}
		}

		// Token: 0x0600CDE1 RID: 52705 RVA: 0x0028E9BC File Offset: 0x0028CBBC
		private void QueueIncomingFileSystemEvent(object source, FileSystemEventArgs eventArgs)
		{
			if (!this.fileWatcher.EnableRaisingEvents)
			{
				return;
			}
			if (!Directory.Exists(eventArgs.FullPath))
			{
				RenamedEventArgs renamedEventArgs = eventArgs as RenamedEventArgs;
				if (renamedEventArgs != null)
				{
					if (this.HasLibraryExtension(renamedEventArgs.FullPath) && !this.HasLibraryExtension(renamedEventArgs.OldFullPath))
					{
						this.Enqueue(new FileSystemEventArgs(WatcherChangeTypes.Created, Path.GetDirectoryName(renamedEventArgs.FullPath), renamedEventArgs.Name));
						return;
					}
					if (!this.HasLibraryExtension(renamedEventArgs.FullPath) && this.HasLibraryExtension(renamedEventArgs.OldFullPath))
					{
						this.Enqueue(new FileSystemEventArgs(WatcherChangeTypes.Deleted, Path.GetDirectoryName(renamedEventArgs.OldFullPath), renamedEventArgs.OldName));
						return;
					}
				}
				if (!this.HasLibraryExtension(eventArgs.FullPath) && eventArgs.ChangeType != WatcherChangeTypes.Deleted)
				{
					return;
				}
			}
			this.Enqueue(eventArgs);
		}

		// Token: 0x0600CDE2 RID: 52706 RVA: 0x0028EA84 File Offset: 0x0028CC84
		private void Enqueue(FileSystemEventArgs eventArgs)
		{
			object obj = this.lockObject;
			lock (obj)
			{
				this.incomingFileChangeEvents.Enqueue(eventArgs);
			}
			ThreadPool.QueueUserWorkItem(delegate(object state)
			{
				this.ProcessIncomingFileChangeEvents();
			});
		}

		// Token: 0x0600CDE3 RID: 52707 RVA: 0x0028EADC File Offset: 0x0028CCDC
		private void ProcessIncomingFileChangeEvents()
		{
			for (;;)
			{
				object obj = this.lockObject;
				FileSystemEventArgs fileSystemEventArgs;
				lock (obj)
				{
					if (this.incomingFileChangeEvents.Count == 0 || this.processing)
					{
						break;
					}
					fileSystemEventArgs = this.incomingFileChangeEvents.Dequeue();
					this.processing = true;
				}
				try
				{
					this.ProcessOneEvent(fileSystemEventArgs);
					continue;
				}
				finally
				{
					obj = this.lockObject;
					lock (obj)
					{
						this.processing = false;
					}
				}
				break;
			}
		}

		// Token: 0x0600CDE4 RID: 52708 RVA: 0x0028EB88 File Offset: 0x0028CD88
		private void ProcessOneEvent(FileSystemEventArgs eventArgs)
		{
			WatcherChangeTypes changeType = eventArgs.ChangeType;
			switch (changeType)
			{
			case WatcherChangeTypes.Created:
				this.OnCreated(eventArgs);
				return;
			case WatcherChangeTypes.Deleted:
				this.OnDeleted(eventArgs);
				return;
			case WatcherChangeTypes.Created | WatcherChangeTypes.Deleted:
				break;
			case WatcherChangeTypes.Changed:
				this.OnChanged(eventArgs);
				return;
			default:
				if (changeType != WatcherChangeTypes.Renamed)
				{
					return;
				}
				this.OnRenamed((RenamedEventArgs)eventArgs);
				break;
			}
		}

		// Token: 0x0600CDE5 RID: 52709 RVA: 0x0028EBE0 File Offset: 0x0028CDE0
		private void OnChanged(FileSystemEventArgs eventArgs)
		{
			Dictionary<string, string> dictionary = this.libraryFiles;
			string text;
			lock (dictionary)
			{
				if (!this.libraryFiles.TryGetValue(eventArgs.FullPath, out text))
				{
					text = null;
				}
			}
			if (text == null)
			{
				this.OnCreated(eventArgs);
				return;
			}
			string contentHash = LibraryFileWatcher.GetContentHash(eventArgs.FullPath);
			if (string.IsNullOrEmpty(contentHash))
			{
				return;
			}
			if (string.Equals(contentHash, text))
			{
				return;
			}
			dictionary = this.libraryFiles;
			lock (dictionary)
			{
				this.libraryFiles[eventArgs.FullPath] = contentHash;
			}
			LibraryChangedEventArgs libraryChangedEventArgs = new LibraryChangedEventArgs(new string[0], new string[] { eventArgs.FullPath }, new string[0]);
			this.OnLibraryChanged(libraryChangedEventArgs);
		}

		// Token: 0x0600CDE6 RID: 52710 RVA: 0x0028ECC0 File Offset: 0x0028CEC0
		private void OnCreated(FileSystemEventArgs eventArgs)
		{
			if (File.Exists(eventArgs.FullPath))
			{
				Dictionary<string, string> dictionary = this.libraryFiles;
				lock (dictionary)
				{
					if (this.libraryFiles.ContainsKey(eventArgs.FullPath))
					{
						return;
					}
				}
				string contentHash = LibraryFileWatcher.GetContentHash(eventArgs.FullPath);
				if (string.IsNullOrEmpty(contentHash))
				{
					return;
				}
				FileInfo fileInfo = new FileInfo(eventArgs.FullPath);
				object obj = this.lockObject;
				lock (obj)
				{
					this.directories.Add(fileInfo.DirectoryName);
					this.libraryFiles.Add(eventArgs.FullPath, contentHash);
				}
				LibraryChangedEventArgs libraryChangedEventArgs = new LibraryChangedEventArgs(new string[] { eventArgs.FullPath }, new string[0], new string[0]);
				this.OnLibraryChanged(libraryChangedEventArgs);
				return;
			}
			else if (Directory.Exists(eventArgs.FullPath))
			{
				object obj = this.lockObject;
				lock (obj)
				{
					if (this.directories.Contains(eventArgs.FullPath))
					{
						return;
					}
				}
				IEnumerable<string> allDirectories = LibraryFileWatcher.GetAllDirectories(eventArgs.FullPath);
				obj = this.lockObject;
				lock (obj)
				{
					this.directories.UnionWith(allDirectories);
				}
				Dictionary<string, string> dictionary2 = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
				foreach (string text in LibraryFileWatcher.GetAllLibraryFiles(eventArgs.FullPath, this.includeSubdirectories, this.libraryFileExtensions))
				{
					obj = this.lockObject;
					lock (obj)
					{
						if (this.libraryFiles.ContainsKey(text))
						{
							continue;
						}
					}
					string contentHash2 = LibraryFileWatcher.GetContentHash(text);
					if (!string.IsNullOrEmpty(contentHash2))
					{
						dictionary2.Add(text, contentHash2);
					}
				}
				foreach (KeyValuePair<string, string> keyValuePair in dictionary2)
				{
					this.libraryFiles.Add(keyValuePair.Key, keyValuePair.Value);
				}
				if (dictionary2.Count > 0)
				{
					LibraryChangedEventArgs libraryChangedEventArgs2 = new LibraryChangedEventArgs(dictionary2.Keys.ToArray<string>(), new string[0], new string[0]);
					this.OnLibraryChanged(libraryChangedEventArgs2);
				}
			}
		}

		// Token: 0x0600CDE7 RID: 52711 RVA: 0x0028EF98 File Offset: 0x0028D198
		private void OnDeleted(FileSystemEventArgs eventArgs)
		{
			string[] array = null;
			object obj = this.lockObject;
			lock (obj)
			{
				IEnumerable<string> enumerable = null;
				if (this.directories.Contains(eventArgs.FullPath))
				{
					this.directories.RemoveWhere((string s) => s.StartsWith(eventArgs.FullPath, StringComparison.OrdinalIgnoreCase));
					enumerable = this.libraryFiles.Keys.Where((string k) => Path.GetDirectoryName(k).StartsWith(eventArgs.FullPath, StringComparison.OrdinalIgnoreCase)).ToList<string>();
				}
				else
				{
					if (!this.libraryFiles.ContainsKey(eventArgs.FullPath))
					{
						return;
					}
					enumerable = this.libraryFiles.Keys.Where((string k) => string.Equals(k, eventArgs.FullPath, StringComparison.OrdinalIgnoreCase)).ToList<string>();
				}
				foreach (string text in enumerable)
				{
					this.libraryFiles.Remove(text);
				}
				array = enumerable.ToArray<string>();
			}
			if (array.Length != 0)
			{
				LibraryChangedEventArgs libraryChangedEventArgs = new LibraryChangedEventArgs(new string[0], new string[0], array);
				this.OnLibraryChanged(libraryChangedEventArgs);
			}
		}

		// Token: 0x0600CDE8 RID: 52712 RVA: 0x0028F0E8 File Offset: 0x0028D2E8
		private void OnRenamed(RenamedEventArgs eventArgs)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
			object obj;
			if (File.Exists(eventArgs.FullPath))
			{
				string oldFullPath = eventArgs.OldFullPath;
				string fullPath = eventArgs.FullPath;
				dictionary.Add(oldFullPath, fullPath);
			}
			else if (Directory.Exists(eventArgs.FullPath))
			{
				string oldFullPath2 = eventArgs.OldFullPath;
				string fullPath2 = eventArgs.FullPath;
				List<string> list = LibraryFileWatcher.GetAllLibraryFiles(fullPath2, this.includeSubdirectories, this.libraryFileExtensions).ToList<string>();
				obj = this.lockObject;
				lock (obj)
				{
					foreach (string text in list)
					{
						string text2 = text.Replace(fullPath2, oldFullPath2);
						if (this.libraryFiles.ContainsKey(text2))
						{
							dictionary.Add(text2, text);
						}
					}
					foreach (string text3 in ((IEnumerable<string>)this.directories.Where((string s) => s.StartsWith(eventArgs.OldFullPath, StringComparison.OrdinalIgnoreCase)).ToList<string>()))
					{
						string text4 = text3.Replace(eventArgs.OldFullPath, eventArgs.FullPath);
						this.directories.Remove(text3);
						this.directories.Add(text4);
					}
				}
			}
			obj = this.lockObject;
			lock (obj)
			{
				foreach (KeyValuePair<string, string> keyValuePair in dictionary)
				{
					string text5 = this.libraryFiles[keyValuePair.Key];
					this.libraryFiles.Remove(keyValuePair.Key);
					this.libraryFiles.Add(keyValuePair.Value, text5);
				}
			}
			if (dictionary.Count > 0)
			{
				LibraryChangedEventArgs libraryChangedEventArgs = new LibraryChangedEventArgs(dictionary.Values.ToArray<string>(), new string[0], dictionary.Keys.ToArray<string>());
				this.OnLibraryChanged(libraryChangedEventArgs);
			}
		}

		// Token: 0x0600CDE9 RID: 52713 RVA: 0x0028F37C File Offset: 0x0028D57C
		private static IEnumerable<string> GetAllDirectories(string root)
		{
			Stack<string> stack = new Stack<string>(20);
			List<string> list = new List<string>();
			stack.Push(root);
			list.Add(root);
			while (stack.Count > 0)
			{
				string text = stack.Pop();
				string[] array;
				try
				{
					array = Directory.GetDirectories(text);
				}
				catch (UnauthorizedAccessException)
				{
					continue;
				}
				catch (DirectoryNotFoundException)
				{
					continue;
				}
				array.ToList<string>().ForEach(new Action<string>(stack.Push));
				list.AddRange(array);
			}
			return list;
		}

		// Token: 0x0600CDEA RID: 52714 RVA: 0x0028F404 File Offset: 0x0028D604
		private static FileStream TryWaitForFileReady(string fullPath, FileMode mode, FileAccess access, FileShare share)
		{
			for (int i = 0; i < 3; i++)
			{
				FileStream fileStream = null;
				try
				{
					fileStream = new FileStream(fullPath, mode, access, share);
					return fileStream;
				}
				catch (IOException)
				{
					if (fileStream != null)
					{
						fileStream.Dispose();
					}
					Thread.Sleep(10);
				}
			}
			return null;
		}

		// Token: 0x0600CDEB RID: 52715 RVA: 0x0028F454 File Offset: 0x0028D654
		private static string GetContentHash(string fileFullPath)
		{
			string text;
			using (FileStream fileStream = LibraryFileWatcher.TryWaitForFileReady(fileFullPath, FileMode.Open, FileAccess.Read, FileShare.Read))
			{
				if (fileStream == null)
				{
					text = null;
				}
				else
				{
					try
					{
						text = Utilities.CreateHash(fileStream);
					}
					catch (IOException)
					{
						text = null;
					}
				}
			}
			return text;
		}

		// Token: 0x04006814 RID: 26644
		private readonly object lockObject;

		// Token: 0x04006815 RID: 26645
		private readonly string[] libraryFileExtensions;

		// Token: 0x04006816 RID: 26646
		private readonly bool includeSubdirectories;

		// Token: 0x04006817 RID: 26647
		private readonly Queue<FileSystemEventArgs> incomingFileChangeEvents;

		// Token: 0x04006818 RID: 26648
		private readonly HashSet<string> directories;

		// Token: 0x04006819 RID: 26649
		private readonly Dictionary<string, string> libraryFiles;

		// Token: 0x0400681A RID: 26650
		private FileSystemWatcher fileWatcher;

		// Token: 0x0400681B RID: 26651
		private bool processing;
	}
}
