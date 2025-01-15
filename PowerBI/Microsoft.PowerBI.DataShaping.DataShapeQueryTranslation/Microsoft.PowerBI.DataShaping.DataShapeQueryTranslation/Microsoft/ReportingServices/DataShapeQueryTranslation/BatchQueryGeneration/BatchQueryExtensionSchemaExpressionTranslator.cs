using System;
using System.Threading;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav;
using Microsoft.Reporting.QueryDesign.BatchQueries;
using Microsoft.Reporting.QueryDesign.Edm.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.ExpressionBuilder.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;
using Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.ExtensionEdm;
using Microsoft.ReportingServices.DataShapeQueryTranslation.QueryGeneration;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchQueryGeneration
{
	// Token: 0x0200013B RID: 315
	internal sealed class BatchQueryExtensionSchemaExpressionTranslator : BasicQueryExpressionTranslatorBase
	{
		// Token: 0x06000BAD RID: 2989 RVA: 0x0002ECEF File Offset: 0x0002CEEF
		private BatchQueryExtensionSchemaExpressionTranslator(ExpressionContext expressionContext, bool suppressModelGrouping, DaxCapabilities daxCapabilities, BatchQueryBuilder batchQueryBuilder, FederatedEntityDataModel model, IConceptualSchema schema, CancellationToken cancellationToken, IFeatureSwitchProvider featureSwitchProvider)
			: base(expressionContext, model, schema, suppressModelGrouping, cancellationToken, featureSwitchProvider, false)
		{
			this.m_daxCapabilities = daxCapabilities;
			this.m_queryBuilder = batchQueryBuilder;
		}

		// Token: 0x06000BAE RID: 2990 RVA: 0x0002ED11 File Offset: 0x0002CF11
		public static QueryExpressionContext Translate(ExpressionNode expressionNode, ExpressionContext expressionContext, bool suppressModelGrouping, DaxCapabilities daxCapabilities, BatchQueryBuilder batchQueryBuilder, FederatedEntityDataModel model, IConceptualSchema schema, CancellationToken cancellationToken, IFeatureSwitchProvider featureSwitchProvider)
		{
			return new BatchQueryExtensionSchemaExpressionTranslator(expressionContext, suppressModelGrouping, daxCapabilities, batchQueryBuilder, model, schema, cancellationToken, featureSwitchProvider).Translate(expressionNode);
		}

		// Token: 0x06000BAF RID: 2991 RVA: 0x0002ED2C File Offset: 0x0002CF2C
		protected override QueryExpression TranslateFunctionCall(FunctionCallExpressionNode functionCallNode, out QueryExpressionFeatures features)
		{
			string name = functionCallNode.Descriptor.Name;
			if (name == "BinColumnMin")
			{
				return this.TranslateBinColumnMinFunctionCall(functionCallNode, out features);
			}
			if (!(name == "BinColumnMax"))
			{
				return base.TranslateFunctionCall(functionCallNode, out features);
			}
			return this.TranslateBinColumnMaxFunctionCall(functionCallNode, out features);
		}

		// Token: 0x06000BB0 RID: 2992 RVA: 0x0002ED7C File Offset: 0x0002CF7C
		private QueryExpression TranslateBinColumnMinFunctionCall(FunctionCallExpressionNode functionCallNode, out QueryExpressionFeatures features)
		{
			Microsoft.DataShaping.Contract.RetailAssert(this.m_daxCapabilities.IsSupported(ModelCapabilitiesKind.VirtualColumns), "VirtualColumns are not supported. Early validation should have taken care of that.");
			features = QueryExpressionFeatures.None;
			ResolvedPropertyExpressionNode resolvedPropertyExpressionNode = (ResolvedPropertyExpressionNode)functionCallNode.Arguments[0];
			ExpressionNode expressionNode = functionCallNode.Arguments[1];
			QueryExpression queryExpression2;
			QueryExpression queryExpression3;
			QueryExpression queryExpression = this.GenerateBinSize(resolvedPropertyExpressionNode, expressionNode, out queryExpression2, out queryExpression3);
			return this.GenerateBinMin(resolvedPropertyExpressionNode, expressionNode, queryExpression, queryExpression2, queryExpression3);
		}

		// Token: 0x06000BB1 RID: 2993 RVA: 0x0002EDDC File Offset: 0x0002CFDC
		private QueryExpression TranslateBinColumnMaxFunctionCall(FunctionCallExpressionNode functionCallNode, out QueryExpressionFeatures features)
		{
			Microsoft.DataShaping.Contract.RetailAssert(this.m_daxCapabilities.IsSupported(ModelCapabilitiesKind.VirtualColumns), "VirtualColumns are not supported. Early validation should have taken care of that.");
			features = QueryExpressionFeatures.None;
			ResolvedPropertyExpressionNode resolvedPropertyExpressionNode = (ResolvedPropertyExpressionNode)functionCallNode.Arguments[0];
			ExpressionNode expressionNode = functionCallNode.Arguments[1];
			QueryExpression queryExpression2;
			QueryExpression queryExpression3;
			QueryExpression queryExpression = this.GenerateBinSize(resolvedPropertyExpressionNode, expressionNode, out queryExpression2, out queryExpression3);
			QueryExpression queryExpression4;
			if (functionCallNode.Arguments.Count == 3)
			{
				ResolvedPropertyExpressionNode resolvedPropertyExpressionNode2 = (ResolvedPropertyExpressionNode)functionCallNode.Arguments[2];
				Microsoft.DataShaping.Contract.RetailAssert(resolvedPropertyExpressionNode2.Property != null, "binMinColumn.Property is expected to not be null");
				IConceptualColumn conceptualColumn = (IConceptualColumn)resolvedPropertyExpressionNode2.Property;
				if (this.m_useConceptualSchema)
				{
					queryExpression4 = conceptualColumn.Entity.ScalarEntity().Field(conceptualColumn);
				}
				else
				{
					EdmFieldInstance correspondingEdmFieldInstance = this.m_model.GetCorrespondingEdmFieldInstance(conceptualColumn);
					queryExpression4 = correspondingEdmFieldInstance.Entity.ScalarEntity(null).Field(correspondingEdmFieldInstance.Field, null);
				}
			}
			else
			{
				queryExpression4 = this.GenerateBinMin(resolvedPropertyExpressionNode, expressionNode, queryExpression, queryExpression2, queryExpression3);
			}
			return queryExpression4.Plus(queryExpression);
		}

		// Token: 0x06000BB2 RID: 2994 RVA: 0x0002EED4 File Offset: 0x0002D0D4
		private QueryExpression GenerateBinMin(ResolvedPropertyExpressionNode targetColumnArgument, ExpressionNode countArgument, QueryExpression size, QueryExpression binCount, QueryExpression minOfColumn)
		{
			Microsoft.DataShaping.Contract.RetailAssert(targetColumnArgument.Property != null, "targetColumnArgument.Property is expected to not be null");
			IConceptualColumn conceptualColumn = (IConceptualColumn)targetColumnArgument.Property;
			QueryFieldExpression queryFieldExpression;
			if (this.m_useConceptualSchema)
			{
				queryFieldExpression = conceptualColumn.Entity.ScalarEntity().Field(conceptualColumn);
			}
			else
			{
				EdmFieldInstance correspondingEdmFieldInstance = this.m_model.GetCorrespondingEdmFieldInstance(conceptualColumn);
				queryFieldExpression = correspondingEdmFieldInstance.Entity.ScalarEntity(null).Field(correspondingEdmFieldInstance.Field, null);
			}
			QueryFunctionExpression queryFunctionExpression = queryFieldExpression.Minus(minOfColumn).DivideRaw(size).Int();
			return queryFieldExpression.IsNull().If(queryFieldExpression.ConceptualResultType.Null(), minOfColumn.Plus(queryFunctionExpression.MinValue(binCount.Minus(Literals.OneInt64)).Multiply(size)));
		}

		// Token: 0x06000BB3 RID: 2995 RVA: 0x0002EF90 File Offset: 0x0002D190
		private QueryExpression GenerateBinSize(ResolvedPropertyExpressionNode targetColumnArgument, ExpressionNode countArgument, out QueryExpression binCount, out QueryExpression minOfColumn)
		{
			QueryExpression queryExpression = base.Translate(countArgument).QueryExpression;
			IConceptualProperty property = targetColumnArgument.Property;
			IConceptualColumn conceptualColumn = (this.m_useConceptualSchema ? property.AsColumn() : null);
			string edmName = property.EdmName;
			QueryExpression queryExpression2 = (this.m_useConceptualSchema ? conceptualColumn.QdmAggregateArgument() : this.m_model.GetCorrespondingEdmFieldInstance(property).QdmAggregateArgument(conceptualColumn));
			binCount = this.m_queryBuilder.DeclareVariable(queryExpression, BatchQueryExtensionSchemaExpressionTranslator.Names.BinCount(edmName));
			minOfColumn = this.m_queryBuilder.DeclareVariable(queryExpression2.Min(), BatchQueryExtensionSchemaExpressionTranslator.Names.Min(edmName));
			QueryVariableReferenceExpression queryVariableReferenceExpression = this.m_queryBuilder.DeclareVariable(queryExpression2.Max().Minus(minOfColumn), BatchQueryExtensionSchemaExpressionTranslator.Names.Difference(edmName));
			return this.m_queryBuilder.DeclareVariable(queryVariableReferenceExpression.GreaterThan(Literals.ZeroInt64).If(queryVariableReferenceExpression.DivideRaw(binCount), Literals.OneInt64), BatchQueryExtensionSchemaExpressionTranslator.Names.BinSize(edmName));
		}

		// Token: 0x040005E7 RID: 1511
		private DaxCapabilities m_daxCapabilities;

		// Token: 0x040005E8 RID: 1512
		private BatchQueryBuilder m_queryBuilder;

		// Token: 0x020002DC RID: 732
		private static class Names
		{
			// Token: 0x06001684 RID: 5764 RVA: 0x00051B24 File Offset: 0x0004FD24
			internal static string BinCount(string targetColumnName)
			{
				return "BinCount" + targetColumnName;
			}

			// Token: 0x06001685 RID: 5765 RVA: 0x00051B31 File Offset: 0x0004FD31
			internal static string BinSize(string targetColumnName)
			{
				return "BinSize" + targetColumnName;
			}

			// Token: 0x06001686 RID: 5766 RVA: 0x00051B3E File Offset: 0x0004FD3E
			internal static string Min(string targetColumnName)
			{
				return "Min" + targetColumnName;
			}

			// Token: 0x06001687 RID: 5767 RVA: 0x00051B4B File Offset: 0x0004FD4B
			internal static string Difference(string targetColumnName)
			{
				return "MaxMinDiff" + targetColumnName;
			}
		}
	}
}
