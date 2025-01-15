using System;
using System.Collections.Generic;

namespace Microsoft.DataShaping.InternalContracts
{
	// Token: 0x02000020 RID: 32
	internal sealed class RawDataDefinition
	{
		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x00003479 File Offset: 0x00001679
		// (set) Token: 0x060000A5 RID: 165 RVA: 0x00003481 File Offset: 0x00001681
		internal string DaxCommand { get; set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x0000348A File Offset: 0x0000168A
		// (set) Token: 0x060000A7 RID: 167 RVA: 0x00003492 File Offset: 0x00001692
		internal IReadOnlyDictionary<string, string> ColumnMapping { get; set; }
	}
}
