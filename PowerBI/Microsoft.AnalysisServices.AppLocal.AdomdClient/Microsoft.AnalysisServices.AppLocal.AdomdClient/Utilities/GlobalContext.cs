using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Microsoft.AnalysisServices.AdomdClient.Utilities
{
	// Token: 0x02000146 RID: 326
	internal static class GlobalContext
	{
		// Token: 0x06001033 RID: 4147 RVA: 0x00037C7F File Offset: 0x00035E7F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AcquireGlobalLock(string globalyUniqueName)
		{
			Monitor.Enter(GlobalContext.GetGlobalLock(globalyUniqueName));
		}

		// Token: 0x06001034 RID: 4148 RVA: 0x00037C8C File Offset: 0x00035E8C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void ReleaseGlobalLock(string globalyUniqueName)
		{
			Monitor.Exit(GlobalContext.GetGlobalLock(globalyUniqueName));
		}

		// Token: 0x06001035 RID: 4149 RVA: 0x00037C99 File Offset: 0x00035E99
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IDisposable CreateGlobalLockScope(string globalyUniqueName)
		{
			return new GlobalContext.LockScope(globalyUniqueName);
		}

		// Token: 0x06001036 RID: 4150 RVA: 0x00037CA1 File Offset: 0x00035EA1
		public static object GetGlobalObject(string globalyUniqueName)
		{
			return AppDomain.CurrentDomain.GetData(globalyUniqueName);
		}

		// Token: 0x06001037 RID: 4151 RVA: 0x00037CB0 File Offset: 0x00035EB0
		public static T GetGlobalObject<T>(string globalyUniqueName)
		{
			object globalObject = GlobalContext.GetGlobalObject(globalyUniqueName);
			if (globalObject == null)
			{
				return default(T);
			}
			return (T)((object)globalObject);
		}

		// Token: 0x06001038 RID: 4152 RVA: 0x00037CD7 File Offset: 0x00035ED7
		public static bool TryGetGlobalObject(string globalyUniqueName, out object @object)
		{
			@object = AppDomain.CurrentDomain.GetData(globalyUniqueName);
			return @object != null;
		}

		// Token: 0x06001039 RID: 4153 RVA: 0x00037CEC File Offset: 0x00035EEC
		public static bool TryGetGlobalObject<T>(string globalyUniqueName, out T @object)
		{
			object obj;
			if (!GlobalContext.TryGetGlobalObject(globalyUniqueName, out obj))
			{
				@object = default(T);
				return false;
			}
			@object = (T)((object)obj);
			return true;
		}

		// Token: 0x0600103A RID: 4154 RVA: 0x00037D19 File Offset: 0x00035F19
		public static void SetGlobalObject(string globalyUniqueName, object @object)
		{
			AppDomain.CurrentDomain.SetData(globalyUniqueName, @object);
		}

		// Token: 0x0600103B RID: 4155 RVA: 0x00037D27 File Offset: 0x00035F27
		public static void ClearGlobalObject(string globalyUniqueName)
		{
			AppDomain.CurrentDomain.SetData(globalyUniqueName, null);
		}

		// Token: 0x0600103C RID: 4156 RVA: 0x00037D38 File Offset: 0x00035F38
		public static object GetThreadObject(string threadUniqueName)
		{
			object obj;
			if (!GlobalContext.GetThreadObjects().TryGetValue(threadUniqueName, out obj))
			{
				obj = null;
			}
			return obj;
		}

		// Token: 0x0600103D RID: 4157 RVA: 0x00037D58 File Offset: 0x00035F58
		public static T GetThreadObject<T>(string threadUniqueName)
		{
			object threadObject = GlobalContext.GetThreadObject(threadUniqueName);
			if (threadObject == null)
			{
				return default(T);
			}
			return (T)((object)threadObject);
		}

		// Token: 0x0600103E RID: 4158 RVA: 0x00037D7F File Offset: 0x00035F7F
		public static bool TryGetThreadObject(string threadUniqueName, out object @object)
		{
			return GlobalContext.GetThreadObjects().TryGetValue(threadUniqueName, out @object);
		}

		// Token: 0x0600103F RID: 4159 RVA: 0x00037D90 File Offset: 0x00035F90
		public static bool TryGetThreadObject<T>(string threadUniqueName, out T @object)
		{
			object obj;
			if (!GlobalContext.TryGetThreadObject(threadUniqueName, out obj))
			{
				@object = default(T);
				return false;
			}
			@object = (T)((object)obj);
			return true;
		}

		// Token: 0x06001040 RID: 4160 RVA: 0x00037DBD File Offset: 0x00035FBD
		public static void SetThreadObject(string threadUniqueName, object @object)
		{
			GlobalContext.GetThreadObjects()[threadUniqueName] = @object;
		}

		// Token: 0x06001041 RID: 4161 RVA: 0x00037DCB File Offset: 0x00035FCB
		public static void ClearThreadObject(string threadUniqueName)
		{
			GlobalContext.GetThreadObjects().Remove(threadUniqueName);
		}

		// Token: 0x06001042 RID: 4162 RVA: 0x00037DDC File Offset: 0x00035FDC
		internal static object GetGlobalLock(string globalyUniqueName)
		{
			IDictionary<string, object> dictionary = GlobalContext.globalLocks;
			object obj2;
			lock (dictionary)
			{
				object obj;
				if (!GlobalContext.globalLocks.TryGetValue(globalyUniqueName, out obj))
				{
					obj = new object();
					GlobalContext.globalLocks.Add(globalyUniqueName, obj);
				}
				obj2 = obj;
			}
			return obj2;
		}

		// Token: 0x06001043 RID: 4163 RVA: 0x00037E3C File Offset: 0x0003603C
		private static IDictionary<string, object> InitializeGlobalLocks()
		{
			AppDomain currentDomain = AppDomain.CurrentDomain;
			IDictionary<string, object> dictionary;
			lock (currentDomain)
			{
				dictionary = (IDictionary<string, object>)AppDomain.CurrentDomain.GetData("MS_AS_GLOBAL_LOCKS");
				if (dictionary == null)
				{
					dictionary = new Dictionary<string, object>();
					AppDomain.CurrentDomain.SetData("MS_AS_GLOBAL_LOCKS", dictionary);
				}
			}
			return dictionary;
		}

		// Token: 0x06001044 RID: 4164 RVA: 0x00037EA4 File Offset: 0x000360A4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static IDictionary<string, object> GetThreadObjects()
		{
			if (GlobalContext.threadObjects == null)
			{
				GlobalContext.EnsureThreadObjects();
			}
			return GlobalContext.threadObjects;
		}

		// Token: 0x06001045 RID: 4165 RVA: 0x00037EB8 File Offset: 0x000360B8
		private static void EnsureThreadObjects()
		{
			AppDomain currentDomain = AppDomain.CurrentDomain;
			IDictionary<int, IDictionary<string, object>> dictionary;
			lock (currentDomain)
			{
				dictionary = (IDictionary<int, IDictionary<string, object>>)AppDomain.CurrentDomain.GetData("MS_AS_THREAD_OBJECTS");
				if (dictionary == null)
				{
					dictionary = new Dictionary<int, IDictionary<string, object>>();
					AppDomain.CurrentDomain.SetData("MS_AS_THREAD_OBJECTS", dictionary);
				}
			}
			IDictionary<int, IDictionary<string, object>> dictionary2 = dictionary;
			lock (dictionary2)
			{
				if (!dictionary.TryGetValue(Thread.CurrentThread.ManagedThreadId, out GlobalContext.threadObjects))
				{
					GlobalContext.threadObjects = new Dictionary<string, object>();
					dictionary.Add(Thread.CurrentThread.ManagedThreadId, GlobalContext.threadObjects);
				}
			}
		}

		// Token: 0x04000B0C RID: 2828
		private const string AppDomainKey_GlobalLocks = "MS_AS_GLOBAL_LOCKS";

		// Token: 0x04000B0D RID: 2829
		private const string AppDomainKey_ThreadObjects = "MS_AS_THREAD_OBJECTS";

		// Token: 0x04000B0E RID: 2830
		private static readonly IDictionary<string, object> globalLocks = GlobalContext.InitializeGlobalLocks();

		// Token: 0x04000B0F RID: 2831
		[ThreadStatic]
		private static IDictionary<string, object> threadObjects;

		// Token: 0x020001FE RID: 510
		private sealed class LockScope : Disposable
		{
			// Token: 0x060014B2 RID: 5298 RVA: 0x00046F1F File Offset: 0x0004511F
			public LockScope(string globalyUniqueName)
			{
				this.@lock = GlobalContext.GetGlobalLock(globalyUniqueName);
				Monitor.Enter(this.@lock);
			}

			// Token: 0x060014B3 RID: 5299 RVA: 0x00046F3E File Offset: 0x0004513E
			protected override void Dispose(bool disposing)
			{
				if (disposing)
				{
					Monitor.Exit(this.@lock);
				}
				base.Dispose(disposing);
			}

			// Token: 0x04000EF5 RID: 3829
			private readonly object @lock;
		}
	}
}
