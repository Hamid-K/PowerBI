using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common.CommandTrees.Internal;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Metadata.Edm.Provider;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Globalization;
using System.Linq;

namespace System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder.Internal
{
	// Token: 0x020006FA RID: 1786
	internal static class ArgumentValidation
	{
		// Token: 0x0600541E RID: 21534 RVA: 0x0012E339 File Offset: 0x0012C539
		internal static ReadOnlyCollection<TElement> NewReadOnlyCollection<TElement>(IList<TElement> list)
		{
			return new ReadOnlyCollection<TElement>(list);
		}

		// Token: 0x0600541F RID: 21535 RVA: 0x0012E341 File Offset: 0x0012C541
		internal static void RequirePolymorphicType(TypeUsage type)
		{
			if (!TypeSemantics.IsPolymorphicType(type))
			{
				throw new ArgumentException(Strings.Cqt_General_PolymorphicTypeRequired(type.ToString()), "type");
			}
		}

		// Token: 0x06005420 RID: 21536 RVA: 0x0012E361 File Offset: 0x0012C561
		internal static void RequireCompatibleType(DbExpression expression, TypeUsage requiredResultType, string argumentName)
		{
			ArgumentValidation.RequireCompatibleType(expression, requiredResultType, argumentName, -1);
		}

		// Token: 0x06005421 RID: 21537 RVA: 0x0012E36C File Offset: 0x0012C56C
		private static void RequireCompatibleType(DbExpression expression, TypeUsage requiredResultType, string argumentName, int argumentIndex)
		{
			if (!TypeSemantics.IsStructurallyEqualOrPromotableTo(expression.ResultType, requiredResultType))
			{
				if (argumentIndex != -1)
				{
					argumentName = StringUtil.FormatIndex(argumentName, argumentIndex);
				}
				throw new ArgumentException(Strings.Cqt_ExpressionLink_TypeMismatch(expression.ResultType.ToString(), requiredResultType.ToString()), argumentName);
			}
		}

		// Token: 0x06005422 RID: 21538 RVA: 0x0012E3A6 File Offset: 0x0012C5A6
		internal static void RequireCompatibleType(DbExpression expression, PrimitiveTypeKind requiredResultType, string argumentName)
		{
			ArgumentValidation.RequireCompatibleType(expression, requiredResultType, argumentName, -1);
		}

		// Token: 0x06005423 RID: 21539 RVA: 0x0012E3B4 File Offset: 0x0012C5B4
		private static void RequireCompatibleType(DbExpression expression, PrimitiveTypeKind requiredResultType, string argumentName, int index)
		{
			PrimitiveTypeKind primitiveTypeKind;
			bool flag = TypeHelpers.TryGetPrimitiveTypeKind(expression.ResultType, out primitiveTypeKind);
			if (!flag || primitiveTypeKind != requiredResultType)
			{
				if (index != -1)
				{
					argumentName = StringUtil.FormatIndex(argumentName, index);
				}
				throw new ArgumentException(Strings.Cqt_ExpressionLink_TypeMismatch(flag ? Enum.GetName(typeof(PrimitiveTypeKind), primitiveTypeKind) : expression.ResultType.ToString(), Enum.GetName(typeof(PrimitiveTypeKind), requiredResultType)), argumentName);
			}
		}

		// Token: 0x06005424 RID: 21540 RVA: 0x0012E42C File Offset: 0x0012C62C
		private static void RequireCompatibleType(DbExpression from, RelationshipEndMember end, bool allowAllRelationshipsInSameTypeHierarchy)
		{
			TypeUsage typeUsage = end.TypeUsage;
			if (!TypeSemantics.IsReferenceType(typeUsage))
			{
				typeUsage = TypeHelpers.CreateReferenceTypeUsage(TypeHelpers.GetEdmType<EntityType>(typeUsage));
			}
			if (allowAllRelationshipsInSameTypeHierarchy)
			{
				if (TypeHelpers.GetCommonTypeUsage(typeUsage, from.ResultType) == null)
				{
					throw new ArgumentException(Strings.Cqt_RelNav_WrongSourceType(typeUsage.ToString()), "from");
				}
			}
			else if (!TypeSemantics.IsStructurallyEqualOrPromotableTo(from.ResultType.EdmType, typeUsage.EdmType))
			{
				throw new ArgumentException(Strings.Cqt_RelNav_WrongSourceType(typeUsage.ToString()), "from");
			}
		}

		// Token: 0x06005425 RID: 21541 RVA: 0x0012E4A9 File Offset: 0x0012C6A9
		internal static void RequireCollectionArgument<TExpressionType>(DbExpression argument)
		{
			if (!TypeSemantics.IsCollectionType(argument.ResultType))
			{
				throw new ArgumentException(Strings.Cqt_Unary_CollectionRequired(typeof(TExpressionType).Name), "argument");
			}
		}

		// Token: 0x06005426 RID: 21542 RVA: 0x0012E4D8 File Offset: 0x0012C6D8
		internal static TypeUsage RequireCollectionArguments<TExpressionType>(DbExpression left, DbExpression right)
		{
			if (!TypeSemantics.IsCollectionType(left.ResultType) || !TypeSemantics.IsCollectionType(right.ResultType))
			{
				throw new ArgumentException(Strings.Cqt_Binary_CollectionsRequired(typeof(TExpressionType).Name));
			}
			TypeUsage commonTypeUsage = TypeHelpers.GetCommonTypeUsage(left.ResultType, right.ResultType);
			if (commonTypeUsage == null)
			{
				throw new ArgumentException(Strings.Cqt_Binary_CollectionsRequired(typeof(TExpressionType).Name));
			}
			return commonTypeUsage;
		}

		// Token: 0x06005427 RID: 21543 RVA: 0x0012E54C File Offset: 0x0012C74C
		internal static TypeUsage RequireComparableCollectionArguments<TExpressionType>(DbExpression left, DbExpression right)
		{
			TypeUsage typeUsage = ArgumentValidation.RequireCollectionArguments<TExpressionType>(left, right);
			if (!TypeHelpers.IsSetComparableOpType(TypeHelpers.GetElementTypeUsage(left.ResultType)))
			{
				throw new ArgumentException(Strings.Cqt_InvalidTypeForSetOperation(TypeHelpers.GetElementTypeUsage(left.ResultType).Identity, typeof(TExpressionType).Name), "left");
			}
			if (!TypeHelpers.IsSetComparableOpType(TypeHelpers.GetElementTypeUsage(right.ResultType)))
			{
				throw new ArgumentException(Strings.Cqt_InvalidTypeForSetOperation(TypeHelpers.GetElementTypeUsage(right.ResultType).Identity, typeof(TExpressionType).Name), "right");
			}
			return typeUsage;
		}

