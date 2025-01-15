using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text.RegularExpressions;
using Microsoft.OData.Core.UriParser;
using Microsoft.OData.Core.UriParser.Semantic;
using Microsoft.OData.Core.UriParser.TreeNodeKinds;
using Microsoft.OData.Core.UriParser.Visitors;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.UriBuilder
{
	// Token: 0x020001BC RID: 444
	internal sealed class NodeToStringBuilder : QueryNodeVisitor<string>
	{
		// Token: 0x06001083 RID: 4227 RVA: 0x000395EC File Offset: 0x000377EC
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

		// Token: 0x06001084 RID: 4228 RVA: 0x00039668 File Offset: 0x00037868
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

		// Token: 0x06001085 RID: 4229 RVA: 0x00039734 File Offset: 0x00037934
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

		// Token: 0x06001086 RID: 4230 RVA: 0x00039820 File Offset: 0x00037A20
		public override string Visit(CountNode node)
		{
			ExceptionUtils.CheckArgumentNotNull<CountNode>(node, "node");
			string text = this.TranslateNode(node.Source);
			return text + "/" + "$count";
		}

		// Token: 0x06001087 RID: 4231 RVA: 0x00039855 File Offset: 0x00037A55
		public override string Visit(CollectionNavigationNode node)
		{
			ExceptionUtils.CheckArgumentNotNull<CollectionNavigationNode>(node, "node");
			return this.TranslatePropertyAccess(node.Source, node.NavigationProperty.Name, node.NavigationSource);
		}

		// Token: 0x06001088 RID: 4232 RVA: 0x0003987F File Offset: 0x00037A7F
		public override string Visit(CollectionPropertyAccessNode node)
		{
			ExceptionUtils.CheckArgumentNotNull<CollectionPropertyAccessNode>(node, "node");
			return this.TranslatePropertyAccess(node.Source, node.Property.Name, null);
		}

		// Token: 0x06001089 RID: 4233 RVA: 0x000398A4 File Offset: 0x00037AA4
		public override string Visit(ConstantNode node)
		{
			ExceptionUtils.CheckArgumentNotNull<ConstantNode>(node, "node");
			if (node.Value == null)
			{
				return "null";
			}
			return node.LiteralText;
		}

		// Token: 0x0600108A RID: 4234 RVA: 0x000398C5 File Offset: 0x00037AC5
		public override string Visit(ConvertNode node)
		{
			ExceptionUtils.CheckArgumentNotNull<ConvertNode>(node, "node");
			return this.TranslateNode(node.Source);
		}

		// Token: 0x0600108B RID: 4235 RVA: 0x000398DE File Offset: 0x00037ADE
		public override string Visit(EntityCollectionCastNode node)
		{
			ExceptionUtils.CheckArgumentNotNull<EntityCollectionCastNode>(node, "node");
			return this.TranslatePropertyAccess(node.Source, node.EntityItemType.Definition.ToString(), null);
		}

		// Token: 0x0600108C RID: 4236 RVA: 0x00039908 File Offset: 0x00037B08
		public override string Visit(CollectionPropertyCastNode node)
		{
			ExceptionUtils.CheckArgumentNotNull<CollectionPropertyCastNode>(node, "node");
			return this.TranslatePropertyAccess(node.Source, node.CollectionType.Definition.ToString(), null);
		}

		// Token: 0x0600108D RID: 4237 RVA: 0x00039932 File Offset: 0x00037B32
		public override string Visit(EntityRangeVariableReferenceNode node)
		{
			ExceptionUtils.CheckArgumentNotNull<EntityRangeVariableReferenceNode>(node, "node");
			if (node.Name == "$it")
			{
				return string.Empty;
			}
			return node.Name;
		}

		// Token: 0x0600108E RID: 4238 RVA: 0x0003995D File Offset: 0x00037B5D
		public override string Visit(NonentityRangeVariableReferenceNode node)
		{
			ExceptionUtils.CheckArgumentNotNull<NonentityRangeVariableReferenceNode>(node, "node");
			return node.Name;
		}

		// Token: 0x0600108F RID: 4239 RVA: 0x00039970 File Offset: 0x00037B70
		public override string Visit(SingleEntityCastNode node)
		{
			ExceptionUtils.CheckArgumentNotNull<SingleEntityCastNode>(node, "node");
			return this.TranslatePropertyAccess(node.Source, node.EntityTypeReference.Definition.ToString(), null);
		}

		// Token: 0x06001090 RID: 4240 RVA: 0x0003999A File Offset: 0x00037B9A
		public override string Visit(SingleValueCastNode node)
		{
			ExceptionUtils.CheckArgumentNotNull<SingleValueCastNode>(node, "node");
			return this.TranslatePropertyAccess(node.Source, node.TypeReference.Definition.ToString(), null);
		}

		// Token: 0x06001091 RID: 4241 RVA: 0x000399C4 File Offset: 0x00037BC4
		public override string Visit(SingleNavigationNode node)
		{
			ExceptionUtils.CheckArgumentNotNull<SingleNavigationNode>(node, "node");
			return this.TranslatePropertyAccess(node.Source, node.NavigationProperty.Name, node.NavigationSource);
		}

		// Token: 0x06001092 RID: 4242 RVA: 0x000399F0 File Offset: 0x00037BF0
		public override string Visit(SingleEntityFunctionCallNode node)
		{
			ExceptionUtils.CheckArgumentNotNull<SingleEntityFunctionCallNode>(node, "node");
			string text = node.Name;
			if (node.Source != null)
			{
				text = this.TranslatePropertyAccess(node.Source, text, null);
			}
			return this.TranslateFunctionCall(text, node.Parameters);
		}

		// Token: 0x06001093 RID: 4243 RVA: 0x00039A34 File Offset: 0x00037C34
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

		// Token: 0x06001094 RID: 4244 RVA: 0x00039A78 File Offset: 0x00037C78
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

		// Token: 0x06001095 RID: 4245 RVA: 0x00039ABC File Offset: 0x00037CBC
		public override string Visit(EntityCollectionFunctionCallNode node)
		{
			ExceptionUtils.CheckArgumentNotNull<EntityCollectionFunctionCallNode>(node, "node");
			string text = node.Name;
			if (node.Source != null)
			{
				text = this.TranslatePropertyAccess(node.Source, text, null);
			}
			return this.TranslateFunctionCall(text, node.Parameters);
		}

		// Token: 0x06001096 RID: 4246 RVA: 0x00039AFF File Offset: 0x00037CFF
		public override string Visit(SingleValueOpenPropertyAccessNode node)
		{
			ExceptionUtils.CheckArgumentNotNull<SingleValueOpenPropertyAccessNode>(node, "node");
			return this.TranslatePropertyAccess(node.Source, node.Name, null);
		}

		// Token: 0x06001097 RID: 4247 RVA: 0x00039B1F File Offset: 0x00037D1F
		public override string Visit(CollectionOpenPropertyAccessNode node)
		{
			ExceptionUtils.CheckArgumentNotNull<CollectionOpenPropertyAccessNode>(node, "node");
			return this.TranslatePropertyAccess(node.Source, node.Name, null);
		}

		// Token: 0x06001098 RID: 4248 RVA: 0x00039B3F File Offset: 0x00037D3F
		public override string Visit(SingleValuePropertyAccessNode node)
		{
			ExceptionUtils.CheckArgumentNotNull<SingleValuePropertyAccessNode>(node, "node");
			return this.TranslatePropertyAccess(node.Source, node.Property.Name, null);
		}

		// Token: 0x06001099 RID: 4249 RVA: 0x00039B64 File Offset: 0x00037D64
		public override string Visit(ParameterAliasNode node)
		{
			ExceptionUtils.CheckArgumentNotNull<ParameterAliasNode>(node, "node");
			return node.Alias;
		}

		// Token: 0x0600109A RID: 4250 RVA: 0x00039B77 File Offset: 0x00037D77
		public override string Visit(NamedFunctionParameterNode node)
		{
			ExceptionUtils.CheckArgumentNotNull<NamedFunctionParameterNode>(node, "node");
			return node.Name + "=" + this.TranslateNode(node.Value);
		}

		// Token: 0x0600109B RID: 4251 RVA: 0x00039BA0 File Offset: 0x00037DA0
		public override string Visit(SearchTermNode node)
		{
			ExceptionUtils.CheckArgumentNotNull<SearchTermNode>(node, "node");
			if (!NodeToStringBuilder.IsValidSearchWord(node.Text))
			{
				return "\"" + node.Text + "\"";
			}
			return node.Text;
		}

		// Token: 0x0600109C RID: 4252 RVA: 0x00039BD8 File Offset: 0x00037DD8
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

		// Token: 0x0600109D RID: 4253 RVA: 0x00039C74 File Offset: 0x00037E74
		internal static string TranslateLevelsClause(LevelsClause levelsClause)
		{
			return levelsClause.IsMaxLevel ? "max" : levelsClause.Level.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x0600109E RID: 4254 RVA: 0x00039CA5 File Offset: 0x00037EA5
		internal string TranslateNode(QueryNode node)
		{
			return node.Accept<string>(this);
		}

		// Token: 0x0600109F RID: 4255 RVA: 0x00039CAE File Offset: 0x00037EAE
		internal string TranslateFilterClause(FilterClause filterClause)
		{
			return this.TranslateNode(filterClause.Expression);
		}

		// Token: 0x060010A0 RID: 4256 RVA: 0x00039CBC File Offset: 0x00037EBC
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

		// Token: 0x060010A1 RID: 4257 RVA: 0x00039D14 File Offset: 0x00037F14
		internal string TranslateSearchClause(SearchClause searchClause)
		{
			this.searchFlag = true;
			string text = this.TranslateNode(searchClause.Expression);
			this.searchFlag = false;
			return text;
		}

		// Token: 0x060010A2 RID: 4258 RVA: 0x00039D40 File Offset: 0x00037F40
		[SuppressMessage("DataWeb.Usage", "AC0018:SystemUriEscapeDataStringRule", Justification = "Values passed to this method are property names and not literals.")]
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

		// Token: 0x060010A3 RID: 4259 RVA: 0x00039DF4 File Offset: 0x00037FF4
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

		// Token: 0x060010A4 RID: 4260 RVA: 0x00039E38 File Offset: 0x00038038
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

		// Token: 0x060010A5 RID: 4261 RVA: 0x00039EBC File Offset: 0x000380BC
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

		// Token: 0x060010A6 RID: 4262 RVA: 0x00039F7C File Offset: 0x0003817C
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

		// Token: 0x060010A7 RID: 4263 RVA: 0x00039FD8 File Offset: 0x000381D8
		private static bool IsValidSearchWord(string text)
		{
			Match match = SearchLexer.InvalidWordPattern.Match(text);
			return !match.Success && !string.Equals(text, "AND") && !string.Equals(text, "OR") && !string.Equals(text, "NOT");
		}

		// Token: 0x0400076B RID: 1899
		private bool searchFlag;
	}
}
