using System;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.DataShaping;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;
using Microsoft.InfoNav.Utils;
using Microsoft.Reporting.QueryDesign;
using Microsoft.Reporting.QueryDesign.Edm.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.ExpressionBuilder.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;
using Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.ExtensionEdm;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.QueryGeneration
{
	// Token: 0x02000082 RID: 130
	internal abstract class BasicQueryExpressionTranslatorBase
	{
		// Token: 0x0600062B RID: 1579 RVA: 0x00016280 File Offset: 0x00014480
		internal BasicQueryExpressionTranslatorBase(ExpressionContext expressionContext, FederatedEntityDataModel model, IConceptualSchema schema, bool suppressModelGrouping, CancellationToken cancellationToken, IFeatureSwitchProvider featureSwitchProvider, bool ignoreFunctionUsageCheck = false)
		{
			this.m_model = model;
			this.m_schema = schema;
			this.m_expressionContext = expressionContext;
			this.m_suppressModelGrouping = suppressModelGrouping;
			this.m_cancellationToken = cancellationToken;
			this.m_ignoreFunctionUsageCheck = ignoreFunctionUsageCheck;
			this.m_featureSwitchProvider = featureSwitchProvider;
			this.m_useConceptualSchema = featureSwitchProvider.IsEnabled(FeatureSwitchKind.QDMConceptualSchema);
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x000162D8 File Offset: 0x000144D8
		protected QueryExpressionContext Translate(ExpressionNode expressionNode)
		{
			QueryExpressionFeatures queryExpressionFeatures;
			QueryExpression queryExpression = this.Translate(expressionNode, out queryExpressionFeatures);
			if (queryExpressionFeatures.HasFlag(QueryExpressionFeatures.RequiresCalculate) && !(queryExpression is QueryCalculateExpression))
			{
				queryExpression = queryExpression.Calculate(Array.Empty<QueryExpression>());
			}
			return new QueryExpressionContext(queryExpression, queryExpressionFeatures, false);
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x00016320 File Offset: 0x00014520
		protected virtual QueryExpression Translate(ExpressionNode node, out QueryExpressionFeatures features)
		{
			ExpressionNodeKind kind = node.Kind;
			if (kind <= ExpressionNodeKind.Literal)
			{
				if (kind == ExpressionNodeKind.BinaryOperator)
				{
					return this.TranslateBinaryOperator((BinaryOperatorExpressionNode)node, out features);
				}
				if (kind == ExpressionNodeKind.FunctionCall)
				{
					return this.TranslateFunctionCall((FunctionCallExpressionNode)node, out features);
				}
				if (kind == ExpressionNodeKind.Literal)
				{
					features = QueryExpressionFeatures.None;
					return this.TranslateLiteral((LiteralExpressionNode)node, out features);
				}
			}
			else if (kind <= ExpressionNodeKind.ResolvedProperty)
			{
				if (kind == ExpressionNodeKind.RelatedResolvedProperty)
				{
					return this.TranslateRelatedColumn((RelatedResolvedPropertyExpressionNode)node, out features);
				}
				if (kind == ExpressionNodeKind.ResolvedProperty)
				{
					return this.TranslateProperty((ResolvedPropertyExpressionNode)node, out features);
				}
			}
			else
			{
				if (kind == ExpressionNodeKind.UnaryOperator)
				{
					return this.TranslateUnaryOperator((UnaryOperatorExpressionNode)node, out features);
				}
				if (kind == ExpressionNodeKind.VisualCalculation)
				{
					return this.TranslateVisualCalculation((VisualCalculationExpressionNode)node, out features);
				}
			}
			throw BasicQueryExpressionTranslatorBase.SyntaxError(node);
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x000163D4 File Offset: 0x000145D4
		private QueryExpression TranslateVisualCalculation(VisualCalculationExpressionNode vcNode, out QueryExpressionFeatures features)
		{
			features = QueryExpressionFeatures.None;
			DaxTextExpressionNode daxTextExpressionNode = (DaxTextExpressionNode)vcNode.Expression;
			return new QueryDaxTextExpression(ConceptualPrimitiveResultType.Variant, daxTextExpressionNode.Text);
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x00016400 File Offset: 0x00014600
		private QueryExpression TranslateProperty(ResolvedPropertyExpressionNode propertyNode, out QueryExpressionFeatures features)
		{
			Microsoft.DataShaping.Contract.RetailAssert(propertyNode.Property != null, "propertyNode.Property is expected to not be null");
			IConceptualProperty property = propertyNode.Property;
			IConceptualColumn conceptualColumn = property as IConceptualColumn;
			if (conceptualColumn != null)
			{
				features = QueryExpressionFeatures.FieldReference;
				if (this.m_useConceptualSchema)
				{
					return property.Entity.QdmReference().Field(conceptualColumn);
				}
				EntitySet entitySet;
				EdmField correspondingEdmField = this.m_model.GetCorrespondingEdmField(property, out entitySet);
				return entitySet.QdmReference(null).Field(correspondingEdmField, null);
			}
			else
			{
				IConceptualMeasure conceptualMeasure = property as IConceptualMeasure;
				if (conceptualMeasure == null)
				{
					throw BasicQueryExpressionTranslatorBase.TranslationError(SR.InvalidPropertyReference(property.EdmName.MarkAsModelInfo()));
				}
				features = QueryExpressionFeatures.ModelMeasure;
				if (this.m_useConceptualSchema)
				{
					return conceptualMeasure.Entity.InvokeMeasure(conceptualMeasure);
				}
				EntitySet entitySet2;
				EdmMeasure correspondingEdmMeasure = this.m_model.GetCorrespondingEdmMeasure(property, out entitySet2);
				return entitySet2.InvokeMeasure(correspondingEdmMeasure, null, null);
			}
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x000164C4 File Offset: 0x000146C4
		private QueryExpression TranslateLiteral(LiteralExpressionNode literalNode, out QueryExpressionFeatures features)
		{
			features = QueryExpressionFeatures.None;
			if (!(literalNode.Value == ScalarValue.Null))
			{
				return QueryExpressionBuilder.Literal(literalNode.Value);
			}
			ConceptualPrimitiveResultType type = literalNode.GetType();
			if (type == null)
			{
				throw BasicQueryExpressionTranslatorBase.TranslationError(TranslationMessages.InvalidLiteralDataType(EngineMessageSeverity.Error, this.m_expressionContext.ObjectType, this.m_expressionContext.ObjectId, this.m_expressionContext.PropertyName, "undefined", "any defined type").Message);
			}
			return type.Null();
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x0001653C File Offset: 0x0001473C
		private QueryExpression TranslateBinaryOperator(BinaryOperatorExpressionNode operatorNode, out QueryExpressionFeatures features)
		{
			QueryExpressionFeatures queryExpressionFeatures;
			QueryExpression queryExpression = this.Translate(operatorNode.Left, out queryExpressionFeatures);
			QueryExpressionFeatures queryExpressionFeatures2;
			QueryExpression queryExpression2 = this.Translate(operatorNode.Right, out queryExpressionFeatures2);
			features = queryExpressionFeatures | queryExpressionFeatures2;
			switch (operatorNode.OperatorKind)
			{
			case BinaryOperatorKind.Add:
				return queryExpression.Plus(queryExpression2);
			case BinaryOperatorKind.And:
				return queryExpression.And(queryExpression2);
			case BinaryOperatorKind.Divide:
				return queryExpression.Divide(queryExpression2);
			case BinaryOperatorKind.Equal:
				return queryExpression.Equal(queryExpression2);
			case BinaryOperatorKind.GreaterThan:
				return queryExpression.GreaterThan(queryExpression2);
			case BinaryOperatorKind.GreaterThanOrEqual:
				return queryExpression.GreaterThanOrEqual(queryExpression2);
			case BinaryOperatorKind.LessThan:
				return queryExpression.LessThan(queryExpression2);
			case BinaryOperatorKind.LessThanOrEqual:
				return queryExpression.LessThanOrEqual(queryExpression2);
			case BinaryOperatorKind.Multiply:
				return queryExpression.Multiply(queryExpression2);
			case BinaryOperatorKind.Or:
				return queryExpression.Or(queryExpression2);
			case BinaryOperatorKind.Subtract:
				return queryExpression.Minus(queryExpression2);
			default:
				throw BasicQueryExpressionTranslatorBase.TranslationError(TranslationMessagePhrases.InvalidOperator(operatorNode.OperatorKind.ToString()));
			}
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x00016628 File Offset: 0x00014828
		private QueryExpression TranslateUnaryOperator(UnaryOperatorExpressionNode operatorNode, out QueryExpressionFeatures features)
		{
			QueryExpression queryExpression = this.Translate(operatorNode.Operand, out features);
			UnaryOperatorKind operatorKind = operatorNode.OperatorKind;
			if (operatorKind == UnaryOperatorKind.Not)
			{
				return queryExpression.Not();
			}
			if (operatorKind != UnaryOperatorKind.Minus)
			{
				throw BasicQueryExpressionTranslatorBase.TranslationError(TranslationMessagePhrases.InvalidOperator(operatorNode.OperatorKind.ToString()));
			}
			return queryExpression.MinusSign();
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x00016684 File Offset: 0x00014884
		private QueryExpression TranslateRelatedColumn(RelatedResolvedPropertyExpressionNode relatedPropertyNode, out QueryExpressionFeatures features)
		{
			IConceptualProperty property = relatedPropertyNode.Property;
			if (this.m_useConceptualSchema)
			{
				features = QueryExpressionFeatures.FieldReference;
				if (property != null)
				{
					return QueryExpressionBuilder.RelatedColumn((IConceptualColumn)property);
				}
			}
			else
			{
				EdmFieldInstance correspondingEdmFieldInstance = this.m_model.GetCorrespondingEdmFieldInstance(property);
				if (correspondingEdmFieldInstance.IsValid)
				{
					features = QueryExpressionFeatures.FieldReference;
					return QueryExpressionBuilder.RelatedColumn(correspondingEdmFieldInstance, null);
				}
			}
			throw BasicQueryExpressionTranslatorBase.TranslationError(SR.InvalidPropertyReference(property.EdmName.MarkAsModelInfo()));
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x000166E8 File Offset: 0x000148E8
		protected virtual QueryExpression TranslateFunctionCall(FunctionCallExpressionNode functionCallNode, out QueryExpressionFeatures features)
		{
			if (!this.m_ignoreFunctionUsageCheck)
			{
				this.CheckQueryFunctionUsage(functionCallNode);
			}
			string name = functionCallNode.Descriptor.Name;
			uint num = global::<PrivateImplementationDetails>.ComputeStringHash(name);
			if (num <= 675462676U)
			{
				if (num != 477688755U)
				{
					if (num != 480618406U)
					{
						if (num == 675462676U)
						{
							if (name == "IsNull")
							{
								return this.TranslateIsNullFunctionCall(functionCallNode, out features);
							}
						}
					}
					else if (name == "MaxValue")
					{
						Func<QueryExpression, QueryExpression, QueryFunctionExpression> func;
						if ((func = BasicQueryExpressionTranslatorBase.<>O.<1>__MaxValue) == null)
						{
							func = (BasicQueryExpressionTranslatorBase.<>O.<1>__MaxValue = new Func<QueryExpression, QueryExpression, QueryFunctionExpression>(CoreFunctions.MaxValue));
						}
						return this.TranslateMinMaxValueFunctionCall(functionCallNode, func, out features);
					}
				}
				else if (name == "IsZero")
				{
					return this.TranslateIsZeroFunctionCall(functionCallNode, out features);
				}
			}
			else if (num <= 2602075852U)
			{
				if (num != 1640800505U)
				{
					if (num == 2602075852U)
					{
						if (name == "MinValue")
						{
							Func<QueryExpression, QueryExpression, QueryFunctionExpression> func2;
							if ((func2 = BasicQueryExpressionTranslatorBase.<>O.<0>__MinValue) == null)
							{
								func2 = (BasicQueryExpressionTranslatorBase.<>O.<0>__MinValue = new Func<QueryExpression, QueryExpression, QueryFunctionExpression>(CoreFunctions.MinValue));
							}
							return this.TranslateMinMaxValueFunctionCall(functionCallNode, func2, out features);
						}
					}
				}
				else if (name == "CountRows")
				{
					return this.TranslateCountRowsFunctionCall(functionCallNode, out features);
				}
			}
			else if (num != 3790059668U)
			{
				if (num == 4289789810U)
				{
					if (name == "Format")
					{
						return this.TranslateFormatFunctionCall(functionCallNode, out features);
					}
				}
			}
			else if (name == "Count")
			{
				return this.TranslateCount(functionCallNode, out features);
			}
			return this.TranslateCoreFunctionCall(functionCallNode, out features);
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x0001686C File Offset: 0x00014A6C
		private QueryExpression TranslateMinMaxValueFunctionCall(FunctionCallExpressionNode functionCallNode, Func<QueryExpression, QueryExpression, QueryFunctionExpression> createExpressionFunc, out QueryExpressionFeatures features)
		{
			ReadOnlyCollection<ExpressionNode> arguments = functionCallNode.Arguments;
			if (arguments == null || arguments.Count != 2)
			{
				throw BasicQueryExpressionTranslatorBase.CannotInvokeFunction(functionCallNode, null);
			}
			QueryExpressionFeatures queryExpressionFeatures;
			QueryExpression queryExpression = this.Translate(functionCallNode.Arguments[0], out queryExpressionFeatures);
			QueryExpressionFeatures queryExpressionFeatures2;
			QueryExpression queryExpression2 = this.Translate(functionCallNode.Arguments[1], out queryExpressionFeatures2);
			features = queryExpressionFeatures | queryExpressionFeatures2;
			return createExpressionFunc(queryExpression, queryExpression2);
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x000168D0 File Offset: 0x00014AD0
		private QueryExpression TranslateIsNullFunctionCall(FunctionCallExpressionNode functionCallNode, out QueryExpressionFeatures features)
		{
			ReadOnlyCollection<ExpressionNode> arguments = functionCallNode.Arguments;
			if (arguments == null || arguments.Count != 1)
			{
				throw BasicQueryExpressionTranslatorBase.CannotInvokeFunction(functionCallNode, null);
			}
			return this.Translate(functionCallNode.Arguments[0], out features).IsNull();
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x00016910 File Offset: 0x00014B10
		private QueryExpression TranslateIsZeroFunctionCall(FunctionCallExpressionNode functionCallNode, out QueryExpressionFeatures features)
		{
			ReadOnlyCollection<ExpressionNode> arguments = functionCallNode.Arguments;
			if (arguments == null || arguments.Count != 2)
			{
				throw BasicQueryExpressionTranslatorBase.CannotInvokeFunction(functionCallNode, null);
			}
			QueryExpression queryExpression = this.Translate(functionCallNode.Arguments[0], out features);
			if (queryExpression.ConceptualResultType == ConceptualPrimitiveResultType.Variant)
			{
				QueryExpression queryExpression2 = queryExpression.Equal(Literals.NullVariant);
				if (((LiteralExpressionNode)arguments[1]).Value.CastValue<bool>())
				{
					queryExpression2 = queryExpression2.And(queryExpression.IsNull().Not());
				}
				return queryExpression2;
			}
			return queryExpression.Equal(Literals.GetTypedZero(queryExpression.ConceptualResultType));
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x000169A8 File Offset: 0x00014BA8
		private QueryExpression TranslateFormatFunctionCall(FunctionCallExpressionNode functionCallNode, out QueryExpressionFeatures features)
		{
			ReadOnlyCollection<ExpressionNode> arguments = functionCallNode.Arguments;
			if (arguments == null || arguments.Count != 2)
			{
				throw BasicQueryExpressionTranslatorBase.CannotInvokeFunction(functionCallNode, null);
			}
			LiteralExpressionNode literalExpressionNode = functionCallNode.Arguments[1] as LiteralExpressionNode;
			if (literalExpressionNode != null)
			{
				string text = literalExpressionNode.Value.Value as string;
				if (text != null)
				{
					return this.Translate(functionCallNode.Arguments[0], out features).Format(text, null);
				}
			}
			throw BasicQueryExpressionTranslatorBase.SyntaxError(functionCallNode.Arguments[1]);
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x00016A28 File Offset: 0x00014C28
		private QueryExpression TranslateCoreFunctionCall(FunctionCallExpressionNode functionCallNode, out QueryExpressionFeatures features)
		{
			features = QueryExpressionFeatures.QueryMeasure;
			ReadOnlyCollection<ExpressionNode> arguments = functionCallNode.Arguments;
			QueryExpression[] array = new QueryExpression[arguments.Count];
			bool flag = functionCallNode.Descriptor.FunctionCategory == FunctionCategory.Aggregate;
			for (int i = 0; i < arguments.Count; i++)
			{
				ExpressionNode expressionNode = arguments[i];
				QueryExpressionFeatures queryExpressionFeatures;
				array[i] = (flag ? this.TranslateAggregateArgument(expressionNode, out queryExpressionFeatures) : this.Translate(expressionNode, out queryExpressionFeatures));
				features |= queryExpressionFeatures;
			}
			if (flag)
			{
				features = BasicQueryExpressionTranslatorBase.UpdateFeaturesForAggregateFunction(features);
			}
			QueryExpression queryExpression;
			try
			{
				queryExpression = CoreFunctions.InvokeFunction(functionCallNode.Descriptor.BackingFunctionName, array);
			}
			catch (QueryFunctionInvocationException ex)
			{
				throw BasicQueryExpressionTranslatorBase.CannotInvokeFunction(functionCallNode, ex);
			}
			return queryExpression;
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x00016AD8 File Offset: 0x00014CD8
		protected void CheckQueryFunctionUsage(FunctionCallExpressionNode function)
		{
			if (function.UsageKind != FunctionUsageKind.Query)
			{
				throw BasicQueryExpressionTranslatorBase.CannotInvokeFunction(function, null);
			}
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x00016AEC File Offset: 0x00014CEC
		protected virtual QueryExpression TranslateCountRowsFunctionCall(FunctionCallExpressionNode functionCallNode, out QueryExpressionFeatures features)
		{
			features = QueryExpressionFeatures.QueryMeasure | QueryExpressionFeatures.RequiresCalculate;
			EntitySet entitySet;
			IConceptualEntity conceptualEntity;
			this.GetCountRowsEntitySetArg(functionCallNode, out entitySet, out conceptualEntity);
			bool countRowsExcludeBlankRowArg = BasicQueryExpressionTranslatorBase.GetCountRowsExcludeBlankRowArg(functionCallNode);
			if (this.m_useConceptualSchema)
			{
				return conceptualEntity.Scan(countRowsExcludeBlankRowArg).CountRows();
			}
			return entitySet.Scan(countRowsExcludeBlankRowArg).CountRows();
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x00016B30 File Offset: 0x00014D30
		private void GetCountRowsEntitySetArg(FunctionCallExpressionNode functionCallNode, out EntitySet entitySet, out IConceptualEntity entity)
		{
			ReadOnlyCollection<ExpressionNode> arguments = functionCallNode.Arguments;
			if (arguments == null || arguments.Count < 1 || !(arguments[0] is ResolvedEntitySetExpressionNode))
			{
				throw BasicQueryExpressionTranslatorBase.CannotInvokeFunction(functionCallNode, null);
			}
			ResolvedEntitySetExpressionNode resolvedEntitySetExpressionNode = (ResolvedEntitySetExpressionNode)arguments[0];
			entity = resolvedEntitySetExpressionNode.Entity;
			FederatedEntityDataModel model = this.m_model;
			entitySet = ((model != null) ? model.GetCorrespondingEntitySet(entity) : null);
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x00016B94 File Offset: 0x00014D94
		private static bool GetCountRowsExcludeBlankRowArg(FunctionCallExpressionNode functionCallNode)
		{
			ReadOnlyCollection<ExpressionNode> arguments = functionCallNode.Arguments;
			if (arguments == null || arguments.Count < 2)
			{
				return false;
			}
			LiteralExpressionNode literalExpressionNode = arguments[1] as LiteralExpressionNode;
			if (literalExpressionNode == null || !literalExpressionNode.Value.IsOfType<bool>())
			{
				throw BasicQueryExpressionTranslatorBase.CannotInvokeFunction(functionCallNode, null);
			}
			return literalExpressionNode.Value.CastValue<bool>();
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x00016BEC File Offset: 0x00014DEC
		private QueryExpression TranslateCount(FunctionCallExpressionNode functionCallNode, out QueryExpressionFeatures features)
		{
			if (!this.ShouldTranslateCountAsCountRows(functionCallNode))
			{
				return this.TranslateCoreFunctionCall(functionCallNode, out features);
			}
			ResolvedPropertyExpressionNode resolvedPropertyExpressionNode = (ResolvedPropertyExpressionNode)functionCallNode.Arguments[0];
			Microsoft.DataShaping.Contract.RetailAssert(resolvedPropertyExpressionNode.Property != null, "propertyNode.Property is expected to not be null");
			IConceptualProperty property = resolvedPropertyExpressionNode.Property;
			IConceptualEntity entity = property.Entity;
			if (this.m_useConceptualSchema)
			{
				IConceptualColumn conceptualColumn = property.AsColumn();
				if (conceptualColumn == null || !conceptualColumn.Grouping.IsIdentityOnEntityKey)
				{
					return this.TranslateCoreFunctionCall(functionCallNode, out features);
				}
				features = QueryExpressionFeatures.QueryMeasure | QueryExpressionFeatures.RequiresCalculate;
				return entity.Scan(true).CountRows();
			}
			else
			{
				EntitySet entitySet;
				EdmField correspondingEdmField = this.m_model.GetCorrespondingEdmField(property, out entitySet);
				EdmFieldInstance edmFieldInstance = entitySet.FieldInstance(correspondingEdmField);
				if (!edmFieldInstance.IsValid || !edmFieldInstance.Field.Grouping.IsIdentityOnEntityKey)
				{
					return this.TranslateCoreFunctionCall(functionCallNode, out features);
				}
				features = QueryExpressionFeatures.QueryMeasure | QueryExpressionFeatures.RequiresCalculate;
				return entitySet.Scan(true).CountRows();
			}
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x00016CC4 File Offset: 0x00014EC4
		private bool ShouldTranslateCountAsCountRows(FunctionCallExpressionNode functionCallNode)
		{
			return !this.m_suppressModelGrouping && functionCallNode.Arguments.Count == 1 && functionCallNode.Arguments[0] is ResolvedPropertyExpressionNode;
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x00016CF8 File Offset: 0x00014EF8
		protected virtual QueryExpression TranslateAggregateArgument(ExpressionNode node, out QueryExpressionFeatures features)
		{
			ExpressionNodeKind kind = node.Kind;
			if (kind <= ExpressionNodeKind.FunctionCall)
			{
				if (kind == ExpressionNodeKind.BinaryOperator)
				{
					return this.TranslateBinaryOperator((BinaryOperatorExpressionNode)node, out features);
				}
				if (kind == ExpressionNodeKind.FunctionCall)
				{
					return this.TranslateFunctionCall((FunctionCallExpressionNode)node, out features);
				}
			}
			else
			{
				if (kind == ExpressionNodeKind.Literal)
				{
					return this.TranslateLiteral((LiteralExpressionNode)node, out features);
				}
				if (kind == ExpressionNodeKind.ResolvedProperty)
				{
					return this.TranslatePropertyAccessAggregateArgument((ResolvedPropertyExpressionNode)node, out features);
				}
			}
			throw BasicQueryExpressionTranslatorBase.SyntaxError(node);
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x00016D68 File Offset: 0x00014F68
		private QueryExpression TranslatePropertyAccessAggregateArgument(ResolvedPropertyExpressionNode propertyNode, out QueryExpressionFeatures features)
		{
			features = QueryExpressionFeatures.FieldReference;
			IConceptualProperty property = propertyNode.Property;
			if (this.m_useConceptualSchema)
			{
				return property.AsColumn().QdmAggregateArgument();
			}
			EdmFieldInstance correspondingEdmFieldInstance = this.m_model.GetCorrespondingEdmFieldInstance(property);
			if (!correspondingEdmFieldInstance.IsValid)
			{
				throw BasicQueryExpressionTranslatorBase.TranslationError(SR.InvalidFieldReference(propertyNode.Property.EdmName.MarkAsModelInfo()));
			}
			return correspondingEdmFieldInstance.QdmAggregateArgument(null);
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x00016DD0 File Offset: 0x00014FD0
		protected static QueryExpressionFeatures UpdateFeaturesForAggregateFunction(QueryExpressionFeatures features)
		{
			if (features.HasFlag(QueryExpressionFeatures.FieldReference))
			{
				features |= QueryExpressionFeatures.RequiresCalculate;
			}
			return features;
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x00016DEC File Offset: 0x00014FEC
		protected static Exception CannotInvokeFunction(FunctionCallExpressionNode functionCallNode, Exception innerException = null)
		{
			string text = TranslationMessagePhrases.CannotInvokeFunction(functionCallNode.Descriptor.Name);
			if (innerException != null)
			{
				return new ExpressionTranslationException(text, innerException);
			}
			return new ExpressionTranslationException(text);
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x00016E20 File Offset: 0x00015020
		protected static Exception SyntaxError(ExpressionNode node)
		{
			return new ExpressionTranslationException(TranslationMessagePhrases.InvalidExpressionSyntax(node.Kind.ToString()));
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x00016E50 File Offset: 0x00015050
		protected static Exception TranslationError(string message)
		{
			return new ExpressionTranslationException(message);
		}

		// Token: 0x04000313 RID: 787
		protected readonly ExpressionContext m_expressionContext;

		// Token: 0x04000314 RID: 788
		protected readonly bool m_suppressModelGrouping;

		// Token: 0x04000315 RID: 789
		protected readonly FederatedEntityDataModel m_model;

		// Token: 0x04000316 RID: 790
		protected readonly IConceptualSchema m_schema;

		// Token: 0x04000317 RID: 791
		protected readonly IFeatureSwitchProvider m_featureSwitchProvider;

		// Token: 0x04000318 RID: 792
		protected readonly CancellationToken m_cancellationToken;

		// Token: 0x04000319 RID: 793
		protected readonly bool m_useConceptualSchema;

		// Token: 0x0400031A RID: 794
		protected readonly bool m_ignoreFunctionUsageCheck;

		// Token: 0x02000292 RID: 658
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040009FD RID: 2557
			public static Func<QueryExpression, QueryExpression, QueryFunctionExpression> <0>__MinValue;

			// Token: 0x040009FE RID: 2558
			public static Func<QueryExpression, QueryExpression, QueryFunctionExpression> <1>__MaxValue;
		}
	}
}
