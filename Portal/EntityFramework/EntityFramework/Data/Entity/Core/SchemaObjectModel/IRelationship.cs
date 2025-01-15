using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects.DataClasses;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x020002F7 RID: 759
	internal interface IRelationship
	{
		// Token: 0x170007A0 RID: 1952
		// (get) Token: 0x0600242D RID: 9261
		string Name { get; }

		// Token: 0x170007A1 RID: 1953
		// (get) Token: 0x0600242E RID: 9262
		string FQName { get; }

		// Token: 0x170007A2 RID: 1954
		// (get) Token: 0x0600242F RID: 9263
		IList<IRelationshipEnd> Ends { get; }

		// Token: 0x170007A3 RID: 1955
		// (get) Token: 0x06002430 RID: 9264
		IList<ReferentialConstraint> Constraints { get; }

		// Token: 0x06002431 RID: 9265
		bool TryGetEnd(string roleName, out IRelationshipEnd end);

		// Token: 0x170007A4 RID: 1956
		// (get) Token: 0x06002432 RID: 9266
		RelationshipKind RelationshipKind { get; }

		// Token: 0x170007A5 RID: 1957
		// (get) Token: 0x06002433 RID: 9267
		bool IsForeignKey { get; }
	}
}
