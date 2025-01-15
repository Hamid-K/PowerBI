using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Core.Objects.Internal;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Data.SqlTypes;
using System.Globalization;
using System.Linq;
using System.Text;

namespace System.Data.Entity.Core
{
	// Token: 0x020002CB RID: 715
	internal static class EntityUtil
	{
		// Token: 0x06002289 RID: 8841 RVA: 0x000617A7 File Offset: 0x0005F9A7
		internal static IEnumerable<KeyValuePair<T1, T2>> Zip<T1, T2>(this IEnumerable<T1> first, IEnumerable<T2> second)
		{
			if (first == null || second == null)
			{
				yield break;
			}
			using (IEnumerator<T1> firstEnumerator = first.GetEnumerator())
			{
				using (IEnumerator<T2> secondEnumerator = second.GetEnumerator())
				{
					while (firstEnumerator.MoveNext() && secondEnumerator.MoveNext())
					{
						yield return new KeyValuePair<T1, T2>(firstEnumerator.Current, secondEnumerator.Current);
					}
				}
				IEnumerator<T2> secondEnumerator = null;
			}
			IEnumerator<T1> firstEnumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0600228A RID: 8842 RVA: 0x000617BE File Offset: 0x0005F9BE
		internal static bool IsAnICollection(Type type)
		{
			return typeof(ICollection<>).IsAssignableFrom(type.GetGenericTypeDefinition()) || type.GetInterface(typeof(ICollection<>).FullName) != null;
		}

		// Token: 0x0600228B RID: 8843 RVA: 0x000617F4 File Offset: 0x0005F9F4
		internal static Type GetCollectionElementType(Type propertyType)
		{
			Type type = propertyType.TryGetElementType(typeof(ICollection<>));
			if (type == null)
			{
				throw new InvalidOperationException(Strings.PocoEntityWrapper_UnexpectedTypeForNavigationProperty(propertyType.FullName, typeof(ICollection<>)));
			}
			return type;
		}

		// Token: 0x0600228C RID: 8844 RVA: 0x0006182C File Offset: 0x0005FA2C
		internal static Type DetermineCollectionType(Type requestedType)
		{
			Type collectionElementType = EntityUtil.GetCollectionElementType(requestedType);
			if (requestedType.IsArray)
			{
				throw new InvalidOperationException(Strings.ObjectQuery_UnableToMaterializeArray(requestedType, typeof(List<>).MakeGenericType(new Type[] { collectionElementType })));
			}
			if (!requestedType.IsAbstract() && requestedType.GetPublicConstructor(new Type[0]) != null)
			{
				return requestedType;
			}
			Type type = typeof(HashSet<>).MakeGenericType(new Type[] { collectionElementType });
			if (requestedType.IsAssignableFrom(type))
			{
				return type;
			}
			Type type2 = typeof(List<>).MakeGenericType(new Type[] { collectionElementType });
			if (requestedType.IsAssignableFrom(type2))
			{
				return type2;
			}
			return null;
		}

		// Token: 0x0600228D RID: 8845 RVA: 0x000618D6 File Offset: 0x0005FAD6
		internal static Type GetEntityIdentityType(Type entityType)
		{
			if (!EntityProxyFactory.IsProxyType(entityType))
			{
				return entityType;
			}
			return entityType.BaseType();
		}

		// Token: 0x0600228E RID: 8846 RVA: 0x000618E8 File Offset: 0x0005FAE8
		internal static string QuoteIdentifier(string identifier)
		{
			return "[" + identifier.Replace("]", "]]") + "]";
		}

		// Token: 0x0600228F RID: 8847 RVA: 0x00061909 File Offset: 0x0005FB09
		internal static MetadataException InvalidSchemaEncountered(string errors)
		{
			return new MetadataException(string.Format(CultureInfo.CurrentCulture, EntityRes.GetString("InvalidSchemaEncountered"), new object[] { errors }));
		}

		// Token: 0x06002290 RID: 8848 RVA: 0x00061930 File Offset: 0x0005FB30
		internal static Exception InternalError(EntityUtil.InternalErrorCode internalError, int location, object additionalInfo)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("{0}, {1}", (int)internalError, location);
			if (additionalInfo != null)
			{
				stringBuilder.AppendFormat(", {0}", additionalInfo);
			}
			return new InvalidOperationException(Strings.ADP_InternalProviderError(stringBuilder.ToString()));
		}

		// Token: 0x06002291 RID: 8849 RVA: 0x0006197B File Offset: 0x0005FB7B
		internal static void CheckValidStateForChangeEntityState(EntityState state)
		{
			if (state <= EntityState.Added)
			{
				if (state - EntityState.Detached <= 1 || state == EntityState.Added)
				{
					return;
				}
			}
			else if (state == EntityState.Deleted || state == EntityState.Modified)
			{
				return;
			}
			throw new ArgumentException(Strings.ObjectContext_InvalidEntityState, "state");
		}

