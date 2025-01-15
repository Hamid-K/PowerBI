using System;

namespace Microsoft.OleDb
{
	// Token: 0x02001E57 RID: 7767
	internal class Binder
	{
		// Token: 0x0600BEB7 RID: 48823 RVA: 0x0026946F File Offset: 0x0026766F
		public Binder(DBACCESSORFLAGS accessorFlags, Binding[] bindings)
		{
			this.accessorFlags = accessorFlags;
			this.bindings = bindings;
		}

		// Token: 0x17002EE2 RID: 12002
		// (get) Token: 0x0600BEB8 RID: 48824 RVA: 0x00269485 File Offset: 0x00267685
		public DBACCESSORFLAGS AccessorFlags
		{
			get
			{
				return this.accessorFlags;
			}
		}

		// Token: 0x17002EE3 RID: 12003
		// (get) Token: 0x0600BEB9 RID: 48825 RVA: 0x0026948D File Offset: 0x0026768D
		public Binding[] Bindings
		{
			get
			{
				return this.bindings;
			}
		}

		// Token: 0x0400611D RID: 24861
		private readonly DBACCESSORFLAGS accessorFlags;

		// Token: 0x0400611E RID: 24862
		private readonly Binding[] bindings;
	}
}
