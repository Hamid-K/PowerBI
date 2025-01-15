using System;
using System.Runtime.Serialization;

namespace Microsoft.DataShaping.Processing
{
	// Token: 0x02000009 RID: 9
	[DataContract]
	internal class DictionaryEncodingInfo
	{
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00002553 File Offset: 0x00000753
		// (set) Token: 0x0600002A RID: 42 RVA: 0x0000255B File Offset: 0x0000075B
		[DataMember(Name = "Count", EmitDefaultValue = false, Order = 10)]
		public int ValueCount { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600002B RID: 43 RVA: 0x00002564 File Offset: 0x00000764
		// (set) Token: 0x0600002C RID: 44 RVA: 0x0000256C File Offset: 0x0000076C
		[DataMember(Name = "Hits", EmitDefaultValue = false, Order = 20)]
		public int HitCount { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600002D RID: 45 RVA: 0x00002575 File Offset: 0x00000775
		// (set) Token: 0x0600002E RID: 46 RVA: 0x0000257D File Offset: 0x0000077D
		[DataMember(Name = "Miss", EmitDefaultValue = false, Order = 30)]
		public int MissCount { get; set; }
	}
}
