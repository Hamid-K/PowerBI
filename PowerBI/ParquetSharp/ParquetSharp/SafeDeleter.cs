using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ParquetSharp
{
	// Token: 0x02000087 RID: 135
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class SafeDeleter
	{
		// Token: 0x060003AA RID: 938 RVA: 0x0000E1F4 File Offset: 0x0000C3F4
		public SafeDeleter(SafeDeleter.DeleteDelegate delete)
		{
			this._unsafeDelete = delete;
			this.Delete = new SafeDeleter.DeleteDelegate(this.SafeDelete);
		}

		// Token: 0x060003AB RID: 939 RVA: 0x0000E218 File Offset: 0x0000C418
		public void GiveOwnership()
		{
			if (this._thisHandle != null)
			{
				throw new InvalidOperationException();
			}
			this._thisHandle = new GCHandle?(GCHandle.Alloc(this));
		}

		// Token: 0x060003AC RID: 940 RVA: 0x0000E244 File Offset: 0x0000C444
		[NullableContext(2)]
		private byte SafeDelete(out string exception)
		{
			byte b;
			try
			{
				b = this._unsafeDelete(out exception);
			}
			finally
			{
				if (this._thisHandle != null)
				{
					this._thisHandle.GetValueOrDefault().Free();
				}
			}
			return b;
		}

		// Token: 0x04000114 RID: 276
		private readonly SafeDeleter.DeleteDelegate _unsafeDelete;

		// Token: 0x04000115 RID: 277
		private GCHandle? _thisHandle;

		// Token: 0x04000116 RID: 278
		internal readonly SafeDeleter.DeleteDelegate Delete;

		// Token: 0x02000123 RID: 291
		// (Invoke) Token: 0x060009A2 RID: 2466
		[NullableContext(0)]
		internal delegate byte DeleteDelegate([MarshalAs(UnmanagedType.LPStr)] out string exception);
	}
}
