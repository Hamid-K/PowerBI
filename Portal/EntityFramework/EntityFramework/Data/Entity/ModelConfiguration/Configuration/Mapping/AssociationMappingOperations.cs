using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.ModelConfiguration.Edm;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.ModelConfiguration.Configuration.Mapping
{
	// Token: 0x02000213 RID: 531
	internal static class AssociationMappingOperations
	{
		// Token: 0x06001C23 RID: 7203 RVA: 0x0004FA70 File Offset: 0x0004DC70
		private static void MoveAssociationSetMappingDependents(AssociationSetMapping associationSetMapping, EndPropertyMapping dependentMapping, EntitySet toSet, bool useExistingColumns)
		{
			EntityType toTable = toSet.ElementType;
			dependentMapping.PropertyMappings.Each(delegate(ScalarPropertyMapping pm)
			{
				EdmProperty oldColumn = pm.Column;
				pm.Column = TableOperations.MoveColumnAndAnyConstraints(associationSetMapping.Table, toTable, oldColumn, useExistingColumns);
				associationSetMapping.Conditions.Where((ConditionPropertyMapping cc) => cc.Column == oldColumn).Each((ConditionPropertyMapping cc) => cc.Column = pm.Column);
			});
			associationSetMapping.StoreEntitySet = toSet;
		}

		// Token: 0x06001C24 RID: 7204 RVA: 0x0004FAC0 File Offset: 0x0004DCC0
		public static void MoveAllDeclaredAssociationSetMappings(DbDatabaseMapping databaseMapping, EntityType entityType, EntityType fromTable, EntityType toTable, bool useExistingColumns)
		{
			IEnumerable<AssociationSetMapping> enumerable = databaseMapping.EntityContainerMappings.SelectMany((EntityContainerMapping asm) => asm.AssociationSetMappings);
			Func<AssociationSetMapping, bool> <>9__1;
			Func<AssociationSetMapping, bool> func;
			if ((func = <>9__1) == null)
			{
				func = (<>9__1 = (AssociationSetMapping a) => a.Table == fromTable && (a.AssociationSet.ElementType.SourceEnd.GetEntityType() == entityType || a.AssociationSet.ElementType.TargetEnd.GetEntityType() == entityType));
			}
			Action<ScalarPropertyMapping> <>9__2;
			foreach (AssociationSetMapping associationSetMapping in enumerable.Where(func).ToArray<AssociationSetMapping>())
			{
				AssociationEndMember associationEndMember;
				AssociationEndMember targetEnd;
				if (!associationSetMapping.AssociationSet.ElementType.TryGuessPrincipalAndDependentEnds(out associationEndMember, out targetEnd))
				{
					targetEnd = associationSetMapping.AssociationSet.ElementType.TargetEnd;
				}
				if (targetEnd.GetEntityType() == entityType)
				{
					EndPropertyMapping endPropertyMapping = ((targetEnd == associationSetMapping.TargetEndMapping.AssociationEnd) ? associationSetMapping.SourceEndMapping : associationSetMapping.TargetEndMapping);
					AssociationMappingOperations.MoveAssociationSetMappingDependents(associationSetMapping, endPropertyMapping, databaseMapping.Database.GetEntitySet(toTable), useExistingColumns);
					IEnumerable<ScalarPropertyMapping> propertyMappings = ((endPropertyMapping == associationSetMapping.TargetEndMapping) ? associationSetMapping.SourceEndMapping : associationSetMapping.TargetEndMapping).PropertyMappings;
					Action<ScalarPropertyMapping> action;
					if ((action = <>9__2) == null)
					{
						action = (<>9__2 = delegate(ScalarPropertyMapping pm)
						{
							if (pm.Column.DeclaringType != toTable)
							{
								pm.Column = toTable.Properties.Single((EdmProperty p) => string.Equals(p.GetPreferredName(), pm.Column.GetPreferredName(), StringComparison.Ordinal));
							}
						});
					}
					propertyMappings.Each(action);
				}
			}
		}
	}
}
