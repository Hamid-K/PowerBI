using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Common.Utils.Boolean;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Linq;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x02000542 RID: 1346
	internal sealed class FunctionImportStructuralTypeMappingKB
	{
		// Token: 0x060041EF RID: 16879 RVA: 0x000DF498 File Offset: 0x000DD698
		internal FunctionImportStructuralTypeMappingKB(IEnumerable<FunctionImportStructuralTypeMapping> structuralTypeMappings, ItemCollection itemCollection)
		{
			this.m_itemCollection = itemCollection;
			if (structuralTypeMappings.Count<FunctionImportStructuralTypeMapping>() == 0)
			{
				this.ReturnTypeColumnsRenameMapping = new Dictionary<string, FunctionImportReturnTypeStructuralTypeColumnRenameMapping>();
				this.NormalizedEntityTypeMappings = new ReadOnlyCollection<FunctionImportNormalizedEntityTypeMapping>(new List<FunctionImportNormalizedEntityTypeMapping>());
				this.DiscriminatorColumns = new ReadOnlyCollection<string>(new List<string>());
				this.MappedEntityTypes = new ReadOnlyCollection<EntityType>(new List<EntityType>());
				return;
			}
			IEnumerable<FunctionImportEntityTypeMapping> enumerable = structuralTypeMappings.OfType<FunctionImportEntityTypeMapping>();
			if (enumerable != null && enumerable.FirstOrDefault<FunctionImportEntityTypeMapping>() != null)
			{
				Dictionary<EntityType, Collection<FunctionImportReturnTypePropertyMapping>> dictionary = new Dictionary<EntityType, Collection<FunctionImportReturnTypePropertyMapping>>();
				Dictionary<EntityType, Collection<FunctionImportReturnTypePropertyMapping>> dictionary2 = new Dictionary<EntityType, Collection<FunctionImportReturnTypePropertyMapping>>();
				List<FunctionImportNormalizedEntityTypeMapping> list = new List<FunctionImportNormalizedEntityTypeMapping>();
				this.MappedEntityTypes = new ReadOnlyCollection<EntityType>(enumerable.SelectMany((FunctionImportEntityTypeMapping mapping) => mapping.GetMappedEntityTypes(this.m_itemCollection)).Distinct<EntityType>().ToList<EntityType>());
				this.DiscriminatorColumns = new ReadOnlyCollection<string>(enumerable.SelectMany((FunctionImportEntityTypeMapping mapping) => mapping.GetDiscriminatorColumns()).Distinct<string>().ToList<string>());
				this.m_entityTypeLineInfos = new KeyToListMap<EntityType, LineInfo>(EqualityComparer<EntityType>.Default);
				this.m_isTypeOfLineInfos = new KeyToListMap<EntityType, LineInfo>(EqualityComparer<EntityType>.Default);
				foreach (FunctionImportEntityTypeMapping functionImportEntityTypeMapping in enumerable)
				{
					foreach (EntityType entityType in functionImportEntityTypeMapping.EntityTypes)
					{
						this.m_entityTypeLineInfos.Add(entityType, functionImportEntityTypeMapping.LineInfo);
					}
					foreach (EntityType entityType2 in functionImportEntityTypeMapping.IsOfTypeEntityTypes)
					{
						this.m_isTypeOfLineInfos.Add(entityType2, functionImportEntityTypeMapping.LineInfo);
					}
					Dictionary<string, FunctionImportEntityTypeMappingCondition> dictionary3 = functionImportEntityTypeMapping.Conditions.ToDictionary((FunctionImportEntityTypeMappingCondition condition) => condition.ColumnName, (FunctionImportEntityTypeMappingCondition condition) => condition);
					List<FunctionImportEntityTypeMappingCondition> list2 = new List<FunctionImportEntityTypeMappingCondition>(this.DiscriminatorColumns.Count);
					for (int i = 0; i < this.DiscriminatorColumns.Count; i++)
					{
						string text = this.DiscriminatorColumns[i];
						FunctionImportEntityTypeMappingCondition functionImportEntityTypeMappingCondition;
						if (dictionary3.TryGetValue(text, out functionImportEntityTypeMappingCondition))
						{
							list2.Add(functionImportEntityTypeMappingCondition);
						}
						else
						{
							list2.Add(null);
						}
					}
					bool[] array = new bool[this.MappedEntityTypes.Count];
					Set<EntityType> set = new Set<EntityType>(functionImportEntityTypeMapping.GetMappedEntityTypes(this.m_itemCollection));
					for (int j = 0; j < this.MappedEntityTypes.Count; j++)
					{
						array[j] = set.Contains(this.MappedEntityTypes[j]);
					}
					list.Add(new FunctionImportNormalizedEntityTypeMapping(this, list2, new BitArray(array)));
					foreach (EntityType entityType3 in functionImportEntityTypeMapping.IsOfTypeEntityTypes)
					{
						if (!dictionary.Keys.Contains(entityType3))
						{
							dictionary.Add(entityType3, new Collection<FunctionImportReturnTypePropertyMapping>());
						}
						foreach (FunctionImportReturnTypePropertyMapping functionImportReturnTypePropertyMapping in functionImportEntityTypeMapping.ColumnsRenameList)
						{
							dictionary[entityType3].Add(functionImportReturnTypePropertyMapping);
						}
					}
					foreach (EntityType entityType4 in functionImportEntityTypeMapping.EntityTypes)
					{
						if (!dictionary2.Keys.Contains(entityType4))
						{
							dictionary2.Add(entityType4, new Collection<FunctionImportReturnTypePropertyMapping>());
						}
						foreach (FunctionImportReturnTypePropertyMapping functionImportReturnTypePropertyMapping2 in functionImportEntityTypeMapping.ColumnsRenameList)
						{
							dictionary2[entityType4].Add(functionImportReturnTypePropertyMapping2);
						}
					}
				}
				this.ReturnTypeColumnsRenameMapping = new FunctionImportReturnTypeEntityTypeColumnsRenameBuilder(dictionary, dictionary2).ColumnRenameMapping;
				this.NormalizedEntityTypeMappings = new ReadOnlyCollection<FunctionImportNormalizedEntityTypeMapping>(list);
				return;
			}
			IEnumerable<FunctionImportComplexTypeMapping> enumerable2 = structuralTypeMappings.Cast<FunctionImportComplexTypeMapping>();
			this.ReturnTypeColumnsRenameMapping = new Dictionary<string, FunctionImportReturnTypeStructuralTypeColumnRenameMapping>();
			foreach (FunctionImportReturnTypePropertyMapping functionImportReturnTypePropertyMapping3 in enumerable2.First<FunctionImportComplexTypeMapping>().ColumnsRenameList)
			{
				FunctionImportReturnTypeStructuralTypeColumnRenameMapping functionImportReturnTypeStructuralTypeColumnRenameMapping = new FunctionImportReturnTypeStructuralTypeColumnRenameMapping(functionImportReturnTypePropertyMapping3.CMember);
				functionImportReturnTypeStructuralTypeColumnRenameMapping.AddRename(new FunctionImportReturnTypeStructuralTypeColumn(functionImportReturnTypePropertyMapping3.SColumn, enumerable2.First<FunctionImportComplexTypeMapping>().ReturnType, false, functionImportReturnTypePropertyMapping3.LineInfo));
				this.ReturnTypeColumnsRenameMapping.Add(functionImportReturnTypePropertyMapping3.CMember, functionImportReturnTypeStructuralTypeColumnRenameMapping);
			}
			this.NormalizedEntityTypeMappings = new ReadOnlyCollection<FunctionImportNormalizedEntityTypeMapping>(new List<FunctionImportNormalizedEntityTypeMapping>());
			this.DiscriminatorColumns = new ReadOnlyCollection<string>(new List<string>());
			this.MappedEntityTypes = new ReadOnlyCollection<EntityType>(new List<EntityType>());
		}

		// Token: 0x060041F0 RID: 16880 RVA: 0x000DFA28 File Offset: 0x000DDC28
		internal bool ValidateTypeConditions(bool validateAmbiguity, IList<EdmSchemaError> errors, string sourceLocation)
		{
			KeyToListMap<EntityType, LineInfo> keyToListMap;
			KeyToListMap<EntityType, LineInfo> keyToListMap2;
			this.GetUnreachableTypes(validateAmbiguity, out keyToListMap, out keyToListMap2);
			bool flag = true;
			foreach (KeyValuePair<EntityType, List<LineInfo>> keyValuePair in keyToListMap.KeyValuePairs)
			{
				LineInfo lineInfo = keyValuePair.Value.First<LineInfo>();
				string text = StringUtil.ToCommaSeparatedString(keyValuePair.Value.Select((LineInfo li) => li.LineNumber));
				EdmSchemaError edmSchemaError = new EdmSchemaError(Strings.Mapping_FunctionImport_UnreachableType(keyValuePair.Key.FullName, text), 2076, EdmSchemaErrorSeverity.Error, sourceLocation, lineInfo.LineNumber, lineInfo.LinePosition);
				errors.Add(edmSchemaError);
				flag = false;
			}
			foreach (KeyValuePair<EntityType, List<LineInfo>> keyValuePair2 in keyToListMap2.KeyValuePairs)
			{
				LineInfo lineInfo2 = keyValuePair2.Value.First<LineInfo>();
				string text2 = StringUtil.ToCommaSeparatedString(keyValuePair2.Value.Select((LineInfo li) => li.LineNumber));
				EdmSchemaError edmSchemaError2 = new EdmSchemaError(Strings.Mapping_FunctionImport_UnreachableIsTypeOf("IsTypeOf(" + keyValuePair2.Key.FullName + ")", text2), 2076, EdmSchemaErrorSeverity.Error, sourceLocation, lineInfo2.LineNumber, lineInfo2.LinePosition);
				errors.Add(edmSchemaError2);
				flag = false;
			}
			return flag;
		}

		// Token: 0x060041F1 RID: 16881 RVA: 0x000DFBC0 File Offset: 0x000DDDC0
		private void GetUnreachableTypes(bool validateAmbiguity, out KeyToListMap<EntityType, LineInfo> unreachableEntityTypes, out KeyToListMap<EntityType, LineInfo> unreachableIsTypeOfs)
		{
			DomainVariable<string, ValueCondition>[] array = this.ConstructDomainVariables();
			DomainConstraintConversionContext<string, ValueCondition> domainConstraintConversionContext = new DomainConstraintConversionContext<string, ValueCondition>();
			Vertex[] array2 = this.ConvertMappingConditionsToVertices(domainConstraintConversionContext, array);
			Set<EntityType> set = (validateAmbiguity ? this.FindUnambiguouslyReachableTypes(domainConstraintConversionContext, array2) : this.FindReachableTypes(domainConstraintConversionContext, array2));
			this.CollectUnreachableTypes(set, out unreachableEntityTypes, out unreachableIsTypeOfs);
		}

		// Token: 0x060041F2 RID: 16882 RVA: 0x000DFC04 File Offset: 0x000DDE04
		private DomainVariable<string, ValueCondition>[] ConstructDomainVariables()
		{
			Set<ValueCondition>[] array = new Set<ValueCondition>[this.DiscriminatorColumns.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new Set<ValueCondition>();
				array[i].Add(ValueCondition.IsOther);
				array[i].Add(ValueCondition.IsNull);
			}
			foreach (FunctionImportNormalizedEntityTypeMapping functionImportNormalizedEntityTypeMapping in this.NormalizedEntityTypeMappings)
			{
				for (int j = 0; j < this.DiscriminatorColumns.Count; j++)
				{
					FunctionImportEntityTypeMappingCondition functionImportEntityTypeMappingCondition = functionImportNormalizedEntityTypeMapping.ColumnConditions[j];
					if (functionImportEntityTypeMappingCondition != null && !functionImportEntityTypeMappingCondition.ConditionValue.IsNotNullCondition)
					{
						array[j].Add(functionImportEntityTypeMappingCondition.ConditionValue);
					}
				}
			}
			DomainVariable<string, ValueCondition>[] array2 = new DomainVariable<string, ValueCondition>[array.Length];
			for (int k = 0; k < array2.Length; k++)
			{
				array2[k] = new DomainVariable<string, ValueCondition>(this.DiscriminatorColumns[k], array[k].MakeReadOnly());
			}
			return array2;
		}

		// Token: 0x060041F3 RID: 16883 RVA: 0x000DFD14 File Offset: 0x000DDF14
		private Vertex[] ConvertMappingConditionsToVertices(ConversionContext<DomainConstraint<string, ValueCondition>> converter, DomainVariable<string, ValueCondition>[] variables)
		{
			Vertex[] array = new Vertex[this.NormalizedEntityTypeMappings.Count];
			for (int i = 0; i < array.Length; i++)
			{
				FunctionImportNormalizedEntityTypeMapping functionImportNormalizedEntityTypeMapping = this.NormalizedEntityTypeMappings[i];
				Vertex vertex = Vertex.One;
				for (int j = 0; j < this.DiscriminatorColumns.Count; j++)
				{
					FunctionImportEntityTypeMappingCondition functionImportEntityTypeMappingCondition = functionImportNormalizedEntityTypeMapping.ColumnConditions[j];
					if (functionImportEntityTypeMappingCondition != null)
					{
						ValueCondition conditionValue = functionImportEntityTypeMappingCondition.ConditionValue;
						if (conditionValue.IsNotNullCondition)
						{
							TermExpr<DomainConstraint<string, ValueCondition>> termExpr = new TermExpr<DomainConstraint<string, ValueCondition>>(new DomainConstraint<string, ValueCondition>(variables[j], ValueCondition.IsNull));
							Vertex vertex2 = converter.TranslateTermToVertex(termExpr);
							vertex = converter.Solver.And(vertex, converter.Solver.Not(vertex2));
						}
						else
						{
							TermExpr<DomainConstraint<string, ValueCondition>> termExpr2 = new TermExpr<DomainConstraint<string, ValueCondition>>(new DomainConstraint<string, ValueCondition>(variables[j], conditionValue));
							vertex = converter.Solver.And(vertex, converter.TranslateTermToVertex(termExpr2));
						}
					}
				}
				array[i] = vertex;
			}
			return array;
		}

		// Token: 0x060041F4 RID: 16884 RVA: 0x000DFE08 File Offset: 0x000DE008
		private Set<EntityType> FindReachableTypes(DomainConstraintConversionContext<string, ValueCondition> converter, Vertex[] mappingConditions)
		{
			Vertex[] array = new Vertex[this.MappedEntityTypes.Count];
			for (int k = 0; k < array.Length; k++)
			{
				Vertex vertex = Vertex.One;
				for (int j = 0; j < this.NormalizedEntityTypeMappings.Count; j++)
				{
					if (this.NormalizedEntityTypeMappings[j].ImpliedEntityTypes[k])
					{
						vertex = converter.Solver.And(vertex, mappingConditions[j]);
					}
					else
					{
						vertex = converter.Solver.And(vertex, converter.Solver.Not(mappingConditions[j]));
					}
				}
				array[k] = vertex;
			}
			Set<EntityType> set = new Set<EntityType>();
			int i;
			Func<Vertex, int, Vertex> <>9__0;
			int i2;
			for (i = 0; i < array.Length; i = i2 + 1)
			{
				Solver solver = converter.Solver;
				IEnumerable<Vertex> enumerable = array;
				Func<Vertex, int, Vertex> func;
				if ((func = <>9__0) == null)
				{
					func = (<>9__0 = delegate(Vertex typeCondition, int ordinal)
					{
						if (ordinal != i)
						{
							return converter.Solver.Not(typeCondition);
						}
						return typeCondition;
					});
				}
				if (!solver.And(enumerable.Select(func)).IsZero())
				{
					set.Add(this.MappedEntityTypes[i]);
				}
				i2 = i;
			}
			return set;
		}

		// Token: 0x060041F5 RID: 16885 RVA: 0x000DFF4C File Offset: 0x000DE14C
		private Set<EntityType> FindUnambiguouslyReachableTypes(DomainConstraintConversionContext<string, ValueCondition> converter, Vertex[] mappingConditions)
		{
			Vertex[] array = new Vertex[this.MappedEntityTypes.Count];
			for (int i = 0; i < array.Length; i++)
			{
				Vertex vertex = Vertex.One;
				for (int j = 0; j < this.NormalizedEntityTypeMappings.Count; j++)
				{
					if (this.NormalizedEntityTypeMappings[j].ImpliedEntityTypes[i])
					{
						vertex = converter.Solver.And(vertex, mappingConditions[j]);
					}
				}
				array[i] = vertex;
			}
			BitArray bitArray = new BitArray(array.Length, true);
			for (int k = 0; k < array.Length; k++)
			{
				if (array[k].IsZero())
				{
					bitArray[k] = false;
				}
				else
				{
					for (int l = k + 1; l < array.Length; l++)
					{
						if (!converter.Solver.And(array[k], array[l]).IsZero())
						{
							bitArray[k] = false;
							bitArray[l] = false;
						}
					}
				}
			}
			Set<EntityType> set = new Set<EntityType>();
			for (int m = 0; m < array.Length; m++)
			{
				if (bitArray[m])
				{
					set.Add(this.MappedEntityTypes[m]);
				}
			}
			return set;
		}

		// Token: 0x060041F6 RID: 16886 RVA: 0x000E0074 File Offset: 0x000DE274
		private void CollectUnreachableTypes(Set<EntityType> reachableTypes, out KeyToListMap<EntityType, LineInfo> entityTypes, out KeyToListMap<EntityType, LineInfo> isTypeOfEntityTypes)
		{
			entityTypes = new KeyToListMap<EntityType, LineInfo>(EqualityComparer<EntityType>.Default);
			isTypeOfEntityTypes = new KeyToListMap<EntityType, LineInfo>(EqualityComparer<EntityType>.Default);
			if (reachableTypes.Count == this.MappedEntityTypes.Count)
			{
				return;
			}
			foreach (EntityType entityType in this.m_isTypeOfLineInfos.Keys)
			{
				if (!MetadataHelper.GetTypeAndSubtypesOf(entityType, this.m_itemCollection, false).Cast<EntityType>().Intersect(reachableTypes)
					.Any<EntityType>())
				{
					isTypeOfEntityTypes.AddRange(entityType, this.m_isTypeOfLineInfos.EnumerateValues(entityType));
				}
			}
			foreach (EntityType entityType2 in this.m_entityTypeLineInfos.Keys)
			{
				if (!reachableTypes.Contains(entityType2))
				{
					entityTypes.AddRange(entityType2, this.m_entityTypeLineInfos.EnumerateValues(entityType2));
				}
			}
		}

		// Token: 0x040016E5 RID: 5861
		private readonly ItemCollection m_itemCollection;

		// Token: 0x040016E6 RID: 5862
		private readonly KeyToListMap<EntityType, LineInfo> m_entityTypeLineInfos;

		// Token: 0x040016E7 RID: 5863
		private readonly KeyToListMap<EntityType, LineInfo> m_isTypeOfLineInfos;

		// Token: 0x040016E8 RID: 5864
		internal readonly ReadOnlyCollection<EntityType> MappedEntityTypes;

		// Token: 0x040016E9 RID: 5865
		internal readonly ReadOnlyCollection<string> DiscriminatorColumns;

		// Token: 0x040016EA RID: 5866
		internal readonly ReadOnlyCollection<FunctionImportNormalizedEntityTypeMapping> NormalizedEntityTypeMappings;

		// Token: 0x040016EB RID: 5867
		internal readonly Dictionary<string, FunctionImportReturnTypeStructuralTypeColumnRenameMapping> ReturnTypeColumnsRenameMapping;
	}
}