		// Token: 0x06002292 RID: 8850 RVA: 0x000619A6 File Offset: 0x0005FBA6
		internal static void CheckValidStateForChangeRelationshipState(EntityState state, string paramName)
		{
			if (state - EntityState.Detached > 1 && state != EntityState.Added && state != EntityState.Deleted)
			{
				throw new ArgumentException(Strings.ObjectContext_InvalidRelationshipState, paramName);
			}
		}

		// Token: 0x06002293 RID: 8851 RVA: 0x000619C2 File Offset: 0x0005FBC2
		internal static void ThrowPropertyIsNotNullable(string propertyName)
		{
			if (string.IsNullOrEmpty(propertyName))
			{
				throw new ConstraintException(Strings.Materializer_PropertyIsNotNullable);
			}
			throw new PropertyConstraintException(Strings.Materializer_PropertyIsNotNullableWithName(propertyName), propertyName);
		}

		// Token: 0x06002294 RID: 8852 RVA: 0x000619E4 File Offset: 0x0005FBE4
		internal static void ThrowSetInvalidValue(object value, Type destinationType, string className, string propertyName)
		{
			if (value == null)
			{
				throw new ConstraintException(Strings.Materializer_SetInvalidValue((Nullable.GetUnderlyingType(destinationType) ?? destinationType).Name, className, propertyName, "null"));
			}
			throw new InvalidOperationException(Strings.Materializer_SetInvalidValue((Nullable.GetUnderlyingType(destinationType) ?? destinationType).Name, className, propertyName, value.GetType().Name));
		}

		// Token: 0x06002295 RID: 8853 RVA: 0x00061A40 File Offset: 0x0005FC40
		internal static InvalidOperationException ValueInvalidCast(Type valueType, Type destinationType)
		{
			if (destinationType.IsValueType() && destinationType.IsGenericType() && typeof(Nullable<>) == destinationType.GetGenericTypeDefinition())
			{
				return new InvalidOperationException(Strings.Materializer_InvalidCastNullable(valueType, destinationType.GetGenericArguments()[0]));
			}
			return new InvalidOperationException(Strings.Materializer_InvalidCastReference(valueType, destinationType));
		}

		// Token: 0x06002296 RID: 8854 RVA: 0x00061A94 File Offset: 0x0005FC94
		internal static void CheckArgumentMergeOption(MergeOption mergeOption)
		{
			if (mergeOption > MergeOption.NoTracking)
			{
				string name = typeof(MergeOption).Name;
				object name2 = typeof(MergeOption).Name;
				int num = (int)mergeOption;
				throw new ArgumentOutOfRangeException(name, Strings.ADP_InvalidEnumerationValue(name2, num.ToString(CultureInfo.InvariantCulture)));
			}
		}

		// Token: 0x06002297 RID: 8855 RVA: 0x00061ADC File Offset: 0x0005FCDC
		internal static void CheckArgumentRefreshMode(RefreshMode refreshMode)
		{
			if (refreshMode != RefreshMode.ClientWins && refreshMode != RefreshMode.StoreWins)
			{
				string name = typeof(RefreshMode).Name;
				object name2 = typeof(RefreshMode).Name;
				int num = (int)refreshMode;
				throw new ArgumentOutOfRangeException(name, Strings.ADP_InvalidEnumerationValue(name2, num.ToString(CultureInfo.InvariantCulture)));
			}
		}

		// Token: 0x06002298 RID: 8856 RVA: 0x00061B28 File Offset: 0x0005FD28
		internal static InvalidOperationException ExecuteFunctionCalledWithNonReaderFunction(EdmFunction functionImport)
		{
			string text;
			if (functionImport.ReturnParameter == null)
			{
				text = Strings.ObjectContext_ExecuteFunctionCalledWithNonQueryFunction(functionImport.Name);
			}
			else
			{
				text = Strings.ObjectContext_ExecuteFunctionCalledWithScalarFunction(functionImport.ReturnParameter.TypeUsage.EdmType.FullName, functionImport.Name);
			}
			return new InvalidOperationException(text);
		}

		// Token: 0x06002299 RID: 8857 RVA: 0x00061B72 File Offset: 0x0005FD72
		internal static void ValidateEntitySetInKey(EntityKey key, EntitySet entitySet)
		{
			EntityUtil.ValidateEntitySetInKey(key, entitySet, null);
		}