		// Token: 0x06005428 RID: 21544 RVA: 0x0012E5E2 File Offset: 0x0012C7E2
		private static EnumerableValidator<TElementIn, TElementOut, TResult> CreateValidator<TElementIn, TElementOut, TResult>(IEnumerable<TElementIn> argument, string argumentName, Func<TElementIn, int, TElementOut> convertElement, Func<List<TElementOut>, TResult> createResult)
		{
			return new EnumerableValidator<TElementIn, TElementOut, TResult>(argument, argumentName)
			{
				ConvertElement = convertElement,
				CreateResult = createResult
			};
		}

		// Token: 0x06005429 RID: 21545 RVA: 0x0012E5F9 File Offset: 0x0012C7F9
		internal static DbExpressionList CreateExpressionList(IEnumerable<DbExpression> arguments, string argumentName, Action<DbExpression, int> validationCallback)
		{
			return ArgumentValidation.CreateExpressionList(arguments, argumentName, false, validationCallback);
		}

		// Token: 0x0600542A RID: 21546 RVA: 0x0012E604 File Offset: 0x0012C804
		private static DbExpressionList CreateExpressionList(IEnumerable<DbExpression> arguments, string argumentName, bool allowEmpty, Action<DbExpression, int> validationCallback)
		{
			EnumerableValidator<DbExpression, DbExpression, DbExpressionList> enumerableValidator = ArgumentValidation.CreateValidator<DbExpression, DbExpression, DbExpressionList>(arguments, argumentName, delegate(DbExpression exp, int idx)
			{
				if (validationCallback != null)
				{
					validationCallback(exp, idx);
				}
				return exp;
			}, (List<DbExpression> expList) => new DbExpressionList(expList));
			enumerableValidator.AllowEmpty = allowEmpty;
			return enumerableValidator.Validate();
		}

		// Token: 0x0600542B RID: 21547 RVA: 0x0012E65C File Offset: 0x0012C85C
		private static DbExpressionList CreateExpressionList(IEnumerable<DbExpression> arguments, string argumentName, int expectedElementCount, Action<DbExpression, int> validationCallback)
		{
			EnumerableValidator<DbExpression, DbExpression, DbExpressionList> enumerableValidator = ArgumentValidation.CreateValidator<DbExpression, DbExpression, DbExpressionList>(arguments, argumentName, delegate(DbExpression exp, int idx)
			{
				if (validationCallback != null)
				{
					validationCallback(exp, idx);
				}
				return exp;
			}, (List<DbExpression> expList) => new DbExpressionList(expList));
			enumerableValidator.ExpectedElementCount = expectedElementCount;
			enumerableValidator.AllowEmpty = false;
			return enumerableValidator.Validate();
		}

		// Token: 0x0600542C RID: 21548 RVA: 0x0012E6BB File Offset: 0x0012C8BB
		private static FunctionParameter[] GetExpectedParameters(EdmFunction function)
		{
			return function.Parameters.Where((FunctionParameter p) => p.Mode == ParameterMode.In || p.Mode == ParameterMode.InOut).ToArray<FunctionParameter>();
		}

		// Token: 0x0600542D RID: 21549 RVA: 0x0012E6EC File Offset: 0x0012C8EC
		internal static DbExpressionList ValidateFunctionAggregate(EdmFunction function, IEnumerable<DbExpression> args)
		{
			ArgumentValidation.CheckFunction(function);
			if (!TypeSemantics.IsAggregateFunction(function) || function.ReturnParameter == null)
			{
				throw new ArgumentException(Strings.Cqt_Aggregate_InvalidFunction, "function");
			}
			FunctionParameter[] expectedParams = ArgumentValidation.GetExpectedParameters(function);
			return ArgumentValidation.CreateExpressionList(args, "argument", expectedParams.Length, delegate(DbExpression exp, int idx)
			{
				TypeUsage typeUsage = expectedParams[idx].TypeUsage;
				TypeUsage typeUsage2 = null;
				if (TypeHelpers.TryGetCollectionElementType(typeUsage, out typeUsage2))
				{
					typeUsage = typeUsage2;
				}
				ArgumentValidation.RequireCompatibleType(exp, typeUsage, "argument");
			});
		}

		// Token: 0x0600542E RID: 21550 RVA: 0x0012E750 File Offset: 0x0012C950
		internal static void ValidateSortClause(DbExpression key)
		{
			if (!TypeHelpers.IsValidSortOpKeyType(key.ResultType))
			{
				throw new ArgumentException(Strings.Cqt_Sort_OrderComparable, "key");
			}
		}

		// Token: 0x0600542F RID: 21551 RVA: 0x0012E76F File Offset: 0x0012C96F
		internal static void ValidateSortClause(DbExpression key, string collation)
		{
			ArgumentValidation.ValidateSortClause(key);
			Check.NotEmpty(collation, "collation");
			if (!TypeSemantics.IsPrimitiveType(key.ResultType, PrimitiveTypeKind.String))
			{
				throw new ArgumentException(Strings.Cqt_Sort_NonStringCollationInvalid, "collation");
			}
		}

		// Token: 0x06005430 RID: 21552 RVA: 0x0012E7A4 File Offset: 0x0012C9A4
		internal static ReadOnlyCollection<DbVariableReferenceExpression> ValidateLambda(IEnumerable<DbVariableReferenceExpression> variables)
		{
			EnumerableValidator<DbVariableReferenceExpression, DbVariableReferenceExpression, ReadOnlyCollection<DbVariableReferenceExpression>> enumerableValidator = ArgumentValidation.CreateValidator<DbVariableReferenceExpression, DbVariableReferenceExpression, ReadOnlyCollection<DbVariableReferenceExpression>>(variables, "variables", delegate(DbVariableReferenceExpression varExp, int idx)
			{
				if (varExp == null)
				{
					throw new ArgumentNullException(StringUtil.FormatIndex("variables", idx));
				}
				return varExp;
			}, (List<DbVariableReferenceExpression> varList) => new ReadOnlyCollection<DbVariableReferenceExpression>(varList));
			enumerableValidator.AllowEmpty = true;
			enumerableValidator.GetName = (DbVariableReferenceExpression varDef, int idx) => varDef.VariableName;
			return enumerableValidator.Validate();
		}

