using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Globalization;
using System.Linq;
using System.Text;

namespace System.Data.Entity.Core.Common.CommandTrees.Internal
{
	// Token: 0x020006EE RID: 1774
	internal class ExpressionPrinter : TreePrinter
	{
		// Token: 0x06005288 RID: 21128 RVA: 0x00128454 File Offset: 0x00126654
		internal string Print(DbDeleteCommandTree tree)
		{
			TreeNode treeNode;
			if (tree.Target != null)
			{
				treeNode = this._visitor.VisitBinding("Target", tree.Target);
			}
			else
			{
				treeNode = new TreeNode("Target", new TreeNode[0]);
			}
			TreeNode treeNode2;
			if (tree.Predicate != null)
			{
				treeNode2 = this._visitor.VisitExpression("Predicate", tree.Predicate);
			}
			else
			{
				treeNode2 = new TreeNode("Predicate", new TreeNode[0]);
			}
			return this.Print(new TreeNode("DbDeleteCommandTree", new TreeNode[]
			{
				ExpressionPrinter.CreateParametersNode(tree),
				treeNode,
				treeNode2
			}));
		}

		// Token: 0x06005289 RID: 21129 RVA: 0x001284EC File Offset: 0x001266EC
		internal string Print(DbFunctionCommandTree tree)
		{
			TreeNode treeNode = new TreeNode("EdmFunction", new TreeNode[0]);
			if (tree.EdmFunction != null)
			{
				treeNode.Children.Add(this._visitor.VisitFunction(tree.EdmFunction, null));
			}
			TreeNode treeNode2 = new TreeNode("ResultType", new TreeNode[0]);
			if (tree.ResultType != null)
			{
				ExpressionPrinter.PrinterVisitor.AppendTypeSpecifier(treeNode2, tree.ResultType);
			}
			return this.Print(new TreeNode("DbFunctionCommandTree", new TreeNode[]
			{
				ExpressionPrinter.CreateParametersNode(tree),
				treeNode,
				treeNode2
			}));
		}

		// Token: 0x0600528A RID: 21130 RVA: 0x0012857C File Offset: 0x0012677C
		internal string Print(DbInsertCommandTree tree)
		{
			TreeNode treeNode = null;
			if (tree.Target != null)
			{
				treeNode = this._visitor.VisitBinding("Target", tree.Target);
			}
			else
			{
				treeNode = new TreeNode("Target", new TreeNode[0]);
			}
			TreeNode treeNode2 = new TreeNode("SetClauses", new TreeNode[0]);
			foreach (DbModificationClause dbModificationClause in tree.SetClauses)
			{
				if (dbModificationClause != null)
				{
					treeNode2.Children.Add(dbModificationClause.Print(this._visitor));
				}
			}
			TreeNode treeNode3;
			if (tree.Returning != null)
			{
				treeNode3 = new TreeNode("Returning", new TreeNode[] { this._visitor.VisitExpression(tree.Returning) });
			}
			else
			{
				treeNode3 = new TreeNode("Returning", new TreeNode[0]);
			}
			return this.Print(new TreeNode("DbInsertCommandTree", new TreeNode[]
			{
				ExpressionPrinter.CreateParametersNode(tree),
				treeNode,
				treeNode2,
				treeNode3
			}));
		}

		// Token: 0x0600528B RID: 21131 RVA: 0x00128694 File Offset: 0x00126894
		internal string Print(DbUpdateCommandTree tree)
		{
			TreeNode treeNode = null;
			if (tree.Target != null)
			{
				treeNode = this._visitor.VisitBinding("Target", tree.Target);
			}
			else
			{
				treeNode = new TreeNode("Target", new TreeNode[0]);
			}
			TreeNode treeNode2 = new TreeNode("SetClauses", new TreeNode[0]);
			foreach (DbModificationClause dbModificationClause in tree.SetClauses)
			{
				if (dbModificationClause != null)
				{
					treeNode2.Children.Add(dbModificationClause.Print(this._visitor));
				}
			}
			TreeNode treeNode3;
			if (tree.Predicate != null)
			{
				treeNode3 = new TreeNode("Predicate", new TreeNode[] { this._visitor.VisitExpression(tree.Predicate) });
			}
			else
			{
				treeNode3 = new TreeNode("Predicate", new TreeNode[0]);
			}
			TreeNode treeNode4;
			if (tree.Returning != null)
			{
				treeNode4 = new TreeNode("Returning", new TreeNode[] { this._visitor.VisitExpression(tree.Returning) });
			}
			else
			{
				treeNode4 = new TreeNode("Returning", new TreeNode[0]);
			}
			return this.Print(new TreeNode("DbUpdateCommandTree", new TreeNode[]
			{
				ExpressionPrinter.CreateParametersNode(tree),
				treeNode,
				treeNode2,
				treeNode3,
				treeNode4
			}));
		}

		// Token: 0x0600528C RID: 21132 RVA: 0x001287F0 File Offset: 0x001269F0
		internal string Print(DbQueryCommandTree tree)
		{
			TreeNode treeNode = new TreeNode("Query", new TreeNode[0]);
			if (tree.Query != null)
			{
				ExpressionPrinter.PrinterVisitor.AppendTypeSpecifier(treeNode, tree.Query.ResultType);
				treeNode.Children.Add(this._visitor.VisitExpression(tree.Query));
			}
			return this.Print(new TreeNode("DbQueryCommandTree", new TreeNode[]
			{
				ExpressionPrinter.CreateParametersNode(tree),
				treeNode
			}));
		}