		// Token: 0x0600229A RID: 8858 RVA: 0x00061B7C File Offset: 0x0005FD7C
		internal static void ValidateEntitySetInKey(EntityKey key, EntitySet entitySet, string argument)
		{
			string entityContainerName = key.EntityContainerName;
			string entitySetName = key.EntitySetName;
			string name = entitySet.EntityContainer.Name;
			string name2 = entitySet.Name;
			if (StringComparer.Ordinal.Equals(entityContainerName, name) && StringComparer.Ordinal.Equals(entitySetName, name2))
			{
				return;
			}
			if (string.IsNullOrEmpty(argument))
			{
				throw new InvalidOperationException(Strings.ObjectContext_InvalidEntitySetInKey(entityContainerName, entitySetName, name, name2));
			}
			throw new InvalidOperationException(Strings.ObjectContext_InvalidEntitySetInKeyFromName(entityContainerName, entitySetName, name, name2, argument));
		}

		// Token: 0x0600229B RID: 8859 RVA: 0x00061BED File Offset: 0x0005FDED
		internal static void ValidateNecessaryModificationFunctionMapping(ModificationFunctionMapping mapping, string currentState, IEntityStateEntry stateEntry, string type, string typeName)
		{
			if (mapping == null)
			{
				throw new UpdateException(Strings.Update_MissingFunctionMapping(currentState, type, typeName), null, new List<IEntityStateEntry> { stateEntry }.Cast<ObjectStateEntry>().Distinct<ObjectStateEntry>());
			}
		}

		// Token: 0x0600229C RID: 8860 RVA: 0x00061C18 File Offset: 0x0005FE18
		internal static UpdateException Update(string message, Exception innerException, params IEntityStateEntry[] stateEntries)
		{
			return new UpdateException(message, innerException, stateEntries.Cast<ObjectStateEntry>().Distinct<ObjectStateEntry>());
		}

		// Token: 0x0600229D RID: 8861 RVA: 0x00061C2C File Offset: 0x0005FE2C
		internal static UpdateException UpdateRelationshipCardinalityConstraintViolation(string relationshipSetName, int minimumCount, int? maximumCount, string entitySetName, int actualCount, string otherEndPluralName, IEntityStateEntry stateEntry)
		{
			string text = EntityUtil.ConvertCardinalityToString(new int?(minimumCount));
			string text2 = EntityUtil.ConvertCardinalityToString(maximumCount);
			string text3 = EntityUtil.ConvertCardinalityToString(new int?(actualCount));
			if (minimumCount == 1 && text == text2)
			{
				return EntityUtil.Update(Strings.Update_RelationshipCardinalityConstraintViolationSingleValue(entitySetName, relationshipSetName, text3, otherEndPluralName, text), null, new IEntityStateEntry[] { stateEntry });
			}
			return EntityUtil.Update(Strings.Update_RelationshipCardinalityConstraintViolation(entitySetName, relationshipSetName, text3, otherEndPluralName, text, text2), null, new IEntityStateEntry[] { stateEntry });
		}

		// Token: 0x0600229E RID: 8862 RVA: 0x00061CA0 File Offset: 0x0005FEA0
		private static string ConvertCardinalityToString(int? cardinality)
		{
			if (cardinality != null)
			{
				return cardinality.Value.ToString(CultureInfo.CurrentCulture);
			}
			return "*";
		}

		// Token: 0x0600229F RID: 8863 RVA: 0x00061CD0 File Offset: 0x0005FED0
		internal static T CheckArgumentOutOfRange<T>(T[] values, int index, string parameterName)
		{
			if (values.Length <= index)
			{
				throw new ArgumentOutOfRangeException(parameterName);
			}
			return values[index];
		}