		// Token: 0x06005431 RID: 21553 RVA: 0x0012E82B File Offset: 0x0012CA2B
		internal static TypeUsage ValidateQuantifier(DbExpression predicate)
		{
			ArgumentValidation.RequireCompatibleType(predicate, PrimitiveTypeKind.Boolean, "predicate");
			return predicate.ResultType;
		}

		// Token: 0x06005432 RID: 21554 RVA: 0x0012E840 File Offset: 0x0012CA40
		internal static TypeUsage ValidateApply(DbExpressionBinding input, DbExpressionBinding apply)
		{
			if (input.VariableName.Equals(apply.VariableName, StringComparison.Ordinal))
			{
				throw new ArgumentException(Strings.Cqt_Apply_DuplicateVariableNames);
			}
			return ArgumentValidation.CreateCollectionOfRowResultType(new List<KeyValuePair<string, TypeUsage>>
			{
				new KeyValuePair<string, TypeUsage>(input.VariableName, input.VariableType),
				new KeyValuePair<string, TypeUsage>(apply.VariableName, apply.VariableType)
			});
		}

		// Token: 0x06005433 RID: 21555 RVA: 0x0012E8A4 File Offset: 0x0012CAA4
		internal static ReadOnlyCollection<DbExpressionBinding> ValidateCrossJoin(IEnumerable<DbExpressionBinding> inputs, out TypeUsage resultType)
		{
			List<DbExpressionBinding> list = new List<DbExpressionBinding>();
			List<KeyValuePair<string, TypeUsage>> list2 = new List<KeyValuePair<string, TypeUsage>>();
			Dictionary<string, int> dictionary = new Dictionary<string, int>();
			IEnumerator<DbExpressionBinding> enumerator = inputs.GetEnumerator();
			int num = 0;
			while (enumerator.MoveNext())
			{
				DbExpressionBinding dbExpressionBinding = enumerator.Current;
				string text = StringUtil.FormatIndex("inputs", num);
				if (dbExpressionBinding == null)
				{
					throw new ArgumentNullException(text);
				}
				int num2 = -1;
				if (dictionary.TryGetValue(dbExpressionBinding.VariableName, out num2))
				{
					throw new ArgumentException(Strings.Cqt_CrossJoin_DuplicateVariableNames(num2, num, dbExpressionBinding.VariableName));
				}
				list.Add(dbExpressionBinding);
				dictionary.Add(dbExpressionBinding.VariableName, num);
				list2.Add(new KeyValuePair<string, TypeUsage>(dbExpressionBinding.VariableName, dbExpressionBinding.VariableType));
				num++;
			}
			if (list.Count < 2)
			{
				throw new ArgumentException(Strings.Cqt_CrossJoin_AtLeastTwoInputs, "inputs");
			}
			resultType = ArgumentValidation.CreateCollectionOfRowResultType(list2);
			return new ReadOnlyCollection<DbExpressionBinding>(list);
		}

		// Token: 0x06005434 RID: 21556 RVA: 0x0012E990 File Offset: 0x0012CB90
		internal static TypeUsage ValidateJoin(DbExpressionBinding left, DbExpressionBinding right, DbExpression joinCondition)
		{
			if (left.VariableName.Equals(right.VariableName, StringComparison.Ordinal))
			{
				throw new ArgumentException(Strings.Cqt_Join_DuplicateVariableNames);
			}
			ArgumentValidation.RequireCompatibleType(joinCondition, PrimitiveTypeKind.Boolean, "joinCondition");
			return ArgumentValidation.CreateCollectionOfRowResultType(new List<KeyValuePair<string, TypeUsage>>(2)
			{
				new KeyValuePair<string, TypeUsage>(left.VariableName, left.VariableType),
				new KeyValuePair<string, TypeUsage>(right.VariableName, right.VariableType)
			});
		}

		// Token: 0x06005435 RID: 21557 RVA: 0x0012EA01 File Offset: 0x0012CC01
		internal static TypeUsage ValidateFilter(DbExpressionBinding input, DbExpression predicate)
		{
			ArgumentValidation.RequireCompatibleType(predicate, PrimitiveTypeKind.Boolean, "predicate");
			return input.Expression.ResultType;
		}

		// Token: 0x06005436 RID: 21558 RVA: 0x0012EA1C File Offset: 0x0012CC1C
		internal static TypeUsage ValidateGroupBy(IEnumerable<KeyValuePair<string, DbExpression>> keys, IEnumerable<KeyValuePair<string, DbAggregate>> aggregates, out DbExpressionList validKeys, out ReadOnlyCollection<DbAggregate> validAggregates)
		{
			List<KeyValuePair<string, TypeUsage>> columns = new List<KeyValuePair<string, TypeUsage>>();
			HashSet<string> keyNames = new HashSet<string>();
			EnumerableValidator<KeyValuePair<string, DbExpression>, DbExpression, DbExpressionList> enumerableValidator = ArgumentValidation.CreateValidator<KeyValuePair<string, DbExpression>, DbExpression, DbExpressionList>(keys, "keys", delegate(KeyValuePair<string, DbExpression> keyInfo, int index)
			{
				ArgumentValidation.CheckNamed<DbExpression>(keyInfo, "keys", index);
				if (!TypeHelpers.IsValidGroupKeyType(keyInfo.Value.ResultType))
				{
					throw new ArgumentException(Strings.Cqt_GroupBy_KeyNotEqualityComparable(keyInfo.Key));
				}
				keyNames.Add(keyInfo.Key);
				columns.Add(new KeyValuePair<string, TypeUsage>(keyInfo.Key, keyInfo.Value.ResultType));
				return keyInfo.Value;
			}, (List<DbExpression> expList) => new DbExpressionList(expList));
			enumerableValidator.AllowEmpty = true;
			enumerableValidator.GetName = (KeyValuePair<string, DbExpression> keyInfo, int idx) => keyInfo.Key;
			validKeys = enumerableValidator.Validate();
			bool hasGroupAggregate = false;
			EnumerableValidator<KeyValuePair<string, DbAggregate>, DbAggregate, ReadOnlyCollection<DbAggregate>> enumerableValidator2 = ArgumentValidation.CreateValidator<KeyValuePair<string, DbAggregate>, DbAggregate, ReadOnlyCollection<DbAggregate>>(aggregates, "aggregates", delegate(KeyValuePair<string, DbAggregate> aggInfo, int idx)
			{
				ArgumentValidation.CheckNamed<DbAggregate>(aggInfo, "aggregates", idx);
				if (keyNames.Contains(aggInfo.Key))
				{
					throw new ArgumentException(Strings.Cqt_GroupBy_AggregateColumnExistsAsGroupColumn(aggInfo.Key));
				}
				if (aggInfo.Value is DbGroupAggregate)
				{
					if (hasGroupAggregate)
					{
						throw new ArgumentException(Strings.Cqt_GroupBy_MoreThanOneGroupAggregate);
					}
					hasGroupAggregate = true;
				}
				columns.Add(new KeyValuePair<string, TypeUsage>(aggInfo.Key, aggInfo.Value.ResultType));
				return aggInfo.Value;
			}, (List<DbAggregate> aggList) => ArgumentValidation.NewReadOnlyCollection<DbAggregate>(aggList));
			enumerableValidator2.AllowEmpty = true;
			enumerableValidator2.GetName = (KeyValuePair<string, DbAggregate> aggInfo, int idx) => aggInfo.Key;
			validAggregates = enumerableValidator2.Validate();
			if (validKeys.Count == 0 && validAggregates.Count == 0)
			{
				throw new ArgumentException(Strings.Cqt_GroupBy_AtLeastOneKeyOrAggregate);
			}
			return ArgumentValidation.CreateCollectionOfRowResultType(columns);
		}

