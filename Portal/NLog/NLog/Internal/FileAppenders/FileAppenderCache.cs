using System;
using System.IO;
using System.Threading;
using NLog.Common;
using NLog.Time;

namespace NLog.Internal.FileAppenders
{
	// Token: 0x02000160 RID: 352
	internal sealed class FileAppenderCache : IDisposable
	{
		// Token: 0x06001092 RID: 4242 RVA: 0x0002B2A4 File Offset: 0x000294A4
		private FileAppenderCache()
			: this(0, null, null)
		{
		}

		// Token: 0x06001093 RID: 4243 RVA: 0x0002B2B0 File Offset: 0x000294B0
		public FileAppenderCache(int size, IFileAppenderFactory appenderFactory, ICreateFileParameters createFileParams)
		{
			this.Size = size;
			this.Factory = appenderFactory;
			this.CreateFileParameters = createFileParams;
			this._appenders = new BaseFileAppender[this.Size];
			this._autoClosingTimer = new Timer(new TimerCallback(this.AutoClosingTimerCallback), null, -1, -1);
			this._externalFileArchivingWatcher.FileChanged += this.ExternalFileArchivingWatcher_OnFileChanged;
		}

		// Token: 0x06001094 RID: 4244 RVA: 0x0002B328 File Offset: 0x00029528
		private void ExternalFileArchivingWatcher_OnFileChanged(object sender, FileSystemEventArgs e)
		{
			if (this._logFileWasArchived || this.CheckCloseAppenders == null || this._autoClosingTimer == null)
			{
				return;
			}
			if (this.FileAppenderFolderChanged(e.FullPath))
			{
				if ((e.ChangeType & (WatcherChangeTypes.Deleted | WatcherChangeTypes.Renamed)) != (WatcherChangeTypes)0)
				{
					this._logFileWasArchived = true;
				}
			}
			else if ((e.ChangeType & WatcherChangeTypes.Created) == WatcherChangeTypes.Created)
			{
				this._logFileWasArchived = true;
			}
			if (this._logFileWasArchived && this._autoClosingTimer != null)
			{
				this._autoClosingTimer.Change(50, -1);
			}
		}

		// Token: 0x06001095 RID: 4245 RVA: 0x0002B3A4 File Offset: 0x000295A4
		private bool FileAppenderFolderChanged(string fullPath)
		{
			if (string.IsNullOrEmpty(fullPath))
			{
				return false;
			}
			if (string.IsNullOrEmpty(this._archiveFilePatternToWatch))
			{
				return true;
			}
			string directoryName = Path.GetDirectoryName(this._archiveFilePatternToWatch);
			if (!string.IsNullOrEmpty(directoryName))
			{
				string directoryName2 = Path.GetDirectoryName(fullPath);
				return !string.Equals(directoryName, directoryName2, StringComparison.OrdinalIgnoreCase);
			}
			return true;
		}

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06001096 RID: 4246 RVA: 0x0002B3F2 File Offset: 0x000295F2
		// (set) Token: 0x06001097 RID: 4247 RVA: 0x0002B3FC File Offset: 0x000295FC
		public string ArchiveFilePatternToWatch
		{
			get
			{
				return this._archiveFilePatternToWatch;
			}
			set
			{
				if (this._archiveFilePatternToWatch != value)
				{
					if (!string.IsNullOrEmpty(this._archiveFilePatternToWatch))
					{
						string directoryName = Path.GetDirectoryName(this._archiveFilePatternToWatch);
						if (string.IsNullOrEmpty(directoryName))
						{
							this._externalFileArchivingWatcher.StopWatching(directoryName);
						}
					}
					this._archiveFilePatternToWatch = value;
					this._logFileWasArchived = false;
				}
			}
		}

		// Token: 0x06001098 RID: 4248 RVA: 0x0002B452 File Offset: 0x00029652
		public void InvalidateAppendersForArchivedFiles()
		{
			if (this._logFileWasArchived)
			{
				this._logFileWasArchived = false;
				InternalLogger.Trace("FileAppender: Invalidate archived files");
				this.CloseAppenders("Cleanup Archive");
			}
		}

