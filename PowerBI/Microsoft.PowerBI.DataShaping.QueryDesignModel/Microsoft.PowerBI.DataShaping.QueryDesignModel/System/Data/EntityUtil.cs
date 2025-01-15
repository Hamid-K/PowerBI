using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Security;
using System.Text;
using System.Threading;
using Microsoft.Data;

namespace System.Data
{
	// Token: 0x02000011 RID: 17
	internal static class EntityUtil
	{
		// Token: 0x06000047 RID: 71 RVA: 0x00002CED File Offset: 0x00000EED
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

		// Token: 0x06000048 RID: 72 RVA: 0x00002D04 File Offset: 0x00000F04
		internal static Exception EntitySqlError(string message)
		{
			return new Exception(message);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002D0C File Offset: 0x00000F0C
		internal static MetadataException InvalidSchemaEncountered(string errors)
		{
			return EntityUtil.Metadata(Strings.InvalidSchemaEncountered(errors));
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002D19 File Offset: 0x00000F19
		internal static ArgumentException CollectionParameterElementIsNullOrEmpty(string parameterName)
		{
			return EntityUtil.Argument(Strings.ADP_CollectionParameterElementIsNullOrEmpty(parameterName));
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002D26 File Offset: 0x00000F26
		internal static ArgumentException EntitySetInAnotherContainer(string parameter)
		{
			return EntityUtil.Argument(Strings.EntitySetInAnotherContainer, parameter);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002D33 File Offset: 0x00000F33
		internal static MetadataException Metadata(string message, Exception inner)
		{
			return new MetadataException(message);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002D3B File Offset: 0x00000F3B
		internal static MetadataException Metadata(string message)
		{
			return new MetadataException(message);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002D43 File Offset: 0x00000F43
		internal static InvalidOperationException InvalidOperation(string error)
		{
			return new InvalidOperationException(error);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002D4B File Offset: 0x00000F4B
		internal static InvalidOperationException InvalidOperation(string error, Exception inner)
		{
			return new InvalidOperationException(error, inner);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002D54 File Offset: 0x00000F54
		internal static T CheckArgumentNull<T>(T value, string parameterName) where T : class
		{
			if (value == null)
			{
				EntityUtil.ThrowArgumentNullException(parameterName);
			}
			return value;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002D65 File Offset: 0x00000F65
		internal static T GenericCheckArgumentNull<T>(T value, string parameterName) where T : class
		{
			return EntityUtil.CheckArgumentNull<T>(value, parameterName);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002D6E File Offset: 0x00000F6E
		internal static ArgumentOutOfRangeException InvalidEnumerationValue(Type type, int value)
		{
			return EntityUtil.ArgumentOutOfRange(Strings.ADP_InvalidEnumerationValue(type.Name, value.ToString(CultureInfo.InvariantCulture)), type.Name);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002D92 File Offset: 0x00000F92
		internal static void ThrowArgumentNullException(string parameterName)
		{
			throw EntityUtil.ArgumentNull(parameterName);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002D9C File Offset: 0x00000F9C
		internal static bool IsCatchableExceptionType(Exception e)
		{
			Type type = e.GetType();
			return type != EntityUtil.StackOverflowType && type != EntityUtil.OutOfMemoryType && type != EntityUtil.ThreadAbortType && type != EntityUtil.NullReferenceType && type != EntityUtil.AccessViolationType && !EntityUtil.SecurityType.IsAssignableFrom(type);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002E04 File Offset: 0x00001004
		internal static IEnumerable<T> CheckArgumentContainsNull<T>(ref IEnumerable<T> enumerableArgument, string argumentName) where T : class
		{
			EntityUtil.GetCheapestSafeEnumerableAsCollection<T>(ref enumerableArgument);
			using (IEnumerator<T> enumerator = enumerableArgument.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current == null)
					{
						throw EntityUtil.Argument(Strings.CheckArgumentContainsNullFailed(argumentName));
					}
				}
			}
			return enumerableArgument;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002E64 File Offset: 0x00001064
		internal static IEnumerable<T> CheckArgumentEmpty<T>(ref IEnumerable<T> enumerableArgument, Func<string, string> errorMessage, string argumentName)
		{
			int num;
			EntityUtil.GetCheapestSafeCountOfEnumerable<T>(ref enumerableArgument, out num);
			if (num <= 0)
			{
				throw EntityUtil.Argument(errorMessage(argumentName));
			}
			return enumerableArgument;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002E8C File Offset: 0x0000108C
		private static void GetCheapestSafeCountOfEnumerable<T>(ref IEnumerable<T> enumerable, out int count)
		{
			ICollection<T> cheapestSafeEnumerableAsCollection = EntityUtil.GetCheapestSafeEnumerableAsCollection<T>(ref enumerable);
			count = cheapestSafeEnumerableAsCollection.Count;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002EA8 File Offset: 0x000010A8
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

		// Token: 0x06000059 RID: 89 RVA: 0x00002ED2 File Offset: 0x000010D2
		internal static ArgumentNullException ArgumentNull(string parameter)
		{
			return new ArgumentNullException(parameter);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002EDA File Offset: 0x000010DA
		internal static NotSupportedException NotSupported(string error)
		{
			return new NotSupportedException(error);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002EE2 File Offset: 0x000010E2
		internal static InvalidOperationException OperationOnReadOnlyItem()
		{
			return EntityUtil.InvalidOperation(Strings.OperationOnReadOnlyItem);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002EEE File Offset: 0x000010EE
		internal static void CheckStringArgument(string value, string parameterName)
		{
			EntityUtil.CheckArgumentNull<string>(value, parameterName);
			if (value.Length == 0)
			{
				throw EntityUtil.InvalidStringArgument(parameterName);
			}
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002F07 File Offset: 0x00001107
		internal static ArgumentException InvalidStringArgument(string parameterName)
		{
			return EntityUtil.Argument(Strings.InvalidStringArgument(parameterName), parameterName);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002F15 File Offset: 0x00001115
		internal static ArgumentException Argument(string error, string parameter)
		{
			return new ArgumentException(error, parameter);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002F1E File Offset: 0x0000111E
		internal static ArgumentException Argument(string error)
		{
			return new ArgumentException(error);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002F26 File Offset: 0x00001126
		internal static void ThrowArgumentOutOfRangeException(string parameterName)
		{
			throw EntityUtil.ArgumentOutOfRange(parameterName);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002F2E File Offset: 0x0000112E
		internal static T CheckArgumentOutOfRange<T>(T[] values, int index, string parameterName)
		{
			if (values.Length <= index)
			{
				EntityUtil.ThrowArgumentOutOfRangeException(parameterName);
			}
			return values[index];
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002F43 File Offset: 0x00001143
		internal static ArgumentException InvalidRelationshipSetName(string name)
		{
			return EntityUtil.Argument(Strings.InvalidRelationshipSetName(name));
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002F50 File Offset: 0x00001150
		internal static ArgumentException MinAndMaxValueMustBeSameForConstantFacet(string facetName, string typeName)
		{
			return EntityUtil.Argument(Strings.MinAndMaxValueMustBeSameForConstantFacet(facetName, typeName));
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00002F5E File Offset: 0x0000115E
		internal static ArgumentException MissingDefaultValueForConstantFacet(string facetName, string typeName)
		{
			return EntityUtil.Argument(Strings.MissingDefaultValueForConstantFacet(facetName, typeName));
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00002F6C File Offset: 0x0000116C
		internal static ArgumentException BothMinAndMaxValueMustBeSpecifiedForNonConstantFacet(string facetName, string typeName)
		{
			return EntityUtil.Argument(Strings.BothMinAndMaxValueMustBeSpecifiedForNonConstantFacet(facetName, typeName));
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00002F7A File Offset: 0x0000117A
		internal static ArgumentException MinAndMaxValueMustBeDifferentForNonConstantFacet(string facetName, string typeName)
		{
			return EntityUtil.Argument(Strings.MinAndMaxValueMustBeDifferentForNonConstantFacet(facetName, typeName));
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00002F88 File Offset: 0x00001188
		internal static ArgumentException MinAndMaxMustBePositive(string facetName, string typeName)
		{
			return EntityUtil.Argument(Strings.MinAndMaxMustBePositive(facetName, typeName));
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00002F96 File Offset: 0x00001196
		internal static ArgumentException MinMustBeLessThanMax(string minimumValue, string facetName, string typeName)
		{
			return EntityUtil.Argument(Strings.MinMustBeLessThanMax(minimumValue, facetName, typeName));
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00002FA5 File Offset: 0x000011A5
		internal static Exception InternalError(EntityUtil.InternalErrorCode internalError)
		{
			return EntityUtil.InvalidOperation(Strings.ADP_InternalProviderError((int)internalError));
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00002FB8 File Offset: 0x000011B8
		internal static Exception InternalError(EntityUtil.InternalErrorCode internalError, int location, object additionalInfo)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "{0}, {1}", (int)internalError, location);
			if (additionalInfo != null)
			{
				stringBuilder.AppendFormat(CultureInfo.InvariantCulture, ", {0}", additionalInfo);
			}
			return EntityUtil.InvalidOperation(Strings.ADP_InternalProviderError(stringBuilder.ToString()));
		}

		// Token: 0x0600006B RID: 107 RVA: 0x0000300D File Offset: 0x0000120D
		internal static Exception InternalError(EntityUtil.InternalErrorCode internalError, int location)
		{
			return EntityUtil.InternalError(internalError, location, null);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003017 File Offset: 0x00001217
		internal static ArgumentException NotBinaryTypeForTypeUsage()
		{
			return EntityUtil.Argument(Strings.NotBinaryTypeForTypeUsage);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003023 File Offset: 0x00001223
		internal static ArgumentException NotDateTimeTypeForTypeUsage()
		{
			return EntityUtil.Argument(Strings.NotDateTimeTypeForTypeUsage);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x0000302F File Offset: 0x0000122F
		internal static ArgumentException NotDateTimeOffsetTypeForTypeUsage()
		{
			return EntityUtil.Argument(Strings.NotDateTimeOffsetTypeForTypeUsage);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x0000303B File Offset: 0x0000123B
		internal static ArgumentException NotTimeTypeForTypeUsage()
		{
			return EntityUtil.Argument(Strings.NotTimeTypeForTypeUsage);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003047 File Offset: 0x00001247
		internal static ArgumentException NotDecimalTypeForTypeUsage()
		{
			return EntityUtil.Argument(Strings.NotDecimalTypeForTypeUsage);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003053 File Offset: 0x00001253
		internal static ArgumentException NotStringTypeForTypeUsage()
		{
			return EntityUtil.Argument(Strings.NotStringTypeForTypeUsage);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x0000305F File Offset: 0x0000125F
		internal static ArgumentOutOfRangeException ArgumentOutOfRange(string message, string parameterName)
		{
			return new ArgumentOutOfRangeException(parameterName, message);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003068 File Offset: 0x00001268
		internal static NotSupportedException NotSupported()
		{
			return new NotSupportedException();
		}

		// Token: 0x06000074 RID: 116 RVA: 0x0000306F File Offset: 0x0000126F
		internal static ArgumentException CollectionParameterElementIsNull(string parameterName)
		{
			return EntityUtil.Argument(Strings.ADP_CollectionParameterElementIsNull(parameterName));
		}

		// Token: 0x06000075 RID: 117 RVA: 0x0000307C File Offset: 0x0000127C
		internal static InvalidOperationException OperationOnReadOnlyCollection()
		{
			return EntityUtil.InvalidOperation(Strings.OperationOnReadOnlyCollection);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003088 File Offset: 0x00001288
		internal static ArgumentException ItemInvalidIdentity(string identity, string parameter)
		{
			return EntityUtil.Argument(Strings.ItemInvalidIdentity(identity), parameter);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003096 File Offset: 0x00001296
		internal static ArgumentException ItemDuplicateIdentity(string identity, string parameter, Exception inner)
		{
			return EntityUtil.Argument(Strings.ItemDuplicateIdentity(identity), parameter, inner);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000030A5 File Offset: 0x000012A5
		internal static ArgumentException Argument(string error, string parameter, Exception inner)
		{
			return new ArgumentException(error, inner);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000030AE File Offset: 0x000012AE
		internal static InvalidOperationException MoreThanOneItemMatchesIdentity(string identity)
		{
			return EntityUtil.InvalidOperation(Strings.MoreThanOneItemMatchesIdentity(identity));
		}

		// Token: 0x0600007A RID: 122 RVA: 0x000030BB File Offset: 0x000012BB
		internal static ArgumentOutOfRangeException ArgumentOutOfRange(string parameterName)
		{
			return new ArgumentOutOfRangeException(parameterName);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x000030C3 File Offset: 0x000012C3
		internal static ArgumentException ArrayTooSmall(string parameter)
		{
			return EntityUtil.Argument(Strings.ArrayTooSmall, parameter);
		}

		// Token: 0x04000040 RID: 64
		private static readonly Type StackOverflowType = typeof(StackOverflowException);

		// Token: 0x04000041 RID: 65
		private static readonly Type OutOfMemoryType = typeof(OutOfMemoryException);

		// Token: 0x04000042 RID: 66
		private static readonly Type ThreadAbortType = typeof(ThreadAbortException);

		// Token: 0x04000043 RID: 67
		private static readonly Type NullReferenceType = typeof(NullReferenceException);

		// Token: 0x04000044 RID: 68
		private static readonly Type AccessViolationType = typeof(AccessViolationException);

		// Token: 0x04000045 RID: 69
		private static readonly Type SecurityType = typeof(SecurityException);

		// Token: 0x02000294 RID: 660
		internal enum InternalErrorCode
		{
			// Token: 0x04000F3D RID: 3901
			WrongNumberOfKeys = 1000,
			// Token: 0x04000F3E RID: 3902
			UnknownColumnMapKind,
			// Token: 0x04000F3F RID: 3903
			NestOverNest,
			// Token: 0x04000F40 RID: 3904
			ColumnCountMismatch,
			// Token: 0x04000F41 RID: 3905
			AssertionFailed,
			// Token: 0x04000F42 RID: 3906
			UnknownVar,
			// Token: 0x04000F43 RID: 3907
			WrongVarType,
			// Token: 0x04000F44 RID: 3908
			ExtentWithoutEntity,
			// Token: 0x04000F45 RID: 3909
			UnnestWithoutInput,
			// Token: 0x04000F46 RID: 3910
			UnnestMultipleCollections,
			// Token: 0x04000F47 RID: 3911
			CodeGen_NoSuchProperty = 1011,
			// Token: 0x04000F48 RID: 3912
			JoinOverSingleStreamNest,
			// Token: 0x04000F49 RID: 3913
			InvalidInternalTree,
			// Token: 0x04000F4A RID: 3914
			NameValuePairNext,
			// Token: 0x04000F4B RID: 3915
			InvalidParserState1,
			// Token: 0x04000F4C RID: 3916
			InvalidParserState2,
			// Token: 0x04000F4D RID: 3917
			SqlGenParametersNotPermitted,
			// Token: 0x04000F4E RID: 3918
			EntityKeyMissingKeyValue,
			// Token: 0x04000F4F RID: 3919
			UpdatePipelineResultRequestInvalid,
			// Token: 0x04000F50 RID: 3920
			InvalidStateEntry,
			// Token: 0x04000F51 RID: 3921
			InvalidPrimitiveTypeKind,
			// Token: 0x04000F52 RID: 3922
			UnknownLinqNodeType = 1023,
			// Token: 0x04000F53 RID: 3923
			CollectionWithNoColumns,
			// Token: 0x04000F54 RID: 3924
			UnexpectedLinqLambdaExpressionFormat,
			// Token: 0x04000F55 RID: 3925
			CommandTreeOnStoredProcedureEntityCommand,
			// Token: 0x04000F56 RID: 3926
			BoolExprAssert,
			// Token: 0x04000F57 RID: 3927
			AttemptToGenerateDefinitionForFunctionWithoutDef,
			// Token: 0x04000F58 RID: 3928
			FailedToGeneratePromotionRank
		}
	}
}
