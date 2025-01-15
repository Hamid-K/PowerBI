using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Microsoft.AnalysisServices.Utilities
{
	// Token: 0x0200013B RID: 315
	internal static class GlobalContext
	{
		// Token: 0x060010C1 RID: 4289 RVA: 0x0003A583 File Offset: 0x00038783
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AcquireGlobalLock(string globalyUniqueName)
		{
			Monitor.Enter(GlobalContext.GetGlobalLock(globalyUniqueName));
		}

		// Token: 0x060010C2 RID: 4290 RVA: 0x0003A590 File Offset: 0x00038790
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void ReleaseGlobalLock(string globalyUniqueName)
		{
			Monitor.Exit(GlobalContext.GetGlobalLock(globalyUniqueName));
		}

		// Token: 0x060010C3 RID: 4291 RVA: 0x0003A59D File Offset: 0x0003879D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IDisposable CreateGlobalLockScope(string globalyUniqueName)
		{
			return new GlobalContext.LockScope(globalyUniqueName);
		}

		// Token: 0x060010C4 RID: 4292 RVA: 0x0003A5A5 File Offset: 0x000387A5
		public static object GetGlobalObject(string globalyUniqueName)
		{
			return AppDomain.CurrentDomain.GetData(globalyUniqueName);
		}

		// Token: 0x060010C5 RID: 4293 RVA: 0x0003A5B4 File Offset: 0x000387B4
		public static T GetGlobalObject<T>(string globalyUniqueName)
		{
			object globalObject = GlobalContext.GetGlobalObject(globalyUniqueName);
			if (globalObject == null)
			{
				return default(T);
			}
			return (T)((object)globalObject);
		}

		// Token: 0x060010C6 RID: 4294 RVA: 0x0003A5DB File Offset: 0x000387DB
		public static bool TryGetGlobalObject(string globalyUniqueName, out object @object)
		{
			@object = AppDomain.CurrentDomain.GetData(globalyUniqueName);
			return @object != null;
		}

		// Token: 0x060010C7 RID: 4295 RVA: 0x0003A5F0 File Offset: 0x000387F0
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

		// Token: 0x060010C8 RID: 4296 RVA: 0x0003A61D File Offset: 0x0003881D
		public static void SetGlobalObject(string globalyUniqueName, object @object)
		{
			AppDomain.CurrentDomain.SetData(globalyUniqueName, @object);
		}

		// Token: 0x060010C9 RID: 4297 RVA: 0x0003A62B File Offset: 0x0003882B
		public static void ClearGlobalObject(string globalyUniqueName)
		{
			AppDomain.CurrentDomain.SetData(globalyUniqueName, null);
		}

		// Token: 0x060010CA RID: 4298 RVA: 0x0003A63C File Offset: 0x0003883C
		public static object GetThreadObject(string threadUniqueName)
		{
			object obj;
			if (!GlobalContext.GetThreadObjects().TryGetValue(threadUniqueName, out obj))
			{
				obj = null;
			}
			return obj;
		}

		// Token: 0x060010CB RID: 4299 RVA: 0x0003A65C File Offset: 0x0003885C
		public static T GetThreadObject<T>(string threadUniqueName)
		{
			object threadObject = GlobalContext.GetThreadObject(threadUniqueName);
			if (threadObject == null)
			{
				return default(T);
			}
			return (T)((object)threadObject);
		}

		// Token: 0x060010CC RID: 4300 RVA: 0x0003A683 File Offset: 0x00038883
		public static bool TryGetThreadObject(string threadUniqueName, out object @object)
		{
			return GlobalContext.GetThreadObjects().TryGetValue(threadUniqueName, out @object);
		}

		// Token: 0x060010CD RID: 4301 RVA: 0x0003A694 File Offset: 0x00038894
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

		// Token: 0x060010CE RID: 4302 RVA: 0x0003A6C1 File Offset: 0x000388C1
		public static void SetThreadObject(string threadUniqueName, object @object)
		{
			GlobalContext.GetThreadObjects()[threadUniqueName] = @object;
		}

		// Token: 0x060010CF RID: 4303 RVA: 0x0003A6CF File Offset: 0x000388CF
		public static void ClearThreadObject(string threadUniqueName)
		{
			GlobalContext.GetThreadObjects().Remove(threadUniqueName);
		}

		// Token: 0x060010D0 RID: 4304 RVA: 0x0003A6E0 File Offset: 0x000388E0
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

		// Token: 0x060010D1 RID: 4305 RVA: 0x0003A740 File Offset: 0x00038940
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

		// Token: 0x060010D2 RID: 4306 RVA: 0x0003A7A8 File Offset: 0x000389A8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static IDictionary<string, object> GetThreadObjects()
		{
			if (GlobalContext.threadObjects == null)
			{
				GlobalContext.EnsureThreadObjects();
			}
			return GlobalContext.threadObjects;
		}

		// Token: 0x060010D3 RID: 4307 RVA: 0x0003A7BC File Offset: 0x000389BC
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

		// Token: 0x04000AC5 RID: 2757
		private const string AppDomainKey_GlobalLocks = "MS_AS_GLOBAL_LOCKS";

		// Token: 0x04000AC6 RID: 2758
		private const string AppDomainKey_ThreadObjects = "MS_AS_THREAD_OBJECTS";

		// Token: 0x04000AC7 RID: 2759
		private static readonly IDictionary<string, object> globalLocks = GlobalContext.InitializeGlobalLocks();

		// Token: 0x04000AC8 RID: 2760
		[ThreadStatic]
		private static IDictionary<string, object> threadObjects;

		// Token: 0x020001DB RID: 475
		private sealed class LockScope : Disposable
		{
			// Token: 0x0600140D RID: 5133 RVA: 0x0004513F File Offset: 0x0004333F
			public LockScope(string globalyUniqueName)
			{
				this.@lock = GlobalContext.GetGlobalLock(globalyUniqueName);
				Monitor.Enter(this.@lock);
			}

			// Token: 0x0600140E RID: 5134 RVA: 0x0004515E File Offset: 0x0004335E
			protected override void Dispose(bool disposing)
			{
				if (disposing)
				{
					Monitor.Exit(this.@lock);
				}
				base.Dispose(disposing);
			}

			// Token: 0x040011AB RID: 4523
			private readonly object @lock;
		}
	}
}
