using System;

namespace Microsoft.HostIntegration.Drda.Common
{
	// Token: 0x02000821 RID: 2081
	public interface IRowSet
	{
		// Token: 0x17000F87 RID: 3975
		ICell this[int columnIndex] { get; }

		// Token: 0x17000F88 RID: 3976
		// (get) Token: 0x060041D5 RID: 16853
		int FieldCount { get; }
	}
}
