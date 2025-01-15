using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Mapping.Update.Internal;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;

namespace System.Data.Entity.ModelConfiguration.Edm.Services
{
	// Token: 0x02000177 RID: 375
	internal class ModificationFunctionMappingGenerator : StructuralTypeMappingGenerator
	{
		// Token: 0x060016CF RID: 5839 RVA: 0x0003C5F8 File Offset: 0x0003A7F8
		public ModificationFunctionMappingGenerator(DbProviderManifest providerManifest)
			: base(providerManifest)
		{
		}

		// Token: 0x060016D0 RID: 5840 RVA: 0x0003C604 File Offset: 0x0003A804
		public void Generate(EntityType entityType, DbDatabaseMapping databaseMapping)
		{
			if (entityType.Abstract)
			{
				return;
			}
			EntitySet entitySet = databaseMapping.Model.GetEntitySet(entityType);
			EntitySetMapping entitySetMapping = databaseMapping.GetEntitySetMapping(entitySet);
			List<ColumnMappingBuilder> list = ModificationFunctionMappingGenerator.GetColumnMappings(entityType, entitySetMapping).ToList<ColumnMappingBuilder>();
			List<Tuple<ModificationFunctionMemberPath, EdmProperty>> list2 = ModificationFunctionMappingGenerator.GetIndependentFkColumns(entityType, databaseMapping).ToList<Tuple<ModificationFunctionMemberPath, EdmProperty>>();
			ModificationFunctionMapping modificationFunctionMapping = this.GenerateFunctionMapping(ModificationOperator.Insert, entitySetMapping.EntitySet, entityType, databaseMapping, entityType.Properties, list2, list, entityType.Properties.Where((EdmProperty p) => p.HasStoreGeneratedPattern()), null);
			ModificationFunctionMapping modificationFunctionMapping2 = this.GenerateFunctionMapping(ModificationOperator.Update, entitySetMapping.EntitySet, entityType, databaseMapping, entityType.Properties, list2, list, entityType.Properties.Where(delegate(EdmProperty p)
			{
				StoreGeneratedPattern? storeGeneratedPattern = p.GetStoreGeneratedPattern();
				StoreGeneratedPattern storeGeneratedPattern2 = StoreGeneratedPattern.Computed;
				return (storeGeneratedPattern.GetValueOrDefault() == storeGeneratedPattern2) & (storeGeneratedPattern != null);
			}), null);
			ModificationFunctionMapping modificationFunctionMapping3 = this.GenerateFunctionMapping(ModificationOperator.Delete, entitySetMapping.EntitySet, entityType, databaseMapping, entityType.Properties, list2, list, null, null);
			EntityTypeModificationFunctionMapping entityTypeModificationFunctionMapping = new EntityTypeModificationFunctionMapping(entityType, modificationFunctionMapping3, modificationFunctionMapping, modificationFunctionMapping2);
			entitySetMapping.AddModificationFunctionMapping(entityTypeModificationFunctionMapping);
		}

		// Token: 0x060016D1 RID: 5841 RVA: 0x0003C704 File Offset: 0x0003A904
		private static IEnumerable<ColumnMappingBuilder> GetColumnMappings(EntityType entityType, EntitySetMapping entitySetMapping)
		{
			return new EntityType[] { entityType }.Concat(ModificationFunctionMappingGenerator.GetParents(entityType)).SelectMany((EntityType et) => entitySetMapping.TypeMappings.Where((TypeMapping stm) => stm.Types.Contains(et)).SelectMany((TypeMapping stm) => stm.MappingFragments).SelectMany((MappingFragment mf) => mf.ColumnMappings));
		}

