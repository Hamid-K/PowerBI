using System;
using System.Collections.Generic;

namespace Microsoft.ProgramSynthesis.Wrangling
{
	// Token: 0x020000B3 RID: 179
	public interface IRow : IEquatable<IRow>
	{
		// Token: 0x1700014B RID: 331
		// (get) Token: 0x0600041A RID: 1050
		IEnumerable<string> ColumnNames { get; }

		// Token: 0x0600041B RID: 1051
		bool TryGetValue(string columnName, out object value);
	}
}
