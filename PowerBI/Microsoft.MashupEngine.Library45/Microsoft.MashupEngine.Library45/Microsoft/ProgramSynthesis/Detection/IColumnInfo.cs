using System;
using System.Collections.Generic;

namespace Microsoft.ProgramSynthesis.Detection
{
	// Token: 0x02000A77 RID: 2679
	public interface IColumnInfo
	{
		// Token: 0x17000B85 RID: 2949
		// (get) Token: 0x06004275 RID: 17013
		string ColumnName { get; }

		// Token: 0x17000B86 RID: 2950
		// (get) Token: 0x06004276 RID: 17014
		bool UseColumnForLearning { get; }

		// Token: 0x17000B87 RID: 2951
		// (get) Token: 0x06004277 RID: 17015
		IEnumerable<string> Data { get; }

		// Token: 0x17000B88 RID: 2952
		// (get) Token: 0x06004278 RID: 17016
		long? TrueLength { get; }

		// Token: 0x06004279 RID: 17017
		void ResetDataStream();
	}
}
