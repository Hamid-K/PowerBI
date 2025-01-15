using System;
using System.Collections.Generic;
using Microsoft.Data.Metadata.Edm;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x0200002E RID: 46
	internal interface IRelationshipEnd
	{
		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x060006C6 RID: 1734
		string Name { get; }

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x060006C7 RID: 1735
		SchemaEntityType Type { get; }

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x060006C8 RID: 1736
		// (set) Token: 0x060006C9 RID: 1737
		RelationshipMultiplicity? Multiplicity { get; set; }

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x060006CA RID: 1738
		ICollection<OnOperation> Operations { get; }
	}
}