		// Token: 0x0600528D RID: 21133 RVA: 0x00128868 File Offset: 0x00126A68
		private static TreeNode CreateParametersNode(DbCommandTree tree)
		{
			TreeNode treeNode = new TreeNode("Parameters", new TreeNode[0]);
			foreach (KeyValuePair<string, TypeUsage> keyValuePair in tree.Parameters)
			{
				TreeNode treeNode2 = new TreeNode(keyValuePair.Key, new TreeNode[0]);
				ExpressionPrinter.PrinterVisitor.AppendTypeSpecifier(treeNode2, keyValuePair.Value);
				treeNode.Children.Add(treeNode2);
			}
			return treeNode;
		}

		// Token: 0x04001DDB RID: 7643
		private readonly ExpressionPrinter.PrinterVisitor _visitor = new ExpressionPrinter.PrinterVisitor();

		// Token: 0x02000C97 RID: 3223
		private class PrinterVisitor : DbExpressionVisitor<TreeNode>
		{
			// Token: 0x06006BE5 RID: 27621 RVA: 0x00170540 File Offset: 0x0016E740
			private static Dictionary<DbExpressionKind, string> InitializeOpMap()
			{
				Dictionary<DbExpressionKind, string> dictionary = new Dictionary<DbExpressionKind, string>(12);
				dictionary[DbExpressionKind.Divide] = "/";
				dictionary[DbExpressionKind.Modulo] = "%";
				dictionary[DbExpressionKind.Multiply] = "*";
				dictionary[DbExpressionKind.Plus] = "+";
				dictionary[DbExpressionKind.Minus] = "-";
				dictionary[DbExpressionKind.UnaryMinus] = "-";
				dictionary[DbExpressionKind.Equals] = "=";
				dictionary[DbExpressionKind.LessThan] = "<";
				dictionary[DbExpressionKind.LessThanOrEquals] = "<=";
				dictionary[DbExpressionKind.GreaterThan] = ">";
				dictionary[DbExpressionKind.GreaterThanOrEquals] = ">=";
				dictionary[DbExpressionKind.NotEquals] = "<>";
				return dictionary;
			}

			// Token: 0x06006BE6 RID: 27622 RVA: 0x001705F0 File Offset: 0x0016E7F0
			internal TreeNode VisitExpression(DbExpression expr)
			{
				return expr.Accept<TreeNode>(this);
			}

			// Token: 0x06006BE7 RID: 27623 RVA: 0x001705F9 File Offset: 0x0016E7F9
			internal TreeNode VisitExpression(string name, DbExpression expr)
			{
				return new TreeNode(name, new TreeNode[] { expr.Accept<TreeNode>(this) });
			}

			// Token: 0x06006BE8 RID: 27624 RVA: 0x00170611 File Offset: 0x0016E811
			internal TreeNode VisitBinding(string propName, DbExpressionBinding binding)
			{
				return this.VisitWithLabel(propName, binding.VariableName, binding.Expression);
			}

			// Token: 0x06006BE9 RID: 27625 RVA: 0x00170628 File Offset: 0x0016E828
			internal TreeNode VisitFunction(EdmFunction func, IList<DbExpression> args)
			{
				TreeNode treeNode = new TreeNode();
				ExpressionPrinter.PrinterVisitor.AppendFullName(treeNode.Text, func);
				ExpressionPrinter.PrinterVisitor.AppendParameters(treeNode, func.Parameters.Select((FunctionParameter fp) => new KeyValuePair<string, TypeUsage>(fp.Name, fp.TypeUsage)));
				if (args != null)
				{
					this.AppendArguments(treeNode, func.Parameters.Select((FunctionParameter fp) => fp.Name).ToArray<string>(), args);
				}
				return treeNode;
			}

			// Token: 0x06006BEA RID: 27626 RVA: 0x001706B2 File Offset: 0x0016E8B2
			private static TreeNode NodeFromExpression(DbExpression expr)
			{
				return new TreeNode(Enum.GetName(typeof(DbExpressionKind), expr.ExpressionKind), new TreeNode[0]);
			}

			// Token: 0x06006BEB RID: 27627 RVA: 0x001706DC File Offset: 0x0016E8DC
			private static void AppendParameters(TreeNode node, IEnumerable<KeyValuePair<string, TypeUsage>> paramInfos)
			{
				node.Text.Append("(");
				int num = 0;
				foreach (KeyValuePair<string, TypeUsage> keyValuePair in paramInfos)
				{
					if (num > 0)
					{
						node.Text.Append(", ");
					}
					ExpressionPrinter.PrinterVisitor.AppendType(node, keyValuePair.Value);
					node.Text.Append(" ");
					node.Text.Append(keyValuePair.Key);
					num++;
				}
				node.Text.Append(")");
			}

			// Token: 0x06006BEC RID: 27628 RVA: 0x0017078C File Offset: 0x0016E98C
			internal static void AppendTypeSpecifier(TreeNode node, TypeUsage type)
			{
				node.Text.Append(" : ");
				ExpressionPrinter.PrinterVisitor.AppendType(node, type);
			}

			// Token: 0x06006BED RID: 27629 RVA: 0x001707A6 File Offset: 0x0016E9A6
			internal static void AppendType(TreeNode node, TypeUsage type)
			{
				ExpressionPrinter.PrinterVisitor.BuildTypeName(node.Text, type);
			}

