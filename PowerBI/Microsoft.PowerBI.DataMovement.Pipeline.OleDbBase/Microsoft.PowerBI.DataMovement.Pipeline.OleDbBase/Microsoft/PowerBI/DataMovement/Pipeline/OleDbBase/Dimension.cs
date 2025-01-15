using System;
using System.Runtime.CompilerServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x0200000C RID: 12
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public class Dimension
	{
		// Token: 0x06000029 RID: 41 RVA: 0x00002820 File Offset: 0x00000A20
		public Dimension(string name, uint columnsCount)
		{
			this.Name = name;
			this.ColumnsCount = columnsCount;
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00002836 File Offset: 0x00000A36
		// (set) Token: 0x0600002B RID: 43 RVA: 0x0000283E File Offset: 0x00000A3E
		public string Name { get; private set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00002847 File Offset: 0x00000A47
		// (set) Token: 0x0600002D RID: 45 RVA: 0x0000284F File Offset: 0x00000A4F
		public uint ColumnsCount { get; private set; }
	}
}
