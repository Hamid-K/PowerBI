using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.OData.Edm.Expressions;
using Microsoft.OData.Edm.Library;
using Microsoft.OData.Edm.Values;

namespace Microsoft.OData.Edm.Validation
{
	// Token: 0x0200016B RID: 363
	public static class ExpressionTypeChecker
	{
		// Token: 0x060006D8 RID: 1752 RVA: 0x00010183 File Offset: 0x0000E383
		public static bool TryCast(this IEdmExpression expression, IEdmTypeReference type, out IEnumerable<EdmError> discoveredErrors)
		{
			return expression.TryCast(type, null, false, out discoveredErrors);
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x00010190 File Offset: 0x0000E390
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
			case EdmExpressionKind.OperationApplication:
			{
				IEdmApplyExpression edmApplyExpression = (IEdmApplyExpression)expression;
				if (edmApplyExpression.AppliedOperation != null)
				{
					IEdmOperation edmOperation = edmApplyExpression.AppliedOperation as IEdmOperation;
					if (edmOperation != null)
					{
						return edmOperation.ReturnType.TestTypeReferenceMatch(type, expression.Location(), matchExactly, out discoveredErrors);
					}
				}
				discoveredErrors = Enumerable.Empty<EdmError>();
				return true;
			}
			case EdmExpressionKind.LabeledExpressionReference:
				return ((IEdmLabeledExpressionReferenceExpression)expression).ReferencedLabeledExpression.TryCast(type, out discoveredErrors);
			case EdmExpressionKind.Labeled:
				return ((IEdmLabeledExpression)expression).Expression.TryCast(type, context, matchExactly, out discoveredErrors);
			}
			discoveredErrors = new EdmError[]
			{
				new EdmError(expression.Location(), EdmErrorCode.ExpressionNotValidForTheAssertedType, Strings.EdmModel_Validator_Semantic_ExpressionNotValidForTheAssertedType)
			};
			return false;
		}

		// Token: 0x060006DA RID: 1754 RVA: 0x000103DC File Offset: 0x0000E5DC
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

		// Token: 0x060006DB RID: 1755 RVA: 0x00010524 File Offset: 0x0000E724
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

		// Token: 0x060006DC RID: 1756 RVA: 0x00010568 File Offset: 0x0000E768
		internal static bool TryCastPathAsType(this IEdmPathExpression expression, IEdmTypeReference type, IEdmType context, bool matchExactly, out IEnumerable<EdmError> discoveredErrors)
		{
			IEdmStructuredType edmStructuredType = context as IEdmStructuredType;
			if (edmStructuredType != null)
			{
				IEdmType edmType = context;
				foreach (string text in expression.Path)
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

		// Token: 0x060006DD RID: 1757 RVA: 0x00010648 File Offset: 0x0000E848
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

		// Token: 0x060006DE RID: 1758 RVA: 0x000106C0 File Offset: 0x0000E8C0
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
					IEdmPropertyConstructor edmPropertyConstructor = Enumerable.FirstOrDefault<IEdmPropertyConstructor>(expression.Properties, (IEdmPropertyConstructor p) => p.Name == typeProperty.Name);
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
			if (Enumerable.FirstOrDefault<EdmError>(list) != null)
			{
				discoveredErrors = list;
				return false;
			}
			discoveredErrors = Enumerable.Empty<EdmError>();
			return true;
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x000108C8 File Offset: 0x0000EAC8
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

		// Token: 0x060006E0 RID: 1760 RVA: 0x00010978 File Offset: 0x0000EB78
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

		// Token: 0x060006E1 RID: 1761 RVA: 0x000109BC File Offset: 0x0000EBBC
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

		// Token: 0x060006E2 RID: 1762 RVA: 0x00010A00 File Offset: 0x0000EC00
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

		// Token: 0x060006E3 RID: 1763 RVA: 0x00010A44 File Offset: 0x0000EC44
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

		// Token: 0x060006E4 RID: 1764 RVA: 0x00010A88 File Offset: 0x0000EC88
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

		// Token: 0x060006E5 RID: 1765 RVA: 0x00010ACC File Offset: 0x0000ECCC
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

		// Token: 0x060006E6 RID: 1766 RVA: 0x00010B10 File Offset: 0x0000ED10
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

		// Token: 0x060006E7 RID: 1767 RVA: 0x00010B54 File Offset: 0x0000ED54
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

		// Token: 0x060006E8 RID: 1768 RVA: 0x00010B98 File Offset: 0x0000ED98
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

		// Token: 0x060006E9 RID: 1769 RVA: 0x00010C58 File Offset: 0x0000EE58
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

		// Token: 0x060006EA RID: 1770 RVA: 0x00010D4C File Offset: 0x0000EF4C
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

		// Token: 0x060006EB RID: 1771 RVA: 0x00010D98 File Offset: 0x0000EF98
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

		// Token: 0x060006EC RID: 1772 RVA: 0x00010E50 File Offset: 0x0000F050
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

		// Token: 0x060006ED RID: 1773 RVA: 0x00010E88 File Offset: 0x0000F088
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

		// Token: 0x060006EE RID: 1774 RVA: 0x00010F74 File Offset: 0x0000F174
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

		// Token: 0x060006EF RID: 1775 RVA: 0x00010FBE File Offset: 0x0000F1BE
		private static bool PromotesTo(this EdmPrimitiveTypeKind startingKind, EdmPrimitiveTypeKind target)
		{
			return startingKind == target || ExpressionTypeChecker.promotionMap[(int)startingKind, (int)target];
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x00010FDC File Offset: 0x0000F1DC
		private static bool[,] InitializePromotionMap()
		{
			int num = Enumerable.Count<FieldInfo>(Enumerable.Where<FieldInfo>(typeof(EdmPrimitiveTypeKind).GetFields(), (FieldInfo f) => f.IsLiteral));
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

		// Token: 0x040002C2 RID: 706
		private static readonly bool[,] promotionMap = ExpressionTypeChecker.InitializePromotionMap();
	}
}
