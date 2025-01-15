using System;

namespace Microsoft.Lucia.Core.DomainModel
{
	// Token: 0x02000181 RID: 385
	public readonly struct EntityRowCountAnnotation
	{
		// Token: 0x06000783 RID: 1923 RVA: 0x0000E106 File Offset: 0x0000C306
		public EntityRowCountAnnotation(int rowCount)
		{
			this.RowCount = rowCount;
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000784 RID: 1924 RVA: 0x0000E10F File Offset: 0x0000C30F
		public int RowCount { get; }
	}
}
