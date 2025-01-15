using System;

namespace Microsoft.Apache.Thrift
{
	// Token: 0x02001FF2 RID: 8178
	internal struct TField
	{
		// Token: 0x17002D16 RID: 11542
		// (get) Token: 0x06011129 RID: 69929 RVA: 0x003AE0B7 File Offset: 0x003AC2B7
		// (set) Token: 0x0601112A RID: 69930 RVA: 0x003AE0BF File Offset: 0x003AC2BF
		public string Name { get; set; }

		// Token: 0x17002D17 RID: 11543
		// (get) Token: 0x0601112B RID: 69931 RVA: 0x003AE0C8 File Offset: 0x003AC2C8
		// (set) Token: 0x0601112C RID: 69932 RVA: 0x003AE0D0 File Offset: 0x003AC2D0
		public TType Type { get; set; }

		// Token: 0x17002D18 RID: 11544
		// (get) Token: 0x0601112D RID: 69933 RVA: 0x003AE0D9 File Offset: 0x003AC2D9
		// (set) Token: 0x0601112E RID: 69934 RVA: 0x003AE0E1 File Offset: 0x003AC2E1
		public short ID { get; set; }

		// Token: 0x0601112F RID: 69935 RVA: 0x003AE0EA File Offset: 0x003AC2EA
		public TField(string name, TType type, short id)
		{
			this.Name = name;
			this.Type = type;
			this.ID = id;
		}
	}
}
