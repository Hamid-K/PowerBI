using System;
using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Threading;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Evaluator.Interface;
using Microsoft.Mashup.Shims.Interprocess;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x020019CC RID: 6604
	public static class DynamicDiskCacheRoot
	{
		// Token: 0x0600A735 RID: 42805 RVA: 0x00229764 File Offset: 0x00227964
		public static void SetDirectory(string directory)
		{
			object obj = DynamicDiskCacheRoot.syncRoot;
			lock (obj)
			{
				using (IHostTrace hostTrace = EvaluatorTracing.CreateTrace("DynamicDiskCacheRoot/SetDirectory", null, TraceEventType.Information, null))
				{
					hostTrace.Add("Directory", directory, true);
					if (DynamicDiskCacheRoot.diskCacheRoot != null)
					{
						hostTrace.Add("State", "Already initialized", false);
						throw new InvalidOperationException("Disk cache root has already been initialized to " + directory);
					}
					DynamicDiskCacheRoot.GetInstances(directory);
					Mutex mutex = null;
					string text = null;
					for (int i = 99; i >= 0; i--)
					{
						string text2 = i.ToString(CultureInfo.InvariantCulture);
						string text3 = Path.Combine(directory, text2);
						Mutex mutex2 = null;
						try
						{
							mutex2 = MutexFactory.Create(true, DynamicDiskCacheRoot.ComposeMutexName(text3));
							if (DynamicDiskCacheRoot.TryDelete(text3))
							{
								if (mutex != null)
								{
									mutex.Close();
								}
								mutex = mutex2;
								mutex2 = null;
								text = text3;
							}
						}
						catch (Exception ex) when (SafeExceptions.IsSafeException(ex))
						{
							hostTrace.Add("Instance" + text2, "Used", false);
						}
						finally
						{
							if (mutex2 != null)
							{
								mutex2.Close();
							}
						}
					}
					if (mutex == null)
					{
						hostTrace.Add("State", "Mutex failure", false);
						throw new InvalidOperationException("Unable to acquire cache directory lock on " + directory);
					}
					try
					{
						Directory.CreateDirectory(text);
					}
					catch (Exception ex2) when (SafeExceptions.IsSafeException(ex2))
					{
						if (mutex != null)
						{
							mutex.Close();
						}
						mutex = null;
						hostTrace.Add("State", "Directory creation failure", false);
						throw new InvalidOperationException("Unable to create directory " + text);
					}
					DynamicDiskCacheRoot.cacheRootMutex = mutex;
					DynamicDiskCacheRoot.diskCacheRoot = text;
				}
			}
		}

		// Token: 0x0600A736 RID: 42806 RVA: 0x00229984 File Offset: 0x00227B84
		public static string GetDirectory()
		{
			object obj = DynamicDiskCacheRoot.syncRoot;
			string text;
			lock (obj)
			{
				text = DynamicDiskCacheRoot.diskCacheRoot;
			}
			return text;
		}

		// Token: 0x0600A737 RID: 42807 RVA: 0x002299C4 File Offset: 0x00227BC4
		private static bool TryDelete(string directory)
		{
			try
			{
				if (Directory.Exists(directory))
				{
					Directory.Delete(directory, true);
				}
				else if (File.Exists(directory))
				{
					File.Delete(directory);
				}
			}
			catch (Exception ex) when (SafeExceptions.IsSafeException(ex))
			{
				using (IHostTrace hostTrace = EvaluatorTracing.CreateTrace("DynamicDiskCacheRoot/TryDelete", null, TraceEventType.Information, null))
				{
					hostTrace.Add(ex, true);
				}
			}
			return !Directory.Exists(directory) && !File.Exists(directory);
		}

		// Token: 0x0600A738 RID: 42808 RVA: 0x00229A60 File Offset: 0x00227C60
		private static string ComposeMutexName(string directory)
		{
			return "MashupDiskCacheMutex_" + DynamicDiskCacheRoot.EscapeMutexName(directory);
		}

		// Token: 0x0600A739 RID: 42809 RVA: 0x00227DB6 File Offset: 0x00225FB6
		private static string EscapeMutexName(string name)
		{
			return name.Replace("_", "__").Replace(Path.DirectorySeparatorChar, '_');
		}

		// Token: 0x0600A73A RID: 42810 RVA: 0x00229A74 File Offset: 0x00227C74
		private static string[] SafeGetDirectoryListing(string directory, string pattern)
		{
			try
			{
				if (Directory.Exists(directory))
				{
					return Directory.GetFileSystemEntries(directory, pattern);
				}
			}
			catch (Exception ex) when (SafeExceptions.IsSafeException(ex))
			{
			}
			return EmptyArray<string>.Instance;
		}

		// Token: 0x0600A73B RID: 42811 RVA: 0x00229AC8 File Offset: 0x00227CC8
		private static BitArray GetInstances(string directory)
		{
			BitArray bitArray = new BitArray(100);
			string[] array = DynamicDiskCacheRoot.SafeGetDirectoryListing(directory, "??");
			for (int i = 0; i < array.Length; i++)
			{
				int num;
				if (int.TryParse(array[i], out num) && num >= 0 && num < 100)
				{
					bitArray[num] = true;
				}
			}
			return bitArray;
		}

		// Token: 0x04005706 RID: 22278
		private const int MaxInstanceCount = 100;

		// Token: 0x04005707 RID: 22279
		private const string InstancePattern = "??";

		// Token: 0x04005708 RID: 22280
		private const string cacheMutexPrefix = "MashupDiskCacheMutex_";

		// Token: 0x04005709 RID: 22281
		private static readonly object syncRoot = new object();

		// Token: 0x0400570A RID: 22282
		private static string diskCacheRoot;

		// Token: 0x0400570B RID: 22283
		private static Mutex cacheRootMutex;
	}
}
