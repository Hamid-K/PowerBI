using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x0200033A RID: 826
	internal class ConstraintManager
	{
		// Token: 0x06002742 RID: 10050 RVA: 0x00072370 File Offset: 0x00070570
		internal bool IsParentChildRelationship(EntitySetBase table1, EntitySetBase table2, out List<ForeignKeyConstraint> constraints)
		{
			this.LoadRelationships(table1.EntityContainer);
			this.LoadRelationships(table2.EntityContainer);
			ExtentPair extentPair = new ExtentPair(table1, table2);
			return this.m_parentChildRelationships.TryGetValue(extentPair, out constraints);
		}

		// Token: 0x06002743 RID: 10051 RVA: 0x000723AC File Offset: 0x000705AC
		internal void LoadRelationships(EntityContainer entityContainer)
		{
			if (this.m_entityContainerMap.ContainsKey(entityContainer))
			{
				return;
			}
			foreach (EntitySetBase entitySetBase in entityContainer.BaseEntitySets)
			{
				RelationshipSet relationshipSet = entitySetBase as RelationshipSet;
				if (relationshipSet != null)
				{
					RelationshipType elementType = relationshipSet.ElementType;
					AssociationType associationType = elementType as AssociationType;
					if (associationType != null && ConstraintManager.IsBinary(elementType))
					{
						foreach (ReferentialConstraint referentialConstraint in associationType.ReferentialConstraints)
						{
							ForeignKeyConstraint foreignKeyConstraint = new ForeignKeyConstraint(relationshipSet, referentialConstraint);
							List<ForeignKeyConstraint> list;
							if (!this.m_parentChildRelationships.TryGetValue(foreignKeyConstraint.Pair, out list))
							{
								list = new List<ForeignKeyConstraint>();
								this.m_parentChildRelationships[foreignKeyConstraint.Pair] = list;
							}
							list.Add(foreignKeyConstraint);
						}
					}
				}
			}
			this.m_entityContainerMap[entityContainer] = entityContainer;
		}

		// Token: 0x06002744 RID: 10052 RVA: 0x000724C0 File Offset: 0x000706C0
		internal ConstraintManager()
		{
			this.m_entityContainerMap = new Dictionary<EntityContainer, EntityContainer>();
			this.m_parentChildRelationships = new Dictionary<ExtentPair, List<ForeignKeyConstraint>>();
		}

		// Token: 0x06002745 RID: 10053 RVA: 0x000724E0 File Offset: 0x000706E0
		private static bool IsBinary(RelationshipType relationshipType)
		{
			int num = 0;
			using (ReadOnlyMetadataCollection<EdmMember>.Enumerator enumerator = relationshipType.Members.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current is RelationshipEndMember)
					{
						num++;
						if (num > 2)
						{
							return false;
						}
					}
				}
			}
			return num == 2;
		}

		// Token: 0x04000DAF RID: 3503
		private readonly Dictionary<EntityContainer, EntityContainer> m_entityContainerMap;

		// Token: 0x04000DB0 RID: 3504
		private readonly Dictionary<ExtentPair, List<ForeignKeyConstraint>> m_parentChildRelationships;
	}
}
