using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.DataShaping;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Data.Contracts;
using Microsoft.Reporting.QueryDesign.Edm.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.ExpressionBuilder.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;
using Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal;

namespace Microsoft.Reporting.QueryDesign.BatchQueries
{
	// Token: 0x0200025F RID: 607
	internal sealed class BatchQueryConstraintValidator
	{
		// Token: 0x06001A5B RID: 6747 RVA: 0x0004860C File Offset: 0x0004680C
		internal BatchQueryConstraintValidator(IConceptualModel conceptualModel, IConceptualSchema schema, QueryNamingContext namingContext, IReadOnlyList<EntitySet> groupEntitySets, IReadOnlyList<IConceptualEntity> groupEntities, IReadOnlyList<GroupAndJoinMeasure> measures, [global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "Expression", "ShouldCrossFilterGroupColumns" })] IReadOnlyList<global::System.ValueTuple<QueryExpression, bool>> contextTables, IReadOnlyList<QueryExistsFilter> existsFilters, ExpressionReferenceNameToTableMapping referenceNameMapping, bool includeDirectManyToManyAssociations, bool useConceptualSchema)
		{
			this._conceptualModel = conceptualModel;
			this._schema = schema;
			this._includeDirectManyToManyAssociations = includeDirectManyToManyAssociations;
			this._namingContext = namingContext;
			this._groupEntitySets = groupEntitySets;
			this._groupEntities = groupEntities;
			this._measures = measures;
			this._contextTables = contextTables;
			this._referenceNameMapping = referenceNameMapping;
			this._useConceptualSchema = useConceptualSchema;
			this.Telemetry = new BatchQueryConstraintTelemetry();
			if (useConceptualSchema)
			{
				this._crossFilteredEntitiesByCardinalityOnly = QueryAlgorithms.DetectCrossFilteredEntitiesByCardinalityOnly(this._groupEntities, this._schema, this._includeDirectManyToManyAssociations);
			}
			else
			{
				this._crossFilteredEntitySetsByCardinalityOnly = QueryAlgorithms.DetectCrossFilteredEntitySetsByCardinalityOnly(this._groupEntitySets, this._conceptualModel, this._includeDirectManyToManyAssociations);
			}
			if (measures.Count == 0)
			{
				this._existsFiltersRelatedToGroups = false;
				this._existsFilterTargetCrossFilteredEntitySets = new HashSet<EntitySet>();
				this._existsFilterTargetCrossFilteredEntities = new HashSet<IConceptualEntity>(ConceptualEntityExtensionAwareEqualityComparer.Instance);
				return;
			}
			if (this._useConceptualSchema)
			{
				IList<IConceptualEntity> list = existsFilters.SelectMany((QueryExistsFilter f) => f.TargetEntities).Evaluate<IConceptualEntity>();
				this._crossFilteredEntities = QueryAlgorithms.DetectCrossFilteredEntities(this._groupEntities, this._schema, this._includeDirectManyToManyAssociations);
				this._existsFiltersRelatedToGroups = list.Intersect(this._crossFilteredEntities.CompleteSet, ConceptualEntityExtensionAwareEqualityComparer.Instance).Any<IConceptualEntity>();
				this._existsFilterTargetCrossFilteredEntities = this.DetectEntitiesCrossFilteredByExistsFilter(list, this._schema);
				return;
			}
			IList<EntitySet> list2 = existsFilters.SelectMany((QueryExistsFilter f) => f.TargetEntitySets).Evaluate<EntitySet>();
			this._crossFilteredEntitySets = QueryAlgorithms.DetectCrossFilteredEntities(this._groupEntitySets, this._conceptualModel, this._includeDirectManyToManyAssociations);
			this._existsFiltersRelatedToGroups = list2.Intersect(this._crossFilteredEntitySets.CompleteSet).Any<EntitySet>();
			this._existsFilterTargetCrossFilteredEntitySets = this.DetectEntitySetsCrossFilteredByExistsFilter(list2, this._conceptualModel);
		}

		// Token: 0x17000781 RID: 1921
		// (get) Token: 0x06001A5C RID: 6748 RVA: 0x000487E1 File Offset: 0x000469E1
		internal BatchQueryConstraintTelemetry Telemetry { get; }

		// Token: 0x17000782 RID: 1922
		// (get) Token: 0x06001A5D RID: 6749 RVA: 0x000487EC File Offset: 0x000469EC
		private bool GroupsCannotBeRelated
		{
			get
			{
				if (!this._useConceptualSchema)
				{
					return this._crossFilteredEntitySetsByCardinalityOnly.IsEmpty && this._conceptualModel.DefaultMeasure == null;
				}
				return this._crossFilteredEntitiesByCardinalityOnly.IsEmpty && this._schema.DefaultMeasure == null;
			}
		}

		// Token: 0x17000783 RID: 1923
		// (get) Token: 0x06001A5E RID: 6750 RVA: 0x00048844 File Offset: 0x00046A44
		private ReadOnlyCollection<EntitySet> EntitySetsToRelatedGroupsInQuery
		{
			get
			{
				return this._crossFilteredEntitySetsByCardinalityOnly.PrunedSet;
			}
		}

		// Token: 0x17000784 RID: 1924
		// (get) Token: 0x06001A5F RID: 6751 RVA: 0x00048851 File Offset: 0x00046A51
		private IReadOnlyList<IConceptualEntity> EntitiesToRelatedGroupsInQuery
		{
			get
			{
				return this._crossFilteredEntitiesByCardinalityOnly.PrunedSet;
			}
		}

		// Token: 0x06001A60 RID: 6752 RVA: 0x0004885E File Offset: 0x00046A5E
		private int GetMeasuresCrossFilteredByGroupsInQuerySetDifference(IEnumerable<EntitySet> measureEntities)
		{
			return measureEntities.Except(this._crossFilteredEntitySets.CompleteSet).Count<EntitySet>();
		}

		// Token: 0x06001A61 RID: 6753 RVA: 0x00048876 File Offset: 0x00046A76
		private int GetMeasuresCrossFilteredByGroupsInQuerySetDifference(IEnumerable<IConceptualEntity> measureEntities)
		{
			return measureEntities.Except(this._crossFilteredEntities.CompleteSet, ConceptualEntityExtensionAwareEqualityComparer.Instance).Count<IConceptualEntity>();
		}

		// Token: 0x06001A62 RID: 6754 RVA: 0x00048894 File Offset: 0x00046A94
		private HashSet<EntitySet> DetectEntitySetsCrossFilteredByExistsFilter(IEnumerable<EntitySet> existsTargets, IConceptualModel conceptualModel)
		{
			HashSet<EntitySet> hashSet = new HashSet<EntitySet>();
			foreach (EntitySet entitySet in existsTargets)
			{
				foreach (EntitySet entitySet2 in QueryAlgorithms.DetectCrossFilteredEntities(Microsoft.Reporting.Util.AsEnumerable<EntitySet>(entitySet), conceptualModel, this._includeDirectManyToManyAssociations).CompleteSet)
				{
					hashSet.Add(entitySet2);
				}
			}
			return hashSet;
		}

		// Token: 0x06001A63 RID: 6755 RVA: 0x00048928 File Offset: 0x00046B28
		private HashSet<IConceptualEntity> DetectEntitiesCrossFilteredByExistsFilter(IEnumerable<IConceptualEntity> existsTargets, IConceptualSchema schema)
		{
			HashSet<IConceptualEntity> hashSet = new HashSet<IConceptualEntity>(ConceptualEntityExtensionAwareEqualityComparer.Instance);
			foreach (IConceptualEntity conceptualEntity in existsTargets)
			{
				foreach (IConceptualEntity conceptualEntity2 in QueryAlgorithms.DetectCrossFilteredEntities(Microsoft.Reporting.Util.AsEnumerable<IConceptualEntity>(conceptualEntity), schema, this._includeDirectManyToManyAssociations).CompleteSet)
				{
					hashSet.Add(conceptualEntity2);
				}
			}
			return hashSet;
		}

		// Token: 0x06001A64 RID: 6756 RVA: 0x000489C4 File Offset: 0x00046BC4
		internal static IReadOnlyList<GroupAndJoinMeasure> TryAddExistsPredicates(QueryNamingContext namingContext, IReadOnlyList<GroupAndJoinMeasure> measures, IReadOnlyList<QueryExistsFilter> existsFilters, bool useConceptualSchema)
		{
			if (measures.Count > 0 || existsFilters.Count == 0)
			{
				return measures;
			}
			return BatchQueryConstraintValidatorAlgorithms.CreatePredicatesFromExistsFilters(existsFilters, namingContext, useConceptualSchema).ToList<GroupAndJoinMeasure>();
		}

		// Token: 0x06001A65 RID: 6757 RVA: 0x000489E8 File Offset: 0x00046BE8
		internal IEnumerable<GroupAndJoinMeasure> DetermineNonEmptyBehavior(out QueryExpression extraNonEmptyFilter, out bool hasUnconstrainedJoin)
		{
			List<int> list = new List<int>();
			List<int> list2 = new List<int>();
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < this._measures.Count; i++)
			{
				GroupAndJoinMeasure groupAndJoinMeasure = this._measures[i];
				if (!groupAndJoinMeasure.SuppressJoinPredicate)
				{
					if (this._useConceptualSchema)
					{
						List<IConceptualEntity> list3 = Measure.FindAnchoredEntityReferences(groupAndJoinMeasure.Column.Expression).ToList<IConceptualEntity>();
						int measuresCrossFilteredByGroupsInQuerySetDifference = this.GetMeasuresCrossFilteredByGroupsInQuerySetDifference(list3);
						if (measuresCrossFilteredByGroupsInQuerySetDifference == 0 || (this._existsFiltersRelatedToGroups && this._existsFilterTargetCrossFilteredEntities.IsSupersetOf(list3)))
						{
							list.Add(i);
						}
						else
						{
							if (measuresCrossFilteredByGroupsInQuerySetDifference != list3.Count || (this._existsFiltersRelatedToGroups && this._existsFilterTargetCrossFilteredEntities.Overlaps(list3)))
							{
								num++;
							}
							else
							{
								num2++;
							}
							list2.Add(i);
						}
					}
					else
					{
						List<EntitySet> list4 = Measure.FindAnchoredEntitySetReferences(groupAndJoinMeasure.Column.Expression).ToList<EntitySet>();
						int measuresCrossFilteredByGroupsInQuerySetDifference2 = this.GetMeasuresCrossFilteredByGroupsInQuerySetDifference(list4);
						if (measuresCrossFilteredByGroupsInQuerySetDifference2 == 0 || (this._existsFiltersRelatedToGroups && this._existsFilterTargetCrossFilteredEntitySets.IsSupersetOf(list4)))
						{
							list.Add(i);
						}
						else
						{
							if (measuresCrossFilteredByGroupsInQuerySetDifference2 != list4.Count || (this._existsFiltersRelatedToGroups && this._existsFilterTargetCrossFilteredEntitySets.Overlaps(list4)))
							{
								num++;
							}
							else
							{
								num2++;
							}
							list2.Add(i);
						}
					}
				}
			}
			this.Telemetry.TotalMeasureCount = this._measures.Count;
			this.Telemetry.RelatedMeasureCount = list.Count;
			this.Telemetry.PartiallyRelatedMeasureCount = num;
			this.Telemetry.UnrelatedMeasureCount = num2;
			this.Telemetry.GroupEntityCount = (this._useConceptualSchema ? this._groupEntities.Count : this._groupEntitySets.Count);
			if (this._useConceptualSchema ? (this._groupEntities.Count < 2) : (this._groupEntitySets.Count < 2))
			{
				hasUnconstrainedJoin = false;
				if (this._contextTables.Count > 0)
				{
					extraNonEmptyFilter = null;
					IReadOnlyList<GroupAndJoinMeasure> readOnlyList = this._measures;
					if (list.Count <= 0)
					{
						readOnlyList = BatchQueryConstraintValidatorAlgorithms.DeterminePredicatesFromContextTables(this._groupEntitySets, this._groupEntities, this._contextTables, readOnlyList, this._conceptualModel, this._schema, this._includeDirectManyToManyAssociations, this._referenceNameMapping, this._namingContext, this._useConceptualSchema);
					}
					return BatchQueryConstraintValidator.IgnoreUnrelatedMeasures(readOnlyList, list2, this.Telemetry);
				}
				extraNonEmptyFilter = null;
				return this._measures;
			}
			else
			{
				if (this.GroupsCannotBeRelated)
				{
					return this.DeterminePredicatesForUnrelatedGroups(list, list2, out extraNonEmptyFilter, out hasUnconstrainedJoin);
				}
				if (list2.Count > 0)
				{
					return this.DeterminePredicatesForUnrelatedMeasures(list, out extraNonEmptyFilter, out hasUnconstrainedJoin);
				}
				if (list.Count > 0)
				{
					extraNonEmptyFilter = null;
					hasUnconstrainedJoin = false;
					return this._measures;
				}
				hasUnconstrainedJoin = false;
				extraNonEmptyFilter = null;
				IEnumerable<GroupAndJoinMeasure> enumerable;
				if (this._useConceptualSchema)
				{
					IConceptualMeasure defaultMeasure = this._schema.DefaultMeasure;
					if (defaultMeasure != null)
					{
						enumerable = BatchQueryConstraintValidatorAlgorithms.CreateDefaultMeasureColumnInlineFiltering(defaultMeasure, this._namingContext).AsReadOnlyList<GroupAndJoinMeasure>();
					}
					else
					{
						enumerable = BatchQueryConstraintValidatorAlgorithms.CreateBaseEntityPredicateColumnsInlineFiltering(null, this.EntitiesToRelatedGroupsInQuery, this._namingContext, this._schema.DiscourageCountRowsOverTables(), this._useConceptualSchema);
					}
				}
				else
				{
					EdmMeasureInstance? defaultMeasure2 = this._conceptualModel.DefaultMeasure;
					if (defaultMeasure2 != null)
					{
						enumerable = BatchQueryConstraintValidatorAlgorithms.CreateDefaultMeasureColumnInlineFiltering(defaultMeasure2.Value, this._namingContext).AsReadOnlyList<GroupAndJoinMeasure>();
					}
					else
					{
						enumerable = BatchQueryConstraintValidatorAlgorithms.CreateBaseEntityPredicateColumnsInlineFiltering(this.EntitySetsToRelatedGroupsInQuery, null, this._namingContext, this._conceptualModel.DiscourageCountRowsOverTables(), this._useConceptualSchema);
					}
				}
				return this._measures.Concat(enumerable);
			}
		}

		// Token: 0x06001A66 RID: 6758 RVA: 0x00048D64 File Offset: 0x00046F64
		private IEnumerable<GroupAndJoinMeasure> DeterminePredicatesForUnrelatedMeasures(List<int> relatedMeasureIndexes, out QueryExpression extraNonEmptyFilter, out bool hasUnconstrainedJoin)
		{
			IEnumerable<GroupAndJoinMeasure> enumerable;
			if (this._useConceptualSchema)
			{
				IConceptualMeasure defaultMeasure = this._schema.DefaultMeasure;
				if (defaultMeasure != null)
				{
					enumerable = new GroupAndJoinMeasure[] { BatchQueryConstraintValidatorAlgorithms.CreateDefaultMeasureColumn(defaultMeasure, this._namingContext) };
				}
				else
				{
					enumerable = BatchQueryConstraintValidatorAlgorithms.CreateBaseEntityPredicateColumns(null, this.EntitiesToRelatedGroupsInQuery, this._namingContext, this._useConceptualSchema);
				}
			}
			else
			{
				EdmMeasureInstance? defaultMeasure2 = this._conceptualModel.DefaultMeasure;
				if (defaultMeasure2 != null)
				{
					enumerable = new GroupAndJoinMeasure[] { BatchQueryConstraintValidatorAlgorithms.CreateDefaultMeasureColumn(defaultMeasure2.Value, this._namingContext) };
				}
				else
				{
					enumerable = BatchQueryConstraintValidatorAlgorithms.CreateBaseEntityPredicateColumns(this.EntitySetsToRelatedGroupsInQuery, null, this._namingContext, this._useConceptualSchema);
				}
			}
			QueryExpression queryExpression = enumerable.Select((GroupAndJoinMeasure c) => BatchQueryConstraintValidatorAlgorithms.CreateIsTruePredicate(c)).OrAll();
			extraNonEmptyFilter = queryExpression;
			if (relatedMeasureIndexes.Count > 0)
			{
				QueryExpression queryExpression2 = relatedMeasureIndexes.Select((int i) => BatchQueryConstraintValidatorAlgorithms.CreateNonEmptyPredicate(this._measures[i])).OrAll();
				extraNonEmptyFilter = queryExpression2.Or(extraNonEmptyFilter);
			}
			hasUnconstrainedJoin = false;
			return this._measures.Concat(enumerable);
		}

		// Token: 0x06001A67 RID: 6759 RVA: 0x00048E74 File Offset: 0x00047074
		private IEnumerable<GroupAndJoinMeasure> DeterminePredicatesForUnrelatedGroups(List<int> relatedMeasureIndexes, List<int> unrelatedMeasureIndexes, out QueryExpression extraNonEmptyFilter, out bool hasUnconstrainedJoin)
		{
			extraNonEmptyFilter = null;
			this.Telemetry.HasUnrelatedGroups = true;
			bool flag = relatedMeasureIndexes.Count > 0;
			bool flag2 = unrelatedMeasureIndexes.Count > 0;
			IEnumerable<GroupAndJoinMeasure> enumerable = this._measures;
			if (flag2)
			{
				enumerable = BatchQueryConstraintValidator.IgnoreUnrelatedMeasures(this._measures, unrelatedMeasureIndexes, this.Telemetry);
			}
			else if (this._contextTables.Count > 0 && !flag)
			{
				enumerable = BatchQueryConstraintValidatorAlgorithms.DeterminePredicatesFromContextTables(this._groupEntitySets, this._groupEntities, this._contextTables, this._measures, this._conceptualModel, this._schema, this._includeDirectManyToManyAssociations, this._referenceNameMapping, this._namingContext, this._useConceptualSchema);
			}
			hasUnconstrainedJoin = !flag;
			return enumerable;
		}

		// Token: 0x06001A68 RID: 6760 RVA: 0x00048F1C File Offset: 0x0004711C
		private static IReadOnlyList<GroupAndJoinMeasure> IgnoreUnrelatedMeasures(IReadOnlyList<GroupAndJoinMeasure> measures, List<int> unrelatedMeasureIndexes, BatchQueryConstraintTelemetry telemetry)
		{
			if (unrelatedMeasureIndexes.Count == 0)
			{
				return measures;
			}
			List<GroupAndJoinMeasure> list = new List<GroupAndJoinMeasure>(measures.Count);
			for (int i = 0; i < measures.Count; i++)
			{
				GroupAndJoinMeasure groupAndJoinMeasure = measures[i];
				GroupAndJoinMeasure groupAndJoinMeasure2 = groupAndJoinMeasure;
				if (unrelatedMeasureIndexes.Contains(i))
				{
					groupAndJoinMeasure2 = new GroupAndJoinMeasure(groupAndJoinMeasure.Column, true);
					telemetry.UnrelatedMeasuresIgnored = true;
				}
				list.Add(groupAndJoinMeasure2);
			}
			return list;
		}

		// Token: 0x04000E92 RID: 3730
		private readonly IConceptualModel _conceptualModel;

		// Token: 0x04000E93 RID: 3731
		private readonly IConceptualSchema _schema;

		// Token: 0x04000E94 RID: 3732
		private readonly QueryNamingContext _namingContext;

		// Token: 0x04000E95 RID: 3733
		private readonly IReadOnlyList<EntitySet> _groupEntitySets;

		// Token: 0x04000E96 RID: 3734
		private readonly IReadOnlyList<IConceptualEntity> _groupEntities;

		// Token: 0x04000E97 RID: 3735
		private readonly IReadOnlyList<GroupAndJoinMeasure> _measures;

		// Token: 0x04000E98 RID: 3736
		[global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "Expression", "ShouldCrossFilterGroupColumns" })]
		private readonly IReadOnlyList<global::System.ValueTuple<QueryExpression, bool>> _contextTables;

		// Token: 0x04000E99 RID: 3737
		private readonly BaseEntitySets _crossFilteredEntitySetsByCardinalityOnly;

		// Token: 0x04000E9A RID: 3738
		private readonly BaseConceptualEntities _crossFilteredEntitiesByCardinalityOnly;

		// Token: 0x04000E9B RID: 3739
		private readonly BaseEntitySets _crossFilteredEntitySets;

		// Token: 0x04000E9C RID: 3740
		private readonly BaseConceptualEntities _crossFilteredEntities;

		// Token: 0x04000E9D RID: 3741
		private readonly ExpressionReferenceNameToTableMapping _referenceNameMapping;

		// Token: 0x04000E9E RID: 3742
		private readonly HashSet<EntitySet> _existsFilterTargetCrossFilteredEntitySets;

		// Token: 0x04000E9F RID: 3743
		private readonly HashSet<IConceptualEntity> _existsFilterTargetCrossFilteredEntities;

		// Token: 0x04000EA0 RID: 3744
		private readonly bool _existsFiltersRelatedToGroups;

		// Token: 0x04000EA1 RID: 3745
		private readonly bool _includeDirectManyToManyAssociations;

		// Token: 0x04000EA2 RID: 3746
		private readonly bool _useConceptualSchema;
	}
}
