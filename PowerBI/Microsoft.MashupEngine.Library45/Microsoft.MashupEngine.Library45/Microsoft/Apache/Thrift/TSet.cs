using System;

namespace Microsoft.Apache.Thrift
{
	// Token: 0x02001FF5 RID: 8181
	internal struct TSet
	{
		// Token: 0x17002D1C RID: 11548
		// (get) Token: 0x06011137 RID: 69943 RVA: 0x003AE14B File Offset: 0x003AC34B
		// (set) Token: 0x06011138 RID: 69944 RVA: 0x003AE153 File Offset: 0x003AC353
		public TType ElementType { get; set; }

		// Token: 0x17002D1D RID: 11549
		// (get) Token: 0x06011139 RID: 69945 RVA: 0x003AE15C File Offset: 0x003AC35C
		// (set) Token: 0x0601113A RID: 69946 RVA: 0x003AE164 File Offset: 0x003AC364
		public int Count { get; set; }

		// Token: 0x0601113B RID: 69947 RVA: 0x003AE16D File Offset: 0x003AC36D
		public TSet(TType elementType, int count)
		{
			this.ElementType = elementType;
			this.Count = count;
		}
	}
}
