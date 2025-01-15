using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Linq;

namespace System.Data.Entity.Core.Objects.Internal
{
	// Token: 0x02000444 RID: 1092
	internal class ForeignKeyFactory
	{
		// Token: 0x0600353A RID: 13626 RVA: 0x000AB375 File Offset: 0x000A9575
		public static bool IsConceptualNullKey(EntityKey key)
		{
			return !(key == null) && string.Equals(key.EntityContainerName, "EntityHasNullForeignKey") && string.Equals(key.EntitySetName, "EntityHasNullForeignKey");
		}

		// Token: 0x0600353B RID: 13627 RVA: 0x000AB3A6 File Offset: 0x000A95A6
		public static bool IsConceptualNullKeyChanged(EntityKey conceptualNullKey, EntityKey realKey)
		{
			return realKey == null || !EntityKey.InternalEquals(conceptualNullKey, realKey, false);
		}

		// Token: 0x0600353C RID: 13628 RVA: 0x000AB3BE File Offset: 0x000A95BE
		public static EntityKey CreateConceptualNullKey(EntityKey originalKey)
		{
			return new EntityKey("EntityHasNullForeignKey.EntityHasNullForeignKey", originalKey.EntityKeyValues);
		}

		// Token: 0x0600353D RID: 13629 RVA: 0x000AB3D0 File Offset: 0x000A95D0
		public static EntityKey CreateKeyFromForeignKeyValues(EntityEntry dependentEntry, RelatedEnd relatedEnd)
		{
			ReferentialConstraint referentialConstraint = ((AssociationType)relatedEnd.RelationMetadata).ReferentialConstraints.First<ReferentialConstraint>();
			return ForeignKeyFactory.CreateKeyFromForeignKeyValues(dependentEntry, referentialConstraint, relatedEnd.GetTargetEntitySetFromRelationshipSet(), false);
		}

		// Token: 0x0600353E RID: 13630 RVA: 0x000AB404 File Offset: 0x000A9604
		public static EntityKey CreateKeyFromForeignKeyValues(EntityEntry dependentEntry, ReferentialConstraint constraint, EntitySet principalEntitySet, bool useOriginalValues)
		{
			ReadOnlyMetadataCollection<EdmProperty> toProperties = constraint.ToProperties;
			int count = toProperties.Count;
			if (count != 1)
			{
				string[] keyMemberNames = principalEntitySet.ElementType.KeyMemberNames;
				object[] array = new object[count];
				ReadOnlyMetadataCollection<EdmProperty> fromProperties = constraint.FromProperties;
				for (int i = 0; i < count; i++)
				{
					object obj = (useOriginalValues ? dependentEntry.GetOriginalEntityValue(toProperties[i].Name) : dependentEntry.GetCurrentEntityValue(toProperties[i].Name));
					if (obj == DBNull.Value)
					{
						return null;
					}
					int num = Array.IndexOf<string>(keyMemberNames, fromProperties[i].Name);
					array[num] = obj;
				}
				return new EntityKey(principalEntitySet, array);
			}
			object obj2 = (useOriginalValues ? dependentEntry.GetOriginalEntityValue(toProperties.First<EdmProperty>().Name) : dependentEntry.GetCurrentEntityValue(toProperties.First<EdmProperty>().Name));
			if (obj2 != DBNull.Value)
			{
				return new EntityKey(principalEntitySet, obj2);
			}
			return null;
		}

		// Token: 0x04001138 RID: 4408
		private const string s_NullPart = "EntityHasNullForeignKey";

		// Token: 0x04001139 RID: 4409
		private const string s_NullForeignKey = "EntityHasNullForeignKey.EntityHasNullForeignKey";
	}
}
