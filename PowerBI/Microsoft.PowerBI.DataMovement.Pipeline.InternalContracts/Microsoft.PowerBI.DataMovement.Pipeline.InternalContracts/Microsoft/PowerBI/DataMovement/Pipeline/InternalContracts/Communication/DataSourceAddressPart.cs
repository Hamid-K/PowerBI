using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.Pipeline.InternalContracts.Communication
{
	// Token: 0x02000023 RID: 35
	[NullableContext(1)]
	[Nullable(0)]
	[DataContract]
	public sealed class DataSourceAddressPart
	{
		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000085 RID: 133 RVA: 0x000024BD File Offset: 0x000006BD
		// (set) Token: 0x06000086 RID: 134 RVA: 0x000024C5 File Offset: 0x000006C5
		[DataMember(Name = "label", IsRequired = false)]
		public string Label { get; set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000087 RID: 135 RVA: 0x000024CE File Offset: 0x000006CE
		// (set) Token: 0x06000088 RID: 136 RVA: 0x000024D6 File Offset: 0x000006D6
		[DataMember(Name = "name", IsRequired = false)]
		public string Name { get; set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000089 RID: 137 RVA: 0x000024DF File Offset: 0x000006DF
		// (set) Token: 0x0600008A RID: 138 RVA: 0x000024E7 File Offset: 0x000006E7
		[DataMember(Name = "value", IsRequired = false)]
		public string Value { get; set; }
	}
}
