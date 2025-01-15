using System;

namespace Microsoft.OleDb
{
	// Token: 0x02001F22 RID: 7970
	internal class RefCount<T>
	{
		// Token: 0x0600C339 RID: 49977 RVA: 0x00271CF9 File Offset: 0x0026FEF9
		public RefCount(T value)
			: this(value, 1)
		{
		}

		// Token: 0x0600C33A RID: 49978 RVA: 0x00271D03 File Offset: 0x0026FF03
		public RefCount(T value, int refCount)
		{
			this.refCount = refCount;
			this.value = value;
		}

		// Token: 0x0600C33B RID: 49979 RVA: 0x00271D1C File Offset: 0x0026FF1C
		public int AddRef()
		{
			int num = this.refCount + 1;
			this.refCount = num;
			return num;
		}

		// Token: 0x0600C33C RID: 49980 RVA: 0x00271D3C File Offset: 0x0026FF3C
		public int Release()
		{
			int num = this.refCount - 1;
			this.refCount = num;
			return num;
		}

		// Token: 0x17002FAD RID: 12205
		// (get) Token: 0x0600C33D RID: 49981 RVA: 0x00271D5A File Offset: 0x0026FF5A
		public T Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x04006474 RID: 25716
		private readonly T value;

		// Token: 0x04006475 RID: 25717
		private int refCount;
	}
}
