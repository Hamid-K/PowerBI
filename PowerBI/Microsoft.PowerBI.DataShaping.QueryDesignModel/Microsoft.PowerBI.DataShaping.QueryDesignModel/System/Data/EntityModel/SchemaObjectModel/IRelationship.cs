using System;
using System.Collections.Generic;
using Microsoft.Data.Metadata.Edm;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x0200002D RID: 45
	internal interface IRelationship
	{
		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x060006BF RID: 1727
		string Name { get; }

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x060006C0 RID: 1728
		string FQName { get; }

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x060006C1 RID: 1729
		IList<IRelationshipEnd> Ends { get; }

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x060006C2 RID: 1730
		IList<ReferentialConstraint> Constraints { get; }

		// Token: 0x060006C3 RID: 1731
		bool TryGetEnd(string roleName, out IRelationshipEnd end);

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x060006C4 RID: 1732
		RelationshipKind RelationshipKind { get; }

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x060006C5 RID: 1733
		bool IsForeignKey { get; }
	}
}
