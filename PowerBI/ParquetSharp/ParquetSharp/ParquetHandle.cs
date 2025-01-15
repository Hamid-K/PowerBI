using System;
using System.Runtime.CompilerServices;

namespace ParquetSharp
{
	// Token: 0x0200007E RID: 126
	internal sealed class ParquetHandle : IDisposable
	{
		// Token: 0x06000365 RID: 869 RVA: 0x0000DC74 File Offset: 0x0000BE74
		[NullableContext(1)]
		public ParquetHandle(IntPtr handle, Action<IntPtr> free)
		{
			this._handle = handle;
			this._free = free;
		}

		// Token: 0x06000366 RID: 870 RVA: 0x0000DC8C File Offset: 0x0000BE8C
		public void Dispose()
		{
			if (this._handle != IntPtr.Zero)
			{
				this._free(this._handle);
				this._handle = IntPtr.Zero;
			}
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000367 RID: 871 RVA: 0x0000DCC8 File Offset: 0x0000BEC8
		~ParquetHandle()
		{
			if (this._handle != IntPtr.Zero)
			{
				this._free(this._handle);
				this._handle = IntPtr.Zero;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000368 RID: 872 RVA: 0x0000DD24 File Offset: 0x0000BF24
		public IntPtr IntPtr
		{
			get
			{
				if (this._handle == IntPtr.Zero)
				{
					throw new NullReferenceException("null native handle");
				}
				return this._handle;
			}
		}

		// Token: 0x040000F4 RID: 244
		private IntPtr _handle;

		// Token: 0x040000F5 RID: 245
		[Nullable(1)]
		private readonly Action<IntPtr> _free;
	}
}
