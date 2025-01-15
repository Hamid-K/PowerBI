using System;
using System.Collections.Generic;
using Microsoft.Data.Metadata.Edm;

namespace System.Data.Common.Utils
{
	// Token: 0x0200005D RID: 93
	internal static class MetadataHelper
	{
		// Token: 0x060008DA RID: 2266 RVA: 0x00013C44 File Offset: 0x00011E44
		internal static TypeUsage ConvertStoreTypeUsageToEdmTypeUsage(TypeUsage storeTypeUsage)
		{
			return storeTypeUsage.GetModelTypeUsage().ShallowCopy(FacetValues.NullFacetValues);
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x00013C58 File Offset: 0x00011E58
		internal static bool CheckIfAllErrorsAreWarnings(IList<EdmSchemaError> schemaErrors)
		{
			int count = schemaErrors.Count;
			for (int i = 0; i < count; i++)
			{
				if (schemaErrors[i].Severity != EdmSchemaErrorSeverity.Warning)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x00013C8C File Offset: 0x00011E8C
		internal static ConcurrencyMode GetConcurrencyMode(TypeUsage typeUsage)
		{
			Facet facet;
			if (typeUsage.Facets.TryGetValue("ConcurrencyMode", false, out facet) && facet.Value != null)
			{
				return (ConcurrencyMode)facet.Value;
			}
			return ConcurrencyMode.None;
		}

		// Token: 0x060008DD RID: 2269 RVA: 0x00013CC4 File Offset: 0x00011EC4
		internal static List<AssociationSet> GetAssociationsForEntitySet(EntitySetBase entitySet)
		{
			List<AssociationSet> list = new List<AssociationSet>();
			foreach (EntitySetBase entitySetBase in entitySet.EntityContainer.BaseEntitySets)
			{
				if (Helper.IsRelationshipSet(entitySetBase))
				{
					AssociationSet associationSet = (AssociationSet)entitySetBase;
					if (MetadataHelper.IsExtentAtSomeRelationshipEnd(associationSet, entitySet))
					{
						list.Add(associationSet);
					}
				}
			}
			return list;
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x00013D3C File Offset: 0x00011F3C
		internal static bool IsExtentAtSomeRelationshipEnd(AssociationSet relationshipSet, EntitySetBase extent)
		{
			return Helper.IsEntitySet(extent) && MetadataHelper.GetSomeEndForEntitySet(relationshipSet, (EntitySet)extent) != null;
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x00013D58 File Offset: 0x00011F58
		internal static AssociationEndMember GetSomeEndForEntitySet(AssociationSet associationSet, EntitySetBase entitySet)
		{
			foreach (AssociationSetEnd associationSetEnd in associationSet.AssociationSetEnds)
			{
				if (associationSetEnd.EntitySet.Equals(entitySet))
				{
					return associationSetEnd.CorrespondingAssociationEndMember;
				}
			}
			return null;
		}

		// Token: 0x040006EA RID: 1770
		internal static IEnumerable<string> PrimitiveTypesNames = new List<string>
		{
			"Binary", "Boolean", "Byte", "DateTime", "Decimal", "Double", "Guid", "Single", "SByte", "Int16",
			"Int32", "Int64", "String", "Time", "DateTimeOffset"
		};

		// Token: 0x040006EB RID: 1771
		internal static IEnumerable<string> StoreGeneratedPatternNames = new List<string> { "None", "Identity", "Computed" };

		// Token: 0x040006EC RID: 1772
		internal static IEnumerable<string> ConcurrencyModeNames = new List<string> { "None", "Fixed" };

		// Token: 0x040006ED RID: 1773
		internal const string ConcurrencyModeFacetName = "ConcurrencyMode";
	}
}