			// Token: 0x06006BEE RID: 27630 RVA: 0x001707B4 File Offset: 0x0016E9B4
			private static void BuildTypeName(StringBuilder text, TypeUsage type)
			{
				RowType rowType = type.EdmType as RowType;
				CollectionType collectionType = type.EdmType as CollectionType;
				RefType refType = type.EdmType as RefType;
				if (TypeSemantics.IsPrimitiveType(type))
				{
					text.Append(type);
					return;
				}
				if (collectionType != null)
				{
					text.Append("Collection{");
					ExpressionPrinter.PrinterVisitor.BuildTypeName(text, collectionType.TypeUsage);
					text.Append("}");
					return;
				}
				if (refType != null)
				{
					text.Append("Ref<");
					ExpressionPrinter.PrinterVisitor.AppendFullName(text, refType.ElementType);
					text.Append(">");
					return;
				}
				if (rowType != null)
				{
					text.Append("Record[");
					int num = 0;
					foreach (EdmProperty edmProperty in rowType.Properties)
					{
						text.Append("'");
						text.Append(edmProperty.Name);
						text.Append("'");
						text.Append("=");
						ExpressionPrinter.PrinterVisitor.BuildTypeName(text, edmProperty.TypeUsage);
						num++;
						if (num < rowType.Properties.Count)
						{
							text.Append(", ");
						}
					}
					text.Append("]");
					return;
				}
				if (!string.IsNullOrEmpty(type.EdmType.NamespaceName))
				{
					text.Append(type.EdmType.NamespaceName);
					text.Append(".");
				}
				text.Append(type.EdmType.Name);
			}

			// Token: 0x06006BEF RID: 27631 RVA: 0x00170948 File Offset: 0x0016EB48
			private static void AppendFullName(StringBuilder text, EdmType type)
			{
				if (BuiltInTypeKind.RowType != type.BuiltInTypeKind && !string.IsNullOrEmpty(type.NamespaceName))
				{
					text.Append(type.NamespaceName);
					text.Append(".");
				}
				text.Append(type.Name);
			}

			// Token: 0x06006BF0 RID: 27632 RVA: 0x00170988 File Offset: 0x0016EB88
			private List<TreeNode> VisitParams(IList<string> paramInfo, IList<DbExpression> args)
			{
				List<TreeNode> list = new List<TreeNode>();
				for (int i = 0; i < paramInfo.Count; i++)
				{
					list.Add(new TreeNode(paramInfo[i], new TreeNode[0])
					{
						Children = { this.VisitExpression(args[i]) }
					});
				}
				return list;
			}

			// Token: 0x06006BF1 RID: 27633 RVA: 0x001709DF File Offset: 0x0016EBDF
			private void AppendArguments(TreeNode node, IList<string> paramNames, IList<DbExpression> args)
			{
				if (paramNames.Count > 0)
				{
					node.Children.Add(new TreeNode("Arguments", this.VisitParams(paramNames, args)));
				}
			}

			// Token: 0x06006BF2 RID: 27634 RVA: 0x00170A08 File Offset: 0x0016EC08
			private TreeNode VisitWithLabel(string label, string name, DbExpression def)
			{
				TreeNode treeNode = new TreeNode(label, new TreeNode[0]);
				treeNode.Text.Append(" : '");
				treeNode.Text.Append(name);
				treeNode.Text.Append("'");
				treeNode.Children.Add(this.VisitExpression(def));
				return treeNode;
			}

			// Token: 0x06006BF3 RID: 27635 RVA: 0x00170A64 File Offset: 0x0016EC64
			private TreeNode VisitBindingList(string propName, IList<DbExpressionBinding> bindings)
			{
				List<TreeNode> list = new List<TreeNode>();
				for (int i = 0; i < bindings.Count; i++)
				{
					list.Add(this.VisitBinding(StringUtil.FormatIndex(propName, i), bindings[i]));
				}
				return new TreeNode(propName, list);
			}

			// Token: 0x06006BF4 RID: 27636 RVA: 0x00170AAC File Offset: 0x0016ECAC
			private TreeNode VisitGroupBinding(DbGroupExpressionBinding groupBinding)
			{
				TreeNode treeNode = this.VisitExpression(groupBinding.Expression);
				TreeNode treeNode2 = new TreeNode();
				treeNode2.Children.Add(treeNode);
				treeNode2.Text.AppendFormat(CultureInfo.InvariantCulture, "Input : '{0}', '{1}'", new object[] { groupBinding.VariableName, groupBinding.GroupVariableName });
				return treeNode2;
			}

			// Token: 0x06006BF5 RID: 27637 RVA: 0x00170B08 File Offset: 0x0016ED08
			private TreeNode Visit(string name, params DbExpression[] exprs)
			{
				TreeNode treeNode = new TreeNode(name, new TreeNode[0]);
				foreach (DbExpression dbExpression in exprs)
				{
					treeNode.Children.Add(this.VisitExpression(dbExpression));
				}
				return treeNode;
			}

			// Token: 0x06006BF6 RID: 27638 RVA: 0x00170B4C File Offset: 0x0016ED4C
			private TreeNode VisitInfix(DbExpression left, string name, DbExpression right)
			{
				if (this._infix)
				{
					return new TreeNode("", new TreeNode[0])
					{
						Children = 
						{
							this.VisitExpression(left),
							new TreeNode(name, new TreeNode[0]),
							this.VisitExpression(right)
						}
					};
				}
				return this.Visit(name, new DbExpression[] { left, right });
			}

			// Token: 0x06006BF7 RID: 27639 RVA: 0x00170BC4 File Offset: 0x0016EDC4
			private TreeNode VisitUnary(DbUnaryExpression expr)
			{
				return this.VisitUnary(expr, false);
			}

			// Token: 0x06006BF8 RID: 27640 RVA: 0x00170BD0 File Offset: 0x0016EDD0
			private TreeNode VisitUnary(DbUnaryExpression expr, bool appendType)
			{
				TreeNode treeNode = ExpressionPrinter.PrinterVisitor.NodeFromExpression(expr);
				if (appendType)
				{
					ExpressionPrinter.PrinterVisitor.AppendTypeSpecifier(treeNode, expr.ResultType);
				}
				treeNode.Children.Add(this.VisitExpression(expr.Argument));
				return treeNode;
			}

