using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Ast;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Ast;
using Microsoft.Mashup.Engine1.Library.Table;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Language.Query
{
	// Token: 0x020017BD RID: 6077
	internal static class QueryHelpers
	{
		// Token: 0x060099B3 RID: 39347 RVA: 0x001FCA62 File Offset: 0x001FAC62
		private static IExpression As(IExpression expression, TypeValue type)
		{
			return new AsBinaryExpressionSyntaxNode(expression, new ConstantExpressionSyntaxNode(type));
		}

		// Token: 0x060099B4 RID: 39348 RVA: 0x001FCA70 File Offset: 0x001FAC70
		public static IFunctionTypeExpression ToFunctionTypeExpression(this FunctionTypeValue functionType)
		{
			IParameter[] array = new IParameter[functionType.ParameterCount];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new ParameterSyntaxNode(Identifier.New(functionType.ParameterName(i)), new ConstantExpressionSyntaxNode(functionType.ParameterType(i)));
			}
			return new FunctionTypeSyntaxNode(new ConstantExpressionSyntaxNode(functionType.ReturnType), array, array.Length);
		}

		// Token: 0x060099B5 RID: 39349 RVA: 0x001FCACC File Offset: 0x001FACCC
		public static IFunctionTypeExpression CreateDefaultFunctionType(params Identifier[] identifiers)
		{
			IParameter[] array = new IParameter[identifiers.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new ParameterSyntaxNode(identifiers[i], null);
			}
			return new FunctionTypeSyntaxNode(null, array, array.Length);
		}

		// Token: 0x060099B6 RID: 39350 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
		public static IExpression Simplify(this IExpression expression)
		{
			return expression;
		}

		// Token: 0x060099B7 RID: 39351 RVA: 0x001FCB08 File Offset: 0x001FAD08
		public static bool TryGetConstant(this IExpression node, out Value constant)
		{
			IConstantExpression constantExpression = node as IConstantExpression;
			if (constantExpression != null)
			{
				constant = constantExpression.Value;
				return true;
			}
			constant = Value.Null;
			return false;
		}

		// Token: 0x060099B8 RID: 39352 RVA: 0x001FCB34 File Offset: 0x001FAD34
		public static bool TryGetStringConstant(this IExpression node, out string s)
		{
			Value value;
			if (node.TryGetConstant(out value) && value.IsText)
			{
				s = value.AsString;
				return true;
			}
			s = null;
			return false;
		}

		// Token: 0x060099B9 RID: 39353 RVA: 0x001FCB64 File Offset: 0x001FAD64
		public static bool TryGetStringList(this IExpression node, int maxCount, out IList<string> list)
		{
			ExpressionKind kind = node.Kind;
			if (kind != ExpressionKind.Constant)
			{
				if (kind == ExpressionKind.List)
				{
					IList<IExpression> members = ((IListExpression)node).Members;
					if (members.Count <= maxCount)
					{
						string[] array = new string[members.Count];
						for (int i = 0; i < members.Count; i++)
						{
							if (!members[i].TryGetStringConstant(out array[i]))
							{
								list = null;
								return false;
							}
						}
						list = array;
						return true;
					}
				}
			}
			else
			{
				Value value = ((IConstantExpression)node).Value;
				if (value.IsList && value.AsList.TryGetStringList(maxCount, out list))
				{
					return true;
				}
			}
			list = null;
			return false;
		}

		// Token: 0x060099BA RID: 39354 RVA: 0x001FCC01 File Offset: 0x001FAE01
		public static bool TryGetIdentifier(this IExpression node, out IIdentifierExpression identifier)
		{
			identifier = node as IIdentifierExpression;
			return identifier != null;
		}

		// Token: 0x060099BB RID: 39355 RVA: 0x001FCC10 File Offset: 0x001FAE10
		public static bool TryGetIdentifier(this IExpression node, out Identifier identifier)
		{
			IIdentifierExpression identifierExpression;
			if (node.TryGetIdentifier(out identifierExpression))
			{
				identifier = identifierExpression.Name;
				return true;
			}
			identifier = null;
			return false;
		}

		// Token: 0x060099BC RID: 39356 RVA: 0x001FCC38 File Offset: 0x001FAE38
		public static bool TryGetFieldAccess(this IExpression candidate, out IExpression expression, out Identifier field)
		{
			IFieldAccessExpression fieldAccessExpression = candidate as IFieldAccessExpression;
			if (fieldAccessExpression != null)
			{
				expression = fieldAccessExpression.Expression;
				field = fieldAccessExpression.MemberName;
				return true;
			}
			expression = null;
			field = null;
			return false;
		}

		// Token: 0x060099BD RID: 39357 RVA: 0x001FCC68 File Offset: 0x001FAE68
		public static bool TryGetInvocation(this IExpression expression, FunctionValue function, int argumentCount, out IList<IExpression> arguments)
		{
			IInvocationExpression invocationExpression = expression as IInvocationExpression;
			Value value;
			if (invocationExpression != null && invocationExpression.Function.TryGetConstant(out value) && value.Equals(function) && invocationExpression.Arguments.Count == argumentCount)
			{
				arguments = invocationExpression.Arguments;
				return true;
			}
			arguments = null;
			return false;
		}

		// Token: 0x060099BE RID: 39358 RVA: 0x001FCCB4 File Offset: 0x001FAEB4
		public static bool TryGetIsNullOrIsNotNullFilter(this IFunctionExpression filter, out bool isIsNull)
		{
			IBinaryExpression binaryExpression = filter.Expression as IBinaryExpression;
			Identifier identifier;
			Value value;
			if (((binaryExpression.Left.TryGetIdentifier(out identifier) && binaryExpression.Right.TryGetConstant(out value)) || (binaryExpression.Right.TryGetIdentifier(out identifier) && binaryExpression.Left.TryGetConstant(out value))) && filter.FunctionType.Parameters.Count == 1 && identifier.Equals(filter.FunctionType.Parameters[0].Identifier) && value.IsNull)
			{
				BinaryOperator2 @operator = binaryExpression.Operator;
				if (@operator == BinaryOperator2.Equals)
				{
					isIsNull = true;
					return true;
				}
				if (@operator == BinaryOperator2.NotEquals)
				{
					isIsNull = false;
					return true;
				}
			}
			isIsNull = false;
			return false;
		}

		// Token: 0x060099BF RID: 39359 RVA: 0x001FCD64 File Offset: 0x001FAF64
		public static IFunctionExpression CreateLookupSelectorExpression(RecordValue index)
		{
			IExpression expression = ConstantExpressionSyntaxNode.True;
			IExpression expression2 = null;
			IExpression expression3 = null;
			for (int i = 0; i < index.Keys.Length; i++)
			{
				Identifier identifier = Identifier.New(index.Keys[i]);
				if (expression2 == null)
				{
					expression2 = new InclusiveIdentifierExpressionSyntaxNode(Identifier.Underscore);
					expression3 = new ConstantExpressionSyntaxNode(index);
				}
				IExpression expression4 = new EqualsBinaryExpressionSyntaxNode(new RequiredFieldAccessExpressionSyntaxNode(expression2, identifier), new RequiredFieldAccessExpressionSyntaxNode(expression3, identifier));
				if (expression == ConstantExpressionSyntaxNode.True)
				{
					expression = expression4;
				}
				else
				{
					expression = new AndBinaryExpressionSyntaxNode(expression, expression4);
				}
			}
			return new FunctionExpressionSyntaxNode(new FunctionTypeSyntaxNode(null, new IParameter[]
			{
				new ParameterSyntaxNode(Identifier.Underscore, null)
			}, 1), expression);
		}

		// Token: 0x060099C0 RID: 39360 RVA: 0x001FCE08 File Offset: 0x001FB008
		public static bool TryGetFirstItemAccess(this IExpression expression, out IExpression collection)
		{
			IElementAccessExpression elementAccessExpression = expression as IElementAccessExpression;
			Value value;
			if (elementAccessExpression != null && !elementAccessExpression.IsOptional && elementAccessExpression.Key.TryGetConstant(out value) && value.Equals(NumberValue.Zero))
			{
				collection = elementAccessExpression.Collection;
				return true;
			}
			IList<IExpression> list;
			if (expression.TryGetInvocation(TableModule.Table.First, 1, out list))
			{
				collection = list[0];
				return true;
			}
			if (expression.TryGetInvocation(Library.List.First, 1, out list))
			{
				collection = list[0];
				return true;
			}
			collection = null;
			return false;
		}

		// Token: 0x060099C1 RID: 39361 RVA: 0x001FCE88 File Offset: 0x001FB088
		public static bool TryGetConstant(this IDictionary<string, IExpression> captures, string name, out Value constant)
		{
			constant = null;
			IExpression expression;
			return captures.TryGetValue(name, out expression) && expression.TryGetConstant(out constant);
		}

		// Token: 0x060099C2 RID: 39362 RVA: 0x001FCEAC File Offset: 0x001FB0AC
		public static bool TryGetStringConstant(this IDictionary<string, IExpression> captures, string name, out string s)
		{
			s = null;
			IExpression expression;
			return captures.TryGetValue(name, out expression) && expression.TryGetStringConstant(out s);
		}

		// Token: 0x060099C3 RID: 39363 RVA: 0x001FCED0 File Offset: 0x001FB0D0
		public static bool TryGetIdentifier(this Dictionary<string, IExpression> captures, string key, out Identifier identifier)
		{
			IExpression expression;
			if (captures.TryGetValue(key, out expression) && expression.TryGetIdentifier(out identifier))
			{
				return true;
			}
			identifier = null;
			return false;
		}

		// Token: 0x04005155 RID: 20821
		public static readonly IFunctionTypeExpression EachFunctionType = new FunctionTypeSyntaxNode(null, new IParameter[]
		{
			new ParameterSyntaxNode(Identifier.Underscore, null)
		}, 1);
	}
}
