using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser;

namespace Microsoft.OData
{
	// Token: 0x020000D1 RID: 209
	internal sealed class NodeToStringBuilder : QueryNodeVisitor<string>
	{
		// Token: 0x060007E4 RID: 2020 RVA: 0x00015E38 File Offset: 0x00014038
		public override string Visit(AllNode node)
		{
			ExceptionUtils.CheckArgumentNotNull<AllNode>(node, "node");
			return string.Concat(new string[]
			{
				this.TranslateNode(node.Source),
				"/",
				"all",
				"(",
				node.CurrentRangeVariable.Name,
				":",
				this.TranslateNode(node.Body),
				")"
			});
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x00015EB4 File Offset: 0x000140B4
		public override string Visit(AnyNode node)
		{
			ExceptionUtils.CheckArgumentNotNull<AnyNode>(node, "node");
			if (node.CurrentRangeVariable == null && node.Body.Kind == QueryNodeKind.Constant)
			{
				return string.Concat(new string[]
				{
					this.TranslateNode(node.Source),
					"/",
					"any",
					"(",
					")"
				});
			}
			return string.Concat(new string[]
			{
				this.TranslateNode(node.Source),
				"/",
				"any",
				"(",
				node.CurrentRangeVariable.Name,
				":",
				this.TranslateNode(node.Body),
				")"
			});
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x00015F80 File Offset: 0x00014180
		public override string Visit(BinaryOperatorNode node)
		{
			ExceptionUtils.CheckArgumentNotNull<BinaryOperatorNode>(node, "node");
			string text = this.TranslateNode(node.Left);
			if (node.Left.Kind == QueryNodeKind.BinaryOperator && NodeToStringBuilder.TranslateBinaryOperatorPriority(((BinaryOperatorNode)node.Left).OperatorKind) < NodeToStringBuilder.TranslateBinaryOperatorPriority(node.OperatorKind))
			{
				text = "(" + text + ")";
			}
			string text2 = this.TranslateNode(node.Right);
			if (node.Right.Kind == QueryNodeKind.BinaryOperator && NodeToStringBuilder.TranslateBinaryOperatorPriority(((BinaryOperatorNode)node.Right).OperatorKind) < NodeToStringBuilder.TranslateBinaryOperatorPriority(node.OperatorKind))
			{
				text2 = "(" + text2 + ")";
			}
			return string.Concat(new object[]
			{
				text,
				' ',
				this.BinaryOperatorNodeToString(node.OperatorKind),
				' ',
				text2
			});
		}

		// Token: 0x060007E7 RID: 2023 RVA: 0x0001606C File Offset: 0x0001426C
		public override string Visit(CountNode node)
		{
			ExceptionUtils.CheckArgumentNotNull<CountNode>(node, "node");
			string text = this.TranslateNode(node.Source);
			return text + "/" + "$count";
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x000160A2 File Offset: 0x000142A2
		public override string Visit(CollectionNavigationNode node)
		{
			ExceptionUtils.CheckArgumentNotNull<CollectionNavigationNode>(node, "node");
			return this.TranslatePropertyAccess(node.Source, node.NavigationProperty.Name, node.NavigationSource);
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x000160CD File Offset: 0x000142CD
		public override string Visit(CollectionPropertyAccessNode node)
		{
			ExceptionUtils.CheckArgumentNotNull<CollectionPropertyAccessNode>(node, "node");
			return this.TranslatePropertyAccess(node.Source, node.Property.Name, null);
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x000160F3 File Offset: 0x000142F3
		public override string Visit(CollectionComplexNode node)
		{
			ExceptionUtils.CheckArgumentNotNull<CollectionComplexNode>(node, "node");
			return this.TranslatePropertyAccess(node.Source, node.Property.Name, null);
		}

		// Token: 0x060007EB RID: 2027 RVA: 0x00016119 File Offset: 0x00014319
		public override string Visit(ConstantNode node)
		{
			ExceptionUtils.CheckArgumentNotNull<ConstantNode>(node, "node");
			if (node.Value == null)
			{
				return "null";
			}
			return node.LiteralText;
		}

		// Token: 0x060007EC RID: 2028 RVA: 0x0001613B File Offset: 0x0001433B
		public override string Visit(ConvertNode node)
		{
			ExceptionUtils.CheckArgumentNotNull<ConvertNode>(node, "node");
			return this.TranslateNode(node.Source);
		}

		// Token: 0x060007ED RID: 2029 RVA: 0x00016155 File Offset: 0x00014355
		public override string Visit(CollectionResourceCastNode node)
		{
			ExceptionUtils.CheckArgumentNotNull<CollectionResourceCastNode>(node, "node");
			return this.TranslatePropertyAccess(node.Source, node.ItemStructuredType.Definition.ToString(), null);
		}

		// Token: 0x060007EE RID: 2030 RVA: 0x00016180 File Offset: 0x00014380
		public override string Visit(ResourceRangeVariableReferenceNode node)
		{
			ExceptionUtils.CheckArgumentNotNull<ResourceRangeVariableReferenceNode>(node, "node");
			if (node.Name == "$it")
			{
				return string.Empty;
			}
			return node.Name;
		}

		// Token: 0x060007EF RID: 2031 RVA: 0x000161AC File Offset: 0x000143AC
		public override string Visit(NonResourceRangeVariableReferenceNode node)
		{
			ExceptionUtils.CheckArgumentNotNull<NonResourceRangeVariableReferenceNode>(node, "node");
			return node.Name;
		}

		// Token: 0x060007F0 RID: 2032 RVA: 0x000161C0 File Offset: 0x000143C0
		public override string Visit(SingleResourceCastNode node)
		{
			ExceptionUtils.CheckArgumentNotNull<SingleResourceCastNode>(node, "node");
			return this.TranslatePropertyAccess(node.Source, node.StructuredTypeReference.Definition.ToString(), null);
		}

		// Token: 0x060007F1 RID: 2033 RVA: 0x000161EB File Offset: 0x000143EB
		public override string Visit(SingleNavigationNode node)
		{
			ExceptionUtils.CheckArgumentNotNull<SingleNavigationNode>(node, "node");
			return this.TranslatePropertyAccess(node.Source, node.NavigationProperty.Name, node.NavigationSource);
		}

		// Token: 0x060007F2 RID: 2034 RVA: 0x00016218 File Offset: 0x00014418
		public override string Visit(SingleResourceFunctionCallNode node)
		{
			ExceptionUtils.CheckArgumentNotNull<SingleResourceFunctionCallNode>(node, "node");
			string text = node.Name;
			if (node.Source != null)
			{
				text = this.TranslatePropertyAccess(node.Source, text, null);
			}
			return this.TranslateFunctionCall(text, node.Parameters);
		}

		// Token: 0x060007F3 RID: 2035 RVA: 0x0001625C File Offset: 0x0001445C
		public override string Visit(SingleValueFunctionCallNode node)
		{
			ExceptionUtils.CheckArgumentNotNull<SingleValueFunctionCallNode>(node, "node");
			string text = node.Name;
			if (node.Source != null)
			{
				text = this.TranslatePropertyAccess(node.Source, text, null);
			}
			return this.TranslateFunctionCall(text, node.Parameters);
		}

		// Token: 0x060007F4 RID: 2036 RVA: 0x000162A0 File Offset: 0x000144A0
		public override string Visit(CollectionFunctionCallNode node)
		{
			ExceptionUtils.CheckArgumentNotNull<CollectionFunctionCallNode>(node, "node");
			string text = node.Name;
			if (node.Source != null)
			{
				text = this.TranslatePropertyAccess(node.Source, text, null);
			}
			return this.TranslateFunctionCall(text, node.Parameters);
		}

		// Token: 0x060007F5 RID: 2037 RVA: 0x000162E4 File Offset: 0x000144E4
		public override string Visit(CollectionResourceFunctionCallNode node)
		{
			ExceptionUtils.CheckArgumentNotNull<CollectionResourceFunctionCallNode>(node, "node");
			string text = node.Name;
			if (node.Source != null)
			{
				text = this.TranslatePropertyAccess(node.Source, text, null);
			}
			return this.TranslateFunctionCall(text, node.Parameters);
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x00016328 File Offset: 0x00014528
		public override string Visit(SingleValueOpenPropertyAccessNode node)
		{
			ExceptionUtils.CheckArgumentNotNull<SingleValueOpenPropertyAccessNode>(node, "node");
			return this.TranslatePropertyAccess(node.Source, node.Name, null);
		}

		// Token: 0x060007F7 RID: 2039 RVA: 0x00016349 File Offset: 0x00014549
		public override string Visit(CollectionOpenPropertyAccessNode node)
		{
			ExceptionUtils.CheckArgumentNotNull<CollectionOpenPropertyAccessNode>(node, "node");
			return this.TranslatePropertyAccess(node.Source, node.Name, null);
		}

		// Token: 0x060007F8 RID: 2040 RVA: 0x0001636A File Offset: 0x0001456A
		public override string Visit(SingleValuePropertyAccessNode node)
		{
			ExceptionUtils.CheckArgumentNotNull<SingleValuePropertyAccessNode>(node, "node");
			return this.TranslatePropertyAccess(node.Source, node.Property.Name, null);
		}

		// Token: 0x060007F9 RID: 2041 RVA: 0x00016390 File Offset: 0x00014590
		public override string Visit(SingleComplexNode node)
		{
			ExceptionUtils.CheckArgumentNotNull<SingleComplexNode>(node, "node");
			return this.TranslatePropertyAccess(node.Source, node.Property.Name, null);
		}

		// Token: 0x060007FA RID: 2042 RVA: 0x000163B6 File Offset: 0x000145B6
		public override string Visit(ParameterAliasNode node)
		{
			ExceptionUtils.CheckArgumentNotNull<ParameterAliasNode>(node, "node");
			return node.Alias;
		}

		// Token: 0x060007FB RID: 2043 RVA: 0x000163CA File Offset: 0x000145CA
		public override string Visit(NamedFunctionParameterNode node)
		{
			ExceptionUtils.CheckArgumentNotNull<NamedFunctionParameterNode>(node, "node");
			return node.Name + "=" + this.TranslateNode(node.Value);
		}

		// Token: 0x060007FC RID: 2044 RVA: 0x000163F4 File Offset: 0x000145F4
		public override string Visit(SearchTermNode node)
		{
			ExceptionUtils.CheckArgumentNotNull<SearchTermNode>(node, "node");
			if (!NodeToStringBuilder.IsValidSearchWord(node.Text))
			{
				return "\"" + node.Text + "\"";
			}
			return node.Text;
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x0001642C File Offset: 0x0001462C
		public override string Visit(UnaryOperatorNode node)
		{
			ExceptionUtils.CheckArgumentNotNull<UnaryOperatorNode>(node, "node");
			string text = null;
			if (node.OperatorKind == UnaryOperatorKind.Negate)
			{
				text = "-";
			}
			if (node.OperatorKind == UnaryOperatorKind.Not)
			{
				if (this.searchFlag)
				{
					text = "NOT";
				}
				else
				{
					text = "not";
				}
			}
			if (node.Operand.Kind == QueryNodeKind.Constant || node.Operand.Kind == QueryNodeKind.SearchTerm)
			{
				return text + ' ' + this.TranslateNode(node.Operand);
			}
			return text + "(" + this.TranslateNode(node.Operand) + ")";
		}

		// Token: 0x060007FE RID: 2046 RVA: 0x000164C8 File Offset: 0x000146C8
		internal static string TranslateLevelsClause(LevelsClause levelsClause)
		{
			return levelsClause.IsMaxLevel ? "max" : levelsClause.Level.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x060007FF RID: 2047 RVA: 0x000164F9 File Offset: 0x000146F9
		internal string TranslateNode(QueryNode node)
		{
			return node.Accept<string>(this);
		}

		// Token: 0x06000800 RID: 2048 RVA: 0x00016502 File Offset: 0x00014702
		internal string TranslateFilterClause(FilterClause filterClause)
		{
			return this.TranslateNode(filterClause.Expression);
		}

		// Token: 0x06000801 RID: 2049 RVA: 0x00016510 File Offset: 0x00014710
		internal string TranslateOrderByClause(OrderByClause orderByClause)
		{
			string text = this.TranslateNode(orderByClause.Expression);
			if (orderByClause.Direction == OrderByDirection.Descending)
			{
				text = text + ' ' + "desc";
			}
			orderByClause = orderByClause.ThenBy;
			if (orderByClause == null)
			{
				return text;
			}
			return text + "," + this.TranslateOrderByClause(orderByClause);
		}

		// Token: 0x06000802 RID: 2050 RVA: 0x00016568 File Offset: 0x00014768
		internal string TranslateSearchClause(SearchClause searchClause)
		{
			this.searchFlag = true;
			string text = this.TranslateNode(searchClause.Expression);
			this.searchFlag = false;
			return text;
		}

		// Token: 0x06000803 RID: 2051 RVA: 0x00016594 File Offset: 0x00014794
		internal string TranslateParameterAliasNodes(IDictionary<string, SingleValueNode> dictionary)
		{
			string text = null;
			if (dictionary != null)
			{
				foreach (KeyValuePair<string, SingleValueNode> keyValuePair in dictionary)
				{
					if (keyValuePair.Value != null)
					{
						string text2 = this.TranslateNode(keyValuePair.Value);
						text = (string.IsNullOrEmpty(text2) ? text : string.Concat(new string[]
						{
							text,
							string.IsNullOrEmpty(text) ? null : "&",
							keyValuePair.Key,
							"=",
							Uri.EscapeDataString(text2)
						}));
					}
				}
			}
			return text;
		}

		// Token: 0x06000804 RID: 2052 RVA: 0x00016640 File Offset: 0x00014840
		private string TranslatePropertyAccess(QueryNode sourceNode, string edmPropertyName, IEdmNavigationSource navigationSource = null)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNode>(sourceNode, "sourceNode");
			ExceptionUtils.CheckArgumentNotNull<string>(edmPropertyName, "edmPropertyName");
			string text = this.TranslateNode(sourceNode);
			if (!string.IsNullOrEmpty(text))
			{
				return text + "/" + edmPropertyName;
			}
			return edmPropertyName;
		}

		// Token: 0x06000805 RID: 2053 RVA: 0x00016684 File Offset: 0x00014884
		private string TranslateFunctionCall(string functionName, IEnumerable<QueryNode> argumentNodes)
		{
			ExceptionUtils.CheckArgumentNotNull<string>(functionName, "functionName");
			string text = string.Empty;
			foreach (QueryNode queryNode in argumentNodes)
			{
				text = text + (string.IsNullOrEmpty(text) ? null : ",") + this.TranslateNode(queryNode);
			}
			return functionName + "(" + text + ")";
		}

		// Token: 0x06000806 RID: 2054 RVA: 0x00016708 File Offset: 0x00014908
		private string BinaryOperatorNodeToString(BinaryOperatorKind operatorKind)
		{
			switch (operatorKind)
			{
			case BinaryOperatorKind.Or:
				if (this.searchFlag)
				{
					return "OR";
				}
				return "or";
			case BinaryOperatorKind.And:
				if (this.searchFlag)
				{
					return "AND";
				}
				return "and";
			case BinaryOperatorKind.Equal:
				return "eq";
			case BinaryOperatorKind.NotEqual:
				return "ne";
			case BinaryOperatorKind.GreaterThan:
				return "gt";
			case BinaryOperatorKind.GreaterThanOrEqual:
				return "ge";
			case BinaryOperatorKind.LessThan:
				return "lt";
			case BinaryOperatorKind.LessThanOrEqual:
				return "le";
			case BinaryOperatorKind.Add:
				return "add";
			case BinaryOperatorKind.Subtract:
				return "sub";
			case BinaryOperatorKind.Multiply:
				return "mul";
			case BinaryOperatorKind.Divide:
				return "div";
			case BinaryOperatorKind.Modulo:
				return "mod";
			case BinaryOperatorKind.Has:
				return "has";
			default:
				return null;
			}
		}

		// Token: 0x06000807 RID: 2055 RVA: 0x000167C8 File Offset: 0x000149C8
		private static int TranslateBinaryOperatorPriority(BinaryOperatorKind operatorKind)
		{
			switch (operatorKind)
			{
			case BinaryOperatorKind.Or:
				return 1;
			case BinaryOperatorKind.And:
				return 2;
			case BinaryOperatorKind.Equal:
			case BinaryOperatorKind.NotEqual:
			case BinaryOperatorKind.GreaterThan:
			case BinaryOperatorKind.GreaterThanOrEqual:
			case BinaryOperatorKind.LessThan:
			case BinaryOperatorKind.LessThanOrEqual:
				return 3;
			case BinaryOperatorKind.Add:
			case BinaryOperatorKind.Subtract:
				return 4;
			case BinaryOperatorKind.Multiply:
			case BinaryOperatorKind.Divide:
			case BinaryOperatorKind.Modulo:
				return 5;
			case BinaryOperatorKind.Has:
				return 6;
			default:
				return -1;
			}
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x00016824 File Offset: 0x00014A24
		private static bool IsValidSearchWord(string text)
		{
			Match match = SearchLexer.InvalidWordPattern.Match(text);
			return !match.Success && !string.Equals(text, "AND") && !string.Equals(text, "OR") && !string.Equals(text, "NOT");
		}

		// Token: 0x0400037D RID: 893
		private bool searchFlag;
	}
}