			// Token: 0x06006BF9 RID: 27641 RVA: 0x00170C0B File Offset: 0x0016EE0B
			private TreeNode VisitBinary(DbBinaryExpression expr)
			{
				TreeNode treeNode = ExpressionPrinter.PrinterVisitor.NodeFromExpression(expr);
				treeNode.Children.Add(this.VisitExpression(expr.Left));
				treeNode.Children.Add(this.VisitExpression(expr.Right));
				return treeNode;
			}

			// Token: 0x06006BFA RID: 27642 RVA: 0x00170C41 File Offset: 0x0016EE41
			public override TreeNode Visit(DbExpression e)
			{
				Check.NotNull<DbExpression>(e, "e");
				throw new NotSupportedException(Strings.Cqt_General_UnsupportedExpression(e.GetType().FullName));
			}

			// Token: 0x06006BFB RID: 27643 RVA: 0x00170C64 File Offset: 0x0016EE64
			public override TreeNode Visit(DbConstantExpression e)
			{
				Check.NotNull<DbConstantExpression>(e, "e");
				TreeNode treeNode = new TreeNode();
				string text = e.Value as string;
				if (text != null)
				{
					text = text.Replace("\r\n", "\\r\\n");
					int num = text.Length;
					if (this._maxStringLength > 0)
					{
						num = Math.Min(text.Length, this._maxStringLength);
					}
					treeNode.Text.Append("'");
					treeNode.Text.Append(text, 0, num);
					if (text.Length > num)
					{
						treeNode.Text.Append("...");
					}
					treeNode.Text.Append("'");
				}
				else
				{
					treeNode.Text.Append(e.Value);
				}
				return treeNode;
			}

			// Token: 0x06006BFC RID: 27644 RVA: 0x00170D28 File Offset: 0x0016EF28
			public override TreeNode Visit(DbNullExpression e)
			{
				Check.NotNull<DbNullExpression>(e, "e");
				return new TreeNode("null", new TreeNode[0]);
			}

			// Token: 0x06006BFD RID: 27645 RVA: 0x00170D46 File Offset: 0x0016EF46
			public override TreeNode Visit(DbVariableReferenceExpression e)
			{
				Check.NotNull<DbVariableReferenceExpression>(e, "e");
				TreeNode treeNode = new TreeNode();
				treeNode.Text.AppendFormat("Var({0})", e.VariableName);
				return treeNode;
			}

			// Token: 0x06006BFE RID: 27646 RVA: 0x00170D70 File Offset: 0x0016EF70
			public override TreeNode Visit(DbParameterReferenceExpression e)
			{
				Check.NotNull<DbParameterReferenceExpression>(e, "e");
				TreeNode treeNode = new TreeNode();
				treeNode.Text.AppendFormat("@{0}", e.ParameterName);
				return treeNode;
			}

			// Token: 0x06006BFF RID: 27647 RVA: 0x00170D9A File Offset: 0x0016EF9A
			public override TreeNode Visit(DbFunctionExpression e)
			{
				Check.NotNull<DbFunctionExpression>(e, "e");
				return this.VisitFunction(e.Function, e.Arguments);
			}

			// Token: 0x06006C00 RID: 27648 RVA: 0x00170DBC File Offset: 0x0016EFBC
			public override TreeNode Visit(DbLambdaExpression expression)
			{
				Check.NotNull<DbLambdaExpression>(expression, "expression");
				TreeNode treeNode = new TreeNode();
				treeNode.Text.Append("Lambda");
				ExpressionPrinter.PrinterVisitor.AppendParameters(treeNode, expression.Lambda.Variables.Select((DbVariableReferenceExpression v) => new KeyValuePair<string, TypeUsage>(v.VariableName, v.ResultType)));
				this.AppendArguments(treeNode, expression.Lambda.Variables.Select((DbVariableReferenceExpression v) => v.VariableName).ToArray<string>(), expression.Arguments);
				treeNode.Children.Add(this.Visit("Body", new DbExpression[] { expression.Lambda.Body }));
				return treeNode;
			}

			// Token: 0x06006C01 RID: 27649 RVA: 0x00170E90 File Offset: 0x0016F090
			public override TreeNode Visit(DbPropertyExpression e)
			{
				Check.NotNull<DbPropertyExpression>(e, "e");
				TreeNode treeNode = null;
				if (e.Instance != null)
				{
					treeNode = this.VisitExpression(e.Instance);
					if (e.Instance.ExpressionKind == DbExpressionKind.VariableReference || (e.Instance.ExpressionKind == DbExpressionKind.Property && treeNode.Children.Count == 0))
					{
						treeNode.Text.Append(".");
						treeNode.Text.Append(e.Property.Name);
						return treeNode;
					}
				}
				TreeNode treeNode2 = new TreeNode(".", new TreeNode[0]);
				EdmProperty edmProperty = e.Property as EdmProperty;
				if (edmProperty != null && !(edmProperty.DeclaringType is RowType))
				{
					ExpressionPrinter.PrinterVisitor.AppendFullName(treeNode2.Text, edmProperty.DeclaringType);
					treeNode2.Text.Append(".");
				}
				treeNode2.Text.Append(e.Property.Name);
				if (treeNode != null)
				{
					treeNode2.Children.Add(new TreeNode("Instance", new TreeNode[] { treeNode }));
				}
				return treeNode2;
			}

			// Token: 0x06006C02 RID: 27650 RVA: 0x00170F9E File Offset: 0x0016F19E
			public override TreeNode Visit(DbComparisonExpression e)
			{
				Check.NotNull<DbComparisonExpression>(e, "e");
				return this.VisitInfix(e.Left, ExpressionPrinter.PrinterVisitor._opMap[e.ExpressionKind], e.Right);
			}

