using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace msclr
{
	// Token: 0x02000015 RID: 21
	internal class @lock : IDisposable
	{
		// Token: 0x06000156 RID: 342 RVA: 0x000025C8 File Offset: 0x000019C8
		public @lock(object _object)
		{
			this.acquire(-1);
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00002A74 File Offset: 0x00001E74
		private void ~lock()
		{
			this.release();
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00001E20 File Offset: 0x00001220
		[return: MarshalAs(UnmanagedType.U1)]
		public bool is_locked()
		{
			return this.m_locked;
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00001E3C File Offset: 0x0000123C
		public implicit operator string()
		{
			return (!this.m_locked) ? _detail_class._safe_false : _detail_class._safe_true;
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00001EE4 File Offset: 0x000012E4
		public void acquire(TimeSpan _timeout)
		{
			if (!this.m_locked)
			{
				Monitor.TryEnter(this.m_object, _timeout, ref this.m_locked);
				if (!this.m_locked)
				{
					throw Marshal.GetExceptionForHR(-2147024638);
				}
			}
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00001EA4 File Offset: 0x000012A4
		public void acquire()
		{
			if (!this.m_locked)
			{
				Monitor.TryEnter(this.m_object, -1, ref this.m_locked);
				if (!this.m_locked)
				{
					throw Marshal.GetExceptionForHR(-2147024638);
				}
			}
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00001E64 File Offset: 0x00001264
		public void acquire(int _timeout)
		{
			if (!this.m_locked)
			{
				Monitor.TryEnter(this.m_object, _timeout, ref this.m_locked);
				if (!this.m_locked)
				{
					throw Marshal.GetExceptionForHR(-2147024638);
				}
			}
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00001F5C File Offset: 0x0000135C
		[return: MarshalAs(UnmanagedType.U1)]
		public bool try_acquire(TimeSpan _timeout)
		{
			if (!this.m_locked)
			{
				Monitor.TryEnter(this.m_object, _timeout, ref this.m_locked);
				if (!this.m_locked)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00001F24 File Offset: 0x00001324
		[return: MarshalAs(UnmanagedType.U1)]
		public bool try_acquire(int _timeout)
		{
			if (!this.m_locked)
			{
				Monitor.TryEnter(this.m_object, _timeout, ref this.m_locked);
				if (!this.m_locked)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00001F94 File Offset: 0x00001394
		public void release()
		{
			if (this.m_locked)
			{
				Monitor.Exit(this.m_object);
				this.m_locked = false;
			}
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00002A90 File Offset: 0x00001E90
		protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool A_0)
		{
			if (A_0)
			{
				this.release();
			}
			else
			{
				base.Finalize();
			}
		}

		// Token: 0x06000161 RID: 353 RVA: 0x000030D8 File Offset: 0x000024D8
		public sealed void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x04000106 RID: 262
		private object m_object = _object;

		// Token: 0x04000107 RID: 263
		private bool m_locked = false;
	}
}
