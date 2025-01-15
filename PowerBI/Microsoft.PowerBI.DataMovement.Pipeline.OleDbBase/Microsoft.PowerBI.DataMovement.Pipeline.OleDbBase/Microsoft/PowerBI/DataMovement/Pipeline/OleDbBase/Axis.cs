using System;
using System.Runtime.CompilerServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x0200000B RID: 11
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public class Axis
	{
		// Token: 0x06000022 RID: 34 RVA: 0x000027D0 File Offset: 0x000009D0
		public Axis(uint index, uint coordinatesCount, Dimension[] dimensions)
		{
			this.Index = index;
			this.CoordinatesCount = coordinatesCount;
			this.Dimensions = dimensions;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000023 RID: 35 RVA: 0x000027ED File Offset: 0x000009ED
		// (set) Token: 0x06000024 RID: 36 RVA: 0x000027F5 File Offset: 0x000009F5
		public uint Index { get; private set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000025 RID: 37 RVA: 0x000027FE File Offset: 0x000009FE
		// (set) Token: 0x06000026 RID: 38 RVA: 0x00002806 File Offset: 0x00000A06
		public uint CoordinatesCount { get; private set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000027 RID: 39 RVA: 0x0000280F File Offset: 0x00000A0F
		// (set) Token: 0x06000028 RID: 40 RVA: 0x00002817 File Offset: 0x00000A17
		public Dimension[] Dimensions { get; private set; }
	}
}
