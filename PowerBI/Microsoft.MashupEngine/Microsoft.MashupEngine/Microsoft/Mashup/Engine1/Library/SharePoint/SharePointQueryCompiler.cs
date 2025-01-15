using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.SharePoint
{
	// Token: 0x02000404 RID: 1028
	internal sealed class SharePointQueryCompiler
	{
		// Token: 0x060022FB RID: 8955 RVA: 0x00061A58 File Offset: 0x0005FC58
		public SharePointQueryCompiler(Keys columns, string folderPathBase)
		{
			this.columns = columns;
			this.folderPathBase = folderPathBase;
		}

		// Token: 0x060022FC RID: 8956 RVA: 0x00061A70 File Offset: 0x0005FC70
		public string CompileEntityQueryExpression(QueryExpression expression)
		{
			switch (expression.Kind)
			{
			case QueryExpressionKind.Binary:
				return this.CompileEntityBinaryExpression((BinaryQueryExpression)expression);
			case QueryExpressionKind.Constant:
				return this.CompileEntityConstantExpression((ConstantQueryExpression)expression);
			case QueryExpressionKind.ColumnAccess:
				return this.CompileEntityColumnAccessExpression((ColumnAccessQueryExpression)expression);
			default:
				throw new NotSupportedException("Entity query expression kind: " + expression.Kind.ToString());
			}
		}

		// Token: 0x060022FD RID: 8957 RVA: 0x00061AE4 File Offset: 0x0005FCE4
		private string CompileEntityBinaryExpression(BinaryQueryExpression expression)
		{
			string text = this.CompileEntityQueryExpression(expression.Left);
			string text2 = this.CompileEntityQueryExpression(expression.Right);
			if (expression.Operator != BinaryOperator2.Equals)
			{
				throw new NotSupportedException("Entity binary query expression operator: " + expression.Operator.ToString());
			}
			if (!(text == "Name"))
			{
				return text;
			}
			return text2;
		}

		// Token: 0x060022FE RID: 8958 RVA: 0x00061B48 File Offset: 0x0005FD48
		private string CompileEntityColumnAccessExpression(ColumnAccessQueryExpression expression)
		{
			string text = this.columns[expression.Column];
			if (text == "Name")
			{
				return "Name";
			}
			throw new NotSupportedException("Entity column access query expression column: " + text);
		}

		// Token: 0x060022FF RID: 8959 RVA: 0x00061B8C File Offset: 0x0005FD8C
		private string CompileEntityConstantExpression(ConstantQueryExpression expression)
		{
			Value value = expression.Value;
			if (value.Kind == ValueKind.Text)
			{
				return value.AsString;
			}
			throw new NotSupportedException("Entity constant query expression value kind: " + value.Kind.ToString());
		}

		// Token: 0x06002300 RID: 8960 RVA: 0x00061BD4 File Offset: 0x0005FDD4
		public string CompileOrderByList(QueryExpression[] expressions, bool[] ascendings)
		{
			string[] array = new string[expressions.Length];
			for (int i = 0; i < expressions.Length; i++)
			{
				string text = this.CompileQueryExpression(expressions[i]);
				if (text == "Extension")
				{
					throw new NotSupportedException("Order by list column: " + text);
				}
				array[i] = SharePointQueryCompiler.WriteOrderByItem(text, ascendings[i]);
			}
			return SharePointQueryCompiler.WriteCommaSeparatedList(array);
		}

		// Token: 0x06002301 RID: 8961 RVA: 0x00061C34 File Offset: 0x0005FE34
		public string CompileQueryExpression(QueryExpression expression)
		{
			switch (expression.Kind)
			{
			case QueryExpressionKind.Binary:
				return this.CompileBinaryExpression((BinaryQueryExpression)expression);
			case QueryExpressionKind.Constant:
				return this.CompileConstantExpression((ConstantQueryExpression)expression);
			case QueryExpressionKind.ColumnAccess:
				return this.CompileColumnAccessExpression((ColumnAccessQueryExpression)expression);
			case QueryExpressionKind.Invocation:
				return this.CompileInvocationExpression((InvocationQueryExpression)expression);
			case QueryExpressionKind.Unary:
				return this.CompileUnaryExpression((UnaryQueryExpression)expression);
			}
			throw new NotSupportedException("Query expression kind: " + expression.Kind.ToString());
		}

		// Token: 0x06002302 RID: 8962 RVA: 0x00061CCC File Offset: 0x0005FECC
		private string CompileBinaryExpression(BinaryQueryExpression expression)
		{
			string text = this.CompileQueryExpression(expression.Left);
			string text2 = this.CompileQueryExpression(expression.Right);
			if (SharePointQueryCompiler.IsExtensionColumn(text) || SharePointQueryCompiler.IsKindColumn(text) || SharePointQueryCompiler.IsExtensionColumn(text2) || SharePointQueryCompiler.IsKindColumn(text2))
			{
				if (SharePointQueryCompiler.IsExtensionColumn(text2) || SharePointQueryCompiler.IsKindColumn(text2))
				{
					string text3 = text;
					text = text2;
					text2 = text3;
				}
				if (expression.Operator != BinaryOperator2.Equals && expression.Operator != BinaryOperator2.NotEquals)
				{
					throw new NotSupportedException("Column access query expression column: " + text);
				}
				BinaryOperator2 binaryOperator = expression.Operator;
				List<string> list = new List<string>(2);
				string text4;
				if (SharePointQueryCompiler.IsExtensionColumn(text))
				{
					if (text2 == "''")
					{
						binaryOperator = ((binaryOperator == BinaryOperator2.Equals) ? BinaryOperator2.NotEquals : BinaryOperator2.Equals);
						text4 = "substringof";
						list.Add("'.'");
						list.Add("Name");
					}
					else
					{
						text4 = "endswith";
						list.Add("Name");
						list.Add(text2);
					}
				}
				else
				{
					text4 = "startswith";
					list.Add("ContentTypeID");
					string text5 = SharePointQueryCompiler.UnquotedText(text2);
					if (!(text5 == "Folder"))
					{
						if (!(text5 == "Table"))
						{
							throw new NotSupportedException("Column access query expression column: " + text + ", value = " + text2);
						}
						list.Add(SharePointQueryCompiler.QuotedText("0x0130"));
					}
					else
					{
						list.Add(SharePointQueryCompiler.QuotedText("0x0120"));
					}
				}
				string text6 = SharePointQueryCompiler.WriteInvocation(text4, list);
				if (binaryOperator == BinaryOperator2.NotEquals)
				{
					text6 = SharePointQueryCompiler.WriteUnary("not", text6);
				}
				return text6;
			}
			else
			{
				string text7;
				switch (expression.Operator)
				{
				case BinaryOperator2.GreaterThan:
					text7 = "gt";
					break;
				case BinaryOperator2.LessThan:
					text7 = "lt";
					break;
				case BinaryOperator2.GreaterThanOrEquals:
					text7 = "ge";
					break;
				case BinaryOperator2.LessThanOrEquals:
					text7 = "le";
					break;
				case BinaryOperator2.Equals:
					text7 = "eq";
					break;
				case BinaryOperator2.NotEquals:
					text7 = "ne";
					break;
				case BinaryOperator2.And:
					text7 = "and";
					break;
				case BinaryOperator2.Or:
					text7 = "or";
					break;
				default:
					throw new NotSupportedException("Binary query expression operator: " + expression.Operator.ToString());
				}
				bool flag = false;
				if (SharePointQueryCompiler.IsFolderPathColumn(text))
				{
					text2 = this.RemoveFolderPathBase(SharePointQueryCompiler.RemoveTrailingSlash(text2));
					flag = true;
				}
				else if (SharePointQueryCompiler.IsFolderPathColumn(text2))
				{
					text = this.RemoveFolderPathBase(SharePointQueryCompiler.RemoveTrailingSlash(text));
					flag = true;
				}
				if (flag && expression.Operator != BinaryOperator2.Equals && expression.Operator != BinaryOperator2.NotEquals)
				{
					throw new NotSupportedException("Binary query expression operator: " + expression.Operator.ToString() + " with folder path column");
				}
				return SharePointQueryCompiler.WriteBinary(text7, text, text2);
			}
		}

		// Token: 0x06002303 RID: 8963 RVA: 0x00061F6C File Offset: 0x0006016C
		private string RemoveFolderPathBase(string quotedPath)
		{
			if (quotedPath.StartsWith("'" + this.folderPathBase, StringComparison.Ordinal))
			{
				string text = quotedPath.Substring(this.folderPathBase.Length + 1, quotedPath.Length - this.folderPathBase.Length - 1);
				return "'" + text;
			}
			return quotedPath;
		}

		// Token: 0x06002304 RID: 8964 RVA: 0x00061FC8 File Offset: 0x000601C8
		private static string RemoveIntersectingFolderPathBase(string pathBase, string path)
		{
			path = SharePointQueryCompiler.UnquotedText(path);
			for (int i = 0; i < pathBase.Length; i++)
			{
				int num = 0;
				bool flag = true;
				int num2 = i;
				while (num2 < pathBase.Length && num < path.Length)
				{
					if (path[num] != pathBase[num2])
					{
						flag = false;
						break;
					}
					num2++;
					num++;
				}
				if (flag)
				{
					path = path.Substring(num, path.Length - num);
					break;
				}
			}
			return SharePointQueryCompiler.QuotedText(path);
		}

		// Token: 0x06002305 RID: 8965 RVA: 0x00062040 File Offset: 0x00060240
		private static string RemoveTrailingSlash(string quotedPath)
		{
			if (quotedPath.EndsWith("/'", StringComparison.Ordinal) && !quotedPath.EndsWith("//'", StringComparison.Ordinal))
			{
				return quotedPath.Substring(0, quotedPath.Length - 2) + "'";
			}
			return quotedPath;
		}

		// Token: 0x06002306 RID: 8966 RVA: 0x0006207C File Offset: 0x0006027C
		private string CompileColumnAccessExpression(ColumnAccessQueryExpression expression)
		{
			string text = this.columns[expression.Column];
			if (!(text == "Extension") && !(text == "Kind"))
			{
				if (!(text == "Name"))
				{
					if (!(text == "Folder Path"))
					{
						throw new NotSupportedException("Column access query expression column: " + text);
					}
					text = "Path";
				}
				else
				{
					text = "Name";
				}
			}
			return text;
		}

		// Token: 0x06002307 RID: 8967 RVA: 0x000620F4 File Offset: 0x000602F4
		private string CompileConstantExpression(ConstantQueryExpression expression)
		{
			Value value = expression.Value;
			string text;
			switch (value.Kind)
			{
			case ValueKind.Number:
				text = value.ToString();
				break;
			case ValueKind.Logical:
				text = (value.AsLogical.Boolean ? "true" : "false");
				break;
			case ValueKind.Text:
				text = SharePointQueryCompiler.QuotedText(value.AsString);
				break;
			default:
				throw new NotSupportedException("Constant query expression value kind: " + value.Kind.ToString());
			}
			return text;
		}

		// Token: 0x06002308 RID: 8968 RVA: 0x00062180 File Offset: 0x00060380
		private string CompileInvocationExpression(InvocationQueryExpression expression)
		{
			ConstantQueryExpression constantQueryExpression = expression.Function as ConstantQueryExpression;
			if (constantQueryExpression == null)
			{
				throw new NotSupportedException("Expected constant query expression, actual kind: " + expression.Function.Kind.ToString());
			}
			FunctionValue asFunction = constantQueryExpression.Value.AsFunction;
			string odataFunctionName = SharePointQueryCompiler.GetODataFunctionName(asFunction);
			List<string> list = new List<string>(expression.Arguments.Count);
			foreach (QueryExpression queryExpression in expression.Arguments)
			{
				list.Add(this.CompileQueryExpression(queryExpression));
			}
			if (SharePointQueryCompiler.IsExtensionColumn(list[0]) || SharePointQueryCompiler.IsKindColumn(list[0]))
			{
				throw new NotSupportedException("Column access query expression column: " + list[0]);
			}
			if (asFunction.Equals(Library.Text.Contains))
			{
				string text = list[0];
				list[0] = list[1];
				list[1] = text;
			}
			if (SharePointQueryCompiler.IsFolderPathColumn(list[0]))
			{
				if (asFunction.Equals(Library.Text.StartsWith))
				{
					if (this.folderPathBase.StartsWith(SharePointQueryCompiler.UnquotedText(list[1]), StringComparison.Ordinal))
					{
						return "true";
					}
					list[1] = this.RemoveFolderPathBase(list[1]);
					string text2 = SharePointQueryCompiler.WriteInvocation(odataFunctionName, list);
					string text3 = SharePointQueryCompiler.RemoveTrailingSlash(list[1]);
					if (text3 == list[1])
					{
						return text2;
					}
					return text2 + " or " + SharePointQueryBuilder.CreateEqualsWorkaround(text3);
				}
				else
				{
					if (!asFunction.Equals(Library.Text.EndsWith))
					{
						throw new NotSupportedException("Operation over [Folder Path] with function: " + odataFunctionName);
					}
					list[1] = SharePointQueryCompiler.RemoveTrailingSlash(list[1]);
					string text4 = SharePointQueryCompiler.WriteInvocation(odataFunctionName, list);
					string text5 = SharePointQueryCompiler.RemoveIntersectingFolderPathBase(this.folderPathBase, list[1]);
					if (text5 == list[1])
					{
						return text4;
					}
					return string.Concat(new string[]
					{
						text4,
						" or (",
						list[0],
						" eq ",
						text5,
						")"
					});
				}
			}
			else
			{
				if (!SharePointQueryCompiler.IsFolderPathColumn(list[1]))
				{
					return SharePointQueryCompiler.WriteInvocation(odataFunctionName, list);
				}
				if (asFunction.Equals(Library.Text.Contains))
				{
					string text6 = list[0];
					string text7 = SharePointQueryCompiler.RemoveIntersectingFolderPathBase(this.folderPathBase, text6);
					string text8 = SharePointQueryCompiler.RemoveTrailingSlash(text7);
					string text9 = SharePointQueryCompiler.WriteInvocation(odataFunctionName, list);
					string text10 = SharePointQueryCompiler.WriteInvocation("endswith", new List<string>
					{
						"Path",
						SharePointQueryCompiler.RemoveTrailingSlash(text6)
					});
					string text11 = SharePointQueryCompiler.WriteInvocation("startswith", new List<string> { "Path", text7 });
					string text12 = SharePointQueryBuilder.CreateEqualsWorkaround(text8);
					return string.Concat(new string[] { text9, " or ", text11, " or ", text10, "or ", text12 });
				}
				throw new NotSupportedException("Operation over [Folder Path] with function: " + odataFunctionName);
			}
		}

		// Token: 0x06002309 RID: 8969 RVA: 0x000624AC File Offset: 0x000606AC
		private string CompileUnaryExpression(UnaryQueryExpression expression)
		{
			string text = this.CompileQueryExpression(expression.Expression);
			if (expression.Operator == UnaryOperator2.Not)
			{
				string text2 = "not";
				return SharePointQueryCompiler.WriteUnary(text2, text);
			}
			throw new NotSupportedException("Unary query expression operator: " + expression.Operator.ToString());
		}

		// Token: 0x0600230A RID: 8970 RVA: 0x00062504 File Offset: 0x00060704
		private static string GetODataFunctionName(FunctionValue function)
		{
			if (function.Equals(Library.Text.Contains))
			{
				return "substringof";
			}
			if (function.Equals(Library.Text.StartsWith))
			{
				return "startswith";
			}
			if (function.Equals(Library.Text.EndsWith))
			{
				return "endswith";
			}
			throw new NotSupportedException("Invocation query expression");
		}

		// Token: 0x0600230B RID: 8971 RVA: 0x00062554 File Offset: 0x00060754
		private static bool IsExtensionColumn(string column)
		{
			return column == "Extension";
		}

		// Token: 0x0600230C RID: 8972 RVA: 0x00062561 File Offset: 0x00060761
		private static bool IsKindColumn(string column)
		{
			return column == "Kind";
		}

		// Token: 0x0600230D RID: 8973 RVA: 0x0006256E File Offset: 0x0006076E
		private static bool IsFolderPathColumn(string column)
		{
			return column == "Path";
		}

		// Token: 0x0600230E RID: 8974 RVA: 0x0006257C File Offset: 0x0006077C
		private static bool NeedsParens(string operand)
		{
			string firstQuotedTextLiteral = SharePointQueryCompiler.GetFirstQuotedTextLiteral(operand);
			return (firstQuotedTextLiteral == null || firstQuotedTextLiteral.Length != operand.Length) && operand.IndexOf(' ') >= 0;
		}

		// Token: 0x0600230F RID: 8975 RVA: 0x000625AF File Offset: 0x000607AF
		public static string QuotedText(string text)
		{
			if (text != null)
			{
				return "'" + text.Replace("'", "''") + "'";
			}
			return null;
		}

		// Token: 0x06002310 RID: 8976 RVA: 0x000625D5 File Offset: 0x000607D5
		public static string UnquotedText(string text)
		{
			if (text != null && text.Length >= 2)
			{
				return text.Substring(1, text.Length - 2).Replace("''", "'");
			}
			return null;
		}

		// Token: 0x06002311 RID: 8977 RVA: 0x00062604 File Offset: 0x00060804
		public static string GetFirstQuotedTextLiteral(string text)
		{
			if (text != null)
			{
				int num = text.IndexOf('\'');
				if (num >= 0)
				{
					int num2 = num;
					int num3 = num + 1;
					bool flag = false;
					while (!flag && num3 < text.Length && (num = text.IndexOf('\'', num3)) >= 0)
					{
						num3 = num + 1;
						flag = true;
						while (num3 < text.Length && text.IndexOf('\'', num3) == num3)
						{
							num3++;
							flag = !flag;
						}
					}
					if (flag)
					{
						return text.Substring(num2, num3 - num2);
					}
				}
			}
			return null;
		}

		// Token: 0x06002312 RID: 8978 RVA: 0x0006267C File Offset: 0x0006087C
		public static string WriteBinary(string op, string leftOperand, string rightOperand)
		{
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = SharePointQueryCompiler.NeedsParens(leftOperand);
			if (flag)
			{
				stringBuilder.Append('(');
			}
			stringBuilder.Append(leftOperand);
			if (flag)
			{
				stringBuilder.Append(')');
			}
			stringBuilder.Append(' ');
			stringBuilder.Append(op);
			stringBuilder.Append(' ');
			bool flag2 = SharePointQueryCompiler.NeedsParens(rightOperand);
			if (flag2)
			{
				stringBuilder.Append('(');
			}
			stringBuilder.Append(rightOperand);
			if (flag2)
			{
				stringBuilder.Append(')');
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002313 RID: 8979 RVA: 0x000626FC File Offset: 0x000608FC
		private static string WriteCommaSeparatedList(string[] items)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < items.Length; i++)
			{
				if (i > 0)
				{
					stringBuilder.Append(',');
				}
				stringBuilder.Append(items[i]);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002314 RID: 8980 RVA: 0x0006273C File Offset: 0x0006093C
		private static string WriteInvocation(string functionName, List<string> args)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(functionName);
			stringBuilder.Append('(');
			bool flag = false;
			foreach (string text in args)
			{
				if (flag)
				{
					stringBuilder.Append(',');
				}
				else
				{
					flag = true;
				}
				stringBuilder.Append(text);
			}
			stringBuilder.Append(')');
			return stringBuilder.ToString();
		}

		// Token: 0x06002315 RID: 8981 RVA: 0x000627C4 File Offset: 0x000609C4
		private static string WriteOrderByItem(string column, bool ascending)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(column);
			stringBuilder.Append(' ');
			stringBuilder.Append(ascending ? "asc" : "desc");
			return stringBuilder.ToString();
		}

		// Token: 0x06002316 RID: 8982 RVA: 0x000627F8 File Offset: 0x000609F8
		private static string WriteUnary(string op, string operand)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(op);
			stringBuilder.Append(' ');
			bool flag = SharePointQueryCompiler.NeedsParens(operand);
			if (flag)
			{
				stringBuilder.Append('(');
			}
			stringBuilder.Append(operand);
			if (flag)
			{
				stringBuilder.Append(')');
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000DF6 RID: 3574
		private const string AndText = "and";

		// Token: 0x04000DF7 RID: 3575
		private const string AscendingText = "asc";

		// Token: 0x04000DF8 RID: 3576
		private const string DateTimeText = "datetime";

		// Token: 0x04000DF9 RID: 3577
		private const string DesendingText = "desc";

		// Token: 0x04000DFA RID: 3578
		private const string DotText = "'.'";

		// Token: 0x04000DFB RID: 3579
		private const string EmptyText = "''";

		// Token: 0x04000DFC RID: 3580
		private const string EndsWithText = "endswith";

		// Token: 0x04000DFD RID: 3581
		private const string EqualsText = "eq";

		// Token: 0x04000DFE RID: 3582
		private const string FalseText = "false";

		// Token: 0x04000DFF RID: 3583
		private const string GreaterThanOrEqualsText = "ge";

		// Token: 0x04000E00 RID: 3584
		private const string GreaterThanText = "gt";

		// Token: 0x04000E01 RID: 3585
		private const string LessThanOrEqualsText = "le";

		// Token: 0x04000E02 RID: 3586
		private const string LessThanText = "lt";

		// Token: 0x04000E03 RID: 3587
		private const string NotEqualsText = "ne";

		// Token: 0x04000E04 RID: 3588
		private const string NotText = "not";

		// Token: 0x04000E05 RID: 3589
		private const string OrText = "or";

		// Token: 0x04000E06 RID: 3590
		private const string QuoteText = "'";

		// Token: 0x04000E07 RID: 3591
		private const string QuoteQuoteText = "''";

		// Token: 0x04000E08 RID: 3592
		private const string TrueText = "true";

		// Token: 0x04000E09 RID: 3593
		private const string StartsWithText = "startswith";

		// Token: 0x04000E0A RID: 3594
		private const string SubstringOfText = "substringof";

		// Token: 0x04000E0B RID: 3595
		private const char BlankChar = ' ';

		// Token: 0x04000E0C RID: 3596
		private const char CommaChar = ',';

		// Token: 0x04000E0D RID: 3597
		private const char LeftParenChar = '(';

		// Token: 0x04000E0E RID: 3598
		private const char QuoteChar = '\'';

		// Token: 0x04000E0F RID: 3599
		private const char RightParenChar = ')';

		// Token: 0x04000E10 RID: 3600
		private readonly Keys columns;

		// Token: 0x04000E11 RID: 3601
		private readonly string folderPathBase;
	}
}
