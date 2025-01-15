using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser;

namespace Microsoft.OData
{
	// Token: 0x020000D4 RID: 212
	internal sealed class NodeToStringBuilder : QueryNodeVisitor<string>
	{
		// Token: 0x060009C5 RID: 2501 RVA: 0x00018EEC File Offset: 0x000170EC
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

		// Token: 0x060009C6 RID: 2502 RVA: 0x00018F68 File Offset: 0x00017168
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

		// Token: 0x060009C7 RID: 2503 RVA: 0x00019034 File Offset: 0x00017234
		public override string Visit(BinaryOperatorNode node)
		{
			ExceptionUtils.CheckArgumentNotNull<BinaryOperatorNode>(node, "node");
			string text = this.TranslateNode(node.Left);
			if ((node.Left.Kind == QueryNodeKind.BinaryOperator && NodeToStringBuilder.TranslateBinaryOperatorPriority(((BinaryOperatorNode)node.Left).OperatorKind) < NodeToStringBuilder.TranslateBinaryOperatorPriority(node.OperatorKind)) || (node.Left.Kind == QueryNodeKind.Convert && ((ConvertNode)node.Left).Source.Kind == QueryNodeKind.BinaryOperator && NodeToStringBuilder.TranslateBinaryOperatorPriority(((BinaryOperatorNode)((ConvertNode)node.Left).Source).OperatorKind) < NodeToStringBuilder.TranslateBinaryOperatorPriority(node.OperatorKind)))
			{
				text = "(" + text + ")";
			}
			string text2 = this.TranslateNode(node.Right);
			if ((node.Right.Kind == QueryNodeKind.BinaryOperator && NodeToStringBuilder.TranslateBinaryOperatorPriority(((BinaryOperatorNode)node.Right).OperatorKind) < NodeToStringBuilder.TranslateBinaryOperatorPriority(node.OperatorKind)) || (node.Right.Kind == QueryNodeKind.Convert && ((ConvertNode)node.Right).Source.Kind == QueryNodeKind.BinaryOperator && NodeToStringBuilder.TranslateBinaryOperatorPriority(((BinaryOperatorNode)((ConvertNode)node.Right).Source).OperatorKind) < NodeToStringBuilder.TranslateBinaryOperatorPriority(node.OperatorKind)))
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

		// Token: 0x060009C8 RID: 2504 RVA: 0x000191C4 File Offset: 0x000173C4
		public override string Visit(InNode node)
		{
			ExceptionUtils.CheckArgumentNotNull<InNode>(node, "node");
			string text = this.TranslateNode(node.Left);
			string text2 = this.TranslateNode(node.Right);
			return string.Concat(new object[] { text, ' ', "in", ' ', text2 });
		}

		// Token: 0x060009C9 RID: 2505 RVA: 0x00019228 File Offset: 0x00017428
		public override string Visit(CountNode node)
		{
			ExceptionUtils.CheckArgumentNotNull<CountNode>(node, "node");
			string text = this.TranslateNode(node.Source);
			return text + "/" + "$count";
		}

		// Token: 0x060009CA RID: 2506 RVA: 0x0001925E File Offset: 0x0001745E
		public override string Visit(CollectionNavigationNode node)
		{
			ExceptionUtils.CheckArgumentNotNull<CollectionNavigationNode>(node, "node");
			return this.TranslatePropertyAccess(node.Source, node.NavigationProperty.Name, node.NavigationSource);
		}

		// Token: 0x060009CB RID: 2507 RVA: 0x00019289 File Offset: 0x00017489
		public override string Visit(CollectionPropertyAccessNode node)
		{
			ExceptionUtils.CheckArgumentNotNull<CollectionPropertyAccessNode>(node, "node");
			return this.TranslatePropertyAccess(node.Source, node.Property.Name, null);
		}

		// Token: 0x060009CC RID: 2508 RVA: 0x000192AF File Offset: 0x000174AF
		public override string Visit(CollectionComplexNode node)
		{
			ExceptionUtils.CheckArgumentNotNull<CollectionComplexNode>(node, "node");
			return this.TranslatePropertyAccess(node.Source, node.Property.Name, null);
		}

		// Token: 0x060009CD RID: 2509 RVA: 0x000192D5 File Offset: 0x000174D5
		public override string Visit(ConstantNode node)
		{
			ExceptionUtils.CheckArgumentNotNull<ConstantNode>(node, "node");
			if (node.Value == null)
			{
				return "null";
			}
			return node.LiteralText;
		}

		// Token: 0x060009CE RID: 2510 RVA: 0x000192F7 File Offset: 0x000174F7
		public override string Visit(CollectionConstantNode node)
		{
			ExceptionUtils.CheckArgumentNotNull<CollectionConstantNode>(node, "node");
			if (string.IsNullOrEmpty(node.LiteralText))
			{
				return "null";
			}
			return node.LiteralText;
		}

		// Token: 0x060009CF RID: 2511 RVA: 0x0001931E File Offset: 0x0001751E
		public override string Visit(ConvertNode node)
		{
			ExceptionUtils.CheckArgumentNotNull<ConvertNode>(node, "node");
			return this.TranslateNode(node.Source);
		}

		// Token: 0x060009D0 RID: 2512 RVA: 0x00019338 File Offset: 0x00017538
		public override string Visit(CollectionResourceCastNode node)
		{
			ExceptionUtils.CheckArgumentNotNull<CollectionResourceCastNode>(node, "node");
			return this.TranslatePropertyAccess(node.Source, node.ItemStructuredType.Definition.ToString(), null);
		}

		// Token: 0x060009D1 RID: 2513 RVA: 0x00019363 File Offset: 0x00017563
		public override string Visit(ResourceRangeVariableReferenceNode node)
		{
			ExceptionUtils.CheckArgumentNotNull<ResourceRangeVariableReferenceNode>(node, "node");
			if (node.Name == "$it")
			{
				return string.Empty;
			}
			return node.Name;
		}

		// Token: 0x060009D2 RID: 2514 RVA: 0x0001938F File Offset: 0x0001758F
		public override string Visit(NonResourceRangeVariableReferenceNode node)
		{
			ExceptionUtils.CheckArgumentNotNull<NonResourceRangeVariableReferenceNode>(node, "node");
			return node.Name;
		}

		// Token: 0x060009D3 RID: 2515 RVA: 0x000193A3 File Offset: 0x000175A3
		public override string Visit(SingleResourceCastNode node)
		{
			ExceptionUtils.CheckArgumentNotNull<SingleResourceCastNode>(node, "node");
			return this.TranslatePropertyAccess(node.Source, node.StructuredTypeReference.Definition.ToString(), null);
		}

		// Token: 0x060009D4 RID: 2516 RVA: 0x000193CE File Offset: 0x000175CE
		public override string Visit(SingleNavigationNode node)
		{
			ExceptionUtils.CheckArgumentNotNull<SingleNavigationNode>(node, "node");
			return this.TranslatePropertyAccess(node.Source, node.NavigationProperty.Name, node.NavigationSource);
		}

		// Token: 0x060009D5 RID: 2517 RVA: 0x000193FC File Offset: 0x000175FC
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

		// Token: 0x060009D6 RID: 2518 RVA: 0x00019440 File Offset: 0x00017640
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

		// Token: 0x060009D7 RID: 2519 RVA: 0x00019484 File Offset: 0x00017684
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

		// Token: 0x060009D8 RID: 2520 RVA: 0x000194C8 File Offset: 0x000176C8
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

		// Token: 0x060009D9 RID: 2521 RVA: 0x0001950C File Offset: 0x0001770C
		public override string Visit(SingleValueOpenPropertyAccessNode node)
		{
			ExceptionUtils.CheckArgumentNotNull<SingleValueOpenPropertyAccessNode>(node, "node");
			return this.TranslatePropertyAccess(node.Source, node.Name, null);
		}

		// Token: 0x060009DA RID: 2522 RVA: 0x0001952D File Offset: 0x0001772D
		public override string Visit(CollectionOpenPropertyAccessNode node)
		{
			ExceptionUtils.CheckArgumentNotNull<CollectionOpenPropertyAccessNode>(node, "node");
			return this.TranslatePropertyAccess(node.Source, node.Name, null);
		}

		// Token: 0x060009DB RID: 2523 RVA: 0x0001954E File Offset: 0x0001774E
		public override string Visit(SingleValuePropertyAccessNode node)
		{
			ExceptionUtils.CheckArgumentNotNull<SingleValuePropertyAccessNode>(node, "node");
			return this.TranslatePropertyAccess(node.Source, node.Property.Name, null);
		}

		// Token: 0x060009DC RID: 2524 RVA: 0x00019574 File Offset: 0x00017774
		public override string Visit(SingleComplexNode node)
		{
			ExceptionUtils.CheckArgumentNotNull<SingleComplexNode>(node, "node");
			return this.TranslatePropertyAccess(node.Source, node.Property.Name, null);
		}

		// Token: 0x060009DD RID: 2525 RVA: 0x0001959A File Offset: 0x0001779A
		public override string Visit(ParameterAliasNode node)
		{
			ExceptionUtils.CheckArgumentNotNull<ParameterAliasNode>(node, "node");
			return node.Alias;
		}

		// Token: 0x060009DE RID: 2526 RVA: 0x000195AE File Offset: 0x000177AE
		public override string Visit(NamedFunctionParameterNode node)
		{
			ExceptionUtils.CheckArgumentNotNull<NamedFunctionParameterNode>(node, "node");
			return node.Name + "=" + this.TranslateNode(node.Value);
		}

		// Token: 0x060009DF RID: 2527 RVA: 0x000195D8 File Offset: 0x000177D8
		public override string Visit(SearchTermNode node)
		{
			ExceptionUtils.CheckArgumentNotNull<SearchTermNode>(node, "node");
			if (!NodeToStringBuilder.IsValidSearchWord(node.Text))
			{
				return "\"" + node.Text + "\"";
			}
			return node.Text;
		}

		// Token: 0x060009E0 RID: 2528 RVA: 0x00019610 File Offset: 0x00017810
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

		// Token: 0x060009E1 RID: 2529 RVA: 0x000196AC File Offset: 0x000178AC
		internal static string TranslateLevelsClause(LevelsClause levelsClause)
		{
			return levelsClause.IsMaxLevel ? "max" : levelsClause.Level.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x060009E2 RID: 2530 RVA: 0x000196DD File Offset: 0x000178DD
		internal string TranslateNode(QueryNode node)
		{
			return node.Accept<string>(this);
		}

		// Token: 0x060009E3 RID: 2531 RVA: 0x000196E6 File Offset: 0x000178E6
		internal string TranslateFilterClause(FilterClause filterClause)
		{
			return this.TranslateNode(filterClause.Expression);
		}

		// Token: 0x060009E4 RID: 2532 RVA: 0x000196F4 File Offset: 0x000178F4
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

		// Token: 0x060009E5 RID: 2533 RVA: 0x0001974C File Offset: 0x0001794C
		internal string TranslateSearchClause(SearchClause searchClause)
		{
			this.searchFlag = true;
			string text = this.TranslateNode(searchClause.Expression);
			this.searchFlag = false;
			return text;
		}

		// Token: 0x060009E6 RID: 2534 RVA: 0x00019778 File Offset: 0x00017978
		internal string TranslateComputeClause(ComputeClause computeClause)
		{
			bool flag = false;
			StringBuilder stringBuilder = new StringBuilder();
			foreach (ComputeExpression computeExpression in computeClause.ComputedItems)
			{
				if (flag)
				{
					stringBuilder.Append(",");
				}
				else
				{
					flag = true;
				}
				stringBuilder.Append(this.TranslateNode(computeExpression.Expression));
				stringBuilder.Append("%20");
				stringBuilder.Append("as");
				stringBuilder.Append("%20");
				stringBuilder.Append(computeExpression.Alias);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060009E7 RID: 2535 RVA: 0x00019824 File Offset: 0x00017A24
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

		// Token: 0x060009E8 RID: 2536 RVA: 0x000198D0 File Offset: 0x00017AD0
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

		// Token: 0x060009E9 RID: 2537 RVA: 0x00019914 File Offset: 0x00017B14
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

		// Token: 0x060009EA RID: 2538 RVA: 0x00019998 File Offset: 0x00017B98
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

		// Token: 0x060009EB RID: 2539 RVA: 0x00019A58 File Offset: 0x00017C58
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

		// Token: 0x060009EC RID: 2540 RVA: 0x00019AB4 File Offset: 0x00017CB4
		private static bool IsValidSearchWord(string text)
		{
			Match match = SearchLexer.InvalidWordPattern.Match(text);
			return !match.Success && !string.Equals(text, "AND", StringComparison.Ordinal) && !string.Equals(text, "OR", StringComparison.Ordinal) && !string.Equals(text, "NOT", StringComparison.Ordinal);
		}

		// Token: 0x040003AB RID: 939
		private bool searchFlag;
	}
}
