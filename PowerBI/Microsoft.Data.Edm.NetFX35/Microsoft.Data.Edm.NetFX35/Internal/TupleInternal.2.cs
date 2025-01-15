using System;

namespace Microsoft.Data.Edm.Internal
{
	// Token: 0x020001C9 RID: 457
	internal class TupleInternal<T1, T2>
	{
		// Token: 0x06000AC4 RID: 2756 RVA: 0x0001FD67 File Offset: 0x0001DF67
		public TupleInternal(T1 item1, T2 item2)
		{
			this.Item1 = item1;
			this.Item2 = item2;
		}

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x06000AC5 RID: 2757 RVA: 0x0001FD7D File Offset: 0x0001DF7D
		// (set) Token: 0x06000AC6 RID: 2758 RVA: 0x0001FD85 File Offset: 0x0001DF85
		public T1 Item1 { get; private set; }

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x06000AC7 RID: 2759 RVA: 0x0001FD8E File Offset: 0x0001DF8E
		// (set) Token: 0x06000AC8 RID: 2760 RVA: 0x0001FD96 File Offset: 0x0001DF96
		public T2 Item2 { get; private set; }
	}
}