			// Token: 0x06006C03 RID: 27651 RVA: 0x00170FCE File Offset: 0x0016F1CE
			public override TreeNode Visit(DbLikeExpression e)
			{
				Check.NotNull<DbLikeExpression>(e, "e");
				return this.Visit("Like", new DbExpression[] { e.Argument, e.Pattern, e.Escape });
			}

			// Token: 0x06006C04 RID: 27652 RVA: 0x00171008 File Offset: 0x0016F208
			public override TreeNode Visit(DbLimitExpression e)
			{
				Check.NotNull<DbLimitExpression>(e, "e");
				return this.Visit(e.WithTies ? "LimitWithTies" : "Limit", new DbExpression[] { e.Argument, e.Limit });
			}

			// Token: 0x06006C05 RID: 27653 RVA: 0x00171048 File Offset: 0x0016F248
			public override TreeNode Visit(DbIsNullExpression e)
			{
				Check.NotNull<DbIsNullExpression>(e, "e");
				return this.VisitUnary(e);
			}

			// Token: 0x06006C06 RID: 27654 RVA: 0x00171060 File Offset: 0x0016F260
			public override TreeNode Visit(DbArithmeticExpression e)
			{
				Check.NotNull<DbArithmeticExpression>(e, "e");
				if (DbExpressionKind.UnaryMinus == e.ExpressionKind)
				{
					return this.Visit(ExpressionPrinter.PrinterVisitor._opMap[e.ExpressionKind], new DbExpression[] { e.Arguments[0] });
				}
				return this.VisitInfix(e.Arguments[0], ExpressionPrinter.PrinterVisitor._opMap[e.ExpressionKind], e.Arguments[1]);
			}

			// Token: 0x06006C07 RID: 27655 RVA: 0x001710DD File Offset: 0x0016F2DD
			public override TreeNode Visit(DbAndExpression e)
			{
				Check.NotNull<DbAndExpression>(e, "e");
				return this.VisitInfix(e.Left, "And", e.Right);
			}

			// Token: 0x06006C08 RID: 27656 RVA: 0x00171102 File Offset: 0x0016F302
			public override TreeNode Visit(DbOrExpression e)
			{
				Check.NotNull<DbOrExpression>(e, "e");
				return this.VisitInfix(e.Left, "Or", e.Right);
			}

			// Token: 0x06006C09 RID: 27657 RVA: 0x00171128 File Offset: 0x0016F328
			public override TreeNode Visit(DbInExpression e)
			{
				Check.NotNull<DbInExpression>(e, "e");
				TreeNode treeNode;
				if (this._infix)
				{
					treeNode = new TreeNode(string.Empty, new TreeNode[0]);
					treeNode.Children.Add(this.VisitExpression(e.Item));
					treeNode.Children.Add(new TreeNode("In", new TreeNode[0]));
				}
				else
				{
					treeNode = new TreeNode("In", new TreeNode[0]);
					treeNode.Children.Add(this.VisitExpression(e.Item));
				}
				foreach (DbExpression dbExpression in e.List)
				{
					treeNode.Children.Add(this.VisitExpression(dbExpression));
				}
				return treeNode;
			}

			// Token: 0x06006C0A RID: 27658 RVA: 0x00171204 File Offset: 0x0016F404
			public override TreeNode Visit(DbNotExpression e)
			{
				Check.NotNull<DbNotExpression>(e, "e");
				return this.VisitUnary(e);
			}

			// Token: 0x06006C0B RID: 27659 RVA: 0x00171219 File Offset: 0x0016F419
			public override TreeNode Visit(DbDistinctExpression e)
			{
				Check.NotNull<DbDistinctExpression>(e, "e");
				return this.VisitUnary(e);
			}

			// Token: 0x06006C0C RID: 27660 RVA: 0x0017122E File Offset: 0x0016F42E
			public override TreeNode Visit(DbElementExpression e)
			{
				Check.NotNull<DbElementExpression>(e, "e");
				return this.VisitUnary(e, true);
			}

			// Token: 0x06006C0D RID: 27661 RVA: 0x00171244 File Offset: 0x0016F444
			public override TreeNode Visit(DbIsEmptyExpression e)
			{
				Check.NotNull<DbIsEmptyExpression>(e, "e");
				return this.VisitUnary(e);
			}

			// Token: 0x06006C0E RID: 27662 RVA: 0x00171259 File Offset: 0x0016F459
			public override TreeNode Visit(DbUnionAllExpression e)
			{
				Check.NotNull<DbUnionAllExpression>(e, "e");
				return this.VisitBinary(e);
			}

			// Token: 0x06006C0F RID: 27663 RVA: 0x0017126E File Offset: 0x0016F46E
			public override TreeNode Visit(DbIntersectExpression e)
			{
				Check.NotNull<DbIntersectExpression>(e, "e");
				return this.VisitBinary(e);
			}

			// Token: 0x06006C10 RID: 27664 RVA: 0x00171283 File Offset: 0x0016F483
			public override TreeNode Visit(DbExceptExpression e)
			{
				Check.NotNull<DbExceptExpression>(e, "e");
				return this.VisitBinary(e);
			}

			// Token: 0x06006C11 RID: 27665 RVA: 0x00171298 File Offset: 0x0016F498
			private TreeNode VisitCastOrTreat(string op, DbUnaryExpression e)
			{
				TreeNode treeNode = this.VisitExpression(e.Argument);
				TreeNode treeNode2;
				if (treeNode.Children.Count == 0)
				{
					treeNode.Text.Insert(0, op);
					treeNode.Text.Insert(op.Length, '(');
					treeNode.Text.Append(" As ");
					ExpressionPrinter.PrinterVisitor.AppendType(treeNode, e.ResultType);
					treeNode.Text.Append(")");
					treeNode2 = treeNode;
				}
				else
				{
					treeNode2 = new TreeNode(op, new TreeNode[0]);
					ExpressionPrinter.PrinterVisitor.AppendTypeSpecifier(treeNode2, e.ResultType);
					treeNode2.Children.Add(treeNode);
				}
				return treeNode2;
			}

