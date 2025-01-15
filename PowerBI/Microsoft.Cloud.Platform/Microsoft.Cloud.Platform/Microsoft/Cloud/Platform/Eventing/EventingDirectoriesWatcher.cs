using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.Eventing.Base;
using Microsoft.Cloud.Platform.EventsKit;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Eventing
{
	// Token: 0x02000386 RID: 902
	public sealed class EventingDirectoriesWatcher : IShuttable, IIdentifiable
	{
		// Token: 0x06001BEE RID: 7150 RVA: 0x0006A7F4 File Offset: 0x000689F4
		public EventingDirectoriesWatcher([NotNull] string sourceDirectory, [NotNull] string targetDirectory, [NotNull] string identifier, long maxTargetDirectorySizeInMb, long reduceDirectorySizeInMb, int maxRetriesToCollectFile, [NotNull] IActivityFactory activityFactory, [NotNull] IEventFilesCollectionEventsKit eventsKit)
		{
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(sourceDirectory, "sourceDirectory");
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(targetDirectory, "targetDirectory");
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(identifier, "identifier");
			ExtendedDiagnostics.EnsureArgumentIsPositive(maxTargetDirectorySizeInMb, "maxTargetDirectorySizeInMb");
			ExtendedDiagnostics.EnsureArgumentIsPositive(reduceDirectorySizeInMb, "reduceDirectorySizeInMb");
			ExtendedDiagnostics.EnsureArgumentNotNull<IActivityFactory>(activityFactory, "activityFactory");
			ExtendedDiagnostics.EnsureArgumentNotNull<IEventFilesCollectionEventsKit>(eventsKit, "eventsKit");
			this.m_locker = new object();
			this.m_filesIdentifier = identifier;
			this.m_sourceDirectory = sourceDirectory;
			this.m_targetDirectory = targetDirectory;
			this.m_eventsKit = eventsKit;
			this.m_activityFactory = activityFactory;
			this.m_filesFailedToCollect = new Dictionary<string, int>();
			this.m_maxRetriesToCollectFile = maxRetriesToCollectFile;
			this.Name = "{0}.DirectoryWatcher".FormatWithInvariantCulture(new object[] { identifier });
			this.m_workTicketManager = new WorkTicketManager("{0}.WTM".FormatWithInvariantCulture(new object[] { this.Name }));
			this.ChangeCleanupPolicy(maxTargetDirectorySizeInMb, reduceDirectorySizeInMb);
			this.m_counter = 1;
			this.m_fileWatcher = new FileSystemWatcher(this.m_sourceDirectory)
			{
				NotifyFilter = NotifyFilters.FileName,
				Filter = "*.etl",
				EnableRaisingEvents = true
			};
			this.m_fileWatcher.Created += this.FileWatcherCallback;
			this.ValidateTargetDirectoryAndTempDirectoryOnSameDrive();
			AsyncInvoker.InvokeMethodAsynchronously(delegate
			{
				this.FileWatcherCallback(this, null);
			}, WaitOrNot.DontWait, "{0}.FirstCollection".FormatWithInvariantCulture(new object[] { this.Name }));
		}

		// Token: 0x06001BEF RID: 7151 RVA: 0x0006A95C File Offset: 0x00068B5C
		public void ForceCollection(bool copyLastFile)
		{
			this.Collect(this.m_workTicketManager.CreateWorkTicket(this), copyLastFile);
		}

		// Token: 0x06001BF0 RID: 7152 RVA: 0x0006A974 File Offset: 0x00068B74
		public void ChangeCleanupPolicy(long maxTargetDirectorySizeInMb, long reduceDirectorySizeInMb)
		{
			object locker = this.m_locker;
			lock (locker)
			{
				this.m_maxDirSizeInBytes = ExtendedMath.MBtoBytes(maxTargetDirectorySizeInMb);
				this.m_reduceByInBytes = ExtendedMath.MBtoBytes(reduceDirectorySizeInMb);
			}
		}

		// Token: 0x06001BF1 RID: 7153 RVA: 0x0006A9C8 File Offset: 0x00068BC8
		private void FileWatcherCallback(object sender, FileSystemEventArgs fileSystemEventArgs)
		{
			Exception ex = TopLevelHandler.Run(sender, TopLevelHandlerOption.SwallowNonfatal, delegate
			{
				this.ForceCollection(false);
			});
			if (ex != null && !ex.GetType().Equals(typeof(ShutdownSequenceStartedException)))
			{
				EventFilesCollectionException ex2 = new EventFilesCollectionException("File collection Failed. See inner exception", ex);
				this.m_eventsKit.NotifyEventFilesCollectionFailed(string.Empty, ex2);
			}
		}

		// Token: 0x06001BF2 RID: 7154 RVA: 0x0006AA20 File Offset: 0x00068C20
		private void Collect(WorkTicket workTicket, bool copyLastFile)
		{
			using (this.m_activityFactory.CreateSyncActivity(SingletonActivityType<EventingDirectoriesWatcher.CollectEventFilesActivityType>.Instance))
			{
				List<FileInfo> filesToMove = new List<FileInfo>();
				Exception ex = TopLevelHandler.Run(this, TopLevelHandlerOption.SwallowNonfatal, delegate
				{
					using (workTicket)
					{
						object locker = this.m_locker;
						lock (locker)
						{
							List<FileInfo> list = (from fi in new DirectoryInfo(this.m_sourceDirectory).EnumerateFiles("*.etl", SearchOption.TopDirectoryOnly)
								orderby fi.CreationTimeUtc
								select fi).ToList<FileInfo>();
							int num = ((list.Count<FileInfo>() > 0) ? (list.Count<FileInfo>() - 1) : 0);
							filesToMove = list.Take(num).ToList<FileInfo>();
							for (int i = 0; i < filesToMove.Count<FileInfo>(); i++)
							{
								DateTime dateTime = ((i < list.Count<FileInfo>() - 1) ? list[i + 1].CreationTimeUtc : ExtendedDateTime.UtcNow);
								this.ExecuteCollectionAction(EventingDirectoriesWatcher.FileCollectionOperation.Move, filesToMove[i].FullName, dateTime);
							}
							if (copyLastFile)
							{
								FileInfo fileInfo = list.LastOrDefault<FileInfo>();
								if (fileInfo != null)
								{
									this.ExecuteCollectionAction(EventingDirectoriesWatcher.FileCollectionOperation.Copy, fileInfo.FullName, ExtendedDateTime.UtcNow);
								}
							}
							this.CleanupTargetDirectoryIfRequired();
						}
					}
				});
				if (ex != null)
				{
					string text = filesToMove.StringJoin(",", (FileInfo f) => f.FullName);
					EventFilesCollectionException ex2 = new EventFilesCollectionException(text, ex);
					this.m_eventsKit.NotifyEventFilesCollectionFailed(text, ex2);
				}
			}
		}

		// Token: 0x06001BF3 RID: 7155 RVA: 0x0006AAF0 File Offset: 0x00068CF0
		private void ExecuteCollectionAction(EventingDirectoriesWatcher.FileCollectionOperation operation, string originalPath, DateTime modifiedTime)
		{
			string target = string.Empty;
			if (!this.m_filesFailedToCollect.ContainsKey(originalPath) || this.m_filesFailedToCollect[originalPath] < this.m_maxRetriesToCollectFile)
			{
				Exception ex = TopLevelHandler.Run(this, TopLevelHandlerOption.SwallowNonfatal, delegate
				{
					string newFileName = this.GetNewFileName(originalPath, modifiedTime);
					target = Path.Combine(this.m_targetDirectory, EventFilesNaming.GetCompressedEventFileName(newFileName));
					if (this.TryCompressEventFileContent(originalPath, target))
					{
						if (operation == EventingDirectoriesWatcher.FileCollectionOperation.Move)
						{
							this.DeleteEventFile(originalPath);
							return;
						}
					}
					else
					{
						string text = Path.Combine(this.m_targetDirectory, newFileName);
						if (operation == EventingDirectoriesWatcher.FileCollectionOperation.Move)
						{
							File.Move(originalPath, text);
							return;
						}
						File.Copy(originalPath, text);
					}
				});
				if (ex != null)
				{
					this.HandleCollectionError(originalPath, ex);
				}
			}
		}

		// Token: 0x06001BF4 RID: 7156 RVA: 0x0006AB7C File Offset: 0x00068D7C
		public void DeleteEventFile(string originalPath)
		{
			bool flag = false;
			Exception ex = null;
			int num = 0;
			while (num < 3 && !flag)
			{
				try
				{
					File.Delete(originalPath);
					flag = true;
				}
				catch (IOException ex)
				{
					Thread.Sleep(3000);
				}
				catch (UnauthorizedAccessException ex)
				{
					break;
				}
				num++;
			}
			if (ex != null && File.Exists(originalPath))
			{
				this.HandleDeleteFileException(originalPath, ex);
			}
		}

		// Token: 0x06001BF5 RID: 7157 RVA: 0x0006ABE8 File Offset: 0x00068DE8
		public bool TryCompressEventFileContent(string sourcePath, string destinationPath)
		{
			bool flag;
			try
			{
				string tempFileName = TempFile.GetTempFileName("tmp", "");
				using (FileStream fileStream = new FileStream(sourcePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
				{
					using (FileStream fileStream2 = File.Create(tempFileName))
					{
						using (DeflateStream deflateStream = new DeflateStream(fileStream2, CompressionMode.Compress))
						{
							fileStream.CopyTo(deflateStream);
						}
					}
				}
				File.Move(tempFileName, destinationPath);
				flag = true;
			}
			catch (IOException ex)
			{
				EventFilesCollectionException ex2 = new EventFilesCollectionException(sourcePath, "Failed to compress event file", ex);
				this.m_eventsKit.NotifyFailedToCompressEventFile(sourcePath, ex2);
				flag = false;
			}
			return flag;
		}

		// Token: 0x06001BF6 RID: 7158 RVA: 0x0006ACB0 File Offset: 0x00068EB0
		private void ValidateTargetDirectoryAndTempDirectoryOnSameDrive()
		{
			string pathRoot = Path.GetPathRoot(Path.GetTempPath());
			string pathRoot2 = Path.GetPathRoot(this.m_targetDirectory);
			if (!string.Equals(Path.GetPathRoot(pathRoot), Path.GetPathRoot(pathRoot2), StringComparison.OrdinalIgnoreCase))
			{
				this.m_eventsKit.NotifyTempDirectoryAndTargetDirectoryNotOnSameDrive(pathRoot, pathRoot2, new EventFilesCollectionException("Temp directory and the directory in which compressed event files are written to are on different drives. As a result, event file compression + movement is not transactional, so corrupted compressed event files might be observed. " + "Temp: '{0}', Target: '{1}'".FormatWithInvariantCulture(new object[] { pathRoot, pathRoot2 })));
			}
		}

		// Token: 0x06001BF7 RID: 7159 RVA: 0x0006AD1C File Offset: 0x00068F1C
		private string GetNewFileName(string oldPath, DateTime lastModifiedTime)
		{
			DateTime creationTimeUtc = File.GetCreationTimeUtc(oldPath);
			string filesIdentifier = this.m_filesIdentifier;
			int counter = this.m_counter;
			this.m_counter = counter + 1;
			return EventFilesNaming.FormatTargetFileName(creationTimeUtc, lastModifiedTime, filesIdentifier, counter);
		}

		// Token: 0x06001BF8 RID: 7160 RVA: 0x0006AD4C File Offset: 0x00068F4C
		private void CleanupTargetDirectoryIfRequired()
		{
			using (this.m_activityFactory.CreateSyncActivity(SingletonActivityType<EventingDirectoriesWatcher.DeleteEventFilesActivityType>.Instance))
			{
				using (MutexUtils.GetNamedSystemMutexScope("Microsoft.Cloud.Platform.EVTDIRECTORIES_6C3ED558-858C-43E9-A9CE-93DE521ADC71", MutexUtilsOptions.ContinueOnMutexAborted))
				{
					this.CleanupDirectoryIfRequired(this.m_targetDirectory, this.m_maxDirSizeInBytes, this.m_reduceByInBytes);
				}
			}
		}

		// Token: 0x06001BF9 RID: 7161 RVA: 0x0006ADC0 File Offset: 0x00068FC0
		private void CleanupDirectoryIfRequired(string directory, long sizeLimitInBytes, long minReduceByInBytes)
		{
			long sizeInBytes = ExtendedDirectory.GetDirectorySizeInBytes(directory, EventingDirectoriesWatcher.s_eventFilesExtensions, SearchOption.AllDirectories);
			if (sizeInBytes >= sizeLimitInBytes)
			{
				long totalDeletedInBytes = 0L;
				new DirectoryInfo(directory);
				IEnumerable<string> enumerable = (from file in (from fi in ExtendedDirectory.EnumerateFiles(directory, EventingDirectoriesWatcher.s_eventFilesExtensions, SearchOption.AllDirectories)
						orderby fi.CreationTimeUtc
						select fi).TakeWhile(delegate(FileInfo file)
					{
						bool flag = totalDeletedInBytes < minReduceByInBytes || sizeInBytes - totalDeletedInBytes > sizeLimitInBytes;
						if (flag)
						{
							totalDeletedInBytes += file.Length;
						}
						return flag;
					})
					select file.FullName).Materialize<string>();
				TraceSourceBase<EventingTrace>.Tracer.TraceInformation("Preparing to delete the following files of total size (bytes) {0}: {1}", new object[]
				{
					totalDeletedInBytes,
					enumerable.StringJoin(",")
				});
				foreach (string text in enumerable)
				{
					try
					{
						File.Delete(text);
					}
					catch (IOException ex)
					{
						this.HandleDeleteFileException(sizeInBytes, sizeLimitInBytes, text, ex);
					}
					catch (UnauthorizedAccessException ex2)
					{
						this.HandleDeleteFileException(sizeInBytes, sizeLimitInBytes, text, ex2);
					}
				}
			}
		}

		// Token: 0x06001BFA RID: 7162 RVA: 0x0006AF5C File Offset: 0x0006915C
		private void HandleDeleteFileException(long sizeInBytes, long sizeLimitInBytes, string file, Exception ex)
		{
			int num = (int)ExtendedMath.BytesToMB(sizeInBytes);
			int num2 = (int)ExtendedMath.BytesToMB(sizeLimitInBytes);
			EventFilesDeletionException ex2 = new EventFilesDeletionException(file, num, string.Empty, ex);
			this.m_eventsKit.NotifyEventFilesDeletionFailed(file, num, num2, ex2);
		}

		// Token: 0x06001BFB RID: 7163 RVA: 0x0006AF98 File Offset: 0x00069198
		private void HandleDeleteFileException(string file, Exception ex)
		{
			EventFilesDeletionException ex2 = new EventFilesDeletionException(file, -1, "Failed to delete file after compression", ex);
			this.m_eventsKit.NotifyFailedToDeleteSourceFileAfterCompression(file, ex2);
		}

		// Token: 0x06001BFC RID: 7164 RVA: 0x0006AFC0 File Offset: 0x000691C0
		private void HandleCollectionError(string filePath, Exception exception)
		{
			EventFilesCollectionException ex = new EventFilesCollectionException(filePath, exception);
			this.m_eventsKit.NotifyEventFilesCollectionFailed(filePath, ex);
			if (exception is IOException)
			{
				if (!this.m_filesFailedToCollect.ContainsKey(filePath))
				{
					this.m_filesFailedToCollect.Add(filePath, 0);
				}
				Dictionary<string, int> filesFailedToCollect = this.m_filesFailedToCollect;
				filesFailedToCollect[filePath]++;
				if (this.m_filesFailedToCollect[filePath] == this.m_maxRetriesToCollectFile)
				{
					this.m_eventsKit.NotifyExhaustedCollectFileMaximumRetries(filePath, this.m_maxRetriesToCollectFile, ex);
				}
			}
		}

		// Token: 0x06001BFD RID: 7165 RVA: 0x0006B048 File Offset: 0x00069248
		public void Stop()
		{
			this.m_fileWatcher.Dispose();
			WorkTicket lastCollectionWorkTicket = this.m_workTicketManager.CreateWorkTicket(this);
			this.m_workTicketManager.Stop();
			AsyncInvoker.InvokeMethodAsynchronously(delegate
			{
				this.Collect(lastCollectionWorkTicket, true);
			}, WaitOrNot.DontWait, "{0}.LastCollection".FormatWithInvariantCulture(new object[] { this.Name }));
		}

		// Token: 0x06001BFE RID: 7166 RVA: 0x0006B0B3 File Offset: 0x000692B3
		public void WaitForStopToComplete()
		{
			this.m_workTicketManager.WaitForStopToComplete();
		}

		// Token: 0x06001BFF RID: 7167 RVA: 0x0006B0C0 File Offset: 0x000692C0
		public void Shutdown()
		{
			this.m_workTicketManager.Shutdown();
		}

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x06001C00 RID: 7168 RVA: 0x0006B0CD File Offset: 0x000692CD
		// (set) Token: 0x06001C01 RID: 7169 RVA: 0x0006B0D5 File Offset: 0x000692D5
		public string Name { get; private set; }

		// Token: 0x04000967 RID: 2407
		private object m_locker;

		// Token: 0x04000968 RID: 2408
		private string m_sourceDirectory;

		// Token: 0x04000969 RID: 2409
		private string m_targetDirectory;

		// Token: 0x0400096A RID: 2410
		private FileSystemWatcher m_fileWatcher;

		// Token: 0x0400096B RID: 2411
		private string m_filesIdentifier;

		// Token: 0x0400096C RID: 2412
		private WorkTicketManager m_workTicketManager;

		// Token: 0x0400096D RID: 2413
		private long m_maxDirSizeInBytes;

		// Token: 0x0400096E RID: 2414
		private long m_reduceByInBytes;

		// Token: 0x0400096F RID: 2415
		private int m_counter;

		// Token: 0x04000970 RID: 2416
		private Dictionary<string, int> m_filesFailedToCollect;

		// Token: 0x04000971 RID: 2417
		private int m_maxRetriesToCollectFile;

		// Token: 0x04000972 RID: 2418
		private IEventFilesCollectionEventsKit m_eventsKit;

		// Token: 0x04000973 RID: 2419
		private IActivityFactory m_activityFactory;

		// Token: 0x04000974 RID: 2420
		private const string c_mutexName = "Microsoft.Cloud.Platform.EVTDIRECTORIES_6C3ED558-858C-43E9-A9CE-93DE521ADC71";

		// Token: 0x04000975 RID: 2421
		private static string[] s_eventFilesExtensions = new string[] { ".etl", ".cmp" };

		// Token: 0x04000976 RID: 2422
		private const int c_numRetriesForDeleteFile = 3;

		// Token: 0x020007B1 RID: 1969
		private enum FileCollectionOperation
		{
			// Token: 0x040016B4 RID: 5812
			Copy,
			// Token: 0x040016B5 RID: 5813
			Move
		}

		// Token: 0x020007B2 RID: 1970
		private class CollectEventFilesActivityType : SingletonActivityType<EventingDirectoriesWatcher.CollectEventFilesActivityType>
		{
			// Token: 0x06003150 RID: 12624 RVA: 0x000A7A2C File Offset: 0x000A5C2C
			public CollectEventFilesActivityType()
				: base("EVTC")
			{
			}
		}

		// Token: 0x020007B3 RID: 1971
		private class DeleteEventFilesActivityType : SingletonActivityType<EventingDirectoriesWatcher.DeleteEventFilesActivityType>
		{
			// Token: 0x06003151 RID: 12625 RVA: 0x000A7A39 File Offset: 0x000A5C39
			public DeleteEventFilesActivityType()
				: base("EVTD")
			{
			}
		}
	}
}