		// Token: 0x06005437 RID: 21559 RVA: 0x0012EB4C File Offset: 0x0012CD4C
		internal static ReadOnlyCollection<DbSortClause> ValidateSortArguments(IEnumerable<DbSortClause> sortOrder)
		{
			EnumerableValidator<DbSortClause, DbSortClause, ReadOnlyCollection<DbSortClause>> enumerableValidator = ArgumentValidation.CreateValidator<DbSortClause, DbSortClause, ReadOnlyCollection<DbSortClause>>(sortOrder, "sortOrder", (DbSortClause key, int idx) => key, (List<DbSortClause> keyList) => ArgumentValidation.NewReadOnlyCollection<DbSortClause>(keyList));
			enumerableValidator.AllowEmpty = false;
			return enumerableValidator.Validate();
		}

		// Token: 0x06005438 RID: 21560 RVA: 0x0012EBAE File Offset: 0x0012CDAE
		internal static ReadOnlyCollection<DbSortClause> ValidateSort(IEnumerable<DbSortClause> sortOrder)
		{
			return ArgumentValidation.ValidateSortArguments(sortOrder);
		}

		// Token: 0x06005439 RID: 21561 RVA: 0x0012EBB8 File Offset: 0x0012CDB8
		internal static TypeUsage ValidateConstant(Type type)
		{
			PrimitiveTypeKind primitiveTypeKind;
			if (!ArgumentValidation.TryGetPrimitiveTypeKind(type, out primitiveTypeKind))
			{
				throw new ArgumentException(Strings.Cqt_Constant_InvalidType, "type");
			}
			return TypeHelpers.GetLiteralTypeUsage(primitiveTypeKind);
		}

		// Token: 0x0600543A RID: 21562 RVA: 0x0012EBE5 File Offset: 0x0012CDE5
		internal static TypeUsage ValidateConstant(object value)
		{
			return ArgumentValidation.ValidateConstant(value.GetType());
		}

		// Token: 0x0600543B RID: 21563 RVA: 0x0012EBF4 File Offset: 0x0012CDF4
		internal static void ValidateConstant(TypeUsage constantType, object value)
		{
			ArgumentValidation.CheckType(constantType, "constantType");
			EnumType enumType;
			if (TypeHelpers.TryGetEdmType<EnumType>(constantType, out enumType))
			{
				Type clrEquivalentType = enumType.UnderlyingType.ClrEquivalentType;
				if (clrEquivalentType != value.GetType() && (!value.GetType().IsEnum() || !ArgumentValidation.ClrEdmEnumTypesMatch(enumType, value.GetType())))
				{
					throw new ArgumentException(Strings.Cqt_Constant_ClrEnumTypeDoesNotMatchEdmEnumType(value.GetType().Name, enumType.Name, clrEquivalentType.Name), "value");
				}
			}
			else
			{
				PrimitiveType primitiveType;
				if (!TypeHelpers.TryGetEdmType<PrimitiveType>(constantType, out primitiveType))
				{
					throw new ArgumentException(Strings.Cqt_Constant_InvalidConstantType(constantType.ToString()), "constantType");
				}
				PrimitiveTypeKind primitiveTypeKind;
				if ((!ArgumentValidation.TryGetPrimitiveTypeKind(value.GetType(), out primitiveTypeKind) || primitiveType.PrimitiveTypeKind != primitiveTypeKind) && (!Helper.IsGeographicType(primitiveType) || primitiveTypeKind != PrimitiveTypeKind.Geography) && (!Helper.IsGeometricType(primitiveType) || primitiveTypeKind != PrimitiveTypeKind.Geometry))
				{
					throw new ArgumentException(Strings.Cqt_Constant_InvalidValueForType(constantType.ToString()), "value");
				}
			}
		}

		// Token: 0x0600543C RID: 21564 RVA: 0x0012ECE0 File Offset: 0x0012CEE0
		internal static TypeUsage ValidateCreateRef(EntitySet entitySet, EntityType entityType, IEnumerable<DbExpression> keyValues, out DbExpression keyConstructor)
		{
			ArgumentValidation.CheckEntitySet(entitySet, "entitySet");
			ArgumentValidation.CheckType(entityType, "entityType");
			if (!TypeSemantics.IsValidPolymorphicCast(entitySet.ElementType, entityType))
			{
				throw new ArgumentException(Strings.Cqt_Ref_PolymorphicArgRequired);
			}
			IList<EdmMember> keyMembers = entityType.KeyMembers;
			EnumerableValidator<DbExpression, KeyValuePair<string, DbExpression>, List<KeyValuePair<string, DbExpression>>> enumerableValidator = ArgumentValidation.CreateValidator<DbExpression, KeyValuePair<string, DbExpression>, List<KeyValuePair<string, DbExpression>>>(keyValues, "keyValues", delegate(DbExpression valueExp, int idx)
			{
				ArgumentValidation.RequireCompatibleType(valueExp, keyMembers[idx].TypeUsage, "keyValues", idx);
				return new KeyValuePair<string, DbExpression>(keyMembers[idx].Name, valueExp);
			}, (List<KeyValuePair<string, DbExpression>> columnList) => columnList);
			enumerableValidator.ExpectedElementCount = keyMembers.Count;
			List<KeyValuePair<string, DbExpression>> list = enumerableValidator.Validate();
			keyConstructor = DbExpressionBuilder.NewRow(list);
			return ArgumentValidation.CreateReferenceResultType(entityType);
		}

