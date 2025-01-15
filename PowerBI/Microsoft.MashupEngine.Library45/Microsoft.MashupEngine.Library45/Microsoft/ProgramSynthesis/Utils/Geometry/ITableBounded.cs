using System;

namespace Microsoft.ProgramSynthesis.Utils.Geometry
{
	// Token: 0x020005DA RID: 1498
	public interface ITableBounded : IBounded<TableUnit>
	{
		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x0600205C RID: 8284
		Bounds<TableUnit> TableBounds { get; }
	}
}
