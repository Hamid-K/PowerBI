using System;
using System.Runtime.CompilerServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x020000BD RID: 189
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal class RefCount<[global::System.Runtime.CompilerServices.Nullable(2)] T>
	{
		// Token: 0x0600032D RID: 813 RVA: 0x00009789 File Offset: 0x00007989
		internal RefCount(T value)
			: this(value, 1)
		{
		}

		// Token: 0x0600032E RID: 814 RVA: 0x00009793 File Offset: 0x00007993
		internal RefCount(T value, int refCount)
		{
			this.refCount = refCount;
			this.value = value;
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x0600032F RID: 815 RVA: 0x000097A9 File Offset: 0x000079A9
		public T Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x06000330 RID: 816 RVA: 0x000097B4 File Offset: 0x000079B4
		public int AddRef()
		{
			int num = this.refCount + 1;
			this.refCount = num;
			return num;
		}

		// Token: 0x06000331 RID: 817 RVA: 0x000097D4 File Offset: 0x000079D4
		public int Release()
		{
			int num = this.refCount - 1;
			this.refCount = num;
			return num;
		}

		// Token: 0x04000374 RID: 884
		private readonly T value;

		// Token: 0x04000375 RID: 885
		private int refCount;
	}
}
