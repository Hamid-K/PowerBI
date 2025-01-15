using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Linq;

namespace System.Data.Entity.ModelConfiguration.Edm.Services
{
	// Token: 0x02000174 RID: 372
	internal class AssociationTypeMappingGenerator : StructuralTypeMappingGenerator
	{
		// Token: 0x060016BD RID: 5821 RVA: 0x0003BE99 File Offset: 0x0003A099
		public AssociationTypeMappingGenerator(DbProviderManifest providerManifest)
			: base(providerManifest)
		{
		}

		// Token: 0x060016BE RID: 5822 RVA: 0x0003BEA2 File Offset: 0x0003A0A2
		public void Generate(AssociationType associationType, DbDatabaseMapping databaseMapping)
		{
			if (associationType.Constraint != null)
			{
				AssociationTypeMappingGenerator.GenerateForeignKeyAssociationType(associationType, databaseMapping);
				return;
			}
			if (associationType.IsManyToMany())
			{
				this.GenerateManyToManyAssociation(associationType, databaseMapping);
				return;
			}
			this.GenerateIndependentAssociationType(associationType, databaseMapping);
		}

		// Token: 0x060016BF RID: 5823 RVA: 0x0003BED0 File Offset: 0x0003A0D0
		private static void GenerateForeignKeyAssociationType(AssociationType associationType, DbDatabaseMapping databaseMapping)
		{
			AssociationEndMember dependentEnd = associationType.Constraint.DependentEnd;
			AssociationEndMember otherEnd = associationType.GetOtherEnd(dependentEnd);
			EntityTypeMapping entityTypeMappingInHierarchy = StructuralTypeMappingGenerator.GetEntityTypeMappingInHierarchy(databaseMapping, otherEnd.GetEntityType());
			EntityTypeMapping dependentEntityTypeMapping = StructuralTypeMappingGenerator.GetEntityTypeMappingInHierarchy(databaseMapping, dependentEnd.GetEntityType());
			ForeignKeyBuilder foreignKeyBuilder = new ForeignKeyBuilder(databaseMapping.Database, associationType.Name)
			{
				PrincipalTable = entityTypeMappingInHierarchy.MappingFragments.Single<MappingFragment>().Table,
				DeleteAction = ((otherEnd.DeleteBehavior != OperationAction.None) ? otherEnd.DeleteBehavior : OperationAction.None)
			};
			dependentEntityTypeMapping.MappingFragments.Single<MappingFragment>().Table.AddForeignKey(foreignKeyBuilder);
			foreignKeyBuilder.DependentColumns = associationType.Constraint.ToProperties.Select((EdmProperty dependentProperty) => dependentEntityTypeMapping.GetPropertyMapping(new EdmProperty[] { dependentProperty }).ColumnProperty);
			foreignKeyBuilder.SetAssociationType(associationType);
		}

		// Token: 0x060016C0 RID: 5824 RVA: 0x0003BFA0 File Offset: 0x0003A1A0
		private void GenerateManyToManyAssociation(AssociationType associationType, DbDatabaseMapping databaseMapping)
		{
			EntityType entityType = associationType.SourceEnd.GetEntityType();
			EntityType entityType2 = associationType.TargetEnd.GetEntityType();
			EntityType entityType3 = databaseMapping.Database.AddTable(entityType.Name + entityType2.Name);
			AssociationSetMapping associationSetMapping = AssociationTypeMappingGenerator.GenerateAssociationSetMapping(associationType, databaseMapping, associationType.SourceEnd, associationType.TargetEnd, entityType3);
			this.GenerateIndependentForeignKeyConstraint(databaseMapping, entityType, entityType2, entityType3, associationSetMapping, associationSetMapping.SourceEndMapping, associationType.SourceEnd.Name, null, true);
			this.GenerateIndependentForeignKeyConstraint(databaseMapping, entityType2, entityType, entityType3, associationSetMapping, associationSetMapping.TargetEndMapping, associationType.TargetEnd.Name, null, true);
		}

		// Token: 0x060016C1 RID: 5825 RVA: 0x0003C034 File Offset: 0x0003A234
		private void GenerateIndependentAssociationType(AssociationType associationType, DbDatabaseMapping databaseMapping)
		{
			AssociationEndMember sourceEnd;
			AssociationEndMember targetEnd;
			if (!associationType.TryGuessPrincipalAndDependentEnds(out sourceEnd, out targetEnd))
			{
				if (!associationType.IsPrincipalConfigured())
				{
					throw Error.UnableToDeterminePrincipal(associationType.SourceEnd.GetEntityType().GetClrType(), associationType.TargetEnd.GetEntityType().GetClrType());
				}
				sourceEnd = associationType.SourceEnd;
				targetEnd = associationType.TargetEnd;
			}
			EntityTypeMapping entityTypeMappingInHierarchy = StructuralTypeMappingGenerator.GetEntityTypeMappingInHierarchy(databaseMapping, targetEnd.GetEntityType());
			EntityType table = entityTypeMappingInHierarchy.MappingFragments.First<MappingFragment>().Table;
			AssociationSetMapping associationSetMapping = AssociationTypeMappingGenerator.GenerateAssociationSetMapping(associationType, databaseMapping, sourceEnd, targetEnd, table);
			this.GenerateIndependentForeignKeyConstraint(databaseMapping, sourceEnd.GetEntityType(), targetEnd.GetEntityType(), table, associationSetMapping, associationSetMapping.SourceEndMapping, associationType.Name, sourceEnd, false);
			foreach (EdmProperty edmProperty in targetEnd.GetEntityType().KeyProperties())
			{
				associationSetMapping.TargetEndMapping.AddPropertyMapping(new ScalarPropertyMapping(edmProperty, entityTypeMappingInHierarchy.GetPropertyMapping(new EdmProperty[] { edmProperty }).ColumnProperty));
			}
		}

