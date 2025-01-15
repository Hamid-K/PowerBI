using System;
using System.Runtime.CompilerServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x0200000D RID: 13
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public class Binder
	{
		// Token: 0x0600002E RID: 46 RVA: 0x00002858 File Offset: 0x00000A58
		public Binder(DBACCESSORFLAGS accessorFlags, DBLENGTH rowSize, Binding[] bindings)
		{
			this.accessorFlags = accessorFlags;
			this.rowSize = rowSize;
			this.bindings = bindings;
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600002F RID: 47 RVA: 0x00002875 File Offset: 0x00000A75
		public DBACCESSORFLAGS AccessorFlags
		{
			get
			{
				return this.accessorFlags;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000030 RID: 48 RVA: 0x0000287D File Offset: 0x00000A7D
		public DBLENGTH RowSize
		{
			get
			{
				return this.rowSize;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00002885 File Offset: 0x00000A85
		public Binding[] Bindings
		{
			get
			{
				return this.bindings;
			}
		}

		// Token: 0x0400001A RID: 26
		private readonly DBACCESSORFLAGS accessorFlags;

		// Token: 0x0400001B RID: 27
		private readonly Binding[] bindings;

		// Token: 0x0400001C RID: 28
		private readonly DBLENGTH rowSize;
	}
}
