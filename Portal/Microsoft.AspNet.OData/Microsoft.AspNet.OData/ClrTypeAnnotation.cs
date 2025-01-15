using System;

namespace Microsoft.AspNet.OData
{
	// Token: 0x02000054 RID: 84
	public class ClrTypeAnnotation
	{
		// Token: 0x06000247 RID: 583 RVA: 0x0000A8EE File Offset: 0x00008AEE
		public ClrTypeAnnotation(Type clrType)
		{
			this.ClrType = clrType;
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000248 RID: 584 RVA: 0x0000A8FD File Offset: 0x00008AFD
		// (set) Token: 0x06000249 RID: 585 RVA: 0x0000A905 File Offset: 0x00008B05
		public Type ClrType { get; private set; }
	}
}
