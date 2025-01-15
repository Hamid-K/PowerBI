using System;

namespace Microsoft.Apache.Thrift
{
	// Token: 0x02001FF4 RID: 8180
	internal struct TMap
	{
		// Token: 0x17002D19 RID: 11545
		// (get) Token: 0x06011130 RID: 69936 RVA: 0x003AE101 File Offset: 0x003AC301
		// (set) Token: 0x06011131 RID: 69937 RVA: 0x003AE109 File Offset: 0x003AC309
		public TType KeyType { get; set; }

		// Token: 0x17002D1A RID: 11546
		// (get) Token: 0x06011132 RID: 69938 RVA: 0x003AE112 File Offset: 0x003AC312
		// (set) Token: 0x06011133 RID: 69939 RVA: 0x003AE11A File Offset: 0x003AC31A
		public TType ValueType { get; set; }

		// Token: 0x17002D1B RID: 11547
		// (get) Token: 0x06011134 RID: 69940 RVA: 0x003AE123 File Offset: 0x003AC323
		// (set) Token: 0x06011135 RID: 69941 RVA: 0x003AE12B File Offset: 0x003AC32B
		public int Count { get; set; }

		// Token: 0x06011136 RID: 69942 RVA: 0x003AE134 File Offset: 0x003AC334
		public TMap(TType keyType, TType valueType, int count)
		{
			this.KeyType = keyType;
			this.ValueType = valueType;
			this.Count = count;
		}
	}
}