		// Token: 0x06001099 RID: 4249 RVA: 0x0002B478 File Offset: 0x00029678
		private void AutoClosingTimerCallback(object state)
		{
			EventHandler checkCloseAppenders = this.CheckCloseAppenders;
			if (checkCloseAppenders != null)
			{
				checkCloseAppenders(this, EventArgs.Empty);
			}
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x0600109A RID: 4250 RVA: 0x0002B49B File Offset: 0x0002969B
		// (set) Token: 0x0600109B RID: 4251 RVA: 0x0002B4A3 File Offset: 0x000296A3
		public ICreateFileParameters CreateFileParameters { get; private set; }

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x0600109C RID: 4252 RVA: 0x0002B4AC File Offset: 0x000296AC
		// (set) Token: 0x0600109D RID: 4253 RVA: 0x0002B4B4 File Offset: 0x000296B4
		public IFileAppenderFactory Factory { get; private set; }

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x0600109E RID: 4254 RVA: 0x0002B4BD File Offset: 0x000296BD
		// (set) Token: 0x0600109F RID: 4255 RVA: 0x0002B4C5 File Offset: 0x000296C5
		public int Size { get; private set; }

		// Token: 0x1400001D RID: 29
		// (add) Token: 0x060010A0 RID: 4256 RVA: 0x0002B4D0 File Offset: 0x000296D0
		// (remove) Token: 0x060010A1 RID: 4257 RVA: 0x0002B508 File Offset: 0x00029708
		public event EventHandler CheckCloseAppenders;

		// Token: 0x060010A2 RID: 4258 RVA: 0x0002B540 File Offset: 0x00029740
		public BaseFileAppender AllocateAppender(string fileName)
		{
			BaseFileAppender baseFileAppender = null;
			int num = this._appenders.Length - 1;
			for (int i = 0; i < this._appenders.Length; i++)
			{
				if (this._appenders[i] == null)
				{
					num = i;
					break;
				}
				if (string.Equals(this._appenders[i].FileName, fileName, StringComparison.OrdinalIgnoreCase))
				{
					BaseFileAppender baseFileAppender2 = this._appenders[i];
					if (i > 0)
					{
						for (int j = i; j > 0; j--)
						{
							this._appenders[j] = this._appenders[j - 1];
						}
						this._appenders[0] = baseFileAppender2;
					}
					baseFileAppender = baseFileAppender2;
					break;
				}
			}
			if (baseFileAppender == null)
			{
				baseFileAppender = this.CreateAppender(fileName, num);
			}
			return baseFileAppender;
		}

		// Token: 0x060010A3 RID: 4259 RVA: 0x0002B5DC File Offset: 0x000297DC
		private BaseFileAppender CreateAppender(string fileName, int freeSpot)
		{
			BaseFileAppender baseFileAppender2;
			try
			{
				InternalLogger.Debug<string>("Creating file appender: {0}", fileName);
				BaseFileAppender baseFileAppender = this.Factory.Open(fileName, this.CreateFileParameters);
				if (this._appenders[freeSpot] != null)
				{
					this.CloseAppender(this._appenders[freeSpot], "Stale", false);
					this._appenders[freeSpot] = null;
				}
				for (int i = freeSpot; i > 0; i--)
				{
					this._appenders[i] = this._appenders[i - 1];
				}
				this._appenders[0] = baseFileAppender;
				baseFileAppender2 = baseFileAppender;
				if (this.CheckCloseAppenders != null)
				{
					if (freeSpot == 0)
					{
						this._logFileWasArchived = false;
					}
					if (!string.IsNullOrEmpty(this._archiveFilePatternToWatch))
					{
						string directoryName = Path.GetDirectoryName(this._archiveFilePatternToWatch);
						if (!Directory.Exists(directoryName))
						{
							Directory.CreateDirectory(directoryName);
						}
						this._externalFileArchivingWatcher.Watch(this._archiveFilePatternToWatch);
					}
					this._externalFileArchivingWatcher.Watch(baseFileAppender2.FileName);
				}
			}
			catch (Exception ex)
			{
				InternalLogger.Warn(ex, "Failed to create file appender: {0}", new object[] { fileName });
				throw;
			}
			return baseFileAppender2;
		}

		// Token: 0x060010A4 RID: 4260 RVA: 0x0002B6DC File Offset: 0x000298DC
		public void CloseAppenders(string reason)
		{
			for (int i = 0; i < this._appenders.Length; i++)
			{
				BaseFileAppender baseFileAppender = this._appenders[i];
				if (baseFileAppender == null)
				{
					break;
				}
				this.CloseAppender(baseFileAppender, reason, true);
				this._appenders[i] = null;
				baseFileAppender.Dispose();
			}
		}

		// Token: 0x060010A5 RID: 4261 RVA: 0x0002B720 File Offset: 0x00029920
		public void CloseAppenders(DateTime expireTime)
		{
			if (this._logFileWasArchived)
			{
				this._logFileWasArchived = false;
				this.CloseAppenders("Cleanup Timer");
				return;
			}
			if (expireTime != DateTime.MinValue)
			{
				int num = 0;
				while (num < this._appenders.Length && this._appenders[num] != null)
				{
					if (this._appenders[num].OpenTimeUtc < expireTime)
					{
						for (int i = num; i < this._appenders.Length; i++)
						{
							BaseFileAppender baseFileAppender = this._appenders[i];
							if (baseFileAppender == null)
							{
								return;
							}
							this.CloseAppender(baseFileAppender, "Expired", num == 0);
							this._appenders[i] = null;
							baseFileAppender.Dispose();
						}
						return;
					}
					num++;
				}
			}
		}

		// Token: 0x060010A6 RID: 4262 RVA: 0x0002B7C8 File Offset: 0x000299C8
		public void FlushAppenders()
		{
			foreach (BaseFileAppender baseFileAppender in this._appenders)
			{
				if (baseFileAppender == null)
				{
					break;
				}
				try
				{
					baseFileAppender.Flush();
				}
				catch (Exception ex)
				{
					InternalLogger.Error(ex, "Failed to flush file '{0}'.", new object[] { baseFileAppender.FileName });
					BaseFileAppender baseFileAppender2 = this.InvalidateAppender(baseFileAppender.FileName);
					if (baseFileAppender2 != null)
					{
						baseFileAppender2.Dispose();
					}
					throw;
				}
			}
		}

		// Token: 0x060010A7 RID: 4263 RVA: 0x0002B83C File Offset: 0x00029A3C
		private BaseFileAppender GetAppender(string fileName)
		{
			for (int i = 0; i < this._appenders.Length; i++)
			{
				BaseFileAppender baseFileAppender = this._appenders[i];
				if (baseFileAppender == null)
				{
					break;
				}
				if (string.Equals(baseFileAppender.FileName, fileName, StringComparison.OrdinalIgnoreCase))
				{
					return baseFileAppender;
				}
			}
			return null;
		}

		// Token: 0x060010A8 RID: 4264 RVA: 0x0002B87C File Offset: 0x00029A7C
		public DateTime? GetFileCreationTimeSource(string filePath, DateTime? fallbackTimeSource = null)
		{
			BaseFileAppender appender = this.GetAppender(filePath);
			DateTime? dateTime = null;
			if (appender != null)
			{
				try
				{
					dateTime = FileCharacteristicsHelper.ValidateFileCreationTime<BaseFileAppender>(appender, (BaseFileAppender f) => f.GetFileCreationTimeUtc(), (BaseFileAppender f) => fallbackTimeSource, null);
					if (dateTime != null)
					{
						DateTime creationTimeUtc = appender.CreationTimeUtc;
						if (dateTime.Value != creationTimeUtc)
						{
							appender.CreationTimeUtc = dateTime.Value;
						}
						return new DateTime?(appender.CreationTimeSource);
					}
				}
				catch (Exception ex)
				{
					InternalLogger.Error(ex, "Failed to get file creation time for file '{0}'.", new object[] { appender.FileName });
					BaseFileAppender baseFileAppender = this.InvalidateAppender(appender.FileName);
					if (baseFileAppender != null)
					{
						baseFileAppender.Dispose();
					}
					throw;
				}
			}
			FileInfo fileInfo = new FileInfo(filePath);
			if (fileInfo.Exists)
			{
				dateTime = new DateTime?(FileCharacteristicsHelper.ValidateFileCreationTime<FileInfo>(fileInfo, (FileInfo f) => new DateTime?(f.GetCreationTimeUtc()), (FileInfo f) => fallbackTimeSource, (FileInfo f) => new DateTime?(f.GetLastWriteTimeUtc())).Value);
				return new DateTime?(TimeSource.Current.FromSystemTime(dateTime.Value));
			}
			return dateTime;
		}

		// Token: 0x060010A9 RID: 4265 RVA: 0x0002B9F0 File Offset: 0x00029BF0
		public DateTime? GetFileLastWriteTimeUtc(string filePath)
		{
			FileInfo fileInfo = new FileInfo(filePath);
			if (fileInfo.Exists)
			{
				return new DateTime?(fileInfo.GetLastWriteTimeUtc());
			}
			return null;
		}

		// Token: 0x060010AA RID: 4266 RVA: 0x0002BA24 File Offset: 0x00029C24
		public long? GetFileLength(string filePath)
		{
			BaseFileAppender appender = this.GetAppender(filePath);
			if (appender != null)
			{
				try
				{
					long? fileLength = appender.GetFileLength();
					if (fileLength != null)
					{
						return fileLength;
					}
				}
				catch (Exception ex)
				{
					InternalLogger.Error(ex, "Failed to get length for file '{0}'.", new object[] { appender.FileName });
					BaseFileAppender baseFileAppender = this.InvalidateAppender(appender.FileName);
					if (baseFileAppender != null)
					{
						baseFileAppender.Dispose();
					}
					throw;
				}
			}
			FileInfo fileInfo = new FileInfo(filePath);
			if (fileInfo.Exists)
			{
				return new long?(fileInfo.Length);
			}
			return null;
		}

		// Token: 0x060010AB RID: 4267 RVA: 0x0002BABC File Offset: 0x00029CBC
		public BaseFileAppender InvalidateAppender(string filePath)
		{
			for (int i = 0; i < this._appenders.Length; i++)
			{
				BaseFileAppender baseFileAppender = this._appenders[i];
				if (baseFileAppender == null)
				{
					break;
				}
				if (string.Equals(baseFileAppender.FileName, filePath, StringComparison.OrdinalIgnoreCase))
				{
					for (int j = i; j < this._appenders.Length - 1; j++)
					{
						this._appenders[j] = this._appenders[j + 1];
					}
					this._appenders[this._appenders.Length - 1] = null;
					this.CloseAppender(baseFileAppender, "Invalidate", this._appenders[0] == null);
					return baseFileAppender;
				}
			}
			return null;
		}

		// Token: 0x060010AC RID: 4268 RVA: 0x0002BB4C File Offset: 0x00029D4C
		private void CloseAppender(BaseFileAppender appender, string reason, bool lastAppender)
		{
			InternalLogger.Debug<string, string>("FileAppender Closing {0} - {1}", reason, appender.FileName);
			if (lastAppender)
			{
				this._autoClosingTimer.Change(-1, -1);
				this._externalFileArchivingWatcher.StopWatching();
				this._logFileWasArchived = false;
			}
			else
			{
				this._externalFileArchivingWatcher.StopWatching(appender.FileName);
			}
			appender.Close();
		}

		// Token: 0x060010AD RID: 4269 RVA: 0x0002BBA8 File Offset: 0x00029DA8
		public void Dispose()
		{
			this.CheckCloseAppenders = null;
			this._externalFileArchivingWatcher.Dispose();
			this._logFileWasArchived = false;
			Timer autoClosingTimer = this._autoClosingTimer;
			if (autoClosingTimer != null)
			{
				this._autoClosingTimer = null;
				autoClosingTimer.WaitForDispose(TimeSpan.Zero);
			}
		}

		// Token: 0x0400047B RID: 1147
		private readonly BaseFileAppender[] _appenders;

		// Token: 0x0400047C RID: 1148
		private Timer _autoClosingTimer;

		// Token: 0x0400047D RID: 1149
		private string _archiveFilePatternToWatch;

		// Token: 0x0400047E RID: 1150
		private readonly MultiFileWatcher _externalFileArchivingWatcher = new MultiFileWatcher(NotifyFilters.FileName | NotifyFilters.DirectoryName);

		// Token: 0x0400047F RID: 1151
		private bool _logFileWasArchived;

		// Token: 0x04000480 RID: 1152
		public static readonly FileAppenderCache Empty = new FileAppenderCache();
	}
}
