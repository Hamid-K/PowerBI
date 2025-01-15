using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;
using Microsoft.InfoNav;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation
{
	// Token: 0x02000047 RID: 71
	internal sealed class ExpressionStringBuilder : ExpressionStringBuilder, Microsoft.ReportingServices.DataShapeQuery.Expressions.IExpressionNodeVisitor<ExpressionNode>, Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions.IExpressionNodeVisitor<ExpressionNode>
	{
		// Token: 0x060002D1 RID: 721 RVA: 0x00007FD7 File Offset: 0x000061D7
		internal ExpressionStringBuilder(ExpressionTable expressionTable, bool outputConceptualSchemaRefs = false)
		{
			this.m_expressionTable = expressionTable;
			this.m_outputConceptualSchemaRefs = outputConceptualSchemaRefs;
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x00007FF0 File Offset: 0x000061F0
		public override string Write(ExpressionId expressionId)
		{
			if (this.m_expressionTable == null)
			{
				return expressionId.Value.ToString(CultureInfo.InvariantCulture);
			}
			ExpressionNode node = this.m_expressionTable.GetNode(expressionId);
			return base.Write(node);
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x00008030 File Offset: 0x00006230
		internal override ExpressionNode Visit(ExpressionNode node)
		{
			switch (node.Kind)
			{
			case ExpressionNodeKind.AggregatableCurrentGroupExpression:
				return this.Visit((AggregatableCurrentGroupExpressionNode)node);
			case ExpressionNodeKind.AggregatableSubQueryExpression:
				return this.Visit((AggregatableSubQueryExpressionNode)node);
			case ExpressionNodeKind.ApplyContextFilter:
				return this.Visit((ApplyContextFilterExpressionNode)node);
			case ExpressionNodeKind.BatchColumnReference:
				return this.Visit((BatchColumnReferenceExpressionNode)node);
			case ExpressionNodeKind.BatchColumnReferenceByExpressionId:
				return this.Visit((BatchColumnReferenceByExpressionIdExpressionNode)node);
			case ExpressionNodeKind.BatchFilterInlinedDeclarationCalculation:
				return this.Visit((BatchFilterInlinedDeclarationCalculationExpressionNode)node);
			case ExpressionNodeKind.BatchScalarDeclarationReference:
				return this.Visit((BatchScalarDeclarationReferenceExpressionNode)node);
			case ExpressionNodeKind.BatchSubQuery:
				return this.Visit((BatchSubQueryExpressionNode)node);
			case ExpressionNodeKind.DataSetFieldReference:
				return this.Visit((DataSetFieldReferenceExpressionNode)node);
			case ExpressionNodeKind.FilterInlinedCalculation:
				return this.Visit((FilterInlinedCalculationExpressionNode)node);
			case ExpressionNodeKind.RelatedResolvedProperty:
				return this.Visit((RelatedResolvedPropertyExpressionNode)node);
			case ExpressionNodeKind.RemoveGroupings:
				return this.Visit((RemoveGroupingsExpressionNode)node);
			case ExpressionNodeKind.ResolvedCalculationReference:
				return this.Visit((ResolvedCalculationReferenceExpressionNode)node);
			case ExpressionNodeKind.ResolvedDataTransformTableColumnReference:
				return this.Visit((ResolvedDataTransformTableColumnReferenceExpressionNode)node);
			case ExpressionNodeKind.ResolvedEntitySet:
				return this.Visit((ResolvedEntitySetExpressionNode)node);
			case ExpressionNodeKind.ResolvedGroupKeyReference:
				return this.Visit((ResolvedGroupKeyReferenceExpressionNode)node);
			case ExpressionNodeKind.ResolvedLimitReference:
				return this.Visit((ResolvedLimitReferenceExpressionNode)node);
			case ExpressionNodeKind.ResolvedProperty:
				return this.Visit((ResolvedPropertyExpressionNode)node);
			case ExpressionNodeKind.ResolvedScopeReference:
				return this.Visit((ResolvedScopeReferenceExpressionNode)node);
			case ExpressionNodeKind.ResolvedScopeValueDefinitionReference:
				return this.Visit((ResolvedScopeValueDefinitionReferenceExpressionNode)node);
			case ExpressionNodeKind.ResolvedSortKeyReference:
				return this.Visit((ResolvedSortKeyReferenceExpressionNode)node);
			case ExpressionNodeKind.ResolvedRollup:
				return this.Visit((ResolvedRollupExpressionNode)node);
			case ExpressionNodeKind.Rollup:
				return this.Visit((RollupExpressionNode)node);
			case ExpressionNodeKind.SingleValue:
				return this.Visit((SingleValueExpressionNode)node);
			case ExpressionNodeKind.SubExpression:
				return this.Visit((SubExpressionNode)node);
			case ExpressionNodeKind.TableSubQueryExpression:
				return this.Visit((TableSubQueryExpressionNode)node);
			case ExpressionNodeKind.UnaryOperator:
				return this.Visit((UnaryOperatorExpressionNode)node);
			}
			return base.VisitInternal(node);
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x000082BA File Offset: 0x000064BA
		public ExpressionNode Visit(UnaryOperatorExpressionNode node)
		{
			this.m_writer.Write('(');
			this.Visit(node.OperatorKind);
			this.Visit(node.Operand);
			this.m_writer.Write(')');
			return node;
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x000082F0 File Offset: 0x000064F0
		private void Visit(UnaryOperatorKind kind)
		{
			if (kind == UnaryOperatorKind.Not)
			{
				this.m_writer.Write('!');
				return;
			}
			if (kind != UnaryOperatorKind.Minus)
			{
				throw new NotSupportedException("Unsupported unary operator kind: " + kind.ToString());
			}
			this.m_writer.Write('-');
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x00008340 File Offset: 0x00006540
		public ExpressionNode Visit(ApplyContextFilterExpressionNode node)
		{
			this.m_writer.Write("ApplyContextFilter(");
			this.Visit(node.Expression);
			this.m_writer.Write(',');
			this.WriteInlineStructures(node.ContextTables);
			this.m_writer.Write(')');
			return node;
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x00008394 File Offset: 0x00006594
		private void WriteInlineStructure(IStructuredToString structure)
		{
			this.m_writer.WriteLine();
			StructuredStringBuilder structuredStringBuilder = new StructuredStringBuilder(new ExpressionStringBuilder(this.m_expressionTable, false), 1, false);
			structure.WriteTo(structuredStringBuilder);
			string text = structuredStringBuilder.ToString();
			this.m_writer.Write(text);
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x000083DC File Offset: 0x000065DC
		private void WriteInlineStructures(IEnumerable<IStructuredToString> structures)
		{
			this.m_writer.Write('[');
			bool flag = true;
			foreach (IStructuredToString structuredToString in structures)
			{
				if (!flag)
				{
					this.m_writer.Write(',');
				}
				this.WriteInlineStructure(structuredToString);
				flag = false;
			}
			this.m_writer.Write(']');
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x00008454 File Offset: 0x00006654
		public ExpressionNode Visit(BatchColumnReferenceExpressionNode node)
		{
			this.WriteTypedReference(node.Kind, node.ColumnName, null, null);
			return node;
		}

		// Token: 0x060002DA RID: 730 RVA: 0x0000847E File Offset: 0x0000667E
		public ExpressionNode Visit(BatchColumnReferenceByExpressionIdExpressionNode node)
		{
			this.WriteTypedReference(node.Kind, null, new ExpressionId?(node.ExpressionId));
			return node;
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0000849C File Offset: 0x0000669C
		public ExpressionNode Visit(BatchFilterInlinedDeclarationCalculationExpressionNode node)
		{
			this.m_writer.Write("BatchFilterInlinedDeclarationCalc(");
			this.Visit(node.ExpressionNode);
			this.m_writer.Write(',');
			this.WriteInlineStructure(node.FilterDeclaration);
			this.m_writer.Write(')');
			return node;
		}

		// Token: 0x060002DC RID: 732 RVA: 0x000084F0 File Offset: 0x000066F0
		public ExpressionNode Visit(BatchScalarDeclarationReferenceExpressionNode node)
		{
			this.WriteTypedReference(node.Kind, node.DeclarationName, null, null);
			return node;
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0000851A File Offset: 0x0000671A
		public ExpressionNode Visit(BatchSubQueryExpressionNode node)
		{
			this.m_writer.Write("BatchSubQuery(");
			this.WriteInlineStructure(node.Table);
			this.m_writer.Write(')');
			return node;
		}

		// Token: 0x060002DE RID: 734 RVA: 0x00008546 File Offset: 0x00006746
		public ExpressionNode Visit(SingleValueExpressionNode node)
		{
			this.m_writer.Write("SingleValue(");
			this.Visit(node.Input);
			this.m_writer.Write(')');
			return node;
		}

		// Token: 0x060002DF RID: 735 RVA: 0x00008574 File Offset: 0x00006774
		public ExpressionNode Visit(RemoveGroupingsExpressionNode node)
		{
			this.m_writer.Write("RemoveGroupings(");
			this.Visit(node.Expression);
			foreach (ExpressionNode expressionNode in node.GroupKeysToRemove)
			{
				this.m_writer.Write(", ");
				this.Visit(expressionNode);
			}
			this.m_writer.Write(')');
			return node;
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x00008600 File Offset: 0x00006800
		public ExpressionNode Visit(ResolvedScopeReferenceExpressionNode node)
		{
			this.WriteTypedReference(node.Kind, node.Scope, null);
			return node;
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x0000862C File Offset: 0x0000682C
		public ExpressionNode Visit(ResolvedEntitySetExpressionNode node)
		{
			this.m_writer.Write(node.Entity.EntityContainerName + "." + node.Entity.EdmName);
			if (this.m_outputConceptualSchemaRefs)
			{
				this.m_writer.Write("[UsedConceptualSchema]");
			}
			return node;
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x0000867D File Offset: 0x0000687D
		public ExpressionNode Visit(ResolvedPropertyExpressionNode node)
		{
			this.WritePropertyReference(node.Container, node.Property);
			return node;
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x00008694 File Offset: 0x00006894
		private void WritePropertyReference(string containerName, IConceptualProperty property)
		{
			if (property.Entity.Schema.SchemaId == "")
			{
				this.WritePropertyReference(containerName, property.Entity.EdmName, property.EdmName);
			}
			else
			{
				this.WritePropertyReference(containerName, property.Entity.Name, property.Name);
			}
			if (this.m_outputConceptualSchemaRefs)
			{
				this.m_writer.Write("[UsedConceptualSchema]");
			}
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x00008708 File Offset: 0x00006908
		private void WritePropertyReference(string containerName, string entitySetName, string propertyName)
		{
			this.m_writer.Write(containerName);
			this.m_writer.Write(".");
			this.m_writer.Write(ExpressionStringBuilder.EncodeIdentifierOrStringLiteral(entitySetName));
			this.m_writer.Write('/');
			this.m_writer.Write(ExpressionStringBuilder.EncodeIdentifierOrStringLiteral(propertyName));
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x00008760 File Offset: 0x00006960
		public ExpressionNode Visit(RelatedResolvedPropertyExpressionNode node)
		{
			this.m_writer.Write("RelatedColumn(");
			this.WritePropertyReference(node.Container, node.Property);
			this.m_writer.Write(")");
			return node;
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x00008798 File Offset: 0x00006998
		public ExpressionNode Visit(FilterInlinedCalculationExpressionNode node)
		{
			this.m_writer.Write("FilterInlinedCalc(");
			this.Visit(node.ExpressionNode);
			this.m_writer.Write(',');
			this.WriteId(node.Calculation);
			this.m_writer.Write(',');
			if (node.FilterCondition != null)
			{
				this.WriteInlineStructure(node.FilterCondition);
			}
			else
			{
				this.Visit(node.FilterExpression);
			}
			this.m_writer.Write(')');
			return node;
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x0000881C File Offset: 0x00006A1C
		public ExpressionNode Visit(ResolvedCalculationReferenceExpressionNode node)
		{
			this.WriteTypedReference(node.Kind, node.Calculation, null);
			return node;
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x00008848 File Offset: 0x00006A48
		public ExpressionNode Visit(ResolvedLimitReferenceExpressionNode node)
		{
			this.WriteTypedReference(node.Kind, node.Limit, null);
			return node;
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x00008874 File Offset: 0x00006A74
		public ExpressionNode Visit(AggregatableSubQueryExpressionNode node)
		{
			this.m_writer.Write("AggregatableSubQuery(");
			this.Visit(this.m_expressionTable.GetNode(node.TargetExpressionId));
			this.m_writer.Write(',');
			this.WriteInlineStructure(node.Table);
			this.m_writer.Write(')');
			return node;
		}

		// Token: 0x060002EA RID: 746 RVA: 0x000088D0 File Offset: 0x00006AD0
		public ExpressionNode Visit(TableSubQueryExpressionNode node)
		{
			this.m_writer.Write("TableSubQuery(");
			this.m_writer.Write(node.Name);
			this.m_writer.Write(',');
			this.m_writer.Write(node.DataSetPlan.Name);
			this.m_writer.Write(',');
			this.m_writer.Write(node.Source.Id);
			this.m_writer.Write(')');
			return node;
		}

		// Token: 0x060002EB RID: 747 RVA: 0x00008952 File Offset: 0x00006B52
		public ExpressionNode Visit(SubExpressionNode node)
		{
			this.m_writer.Write("SubExpr(");
			this.Visit(this.m_expressionTable.GetNode(node.ExpressionId));
			this.m_writer.Write(')');
			return node;
		}

		// Token: 0x060002EC RID: 748 RVA: 0x0000898C File Offset: 0x00006B8C
		public ExpressionNode Visit(DataSetFieldReferenceExpressionNode node)
		{
			this.m_writer.Write("DataSetField(");
			this.m_writer.Write(node.DataSetPlan.Name);
			this.m_writer.Write(',');
			this.m_writer.Write(node.FieldName);
			if (node.TablePlan != null)
			{
				this.m_writer.Write(',');
				this.m_writer.Write(node.TablePlan.Name);
			}
			this.m_writer.Write(')');
			return node;
		}

		// Token: 0x060002ED RID: 749 RVA: 0x00008A16 File Offset: 0x00006C16
		public ExpressionNode Visit(RollupExpressionNode node)
		{
			this.m_writer.Write("Rollup(");
			this.Visit(node.Argument);
			this.m_writer.Write(')');
			return node;
		}

		// Token: 0x060002EE RID: 750 RVA: 0x00008A43 File Offset: 0x00006C43
		public ExpressionNode Visit(ResolvedRollupExpressionNode node)
		{
			this.m_writer.Write("Rollup(");
			this.Visit(node.Argument);
			this.m_writer.Write(')');
			return node;
		}

		// Token: 0x060002EF RID: 751 RVA: 0x00008A70 File Offset: 0x00006C70
		public ExpressionNode Visit(ResolvedGroupKeyReferenceExpressionNode node)
		{
			this.WriteTypedReference(node.Kind, node.GroupKey, node.GroupKey.Value.ExpressionId);
			return node;
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x00008A98 File Offset: 0x00006C98
		public ExpressionNode Visit(ResolvedSortKeyReferenceExpressionNode node)
		{
			this.WriteTypedReference(node.Kind, node.SortKey, null);
			return node;
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x00008AC4 File Offset: 0x00006CC4
		public ExpressionNode Visit(ResolvedScopeValueDefinitionReferenceExpressionNode node)
		{
			this.WriteTypedReference(node.Kind, node.ScopeValueDefinition, null);
			return node;
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x00008AED File Offset: 0x00006CED
		public ExpressionNode Visit(AggregatableCurrentGroupExpressionNode node)
		{
			this.m_writer.Write("CurrentGroup(");
			this.Visit(node.Projection);
			this.m_writer.Write(')');
			return node;
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x00008B1C File Offset: 0x00006D1C
		public ExpressionNode Visit(ResolvedDataTransformTableColumnReferenceExpressionNode node)
		{
			this.WriteTypedReference(node.Kind, this.GetId(node.Table), this.GetId(node.Column), null);
			return node;
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x00008B57 File Offset: 0x00006D57
		private string GetId(IIdentifiable identifiable)
		{
			if (identifiable == null || identifiable.Id == null || identifiable.Id.Value == null)
			{
				return string.Empty;
			}
			return identifiable.Id.Value;
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x00008B88 File Offset: 0x00006D88
		private void WriteTypedReference(ExpressionNodeKind nodeKind, IIdentifiable identifiable, ExpressionId? exprId = null)
		{
			this.WriteTypedReference(nodeKind, this.GetId(identifiable), null, exprId);
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x00008B9C File Offset: 0x00006D9C
		private void WriteTypedReference(ExpressionNodeKind nodeKind, string value, string value2 = null, ExpressionId? exprId = null)
		{
			this.m_writer.Write('[');
			this.m_writer.Write(ExpressionStringBuilder.GetTypePrefix(nodeKind));
			this.m_writer.Write(':');
			if (!string.IsNullOrEmpty(value))
			{
				this.m_writer.Write(value);
			}
			else
			{
				this.m_writer.Write(exprId.Value);
			}
			if (value2 != null)
			{
				this.m_writer.Write(",");
				this.m_writer.Write(value2);
			}
			this.m_writer.Write(']');
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x00008C2D File Offset: 0x00006E2D
		private void WriteId(IIdentifiable identifiable)
		{
			if (identifiable == null)
			{
				this.m_writer.Write(string.Empty);
				return;
			}
			this.WriteId(identifiable.Id);
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x00008C4F File Offset: 0x00006E4F
		private void WriteId(Identifier id)
		{
			if (id == null || id.Value == null)
			{
				this.m_writer.Write(string.Empty);
				return;
			}
			this.m_writer.Write(id.Value);
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x00008C84 File Offset: 0x00006E84
		private static string GetTypePrefix(ExpressionNodeKind nodeKind)
		{
			switch (nodeKind)
			{
			case ExpressionNodeKind.BatchColumnReference:
				return "BatchColumn";
			case ExpressionNodeKind.BatchColumnReferenceByExpressionId:
				return "BatchColumnReferenceByExpressionId";
			case ExpressionNodeKind.BatchFilterInlinedDeclarationCalculation:
			case ExpressionNodeKind.BatchSubQuery:
			case ExpressionNodeKind.BinaryOperator:
				break;
			case ExpressionNodeKind.BatchScalarDeclarationReference:
				return "BatchScalarDeclaration";
			case ExpressionNodeKind.DataSetFieldReference:
				return "DataSetField";
			default:
				switch (nodeKind)
				{
				case ExpressionNodeKind.ResolvedCalculationReference:
					return "Calc";
				case ExpressionNodeKind.ResolvedDataTransformTableColumnReference:
					return "TransformTableColumn";
				case ExpressionNodeKind.ResolvedFilterConditionReference:
					return "FilterCondition";
				case ExpressionNodeKind.ResolvedGroupKeyReference:
					return "GroupKey";
				case ExpressionNodeKind.ResolvedLimitReference:
					return "Limit";
				case ExpressionNodeKind.ResolvedScopeReference:
					return "Scope";
				case ExpressionNodeKind.ResolvedScopeValueDefinitionReference:
					return "ScopeValueDef";
				case ExpressionNodeKind.ResolvedSortKeyReference:
					return "SortKey";
				case ExpressionNodeKind.StructureReference:
					return "Structure";
				}
				break;
			}
			throw new NotSupportedException("Unsupported expression node kind: " + nodeKind.ToString());
		}

		// Token: 0x040000CA RID: 202
		private readonly ExpressionTable m_expressionTable;

		// Token: 0x040000CB RID: 203
		private readonly bool m_outputConceptualSchemaRefs;
	}
}