		// Token: 0x0600543D RID: 21565 RVA: 0x0012ED8C File Offset: 0x0012CF8C
		internal static TypeUsage ValidateRefFromKey(EntitySet entitySet, DbExpression keyValues, EntityType entityType)
		{
			ArgumentValidation.CheckEntitySet(entitySet, "entitySet");
			ArgumentValidation.CheckType(entityType);
			if (!TypeSemantics.IsValidPolymorphicCast(entitySet.ElementType, entityType))
			{
				throw new ArgumentException(Strings.Cqt_Ref_PolymorphicArgRequired);
			}
			TypeUsage typeUsage = ArgumentValidation.CreateResultType(TypeHelpers.CreateKeyRowType(entitySet.ElementType));
			ArgumentValidation.RequireCompatibleType(keyValues, typeUsage, "keyValues");
			return ArgumentValidation.CreateReferenceResultType(entityType);
		}

		// Token: 0x0600543E RID: 21566 RVA: 0x0012EDE8 File Offset: 0x0012CFE8
		internal static TypeUsage ValidateNavigate(DbExpression navigateFrom, RelationshipType type, string fromEndName, string toEndName, out RelationshipEndMember fromEnd, out RelationshipEndMember toEnd)
		{
			ArgumentValidation.CheckType(type);
			if (!type.RelationshipEndMembers.TryGetValue(fromEndName, false, out fromEnd))
			{
				throw new ArgumentOutOfRangeException(fromEndName, Strings.Cqt_Factory_NoSuchRelationEnd);
			}
			if (!type.RelationshipEndMembers.TryGetValue(toEndName, false, out toEnd))
			{
				throw new ArgumentOutOfRangeException(toEndName, Strings.Cqt_Factory_NoSuchRelationEnd);
			}
			ArgumentValidation.RequireCompatibleType(navigateFrom, fromEnd, false);
			return ArgumentValidation.CreateResultType(toEnd);
		}

		// Token: 0x0600543F RID: 21567 RVA: 0x0012EE48 File Offset: 0x0012D048
		internal static TypeUsage ValidateNavigate(DbExpression navigateFrom, RelationshipEndMember fromEnd, RelationshipEndMember toEnd, out RelationshipType relType, bool allowAllRelationshipsInSameTypeHierarchy)
		{
			ArgumentValidation.CheckMember(fromEnd, "fromEnd");
			ArgumentValidation.CheckMember(toEnd, "toEnd");
			relType = fromEnd.DeclaringType as RelationshipType;
			ArgumentValidation.CheckType(relType);
			if (!relType.Equals(toEnd.DeclaringType))
			{
				throw new ArgumentException(Strings.Cqt_Factory_IncompatibleRelationEnds, "toEnd");
			}
			ArgumentValidation.RequireCompatibleType(navigateFrom, fromEnd, allowAllRelationshipsInSameTypeHierarchy);
			return ArgumentValidation.CreateResultType(toEnd);
		}

		// Token: 0x06005440 RID: 21568 RVA: 0x0012EEAD File Offset: 0x0012D0AD
		internal static TypeUsage ValidateElement(DbExpression argument)
		{
			ArgumentValidation.RequireCollectionArgument<DbElementExpression>(argument);
			return TypeHelpers.GetEdmType<CollectionType>(argument.ResultType).TypeUsage;
		}

		// Token: 0x06005441 RID: 21569 RVA: 0x0012EEC8 File Offset: 0x0012D0C8
		internal static TypeUsage ValidateCase(IEnumerable<DbExpression> whenExpressions, IEnumerable<DbExpression> thenExpressions, DbExpression elseExpression, out DbExpressionList validWhens, out DbExpressionList validThens)
		{
			validWhens = ArgumentValidation.CreateExpressionList(whenExpressions, "whenExpressions", delegate(DbExpression exp, int idx)
			{
				ArgumentValidation.RequireCompatibleType(exp, PrimitiveTypeKind.Boolean, "whenExpressions", idx);
			});
			TypeUsage commonResultType = null;
			validThens = ArgumentValidation.CreateExpressionList(thenExpressions, "thenExpressions", delegate(DbExpression exp, int idx)
			{
				if (commonResultType == null)
				{
					commonResultType = exp.ResultType;
					return;
				}
				commonResultType = TypeHelpers.GetCommonTypeUsage(exp.ResultType, commonResultType);
				if (commonResultType == null)
				{
					throw new ArgumentException(Strings.Cqt_Case_InvalidResultType);
				}
			});
			commonResultType = TypeHelpers.GetCommonTypeUsage(elseExpression.ResultType, commonResultType);
			if (commonResultType == null)
			{
				throw new ArgumentException(Strings.Cqt_Case_InvalidResultType);
			}
			if (validWhens.Count != validThens.Count)
			{
				throw new ArgumentException(Strings.Cqt_Case_WhensMustEqualThens);
			}
			return commonResultType;
		}

		// Token: 0x06005442 RID: 21570 RVA: 0x0012EF74 File Offset: 0x0012D174
		internal static TypeUsage ValidateFunction(EdmFunction function, IEnumerable<DbExpression> arguments, out DbExpressionList validArgs)
		{
			ArgumentValidation.CheckFunction(function);
			if (!function.IsComposableAttribute)
			{
				throw new ArgumentException(Strings.Cqt_Function_NonComposableInExpression, "function");
			}
			if (!string.IsNullOrEmpty(function.CommandTextAttribute) && !function.HasUserDefinedBody)
			{
				throw new ArgumentException(Strings.Cqt_Function_CommandTextInExpression, "function");
			}
			if (function.ReturnParameter == null)
			{
				throw new ArgumentException(Strings.Cqt_Function_VoidResultInvalid, "function");
			}
			FunctionParameter[] expectedParams = ArgumentValidation.GetExpectedParameters(function);
			validArgs = ArgumentValidation.CreateExpressionList(arguments, "arguments", expectedParams.Length, delegate(DbExpression exp, int idx)
			{
				ArgumentValidation.RequireCompatibleType(exp, expectedParams[idx].TypeUsage, "arguments", idx);
			});
			return function.ReturnParameter.TypeUsage;
		}

