using System;

namespace Microsoft.InfoNav.Data.Contracts.ConceptualSchema.Annotations.Statistics
{
	// Token: 0x0200013D RID: 317
	public readonly struct EntityRowCountAnnotation
	{
		// Token: 0x06000821 RID: 2081 RVA: 0x00010D19 File Offset: 0x0000EF19
		public EntityRowCountAnnotation(int rowCount)
		{
			this.RowCount = rowCount;
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06000822 RID: 2082 RVA: 0x00010D22 File Offset: 0x0000EF22
		public int RowCount { get; }
	}
}
