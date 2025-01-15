using System;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.Core.Objects.DataClasses
{
	// Token: 0x02000480 RID: 1152
	[Serializable]
	internal class RelationshipFixer<TSourceEntity, TTargetEntity> : IRelationshipFixer where TSourceEntity : class where TTargetEntity : class
	{
		// Token: 0x060038CD RID: 14541 RVA: 0x000BB3F6 File Offset: 0x000B95F6
		internal RelationshipFixer(RelationshipMultiplicity sourceRoleMultiplicity, RelationshipMultiplicity targetRoleMultiplicity)
		{
			this._sourceRoleMultiplicity = sourceRoleMultiplicity;
			this._targetRoleMultiplicity = targetRoleMultiplicity;
		}

		// Token: 0x060038CE RID: 14542 RVA: 0x000BB40C File Offset: 0x000B960C
		RelatedEnd IRelationshipFixer.CreateSourceEnd(RelationshipNavigation navigation, RelationshipManager relationshipManager)
		{
			return relationshipManager.CreateRelatedEnd<TTargetEntity, TSourceEntity>(navigation, this._targetRoleMultiplicity, this._sourceRoleMultiplicity, null);
		}

		// Token: 0x040012F5 RID: 4853
		private readonly RelationshipMultiplicity _sourceRoleMultiplicity;

		// Token: 0x040012F6 RID: 4854
		private readonly RelationshipMultiplicity _targetRoleMultiplicity;
	}
}