		// Token: 0x06005443 RID: 21571 RVA: 0x0012F01C File Offset: 0x0012D21C
		internal static TypeUsage ValidateInvoke(DbLambda lambda, IEnumerable<DbExpression> arguments, out DbExpressionList validArguments)
		{
			validArguments = null;
			EnumerableValidator<DbExpression, DbExpression, DbExpressionList> enumerableValidator = ArgumentValidation.CreateValidator<DbExpression, DbExpression, DbExpressionList>(arguments, "arguments", delegate(DbExpression exp, int idx)
			{
				ArgumentValidation.RequireCompatibleType(exp, lambda.Variables[idx].ResultType, "arguments", idx);
				return exp;
			}, (List<DbExpression> expList) => new DbExpressionList(expList));
			enumerableValidator.ExpectedElementCount = lambda.Variables.Count;
			validArguments = enumerableValidator.Validate();
			return lambda.Body.ResultType;
		}

		// Token: 0x06005444 RID: 21572 RVA: 0x0012F09E File Offset: 0x0012D29E
		internal static TypeUsage ValidateNewEmptyCollection(TypeUsage collectionType, out DbExpressionList validElements)
		{
			ArgumentValidation.CheckType(collectionType, "collectionType");
			if (!TypeSemantics.IsCollectionType(collectionType))
			{
				throw new ArgumentException(Strings.Cqt_NewInstance_CollectionTypeRequired, "collectionType");
			}
			validElements = new DbExpressionList(new DbExpression[0]);
			return collectionType;
		}

		// Token: 0x06005445 RID: 21573 RVA: 0x0012F0D4 File Offset: 0x0012D2D4
		internal static TypeUsage ValidateNewRow(IEnumerable<KeyValuePair<string, DbExpression>> columnValues, out DbExpressionList validElements)
		{
			List<KeyValuePair<string, TypeUsage>> columnTypes = new List<KeyValuePair<string, TypeUsage>>();
			EnumerableValidator<KeyValuePair<string, DbExpression>, DbExpression, DbExpressionList> enumerableValidator = ArgumentValidation.CreateValidator<KeyValuePair<string, DbExpression>, DbExpression, DbExpressionList>(columnValues, "columnValues", delegate(KeyValuePair<string, DbExpression> columnValue, int idx)
			{
				ArgumentValidation.CheckNamed<DbExpression>(columnValue, "columnValues", idx);
				columnTypes.Add(new KeyValuePair<string, TypeUsage>(columnValue.Key, columnValue.Value.ResultType));
				return columnValue.Value;
			}, (List<DbExpression> expList) => new DbExpressionList(expList));
			enumerableValidator.GetName = (KeyValuePair<string, DbExpression> columnValue, int idx) => columnValue.Key;
			validElements = enumerableValidator.Validate();
			return ArgumentValidation.CreateResultType(TypeHelpers.CreateRowType(columnTypes));
		}

		// Token: 0x06005446 RID: 21574 RVA: 0x0012F168 File Offset: 0x0012D368
		internal static TypeUsage ValidateNew(TypeUsage instanceType, IEnumerable<DbExpression> arguments, out DbExpressionList validArguments)
		{
			ArgumentValidation.CheckType(instanceType, "instanceType");
			CollectionType collectionType = null;
			if (TypeHelpers.TryGetEdmType<CollectionType>(instanceType, out collectionType) && collectionType != null)
			{
				TypeUsage elementType = collectionType.TypeUsage;
				validArguments = ArgumentValidation.CreateExpressionList(arguments, "arguments", true, delegate(DbExpression exp, int idx)
				{
					ArgumentValidation.RequireCompatibleType(exp, elementType, "arguments", idx);
				});
			}
			else
			{
				List<TypeUsage> expectedTypes = ArgumentValidation.GetStructuralMemberTypes(instanceType);
				int pos = 0;
				validArguments = ArgumentValidation.CreateExpressionList(arguments, "arguments", expectedTypes.Count, delegate(DbExpression exp, int idx)
				{
					List<TypeUsage> expectedTypes2 = expectedTypes;
					int pos2 = pos;
					pos = pos2 + 1;
					ArgumentValidation.RequireCompatibleType(exp, expectedTypes2[pos2], "arguments", idx);
				});
			}
			return instanceType;
		}

		// Token: 0x06005447 RID: 21575 RVA: 0x0012F1FC File Offset: 0x0012D3FC
		private static List<TypeUsage> GetStructuralMemberTypes(TypeUsage instanceType)
		{
			StructuralType structuralType = instanceType.EdmType as StructuralType;
			if (structuralType == null)
			{
				throw new ArgumentException(Strings.Cqt_NewInstance_StructuralTypeRequired, "instanceType");
			}
			if (structuralType.Abstract)
			{
				throw new ArgumentException(Strings.Cqt_NewInstance_CannotInstantiateAbstractType(instanceType.ToString()), "instanceType");
			}
			IBaseList<EdmMember> allStructuralMembers = TypeHelpers.GetAllStructuralMembers(structuralType);
			if (allStructuralMembers == null || allStructuralMembers.Count < 1)
			{
				throw new ArgumentException(Strings.Cqt_NewInstance_CannotInstantiateMemberlessType(instanceType.ToString()), "instanceType");
			}
			List<TypeUsage> list = new List<TypeUsage>(allStructuralMembers.Count);
			for (int i = 0; i < allStructuralMembers.Count; i++)
			{
				list.Add(Helper.GetModelTypeUsage(allStructuralMembers[i]));
			}
			return list;
		}

