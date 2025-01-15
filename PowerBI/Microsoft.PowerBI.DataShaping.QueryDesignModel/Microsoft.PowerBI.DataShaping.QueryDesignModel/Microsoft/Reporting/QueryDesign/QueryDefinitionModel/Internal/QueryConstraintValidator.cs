using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.DataShaping;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Data.Contracts;
using Microsoft.Reporting.QueryDesign.Edm.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.ExpressionBuilder.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;

namespace Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal
{
	// Token: 0x0200010C RID: 268
	internal sealed class QueryConstraintValidator
	{
		// Token: 0x06000F8D RID: 3981 RVA: 0x0002AEE4 File Offset: 0x000290E4
		internal QueryConstraintValidator(EntityDataModel entityDataModel, IConceptualSchema schema, IEnumerable<Group> groups, IEnumerable<Measure> measures, IEnumerable<IJoinPredicate> explicitJoinPredicates, bool crossJoinQueryPlan, bool includeDirectManyToManyAssociations, bool useConceptualSchema)
		{
			this._entityDataModel = entityDataModel;
			this._schema = schema;
			this._groupEntitySets = (useConceptualSchema ? null : QueryConstraintValidator.FindAllGroupEntitySetReferences(groups).ToReadOnlyCollection<EntitySet>());
			this._groupEntities = (useConceptualSchema ? QueryConstraintValidator.FindAllGroupEntityReferences(groups).ToList<IConceptualEntity>() : null);
			this._measures = measures.ToReadOnlyCollection<Measure>();
			this._explicitJoinPredicates = ((explicitJoinPredicates != null) ? explicitJoinPredicates.ToReadOnlyCollection<IJoinPredicate>() : null);
			this._multipleGroups = groups.EmptyIfNull<Group>().Count<Group>() > 1;
			this._crossJoinQueryPlan = crossJoinQueryPlan;
			this._includeDirectManyToManyAssociations = includeDirectManyToManyAssociations;
			this._useConceptualSchema = useConceptualSchema;
			if (useConceptualSchema)
			{
				this._crossFilteredEntitiesByCardinalityOnly = QueryAlgorithms.DetectCrossFilteredEntitiesByCardinalityOnly(this._groupEntities, this._schema, this._includeDirectManyToManyAssociations);
				this._crossFilteredEntities = QueryAlgorithms.DetectCrossFilteredEntities(this._groupEntities, this._schema, this._includeDirectManyToManyAssociations);
				return;
			}
			this._crossFilteredEntitySetsByCardinalityOnly = QueryAlgorithms.DetectCrossFilteredEntitySetsByCardinalityOnly(this._groupEntitySets, this._entityDataModel, this._includeDirectManyToManyAssociations);
			this._crossFilteredEntitySets = QueryAlgorithms.DetectCrossFilteredEntitySets(this._groupEntitySets, this._entityDataModel, this._includeDirectManyToManyAssociations);
		}

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x06000F8E RID: 3982 RVA: 0x0002AFFB File Offset: 0x000291FB
		internal ReadOnlyCollection<EntitySet> GroupEntitySets
		{
			get
			{
				return this._groupEntitySets;
			}
		}

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x06000F8F RID: 3983 RVA: 0x0002B003 File Offset: 0x00029203
		internal IReadOnlyList<IConceptualEntity> GroupEntities
		{
			get
			{
				return this._groupEntities;
			}
		}

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x06000F90 RID: 3984 RVA: 0x0002B00B File Offset: 0x0002920B
		internal bool HasJoin
		{
			get
			{
				if (!this._useConceptualSchema)
				{
					return this._groupEntitySets.Count > 1;
				}
				return this._groupEntities.Count > 1;
			}
		}

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x06000F91 RID: 3985 RVA: 0x0002B032 File Offset: 0x00029232
		internal bool MultipleGroups
		{
			get
			{
				return this._multipleGroups;
			}
		}

		// Token: 0x06000F92 RID: 3986 RVA: 0x0002B03C File Offset: 0x0002923C
		internal static HashSet<EntitySet> FindAllGroupEntitySetReferences(IEnumerable<Group> groups)
		{
			return (from g in groups.EmptyIfNull<Group>()
				from k in g.Keys
				from entity in k.Expression.FindEntitySetReferences(QdmExpressionBuilder.EntityRefSearchBehavior.All)
				select entity).ToSet<EntitySet>();
		}

