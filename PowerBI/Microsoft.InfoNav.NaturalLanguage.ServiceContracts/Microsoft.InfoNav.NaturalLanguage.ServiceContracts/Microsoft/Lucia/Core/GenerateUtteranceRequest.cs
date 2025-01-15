using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.Lucia.Core
{
	// Token: 0x02000143 RID: 323
	[DataContract(Name = "GenerateUtteranceRequest", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class GenerateUtteranceRequest
	{
		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06000664 RID: 1636 RVA: 0x0000B4A6 File Offset: 0x000096A6
		// (set) Token: 0x06000665 RID: 1637 RVA: 0x0000B4AE File Offset: 0x000096AE
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 10)]
		public int Version { get; set; }

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000666 RID: 1638 RVA: 0x0000B4B7 File Offset: 0x000096B7
		// (set) Token: 0x06000667 RID: 1639 RVA: 0x0000B4BF File Offset: 0x000096BF
		[DataMember(IsRequired = true, EmitDefaultValue = false, Order = 20)]
		public QueryDefinition QueryDefinition { get; set; }

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06000668 RID: 1640 RVA: 0x0000B4C8 File Offset: 0x000096C8
		// (set) Token: 0x06000669 RID: 1641 RVA: 0x0000B4D0 File Offset: 0x000096D0
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 30)]
		public DependentSchema[] DependentSchemas { get; set; }

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x0600066A RID: 1642 RVA: 0x0000B4D9 File Offset: 0x000096D9
		// (set) Token: 0x0600066B RID: 1643 RVA: 0x0000B4E1 File Offset: 0x000096E1
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 40)]
		public GenerateUtteranceRequestOptions Options { get; set; }

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x0600066C RID: 1644 RVA: 0x0000B4EA File Offset: 0x000096EA
		// (set) Token: 0x0600066D RID: 1645 RVA: 0x0000B4F2 File Offset: 0x000096F2
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 50)]
		public InterpretRequestOptions InterpretOptions { get; set; }

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x0600066E RID: 1646 RVA: 0x0000B4FB File Offset: 0x000096FB
		// (set) Token: 0x0600066F RID: 1647 RVA: 0x0000B503 File Offset: 0x00009703
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 60)]
		public IList<FeatureSwitch> FeatureSwitches { get; set; }
	}
}
