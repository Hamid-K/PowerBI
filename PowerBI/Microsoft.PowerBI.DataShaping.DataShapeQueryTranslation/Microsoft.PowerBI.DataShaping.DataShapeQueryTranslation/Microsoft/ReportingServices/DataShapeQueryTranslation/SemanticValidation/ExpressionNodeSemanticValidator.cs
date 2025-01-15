using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Data.Contracts.ConceptualSchema.Annotations;
using Microsoft.Reporting;
using Microsoft.ReportingServices.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.Annotations;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.SemanticValidation
{
	// Token: 0x02000066 RID: 102
	internal sealed class ExpressionNodeSemanticValidator : ExpressionNodeTreeTransform
	{
		// Token: 0x0600054F RID: 1359 RVA: 0x00011FF1 File Offset: 0x000101F1
		private ExpressionNodeSemanticValidator(ExpressionFeatureFlags allowedFeatures, ExpressionContext context, ScopeTree scopeTree, IScope containingScope, ExpressionTable expressionTable, DataShapeAnnotations annotations, DaxCapabilitiesAnnotation daxCapabilitiesAnnotation)
			: base(true)
		{
			this.m_allowedFeatures = allowedFeatures;
			this.m_context = context;
			this.m_scopeTree = scopeTree;
			this.m_containingScope = containingScope;
			this.m_expressionTable = expressionTable;
			this.m_annotations = annotations;
			this.m_daxCapabilitiesAnnotation = daxCapabilitiesAnnotation;
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000550 RID: 1360 RVA: 0x0001202F File Offset: 0x0001022F
		private FunctionCallExpressionNode CurrentFunction
		{
			get
			{
				return (FunctionCallExpressionNode)base.ParentNodes.FirstOrDefault((ExpressionNode n) => n.Kind == ExpressionNodeKind.FunctionCall);
			}
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x00012060 File Offset: 0x00010260
		private FunctionCallExpressionNode GetParentFunction(int skip = 0)
		{
			return (FunctionCallExpressionNode)base.ParentNodes.Where((ExpressionNode n) => n.Kind == ExpressionNodeKind.FunctionCall).Skip(skip).FirstOrDefault<ExpressionNode>();
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x0001209C File Offset: 0x0001029C
		public static ExpressionValidationResult Validate(ExpressionNode node, ExpressionFeatureFlags allowedFeatures, ExpressionContext context, ScopeTree scopeTree, IScope containingScope, ExpressionTable inputExpressionTable, DataShapeAnnotations annotations, DaxCapabilitiesAnnotation daxCapabilitiesAnnotation)
		{
			return new ExpressionNodeSemanticValidator(allowedFeatures, context, scopeTree, containingScope, inputExpressionTable, annotations, daxCapabilitiesAnnotation).Validate(node);
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x000120B4 File Offset: 0x000102B4
		private ExpressionValidationResult Validate(ExpressionNode node)
		{
			return new ExpressionValidationResult(this.Visit(node), this.m_referencedCalculations);
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x000120C8 File Offset: 0x000102C8
		public override ExpressionNode Visit(FunctionCallExpressionNode node)
		{
			this.ValidateFunction(node);
			this.ValidateArguments(node);
			node = this.AssignFunctionUsageKind(node);
			return base.Visit(node);
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x000120E8 File Offset: 0x000102E8
		private void ValidateFunction(FunctionCallExpressionNode node)
		{
			string name = node.Descriptor.Name;
			uint num = global::<PrivateImplementationDetails>.ComputeStringHash(name);
			if (num <= 2190492344U)
			{
				if (num <= 1155500979U)
				{
					if (num != 969105777U)
					{
						if (num != 1155500979U)
						{
							return;
						}
						if (!(name == "Rollup"))
						{
							return;
						}
						this.ValidateRollup(node);
						return;
					}
					else
					{
						if (!(name == "SynchronizationIndex"))
						{
							return;
						}
						this.ValidateSynchronizationindex(node);
						return;
					}
				}
				else if (num != 1676043659U)
				{
					if (num != 2190492344U)
					{
						return;
					}
					if (!(name == "PercentileExc"))
					{
						return;
					}
				}
				else
				{
					if (!(name == "Scope"))
					{
						return;
					}
					this.ValidateScope(node);
					return;
				}
			}
			else if (num <= 2988423237U)
			{
				if (num != 2740707114U)
				{
					if (num != 2988423237U)
					{
						return;
					}
					if (!(name == "LimitProperty"))
					{
						return;
					}
					if (base.ParentNodes.Count > 1)
					{
						this.RegisterInvalidFunctionUsageError(node);
						return;
					}
					return;
				}
				else
				{
					if (!(name == "Evaluate"))
					{
						return;
					}
					this.ValidateEvaluate(node);
					return;
				}
			}
			else if (num != 3546368141U)
			{
				if (num != 3763614678U)
				{
					return;
				}
				if (!(name == "PercentileInc"))
				{
					return;
				}
			}
			else
			{
				if (!(name == "Subtotal"))
				{
					return;
				}
				this.CheckFunctionRestriction(ExpressionFeatureFlags.Subtotal, "Subtotal");
				if (base.ParentNodes.Count > 1)
				{
					this.RegisterInvalidFunctionUsageError(node);
					return;
				}
				return;
			}
			this.ValidatePercentile(node);
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x00012237 File Offset: 0x00010437
		private void ValidateSynchronizationindex(FunctionCallExpressionNode node)
		{
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x0001223C File Offset: 0x0001043C
		private void ValidatePercentile(FunctionCallExpressionNode node)
		{
			if (!node.HasValidArgumentCount)
			{
				return;
			}
			LiteralExpressionNode literalExpressionNode = node.Arguments[1] as LiteralExpressionNode;
			if (literalExpressionNode == null)
			{
				return;
			}
			string name = node.Descriptor.Name;
			if (!(name == "PercentileExc"))
			{
				if (!(name == "PercentileInc"))
				{
					Microsoft.DataShaping.Contract.RetailFail("Unexpected percentile function name");
				}
				else
				{
					if (!literalExpressionNode.Value.IsOfType<double>())
					{
						this.m_context.ErrorContext.Register(TranslationMessages.InvalidExpression(EngineMessageSeverity.Error, this.m_context.ObjectType, this.m_context.ObjectId, this.m_context.PropertyName, TranslationMessagePhrases.InvalidPercentileIncKValue(node.Descriptor.Name, 1)));
						return;
					}
					double num = literalExpressionNode.Value.CastValue<double>();
					if (num < 0.0 || num > 1.0)
					{
						this.m_context.ErrorContext.Register(TranslationMessages.InvalidExpression(EngineMessageSeverity.Error, this.m_context.ObjectType, this.m_context.ObjectId, this.m_context.PropertyName, TranslationMessagePhrases.InvalidPercentileIncKValue(node.Descriptor.Name, 1)));
						return;
					}
				}
			}
			else
			{
				if (!literalExpressionNode.Value.IsOfType<double>())
				{
					this.m_context.ErrorContext.Register(TranslationMessages.InvalidExpression(EngineMessageSeverity.Error, this.m_context.ObjectType, this.m_context.ObjectId, this.m_context.PropertyName, TranslationMessagePhrases.InvalidPercentileExcKValue(node.Descriptor.Name, 1)));
					return;
				}
				double num2 = literalExpressionNode.Value.CastValue<double>();
				if (num2 <= 0.0 || num2 >= 1.0)
				{
					this.m_context.ErrorContext.Register(TranslationMessages.InvalidExpression(EngineMessageSeverity.Error, this.m_context.ObjectType, this.m_context.ObjectId, this.m_context.PropertyName, TranslationMessagePhrases.InvalidPercentileExcKValue(node.Descriptor.Name, 1)));
					return;
				}
			}
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x00012438 File Offset: 0x00010638
		private void CheckFunctionRestriction(ExpressionFeatureFlags functionFlag, string functionName)
		{
			if (this.IsRestricted(functionFlag))
			{
				this.m_context.ErrorContext.Register(TranslationMessages.InvalidExpression(EngineMessageSeverity.Error, this.m_context.ObjectType, this.m_context.ObjectId, this.m_context.PropertyName, TranslationMessagePhrases.InvalidFunctionForExpression(functionName)));
			}
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x0001248C File Offset: 0x0001068C
		private void ValidateEvaluate(FunctionCallExpressionNode node)
		{
			if (node.Arguments.Count == 2)
			{
				FunctionCallExpressionNode functionCallExpressionNode = node.Arguments[1] as FunctionCallExpressionNode;
				if (functionCallExpressionNode != null && functionCallExpressionNode.Descriptor.Name == "Rollup")
				{
					this.CheckFunctionRestriction(ExpressionFeatureFlags.EvaluateWithRollup, "EvaluateWithRollup");
				}
				else if (functionCallExpressionNode != null && functionCallExpressionNode.Descriptor.Name == "Scope")
				{
					this.CheckFunctionRestriction(ExpressionFeatureFlags.EvaluateWithScope, "EvaluateWithScope");
				}
				else
				{
					this.CheckFunctionRestriction(ExpressionFeatureFlags.Evaluate, "Evaluate");
				}
				if ((functionCallExpressionNode == null || !(functionCallExpressionNode.Descriptor.Name == "Scope")) && base.ParentNodes.Count != 1)
				{
					FunctionCallExpressionNode parentFunction = this.GetParentFunction(1);
					if (parentFunction == null || parentFunction.Descriptor.FunctionCategory != FunctionCategory.Aggregate)
					{
						this.m_context.ErrorContext.Register(TranslationMessages.InvalidUsageOfEvaluateFunction(EngineMessageSeverity.Error, this.m_context.ObjectType, this.m_context.ObjectId, this.m_context.PropertyName));
					}
				}
			}
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x0001259C File Offset: 0x0001079C
		private void ValidateRollup(FunctionCallExpressionNode node)
		{
			if (!this.ValidateContextArgumentToEvaluate(node))
			{
				return;
			}
			if (node.Arguments.Count == 2)
			{
				ResolvedScopeReferenceExpressionNode resolvedScopeReferenceExpressionNode = node.Arguments[0] as ResolvedScopeReferenceExpressionNode;
				ResolvedScopeReferenceExpressionNode resolvedScopeReferenceExpressionNode2 = node.Arguments[1] as ResolvedScopeReferenceExpressionNode;
				if (resolvedScopeReferenceExpressionNode != null && resolvedScopeReferenceExpressionNode2 != null)
				{
					if (!this.m_scopeTree.IsParentScope(resolvedScopeReferenceExpressionNode2.Scope, resolvedScopeReferenceExpressionNode.Scope))
					{
						this.m_context.ErrorContext.Register(TranslationMessages.InvalidExpression(EngineMessageSeverity.Error, this.m_context.ObjectType, this.m_context.ObjectId, this.m_context.PropertyName, TranslationMessagePhrases.InvalidRollupScopes(resolvedScopeReferenceExpressionNode.Scope.Id.Value, resolvedScopeReferenceExpressionNode2.Scope.Id.Value)));
					}
					if (base.ParentNodes.Count == 2 && this.m_containingScope != resolvedScopeReferenceExpressionNode2.Scope)
					{
						this.m_context.ErrorContext.Register(TranslationMessages.InvalidExpression(EngineMessageSeverity.Error, this.m_context.ObjectType, this.m_context.ObjectId, this.m_context.PropertyName, TranslationMessagePhrases.InvalidRollupScopesInTopLevelEvaluate(this.m_containingScope.Id.Value, resolvedScopeReferenceExpressionNode2.Scope.Id.Value)));
					}
				}
			}
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x000126E4 File Offset: 0x000108E4
		private void ValidateScope(FunctionCallExpressionNode node)
		{
			if (!this.ValidateContextArgumentToEvaluate(node))
			{
				return;
			}
			HashSet<ResolvedPropertyExpressionNode> hashSet = new HashSet<ResolvedPropertyExpressionNode>();
			foreach (ResolvedPropertyExpressionNode resolvedPropertyExpressionNode in node.Arguments.OfType<ResolvedPropertyExpressionNode>())
			{
				if (!hashSet.Add(resolvedPropertyExpressionNode))
				{
					this.m_context.ErrorContext.Register(TranslationMessages.InvalidExpression(EngineMessageSeverity.Error, this.m_context.ObjectType, this.m_context.ObjectId, this.m_context.PropertyName, TranslationMessagePhrases.DuplicateArgumentToScopeFunction(TranslationMessageUtils.GetPropertyNameForError(resolvedPropertyExpressionNode))));
				}
			}
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x0001278C File Offset: 0x0001098C
		private bool ValidateContextArgumentToEvaluate(FunctionCallExpressionNode node)
		{
			FunctionCallExpressionNode parentFunction = this.GetParentFunction(1);
			if (parentFunction == null || parentFunction.Descriptor.Name != "Evaluate")
			{
				this.RegisterInvalidFunctionUsageError(node);
				return false;
			}
			if (parentFunction.Arguments.Count != 2)
			{
				return false;
			}
			if (!node.Equals(parentFunction.Arguments[1]))
			{
				this.RegisterInvalidFunctionUsageError(node);
				return false;
			}
			return true;
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x000127F4 File Offset: 0x000109F4
		private void RegisterInvalidFunctionUsageError(FunctionCallExpressionNode node)
		{
			this.m_context.ErrorContext.Register(TranslationMessages.InvalidExpression(EngineMessageSeverity.Error, this.m_context.ObjectType, this.m_context.ObjectId, this.m_context.PropertyName, TranslationMessagePhrases.InvalidUsageOfFunction(node.Descriptor.Name)));
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x00012848 File Offset: 0x00010A48
		private void ValidateArguments(FunctionCallExpressionNode node)
		{
			ReadOnlyCollection<ArgumentDescriptor> arguments = node.Descriptor.Arguments;
			int num = ((node.Arguments == null) ? 0 : node.Arguments.Count);
			for (int i = 0; i < arguments.Count; i++)
			{
				ArgumentDescriptor argumentDescriptor = arguments[i];
				if (argumentDescriptor.IsVarArg)
				{
					while (i < num)
					{
						this.ValidateArgument(node, argumentDescriptor, i);
						i++;
					}
					return;
				}
				if (i >= num)
				{
					this.RegisterInvalidArgumentCountError(node);
					return;
				}
				this.ValidateArgument(node, argumentDescriptor, i);
			}
			if (num > arguments.Count)
			{
				this.RegisterInvalidArgumentCountError(node);
				return;
			}
			if (node.Descriptor.Name == "Evaluate")
			{
				this.ValidateEvaluateArguments(node);
			}
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x000128F4 File Offset: 0x00010AF4
		private void ValidateEvaluateArguments(FunctionCallExpressionNode node)
		{
			ExpressionNode expressionNode = node.Arguments[1];
			if (expressionNode.Kind == ExpressionNodeKind.ResolvedScopeReference)
			{
				return;
			}
			if (expressionNode.Kind == ExpressionNodeKind.FunctionCall)
			{
				string name = ((FunctionCallExpressionNode)expressionNode).Descriptor.Name;
				if (name == "Rollup" || name == "Scope")
				{
					return;
				}
			}
			this.m_context.ErrorContext.Register(TranslationMessages.InvalidExpression(EngineMessageSeverity.Error, this.m_context.ObjectType, this.m_context.ObjectId, this.m_context.PropertyName, TranslationMessagePhrases.InvalidArgumentKindForFunction(node.Descriptor.Name, expressionNode.Kind.ToString(), 1)));
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x000129AC File Offset: 0x00010BAC
		private void ValidateArgument(FunctionCallExpressionNode node, ArgumentDescriptor argDescriptor, int argIndex)
		{
			ExpressionNodeKind kind = node.Arguments[argIndex].Kind;
			if (!argDescriptor.MatchesNodeKind(kind))
			{
				this.m_context.ErrorContext.Register(TranslationMessages.InvalidExpression(EngineMessageSeverity.Error, this.m_context.ObjectType, this.m_context.ObjectId, this.m_context.PropertyName, TranslationMessagePhrases.InvalidArgumentKindForFunction(node.Descriptor.Name, kind.ToString(), argIndex)));
			}
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x00012A2C File Offset: 0x00010C2C
		private void RegisterInvalidArgumentCountError(FunctionCallExpressionNode node)
		{
			if (node.Descriptor.Arguments.Any((ArgumentDescriptor a) => a.IsVarArg))
			{
				int num = node.Descriptor.Arguments.Count((ArgumentDescriptor a) => !a.IsVarArg);
				this.m_context.ErrorContext.Register(TranslationMessages.InvalidExpression(EngineMessageSeverity.Error, this.m_context.ObjectType, this.m_context.ObjectId, this.m_context.PropertyName, TranslationMessagePhrases.InvalidArgumentCountForFunction_VarArg(node.Descriptor.Name, num)));
				return;
			}
			this.m_context.ErrorContext.Register(TranslationMessages.InvalidExpression(EngineMessageSeverity.Error, this.m_context.ObjectType, this.m_context.ObjectId, this.m_context.PropertyName, TranslationMessagePhrases.InvalidArgumentCountForFunction(node.Descriptor.Name, node.Descriptor.Arguments.Count)));
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x00012B3C File Offset: 0x00010D3C
		private FunctionCallExpressionNode AssignFunctionUsageKind(FunctionCallExpressionNode node)
		{
			FunctionUsageKind functionUsageKind;
			if (node.Descriptor.CanBeHandledByQuery || node.Descriptor.CanBeHandledByProcessing)
			{
				functionUsageKind = FunctionUsageKind.Query;
				if (node.Descriptor.CanBeHandledByProcessing)
				{
					if (base.ParentNodes.Skip(1).All((ExpressionNode n) => n.Kind == ExpressionNodeKind.FunctionCall && ((FunctionCallExpressionNode)n).UsageKind == FunctionUsageKind.Processing))
					{
						functionUsageKind = FunctionUsageKind.Processing;
					}
				}
			}
			else
			{
				functionUsageKind = FunctionUsageKind.DataShapeQueryTranslation;
			}
			if (functionUsageKind == FunctionUsageKind.Processing && this.IsRestricted(ExpressionFeatureFlags.ProcessingFunctions))
			{
				this.m_context.ErrorContext.Register(TranslationMessages.InvalidExpression(EngineMessageSeverity.Error, this.m_context.ObjectType, this.m_context.ObjectId, this.m_context.PropertyName, TranslationMessagePhrases.InvalidFunctionForExpression(node.Descriptor.Name)));
			}
			node = new FunctionCallExpressionNode(node.Descriptor, functionUsageKind, node.Arguments);
			return base.ReplaceCurrentNode<FunctionCallExpressionNode>(node);
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x00012C1C File Offset: 0x00010E1C
		public override ExpressionNode Visit(ResolvedEntitySetExpressionNode node)
		{
			FunctionCallExpressionNode currentFunction = this.CurrentFunction;
			if (currentFunction == null || !(currentFunction.Descriptor.Name == "CountRows"))
			{
				this.m_context.ErrorContext.Register(TranslationMessages.InvalidExpression(EngineMessageSeverity.Error, this.m_context.ObjectType, this.m_context.ObjectId, this.m_context.PropertyName, TranslationMessagePhrases.EntitySetReferenceNotAllowed()));
			}
			return base.Visit(node);
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x00012C90 File Offset: 0x00010E90
		public override ExpressionNode Visit(ResolvedCalculationReferenceExpressionNode node)
		{
			if (this.IsRestricted(ExpressionFeatureFlags.CalculationReferences))
			{
				this.m_context.ErrorContext.Register(TranslationMessages.InvalidExpression(EngineMessageSeverity.Error, this.m_context.ObjectType, this.m_context.ObjectId, this.m_context.PropertyName, TranslationMessagePhrases.CalculationReferenceNotAllowed()));
			}
			else
			{
				if (this.m_referencedCalculations == null)
				{
					this.m_referencedCalculations = new List<Calculation>();
				}
				this.m_referencedCalculations.Add(node.Calculation);
			}
			return base.Visit(node);
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x00012D10 File Offset: 0x00010F10
		public override ExpressionNode Visit(ResolvedPropertyExpressionNode node)
		{
			if (this.IsRestricted(ExpressionFeatureFlags.BinaryOrImageFieldReference))
			{
				IConceptualProperty property = node.Property;
				if (property.IsImageProperty() && property.ConceptualDataCategory != ConceptualDataCategory.ImageUrl)
				{
					this.m_context.ErrorContext.Register(TranslationMessages.ImageOrBinaryFieldReferenceNotAllowed(EngineMessageSeverity.Error, this.m_context.ObjectType, this.m_context.ObjectId, this.m_context.PropertyName, TranslationMessageUtils.GetPropertyNameForError(node)));
				}
			}
			if (this.IsRestricted(ExpressionFeatureFlags.ModelReferences))
			{
				this.m_context.ErrorContext.Register(TranslationMessages.InvalidExpression(EngineMessageSeverity.Error, this.m_context.ObjectType, this.m_context.ObjectId, this.m_context.PropertyName, TranslationMessagePhrases.ModelReferenceNotAllowed(this.m_containingScope.ObjectType, this.m_containingScope.Id)));
			}
			return base.Visit(node);
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x00012DE4 File Offset: 0x00010FE4
		public override ExpressionNode Visit(ResolvedScopeReferenceExpressionNode node)
		{
			if (this.IsRestricted(ExpressionFeatureFlags.ScopeReference))
			{
				this.m_context.ErrorContext.Register(TranslationMessages.InvalidExpression(EngineMessageSeverity.Error, this.m_context.ObjectType, this.m_context.ObjectId, this.m_context.PropertyName, TranslationMessagePhrases.StructureReferenceNotAllowed(this.m_containingScope.ObjectType, this.m_containingScope.Id)));
			}
			return base.Visit(node);
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x00012E58 File Offset: 0x00011058
		public override ExpressionNode Visit(LiteralExpressionNode node)
		{
			if (node.Value.IsOfType<DateTime>())
			{
				ExpressionNodeSemanticValidator.ValidateDateTimeLiteral(node.Value.CastValue<DateTime>(), this.m_context.ErrorContext, this.m_context.ObjectType, this.m_context.ObjectId, this.m_context.PropertyName, this.m_daxCapabilitiesAnnotation);
			}
			return base.Visit(node);
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x00012EC1 File Offset: 0x000110C1
		public static void ValidateDateTimeLiteral(DateTime value, TranslationErrorContext errorContext, ObjectType objectType, Identifier objectId, string propertyName, DaxCapabilitiesAnnotation daxCapabilitiesAnnotation)
		{
			if (!DateTimeUtil.IsDateTimeSupportedInDAX(value, daxCapabilitiesAnnotation))
			{
				errorContext.Register(TranslationMessages.UnsupportedDateTimeLiteral(EngineMessageSeverity.Error, objectType, objectId, propertyName, value, daxCapabilitiesAnnotation));
			}
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x00012EE0 File Offset: 0x000110E0
		private bool IsRestricted(ExpressionFeatureFlags feature)
		{
			return !this.m_allowedFeatures.HasFlag(feature);
		}

		// Token: 0x0400028D RID: 653
		private readonly ExpressionFeatureFlags m_allowedFeatures;

		// Token: 0x0400028E RID: 654
		private readonly ExpressionContext m_context;

		// Token: 0x0400028F RID: 655
		private readonly ScopeTree m_scopeTree;

		// Token: 0x04000290 RID: 656
		private readonly IScope m_containingScope;

		// Token: 0x04000291 RID: 657
		private readonly ExpressionTable m_expressionTable;

		// Token: 0x04000292 RID: 658
		private readonly DataShapeAnnotations m_annotations;

		// Token: 0x04000293 RID: 659
		private readonly DaxCapabilitiesAnnotation m_daxCapabilitiesAnnotation;

		// Token: 0x04000294 RID: 660
		private List<Calculation> m_referencedCalculations;
	}
}
