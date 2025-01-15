using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.ModelConfiguration.Edm;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x020001AE RID: 430
	public class NavigationPropertyNameForeignKeyDiscoveryConvention : ForeignKeyDiscoveryConvention
	{
		// Token: 0x06001781 RID: 6017 RVA: 0x0003F870 File Offset: 0x0003DA70
		protected override bool MatchDependentKeyProperty(AssociationType associationType, AssociationEndMember dependentAssociationEnd, EdmProperty dependentProperty, EntityType principalEntityType, EdmProperty principalKeyProperty)
		{
			Check.NotNull<AssociationType>(associationType, "associationType");
			Check.NotNull<AssociationEndMember>(dependentAssociationEnd, "dependentAssociationEnd");
			Check.NotNull<EdmProperty>(dependentProperty, "dependentProperty");
			Check.NotNull<EntityType>(principalEntityType, "principalEntityType");
			Check.NotNull<EdmProperty>(principalKeyProperty, "principalKeyProperty");
			AssociationEndMember otherEnd = associationType.GetOtherEnd(dependentAssociationEnd);
			NavigationProperty navigationProperty = dependentAssociationEnd.GetEntityType().NavigationProperties.SingleOrDefault((NavigationProperty n) => n.ResultEnd == otherEnd);
			return navigationProperty != null && string.Equals(dependentProperty.Name, navigationProperty.Name + principalKeyProperty.Name, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x06001782 RID: 6018 RVA: 0x0003F90E File Offset: 0x0003DB0E
		protected override bool SupportsMultipleAssociations
		{
			get
			{
				return true;
			}
		}
	}
}
