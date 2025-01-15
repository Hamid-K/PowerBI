using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Microsoft.AnalysisServices.AdomdClient.Utilities
{
	// Token: 0x02000146 RID: 326
	internal static class GlobalContext
	{
		// Token: 0x06001026 RID: 4134 RVA: 0x0003794F File Offset: 0x00035B4F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AcquireGlobalLock(string globalyUniqueName)
		{
			Monitor.Enter(GlobalContext.GetGlobalLock(globalyUniqueName));
		}

		// Token: 0x06001027 RID: 4135 RVA: 0x0003795C File Offset: 0x00035B5C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void ReleaseGlobalLock(string globalyUniqueName)
		{
			Monitor.Exit(GlobalContext.GetGlobalLock(globalyUniqueName));
		}

		// Token: 0x06001028 RID: 4136 RVA: 0x00037969 File Offset: 0x00035B69
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IDisposable CreateGlobalLockScope(string globalyUniqueName)
		{
			return new GlobalContext.LockScope(globalyUniqueName);
		}

		// Token: 0x06001029 RID: 4137 RVA: 0x00037971 File Offset: 0x00035B71
		public static object GetGlobalObject(string globalyUniqueName)
		{
			return AppDomain.CurrentDomain.GetData(globalyUniqueName);
		}

		// Token: 0x0600102A RID: 4138 RVA: 0x00037980 File Offset: 0x00035B80
		public static T GetGlobalObject<T>(string globalyUniqueName)
		{
			object globalObject = GlobalContext.GetGlobalObject(globalyUniqueName);
			if (globalObject == null)
			{
				return default(T);
			}
			return (T)((object)globalObject);
		}

		// Token: 0x0600102B RID: 4139 RVA: 0x000379A7 File Offset: 0x00035BA7
		public static bool TryGetGlobalObject(string globalyUniqueName, out object @object)
		{
			@object = AppDomain.CurrentDomain.GetData(globalyUniqueName);
			return @object != null;
		}

		// Token: 0x0600102C RID: 4140 RVA: 0x000379BC File Offset: 0x00035BBC
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

		// Token: 0x0600102D RID: 4141 RVA: 0x000379E9 File Offset: 0x00035BE9
		public static void SetGlobalObject(string globalyUniqueName, object @object)
		{
			AppDomain.CurrentDomain.SetData(globalyUniqueName, @object);
		}

		// Token: 0x0600102E RID: 4142 RVA: 0x000379F7 File Offset: 0x00035BF7
		public static void ClearGlobalObject(string globalyUniqueName)
		{
			AppDomain.CurrentDomain.SetData(globalyUniqueName, null);
		}

		// Token: 0x0600102F RID: 4143 RVA: 0x00037A08 File Offset: 0x00035C08
		public static object GetThreadObject(string threadUniqueName)
		{
			object obj;
			if (!GlobalContext.GetThreadObjects().TryGetValue(threadUniqueName, out obj))
			{
				obj = null;
			}
			return obj;
		}

		// Token: 0x06001030 RID: 4144 RVA: 0x00037A28 File Offset: 0x00035C28
		public static T GetThreadObject<T>(string threadUniqueName)
		{
			object threadObject = GlobalContext.GetThreadObject(threadUniqueName);
			if (threadObject == null)
			{
				return default(T);
			}
			return (T)((object)threadObject);
		}

		// Token: 0x06001031 RID: 4145 RVA: 0x00037A4F File Offset: 0x00035C4F
		public static bool TryGetThreadObject(string threadUniqueName, out object @object)
		{
			return GlobalContext.GetThreadObjects().TryGetValue(threadUniqueName, out @object);
		}

		// Token: 0x06001032 RID: 4146 RVA: 0x00037A60 File Offset: 0x00035C60
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

		// Token: 0x06001033 RID: 4147 RVA: 0x00037A8D File Offset: 0x00035C8D
		public static void SetThreadObject(string threadUniqueName, object @object)
		{
			GlobalContext.GetThreadObjects()[threadUniqueName] = @object;
		}

		// Token: 0x06001034 RID: 4148 RVA: 0x00037A9B File Offset: 0x00035C9B
		public static void ClearThreadObject(string threadUniqueName)
		{
			GlobalContext.GetThreadObjects().Remove(threadUniqueName);
		}

		// Token: 0x06001035 RID: 4149 RVA: 0x00037AAC File Offset: 0x00035CAC
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

		// Token: 0x06001036 RID: 4150 RVA: 0x00037B0C File Offset: 0x00035D0C
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

		// Token: 0x06001037 RID: 4151 RVA: 0x00037B74 File Offset: 0x00035D74
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static IDictionary<string, object> GetThreadObjects()
		{
			if (GlobalContext.threadObjects == null)
			{
				GlobalContext.EnsureThreadObjects();
			}
			return GlobalContext.threadObjects;
		}

		// Token: 0x06001038 RID: 4152 RVA: 0x00037B88 File Offset: 0x00035D88
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

		// Token: 0x04000AFF RID: 2815
		private const string AppDomainKey_GlobalLocks = "MS_AS_GLOBAL_LOCKS";

		// Token: 0x04000B00 RID: 2816
		private const string AppDomainKey_ThreadObjects = "MS_AS_THREAD_OBJECTS";

		// Token: 0x04000B01 RID: 2817
		private static readonly IDictionary<string, object> globalLocks = GlobalContext.InitializeGlobalLocks();

		// Token: 0x04000B02 RID: 2818
		[ThreadStatic]
		private static IDictionary<string, object> threadObjects;

		// Token: 0x020001FE RID: 510
		private sealed class LockScope : Disposable
		{
			// Token: 0x060014A5 RID: 5285 RVA: 0x000469E3 File Offset: 0x00044BE3
			public LockScope(string globalyUniqueName)
			{
				this.@lock = GlobalContext.GetGlobalLock(globalyUniqueName);
				Monitor.Enter(this.@lock);
			}

			// Token: 0x060014A6 RID: 5286 RVA: 0x00046A02 File Offset: 0x00044C02
			protected override void Dispose(bool disposing)
			{
				if (disposing)
				{
					Monitor.Exit(this.@lock);
				}
				base.Dispose(disposing);
			}

			// Token: 0x04000EDF RID: 3807
			private readonly object @lock;
		}
	}
}
