using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x02000206 RID: 518
	public sealed class Picture : IDisposable
	{
		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000B80 RID: 2944 RVA: 0x0003EA6C File Offset: 0x0003CC6C
		internal Picture.Impl Contents
		{
			get
			{
				return this._impl;
			}
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x0003EA74 File Offset: 0x0003CC74
		internal Picture(string path)
		{
			this._impl = new Picture.Impl(path);
			this._impl.AddRef();
		}

		// Token: 0x06000B82 RID: 2946 RVA: 0x0003EA94 File Offset: 0x0003CC94
		internal Picture(Picture.Impl impl)
		{
			this._impl = impl;
			this._impl.AddRef();
		}

		// Token: 0x06000B83 RID: 2947 RVA: 0x0003EAAF File Offset: 0x0003CCAF
		internal void DisposeAndSurrender(out Picture.Impl impl)
		{
			if (this._impl == null || this._impl.Release() > 0)
			{
				impl = null;
			}
			else
			{
				impl = this._impl;
			}
			this._impl = null;
		}

		// Token: 0x06000B84 RID: 2948 RVA: 0x0003EADB File Offset: 0x0003CCDB
		public void Dispose()
		{
			if (this._impl != null)
			{
				if (this._impl.Release() <= 0)
				{
					this._impl.Free();
				}
				this._impl = null;
			}
		}

		// Token: 0x04000649 RID: 1609
		private Picture.Impl _impl;

		// Token: 0x02000207 RID: 519
		internal class Impl
		{
			// Token: 0x17000158 RID: 344
			// (get) Token: 0x06000B85 RID: 2949 RVA: 0x0003EB05 File Offset: 0x0003CD05
			public Bitmap Pixels
			{
				get
				{
					return this._pixels;
				}
			}

			// Token: 0x17000159 RID: 345
			// (get) Token: 0x06000B86 RID: 2950 RVA: 0x0003EB0D File Offset: 0x0003CD0D
			public Graphics Context
			{
				get
				{
					return this._context;
				}
			}

			// Token: 0x06000B87 RID: 2951 RVA: 0x0003EB15 File Offset: 0x0003CD15
			public Impl(string path)
			{
				this._pixels = new Bitmap(path);
				this._context = null;
			}

			// Token: 0x06000B88 RID: 2952 RVA: 0x0003EB30 File Offset: 0x0003CD30
			public Impl(int width, int height, PixelFormat fmt)
			{
				this._pixels = new Bitmap(width, height, fmt);
				this._context = Graphics.FromImage(this.Pixels);
			}

			// Token: 0x06000B89 RID: 2953 RVA: 0x0003EB58 File Offset: 0x0003CD58
			public int AddRef()
			{
				return Interlocked.Increment(ref this._refCount);
			}

			// Token: 0x06000B8A RID: 2954 RVA: 0x0003EB74 File Offset: 0x0003CD74
			public int Release()
			{
				return Interlocked.Decrement(ref this._refCount);
			}

			// Token: 0x06000B8B RID: 2955 RVA: 0x0003EB8E File Offset: 0x0003CD8E
			public void Free()
			{
				if (this._context != null)
				{
					this._context.Dispose();
					this._context = null;
				}
				if (this._pixels != null)
				{
					this._pixels.Dispose();
					this._pixels = null;
				}
			}

			// Token: 0x0400064A RID: 1610
			private int _refCount;

			// Token: 0x0400064B RID: 1611
			private Bitmap _pixels;

			// Token: 0x0400064C RID: 1612
			private Graphics _context;
		}
	}
}
