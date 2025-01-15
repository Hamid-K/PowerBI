using System;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav;
using Microsoft.ReportingServices.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.DataShapeValidation;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.ModelReconciliation
{
	// Token: 0x0200009E RID: 158
	internal sealed class ExpressionNodeTreeReconciler : ExpressionNodeTreeTransform
	{
		// Token: 0x06000749 RID: 1865 RVA: 0x0001BE3C File Offset: 0x0001A03C
		private ExpressionNodeTreeReconciler(ExpressionContext context, IFederatedConceptualSchema schema, ScopeTree scopeTree, IdentifierValidator idValidator, IFeatureSwitchProvider featureSwitchProvider)
			: base(false)
		{
			this.m_context = context;
			this.m_schema = schema;
			this.m_scopeTree = scopeTree;
			this.m_idValidator = idValidator;
			this.m_featureSwitchProvider = featureSwitchProvider;
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x0001BE6A File Offset: 0x0001A06A
		public static ExpressionNode Reconcile(ExpressionContext context, IFederatedConceptualSchema schema, ScopeTree scopeTree, IdentifierValidator idValidator, ExpressionNode node, IFeatureSwitchProvider featureSwitchProvider)
		{
			return new ExpressionNodeTreeReconciler(context, schema, scopeTree, idValidator, featureSwitchProvider).Visit(node);
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x0001BE80 File Offset: 0x0001A080
		public override ExpressionNode Visit(UnaryOperatorExpressionNode node)
		{
			ExpressionNode expressionNode = this.Visit(node.Operand);
			return new UnaryOperatorExpressionNode(node.OperatorKind, expressionNode);
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x0001BEA8 File Offset: 0x0001A0A8
		public override ExpressionNode Visit(BinaryOperatorExpressionNode node)
		{
			ExpressionNode expressionNode = this.Visit(node.Left);
			ExpressionNode expressionNode2 = this.Visit(node.Right);
			return new BinaryOperatorExpressionNode(node.OperatorKind, expressionNode, expressionNode2);
		}

		// Token: 0x0600074D RID: 1869 RVA: 0x0001BEDC File Offset: 0x0001A0DC
		public override ExpressionNode Visit(EntitySetExpressionNode node)
		{
			IConceptualEntity conceptualEntity = node.Entity;
			if (conceptualEntity == null)
			{
				conceptualEntity = this.ResolveEntity(node);
			}
			if (conceptualEntity == null)
			{
				this.RegisterEntityNotFoundError(node.GetFullName());
				return node;
			}
			return new ResolvedEntitySetExpressionNode(conceptualEntity);
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x0001BF14 File Offset: 0x0001A114
		public override ExpressionNode Visit(PropertyExpressionNode node)
		{
			IConceptualProperty conceptualProperty = node.Property;
			if (conceptualProperty == null)
			{
				conceptualProperty = this.ResolveSchemaProperty(node);
			}
			if (conceptualProperty == null)
			{
				return node;
			}
			return ExprNodes.ModelProperty(conceptualProperty);
		}

		// Token: 0x0600074F RID: 1871 RVA: 0x0001BF40 File Offset: 0x0001A140
		private IConceptualProperty ResolveSchemaProperty(PropertyExpressionNode node)
		{
			IConceptualEntity conceptualEntity = this.ResolveEntity(node.EntitySet);
			if (conceptualEntity == null)
			{
				return null;
			}
			IConceptualProperty conceptualProperty;
			if (conceptualEntity.Schema.SchemaId == "")
			{
				if (!conceptualEntity.TryGetPropertyByEdmName(node.Name, out conceptualProperty))
				{
					this.m_context.ErrorContext.Register(TranslationMessages.InvalidExpression(EngineMessageSeverity.Error, this.m_context.ObjectType, this.m_context.ObjectId, this.m_context.PropertyName, TranslationMessagePhrases.InvalidFieldReference(TranslationMessageUtils.GetPropertyNameForError(node.EntitySet.Name, node.Name))));
				}
			}
			else if (!conceptualEntity.TryGetProperty(node.Name, out conceptualProperty))
			{
				this.m_context.ErrorContext.Register(TranslationMessages.InvalidExpression(EngineMessageSeverity.Error, this.m_context.ObjectType, this.m_context.ObjectId, this.m_context.PropertyName, TranslationMessagePhrases.InvalidFieldReference(TranslationMessageUtils.GetPropertyNameForError(node.EntitySet.Name, node.Name))));
			}
			return conceptualProperty;
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x0001C040 File Offset: 0x0001A240
		private IConceptualEntity ResolveEntity(EntitySetExpressionNode entitySetNode)
		{
			IConceptualSchema conceptualSchema;
			if (!this.m_schema.TryGetSchema(entitySetNode.Container, out conceptualSchema) && !this.m_schema.TryGetDefaultSchema(out conceptualSchema))
			{
				Microsoft.DataShaping.Contract.RetailFail("Could not find schema.");
			}
			string fullName = entitySetNode.GetFullName();
			IConceptualEntity conceptualEntity;
			if (!conceptualSchema.TryGetEntityByEdmName(fullName, out conceptualEntity))
			{
				this.RegisterEntityNotFoundError(fullName);
			}
			return conceptualEntity;
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x0001C098 File Offset: 0x0001A298
		public override ExpressionNode Visit(StructureReferenceExpressionNode node)
		{
			IIdentifiable identifiable;
			if (this.m_scopeTree.TryGetItemById(node.TargetId, out identifiable))
			{
				Calculation calculation = identifiable as Calculation;
				if (calculation != null)
				{
					return new ResolvedCalculationReferenceExpressionNode(calculation);
				}
				IScope scope = identifiable as IScope;
				if (scope != null)
				{
					return new ResolvedScopeReferenceExpressionNode(scope);
				}
			}
			if (this.m_idValidator.TryGetById(node.TargetId, out identifiable))
			{
				Limit limit = identifiable as Limit;
				if (limit != null)
				{
					return new ResolvedLimitReferenceExpressionNode(limit);
				}
				GroupKey groupKey = identifiable as GroupKey;
				if (groupKey != null)
				{
					return new ResolvedGroupKeyReferenceExpressionNode(groupKey);
				}
				SortKey sortKey = identifiable as SortKey;
				if (sortKey != null)
				{
					return new ResolvedSortKeyReferenceExpressionNode(sortKey);
				}
				ScopeValueDefinition scopeValueDefinition = identifiable as ScopeValueDefinition;
				if (scopeValueDefinition != null)
				{
					return new ResolvedScopeValueDefinitionReferenceExpressionNode(scopeValueDefinition);
				}
			}
			this.m_context.ErrorContext.Register(TranslationMessages.InvalidExpression(EngineMessageSeverity.Error, this.m_context.ObjectType, this.m_context.ObjectId, this.m_context.PropertyName, TranslationMessagePhrases.MissingOrInvalidStructuralReferenceTarget(node.TargetId)));
			return node;
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x0001C184 File Offset: 0x0001A384
		public override ExpressionNode Visit(DataTransformTableColumnReferenceExpressionNode node)
		{
			IIdentifiable identifiable;
			if (this.m_idValidator.TryGetById(node.Table.TargetId, out identifiable))
			{
				DataTransformTable dataTransformTable = identifiable as DataTransformTable;
				DataTransformTableColumn dataTransformTableColumn;
				if (dataTransformTable != null && dataTransformTable.TryGetColumn(node.Column.TargetId, out dataTransformTableColumn))
				{
					return new ResolvedDataTransformTableColumnReferenceExpressionNode(dataTransformTable, dataTransformTableColumn);
				}
			}
			this.m_context.ErrorContext.Register(TranslationMessages.InvalidExpression(EngineMessageSeverity.Error, this.m_context.ObjectType, this.m_context.ObjectId, this.m_context.PropertyName, TranslationMessagePhrases.MissingOrInvalidStructuralReferenceTarget(node.Table.TargetId)));
			return node;
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x0001C21C File Offset: 0x0001A41C
		public override ExpressionNode Visit(FunctionCallExpressionNode node)
		{
			string name = node.Descriptor.Name;
			ResolvedScopeReferenceExpressionNode resolvedScopeReferenceExpressionNode;
			ResolvedScopeReferenceExpressionNode resolvedScopeReferenceExpressionNode2;
			if (!(name == "Intersect"))
			{
				if (name == "ScopeOf")
				{
					ResolvedCalculationReferenceExpressionNode resolvedCalculationReferenceExpressionNode;
					if (node.Arguments.Count == 1 && this.TryResolveFunctionArgument<ResolvedCalculationReferenceExpressionNode>(node.Arguments[0], out resolvedCalculationReferenceExpressionNode))
					{
						Calculation calculation = (Calculation)resolvedCalculationReferenceExpressionNode.Target;
						IScope containingScope = this.m_scopeTree.GetContainingScope(calculation);
						if (containingScope != null)
						{
							return new ResolvedScopeReferenceExpressionNode(containingScope);
						}
						this.m_context.ErrorContext.Register(TranslationMessages.InvalidExpression(EngineMessageSeverity.Error, this.m_context.ObjectType, this.m_context.ObjectId, this.m_context.PropertyName, TranslationMessagePhrases.MissingOrInvalidStructuralReferenceTarget(calculation.Id)));
					}
				}
			}
			else if (node.Arguments.Count == 2 && this.TryResolveFunctionArgument<ResolvedScopeReferenceExpressionNode>(node.Arguments[0], out resolvedScopeReferenceExpressionNode) && this.TryResolveFunctionArgument<ResolvedScopeReferenceExpressionNode>(node.Arguments[1], out resolvedScopeReferenceExpressionNode2))
			{
				IScope scope = null;
				if (this.m_scopeTree.TryGetIntersectionScope(resolvedScopeReferenceExpressionNode.Scope, resolvedScopeReferenceExpressionNode2.Scope, out scope))
				{
					return new ResolvedScopeReferenceExpressionNode(scope);
				}
				this.m_context.ErrorContext.Register(TranslationMessages.InvalidExpression(EngineMessageSeverity.Error, this.m_context.ObjectType, this.m_context.ObjectId, this.m_context.PropertyName, TranslationMessagePhrases.MissingOrInvalidStructuralReferenceTarget(resolvedScopeReferenceExpressionNode.Target.Id)));
			}
			return base.Visit(node);
		}

		// Token: 0x06000754 RID: 1876 RVA: 0x0001C3A4 File Offset: 0x0001A5A4
		private bool TryResolveFunctionArgument<ArgType>(ExpressionNode node, out ArgType resolvedArg) where ArgType : class
		{
			resolvedArg = this.Visit(node) as ArgType;
			return resolvedArg != null;
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x0001C3D0 File Offset: 0x0001A5D0
		private void RegisterEntityNotFoundError(string name)
		{
			this.m_context.ErrorContext.Register(TranslationMessages.InvalidExpression(EngineMessageSeverity.Error, this.m_context.ObjectType, this.m_context.ObjectId, this.m_context.PropertyName, TranslationMessagePhrases.EntitySetNotFound(TranslationMessageUtils.GetEntityNameForError(name))));
		}

		// Token: 0x04000387 RID: 903
		private readonly ExpressionContext m_context;

		// Token: 0x04000388 RID: 904
		private readonly IFederatedConceptualSchema m_schema;

		// Token: 0x04000389 RID: 905
		private readonly ScopeTree m_scopeTree;

		// Token: 0x0400038A RID: 906
		private readonly IdentifierValidator m_idValidator;

		// Token: 0x0400038B RID: 907
		private readonly IFeatureSwitchProvider m_featureSwitchProvider;
	}
}
