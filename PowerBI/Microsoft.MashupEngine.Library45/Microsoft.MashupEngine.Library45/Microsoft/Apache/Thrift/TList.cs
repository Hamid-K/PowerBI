using System;

namespace Microsoft.Apache.Thrift
{
	// Token: 0x02001FF6 RID: 8182
	internal struct TList
	{
		// Token: 0x17002D1E RID: 11550
		// (get) Token: 0x0601113C RID: 69948 RVA: 0x003AE17D File Offset: 0x003AC37D
		// (set) Token: 0x0601113D RID: 69949 RVA: 0x003AE185 File Offset: 0x003AC385
		public TType ElementType { get; set; }

		// Token: 0x17002D1F RID: 11551
		// (get) Token: 0x0601113E RID: 69950 RVA: 0x003AE18E File Offset: 0x003AC38E
		// (set) Token: 0x0601113F RID: 69951 RVA: 0x003AE196 File Offset: 0x003AC396
		public int Count { get; set; }

		// Token: 0x06011140 RID: 69952 RVA: 0x003AE19F File Offset: 0x003AC39F
		public TList(TType elementType, int count)
		{
			this.ElementType = elementType;
			this.Count = count;
		}
	}
}
