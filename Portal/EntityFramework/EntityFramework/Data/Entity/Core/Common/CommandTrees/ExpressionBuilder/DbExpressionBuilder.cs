using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder.Internal;
using System.Data.Entity.Core.Common.CommandTrees.Internal;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Metadata.Edm.Provider;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder
{
	// Token: 0x020006F6 RID: 1782
	public static class DbExpressionBuilder
	{
		// Token: 0x060052D0 RID: 21200 RVA: 0x0012A089 File Offset: 0x00128289
		public static KeyValuePair<string, DbExpression> As(this DbExpression value, string alias)
		{
			return new KeyValuePair<string, DbExpression>(alias, value);
		}

		// Token: 0x060052D1 RID: 21201 RVA: 0x0012A092 File Offset: 0x00128292
		public static KeyValuePair<string, DbAggregate> As(this DbAggregate value, string alias)
		{
			return new KeyValuePair<string, DbAggregate>(alias, value);
		}

		// Token: 0x060052D2 RID: 21202 RVA: 0x0012A09B File Offset: 0x0012829B
		public static DbExpressionBinding Bind(this DbExpression input)
		{
			Check.NotNull<DbExpression>(input, "input");
			return input.BindAs(DbExpressionBuilder._bindingAliases.Next());
		}

		// Token: 0x060052D3 RID: 21203 RVA: 0x0012A0BC File Offset: 0x001282BC
		public static DbExpressionBinding BindAs(this DbExpression input, string varName)
		{
			Check.NotNull<DbExpression>(input, "input");
			Check.NotNull<string>(varName, "varName");
			Check.NotEmpty(varName, "varName");
			TypeUsage typeUsage = null;
			if (!TypeHelpers.TryGetCollectionElementType(input.ResultType, out typeUsage))
			{
				throw new ArgumentException(Strings.Cqt_Binding_CollectionRequired, "input");
			}
			DbVariableReferenceExpression dbVariableReferenceExpression = new DbVariableReferenceExpression(typeUsage, varName);
			return new DbExpressionBinding(input, dbVariableReferenceExpression);
		}

		// Token: 0x060052D4 RID: 21204 RVA: 0x0012A120 File Offset: 0x00128320
		public static DbGroupExpressionBinding GroupBind(this DbExpression input)
		{
			Check.NotNull<DbExpression>(input, "input");
			string text = DbExpressionBuilder._bindingAliases.Next();
			return input.GroupBindAs(text, string.Format(CultureInfo.InvariantCulture, "Group{0}", new object[] { text }));
		}

		// Token: 0x060052D5 RID: 21205 RVA: 0x0012A164 File Offset: 0x00128364
		public static DbGroupExpressionBinding GroupBindAs(this DbExpression input, string varName, string groupVarName)
		{
			Check.NotNull<DbExpression>(input, "input");
			Check.NotNull<string>(varName, "varName");
			Check.NotEmpty(varName, "varName");
			Check.NotNull<string>(groupVarName, "groupVarName");
			Check.NotEmpty(groupVarName, "groupVarName");
			TypeUsage typeUsage = null;
			if (!TypeHelpers.TryGetCollectionElementType(input.ResultType, out typeUsage))
			{
				throw new ArgumentException(Strings.Cqt_GroupBinding_CollectionRequired, "input");
			}
			DbVariableReferenceExpression dbVariableReferenceExpression = new DbVariableReferenceExpression(typeUsage, varName);
			DbVariableReferenceExpression dbVariableReferenceExpression2 = new DbVariableReferenceExpression(typeUsage, groupVarName);
			return new DbGroupExpressionBinding(input, dbVariableReferenceExpression, dbVariableReferenceExpression2);
		}

		// Token: 0x060052D6 RID: 21206 RVA: 0x0012A1E6 File Offset: 0x001283E6
		public static DbFunctionAggregate Aggregate(this EdmFunction function, DbExpression argument)
		{
			Check.NotNull<EdmFunction>(function, "function");
			Check.NotNull<DbExpression>(argument, "argument");
			return DbExpressionBuilder.CreateFunctionAggregate(function, argument, false);
		}

		// Token: 0x060052D7 RID: 21207 RVA: 0x0012A208 File Offset: 0x00128408
		public static DbFunctionAggregate AggregateDistinct(this EdmFunction function, DbExpression argument)
		{
			Check.NotNull<EdmFunction>(function, "function");
			Check.NotNull<DbExpression>(argument, "argument");
			return DbExpressionBuilder.CreateFunctionAggregate(function, argument, true);
		}

		// Token: 0x060052D8 RID: 21208 RVA: 0x0012A22C File Offset: 0x0012842C
		private static DbFunctionAggregate CreateFunctionAggregate(EdmFunction function, DbExpression argument, bool isDistinct)
		{
			DbExpressionList dbExpressionList = ArgumentValidation.ValidateFunctionAggregate(function, new DbExpression[] { argument });
			return new DbFunctionAggregate(function.ReturnParameter.TypeUsage, dbExpressionList, function, isDistinct);
		}

		// Token: 0x060052D9 RID: 21209 RVA: 0x0012A25D File Offset: 0x0012845D
		public static DbFunctionAggregate Aggregate(this EdmFunction function, IEnumerable<DbExpression> arguments)
		{
			Check.NotNull<EdmFunction>(function, "function");
			Check.NotNull<IEnumerable<DbExpression>>(arguments, "argument");
			if (!arguments.Any<DbExpression>())
			{
				throw new ArgumentNullException("arguments");
			}
			return DbExpressionBuilder.CreateFunctionAggregate(function, arguments, false);
		}

		// Token: 0x060052DA RID: 21210 RVA: 0x0012A292 File Offset: 0x00128492
		public static DbFunctionAggregate AggregateDistinct(this EdmFunction function, IEnumerable<DbExpression> arguments)
		{
			Check.NotNull<EdmFunction>(function, "function");
			Check.NotNull<IEnumerable<DbExpression>>(arguments, "argument");
			if (!arguments.Any<DbExpression>())
			{
				throw new ArgumentNullException("arguments");
			}
			return DbExpressionBuilder.CreateFunctionAggregate(function, arguments, true);
		}

		// Token: 0x060052DB RID: 21211 RVA: 0x0012A2C8 File Offset: 0x001284C8
		private static DbFunctionAggregate CreateFunctionAggregate(EdmFunction function, IEnumerable<DbExpression> arguments, bool isDistinct)
		{
			DbExpressionList dbExpressionList = ArgumentValidation.ValidateFunctionAggregate(function, arguments);
			return new DbFunctionAggregate(function.ReturnParameter.TypeUsage, dbExpressionList, function, isDistinct);
		}

		// Token: 0x060052DC RID: 21212 RVA: 0x0012A2F0 File Offset: 0x001284F0
		public static DbGroupAggregate GroupAggregate(DbExpression argument)
		{
			Check.NotNull<DbExpression>(argument, "argument");
			DbExpressionList dbExpressionList = new DbExpressionList(new DbExpression[] { argument });
			return new DbGroupAggregate(TypeHelpers.CreateCollectionTypeUsage(argument.ResultType), dbExpressionList);
		}

		// Token: 0x060052DD RID: 21213 RVA: 0x0012A32A File Offset: 0x0012852A
		public static DbLambda Lambda(DbExpression body, IEnumerable<DbVariableReferenceExpression> variables)
		{
			Check.NotNull<DbExpression>(body, "body");
			Check.NotNull<IEnumerable<DbVariableReferenceExpression>>(variables, "variables");
			return DbExpressionBuilder.CreateLambda(body, variables);
		}

		// Token: 0x060052DE RID: 21214 RVA: 0x0012A34B File Offset: 0x0012854B
		public static DbLambda Lambda(DbExpression body, params DbVariableReferenceExpression[] variables)
		{
			Check.NotNull<DbExpression>(body, "body");
			Check.NotNull<DbVariableReferenceExpression[]>(variables, "variables");
			return DbExpressionBuilder.CreateLambda(body, variables);
		}

		// Token: 0x060052DF RID: 21215 RVA: 0x0012A36C File Offset: 0x0012856C
		private static DbLambda CreateLambda(DbExpression body, IEnumerable<DbVariableReferenceExpression> variables)
		{
			return new DbLambda(ArgumentValidation.ValidateLambda(variables), body);
		}

		// Token: 0x060052E0 RID: 21216 RVA: 0x0012A37A File Offset: 0x0012857A
		public static DbSortClause ToSortClause(this DbExpression key)
		{
			Check.NotNull<DbExpression>(key, "key");
			ArgumentValidation.ValidateSortClause(key);
			return new DbSortClause(key, true, string.Empty);
		}

		// Token: 0x060052E1 RID: 21217 RVA: 0x0012A39A File Offset: 0x0012859A
		public static DbSortClause ToSortClauseDescending(this DbExpression key)
		{
			Check.NotNull<DbExpression>(key, "key");
			ArgumentValidation.ValidateSortClause(key);
			return new DbSortClause(key, false, string.Empty);
		}

		// Token: 0x060052E2 RID: 21218 RVA: 0x0012A3BA File Offset: 0x001285BA
		public static DbSortClause ToSortClause(this DbExpression key, string collation)
		{
			Check.NotNull<DbExpression>(key, "key");
			Check.NotNull<string>(collation, "collation");
			ArgumentValidation.ValidateSortClause(key, collation);
			return new DbSortClause(key, true, collation);
		}

		// Token: 0x060052E3 RID: 21219 RVA: 0x0012A3E3 File Offset: 0x001285E3
		public static DbSortClause ToSortClauseDescending(this DbExpression key, string collation)
		{
			Check.NotNull<DbExpression>(key, "key");
			Check.NotNull<string>(collation, "collation");
			ArgumentValidation.ValidateSortClause(key, collation);
			return new DbSortClause(key, false, collation);
		}

		// Token: 0x060052E4 RID: 21220 RVA: 0x0012A40C File Offset: 0x0012860C
		public static DbQuantifierExpression All(this DbExpressionBinding input, DbExpression predicate)
		{
			Check.NotNull<DbExpression>(predicate, "predicate");
			Check.NotNull<DbExpressionBinding>(input, "input");
			TypeUsage typeUsage = ArgumentValidation.ValidateQuantifier(predicate);
			return new DbQuantifierExpression(DbExpressionKind.All, typeUsage, input, predicate);
		}

		// Token: 0x060052E5 RID: 21221 RVA: 0x0012A444 File Offset: 0x00128644
		public static DbQuantifierExpression Any(this DbExpressionBinding input, DbExpression predicate)
		{
			Check.NotNull<DbExpression>(predicate, "predicate");
			Check.NotNull<DbExpressionBinding>(input, "input");
			TypeUsage typeUsage = ArgumentValidation.ValidateQuantifier(predicate);
			return new DbQuantifierExpression(DbExpressionKind.Any, typeUsage, input, predicate);
		}

		// Token: 0x060052E6 RID: 21222 RVA: 0x0012A47C File Offset: 0x0012867C
		public static DbApplyExpression CrossApply(this DbExpressionBinding input, DbExpressionBinding apply)
		{
			Check.NotNull<DbExpressionBinding>(input, "input");
			Check.NotNull<DbExpressionBinding>(apply, "apply");
			DbExpressionBuilder.ValidateApply(input, apply);
			TypeUsage typeUsage = DbExpressionBuilder.CreateApplyResultType(input, apply);
			return new DbApplyExpression(DbExpressionKind.CrossApply, typeUsage, input, apply);
		}

		// Token: 0x060052E7 RID: 21223 RVA: 0x0012A4BC File Offset: 0x001286BC
		public static DbApplyExpression OuterApply(this DbExpressionBinding input, DbExpressionBinding apply)
		{
			Check.NotNull<DbExpressionBinding>(input, "input");
			Check.NotNull<DbExpressionBinding>(apply, "apply");
			DbExpressionBuilder.ValidateApply(input, apply);
			TypeUsage typeUsage = DbExpressionBuilder.CreateApplyResultType(input, apply);
			return new DbApplyExpression(DbExpressionKind.OuterApply, typeUsage, input, apply);
		}

		// Token: 0x060052E8 RID: 21224 RVA: 0x0012A4FA File Offset: 0x001286FA
		private static void ValidateApply(DbExpressionBinding input, DbExpressionBinding apply)
		{
			if (input.VariableName.Equals(apply.VariableName, StringComparison.Ordinal))
			{
				throw new ArgumentException(Strings.Cqt_Apply_DuplicateVariableNames);
			}
		}

		// Token: 0x060052E9 RID: 21225 RVA: 0x0012A51B File Offset: 0x0012871B
		private static TypeUsage CreateApplyResultType(DbExpressionBinding input, DbExpressionBinding apply)
		{
			return ArgumentValidation.CreateCollectionOfRowResultType(new List<KeyValuePair<string, TypeUsage>>
			{
				new KeyValuePair<string, TypeUsage>(input.VariableName, input.VariableType),
				new KeyValuePair<string, TypeUsage>(apply.VariableName, apply.VariableType)
			});
		}

		// Token: 0x060052EA RID: 21226 RVA: 0x0012A558 File Offset: 0x00128758
		public static DbCrossJoinExpression CrossJoin(IEnumerable<DbExpressionBinding> inputs)
		{
			Check.NotNull<IEnumerable<DbExpressionBinding>>(inputs, "inputs");
			TypeUsage typeUsage;
			ReadOnlyCollection<DbExpressionBinding> readOnlyCollection = ArgumentValidation.ValidateCrossJoin(inputs, out typeUsage);
			return new DbCrossJoinExpression(typeUsage, readOnlyCollection);
		}

		// Token: 0x060052EB RID: 21227 RVA: 0x0012A584 File Offset: 0x00128784
		public static DbJoinExpression InnerJoin(this DbExpressionBinding left, DbExpressionBinding right, DbExpression joinCondition)
		{
			Check.NotNull<DbExpressionBinding>(left, "left");
			Check.NotNull<DbExpressionBinding>(right, "right");
			Check.NotNull<DbExpression>(joinCondition, "joinCondition");
			TypeUsage typeUsage = ArgumentValidation.ValidateJoin(left, right, joinCondition);
			return new DbJoinExpression(DbExpressionKind.InnerJoin, typeUsage, left, right, joinCondition);
		}

		// Token: 0x060052EC RID: 21228 RVA: 0x0012A5CC File Offset: 0x001287CC
		public static DbJoinExpression LeftOuterJoin(this DbExpressionBinding left, DbExpressionBinding right, DbExpression joinCondition)
		{
			Check.NotNull<DbExpressionBinding>(left, "left");
			Check.NotNull<DbExpressionBinding>(right, "right");
			Check.NotNull<DbExpression>(joinCondition, "joinCondition");
			TypeUsage typeUsage = ArgumentValidation.ValidateJoin(left, right, joinCondition);
			return new DbJoinExpression(DbExpressionKind.LeftOuterJoin, typeUsage, left, right, joinCondition);
		}

		// Token: 0x060052ED RID: 21229 RVA: 0x0012A614 File Offset: 0x00128814
		public static DbJoinExpression FullOuterJoin(this DbExpressionBinding left, DbExpressionBinding right, DbExpression joinCondition)
		{
			Check.NotNull<DbExpressionBinding>(left, "left");
			Check.NotNull<DbExpressionBinding>(right, "right");
			Check.NotNull<DbExpression>(joinCondition, "joinCondition");
			TypeUsage typeUsage = ArgumentValidation.ValidateJoin(left, right, joinCondition);
			return new DbJoinExpression(DbExpressionKind.FullOuterJoin, typeUsage, left, right, joinCondition);
		}

		// Token: 0x060052EE RID: 21230 RVA: 0x0012A659 File Offset: 0x00128859
		public static DbFilterExpression Filter(this DbExpressionBinding input, DbExpression predicate)
		{
			Check.NotNull<DbExpressionBinding>(input, "input");
			Check.NotNull<DbExpression>(predicate, "predicate");
			return new DbFilterExpression(ArgumentValidation.ValidateFilter(input, predicate), input, predicate);
		}

		// Token: 0x060052EF RID: 21231 RVA: 0x0012A684 File Offset: 0x00128884
		public static DbGroupByExpression GroupBy(this DbGroupExpressionBinding input, IEnumerable<KeyValuePair<string, DbExpression>> keys, IEnumerable<KeyValuePair<string, DbAggregate>> aggregates)
		{
			Check.NotNull<DbGroupExpressionBinding>(input, "input");
			Check.NotNull<IEnumerable<KeyValuePair<string, DbExpression>>>(keys, "keys");
			Check.NotNull<IEnumerable<KeyValuePair<string, DbAggregate>>>(aggregates, "aggregates");
			DbExpressionList dbExpressionList;
			ReadOnlyCollection<DbAggregate> readOnlyCollection;
			return new DbGroupByExpression(ArgumentValidation.ValidateGroupBy(keys, aggregates, out dbExpressionList, out readOnlyCollection), input, dbExpressionList, readOnlyCollection);
		}

		// Token: 0x060052F0 RID: 21232 RVA: 0x0012A6C8 File Offset: 0x001288C8
		public static DbProjectExpression Project(this DbExpressionBinding input, DbExpression projection)
		{
			Check.NotNull<DbExpression>(projection, "projection");
			Check.NotNull<DbExpressionBinding>(input, "input");
			return new DbProjectExpression(DbExpressionBuilder.CreateCollectionResultType(projection.ResultType), input, projection);
		}

		// Token: 0x060052F1 RID: 21233 RVA: 0x0012A6F4 File Offset: 0x001288F4
		public static DbSkipExpression Skip(this DbExpressionBinding input, IEnumerable<DbSortClause> sortOrder, DbExpression count)
		{
			Check.NotNull<DbExpressionBinding>(input, "input");
			Check.NotNull<IEnumerable<DbSortClause>>(sortOrder, "sortOrder");
			Check.NotNull<DbExpression>(count, "count");
			ReadOnlyCollection<DbSortClause> readOnlyCollection = ArgumentValidation.ValidateSortArguments(sortOrder);
			if (!TypeSemantics.IsIntegerNumericType(count.ResultType))
			{
				throw new ArgumentException(Strings.Cqt_Skip_IntegerRequired, "count");
			}
			if (count.ExpressionKind != DbExpressionKind.Constant && count.ExpressionKind != DbExpressionKind.ParameterReference)
			{
				throw new ArgumentException(Strings.Cqt_Skip_ConstantOrParameterRefRequired, "count");
			}
			if (DbExpressionBuilder.IsConstantNegativeInteger(count))
			{
				throw new ArgumentException(Strings.Cqt_Skip_NonNegativeCountRequired, "count");
			}
			return new DbSkipExpression(input.Expression.ResultType, input, readOnlyCollection, count);
		}

		// Token: 0x060052F2 RID: 21234 RVA: 0x0012A798 File Offset: 0x00128998
		public static DbSortExpression Sort(this DbExpressionBinding input, IEnumerable<DbSortClause> sortOrder)
		{
			Check.NotNull<DbExpressionBinding>(input, "input");
			ReadOnlyCollection<DbSortClause> readOnlyCollection = ArgumentValidation.ValidateSort(sortOrder);
			return new DbSortExpression(input.Expression.ResultType, input, readOnlyCollection);
		}

		// Token: 0x060052F3 RID: 21235 RVA: 0x0012A7CA File Offset: 0x001289CA
		public static DbNullExpression Null(this TypeUsage nullType)
		{
			Check.NotNull<TypeUsage>(nullType, "nullType");
			ArgumentValidation.CheckType(nullType, "nullType");
			return new DbNullExpression(nullType);
		}

		// Token: 0x17001001 RID: 4097
		// (get) Token: 0x060052F4 RID: 21236 RVA: 0x0012A7E9 File Offset: 0x001289E9
		public static DbConstantExpression True
		{
			get
			{
				return DbExpressionBuilder._boolTrue;
			}
		}

		// Token: 0x17001002 RID: 4098
		// (get) Token: 0x060052F5 RID: 21237 RVA: 0x0012A7F0 File Offset: 0x001289F0
		public static DbConstantExpression False
		{
			get
			{
				return DbExpressionBuilder._boolFalse;
			}
		}

		// Token: 0x060052F6 RID: 21238 RVA: 0x0012A7F7 File Offset: 0x001289F7
		public static DbConstantExpression Constant(object value)
		{
			Check.NotNull<object>(value, "value");
			return new DbConstantExpression(ArgumentValidation.ValidateConstant(value), value);
		}

		// Token: 0x060052F7 RID: 21239 RVA: 0x0012A811 File Offset: 0x00128A11
		public static DbConstantExpression Constant(this TypeUsage constantType, object value)
		{
			Check.NotNull<TypeUsage>(constantType, "constantType");
			Check.NotNull<object>(value, "value");
			ArgumentValidation.ValidateConstant(constantType, value);
			return new DbConstantExpression(constantType, value);
		}

		// Token: 0x060052F8 RID: 21240 RVA: 0x0012A839 File Offset: 0x00128A39
		public static DbParameterReferenceExpression Parameter(this TypeUsage type, string name)
		{
			Check.NotNull<TypeUsage>(type, "type");
			Check.NotNull<string>(name, "name");
			ArgumentValidation.CheckType(type);
			if (!DbCommandTree.IsValidParameterName(name))
			{
				throw new ArgumentException(Strings.Cqt_CommandTree_InvalidParameterName(name), "name");
			}
			return new DbParameterReferenceExpression(type, name);
		}

		// Token: 0x060052F9 RID: 21241 RVA: 0x0012A879 File Offset: 0x00128A79
		public static DbVariableReferenceExpression Variable(this TypeUsage type, string name)
		{
			Check.NotNull<TypeUsage>(type, "type");
			Check.NotNull<string>(name, "name");
			Check.NotEmpty(name, "name");
			ArgumentValidation.CheckType(type);
			return new DbVariableReferenceExpression(type, name);
		}

		// Token: 0x060052FA RID: 21242 RVA: 0x0012A8AC File Offset: 0x00128AAC
		public static DbScanExpression Scan(this EntitySetBase targetSet)
		{
			Check.NotNull<EntitySetBase>(targetSet, "targetSet");
			ArgumentValidation.CheckEntitySet(targetSet, "targetSet");
			return new DbScanExpression(DbExpressionBuilder.CreateCollectionResultType(targetSet.ElementType), targetSet);
		}

		// Token: 0x060052FB RID: 21243 RVA: 0x0012A8D8 File Offset: 0x00128AD8
		public static DbAndExpression And(this DbExpression left, DbExpression right)
		{
			Check.NotNull<DbExpression>(left, "left");
			Check.NotNull<DbExpression>(right, "right");
			TypeUsage commonTypeUsage = TypeHelpers.GetCommonTypeUsage(left.ResultType, right.ResultType);
			if (commonTypeUsage == null || !TypeSemantics.IsPrimitiveType(commonTypeUsage, PrimitiveTypeKind.Boolean))
			{
				throw new ArgumentException(Strings.Cqt_And_BooleanArgumentsRequired);
			}
			return new DbAndExpression(commonTypeUsage, left, right);
		}

		// Token: 0x060052FC RID: 21244 RVA: 0x0012A930 File Offset: 0x00128B30
		public static DbOrExpression Or(this DbExpression left, DbExpression right)
		{
			Check.NotNull<DbExpression>(left, "left");
			Check.NotNull<DbExpression>(right, "right");
			TypeUsage commonTypeUsage = TypeHelpers.GetCommonTypeUsage(left.ResultType, right.ResultType);
			if (commonTypeUsage == null || !TypeSemantics.IsPrimitiveType(commonTypeUsage, PrimitiveTypeKind.Boolean))
			{
				throw new ArgumentException(Strings.Cqt_Or_BooleanArgumentsRequired);
			}
			return new DbOrExpression(commonTypeUsage, left, right);
		}

		// Token: 0x060052FD RID: 21245 RVA: 0x0012A988 File Offset: 0x00128B88
		public static DbInExpression In(this DbExpression expression, IList<DbConstantExpression> list)
		{
			Check.NotNull<DbExpression>(expression, "expression");
			Check.NotNull<IList<DbConstantExpression>>(list, "list");
			List<DbExpression> list2 = new List<DbExpression>(list.Count);
			foreach (DbConstantExpression dbConstantExpression in list)
			{
				if (!TypeSemantics.IsEqual(expression.ResultType, dbConstantExpression.ResultType))
				{
					throw new ArgumentException(Strings.Cqt_In_SameResultTypeRequired);
				}
				list2.Add(dbConstantExpression);
			}
			return DbExpressionBuilder.CreateInExpression(expression, list2);
		}

		// Token: 0x060052FE RID: 21246 RVA: 0x0012AA1C File Offset: 0x00128C1C
		internal static DbInExpression CreateInExpression(DbExpression item, IList<DbExpression> list)
		{
			return new DbInExpression(DbExpressionBuilder._booleanType, item, new DbExpressionList(list));
		}

		// Token: 0x060052FF RID: 21247 RVA: 0x0012AA2F File Offset: 0x00128C2F
		public static DbNotExpression Not(this DbExpression argument)
		{
			Check.NotNull<DbExpression>(argument, "argument");
			if (!TypeSemantics.IsPrimitiveType(argument.ResultType, PrimitiveTypeKind.Boolean))
			{
				throw new ArgumentException(Strings.Cqt_Not_BooleanArgumentRequired);
			}
			return new DbNotExpression(argument.ResultType, argument);
		}

		// Token: 0x06005300 RID: 21248 RVA: 0x0012AA64 File Offset: 0x00128C64
		private static DbArithmeticExpression CreateArithmetic(DbExpressionKind kind, DbExpression left, DbExpression right)
		{
			TypeUsage commonTypeUsage = TypeHelpers.GetCommonTypeUsage(left.ResultType, right.ResultType);
			if (commonTypeUsage == null || !TypeSemantics.IsNumericType(commonTypeUsage))
			{
				throw new ArgumentException(Strings.Cqt_Arithmetic_NumericCommonType);
			}
			DbExpressionList dbExpressionList = new DbExpressionList(new DbExpression[] { left, right });
			return new DbArithmeticExpression(kind, commonTypeUsage, dbExpressionList);
		}

		// Token: 0x06005301 RID: 21249 RVA: 0x0012AAB5 File Offset: 0x00128CB5
		public static DbArithmeticExpression Divide(this DbExpression left, DbExpression right)
		{
			Check.NotNull<DbExpression>(left, "left");
			Check.NotNull<DbExpression>(right, "right");
			return DbExpressionBuilder.CreateArithmetic(DbExpressionKind.Divide, left, right);
		}

		// Token: 0x06005302 RID: 21250 RVA: 0x0012AAD8 File Offset: 0x00128CD8
		public static DbArithmeticExpression Minus(this DbExpression left, DbExpression right)
		{
			Check.NotNull<DbExpression>(left, "left");
			Check.NotNull<DbExpression>(right, "right");
			return DbExpressionBuilder.CreateArithmetic(DbExpressionKind.Minus, left, right);
		}

		// Token: 0x06005303 RID: 21251 RVA: 0x0012AAFB File Offset: 0x00128CFB
		public static DbArithmeticExpression Modulo(this DbExpression left, DbExpression right)
		{
			Check.NotNull<DbExpression>(left, "left");
			Check.NotNull<DbExpression>(right, "right");
			return DbExpressionBuilder.CreateArithmetic(DbExpressionKind.Modulo, left, right);
		}

		// Token: 0x06005304 RID: 21252 RVA: 0x0012AB1E File Offset: 0x00128D1E
		public static DbArithmeticExpression Multiply(this DbExpression left, DbExpression right)
		{
			Check.NotNull<DbExpression>(left, "left");
			Check.NotNull<DbExpression>(right, "right");
			return DbExpressionBuilder.CreateArithmetic(DbExpressionKind.Multiply, left, right);
		}

		// Token: 0x06005305 RID: 21253 RVA: 0x0012AB41 File Offset: 0x00128D41
		public static DbArithmeticExpression Plus(this DbExpression left, DbExpression right)
		{
			Check.NotNull<DbExpression>(left, "left");
			Check.NotNull<DbExpression>(right, "right");
			return DbExpressionBuilder.CreateArithmetic(DbExpressionKind.Plus, left, right);
		}

		// Token: 0x06005306 RID: 21254 RVA: 0x0012AB64 File Offset: 0x00128D64
		public static DbArithmeticExpression UnaryMinus(this DbExpression argument)
		{
			Check.NotNull<DbExpression>(argument, "argument");
			TypeUsage typeUsage = argument.ResultType;
			if (!TypeSemantics.IsNumericType(typeUsage))
			{
				throw new ArgumentException(Strings.Cqt_Arithmetic_NumericCommonType);
			}
			if (TypeSemantics.IsUnsignedNumericType(argument.ResultType))
			{
				typeUsage = null;
				if (!TypeHelpers.TryGetClosestPromotableType(argument.ResultType, out typeUsage))
				{
					throw new ArgumentException(Strings.Cqt_Arithmetic_InvalidUnsignedTypeForUnaryMinus(argument.ResultType.EdmType.FullName));
				}
			}
			return new DbArithmeticExpression(DbExpressionKind.UnaryMinus, typeUsage, new DbExpressionList(new DbExpression[] { argument }));
		}

		// Token: 0x06005307 RID: 21255 RVA: 0x0012ABE7 File Offset: 0x00128DE7
		public static DbArithmeticExpression Negate(this DbExpression argument)
		{
			return argument.UnaryMinus();
		}

		// Token: 0x06005308 RID: 21256 RVA: 0x0012ABF0 File Offset: 0x00128DF0
		private static DbComparisonExpression CreateComparison(DbExpressionKind kind, DbExpression left, DbExpression right)
		{
			bool flag = true;
			bool flag2 = true;
			if (DbExpressionKind.GreaterThanOrEquals == kind || DbExpressionKind.LessThanOrEquals == kind)
			{
				flag = TypeSemantics.IsEqualComparableTo(left.ResultType, right.ResultType);
				flag2 = TypeSemantics.IsOrderComparableTo(left.ResultType, right.ResultType);
			}
			else if (DbExpressionKind.Equals == kind || DbExpressionKind.NotEquals == kind)
			{
				flag = TypeSemantics.IsEqualComparableTo(left.ResultType, right.ResultType);
			}
			else
			{
				flag2 = TypeSemantics.IsOrderComparableTo(left.ResultType, right.ResultType);
			}
			if (!flag || !flag2)
			{
				throw new ArgumentException(Strings.Cqt_Comparison_ComparableRequired);
			}
			return new DbComparisonExpression(kind, DbExpressionBuilder._booleanType, left, right);
		}

		// Token: 0x06005309 RID: 21257 RVA: 0x0012AC7F File Offset: 0x00128E7F
		public static DbComparisonExpression Equal(this DbExpression left, DbExpression right)
		{
			Check.NotNull<DbExpression>(left, "left");
			Check.NotNull<DbExpression>(right, "right");
			return DbExpressionBuilder.CreateComparison(DbExpressionKind.Equals, left, right);
		}

		// Token: 0x0600530A RID: 21258 RVA: 0x0012ACA2 File Offset: 0x00128EA2
		public static DbComparisonExpression NotEqual(this DbExpression left, DbExpression right)
		{
			Check.NotNull<DbExpression>(left, "left");
			Check.NotNull<DbExpression>(right, "right");
			return DbExpressionBuilder.CreateComparison(DbExpressionKind.NotEquals, left, right);
		}

		// Token: 0x0600530B RID: 21259 RVA: 0x0012ACC5 File Offset: 0x00128EC5
		public static DbComparisonExpression GreaterThan(this DbExpression left, DbExpression right)
		{
			Check.NotNull<DbExpression>(left, "left");
			Check.NotNull<DbExpression>(right, "right");
			return DbExpressionBuilder.CreateComparison(DbExpressionKind.GreaterThan, left, right);
		}

		// Token: 0x0600530C RID: 21260 RVA: 0x0012ACE8 File Offset: 0x00128EE8
		public static DbComparisonExpression LessThan(this DbExpression left, DbExpression right)
		{
			Check.NotNull<DbExpression>(left, "left");
			Check.NotNull<DbExpression>(right, "right");
			return DbExpressionBuilder.CreateComparison(DbExpressionKind.LessThan, left, right);
		}

		// Token: 0x0600530D RID: 21261 RVA: 0x0012AD0B File Offset: 0x00128F0B
		public static DbComparisonExpression GreaterThanOrEqual(this DbExpression left, DbExpression right)
		{
			Check.NotNull<DbExpression>(left, "left");
			Check.NotNull<DbExpression>(right, "right");
			return DbExpressionBuilder.CreateComparison(DbExpressionKind.GreaterThanOrEquals, left, right);
		}

		// Token: 0x0600530E RID: 21262 RVA: 0x0012AD2E File Offset: 0x00128F2E
		public static DbComparisonExpression LessThanOrEqual(this DbExpression left, DbExpression right)
		{
			Check.NotNull<DbExpression>(left, "left");
			Check.NotNull<DbExpression>(right, "right");
			return DbExpressionBuilder.CreateComparison(DbExpressionKind.LessThanOrEquals, left, right);
		}

		// Token: 0x0600530F RID: 21263 RVA: 0x0012AD51 File Offset: 0x00128F51
		public static DbIsNullExpression IsNull(this DbExpression argument)
		{
			Check.NotNull<DbExpression>(argument, "argument");
			DbExpressionBuilder.ValidateIsNull(argument);
			return new DbIsNullExpression(DbExpressionBuilder._booleanType, argument);
		}

		// Token: 0x06005310 RID: 21264 RVA: 0x0012AD70 File Offset: 0x00128F70
		private static void ValidateIsNull(DbExpression argument)
		{
			if (TypeSemantics.IsCollectionType(argument.ResultType))
			{
				throw new ArgumentException(Strings.Cqt_IsNull_CollectionNotAllowed);
			}
			if (!TypeHelpers.IsValidIsNullOpType(argument.ResultType))
			{
				throw new ArgumentException(Strings.Cqt_IsNull_InvalidType);
			}
		}

		// Token: 0x06005311 RID: 21265 RVA: 0x0012ADA4 File Offset: 0x00128FA4
		public static DbLikeExpression Like(this DbExpression argument, DbExpression pattern)
		{
			Check.NotNull<DbExpression>(argument, "argument");
			Check.NotNull<DbExpression>(pattern, "pattern");
			DbExpressionBuilder.ValidateLike(argument, pattern);
			DbExpression dbExpression = pattern.ResultType.Null();
			return new DbLikeExpression(DbExpressionBuilder._booleanType, argument, pattern, dbExpression);
		}

		// Token: 0x06005312 RID: 21266 RVA: 0x0012ADE9 File Offset: 0x00128FE9
		public static DbLikeExpression Like(this DbExpression argument, DbExpression pattern, DbExpression escape)
		{
			Check.NotNull<DbExpression>(argument, "argument");
			Check.NotNull<DbExpression>(pattern, "pattern");
			Check.NotNull<DbExpression>(escape, "escape");
			DbExpressionBuilder.ValidateLike(argument, pattern, escape);
			return new DbLikeExpression(DbExpressionBuilder._booleanType, argument, pattern, escape);
		}

		// Token: 0x06005313 RID: 21267 RVA: 0x0012AE24 File Offset: 0x00129024
		private static void ValidateLike(DbExpression argument, DbExpression pattern, DbExpression escape)
		{
			DbExpressionBuilder.ValidateLike(argument, pattern);
			ArgumentValidation.RequireCompatibleType(escape, PrimitiveTypeKind.String, "escape");
		}

		// Token: 0x06005314 RID: 21268 RVA: 0x0012AE3A File Offset: 0x0012903A
		private static void ValidateLike(DbExpression argument, DbExpression pattern)
		{
			ArgumentValidation.RequireCompatibleType(argument, PrimitiveTypeKind.String, "argument");
			ArgumentValidation.RequireCompatibleType(pattern, PrimitiveTypeKind.String, "pattern");
		}

		// Token: 0x06005315 RID: 21269 RVA: 0x0012AE58 File Offset: 0x00129058
		public static DbCastExpression CastTo(this DbExpression argument, TypeUsage toType)
		{
			Check.NotNull<DbExpression>(argument, "argument");
			Check.NotNull<TypeUsage>(toType, "toType");
			ArgumentValidation.CheckType(toType, "toType");
			if (!TypeSemantics.IsCastAllowed(argument.ResultType, toType))
			{
				throw new ArgumentException(Strings.Cqt_Cast_InvalidCast(argument.ResultType.ToString(), toType.ToString()));
			}
			return new DbCastExpression(toType, argument);
		}

		// Token: 0x06005316 RID: 21270 RVA: 0x0012AEBC File Offset: 0x001290BC
		public static DbTreatExpression TreatAs(this DbExpression argument, TypeUsage treatType)
		{
			Check.NotNull<DbExpression>(argument, "argument");
			Check.NotNull<TypeUsage>(treatType, "treatType");
			ArgumentValidation.CheckType(treatType, "treatType");
			ArgumentValidation.RequirePolymorphicType(treatType);
			if (!TypeSemantics.IsValidPolymorphicCast(argument.ResultType, treatType))
			{
				throw new ArgumentException(Strings.Cqt_General_PolymorphicArgRequired(typeof(DbTreatExpression).Name));
			}
			return new DbTreatExpression(treatType, argument);
		}

		// Token: 0x06005317 RID: 21271 RVA: 0x0012AF24 File Offset: 0x00129124
		public static DbOfTypeExpression OfType(this DbExpression argument, TypeUsage type)
		{
			Check.NotNull<DbExpression>(argument, "argument");
			Check.NotNull<TypeUsage>(type, "type");
			DbExpressionBuilder.ValidateOfType(argument, type);
			TypeUsage typeUsage = DbExpressionBuilder.CreateCollectionResultType(type);
			return new DbOfTypeExpression(DbExpressionKind.OfType, typeUsage, argument, type);
		}

		// Token: 0x06005318 RID: 21272 RVA: 0x0012AF64 File Offset: 0x00129164
		public static DbOfTypeExpression OfTypeOnly(this DbExpression argument, TypeUsage type)
		{
			Check.NotNull<DbExpression>(argument, "argument");
			Check.NotNull<TypeUsage>(type, "type");
			DbExpressionBuilder.ValidateOfType(argument, type);
			TypeUsage typeUsage = DbExpressionBuilder.CreateCollectionResultType(type);
			return new DbOfTypeExpression(DbExpressionKind.OfTypeOnly, typeUsage, argument, type);
		}

		// Token: 0x06005319 RID: 21273 RVA: 0x0012AFA1 File Offset: 0x001291A1
		public static DbIsOfExpression IsOf(this DbExpression argument, TypeUsage type)
		{
			Check.NotNull<DbExpression>(argument, "argument");
			Check.NotNull<TypeUsage>(type, "type");
			DbExpressionBuilder.ValidateIsOf(argument, type);
			return new DbIsOfExpression(DbExpressionKind.IsOf, DbExpressionBuilder._booleanType, argument, type);
		}

		// Token: 0x0600531A RID: 21274 RVA: 0x0012AFD0 File Offset: 0x001291D0
		public static DbIsOfExpression IsOfOnly(this DbExpression argument, TypeUsage type)
		{
			Check.NotNull<DbExpression>(argument, "argument");
			Check.NotNull<TypeUsage>(type, "type");
			DbExpressionBuilder.ValidateIsOf(argument, type);
			return new DbIsOfExpression(DbExpressionKind.IsOfOnly, DbExpressionBuilder._booleanType, argument, type);
		}

		// Token: 0x0600531B RID: 21275 RVA: 0x0012B000 File Offset: 0x00129200
		private static void ValidateOfType(DbExpression argument, TypeUsage type)
		{
			ArgumentValidation.CheckType(type, "type");
			ArgumentValidation.RequirePolymorphicType(type);
			ArgumentValidation.RequireCollectionArgument<DbOfTypeExpression>(argument);
			TypeUsage typeUsage = null;
			if (!TypeHelpers.TryGetCollectionElementType(argument.ResultType, out typeUsage) || !TypeSemantics.IsValidPolymorphicCast(typeUsage, type))
			{
				throw new ArgumentException(Strings.Cqt_General_PolymorphicArgRequired(typeof(DbOfTypeExpression).Name));
			}
		}

		// Token: 0x0600531C RID: 21276 RVA: 0x0012B058 File Offset: 0x00129258
		private static void ValidateIsOf(DbExpression argument, TypeUsage type)
		{
			ArgumentValidation.CheckType(type, "type");
			ArgumentValidation.RequirePolymorphicType(type);
			if (!TypeSemantics.IsValidPolymorphicCast(argument.ResultType, type))
			{
				throw new ArgumentException(Strings.Cqt_General_PolymorphicArgRequired(typeof(DbIsOfExpression).Name));
			}
		}

		// Token: 0x0600531D RID: 21277 RVA: 0x0012B094 File Offset: 0x00129294
		public static DbDerefExpression Deref(this DbExpression argument)
		{
			Check.NotNull<DbExpression>(argument, "argument");
			EntityType entityType;
			if (!TypeHelpers.TryGetRefEntityType(argument.ResultType, out entityType))
			{
				throw new ArgumentException(Strings.Cqt_DeRef_RefRequired, "argument");
			}
			return new DbDerefExpression(TypeUsage.Create(entityType), argument);
		}

		// Token: 0x0600531E RID: 21278 RVA: 0x0012B0D8 File Offset: 0x001292D8
		public static DbEntityRefExpression GetEntityRef(this DbExpression argument)
		{
			Check.NotNull<DbExpression>(argument, "argument");
			EntityType entityType = null;
			if (!TypeHelpers.TryGetEdmType<EntityType>(argument.ResultType, out entityType))
			{
				throw new ArgumentException(Strings.Cqt_GetEntityRef_EntityRequired, "argument");
			}
			return new DbEntityRefExpression(ArgumentValidation.CreateReferenceResultType(entityType), argument);
		}

		// Token: 0x0600531F RID: 21279 RVA: 0x0012B11E File Offset: 0x0012931E
		public static DbRefExpression CreateRef(this EntitySet entitySet, IEnumerable<DbExpression> keyValues)
		{
			Check.NotNull<EntitySet>(entitySet, "entitySet");
			Check.NotNull<IEnumerable<DbExpression>>(keyValues, "keyValues");
			return DbExpressionBuilder.CreateRefExpression(entitySet, keyValues);
		}

		// Token: 0x06005320 RID: 21280 RVA: 0x0012B13F File Offset: 0x0012933F
		public static DbRefExpression CreateRef(this EntitySet entitySet, params DbExpression[] keyValues)
		{
			Check.NotNull<EntitySet>(entitySet, "entitySet");
			Check.NotNull<DbExpression[]>(keyValues, "keyValues");
			return DbExpressionBuilder.CreateRefExpression(entitySet, keyValues);
		}

		// Token: 0x06005321 RID: 21281 RVA: 0x0012B160 File Offset: 0x00129360
		public static DbRefExpression CreateRef(this EntitySet entitySet, EntityType entityType, IEnumerable<DbExpression> keyValues)
		{
			Check.NotNull<EntitySet>(entitySet, "entitySet");
			Check.NotNull<EntityType>(entityType, "entityType");
			Check.NotNull<IEnumerable<DbExpression>>(keyValues, "keyValues");
			return DbExpressionBuilder.CreateRefExpression(entitySet, entityType, keyValues);
		}

		// Token: 0x06005322 RID: 21282 RVA: 0x0012B18E File Offset: 0x0012938E
		public static DbRefExpression CreateRef(this EntitySet entitySet, EntityType entityType, params DbExpression[] keyValues)
		{
			Check.NotNull<EntitySet>(entitySet, "entitySet");
			Check.NotNull<EntityType>(entityType, "entityType");
			Check.NotNull<DbExpression[]>(keyValues, "keyValues");
			return DbExpressionBuilder.CreateRefExpression(entitySet, entityType, keyValues);
		}

		// Token: 0x06005323 RID: 21283 RVA: 0x0012B1BC File Offset: 0x001293BC
		private static DbRefExpression CreateRefExpression(EntitySet entitySet, IEnumerable<DbExpression> keyValues)
		{
			DbExpression dbExpression;
			return new DbRefExpression(ArgumentValidation.ValidateCreateRef(entitySet, entitySet.ElementType, keyValues, out dbExpression), entitySet, dbExpression);
		}

		// Token: 0x06005324 RID: 21284 RVA: 0x0012B1E0 File Offset: 0x001293E0
		private static DbRefExpression CreateRefExpression(EntitySet entitySet, EntityType entityType, IEnumerable<DbExpression> keyValues)
		{
			Check.NotNull<EntitySet>(entitySet, "entitySet");
			Check.NotNull<EntityType>(entityType, "entityType");
			DbExpression dbExpression;
			return new DbRefExpression(ArgumentValidation.ValidateCreateRef(entitySet, entityType, keyValues, out dbExpression), entitySet, dbExpression);
		}

		// Token: 0x06005325 RID: 21285 RVA: 0x0012B216 File Offset: 0x00129416
		public static DbRefExpression RefFromKey(this EntitySet entitySet, DbExpression keyRow)
		{
			Check.NotNull<EntitySet>(entitySet, "entitySet");
			Check.NotNull<DbExpression>(keyRow, "keyRow");
			return new DbRefExpression(ArgumentValidation.ValidateRefFromKey(entitySet, keyRow, entitySet.ElementType), entitySet, keyRow);
		}

		// Token: 0x06005326 RID: 21286 RVA: 0x0012B244 File Offset: 0x00129444
		public static DbRefExpression RefFromKey(this EntitySet entitySet, DbExpression keyRow, EntityType entityType)
		{
			Check.NotNull<EntitySet>(entitySet, "entitySet");
			Check.NotNull<DbExpression>(keyRow, "keyRow");
			Check.NotNull<EntityType>(entityType, "entityType");
			return new DbRefExpression(ArgumentValidation.ValidateRefFromKey(entitySet, keyRow, entityType), entitySet, keyRow);
		}

		// Token: 0x06005327 RID: 21287 RVA: 0x0012B27C File Offset: 0x0012947C
		public static DbRefKeyExpression GetRefKey(this DbExpression argument)
		{
			Check.NotNull<DbExpression>(argument, "argument");
			RefType refType = null;
			if (!TypeHelpers.TryGetEdmType<RefType>(argument.ResultType, out refType))
			{
				throw new ArgumentException(Strings.Cqt_GetRefKey_RefRequired, "argument");
			}
			return new DbRefKeyExpression(TypeUsage.Create(TypeHelpers.CreateKeyRowType(refType.ElementType)), argument);
		}

		// Token: 0x06005328 RID: 21288 RVA: 0x0012B2CC File Offset: 0x001294CC
		public static DbRelationshipNavigationExpression Navigate(this DbExpression navigateFrom, RelationshipEndMember fromEnd, RelationshipEndMember toEnd)
		{
			Check.NotNull<DbExpression>(navigateFrom, "navigateFrom");
			Check.NotNull<RelationshipEndMember>(fromEnd, "fromEnd");
			Check.NotNull<RelationshipEndMember>(toEnd, "toEnd");
			RelationshipType relationshipType;
			return new DbRelationshipNavigationExpression(ArgumentValidation.ValidateNavigate(navigateFrom, fromEnd, toEnd, out relationshipType, false), relationshipType, fromEnd, toEnd, navigateFrom);
		}

		// Token: 0x06005329 RID: 21289 RVA: 0x0012B314 File Offset: 0x00129514
		public static DbRelationshipNavigationExpression Navigate(this RelationshipType type, string fromEndName, string toEndName, DbExpression navigateFrom)
		{
			Check.NotNull<RelationshipType>(type, "type");
			Check.NotNull<string>(fromEndName, "fromEndName");
			Check.NotNull<string>(toEndName, "toEndName");
			Check.NotNull<DbExpression>(navigateFrom, "navigateFrom");
			RelationshipEndMember relationshipEndMember;
			RelationshipEndMember relationshipEndMember2;
			return new DbRelationshipNavigationExpression(ArgumentValidation.ValidateNavigate(navigateFrom, type, fromEndName, toEndName, out relationshipEndMember, out relationshipEndMember2), type, relationshipEndMember, relationshipEndMember2, navigateFrom);
		}

		// Token: 0x0600532A RID: 21290 RVA: 0x0012B368 File Offset: 0x00129568
		public static DbDistinctExpression Distinct(this DbExpression argument)
		{
			Check.NotNull<DbExpression>(argument, "argument");
			ArgumentValidation.RequireCollectionArgument<DbDistinctExpression>(argument);
			if (!TypeHelpers.IsValidDistinctOpType(TypeHelpers.GetEdmType<CollectionType>(argument.ResultType).TypeUsage))
			{
				throw new ArgumentException(Strings.Cqt_Distinct_InvalidCollection, "argument");
			}
			return new DbDistinctExpression(argument.ResultType, argument);
		}

		// Token: 0x0600532B RID: 21291 RVA: 0x0012B3BA File Offset: 0x001295BA
		public static DbElementExpression Element(this DbExpression argument)
		{
			Check.NotNull<DbExpression>(argument, "argument");
			return new DbElementExpression(ArgumentValidation.ValidateElement(argument), argument);
		}

		// Token: 0x0600532C RID: 21292 RVA: 0x0012B3D4 File Offset: 0x001295D4
		public static DbIsEmptyExpression IsEmpty(this DbExpression argument)
		{
			Check.NotNull<DbExpression>(argument, "argument");
			ArgumentValidation.RequireCollectionArgument<DbIsEmptyExpression>(argument);
			return new DbIsEmptyExpression(DbExpressionBuilder._booleanType, argument);
		}

		// Token: 0x0600532D RID: 21293 RVA: 0x0012B3F3 File Offset: 0x001295F3
		public static DbExceptExpression Except(this DbExpression left, DbExpression right)
		{
			Check.NotNull<DbExpression>(left, "left");
			Check.NotNull<DbExpression>(right, "right");
			ArgumentValidation.RequireComparableCollectionArguments<DbExceptExpression>(left, right);
			return new DbExceptExpression(left.ResultType, left, right);
		}

		// Token: 0x0600532E RID: 21294 RVA: 0x0012B422 File Offset: 0x00129622
		public static DbIntersectExpression Intersect(this DbExpression left, DbExpression right)
		{
			Check.NotNull<DbExpression>(left, "left");
			Check.NotNull<DbExpression>(right, "right");
			return new DbIntersectExpression(ArgumentValidation.RequireComparableCollectionArguments<DbIntersectExpression>(left, right), left, right);
		}

		// Token: 0x0600532F RID: 21295 RVA: 0x0012B44A File Offset: 0x0012964A
		public static DbUnionAllExpression UnionAll(this DbExpression left, DbExpression right)
		{
			Check.NotNull<DbExpression>(left, "left");
			Check.NotNull<DbExpression>(right, "right");
			return new DbUnionAllExpression(ArgumentValidation.RequireCollectionArguments<DbUnionAllExpression>(left, right), left, right);
		}

		// Token: 0x06005330 RID: 21296 RVA: 0x0012B474 File Offset: 0x00129674
		public static DbLimitExpression Limit(this DbExpression argument, DbExpression count)
		{
			Check.NotNull<DbExpression>(argument, "argument");
			Check.NotNull<DbExpression>(count, "count");
			ArgumentValidation.RequireCollectionArgument<DbLimitExpression>(argument);
			if (!TypeSemantics.IsIntegerNumericType(count.ResultType))
			{
				throw new ArgumentException(Strings.Cqt_Limit_IntegerRequired, "count");
			}
			if (count.ExpressionKind != DbExpressionKind.Constant && count.ExpressionKind != DbExpressionKind.ParameterReference)
			{
				throw new ArgumentException(Strings.Cqt_Limit_ConstantOrParameterRefRequired, "count");
			}
			if (DbExpressionBuilder.IsConstantNegativeInteger(count))
			{
				throw new ArgumentException(Strings.Cqt_Limit_NonNegativeLimitRequired, "count");
			}
			return new DbLimitExpression(argument.ResultType, argument, count, false);
		}

		// Token: 0x06005331 RID: 21297 RVA: 0x0012B508 File Offset: 0x00129708
		public static DbCaseExpression Case(IEnumerable<DbExpression> whenExpressions, IEnumerable<DbExpression> thenExpressions, DbExpression elseExpression)
		{
			Check.NotNull<IEnumerable<DbExpression>>(whenExpressions, "whenExpressions");
			Check.NotNull<IEnumerable<DbExpression>>(thenExpressions, "thenExpressions");
			Check.NotNull<DbExpression>(elseExpression, "elseExpression");
			DbExpressionList dbExpressionList;
			DbExpressionList dbExpressionList2;
			return new DbCaseExpression(ArgumentValidation.ValidateCase(whenExpressions, thenExpressions, elseExpression, out dbExpressionList, out dbExpressionList2), dbExpressionList, dbExpressionList2, elseExpression);
		}

		// Token: 0x06005332 RID: 21298 RVA: 0x0012B54D File Offset: 0x0012974D
		public static DbFunctionExpression Invoke(this EdmFunction function, IEnumerable<DbExpression> arguments)
		{
			Check.NotNull<EdmFunction>(function, "function");
			return DbExpressionBuilder.InvokeFunction(function, arguments);
		}

		// Token: 0x06005333 RID: 21299 RVA: 0x0012B562 File Offset: 0x00129762
		public static DbFunctionExpression Invoke(this EdmFunction function, params DbExpression[] arguments)
		{
			Check.NotNull<EdmFunction>(function, "function");
			return DbExpressionBuilder.InvokeFunction(function, arguments);
		}

		// Token: 0x06005334 RID: 21300 RVA: 0x0012B578 File Offset: 0x00129778
		private static DbFunctionExpression InvokeFunction(EdmFunction function, IEnumerable<DbExpression> arguments)
		{
			DbExpressionList dbExpressionList;
			return new DbFunctionExpression(ArgumentValidation.ValidateFunction(function, arguments, out dbExpressionList), function, dbExpressionList);
		}

		// Token: 0x06005335 RID: 21301 RVA: 0x0012B595 File Offset: 0x00129795
		public static DbLambdaExpression Invoke(this DbLambda lambda, IEnumerable<DbExpression> arguments)
		{
			Check.NotNull<DbLambda>(lambda, "lambda");
			Check.NotNull<IEnumerable<DbExpression>>(arguments, "arguments");
			return DbExpressionBuilder.InvokeLambda(lambda, arguments);
		}

		// Token: 0x06005336 RID: 21302 RVA: 0x0012B5B6 File Offset: 0x001297B6
		public static DbLambdaExpression Invoke(this DbLambda lambda, params DbExpression[] arguments)
		{
			Check.NotNull<DbLambda>(lambda, "lambda");
			Check.NotNull<DbExpression[]>(arguments, "arguments");
			return DbExpressionBuilder.InvokeLambda(lambda, arguments);
		}

		// Token: 0x06005337 RID: 21303 RVA: 0x0012B5D8 File Offset: 0x001297D8
		private static DbLambdaExpression InvokeLambda(DbLambda lambda, IEnumerable<DbExpression> arguments)
		{
			DbExpressionList dbExpressionList;
			return new DbLambdaExpression(ArgumentValidation.ValidateInvoke(lambda, arguments, out dbExpressionList), lambda, dbExpressionList);
		}

		// Token: 0x06005338 RID: 21304 RVA: 0x0012B5F5 File Offset: 0x001297F5
		public static DbNewInstanceExpression New(this TypeUsage instanceType, IEnumerable<DbExpression> arguments)
		{
			Check.NotNull<TypeUsage>(instanceType, "instanceType");
			return DbExpressionBuilder.NewInstance(instanceType, arguments);
		}

		// Token: 0x06005339 RID: 21305 RVA: 0x0012B60A File Offset: 0x0012980A
		public static DbNewInstanceExpression New(this TypeUsage instanceType, params DbExpression[] arguments)
		{
			Check.NotNull<TypeUsage>(instanceType, "instanceType");
			return DbExpressionBuilder.NewInstance(instanceType, arguments);
		}

		// Token: 0x0600533A RID: 21306 RVA: 0x0012B620 File Offset: 0x00129820
		private static DbNewInstanceExpression NewInstance(TypeUsage instanceType, IEnumerable<DbExpression> arguments)
		{
			DbExpressionList dbExpressionList;
			return new DbNewInstanceExpression(ArgumentValidation.ValidateNew(instanceType, arguments, out dbExpressionList), dbExpressionList);
		}

		// Token: 0x0600533B RID: 21307 RVA: 0x0012B63C File Offset: 0x0012983C
		public static DbNewInstanceExpression NewCollection(IEnumerable<DbExpression> elements)
		{
			return DbExpressionBuilder.CreateNewCollection(elements);
		}

		// Token: 0x0600533C RID: 21308 RVA: 0x0012B644 File Offset: 0x00129844
		public static DbNewInstanceExpression NewCollection(params DbExpression[] elements)
		{
			Check.NotNull<DbExpression[]>(elements, "elements");
			return DbExpressionBuilder.CreateNewCollection(elements);
		}

		// Token: 0x0600533D RID: 21309 RVA: 0x0012B658 File Offset: 0x00129858
		private static DbNewInstanceExpression CreateNewCollection(IEnumerable<DbExpression> elements)
		{
			TypeUsage commonElementType = null;
			DbExpressionList dbExpressionList = ArgumentValidation.CreateExpressionList(elements, "elements", delegate(DbExpression exp, int idx)
			{
				if (commonElementType == null)
				{
					commonElementType = exp.ResultType;
				}
				else
				{
					commonElementType = TypeSemantics.GetCommonType(commonElementType, exp.ResultType);
				}
				if (commonElementType == null)
				{
					throw new ArgumentException(Strings.Cqt_Factory_NewCollectionInvalidCommonType, "collectionElements");
				}
			});
			return new DbNewInstanceExpression(DbExpressionBuilder.CreateCollectionResultType(commonElementType), dbExpressionList);
		}

		// Token: 0x0600533E RID: 21310 RVA: 0x0012B69C File Offset: 0x0012989C
		public static DbNewInstanceExpression NewEmptyCollection(this TypeUsage collectionType)
		{
			Check.NotNull<TypeUsage>(collectionType, "collectionType");
			DbExpressionList dbExpressionList;
			return new DbNewInstanceExpression(ArgumentValidation.ValidateNewEmptyCollection(collectionType, out dbExpressionList), dbExpressionList);
		}

		// Token: 0x0600533F RID: 21311 RVA: 0x0012B6C4 File Offset: 0x001298C4
		public static DbNewInstanceExpression NewRow(IEnumerable<KeyValuePair<string, DbExpression>> columnValues)
		{
			Check.NotNull<IEnumerable<KeyValuePair<string, DbExpression>>>(columnValues, "columnValues");
			DbExpressionList dbExpressionList;
			return new DbNewInstanceExpression(ArgumentValidation.ValidateNewRow(columnValues, out dbExpressionList), dbExpressionList);
		}

		// Token: 0x06005340 RID: 21312 RVA: 0x0012B6EB File Offset: 0x001298EB
		public static DbPropertyExpression Property(this DbExpression instance, EdmProperty propertyMetadata)
		{
			Check.NotNull<DbExpression>(instance, "instance");
			Check.NotNull<EdmProperty>(propertyMetadata, "propertyMetadata");
			return DbExpressionBuilder.PropertyFromMember(instance, propertyMetadata, "propertyMetadata");
		}

		// Token: 0x06005341 RID: 21313 RVA: 0x0012B711 File Offset: 0x00129911
		public static DbPropertyExpression Property(this DbExpression instance, NavigationProperty navigationProperty)
		{
			Check.NotNull<DbExpression>(instance, "instance");
			Check.NotNull<NavigationProperty>(navigationProperty, "navigationProperty");
			return DbExpressionBuilder.PropertyFromMember(instance, navigationProperty, "navigationProperty");
		}

		// Token: 0x06005342 RID: 21314 RVA: 0x0012B737 File Offset: 0x00129937
		public static DbPropertyExpression Property(this DbExpression instance, RelationshipEndMember relationshipEnd)
		{
			Check.NotNull<DbExpression>(instance, "instance");
			Check.NotNull<RelationshipEndMember>(relationshipEnd, "relationshipEnd");
			return DbExpressionBuilder.PropertyFromMember(instance, relationshipEnd, "relationshipEnd");
		}

		// Token: 0x06005343 RID: 21315 RVA: 0x0012B75D File Offset: 0x0012995D
		public static DbPropertyExpression Property(this DbExpression instance, string propertyName)
		{
			return DbExpressionBuilder.PropertyByName(instance, propertyName, false);
		}

		// Token: 0x06005344 RID: 21316 RVA: 0x0012B768 File Offset: 0x00129968
		private static DbPropertyExpression PropertyFromMember(DbExpression instance, EdmMember property, string propertyArgumentName)
		{
			ArgumentValidation.CheckMember(property, propertyArgumentName);
			if (instance == null)
			{
				throw new ArgumentException(Strings.Cqt_Property_InstanceRequiredForInstance, "instance");
			}
			TypeUsage typeUsage = TypeUsage.Create(property.DeclaringType);
			ArgumentValidation.RequireCompatibleType(instance, typeUsage, "instance");
			return new DbPropertyExpression(Helper.GetModelTypeUsage(property), property, instance);
		}

		// Token: 0x06005345 RID: 21317 RVA: 0x0012B7B4 File Offset: 0x001299B4
		private static DbPropertyExpression PropertyByName(DbExpression instance, string propertyName, bool ignoreCase)
		{
			Check.NotNull<DbExpression>(instance, "instance");
			Check.NotNull<string>(propertyName, "propertyName");
			EdmMember edmMember;
			return new DbPropertyExpression(ArgumentValidation.ValidateProperty(instance, propertyName, ignoreCase, out edmMember), edmMember, instance);
		}

		// Token: 0x06005346 RID: 21318 RVA: 0x0012B7EA File Offset: 0x001299EA
		public static DbSetClause SetClause(DbExpression property, DbExpression value)
		{
			Check.NotNull<DbExpression>(property, "property");
			Check.NotNull<DbExpression>(value, "value");
			return new DbSetClause(property, value);
		}

		// Token: 0x06005347 RID: 21319 RVA: 0x0012B80B File Offset: 0x00129A0B
		private static string ExtractAlias(MethodInfo method)
		{
			return DbExpressionBuilder.ExtractAliases(method)[0];
		}

		// Token: 0x06005348 RID: 21320 RVA: 0x0012B818 File Offset: 0x00129A18
		internal static string[] ExtractAliases(MethodInfo method)
		{
			ParameterInfo[] parameters = method.GetParameters();
			int num;
			int num2;
			if (method.IsStatic && "System.Runtime.CompilerServices.Closure" == parameters[0].ParameterType.FullName)
			{
				num = 1;
				num2 = parameters.Length - 1;
			}
			else
			{
				num = 0;
				num2 = parameters.Length;
			}
			string[] array = new string[num2];
			bool flag = parameters.Skip(num).Any((ParameterInfo p) => p.Name == null);
			for (int i = num; i < parameters.Length; i++)
			{
				array[i - num] = (flag ? DbExpressionBuilder._bindingAliases.Next() : parameters[i].Name);
			}
			return array;
		}

		// Token: 0x06005349 RID: 21321 RVA: 0x0012B8C4 File Offset: 0x00129AC4
		private static DbExpressionBinding ConvertToBinding<TResult>(DbExpression source, Func<DbExpression, TResult> argument, out TResult argumentResult)
		{
			string text = DbExpressionBuilder.ExtractAlias(argument.Method);
			DbExpressionBinding dbExpressionBinding = source.BindAs(text);
			argumentResult = argument(dbExpressionBinding.Variable);
			return dbExpressionBinding;
		}

		// Token: 0x0600534A RID: 21322 RVA: 0x0012B8F8 File Offset: 0x00129AF8
		private static DbExpressionBinding[] ConvertToBinding(DbExpression left, DbExpression right, Func<DbExpression, DbExpression, DbExpression> argument, out DbExpression argumentExp)
		{
			string[] array = DbExpressionBuilder.ExtractAliases(argument.Method);
			DbExpressionBinding dbExpressionBinding = left.BindAs(array[0]);
			DbExpressionBinding dbExpressionBinding2 = right.BindAs(array[1]);
			argumentExp = argument(dbExpressionBinding.Variable, dbExpressionBinding2.Variable);
			return new DbExpressionBinding[] { dbExpressionBinding, dbExpressionBinding2 };
		}

		// Token: 0x0600534B RID: 21323 RVA: 0x0012B948 File Offset: 0x00129B48
		internal static List<KeyValuePair<string, TRequired>> TryGetAnonymousTypeValues<TInstance, TRequired>(object instance)
		{
			IEnumerable<PropertyInfo> instanceProperties = typeof(TInstance).GetInstanceProperties();
			if (!(typeof(TInstance).BaseType() != typeof(object)))
			{
				if (!instanceProperties.Any((PropertyInfo p) => !p.IsPublic()))
				{
					List<KeyValuePair<string, TRequired>> list = null;
					foreach (PropertyInfo propertyInfo in instanceProperties.Where((PropertyInfo p) => p.IsPublic()))
					{
						if (!propertyInfo.CanRead || !typeof(TRequired).IsAssignableFrom(propertyInfo.PropertyType))
						{
							return null;
						}
						if (list == null)
						{
							list = new List<KeyValuePair<string, TRequired>>();
						}
						list.Add(new KeyValuePair<string, TRequired>(propertyInfo.Name, (TRequired)((object)propertyInfo.GetValue(instance, null))));
					}
					return list;
				}
			}
			return null;
		}

		// Token: 0x0600534C RID: 21324 RVA: 0x0012BA5C File Offset: 0x00129C5C
		private static bool TryResolveToConstant(Type type, object value, out DbExpression constantOrNullExpression)
		{
			constantOrNullExpression = null;
			Type type2 = type;
			if (type.IsGenericType() && typeof(Nullable<>).Equals(type.GetGenericTypeDefinition()))
			{
				type2 = type.GetGenericArguments()[0];
			}
			PrimitiveTypeKind primitiveTypeKind;
			if (ClrProviderManifest.TryGetPrimitiveTypeKind(type2, out primitiveTypeKind))
			{
				TypeUsage literalTypeUsage = TypeHelpers.GetLiteralTypeUsage(primitiveTypeKind);
				if (value == null)
				{
					constantOrNullExpression = literalTypeUsage.Null();
				}
				else
				{
					constantOrNullExpression = literalTypeUsage.Constant(value);
				}
			}
			return constantOrNullExpression != null;
		}

		// Token: 0x0600534D RID: 21325 RVA: 0x0012BAC4 File Offset: 0x00129CC4
		private static DbExpression ResolveToExpression<TArgument>(TArgument argument)
		{
			object obj = argument;
			DbExpression dbExpression;
			if (DbExpressionBuilder.TryResolveToConstant(typeof(TArgument), obj, out dbExpression))
			{
				return dbExpression;
			}
			if (obj == null)
			{
				return null;
			}
			if (typeof(DbExpression).IsAssignableFrom(typeof(TArgument)))
			{
				return (DbExpression)obj;
			}
			if (typeof(Row).Equals(typeof(TArgument)))
			{
				return ((Row)obj).ToExpression();
			}
			List<KeyValuePair<string, DbExpression>> list = DbExpressionBuilder.TryGetAnonymousTypeValues<TArgument, DbExpression>(obj);
			if (list != null)
			{
				return DbExpressionBuilder.NewRow(list);
			}
			throw new NotSupportedException(Strings.Cqt_Factory_MethodResultTypeNotSupported(typeof(TArgument).FullName));
		}

		// Token: 0x0600534E RID: 21326 RVA: 0x0012BB68 File Offset: 0x00129D68
		private static DbApplyExpression CreateApply(DbExpression source, Func<DbExpression, KeyValuePair<string, DbExpression>> apply, Func<DbExpressionBinding, DbExpressionBinding, DbApplyExpression> resultBuilder)
		{
			KeyValuePair<string, DbExpression> keyValuePair;
			DbExpressionBinding dbExpressionBinding = DbExpressionBuilder.ConvertToBinding<KeyValuePair<string, DbExpression>>(source, apply, out keyValuePair);
			DbExpressionBinding dbExpressionBinding2 = keyValuePair.Value.BindAs(keyValuePair.Key);
			return resultBuilder(dbExpressionBinding, dbExpressionBinding2);
		}

		// Token: 0x0600534F RID: 21327 RVA: 0x0012BB9C File Offset: 0x00129D9C
		public static DbQuantifierExpression All(this DbExpression source, Func<DbExpression, DbExpression> predicate)
		{
			Check.NotNull<DbExpression>(source, "source");
			Check.NotNull<Func<DbExpression, DbExpression>>(predicate, "predicate");
			DbExpression dbExpression;
			return DbExpressionBuilder.ConvertToBinding<DbExpression>(source, predicate, out dbExpression).All(dbExpression);
		}

		// Token: 0x06005350 RID: 21328 RVA: 0x0012BBD0 File Offset: 0x00129DD0
		public static DbExpression Any(this DbExpression source)
		{
			return source.Exists();
		}

		// Token: 0x06005351 RID: 21329 RVA: 0x0012BBD8 File Offset: 0x00129DD8
		public static DbExpression Exists(this DbExpression argument)
		{
			return argument.IsEmpty().Not();
		}

		// Token: 0x06005352 RID: 21330 RVA: 0x0012BBE8 File Offset: 0x00129DE8
		public static DbQuantifierExpression Any(this DbExpression source, Func<DbExpression, DbExpression> predicate)
		{
			Check.NotNull<DbExpression>(source, "source");
			Check.NotNull<Func<DbExpression, DbExpression>>(predicate, "predicate");
			DbExpression dbExpression;
			return DbExpressionBuilder.ConvertToBinding<DbExpression>(source, predicate, out dbExpression).Any(dbExpression);
		}

		// Token: 0x06005353 RID: 21331 RVA: 0x0012BC1C File Offset: 0x00129E1C
		public static DbApplyExpression CrossApply(this DbExpression source, Func<DbExpression, KeyValuePair<string, DbExpression>> apply)
		{
			Check.NotNull<DbExpression>(source, "source");
			Check.NotNull<Func<DbExpression, KeyValuePair<string, DbExpression>>>(apply, "apply");
			return DbExpressionBuilder.CreateApply(source, apply, new Func<DbExpressionBinding, DbExpressionBinding, DbApplyExpression>(DbExpressionBuilder.CrossApply));
		}

		// Token: 0x06005354 RID: 21332 RVA: 0x0012BC49 File Offset: 0x00129E49
		public static DbApplyExpression OuterApply(this DbExpression source, Func<DbExpression, KeyValuePair<string, DbExpression>> apply)
		{
			Check.NotNull<DbExpression>(source, "source");
			Check.NotNull<Func<DbExpression, KeyValuePair<string, DbExpression>>>(apply, "apply");
			return DbExpressionBuilder.CreateApply(source, apply, new Func<DbExpressionBinding, DbExpressionBinding, DbApplyExpression>(DbExpressionBuilder.OuterApply));
		}

		// Token: 0x06005355 RID: 21333 RVA: 0x0012BC78 File Offset: 0x00129E78
		public static DbJoinExpression FullOuterJoin(this DbExpression left, DbExpression right, Func<DbExpression, DbExpression, DbExpression> joinCondition)
		{
			Check.NotNull<DbExpression>(left, "left");
			Check.NotNull<DbExpression>(right, "right");
			Check.NotNull<Func<DbExpression, DbExpression, DbExpression>>(joinCondition, "joinCondition");
			DbExpression dbExpression;
			DbExpressionBinding[] array = DbExpressionBuilder.ConvertToBinding(left, right, joinCondition, out dbExpression);
			return array[0].FullOuterJoin(array[1], dbExpression);
		}

		// Token: 0x06005356 RID: 21334 RVA: 0x0012BCC0 File Offset: 0x00129EC0
		public static DbJoinExpression InnerJoin(this DbExpression left, DbExpression right, Func<DbExpression, DbExpression, DbExpression> joinCondition)
		{
			Check.NotNull<DbExpression>(left, "left");
			Check.NotNull<DbExpression>(right, "right");
			Check.NotNull<Func<DbExpression, DbExpression, DbExpression>>(joinCondition, "joinCondition");
			DbExpression dbExpression;
			DbExpressionBinding[] array = DbExpressionBuilder.ConvertToBinding(left, right, joinCondition, out dbExpression);
			return array[0].InnerJoin(array[1], dbExpression);
		}

		// Token: 0x06005357 RID: 21335 RVA: 0x0012BD08 File Offset: 0x00129F08
		public static DbJoinExpression LeftOuterJoin(this DbExpression left, DbExpression right, Func<DbExpression, DbExpression, DbExpression> joinCondition)
		{
			Check.NotNull<DbExpression>(left, "left");
			Check.NotNull<DbExpression>(right, "right");
			Check.NotNull<Func<DbExpression, DbExpression, DbExpression>>(joinCondition, "joinCondition");
			DbExpression dbExpression;
			DbExpressionBinding[] array = DbExpressionBuilder.ConvertToBinding(left, right, joinCondition, out dbExpression);
			return array[0].LeftOuterJoin(array[1], dbExpression);
		}

		// Token: 0x06005358 RID: 21336 RVA: 0x0012BD50 File Offset: 0x00129F50
		public static DbJoinExpression Join(this DbExpression outer, DbExpression inner, Func<DbExpression, DbExpression> outerKey, Func<DbExpression, DbExpression> innerKey)
		{
			Check.NotNull<DbExpression>(outer, "outer");
			Check.NotNull<DbExpression>(inner, "inner");
			Check.NotNull<Func<DbExpression, DbExpression>>(outerKey, "outerKey");
			Check.NotNull<Func<DbExpression, DbExpression>>(innerKey, "innerKey");
			DbExpression dbExpression;
			DbExpressionBinding dbExpressionBinding = DbExpressionBuilder.ConvertToBinding<DbExpression>(outer, outerKey, out dbExpression);
			DbExpression dbExpression2;
			DbExpressionBinding dbExpressionBinding2 = DbExpressionBuilder.ConvertToBinding<DbExpression>(inner, innerKey, out dbExpression2);
			DbExpression dbExpression3 = dbExpression.Equal(dbExpression2);
			return dbExpressionBinding.InnerJoin(dbExpressionBinding2, dbExpression3);
		}

		// Token: 0x06005359 RID: 21337 RVA: 0x0012BDB0 File Offset: 0x00129FB0
		public static DbProjectExpression Join<TSelector>(this DbExpression outer, DbExpression inner, Func<DbExpression, DbExpression> outerKey, Func<DbExpression, DbExpression> innerKey, Func<DbExpression, DbExpression, TSelector> selector)
		{
			Check.NotNull<Func<DbExpression, DbExpression, TSelector>>(selector, "selector");
			DbJoinExpression dbJoinExpression = outer.Join(inner, outerKey, innerKey);
			DbExpressionBinding dbExpressionBinding = dbJoinExpression.Bind();
			DbExpression dbExpression = dbExpressionBinding.Variable.Property(dbJoinExpression.Left.VariableName);
			DbExpression dbExpression2 = dbExpressionBinding.Variable.Property(dbJoinExpression.Right.VariableName);
			DbExpression dbExpression3 = DbExpressionBuilder.ResolveToExpression<TSelector>(selector(dbExpression, dbExpression2));
			return dbExpressionBinding.Project(dbExpression3);
		}

		// Token: 0x0600535A RID: 21338 RVA: 0x0012BE20 File Offset: 0x0012A020
		public static DbSortExpression OrderBy(this DbExpression source, Func<DbExpression, DbExpression> sortKey)
		{
			Check.NotNull<DbExpression>(source, "source");
			Check.NotNull<Func<DbExpression, DbExpression>>(sortKey, "sortKey");
			DbExpression dbExpression;
			DbExpressionBinding dbExpressionBinding = DbExpressionBuilder.ConvertToBinding<DbExpression>(source, sortKey, out dbExpression);
			DbSortClause dbSortClause = dbExpression.ToSortClause();
			return dbExpressionBinding.Sort(new DbSortClause[] { dbSortClause });
		}

		// Token: 0x0600535B RID: 21339 RVA: 0x0012BE64 File Offset: 0x0012A064
		public static DbSortExpression OrderBy(this DbExpression source, Func<DbExpression, DbExpression> sortKey, string collation)
		{
			Check.NotNull<DbExpression>(source, "source");
			Check.NotNull<Func<DbExpression, DbExpression>>(sortKey, "sortKey");
			DbExpression dbExpression;
			DbExpressionBinding dbExpressionBinding = DbExpressionBuilder.ConvertToBinding<DbExpression>(source, sortKey, out dbExpression);
			DbSortClause dbSortClause = dbExpression.ToSortClause(collation);
			return dbExpressionBinding.Sort(new DbSortClause[] { dbSortClause });
		}

		// Token: 0x0600535C RID: 21340 RVA: 0x0012BEAC File Offset: 0x0012A0AC
		public static DbSortExpression OrderByDescending(this DbExpression source, Func<DbExpression, DbExpression> sortKey)
		{
			Check.NotNull<DbExpression>(source, "source");
			Check.NotNull<Func<DbExpression, DbExpression>>(sortKey, "sortKey");
			DbExpression dbExpression;
			DbExpressionBinding dbExpressionBinding = DbExpressionBuilder.ConvertToBinding<DbExpression>(source, sortKey, out dbExpression);
			DbSortClause dbSortClause = dbExpression.ToSortClauseDescending();
			return dbExpressionBinding.Sort(new DbSortClause[] { dbSortClause });
		}

		// Token: 0x0600535D RID: 21341 RVA: 0x0012BEF0 File Offset: 0x0012A0F0
		public static DbSortExpression OrderByDescending(this DbExpression source, Func<DbExpression, DbExpression> sortKey, string collation)
		{
			Check.NotNull<DbExpression>(source, "source");
			Check.NotNull<Func<DbExpression, DbExpression>>(sortKey, "sortKey");
			DbExpression dbExpression;
			DbExpressionBinding dbExpressionBinding = DbExpressionBuilder.ConvertToBinding<DbExpression>(source, sortKey, out dbExpression);
			DbSortClause dbSortClause = dbExpression.ToSortClauseDescending(collation);
			return dbExpressionBinding.Sort(new DbSortClause[] { dbSortClause });
		}

		// Token: 0x0600535E RID: 21342 RVA: 0x0012BF38 File Offset: 0x0012A138
		public static DbProjectExpression Select<TProjection>(this DbExpression source, Func<DbExpression, TProjection> projection)
		{
			Check.NotNull<DbExpression>(source, "source");
			Check.NotNull<Func<DbExpression, TProjection>>(projection, "projection");
			TProjection tprojection;
			DbExpressionBinding dbExpressionBinding = DbExpressionBuilder.ConvertToBinding<TProjection>(source, projection, out tprojection);
			DbExpression dbExpression = DbExpressionBuilder.ResolveToExpression<TProjection>(tprojection);
			return dbExpressionBinding.Project(dbExpression);
		}

		// Token: 0x0600535F RID: 21343 RVA: 0x0012BF74 File Offset: 0x0012A174
		public static DbProjectExpression SelectMany(this DbExpression source, Func<DbExpression, DbExpression> apply)
		{
			Check.NotNull<DbExpression>(source, "source");
			Check.NotNull<Func<DbExpression, DbExpression>>(apply, "apply");
			DbExpression dbExpression;
			DbExpressionBinding dbExpressionBinding = DbExpressionBuilder.ConvertToBinding<DbExpression>(source, apply, out dbExpression);
			DbExpressionBinding dbExpressionBinding2 = dbExpression.Bind();
			DbExpressionBinding dbExpressionBinding3 = dbExpressionBinding.CrossApply(dbExpressionBinding2).Bind();
			return dbExpressionBinding3.Project(dbExpressionBinding3.Variable.Property(dbExpressionBinding2.VariableName));
		}

		// Token: 0x06005360 RID: 21344 RVA: 0x0012BFCC File Offset: 0x0012A1CC
		public static DbProjectExpression SelectMany<TSelector>(this DbExpression source, Func<DbExpression, DbExpression> apply, Func<DbExpression, DbExpression, TSelector> selector)
		{
			Check.NotNull<DbExpression>(source, "source");
			Check.NotNull<Func<DbExpression, DbExpression>>(apply, "apply");
			Check.NotNull<Func<DbExpression, DbExpression, TSelector>>(selector, "selector");
			DbExpression dbExpression;
			DbExpressionBinding dbExpressionBinding = DbExpressionBuilder.ConvertToBinding<DbExpression>(source, apply, out dbExpression);
			DbExpressionBinding dbExpressionBinding2 = dbExpression.Bind();
			DbExpressionBinding dbExpressionBinding3 = dbExpressionBinding.CrossApply(dbExpressionBinding2).Bind();
			DbExpression dbExpression2 = dbExpressionBinding3.Variable.Property(dbExpressionBinding.VariableName);
			DbExpression dbExpression3 = dbExpressionBinding3.Variable.Property(dbExpressionBinding2.VariableName);
			DbExpression dbExpression4 = DbExpressionBuilder.ResolveToExpression<TSelector>(selector(dbExpression2, dbExpression3));
			return dbExpressionBinding3.Project(dbExpression4);
		}

		// Token: 0x06005361 RID: 21345 RVA: 0x0012C056 File Offset: 0x0012A256
		public static DbSkipExpression Skip(this DbSortExpression argument, DbExpression count)
		{
			Check.NotNull<DbSortExpression>(argument, "argument");
			return argument.Input.Skip(argument.SortOrder, count);
		}

		// Token: 0x06005362 RID: 21346 RVA: 0x0012C076 File Offset: 0x0012A276
		public static DbLimitExpression Take(this DbExpression argument, DbExpression count)
		{
			Check.NotNull<DbExpression>(argument, "argument");
			Check.NotNull<DbExpression>(count, "count");
			return argument.Limit(count);
		}

		// Token: 0x06005363 RID: 21347 RVA: 0x0012C098 File Offset: 0x0012A298
		private static DbSortExpression CreateThenBy(DbSortExpression source, Func<DbExpression, DbExpression> sortKey, bool ascending, string collation, bool useCollation)
		{
			DbExpression dbExpression = sortKey(source.Input.Variable);
			DbSortClause dbSortClause;
			if (useCollation)
			{
				dbSortClause = (ascending ? dbExpression.ToSortClause(collation) : dbExpression.ToSortClauseDescending(collation));
			}
			else
			{
				dbSortClause = (ascending ? dbExpression.ToSortClause() : dbExpression.ToSortClauseDescending());
			}
			List<DbSortClause> list = new List<DbSortClause>(source.SortOrder.Count + 1);
			list.AddRange(source.SortOrder);
			list.Add(dbSortClause);
			return source.Input.Sort(list);
		}

		// Token: 0x06005364 RID: 21348 RVA: 0x0012C115 File Offset: 0x0012A315
		public static DbSortExpression ThenBy(this DbSortExpression source, Func<DbExpression, DbExpression> sortKey)
		{
			Check.NotNull<DbSortExpression>(source, "source");
			Check.NotNull<Func<DbExpression, DbExpression>>(sortKey, "sortKey");
			return DbExpressionBuilder.CreateThenBy(source, sortKey, true, null, false);
		}

		// Token: 0x06005365 RID: 21349 RVA: 0x0012C139 File Offset: 0x0012A339
		public static DbSortExpression ThenBy(this DbSortExpression source, Func<DbExpression, DbExpression> sortKey, string collation)
		{
			Check.NotNull<DbSortExpression>(source, "source");
			Check.NotNull<Func<DbExpression, DbExpression>>(sortKey, "sortKey");
			return DbExpressionBuilder.CreateThenBy(source, sortKey, true, collation, true);
		}

		// Token: 0x06005366 RID: 21350 RVA: 0x0012C15D File Offset: 0x0012A35D
		public static DbSortExpression ThenByDescending(this DbSortExpression source, Func<DbExpression, DbExpression> sortKey)
		{
			Check.NotNull<DbSortExpression>(source, "source");
			Check.NotNull<Func<DbExpression, DbExpression>>(sortKey, "sortKey");
			return DbExpressionBuilder.CreateThenBy(source, sortKey, false, null, false);
		}

		// Token: 0x06005367 RID: 21351 RVA: 0x0012C181 File Offset: 0x0012A381
		public static DbSortExpression ThenByDescending(this DbSortExpression source, Func<DbExpression, DbExpression> sortKey, string collation)
		{
			Check.NotNull<DbSortExpression>(source, "source");
			Check.NotNull<Func<DbExpression, DbExpression>>(sortKey, "sortKey");
			return DbExpressionBuilder.CreateThenBy(source, sortKey, false, collation, true);
		}

		// Token: 0x06005368 RID: 21352 RVA: 0x0012C1A8 File Offset: 0x0012A3A8
		public static DbFilterExpression Where(this DbExpression source, Func<DbExpression, DbExpression> predicate)
		{
			Check.NotNull<DbExpression>(source, "source");
			Check.NotNull<Func<DbExpression, DbExpression>>(predicate, "predicate");
			DbExpression dbExpression;
			return DbExpressionBuilder.ConvertToBinding<DbExpression>(source, predicate, out dbExpression).Filter(dbExpression);
		}

		// Token: 0x06005369 RID: 21353 RVA: 0x0012C1DC File Offset: 0x0012A3DC
		public static DbExpression Union(this DbExpression left, DbExpression right)
		{
			return left.UnionAll(right).Distinct();
		}

		// Token: 0x17001003 RID: 4099
		// (get) Token: 0x0600536A RID: 21354 RVA: 0x0012C1EA File Offset: 0x0012A3EA
		internal static AliasGenerator AliasGenerator
		{
			get
			{
				return DbExpressionBuilder._bindingAliases;
			}
		}

		// Token: 0x0600536B RID: 21355 RVA: 0x0012C1F4 File Offset: 0x0012A3F4
		internal static DbNullExpression CreatePrimitiveNullExpression(PrimitiveTypeKind primitiveType)
		{
			switch (primitiveType)
			{
			case PrimitiveTypeKind.Binary:
				return DbExpressionBuilder._binaryNull;
			case PrimitiveTypeKind.Boolean:
				return DbExpressionBuilder._boolNull;
			case PrimitiveTypeKind.Byte:
				return DbExpressionBuilder._byteNull;
			case PrimitiveTypeKind.DateTime:
				return DbExpressionBuilder._dateTimeNull;
			case PrimitiveTypeKind.Decimal:
				return DbExpressionBuilder._decimalNull;
			case PrimitiveTypeKind.Double:
				return DbExpressionBuilder._doubleNull;
			case PrimitiveTypeKind.Guid:
				return DbExpressionBuilder._guidNull;
			case PrimitiveTypeKind.Single:
				return DbExpressionBuilder._singleNull;
			case PrimitiveTypeKind.SByte:
				return DbExpressionBuilder._sbyteNull;
			case PrimitiveTypeKind.Int16:
				return DbExpressionBuilder._int16Null;
			case PrimitiveTypeKind.Int32:
				return DbExpressionBuilder._int32Null;
			case PrimitiveTypeKind.Int64:
				return DbExpressionBuilder._int64Null;
			case PrimitiveTypeKind.String:
				return DbExpressionBuilder._stringNull;
			case PrimitiveTypeKind.Time:
				return DbExpressionBuilder._timeNull;
			case PrimitiveTypeKind.DateTimeOffset:
				return DbExpressionBuilder._dateTimeOffsetNull;
			case PrimitiveTypeKind.Geometry:
				return DbExpressionBuilder._geometryNull;
			case PrimitiveTypeKind.Geography:
				return DbExpressionBuilder._geographyNull;
			default:
			{
				string name = typeof(PrimitiveTypeKind).Name;
				int num = (int)primitiveType;
				throw new ArgumentOutOfRangeException(name, Strings.ADP_InvalidEnumerationValue(name, num.ToString(CultureInfo.InvariantCulture)));
			}
			}
		}

		// Token: 0x0600536C RID: 21356 RVA: 0x0012C2DC File Offset: 0x0012A4DC
		internal static DbApplyExpression CreateApplyExpressionByKind(DbExpressionKind applyKind, DbExpressionBinding input, DbExpressionBinding apply)
		{
			if (applyKind == DbExpressionKind.CrossApply)
			{
				return input.CrossApply(apply);
			}
			if (applyKind != DbExpressionKind.OuterApply)
			{
				string name = typeof(DbExpressionKind).Name;
				int num = (int)applyKind;
				throw new ArgumentOutOfRangeException(name, Strings.ADP_InvalidEnumerationValue(name, num.ToString(CultureInfo.InvariantCulture)));
			}
			return input.OuterApply(apply);
		}

		// Token: 0x0600536D RID: 21357 RVA: 0x0012C32C File Offset: 0x0012A52C
		internal static DbExpression CreateJoinExpressionByKind(DbExpressionKind joinKind, DbExpression joinCondition, DbExpressionBinding input1, DbExpressionBinding input2)
		{
			if (DbExpressionKind.CrossJoin == joinKind)
			{
				return DbExpressionBuilder.CrossJoin(new DbExpressionBinding[] { input1, input2 });
			}
			if (joinKind == DbExpressionKind.FullOuterJoin)
			{
				return input1.FullOuterJoin(input2, joinCondition);
			}
			if (joinKind == DbExpressionKind.InnerJoin)
			{
				return input1.InnerJoin(input2, joinCondition);
			}
			if (joinKind != DbExpressionKind.LeftOuterJoin)
			{
				string name = typeof(DbExpressionKind).Name;
				int num = (int)joinKind;
				throw new ArgumentOutOfRangeException(name, Strings.ADP_InvalidEnumerationValue(name, num.ToString(CultureInfo.InvariantCulture)));
			}
			return input1.LeftOuterJoin(input2, joinCondition);
		}

		// Token: 0x0600536E RID: 21358 RVA: 0x0012C3A8 File Offset: 0x0012A5A8
		internal static DbElementExpression CreateElementExpressionUnwrapSingleProperty(DbExpression argument)
		{
			IList<EdmProperty> properties = TypeHelpers.GetProperties(ArgumentValidation.ValidateElement(argument));
			if (properties == null || properties.Count != 1)
			{
				throw new ArgumentException(Strings.Cqt_Element_InvalidArgumentForUnwrapSingleProperty, "argument");
			}
			return new DbElementExpression(properties[0].TypeUsage, argument, true);
		}

		// Token: 0x0600536F RID: 21359 RVA: 0x0012C3F0 File Offset: 0x0012A5F0
		internal static DbRelatedEntityRef CreateRelatedEntityRef(RelationshipEndMember sourceEnd, RelationshipEndMember targetEnd, DbExpression targetEntity)
		{
			return new DbRelatedEntityRef(sourceEnd, targetEnd, targetEntity);
		}

		// Token: 0x06005370 RID: 21360 RVA: 0x0012C3FC File Offset: 0x0012A5FC
		internal static DbNewInstanceExpression CreateNewEntityWithRelationshipsExpression(EntityType entityType, IList<DbExpression> attributeValues, IList<DbRelatedEntityRef> relationships)
		{
			DbExpressionList dbExpressionList;
			ReadOnlyCollection<DbRelatedEntityRef> readOnlyCollection;
			return new DbNewInstanceExpression(ArgumentValidation.ValidateNewEntityWithRelationships(entityType, attributeValues, relationships, out dbExpressionList, out readOnlyCollection), dbExpressionList, readOnlyCollection);
		}

		// Token: 0x06005371 RID: 21361 RVA: 0x0012C41C File Offset: 0x0012A61C
		internal static DbRelationshipNavigationExpression NavigateAllowingAllRelationshipsInSameTypeHierarchy(this DbExpression navigateFrom, RelationshipEndMember fromEnd, RelationshipEndMember toEnd)
		{
			RelationshipType relationshipType;
			return new DbRelationshipNavigationExpression(ArgumentValidation.ValidateNavigate(navigateFrom, fromEnd, toEnd, out relationshipType, true), relationshipType, fromEnd, toEnd, navigateFrom);
		}

		// Token: 0x06005372 RID: 21362 RVA: 0x0012C43D File Offset: 0x0012A63D
		internal static DbPropertyExpression CreatePropertyExpressionFromMember(DbExpression instance, EdmMember member)
		{
			return DbExpressionBuilder.PropertyFromMember(instance, member, "member");
		}

		// Token: 0x06005373 RID: 21363 RVA: 0x0012C44B File Offset: 0x0012A64B
		private static TypeUsage CreateCollectionResultType(EdmType type)
		{
			return TypeUsage.Create(TypeHelpers.CreateCollectionType(TypeUsage.Create(type)));
		}

		// Token: 0x06005374 RID: 21364 RVA: 0x0012C45D File Offset: 0x0012A65D
		private static TypeUsage CreateCollectionResultType(TypeUsage elementType)
		{
			return TypeUsage.Create(TypeHelpers.CreateCollectionType(elementType));
		}

		// Token: 0x06005375 RID: 21365 RVA: 0x0012C46A File Offset: 0x0012A66A
		private static bool IsConstantNegativeInteger(DbExpression expression)
		{
			return expression.ExpressionKind == DbExpressionKind.Constant && TypeSemantics.IsIntegerNumericType(expression.ResultType) && Convert.ToInt64(((DbConstantExpression)expression).Value, CultureInfo.InvariantCulture) < 0L;
		}

		// Token: 0x04001DEE RID: 7662
		private static readonly TypeUsage _booleanType = EdmProviderManifest.Instance.GetCanonicalModelTypeUsage(PrimitiveTypeKind.Boolean);

		// Token: 0x04001DEF RID: 7663
		private static readonly AliasGenerator _bindingAliases = new AliasGenerator("Var_", 0);

		// Token: 0x04001DF0 RID: 7664
		private static readonly DbNullExpression _binaryNull = EdmProviderManifest.Instance.GetCanonicalModelTypeUsage(PrimitiveTypeKind.Binary).Null();

		// Token: 0x04001DF1 RID: 7665
		private static readonly DbNullExpression _boolNull = EdmProviderManifest.Instance.GetCanonicalModelTypeUsage(PrimitiveTypeKind.Boolean).Null();

		// Token: 0x04001DF2 RID: 7666
		private static readonly DbNullExpression _byteNull = EdmProviderManifest.Instance.GetCanonicalModelTypeUsage(PrimitiveTypeKind.Byte).Null();

		// Token: 0x04001DF3 RID: 7667
		private static readonly DbNullExpression _dateTimeNull = EdmProviderManifest.Instance.GetCanonicalModelTypeUsage(PrimitiveTypeKind.DateTime).Null();

		// Token: 0x04001DF4 RID: 7668
		private static readonly DbNullExpression _dateTimeOffsetNull = EdmProviderManifest.Instance.GetCanonicalModelTypeUsage(PrimitiveTypeKind.DateTimeOffset).Null();

		// Token: 0x04001DF5 RID: 7669
		private static readonly DbNullExpression _decimalNull = EdmProviderManifest.Instance.GetCanonicalModelTypeUsage(PrimitiveTypeKind.Decimal).Null();

		// Token: 0x04001DF6 RID: 7670
		private static readonly DbNullExpression _doubleNull = EdmProviderManifest.Instance.GetCanonicalModelTypeUsage(PrimitiveTypeKind.Double).Null();

		// Token: 0x04001DF7 RID: 7671
		private static readonly DbNullExpression _geographyNull = EdmProviderManifest.Instance.GetCanonicalModelTypeUsage(PrimitiveTypeKind.Geography).Null();

		// Token: 0x04001DF8 RID: 7672
		private static readonly DbNullExpression _geometryNull = EdmProviderManifest.Instance.GetCanonicalModelTypeUsage(PrimitiveTypeKind.Geometry).Null();

		// Token: 0x04001DF9 RID: 7673
		private static readonly DbNullExpression _guidNull = EdmProviderManifest.Instance.GetCanonicalModelTypeUsage(PrimitiveTypeKind.Guid).Null();

		// Token: 0x04001DFA RID: 7674
		private static readonly DbNullExpression _int16Null = EdmProviderManifest.Instance.GetCanonicalModelTypeUsage(PrimitiveTypeKind.Int16).Null();

		// Token: 0x04001DFB RID: 7675
		private static readonly DbNullExpression _int32Null = EdmProviderManifest.Instance.GetCanonicalModelTypeUsage(PrimitiveTypeKind.Int32).Null();

		// Token: 0x04001DFC RID: 7676
		private static readonly DbNullExpression _int64Null = EdmProviderManifest.Instance.GetCanonicalModelTypeUsage(PrimitiveTypeKind.Int64).Null();

		// Token: 0x04001DFD RID: 7677
		private static readonly DbNullExpression _sbyteNull = EdmProviderManifest.Instance.GetCanonicalModelTypeUsage(PrimitiveTypeKind.SByte).Null();

		// Token: 0x04001DFE RID: 7678
		private static readonly DbNullExpression _singleNull = EdmProviderManifest.Instance.GetCanonicalModelTypeUsage(PrimitiveTypeKind.Single).Null();

		// Token: 0x04001DFF RID: 7679
		private static readonly DbNullExpression _stringNull = EdmProviderManifest.Instance.GetCanonicalModelTypeUsage(PrimitiveTypeKind.String).Null();

		// Token: 0x04001E00 RID: 7680
		private static readonly DbNullExpression _timeNull = EdmProviderManifest.Instance.GetCanonicalModelTypeUsage(PrimitiveTypeKind.Time).Null();

		// Token: 0x04001E01 RID: 7681
		private static readonly DbConstantExpression _boolTrue = DbExpressionBuilder.Constant(true);

		// Token: 0x04001E02 RID: 7682
		private static readonly DbConstantExpression _boolFalse = DbExpressionBuilder.Constant(false);
	}
}
