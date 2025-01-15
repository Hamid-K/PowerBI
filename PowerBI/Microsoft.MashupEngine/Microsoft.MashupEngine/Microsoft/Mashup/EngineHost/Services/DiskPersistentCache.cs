using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Evaluator.Interface;
using Microsoft.Mashup.Shims.Interprocess;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x020019B5 RID: 6581
	public class DiskPersistentCache : PersistentCache
	{
		// Token: 0x0600A6B0 RID: 42672 RVA: 0x00227810 File Offset: 0x00225A10
		public DiskPersistentCache(string directory, long maxSize, long trimSize, long maxEntryLength, bool userSpecific, IEvaluationConstants evaluationConstants)
		{
			this.syncRoot = new object();
			this.keyHasher = new KeyHasher();
			this.directory = directory;
			this.maxSize = maxSize;
			this.trimSize = trimSize;
			this.maxEntryLength = maxEntryLength;
			this.userSpecific = userSpecific;
			this.entries = new Dictionary<string, DiskPersistentCache.CacheEntry>();
			this.accessed = new Dictionary<string, DateTime>();
			this.created = new Dictionary<string, DateTime>();
			this.persistentDictionary = new DiskPersistentCache.PersistentDictionary(Path.Combine(directory, "Index"), evaluationConstants);
			this.purgeMutex = MutexFactory.Create(false, this.ComposePurgeMutexName());
			this.directorySize = DiskPersistentCache.CreateDirectorySize(directory);
			this.evaluationConstants = evaluationConstants;
			this.staleness = DateTime.UtcNow;
		}

		// Token: 0x17002A87 RID: 10887
		// (get) Token: 0x0600A6B1 RID: 42673 RVA: 0x002278C8 File Offset: 0x00225AC8
		public override long MaxEntryLength
		{
			get
			{
				return this.maxEntryLength;
			}
		}

		// Token: 0x17002A88 RID: 10888
		// (get) Token: 0x0600A6B2 RID: 42674 RVA: 0x002278D0 File Offset: 0x00225AD0
		// (set) Token: 0x0600A6B3 RID: 42675 RVA: 0x00227914 File Offset: 0x00225B14
		public override DateTime Staleness
		{
			get
			{
				object obj = this.syncRoot;
				DateTime dateTime;
				lock (obj)
				{
					dateTime = this.staleness;
				}
				return dateTime;
			}
			set
			{
				object obj = this.syncRoot;
				lock (obj)
				{
					if (value < this.staleness)
					{
						this.staleness = value;
					}
				}
			}
		}

		// Token: 0x17002A89 RID: 10889
		// (get) Token: 0x0600A6B4 RID: 42676 RVA: 0x00227964 File Offset: 0x00225B64
		public override CacheSize CacheSize
		{
			get
			{
				return new CacheSize(this.directorySize.Count, this.directorySize.Size);
			}
		}

		// Token: 0x17002A8A RID: 10890
		// (get) Token: 0x0600A6B5 RID: 42677 RVA: 0x00227984 File Offset: 0x00225B84
		public override CacheVersion Current
		{
			get
			{
				CacheVersion cacheVersion;
				using (this.GetVersionLock())
				{
					cacheVersion = LongCacheVersion.New(this.GetCurrentVersion());
				}
				return cacheVersion;
			}
		}

		// Token: 0x17002A8B RID: 10891
		// (get) Token: 0x0600A6B6 RID: 42678 RVA: 0x002279C8 File Offset: 0x00225BC8
		public override bool? UserSpecific
		{
			get
			{
				return new bool?(this.userSpecific);
			}
		}

		// Token: 0x0600A6B7 RID: 42679 RVA: 0x002279D8 File Offset: 0x00225BD8
		public override CacheVersion Increment()
		{
			CacheVersion cacheVersion;
			using (this.GetVersionLock())
			{
				long num = this.GetCurrentVersion() + 1L;
				this.WriteVersionFile(num);
				cacheVersion = LongCacheVersion.New(num);
			}
			return cacheVersion;
		}

		// Token: 0x0600A6B8 RID: 42680 RVA: 0x00227A28 File Offset: 0x00225C28
		private DiskPersistentCache.MutexLock GetVersionLock()
		{
			return new DiskPersistentCache.MutexLock(MutexFactory.Create(false, DiskPersistentCache.EscapeMutexName(this.GetPathName("Cache.Version") + ".mutex")));
		}

		// Token: 0x0600A6B9 RID: 42681 RVA: 0x00227A50 File Offset: 0x00225C50
		private long GetCurrentVersion()
		{
			long num = this.ReadVersionFile();
			if (num == -1L)
			{
				Dictionary<string, DiskPersistentCache.CacheEntry> dictionary;
				if (this.persistentDictionary.TryGetKeyValues(out dictionary))
				{
					foreach (DiskPersistentCache.CacheEntry cacheEntry in dictionary.Values)
					{
						num = Math.Max(num, cacheEntry.version);
					}
				}
				num = Math.Max(num, 0L);
				this.WriteVersionFile(num);
			}
			return num;
		}

		// Token: 0x0600A6BA RID: 42682 RVA: 0x00227AD8 File Offset: 0x00225CD8
		private long ReadVersionFile()
		{
			byte[] array = FileSystemAccessHelper.IgnoringAccessExceptions<byte[]>("DiskPersistentCache/ReadVersionFile", () => File.ReadAllBytes(this.GetPathName("Cache.Version")), this.evaluationConstants);
			try
			{
				return BitConverter.ToInt64(array, 0);
			}
			catch (ArgumentException)
			{
			}
			return -1L;
		}

		// Token: 0x0600A6BB RID: 42683 RVA: 0x00227B24 File Offset: 0x00225D24
		private void WriteVersionFile(long version)
		{
			byte[] bytes = BitConverter.GetBytes(version);
			FileSystemAccessHelper.IgnoringAccessExceptions("DiskPersistentCache/WriteVersionFile", delegate
			{
				File.WriteAllBytes(this.GetPathName("Cache.Version"), bytes);
			}, this.evaluationConstants);
		}

		// Token: 0x0600A6BC RID: 42684 RVA: 0x00227B66 File Offset: 0x00225D66
		public static long GetCacheDirectorySize(string directory)
		{
			return DiskPersistentCache.CreateDirectorySize(directory).Size;
		}

		// Token: 0x0600A6BD RID: 42685 RVA: 0x00227B74 File Offset: 0x00225D74
		public override bool TryGetStorage(string key, DateTime maxStaleness, CacheVersion minVersion, out IStorage storage)
		{
			string text;
			Stream stream;
			if (!this.TryGetStream(key, maxStaleness, LongCacheVersion.ToLong(minVersion), out text, out stream))
			{
				storage = null;
				return false;
			}
			storage = StreamStorage.Open(stream);
			return true;
		}

		// Token: 0x0600A6BE RID: 42686 RVA: 0x00227BA5 File Offset: 0x00225DA5
		public override IStorage CreateStorage()
		{
			return StreamStorage.Create(this.CreateStream());
		}

		// Token: 0x0600A6BF RID: 42687 RVA: 0x00227BB4 File Offset: 0x00225DB4
		public override void CommitStorage(string key, CacheVersion maxVersion, IStorage storage)
		{
			if (maxVersion == null)
			{
				throw new NotSupportedException();
			}
			DiskPersistentCache.CommitFileStream commitFileStream = (DiskPersistentCache.CommitFileStream)((StreamStorage)storage).Commit();
			this.CommitStream(key, LongCacheVersion.ToLong(maxVersion), commitFileStream);
		}

		// Token: 0x0600A6C0 RID: 42688 RVA: 0x00227BEC File Offset: 0x00225DEC
		public override IPagedStorage OpenStorage(string key, DateTime maxStaleness, CacheVersion minVersion, int pageSize, int maxPageCount)
		{
			int num = (int)MemoryMappedFile.PageAlign((long)pageSize);
			string text = string.Format(CultureInfo.InvariantCulture, "{0}/{1}/{2}", key.Replace("/", "//"), num, maxPageCount);
			string text2 = "MashupCacheCreationMutex_" + DiskPersistentCache.EscapeBase64(this.keyHasher.HashKey(this.directory + "/" + text));
			Mutex mutex = MutexFactory.Create(false, text2);
			IPagedStorage pagedStorage2;
			try
			{
				using (new DiskPersistentCache.MutexLock(mutex))
				{
					string text3;
					bool flag = !this.TryGetFileName(text, maxStaleness, LongCacheVersion.ToLong(minVersion), out text3);
					if (flag)
					{
						text3 = Guid.NewGuid().ToString() + ".dat";
						this.directorySize.Add((long)pageSize * (long)maxPageCount);
					}
					IPagedStorage pagedStorage;
					try
					{
						pagedStorage = new DiskPersistentCache.PagedStorage(this, text, text3, num, maxPageCount, mutex);
						if (pageSize != num)
						{
							pagedStorage = new PageSizePagedStorage(pagedStorage, pageSize);
						}
					}
					catch (Exception ex)
					{
						using (IHostTrace hostTrace = EvaluatorTracing.CreateTrace("DiskPersistentCache/OpenStorage", this.evaluationConstants, TraceEventType.Information, null))
						{
							if (!SafeExceptions.TraceIsSafeException(hostTrace, ex))
							{
								throw;
							}
						}
						throw new PersistentCacheException(Strings.Cache_EntryTooLarge, null);
					}
					if (flag)
					{
						this.AddEntry(text, text3, LongCacheVersion.ToLong(base.CacheClock.Current));
					}
					pagedStorage2 = pagedStorage;
				}
			}
			finally
			{
				mutex.Close();
			}
			return pagedStorage2;
		}

		// Token: 0x0600A6C1 RID: 42689 RVA: 0x00227D84 File Offset: 0x00225F84
		private string ComposePurgeMutexName()
		{
			return "MashupCachePurgeMutex_" + DiskPersistentCache.EscapeMutexName(this.directory);
		}

		// Token: 0x0600A6C2 RID: 42690 RVA: 0x00227D9B File Offset: 0x00225F9B
		private static string EscapeBase64(string base64)
		{
			return base64.Replace("_", "__").Replace('/', '_');
		}

		// Token: 0x0600A6C3 RID: 42691 RVA: 0x00227DB6 File Offset: 0x00225FB6
		private static string EscapeMutexName(string name)
		{
			return name.Replace("_", "__").Replace(Path.DirectorySeparatorChar, '_');
		}

		// Token: 0x0600A6C4 RID: 42692 RVA: 0x00227DD4 File Offset: 0x00225FD4
		private static DirectorySize CreateDirectorySize(string directory)
		{
			return new DirectorySize(directory, "*.dat");
		}

		// Token: 0x0600A6C5 RID: 42693 RVA: 0x00227DE4 File Offset: 0x00225FE4
		private bool TryGetFileName(string key, DateTime maxStaleness, long minVersion, out string fileName)
		{
			Stream stream;
			if (this.TryGetStream(key, maxStaleness, minVersion, out fileName, out stream))
			{
				stream.Dispose();
				return true;
			}
			fileName = null;
			return false;
		}

		// Token: 0x0600A6C6 RID: 42694 RVA: 0x00227E10 File Offset: 0x00226010
		private bool TryGetStream(string key, DateTime maxStaleness, long minVersion, out string fileName, out Stream stream)
		{
			if (this.TryOpenCachedKey(key, maxStaleness, minVersion, out fileName, out stream))
			{
				return true;
			}
			DiskPersistentCache.CacheEntry cacheEntry;
			if (this.persistentDictionary.TryGetValue(key, out cacheEntry) && this.TryOpenFile(cacheEntry, maxStaleness, minVersion, out stream))
			{
				object obj = this.syncRoot;
				lock (obj)
				{
					this.entries[key] = cacheEntry;
				}
				fileName = cacheEntry.fileName;
				return true;
			}
			fileName = null;
			return false;
		}

		// Token: 0x0600A6C7 RID: 42695 RVA: 0x00227E98 File Offset: 0x00226098
		private bool TryOpenCachedKey(string key, DateTime maxStaleness, long minVersion, out string fileName, out Stream stream)
		{
			object obj = this.syncRoot;
			DiskPersistentCache.CacheEntry cacheEntry;
			lock (obj)
			{
				if (!this.entries.TryGetValue(key, out cacheEntry))
				{
					fileName = null;
					stream = null;
					return false;
				}
				this.accessed[cacheEntry.fileName] = DateTime.UtcNow;
			}
			if (this.TryOpenFile(cacheEntry, maxStaleness, minVersion, out stream))
			{
				fileName = cacheEntry.fileName;
				return true;
			}
			obj = this.syncRoot;
			lock (obj)
			{
				if (this.entries.ContainsKey(key))
				{
					this.entries.Remove(key);
				}
			}
			fileName = null;
			stream = null;
			return false;
		}

		// Token: 0x0600A6C8 RID: 42696 RVA: 0x00227F6C File Offset: 0x0022616C
		private bool TryOpenFile(DiskPersistentCache.CacheEntry entry, DateTime maxStaleness, long minVersion, out Stream stream)
		{
			if (entry.version < minVersion)
			{
				stream = null;
				return false;
			}
			DateTime dateTime = default(DateTime);
			object obj = this.syncRoot;
			lock (obj)
			{
				if (this.created.TryGetValue(entry.fileName, out dateTime))
				{
					if (dateTime < maxStaleness)
					{
						stream = null;
						return false;
					}
					if (dateTime < this.staleness)
					{
						this.staleness = dateTime;
					}
				}
			}
			string path = this.GetPathName(entry.fileName);
			if (dateTime == default(DateTime))
			{
				dateTime = FileSystemAccessHelper.IgnoringAccessExceptions<DateTime>("DiskPersistentCache/TryOpenFile/GetCreationTimeUtc", () => File.GetCreationTimeUtc(path), this.evaluationConstants);
				if (dateTime == default(DateTime))
				{
					stream = null;
					return false;
				}
				obj = this.syncRoot;
				lock (obj)
				{
					this.created[entry.fileName] = dateTime;
					if (dateTime < maxStaleness)
					{
						stream = null;
						return false;
					}
					if (dateTime < this.staleness)
					{
						this.staleness = dateTime;
					}
				}
			}
			stream = FileSystemAccessHelper.IgnoringAccessExceptions<FileStream>("DiskPersistentCache/TryOpenFile/newFileStream", () => new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite), this.evaluationConstants);
			return stream != null;
		}

		// Token: 0x0600A6C9 RID: 42697 RVA: 0x002280E8 File Offset: 0x002262E8
		private Stream CreateStream()
		{
			this.directorySize.Add(this.maxEntryLength);
			string pathName = this.GetPathName(Guid.NewGuid().ToString() + ".dat");
			Stream stream;
			try
			{
				stream = new DiskPersistentCache.CommitFileStream(pathName, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite, this.directorySize, this.maxEntryLength, this.evaluationConstants);
			}
			catch (DirectoryNotFoundException)
			{
				Directory.CreateDirectory(Path.GetDirectoryName(pathName));
				stream = new DiskPersistentCache.CommitFileStream(pathName, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite, this.directorySize, this.maxEntryLength, this.evaluationConstants);
			}
			return stream;
		}

		// Token: 0x0600A6CA RID: 42698 RVA: 0x00228184 File Offset: 0x00226384
		private void CommitStream(string key, long version, DiskPersistentCache.CommitFileStream commitStream)
		{
			DiskPersistentCache.CacheEntry cacheEntry = this.persistentDictionary.Remove(key, version);
			if (cacheEntry != default(DiskPersistentCache.CacheEntry))
			{
				DiskPersistentCache.Delete(this.GetPathName(cacheEntry.fileName), this.evaluationConstants);
			}
			commitStream.Flush();
			commitStream.Commit();
			this.AddEntry(key, Path.GetFileName(commitStream.Name), version);
			this.Purge();
		}

		// Token: 0x0600A6CB RID: 42699 RVA: 0x002281EC File Offset: 0x002263EC
		private void AddEntry(string key, string fileName, long version)
		{
			DiskPersistentCache.CacheEntry cacheEntry = new DiskPersistentCache.CacheEntry
			{
				fileName = fileName,
				version = version
			};
			bool flag = this.persistentDictionary.TryAdd(key, cacheEntry);
			object obj = this.syncRoot;
			lock (obj)
			{
				if (flag)
				{
					this.entries[key] = cacheEntry;
					this.created[cacheEntry.fileName] = DateTime.UtcNow;
				}
				else if (this.entries.ContainsKey(key))
				{
					this.entries.Remove(key);
					this.created.Remove(cacheEntry.fileName);
				}
			}
		}

		// Token: 0x0600A6CC RID: 42700 RVA: 0x002282A8 File Offset: 0x002264A8
		public override void Purge()
		{
			object obj = this.syncRoot;
			lock (obj)
			{
				foreach (KeyValuePair<string, DateTime> keyValuePair in this.accessed)
				{
					this.SetLastAccessTime(this.GetPathName(keyValuePair.Key), keyValuePair.Value);
				}
				this.accessed.Clear();
			}
			if (this.ShouldPurge())
			{
				this.DeleteFiles();
			}
		}

		// Token: 0x0600A6CD RID: 42701 RVA: 0x00228350 File Offset: 0x00226550
		private bool ShouldPurge()
		{
			return this.directorySize.Size >= this.maxSize;
		}

		// Token: 0x0600A6CE RID: 42702 RVA: 0x0000336E File Offset: 0x0000156E
		public override void Dispose()
		{
		}

		// Token: 0x0600A6CF RID: 42703 RVA: 0x00228368 File Offset: 0x00226568
		private static Stream CreateFile(string pathName)
		{
			Stream stream;
			try
			{
				stream = new FileStream(pathName, FileMode.Create, FileAccess.Write);
			}
			catch (DirectoryNotFoundException)
			{
				Directory.CreateDirectory(Path.GetDirectoryName(pathName));
				stream = new FileStream(pathName, FileMode.Create, FileAccess.Write);
			}
			return stream;
		}

		// Token: 0x0600A6D0 RID: 42704 RVA: 0x002283AC File Offset: 0x002265AC
		private long GetLength(string pathName)
		{
			return FileSystemAccessHelper.IgnoringAccessExceptions<long>("DiskPersistentCache/GetLength", () => new FileInfo(pathName).Length, this.evaluationConstants);
		}

		// Token: 0x0600A6D1 RID: 42705 RVA: 0x002283E4 File Offset: 0x002265E4
		private void DeleteFiles()
		{
			bool flag;
			try
			{
				flag = this.purgeMutex.WaitOne();
			}
			catch (AbandonedMutexException)
			{
				flag = true;
			}
			if (flag)
			{
				try
				{
					this._DeleteFiles();
				}
				finally
				{
					this.purgeMutex.ReleaseMutex();
				}
			}
		}

		// Token: 0x0600A6D2 RID: 42706 RVA: 0x00228438 File Offset: 0x00226638
		private void _DeleteFiles()
		{
			string[] array;
			if (!FileSystemAccessHelper.TryIgnoringAccessExceptions<string[]>("DiskPersistentCache/_DeleteFiles", () => Directory.GetFiles(this.directory, "*.dat"), this.evaluationConstants, out array))
			{
				return;
			}
			string[] array2 = array.Select((string path) => Path.GetFileName(path)).ToArray<string>();
			Dictionary<string, DiskPersistentCache.CacheEntry> dictionary;
			if (!this.persistentDictionary.TryGetKeyValues(out dictionary))
			{
				return;
			}
			Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
			foreach (KeyValuePair<string, DiskPersistentCache.CacheEntry> keyValuePair in dictionary)
			{
				dictionary2.Add(keyValuePair.Value.fileName, keyValuePair.Key);
			}
			HashSet<string> hashSet = new HashSet<string>();
			long num = 0L;
			using (MaxCountTraceSource maxCountTraceSource = FileSystemAccessHelper.CreateTraceSource("DiskPersistentCache/DeleteMissing", this.evaluationConstants, 10))
			{
				foreach (string text in array2)
				{
					string pathName = this.GetPathName(text);
					long length = this.GetLength(pathName);
					if (dictionary2.ContainsKey(text))
					{
						num += DirectorySize.PhysicalSize(length);
					}
					else
					{
						this.Delete(maxCountTraceSource, pathName);
						hashSet.Add(text);
					}
				}
			}
			HashSet<string> hashSet2 = new HashSet<string>(array2);
			foreach (KeyValuePair<string, DiskPersistentCache.CacheEntry> keyValuePair2 in dictionary)
			{
				if (!hashSet2.Contains(keyValuePair2.Value.fileName))
				{
					this.persistentDictionary.Remove(keyValuePair2.Key);
				}
			}
			if (num >= this.maxSize)
			{
				DateTime[] array3 = array2.Select((string fileName) => this.GetLastAccessTime(this.GetPathName(fileName))).ToArray<DateTime>();
				int[] array4 = new int[array3.Length];
				for (int j = 0; j < array4.Length; j++)
				{
					array4[j] = j;
				}
				Array.Sort<int>(array4, new DiskPersistentCache.LastAccessComparer(array3));
				using (MaxCountTraceSource maxCountTraceSource2 = FileSystemAccessHelper.CreateTraceSource("DiskPersistentCache/DeleteLRU", this.evaluationConstants, 10))
				{
					int num2 = 0;
					while (num2 < array2.Length && num > this.trimSize)
					{
						string text2 = array2[array4[num2]];
						if (!hashSet.Contains(text2))
						{
							this.persistentDictionary.Remove(dictionary2[text2]);
							string pathName2 = this.GetPathName(text2);
							num -= DirectorySize.PhysicalSize(this.GetLength(pathName2));
							this.Delete(maxCountTraceSource2, pathName2);
						}
						num2++;
					}
				}
			}
			this.directorySize.Clear();
		}

		// Token: 0x0600A6D3 RID: 42707 RVA: 0x002286F4 File Offset: 0x002268F4
		private void Delete(IHostTraceSource traceSource, string pathName)
		{
			FileSystemAccessHelper.IgnoringAccessExceptions(traceSource, delegate
			{
				File.Delete(pathName);
			});
		}

		// Token: 0x0600A6D4 RID: 42708 RVA: 0x00228720 File Offset: 0x00226920
		private static void Delete(string pathName, IEvaluationConstants evaluationConstants)
		{
			FileSystemAccessHelper.IgnoringAccessExceptions("DiskPersistentCache/Delete", delegate
			{
				File.Delete(pathName);
			}, evaluationConstants);
		}

		// Token: 0x0600A6D5 RID: 42709 RVA: 0x00228754 File Offset: 0x00226954
		private DateTime GetLastAccessTime(string pathName)
		{
			return FileSystemAccessHelper.IgnoringAccessExceptions<DateTime>("DiskPersistentCache/GetLastAccessTime", () => File.GetLastAccessTimeUtc(pathName), this.evaluationConstants);
		}

		// Token: 0x0600A6D6 RID: 42710 RVA: 0x0022878C File Offset: 0x0022698C
		private void SetLastAccessTime(string pathName, DateTime dateTime)
		{
			FileSystemAccessHelper.IgnoringAccessExceptions("DiskPersistentCache/SetLastAccessTime", delegate
			{
				File.SetLastAccessTimeUtc(pathName, dateTime);
			}, this.evaluationConstants);
		}

		// Token: 0x0600A6D7 RID: 42711 RVA: 0x002287C9 File Offset: 0x002269C9
		private string GetPathName(string fileName)
		{
			return Path.Combine(this.directory, fileName);
		}

		// Token: 0x040056C3 RID: 22211
		public const long DefaultMaxCacheSize = 4294967296L;

		// Token: 0x040056C4 RID: 22212
		public const long RecommendedMinCacheSize = 33554432L;

		// Token: 0x040056C5 RID: 22213
		public const long DefaultQnaMaxCacheSize = 4294967296L;

		// Token: 0x040056C6 RID: 22214
		public const long RecommendedQnaMinCacheSize = 536870912L;

		// Token: 0x040056C7 RID: 22215
		private const string dataFiles = "*.dat";

		// Token: 0x040056C8 RID: 22216
		private const string versionFile = "Cache.Version";

		// Token: 0x040056C9 RID: 22217
		private const string purgeMutexPrefix = "MashupCachePurgeMutex_";

		// Token: 0x040056CA RID: 22218
		private const string creationMutexPrefix = "MashupCacheCreationMutex_";

		// Token: 0x040056CB RID: 22219
		private readonly object syncRoot;

		// Token: 0x040056CC RID: 22220
		private readonly KeyHasher keyHasher;

		// Token: 0x040056CD RID: 22221
		private readonly string directory;

		// Token: 0x040056CE RID: 22222
		private readonly long maxSize;

		// Token: 0x040056CF RID: 22223
		private readonly long trimSize;

		// Token: 0x040056D0 RID: 22224
		private readonly long maxEntryLength;

		// Token: 0x040056D1 RID: 22225
		private readonly bool userSpecific;

		// Token: 0x040056D2 RID: 22226
		private readonly Dictionary<string, DiskPersistentCache.CacheEntry> entries;

		// Token: 0x040056D3 RID: 22227
		private readonly Dictionary<string, DateTime> accessed;

		// Token: 0x040056D4 RID: 22228
		private readonly Dictionary<string, DateTime> created;

		// Token: 0x040056D5 RID: 22229
		private readonly DiskPersistentCache.PersistentDictionary persistentDictionary;

		// Token: 0x040056D6 RID: 22230
		private readonly Mutex purgeMutex;

		// Token: 0x040056D7 RID: 22231
		private readonly DirectorySize directorySize;

		// Token: 0x040056D8 RID: 22232
		private readonly IEvaluationConstants evaluationConstants;

		// Token: 0x040056D9 RID: 22233
		private DateTime staleness;

		// Token: 0x020019B6 RID: 6582
		private struct CacheEntry
		{
			// Token: 0x0600A6DB RID: 42715 RVA: 0x0022880A File Offset: 0x00226A0A
			public static bool operator ==(DiskPersistentCache.CacheEntry left, DiskPersistentCache.CacheEntry right)
			{
				return left.fileName == right.fileName && left.version == right.version;
			}

			// Token: 0x0600A6DC RID: 42716 RVA: 0x0022882F File Offset: 0x00226A2F
			public static bool operator !=(DiskPersistentCache.CacheEntry left, DiskPersistentCache.CacheEntry right)
			{
				return left.fileName != right.fileName || left.version != right.version;
			}

			// Token: 0x0600A6DD RID: 42717 RVA: 0x00228857 File Offset: 0x00226A57
			public override bool Equals(object other)
			{
				return other is DiskPersistentCache.CacheEntry && this.Equals((DiskPersistentCache.CacheEntry)other);
			}

			// Token: 0x0600A6DE RID: 42718 RVA: 0x0022886F File Offset: 0x00226A6F
			public bool Equals(DiskPersistentCache.CacheEntry other)
			{
				return this == other;
			}

			// Token: 0x0600A6DF RID: 42719 RVA: 0x00228880 File Offset: 0x00226A80
			public override int GetHashCode()
			{
				int num = (int)this.version;
				if (this.fileName != null)
				{
					num += 37 * this.fileName.GetHashCode();
				}
				return num;
			}

			// Token: 0x040056DA RID: 22234
			public string fileName;

			// Token: 0x040056DB RID: 22235
			public long version;
		}

		// Token: 0x020019B7 RID: 6583
		private class LastAccessComparer : IComparer<int>
		{
			// Token: 0x0600A6E0 RID: 42720 RVA: 0x002288AF File Offset: 0x00226AAF
			public LastAccessComparer(DateTime[] dateTimes)
			{
				this.dateTimes = dateTimes;
			}

			// Token: 0x0600A6E1 RID: 42721 RVA: 0x002288BE File Offset: 0x00226ABE
			public int Compare(int x, int y)
			{
				return this.dateTimes[x].CompareTo(this.dateTimes[y]);
			}

			// Token: 0x040056DC RID: 22236
			private readonly DateTime[] dateTimes;
		}

		// Token: 0x020019B8 RID: 6584
		private class CommitFileStream : FileStream
		{
			// Token: 0x0600A6E2 RID: 42722 RVA: 0x002288DD File Offset: 0x00226ADD
			public CommitFileStream(string path, FileMode mode, FileAccess access, FileShare share, DirectorySize directorySize, long maxLength, IEvaluationConstants evaluationConstants)
				: base(path, mode, access, share)
			{
				this.commit = false;
				this.directorySize = directorySize;
				this.maxLength = maxLength;
				this.evaluationConstants = evaluationConstants;
			}

			// Token: 0x0600A6E3 RID: 42723 RVA: 0x0022890C File Offset: 0x00226B0C
			public void Commit()
			{
				if (this.commit)
				{
					throw new InvalidOperationException();
				}
				this.commit = true;
				long num = DirectorySize.PhysicalSize(this.Length);
				if (this.directorySize != null)
				{
					this.directorySize.Update(num - this.maxLength);
					this.directorySize = null;
				}
			}

			// Token: 0x0600A6E4 RID: 42724 RVA: 0x0022895C File Offset: 0x00226B5C
			public override void Close()
			{
				base.Close();
				if (!this.commit)
				{
					this.commit = true;
					DiskPersistentCache.Delete(base.Name, this.evaluationConstants);
					long num = 0L;
					if (this.directorySize != null)
					{
						this.directorySize.Add(num - this.maxLength);
						this.directorySize = null;
					}
				}
			}

			// Token: 0x040056DD RID: 22237
			private bool commit;

			// Token: 0x040056DE RID: 22238
			private DirectorySize directorySize;

			// Token: 0x040056DF RID: 22239
			private readonly long maxLength;

			// Token: 0x040056E0 RID: 22240
			private readonly IEvaluationConstants evaluationConstants;
		}

		// Token: 0x020019B9 RID: 6585
		private class PagedStorage : IPagedStorage, IDisposable
		{
			// Token: 0x0600A6E5 RID: 42725 RVA: 0x002289B4 File Offset: 0x00226BB4
			public PagedStorage(DiskPersistentCache cache, string key, string fileName, int pageSize, int maxPageCount, Mutex creationMutex)
			{
				this.cache = cache;
				this.key = key;
				this.fileName = fileName;
				this.syncName = DiskPersistentCache.EscapeMutexName(fileName);
				this.pageSize = pageSize;
				this.maxPageCount = maxPageCount;
				long num = MemoryMappedFile.PageAlign((long)this.maxPageCount);
				long num2 = (long)this.pageSize * (long)this.maxPageCount;
				try
				{
					using (new DiskPersistentCache.MutexLock(creationMutex))
					{
						this.file = this.OpenFileMapping(num, num2);
						this.validPagesView = this.file.MapView(0UL, (uint)num);
					}
				}
				catch
				{
					if (this.validPagesView != null)
					{
						this.validPagesView.Dispose();
					}
					if (this.file != null)
					{
						this.file.Dispose();
					}
					throw;
				}
			}

			// Token: 0x17002A8C RID: 10892
			// (get) Token: 0x0600A6E6 RID: 42726 RVA: 0x00228A98 File Offset: 0x00226C98
			public int PageSize
			{
				get
				{
					return this.pageSize;
				}
			}

			// Token: 0x17002A8D RID: 10893
			// (get) Token: 0x0600A6E7 RID: 42727 RVA: 0x00228AA0 File Offset: 0x00226CA0
			public int MaxPageCount
			{
				get
				{
					return this.maxPageCount;
				}
			}

			// Token: 0x0600A6E8 RID: 42728 RVA: 0x00228AA8 File Offset: 0x00226CA8
			public Stream OpenPage(int pageIndex, out bool created)
			{
				if (pageIndex >= this.maxPageCount)
				{
					throw new PersistentCacheException(Strings.Cache_EntryTooLarge, null);
				}
				NamedSemaphore namedSemaphore = null;
				Stream stream;
				try
				{
					if (!this.GetValidPage(pageIndex))
					{
						namedSemaphore = new NamedSemaphore(this.syncName + "." + pageIndex.ToString(CultureInfo.InvariantCulture), 1);
						namedSemaphore.Wait();
						if (this.GetValidPage(pageIndex))
						{
							namedSemaphore.Release();
							namedSemaphore = null;
						}
					}
					created = namedSemaphore != null;
					Stream mappedPage = this.GetMappedPage(pageIndex);
					mappedPage.SetLength((long)(created ? 0 : this.pageSize));
					stream = new DiskPersistentCache.PagedStorage.PagedStorageStream(pageIndex, mappedPage, namedSemaphore);
				}
				catch
				{
					if (namedSemaphore != null)
					{
						namedSemaphore.Release();
					}
					throw;
				}
				return stream;
			}

			// Token: 0x0600A6E9 RID: 42729 RVA: 0x00228B5C File Offset: 0x00226D5C
			public void CommitPage(Stream stream)
			{
				DiskPersistentCache.PagedStorage.PagedStorageStream pagedStorageStream = (DiskPersistentCache.PagedStorage.PagedStorageStream)stream;
				this.SetValidPage(pagedStorageStream.PageIndex);
				pagedStorageStream.ReleasePageCreationLock();
			}

			// Token: 0x0600A6EA RID: 42730 RVA: 0x00228B84 File Offset: 0x00226D84
			public void Dispose()
			{
				string text;
				this.cache.TryGetFileName(this.key, DateTime.MinValue, -1L, out text);
				this.validPagesView.Dispose();
				this.file.Dispose();
			}

			// Token: 0x0600A6EB RID: 42731 RVA: 0x00228BC4 File Offset: 0x00226DC4
			private MemoryMappedFile OpenFileMapping(long validPagesLength, long contentLength)
			{
				FileStream fileStream = File.Open(this.cache.GetPathName(this.fileName), FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
				if (fileStream.Length < validPagesLength)
				{
					fileStream.WriteZeros(validPagesLength);
					fileStream.Flush();
					fileStream.Position = 0L;
				}
				return MemoryMappedFile.Open(fileStream, (ulong)(validPagesLength + contentLength));
			}

			// Token: 0x0600A6EC RID: 42732 RVA: 0x00228C14 File Offset: 0x00226E14
			private bool GetValidPage(int pageIndex)
			{
				byte[] array = new byte[1];
				this.validPagesView.Read(pageIndex, 1, array, 0);
				return array[0] > 0;
			}

			// Token: 0x0600A6ED RID: 42733 RVA: 0x00228C40 File Offset: 0x00226E40
			private void SetValidPage(int pageIndex)
			{
				byte[] array = new byte[] { 1 };
				this.validPagesView.Write(pageIndex, 1, array, 0);
			}

			// Token: 0x0600A6EE RID: 42734 RVA: 0x00228C68 File Offset: 0x00226E68
			private Stream GetMappedPage(int pageIndex)
			{
				MemoryMappedView memoryMappedView = null;
				Stream stream = null;
				Stream stream2;
				try
				{
					memoryMappedView = this.file.MapView((ulong)this.validPagesView.Length + (ulong)((long)pageIndex * (long)this.pageSize), (uint)this.pageSize);
					stream = new MemoryMappedViewStream(memoryMappedView);
					stream.SetLength((long)((ulong)memoryMappedView.Length));
					stream2 = stream.AfterDispose(new Action(memoryMappedView.Dispose));
				}
				catch (Exception ex)
				{
					if (stream != null)
					{
						stream.Dispose();
					}
					if (memoryMappedView != null)
					{
						memoryMappedView.Dispose();
					}
					using (IHostTrace hostTrace = EvaluatorTracing.CreateTrace("DiskPersistentCache/PagedStorage/GetMappedPage", this.cache.evaluationConstants, TraceEventType.Information, null))
					{
						if (!SafeExceptions.TraceIsSafeException(hostTrace, ex))
						{
							throw;
						}
					}
					throw new PersistentCacheException(Strings.Cache_EntryTooLarge, null);
				}
				return stream2;
			}

			// Token: 0x040056E1 RID: 22241
			private readonly DiskPersistentCache cache;

			// Token: 0x040056E2 RID: 22242
			private readonly string key;

			// Token: 0x040056E3 RID: 22243
			private readonly string fileName;

			// Token: 0x040056E4 RID: 22244
			private readonly string syncName;

			// Token: 0x040056E5 RID: 22245
			private readonly int pageSize;

			// Token: 0x040056E6 RID: 22246
			private readonly int maxPageCount;

			// Token: 0x040056E7 RID: 22247
			private readonly MemoryMappedFile file;

			// Token: 0x040056E8 RID: 22248
			private readonly MemoryMappedView validPagesView;

			// Token: 0x020019BA RID: 6586
			private sealed class PagedStorageStream : DelegatingStream
			{
				// Token: 0x0600A6EF RID: 42735 RVA: 0x00228D3C File Offset: 0x00226F3C
				public PagedStorageStream(int pageIndex, Stream stream, NamedSemaphore pageCreationLock)
					: base(stream)
				{
					this.pageIndex = pageIndex;
					this.pageCreationLock = pageCreationLock;
				}

				// Token: 0x17002A8E RID: 10894
				// (get) Token: 0x0600A6F0 RID: 42736 RVA: 0x00002139 File Offset: 0x00000339
				public override bool CanRead
				{
					get
					{
						return true;
					}
				}

				// Token: 0x17002A8F RID: 10895
				// (get) Token: 0x0600A6F1 RID: 42737 RVA: 0x00228D53 File Offset: 0x00226F53
				public override bool CanWrite
				{
					get
					{
						return this.pageCreationLock != null;
					}
				}

				// Token: 0x17002A90 RID: 10896
				// (get) Token: 0x0600A6F2 RID: 42738 RVA: 0x00002139 File Offset: 0x00000339
				public override bool CanSeek
				{
					get
					{
						return true;
					}
				}

				// Token: 0x17002A91 RID: 10897
				// (get) Token: 0x0600A6F3 RID: 42739 RVA: 0x00228D5E File Offset: 0x00226F5E
				public int PageIndex
				{
					get
					{
						return this.pageIndex;
					}
				}

				// Token: 0x0600A6F4 RID: 42740 RVA: 0x000033E7 File Offset: 0x000015E7
				public override void SetLength(long length)
				{
					throw new NotSupportedException();
				}

				// Token: 0x0600A6F5 RID: 42741 RVA: 0x00228D66 File Offset: 0x00226F66
				public override void WriteByte(byte b)
				{
					if (!this.CanWrite)
					{
						throw new NotSupportedException();
					}
					base.WriteByte(b);
				}

				// Token: 0x0600A6F6 RID: 42742 RVA: 0x00228D7D File Offset: 0x00226F7D
				public override void Write(byte[] buffer, int offset, int count)
				{
					if (!this.CanWrite)
					{
						throw new NotSupportedException();
					}
					base.Write(buffer, offset, count);
				}

				// Token: 0x0600A6F7 RID: 42743 RVA: 0x00228D96 File Offset: 0x00226F96
				public void ReleasePageCreationLock()
				{
					if (this.pageCreationLock != null)
					{
						this.pageCreationLock.Release();
						this.pageCreationLock.Dispose();
						this.pageCreationLock = null;
					}
				}

				// Token: 0x0600A6F8 RID: 42744 RVA: 0x00228DBD File Offset: 0x00226FBD
				public override void Close()
				{
					this.ReleasePageCreationLock();
					base.Close();
				}

				// Token: 0x0600A6F9 RID: 42745 RVA: 0x00228DCB File Offset: 0x00226FCB
				protected override void Dispose(bool disposing)
				{
					if (disposing)
					{
						this.ReleasePageCreationLock();
					}
					base.Dispose();
				}

				// Token: 0x040056E9 RID: 22249
				private readonly int pageIndex;

				// Token: 0x040056EA RID: 22250
				private NamedSemaphore pageCreationLock;
			}
		}

		// Token: 0x020019BB RID: 6587
		private class PersistentDictionary
		{
			// Token: 0x0600A6FA RID: 42746 RVA: 0x00228DDC File Offset: 0x00226FDC
			public PersistentDictionary(string directory, IEvaluationConstants evaluationConstants)
			{
				this.directory = directory;
				FileSystemAccessHelper.CreateDirectory(directory, evaluationConstants);
				this.evaluationConstants = evaluationConstants;
			}

			// Token: 0x0600A6FB RID: 42747 RVA: 0x00228DFC File Offset: 0x00226FFC
			public bool TryGetKeyValues(out Dictionary<string, DiskPersistentCache.CacheEntry> dictionary)
			{
				string[] array;
				if (!FileSystemAccessHelper.TryIgnoringAccessExceptions<string[]>("DiskPersistentCache/PersistentDictionary/TryGetKeyValues", () => Directory.GetFiles(this.directory, "*.idx"), this.evaluationConstants, out array))
				{
					dictionary = null;
					return false;
				}
				dictionary = new Dictionary<string, DiskPersistentCache.CacheEntry>();
				foreach (string text in array)
				{
					string fileName = Path.GetFileName(text);
					using (this.GetLock(fileName))
					{
						this.ReadBucket(text, dictionary);
					}
				}
				return true;
			}

			// Token: 0x0600A6FC RID: 42748 RVA: 0x00228E88 File Offset: 0x00227088
			public bool TryGetValue(string key, out DiskPersistentCache.CacheEntry entry)
			{
				string hashFileName = this.GetHashFileName(key);
				string pathName = this.GetPathName(hashFileName);
				bool flag;
				using (this.GetLock(hashFileName))
				{
					flag = this.ReadBucket(pathName).TryGetValue(key, out entry);
				}
				return flag;
			}

			// Token: 0x0600A6FD RID: 42749 RVA: 0x00228EE0 File Offset: 0x002270E0
			public bool TryAdd(string key, DiskPersistentCache.CacheEntry entry)
			{
				DiskPersistentCache.CacheEntry cacheEntry;
				return this.TrySet(key, entry.version, entry, out cacheEntry);
			}

			// Token: 0x0600A6FE RID: 42750 RVA: 0x00228EFD File Offset: 0x002270FD
			public DiskPersistentCache.CacheEntry Remove(string key)
			{
				return this.Remove(key, long.MaxValue);
			}

			// Token: 0x0600A6FF RID: 42751 RVA: 0x00228F10 File Offset: 0x00227110
			public DiskPersistentCache.CacheEntry Remove(string key, long maxVersion)
			{
				DiskPersistentCache.CacheEntry cacheEntry;
				this.TrySet(key, maxVersion, default(DiskPersistentCache.CacheEntry), out cacheEntry);
				return cacheEntry;
			}

			// Token: 0x0600A700 RID: 42752 RVA: 0x00228F34 File Offset: 0x00227134
			private bool TrySet(string key, long maxVersion, DiskPersistentCache.CacheEntry entry, out DiskPersistentCache.CacheEntry oldEntry)
			{
				string hashFileName = this.GetHashFileName(key);
				string hashPathName = this.GetPathName(hashFileName);
				string tempPathName = this.GetPathName(Guid.NewGuid().ToString() + ".tmp");
				bool flag = false;
				using (this.GetLock(hashFileName))
				{
					Dictionary<string, DiskPersistentCache.CacheEntry> bucket = this.ReadBucket(hashPathName);
					if (!bucket.TryGetValue(key, out oldEntry) || oldEntry.version <= maxVersion)
					{
						if (entry != default(DiskPersistentCache.CacheEntry))
						{
							bucket[key] = entry;
						}
						else
						{
							bucket.Remove(key);
						}
						if (bucket.Count == 0)
						{
							FileSystemAccessHelper.IgnoringAccessExceptions("DiskPersistentCache/PersistentDictionary/TrySet/FileDelete/1", delegate
							{
								File.Delete(hashPathName);
							}, this.evaluationConstants);
						}
						else
						{
							flag = FileSystemAccessHelper.TryIgnoringAccessExceptions("DiskPersistentCache/PersistentDictionary/TrySet/WriteBucket", delegate
							{
								DiskPersistentCache.PersistentDictionary.WriteBucket(tempPathName, bucket);
							}, this.evaluationConstants);
							FileSystemAccessHelper.IgnoringAccessExceptions("DiskPersistentCache/PersistentDictionary/TrySet/FileDelete/2", delegate
							{
								File.Delete(hashPathName);
							}, this.evaluationConstants);
							if (flag)
							{
								flag = FileSystemAccessHelper.TryIgnoringAccessExceptions("DiskPersistentCache/PersistentDictionary/TrySet/FileMove", delegate
								{
									File.Move(tempPathName, hashPathName);
								}, this.evaluationConstants);
							}
							if (!flag)
							{
								FileSystemAccessHelper.IgnoringAccessExceptions("DiskPersistentCache/PersistentDictionary/TrySet/FileDelete/3", delegate
								{
									File.Delete(tempPathName);
								}, this.evaluationConstants);
							}
						}
					}
					else
					{
						oldEntry = default(DiskPersistentCache.CacheEntry);
					}
				}
				return flag;
			}

			// Token: 0x0600A701 RID: 42753 RVA: 0x00229100 File Offset: 0x00227300
			private Dictionary<string, DiskPersistentCache.CacheEntry> ReadBucket(string pathName)
			{
				Dictionary<string, DiskPersistentCache.CacheEntry> dictionary = new Dictionary<string, DiskPersistentCache.CacheEntry>();
				this.ReadBucket(pathName, dictionary);
				return dictionary;
			}

			// Token: 0x0600A702 RID: 42754 RVA: 0x0022911C File Offset: 0x0022731C
			private void ReadBucket(string pathName, Dictionary<string, DiskPersistentCache.CacheEntry> dictionary)
			{
				FileSystemAccessHelper.IgnoringAccessExceptions("DiskPersistentCache/PersistentDictionary/ReadBucket", delegate
				{
					if (!File.Exists(pathName))
					{
						return;
					}
					using (FileStream fileStream = new FileStream(pathName, FileMode.Open, FileAccess.Read))
					{
						using (BinaryReader binaryReader = new BinaryReader(fileStream))
						{
							int num = binaryReader.ReadInt32();
							for (int i = 0; i < num; i++)
							{
								string text = binaryReader.ReadString();
								string text2 = binaryReader.ReadString();
								long num2 = binaryReader.ReadInt64();
								DiskPersistentCache.CacheEntry cacheEntry = new DiskPersistentCache.CacheEntry
								{
									fileName = text2,
									version = num2
								};
								dictionary.Add(text, cacheEntry);
							}
						}
					}
				}, this.evaluationConstants);
			}

			// Token: 0x0600A703 RID: 42755 RVA: 0x0022915C File Offset: 0x0022735C
			private static void WriteBucket(string pathName, Dictionary<string, DiskPersistentCache.CacheEntry> bucket)
			{
				using (Stream stream = DiskPersistentCache.CreateFile(pathName))
				{
					using (BinaryWriter binaryWriter = new BinaryWriter(stream))
					{
						binaryWriter.Write(bucket.Count);
						foreach (KeyValuePair<string, DiskPersistentCache.CacheEntry> keyValuePair in bucket)
						{
							binaryWriter.Write(keyValuePair.Key);
							binaryWriter.Write(keyValuePair.Value.fileName);
							binaryWriter.Write(keyValuePair.Value.version);
						}
					}
				}
			}

			// Token: 0x0600A704 RID: 42756 RVA: 0x0022921C File Offset: 0x0022741C
			private string GetPathName(string fileName)
			{
				return Path.Combine(this.directory, fileName);
			}

			// Token: 0x0600A705 RID: 42757 RVA: 0x0022922C File Offset: 0x0022742C
			private string GetHashFileName(string key)
			{
				DiskPersistentCache.PersistentDictionary.HashBuilder hashBuilder = default(DiskPersistentCache.PersistentDictionary.HashBuilder);
				for (int i = 0; i < key.Length; i++)
				{
					hashBuilder.Add((int)key[i]);
				}
				return hashBuilder.ToHash().ToString("X16", CultureInfo.InvariantCulture) + ".idx";
			}

			// Token: 0x0600A706 RID: 42758 RVA: 0x00229283 File Offset: 0x00227483
			private DiskPersistentCache.PersistentDictionary.Lock GetLock(string name)
			{
				return new DiskPersistentCache.PersistentDictionary.Lock(name);
			}

			// Token: 0x040056EB RID: 22251
			private const string indexFiles = "*.idx";

			// Token: 0x040056EC RID: 22252
			private readonly string directory;

			// Token: 0x040056ED RID: 22253
			private readonly IEvaluationConstants evaluationConstants;

			// Token: 0x020019BC RID: 6588
			private struct Lock : IDisposable
			{
				// Token: 0x0600A708 RID: 42760 RVA: 0x0022929D File Offset: 0x0022749D
				public Lock(string name)
				{
					this.mutex = MutexFactory.Create(false, "DataExplorerCache_" + name);
					this.mutexLock = new DiskPersistentCache.MutexLock(this.mutex);
				}

				// Token: 0x0600A709 RID: 42761 RVA: 0x002292C8 File Offset: 0x002274C8
				public void Dispose()
				{
					this.mutexLock.Dispose();
					this.mutex.Close();
				}

				// Token: 0x040056EE RID: 22254
				private readonly Mutex mutex;

				// Token: 0x040056EF RID: 22255
				private readonly DiskPersistentCache.MutexLock mutexLock;
			}

			// Token: 0x020019BD RID: 6589
			private struct HashBuilder
			{
				// Token: 0x0600A70A RID: 42762 RVA: 0x002292EE File Offset: 0x002274EE
				public void Add(int value)
				{
					this.hash += (long)value;
					this.hash += this.hash << 10;
					this.hash ^= this.hash >> 6;
				}

				// Token: 0x0600A70B RID: 42763 RVA: 0x0022932C File Offset: 0x0022752C
				public long ToHash()
				{
					this.hash += this.hash << 3;
					this.hash ^= this.hash >> 11;
					this.hash += this.hash << 15;
					return this.hash;
				}

				// Token: 0x040056F0 RID: 22256
				private long hash;
			}
		}

		// Token: 0x020019C1 RID: 6593
		private struct MutexLock : IDisposable
		{
			// Token: 0x0600A715 RID: 42773 RVA: 0x00229484 File Offset: 0x00227684
			public MutexLock(Mutex mutex)
			{
				this.mutex = mutex;
				try
				{
					this.mutex.WaitOne();
				}
				catch (AbandonedMutexException)
				{
				}
			}

			// Token: 0x0600A716 RID: 42774 RVA: 0x002294BC File Offset: 0x002276BC
			public void Dispose()
			{
				this.mutex.ReleaseMutex();
			}

			// Token: 0x040056F7 RID: 22263
			private readonly Mutex mutex;
		}
	}
}
