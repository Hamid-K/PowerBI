using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Data.Contracts;
using Microsoft.InfoNav.Data.Contracts.ConceptualSchema.Annotations;
using Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal;
using Microsoft.ReportingServices.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.Annotations;
using Microsoft.ReportingServices.DataShapeQueryTranslation.ExpressionAnalysis;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.SemanticValidation
{
	// Token: 0x02000068 RID: 104
	internal sealed class FilterSemanticValidator : FilterExpressionVisitor
	{
		// Token: 0x0600056D RID: 1389 RVA: 0x00012F2C File Offset: 0x0001112C
		private FilterSemanticValidator(ExpressionSemanticValidator expressionValidator, TranslationErrorContext errorContext, Identifier objectId, DataShape dataShape, IIdentifiable filterTarget, ExpressionTable expressionTable, ScopeTree scopeTree, DataShapeAnnotations annotations, IFederatedConceptualSchema schema, bool allowHierarchicalMultiEntitySlicers, VisitDataShapeDelegate visitDataShape)
			: base(visitDataShape)
		{
			this.m_expressionValidator = expressionValidator;
			this.m_errorContext = errorContext;
			this.m_objectId = objectId;
			this.m_dataShape = dataShape;
			this.m_filterTarget = filterTarget;
			this.m_expressionTable = expressionTable;
			this.m_scopeTree = scopeTree;
			this.m_annotations = annotations;
			this.m_schema = schema;
			this.m_allowHierarchicalMultiEntitySlicers = allowHierarchicalMultiEntitySlicers;
			this.m_entitySetReferenceValidationContext = new FilterSemanticValidator.EntitySetReferenceValidationContext(new Action(this.ValidateEntitySetReferences));
			this.m_filterTargetScope = FilterSemanticValidator.DetermineFilterTargetScope(filterTarget, scopeTree);
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x00012FB4 File Offset: 0x000111B4
		private static IScope DetermineFilterTargetScope(IIdentifiable filterTarget, ScopeTree scopeTree)
		{
			IScope scope = filterTarget as IScope;
			if (scope != null)
			{
				return scope;
			}
			Calculation calculation = filterTarget as Calculation;
			return scopeTree.GetContainingScope(calculation);
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x00012FDC File Offset: 0x000111DC
		public static void Validate(Microsoft.DataShaping.InternalContracts.DataShapeQuery.Filter filter, ExpressionSemanticValidator expressionValidator, TranslationErrorContext errorContext, Identifier objectId, DataShape dataShape, IIdentifiable filterTarget, ExpressionTable expressionTable, ScopeTree scopeTree, DataShapeAnnotations annotations, IFederatedConceptualSchema schema, VisitDataShapeDelegate visitDataShape, bool allowHierarchicalMultiEntitySlicers, out bool isScopeFilter)
		{
			FilterSemanticValidator filterSemanticValidator = new FilterSemanticValidator(expressionValidator, errorContext, objectId, dataShape, filterTarget, expressionTable, scopeTree, annotations, schema, allowHierarchicalMultiEntitySlicers, visitDataShape);
			filterSemanticValidator.m_isScopeFilter = false;
			filter.Condition.Accept<Microsoft.DataShaping.InternalContracts.DataShapeQuery.FilterCondition>(filterSemanticValidator);
			isScopeFilter = filterSemanticValidator.m_isScopeFilter;
			filterSemanticValidator.ValidateFilterTarget(annotations);
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x00013028 File Offset: 0x00011228
		private void ValidateFilterTarget(DataShapeAnnotations annotations)
		{
			IScope scope = null;
			ObjectType objectType = this.m_filterTarget.ObjectType;
			if (objectType != ObjectType.Calculation)
			{
				if (objectType - ObjectType.DataIntersection > 1)
				{
					if (objectType == ObjectType.DataShape)
					{
						scope = this.m_scopeTree.GetScope(this.m_filterTarget.Id);
					}
				}
				else
				{
					IScope scope2 = this.m_scopeTree.GetScope(this.m_filterTarget.Id);
					scope = annotations.GetContainingDataShape(scope2);
				}
			}
			else
			{
				Calculation calculation = this.m_filterTarget as Calculation;
				IScope containingScope = this.m_scopeTree.GetContainingScope(calculation);
				scope = annotations.GetContainingDataShape(containingScope);
			}
			if (!this.m_scopeTree.AreSameScope(this.m_dataShape, scope))
			{
				this.m_errorContext.Register(TranslationMessages.InvalidFilterTargetScope(EngineMessageSeverity.Error, ObjectType.DataShape, this.m_dataShape.Id, "Filters", this.m_filterTarget.ObjectType, this.m_filterTarget.Id));
			}
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x00013103 File Offset: 0x00011303
		internal override void VisitExpression(Expression expression, Microsoft.DataShaping.InternalContracts.DataShapeQuery.FilterCondition owner, string propertyName)
		{
			this.m_expressionValidator.Validate(expression, ExpressionFeatures.FilterExpression, owner.ObjectType, this.m_objectId, propertyName, this.m_filterTargetScope);
			this.ValidateFilterCondition(owner, expression);
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x00013134 File Offset: 0x00011334
		internal override Microsoft.DataShaping.InternalContracts.DataShapeQuery.FilterCondition Visit(BinaryFilterCondition condition)
		{
			Action action = delegate
			{
				this.<>n__0(condition);
				FilterSemanticValidator.EntityReferenceValidationScope currentScope = this.m_entitySetReferenceValidationContext.CurrentScope;
				if (currentScope.ReferencedEntities.Count > 1)
				{
					this.m_errorContext.Register(TranslationMessages.InvalidFilterConditionMultipleEntitySets(EngineMessageSeverity.Error, currentScope.ObjectType, currentScope.Identifier));
				}
			};
			this.m_entitySetReferenceValidationContext.RunInExistingOrNewValidatingScope(action, condition.ObjectType, condition.Id, false);
			return condition;
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x0001318C File Offset: 0x0001138C
		internal override Microsoft.DataShaping.InternalContracts.DataShapeQuery.FilterCondition Visit(InFilterCondition condition)
		{
			this.m_entitySetReferenceValidationContext.RunInNonValidatingScope(delegate
			{
				this.<>n__1(condition);
			});
			if (condition.HasTable && !this.ModelSupportsInOperator())
			{
				this.m_errorContext.Register(TranslationMessages.InTableFilterNotSupportedForModel(EngineMessageSeverity.Error, ObjectType.InFilterCondition, this.m_objectId, "Table"));
			}
			return condition;
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x000131FD File Offset: 0x000113FD
		private bool ModelSupportsInOperator()
		{
			return this.m_schema.GetDefaultSchemaDaxCapabilitiesAnnotation().SupportsInOperator;
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x00013210 File Offset: 0x00011410
		internal override Microsoft.DataShaping.InternalContracts.DataShapeQuery.FilterCondition Visit(Microsoft.DataShaping.InternalContracts.DataShapeQuery.CompoundFilterCondition condition)
		{
			if (!condition.Operator.IsValid || this.m_annotations.HasComplexSlicer(this.m_dataShape) || !FilterComplexityAnalyzer.ConditionsAreCorrelated(condition, this.m_isNegated))
			{
				this.m_entitySetReferenceValidationContext.RunInNonValidatingScope(delegate
				{
					this.<>n__2(condition);
				});
			}
			else
			{
				this.m_entitySetReferenceValidationContext.RunInExistingOrNewValidatingScope(delegate
				{
					this.<>n__2(condition);
				}, condition.ObjectType, condition.Id, this.m_allowHierarchicalMultiEntitySlicers);
			}
			return condition;
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x000132C4 File Offset: 0x000114C4
		internal override Microsoft.DataShaping.InternalContracts.DataShapeQuery.FilterCondition Visit(ContextFilterCondition condition)
		{
			if (!condition.DataShape.ContextOnly.GetValueOrDefault<bool>())
			{
				this.m_errorContext.Register(TranslationMessages.InvalidContextOnlyFlagForFilterContextDataShape(EngineMessageSeverity.Error, this.m_dataShape.ObjectType, this.m_dataShape.Id, "ContextOnly", condition.DataShape.Id));
			}
			base.Visit(condition);
			return condition;
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x00013323 File Offset: 0x00011523
		internal override Microsoft.DataShaping.InternalContracts.DataShapeQuery.FilterCondition Visit(ApplyFilterCondition condition)
		{
			if (!this.IsValidApplyFilterDataShapeReference(condition))
			{
				this.m_errorContext.Register(TranslationMessages.InvalidApplyFilterDataShapeReference(EngineMessageSeverity.Error, condition.ObjectType, this.m_objectId, "DataShapeReference"));
			}
			base.Visit(condition);
			return condition;
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x0001335C File Offset: 0x0001155C
		private bool IsValidApplyFilterDataShapeReference(ApplyFilterCondition condition)
		{
			if (condition.DataShapeReference == null)
			{
				return true;
			}
			ResolvedScopeReferenceExpressionNode resolvedScopeReferenceExpressionNode = this.m_expressionTable.GetNode(condition.DataShapeReference) as ResolvedScopeReferenceExpressionNode;
			if (resolvedScopeReferenceExpressionNode != null)
			{
				DataShape dataShape = resolvedScopeReferenceExpressionNode.Target as DataShape;
				if (dataShape != null)
				{
					return dataShape.IsIndependent && dataShape.ContextOnly.GetValueOrDefault<bool>() && !this.m_scopeTree.IsSameOrParentScope(dataShape, this.m_dataShape);
				}
			}
			return false;
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x000133D0 File Offset: 0x000115D0
		private void ValidateFilterCondition(Microsoft.DataShaping.InternalContracts.DataShapeQuery.FilterCondition condition, Expression expression)
		{
			ObjectType objectType = this.m_filterTarget.ObjectType;
			ExpressionNode node = this.m_expressionTable.GetNode(expression);
			bool flag = MeasureAnalyzer.IsMeasure(node);
			if (objectType == ObjectType.DataMember || objectType == ObjectType.DataIntersection)
			{
				if (!flag && !FilterSemanticValidator.IsNeutralConditionExpression(node))
				{
					this.m_errorContext.Register(TranslationMessages.InvalidExpression(EngineMessageSeverity.Error, condition.ObjectType, this.m_filterTarget.Id, "Condition", TranslationMessagePhrases.InvalidFilter(objectType, this.m_objectId)));
				}
				else
				{
					this.m_isScopeFilter = true;
				}
			}
			if (objectType == ObjectType.Calculation && flag)
			{
				this.m_errorContext.Register(TranslationMessages.InvalidExpression(EngineMessageSeverity.Error, condition.ObjectType, this.m_filterTarget.Id, "Condition", TranslationMessagePhrases.InvalidFilter(objectType, this.m_objectId)));
			}
			if (objectType == ObjectType.DataShape)
			{
				if (flag)
				{
					DataShape dataShape = (DataShape)this.m_filterTarget;
					if (!this.m_scopeTree.IsRoot(dataShape) || this.m_scopeTree.IsOrContainsGroup(dataShape))
					{
						this.m_errorContext.Register(TranslationMessages.InvalidExpression(EngineMessageSeverity.Error, condition.ObjectType, this.m_filterTarget.Id, "Condition", TranslationMessagePhrases.InvalidFilter(objectType, this.m_objectId)));
					}
					else
					{
						this.m_isScopeFilter = true;
					}
				}
				else if (!FilterSemanticValidator.IsNeutralConditionExpression(node))
				{
					this.m_isSlicer = true;
				}
			}
			if (this.m_isSlicer && this.m_isScopeFilter)
			{
				this.m_errorContext.Register(TranslationMessages.InvalidMixedCompoundFilterCondition(EngineMessageSeverity.Error, objectType, this.m_objectId.Value, "Target"));
			}
			if (flag && this.m_annotations.HasComplexSlicer(this.m_dataShape))
			{
				this.m_errorContext.Register(TranslationMessages.ComplexSlicerNotAllowedWithMeasures(EngineMessageSeverity.Error, this.m_dataShape.Id));
			}
			this.AddEntitySetReferences(node);
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x00013577 File Offset: 0x00011777
		private static bool IsNeutralConditionExpression(ExpressionNode expressionNode)
		{
			return expressionNode is LiteralExpressionNode || expressionNode is QueryParameterReferenceExpressionNode;
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x0001358C File Offset: 0x0001178C
		private void AddEntitySetReferences(ExpressionNode expressionNode)
		{
			FilterSemanticValidator.EntityReferenceValidationScope currentScope = this.m_entitySetReferenceValidationContext.CurrentScope;
			if (currentScope != null && currentScope.IsValidating)
			{
				IReadOnlyList<IConceptualEntity> readOnlyList = EntitySetReferenceAnalyzer.Analyze(expressionNode);
				if (readOnlyList != null)
				{
					currentScope.ReferencedEntities.UnionWith(readOnlyList.Select((IConceptualEntity e) => e.GetBaseModelEntity()));
				}
			}
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x000135EC File Offset: 0x000117EC
		private void ValidateEntitySetReferences()
		{
			FilterSemanticValidator.EntityReferenceValidationScope currentScope = this.m_entitySetReferenceValidationContext.CurrentScope;
			if (currentScope != null && currentScope.IsValidating)
			{
				bool flag = this.m_annotations.HasComplexSlicer(this.m_dataShape);
				if (currentScope.ReferencedEntities.Count > 1 && !flag)
				{
					if (currentScope.AllowHierarchicalMultiEntitySlicers)
					{
						if (!QueryAlgorithms.HasHierarchicalFilterPath(currentScope.ReferencedEntities, this.m_schema.GetDefaultSchema()))
						{
							this.m_errorContext.Register(TranslationMessages.InvalidFilterConditionNonHierarchicalEntitySets(EngineMessageSeverity.Error, currentScope.ObjectType, currentScope.Identifier));
							return;
						}
					}
					else
					{
						this.m_errorContext.Register(TranslationMessages.InvalidFilterConditionMultipleEntitySets(EngineMessageSeverity.Error, currentScope.ObjectType, currentScope.Identifier));
					}
				}
			}
		}

		// Token: 0x04000297 RID: 663
		private readonly ExpressionSemanticValidator m_expressionValidator;

		// Token: 0x04000298 RID: 664
		private readonly TranslationErrorContext m_errorContext;

		// Token: 0x04000299 RID: 665
		private readonly Identifier m_objectId;

		// Token: 0x0400029A RID: 666
		private readonly DataShape m_dataShape;

		// Token: 0x0400029B RID: 667
		private readonly IIdentifiable m_filterTarget;

		// Token: 0x0400029C RID: 668
		private readonly ExpressionTable m_expressionTable;

		// Token: 0x0400029D RID: 669
		private readonly ScopeTree m_scopeTree;

		// Token: 0x0400029E RID: 670
		private readonly DataShapeAnnotations m_annotations;

		// Token: 0x0400029F RID: 671
		private readonly IScope m_filterTargetScope;

		// Token: 0x040002A0 RID: 672
		private readonly FilterSemanticValidator.EntitySetReferenceValidationContext m_entitySetReferenceValidationContext;

		// Token: 0x040002A1 RID: 673
		private readonly IFederatedConceptualSchema m_schema;

		// Token: 0x040002A2 RID: 674
		private readonly bool m_allowHierarchicalMultiEntitySlicers;

		// Token: 0x040002A3 RID: 675
		private bool m_isScopeFilter;

		// Token: 0x040002A4 RID: 676
		private bool m_isSlicer;

		// Token: 0x0200027D RID: 637
		private sealed class EntitySetReferenceValidationContext
		{
			// Token: 0x06001546 RID: 5446 RVA: 0x0004FB4B File Offset: 0x0004DD4B
			internal EntitySetReferenceValidationContext(Action validateEntityReferences)
			{
				this.m_validateEntityReferences = validateEntityReferences;
				this.m_entityReferenceValidationScopes = new Stack<FilterSemanticValidator.EntityReferenceValidationScope>(4);
			}

			// Token: 0x170003CF RID: 975
			// (get) Token: 0x06001547 RID: 5447 RVA: 0x0004FB66 File Offset: 0x0004DD66
			public FilterSemanticValidator.EntityReferenceValidationScope CurrentScope
			{
				get
				{
					if (this.m_entityReferenceValidationScopes.Count != 0)
					{
						return this.m_entityReferenceValidationScopes.Peek();
					}
					return null;
				}
			}

			// Token: 0x06001548 RID: 5448 RVA: 0x0004FB84 File Offset: 0x0004DD84
			public void RunInExistingOrNewValidatingScope(Action action, ObjectType objectType, Identifier id, bool allowHierarchicalMultiEntitySlicers)
			{
				FilterSemanticValidator.EntityReferenceValidationScope currentScope = this.CurrentScope;
				FilterSemanticValidator.EntityReferenceValidationScope entityReferenceValidationScope = ((currentScope == null || !currentScope.IsValidating || currentScope.AllowHierarchicalMultiEntitySlicers != allowHierarchicalMultiEntitySlicers) ? new FilterSemanticValidator.EntityReferenceValidationScope(objectType, id, true, allowHierarchicalMultiEntitySlicers) : null);
				this.RunInScope(action, entityReferenceValidationScope);
			}

			// Token: 0x06001549 RID: 5449 RVA: 0x0004FBCB File Offset: 0x0004DDCB
			public void RunInNonValidatingScope(Action action)
			{
				this.RunInScope(action, new FilterSemanticValidator.EntityReferenceValidationScope(false, false));
			}

			// Token: 0x0600154A RID: 5450 RVA: 0x0004FBDC File Offset: 0x0004DDDC
			private void RunInScope(Action action, FilterSemanticValidator.EntityReferenceValidationScope scope)
			{
				if (scope != null)
				{
					this.m_entityReferenceValidationScopes.Push(scope);
				}
				action();
				if (scope != null)
				{
					if (scope.IsValidating)
					{
						this.m_validateEntityReferences();
					}
					this.m_entityReferenceValidationScopes.Pop();
					FilterSemanticValidator.EntityReferenceValidationScope entityReferenceValidationScope = this.m_entityReferenceValidationScopes.PeekOrDefault<FilterSemanticValidator.EntityReferenceValidationScope>();
					if (entityReferenceValidationScope != null)
					{
						entityReferenceValidationScope.ReferencedEntities.UnionWith(scope.ReferencedEntities);
					}
				}
			}

			// Token: 0x040009C7 RID: 2503
			private readonly Action m_validateEntityReferences;

			// Token: 0x040009C8 RID: 2504
			private readonly Stack<FilterSemanticValidator.EntityReferenceValidationScope> m_entityReferenceValidationScopes;
		}

		// Token: 0x0200027E RID: 638
		private sealed class EntityReferenceValidationScope
		{
			// Token: 0x0600154B RID: 5451 RVA: 0x0004FC40 File Offset: 0x0004DE40
			internal EntityReferenceValidationScope(bool requiresValidation, bool allowHierarchicalMultiEntitySlicers)
			{
				this.IsValidating = requiresValidation;
				this.AllowHierarchicalMultiEntitySlicers = allowHierarchicalMultiEntitySlicers;
				this.ReferencedEntities = new HashSet<IConceptualEntity>(ConceptualEntityExtensionAwareEqualityComparer.Instance);
			}

			// Token: 0x0600154C RID: 5452 RVA: 0x0004FC66 File Offset: 0x0004DE66
			internal EntityReferenceValidationScope(ObjectType objectType, Identifier id, bool requiresValidation, bool allowHierarchicalMultiEntitySlicers)
				: this(requiresValidation, allowHierarchicalMultiEntitySlicers)
			{
				this.m_objectType = objectType;
				this.m_id = id;
			}

			// Token: 0x170003D0 RID: 976
			// (get) Token: 0x0600154D RID: 5453 RVA: 0x0004FC7F File Offset: 0x0004DE7F
			public bool IsValidating { get; }

			// Token: 0x170003D1 RID: 977
			// (get) Token: 0x0600154E RID: 5454 RVA: 0x0004FC87 File Offset: 0x0004DE87
			public bool AllowHierarchicalMultiEntitySlicers { get; }

			// Token: 0x170003D2 RID: 978
			// (get) Token: 0x0600154F RID: 5455 RVA: 0x0004FC8F File Offset: 0x0004DE8F
			public ObjectType ObjectType
			{
				get
				{
					return this.m_objectType;
				}
			}

			// Token: 0x170003D3 RID: 979
			// (get) Token: 0x06001550 RID: 5456 RVA: 0x0004FC97 File Offset: 0x0004DE97
			public Identifier Identifier
			{
				get
				{
					return this.m_id;
				}
			}

			// Token: 0x170003D4 RID: 980
			// (get) Token: 0x06001551 RID: 5457 RVA: 0x0004FC9F File Offset: 0x0004DE9F
			public HashSet<IConceptualEntity> ReferencedEntities { get; }

			// Token: 0x040009C9 RID: 2505
			private readonly ObjectType m_objectType;

			// Token: 0x040009CA RID: 2506
			private readonly Identifier m_id;
		}
	}
}
