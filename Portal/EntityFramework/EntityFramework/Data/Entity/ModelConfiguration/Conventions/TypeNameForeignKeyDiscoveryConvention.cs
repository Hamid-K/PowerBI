using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x020001B6 RID: 438
	public class TypeNameForeignKeyDiscoveryConvention : ForeignKeyDiscoveryConvention
	{
		// Token: 0x060017A0 RID: 6048 RVA: 0x00040258 File Offset: 0x0003E458
		protected override bool MatchDependentKeyProperty(AssociationType associationType, AssociationEndMember dependentAssociationEnd, EdmProperty dependentProperty, EntityType principalEntityType, EdmProperty principalKeyProperty)
		{
			Check.NotNull<AssociationType>(associationType, "associationType");
			Check.NotNull<AssociationEndMember>(dependentAssociationEnd, "dependentAssociationEnd");
			Check.NotNull<EdmProperty>(dependentProperty, "dependentProperty");
			Check.NotNull<EntityType>(principalEntityType, "principalEntityType");
			Check.NotNull<EdmProperty>(principalKeyProperty, "principalKeyProperty");
			return string.Equals(dependentProperty.Name, principalEntityType.Name + principalKeyProperty.Name, StringComparison.OrdinalIgnoreCase);
		}
	}
}