		// Token: 0x060022A0 RID: 8864 RVA: 0x00061CE8 File Offset: 0x0005FEE8
		internal static IEnumerable<T> CheckArgumentContainsNull<T>(ref IEnumerable<T> enumerableArgument, string argumentName) where T : class
		{
			EntityUtil.GetCheapestSafeEnumerableAsCollection<T>(ref enumerableArgument);
			using (IEnumerator<T> enumerator = enumerableArgument.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current == null)
					{
						throw new ArgumentException(Strings.CheckArgumentContainsNullFailed(argumentName));
					}
				}
			}
			return enumerableArgument;
		}

		// Token: 0x060022A1 RID: 8865 RVA: 0x00061D48 File Offset: 0x0005FF48
		internal static IEnumerable<T> CheckArgumentEmpty<T>(ref IEnumerable<T> enumerableArgument, Func<string, string> errorMessage, string argumentName)
		{
			int num;
			EntityUtil.GetCheapestSafeCountOfEnumerable<T>(ref enumerableArgument, out num);
			if (num <= 0)
			{
				throw new ArgumentException(errorMessage(argumentName));
			}
			return enumerableArgument;
		}

		// Token: 0x060022A2 RID: 8866 RVA: 0x00061D70 File Offset: 0x0005FF70
		private static void GetCheapestSafeCountOfEnumerable<T>(ref IEnumerable<T> enumerable, out int count)
		{
			ICollection<T> cheapestSafeEnumerableAsCollection = EntityUtil.GetCheapestSafeEnumerableAsCollection<T>(ref enumerable);
			count = cheapestSafeEnumerableAsCollection.Count;
		}

		// Token: 0x060022A3 RID: 8867 RVA: 0x00061D8C File Offset: 0x0005FF8C
		private static ICollection<T> GetCheapestSafeEnumerableAsCollection<T>(ref IEnumerable<T> enumerable)
		{
			ICollection<T> collection = enumerable as ICollection<T>;
			if (collection != null)
			{
				return collection;
			}
			enumerable = new List<T>(enumerable);
			return enumerable as ICollection<T>;
		}

		// Token: 0x060022A4 RID: 8868 RVA: 0x00061DB8 File Offset: 0x0005FFB8
		internal static bool IsNull(object value)
		{
			if (value == null || DBNull.Value == value)
			{
				return true;
			}
			INullable nullable = value as INullable;
			return nullable != null && nullable.IsNull;
		}

		// Token: 0x060022A5 RID: 8869 RVA: 0x00061DE4 File Offset: 0x0005FFE4
		internal static int SrcCompare(string strA, string strB)
		{
			if (!(strA == strB))
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x060022A6 RID: 8870 RVA: 0x00061DF2 File Offset: 0x0005FFF2
		internal static int DstCompare(string strA, string strB)
		{
			return CultureInfo.CurrentCulture.CompareInfo.Compare(strA, strB, CompareOptions.IgnoreCase | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth);
		}

		// Token: 0x04000BEE RID: 3054
		internal const int AssemblyQualifiedNameIndex = 3;

		// Token: 0x04000BEF RID: 3055
		internal const int InvariantNameIndex = 2;

		// Token: 0x04000BF0 RID: 3056
		internal const string Parameter = "Parameter";

		// Token: 0x04000BF1 RID: 3057
		internal const CompareOptions StringCompareOptions = CompareOptions.IgnoreCase | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth;

		// Token: 0x04000BF2 RID: 3058
		internal static Dictionary<string, string> COMPILER_VERSION = new Dictionary<string, string> { { "CompilerVersion", "V3.5" } };

		// Token: 0x020009AE RID: 2478
		internal enum InternalErrorCode
		{
			// Token: 0x040027C6 RID: 10182
			WrongNumberOfKeys = 1000,
			// Token: 0x040027C7 RID: 10183
			UnknownColumnMapKind,
			// Token: 0x040027C8 RID: 10184
			NestOverNest,
			// Token: 0x040027C9 RID: 10185
			ColumnCountMismatch,
			// Token: 0x040027CA RID: 10186
			AssertionFailed,
			// Token: 0x040027CB RID: 10187
			UnknownVar,
			// Token: 0x040027CC RID: 10188
			WrongVarType,
			// Token: 0x040027CD RID: 10189
			ExtentWithoutEntity,
			// Token: 0x040027CE RID: 10190
			UnnestWithoutInput,
			// Token: 0x040027CF RID: 10191
			UnnestMultipleCollections,
			// Token: 0x040027D0 RID: 10192
			CodeGen_NoSuchProperty = 1011,
			// Token: 0x040027D1 RID: 10193
			JoinOverSingleStreamNest,
			// Token: 0x040027D2 RID: 10194
			InvalidInternalTree,
			// Token: 0x040027D3 RID: 10195
			NameValuePairNext,
			// Token: 0x040027D4 RID: 10196
			InvalidParserState1,
			// Token: 0x040027D5 RID: 10197
			InvalidParserState2,
			// Token: 0x040027D6 RID: 10198
			SqlGenParametersNotPermitted,
			// Token: 0x040027D7 RID: 10199
			EntityKeyMissingKeyValue,
			// Token: 0x040027D8 RID: 10200
			UpdatePipelineResultRequestInvalid,
			// Token: 0x040027D9 RID: 10201
			InvalidStateEntry,
			// Token: 0x040027DA RID: 10202
			InvalidPrimitiveTypeKind,
			// Token: 0x040027DB RID: 10203
			UnknownLinqNodeType = 1023,
			// Token: 0x040027DC RID: 10204
			CollectionWithNoColumns,
			// Token: 0x040027DD RID: 10205
			UnexpectedLinqLambdaExpressionFormat,
			// Token: 0x040027DE RID: 10206
			CommandTreeOnStoredProcedureEntityCommand,
			// Token: 0x040027DF RID: 10207
			BoolExprAssert,
			// Token: 0x040027E0 RID: 10208
			FailedToGeneratePromotionRank = 1029
		}
	}
}
