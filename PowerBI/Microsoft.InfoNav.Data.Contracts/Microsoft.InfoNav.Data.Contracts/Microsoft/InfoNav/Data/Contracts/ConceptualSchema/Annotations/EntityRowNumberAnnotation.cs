using System;

namespace Microsoft.InfoNav.Data.Contracts.ConceptualSchema.Annotations
{
	// Token: 0x0200012E RID: 302
	public readonly struct EntityRowNumberAnnotation
	{
		// Token: 0x060007EC RID: 2028 RVA: 0x000106CF File Offset: 0x0000E8CF
		public EntityRowNumberAnnotation(string name)
		{
			this.Name = name;
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x060007ED RID: 2029 RVA: 0x000106D8 File Offset: 0x0000E8D8
		public string Name { get; }
	}
}
