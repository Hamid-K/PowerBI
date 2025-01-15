using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.DataShaping.InternalContracts.DataShapeDefinition
{
	// Token: 0x0200012B RID: 299
	[DataContract]
	internal sealed class DataWindows
	{
		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000821 RID: 2081 RVA: 0x000100D2 File Offset: 0x0000E2D2
		// (set) Token: 0x06000822 RID: 2082 RVA: 0x000100DA File Offset: 0x0000E2DA
		[DataMember(EmitDefaultValue = false, Order = 1)]
		internal IList<DataWindow> Windows { get; set; }

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000823 RID: 2083 RVA: 0x000100E3 File Offset: 0x0000E2E3
		// (set) Token: 0x06000824 RID: 2084 RVA: 0x000100EB File Offset: 0x0000E2EB
		[DataMember(EmitDefaultValue = false, Order = 2)]
		internal DataBinding DataBinding { get; set; }
	}
}
