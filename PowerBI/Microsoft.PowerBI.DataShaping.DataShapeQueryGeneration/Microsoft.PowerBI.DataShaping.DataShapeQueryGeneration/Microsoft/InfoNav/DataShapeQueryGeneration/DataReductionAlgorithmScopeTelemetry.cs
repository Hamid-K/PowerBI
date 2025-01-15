using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x02000023 RID: 35
	[DataContract]
	internal sealed class DataReductionAlgorithmScopeTelemetry
	{
		// Token: 0x0600017A RID: 378 RVA: 0x00008E3A File Offset: 0x0000703A
		private DataReductionAlgorithmScopeTelemetry()
		{
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00008E42 File Offset: 0x00007042
		internal DataReductionAlgorithmScopeTelemetry(IntermediateReductionScope scope)
		{
			List<int> primary = scope.Primary;
			this.Primary = ((primary != null) ? primary.ToArray() : null);
			List<int> secondary = scope.Secondary;
			this.Secondary = ((secondary != null) ? secondary.ToArray() : null);
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600017C RID: 380 RVA: 0x00008E7A File Offset: 0x0000707A
		// (set) Token: 0x0600017D RID: 381 RVA: 0x00008E82 File Offset: 0x00007082
		[DataMember(Name = "P", EmitDefaultValue = false, Order = 10)]
		internal IReadOnlyList<int> Primary { get; private set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600017E RID: 382 RVA: 0x00008E8B File Offset: 0x0000708B
		// (set) Token: 0x0600017F RID: 383 RVA: 0x00008E93 File Offset: 0x00007093
		[DataMember(Name = "S", EmitDefaultValue = false, Order = 20)]
		internal IReadOnlyList<int> Secondary { get; private set; }
	}
}
