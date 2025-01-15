using System;

namespace Microsoft.HostIntegration.Drda.Common
{
	// Token: 0x02000823 RID: 2083
	public interface ISchemaTable
	{
		// Token: 0x17000FAD RID: 4013
		// (get) Token: 0x0600421E RID: 16926
		int ColumnCount { get; }

		// Token: 0x17000FAE RID: 4014
		IColumnSchema this[int columnIndex] { get; }

		// Token: 0x17000FAF RID: 4015
		IColumnSchema this[string columnName] { get; }
	}
}