		// Token: 0x060016D2 RID: 5842 RVA: 0x0003C744 File Offset: 0x0003A944
		public void Generate(AssociationSetMapping associationSetMapping, DbDatabaseMapping databaseMapping)
		{
			List<Tuple<ModificationFunctionMemberPath, EdmProperty>> list = ModificationFunctionMappingGenerator.GetIndependentFkColumns(associationSetMapping).ToList<Tuple<ModificationFunctionMemberPath, EdmProperty>>();
			EdmType entityType = associationSetMapping.AssociationSet.ElementType.SourceEnd.GetEntityType();
			EntityType entityType2 = associationSetMapping.AssociationSet.ElementType.TargetEnd.GetEntityType();
			string text = entityType.Name + entityType2.Name;
			ModificationFunctionMapping modificationFunctionMapping = this.GenerateFunctionMapping(ModificationOperator.Insert, associationSetMapping.AssociationSet, associationSetMapping.AssociationSet.ElementType, databaseMapping, Enumerable.Empty<EdmProperty>(), list, new ColumnMappingBuilder[0], null, text);
			ModificationFunctionMapping modificationFunctionMapping2 = this.GenerateFunctionMapping(ModificationOperator.Delete, associationSetMapping.AssociationSet, associationSetMapping.AssociationSet.ElementType, databaseMapping, Enumerable.Empty<EdmProperty>(), list, new ColumnMappingBuilder[0], null, text);
			associationSetMapping.ModificationFunctionMapping = new AssociationSetModificationFunctionMapping(associationSetMapping.AssociationSet, modificationFunctionMapping2, modificationFunctionMapping);
		}

