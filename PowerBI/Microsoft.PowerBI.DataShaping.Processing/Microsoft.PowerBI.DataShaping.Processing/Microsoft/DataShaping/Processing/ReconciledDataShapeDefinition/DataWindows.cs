using System;
using System.Collections.Generic;

namespace Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition
{
	// Token: 0x02000040 RID: 64
	internal sealed class DataWindows
	{
		// Token: 0x060001D2 RID: 466 RVA: 0x00005F66 File Offset: 0x00004166
		internal DataWindows(IReadOnlyList<DataWindow> windows, DataBinding binding)
		{
			this.Windows = windows;
			this.Binding = binding;
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x00005F7C File Offset: 0x0000417C
		internal IReadOnlyList<DataWindow> Windows { get; }

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x00005F84 File Offset: 0x00004184
		internal DataBinding Binding { get; }
	}
}
