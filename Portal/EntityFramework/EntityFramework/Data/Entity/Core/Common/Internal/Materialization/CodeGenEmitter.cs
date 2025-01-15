using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.Entity.Core.Objects.ELinq;
using System.Data.Entity.Core.Objects.Internal;
using System.Data.Entity.Core.Query.InternalTrees;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Data.Entity.Core.Common.Internal.Materialization
{
	// Token: 0x02000632 RID: 1586
	internal static class CodeGenEmitter
	{
		// Token: 0x06004C48 RID: 19528 RVA: 0x0010CEFC File Offset: 0x0010B0FC
		internal static bool BinaryEquals(byte[] left, byte[] right)
		{
			if (left == null)
			{
				return right == null;
			}
			if (right == null)
			{
				return false;
			}
			if (left.Length != right.Length)
			{
				return false;
			}
			for (int i = 0; i < left.Length; i++)
			{
				if (left[i] != right[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06004C49 RID: 19529 RVA: 0x0010CF39 File Offset: 0x0010B139
		internal static Func<Shaper, TResult> Compile<TResult>(Expression body)
		{
			return CodeGenEmitter.BuildShaperLambda<TResult>(body).Compile();
		}

		// Token: 0x06004C4A RID: 19530 RVA: 0x0010CF46 File Offset: 0x0010B146
		internal static Expression<Func<Shaper, TResult>> BuildShaperLambda<TResult>(Expression body)
		{
			if (body != null)
			{
				return Expression.Lambda<Func<Shaper, TResult>>(body, new ParameterExpression[] { CodeGenEmitter.Shaper_Parameter });
			}
			return null;
		}

		// Token: 0x06004C4B RID: 19531 RVA: 0x0010CF61 File Offset: 0x0010B161
		internal static object Compile(Type resultType, Expression body)
		{
			return CodeGenEmitter.CodeGenEmitter_Compile.MakeGenericMethod(new Type[] { resultType }).Invoke(null, new object[] { body });
		}

		// Token: 0x06004C4C RID: 19532 RVA: 0x0010CF88 File Offset: 0x0010B188
		internal static Expression Emit_AndAlso(IEnumerable<Expression> operands)
		{
			Expression expression = null;
			foreach (Expression expression2 in operands)
			{
				if (expression == null)
				{
					expression = expression2;
				}
				else
				{
					expression = Expression.AndAlso(expression, expression2);
				}
			}
			return expression;
		}

		// Token: 0x06004C4D RID: 19533 RVA: 0x0010CFDC File Offset: 0x0010B1DC
		internal static Expression Emit_BitwiseOr(IEnumerable<Expression> operands)
		{
			Expression expression = null;
			foreach (Expression expression2 in operands)
			{
				if (expression == null)
				{
					expression = expression2;
				}
				else
				{
					expression = Expression.Or(expression, expression2);
				}
			}
			return expression;
		}

		// Token: 0x06004C4E RID: 19534 RVA: 0x0010D030 File Offset: 0x0010B230
		internal static Expression Emit_NullConstant(Type type)
		{
			Expression expression;
			if (type.IsNullable())
			{
				expression = Expression.Constant(null, type);
			}
			else
			{
				expression = CodeGenEmitter.Emit_EnsureType(Expression.Constant(null, typeof(object)), type);
			}
			return expression;
		}

		// Token: 0x06004C4F RID: 19535 RVA: 0x0010D067 File Offset: 0x0010B267
		internal static Expression Emit_WrappedNullConstant()
		{
			return Expression.Property(null, CodeGenEmitter.EntityWrapperFactory_NullWrapper);
		}

		// Token: 0x06004C50 RID: 19536 RVA: 0x0010D074 File Offset: 0x0010B274
		internal static Expression Emit_EnsureType(Expression input, Type type)
		{
			Expression expression = input;
			if (input.Type != type && !typeof(IEntityWrapper).IsAssignableFrom(input.Type))
			{
				if (type.IsAssignableFrom(input.Type))
				{
					expression = Expression.Convert(input, type);
				}
				else
				{
					expression = Expression.Call(CodeGenEmitter.CodeGenEmitter_CheckedConvert.MakeGenericMethod(new Type[] { input.Type, type }), input);
				}
			}
			return expression;
		}

		// Token: 0x06004C51 RID: 19537 RVA: 0x0010D0E8 File Offset: 0x0010B2E8
		internal static Expression Emit_EnsureTypeAndWrap(Expression input, Expression keyReader, Expression entitySetReader, Type requestedType, Type identityType, Type actualType, MergeOption mergeOption, bool isProxy)
		{
			Expression expression = CodeGenEmitter.Emit_EnsureType(input, requestedType);
			if (!requestedType.IsClass())
			{
				expression = CodeGenEmitter.Emit_EnsureType(input, typeof(object));
			}
			expression = CodeGenEmitter.Emit_EnsureType(expression, actualType);
			return CodeGenEmitter.CreateEntityWrapper(expression, keyReader, entitySetReader, actualType, identityType, mergeOption, isProxy);
		}

		// Token: 0x06004C52 RID: 19538 RVA: 0x0010D130 File Offset: 0x0010B330
		internal static Expression CreateEntityWrapper(Expression input, Expression keyReader, Expression entitySetReader, Type actualType, Type identityType, MergeOption mergeOption, bool isProxy)
		{
			bool flag = actualType.OverridesEqualsOrGetHashCode();
			bool flag2 = typeof(IEntityWithKey).IsAssignableFrom(actualType);
			bool flag3 = typeof(IEntityWithRelationships).IsAssignableFrom(actualType);
			bool flag4 = typeof(IEntityWithChangeTracker).IsAssignableFrom(actualType);
			Expression expression;
			if (flag3 && flag4 && flag2 && !isProxy)
			{
				expression = Expression.New(typeof(LightweightEntityWrapper<>).MakeGenericType(new Type[] { actualType }).GetDeclaredConstructor(new Type[]
				{
					actualType,
					typeof(EntityKey),
					typeof(EntitySet),
					typeof(ObjectContext),
					typeof(MergeOption),
					typeof(Type),
					typeof(bool)
				}), new Expression[]
				{
					input,
					keyReader,
					entitySetReader,
					CodeGenEmitter.Shaper_Context,
					Expression.Constant(mergeOption, typeof(MergeOption)),
					Expression.Constant(identityType, typeof(Type)),
					Expression.Constant(flag, typeof(bool))
				});
			}
			else
			{
				Expression expression2 = ((!flag3 || isProxy) ? Expression.Call(CodeGenEmitter.EntityWrapperFactory_GetPocoPropertyAccessorStrategyFunc, new Expression[0]) : Expression.Call(CodeGenEmitter.EntityWrapperFactory_GetNullPropertyAccessorStrategyFunc, new Expression[0]));
				Expression expression3 = (flag2 ? Expression.Call(CodeGenEmitter.EntityWrapperFactory_GetEntityWithKeyStrategyStrategyFunc, new Expression[0]) : Expression.Call(CodeGenEmitter.EntityWrapperFactory_GetPocoEntityKeyStrategyFunc, new Expression[0]));
				Expression expression4 = (flag4 ? Expression.Call(CodeGenEmitter.EntityWrapperFactory_GetEntityWithChangeTrackerStrategyFunc, new Expression[0]) : Expression.Call(CodeGenEmitter.EntityWrapperFactory_GetSnapshotChangeTrackingStrategyFunc, new Expression[0]));
				expression = Expression.New((flag3 ? typeof(EntityWrapperWithRelationships<>).MakeGenericType(new Type[] { actualType }) : typeof(EntityWrapperWithoutRelationships<>).MakeGenericType(new Type[] { actualType })).GetDeclaredConstructor(new Type[]
				{
					actualType,
					typeof(EntityKey),
					typeof(EntitySet),
					typeof(ObjectContext),
					typeof(MergeOption),
					typeof(Type),
					typeof(Func<object, IPropertyAccessorStrategy>),
					typeof(Func<object, IChangeTrackingStrategy>),
					typeof(Func<object, IEntityKeyStrategy>),
					typeof(bool)
				}), new Expression[]
				{
					input,
					keyReader,
					entitySetReader,
					CodeGenEmitter.Shaper_Context,
					Expression.Constant(mergeOption, typeof(MergeOption)),
					Expression.Constant(identityType, typeof(Type)),
					expression2,
					expression4,
					expression3,
					Expression.Constant(flag, typeof(bool))
				});
			}
			return Expression.Convert(expression, typeof(IEntityWrapper));
		}

		// Token: 0x06004C53 RID: 19539 RVA: 0x0010D42B File Offset: 0x0010B62B
		internal static Expression Emit_UnwrapAndEnsureType(Expression input, Type type)
		{
			return CodeGenEmitter.Emit_EnsureType(Expression.Property(input, CodeGenEmitter.IEntityWrapper_Entity), type);
		}

		// Token: 0x06004C54 RID: 19540 RVA: 0x0010D440 File Offset: 0x0010B640
		internal static TTarget CheckedConvert<TSource, TTarget>(TSource value)
		{
			TTarget ttarget;
			try
			{
				ttarget = (TTarget)((object)value);
			}
			catch (InvalidCastException)
			{
				Type type = value.GetType();
				if (type.IsGenericType() && type.GetGenericTypeDefinition() == typeof(CompensatingCollection<>))
				{
					type = typeof(IEnumerable<>).MakeGenericType(type.GetGenericArguments());
				}
				throw EntityUtil.ValueInvalidCast(type, typeof(TTarget));
			}
			catch (NullReferenceException)
			{
				throw new InvalidOperationException(Strings.Materializer_NullReferenceCast(typeof(TTarget).Name));
			}
			return ttarget;
		}

		// Token: 0x06004C55 RID: 19541 RVA: 0x0010D4E8 File Offset: 0x0010B6E8
		internal static Expression Emit_Equal(Expression left, Expression right)
		{
			Expression expression;
			if (typeof(byte[]) == left.Type)
			{
				expression = Expression.Call(CodeGenEmitter.CodeGenEmitter_BinaryEquals, left, right);
			}
			else
			{
				expression = Expression.Equal(left, right);
			}
			return expression;
		}

		// Token: 0x06004C56 RID: 19542 RVA: 0x0010D524 File Offset: 0x0010B724
		internal static Expression Emit_EntityKey_HasValue(SimpleColumnMap[] keyColumns)
		{
			return Expression.Not(CodeGenEmitter.Emit_Reader_IsDBNull(keyColumns[0]));
		}

		// Token: 0x06004C57 RID: 19543 RVA: 0x0010D533 File Offset: 0x0010B733
		internal static Expression Emit_Reader_GetValue(int ordinal, Type type)
		{
			return CodeGenEmitter.Emit_EnsureType(Expression.Call(CodeGenEmitter.Shaper_Reader, CodeGenEmitter.DbDataReader_GetValue, new Expression[] { Expression.Constant(ordinal) }), type);
		}

		// Token: 0x06004C58 RID: 19544 RVA: 0x0010D55E File Offset: 0x0010B75E
		internal static Expression Emit_Reader_IsDBNull(int ordinal)
		{
			return Expression.Call(CodeGenEmitter.Shaper_Reader, CodeGenEmitter.DbDataReader_IsDBNull, new Expression[] { Expression.Constant(ordinal) });
		}

		// Token: 0x06004C59 RID: 19545 RVA: 0x0010D583 File Offset: 0x0010B783
		internal static Expression Emit_Reader_IsDBNull(ColumnMap columnMap)
		{
			return CodeGenEmitter.Emit_Reader_IsDBNull(((ScalarColumnMap)columnMap).ColumnPos);
		}

		// Token: 0x06004C5A RID: 19546 RVA: 0x0010D595 File Offset: 0x0010B795
		internal static Expression Emit_Conditional_NotDBNull(Expression result, int ordinal, Type columnType)
		{
			result = Expression.Condition(CodeGenEmitter.Emit_Reader_IsDBNull(ordinal), Expression.Constant(TypeSystem.GetDefaultValue(columnType), columnType), result);
			return result;
		}

		// Token: 0x06004C5B RID: 19547 RVA: 0x0010D5B4 File Offset: 0x0010B7B4
		internal static MethodInfo GetReaderMethod(Type type, out bool isNullable)
		{
			isNullable = false;
			Type underlyingType = Nullable.GetUnderlyingType(type);
			if (null != underlyingType)
			{
				isNullable = true;
				type = underlyingType;
			}
			MethodInfo methodInfo;
			switch (Type.GetTypeCode(type))
			{
			case TypeCode.Boolean:
				return CodeGenEmitter.DbDataReader_GetBoolean;
			case TypeCode.Byte:
				return CodeGenEmitter.DbDataReader_GetByte;
			case TypeCode.Int16:
				return CodeGenEmitter.DbDataReader_GetInt16;
			case TypeCode.Int32:
				return CodeGenEmitter.DbDataReader_GetInt32;
			case TypeCode.Int64:
				return CodeGenEmitter.DbDataReader_GetInt64;
			case TypeCode.Single:
				return CodeGenEmitter.DbDataReader_GetFloat;
			case TypeCode.Double:
				return CodeGenEmitter.DbDataReader_GetDouble;
			case TypeCode.Decimal:
				return CodeGenEmitter.DbDataReader_GetDecimal;
			case TypeCode.DateTime:
				return CodeGenEmitter.DbDataReader_GetDateTime;
			case TypeCode.String:
				methodInfo = CodeGenEmitter.DbDataReader_GetString;
				isNullable = true;
				return methodInfo;
			}
			if (typeof(Guid) == type)
			{
				methodInfo = CodeGenEmitter.DbDataReader_GetGuid;
			}
			else if (typeof(TimeSpan) == type || typeof(DateTimeOffset) == type)
			{
				methodInfo = CodeGenEmitter.DbDataReader_GetValue;
			}
			else if (typeof(object) == type)
			{
				methodInfo = CodeGenEmitter.DbDataReader_GetValue;
			}
			else
			{
				methodInfo = CodeGenEmitter.DbDataReader_GetValue;
				isNullable = true;
			}
			return methodInfo;
		}

		// Token: 0x06004C5C RID: 19548 RVA: 0x0010D700 File Offset: 0x0010B900
		internal static Expression Emit_Shaper_GetPropertyValueWithErrorHandling(Type propertyType, int ordinal, string propertyName, string typeName, TypeUsage columnType)
		{
			PrimitiveTypeKind primitiveTypeKind;
			Expression expression;
			if (Helper.IsSpatialType(columnType, out primitiveTypeKind))
			{
				expression = Expression.Call(CodeGenEmitter.Shaper_Parameter, CodeGenEmitter.Shaper_GetSpatialPropertyValueWithErrorHandling.MakeGenericMethod(new Type[] { propertyType }), new Expression[]
				{
					Expression.Constant(ordinal),
					Expression.Constant(propertyName),
					Expression.Constant(typeName),
					Expression.Constant(primitiveTypeKind, typeof(PrimitiveTypeKind))
				});
			}
			else
			{
				expression = Expression.Call(CodeGenEmitter.Shaper_Parameter, CodeGenEmitter.Shaper_GetPropertyValueWithErrorHandling.MakeGenericMethod(new Type[] { propertyType }), Expression.Constant(ordinal), Expression.Constant(propertyName), Expression.Constant(typeName));
			}
			return expression;
		}

		// Token: 0x06004C5D RID: 19549 RVA: 0x0010D7B0 File Offset: 0x0010B9B0
		internal static Expression Emit_Shaper_GetColumnValueWithErrorHandling(Type resultType, int ordinal, TypeUsage columnType)
		{
			PrimitiveTypeKind primitiveTypeKind;
			Expression expression;
			if (Helper.IsSpatialType(columnType, out primitiveTypeKind))
			{
				primitiveTypeKind = (Helper.IsGeographicType((PrimitiveType)columnType.EdmType) ? PrimitiveTypeKind.Geography : PrimitiveTypeKind.Geometry);
				expression = Expression.Call(CodeGenEmitter.Shaper_Parameter, CodeGenEmitter.Shaper_GetSpatialColumnValueWithErrorHandling.MakeGenericMethod(new Type[] { resultType }), Expression.Constant(ordinal), Expression.Constant(primitiveTypeKind, typeof(PrimitiveTypeKind)));
			}
			else
			{
				expression = Expression.Call(CodeGenEmitter.Shaper_Parameter, CodeGenEmitter.Shaper_GetColumnValueWithErrorHandling.MakeGenericMethod(new Type[] { resultType }), new Expression[] { Expression.Constant(ordinal) });
			}
			return expression;
		}

		// Token: 0x06004C5E RID: 19550 RVA: 0x0010D855 File Offset: 0x0010BA55
		internal static Expression Emit_Shaper_GetHierarchyIdColumnValue(int ordinal)
		{
			return Expression.Call(CodeGenEmitter.Shaper_Parameter, CodeGenEmitter.Shaper_GetHierarchyIdColumnValue, new Expression[] { Expression.Constant(ordinal) });
		}

		// Token: 0x06004C5F RID: 19551 RVA: 0x0010D87A File Offset: 0x0010BA7A
		internal static Expression Emit_Shaper_GetGeographyColumnValue(int ordinal)
		{
			return Expression.Call(CodeGenEmitter.Shaper_Parameter, CodeGenEmitter.Shaper_GetGeographyColumnValue, new Expression[] { Expression.Constant(ordinal) });
		}

		// Token: 0x06004C60 RID: 19552 RVA: 0x0010D89F File Offset: 0x0010BA9F
		internal static Expression Emit_Shaper_GetGeometryColumnValue(int ordinal)
		{
			return Expression.Call(CodeGenEmitter.Shaper_Parameter, CodeGenEmitter.Shaper_GetGeometryColumnValue, new Expression[] { Expression.Constant(ordinal) });
		}

		// Token: 0x06004C61 RID: 19553 RVA: 0x0010D8C4 File Offset: 0x0010BAC4
		internal static Expression Emit_Shaper_GetState(int stateSlotNumber, Type type)
		{
			return CodeGenEmitter.Emit_EnsureType(Expression.ArrayIndex(CodeGenEmitter.Shaper_State, Expression.Constant(stateSlotNumber)), type);
		}

		// Token: 0x06004C62 RID: 19554 RVA: 0x0010D8E1 File Offset: 0x0010BAE1
		internal static Expression Emit_Shaper_SetState(int stateSlotNumber, Expression value)
		{
			return Expression.Call(CodeGenEmitter.Shaper_Parameter, CodeGenEmitter.Shaper_SetState.MakeGenericMethod(new Type[] { value.Type }), Expression.Constant(stateSlotNumber), value);
		}

		// Token: 0x06004C63 RID: 19555 RVA: 0x0010D912 File Offset: 0x0010BB12
		internal static Expression Emit_Shaper_SetStatePassthrough(int stateSlotNumber, Expression value)
		{
			return Expression.Call(CodeGenEmitter.Shaper_Parameter, CodeGenEmitter.Shaper_SetStatePassthrough.MakeGenericMethod(new Type[] { value.Type }), Expression.Constant(stateSlotNumber), value);
		}

		// Token: 0x04001AD0 RID: 6864
		internal static readonly MethodInfo CodeGenEmitter_BinaryEquals = typeof(CodeGenEmitter).GetOnlyDeclaredMethod("BinaryEquals");

		// Token: 0x04001AD1 RID: 6865
		internal static readonly MethodInfo CodeGenEmitter_CheckedConvert = typeof(CodeGenEmitter).GetOnlyDeclaredMethod("CheckedConvert");

		// Token: 0x04001AD2 RID: 6866
		internal static readonly MethodInfo CodeGenEmitter_Compile = typeof(CodeGenEmitter).GetDeclaredMethod("Compile", new Type[] { typeof(Expression) });

		// Token: 0x04001AD3 RID: 6867
		internal static readonly MethodInfo DbDataReader_GetValue = typeof(DbDataReader).GetOnlyDeclaredMethod("GetValue");

		// Token: 0x04001AD4 RID: 6868
		internal static readonly MethodInfo DbDataReader_GetString = typeof(DbDataReader).GetOnlyDeclaredMethod("GetString");

		// Token: 0x04001AD5 RID: 6869
		internal static readonly MethodInfo DbDataReader_GetInt16 = typeof(DbDataReader).GetOnlyDeclaredMethod("GetInt16");

		// Token: 0x04001AD6 RID: 6870
		internal static readonly MethodInfo DbDataReader_GetInt32 = typeof(DbDataReader).GetOnlyDeclaredMethod("GetInt32");

		// Token: 0x04001AD7 RID: 6871
		internal static readonly MethodInfo DbDataReader_GetInt64 = typeof(DbDataReader).GetOnlyDeclaredMethod("GetInt64");

		// Token: 0x04001AD8 RID: 6872
		internal static readonly MethodInfo DbDataReader_GetBoolean = typeof(DbDataReader).GetOnlyDeclaredMethod("GetBoolean");

		// Token: 0x04001AD9 RID: 6873
		internal static readonly MethodInfo DbDataReader_GetDecimal = typeof(DbDataReader).GetOnlyDeclaredMethod("GetDecimal");

		// Token: 0x04001ADA RID: 6874
		internal static readonly MethodInfo DbDataReader_GetFloat = typeof(DbDataReader).GetOnlyDeclaredMethod("GetFloat");

		// Token: 0x04001ADB RID: 6875
		internal static readonly MethodInfo DbDataReader_GetDouble = typeof(DbDataReader).GetOnlyDeclaredMethod("GetDouble");

		// Token: 0x04001ADC RID: 6876
		internal static readonly MethodInfo DbDataReader_GetDateTime = typeof(DbDataReader).GetOnlyDeclaredMethod("GetDateTime");

		// Token: 0x04001ADD RID: 6877
		internal static readonly MethodInfo DbDataReader_GetGuid = typeof(DbDataReader).GetOnlyDeclaredMethod("GetGuid");

		// Token: 0x04001ADE RID: 6878
		internal static readonly MethodInfo DbDataReader_GetByte = typeof(DbDataReader).GetOnlyDeclaredMethod("GetByte");

		// Token: 0x04001ADF RID: 6879
		internal static readonly MethodInfo DbDataReader_IsDBNull = typeof(DbDataReader).GetOnlyDeclaredMethod("IsDBNull");

		// Token: 0x04001AE0 RID: 6880
		internal static readonly ConstructorInfo EntityKey_ctor_SingleKey = typeof(EntityKey).GetDeclaredConstructor(new Type[]
		{
			typeof(EntitySetBase),
			typeof(object)
		});

		// Token: 0x04001AE1 RID: 6881
		internal static readonly ConstructorInfo EntityKey_ctor_CompositeKey = typeof(EntityKey).GetDeclaredConstructor(new Type[]
		{
			typeof(EntitySetBase),
			typeof(object[])
		});

		// Token: 0x04001AE2 RID: 6882
		internal static readonly MethodInfo EntityWrapperFactory_GetEntityWithChangeTrackerStrategyFunc = typeof(EntityWrapperFactory).GetOnlyDeclaredMethod("GetEntityWithChangeTrackerStrategyFunc");

		// Token: 0x04001AE3 RID: 6883
		internal static readonly MethodInfo EntityWrapperFactory_GetEntityWithKeyStrategyStrategyFunc = typeof(EntityWrapperFactory).GetOnlyDeclaredMethod("GetEntityWithKeyStrategyStrategyFunc");

		// Token: 0x04001AE4 RID: 6884
		internal static readonly MethodInfo EntityProxyTypeInfo_SetEntityWrapper = typeof(EntityProxyTypeInfo).GetOnlyDeclaredMethod("SetEntityWrapper");

		// Token: 0x04001AE5 RID: 6885
		internal static readonly MethodInfo EntityWrapperFactory_GetNullPropertyAccessorStrategyFunc = typeof(EntityWrapperFactory).GetOnlyDeclaredMethod("GetNullPropertyAccessorStrategyFunc");

		// Token: 0x04001AE6 RID: 6886
		internal static readonly MethodInfo EntityWrapperFactory_GetPocoEntityKeyStrategyFunc = typeof(EntityWrapperFactory).GetOnlyDeclaredMethod("GetPocoEntityKeyStrategyFunc");

		// Token: 0x04001AE7 RID: 6887
		internal static readonly MethodInfo EntityWrapperFactory_GetPocoPropertyAccessorStrategyFunc = typeof(EntityWrapperFactory).GetOnlyDeclaredMethod("GetPocoPropertyAccessorStrategyFunc");

		// Token: 0x04001AE8 RID: 6888
		internal static readonly MethodInfo EntityWrapperFactory_GetSnapshotChangeTrackingStrategyFunc = typeof(EntityWrapperFactory).GetOnlyDeclaredMethod("GetSnapshotChangeTrackingStrategyFunc");

		// Token: 0x04001AE9 RID: 6889
		internal static readonly PropertyInfo EntityWrapperFactory_NullWrapper = typeof(NullEntityWrapper).GetDeclaredProperty("NullWrapper");

		// Token: 0x04001AEA RID: 6890
		internal static readonly PropertyInfo IEntityWrapper_Entity = typeof(IEntityWrapper).GetDeclaredProperty("Entity");

		// Token: 0x04001AEB RID: 6891
		internal static readonly MethodInfo IEqualityComparerOfString_Equals = typeof(IEqualityComparer<string>).GetDeclaredMethod("Equals", new Type[]
		{
			typeof(string),
			typeof(string)
		});

		// Token: 0x04001AEC RID: 6892
		internal static readonly ConstructorInfo MaterializedDataRecord_ctor = typeof(MaterializedDataRecord).GetDeclaredConstructor(new Type[]
		{
			typeof(MetadataWorkspace),
			typeof(TypeUsage),
			typeof(object[])
		});

		// Token: 0x04001AED RID: 6893
		internal static readonly MethodInfo RecordState_GatherData = typeof(RecordState).GetOnlyDeclaredMethod("GatherData");

		// Token: 0x04001AEE RID: 6894
		internal static readonly MethodInfo RecordState_SetNullRecord = typeof(RecordState).GetOnlyDeclaredMethod("SetNullRecord");

		// Token: 0x04001AEF RID: 6895
		internal static readonly MethodInfo Shaper_Discriminate = typeof(Shaper).GetOnlyDeclaredMethod("Discriminate");

		// Token: 0x04001AF0 RID: 6896
		internal static readonly MethodInfo Shaper_GetPropertyValueWithErrorHandling = typeof(Shaper).GetOnlyDeclaredMethod("GetPropertyValueWithErrorHandling");

		// Token: 0x04001AF1 RID: 6897
		internal static readonly MethodInfo Shaper_GetColumnValueWithErrorHandling = typeof(Shaper).GetOnlyDeclaredMethod("GetColumnValueWithErrorHandling");

		// Token: 0x04001AF2 RID: 6898
		internal static readonly MethodInfo Shaper_GetHierarchyIdColumnValue = typeof(Shaper).GetOnlyDeclaredMethod("GetHierarchyIdColumnValue");

		// Token: 0x04001AF3 RID: 6899
		internal static readonly MethodInfo Shaper_GetGeographyColumnValue = typeof(Shaper).GetOnlyDeclaredMethod("GetGeographyColumnValue");

		// Token: 0x04001AF4 RID: 6900
		internal static readonly MethodInfo Shaper_GetGeometryColumnValue = typeof(Shaper).GetOnlyDeclaredMethod("GetGeometryColumnValue");

		// Token: 0x04001AF5 RID: 6901
		internal static readonly MethodInfo Shaper_GetSpatialColumnValueWithErrorHandling = typeof(Shaper).GetOnlyDeclaredMethod("GetSpatialColumnValueWithErrorHandling");

		// Token: 0x04001AF6 RID: 6902
		internal static readonly MethodInfo Shaper_GetSpatialPropertyValueWithErrorHandling = typeof(Shaper).GetOnlyDeclaredMethod("GetSpatialPropertyValueWithErrorHandling");

		// Token: 0x04001AF7 RID: 6903
		internal static readonly MethodInfo Shaper_HandleEntity = typeof(Shaper).GetOnlyDeclaredMethod("HandleEntity");

		// Token: 0x04001AF8 RID: 6904
		internal static readonly MethodInfo Shaper_HandleEntityAppendOnly = typeof(Shaper).GetOnlyDeclaredMethod("HandleEntityAppendOnly");

		// Token: 0x04001AF9 RID: 6905
		internal static readonly MethodInfo Shaper_HandleEntityNoTracking = typeof(Shaper).GetOnlyDeclaredMethod("HandleEntityNoTracking");

		// Token: 0x04001AFA RID: 6906
		internal static readonly MethodInfo Shaper_HandleFullSpanCollection = typeof(Shaper).GetOnlyDeclaredMethod("HandleFullSpanCollection");

		// Token: 0x04001AFB RID: 6907
		internal static readonly MethodInfo Shaper_HandleFullSpanElement = typeof(Shaper).GetOnlyDeclaredMethod("HandleFullSpanElement");

		// Token: 0x04001AFC RID: 6908
		internal static readonly MethodInfo Shaper_HandleIEntityWithKey = typeof(Shaper).GetOnlyDeclaredMethod("HandleIEntityWithKey");

		// Token: 0x04001AFD RID: 6909
		internal static readonly MethodInfo Shaper_HandleRelationshipSpan = typeof(Shaper).GetOnlyDeclaredMethod("HandleRelationshipSpan");

		// Token: 0x04001AFE RID: 6910
		internal static readonly MethodInfo Shaper_SetColumnValue = typeof(Shaper).GetOnlyDeclaredMethod("SetColumnValue");

		// Token: 0x04001AFF RID: 6911
		internal static readonly MethodInfo Shaper_SetEntityRecordInfo = typeof(Shaper).GetOnlyDeclaredMethod("SetEntityRecordInfo");

		// Token: 0x04001B00 RID: 6912
		internal static readonly MethodInfo Shaper_SetState = typeof(Shaper).GetOnlyDeclaredMethod("SetState");

		// Token: 0x04001B01 RID: 6913
		internal static readonly MethodInfo Shaper_SetStatePassthrough = typeof(Shaper).GetOnlyDeclaredMethod("SetStatePassthrough");

		// Token: 0x04001B02 RID: 6914
		internal static readonly Expression DBNull_Value = Expression.Constant(DBNull.Value, typeof(object));

		// Token: 0x04001B03 RID: 6915
		internal static readonly ParameterExpression Shaper_Parameter = Expression.Parameter(typeof(Shaper), "shaper");

		// Token: 0x04001B04 RID: 6916
		internal static readonly Expression Shaper_Reader = Expression.Field(CodeGenEmitter.Shaper_Parameter, typeof(Shaper).GetField("Reader"));

		// Token: 0x04001B05 RID: 6917
		internal static readonly Expression Shaper_Workspace = Expression.Field(CodeGenEmitter.Shaper_Parameter, typeof(Shaper).GetField("Workspace"));

		// Token: 0x04001B06 RID: 6918
		internal static readonly Expression Shaper_State = Expression.Field(CodeGenEmitter.Shaper_Parameter, typeof(Shaper).GetField("State"));

		// Token: 0x04001B07 RID: 6919
		internal static readonly Expression Shaper_Context = Expression.Field(CodeGenEmitter.Shaper_Parameter, typeof(Shaper).GetField("Context"));

		// Token: 0x04001B08 RID: 6920
		internal static readonly Expression Shaper_Context_Options = Expression.Property(CodeGenEmitter.Shaper_Context, typeof(ObjectContext).GetDeclaredProperty("ContextOptions"));

		// Token: 0x04001B09 RID: 6921
		internal static readonly Expression Shaper_ProxyCreationEnabled = Expression.Property(CodeGenEmitter.Shaper_Context_Options, typeof(ObjectContextOptions).GetDeclaredProperty("ProxyCreationEnabled"));
	}
}
