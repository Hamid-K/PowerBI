using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput
{
	// Token: 0x02000139 RID: 313
	public interface ITable<out T> : IEnumerable<IEnumerable<T>>, IEnumerable
	{
		// Token: 0x170001CB RID: 459
		// (get) Token: 0x060006F3 RID: 1779
		IEnumerable<string> ColumnNames { get; }

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x060006F4 RID: 1780
		IEnumerable<IEnumerable<T>> Rows { get; }

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x060006F5 RID: 1781
		IReadOnlyList<ITableMetadata> Metadata { get; }
	}
}