		// Token: 0x060016D3 RID: 5843 RVA: 0x0003C7FE File Offset: 0x0003A9FE
		private static IEnumerable<Tuple<ModificationFunctionMemberPath, EdmProperty>> GetIndependentFkColumns(AssociationSetMapping associationSetMapping)
		{
			foreach (ScalarPropertyMapping scalarPropertyMapping in associationSetMapping.SourceEndMapping.PropertyMappings)
			{
				yield return Tuple.Create<ModificationFunctionMemberPath, EdmProperty>(new ModificationFunctionMemberPath(new EdmMember[]
				{
					scalarPropertyMapping.Property,
					associationSetMapping.SourceEndMapping.AssociationEnd
				}, associationSetMapping.AssociationSet), scalarPropertyMapping.Column);
			}
			IEnumerator<ScalarPropertyMapping> enumerator = null;
			foreach (ScalarPropertyMapping scalarPropertyMapping2 in associationSetMapping.TargetEndMapping.PropertyMappings)
			{
				yield return Tuple.Create<ModificationFunctionMemberPath, EdmProperty>(new ModificationFunctionMemberPath(new EdmMember[]
				{
					scalarPropertyMapping2.Property,
					associationSetMapping.TargetEndMapping.AssociationEnd
				}, associationSetMapping.AssociationSet), scalarPropertyMapping2.Column);
			}
			enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060016D4 RID: 5844 RVA: 0x0003C80E File Offset: 0x0003AA0E
		private static IEnumerable<Tuple<ModificationFunctionMemberPath, EdmProperty>> GetIndependentFkColumns(EntityType entityType, DbDatabaseMapping databaseMapping)
		{
			foreach (AssociationSetMapping associationSetMapping in databaseMapping.GetAssociationSetMappings())
			{
				AssociationType elementType = associationSetMapping.AssociationSet.ElementType;
				if (!elementType.IsManyToMany())
				{
					AssociationEndMember associationEndMember;
					AssociationEndMember dependentEnd;
					if (!elementType.TryGuessPrincipalAndDependentEnds(out associationEndMember, out dependentEnd))
					{
						dependentEnd = elementType.TargetEnd;
					}
					EntityType entityType2 = dependentEnd.GetEntityType();
					if (entityType2 == entityType || ModificationFunctionMappingGenerator.GetParents(entityType).Contains(entityType2))
					{
						EndPropertyMapping endPropertyMapping = ((associationSetMapping.TargetEndMapping.AssociationEnd != dependentEnd) ? associationSetMapping.TargetEndMapping : associationSetMapping.SourceEndMapping);
						foreach (ScalarPropertyMapping scalarPropertyMapping in endPropertyMapping.PropertyMappings)
						{
							yield return Tuple.Create<ModificationFunctionMemberPath, EdmProperty>(new ModificationFunctionMemberPath(new EdmMember[] { scalarPropertyMapping.Property, dependentEnd }, associationSetMapping.AssociationSet), scalarPropertyMapping.Column);
						}
						IEnumerator<ScalarPropertyMapping> enumerator2 = null;
					}
					dependentEnd = null;
					associationSetMapping = null;
				}
			}
			IEnumerator<AssociationSetMapping> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060016D5 RID: 5845 RVA: 0x0003C825 File Offset: 0x0003AA25
		private static IEnumerable<EntityType> GetParents(EntityType entityType)
		{
			while (entityType.BaseType != null)
			{
				yield return (EntityType)entityType.BaseType;
				entityType = (EntityType)entityType.BaseType;
			}
			yield break;
		}

		// Token: 0x060016D6 RID: 5846 RVA: 0x0003C838 File Offset: 0x0003AA38
		private ModificationFunctionMapping GenerateFunctionMapping(ModificationOperator modificationOperator, EntitySetBase entitySetBase, EntityTypeBase entityTypeBase, DbDatabaseMapping databaseMapping, IEnumerable<EdmProperty> parameterProperties, IEnumerable<Tuple<ModificationFunctionMemberPath, EdmProperty>> iaFkProperties, IList<ColumnMappingBuilder> columnMappings, IEnumerable<EdmProperty> resultProperties = null, string functionNamePrefix = null)
		{
			bool flag = modificationOperator == ModificationOperator.Delete;
			FunctionParameterMappingGenerator functionParameterMappingGenerator = new FunctionParameterMappingGenerator(this._providerManifest);
			List<ModificationFunctionParameterBinding> list = functionParameterMappingGenerator.Generate((modificationOperator == ModificationOperator.Insert && ModificationFunctionMappingGenerator.IsTableSplitDependent(entityTypeBase, databaseMapping)) ? ModificationOperator.Update : modificationOperator, parameterProperties, columnMappings, new List<EdmProperty>(), flag).Concat(functionParameterMappingGenerator.Generate(iaFkProperties, flag)).ToList<ModificationFunctionParameterBinding>();
			List<FunctionParameter> list2 = list.Select((ModificationFunctionParameterBinding b) => b.Parameter).ToList<FunctionParameter>();
			ModificationFunctionMappingGenerator.UniquifyParameterNames(list2);
			EdmFunctionPayload edmFunctionPayload = new EdmFunctionPayload
			{
				ReturnParameters = new FunctionParameter[0],
				Parameters = list2.ToArray(),
				IsComposable = new bool?(false)
			};
			EdmFunction edmFunction = databaseMapping.Database.AddFunction((functionNamePrefix ?? entityTypeBase.Name) + "_" + modificationOperator.ToString(), edmFunctionPayload);
			return new ModificationFunctionMapping(entitySetBase, entityTypeBase, edmFunction, list, null, (resultProperties != null) ? resultProperties.Select((EdmProperty p) => new ModificationFunctionResultBinding(columnMappings.First((ColumnMappingBuilder cm) => cm.PropertyPath.SequenceEqual(new EdmProperty[] { p })).ColumnProperty.Name, p)) : null);
		}

		// Token: 0x060016D7 RID: 5847 RVA: 0x0003C958 File Offset: 0x0003AB58
		private static bool IsTableSplitDependent(EntityTypeBase entityTypeBase, DbDatabaseMapping databaseMapping)
		{
			AssociationType associationType = databaseMapping.Model.AssociationTypes.SingleOrDefault((AssociationType at) => at.IsForeignKey && at.IsRequiredToRequired() && !at.IsSelfReferencing() && (at.SourceEnd.GetEntityType().IsAssignableFrom(entityTypeBase) || at.TargetEnd.GetEntityType().IsAssignableFrom(entityTypeBase)) && databaseMapping.Database.AssociationTypes.All((AssociationType fk) => fk.Name != at.Name));
			return associationType != null && associationType.TargetEnd.GetEntityType() == entityTypeBase;
		}

		// Token: 0x060016D8 RID: 5848 RVA: 0x0003C9B4 File Offset: 0x0003ABB4
		private static void UniquifyParameterNames(IList<FunctionParameter> parameters)
		{
			foreach (FunctionParameter functionParameter in parameters)
			{
				functionParameter.Name = parameters.Except(new FunctionParameter[] { functionParameter }).UniquifyName(functionParameter.Name);
			}
		}
	}
}
