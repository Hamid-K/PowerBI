using System;
using System.Collections.Generic;
using System.Threading;
using JetBrains.Annotations;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000201 RID: 513
	public class ExpirationDetector : IShuttable
	{
		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06000D94 RID: 3476 RVA: 0x0002F919 File Offset: 0x0002DB19
		internal int ObjectsToBeWatchedCounter
		{
			get
			{
				return this.m_objectsToBeWatched.Count;
			}
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x06000D95 RID: 3477 RVA: 0x0002F926 File Offset: 0x0002DB26
		// (set) Token: 0x06000D96 RID: 3478 RVA: 0x0002F92E File Offset: 0x0002DB2E
		internal int Period
		{
			get
			{
				return this.m_period;
			}
			set
			{
				this.m_period = value;
			}
		}

		// Token: 0x06000D97 RID: 3479 RVA: 0x0002F938 File Offset: 0x0002DB38
		public ExpirationDetector([NotNull] string identifier, TimeSpan ttl, [NotNull] ExpirationDetector.OnObjectExpired callback)
		{
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(identifier, "identifier");
			ExtendedDiagnostics.EnsureArgumentIsPositive((long)ttl.TotalMilliseconds, "ttl.TotalMilliseconds");
			ExtendedDiagnostics.EnsureArgumentNotNull<ExpirationDetector.OnObjectExpired>(callback, "callback");
			this.m_identifier = identifier;
			this.m_timerFactory = new TimerFactory(identifier + ".ExpirationDetector", TimerCreationFlags.Crash);
			TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Info, "Detector '{0}' start to detect now", new object[] { this.m_identifier });
			this.m_objectsToBeWatched = new LinkedList<ExpirationDetector.WatchedObject>();
			this.m_onObjectExpired = callback;
			this.m_ttl = ttl;
			this.m_active = true;
			this.m_period = (int)(this.m_ttl.TotalMilliseconds * 0.2);
			this.m_period = ((this.m_period < 1000) ? 1000 : this.m_period);
			this.m_locker = new object();
		}

		// Token: 0x06000D98 RID: 3480 RVA: 0x0002FA18 File Offset: 0x0002DC18
		public void StartWatching(string objIdentifier, object obj)
		{
			object locker = this.m_locker;
			lock (locker)
			{
				if (!this.m_active)
				{
					TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Warning, "Detector '{0}' has been stopped.", new object[] { this.m_identifier });
					throw new ShutdownSequenceStartedException("Detector " + this.m_identifier + " has been stopped.");
				}
				DateTime dateTime = DateTime.UtcNow.Add(this.m_ttl);
				ExpirationDetector.WatchedObject watchedObject = new ExpirationDetector.WatchedObject(dateTime, obj, objIdentifier);
				this.m_objectsToBeWatched.AddLast(watchedObject);
				if (this.m_periodicTimer == null)
				{
					this.m_periodicTimer = this.m_timerFactory.SchedulePeriodicTimer(this.m_identifier + ".TimerFactory", this.m_period, new TimerCallback(this.OnPeriodicOperation), null);
				}
			}
		}

		// Token: 0x06000D99 RID: 3481 RVA: 0x0002FAFC File Offset: 0x0002DCFC
		public void StopWatching(object obj)
		{
			bool flag = false;
			object locker = this.m_locker;
			lock (locker)
			{
				if (!this.m_active)
				{
					TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Warning, "Detector '{0}' has been stopped.", new object[] { this.m_identifier });
					throw new ShutdownSequenceStartedException("Detector " + this.m_identifier + " has been stopped.");
				}
				foreach (ExpirationDetector.WatchedObject watchedObject in this.m_objectsToBeWatched)
				{
					if (watchedObject.m_key.Equals(obj))
					{
						this.m_objectsToBeWatched.Remove(watchedObject);
						flag = true;
						break;
					}
				}
			}
			if (!flag)
			{
				TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Warning, "StopWatching failed since given object does not exist in the list.");
			}
		}

		// Token: 0x06000D9A RID: 3482 RVA: 0x0002FBE8 File Offset: 0x0002DDE8
		private void OnPeriodicOperation(object target)
		{
			List<ExpirationDetector.WatchedObject> list = new List<ExpirationDetector.WatchedObject>();
			DateTime utcNow = DateTime.UtcNow;
			object locker = this.m_locker;
			lock (locker)
			{
				if (!this.m_active)
				{
					return;
				}
				int num = this.m_objectsToBeWatched.Count;
				while (num > 0 && this.m_objectsToBeWatched.First.Value.m_expirationTime <= utcNow)
				{
					list.Add(this.m_objectsToBeWatched.First.Value);
					this.m_objectsToBeWatched.RemoveFirst();
					num--;
				}
			}
			foreach (ExpirationDetector.WatchedObject watchedObject in list)
			{
				TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Warning, "Detector '{0}' has detected an expired object {1}", new object[] { this.m_identifier, watchedObject.m_identifier });
				this.m_onObjectExpired(watchedObject.m_identifier, watchedObject.m_key);
			}
		}

		// Token: 0x06000D9B RID: 3483 RVA: 0x0002FD10 File Offset: 0x0002DF10
		public void Stop()
		{
			object locker = this.m_locker;
			lock (locker)
			{
				this.m_active = false;
			}
			this.m_timerFactory.Stop();
		}

		// Token: 0x06000D9C RID: 3484 RVA: 0x0002FD5C File Offset: 0x0002DF5C
		public void WaitForStopToComplete()
		{
			this.m_timerFactory.WaitForStopToComplete();
		}

		// Token: 0x06000D9D RID: 3485 RVA: 0x0002FD69 File Offset: 0x0002DF69
		public void Shutdown()
		{
			this.m_timerFactory.Shutdown();
		}

		// Token: 0x04000559 RID: 1369
		private string m_identifier;

		// Token: 0x0400055A RID: 1370
		private TimerFactory m_timerFactory;

		// Token: 0x0400055B RID: 1371
		private PeriodicTimer m_periodicTimer;

		// Token: 0x0400055C RID: 1372
		private LinkedList<ExpirationDetector.WatchedObject> m_objectsToBeWatched;

		// Token: 0x0400055D RID: 1373
		private ExpirationDetector.OnObjectExpired m_onObjectExpired;

		// Token: 0x0400055E RID: 1374
		private TimeSpan m_ttl;

		// Token: 0x0400055F RID: 1375
		private int m_period;

		// Token: 0x04000560 RID: 1376
		private bool m_active;

		// Token: 0x04000561 RID: 1377
		private object m_locker;

		// Token: 0x020006A2 RID: 1698
		private struct WatchedObject
		{
			// Token: 0x06002E0C RID: 11788 RVA: 0x000A1A9B File Offset: 0x0009FC9B
			public WatchedObject(DateTime expirationTime, object key, string identifier)
			{
				this.m_expirationTime = expirationTime;
				this.m_key = key;
				this.m_identifier = identifier;
			}

			// Token: 0x040012CD RID: 4813
			public DateTime m_expirationTime;

			// Token: 0x040012CE RID: 4814
			public object m_key;

			// Token: 0x040012CF RID: 4815
			public string m_identifier;
		}

		// Token: 0x020006A3 RID: 1699
		// (Invoke) Token: 0x06002E0E RID: 11790
		public delegate void OnObjectExpired(string identifier, object obj);
	}
}
