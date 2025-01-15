using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Microsoft.Mashup.Engine.Ast;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Runtime.Typeflow;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x0200130B RID: 4875
	internal static class FoldingWarnings
	{
		// Token: 0x060080D5 RID: 32981 RVA: 0x001B752B File Offset: 0x001B572B
		public static FoldingWarnings.FoldingWarning<string> SqlCapabilities(string property)
		{
			return new FoldingWarnings.FoldingWarning<string>("This driver doesn't set the {0} feature. You can override it by using SqlCapabilities.", property);
		}

		// Token: 0x060080D6 RID: 32982 RVA: 0x001B7538 File Offset: 0x001B5738
		public static FoldingWarnings.FoldingWarning<T1, T3, T2> InvalidSqlGetInfo<T1, T2, T3>(T1 expected, T2 actual, T3 fieldName)
		{
			return new FoldingWarnings.FoldingWarning<T1, T3, T2>("This driver is expected to have {0} for {1}, but it only has {2}. You can use SqlGetInfo to override this.", expected, fieldName, actual);
		}

		// Token: 0x060080D7 RID: 32983 RVA: 0x001B7547 File Offset: 0x001B5747
		public static FoldingWarnings.FoldingWarning<TableTypeAlgebra.JoinKind> NotSupportJoinByDriver(TableTypeAlgebra.JoinKind joinKind)
		{
			return new FoldingWarnings.FoldingWarning<TableTypeAlgebra.JoinKind>("This driver doesn't support {0} joins. You can override this by using SqlGetInfo for SQL_SQL92_RELATIONAL_JOIN_OPERATORS.", joinKind);
		}

		// Token: 0x060080D8 RID: 32984 RVA: 0x001B7554 File Offset: 0x001B5754
		public static FoldingWarnings.FoldingWarning<FoldingWarnings.StringFormatter<Keys>, FoldingWarnings.StringFormatter<FunctionValue>> AddColumnsKind(Keys columns, FunctionValue function)
		{
			return new FoldingWarnings.FoldingWarning<FoldingWarnings.StringFormatter<Keys>, FoldingWarnings.StringFormatter<FunctionValue>>("Folding failed on adding columns when applying function to columns {0}. Function {1}.", new FoldingWarnings.StringFormatter<Keys>(columns, new Func<Keys, string>(FoldingWarnings.KeysToString)), new FoldingWarnings.StringFormatter<FunctionValue>(function, new Func<FunctionValue, string>(FoldingWarnings.FunctionValueToString)));
		}

		// Token: 0x060080D9 RID: 32985 RVA: 0x001B7584 File Offset: 0x001B5784
		public static FoldingWarnings.FoldingWarning<int, string> ConstantRequired(int argNumber, string functionName)
		{
			return new FoldingWarnings.FoldingWarning<int, string>("Argument {0} to function {1} should be a constant.", argNumber, functionName);
		}

		// Token: 0x060080DA RID: 32986 RVA: 0x001B7592 File Offset: 0x001B5792
		public static FoldingWarnings.FoldingWarning<FoldingWarnings.StringFormatter<Keys>, FoldingWarnings.StringFormatter<TableDistinct>> DistinctColumns(TableDistinct distinctCriteria, Keys columns)
		{
			return new FoldingWarnings.FoldingWarning<FoldingWarnings.StringFormatter<Keys>, FoldingWarnings.StringFormatter<TableDistinct>>("Could not find distinct columns from columns {0} with functions {1}.", new FoldingWarnings.StringFormatter<Keys>(columns, new Func<Keys, string>(FoldingWarnings.KeysToString)), new FoldingWarnings.StringFormatter<TableDistinct>(distinctCriteria, new Func<TableDistinct, string>(FoldingWarnings.TableDistinctToString)));
		}

		// Token: 0x060080DB RID: 32987 RVA: 0x001B75C2 File Offset: 0x001B57C2
		public static FoldingWarnings.FoldingWarning<int, int> DistinctColumnsCount(int distinctColumnsLength, int selectItemsCount)
		{
			return new FoldingWarnings.FoldingWarning<int, int>("Distinct columns count {0} doesn't match select columns count {1}.", distinctColumnsLength, selectItemsCount);
		}

		// Token: 0x060080DC RID: 32988 RVA: 0x001B75D0 File Offset: 0x001B57D0
		public static FoldingWarnings.FoldingWarning<string> FunctionNotImplemented(string functionName)
		{
			return new FoldingWarnings.FoldingWarning<string>("This data source does not implement folding for the function {0}.", functionName);
		}

		// Token: 0x060080DD RID: 32989 RVA: 0x001B75DD File Offset: 0x001B57DD
		public static FoldingWarnings.FoldingWarning<string, string, string> InvalidFunctionArgument(string argumentName, string functionName, string content)
		{
			return new FoldingWarnings.FoldingWarning<string, string, string>("The value of the '{0}' argument to function {1} should be {2}.", argumentName, functionName, content);
		}

		// Token: 0x060080DE RID: 32990 RVA: 0x001B75EC File Offset: 0x001B57EC
		public static FoldingWarnings.FoldingWarning<string, string, string, string> MismatchedPivotColumnTypes(string field1, string type1, string field2, string type2)
		{
			return new FoldingWarnings.FoldingWarning<string, string, string, string>("The pivot column fields must have compatible types. Field {0} is of type {1}, but field {2} is of type {3}.", field1, type1, field2, type2);
		}

		// Token: 0x060080DF RID: 32991 RVA: 0x001B75FC File Offset: 0x001B57FC
		public static FoldingWarnings.FoldingWarning<TableTypeAlgebra.JoinKind> InnerJoinOnly(TableTypeAlgebra.JoinKind joinKind)
		{
			return new FoldingWarnings.FoldingWarning<TableTypeAlgebra.JoinKind>("Inner Join is required when left key column is empty. Consider changing {0} to JoinKind.InnerJoin.", joinKind);
		}

		// Token: 0x060080E0 RID: 32992 RVA: 0x001B7609 File Offset: 0x001B5809
		public static FoldingWarnings.FoldingWarning<string, int> InvalidArgumentsCount(string functionName, int expectedArgumentCount)
		{
			return new FoldingWarnings.FoldingWarning<string, int>("The argument count of function {0} should be {1}.", functionName, expectedArgumentCount);
		}

		// Token: 0x060080E1 RID: 32993 RVA: 0x001B7617 File Offset: 0x001B5817
		public static FoldingWarnings.FoldingWarning<string, int, int> InvalidArgumentsCount(string functionName, int minExpectedArgumentCount, int maxExpectedArgumentCount)
		{
			return new FoldingWarnings.FoldingWarning<string, int, int>("The argument count of function {0} should be between {1} and {2}.", functionName, minExpectedArgumentCount, maxExpectedArgumentCount);
		}

		// Token: 0x060080E2 RID: 32994 RVA: 0x001B7626 File Offset: 0x001B5826
		public static FoldingWarnings.FoldingWarning<FoldingWarnings.StringFormatter<InvocationQueryExpression>, int, int> InvalidArgumentsCount(InvocationQueryExpression expression, int minCount, int maxCount)
		{
			return new FoldingWarnings.FoldingWarning<FoldingWarnings.StringFormatter<InvocationQueryExpression>, int, int>("The argument count of function {0} should be between {1} and {2}.", new FoldingWarnings.StringFormatter<InvocationQueryExpression>(expression, new Func<InvocationQueryExpression, string>(FoldingWarnings.QueryExpressionToString)), minCount, maxCount);
		}

		// Token: 0x060080E3 RID: 32995 RVA: 0x001B7646 File Offset: 0x001B5846
		public static FoldingWarnings.FoldingWarning<FoldingWarnings.StringFormatter<InvocationQueryExpression>, int> InvalidArgumentsCount(InvocationQueryExpression expression, int requiredCount)
		{
			return new FoldingWarnings.FoldingWarning<FoldingWarnings.StringFormatter<InvocationQueryExpression>, int>("The argument count of function {0} should be {1}.", new FoldingWarnings.StringFormatter<InvocationQueryExpression>(expression, new Func<InvocationQueryExpression, string>(FoldingWarnings.QueryExpressionToString)), requiredCount);
		}

		// Token: 0x060080E4 RID: 32996 RVA: 0x001B7665 File Offset: 0x001B5865
		public static FoldingWarnings.FoldingWarning<int, FoldingWarnings.StringFormatter<InvocationQueryExpression>, string> InvalidArgumentType(InvocationQueryExpression expression, int argumentIndex, string required)
		{
			return new FoldingWarnings.FoldingWarning<int, FoldingWarnings.StringFormatter<InvocationQueryExpression>, string>("The type of argument {0} to function {1} should be {2}.", argumentIndex, new FoldingWarnings.StringFormatter<InvocationQueryExpression>(expression, new Func<InvocationQueryExpression, string>(FoldingWarnings.QueryExpressionToString)), required);
		}

		// Token: 0x060080E5 RID: 32997 RVA: 0x001B7685 File Offset: 0x001B5885
		public static FoldingWarnings.FoldingWarning<int, string, string> InvalidArgumentType(int argumentIndex, string functionName, string requiredType)
		{
			return new FoldingWarnings.FoldingWarning<int, string, string>("The type of argument {0} to function {1} should be {2}.", argumentIndex, functionName, requiredType);
		}

		// Token: 0x060080E6 RID: 32998 RVA: 0x001B7694 File Offset: 0x001B5894
		public static FoldingWarnings.FoldingWarning<int, FoldingWarnings.StringFormatter<InvocationQueryExpression>, string> InvalidArgumentValue(InvocationQueryExpression expression, int argumentIndex, string reason)
		{
			return new FoldingWarnings.FoldingWarning<int, FoldingWarnings.StringFormatter<InvocationQueryExpression>, string>("The value of argument {0} to function {1} should qualify {2}.", argumentIndex, new FoldingWarnings.StringFormatter<InvocationQueryExpression>(expression, new Func<InvocationQueryExpression, string>(FoldingWarnings.QueryExpressionToString)), reason);
		}

		// Token: 0x060080E7 RID: 32999 RVA: 0x001B76B4 File Offset: 0x001B58B4
		public static FoldingWarnings.FoldingWarning<FoldingWarnings.StringFormatter<InvocationQueryExpression>> InvalidFunction(InvocationQueryExpression expression)
		{
			return new FoldingWarnings.FoldingWarning<FoldingWarnings.StringFormatter<InvocationQueryExpression>>("The function {0} is not valid.", new FoldingWarnings.StringFormatter<InvocationQueryExpression>(expression, new Func<InvocationQueryExpression, string>(FoldingWarnings.QueryExpressionToString)));
		}

		// Token: 0x060080E8 RID: 33000 RVA: 0x001B76D2 File Offset: 0x001B58D2
		public static FoldingWarnings.FoldingWarning<string> InvalidType(string type)
		{
			return new FoldingWarnings.FoldingWarning<string>("The type {0} is not supported.", type);
		}

		// Token: 0x060080E9 RID: 33001 RVA: 0x001B76DF File Offset: 0x001B58DF
		public static FoldingWarnings.FoldingWarning<string, string> InvalidType(string type, string idea)
		{
			return new FoldingWarnings.FoldingWarning<string, string>("The type {0} is not supported. Consider: {1}.", type, idea);
		}

		// Token: 0x060080EA RID: 33002 RVA: 0x001B76ED File Offset: 0x001B58ED
		public static FoldingWarnings.FoldingWarning<FoldingWarnings.StringFormatter<Value>, string> InvalidValue(Value val, string reason)
		{
			return new FoldingWarnings.FoldingWarning<FoldingWarnings.StringFormatter<Value>, string>("The value {0} is not supported: {1}.", new FoldingWarnings.StringFormatter<Value>(val, new Func<Value, string>(FoldingWarnings.ValueObjectToString)), reason);
		}

		// Token: 0x060080EB RID: 33003 RVA: 0x001B770C File Offset: 0x001B590C
		public static FoldingWarnings.FoldingWarning<FoldingWarnings.StringFormatter<string[]>, FoldingWarnings.StringFormatter<IDictionary<string, string>>> NoLikeEscapeCharacter(string[] likeEscapeCharacterCandidates, IDictionary<string, string> stringLiteralEscapeCharacters)
		{
			return new FoldingWarnings.FoldingWarning<FoldingWarnings.StringFormatter<string[]>, FoldingWarnings.StringFormatter<IDictionary<string, string>>>("Not able to find like escape character from candidates( {0} ) which should not be contained in data source String Literal Escape Characters {1}. You can override this by using SqlCapabilities for StringLiteralEscapeCharacters.", new FoldingWarnings.StringFormatter<string[]>(likeEscapeCharacterCandidates, new Func<string[], string>(FoldingWarnings.StringArrayToString)), new FoldingWarnings.StringFormatter<IDictionary<string, string>>(stringLiteralEscapeCharacters, new Func<IDictionary<string, string>, string>(FoldingWarnings.DictionaryToString)));
		}

		// Token: 0x060080EC RID: 33004 RVA: 0x001B773C File Offset: 0x001B593C
		public static FoldingWarnings.FoldingWarning<NumberValue, string> NumberTryGet(NumberValue numberValue, Type type)
		{
			return new FoldingWarnings.FoldingWarning<NumberValue, string>("Unable to convert constant {0} to type {1}.", numberValue, type.Name);
		}

		// Token: 0x060080ED RID: 33005 RVA: 0x001B774F File Offset: 0x001B594F
		public static FoldingWarnings.FoldingWarning<FoldingWarnings.StringFormatter<Keys>, FoldingWarnings.StringFormatter<FunctionValue>> SelectRows(Keys columns, FunctionValue functionValue)
		{
			return new FoldingWarnings.FoldingWarning<FoldingWarnings.StringFormatter<Keys>, FoldingWarnings.StringFormatter<FunctionValue>>("Failed to convert query expression for Columns {0} Function {1}.", new FoldingWarnings.StringFormatter<Keys>(columns, new Func<Keys, string>(FoldingWarnings.KeysToString)), new FoldingWarnings.StringFormatter<FunctionValue>(functionValue, new Func<FunctionValue, string>(FoldingWarnings.FunctionValueToString)));
		}

		// Token: 0x060080EE RID: 33006 RVA: 0x001B777F File Offset: 0x001B597F
		public static FoldingWarnings.FoldingWarning<RowCount, RowCount> SkipTake(RowRange rowRange)
		{
			return new FoldingWarnings.FoldingWarning<RowCount, RowCount>("Row Range is not able to fold. Try to avoid functions like Table.Skip, Table.Range, Table.FirstN, etc. Current Row Range SkipCount {0}; TakeCount {1}.", rowRange.SkipCount, rowRange.TakeCount);
		}

		// Token: 0x060080EF RID: 33007 RVA: 0x001B7799 File Offset: 0x001B5999
		public static FoldingWarnings.FoldingWarning<FoldingWarnings.StringFormatter<Keys>> SortColumns(Keys columns)
		{
			return new FoldingWarnings.FoldingWarning<FoldingWarnings.StringFormatter<Keys>>("Not able to sort columns {0}. Check Table.Sort and/or other related functions in your M code.", new FoldingWarnings.StringFormatter<Keys>(columns, new Func<Keys, string>(FoldingWarnings.KeysToString)));
		}

		// Token: 0x060080F0 RID: 33008 RVA: 0x001B77B7 File Offset: 0x001B59B7
		public static FoldingWarnings.FoldingWarning<int> SortInvalidColumn(int index)
		{
			return new FoldingWarnings.FoldingWarning<int>("The sort column at index {0} should be a table column. Please take a look Table.Sort at your M code.", index);
		}

		// Token: 0x060080F1 RID: 33009 RVA: 0x001B77C4 File Offset: 0x001B59C4
		public static FoldingWarnings.FoldingWarning<TableTypeAlgebra.JoinKind> UnsupportedJoinKind(TableTypeAlgebra.JoinKind joinKind)
		{
			return new FoldingWarnings.FoldingWarning<TableTypeAlgebra.JoinKind>("The join {0} is not supported.", joinKind);
		}

		// Token: 0x060080F2 RID: 33010 RVA: 0x001B77D1 File Offset: 0x001B59D1
		public static FoldingWarnings.FoldingWarning<string> UnknownOperator(string @operator)
		{
			return new FoldingWarnings.FoldingWarning<string>("This operator is not supported for folding ({0}).", @operator);
		}

		// Token: 0x060080F3 RID: 33011 RVA: 0x001B77DE File Offset: 0x001B59DE
		public static FoldingWarnings.FoldingWarning<string> UnsupportedOperator(string @operator)
		{
			return new FoldingWarnings.FoldingWarning<string>("The {0} operator is not supported for folding.", @operator);
		}

		// Token: 0x060080F4 RID: 33012 RVA: 0x001B77EB File Offset: 0x001B59EB
		public static FoldingWarnings.FoldingWarning<string, string, string> UnsupportedOperatorTypes(string @operator, string left, string right)
		{
			return new FoldingWarnings.FoldingWarning<string, string, string>("The {0} operator is not supported for folding over type {1} and {2}.", @operator, left, right);
		}

		// Token: 0x060080F5 RID: 33013 RVA: 0x001B77FA File Offset: 0x001B59FA
		public static FoldingWarnings.FoldingWarning<string, string> UnsupportedFunctionArgumentType(string function, string type)
		{
			return new FoldingWarnings.FoldingWarning<string, string>("Function {0} does not support folding over arguments of type {1}.", function, type);
		}

		// Token: 0x060080F6 RID: 33014 RVA: 0x001B7808 File Offset: 0x001B5A08
		public static FoldingWarnings.FoldingWarning<string> UnsupportedFunction(string function)
		{
			return new FoldingWarnings.FoldingWarning<string>("This driver doesn't support the function {0}.", function);
		}

		// Token: 0x060080F7 RID: 33015 RVA: 0x001B7815 File Offset: 0x001B5A15
		public static FoldingWarnings.FoldingWarning<string, string> HeterogeneousJoin(string left, string right)
		{
			return new FoldingWarnings.FoldingWarning<string, string>("Attempt to perform heterogeneous join between {0} and {1}.", left, right);
		}

		// Token: 0x060080F8 RID: 33016 RVA: 0x001B7824 File Offset: 0x001B5A24
		public static string InvocationExpressionToString(IInvocationExpression expression)
		{
			string text = "Unknown-Function";
			Identifier identifier;
			if (expression.Function.TryGetIdentifier(out identifier) && identifier != null)
			{
				text = identifier.Name;
			}
			return string.Format(CultureInfo.InvariantCulture, "{0}(Arguments[{1}])", text, expression.Arguments.Count);
		}

		// Token: 0x060080F9 RID: 33017 RVA: 0x001B7878 File Offset: 0x001B5A78
		private static string ValueObjectToString(Value value)
		{
			string text = "Unknown";
			string text2 = "Unknown";
			if (value != null)
			{
				text = value.Kind.ToString();
				text2 = value.ToString();
			}
			return string.Format(CultureInfo.InvariantCulture, "{0}:{1}", text, text2);
		}

		// Token: 0x060080FA RID: 33018 RVA: 0x001B78C4 File Offset: 0x001B5AC4
		private static string QueryExpressionToString(InvocationQueryExpression expression)
		{
			IExpression expression2 = null;
			if (expression.Function.Kind == QueryExpressionKind.Constant)
			{
				Value value = ((ConstantQueryExpression)expression.Function).Value;
				if (value.Expression != null)
				{
					expression2 = value.Expression;
				}
				else
				{
					string text = value.ToSource();
					if (!string.IsNullOrEmpty(text))
					{
						return text;
					}
				}
			}
			if (expression2 != null && expression2.Kind == ExpressionKind.Identifier)
			{
				return ((LibraryIdentifierExpression)expression2).Name.ToString();
			}
			return "Non-Function";
		}

		// Token: 0x060080FB RID: 33019 RVA: 0x001B7936 File Offset: 0x001B5B36
		public static string FunctionValueToString(FunctionValue functionValue)
		{
			if (functionValue.Expression == null)
			{
				return "(unknown function)";
			}
			return FoldingWarnings.expressionToMVisitor.Value.Visit(functionValue.Expression);
		}

		// Token: 0x060080FC RID: 33020 RVA: 0x001B795C File Offset: 0x001B5B5C
		public static string TypeValueToString(TypeValue typeValue)
		{
			if (typeValue == null)
			{
				return "null";
			}
			return string.Format(CultureInfo.InvariantCulture, "{0}.Kind={1}, {0}.TypeKind={2}", typeValue.GetType().Name, typeValue.Kind.ToString(), typeValue.TypeKind.ToString());
		}

		// Token: 0x060080FD RID: 33021 RVA: 0x001B79B4 File Offset: 0x001B5BB4
		public static string GetObjectTypeName(object type)
		{
			if (type == null)
			{
				return "null";
			}
			return type.GetType().Name;
		}

		// Token: 0x060080FE RID: 33022 RVA: 0x001B79CA File Offset: 0x001B5BCA
		private static string KeysToString(Keys keys)
		{
			if (keys != null)
			{
				return keys.ToString();
			}
			return string.Empty;
		}

		// Token: 0x060080FF RID: 33023 RVA: 0x001B79DC File Offset: 0x001B5BDC
		private static string TableDistinctToString(TableDistinct distinctCriteria)
		{
			StringBuilder builder = new StringBuilder();
			if (distinctCriteria.Distincts != null)
			{
				Array.ForEach<Distinct>(distinctCriteria.Distincts, delegate(Distinct distinct)
				{
					if (distinct.Selector != null)
					{
						builder.Append(FoldingWarnings.FunctionValueToString(distinct.Selector));
					}
					builder.Append(";");
				});
			}
			return builder.ToString();
		}

		// Token: 0x06008100 RID: 33024 RVA: 0x001B7A24 File Offset: 0x001B5C24
		private static string StringArrayToString(string[] strings)
		{
			return string.Join(" ", strings);
		}

		// Token: 0x06008101 RID: 33025 RVA: 0x001B7A34 File Offset: 0x001B5C34
		private static string DictionaryToString(IDictionary<string, string> dictionary)
		{
			string text = "";
			foreach (KeyValuePair<string, string> keyValuePair in dictionary)
			{
				text = string.Concat(new string[] { text, "{", keyValuePair.Key, ",", keyValuePair.Value, "}," });
			}
			return text.TrimEnd(new char[] { ',' });
		}

		// Token: 0x04004627 RID: 17959
		public const string AggregatesNotAllowed = "Aggregates are not allowed.";

		// Token: 0x04004628 RID: 17960
		public const string DifferentQueryDomainAtJoin = "The left and right queries come from different data sources.";

		// Token: 0x04004629 RID: 17961
		public const string GroupAdjacent = "Folding failed because GroupKind is Local. Check groupKind parameter in Table.Group and/or other related functions in your M code.";

		// Token: 0x0400462A RID: 17962
		public const string GroupComparer = "Folding failed due to Compare exists. Check comparer parameter in Table.Group and/or other related functions in your M code.";

		// Token: 0x0400462B RID: 17963
		public const string GroupOrderByColumns = "Order By columns should not exist when doing Group By. Check Table.Sort and/or other related functions in your M code.";

		// Token: 0x0400462C RID: 17964
		public const string InvalidKeyComparerAtJoin = "Key comparer function should be Value.Equals or Value.NullableEquals.";

		// Token: 0x0400462D RID: 17965
		public const string NotImplemented = "Not implemented";

		// Token: 0x0400462E RID: 17966
		public const string SelectionColumnEmpty = "The collection of selected columns is empty.";

		// Token: 0x0400462F RID: 17967
		public const string FractionalSecondsScaleNotSet = "Fraction of a second specified in query but FractionalSecondsScale not set in connector.";

		// Token: 0x04004630 RID: 17968
		public const string PivotNotSupported = "This data source does not support pivot or unpivot operations.";

		// Token: 0x04004631 RID: 17969
		private const string AddColumnsKindFormat = "Folding failed on adding columns when applying function to columns {0}. Function {1}.";

		// Token: 0x04004632 RID: 17970
		private const string ConstantRequiredFormat = "Argument {0} to function {1} should be a constant.";

		// Token: 0x04004633 RID: 17971
		private const string DistinctColumnsFormat = "Could not find distinct columns from columns {0} with functions {1}.";

		// Token: 0x04004634 RID: 17972
		private const string DistinctColumnsCountFormat = "Distinct columns count {0} doesn't match select columns count {1}.";

		// Token: 0x04004635 RID: 17973
		private const string FunctionNotImplementedFormat = "This data source does not implement folding for the function {0}.";

		// Token: 0x04004636 RID: 17974
		private const string HeterogeneousJoinFormat = "Attempt to perform heterogeneous join between {0} and {1}.";

		// Token: 0x04004637 RID: 17975
		private const string InnerJoinOnlyFormat = "Inner Join is required when left key column is empty. Consider changing {0} to JoinKind.InnerJoin.";

		// Token: 0x04004638 RID: 17976
		private const string InvalidFunctionArgumentFormat = "The value of the '{0}' argument to function {1} should be {2}.";

		// Token: 0x04004639 RID: 17977
		private const string InvalidFunctionArgumentsCountFormat = "The argument count of function {0} should be {1}.";

		// Token: 0x0400463A RID: 17978
		private const string InvalidExpressionArgumentsCountFormat = "The argument count of function {0} should be between {1} and {2}.";

		// Token: 0x0400463B RID: 17979
		private const string InvalidFunctionArgumentTypeFormat = "The type of argument {0} to function {1} should be {2}.";

		// Token: 0x0400463C RID: 17980
		private const string InvalidFunctionArgumentValueFormat = "The value of argument {0} to function {1} should qualify {2}.";

		// Token: 0x0400463D RID: 17981
		private const string InvalidFunctionFormat = "The function {0} is not valid.";

		// Token: 0x0400463E RID: 17982
		private const string InvalidSqlGetInfoFormat = "This driver is expected to have {0} for {1}, but it only has {2}. You can use SqlGetInfo to override this.";

		// Token: 0x0400463F RID: 17983
		private const string InvalidTypeFormat1 = "The type {0} is not supported.";

		// Token: 0x04004640 RID: 17984
		private const string InvalidTypeFormat2 = "The type {0} is not supported. Consider: {1}.";

		// Token: 0x04004641 RID: 17985
		private const string InvalidValueFormat = "The value {0} is not supported: {1}.";

		// Token: 0x04004642 RID: 17986
		private const string MismatchedPivotColumnTypesFormat = "The pivot column fields must have compatible types. Field {0} is of type {1}, but field {2} is of type {3}.";

		// Token: 0x04004643 RID: 17987
		private const string NoLikeEscapeCharacterFormat = "Not able to find like escape character from candidates( {0} ) which should not be contained in data source String Literal Escape Characters {1}. You can override this by using SqlCapabilities for StringLiteralEscapeCharacters.";

		// Token: 0x04004644 RID: 17988
		private const string NotSupportJoinByDriverFormat = "This driver doesn't support {0} joins. You can override this by using SqlGetInfo for SQL_SQL92_RELATIONAL_JOIN_OPERATORS.";

		// Token: 0x04004645 RID: 17989
		private const string NumberTryGetFormat = "Unable to convert constant {0} to type {1}.";

		// Token: 0x04004646 RID: 17990
		private const string SelectRowsFormat = "Failed to convert query expression for Columns {0} Function {1}.";

		// Token: 0x04004647 RID: 17991
		private const string SkipTakeFormat = "Row Range is not able to fold. Try to avoid functions like Table.Skip, Table.Range, Table.FirstN, etc. Current Row Range SkipCount {0}; TakeCount {1}.";

		// Token: 0x04004648 RID: 17992
		private const string SortColumnsFormat = "Not able to sort columns {0}. Check Table.Sort and/or other related functions in your M code.";

		// Token: 0x04004649 RID: 17993
		private const string SortInvalidColumnFormat = "The sort column at index {0} should be a table column. Please take a look Table.Sort at your M code.";

		// Token: 0x0400464A RID: 17994
		private const string SqlCapabilitiesFormat = "This driver doesn't set the {0} feature. You can override it by using SqlCapabilities.";

		// Token: 0x0400464B RID: 17995
		private const string UnknownOperatorFormat = "This operator is not supported for folding ({0}).";

		// Token: 0x0400464C RID: 17996
		private const string UnsupportedOperatorFormat = "The {0} operator is not supported for folding.";

		// Token: 0x0400464D RID: 17997
		private const string UnsupportedOperatorTypesFormat = "The {0} operator is not supported for folding over type {1} and {2}.";

		// Token: 0x0400464E RID: 17998
		private const string UnsupportedFunctionArgumentTypeFormat = "Function {0} does not support folding over arguments of type {1}.";

		// Token: 0x0400464F RID: 17999
		private const string UnsupportedFunctionFormat = "This driver doesn't support the function {0}.";

		// Token: 0x04004650 RID: 18000
		private const string UnsupportedJoinKindFormat = "The join {0} is not supported.";

		// Token: 0x04004651 RID: 18001
		private static readonly Lazy<FoldingWarnings.InvocationToMVisitor> expressionToMVisitor = new Lazy<FoldingWarnings.InvocationToMVisitor>(() => new FoldingWarnings.InvocationToMVisitor());

		// Token: 0x0200130C RID: 4876
		public struct FoldingWarning<T1> : IFoldingWarning
		{
			// Token: 0x06008103 RID: 33027 RVA: 0x001B7AE4 File Offset: 0x001B5CE4
			public FoldingWarning(string format, T1 value1)
			{
				this.format = format;
				this.value1 = value1;
			}

			// Token: 0x06008104 RID: 33028 RVA: 0x001B7AF4 File Offset: 0x001B5CF4
			public void Trace(IHostTrace trace)
			{
				trace.Add("ErrorMessage", string.Format(CultureInfo.InvariantCulture, this.format, this.value1), true);
			}

			// Token: 0x04004652 RID: 18002
			private readonly string format;

			// Token: 0x04004653 RID: 18003
			private readonly T1 value1;
		}

		// Token: 0x0200130D RID: 4877
		public struct FoldingWarning<T1, T2> : IFoldingWarning
		{
			// Token: 0x06008105 RID: 33029 RVA: 0x001B7B1D File Offset: 0x001B5D1D
			public FoldingWarning(string format, T1 value1, T2 value2)
			{
				this.format = format;
				this.value1 = value1;
				this.value2 = value2;
			}

			// Token: 0x06008106 RID: 33030 RVA: 0x001B7B34 File Offset: 0x001B5D34
			public void Trace(IHostTrace trace)
			{
				trace.Add("ErrorMessage", string.Format(CultureInfo.InvariantCulture, this.format, this.value1, this.value2), true);
			}

			// Token: 0x04004654 RID: 18004
			private readonly string format;

			// Token: 0x04004655 RID: 18005
			private readonly T1 value1;

			// Token: 0x04004656 RID: 18006
			private readonly T2 value2;
		}

		// Token: 0x0200130E RID: 4878
		public struct FoldingWarning<T1, T2, T3> : IFoldingWarning
		{
			// Token: 0x06008107 RID: 33031 RVA: 0x001B7B68 File Offset: 0x001B5D68
			public FoldingWarning(string format, T1 value1, T2 value2, T3 value3)
			{
				this.format = format;
				this.value1 = value1;
				this.value2 = value2;
				this.value3 = value3;
			}

			// Token: 0x06008108 RID: 33032 RVA: 0x001B7B87 File Offset: 0x001B5D87
			public void Trace(IHostTrace trace)
			{
				trace.Add("ErrorMessage", string.Format(CultureInfo.InvariantCulture, this.format, this.value1, this.value2, this.value3), true);
			}

			// Token: 0x04004657 RID: 18007
			private readonly string format;

			// Token: 0x04004658 RID: 18008
			private readonly T1 value1;

			// Token: 0x04004659 RID: 18009
			private readonly T2 value2;

			// Token: 0x0400465A RID: 18010
			private readonly T3 value3;
		}

		// Token: 0x0200130F RID: 4879
		public struct FoldingWarning<T1, T2, T3, T4> : IFoldingWarning
		{
			// Token: 0x06008109 RID: 33033 RVA: 0x001B7BC6 File Offset: 0x001B5DC6
			public FoldingWarning(string format, T1 value1, T2 value2, T3 value3, T4 value4)
			{
				this.format = format;
				this.value1 = value1;
				this.value2 = value2;
				this.value3 = value3;
				this.value4 = value4;
			}

			// Token: 0x0600810A RID: 33034 RVA: 0x001B7BF0 File Offset: 0x001B5DF0
			public void Trace(IHostTrace trace)
			{
				trace.Add("ErrorMessage", string.Format(CultureInfo.InvariantCulture, this.format, new object[] { this.value1, this.value2, this.value3, this.value4 }), true);
			}

			// Token: 0x0400465B RID: 18011
			private readonly string format;

			// Token: 0x0400465C RID: 18012
			private readonly T1 value1;

			// Token: 0x0400465D RID: 18013
			private readonly T2 value2;

			// Token: 0x0400465E RID: 18014
			private readonly T3 value3;

			// Token: 0x0400465F RID: 18015
			private readonly T4 value4;
		}

		// Token: 0x02001310 RID: 4880
		public struct FoldingWarning<T1, T2, T3, T4, T5> : IFoldingWarning
		{
			// Token: 0x0600810B RID: 33035 RVA: 0x001B7C57 File Offset: 0x001B5E57
			public FoldingWarning(string format, T1 value1, T2 value2, T3 value3, T4 value4, T5 value5)
			{
				this.format = format;
				this.value1 = value1;
				this.value2 = value2;
				this.value3 = value3;
				this.value4 = value4;
				this.value5 = value5;
			}

			// Token: 0x0600810C RID: 33036 RVA: 0x001B7C88 File Offset: 0x001B5E88
			public void Trace(IHostTrace trace)
			{
				trace.Add("ErrorMessage", string.Format(CultureInfo.InvariantCulture, this.format, new object[] { this.value1, this.value2, this.value3, this.value4, this.value5 }), true);
			}

			// Token: 0x04004660 RID: 18016
			private readonly string format;

			// Token: 0x04004661 RID: 18017
			private readonly T1 value1;

			// Token: 0x04004662 RID: 18018
			private readonly T2 value2;

			// Token: 0x04004663 RID: 18019
			private readonly T3 value3;

			// Token: 0x04004664 RID: 18020
			private readonly T4 value4;

			// Token: 0x04004665 RID: 18021
			private readonly T5 value5;
		}

		// Token: 0x02001311 RID: 4881
		public struct StringFormatter<T1>
		{
			// Token: 0x0600810D RID: 33037 RVA: 0x001B7CFD File Offset: 0x001B5EFD
			public StringFormatter(T1 value1, Func<T1, string> toString)
			{
				this.value1 = value1;
				this.toString = toString;
			}

			// Token: 0x0600810E RID: 33038 RVA: 0x001B7D0D File Offset: 0x001B5F0D
			public override string ToString()
			{
				return this.toString(this.value1);
			}

			// Token: 0x04004666 RID: 18022
			private readonly T1 value1;

			// Token: 0x04004667 RID: 18023
			private readonly Func<T1, string> toString;
		}

		// Token: 0x02001312 RID: 4882
		public struct StringFormatter<T1, T2>
		{
			// Token: 0x0600810F RID: 33039 RVA: 0x001B7D20 File Offset: 0x001B5F20
			public StringFormatter(T1 value1, T2 value2, Func<T1, T2, string> toString)
			{
				this.value1 = value1;
				this.value2 = value2;
				this.toString = toString;
			}

			// Token: 0x06008110 RID: 33040 RVA: 0x001B7D37 File Offset: 0x001B5F37
			public override string ToString()
			{
				return this.toString(this.value1, this.value2);
			}

			// Token: 0x04004668 RID: 18024
			private readonly T1 value1;

			// Token: 0x04004669 RID: 18025
			private readonly T2 value2;

			// Token: 0x0400466A RID: 18026
			private readonly Func<T1, T2, string> toString;
		}

		// Token: 0x02001313 RID: 4883
		public struct FoldingException : IFoldingWarning
		{
			// Token: 0x06008111 RID: 33041 RVA: 0x001B7D50 File Offset: 0x001B5F50
			public FoldingException(Exception exception)
			{
				this.exception = exception;
			}

			// Token: 0x06008112 RID: 33042 RVA: 0x001B7D59 File Offset: 0x001B5F59
			public void Trace(IHostTrace trace)
			{
				trace.Add(this.exception, true);
			}

			// Token: 0x0400466B RID: 18027
			private readonly Exception exception;
		}

		// Token: 0x02001314 RID: 4884
		private class InvocationToMVisitor : ExpressionToMVisitor
		{
			// Token: 0x06008113 RID: 33043 RVA: 0x001B7D68 File Offset: 0x001B5F68
			public InvocationToMVisitor()
				: base(Engine.Instance, null)
			{
			}

			// Token: 0x06008114 RID: 33044 RVA: 0x001B7D78 File Offset: 0x001B5F78
			protected override void WriteTable(ITableValue table)
			{
				IndentedTextWriter writer = this.writer;
				int num = writer.Indent;
				writer.Indent = num + 1;
				this.writer.Write("#table({}, {}) /* table data omitted */");
				IndentedTextWriter writer2 = this.writer;
				num = writer2.Indent;
				writer2.Indent = num - 1;
			}
		}
	}
}
