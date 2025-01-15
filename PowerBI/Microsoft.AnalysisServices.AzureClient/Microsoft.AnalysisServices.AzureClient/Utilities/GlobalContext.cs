using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Microsoft.AnalysisServices.AzureClient.Utilities
{
	// Token: 0x0200002A RID: 42
	internal static class GlobalContext
	{
		// Token: 0x0600013C RID: 316 RVA: 0x00006977 File Offset: 0x00004B77
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AcquireGlobalLock(string globalyUniqueName)
		{
			Monitor.Enter(GlobalContext.GetGlobalLock(globalyUniqueName));
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00006984 File Offset: 0x00004B84
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void ReleaseGlobalLock(string globalyUniqueName)
		{
			Monitor.Exit(GlobalContext.GetGlobalLock(globalyUniqueName));
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00006991 File Offset: 0x00004B91
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IDisposable CreateGlobalLockScope(string globalyUniqueName)
		{
			return new GlobalContext.LockScope(globalyUniqueName);
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00006999 File Offset: 0x00004B99
		public static object GetGlobalObject(string globalyUniqueName)
		{
			return AppDomain.CurrentDomain.GetData(globalyUniqueName);
		}

		// Token: 0x06000140 RID: 320 RVA: 0x000069A8 File Offset: 0x00004BA8
		public static T GetGlobalObject<T>(string globalyUniqueName)
		{
			object globalObject = GlobalContext.GetGlobalObject(globalyUniqueName);
			if (globalObject == null)
			{
				return default(T);
			}
			return (T)((object)globalObject);
		}

		// Token: 0x06000141 RID: 321 RVA: 0x000069CF File Offset: 0x00004BCF
		public static bool TryGetGlobalObject(string globalyUniqueName, out object @object)
		{
			@object = AppDomain.CurrentDomain.GetData(globalyUniqueName);
			return @object != null;
		}

		// Token: 0x06000142 RID: 322 RVA: 0x000069E4 File Offset: 0x00004BE4
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

		// Token: 0x06000143 RID: 323 RVA: 0x00006A11 File Offset: 0x00004C11
		public static void SetGlobalObject(string globalyUniqueName, object @object)
		{
			AppDomain.CurrentDomain.SetData(globalyUniqueName, @object);
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00006A1F File Offset: 0x00004C1F
		public static void ClearGlobalObject(string globalyUniqueName)
		{
			AppDomain.CurrentDomain.SetData(globalyUniqueName, null);
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00006A30 File Offset: 0x00004C30
		public static object GetThreadObject(string threadUniqueName)
		{
			object obj;
			if (!GlobalContext.GetThreadObjects().TryGetValue(threadUniqueName, out obj))
			{
				obj = null;
			}
			return obj;
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00006A50 File Offset: 0x00004C50
		public static T GetThreadObject<T>(string threadUniqueName)
		{
			object threadObject = GlobalContext.GetThreadObject(threadUniqueName);
			if (threadObject == null)
			{
				return default(T);
			}
			return (T)((object)threadObject);
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00006A77 File Offset: 0x00004C77
		public static bool TryGetThreadObject(string threadUniqueName, out object @object)
		{
			return GlobalContext.GetThreadObjects().TryGetValue(threadUniqueName, out @object);
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00006A88 File Offset: 0x00004C88
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

		// Token: 0x06000149 RID: 329 RVA: 0x00006AB5 File Offset: 0x00004CB5
		public static void SetThreadObject(string threadUniqueName, object @object)
		{
			GlobalContext.GetThreadObjects()[threadUniqueName] = @object;
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00006AC3 File Offset: 0x00004CC3
		public static void ClearThreadObject(string threadUniqueName)
		{
			GlobalContext.GetThreadObjects().Remove(threadUniqueName);
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00006AD4 File Offset: 0x00004CD4
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

		// Token: 0x0600014C RID: 332 RVA: 0x00006B34 File Offset: 0x00004D34
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

		// Token: 0x0600014D RID: 333 RVA: 0x00006B9C File Offset: 0x00004D9C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static IDictionary<string, object> GetThreadObjects()
		{
			if (GlobalContext.threadObjects == null)
			{
				GlobalContext.EnsureThreadObjects();
			}
			return GlobalContext.threadObjects;
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00006BB0 File Offset: 0x00004DB0
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

		// Token: 0x040000CA RID: 202
		private const string AppDomainKey_GlobalLocks = "MS_AS_GLOBAL_LOCKS";

		// Token: 0x040000CB RID: 203
		private const string AppDomainKey_ThreadObjects = "MS_AS_THREAD_OBJECTS";

		// Token: 0x040000CC RID: 204
		private static readonly IDictionary<string, object> globalLocks = GlobalContext.InitializeGlobalLocks();

		// Token: 0x040000CD RID: 205
		[ThreadStatic]
		private static IDictionary<string, object> threadObjects;

		// Token: 0x02000070 RID: 112
		private sealed class LockScope : Disposable
		{
			// Token: 0x060002E4 RID: 740 RVA: 0x0000C453 File Offset: 0x0000A653
			public LockScope(string globalyUniqueName)
			{
				this.@lock = GlobalContext.GetGlobalLock(globalyUniqueName);
				Monitor.Enter(this.@lock);
			}

			// Token: 0x060002E5 RID: 741 RVA: 0x0000C472 File Offset: 0x0000A672
			protected override void Dispose(bool disposing)
			{
				if (disposing)
				{
					Monitor.Exit(this.@lock);
				}
				base.Dispose(disposing);
			}

			// Token: 0x0400021D RID: 541
			private readonly object @lock;
		}
	}
}
