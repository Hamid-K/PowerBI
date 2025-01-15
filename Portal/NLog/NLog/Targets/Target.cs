using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using NLog.Common;
using NLog.Config;
using NLog.Internal;
using NLog.Layouts;

namespace NLog.Targets
{
	// Token: 0x02000053 RID: 83
	[NLogConfigurationItem]
	public abstract class Target : ISupportsInitialize, IDisposable
	{
		// Token: 0x17000131 RID: 305
		// (get) Token: 0x060007A5 RID: 1957 RVA: 0x000132E4 File Offset: 0x000114E4
		// (set) Token: 0x060007A6 RID: 1958 RVA: 0x000132EC File Offset: 0x000114EC
		internal StackTraceUsage StackTraceUsage { get; private set; }

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x060007A7 RID: 1959 RVA: 0x000132F5 File Offset: 0x000114F5
		// (set) Token: 0x060007A8 RID: 1960 RVA: 0x000132FD File Offset: 0x000114FD
		public string Name { get; set; }

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x060007A9 RID: 1961 RVA: 0x00013306 File Offset: 0x00011506
		// (set) Token: 0x060007AA RID: 1962 RVA: 0x0001330E File Offset: 0x0001150E
		public bool OptimizeBufferReuse { get; set; }

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x060007AB RID: 1963 RVA: 0x00013317 File Offset: 0x00011517
		protected object SyncRoot { get; } = new object();

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x060007AC RID: 1964 RVA: 0x0001331F File Offset: 0x0001151F
		// (set) Token: 0x060007AD RID: 1965 RVA: 0x00013327 File Offset: 0x00011527
		private protected LoggingConfiguration LoggingConfiguration { protected get; private set; }

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x060007AE RID: 1966 RVA: 0x00013330 File Offset: 0x00011530
		protected bool IsInitialized
		{
			get
			{
				if (this._isInitialized)
				{
					return true;
				}
				object syncRoot = this.SyncRoot;
				bool isInitialized;
				lock (syncRoot)
				{
					isInitialized = this._isInitialized;
				}
				return isInitialized;
			}
		}

