using System;

namespace Model
{
	// Token: 0x0200000D RID: 13
	public class DataRetrievalPlan
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000036 RID: 54 RVA: 0x0000221C File Offset: 0x0000041C
		// (set) Token: 0x06000037 RID: 55 RVA: 0x00002224 File Offset: 0x00000424
		public DataSource DataSource { get; set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000038 RID: 56 RVA: 0x0000222D File Offset: 0x0000042D
		// (set) Token: 0x06000039 RID: 57 RVA: 0x00002235 File Offset: 0x00000435
		public Query Query { get; set; }
	}
}
