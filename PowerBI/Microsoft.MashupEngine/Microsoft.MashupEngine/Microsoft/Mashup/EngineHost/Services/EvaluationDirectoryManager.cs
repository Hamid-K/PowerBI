using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Evaluator;
using Microsoft.Mashup.Shims.Interprocess;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x020019D6 RID: 6614
	internal sealed class EvaluationDirectoryManager : IDisposable
	{
		// Token: 0x0600A77A RID: 42874 RVA: 0x0022A6C0 File Offset: 0x002288C0
		public EvaluationDirectoryManager(IList<string> directoryRoots, IList<string> exclusiveDirectories, IEvaluationConstants evaluationConstants)
		{
			this.evaluationConstants = evaluationConstants;
			this.directoryRootsAndLocks = new KeyValuePair<string, Mutex>[directoryRoots.Count];
			for (int i = 0; i < directoryRoots.Count; i++)
			{
				Mutex mutex = MutexFactory.Create(false, EvaluationDirectoryManager.ComposeMutexName(directoryRoots[i]));
				this.directoryRootsAndLocks[i] = new KeyValuePair<string, Mutex>(directoryRoots[i], mutex);
			}
			this.exclusiveDirectoriesAndLocks = new KeyValuePair<string, SharedExclusiveLock>[exclusiveDirectories.Count];
			try
			{
				for (int j = 0; j < exclusiveDirectories.Count; j++)
				{
					SharedExclusiveLock sharedExclusiveLock = new SharedExclusiveLock(EvaluationDirectoryManager.ComposeSemaphoreName(exclusiveDirectories[j]));
					if (!sharedExclusiveLock.TryAcquireSharedLock(EvaluationDirectoryManager.lockTimeout))
					{
						throw new TimeoutException(string.Format(CultureInfo.InvariantCulture, "Could not acquire lock for '{0}' within {1} seconds.", exclusiveDirectories[j], EvaluationDirectoryManager.lockTimeout.TotalSeconds));
					}
					this.exclusiveDirectoriesAndLocks[j] = new KeyValuePair<string, SharedExclusiveLock>(exclusiveDirectories[j], sharedExclusiveLock);
				}
			}
			catch
			{
				this.CleanUpLocks();
				throw;
			}
		}

		// Token: 0x0600A77B RID: 42875 RVA: 0x0022A7CC File Offset: 0x002289CC
		public void Dispose()
		{
			this.CleanUpLocks();
			EvaluatorThreadPool.Start(new ThreadStart(this.CleanupThread));
		}

		// Token: 0x0600A77C RID: 42876 RVA: 0x0022A7E8 File Offset: 0x002289E8
		private void CleanUpLocks()
		{
			if (this.exclusiveDirectoriesAndLocks != null)
			{
				for (int i = 0; i < this.exclusiveDirectoriesAndLocks.Length; i++)
				{
					KeyValuePair<string, SharedExclusiveLock> keyValuePair = this.exclusiveDirectoriesAndLocks[i];
					if (keyValuePair.Value != null)
					{
						keyValuePair.Value.ReleaseSharedLock();
						keyValuePair.Value.Dispose();
					}
				}
				this.exclusiveDirectoriesAndLocks = null;
			}
		}

		// Token: 0x0600A77D RID: 42877 RVA: 0x0022A848 File Offset: 0x00228A48
		private void CleanupThread()
		{
			foreach (KeyValuePair<string, Mutex> keyValuePair in this.directoryRootsAndLocks)
			{
				Mutex value = keyValuePair.Value;
				bool flag;
				try
				{
					flag = value.WaitOne(0);
				}
				catch (AbandonedMutexException)
				{
					flag = true;
				}
				catch (UnauthorizedAccessException)
				{
					flag = false;
				}
				if (flag)
				{
					try
					{
						this.CleanupRootDirectory(keyValuePair.Key);
					}
					finally
					{
						value.ReleaseMutex();
					}
				}
			}
		}

		// Token: 0x0600A77E RID: 42878 RVA: 0x0022A8D4 File Offset: 0x00228AD4
		private void CleanupRootDirectory(string directory)
		{
			HashSet<string> hashSet = new HashSet<string>();
			Func<string[]> <>9__0;
			bool flag;
			do
			{
				flag = false;
				string text = "EvaluationDirectoryManager/CleanupRootDirectory";
				Func<string[]> func;
				if ((func = <>9__0) == null)
				{
					func = (<>9__0 = () => Directory.GetDirectories(directory));
				}
				string[] array = FileSystemAccessHelper.IgnoringAccessExceptions<string[]>(text, func, this.evaluationConstants);
				if (array != null && array.Length != 0)
				{
					foreach (string text2 in array)
					{
						if (hashSet.Add(text2))
						{
							this.DeleteDirectoryIfNotInUse(text2);
							flag = true;
						}
					}
				}
			}
			while (flag);
		}

		// Token: 0x0600A77F RID: 42879 RVA: 0x0022A964 File Offset: 0x00228B64
		private void DeleteDirectoryIfNotInUse(string directoryToDelete)
		{
			Action <>9__1;
			PurgeHelper.PurgeIfNotUsed(EvaluationDirectoryManager.ComposeSemaphoreName(directoryToDelete), delegate
			{
				string text = "EvaluationDirectoryManager/DeleteDirectoryIfNotInUse";
				Action action;
				if ((action = <>9__1) == null)
				{
					action = (<>9__1 = delegate
					{
						Directory.Delete(directoryToDelete, true);
					});
				}
				FileSystemAccessHelper.IgnoringAccessExceptions(text, action, this.evaluationConstants);
			});
		}

		// Token: 0x0600A780 RID: 42880 RVA: 0x0022A9A1 File Offset: 0x00228BA1
		internal static string ComposeSemaphoreName(string directory)
		{
			return "MashupSessionCacheSemaphore_" + SharedExclusiveLock.CreateIdentityFromFilePath(directory);
		}

		// Token: 0x0600A781 RID: 42881 RVA: 0x0022A9B3 File Offset: 0x00228BB3
		private static string ComposeMutexName(string directory)
		{
			return "MashupSessionCacheMutex_" + SharedExclusiveLock.CreateIdentityFromFilePath(directory);
		}

		// Token: 0x04005732 RID: 22322
		private const string semaphorePrefix = "MashupSessionCacheSemaphore_";

		// Token: 0x04005733 RID: 22323
		private const string mutexPrefix = "MashupSessionCacheMutex_";

		// Token: 0x04005734 RID: 22324
		private static readonly TimeSpan lockTimeout = TimeSpan.FromSeconds(60.0);

		// Token: 0x04005735 RID: 22325
		private readonly IEvaluationConstants evaluationConstants;

		// Token: 0x04005736 RID: 22326
		private KeyValuePair<string, Mutex>[] directoryRootsAndLocks;

		// Token: 0x04005737 RID: 22327
		private KeyValuePair<string, SharedExclusiveLock>[] exclusiveDirectoriesAndLocks;
	}
}