		// Token: 0x06005448 RID: 21576 RVA: 0x0012F2A0 File Offset: 0x0012D4A0
		internal static TypeUsage ValidateNewEntityWithRelationships(EntityType entityType, IEnumerable<DbExpression> attributeValues, IList<DbRelatedEntityRef> relationships, out DbExpressionList validArguments, out ReadOnlyCollection<DbRelatedEntityRef> validRelatedRefs)
		{
			TypeUsage typeUsage = ArgumentValidation.CreateResultType(entityType);
			typeUsage = ArgumentValidation.ValidateNew(typeUsage, attributeValues, out validArguments);
			if (relationships.Count > 0)
			{
				List<DbRelatedEntityRef> list = new List<DbRelatedEntityRef>(relationships.Count);
				for (int i = 0; i < relationships.Count; i++)
				{
					DbRelatedEntityRef dbRelatedEntityRef = relationships[i];
					EntityTypeBase elementType = TypeHelpers.GetEdmType<RefType>(dbRelatedEntityRef.SourceEnd.TypeUsage).ElementType;
					if (!entityType.EdmEquals(elementType) && !entityType.IsSubtypeOf(elementType))
					{
						throw new ArgumentException(Strings.Cqt_NewInstance_IncompatibleRelatedEntity_SourceTypeNotValid, StringUtil.FormatIndex("relationships", i));
					}
					list.Add(dbRelatedEntityRef);
				}
				validRelatedRefs = new ReadOnlyCollection<DbRelatedEntityRef>(list);
			}
			else
			{
				validRelatedRefs = new ReadOnlyCollection<DbRelatedEntityRef>(new DbRelatedEntityRef[0]);
			}
			return typeUsage;
		}

		// Token: 0x06005449 RID: 21577 RVA: 0x0012F350 File Offset: 0x0012D550
		internal static TypeUsage ValidateProperty(DbExpression instance, string propertyName, bool ignoreCase, out EdmMember foundMember)
		{
			StructuralType structuralType;
			if (TypeHelpers.TryGetEdmType<StructuralType>(instance.ResultType, out structuralType) && structuralType.Members.TryGetValue(propertyName, ignoreCase, out foundMember) && foundMember != null && (Helper.IsRelationshipEndMember(foundMember) || Helper.IsEdmProperty(foundMember) || Helper.IsNavigationProperty(foundMember)))
			{
				return Helper.GetModelTypeUsage(foundMember);
			}
			throw new ArgumentOutOfRangeException("propertyName", Strings.NoSuchProperty(propertyName, instance.ResultType.ToString()));
		}

		// Token: 0x0600544A RID: 21578 RVA: 0x0012F3C0 File Offset: 0x0012D5C0
		private static void CheckNamed<T>(KeyValuePair<string, T> element, string argumentName, int index)
		{
			if (string.IsNullOrEmpty(element.Key))
			{
				if (index != -1)
				{
					argumentName = StringUtil.FormatIndex(argumentName, index);
				}
				throw new ArgumentNullException(string.Format(CultureInfo.InvariantCulture, "{0}.Key", new object[] { argumentName }));
			}
			if (element.Value == null)
			{
				if (index != -1)
				{
					argumentName = StringUtil.FormatIndex(argumentName, index);
				}
				throw new ArgumentNullException(string.Format(CultureInfo.InvariantCulture, "{0}.Value", new object[] { argumentName }));
			}
		}

		// Token: 0x0600544B RID: 21579 RVA: 0x0012F441 File Offset: 0x0012D641
		private static void CheckReadOnly(GlobalItem item, string varName)
		{
			if (!item.IsReadOnly)
			{
				throw new ArgumentException(Strings.Cqt_General_MetadataNotReadOnly, varName);
			}
		}

		// Token: 0x0600544C RID: 21580 RVA: 0x0012F457 File Offset: 0x0012D657
		private static void CheckReadOnly(TypeUsage item, string varName)
		{
			if (!item.IsReadOnly)
			{
				throw new ArgumentException(Strings.Cqt_General_MetadataNotReadOnly, varName);
			}
		}

		// Token: 0x0600544D RID: 21581 RVA: 0x0012F46D File Offset: 0x0012D66D
		private static void CheckReadOnly(EntitySetBase item, string varName)
		{
			if (!item.IsReadOnly)
			{
				throw new ArgumentException(Strings.Cqt_General_MetadataNotReadOnly, varName);
			}
		}

		// Token: 0x0600544E RID: 21582 RVA: 0x0012F483 File Offset: 0x0012D683
		private static void CheckType(EdmType type)
		{
			ArgumentValidation.CheckType(type, "type");
		}

		// Token: 0x0600544F RID: 21583 RVA: 0x0012F490 File Offset: 0x0012D690
		private static void CheckType(EdmType type, string argumentName)
		{
			ArgumentValidation.CheckReadOnly(type, argumentName);
		}

		// Token: 0x06005450 RID: 21584 RVA: 0x0012F499 File Offset: 0x0012D699
		internal static void CheckType(TypeUsage type)
		{
			ArgumentValidation.CheckType(type, "type");
		}

		// Token: 0x06005451 RID: 21585 RVA: 0x0012F4A6 File Offset: 0x0012D6A6
		internal static void CheckType(TypeUsage type, string varName)
		{
			ArgumentValidation.CheckReadOnly(type, varName);
			if (!ArgumentValidation.CheckDataSpace(type))
			{
				throw new ArgumentException(Strings.Cqt_Metadata_TypeUsageIncorrectSpace, "type");
			}
		}

		// Token: 0x06005452 RID: 21586 RVA: 0x0012F4C7 File Offset: 0x0012D6C7
		internal static void CheckMember(EdmMember memberMeta, string varName)
		{
			ArgumentValidation.CheckReadOnly(memberMeta.DeclaringType, varName);
			if (!ArgumentValidation.CheckDataSpace(memberMeta.TypeUsage) || !ArgumentValidation.CheckDataSpace(memberMeta.DeclaringType))
			{
				throw new ArgumentException(Strings.Cqt_Metadata_EdmMemberIncorrectSpace, varName);
			}
		}

		// Token: 0x06005453 RID: 21587 RVA: 0x0012F4FB File Offset: 0x0012D6FB
		private static void CheckParameter(FunctionParameter paramMeta, string varName)
		{
			ArgumentValidation.CheckReadOnly(paramMeta.DeclaringFunction, varName);
			if (!ArgumentValidation.CheckDataSpace(paramMeta.TypeUsage))
			{
				throw new ArgumentException(Strings.Cqt_Metadata_FunctionParameterIncorrectSpace, varName);
			}
		}

