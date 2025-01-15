using System;
using System.Runtime.Serialization;

namespace Microsoft.Lucia.Core
{
	// Token: 0x020000ED RID: 237
	[DataContract(Name = "SynonymMatch", Namespace = "http://schemas.microsoft.com/sqlbi/2014/10/LinguisticDataProviderService")]
	public sealed class SynonymMatch
	{
		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000495 RID: 1173 RVA: 0x0000882D File Offset: 0x00006A2D
		// (set) Token: 0x06000496 RID: 1174 RVA: 0x00008835 File Offset: 0x00006A35
		[DataMember(IsRequired = true, Order = 1)]
		public string[] Tokens { get; set; }

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000497 RID: 1175 RVA: 0x0000883E File Offset: 0x00006A3E
		// (set) Token: 0x06000498 RID: 1176 RVA: 0x00008846 File Offset: 0x00006A46
		[DataMember(IsRequired = true, Order = 2)]
		public SynonymProvider Provider { get; set; }

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000499 RID: 1177 RVA: 0x0000884F File Offset: 0x00006A4F
		// (set) Token: 0x0600049A RID: 1178 RVA: 0x00008857 File Offset: 0x00006A57
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 3)]
		public double? Weight { get; set; }

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x0600049B RID: 1179 RVA: 0x00008860 File Offset: 0x00006A60
		// (set) Token: 0x0600049C RID: 1180 RVA: 0x00008868 File Offset: 0x00006A68
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 4)]
		public PosTagKind Tag { get; set; }
	}
}
