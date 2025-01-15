using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.Pipeline.InternalContracts.Communication
{
	// Token: 0x02000024 RID: 36
	[NullableContext(1)]
	[Nullable(0)]
	[DataContract]
	public sealed class DataSourceReferenceMetadata
	{
		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600008C RID: 140 RVA: 0x000024F8 File Offset: 0x000006F8
		// (set) Token: 0x0600008D RID: 141 RVA: 0x00002500 File Offset: 0x00000700
		[DataMember(Name = "dataSourceKind", IsRequired = false)]
		public string DataSourceKind { get; set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600008E RID: 142 RVA: 0x00002509 File Offset: 0x00000709
		// (set) Token: 0x0600008F RID: 143 RVA: 0x00002511 File Offset: 0x00000711
		[DataMember(Name = "dataSourcePath", IsRequired = false)]
		public string DataSourcePath { get; set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000090 RID: 144 RVA: 0x0000251A File Offset: 0x0000071A
		// (set) Token: 0x06000091 RID: 145 RVA: 0x00002522 File Offset: 0x00000722
		[DataMember(Name = "query", IsRequired = false)]
		public string Query { get; set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000092 RID: 146 RVA: 0x0000252B File Offset: 0x0000072B
		// (set) Token: 0x06000093 RID: 147 RVA: 0x00002533 File Offset: 0x00000733
		[DataMember(Name = "dataSourceAddress", IsRequired = false)]
		public IList<DataSourceAddressPart> DataSourceAddress { get; set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000094 RID: 148 RVA: 0x0000253C File Offset: 0x0000073C
		// (set) Token: 0x06000095 RID: 149 RVA: 0x00002544 File Offset: 0x00000744
		[DataMember(Name = "dsrJson", IsRequired = false)]
		public string DsrJson { get; set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000096 RID: 150 RVA: 0x0000254D File Offset: 0x0000074D
		// (set) Token: 0x06000097 RID: 151 RVA: 0x00002555 File Offset: 0x00000755
		[DataMember(Name = "analyzableDoc", IsRequired = false)]
		public string AnalyzableDoc { get; set; }
	}
}