		// Token: 0x060007AF RID: 1967 RVA: 0x00013380 File Offset: 0x00011580
		void ISupportsInitialize.Initialize(LoggingConfiguration configuration)
		{
			object syncRoot = this.SyncRoot;
			lock (syncRoot)
			{
				bool isInitialized = this._isInitialized;
				this.Initialize(configuration);
				if (isInitialized && configuration != null)
				{
					this.FindAllLayouts();
				}
			}
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x000133D4 File Offset: 0x000115D4
		void ISupportsInitialize.Close()
		{
			this.Close();
		}

		// Token: 0x060007B1 RID: 1969 RVA: 0x000133DC File Offset: 0x000115DC
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x000133EC File Offset: 0x000115EC
		public void Flush(AsyncContinuation asyncContinuation)
		{
			if (asyncContinuation == null)
			{
				throw new ArgumentNullException("asyncContinuation");
			}
			asyncContinuation = AsyncHelpers.PreventMultipleCalls(asyncContinuation);
			object syncRoot = this.SyncRoot;
			lock (syncRoot)
			{
				if (!this.IsInitialized)
				{
					asyncContinuation(null);
				}
				else
				{
					try
					{
						this.FlushAsync(asyncContinuation);
					}
					catch (Exception ex)
					{
						if (ex.MustBeRethrown())
						{
							throw;
						}
						asyncContinuation(ex);
					}
				}
			}
		}

		// Token: 0x060007B3 RID: 1971 RVA: 0x00013478 File Offset: 0x00011678
		public void PrecalculateVolatileLayouts(LogEventInfo logEvent)
		{
			if (this._allLayoutsAreThreadAgnostic && (!this._oneLayoutIsMutableUnsafe || logEvent.IsLogEventMutableSafe()))
			{
				return;
			}
			if (this.OptimizeBufferReuse && this._allLayoutsAreThreadSafe)
			{
				this.PrecalculateVolatileLayoutsConcurrent(logEvent);
				return;
			}
			this.PrecalculateVolatileLayoutsWithLock(logEvent);
		}

		// Token: 0x060007B4 RID: 1972 RVA: 0x000134B4 File Offset: 0x000116B4
		private void PrecalculateVolatileLayoutsConcurrent(LogEventInfo logEvent)
		{
			if (!this.IsInitialized)
			{
				return;
			}
			if (this._allLayouts == null)
			{
				return;
			}
			if (this._precalculateStringBuilderPool == null)
			{
				Interlocked.CompareExchange<StringBuilderPool>(ref this._precalculateStringBuilderPool, new StringBuilderPool(Environment.ProcessorCount * 2, 1024, 524288), null);
			}
			using (StringBuilderPool.ItemHolder itemHolder = this._precalculateStringBuilderPool.Acquire())
			{
				foreach (Layout layout in this._allLayouts)
				{
					itemHolder.Item.ClearBuilder();
					layout.PrecalculateBuilder(logEvent, itemHolder.Item);
				}
			}
		}

		// Token: 0x060007B5 RID: 1973 RVA: 0x0001357C File Offset: 0x0001177C
		private void PrecalculateVolatileLayoutsWithLock(LogEventInfo logEvent)
		{
			object syncRoot = this.SyncRoot;
			lock (syncRoot)
			{
				if (this._isInitialized)
				{
					if (this._allLayouts != null)
					{
						if (this.OptimizeBufferReuse)
						{
							using (ReusableObjectCreator<StringBuilder>.LockOject lockOject = this.ReusableLayoutBuilder.Allocate())
							{
								using (List<Layout>.Enumerator enumerator = this._allLayouts.GetEnumerator())
								{
									while (enumerator.MoveNext())
									{
										Layout layout = enumerator.Current;
										lockOject.Result.ClearBuilder();
										layout.PrecalculateBuilder(logEvent, lockOject.Result);
									}
									return;
								}
							}
						}
						foreach (Layout layout2 in this._allLayouts)
						{
							layout2.Precalculate(logEvent);
						}
					}
				}
			}
		}

		// Token: 0x060007B6 RID: 1974 RVA: 0x00013690 File Offset: 0x00011890
		public override string ToString()
		{
			TargetAttribute customAttribute = base.GetType().GetCustomAttribute<TargetAttribute>();
			if (customAttribute != null)
			{
				return customAttribute.Name + " Target[" + (this.Name ?? "(unnamed)") + "]";
			}
			return base.GetType().Name;
		}

		// Token: 0x060007B7 RID: 1975 RVA: 0x000136DC File Offset: 0x000118DC
		public void WriteAsyncLogEvent(AsyncLogEventInfo logEvent)
		{
			if (!this.IsInitialized)
			{
				object obj = this.SyncRoot;
				lock (obj)
				{
					logEvent.Continuation(null);
				}
				return;
			}
			if (this._initializeException != null)
			{
				object obj = this.SyncRoot;
				lock (obj)
				{
					logEvent.Continuation(this.CreateInitException());
				}
				return;
			}
			AsyncContinuation asyncContinuation = AsyncHelpers.PreventMultipleCalls(logEvent.Continuation);
			AsyncLogEventInfo asyncLogEventInfo = logEvent.LogEvent.WithContinuation(asyncContinuation);
			try
			{
				this.WriteAsyncThreadSafe(asyncLogEventInfo);
			}
			catch (Exception ex)
			{
				if (ex.MustBeRethrown())
				{
					throw;
				}
				asyncLogEventInfo.Continuation(ex);
			}
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x000137C0 File Offset: 0x000119C0
		public void WriteAsyncLogEvents(params AsyncLogEventInfo[] logEvents)
		{
			if (logEvents == null || logEvents.Length == 0)
			{
				return;
			}
			this.WriteAsyncLogEvents(logEvents);
		}

		// Token: 0x060007B9 RID: 1977 RVA: 0x000137D4 File Offset: 0x000119D4
		public void WriteAsyncLogEvents(IList<AsyncLogEventInfo> logEvents)
		{
			if (logEvents == null || logEvents.Count == 0)
			{
				return;
			}
			if (!this.IsInitialized)
			{
				object obj = this.SyncRoot;
				lock (obj)
				{
					for (int i = 0; i < logEvents.Count; i++)
					{
						logEvents[i].Continuation(null);
					}
				}
				return;
			}
			if (this._initializeException != null)
			{
				object obj = this.SyncRoot;
				lock (obj)
				{
					for (int j = 0; j < logEvents.Count; j++)
					{
						logEvents[j].Continuation(this.CreateInitException());
					}
				}
				return;
			}
			IList<AsyncLogEventInfo> list;
			if (this.OptimizeBufferReuse)
			{
				for (int k = 0; k < logEvents.Count; k++)
				{
					logEvents[k] = logEvents[k].LogEvent.WithContinuation(AsyncHelpers.PreventMultipleCalls(logEvents[k].Continuation));
				}
				list = logEvents;
			}
			else
			{
				AsyncLogEventInfo[] array = new AsyncLogEventInfo[logEvents.Count];
				for (int l = 0; l < logEvents.Count; l++)
				{
					AsyncLogEventInfo asyncLogEventInfo = logEvents[l];
					array[l] = asyncLogEventInfo.LogEvent.WithContinuation(AsyncHelpers.PreventMultipleCalls(asyncLogEventInfo.Continuation));
				}
				list = array;
			}
			try
			{
				this.WriteAsyncThreadSafe(list);
			}
			catch (Exception ex)
			{
				if (ex.MustBeRethrown())
				{
					throw;
				}
				for (int m = 0; m < list.Count; m++)
				{
					list[m].Continuation(ex);
				}
			}
		}

		// Token: 0x060007BA RID: 1978 RVA: 0x000139A4 File Offset: 0x00011BA4
		internal void Initialize(LoggingConfiguration configuration)
		{
			object syncRoot = this.SyncRoot;
			lock (syncRoot)
			{
				this.LoggingConfiguration = configuration;
				if (!this.IsInitialized)
				{
					PropertyHelper.CheckRequiredParameters(this);
					try
					{
						this.InitializeTarget();
						this._initializeException = null;
						if (!this._scannedForLayouts)
						{
							InternalLogger.Debug<Target>("{0}: InitializeTarget is done but not scanned For Layouts", this);
							this.FindAllLayouts();
						}
					}
					catch (Exception ex)
					{
						InternalLogger.Error(ex, "{0}: Error initializing target", new object[] { this });
						this._initializeException = ex;
						if (ex.MustBeRethrown())
						{
							throw;
						}
					}
					finally
					{
						this._isInitialized = true;
					}
				}
			}
		}

		// Token: 0x060007BB RID: 1979 RVA: 0x00013A68 File Offset: 0x00011C68
		internal void Close()
		{
			object syncRoot = this.SyncRoot;
			lock (syncRoot)
			{
				this.LoggingConfiguration = null;
				if (this.IsInitialized)
				{
					this._isInitialized = false;
					try
					{
						if (this._initializeException == null)
						{
							InternalLogger.Debug<Target>("Closing target '{0}'.", this);
							this.CloseTarget();
							InternalLogger.Debug<Target>("Closed target '{0}'.", this);
						}
					}
					catch (Exception ex)
					{
						InternalLogger.Error(ex, "{0}: Error closing target", new object[] { this });
						if (ex.MustBeRethrown())
						{
							throw;
						}
					}
				}
			}
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x00013B10 File Offset: 0x00011D10
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this._isInitialized)
			{
				this._isInitialized = false;
				if (this._initializeException == null)
				{
					this.CloseTarget();
				}
			}
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x00013B36 File Offset: 0x00011D36
		protected virtual void InitializeTarget()
		{
			this.FindAllLayouts();
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x00013B40 File Offset: 0x00011D40
		private void FindAllLayouts()
		{
			this._allLayouts = ObjectGraphScanner.FindReachableObjects<Layout>(false, new object[] { this });
			InternalLogger.Trace<Target, int>("{0} has {1} layouts", this, this._allLayouts.Count);
			this._allLayoutsAreThreadAgnostic = this._allLayouts.All((Layout layout) => layout.ThreadAgnostic);
			bool flag;
			if (this._allLayoutsAreThreadAgnostic)
			{
				flag = this._allLayouts.Any((Layout layout) => layout.MutableUnsafe);
			}
			else
			{
				flag = false;
			}
			this._oneLayoutIsMutableUnsafe = flag;
			if (!this._allLayoutsAreThreadAgnostic || this._oneLayoutIsMutableUnsafe)
			{
				this._allLayoutsAreThreadSafe = this._allLayouts.All((Layout layout) => layout.ThreadSafe);
			}
			this.StackTraceUsage = this._allLayouts.DefaultIfEmpty<Layout>().Max(delegate(Layout layout)
			{
				if (layout == null)
				{
					return StackTraceUsage.None;
				}
				return layout.StackTraceUsage;
			});
			IUsesStackTrace usesStackTrace;
			if ((usesStackTrace = this as IUsesStackTrace) != null && usesStackTrace.StackTraceUsage > this.StackTraceUsage)
			{
				this.StackTraceUsage = usesStackTrace.StackTraceUsage;
			}
			this._scannedForLayouts = true;
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x00013C84 File Offset: 0x00011E84
		protected virtual void CloseTarget()
		{
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x00013C86 File Offset: 0x00011E86
		protected virtual void FlushAsync(AsyncContinuation asyncContinuation)
		{
			asyncContinuation(null);
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x00013C8F File Offset: 0x00011E8F
		protected virtual void Write(LogEventInfo logEvent)
		{
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x00013C94 File Offset: 0x00011E94
		protected virtual void Write(AsyncLogEventInfo logEvent)
		{
			try
			{
				this.Write(logEvent.LogEvent);
				logEvent.Continuation(null);
			}
			catch (Exception ex)
			{
				if (ex.MustBeRethrown())
				{
					throw;
				}
				logEvent.Continuation(ex);
			}
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x00013CE8 File Offset: 0x00011EE8
		protected virtual void WriteAsyncThreadSafe(AsyncLogEventInfo logEvent)
		{
			object syncRoot = this.SyncRoot;
			lock (syncRoot)
			{
				if (!this.IsInitialized)
				{
					logEvent.Continuation(null);
				}
				else
				{
					this.Write(logEvent);
				}
			}
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x00013D40 File Offset: 0x00011F40
		[Obsolete("Instead override Write(IList<AsyncLogEventInfo> logEvents. Marked obsolete on NLog 4.5")]
		protected virtual void Write(AsyncLogEventInfo[] logEvents)
		{
			this.Write(logEvents);
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x00013D4C File Offset: 0x00011F4C
		protected virtual void Write(IList<AsyncLogEventInfo> logEvents)
		{
			for (int i = 0; i < logEvents.Count; i++)
			{
				this.Write(logEvents[i]);
			}
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x00013D77 File Offset: 0x00011F77
		[Obsolete("Instead override WriteAsyncThreadSafe(IList<AsyncLogEventInfo> logEvents. Marked obsolete on NLog 4.5")]
		protected virtual void WriteAsyncThreadSafe(AsyncLogEventInfo[] logEvents)
		{
			this.WriteAsyncThreadSafe(logEvents);
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x00013D80 File Offset: 0x00011F80
		protected virtual void WriteAsyncThreadSafe(IList<AsyncLogEventInfo> logEvents)
		{
			object syncRoot = this.SyncRoot;
			lock (syncRoot)
			{
				if (!this.IsInitialized)
				{
					for (int i = 0; i < logEvents.Count; i++)
					{
						logEvents[i].Continuation(null);
					}
				}
				else
				{
					AsyncLogEventInfo[] array = (this.OptimizeBufferReuse ? null : (logEvents as AsyncLogEventInfo[]));
					if (!this.OptimizeBufferReuse && array != null)
					{
						this.Write(array);
					}
					else
					{
						this.Write(logEvents);
					}
				}
			}
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x00013E18 File Offset: 0x00012018
		private Exception CreateInitException()
		{
			return new NLogRuntimeException(string.Format("Target {0} failed to initialize.", this), this._initializeException);
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x00013E30 File Offset: 0x00012030
		[Obsolete("Logger.Trace(logEvent) now automatically captures the logEvent Properties. Marked obsolete on NLog 4.6")]
		protected void MergeEventProperties(LogEventInfo logEvent)
		{
			if (logEvent.Parameters == null || logEvent.Parameters.Length == 0)
			{
				return;
			}
			for (int i = 0; i < logEvent.Parameters.Length; i++)
			{
				LogEventInfo logEventInfo;
				if ((logEventInfo = logEvent.Parameters[i] as LogEventInfo) != null && logEventInfo.HasProperties)
				{
					foreach (object obj in logEventInfo.Properties.Keys)
					{
						logEvent.Properties.Add(obj, logEventInfo.Properties[obj]);
					}
					logEventInfo.Properties.Clear();
				}
			}
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x00013EDC File Offset: 0x000120DC
		protected string RenderLogEvent(Layout layout, LogEventInfo logEvent)
		{
			if (layout == null || logEvent == null)
			{
				return null;
			}
			if (this.OptimizeBufferReuse)
			{
				SimpleLayout simpleLayout = layout as SimpleLayout;
				if (simpleLayout != null && simpleLayout.IsFixedText)
				{
					return simpleLayout.Render(logEvent);
				}
				string text;
				if (Target.TryGetCachedValue(layout, logEvent, out text))
				{
					return text;
				}
				if (simpleLayout != null && simpleLayout.IsSimpleStringText)
				{
					return simpleLayout.Render(logEvent);
				}
				using (ReusableObjectCreator<StringBuilder>.LockOject lockOject = this.ReusableLayoutBuilder.Allocate())
				{
					return layout.RenderAllocateBuilder(logEvent, lockOject.Result);
				}
			}
			return layout.Render(logEvent);
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x00013F78 File Offset: 0x00012178
		private static bool TryGetCachedValue(Layout layout, LogEventInfo logEvent, out string value)
		{
			object obj;
			if ((!layout.ThreadAgnostic || layout.MutableUnsafe) && logEvent.TryGetCachedLayoutValue(layout, out obj))
			{
				value = ((obj != null) ? obj.ToString() : null) ?? string.Empty;
				return true;
			}
			value = null;
			return false;
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x00013FC0 File Offset: 0x000121C0
		public static void Register<T>(string name) where T : Target
		{
			Type typeFromHandle = typeof(T);
			Target.Register(name, typeFromHandle);
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x00013FDF File Offset: 0x000121DF
		public static void Register(string name, Type targetType)
		{
			ConfigurationItemFactory.Default.Targets.RegisterDefinition(name, targetType);
		}

		// Token: 0x04000181 RID: 385
		private List<Layout> _allLayouts;

		// Token: 0x04000182 RID: 386
		private bool _allLayoutsAreThreadAgnostic;

		// Token: 0x04000183 RID: 387
		private bool _allLayoutsAreThreadSafe;

		// Token: 0x04000184 RID: 388
		private bool _oneLayoutIsMutableUnsafe;

		// Token: 0x04000185 RID: 389
		private bool _scannedForLayouts;

		// Token: 0x04000186 RID: 390
		private Exception _initializeException;

		// Token: 0x0400018C RID: 396
		private volatile bool _isInitialized;

		// Token: 0x0400018D RID: 397
		internal readonly ReusableBuilderCreator ReusableLayoutBuilder = new ReusableBuilderCreator();

		// Token: 0x0400018E RID: 398
		private StringBuilderPool _precalculateStringBuilderPool;
	}
}
