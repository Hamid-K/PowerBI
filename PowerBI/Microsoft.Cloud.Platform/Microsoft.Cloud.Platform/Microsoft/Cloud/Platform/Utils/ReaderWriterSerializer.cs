using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000277 RID: 631
	public sealed class ReaderWriterSerializer<T> : IReaderWriterSerializer<T>, IIdentifiable, IShuttable where T : IEquatable<T>
	{
		// Token: 0x060010C5 RID: 4293 RVA: 0x00039F86 File Offset: 0x00038186
		public ReaderWriterSerializer(string name)
		{
			this.m_readerWriterLocks = new Dictionary<T, AsyncReaderWriterLock>();
			this.Name = name;
			this.m_lock = new object();
			this.m_active = true;
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x060010C6 RID: 4294 RVA: 0x00039FB2 File Offset: 0x000381B2
		// (set) Token: 0x060010C7 RID: 4295 RVA: 0x00039FBA File Offset: 0x000381BA
		public string Name { get; private set; }

		// Token: 0x060010C8 RID: 4296 RVA: 0x00039FC4 File Offset: 0x000381C4
		public void Stop()
		{
			object @lock = this.m_lock;
			List<AsyncReaderWriterLock> list;
			lock (@lock)
			{
				Ensure.IsTrue(this.m_active, "ReaderWriterSerializer.Stop has been invoked more than once");
				this.m_active = false;
				list = this.m_readerWriterLocks.Values.ToList<AsyncReaderWriterLock>();
			}
			list.ForEach(delegate(AsyncReaderWriterLock rw)
			{
				rw.Stop();
			});
		}

		// Token: 0x060010C9 RID: 4297 RVA: 0x0003A04C File Offset: 0x0003824C
		public void WaitForStopToComplete()
		{
			object @lock = this.m_lock;
			List<AsyncReaderWriterLock> list;
			lock (@lock)
			{
				Ensure.IsTrue(!this.m_active, "ReaderWriterSerializer.WaitForStopToComplete has been called without Stop being called beforehand");
				list = this.m_readerWriterLocks.Values.ToList<AsyncReaderWriterLock>();
			}
			list.ForEach(delegate(AsyncReaderWriterLock rw)
			{
				rw.WaitForStopToComplete();
			});
		}

		// Token: 0x060010CA RID: 4298 RVA: 0x0003A0D0 File Offset: 0x000382D0
		public void Shutdown()
		{
			object @lock = this.m_lock;
			List<AsyncReaderWriterLock> list;
			lock (@lock)
			{
				Ensure.IsTrue(!this.m_active, "ReaderWriterSerializer.Shutdown has been called without Stop/WaitForStopToComplete being called beforehand");
				list = this.m_readerWriterLocks.Values.ToList<AsyncReaderWriterLock>();
			}
			list.ForEach(delegate(AsyncReaderWriterLock rw)
			{
				rw.Shutdown();
			});
		}

		// Token: 0x060010CB RID: 4299 RVA: 0x0003A154 File Offset: 0x00038354
		public IAsyncResult BeginAcquireReaderLock(T key, AsyncCallback callback, object state)
		{
			return this.GetReaderWriterLock(key).AcquireReaderLockAsync(key.ToString()).ToApm(callback, state);
		}

		// Token: 0x060010CC RID: 4300 RVA: 0x0003A176 File Offset: 0x00038376
		public IDisposable EndAcquireReaderLock(IAsyncResult ar)
		{
			return ((Task<AcquiredReaderLock>)ar).ExtendedResult<AcquiredReaderLock>();
		}

		// Token: 0x060010CD RID: 4301 RVA: 0x0003A183 File Offset: 0x00038383
		public IAsyncResult BeginAcquireWriterLock(T key, AsyncCallback callback, object state)
		{
			return this.GetReaderWriterLock(key).AcquireWriterLockAsync(key.ToString()).ToApm(callback, state);
		}

		// Token: 0x060010CE RID: 4302 RVA: 0x0003A1A5 File Offset: 0x000383A5
		public IDisposable EndAcquireWriterLock(IAsyncResult ar)
		{
			return ((Task<AcquiredWriterLock>)ar).ExtendedResult<AcquiredWriterLock>();
		}

		// Token: 0x060010CF RID: 4303 RVA: 0x0003A1B2 File Offset: 0x000383B2
		public bool TryAcquireWriterLock(T key, out IDisposable lockHandle)
		{
			return this.GetReaderWriterLock(key).TryAcquireWriterLock(key.ToString(), out lockHandle);
		}

		// Token: 0x060010D0 RID: 4304 RVA: 0x0003A1CE File Offset: 0x000383CE
		public bool TryAcquireReaderLock(T key, out IDisposable lockHandle)
		{
			return this.GetReaderWriterLock(key).TryAcquireReaderLock(key.ToString(), out lockHandle);
		}

		// Token: 0x060010D1 RID: 4305 RVA: 0x0003A1EC File Offset: 0x000383EC
		public IDisposable DowngradeToReaderLock(T key, [NotNull] IDisposable lockHandle)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<IDisposable>(lockHandle, "lockHandle");
			AcquiredWriterLock acquiredWriterLock = (AcquiredWriterLock)lockHandle;
			return this.GetReaderWriterLock(key).DowngradeToReaderLock(acquiredWriterLock);
		}

		// Token: 0x060010D2 RID: 4306 RVA: 0x0003A218 File Offset: 0x00038418
		private AsyncReaderWriterLock GetReaderWriterLock(T key)
		{
			AsyncReaderWriterLock asyncReaderWriterLock = null;
			object @lock = this.m_lock;
			lock (@lock)
			{
				if (!this.m_readerWriterLocks.TryGetValue(key, out asyncReaderWriterLock))
				{
					asyncReaderWriterLock = new AsyncReaderWriterLock(key.ToString());
					this.m_readerWriterLocks.Add(key, asyncReaderWriterLock);
				}
			}
			return asyncReaderWriterLock;
		}

		// Token: 0x04000633 RID: 1587
		private Dictionary<T, AsyncReaderWriterLock> m_readerWriterLocks;

		// Token: 0x04000634 RID: 1588
		private object m_lock;

		// Token: 0x04000635 RID: 1589
		private bool m_active;
	}
}
