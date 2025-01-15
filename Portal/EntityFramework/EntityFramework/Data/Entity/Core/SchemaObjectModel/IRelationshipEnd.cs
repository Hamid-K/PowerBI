using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x020002F8 RID: 760
	internal interface IRelationshipEnd
	{
		// Token: 0x170007A6 RID: 1958
		// (get) Token: 0x06002434 RID: 9268
		string Name { get; }

		// Token: 0x170007A7 RID: 1959
		// (get) Token: 0x06002435 RID: 9269
		SchemaEntityType Type { get; }

		// Token: 0x170007A8 RID: 1960
		// (get) Token: 0x06002436 RID: 9270
		// (set) Token: 0x06002437 RID: 9271
		RelationshipMultiplicity? Multiplicity { get; set; }

		// Token: 0x170007A9 RID: 1961
		// (get) Token: 0x06002438 RID: 9272
		ICollection<OnOperation> Operations { get; }
	}
}
