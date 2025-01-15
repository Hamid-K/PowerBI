using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x020000B0 RID: 176
	internal class TupleInternal<T1, T2>
	{
		// Token: 0x060003F2 RID: 1010 RVA: 0x0000A7C7 File Offset: 0x000089C7
		public TupleInternal(T1 item1, T2 item2)
		{
			this.Item1 = item1;
			this.Item2 = item2;
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x060003F3 RID: 1011 RVA: 0x0000A7DD File Offset: 0x000089DD
		// (set) Token: 0x060003F4 RID: 1012 RVA: 0x0000A7E5 File Offset: 0x000089E5
		public T1 Item1 { get; private set; }

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x060003F5 RID: 1013 RVA: 0x0000A7EE File Offset: 0x000089EE
		// (set) Token: 0x060003F6 RID: 1014 RVA: 0x0000A7F6 File Offset: 0x000089F6
		public T2 Item2 { get; private set; }
	}
}