			// Token: 0x06006C12 RID: 27666 RVA: 0x0017133B File Offset: 0x0016F53B
			public override TreeNode Visit(DbTreatExpression e)
			{
				Check.NotNull<DbTreatExpression>(e, "e");
				return this.VisitCastOrTreat("Treat", e);
			}

			// Token: 0x06006C13 RID: 27667 RVA: 0x00171355 File Offset: 0x0016F555
			public override TreeNode Visit(DbCastExpression e)
			{
				Check.NotNull<DbCastExpression>(e, "e");
				return this.VisitCastOrTreat("Cast", e);
			}

			// Token: 0x06006C14 RID: 27668 RVA: 0x00171370 File Offset: 0x0016F570
			public override TreeNode Visit(DbIsOfExpression e)
			{
				Check.NotNull<DbIsOfExpression>(e, "e");
				TreeNode treeNode = new TreeNode();
				if (DbExpressionKind.IsOfOnly == e.ExpressionKind)
				{
					treeNode.Text.Append("IsOfOnly");
				}
				else
				{
					treeNode.Text.Append("IsOf");
				}
				ExpressionPrinter.PrinterVisitor.AppendTypeSpecifier(treeNode, e.OfType);
				treeNode.Children.Add(this.VisitExpression(e.Argument));
				return treeNode;
			}

			// Token: 0x06006C15 RID: 27669 RVA: 0x001713E4 File Offset: 0x0016F5E4
			public override TreeNode Visit(DbOfTypeExpression e)
			{
				Check.NotNull<DbOfTypeExpression>(e, "e");
				TreeNode treeNode = new TreeNode((e.ExpressionKind == DbExpressionKind.OfTypeOnly) ? "OfTypeOnly" : "OfType", new TreeNode[0]);
				ExpressionPrinter.PrinterVisitor.AppendTypeSpecifier(treeNode, e.OfType);
				treeNode.Children.Add(this.VisitExpression(e.Argument));
				return treeNode;
			}

			// Token: 0x06006C16 RID: 27670 RVA: 0x00171444 File Offset: 0x0016F644
			public override TreeNode Visit(DbCaseExpression e)
			{
				Check.NotNull<DbCaseExpression>(e, "e");
				TreeNode treeNode = new TreeNode("Case", new TreeNode[0]);
				for (int i = 0; i < e.When.Count; i++)
				{
					treeNode.Children.Add(this.Visit("When", new DbExpression[] { e.When[i] }));
					treeNode.Children.Add(this.Visit("Then", new DbExpression[] { e.Then[i] }));
				}
				treeNode.Children.Add(this.Visit("Else", new DbExpression[] { e.Else }));
				return treeNode;
			}

			// Token: 0x06006C17 RID: 27671 RVA: 0x00171500 File Offset: 0x0016F700
			public override TreeNode Visit(DbNewInstanceExpression e)
			{
				Check.NotNull<DbNewInstanceExpression>(e, "e");
				TreeNode treeNode = ExpressionPrinter.PrinterVisitor.NodeFromExpression(e);
				ExpressionPrinter.PrinterVisitor.AppendTypeSpecifier(treeNode, e.ResultType);
				if (BuiltInTypeKind.CollectionType == e.ResultType.EdmType.BuiltInTypeKind)
				{
					using (IEnumerator<DbExpression> enumerator = e.Arguments.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							DbExpression dbExpression = enumerator.Current;
							treeNode.Children.Add(this.VisitExpression(dbExpression));
						}
						return treeNode;
					}
				}
				string text = ((BuiltInTypeKind.RowType == e.ResultType.EdmType.BuiltInTypeKind) ? "Column" : "Property");
				IList<EdmProperty> properties = TypeHelpers.GetProperties(e.ResultType);
				for (int i = 0; i < properties.Count; i++)
				{
					treeNode.Children.Add(this.VisitWithLabel(text, properties[i].Name, e.Arguments[i]));
				}
				if (BuiltInTypeKind.EntityType == e.ResultType.EdmType.BuiltInTypeKind && e.HasRelatedEntityReferences)
				{
					TreeNode treeNode2 = new TreeNode("RelatedEntityReferences", new TreeNode[0]);
					foreach (DbRelatedEntityRef dbRelatedEntityRef in e.RelatedEntityReferences)
					{
						TreeNode treeNode3 = ExpressionPrinter.PrinterVisitor.CreateNavigationNode(dbRelatedEntityRef.SourceEnd, dbRelatedEntityRef.TargetEnd);
						treeNode3.Children.Add(ExpressionPrinter.PrinterVisitor.CreateRelationshipNode((RelationshipType)dbRelatedEntityRef.SourceEnd.DeclaringType));
						treeNode3.Children.Add(this.VisitExpression(dbRelatedEntityRef.TargetEntityReference));
						treeNode2.Children.Add(treeNode3);
					}
					treeNode.Children.Add(treeNode2);
				}
				return treeNode;
			}

			// Token: 0x06006C18 RID: 27672 RVA: 0x001716DC File Offset: 0x0016F8DC
			public override TreeNode Visit(DbRefExpression e)
			{
				Check.NotNull<DbRefExpression>(e, "e");
				TreeNode treeNode = new TreeNode("Ref", new TreeNode[0]);
				treeNode.Text.Append("<");
				ExpressionPrinter.PrinterVisitor.AppendFullName(treeNode.Text, TypeHelpers.GetEdmType<RefType>(e.ResultType).ElementType);
				treeNode.Text.Append(">");
				TreeNode treeNode2 = new TreeNode("EntitySet : ", new TreeNode[0]);
				treeNode2.Text.Append(e.EntitySet.EntityContainer.Name);
				treeNode2.Text.Append(".");
				treeNode2.Text.Append(e.EntitySet.Name);
				treeNode.Children.Add(treeNode2);
				treeNode.Children.Add(this.Visit("Keys", new DbExpression[] { e.Argument }));
				return treeNode;
			}

