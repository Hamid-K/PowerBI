using System;
using System.Collections.Generic;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Expressions;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.CommonDefinitionGeneration
{
	// Token: 0x0200011F RID: 287
	internal sealed class DsdExpressionGenerator
	{
		// Token: 0x06000AA6 RID: 2726 RVA: 0x0002961A File Offset: 0x0002781A
		private DsdExpressionGenerator(ExpressionTable expressionTable, DsdExpressionGenerator.GetTableIdForFieldReference getTableIdForField, DsdExpressionGenerator.GetTableIdForTransformColumn getTableIdForTransform, Dictionary<Identifier, DsdLimitMetadata> limitMetadata)
		{
			this.m_getTableIdForField = getTableIdForField;
			this.m_getTableIdForTransform = getTableIdForTransform;
			this.m_expressionTable = expressionTable;
			this.m_limitMetadata = limitMetadata;
		}

		// Token: 0x06000AA7 RID: 2727 RVA: 0x0002963F File Offset: 0x0002783F
		public static Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Expressions.ExpressionNode Generate(Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions.ExpressionNode node, ExpressionTable expressionTable, DsdExpressionGenerator.GetTableIdForFieldReference getTableIdForField, DsdExpressionGenerator.GetTableIdForTransformColumn getTableIdForTransform, Dictionary<Identifier, DsdLimitMetadata> limitCountExpressions)
		{
			return new DsdExpressionGenerator(expressionTable, getTableIdForField, getTableIdForTransform, limitCountExpressions).Build(node);
		}

		// Token: 0x06000AA8 RID: 2728 RVA: 0x00029654 File Offset: 0x00027854
		private Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Expressions.ExpressionNode Build(Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions.ExpressionNode node)
		{
			ExpressionNodeKind kind = node.Kind;
			if (kind <= ExpressionNodeKind.Literal)
			{
				if (kind == ExpressionNodeKind.DataSetFieldReference)
				{
					return this.BuildFieldReference((DataSetFieldReferenceExpressionNode)node);
				}
				if (kind == ExpressionNodeKind.FunctionCall)
				{
					return this.BuildFunctionCall((Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions.FunctionCallExpressionNode)node);
				}
				if (kind == ExpressionNodeKind.Literal)
				{
					return this.BuildLiteral((Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions.LiteralExpressionNode)node);
				}
			}
			else
			{
				if (kind == ExpressionNodeKind.ResolvedDataTransformTableColumnReference)
				{
					return this.BuildFieldReference((ResolvedDataTransformTableColumnReferenceExpressionNode)node);
				}
				if (kind == ExpressionNodeKind.ResolvedRollup)
				{
					return this.BuildResolvedRollupExpressionNode((ResolvedRollupExpressionNode)node);
				}
				if (kind == ExpressionNodeKind.SubExpression)
				{
					return this.BuildSubExpression((SubExpressionNode)node);
				}
			}
			Contract.RetailFail("Unsupported ExpressionNodeKind: {0}", node.Kind);
			throw new InvalidOperationException(StringUtil.FormatInvariant("Unsupported ExpressionNodeKind: {0}", new object[] { node.Kind }));
		}

		// Token: 0x06000AA9 RID: 2729 RVA: 0x00029718 File Offset: 0x00027918
		private Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Expressions.ExpressionNode BuildResolvedRollupExpressionNode(ResolvedRollupExpressionNode node)
		{
			Calculation calculation = node.Argument.Calculation;
			DataSetFieldReferenceExpressionNode dataSetFieldReferenceExpressionNode = this.m_expressionTable.GetNode(calculation.Value) as DataSetFieldReferenceExpressionNode;
			return this.BuildFieldReference(dataSetFieldReferenceExpressionNode);
		}

		// Token: 0x06000AAA RID: 2730 RVA: 0x00029750 File Offset: 0x00027950
		private Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Expressions.ExpressionNode BuildFunctionCall(Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions.FunctionCallExpressionNode node)
		{
			Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Expressions.ExpressionNode expressionNode;
			if (this.TryHandleSpecialFunction(node, out expressionNode))
			{
				return expressionNode;
			}
			Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Expressions.FunctionCallExpressionNode functionCallExpressionNode = new Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Expressions.FunctionCallExpressionNode
			{
				Kind = this.ComputeFunctionKind(node.Descriptor.BackingFunctionName)
			};
			if (node.Arguments != null)
			{
				functionCallExpressionNode.Arguments = new List<Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Expressions.ExpressionNode>(node.Arguments.Count);
				foreach (Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions.ExpressionNode expressionNode2 in node.Arguments)
				{
					functionCallExpressionNode.Arguments.Add(this.Build(expressionNode2));
				}
			}
			return functionCallExpressionNode;
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x000297F4 File Offset: 0x000279F4
		private bool TryHandleSpecialFunction(Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions.FunctionCallExpressionNode node, out Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Expressions.ExpressionNode expr)
		{
			expr = null;
			if (!(node.Descriptor.BackingFunctionName == "LimitProperty"))
			{
				return false;
			}
			ResolvedLimitReferenceExpressionNode resolvedLimitReferenceExpressionNode = node.Arguments[0] as ResolvedLimitReferenceExpressionNode;
			DsdLimitMetadata dsdLimitMetadata;
			if (!this.m_limitMetadata.TryGetValue(resolvedLimitReferenceExpressionNode.Limit.Id, out dsdLimitMetadata))
			{
				Contract.RetailFail("Could not find a limit count expression for limit {0}", resolvedLimitReferenceExpressionNode.Limit.Id);
			}
			if (node.Arguments.Count == 1)
			{
				expr = dsdLimitMetadata.LimitCount;
				return true;
			}
			Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions.LiteralExpressionNode literalExpressionNode = node.Arguments[1] as Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions.LiteralExpressionNode;
			Contract.RetailAssert(dsdLimitMetadata.Properties != null, "LimitMetadata should have properties.");
			string text = literalExpressionNode.Value.Value as string;
			if (!dsdLimitMetadata.Properties.TryGetValue(text, out expr))
			{
				Contract.RetailAssert(expr != null, "LimitMetadata should find the ExpressionNode using the property name.");
			}
			return true;
		}

		// Token: 0x06000AAC RID: 2732 RVA: 0x000298D0 File Offset: 0x00027AD0
		private FunctionKind ComputeFunctionKind(string functionName)
		{
			if (functionName == "Array")
			{
				return FunctionKind.Array;
			}
			if (functionName == "Comparable")
			{
				return FunctionKind.Comparable;
			}
			if (functionName == "MinValue")
			{
				return FunctionKind.MinValue;
			}
			if (functionName == "MaxValue")
			{
				return FunctionKind.MaxValue;
			}
			Contract.RetailFail("Unsupported FunctionCall: {0}", functionName);
			throw new InvalidOperationException(StringUtil.FormatInvariant("Unsupported FunctionCall: {0}", new object[] { functionName }));
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x00029940 File Offset: 0x00027B40
		private Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Expressions.ExpressionNode BuildFieldReference(DataSetFieldReferenceExpressionNode node)
		{
			string text = this.m_getTableIdForField(node);
			return new FieldValueExpressionNode
			{
				TableId = text,
				FieldId = node.FieldName
			};
		}

		// Token: 0x06000AAE RID: 2734 RVA: 0x00029974 File Offset: 0x00027B74
		private Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Expressions.ExpressionNode BuildFieldReference(ResolvedDataTransformTableColumnReferenceExpressionNode node)
		{
			string text = this.m_getTableIdForTransform(node.Table);
			return new FieldValueExpressionNode
			{
				TableId = text,
				FieldId = node.Column.Id.Value
			};
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x000299B5 File Offset: 0x00027BB5
		private Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Expressions.ExpressionNode BuildSubExpression(SubExpressionNode node)
		{
			return this.Build(this.m_expressionTable.GetNode(node));
		}

		// Token: 0x06000AB0 RID: 2736 RVA: 0x000299CC File Offset: 0x00027BCC
		private Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Expressions.ExpressionNode BuildLiteral(Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions.LiteralExpressionNode node)
		{
			return new Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Expressions.LiteralExpressionNode
			{
				Value = node.Value.Value
			};
		}

		// Token: 0x04000580 RID: 1408
		private readonly DsdExpressionGenerator.GetTableIdForFieldReference m_getTableIdForField;

		// Token: 0x04000581 RID: 1409
		private readonly DsdExpressionGenerator.GetTableIdForTransformColumn m_getTableIdForTransform;

		// Token: 0x04000582 RID: 1410
		private readonly ExpressionTable m_expressionTable;

		// Token: 0x04000583 RID: 1411
		private readonly Dictionary<Identifier, DsdLimitMetadata> m_limitMetadata;

		// Token: 0x020002D0 RID: 720
		// (Invoke) Token: 0x06001663 RID: 5731
		internal delegate string GetTableIdForFieldReference(DataSetFieldReferenceExpressionNode node);

		// Token: 0x020002D1 RID: 721
		// (Invoke) Token: 0x06001667 RID: 5735
		internal delegate string GetTableIdForTransformColumn(DataTransformTable table);
	}
}