		// Token: 0x060016C2 RID: 5826 RVA: 0x0003C148 File Offset: 0x0003A348
		private static AssociationSetMapping GenerateAssociationSetMapping(AssociationType associationType, DbDatabaseMapping databaseMapping, AssociationEndMember principalEnd, AssociationEndMember dependentEnd, EntityType dependentTable)
		{
			AssociationSetMapping associationSetMapping = databaseMapping.AddAssociationSetMapping(databaseMapping.Model.GetAssociationSet(associationType), databaseMapping.Database.GetEntitySet(dependentTable));
			associationSetMapping.StoreEntitySet = databaseMapping.Database.GetEntitySet(dependentTable);
			associationSetMapping.SourceEndMapping.AssociationEnd = principalEnd;
			associationSetMapping.TargetEndMapping.AssociationEnd = dependentEnd;
			return associationSetMapping;
		}

		// Token: 0x060016C3 RID: 5827 RVA: 0x0003C1A0 File Offset: 0x0003A3A0
		private void GenerateIndependentForeignKeyConstraint(DbDatabaseMapping databaseMapping, EntityType principalEntityType, EntityType dependentEntityType, EntityType dependentTable, AssociationSetMapping associationSetMapping, EndPropertyMapping associationEndMapping, string name, AssociationEndMember principalEnd, bool isPrimaryKeyColumn = false)
		{
			EntityType table = StructuralTypeMappingGenerator.GetEntityTypeMappingInHierarchy(databaseMapping, principalEntityType).MappingFragments.Single<MappingFragment>().Table;
			ForeignKeyBuilder foreignKeyBuilder = new ForeignKeyBuilder(databaseMapping.Database, name)
			{
				PrincipalTable = table,
				DeleteAction = ((associationEndMapping.AssociationEnd.DeleteBehavior != OperationAction.None) ? associationEndMapping.AssociationEnd.DeleteBehavior : OperationAction.None)
			};
			NavigationProperty navigationProperty = databaseMapping.Model.EntityTypes.SelectMany((EntityType e) => e.DeclaredNavigationProperties).SingleOrDefault((NavigationProperty n) => n.ResultEnd == principalEnd);
			dependentTable.AddForeignKey(foreignKeyBuilder);
			foreignKeyBuilder.DependentColumns = this.GenerateIndependentForeignKeyColumns(principalEntityType, dependentEntityType, associationSetMapping, associationEndMapping, dependentTable, isPrimaryKeyColumn, navigationProperty);
		}

		// Token: 0x060016C4 RID: 5828 RVA: 0x0003C26C File Offset: 0x0003A46C
		private IEnumerable<EdmProperty> GenerateIndependentForeignKeyColumns(EntityType principalEntityType, EntityType dependentEntityType, AssociationSetMapping associationSetMapping, EndPropertyMapping associationEndMapping, EntityType dependentTable, bool isPrimaryKeyColumn, NavigationProperty principalNavigationProperty)
		{
			foreach (EdmProperty property in principalEntityType.KeyProperties())
			{
				string text = ((principalNavigationProperty != null) ? principalNavigationProperty.Name : principalEntityType.Name) + "_" + property.Name;
				EdmProperty foreignKeyColumn = base.MapTableColumn(property, text, false);
				dependentTable.AddColumn(foreignKeyColumn);
				if (isPrimaryKeyColumn)
				{
					dependentTable.AddKeyMember(foreignKeyColumn);
				}
				foreignKeyColumn.Nullable = associationEndMapping.AssociationEnd.IsOptional() || (associationEndMapping.AssociationEnd.IsRequired() && dependentEntityType.BaseType != null);
				foreignKeyColumn.StoreGeneratedPattern = StoreGeneratedPattern.None;
				yield return foreignKeyColumn;
				associationEndMapping.AddPropertyMapping(new ScalarPropertyMapping(property, foreignKeyColumn));
				if (foreignKeyColumn.Nullable)
				{
					associationSetMapping.AddCondition(new IsNullConditionMapping(foreignKeyColumn, false));
				}
				foreignKeyColumn = null;
				property = null;
			}
			IEnumerator<EdmProperty> enumerator = null;
			yield break;
			yield break;
		}
	}
}