			// Token: 0x06006C19 RID: 27673 RVA: 0x001717CA File Offset: 0x0016F9CA
			private static TreeNode CreateRelationshipNode(RelationshipType relType)
			{
				TreeNode treeNode = new TreeNode("Relationship", new TreeNode[0]);
				treeNode.Text.Append(" : ");
				ExpressionPrinter.PrinterVisitor.AppendFullName(treeNode.Text, relType);
				return treeNode;
			}

			// Token: 0x06006C1A RID: 27674 RVA: 0x001717FC File Offset: 0x0016F9FC
			private static TreeNode CreateNavigationNode(RelationshipEndMember fromEnd, RelationshipEndMember toEnd)
			{
				TreeNode treeNode = new TreeNode();
				treeNode.Text.Append("Navigation : ");
				treeNode.Text.Append(fromEnd.Name);
				treeNode.Text.Append(" -> ");
				treeNode.Text.Append(toEnd.Name);
				return treeNode;
			}

			// Token: 0x06006C1B RID: 27675 RVA: 0x00171854 File Offset: 0x0016FA54
			public override TreeNode Visit(DbRelationshipNavigationExpression e)
			{
				Check.NotNull<DbRelationshipNavigationExpression>(e, "e");
				TreeNode treeNode = ExpressionPrinter.PrinterVisitor.NodeFromExpression(e);
				treeNode.Children.Add(ExpressionPrinter.PrinterVisitor.CreateRelationshipNode(e.Relationship));
				treeNode.Children.Add(ExpressionPrinter.PrinterVisitor.CreateNavigationNode(e.NavigateFrom, e.NavigateTo));
				treeNode.Children.Add(this.Visit("Source", new DbExpression[] { e.NavigationSource }));
				return treeNode;
			}

			// Token: 0x06006C1C RID: 27676 RVA: 0x001718CC File Offset: 0x0016FACC
			public override TreeNode Visit(DbDerefExpression e)
			{
				Check.NotNull<DbDerefExpression>(e, "e");
				return this.VisitUnary(e);
			}

			// Token: 0x06006C1D RID: 27677 RVA: 0x001718E1 File Offset: 0x0016FAE1
			public override TreeNode Visit(DbRefKeyExpression e)
			{
				Check.NotNull<DbRefKeyExpression>(e, "e");
				return this.VisitUnary(e, true);
			}

			// Token: 0x06006C1E RID: 27678 RVA: 0x001718F7 File Offset: 0x0016FAF7
			public override TreeNode Visit(DbEntityRefExpression e)
			{
				Check.NotNull<DbEntityRefExpression>(e, "e");
				return this.VisitUnary(e, true);
			}

			// Token: 0x06006C1F RID: 27679 RVA: 0x00171910 File Offset: 0x0016FB10
			public override TreeNode Visit(DbScanExpression e)
			{
				Check.NotNull<DbScanExpression>(e, "e");
				TreeNode treeNode = ExpressionPrinter.PrinterVisitor.NodeFromExpression(e);
				treeNode.Text.Append(" : ");
				treeNode.Text.Append(e.Target.EntityContainer.Name);
				treeNode.Text.Append(".");
				treeNode.Text.Append(e.Target.Name);
				return treeNode;
			}

			// Token: 0x06006C20 RID: 27680 RVA: 0x00171984 File Offset: 0x0016FB84
			public override TreeNode Visit(DbFilterExpression e)
			{
				Check.NotNull<DbFilterExpression>(e, "e");
				TreeNode treeNode = ExpressionPrinter.PrinterVisitor.NodeFromExpression(e);
				treeNode.Children.Add(this.VisitBinding("Input", e.Input));
				treeNode.Children.Add(this.Visit("Predicate", new DbExpression[] { e.Predicate }));
				return treeNode;
			}

			// Token: 0x06006C21 RID: 27681 RVA: 0x001719E8 File Offset: 0x0016FBE8
			public override TreeNode Visit(DbProjectExpression e)
			{
				Check.NotNull<DbProjectExpression>(e, "e");
				TreeNode treeNode = ExpressionPrinter.PrinterVisitor.NodeFromExpression(e);
				treeNode.Children.Add(this.VisitBinding("Input", e.Input));
				treeNode.Children.Add(this.Visit("Projection", new DbExpression[] { e.Projection }));
				return treeNode;
			}

			// Token: 0x06006C22 RID: 27682 RVA: 0x00171A4A File Offset: 0x0016FC4A
			public override TreeNode Visit(DbCrossJoinExpression e)
			{
				Check.NotNull<DbCrossJoinExpression>(e, "e");
				TreeNode treeNode = ExpressionPrinter.PrinterVisitor.NodeFromExpression(e);
				treeNode.Children.Add(this.VisitBindingList("Inputs", e.Inputs));
				return treeNode;
			}

			// Token: 0x06006C23 RID: 27683 RVA: 0x00171A7C File Offset: 0x0016FC7C
			public override TreeNode Visit(DbJoinExpression e)
			{
				Check.NotNull<DbJoinExpression>(e, "e");
				TreeNode treeNode = ExpressionPrinter.PrinterVisitor.NodeFromExpression(e);
				treeNode.Children.Add(this.VisitBinding("Left", e.Left));
				treeNode.Children.Add(this.VisitBinding("Right", e.Right));
				treeNode.Children.Add(this.Visit("JoinCondition", new DbExpression[] { e.JoinCondition }));
				return treeNode;
			}

