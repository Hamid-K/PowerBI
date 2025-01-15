using System;

namespace System.Data.Entity.Core.Objects.DataClasses
{
	// Token: 0x0200047E RID: 1150
	internal interface IRelationshipFixer
	{
		// Token: 0x06003846 RID: 14406
		RelatedEnd CreateSourceEnd(RelationshipNavigation navigation, RelationshipManager relationshipManager);
	}
}