		// Token: 0x06000F93 RID: 3987 RVA: 0x0002B0DC File Offset: 0x000292DC
		internal static HashSet<IConceptualEntity> FindAllGroupEntityReferences(IEnumerable<Group> groups)
		{
			return (from g in groups.EmptyIfNull<Group>()
				from k in g.Keys
				from entity in k.Expression.FindEntityReferences(QdmExpressionBuilder.EntityRefSearchBehavior.All)
				select entity).ToSet(ConceptualEntityExtensionAwareEqualityComparer.Instance);
		}

		// Token: 0x06000F94 RID: 3988 RVA: 0x0002B17F File Offset: 0x0002937F
		internal bool HasUnconstrainedJoin()
		{
			return this.HasJoin && !this.GetJoinPredicates().Any<IJoinPredicate>();
		}

		// Token: 0x06000F95 RID: 3989 RVA: 0x0002B199 File Offset: 0x00029399
		internal bool HasUnconstrainedMeasures()
		{
			return this._measures.Any((Measure m) => this.IsMeasureExpressionUnconstrained(m.Expression));
		}

		// Token: 0x06000F96 RID: 3990 RVA: 0x0002B1B4 File Offset: 0x000293B4
		internal bool HasUnconstrainedMeasuresOrUnanchoredExplicitJoinPredicates()
		{
			if (this.HasUnconstrainedMeasures())
			{
				return true;
			}
			if (this._explicitJoinPredicates != null)
			{
				if (this._explicitJoinPredicates.Any((IJoinPredicate jp) => !jp.IsAnchored))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000F97 RID: 3991 RVA: 0x0002B202 File Offset: 0x00029402
		internal bool IsMeasureExpressionUnconstrained(QueryExpression measureExpression)
		{
			return !Measure.IsMeasureExpressionAnchored(measureExpression) || !this.IsMeasureCrossFilteredByGroupsInQuery(measureExpression);
		}

		// Token: 0x06000F98 RID: 3992 RVA: 0x0002B218 File Offset: 0x00029418
		internal bool IsMeasureCrossFilteredByGroupsInQuery(QueryExpression measureExpression)
		{
			if (!this._useConceptualSchema)
			{
				return Measure.IsMeasureCrossFilteredByGroupsInQuery(measureExpression, this._crossFilteredEntitySets);
			}
			return Measure.IsMeasureCrossFilteredByGroupsInQuery(measureExpression, this._crossFilteredEntities);
		}

		// Token: 0x06000F99 RID: 3993 RVA: 0x0002B23C File Offset: 0x0002943C
		internal IReadOnlyList<IJoinPredicate> GetJoinPredicates()
		{
			if (this._explicitJoinPredicates != null)
			{
				return this._explicitJoinPredicates;
			}
			if (this._measures.Any<Measure>())
			{
				if (this._useConceptualSchema ? (!this._groupEntities.Any<IConceptualEntity>()) : (!this._groupEntitySets.Any<EntitySet>()))
				{
					return this._measures.OfType<IJoinPredicate>().ToList<IJoinPredicate>();
				}
				List<IJoinPredicate> list = new List<IJoinPredicate>();
				List<IJoinPredicate> list2 = new List<IJoinPredicate>();
				foreach (Measure measure in this._measures)
				{
					IJoinPredicate joinPredicate = measure as IJoinPredicate;
					if (joinPredicate != null)
					{
						if (this.IsMeasureCrossFilteredByGroupsInQuery(measure.Expression))
						{
							list.Add(joinPredicate);
						}
						else
						{
							list2.Add(joinPredicate);
						}
					}
				}
				IReadOnlyList<IJoinPredicate> readOnlyList = list;
				if (list2.Count > 0 && this.AnyDefaultPredicate())
				{
					IReadOnlyList<IJoinPredicate> readOnlyList2 = this.CreateDefaultPredicates();
					if (readOnlyList2.Count > 0)
					{
						QueryExpression queryExpression = readOnlyList2.ToPredicateExpressions().OrAll();
						QueryExpression queryExpression2 = list2.ToPredicateExpressions().OrAll();
						QueryOperatorExpression queryOperatorExpression = queryExpression.And(queryExpression2);
						ExpressionJoinPredicate expressionJoinPredicate = new ExpressionJoinPredicate(queryOperatorExpression, Measure.IsMeasureExpressionAnchored(queryOperatorExpression));
						readOnlyList = readOnlyList.Concat(new IJoinPredicate[] { expressionJoinPredicate }).ToList<IJoinPredicate>();
					}
					else
					{
						readOnlyList = readOnlyList.Concat(list2).ToList<IJoinPredicate>();
					}
				}
				return readOnlyList;
			}
			else
			{
				if (this.AnyDefaultPredicate())
				{
					return this.CreateDefaultPredicates();
				}
				return Microsoft.DataShaping.Util.EmptyReadOnlyList<IJoinPredicate>();
			}
		}

		// Token: 0x06000F9A RID: 3994 RVA: 0x0002B3A8 File Offset: 0x000295A8
		private bool AnyDefaultPredicate()
		{
			if (!this._useConceptualSchema)
			{
				return this._entityDataModel.DefaultMeasure != null || !this._crossFilteredEntitySetsByCardinalityOnly.IsEmpty;
			}
			return this._schema.DefaultMeasure != null || !this._crossFilteredEntitiesByCardinalityOnly.IsEmpty;
		}

		// Token: 0x06000F9B RID: 3995 RVA: 0x0002B400 File Offset: 0x00029600
		private IReadOnlyList<IJoinPredicate> CreateDefaultPredicates()
		{
			if (this._useConceptualSchema)
			{
				IConceptualMeasure defaultMeasure = this._schema.DefaultMeasure;
				if (defaultMeasure != null)
				{
					return new ExpressionJoinPredicate(defaultMeasure.InvokeMeasure().IsNull().Not(), false).AsReadOnlyList<ExpressionJoinPredicate>();
				}
				if (this.GroupEntities.Count > 1 || (this.GroupEntities.Count == 1 && this._multipleGroups && this._crossJoinQueryPlan))
				{
					return this._crossFilteredEntitiesByCardinalityOnly.PrunedSet.Select((IConceptualEntity e) => new EntityJoinPredicate(null, e)).ToList<EntityJoinPredicate>();
				}
			}
			else
			{
				EdmMeasureInstance? defaultMeasure2 = this._entityDataModel.DefaultMeasure;
				if (defaultMeasure2 != null)
				{
					return new ExpressionJoinPredicate(defaultMeasure2.Value.InvokeMeasure(null).IsNull().Not(), false).AsReadOnlyList<ExpressionJoinPredicate>();
				}
				if (this.GroupEntitySets.Count > 1 || (this.GroupEntitySets.Count == 1 && this._multipleGroups && this._crossJoinQueryPlan))
				{
					return this._crossFilteredEntitySetsByCardinalityOnly.PrunedSet.Select((EntitySet e) => new EntityJoinPredicate(e, null)).ToList<EntityJoinPredicate>();
				}
			}
			return Microsoft.DataShaping.Util.EmptyReadOnlyList<IJoinPredicate>();
		}

		// Token: 0x04000A12 RID: 2578
		private readonly EntityDataModel _entityDataModel;

		// Token: 0x04000A13 RID: 2579
		private readonly IConceptualSchema _schema;

		// Token: 0x04000A14 RID: 2580
		private readonly ReadOnlyCollection<EntitySet> _groupEntitySets;

		// Token: 0x04000A15 RID: 2581
		private readonly IReadOnlyList<IConceptualEntity> _groupEntities;

		// Token: 0x04000A16 RID: 2582
		private readonly ReadOnlyCollection<Measure> _measures;

		// Token: 0x04000A17 RID: 2583
		private readonly ReadOnlyCollection<IJoinPredicate> _explicitJoinPredicates;

		// Token: 0x04000A18 RID: 2584
		private readonly bool _multipleGroups;

		// Token: 0x04000A19 RID: 2585
		private readonly bool _crossJoinQueryPlan;

		// Token: 0x04000A1A RID: 2586
		private readonly BaseEntitySets _crossFilteredEntitySetsByCardinalityOnly;

		// Token: 0x04000A1B RID: 2587
		private readonly BaseConceptualEntities _crossFilteredEntitiesByCardinalityOnly;

		// Token: 0x04000A1C RID: 2588
		private readonly BaseEntitySets _crossFilteredEntitySets;

		// Token: 0x04000A1D RID: 2589
		private readonly BaseConceptualEntities _crossFilteredEntities;

		// Token: 0x04000A1E RID: 2590
		private readonly bool _includeDirectManyToManyAssociations;

		// Token: 0x04000A1F RID: 2591
		private readonly bool _useConceptualSchema;
	}
}