			// Token: 0x06006C24 RID: 27684 RVA: 0x00171AFC File Offset: 0x0016FCFC
			public override TreeNode Visit(DbApplyExpression e)
			{
				Check.NotNull<DbApplyExpression>(e, "e");
				TreeNode treeNode = ExpressionPrinter.PrinterVisitor.NodeFromExpression(e);
				treeNode.Children.Add(this.VisitBinding("Input", e.Input));
				treeNode.Children.Add(this.VisitBinding("Apply", e.Apply));
				return treeNode;
			}

			// Token: 0x06006C25 RID: 27685 RVA: 0x00171B54 File Offset: 0x0016FD54
			public override TreeNode Visit(DbGroupByExpression e)
			{
				Check.NotNull<DbGroupByExpression>(e, "e");
				List<TreeNode> list = new List<TreeNode>();
				List<TreeNode> list2 = new List<TreeNode>();
				RowType edmType = TypeHelpers.GetEdmType<RowType>(TypeHelpers.GetEdmType<CollectionType>(e.ResultType).TypeUsage);
				int num = 0;
				for (int i = 0; i < e.Keys.Count; i++)
				{
					list.Add(this.VisitWithLabel("Key", edmType.Properties[i].Name, e.Keys[num]));
					num++;
				}
				int num2 = 0;
				for (int j = e.Keys.Count; j < edmType.Properties.Count; j++)
				{
					TreeNode treeNode = new TreeNode("Aggregate : '", new TreeNode[0]);
					treeNode.Text.Append(edmType.Properties[j].Name);
					treeNode.Text.Append("'");
					DbFunctionAggregate dbFunctionAggregate = e.Aggregates[num2] as DbFunctionAggregate;
					if (dbFunctionAggregate != null)
					{
						TreeNode treeNode2 = this.VisitFunction(dbFunctionAggregate.Function, dbFunctionAggregate.Arguments);
						if (dbFunctionAggregate.Distinct)
						{
							treeNode2 = new TreeNode("Distinct", new TreeNode[] { treeNode2 });
						}
						treeNode.Children.Add(treeNode2);
					}
					else
					{
						DbGroupAggregate dbGroupAggregate = e.Aggregates[num2] as DbGroupAggregate;
						treeNode.Children.Add(this.Visit("GroupAggregate", new DbExpression[] { dbGroupAggregate.Arguments[0] }));
					}
					list2.Add(treeNode);
					num2++;
				}
				TreeNode treeNode3 = ExpressionPrinter.PrinterVisitor.NodeFromExpression(e);
				treeNode3.Children.Add(this.VisitGroupBinding(e.Input));
				if (list.Count > 0)
				{
					treeNode3.Children.Add(new TreeNode("Keys", list));
				}
				if (list2.Count > 0)
				{
					treeNode3.Children.Add(new TreeNode("Aggregates", list2));
				}
				return treeNode3;
			}

			// Token: 0x06006C26 RID: 27686 RVA: 0x00171D60 File Offset: 0x0016FF60
			private TreeNode VisitSortOrder(IList<DbSortClause> sortOrder)
			{
				TreeNode treeNode = new TreeNode("SortOrder", new TreeNode[0]);
				foreach (DbSortClause dbSortClause in sortOrder)
				{
					TreeNode treeNode2 = this.Visit(dbSortClause.Ascending ? "Asc" : "Desc", new DbExpression[] { dbSortClause.Expression });
					if (!string.IsNullOrEmpty(dbSortClause.Collation))
					{
						treeNode2.Text.Append(" : ");
						treeNode2.Text.Append(dbSortClause.Collation);
					}
					treeNode.Children.Add(treeNode2);
				}
				return treeNode;
			}

			// Token: 0x06006C27 RID: 27687 RVA: 0x00171E1C File Offset: 0x0017001C
			public override TreeNode Visit(DbSkipExpression e)
			{
				Check.NotNull<DbSkipExpression>(e, "e");
				TreeNode treeNode = ExpressionPrinter.PrinterVisitor.NodeFromExpression(e);
				treeNode.Children.Add(this.VisitBinding("Input", e.Input));
				treeNode.Children.Add(this.VisitSortOrder(e.SortOrder));
				treeNode.Children.Add(this.Visit("Count", new DbExpression[] { e.Count }));
				return treeNode;
			}

			// Token: 0x06006C28 RID: 27688 RVA: 0x00171E98 File Offset: 0x00170098
			public override TreeNode Visit(DbSortExpression e)
			{
				Check.NotNull<DbSortExpression>(e, "e");
				TreeNode treeNode = ExpressionPrinter.PrinterVisitor.NodeFromExpression(e);
				treeNode.Children.Add(this.VisitBinding("Input", e.Input));
				treeNode.Children.Add(this.VisitSortOrder(e.SortOrder));
				return treeNode;
			}

			// Token: 0x06006C29 RID: 27689 RVA: 0x00171EEC File Offset: 0x001700EC
			public override TreeNode Visit(DbQuantifierExpression e)
			{
				Check.NotNull<DbQuantifierExpression>(e, "e");
				TreeNode treeNode = ExpressionPrinter.PrinterVisitor.NodeFromExpression(e);
				treeNode.Children.Add(this.VisitBinding("Input", e.Input));
				treeNode.Children.Add(this.Visit("Predicate", new DbExpression[] { e.Predicate }));
				return treeNode;
			}

			// Token: 0x040031B3 RID: 12723
			private static readonly Dictionary<DbExpressionKind, string> _opMap = ExpressionPrinter.PrinterVisitor.InitializeOpMap();

			// Token: 0x040031B4 RID: 12724
			private int _maxStringLength = 80;

			// Token: 0x040031B5 RID: 12725
			private bool _infix = true;
		}
	}
}
