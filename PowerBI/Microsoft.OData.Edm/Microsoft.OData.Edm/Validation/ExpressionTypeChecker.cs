using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Validation
{
	// Token: 0x02000141 RID: 321
	public static class ExpressionTypeChecker
	{
		// Token: 0x0600080F RID: 2063 RVA: 0x00013C5A File Offset: 0x00011E5A
		public static bool TryCast(this IEdmExpression expression, IEdmTypeReference type, out IEnumerable<EdmError> discoveredErrors)
		{
			return expression.TryCast(type, null, false, out discoveredErrors);
		}

		// Token: 0x06000810 RID: 2064 RVA: 0x00013C68 File Offset: 0x00011E68
		public static bool TryCast(this IEdmExpression expression, IEdmTypeReference type, IEdmType context, bool matchExactly, out IEnumerable<EdmError> discoveredErrors)
		{
			EdmUtil.CheckArgumentNull<IEdmExpression>(expression, "expression");
			type = type.AsActualTypeReference();
			if (type == null || type.TypeKind() == EdmTypeKind.None)
			{
				discoveredErrors = Enumerable.Empty<EdmError>();
				return true;
			}
			switch (expression.ExpressionKind)
			{
			case EdmExpressionKind.BinaryConstant:
			case EdmExpressionKind.BooleanConstant:
			case EdmExpressionKind.DateTimeOffsetConstant:
			case EdmExpressionKind.DecimalConstant:
			case EdmExpressionKind.FloatingConstant:
			case EdmExpressionKind.GuidConstant:
			case EdmExpressionKind.IntegerConstant:
			case EdmExpressionKind.StringConstant:
			case EdmExpressionKind.DurationConstant:
			case EdmExpressionKind.DateConstant:
			case EdmExpressionKind.TimeOfDayConstant:
			{
				IEdmPrimitiveValue edmPrimitiveValue = (IEdmPrimitiveValue)expression;
				if (edmPrimitiveValue.Type != null)
				{
					return edmPrimitiveValue.Type.TestTypeReferenceMatch(type, expression.Location(), matchExactly, out discoveredErrors);
				}
				return edmPrimitiveValue.TryCastPrimitiveAsType(type, out discoveredErrors);
			}
			case EdmExpressionKind.Null:
				return ((IEdmNullExpression)expression).TryCastNullAsType(type, out discoveredErrors);
			case EdmExpressionKind.Record:
			{
				IEdmRecordExpression edmRecordExpression = (IEdmRecordExpression)expression;
				if (edmRecordExpression.DeclaredType != null)
				{
					return edmRecordExpression.DeclaredType.TestTypeReferenceMatch(type, expression.Location(), matchExactly, out discoveredErrors);
				}
				return edmRecordExpression.TryCastRecordAsType(type, context, matchExactly, out discoveredErrors);
			}
			case EdmExpressionKind.Collection:
			{
				IEdmCollectionExpression edmCollectionExpression = (IEdmCollectionExpression)expression;
				if (edmCollectionExpression.DeclaredType != null)
				{
					return edmCollectionExpression.DeclaredType.TestTypeReferenceMatch(type, expression.Location(), matchExactly, out discoveredErrors);
				}
				return edmCollectionExpression.TryCastCollectionAsType(type, context, matchExactly, out discoveredErrors);
			}
			case EdmExpressionKind.Path:
			case EdmExpressionKind.PropertyPath:
			case EdmExpressionKind.NavigationPropertyPath:
				return ((IEdmPathExpression)expression).TryCastPathAsType(type, context, matchExactly, out discoveredErrors);
			case EdmExpressionKind.If:
				return ((IEdmIfExpression)expression).TryCastIfAsType(type, context, matchExactly, out discoveredErrors);
			case EdmExpressionKind.Cast:
				return ((IEdmCastExpression)expression).Type.TestTypeReferenceMatch(type, expression.Location(), matchExactly, out discoveredErrors);
			case EdmExpressionKind.IsType:
				return EdmCoreModel.Instance.GetBoolean(false).TestTypeReferenceMatch(type, expression.Location(), matchExactly, out discoveredErrors);
			case EdmExpressionKind.FunctionApplication:
			{
				IEdmApplyExpression edmApplyExpression = (IEdmApplyExpression)expression;
				if (edmApplyExpression.AppliedFunction != null)
				{
					IEdmOperation appliedFunction = edmApplyExpression.AppliedFunction;
					if (appliedFunction != null)
					{
						return appliedFunction.ReturnType.TestTypeReferenceMatch(type, expression.Location(), matchExactly, out discoveredErrors);
					}
				}
				discoveredErrors = Enumerable.Empty<EdmError>();
				return true;
			}
			case EdmExpressionKind.LabeledExpressionReference:
				return ((IEdmLabeledExpressionReferenceExpression)expression).ReferencedLabeledExpression.TryCast(type, out discoveredErrors);
			case EdmExpressionKind.Labeled:
				return ((IEdmLabeledExpression)expression).Expression.TryCast(type, context, matchExactly, out discoveredErrors);
			case EdmExpressionKind.EnumMember:
				return ExpressionTypeChecker.TryCastEnumConstantAsType((IEdmEnumMemberExpression)expression, type, matchExactly, out discoveredErrors);
			default:
				discoveredErrors = new EdmError[]
				{
					new EdmError(expression.Location(), EdmErrorCode.ExpressionNotValidForTheAssertedType, Strings.EdmModel_Validator_Semantic_ExpressionNotValidForTheAssertedType)
				};
				return false;
			}
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x00013EA8 File Offset: 0x000120A8
		internal static bool TryCastPrimitiveAsType(this IEdmPrimitiveValue expression, IEdmTypeReference type, out IEnumerable<EdmError> discoveredErrors)
		{
			if (!type.IsPrimitive())
			{
				discoveredErrors = new EdmError[]
				{
					new EdmError(expression.Location(), EdmErrorCode.PrimitiveConstantExpressionNotValidForNonPrimitiveType, Strings.EdmModel_Validator_Semantic_PrimitiveConstantExpressionNotValidForNonPrimitiveType)
				};
				return false;
			}
			switch (expression.ValueKind)
			{
			case EdmValueKind.Binary:
				return ExpressionTypeChecker.TryCastBinaryConstantAsType((IEdmBinaryConstantExpression)expression, type, out discoveredErrors);
			case EdmValueKind.Boolean:
				return ExpressionTypeChecker.TryCastBooleanConstantAsType((IEdmBooleanConstantExpression)expression, type, out discoveredErrors);
			case EdmValueKind.DateTimeOffset:
				return ExpressionTypeChecker.TryCastDateTimeOffsetConstantAsType((IEdmDateTimeOffsetConstantExpression)expression, type, out discoveredErrors);
			case EdmValueKind.Decimal:
				return ExpressionTypeChecker.TryCastDecimalConstantAsType((IEdmDecimalConstantExpression)expression, type, out discoveredErrors);
			case EdmValueKind.Floating:
				return ExpressionTypeChecker.TryCastFloatingConstantAsType((IEdmFloatingConstantExpression)expression, type, out discoveredErrors);
			case EdmValueKind.Guid:
				return ExpressionTypeChecker.TryCastGuidConstantAsType((IEdmGuidConstantExpression)expression, type, out discoveredErrors);
			case EdmValueKind.Integer:
				return ExpressionTypeChecker.TryCastIntegerConstantAsType((IEdmIntegerConstantExpression)expression, type, out discoveredErrors);
			case EdmValueKind.String:
				return ExpressionTypeChecker.TryCastStringConstantAsType((IEdmStringConstantExpression)expression, type, out discoveredErrors);
			case EdmValueKind.Duration:
				return ExpressionTypeChecker.TryCastDurationConstantAsType((IEdmDurationConstantExpression)expression, type, out discoveredErrors);
			case EdmValueKind.Date:
				return ExpressionTypeChecker.TryCastDateConstantAsType((IEdmDateConstantExpression)expression, type, out discoveredErrors);
			case EdmValueKind.TimeOfDay:
				return ExpressionTypeChecker.TryCastTimeOfDayConstantAsType((IEdmTimeOfDayConstantExpression)expression, type, out discoveredErrors);
			}
			discoveredErrors = new EdmError[]
			{
				new EdmError(expression.Location(), EdmErrorCode.ExpressionPrimitiveKindNotValidForAssertedType, Strings.EdmModel_Validator_Semantic_ExpressionPrimitiveKindNotValidForAssertedType)
			};
			return false;
		}

		// Token: 0x06000812 RID: 2066 RVA: 0x00013FEA File Offset: 0x000121EA
		internal static bool TryCastNullAsType(this IEdmNullExpression expression, IEdmTypeReference type, out IEnumerable<EdmError> discoveredErrors)
		{
			if (!type.IsNullable)
			{
				discoveredErrors = new EdmError[]
				{
					new EdmError(expression.Location(), EdmErrorCode.NullCannotBeAssertedToBeANonNullableType, Strings.EdmModel_Validator_Semantic_NullCannotBeAssertedToBeANonNullableType)
				};
				return false;
			}
			discoveredErrors = Enumerable.Empty<EdmError>();
			return true;
		}

		// Token: 0x06000813 RID: 2067 RVA: 0x00014020 File Offset: 0x00012220
		internal static bool TryCastPathAsType(this IEdmPathExpression expression, IEdmTypeReference type, IEdmType context, bool matchExactly, out IEnumerable<EdmError> discoveredErrors)
		{
			IEdmStructuredType edmStructuredType = context as IEdmStructuredType;
			if (edmStructuredType != null)
			{
				IEdmType edmType = context;
				foreach (string text in expression.PathSegments)
				{
					IEdmStructuredType edmStructuredType2 = edmType as IEdmStructuredType;
					if (edmStructuredType2 == null)
					{
						discoveredErrors = new EdmError[]
						{
							new EdmError(expression.Location(), EdmErrorCode.PathIsNotValidForTheGivenContext, Strings.EdmModel_Validator_Semantic_PathIsNotValidForTheGivenContext(text))
						};
						return false;
					}
					IEdmProperty edmProperty = edmStructuredType2.FindProperty(text);
					edmType = ((edmProperty != null) ? edmProperty.Type.Definition : null);
					if (edmType == null)
					{
						discoveredErrors = Enumerable.Empty<EdmError>();
						return true;
					}
				}
				return edmType.TestTypeMatch(type.Definition, expression.Location(), matchExactly, out discoveredErrors);
			}
			discoveredErrors = Enumerable.Empty<EdmError>();
			return true;
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x000140F8 File Offset: 0x000122F8
		internal static bool TryCastIfAsType(this IEdmIfExpression expression, IEdmTypeReference type, IEdmType context, bool matchExactly, out IEnumerable<EdmError> discoveredErrors)
		{
			IEnumerable<EdmError> enumerable;
			bool flag = expression.TrueExpression.TryCast(type, context, matchExactly, out enumerable);
			IEnumerable<EdmError> enumerable2;
			flag = expression.FalseExpression.TryCast(type, context, matchExactly, out enumerable2) && flag;
			if (!flag)
			{
				List<EdmError> list = new List<EdmError>(enumerable);
				list.AddRange(enumerable2);
				discoveredErrors = list;
			}
			else
			{
				discoveredErrors = Enumerable.Empty<EdmError>();
			}
			return flag;
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x0001414C File Offset: 0x0001234C
		internal static bool TryCastRecordAsType(this IEdmRecordExpression expression, IEdmTypeReference type, IEdmType context, bool matchExactly, out IEnumerable<EdmError> discoveredErrors)
		{
			EdmUtil.CheckArgumentNull<IEdmRecordExpression>(expression, "expression");
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			if (!type.IsStructured())
			{
				discoveredErrors = new EdmError[]
				{
					new EdmError(expression.Location(), EdmErrorCode.RecordExpressionNotValidForNonStructuredType, Strings.EdmModel_Validator_Semantic_RecordExpressionNotValidForNonStructuredType)
				};
				return false;
			}
			HashSetInternal<string> hashSetInternal = new HashSetInternal<string>();
			List<EdmError> list = new List<EdmError>();
			IEdmStructuredTypeReference edmStructuredTypeReference = type.AsStructured();
			using (IEnumerator<IEdmProperty> enumerator = edmStructuredTypeReference.StructuredDefinition().Properties().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					IEdmProperty typeProperty = enumerator.Current;
					IEdmPropertyConstructor edmPropertyConstructor = expression.Properties.FirstOrDefault((IEdmPropertyConstructor p) => p.Name == typeProperty.Name);
					if (edmPropertyConstructor == null)
					{
						list.Add(new EdmError(expression.Location(), EdmErrorCode.RecordExpressionMissingRequiredProperty, Strings.EdmModel_Validator_Semantic_RecordExpressionMissingProperty(typeProperty.Name)));
					}
					else
					{
						IEnumerable<EdmError> enumerable;
						if (!edmPropertyConstructor.Value.TryCast(typeProperty.Type, context, matchExactly, out enumerable))
						{
							foreach (EdmError edmError in enumerable)
							{
								list.Add(edmError);
							}
						}
						hashSetInternal.Add(typeProperty.Name);
					}
				}
			}
			if (!edmStructuredTypeReference.IsOpen())
			{
				foreach (IEdmPropertyConstructor edmPropertyConstructor2 in expression.Properties)
				{
					if (!hashSetInternal.Contains(edmPropertyConstructor2.Name))
					{
						list.Add(new EdmError(expression.Location(), EdmErrorCode.RecordExpressionHasExtraProperties, Strings.EdmModel_Validator_Semantic_RecordExpressionHasExtraProperties(edmPropertyConstructor2.Name)));
					}
				}
			}
			if (list.FirstOrDefault<EdmError>() != null)
			{
				discoveredErrors = list;
				return false;
			}
			discoveredErrors = Enumerable.Empty<EdmError>();
			return true;
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x00014344 File Offset: 0x00012544
		internal static bool TryCastCollectionAsType(this IEdmCollectionExpression expression, IEdmTypeReference type, IEdmType context, bool matchExactly, out IEnumerable<EdmError> discoveredErrors)
		{
			if (!type.IsCollection())
			{
				discoveredErrors = new EdmError[]
				{
					new EdmError(expression.Location(), EdmErrorCode.CollectionExpressionNotValidForNonCollectionType, Strings.EdmModel_Validator_Semantic_CollectionExpressionNotValidForNonCollectionType)
				};
				return false;
			}
			IEdmTypeReference edmTypeReference = type.AsCollection().ElementType();
			bool flag = true;
			List<EdmError> list = new List<EdmError>();
			foreach (IEdmExpression edmExpression in expression.Elements)
			{
				IEnumerable<EdmError> enumerable;
				flag = edmExpression.TryCast(edmTypeReference, context, matchExactly, out enumerable) && flag;
				list.AddRange(enumerable);
			}
			discoveredErrors = list;
			return flag;
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x000143EC File Offset: 0x000125EC
		private static bool TryCastGuidConstantAsType(IEdmGuidConstantExpression expression, IEdmTypeReference type, out IEnumerable<EdmError> discoveredErrors)
		{
			if (!type.IsGuid())
			{
				discoveredErrors = new EdmError[]
				{
					new EdmError(expression.Location(), EdmErrorCode.ExpressionPrimitiveKindNotValidForAssertedType, Strings.EdmModel_Validator_Semantic_ExpressionPrimitiveKindNotValidForAssertedType)
				};
				return false;
			}
			discoveredErrors = Enumerable.Empty<EdmError>();
			return true;
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x00014420 File Offset: 0x00012620
		private static bool TryCastFloatingConstantAsType(IEdmFloatingConstantExpression expression, IEdmTypeReference type, out IEnumerable<EdmError> discoveredErrors)
		{
			if (!type.IsFloating())
			{
				discoveredErrors = new EdmError[]
				{
					new EdmError(expression.Location(), EdmErrorCode.ExpressionPrimitiveKindNotValidForAssertedType, Strings.EdmModel_Validator_Semantic_ExpressionPrimitiveKindNotValidForAssertedType)
				};
				return false;
			}
			discoveredErrors = Enumerable.Empty<EdmError>();
			return true;
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x00014454 File Offset: 0x00012654
		private static bool TryCastDecimalConstantAsType(IEdmDecimalConstantExpression expression, IEdmTypeReference type, out IEnumerable<EdmError> discoveredErrors)
		{
			if (!type.IsDecimal())
			{
				discoveredErrors = new EdmError[]
				{
					new EdmError(expression.Location(), EdmErrorCode.ExpressionPrimitiveKindNotValidForAssertedType, Strings.EdmModel_Validator_Semantic_ExpressionPrimitiveKindNotValidForAssertedType)
				};
				return false;
			}
			discoveredErrors = Enumerable.Empty<EdmError>();
			return true;
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x00014488 File Offset: 0x00012688
		private static bool TryCastDateTimeOffsetConstantAsType(IEdmDateTimeOffsetConstantExpression expression, IEdmTypeReference type, out IEnumerable<EdmError> discoveredErrors)
		{
			if (!type.IsDateTimeOffset())
			{
				discoveredErrors = new EdmError[]
				{
					new EdmError(expression.Location(), EdmErrorCode.ExpressionPrimitiveKindNotValidForAssertedType, Strings.EdmModel_Validator_Semantic_ExpressionPrimitiveKindNotValidForAssertedType)
				};
				return false;
			}
			discoveredErrors = Enumerable.Empty<EdmError>();
			return true;
		}

		// Token: 0x0600081B RID: 2075 RVA: 0x000144BC File Offset: 0x000126BC
		private static bool TryCastDurationConstantAsType(IEdmDurationConstantExpression expression, IEdmTypeReference type, out IEnumerable<EdmError> discoveredErrors)
		{
			if (!type.IsDuration())
			{
				discoveredErrors = new EdmError[]
				{
					new EdmError(expression.Location(), EdmErrorCode.ExpressionPrimitiveKindNotValidForAssertedType, Strings.EdmModel_Validator_Semantic_ExpressionPrimitiveKindNotValidForAssertedType)
				};
				return false;
			}
			discoveredErrors = Enumerable.Empty<EdmError>();
			return true;
		}

		// Token: 0x0600081C RID: 2076 RVA: 0x000144F0 File Offset: 0x000126F0
		private static bool TryCastDateConstantAsType(IEdmDateConstantExpression expression, IEdmTypeReference type, out IEnumerable<EdmError> discoveredErrors)
		{
			if (!type.IsDate())
			{
				discoveredErrors = new EdmError[]
				{
					new EdmError(expression.Location(), EdmErrorCode.ExpressionPrimitiveKindNotValidForAssertedType, Strings.EdmModel_Validator_Semantic_ExpressionPrimitiveKindNotValidForAssertedType)
				};
				return false;
			}
			discoveredErrors = Enumerable.Empty<EdmError>();
			return true;
		}

		// Token: 0x0600081D RID: 2077 RVA: 0x00014524 File Offset: 0x00012724
		private static bool TryCastTimeOfDayConstantAsType(IEdmTimeOfDayConstantExpression expression, IEdmTypeReference type, out IEnumerable<EdmError> discoveredErrors)
		{
			if (!type.IsTimeOfDay())
			{
				discoveredErrors = new EdmError[]
				{
					new EdmError(expression.Location(), EdmErrorCode.ExpressionPrimitiveKindNotValidForAssertedType, Strings.EdmModel_Validator_Semantic_ExpressionPrimitiveKindNotValidForAssertedType)
				};
				return false;
			}
			discoveredErrors = Enumerable.Empty<EdmError>();
			return true;
		}

		// Token: 0x0600081E RID: 2078 RVA: 0x00014558 File Offset: 0x00012758
		private static bool TryCastBooleanConstantAsType(IEdmBooleanConstantExpression expression, IEdmTypeReference type, out IEnumerable<EdmError> discoveredErrors)
		{
			if (!type.IsBoolean())
			{
				discoveredErrors = new EdmError[]
				{
					new EdmError(expression.Location(), EdmErrorCode.ExpressionPrimitiveKindNotValidForAssertedType, Strings.EdmModel_Validator_Semantic_ExpressionPrimitiveKindNotValidForAssertedType)
				};
				return false;
			}
			discoveredErrors = Enumerable.Empty<EdmError>();
			return true;
		}

		// Token: 0x0600081F RID: 2079 RVA: 0x0001458C File Offset: 0x0001278C
		private static bool TryCastStringConstantAsType(IEdmStringConstantExpression expression, IEdmTypeReference type, out IEnumerable<EdmError> discoveredErrors)
		{
			if (!type.IsString())
			{
				discoveredErrors = new EdmError[]
				{
					new EdmError(expression.Location(), EdmErrorCode.ExpressionPrimitiveKindNotValidForAssertedType, Strings.EdmModel_Validator_Semantic_ExpressionPrimitiveKindNotValidForAssertedType)
				};
				return false;
			}
			IEdmStringTypeReference edmStringTypeReference = type.AsString();
			if (edmStringTypeReference.MaxLength != null && expression.Value.Length > edmStringTypeReference.MaxLength.Value)
			{
				discoveredErrors = new EdmError[]
				{
					new EdmError(expression.Location(), EdmErrorCode.StringConstantLengthOutOfRange, Strings.EdmModel_Validator_Semantic_StringConstantLengthOutOfRange(expression.Value.Length, edmStringTypeReference.MaxLength.Value))
				};
				return false;
			}
			discoveredErrors = Enumerable.Empty<EdmError>();
			return true;
		}

		// Token: 0x06000820 RID: 2080 RVA: 0x00014644 File Offset: 0x00012844
		private static bool TryCastIntegerConstantAsType(IEdmIntegerConstantExpression expression, IEdmTypeReference type, out IEnumerable<EdmError> discoveredErrors)
		{
			if (!type.IsIntegral())
			{
				discoveredErrors = new EdmError[]
				{
					new EdmError(expression.Location(), EdmErrorCode.ExpressionPrimitiveKindNotValidForAssertedType, Strings.EdmModel_Validator_Semantic_ExpressionPrimitiveKindNotValidForAssertedType)
				};
				return false;
			}
			switch (type.PrimitiveKind())
			{
			case EdmPrimitiveTypeKind.Byte:
				return ExpressionTypeChecker.TryCastIntegerConstantInRange(expression, 0L, 255L, out discoveredErrors);
			case EdmPrimitiveTypeKind.Int16:
				return ExpressionTypeChecker.TryCastIntegerConstantInRange(expression, -32768L, 32767L, out discoveredErrors);
			case EdmPrimitiveTypeKind.Int32:
				return ExpressionTypeChecker.TryCastIntegerConstantInRange(expression, -2147483648L, 2147483647L, out discoveredErrors);
			case EdmPrimitiveTypeKind.Int64:
				return ExpressionTypeChecker.TryCastIntegerConstantInRange(expression, long.MinValue, long.MaxValue, out discoveredErrors);
			case EdmPrimitiveTypeKind.SByte:
				return ExpressionTypeChecker.TryCastIntegerConstantInRange(expression, -128L, 127L, out discoveredErrors);
			}
			discoveredErrors = new EdmError[]
			{
				new EdmError(expression.Location(), EdmErrorCode.ExpressionPrimitiveKindNotValidForAssertedType, Strings.EdmModel_Validator_Semantic_ExpressionPrimitiveKindNotValidForAssertedType)
			};
			return false;
		}

		// Token: 0x06000821 RID: 2081 RVA: 0x00014731 File Offset: 0x00012931
		private static bool TryCastIntegerConstantInRange(IEdmIntegerConstantExpression expression, long min, long max, out IEnumerable<EdmError> discoveredErrors)
		{
			if (expression.Value < min || expression.Value > max)
			{
				discoveredErrors = new EdmError[]
				{
					new EdmError(expression.Location(), EdmErrorCode.IntegerConstantValueOutOfRange, Strings.EdmModel_Validator_Semantic_IntegerConstantValueOutOfRange)
				};
				return false;
			}
			discoveredErrors = Enumerable.Empty<EdmError>();
			return true;
		}

		// Token: 0x06000822 RID: 2082 RVA: 0x00014770 File Offset: 0x00012970
		private static bool TryCastBinaryConstantAsType(IEdmBinaryConstantExpression expression, IEdmTypeReference type, out IEnumerable<EdmError> discoveredErrors)
		{
			if (!type.IsBinary())
			{
				discoveredErrors = new EdmError[]
				{
					new EdmError(expression.Location(), EdmErrorCode.ExpressionPrimitiveKindNotValidForAssertedType, Strings.EdmModel_Validator_Semantic_ExpressionPrimitiveKindNotValidForAssertedType)
				};
				return false;
			}
			IEdmBinaryTypeReference edmBinaryTypeReference = type.AsBinary();
			if (edmBinaryTypeReference.MaxLength != null && expression.Value.Length > edmBinaryTypeReference.MaxLength.Value)
			{
				discoveredErrors = new EdmError[]
				{
					new EdmError(expression.Location(), EdmErrorCode.BinaryConstantLengthOutOfRange, Strings.EdmModel_Validator_Semantic_BinaryConstantLengthOutOfRange(expression.Value.Length, edmBinaryTypeReference.MaxLength.Value))
				};
				return false;
			}
			discoveredErrors = Enumerable.Empty<EdmError>();
			return true;
		}

		// Token: 0x06000823 RID: 2083 RVA: 0x00014820 File Offset: 0x00012A20
		private static bool TryCastEnumConstantAsType(IEdmEnumMemberExpression expression, IEdmTypeReference type, bool matchExactly, out IEnumerable<EdmError> discoveredErrors)
		{
			if (!type.IsEnum())
			{
				discoveredErrors = new EdmError[]
				{
					new EdmError(expression.Location(), EdmErrorCode.ExpressionEnumKindNotValidForAssertedType, Strings.EdmModel_Validator_Semantic_ExpressionEnumKindNotValidForAssertedType)
				};
				return false;
			}
			foreach (IEdmEnumMember edmEnumMember in expression.EnumMembers)
			{
				if (!edmEnumMember.DeclaringType.TestTypeMatch(type.Definition, expression.Location(), matchExactly, out discoveredErrors))
				{
					return false;
				}
			}
			discoveredErrors = Enumerable.Empty<EdmError>();
			return true;
		}

		// Token: 0x06000824 RID: 2084 RVA: 0x000148BC File Offset: 0x00012ABC
		private static bool TestTypeReferenceMatch(this IEdmTypeReference expressionType, IEdmTypeReference assertedType, EdmLocation location, bool matchExactly, out IEnumerable<EdmError> discoveredErrors)
		{
			if (!expressionType.TestNullabilityMatch(assertedType, location, out discoveredErrors))
			{
				return false;
			}
			if (expressionType.IsBad())
			{
				discoveredErrors = Enumerable.Empty<EdmError>();
				return true;
			}
			return expressionType.Definition.TestTypeMatch(assertedType.Definition, location, matchExactly, out discoveredErrors);
		}

		// Token: 0x06000825 RID: 2085 RVA: 0x000148F4 File Offset: 0x00012AF4
		private static bool TestTypeMatch(this IEdmType expressionType, IEdmType assertedType, EdmLocation location, bool matchExactly, out IEnumerable<EdmError> discoveredErrors)
		{
			if (matchExactly)
			{
				if (!expressionType.IsEquivalentTo(assertedType))
				{
					discoveredErrors = new EdmError[]
					{
						new EdmError(location, EdmErrorCode.ExpressionNotValidForTheAssertedType, Strings.EdmModel_Validator_Semantic_ExpressionNotValidForTheAssertedType)
					};
					return false;
				}
			}
			else
			{
				if (expressionType.TypeKind == EdmTypeKind.None || expressionType.IsBad())
				{
					discoveredErrors = Enumerable.Empty<EdmError>();
					return true;
				}
				if (expressionType.TypeKind == EdmTypeKind.Primitive && assertedType.TypeKind == EdmTypeKind.Primitive)
				{
					IEdmPrimitiveType edmPrimitiveType = expressionType as IEdmPrimitiveType;
					IEdmPrimitiveType edmPrimitiveType2 = assertedType as IEdmPrimitiveType;
					if (!edmPrimitiveType.PrimitiveKind.PromotesTo(edmPrimitiveType2.PrimitiveKind))
					{
						discoveredErrors = new EdmError[]
						{
							new EdmError(location, EdmErrorCode.ExpressionPrimitiveKindNotValidForAssertedType, Strings.EdmModel_Validator_Semantic_ExpressionPrimitiveKindCannotPromoteToAssertedType(expressionType.ToTraceString(), assertedType.ToTraceString()))
						};
						return false;
					}
				}
				else if (!expressionType.IsOrInheritsFrom(assertedType))
				{
					discoveredErrors = new EdmError[]
					{
						new EdmError(location, EdmErrorCode.ExpressionNotValidForTheAssertedType, Strings.EdmModel_Validator_Semantic_ExpressionNotValidForTheAssertedType)
					};
					return false;
				}
			}
			discoveredErrors = Enumerable.Empty<EdmError>();
			return true;
		}

		// Token: 0x06000826 RID: 2086 RVA: 0x000149D5 File Offset: 0x00012BD5
		private static bool TestNullabilityMatch(this IEdmTypeReference expressionType, IEdmTypeReference assertedType, EdmLocation location, out IEnumerable<EdmError> discoveredErrors)
		{
			if (!assertedType.IsNullable && expressionType.IsNullable)
			{
				discoveredErrors = new EdmError[]
				{
					new EdmError(location, EdmErrorCode.CannotAssertNullableTypeAsNonNullableType, Strings.EdmModel_Validator_Semantic_CannotAssertNullableTypeAsNonNullableType(expressionType.FullName()))
				};
				return false;
			}
			discoveredErrors = Enumerable.Empty<EdmError>();
			return true;
		}

		// Token: 0x06000827 RID: 2087 RVA: 0x00014A12 File Offset: 0x00012C12
		private static bool PromotesTo(this EdmPrimitiveTypeKind startingKind, EdmPrimitiveTypeKind target)
		{
			return startingKind == target || ExpressionTypeChecker.promotionMap[(int)startingKind, (int)target];
		}

		// Token: 0x06000828 RID: 2088 RVA: 0x00014A28 File Offset: 0x00012C28
		private static bool[,] InitializePromotionMap()
		{
			int num = (from f in typeof(EdmPrimitiveTypeKind).GetFields()
				where f.IsLiteral
				select f).Count<FieldInfo>();
			bool[,] array = new bool[num, num];
			array[3, 8] = true;
			array[3, 9] = true;
			array[3, 10] = true;
			array[11, 8] = true;
			array[11, 9] = true;
			array[11, 10] = true;
			array[8, 9] = true;
			array[8, 10] = true;
			array[9, 10] = true;
			array[12, 6] = true;
			array[20, 16] = true;
			array[18, 16] = true;
			array[22, 16] = true;
			array[23, 16] = true;
			array[21, 16] = true;
			array[17, 16] = true;
			array[19, 16] = true;
			array[28, 24] = true;
			array[26, 24] = true;
			array[30, 24] = true;
			array[31, 24] = true;
			array[29, 24] = true;
			array[25, 24] = true;
			array[27, 24] = true;
			return array;
		}

		// Token: 0x0400038A RID: 906
		private static readonly bool[,] promotionMap = ExpressionTypeChecker.InitializePromotionMap();
	}
}
