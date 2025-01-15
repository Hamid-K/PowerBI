using System;
using System.Collections.Generic;
using System.IO;
using NLog.Common;

namespace NLog.Internal
{
	// Token: 0x0200012A RID: 298
	internal sealed class MultiFileWatcher : IDisposable
	{
		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06000EF7 RID: 3831 RVA: 0x000253C8 File Offset: 0x000235C8
		// (set) Token: 0x06000EF8 RID: 3832 RVA: 0x000253D0 File Offset: 0x000235D0
		public NotifyFilters NotifyFilters { get; set; }

		// Token: 0x1400001C RID: 28
		// (add) Token: 0x06000EF9 RID: 3833 RVA: 0x000253DC File Offset: 0x000235DC
		// (remove) Token: 0x06000EFA RID: 3834 RVA: 0x00025414 File Offset: 0x00023614
		public event FileSystemEventHandler FileChanged;

		// Token: 0x06000EFB RID: 3835 RVA: 0x00025449 File Offset: 0x00023649
		public MultiFileWatcher()
			: this(NotifyFilters.Attributes | NotifyFilters.Size | NotifyFilters.LastWrite | NotifyFilters.CreationTime | NotifyFilters.Security)
		{
		}

		// Token: 0x06000EFC RID: 3836 RVA: 0x00025456 File Offset: 0x00023656
		public MultiFileWatcher(NotifyFilters notifyFilters)
		{
			this.NotifyFilters = notifyFilters;
		}

		// Token: 0x06000EFD RID: 3837 RVA: 0x00025470 File Offset: 0x00023670
		public void Dispose()
		{
			this.FileChanged = null;
			this.StopWatching();
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000EFE RID: 3838 RVA: 0x00025488 File Offset: 0x00023688
		public void StopWatching()
		{
			Dictionary<string, FileSystemWatcher> watcherMap = this._watcherMap;
			lock (watcherMap)
			{
				foreach (FileSystemWatcher fileSystemWatcher in this._watcherMap.Values)
				{
					this.StopWatching(fileSystemWatcher);
				}
				this._watcherMap.Clear();
			}
		}

		// Token: 0x06000EFF RID: 3839 RVA: 0x00025514 File Offset: 0x00023714
		public void StopWatching(string fileName)
		{
			Dictionary<string, FileSystemWatcher> watcherMap = this._watcherMap;
			lock (watcherMap)
			{
				FileSystemWatcher fileSystemWatcher;
				if (this._watcherMap.TryGetValue(fileName, out fileSystemWatcher))
				{
					this.StopWatching(fileSystemWatcher);
					this._watcherMap.Remove(fileName);
				}
			}
		}

		// Token: 0x06000F00 RID: 3840 RVA: 0x00025574 File Offset: 0x00023774
		public void Watch(IEnumerable<string> fileNames)
		{
			if (fileNames == null)
			{
				return;
			}
			foreach (string text in fileNames)
			{
				this.Watch(text);
			}
		}

		// Token: 0x06000F01 RID: 3841 RVA: 0x000255C0 File Offset: 0x000237C0
		internal void Watch(string fileName)
		{
			string directoryName = Path.GetDirectoryName(fileName);
			if (!Directory.Exists(directoryName))
			{
				InternalLogger.Warn<string, string>("Cannot watch file {0} for changes as directory {1} doesn't exist", fileName, directoryName);
				return;
			}
			Dictionary<string, FileSystemWatcher> watcherMap = this._watcherMap;
			lock (watcherMap)
			{
				if (!this._watcherMap.ContainsKey(fileName))
				{
					FileSystemWatcher fileSystemWatcher = null;
					try
					{
						fileSystemWatcher = new FileSystemWatcher
						{
							Path = directoryName,
							Filter = Path.GetFileName(fileName),
							NotifyFilter = this.NotifyFilters
						};
						fileSystemWatcher.Created += this.OnFileChanged;
						fileSystemWatcher.Changed += this.OnFileChanged;
						fileSystemWatcher.Deleted += this.OnFileChanged;
						fileSystemWatcher.Renamed += new RenamedEventHandler(this.OnFileChanged);
						fileSystemWatcher.Error += this.OnWatcherError;
						fileSystemWatcher.EnableRaisingEvents = true;
						InternalLogger.Debug<string, string>("Watching path '{0}' filter '{1}' for changes.", fileSystemWatcher.Path, fileSystemWatcher.Filter);
						this._watcherMap.Add(fileName, fileSystemWatcher);
					}
					catch (Exception ex)
					{
						InternalLogger.Error(ex, "Failed Watching path '{0}' with file '{1}' for changes.", new object[] { directoryName, fileName });
						if (ex.MustBeRethrown())
						{
							throw;
						}
						if (fileSystemWatcher != null)
						{
							this.StopWatching(fileSystemWatcher);
						}
					}
				}
			}
		}

		// Token: 0x06000F02 RID: 3842 RVA: 0x00025714 File Offset: 0x00023914
		private void StopWatching(FileSystemWatcher watcher)
		{
			try
			{
				InternalLogger.Debug<string, string>("Stopping file watching for path '{0}' filter '{1}'", watcher.Path, watcher.Filter);
				watcher.EnableRaisingEvents = false;
				watcher.Created -= this.OnFileChanged;
				watcher.Changed -= this.OnFileChanged;
				watcher.Deleted -= this.OnFileChanged;
				watcher.Renamed -= new RenamedEventHandler(this.OnFileChanged);
				watcher.Error -= this.OnWatcherError;
				watcher.Dispose();
			}
			catch (Exception ex)
			{
				InternalLogger.Error(ex, "Failed to stop file watcher for path '{0}' filter '{1}'", new object[] { watcher.Path, watcher.Filter });
				if (ex.MustBeRethrown())
				{
					throw;
				}
			}
		}

		// Token: 0x06000F03 RID: 3843 RVA: 0x000257E0 File Offset: 0x000239E0
		private void OnWatcherError(object source, ErrorEventArgs e)
		{
			string text = string.Empty;
			FileSystemWatcher fileSystemWatcher = source as FileSystemWatcher;
			if (fileSystemWatcher != null)
			{
				text = fileSystemWatcher.Path;
			}
			Exception exception = e.GetException();
			if (exception != null)
			{
				InternalLogger.Warn(exception, "Error Watching Path {0}", new object[] { text });
				return;
			}
			InternalLogger.Warn<string>("Error Watching Path {0}", text);
		}

		// Token: 0x06000F04 RID: 3844 RVA: 0x00025830 File Offset: 0x00023A30
		private void OnFileChanged(object source, FileSystemEventArgs e)
		{
			FileSystemEventHandler fileChanged = this.FileChanged;
			if (fileChanged != null)
			{
				try
				{
					fileChanged(source, e);
				}
				catch (Exception ex)
				{
					InternalLogger.Error(ex, "Error Handling File Changed");
					if (ex.MustBeRethrownImmediately())
					{
						throw;
					}
				}
			}
		}

		// Token: 0x04000405 RID: 1029
		private readonly Dictionary<string, FileSystemWatcher> _watcherMap = new Dictionary<string, FileSystemWatcher>();
	}
}