		// Token: 0x06005454 RID: 21588 RVA: 0x0012F524 File Offset: 0x0012D724
		private static void CheckFunction(EdmFunction function)
		{
			ArgumentValidation.CheckReadOnly(function, "function");
			if (!ArgumentValidation.CheckDataSpace(function))
			{
				throw new ArgumentException(Strings.Cqt_Metadata_FunctionIncorrectSpace, "function");
			}
			if (function.IsComposableAttribute && function.ReturnParameter == null)
			{
				throw new ArgumentException(Strings.Cqt_Metadata_FunctionReturnParameterNull, "function");
			}
			if (function.ReturnParameter != null && !ArgumentValidation.CheckDataSpace(function.ReturnParameter.TypeUsage))
			{
				throw new ArgumentException(Strings.Cqt_Metadata_FunctionParameterIncorrectSpace, "function.ReturnParameter");
			}
			IList<FunctionParameter> parameters = function.Parameters;
			for (int i = 0; i < parameters.Count; i++)
			{
				ArgumentValidation.CheckParameter(parameters[i], StringUtil.FormatIndex("function.Parameters", i));
			}
		}

		// Token: 0x06005455 RID: 21589 RVA: 0x0012F5D0 File Offset: 0x0012D7D0
		internal static void CheckEntitySet(EntitySetBase entitySet, string varName)
		{
			ArgumentValidation.CheckReadOnly(entitySet, varName);
			if (entitySet.EntityContainer == null)
			{
				throw new ArgumentException(Strings.Cqt_Metadata_EntitySetEntityContainerNull, varName);
			}
			if (!ArgumentValidation.CheckDataSpace(entitySet.EntityContainer))
			{
				throw new ArgumentException(Strings.Cqt_Metadata_EntitySetIncorrectSpace, varName);
			}
			if (!ArgumentValidation.CheckDataSpace(entitySet.ElementType))
			{
				throw new ArgumentException(Strings.Cqt_Metadata_EntitySetIncorrectSpace, varName);
			}
		}

		// Token: 0x06005456 RID: 21590 RVA: 0x0012F62A File Offset: 0x0012D82A
		private static bool CheckDataSpace(TypeUsage type)
		{
			return ArgumentValidation.CheckDataSpace(type.EdmType);
		}

		// Token: 0x06005457 RID: 21591 RVA: 0x0012F638 File Offset: 0x0012D838
		private static bool CheckDataSpace(GlobalItem item)
		{
			if (BuiltInTypeKind.PrimitiveType == item.BuiltInTypeKind || (BuiltInTypeKind.EdmFunction == item.BuiltInTypeKind && DataSpace.CSpace == item.DataSpace))
			{
				return true;
			}
			if (Helper.IsRowType(item))
			{
				using (ReadOnlyMetadataCollection<EdmProperty>.Enumerator enumerator = ((RowType)item).Properties.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (!ArgumentValidation.CheckDataSpace(enumerator.Current.TypeUsage))
						{
							return false;
						}
					}
				}
				return true;
			}
			if (Helper.IsCollectionType(item))
			{
				return ArgumentValidation.CheckDataSpace(((CollectionType)item).TypeUsage);
			}
			if (Helper.IsRefType(item))
			{
				return ArgumentValidation.CheckDataSpace(((RefType)item).ElementType);
			}
			return item.DataSpace == DataSpace.SSpace || item.DataSpace == DataSpace.CSpace;
		}

		// Token: 0x06005458 RID: 21592 RVA: 0x0012F70C File Offset: 0x0012D90C
		internal static TypeUsage CreateCollectionOfRowResultType(List<KeyValuePair<string, TypeUsage>> columns)
		{
			return TypeUsage.Create(TypeHelpers.CreateCollectionType(TypeUsage.Create(TypeHelpers.CreateRowType(columns))));
		}

		// Token: 0x06005459 RID: 21593 RVA: 0x0012F723 File Offset: 0x0012D923
		private static TypeUsage CreateResultType(EdmType resultType)
		{
			return TypeUsage.Create(resultType);
		}

		// Token: 0x0600545A RID: 21594 RVA: 0x0012F72C File Offset: 0x0012D92C
		private static TypeUsage CreateResultType(RelationshipEndMember end)
		{
			TypeUsage typeUsage = end.TypeUsage;
			if (!TypeSemantics.IsReferenceType(typeUsage))
			{
				typeUsage = TypeHelpers.CreateReferenceTypeUsage(TypeHelpers.GetEdmType<EntityType>(typeUsage));
			}
			if (RelationshipMultiplicity.Many == end.RelationshipMultiplicity)
			{
				typeUsage = TypeHelpers.CreateCollectionTypeUsage(typeUsage);
			}
			return typeUsage;
		}

		// Token: 0x0600545B RID: 21595 RVA: 0x0012F765 File Offset: 0x0012D965
		internal static TypeUsage CreateReferenceResultType(EntityTypeBase referencedEntityType)
		{
			return TypeUsage.Create(TypeHelpers.CreateReferenceType(referencedEntityType));
		}

		// Token: 0x0600545C RID: 21596 RVA: 0x0012F772 File Offset: 0x0012D972
		private static bool TryGetPrimitiveTypeKind(Type clrType, out PrimitiveTypeKind primitiveTypeKind)
		{
			return ClrProviderManifest.TryGetPrimitiveTypeKind(clrType, out primitiveTypeKind);
		}

		// Token: 0x0600545D RID: 21597 RVA: 0x0012F77C File Offset: 0x0012D97C
		private static bool ClrEdmEnumTypesMatch(EnumType edmEnumType, Type clrEnumType)
		{
			if (clrEnumType.Name != edmEnumType.Name || clrEnumType.GetEnumNames().Length < edmEnumType.Members.Count)
			{
				return false;
			}
			PrimitiveTypeKind primitiveTypeKind;
			if (!ArgumentValidation.TryGetPrimitiveTypeKind(clrEnumType.GetEnumUnderlyingType(), out primitiveTypeKind) || primitiveTypeKind != edmEnumType.UnderlyingType.PrimitiveTypeKind)
			{
				return false;
			}
			foreach (EnumMember enumMember in edmEnumType.Members)
			{
				if (!clrEnumType.GetEnumNames().Contains(enumMember.Name) || !enumMember.Value.Equals(Convert.ChangeType(Enum.Parse(clrEnumType, enumMember.Name), clrEnumType.GetEnumUnderlyingType(), CultureInfo.InvariantCulture)))
				{
					return false;
				}
			}
			return true;
		}
	}
}
