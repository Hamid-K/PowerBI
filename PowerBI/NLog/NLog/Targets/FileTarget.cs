using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using NLog.Common;
using NLog.Config;
using NLog.Internal;
using NLog.Internal.FileAppenders;
using NLog.Layouts;
using NLog.Targets.FileArchiveModes;
using NLog.Time;

namespace NLog.Targets
{
	// Token: 0x0200003C RID: 60
	[Target("File")]
	public class FileTarget : TargetWithLayoutHeaderAndFooter, ICreateFileParameters
	{
		// Token: 0x060005FD RID: 1533 RVA: 0x0000DDBC File Offset: 0x0000BFBC
		private IFileArchiveMode GetFileArchiveHelper(string archiveFilePattern)
		{
			IFileArchiveMode fileArchiveMode;
			if ((fileArchiveMode = this._fileArchiveHelper) == null)
			{
				fileArchiveMode = (this._fileArchiveHelper = FileArchiveModeFactory.CreateArchiveStyle(archiveFilePattern, this.ArchiveNumbering, this.GetArchiveDateFormatString(this.ArchiveDateFormat), this.ArchiveFileName != null, this.MaxArchiveFiles));
			}
			return fileArchiveMode;
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x0000DE04 File Offset: 0x0000C004
		public FileTarget()
		{
			this.ArchiveNumbering = ArchiveNumberingMode.Sequence;
			this._maxArchiveFiles = 0;
			this.ConcurrentWriteAttemptDelay = 1;
			this.ArchiveEvery = FileArchivePeriod.None;
			this.ArchiveAboveSize = -1L;
			this.ConcurrentWriteAttempts = 10;
			this.ConcurrentWrites = true;
			this.Encoding = Encoding.Default;
			this.BufferSize = 32768;
			this.AutoFlush = true;
			this.FileAttributes = Win32FileAttributes.Normal;
			this.LineEnding = LineEndingMode.Default;
			this.EnableFileDelete = true;
			this.OpenFileCacheTimeout = -1;
			this.OpenFileCacheSize = 5;
			this.CreateDirs = true;
			this.ForceManaged = false;
			this.ArchiveDateFormat = string.Empty;
			this._fileAppenderCache = FileAppenderCache.Empty;
			this.CleanupFileName = true;
			this.WriteFooterOnArchivingOnly = false;
			base.OptimizeBufferReuse = base.GetType() == typeof(FileTarget);
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x0000DF2B File Offset: 0x0000C12B
		public FileTarget(string name)
			: this()
		{
			base.Name = name;
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000601 RID: 1537 RVA: 0x0000DF3A File Offset: 0x0000C13A
		// (set) Token: 0x06000602 RID: 1538 RVA: 0x0000DF4D File Offset: 0x0000C14D
		[RequiredParameter]
		public Layout FileName
		{
			get
			{
				FilePathLayout fullFileName = this._fullFileName;
				if (fullFileName == null)
				{
					return null;
				}
				return fullFileName.GetLayout();
			}
			set
			{
				this._fullFileName = this.CreateFileNameLayout(value);
				this.ResetFileAppenders("FileName Changed");
			}
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x0000DF67 File Offset: 0x0000C167
		private FilePathLayout CreateFileNameLayout(Layout value)
		{
			if (value == null)
			{
				return null;
			}
			return new FilePathLayout(value, this.CleanupFileName, this.FileNameKind);
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000604 RID: 1540 RVA: 0x0000DF80 File Offset: 0x0000C180
		// (set) Token: 0x06000605 RID: 1541 RVA: 0x0000DF88 File Offset: 0x0000C188
		[DefaultValue(true)]
		public bool CleanupFileName
		{
			get
			{
				return this._cleanupFileName;
			}
			set
			{
				if (this._cleanupFileName != value)
				{
					this._cleanupFileName = value;
					this._fullFileName = this.CreateFileNameLayout(this.FileName);
					this._fullArchiveFileName = this.CreateFileNameLayout(this.ArchiveFileName);
					this.ResetFileAppenders("CleanupFileName Changed");
				}
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000606 RID: 1542 RVA: 0x0000DFD4 File Offset: 0x0000C1D4
		// (set) Token: 0x06000607 RID: 1543 RVA: 0x0000DFDC File Offset: 0x0000C1DC
		[DefaultValue(FilePathKind.Unknown)]
		public FilePathKind FileNameKind
		{
			get
			{
				return this._fileNameKind;
			}
			set
			{
				if (this._fileNameKind != value)
				{
					this._fileNameKind = value;
					this._fullFileName = this.CreateFileNameLayout(this.FileName);
					this.ResetFileAppenders("FileNameKind Changed");
				}
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000608 RID: 1544 RVA: 0x0000E00B File Offset: 0x0000C20B
		// (set) Token: 0x06000609 RID: 1545 RVA: 0x0000E013 File Offset: 0x0000C213
		[DefaultValue(true)]
		[Advanced]
		public bool CreateDirs { get; set; }

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x0600060A RID: 1546 RVA: 0x0000E01C File Offset: 0x0000C21C
		// (set) Token: 0x0600060B RID: 1547 RVA: 0x0000E024 File Offset: 0x0000C224
		[DefaultValue(false)]
		public bool DeleteOldFileOnStartup { get; set; }

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x0600060C RID: 1548 RVA: 0x0000E02D File Offset: 0x0000C22D
		// (set) Token: 0x0600060D RID: 1549 RVA: 0x0000E035 File Offset: 0x0000C235
		[DefaultValue(false)]
		[Advanced]
		public bool ReplaceFileContentsOnEachWrite { get; set; }

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x0600060E RID: 1550 RVA: 0x0000E03E File Offset: 0x0000C23E
		// (set) Token: 0x0600060F RID: 1551 RVA: 0x0000E046 File Offset: 0x0000C246
		[DefaultValue(false)]
		public bool KeepFileOpen
		{
			get
			{
				return this._keepFileOpen;
			}
			set
			{
				if (this._keepFileOpen != value)
				{
					this._keepFileOpen = value;
					this.ResetFileAppenders("KeepFileOpen Changed");
				}
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000610 RID: 1552 RVA: 0x0000E063 File Offset: 0x0000C263
		// (set) Token: 0x06000611 RID: 1553 RVA: 0x0000E06B File Offset: 0x0000C26B
		[Obsolete("This option will be removed in NLog 5. Marked obsolete on NLog 4.5")]
		[DefaultValue(0)]
		public int maxLogFilenames { get; set; }

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000612 RID: 1554 RVA: 0x0000E074 File Offset: 0x0000C274
		// (set) Token: 0x06000613 RID: 1555 RVA: 0x0000E07C File Offset: 0x0000C27C
		[DefaultValue(true)]
		public bool EnableFileDelete { get; set; }

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000614 RID: 1556 RVA: 0x0000E085 File Offset: 0x0000C285
		// (set) Token: 0x06000615 RID: 1557 RVA: 0x0000E08D File Offset: 0x0000C28D
		[Advanced]
		public Win32FileAttributes FileAttributes { get; set; }

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000616 RID: 1558 RVA: 0x0000E096 File Offset: 0x0000C296
		bool ICreateFileParameters.IsArchivingEnabled
		{
			get
			{
				return this.IsArchivingEnabled;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000617 RID: 1559 RVA: 0x0000E09E File Offset: 0x0000C29E
		// (set) Token: 0x06000618 RID: 1560 RVA: 0x0000E0A6 File Offset: 0x0000C2A6
		[Advanced]
		public LineEndingMode LineEnding { get; set; }

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000619 RID: 1561 RVA: 0x0000E0AF File Offset: 0x0000C2AF
		// (set) Token: 0x0600061A RID: 1562 RVA: 0x0000E0B7 File Offset: 0x0000C2B7
		[DefaultValue(true)]
		public bool AutoFlush { get; set; }

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x0600061B RID: 1563 RVA: 0x0000E0C0 File Offset: 0x0000C2C0
		// (set) Token: 0x0600061C RID: 1564 RVA: 0x0000E0C8 File Offset: 0x0000C2C8
		[DefaultValue(5)]
		[Advanced]
		public int OpenFileCacheSize { get; set; }

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x0600061D RID: 1565 RVA: 0x0000E0D1 File Offset: 0x0000C2D1
		// (set) Token: 0x0600061E RID: 1566 RVA: 0x0000E0D9 File Offset: 0x0000C2D9
		[DefaultValue(-1)]
		[Advanced]
		public int OpenFileCacheTimeout { get; set; }

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x0600061F RID: 1567 RVA: 0x0000E0E2 File Offset: 0x0000C2E2
		// (set) Token: 0x06000620 RID: 1568 RVA: 0x0000E0EA File Offset: 0x0000C2EA
		public int OpenFileFlushTimeout { get; set; }

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000621 RID: 1569 RVA: 0x0000E0F3 File Offset: 0x0000C2F3
		// (set) Token: 0x06000622 RID: 1570 RVA: 0x0000E0FB File Offset: 0x0000C2FB
		[DefaultValue(32768)]
		public int BufferSize { get; set; }

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000623 RID: 1571 RVA: 0x0000E104 File Offset: 0x0000C304
		// (set) Token: 0x06000624 RID: 1572 RVA: 0x0000E10C File Offset: 0x0000C30C
		public Encoding Encoding { get; set; }

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000625 RID: 1573 RVA: 0x0000E115 File Offset: 0x0000C315
		// (set) Token: 0x06000626 RID: 1574 RVA: 0x0000E11D File Offset: 0x0000C31D
		[DefaultValue(false)]
		[Advanced]
		public bool DiscardAll { get; set; }

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000627 RID: 1575 RVA: 0x0000E128 File Offset: 0x0000C328
		// (set) Token: 0x06000628 RID: 1576 RVA: 0x0000E154 File Offset: 0x0000C354
		[DefaultValue(true)]
		public bool ConcurrentWrites
		{
			get
			{
				bool? concurrentWrites = this._concurrentWrites;
				if (concurrentWrites == null)
				{
					return PlatformDetector.SupportsSharableMutex;
				}
				return concurrentWrites.GetValueOrDefault();
			}
			set
			{
				bool? concurrentWrites = this._concurrentWrites;
				if (!((concurrentWrites.GetValueOrDefault() == value) & (concurrentWrites != null)))
				{
					this._concurrentWrites = new bool?(value);
					this.ResetFileAppenders("ConcurrentWrites Changed");
				}
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000629 RID: 1577 RVA: 0x0000E195 File Offset: 0x0000C395
		// (set) Token: 0x0600062A RID: 1578 RVA: 0x0000E19D File Offset: 0x0000C39D
		[DefaultValue(false)]
		public bool NetworkWrites { get; set; }

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x0600062B RID: 1579 RVA: 0x0000E1A6 File Offset: 0x0000C3A6
		// (set) Token: 0x0600062C RID: 1580 RVA: 0x0000E1AE File Offset: 0x0000C3AE
		[DefaultValue(false)]
		public bool WriteBom { get; set; }

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x0600062D RID: 1581 RVA: 0x0000E1B7 File Offset: 0x0000C3B7
		// (set) Token: 0x0600062E RID: 1582 RVA: 0x0000E1BF File Offset: 0x0000C3BF
		[DefaultValue(10)]
		[Advanced]
		public int ConcurrentWriteAttempts { get; set; }

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x0600062F RID: 1583 RVA: 0x0000E1C8 File Offset: 0x0000C3C8
		// (set) Token: 0x06000630 RID: 1584 RVA: 0x0000E1D0 File Offset: 0x0000C3D0
		[DefaultValue(1)]
		[Advanced]
		public int ConcurrentWriteAttemptDelay { get; set; }

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000631 RID: 1585 RVA: 0x0000E1D9 File Offset: 0x0000C3D9
		// (set) Token: 0x06000632 RID: 1586 RVA: 0x0000E1E1 File Offset: 0x0000C3E1
		[DefaultValue(false)]
		public bool ArchiveOldFileOnStartup { get; set; }

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000633 RID: 1587 RVA: 0x0000E1EA File Offset: 0x0000C3EA
		// (set) Token: 0x06000634 RID: 1588 RVA: 0x0000E1F2 File Offset: 0x0000C3F2
		[DefaultValue("")]
		public string ArchiveDateFormat
		{
			get
			{
				return this._archiveDateFormat;
			}
			set
			{
				if (this._archiveDateFormat != value)
				{
					this._archiveDateFormat = value;
					this.ResetFileAppenders("ArchiveDateFormat Changed");
				}
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000635 RID: 1589 RVA: 0x0000E214 File Offset: 0x0000C414
		// (set) Token: 0x06000636 RID: 1590 RVA: 0x0000E21C File Offset: 0x0000C41C
		public long ArchiveAboveSize
		{
			get
			{
				return this._archiveAboveSize;
			}
			set
			{
				if (this._archiveAboveSize == -1L != (value == -1L))
				{
					this._archiveAboveSize = value;
					this.ResetFileAppenders("ArchiveAboveSize Changed");
					return;
				}
				this._archiveAboveSize = value;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000637 RID: 1591 RVA: 0x0000E249 File Offset: 0x0000C449
		// (set) Token: 0x06000638 RID: 1592 RVA: 0x0000E251 File Offset: 0x0000C451
		public FileArchivePeriod ArchiveEvery
		{
			get
			{
				return this._archiveEvery;
			}
			set
			{
				if (this._archiveEvery != value)
				{
					this._archiveEvery = value;
					this.ResetFileAppenders("ArchiveEvery Changed");
				}
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000639 RID: 1593 RVA: 0x0000E26E File Offset: 0x0000C46E
		// (set) Token: 0x0600063A RID: 1594 RVA: 0x0000E276 File Offset: 0x0000C476
		public FilePathKind ArchiveFileKind
		{
			get
			{
				return this._archiveFileKind;
			}
			set
			{
				if (this._archiveFileKind != value)
				{
					this._archiveFileKind = value;
					this._fullArchiveFileName = this.CreateFileNameLayout(this.ArchiveFileName);
					this.ResetFileAppenders("ArchiveFileKind Changed");
				}
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x0600063B RID: 1595 RVA: 0x0000E2A5 File Offset: 0x0000C4A5
		// (set) Token: 0x0600063C RID: 1596 RVA: 0x0000E2BC File Offset: 0x0000C4BC
		public Layout ArchiveFileName
		{
			get
			{
				if (this._fullArchiveFileName == null)
				{
					return null;
				}
				return this._fullArchiveFileName.GetLayout();
			}
			set
			{
				this._fullArchiveFileName = this.CreateFileNameLayout(value);
				this.ResetFileAppenders("ArchiveFileName Changed");
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x0600063D RID: 1597 RVA: 0x0000E2D6 File Offset: 0x0000C4D6
		// (set) Token: 0x0600063E RID: 1598 RVA: 0x0000E2DE File Offset: 0x0000C4DE
		[DefaultValue(0)]
		public int MaxArchiveFiles
		{
			get
			{
				return this._maxArchiveFiles;
			}
			set
			{
				if (this._maxArchiveFiles != value)
				{
					this._maxArchiveFiles = value;
					this.ResetFileAppenders("MaxArchiveFiles Changed");
				}
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x0600063F RID: 1599 RVA: 0x0000E2FB File Offset: 0x0000C4FB
		// (set) Token: 0x06000640 RID: 1600 RVA: 0x0000E303 File Offset: 0x0000C503
		public ArchiveNumberingMode ArchiveNumbering
		{
			get
			{
				return this._archiveNumbering;
			}
			set
			{
				if (this._archiveNumbering != value)
				{
					this._archiveNumbering = value;
					this.ResetFileAppenders("ArchiveNumbering Changed");
				}
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000641 RID: 1601 RVA: 0x0000E320 File Offset: 0x0000C520
		// (set) Token: 0x06000642 RID: 1602 RVA: 0x0000E327 File Offset: 0x0000C527
		public static IFileCompressor FileCompressor { get; set; } = new ZipArchiveFileCompressor();

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000643 RID: 1603 RVA: 0x0000E32F File Offset: 0x0000C52F
		// (set) Token: 0x06000644 RID: 1604 RVA: 0x0000E343 File Offset: 0x0000C543
		[DefaultValue(false)]
		public bool EnableArchiveFileCompression
		{
			get
			{
				return this._enableArchiveFileCompression && FileTarget.FileCompressor != null;
			}
			set
			{
				if (this._enableArchiveFileCompression != value)
				{
					this._enableArchiveFileCompression = value;
					this.ResetFileAppenders("EnableArchiveFileCompression Changed");
				}
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000645 RID: 1605 RVA: 0x0000E360 File Offset: 0x0000C560
		// (set) Token: 0x06000646 RID: 1606 RVA: 0x0000E368 File Offset: 0x0000C568
		[DefaultValue(false)]
		public bool ForceManaged { get; set; }

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000647 RID: 1607 RVA: 0x0000E371 File Offset: 0x0000C571
		// (set) Token: 0x06000648 RID: 1608 RVA: 0x0000E379 File Offset: 0x0000C579
		[DefaultValue(false)]
		public bool ForceMutexConcurrentWrites { get; set; }

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000649 RID: 1609 RVA: 0x0000E382 File Offset: 0x0000C582
		// (set) Token: 0x0600064A RID: 1610 RVA: 0x0000E38A File Offset: 0x0000C58A
		[DefaultValue(false)]
		public bool WriteFooterOnArchivingOnly { get; set; }

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x0600064B RID: 1611 RVA: 0x0000E393 File Offset: 0x0000C593
		protected internal string NewLineChars
		{
			get
			{
				return this.LineEnding.NewLineCharacters;
			}
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x0000E3A0 File Offset: 0x0000C5A0
		private void RefreshArchiveFilePatternToWatch(string fileName, LogEventInfo logEvent)
		{
			if (this._fileAppenderCache != null)
			{
				this._fileAppenderCache.CheckCloseAppenders -= this.AutoCloseAppendersAfterArchive;
				object obj = this.IsArchivingEnabled && this.KeepFileOpen && this.ConcurrentWrites;
				bool flag = this.KeepFileOpen && this.EnableFileDelete && !this.NetworkWrites && !this.ReplaceFileContentsOnEachWrite && !this.EnableFileDeleteSimpleMonitor;
				object obj2 = obj;
				if ((obj2 | flag) != null)
				{
					this._fileAppenderCache.CheckCloseAppenders += this.AutoCloseAppendersAfterArchive;
				}
				if (obj2 != null)
				{
					string archiveFileNamePattern = this.GetArchiveFileNamePattern(fileName, logEvent);
					string text = ((((!string.IsNullOrEmpty(archiveFileNamePattern)) ? this.GetFileArchiveHelper(archiveFileNamePattern) : null) != null) ? this._fileArchiveHelper.GenerateFileNameMask(archiveFileNamePattern) : string.Empty);
					string text2 = ((!string.IsNullOrEmpty(text)) ? Path.Combine(Path.GetDirectoryName(archiveFileNamePattern), text) : string.Empty);
					this._fileAppenderCache.ArchiveFilePatternToWatch = text2;
					return;
				}
				this._fileAppenderCache.ArchiveFilePatternToWatch = null;
			}
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x0000E49C File Offset: 0x0000C69C
		public void CleanupInitializedFiles()
		{
			this.CleanupInitializedFiles(TimeSource.Current.Time.AddDays(-2.0));
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x0000E4CC File Offset: 0x0000C6CC
		public void CleanupInitializedFiles(DateTime cleanupThreshold)
		{
			if (InternalLogger.IsTraceEnabled)
			{
				InternalLogger.Trace<string, DateTime>("FileTarget(Name={0}): Cleanup Initialized Files with cleanupThreshold {1}", base.Name, cleanupThreshold);
			}
			List<string> list = null;
			foreach (KeyValuePair<string, DateTime> keyValuePair in this._initializedFiles)
			{
				if (keyValuePair.Value < cleanupThreshold)
				{
					if (list == null)
					{
						list = new List<string>();
					}
					list.Add(keyValuePair.Key);
				}
			}
			if (list != null)
			{
				foreach (string text in list)
				{
					this.FinalizeFile(text, false);
				}
			}
			InternalLogger.Trace<string>("FileTarget(Name={0}): CleanupInitializedFiles Done", base.Name);
		}

		// Token: 0x0600064F RID: 1615 RVA: 0x0000E5AC File Offset: 0x0000C7AC
		protected override void FlushAsync(AsyncContinuation asyncContinuation)
		{
			try
			{
				InternalLogger.Trace<string>("FileTarget(Name={0}): FlushAsync", base.Name);
				this._fileAppenderCache.FlushAppenders();
				asyncContinuation(null);
				InternalLogger.Trace<string>("FileTarget(Name={0}): FlushAsync Done", base.Name);
			}
			catch (Exception ex)
			{
				InternalLogger.Warn(ex, "FileTarget(Name={0}): Exception in FlushAsync", new object[] { base.Name });
				if (ex.MustBeRethrown())
				{
					throw;
				}
				asyncContinuation(ex);
			}
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x0000E62C File Offset: 0x0000C82C
		private IFileAppenderFactory GetFileAppenderFactory()
		{
			if (this.DiscardAll)
			{
				return NullAppender.TheFactory;
			}
			if (!this.KeepFileOpen)
			{
				return RetryingMultiProcessFileAppender.TheFactory;
			}
			if (this.NetworkWrites)
			{
				return RetryingMultiProcessFileAppender.TheFactory;
			}
			if (this.ConcurrentWrites)
			{
				if (!this.ForceMutexConcurrentWrites && PlatformDetector.IsWin32 && !PlatformDetector.IsMono)
				{
					return WindowsMultiProcessFileAppender.TheFactory;
				}
				if (PlatformDetector.SupportsSharableMutex)
				{
					return MutexMultiProcessFileAppender.TheFactory;
				}
				return RetryingMultiProcessFileAppender.TheFactory;
			}
			else
			{
				if (this.IsArchivingEnabled)
				{
					return CountingSingleProcessFileAppender.TheFactory;
				}
				return SingleProcessFileAppender.TheFactory;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000651 RID: 1617 RVA: 0x0000E6AD File Offset: 0x0000C8AD
		private bool IsArchivingEnabled
		{
			get
			{
				return this.ArchiveAboveSize != -1L || this.ArchiveEvery > FileArchivePeriod.None;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000652 RID: 1618 RVA: 0x0000E6C4 File Offset: 0x0000C8C4
		private bool IsSimpleKeepFileOpen
		{
			get
			{
				return this.KeepFileOpen && !this.NetworkWrites && !this.ReplaceFileContentsOnEachWrite && !this.ConcurrentWrites;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000653 RID: 1619 RVA: 0x0000E6E9 File Offset: 0x0000C8E9
		private bool EnableFileDeleteSimpleMonitor
		{
			get
			{
				return this.EnableFileDelete && !PlatformDetector.IsWin32 && this.IsSimpleKeepFileOpen;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000654 RID: 1620 RVA: 0x0000E702 File Offset: 0x0000C902
		bool ICreateFileParameters.EnableFileDeleteSimpleMonitor
		{
			get
			{
				return this.EnableFileDeleteSimpleMonitor;
			}
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x0000E70C File Offset: 0x0000C90C
		protected override void InitializeTarget()
		{
			base.InitializeTarget();
			IFileAppenderFactory fileAppenderFactory = this.GetFileAppenderFactory();
			if (InternalLogger.IsTraceEnabled)
			{
				InternalLogger.Trace<string, Type>("FileTarget(Name={0}): Using appenderFactory: {1}", base.Name, fileAppenderFactory.GetType());
			}
			this._fileAppenderCache = new FileAppenderCache(this.OpenFileCacheSize, fileAppenderFactory, this);
			if ((this.OpenFileCacheSize > 0 || this.EnableFileDelete) && (this.OpenFileCacheTimeout > 0 || this.OpenFileFlushTimeout > 0))
			{
				int num = ((this.OpenFileCacheTimeout > 0 && this.OpenFileFlushTimeout > 0) ? Math.Min(this.OpenFileCacheTimeout, this.OpenFileFlushTimeout) : Math.Max(this.OpenFileCacheTimeout, this.OpenFileFlushTimeout));
				InternalLogger.Trace<string>("FileTarget(Name={0}): Start autoClosingTimer", base.Name);
				this._autoClosingTimer = new Timer(delegate(object state)
				{
					this.AutoClosingTimerCallback(this, EventArgs.Empty);
				}, null, num * 1000, num * 1000);
			}
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x0000E7E8 File Offset: 0x0000C9E8
		protected override void CloseTarget()
		{
			base.CloseTarget();
			foreach (string text in new List<string>(this._initializedFiles.Keys))
			{
				this.FinalizeFile(text, false);
			}
			this._fileArchiveHelper = null;
			Timer autoClosingTimer = this._autoClosingTimer;
			if (autoClosingTimer != null)
			{
				InternalLogger.Trace<string>("FileTarget(Name={0}): Stop autoClosingTimer", base.Name);
				this._autoClosingTimer = null;
				autoClosingTimer.WaitForDispose(TimeSpan.Zero);
			}
			this._fileAppenderCache.CloseAppenders("Dispose");
			this._fileAppenderCache.Dispose();
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x0000E89C File Offset: 0x0000CA9C
		private void ResetFileAppenders(string reason)
		{
			this._fileArchiveHelper = null;
			if (base.IsInitialized)
			{
				this._fileAppenderCache.CloseAppenders(reason);
				this._initializedFiles.Clear();
			}
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x0000E8C4 File Offset: 0x0000CAC4
		protected override void Write(LogEventInfo logEvent)
		{
			string fullFileName = this.GetFullFileName(logEvent);
			if (string.IsNullOrEmpty(fullFileName))
			{
				throw new ArgumentException("The path is not of a legal form.");
			}
			if (base.OptimizeBufferReuse)
			{
				using (ReusableObjectCreator<MemoryStream>.LockOject lockOject = this._reusableFileWriteStream.Allocate())
				{
					using (ReusableObjectCreator<StringBuilder>.LockOject lockOject2 = this.ReusableLayoutBuilder.Allocate())
					{
						using (ReusableObjectCreator<char[]>.LockOject lockOject3 = this._reusableEncodingBuffer.Allocate())
						{
							this.RenderFormattedMessageToStream(logEvent, lockOject2.Result, lockOject3.Result, lockOject.Result);
						}
					}
					this.ProcessLogEvent(logEvent, fullFileName, new ArraySegment<byte>(lockOject.Result.GetBuffer(), 0, (int)lockOject.Result.Length));
					return;
				}
			}
			byte[] bytesToWrite = this.GetBytesToWrite(logEvent);
			this.ProcessLogEvent(logEvent, fullFileName, new ArraySegment<byte>(bytesToWrite));
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x0000E9C8 File Offset: 0x0000CBC8
		internal string GetFullFileName(LogEventInfo logEvent)
		{
			if (this._fullFileName == null)
			{
				return null;
			}
			if (base.OptimizeBufferReuse)
			{
				using (ReusableObjectCreator<StringBuilder>.LockOject lockOject = this.ReusableLayoutBuilder.Allocate())
				{
					return this._fullFileName.RenderWithBuilder(logEvent, lockOject.Result);
				}
			}
			return this._fullFileName.Render(logEvent);
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x0000EA34 File Offset: 0x0000CC34
		[Obsolete("Instead override Write(IList<AsyncLogEventInfo> logEvents. Marked obsolete on NLog 4.5")]
		protected override void Write(AsyncLogEventInfo[] logEvents)
		{
			this.Write(logEvents);
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x0000EA40 File Offset: 0x0000CC40
		protected override void Write(IList<AsyncLogEventInfo> logEvents)
		{
			if (this._getFullFileNameDelegate == null)
			{
				this._getFullFileNameDelegate = (AsyncLogEventInfo c) => this.GetFullFileName(c.LogEvent);
			}
			SortHelpers.ReadOnlySingleBucketDictionary<string, IList<AsyncLogEventInfo>> readOnlySingleBucketDictionary = logEvents.BucketSort(this._getFullFileNameDelegate);
			using (ReusableObjectCreator<MemoryStream>.LockOject lockOject = ((base.OptimizeBufferReuse && logEvents.Count <= 1000) ? this._reusableAsyncFileWriteStream.Allocate() : this._reusableAsyncFileWriteStream.None))
			{
				using (MemoryStream memoryStream = ((lockOject.Result != null) ? null : new MemoryStream()))
				{
					MemoryStream memoryStream2 = memoryStream ?? lockOject.Result;
					foreach (KeyValuePair<string, IList<AsyncLogEventInfo>> keyValuePair in readOnlySingleBucketDictionary)
					{
						int count = keyValuePair.Value.Count;
						if (count > 0)
						{
							string key = keyValuePair.Key;
							if (string.IsNullOrEmpty(key))
							{
								ArgumentException ex = new ArgumentException("The path is not of a legal form.");
								for (int i = 0; i < count; i++)
								{
									keyValuePair.Value[i].Continuation(ex);
								}
							}
							else
							{
								memoryStream2.SetLength(0L);
								memoryStream2.Position = 0L;
								this.WriteToMemoryStream(keyValuePair.Value, memoryStream2);
								Exception ex2;
								this.FlushCurrentFileWrites(key, keyValuePair.Value[0].LogEvent, memoryStream2, out ex2);
								for (int j = 0; j < count; j++)
								{
									keyValuePair.Value[j].Continuation(ex2);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x0000EC2C File Offset: 0x0000CE2C
		private void WriteToMemoryStream(IList<AsyncLogEventInfo> logEvents, MemoryStream ms)
		{
			int count = logEvents.Count;
			using (ReusableObjectCreator<StringBuilder>.LockOject lockOject = (base.OptimizeBufferReuse ? this.ReusableLayoutBuilder.Allocate() : this.ReusableLayoutBuilder.None))
			{
				using (ReusableObjectCreator<char[]>.LockOject lockOject2 = (base.OptimizeBufferReuse ? this._reusableEncodingBuffer.Allocate() : this._reusableEncodingBuffer.None))
				{
					using (ReusableObjectCreator<MemoryStream>.LockOject lockOject3 = (base.OptimizeBufferReuse ? this._reusableFileWriteStream.Allocate() : this._reusableFileWriteStream.None))
					{
						bool flag = lockOject.Result != null && lockOject3.Result != null;
						for (int i = 0; i < count; i++)
						{
							AsyncLogEventInfo asyncLogEventInfo = logEvents[i];
							if (flag)
							{
								lockOject3.Result.Position = 0L;
								lockOject3.Result.SetLength(0L);
								lockOject.Result.ClearBuilder();
								this.RenderFormattedMessageToStream(asyncLogEventInfo.LogEvent, lockOject.Result, lockOject2.Result, lockOject3.Result);
								ms.Write(lockOject3.Result.GetBuffer(), 0, (int)lockOject3.Result.Length);
							}
							else
							{
								byte[] bytesToWrite = this.GetBytesToWrite(asyncLogEventInfo.LogEvent);
								if (ms.Capacity == 0)
								{
									ms.Capacity = this.GetMemoryStreamInitialSize(count, bytesToWrite.Length);
								}
								ms.Write(bytesToWrite, 0, bytesToWrite.Length);
							}
						}
					}
				}
			}
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x0000EDF0 File Offset: 0x0000CFF0
		private int GetMemoryStreamInitialSize(int eventsCount, int firstEventSize)
		{
			if (eventsCount > 10)
			{
				return (1 + eventsCount) * (firstEventSize / 1024 + 1) * 1024;
			}
			if (eventsCount > 1)
			{
				return (1 + eventsCount) * firstEventSize;
			}
			return firstEventSize;
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x0000EE18 File Offset: 0x0000D018
		private void ProcessLogEvent(LogEventInfo logEvent, string fileName, ArraySegment<byte> bytesToWrite)
		{
			DateTime dateTime = this.InitializeFile(fileName, logEvent);
			bool flag = dateTime == DateTime.MinValue;
			if (flag && fileName == this._previousLogFileName && this._previousLogEventTimestamp != null)
			{
				dateTime = this._previousLogEventTimestamp.Value;
			}
			if (this.TryArchiveFile(fileName, logEvent, bytesToWrite.Count, dateTime, flag))
			{
				flag = this.InitializeFile(fileName, logEvent) == DateTime.MinValue;
			}
			this.WriteToFile(fileName, bytesToWrite, flag);
			this._previousLogFileName = fileName;
			this._previousLogEventTimestamp = new DateTime?(logEvent.TimeStamp);
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x0000EEAB File Offset: 0x0000D0AB
		protected virtual string GetFormattedMessage(LogEventInfo logEvent)
		{
			return this.Layout.Render(logEvent);
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x0000EEBC File Offset: 0x0000D0BC
		protected virtual byte[] GetBytesToWrite(LogEventInfo logEvent)
		{
			string formattedMessage = this.GetFormattedMessage(logEvent);
			int byteCount = this.Encoding.GetByteCount(formattedMessage);
			int byteCount2 = this.Encoding.GetByteCount(this.NewLineChars);
			byte[] array = new byte[byteCount + byteCount2];
			this.Encoding.GetBytes(formattedMessage, 0, formattedMessage.Length, array, 0);
			this.Encoding.GetBytes(this.NewLineChars, 0, this.NewLineChars.Length, array, byteCount);
			return this.TransformBytes(array);
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x0000EF36 File Offset: 0x0000D136
		protected virtual byte[] TransformBytes(byte[] value)
		{
			return value;
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x0000EF39 File Offset: 0x0000D139
		protected virtual void RenderFormattedMessageToStream(LogEventInfo logEvent, StringBuilder formatBuilder, char[] transformBuffer, MemoryStream streamTarget)
		{
			this.RenderFormattedMessage(logEvent, formatBuilder);
			formatBuilder.Append(this.NewLineChars);
			this.TransformBuilderToStream(logEvent, formatBuilder, transformBuffer, streamTarget);
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x0000EF5B File Offset: 0x0000D15B
		protected virtual void RenderFormattedMessage(LogEventInfo logEvent, StringBuilder target)
		{
			this.Layout.RenderAppendBuilder(logEvent, target, false);
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x0000EF6B File Offset: 0x0000D16B
		private void TransformBuilderToStream(LogEventInfo logEvent, StringBuilder builder, char[] transformBuffer, MemoryStream workStream)
		{
			builder.CopyToStream(workStream, this.Encoding, transformBuffer);
			this.TransformStream(logEvent, workStream);
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x0000EF85 File Offset: 0x0000D185
		protected virtual void TransformStream(LogEventInfo logEvent, MemoryStream stream)
		{
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x0000EF88 File Offset: 0x0000D188
		private void FlushCurrentFileWrites(string currentFileName, LogEventInfo firstLogEvent, MemoryStream ms, out Exception lastException)
		{
			lastException = null;
			try
			{
				if (currentFileName != null)
				{
					ArraySegment<byte> arraySegment = new ArraySegment<byte>(ms.GetBuffer(), 0, (int)ms.Length);
					this.ProcessLogEvent(firstLogEvent, currentFileName, arraySegment);
				}
			}
			catch (Exception ex)
			{
				if (ex.MustBeRethrown())
				{
					throw;
				}
				lastException = ex;
			}
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x0000EFE0 File Offset: 0x0000D1E0
		private void ArchiveFile(string fileName, string archiveFileName)
		{
			string directoryName = Path.GetDirectoryName(archiveFileName);
			if (directoryName != null && !Directory.Exists(directoryName))
			{
				Directory.CreateDirectory(directoryName);
			}
			if (string.Equals(fileName, archiveFileName, StringComparison.OrdinalIgnoreCase))
			{
				InternalLogger.Info<string, string>("FileTarget(Name={0}): Archiving {1} skipped as ArchiveFileName equals FileName", base.Name, fileName);
				return;
			}
			if (this.EnableArchiveFileCompression)
			{
				InternalLogger.Info<string, string, string>("FileTarget(Name={0}): Archiving {1} to compressed {2}", base.Name, fileName, archiveFileName);
				FileTarget.FileCompressor.CompressFile(fileName, archiveFileName);
				this.DeleteAndWaitForFileDelete(fileName);
				return;
			}
			InternalLogger.Info<string, string, string>("FileTarget(Name={0}): Archiving {1} to {2}", base.Name, fileName, archiveFileName);
			if (File.Exists(archiveFileName))
			{
				this.ArchiveFileAppendExisting(fileName, archiveFileName);
				return;
			}
			this.ArchiveFileMove(fileName, archiveFileName);
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x0000F07C File Offset: 0x0000D27C
		private void ArchiveFileAppendExisting(string fileName, string archiveFileName)
		{
			InternalLogger.Info<string, string>("FileTarget(Name={0}): Already exists, append to {1}", base.Name, archiveFileName);
			FileShare fileShare = FileShare.ReadWrite;
			if (this.EnableFileDelete)
			{
				fileShare |= FileShare.Delete;
			}
			using (FileStream fileStream = File.Open(fileName, FileMode.Open, FileAccess.ReadWrite, fileShare))
			{
				using (FileStream fileStream2 = File.Open(archiveFileName, FileMode.Append))
				{
					fileStream.CopyAndSkipBom(fileStream2, this.Encoding);
					fileStream.SetLength(0L);
					if (this.EnableFileDelete && !this.DeleteOldArchiveFile(fileName))
					{
						fileShare &= ~FileShare.Delete;
					}
					fileStream.Close();
					fileStream2.Flush(true);
				}
			}
			if ((fileShare & FileShare.Delete) == FileShare.None)
			{
				this.DeleteOldArchiveFile(fileName);
			}
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x0000F134 File Offset: 0x0000D334
		private void ArchiveFileMove(string fileName, string archiveFileName)
		{
			try
			{
				InternalLogger.Debug<string, string, string>("FileTarget(Name={0}): Move file from '{1}' to '{2}'", base.Name, fileName, archiveFileName);
				File.Move(fileName, archiveFileName);
			}
			catch (IOException ex)
			{
				if (this.IsSimpleKeepFileOpen)
				{
					throw;
				}
				if (!this.EnableFileDelete && this.KeepFileOpen)
				{
					throw;
				}
				if (!PlatformDetector.SupportsSharableMutex)
				{
					throw;
				}
				InternalLogger.Warn(ex, "FileTarget(Name={0}): Archiving failed. Checking for retry move of {1} to {2}.", new object[] { base.Name, fileName, archiveFileName });
				if (!File.Exists(fileName) || File.Exists(archiveFileName))
				{
					throw;
				}
				AsyncHelpers.WaitForDelay(TimeSpan.FromMilliseconds(50.0));
				if (!File.Exists(fileName) || File.Exists(archiveFileName))
				{
					throw;
				}
				InternalLogger.Debug<string, string, string>("FileTarget(Name={0}): Archiving retrying move of {1} to {2}.", base.Name, fileName, archiveFileName);
				File.Move(fileName, archiveFileName);
			}
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x0000F208 File Offset: 0x0000D408
		private bool DeleteOldArchiveFile(string fileName)
		{
			bool flag;
			try
			{
				InternalLogger.Info<string, string>("FileTarget(Name={0}): Deleting old archive file: '{1}'.", base.Name, fileName);
				File.Delete(fileName);
				flag = true;
			}
			catch (DirectoryNotFoundException ex)
			{
				InternalLogger.Debug(ex, "FileTarget(Name={0}): Failed to delete old log file '{1}' as directory is missing.", new object[] { base.Name, fileName });
				flag = false;
			}
			catch (Exception ex2)
			{
				InternalLogger.Warn(ex2, "FileTarget(Name={0}): Failed to delete old archive file: '{1}'.", new object[] { base.Name, fileName });
				if (ex2.MustBeRethrown())
				{
					throw;
				}
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x0000F29C File Offset: 0x0000D49C
		private void DeleteAndWaitForFileDelete(string fileName)
		{
			try
			{
				InternalLogger.Trace<string, string>("FileTarget(Name={0}): Waiting for file delete of '{1}' for 12 sec", base.Name, fileName);
				DateTime creationTime = new FileInfo(fileName).CreationTime;
				if (this.DeleteOldArchiveFile(fileName) && File.Exists(fileName))
				{
					for (int i = 0; i < 120; i++)
					{
						AsyncHelpers.WaitForDelay(TimeSpan.FromMilliseconds(100.0));
						FileInfo fileInfo = new FileInfo(fileName);
						if (!fileInfo.Exists || fileInfo.CreationTime != creationTime)
						{
							return;
						}
					}
					InternalLogger.Warn<string, string>("FileTarget(Name={0}): Timeout while deleting old archive file: '{1}'.", base.Name, fileName);
				}
			}
			catch (Exception ex)
			{
				InternalLogger.Warn(ex, "FileTarget(Name={0}): Failed to delete old archive file: '{1}'.", new object[] { base.Name, fileName });
				if (ex.MustBeRethrown())
				{
					throw;
				}
			}
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x0000F364 File Offset: 0x0000D564
		private string GetArchiveDateFormatString(string defaultFormat)
		{
			if (!string.IsNullOrEmpty(defaultFormat))
			{
				return defaultFormat;
			}
			switch (this.ArchiveEvery)
			{
			case FileArchivePeriod.Year:
				return "yyyy";
			case FileArchivePeriod.Month:
				return "yyyyMM";
			case FileArchivePeriod.Hour:
				return "yyyyMMddHH";
			case FileArchivePeriod.Minute:
				return "yyyyMMddHHmm";
			}
			return "yyyyMMdd";
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x0000F3C0 File Offset: 0x0000D5C0
		private DateTime? GetArchiveDate(string fileName, LogEventInfo logEvent, DateTime previousLogEventTimestamp)
		{
			DateTime? fileLastWriteTimeUtc = this._fileAppenderCache.GetFileLastWriteTimeUtc(fileName);
			InternalLogger.Trace<string, DateTime?, DateTime>("FileTarget(Name={0}): Calculating archive date. File-LastModifiedUtc: {1}; Previous LogEvent-TimeStamp: {2}", base.Name, fileLastWriteTimeUtc, previousLogEventTimestamp);
			if (fileLastWriteTimeUtc != null)
			{
				DateTime dateTime = TimeSource.Current.FromSystemTime(fileLastWriteTimeUtc.Value);
				if (previousLogEventTimestamp != DateTime.MinValue)
				{
					if (previousLogEventTimestamp > dateTime)
					{
						InternalLogger.Trace<string, DateTime, DateTime>("FileTarget(Name={0}): Using previous LogEvent-TimeStamp {1}, because more recent than File-LastModified {2}", base.Name, previousLogEventTimestamp, dateTime);
						return new DateTime?(previousLogEventTimestamp);
					}
					if (this.PreviousLogOverlappedPeriod(logEvent, previousLogEventTimestamp, dateTime))
					{
						InternalLogger.Trace<string, DateTime, DateTime>("FileTarget(Name={0}): Using previous LogEvent-TimeStamp {1}, because archive period is overlapping with File-LastModified {2}", base.Name, previousLogEventTimestamp, dateTime);
						return new DateTime?(previousLogEventTimestamp);
					}
					if (!this.AutoFlush && this.IsSimpleKeepFileOpen && previousLogEventTimestamp < dateTime)
					{
						InternalLogger.Trace<string, DateTime, DateTime>("FileTarget(Name={0}): Using previous LogEvent-TimeStamp {1}, because AutoFlush=false affects File-LastModified {2}", base.Name, previousLogEventTimestamp, dateTime);
						return new DateTime?(previousLogEventTimestamp);
					}
				}
				InternalLogger.Trace<string, DateTime>("FileTarget(Name={0}): Using last write time: {1}", base.Name, dateTime);
				return new DateTime?(dateTime);
			}
			if (previousLogEventTimestamp == DateTime.MinValue)
			{
				InternalLogger.Info<string, string>("FileTarget(Name={0}): Unable to acquire useful timestamp to archive file: {1}", base.Name, fileName);
				return null;
			}
			return new DateTime?(previousLogEventTimestamp);
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x0000F4D4 File Offset: 0x0000D6D4
		private bool PreviousLogOverlappedPeriod(LogEventInfo logEvent, DateTime previousLogEventTimestamp, DateTime lastFileWrite)
		{
			string archiveDateFormatString = this.GetArchiveDateFormatString(string.Empty);
			string text = lastFileWrite.ToString(archiveDateFormatString, CultureInfo.InvariantCulture);
			string text2 = logEvent.TimeStamp.ToString(archiveDateFormatString, CultureInfo.InvariantCulture);
			if (text != text2)
			{
				return false;
			}
			DateTime? dateTime = this.CalculateNextArchiveEventTime(previousLogEventTimestamp);
			if (dateTime == null)
			{
				return false;
			}
			string text3 = dateTime.Value.ToString(archiveDateFormatString, CultureInfo.InvariantCulture);
			return text == text3;
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x0000F550 File Offset: 0x0000D750
		private DateTime? CalculateNextArchiveEventTime(DateTime timestamp)
		{
			switch (this.ArchiveEvery)
			{
			case FileArchivePeriod.Year:
				return new DateTime?(timestamp.AddYears(1));
			case FileArchivePeriod.Month:
				return new DateTime?(timestamp.AddMonths(1));
			case FileArchivePeriod.Day:
				return new DateTime?(timestamp.AddDays(1.0));
			case FileArchivePeriod.Hour:
				return new DateTime?(timestamp.AddHours(1.0));
			case FileArchivePeriod.Minute:
				return new DateTime?(timestamp.AddMinutes(1.0));
			case FileArchivePeriod.Sunday:
				return new DateTime?(FileTarget.CalculateNextWeekday(timestamp, DayOfWeek.Sunday));
			case FileArchivePeriod.Monday:
				return new DateTime?(FileTarget.CalculateNextWeekday(timestamp, DayOfWeek.Monday));
			case FileArchivePeriod.Tuesday:
				return new DateTime?(FileTarget.CalculateNextWeekday(timestamp, DayOfWeek.Tuesday));
			case FileArchivePeriod.Wednesday:
				return new DateTime?(FileTarget.CalculateNextWeekday(timestamp, DayOfWeek.Wednesday));
			case FileArchivePeriod.Thursday:
				return new DateTime?(FileTarget.CalculateNextWeekday(timestamp, DayOfWeek.Thursday));
			case FileArchivePeriod.Friday:
				return new DateTime?(FileTarget.CalculateNextWeekday(timestamp, DayOfWeek.Friday));
			case FileArchivePeriod.Saturday:
				return new DateTime?(FileTarget.CalculateNextWeekday(timestamp, DayOfWeek.Saturday));
			default:
				return null;
			}
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x0000F664 File Offset: 0x0000D864
		public static DateTime CalculateNextWeekday(DateTime previousLogEventTimestamp, DayOfWeek dayOfWeek)
		{
			int dayOfWeek2 = (int)previousLogEventTimestamp.DayOfWeek;
			int num = (int)dayOfWeek;
			if (num <= dayOfWeek2)
			{
				num += 7;
			}
			return previousLogEventTimestamp.AddDays((double)(num - dayOfWeek2));
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x0000F690 File Offset: 0x0000D890
		private void DoAutoArchive(string fileName, LogEventInfo eventInfo, DateTime previousLogEventTimestamp, bool initializedNewFile)
		{
			InternalLogger.Debug<string, string>("FileTarget(Name={0}): Do archive file: '{1}'", base.Name, fileName);
			FileInfo fileInfo = new FileInfo(fileName);
			if (!fileInfo.Exists)
			{
				BaseFileAppender baseFileAppender = this._fileAppenderCache.InvalidateAppender(fileName);
				if (baseFileAppender == null)
				{
					return;
				}
				baseFileAppender.Dispose();
				return;
			}
			else
			{
				string archiveFileNamePattern = this.GetArchiveFileNamePattern(fileName, eventInfo);
				if (string.IsNullOrEmpty(archiveFileNamePattern))
				{
					InternalLogger.Warn<string>("FileTarget(Name={0}): Skip auto archive because archiveFilePattern is NULL", base.Name);
					return;
				}
				DateTime? archiveDate = this.GetArchiveDate(fileName, eventInfo, previousLogEventTimestamp);
				string text = this.GenerateArchiveFileNameAfterCleanup(fileName, fileInfo, archiveFileNamePattern, archiveDate, initializedNewFile);
				if (!string.IsNullOrEmpty(text))
				{
					this.ArchiveFile(fileInfo.FullName, text);
				}
				return;
			}
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x0000F724 File Offset: 0x0000D924
		private string GenerateArchiveFileNameAfterCleanup(string fileName, FileInfo fileInfo, string archiveFilePattern, DateTime? archiveDate, bool initializedNewFile)
		{
			InternalLogger.Trace<string, string>("FileTarget(Name={0}): Archive pattern '{1}'", base.Name, archiveFilePattern);
			IFileArchiveMode fileArchiveHelper = this.GetFileArchiveHelper(archiveFilePattern);
			List<DateAndSequenceArchive> existingArchiveFiles = fileArchiveHelper.GetExistingArchiveFiles(archiveFilePattern);
			if (this.MaxArchiveFiles == 1)
			{
				InternalLogger.Trace<string>("FileTarget(Name={0}): MaxArchiveFiles = 1", base.Name);
				for (int i = existingArchiveFiles.Count - 1; i >= 0; i--)
				{
					DateAndSequenceArchive dateAndSequenceArchive = existingArchiveFiles[i];
					if (!string.Equals(dateAndSequenceArchive.FileName, fileInfo.FullName, StringComparison.OrdinalIgnoreCase))
					{
						this.DeleteOldArchiveFile(dateAndSequenceArchive.FileName);
						existingArchiveFiles.RemoveAt(i);
					}
				}
				if (initializedNewFile && string.Equals(Path.GetDirectoryName(archiveFilePattern), fileInfo.DirectoryName, StringComparison.OrdinalIgnoreCase))
				{
					this.DeleteOldArchiveFile(fileName);
					return null;
				}
			}
			DateAndSequenceArchive dateAndSequenceArchive2 = ((archiveDate != null) ? fileArchiveHelper.GenerateArchiveFileName(archiveFilePattern, archiveDate.Value, existingArchiveFiles) : null);
			if (dateAndSequenceArchive2 == null)
			{
				return null;
			}
			if (!initializedNewFile)
			{
				this.FinalizeFile(fileName, true);
			}
			if (string.Equals(Path.GetDirectoryName(dateAndSequenceArchive2.FileName), fileInfo.DirectoryName, StringComparison.OrdinalIgnoreCase))
			{
				for (int j = 0; j < existingArchiveFiles.Count; j++)
				{
					if (string.Equals(existingArchiveFiles[j].FileName, fileInfo.FullName, StringComparison.OrdinalIgnoreCase))
					{
						existingArchiveFiles.RemoveAt(j);
						break;
					}
				}
			}
			existingArchiveFiles.Add(dateAndSequenceArchive2);
			foreach (DateAndSequenceArchive dateAndSequenceArchive3 in fileArchiveHelper.CheckArchiveCleanup(archiveFilePattern, existingArchiveFiles, this.MaxArchiveFiles))
			{
				this.DeleteOldArchiveFile(dateAndSequenceArchive3.FileName);
			}
			return dateAndSequenceArchive2.FileName;
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x0000F8B8 File Offset: 0x0000DAB8
		private string GetArchiveFileNamePattern(string fileName, LogEventInfo eventInfo)
		{
			if (this._fullArchiveFileName != null)
			{
				return this._fullArchiveFileName.Render(eventInfo);
			}
			if (this.EnableArchiveFileCompression)
			{
				return Path.ChangeExtension(fileName, ".zip");
			}
			return fileName;
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x0000F8E4 File Offset: 0x0000DAE4
		private bool TryArchiveFile(string fileName, LogEventInfo ev, int upcomingWriteSize, DateTime previousLogEventTimestamp, bool initializedNewFile)
		{
			if (!this.IsArchivingEnabled)
			{
				return false;
			}
			string text = string.Empty;
			BaseFileAppender baseFileAppender = null;
			try
			{
				text = this.GetArchiveFileName(fileName, ev, upcomingWriteSize, previousLogEventTimestamp);
				if (!string.IsNullOrEmpty(text))
				{
					InternalLogger.Trace<string, string>("FileTarget(Name={0}): Archive attempt for file '{1}'", base.Name, text);
					baseFileAppender = this._fileAppenderCache.InvalidateAppender(fileName);
					if (fileName != text)
					{
						BaseFileAppender baseFileAppender2 = this._fileAppenderCache.InvalidateAppender(text);
						baseFileAppender = baseFileAppender ?? baseFileAppender2;
					}
					if (!string.IsNullOrEmpty(this._previousLogFileName) && this._previousLogFileName != text && this._previousLogFileName != fileName)
					{
						BaseFileAppender baseFileAppender3 = this._fileAppenderCache.InvalidateAppender(this._previousLogFileName);
						baseFileAppender = baseFileAppender ?? baseFileAppender3;
					}
					this._fileAppenderCache.InvalidateAppendersForArchivedFiles();
				}
				else
				{
					this._fileAppenderCache.InvalidateAppendersForArchivedFiles();
				}
			}
			catch (Exception ex)
			{
				InternalLogger.Warn(ex, "FileTarget(Name={0}): Failed to check archive for file '{1}'.", new object[] { base.Name, fileName });
				if (ex.MustBeRethrown())
				{
					throw;
				}
			}
			if (!string.IsNullOrEmpty(text))
			{
				try
				{
					try
					{
						BaseMutexFileAppender baseMutexFileAppender;
						if ((baseMutexFileAppender = baseFileAppender as BaseMutexFileAppender) != null && baseMutexFileAppender.ArchiveMutex != null)
						{
							baseMutexFileAppender.ArchiveMutex.WaitOne();
						}
						else
						{
							InternalLogger.Info<string, string>("FileTarget(Name={0}): Archive mutex not available: {1}", base.Name, text);
						}
					}
					catch (AbandonedMutexException)
					{
					}
					string archiveFileName = this.GetArchiveFileName(fileName, ev, upcomingWriteSize, previousLogEventTimestamp);
					if (string.IsNullOrEmpty(archiveFileName))
					{
						InternalLogger.Trace<string, string>("FileTarget(Name={0}): Archive already performed for file '{1}'", base.Name, text);
						if (text != fileName)
						{
							this._initializedFiles.Remove(fileName);
						}
						this._initializedFiles.Remove(text);
					}
					else
					{
						text = archiveFileName;
						this.DoAutoArchive(text, ev, previousLogEventTimestamp, initializedNewFile);
						this._initializedFiles.Remove(text);
					}
					if (this._previousLogFileName == text)
					{
						this._previousLogFileName = null;
						this._previousLogEventTimestamp = null;
					}
					return true;
				}
				catch (Exception ex2)
				{
					InternalLogger.Warn(ex2, "FileTarget(Name={0}): Failed to archive file '{1}'.", new object[] { base.Name, text });
					if (ex2.MustBeRethrown())
					{
						throw;
					}
				}
				finally
				{
					BaseMutexFileAppender baseMutexFileAppender2;
					if ((baseMutexFileAppender2 = baseFileAppender as BaseMutexFileAppender) != null)
					{
						Mutex archiveMutex = baseMutexFileAppender2.ArchiveMutex;
						if (archiveMutex != null)
						{
							archiveMutex.ReleaseMutex();
						}
					}
					if (baseFileAppender != null)
					{
						baseFileAppender.Dispose();
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x0000FB40 File Offset: 0x0000DD40
		private string GetArchiveFileName(string fileName, LogEventInfo ev, int upcomingWriteSize, DateTime previousLogEventTimestamp)
		{
			if (fileName != null || this._previousLogFileName != null)
			{
				return this.GetArchiveFileNameBasedOnFileSize(fileName, upcomingWriteSize) ?? this.GetArchiveFileNameBasedOnTime(fileName, ev, previousLogEventTimestamp);
			}
			return null;
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x0000FB6C File Offset: 0x0000DD6C
		private string GetPotentialFileForArchiving(string fileName)
		{
			if (string.Equals(fileName, this._previousLogFileName, StringComparison.OrdinalIgnoreCase))
			{
				return fileName;
			}
			if (string.IsNullOrEmpty(this._previousLogFileName))
			{
				return fileName;
			}
			if (string.IsNullOrEmpty(fileName))
			{
				return this._previousLogFileName;
			}
			if (this._fileAppenderCache.GetFileLength(fileName) == null)
			{
				return this._previousLogFileName;
			}
			return fileName;
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x0000FBC8 File Offset: 0x0000DDC8
		private string GetArchiveFileNameBasedOnFileSize(string fileName, int upcomingWriteSize)
		{
			if (this.ArchiveAboveSize == -1L)
			{
				return null;
			}
			string potentialFileForArchiving = this.GetPotentialFileForArchiving(fileName);
			if (potentialFileForArchiving == null)
			{
				return null;
			}
			long? fileLength = this._fileAppenderCache.GetFileLength(potentialFileForArchiving);
			if (fileLength == null)
			{
				return null;
			}
			if (potentialFileForArchiving != fileName)
			{
				upcomingWriteSize = 0;
			}
			if (fileLength.Value + (long)upcomingWriteSize > this.ArchiveAboveSize)
			{
				return potentialFileForArchiving;
			}
			return null;
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x0000FC2C File Offset: 0x0000DE2C
		private string GetArchiveFileNameBasedOnTime(string fileName, LogEventInfo logEvent, DateTime previousLogEventTimestamp)
		{
			if (this.ArchiveEvery == FileArchivePeriod.None)
			{
				return null;
			}
			fileName = this.GetPotentialFileForArchiving(fileName);
			if (fileName == null)
			{
				return null;
			}
			DateTime? dateTime = ((previousLogEventTimestamp != DateTime.MinValue && this.IsSimpleKeepFileOpen) ? new DateTime?(previousLogEventTimestamp) : null);
			DateTime? fileCreationTimeSource = this._fileAppenderCache.GetFileCreationTimeSource(fileName, dateTime);
			if (fileCreationTimeSource == null)
			{
				return null;
			}
			if (previousLogEventTimestamp != DateTime.MinValue && previousLogEventTimestamp < fileCreationTimeSource && FileTarget.TruncateArchiveTime(previousLogEventTimestamp, FileArchivePeriod.Minute) < FileTarget.TruncateArchiveTime(fileCreationTimeSource.Value, FileArchivePeriod.Minute) && PlatformDetector.IsUnix)
			{
				if (this.IsSimpleKeepFileOpen)
				{
					InternalLogger.Debug<string, DateTime?, DateTime>("FileTarget(Name={0}): Adjusted file creation time from {1} to {2}. Linux FileSystem probably don't support file birthtime.", base.Name, fileCreationTimeSource, previousLogEventTimestamp);
					fileCreationTimeSource = new DateTime?(previousLogEventTimestamp);
				}
				else
				{
					InternalLogger.Debug<string, DateTime?, DateTime>("FileTarget(Name={0}): File creation time {1} newer than previous file write time {2}. Linux FileSystem probably don't support file birthtime, unless multiple applications are writing to the same file. Configure FileTarget.KeepFileOpen=true AND FileTarget.ConcurrentWrites=false, so NLog can fix this.", base.Name, fileCreationTimeSource, previousLogEventTimestamp);
				}
			}
			DateTime dateTime2 = FileTarget.TruncateArchiveTime(fileCreationTimeSource.Value, this.ArchiveEvery);
			DateTime dateTime3 = FileTarget.TruncateArchiveTime(logEvent.TimeStamp, this.ArchiveEvery);
			if (dateTime2 != dateTime3)
			{
				string archiveDateFormatString = this.GetArchiveDateFormatString(string.Empty);
				string text = fileCreationTimeSource.Value.ToString(archiveDateFormatString, CultureInfo.InvariantCulture);
				string text2 = logEvent.TimeStamp.ToString(archiveDateFormatString, CultureInfo.InvariantCulture);
				if (text != text2)
				{
					return fileName;
				}
			}
			return null;
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x0000FD8C File Offset: 0x0000DF8C
		private static DateTime TruncateArchiveTime(DateTime input, FileArchivePeriod resolution)
		{
			switch (resolution)
			{
			case FileArchivePeriod.Year:
				return new DateTime(input.Year, 1, 1, 0, 0, 0, 0, input.Kind);
			case FileArchivePeriod.Month:
				return new DateTime(input.Year, input.Month, 1, 0, 0, 0, input.Kind);
			case FileArchivePeriod.Day:
				return input.Date;
			case FileArchivePeriod.Hour:
				return input.AddTicks(-(input.Ticks % 36000000000L));
			case FileArchivePeriod.Minute:
				return input.AddTicks(-(input.Ticks % 600000000L));
			case FileArchivePeriod.Sunday:
				return FileTarget.CalculateNextWeekday(input.Date, DayOfWeek.Sunday);
			case FileArchivePeriod.Monday:
				return FileTarget.CalculateNextWeekday(input.Date, DayOfWeek.Monday);
			case FileArchivePeriod.Tuesday:
				return FileTarget.CalculateNextWeekday(input.Date, DayOfWeek.Tuesday);
			case FileArchivePeriod.Wednesday:
				return FileTarget.CalculateNextWeekday(input.Date, DayOfWeek.Wednesday);
			case FileArchivePeriod.Thursday:
				return FileTarget.CalculateNextWeekday(input.Date, DayOfWeek.Thursday);
			case FileArchivePeriod.Friday:
				return FileTarget.CalculateNextWeekday(input.Date, DayOfWeek.Friday);
			case FileArchivePeriod.Saturday:
				return FileTarget.CalculateNextWeekday(input.Date, DayOfWeek.Saturday);
			default:
				return input;
			}
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x0000FEAC File Offset: 0x0000E0AC
		private void AutoCloseAppendersAfterArchive(object sender, EventArgs state)
		{
			if (!Monitor.TryEnter(base.SyncRoot, TimeSpan.FromSeconds(2.0)))
			{
				return;
			}
			try
			{
				if (base.IsInitialized)
				{
					InternalLogger.Trace<string>("FileTarget(Name={0}): Auto Close FileAppenders after archive", base.Name);
					this._fileAppenderCache.CloseAppenders(DateTime.MinValue);
				}
			}
			catch (Exception ex)
			{
				InternalLogger.Warn(ex, "FileTarget(Name={0}): Exception in AutoCloseAppendersAfterArchive", new object[] { base.Name });
				if (ex.MustBeRethrownImmediately())
				{
					throw;
				}
			}
			finally
			{
				Monitor.Exit(base.SyncRoot);
			}
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x0000FF54 File Offset: 0x0000E154
		private void AutoClosingTimerCallback(object sender, EventArgs state)
		{
			if (!Monitor.TryEnter(base.SyncRoot, TimeSpan.FromSeconds(0.5)))
			{
				return;
			}
			try
			{
				if (base.IsInitialized)
				{
					if (this.OpenFileCacheTimeout > 0)
					{
						DateTime dateTime = DateTime.UtcNow.AddSeconds((double)(-(double)this.OpenFileCacheTimeout));
						InternalLogger.Trace<string>("FileTarget(Name={0}): Auto Close FileAppenders", base.Name);
						this._fileAppenderCache.CloseAppenders(dateTime);
					}
					if (this.OpenFileFlushTimeout > 0 && !this.AutoFlush)
					{
						this.ConditionalFlushOpenFileAppenders();
					}
				}
			}
			catch (Exception ex)
			{
				InternalLogger.Warn(ex, "FileTarget(Name={0}): Exception in AutoClosingTimerCallback", new object[] { base.Name });
				if (ex.MustBeRethrownImmediately())
				{
					throw;
				}
			}
			finally
			{
				Monitor.Exit(base.SyncRoot);
			}
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x00010030 File Offset: 0x0000E230
		private void ConditionalFlushOpenFileAppenders()
		{
			DateTime dateTime = TimeSource.Current.Time.AddSeconds((double)(-(double)Math.Max(this.OpenFileFlushTimeout, 5) * 2));
			bool flag = false;
			foreach (KeyValuePair<string, DateTime> keyValuePair in this._initializedFiles)
			{
				if (keyValuePair.Value > dateTime)
				{
					flag = true;
					break;
				}
			}
			if (flag)
			{
				InternalLogger.Trace<string>("FileTarget(Name={0}): Auto Flush FileAppenders", base.Name);
				this._fileAppenderCache.FlushAppenders();
			}
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x000100D4 File Offset: 0x0000E2D4
		private void WriteToFile(string fileName, ArraySegment<byte> bytes, bool initializedNewFile)
		{
			if (this.ReplaceFileContentsOnEachWrite)
			{
				this.ReplaceFileContent(fileName, bytes, true);
				return;
			}
			BaseFileAppender baseFileAppender = this._fileAppenderCache.AllocateAppender(fileName);
			try
			{
				if (initializedNewFile)
				{
					this.WriteHeaderAndBom(baseFileAppender);
				}
				baseFileAppender.Write(bytes.Array, bytes.Offset, bytes.Count);
				if (this.AutoFlush)
				{
					baseFileAppender.Flush();
				}
			}
			catch (Exception ex)
			{
				InternalLogger.Error(ex, "FileTarget(Name={0}): Failed write to file '{1}'.", new object[] { base.Name, fileName });
				BaseFileAppender baseFileAppender2 = this._fileAppenderCache.InvalidateAppender(fileName);
				if (baseFileAppender2 != null)
				{
					baseFileAppender2.Dispose();
				}
				throw;
			}
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x0001017C File Offset: 0x0000E37C
		private DateTime InitializeFile(string fileName, LogEventInfo logEvent)
		{
			if (this._initializedFiles.Count != 0 && this._previousLogEventTimestamp != null && this._previousLogFileName == fileName && logEvent.TimeStamp == this._previousLogEventTimestamp.Value)
			{
				return this._previousLogEventTimestamp.Value;
			}
			DateTime timeStamp = logEvent.TimeStamp;
			DateTime dateTime;
			if (!this._initializedFiles.TryGetValue(fileName, out dateTime))
			{
				this.ProcessOnStartup(fileName, logEvent);
				this._initializedFilesCounter++;
				if (this._initializedFilesCounter >= 25)
				{
					this._initializedFilesCounter = 0;
					this.CleanupInitializedFiles();
				}
				this._initializedFiles[fileName] = timeStamp;
				return DateTime.MinValue;
			}
			if (dateTime != timeStamp)
			{
				this._initializedFiles[fileName] = timeStamp;
			}
			return dateTime;
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x00010244 File Offset: 0x0000E444
		private void FinalizeFile(string fileName, bool isArchiving = false)
		{
			InternalLogger.Trace<string, string, bool>("FileTarget(Name={0}): FinalizeFile '{1}, isArchiving: {2}'", base.Name, fileName, isArchiving);
			if (isArchiving || !this.WriteFooterOnArchivingOnly)
			{
				this.WriteFooter(fileName);
			}
			BaseFileAppender baseFileAppender = this._fileAppenderCache.InvalidateAppender(fileName);
			if (baseFileAppender != null)
			{
				baseFileAppender.Dispose();
			}
			this._initializedFiles.Remove(fileName);
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x0001029C File Offset: 0x0000E49C
		private void WriteFooter(string fileName)
		{
			ArraySegment<byte> layoutBytes = this.GetLayoutBytes(base.Footer);
			if (layoutBytes.Count > 0 && File.Exists(fileName))
			{
				this.WriteToFile(fileName, layoutBytes, false);
			}
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x000102D4 File Offset: 0x0000E4D4
		private void ProcessOnStartup(string fileName, LogEventInfo logEvent)
		{
			InternalLogger.Debug<string, string>("FileTarget(Name={0}): Process file '{1}' on startup", base.Name, fileName);
			this.RefreshArchiveFilePatternToWatch(fileName, logEvent);
			if (this.ArchiveOldFileOnStartup)
			{
				try
				{
					this.DoAutoArchive(fileName, logEvent, DateTime.MinValue, true);
				}
				catch (Exception ex)
				{
					InternalLogger.Warn(ex, "FileTarget(Name={0}): Unable to archive old log file '{1}'.", new object[] { base.Name, fileName });
					if (ex.MustBeRethrown())
					{
						throw;
					}
				}
			}
			if (this.DeleteOldFileOnStartup)
			{
				this.DeleteOldArchiveFile(fileName);
			}
			string archiveFileNamePattern = this.GetArchiveFileNamePattern(fileName, logEvent);
			if (!string.IsNullOrEmpty(archiveFileNamePattern) && FileArchiveModeFactory.ShouldDeleteOldArchives(this.MaxArchiveFiles))
			{
				IFileArchiveMode fileArchiveHelper = this.GetFileArchiveHelper(archiveFileNamePattern);
				if (fileArchiveHelper.AttemptCleanupOnInitializeFile(archiveFileNamePattern, this.MaxArchiveFiles))
				{
					List<DateAndSequenceArchive> existingArchiveFiles = fileArchiveHelper.GetExistingArchiveFiles(archiveFileNamePattern);
					foreach (DateAndSequenceArchive dateAndSequenceArchive in fileArchiveHelper.CheckArchiveCleanup(archiveFileNamePattern, existingArchiveFiles, this.MaxArchiveFiles))
					{
						this.DeleteOldArchiveFile(dateAndSequenceArchive.FileName);
					}
				}
			}
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x000103EC File Offset: 0x0000E5EC
		private void ReplaceFileContent(string fileName, ArraySegment<byte> bytes, bool firstAttempt)
		{
			try
			{
				using (FileStream fileStream = File.Create(fileName))
				{
					ArraySegment<byte> layoutBytes = this.GetLayoutBytes(base.Header);
					if (layoutBytes.Count > 0)
					{
						fileStream.Write(layoutBytes.Array, layoutBytes.Offset, layoutBytes.Count);
					}
					fileStream.Write(bytes.Array, bytes.Offset, bytes.Count);
					ArraySegment<byte> layoutBytes2 = this.GetLayoutBytes(base.Footer);
					if (layoutBytes2.Count > 0)
					{
						fileStream.Write(layoutBytes2.Array, layoutBytes2.Offset, layoutBytes2.Count);
					}
				}
			}
			catch (DirectoryNotFoundException)
			{
				if (!this.CreateDirs || !firstAttempt)
				{
					throw;
				}
				Directory.CreateDirectory(Path.GetDirectoryName(fileName));
				this.ReplaceFileContent(fileName, bytes, false);
			}
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x000104D0 File Offset: 0x0000E6D0
		private void WriteHeaderAndBom(BaseFileAppender appender)
		{
			if (base.Header == null && !this.WriteBom)
			{
				return;
			}
			long? fileLength = appender.GetFileLength();
			if (fileLength != null)
			{
				long? num = fileLength;
				long num2 = 0L;
				if (!((num.GetValueOrDefault() == num2) & (num != null)))
				{
					return;
				}
			}
			if (this.WriteBom)
			{
				InternalLogger.Trace<string, Encoding>("FileTarget(Name={0}): Write byte order mark from encoding={1}", base.Name, this.Encoding);
				byte[] preamble = this.Encoding.GetPreamble();
				if (preamble.Length != 0)
				{
					appender.Write(preamble, 0, preamble.Length);
				}
			}
			if (base.Header != null)
			{
				InternalLogger.Trace<string>("FileTarget(Name={0}): Write header", base.Name);
				ArraySegment<byte> layoutBytes = this.GetLayoutBytes(base.Header);
				if (layoutBytes.Count > 0)
				{
					appender.Write(layoutBytes.Array, layoutBytes.Offset, layoutBytes.Count);
				}
			}
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x000105A0 File Offset: 0x0000E7A0
		private ArraySegment<byte> GetLayoutBytes(Layout layout)
		{
			if (layout == null)
			{
				return default(ArraySegment<byte>);
			}
			if (base.OptimizeBufferReuse)
			{
				using (ReusableObjectCreator<StringBuilder>.LockOject lockOject = this.ReusableLayoutBuilder.Allocate())
				{
					using (ReusableObjectCreator<char[]>.LockOject lockOject2 = this._reusableEncodingBuffer.Allocate())
					{
						LogEventInfo logEventInfo = LogEventInfo.CreateNullEvent();
						layout.RenderAppendBuilder(logEventInfo, lockOject.Result, false);
						lockOject.Result.Append(this.NewLineChars);
						using (MemoryStream memoryStream = new MemoryStream(lockOject.Result.Length))
						{
							this.TransformBuilderToStream(logEventInfo, lockOject.Result, lockOject2.Result, memoryStream);
							return new ArraySegment<byte>(memoryStream.ToArray());
						}
					}
				}
			}
			string text = layout.Render(LogEventInfo.CreateNullEvent()) + this.NewLineChars;
			return new ArraySegment<byte>(this.TransformBytes(this.Encoding.GetBytes(text)));
		}

		// Token: 0x040000E9 RID: 233
		private const int InitializedFilesCleanupPeriod = 2;

		// Token: 0x040000EA RID: 234
		private const int InitializedFilesCounterMax = 25;

		// Token: 0x040000EB RID: 235
		private const int ArchiveAboveSizeDisabled = -1;

		// Token: 0x040000EC RID: 236
		private readonly Dictionary<string, DateTime> _initializedFiles = new Dictionary<string, DateTime>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x040000ED RID: 237
		private FileAppenderCache _fileAppenderCache;

		// Token: 0x040000EE RID: 238
		private IFileArchiveMode _fileArchiveHelper;

		// Token: 0x040000EF RID: 239
		private Timer _autoClosingTimer;

		// Token: 0x040000F0 RID: 240
		private int _initializedFilesCounter;

		// Token: 0x040000F1 RID: 241
		private int _maxArchiveFiles;

		// Token: 0x040000F2 RID: 242
		private FilePathLayout _fullFileName;

		// Token: 0x040000F3 RID: 243
		private FilePathLayout _fullArchiveFileName;

		// Token: 0x040000F4 RID: 244
		private FileArchivePeriod _archiveEvery;

		// Token: 0x040000F5 RID: 245
		private long _archiveAboveSize;

		// Token: 0x040000F6 RID: 246
		private bool _enableArchiveFileCompression;

		// Token: 0x040000F7 RID: 247
		private DateTime? _previousLogEventTimestamp;

		// Token: 0x040000F8 RID: 248
		private string _previousLogFileName;

		// Token: 0x040000F9 RID: 249
		private bool? _concurrentWrites;

		// Token: 0x040000FA RID: 250
		private bool _keepFileOpen;

		// Token: 0x040000FB RID: 251
		private bool _cleanupFileName;

		// Token: 0x040000FC RID: 252
		private FilePathKind _fileNameKind;

		// Token: 0x040000FD RID: 253
		private FilePathKind _archiveFileKind;

		// Token: 0x04000111 RID: 273
		private string _archiveDateFormat;

		// Token: 0x04000112 RID: 274
		private ArchiveNumberingMode _archiveNumbering;

		// Token: 0x04000117 RID: 279
		private readonly ReusableStreamCreator _reusableFileWriteStream = new ReusableStreamCreator(4096);

		// Token: 0x04000118 RID: 280
		private readonly ReusableStreamCreator _reusableAsyncFileWriteStream = new ReusableStreamCreator(4096);

		// Token: 0x04000119 RID: 281
		private readonly ReusableBufferCreator _reusableEncodingBuffer = new ReusableBufferCreator(1024);

		// Token: 0x0400011A RID: 282
		private SortHelpers.KeySelector<AsyncLogEventInfo, string> _getFullFileNameDelegate;
	}
}
