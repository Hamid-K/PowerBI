using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.OData.Edm;
using Microsoft.OData.Metadata;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020001D7 RID: 471
	internal static class TypePromotionUtils
	{
		// Token: 0x0600152E RID: 5422 RVA: 0x0003C5A4 File Offset: 0x0003A7A4
		internal static void GetTypeFacets(IEdmTypeReference type, out int? precision, out int? scale)
		{
			precision = null;
			scale = null;
			IEdmDecimalTypeReference edmDecimalTypeReference = type as IEdmDecimalTypeReference;
			if (edmDecimalTypeReference != null)
			{
				precision = edmDecimalTypeReference.Precision;
				scale = edmDecimalTypeReference.Scale;
				return;
			}
			IEdmTemporalTypeReference edmTemporalTypeReference = type as IEdmTemporalTypeReference;
			if (edmTemporalTypeReference != null)
			{
				precision = edmTemporalTypeReference.Precision;
				return;
			}
		}

		// Token: 0x0600152F RID: 5423 RVA: 0x0003C5FC File Offset: 0x0003A7FC
		internal static bool PromoteOperandTypes(BinaryOperatorKind operatorKind, SingleValueNode leftNode, SingleValueNode rightNode, out IEdmTypeReference left, out IEdmTypeReference right, TypeFacetsPromotionRules facetsPromotionRules)
		{
			left = leftNode.TypeReference;
			right = rightNode.TypeReference;
			if (left == null && right == null)
			{
				return true;
			}
			if (operatorKind == BinaryOperatorKind.NotEqual || operatorKind == BinaryOperatorKind.Equal)
			{
				if (TypePromotionUtils.TryHandleEqualityOperatorForEntityOrComplexTypes(ref left, ref right))
				{
					return true;
				}
				if (left != null && right != null && left.IsEnum() && right.IsString())
				{
					right = left;
					return true;
				}
				if (left != null && right != null && right.IsEnum() && left.IsString())
				{
					left = right;
					return true;
				}
				if (left == null && right != null && (right.IsEnum() || right is IEdmSpatialTypeReference))
				{
					left = right;
					return true;
				}
				if (right == null && left != null && (left.IsEnum() || left is IEdmSpatialTypeReference))
				{
					right = left;
					return true;
				}
			}
			if (left != null && right != null && left.IsEnum() && right.IsEnum())
			{
				return string.Equals(left.FullName(), right.FullName(), StringComparison.Ordinal);
			}
			if (left != null && left.IsTypeDefinition())
			{
				left = left.AsPrimitive();
			}
			if (right != null && right.IsTypeDefinition())
			{
				right = right.AsPrimitive();
			}
			if ((left != null && !left.IsODataPrimitiveTypeKind()) || (right != null && !right.IsODataPrimitiveTypeKind()))
			{
				return false;
			}
			FunctionSignature functionSignature;
			bool flag = TypePromotionUtils.FindBestSignature(TypePromotionUtils.GetFunctionSignatures(operatorKind), new SingleValueNode[] { leftNode, rightNode }, new IEdmTypeReference[] { left, right }, out functionSignature) == 1;
			if (flag)
			{
				int? num;
				int? num2;
				TypePromotionUtils.GetTypeFacets(left, out num, out num2);
				int? num3;
				int? num4;
				TypePromotionUtils.GetTypeFacets(right, out num3, out num4);
				int? promotedPrecision = facetsPromotionRules.GetPromotedPrecision(num, num3);
				int? promotedScale = facetsPromotionRules.GetPromotedScale(num2, num4);
				left = functionSignature.GetArgumentTypeWithFacets(0, promotedPrecision, promotedScale);
				right = functionSignature.GetArgumentTypeWithFacets(1, promotedPrecision, promotedScale);
			}
			return flag;
		}

		// Token: 0x06001530 RID: 5424 RVA: 0x0003C7C8 File Offset: 0x0003A9C8
		internal static bool PromoteOperandType(UnaryOperatorKind operatorKind, ref IEdmTypeReference typeReference)
		{
			if (typeReference == null)
			{
				return true;
			}
			FunctionSignature functionSignature;
			bool flag = TypePromotionUtils.FindBestSignature(TypePromotionUtils.GetFunctionSignatures(operatorKind), new SingleValueNode[1], new IEdmTypeReference[] { typeReference }, out functionSignature) == 1;
			if (flag)
			{
				int? num;
				int? num2;
				TypePromotionUtils.GetTypeFacets(typeReference, out num, out num2);
				typeReference = functionSignature.GetArgumentTypeWithFacets(0, num, num2);
			}
			return flag;
		}

		// Token: 0x06001531 RID: 5425 RVA: 0x0003C818 File Offset: 0x0003AA18
		internal static KeyValuePair<string, FunctionSignatureWithReturnType> FindBestFunctionSignature(IList<KeyValuePair<string, FunctionSignatureWithReturnType>> nameFunctions, SingleValueNode[] argumentNodes, string functionCallToken)
		{
			IEdmTypeReference[] array = argumentNodes.Select((SingleValueNode s) => s.TypeReference).ToArray<IEdmTypeReference>();
			IList<KeyValuePair<string, FunctionSignatureWithReturnType>> list = new List<KeyValuePair<string, FunctionSignatureWithReturnType>>(nameFunctions.Count);
			foreach (KeyValuePair<string, FunctionSignatureWithReturnType> keyValuePair in nameFunctions)
			{
				if (keyValuePair.Value.ArgumentTypes.Length == array.Length)
				{
					bool flag = true;
					for (int i = 0; i < keyValuePair.Value.ArgumentTypes.Length; i++)
					{
						if (!TypePromotionUtils.CanPromoteNodeTo(argumentNodes[i], array[i], keyValuePair.Value.ArgumentTypes[i]))
						{
							flag = false;
							break;
						}
					}
					if (flag)
					{
						list.Add(keyValuePair);
					}
				}
			}
			if (list.Count == 0)
			{
				return TypePromotionUtils.NotFoundKeyValuePair;
			}
			if (list.Count == 1)
			{
				return list[0];
			}
			IList<KeyValuePair<string, FunctionSignatureWithReturnType>> list2 = new List<KeyValuePair<string, FunctionSignatureWithReturnType>>();
			for (int j = 0; j < list.Count; j++)
			{
				bool flag2 = true;
				for (int k = 0; k < list.Count; k++)
				{
					if (j != k && TypePromotionUtils.MatchesArgumentTypesBetterThan(array, list[k].Value.ArgumentTypes, list[j].Value.ArgumentTypes))
					{
						flag2 = false;
						break;
					}
				}
				if (flag2)
				{
					list2.Add(list[j]);
				}
			}
			KeyValuePair<string, FunctionSignatureWithReturnType> keyValuePair2 = TypePromotionUtils.NotFoundKeyValuePair;
			if (list2.Count == 1)
			{
				keyValuePair2 = list2[0];
			}
			else
			{
				foreach (KeyValuePair<string, FunctionSignatureWithReturnType> keyValuePair3 in list2)
				{
					if (keyValuePair3.Key.Equals(functionCallToken, StringComparison.Ordinal))
					{
						keyValuePair2 = keyValuePair3;
						break;
					}
				}
			}
			return keyValuePair2;
		}

		// Token: 0x06001532 RID: 5426 RVA: 0x0003CA04 File Offset: 0x0003AC04
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "One method to describe all rules around converts.")]
		internal static bool CanConvertTo(SingleValueNode sourceNodeOrNull, IEdmTypeReference sourceReference, IEdmTypeReference targetReference)
		{
			if (sourceReference.IsEquivalentTo(targetReference))
			{
				return true;
			}
			if (sourceReference.IsUntyped() || targetReference.IsUntyped())
			{
				return true;
			}
			if (targetReference.IsStructured())
			{
				return (sourceReference.IsODataComplexTypeKind() || sourceReference.IsODataEntityTypeKind()) && ((IEdmStructuredType)targetReference.Definition).IsAssignableFrom((IEdmStructuredType)sourceReference.Definition);
			}
			if (sourceReference.IsEnum() && targetReference.IsEnum())
			{
				return sourceReference.Definition.IsEquivalentTo(targetReference.Definition) && (targetReference.IsNullable() || !sourceReference.IsNullable());
			}
			if (targetReference.IsEnum() && sourceReference.IsString())
			{
				return true;
			}
			IEdmPrimitiveTypeReference edmPrimitiveTypeReference = sourceReference.AsPrimitiveOrNull();
			IEdmPrimitiveTypeReference edmPrimitiveTypeReference2 = targetReference.AsPrimitiveOrNull();
			return edmPrimitiveTypeReference != null && edmPrimitiveTypeReference2 != null && MetadataUtilsCommon.CanConvertPrimitiveTypeTo(sourceNodeOrNull, edmPrimitiveTypeReference.PrimitiveDefinition(), edmPrimitiveTypeReference2.PrimitiveDefinition());
		}

		// Token: 0x06001533 RID: 5427 RVA: 0x0003CAD7 File Offset: 0x0003ACD7
		private static IEnumerable<FunctionSignature> GetAdditionTermporalSignatures()
		{
			IEdmTypeReference[] array = new IEdmTemporalTypeReference[]
			{
				EdmCoreModel.Instance.GetDateTimeOffset(false),
				EdmCoreModel.Instance.GetDuration(false)
			};
			FunctionSignature.CreateArgumentTypeWithFacets[] array2 = new FunctionSignature.CreateArgumentTypeWithFacets[2];
			array2[0] = (int? p, int? s) => EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.DateTimeOffset, p, false);
			array2[1] = (int? p, int? s) => EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.Duration, p, false);
			yield return new FunctionSignature(array, array2);
			IEdmTypeReference[] array3 = new IEdmTemporalTypeReference[]
			{
				EdmCoreModel.Instance.GetDateTimeOffset(true),
				EdmCoreModel.Instance.GetDuration(true)
			};
			FunctionSignature.CreateArgumentTypeWithFacets[] array4 = new FunctionSignature.CreateArgumentTypeWithFacets[2];
			array4[0] = (int? p, int? s) => EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.DateTimeOffset, p, true);
			array4[1] = (int? p, int? s) => EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.Duration, p, true);
			yield return new FunctionSignature(array3, array4);
			IEdmTypeReference[] array5 = new IEdmTemporalTypeReference[]
			{
				EdmCoreModel.Instance.GetDuration(false),
				EdmCoreModel.Instance.GetDateTimeOffset(false)
			};
			FunctionSignature.CreateArgumentTypeWithFacets[] array6 = new FunctionSignature.CreateArgumentTypeWithFacets[2];
			array6[0] = (int? p, int? s) => EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.Duration, p, false);
			array6[1] = (int? p, int? s) => EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.DateTimeOffset, p, false);
			yield return new FunctionSignature(array5, array6);
			IEdmTypeReference[] array7 = new IEdmTemporalTypeReference[]
			{
				EdmCoreModel.Instance.GetDuration(true),
				EdmCoreModel.Instance.GetDateTimeOffset(true)
			};
			FunctionSignature.CreateArgumentTypeWithFacets[] array8 = new FunctionSignature.CreateArgumentTypeWithFacets[2];
			array8[0] = (int? p, int? s) => EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.Duration, p, true);
			array8[1] = (int? p, int? s) => EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.DateTimeOffset, p, true);
			yield return new FunctionSignature(array7, array8);
			IEdmTypeReference[] array9 = new IEdmTemporalTypeReference[]
			{
				EdmCoreModel.Instance.GetDuration(false),
				EdmCoreModel.Instance.GetDuration(false)
			};
			FunctionSignature.CreateArgumentTypeWithFacets[] array10 = new FunctionSignature.CreateArgumentTypeWithFacets[2];
			array10[0] = (int? p, int? s) => EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.Duration, p, false);
			array10[1] = (int? p, int? s) => EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.Duration, p, false);
			yield return new FunctionSignature(array9, array10);
			IEdmTypeReference[] array11 = new IEdmTemporalTypeReference[]
			{
				EdmCoreModel.Instance.GetDuration(true),
				EdmCoreModel.Instance.GetDuration(true)
			};
			FunctionSignature.CreateArgumentTypeWithFacets[] array12 = new FunctionSignature.CreateArgumentTypeWithFacets[2];
			array12[0] = (int? p, int? s) => EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.Duration, p, true);
			array12[1] = (int? p, int? s) => EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.Duration, p, true);
			yield return new FunctionSignature(array11, array12);
			IEdmTypeReference[] array13 = new IEdmPrimitiveTypeReference[]
			{
				EdmCoreModel.Instance.GetDate(false),
				EdmCoreModel.Instance.GetDuration(false)
			};
			FunctionSignature.CreateArgumentTypeWithFacets[] array14 = new FunctionSignature.CreateArgumentTypeWithFacets[2];
			array14[1] = (int? p, int? s) => EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.Duration, p, false);
			yield return new FunctionSignature(array13, array14);
			IEdmTypeReference[] array15 = new IEdmPrimitiveTypeReference[]
			{
				EdmCoreModel.Instance.GetDate(true),
				EdmCoreModel.Instance.GetDuration(true)
			};
			FunctionSignature.CreateArgumentTypeWithFacets[] array16 = new FunctionSignature.CreateArgumentTypeWithFacets[2];
			array16[1] = (int? p, int? s) => EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.Duration, p, true);
			yield return new FunctionSignature(array15, array16);
			IEdmTypeReference[] array17 = new IEdmPrimitiveTypeReference[]
			{
				EdmCoreModel.Instance.GetDuration(false),
				EdmCoreModel.Instance.GetDate(false)
			};
			FunctionSignature.CreateArgumentTypeWithFacets[] array18 = new FunctionSignature.CreateArgumentTypeWithFacets[2];
			array18[0] = (int? p, int? s) => EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.Duration, p, false);
			yield return new FunctionSignature(array17, array18);
			IEdmTypeReference[] array19 = new IEdmPrimitiveTypeReference[]
			{
				EdmCoreModel.Instance.GetDuration(true),
				EdmCoreModel.Instance.GetDate(true)
			};
			FunctionSignature.CreateArgumentTypeWithFacets[] array20 = new FunctionSignature.CreateArgumentTypeWithFacets[2];
			array20[0] = (int? p, int? s) => EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.Duration, p, true);
			yield return new FunctionSignature(array19, array20);
			yield break;
		}

		// Token: 0x06001534 RID: 5428 RVA: 0x0003CAE0 File Offset: 0x0003ACE0
		private static IEnumerable<FunctionSignature> GetSubtractionTermporalSignatures()
		{
			IEdmTypeReference[] array = new IEdmTemporalTypeReference[]
			{
				EdmCoreModel.Instance.GetDateTimeOffset(false),
				EdmCoreModel.Instance.GetDuration(false)
			};
			FunctionSignature.CreateArgumentTypeWithFacets[] array2 = new FunctionSignature.CreateArgumentTypeWithFacets[2];
			array2[0] = (int? p, int? s) => EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.DateTimeOffset, p, false);
			array2[1] = (int? p, int? s) => EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.Duration, p, false);
			yield return new FunctionSignature(array, array2);
			IEdmTypeReference[] array3 = new IEdmTemporalTypeReference[]
			{
				EdmCoreModel.Instance.GetDateTimeOffset(true),
				EdmCoreModel.Instance.GetDuration(true)
			};
			FunctionSignature.CreateArgumentTypeWithFacets[] array4 = new FunctionSignature.CreateArgumentTypeWithFacets[2];
			array4[0] = (int? p, int? s) => EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.DateTimeOffset, p, true);
			array4[1] = (int? p, int? s) => EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.Duration, p, true);
			yield return new FunctionSignature(array3, array4);
			IEdmTypeReference[] array5 = new IEdmTemporalTypeReference[]
			{
				EdmCoreModel.Instance.GetDuration(false),
				EdmCoreModel.Instance.GetDuration(false)
			};
			FunctionSignature.CreateArgumentTypeWithFacets[] array6 = new FunctionSignature.CreateArgumentTypeWithFacets[2];
			array6[0] = (int? p, int? s) => EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.Duration, p, false);
			array6[1] = (int? p, int? s) => EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.Duration, p, false);
			yield return new FunctionSignature(array5, array6);
			IEdmTypeReference[] array7 = new IEdmTemporalTypeReference[]
			{
				EdmCoreModel.Instance.GetDuration(true),
				EdmCoreModel.Instance.GetDuration(true)
			};
			FunctionSignature.CreateArgumentTypeWithFacets[] array8 = new FunctionSignature.CreateArgumentTypeWithFacets[2];
			array8[0] = (int? p, int? s) => EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.Duration, p, true);
			array8[1] = (int? p, int? s) => EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.Duration, p, true);
			yield return new FunctionSignature(array7, array8);
			IEdmTypeReference[] array9 = new IEdmTemporalTypeReference[]
			{
				EdmCoreModel.Instance.GetDateTimeOffset(false),
				EdmCoreModel.Instance.GetDateTimeOffset(false)
			};
			FunctionSignature.CreateArgumentTypeWithFacets[] array10 = new FunctionSignature.CreateArgumentTypeWithFacets[2];
			array10[0] = (int? p, int? s) => EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.DateTimeOffset, p, false);
			array10[1] = (int? p, int? s) => EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.DateTimeOffset, p, false);
			yield return new FunctionSignature(array9, array10);
			IEdmTypeReference[] array11 = new IEdmTemporalTypeReference[]
			{
				EdmCoreModel.Instance.GetDateTimeOffset(true),
				EdmCoreModel.Instance.GetDateTimeOffset(true)
			};
			FunctionSignature.CreateArgumentTypeWithFacets[] array12 = new FunctionSignature.CreateArgumentTypeWithFacets[2];
			array12[0] = (int? p, int? s) => EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.DateTimeOffset, p, true);
			array12[1] = (int? p, int? s) => EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.DateTimeOffset, p, true);
			yield return new FunctionSignature(array11, array12);
			IEdmTypeReference[] array13 = new IEdmPrimitiveTypeReference[]
			{
				EdmCoreModel.Instance.GetDate(false),
				EdmCoreModel.Instance.GetDuration(false)
			};
			FunctionSignature.CreateArgumentTypeWithFacets[] array14 = new FunctionSignature.CreateArgumentTypeWithFacets[2];
			array14[1] = (int? p, int? s) => EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.Duration, p, false);
			yield return new FunctionSignature(array13, array14);
			IEdmTypeReference[] array15 = new IEdmPrimitiveTypeReference[]
			{
				EdmCoreModel.Instance.GetDate(true),
				EdmCoreModel.Instance.GetDuration(true)
			};
			FunctionSignature.CreateArgumentTypeWithFacets[] array16 = new FunctionSignature.CreateArgumentTypeWithFacets[2];
			array16[1] = (int? p, int? s) => EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.Duration, p, true);
			yield return new FunctionSignature(array15, array16);
			yield return new FunctionSignature(new IEdmPrimitiveTypeReference[]
			{
				EdmCoreModel.Instance.GetDate(false),
				EdmCoreModel.Instance.GetDate(false)
			}, null);
			yield return new FunctionSignature(new IEdmPrimitiveTypeReference[]
			{
				EdmCoreModel.Instance.GetDate(true),
				EdmCoreModel.Instance.GetDate(true)
			}, null);
			yield break;
		}

		// Token: 0x06001535 RID: 5429 RVA: 0x0003CAEC File Offset: 0x0003ACEC
		private static FunctionSignature[] GetFunctionSignatures(BinaryOperatorKind operatorKind)
		{
			switch (operatorKind)
			{
			case BinaryOperatorKind.Or:
			case BinaryOperatorKind.And:
				return TypePromotionUtils.logicalSignatures;
			case BinaryOperatorKind.Equal:
			case BinaryOperatorKind.NotEqual:
			case BinaryOperatorKind.GreaterThan:
			case BinaryOperatorKind.GreaterThanOrEqual:
			case BinaryOperatorKind.LessThan:
			case BinaryOperatorKind.LessThanOrEqual:
				return TypePromotionUtils.relationalSignatures;
			case BinaryOperatorKind.Add:
				return TypePromotionUtils.AdditionSignatures;
			case BinaryOperatorKind.Subtract:
				return TypePromotionUtils.SubtractionSignatures;
			case BinaryOperatorKind.Multiply:
			case BinaryOperatorKind.Divide:
			case BinaryOperatorKind.Modulo:
				return TypePromotionUtils.arithmeticSignatures;
			default:
				throw new ODataException(Strings.General_InternalError(InternalErrorCodes.TypePromotionUtils_GetFunctionSignatures_Binary_UnreachableCodepath));
			}
		}

		// Token: 0x06001536 RID: 5430 RVA: 0x0003CB63 File Offset: 0x0003AD63
		private static FunctionSignature[] GetFunctionSignatures(UnaryOperatorKind operatorKind)
		{
			if (operatorKind == UnaryOperatorKind.Negate)
			{
				return TypePromotionUtils.negationSignatures;
			}
			if (operatorKind != UnaryOperatorKind.Not)
			{
				throw new ODataException(Strings.General_InternalError(InternalErrorCodes.TypePromotionUtils_GetFunctionSignatures_Unary_UnreachableCodepath));
			}
			return TypePromotionUtils.notSignatures;
		}

		// Token: 0x06001537 RID: 5431 RVA: 0x0003CB8C File Offset: 0x0003AD8C
		private static int FindBestSignature(FunctionSignature[] signatures, SingleValueNode[] argumentNodes, IEdmTypeReference[] argumentTypes, out FunctionSignature bestMatch)
		{
			bestMatch = null;
			List<FunctionSignature> list = signatures.Where((FunctionSignature signature) => TypePromotionUtils.IsApplicable(signature, argumentNodes, argumentTypes)).ToList<FunctionSignature>();
			if (list.Count > 1)
			{
				list = TypePromotionUtils.FindBestApplicableSignatures(list, argumentTypes);
			}
			int count = list.Count;
			if (count == 1)
			{
				bestMatch = list[0];
				for (int i = 0; i < argumentTypes.Length; i++)
				{
					argumentTypes[i] = bestMatch.ArgumentTypes[i];
				}
				return count;
			}
			if (count == 2 && argumentTypes.Length == 2 && list[0].ArgumentTypes[0].Definition.IsEquivalentTo(list[1].ArgumentTypes[0].Definition) && list[0].ArgumentTypes[1].Definition.IsEquivalentTo(list[1].ArgumentTypes[1].Definition))
			{
				bestMatch = (list[0].ArgumentTypes[0].IsNullable ? list[0] : list[1]);
				argumentTypes[0] = bestMatch.ArgumentTypes[0];
				argumentTypes[1] = bestMatch.ArgumentTypes[1];
				return 1;
			}
			return count;
		}

		// Token: 0x06001538 RID: 5432 RVA: 0x0003CCD4 File Offset: 0x0003AED4
		private static bool IsApplicable(FunctionSignature signature, SingleValueNode[] argumentNodes, IEdmTypeReference[] argumentTypes)
		{
			if (signature.ArgumentTypes.Length != argumentTypes.Length)
			{
				return false;
			}
			for (int i = 0; i < argumentTypes.Length; i++)
			{
				if (!TypePromotionUtils.CanPromoteNodeTo(argumentNodes[i], argumentTypes[i], signature.ArgumentTypes[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001539 RID: 5433 RVA: 0x0003CD18 File Offset: 0x0003AF18
		private static bool CanPromoteNodeTo(SingleValueNode sourceNodeOrNull, IEdmTypeReference sourceType, IEdmTypeReference targetType)
		{
			if (sourceType == null)
			{
				return targetType.IsNullable;
			}
			if (sourceType.IsEquivalentTo(targetType))
			{
				return true;
			}
			if (TypePromotionUtils.CanConvertTo(sourceNodeOrNull, sourceType, targetType))
			{
				return true;
			}
			if (sourceType.IsNullable && targetType.IsODataValueType())
			{
				IEdmTypeReference edmTypeReference = sourceType.Definition.ToTypeReference(false);
				if (TypePromotionUtils.CanConvertTo(sourceNodeOrNull, edmTypeReference, targetType))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600153A RID: 5434 RVA: 0x0003CD70 File Offset: 0x0003AF70
		private static List<FunctionSignature> FindBestApplicableSignatures(List<FunctionSignature> signatures, IEdmTypeReference[] argumentTypes)
		{
			List<FunctionSignature> list = new List<FunctionSignature>();
			foreach (FunctionSignature functionSignature in signatures)
			{
				bool flag = true;
				foreach (FunctionSignature functionSignature2 in signatures)
				{
					if (functionSignature2 != functionSignature && TypePromotionUtils.MatchesArgumentTypesBetterThan(argumentTypes, functionSignature2.ArgumentTypes, functionSignature.ArgumentTypes))
					{
						flag = false;
						break;
					}
				}
				if (flag)
				{
					list.Add(functionSignature);
				}
			}
			return list;
		}

		// Token: 0x0600153B RID: 5435 RVA: 0x0003CE20 File Offset: 0x0003B020
		private static bool MatchesArgumentTypesBetterThan(IEdmTypeReference[] argumentTypes, IEdmTypeReference[] firstCandidate, IEdmTypeReference[] secondCandidate)
		{
			bool flag = false;
			for (int i = 0; i < argumentTypes.Length; i++)
			{
				if (argumentTypes[i] != null)
				{
					int num = TypePromotionUtils.CompareConversions(argumentTypes[i], firstCandidate[i], secondCandidate[i]);
					if (num < 0)
					{
						return false;
					}
					if (num > 0)
					{
						flag = true;
					}
				}
			}
			return flag;
		}

		// Token: 0x0600153C RID: 5436 RVA: 0x0003CE60 File Offset: 0x0003B060
		private static int CompareConversions(IEdmTypeReference source, IEdmTypeReference targetA, IEdmTypeReference targetB)
		{
			if (targetA.IsEquivalentTo(targetB))
			{
				return 0;
			}
			if (source.IsEquivalentTo(targetA))
			{
				return 1;
			}
			if (source.IsEquivalentTo(targetB))
			{
				return -1;
			}
			bool flag = TypePromotionUtils.CanConvertTo(null, targetA, targetB);
			bool flag2 = TypePromotionUtils.CanConvertTo(null, targetB, targetA);
			if (flag && !flag2)
			{
				return 1;
			}
			if (flag2 && !flag)
			{
				return -1;
			}
			bool isNullable = source.IsNullable;
			bool isNullable2 = targetA.IsNullable;
			bool isNullable3 = targetB.IsNullable;
			if (isNullable == isNullable2 && isNullable != isNullable3)
			{
				return 1;
			}
			if (isNullable != isNullable2 && isNullable == isNullable3)
			{
				return -1;
			}
			if (TypePromotionUtils.IsSignedIntegralType(targetA) && TypePromotionUtils.IsUnsignedIntegralType(targetB))
			{
				return 1;
			}
			if (TypePromotionUtils.IsSignedIntegralType(targetB) && TypePromotionUtils.IsUnsignedIntegralType(targetA))
			{
				return -1;
			}
			if (TypePromotionUtils.IsDecimalType(targetA) && TypePromotionUtils.IsDoubleOrSingle(targetB))
			{
				return -1;
			}
			if (TypePromotionUtils.IsDecimalType(targetB) && TypePromotionUtils.IsDoubleOrSingle(targetA))
			{
				return 1;
			}
			if (TypePromotionUtils.IsDateTimeOffset(targetA) && TypePromotionUtils.IsDate(targetB))
			{
				return 1;
			}
			if (TypePromotionUtils.IsDateTimeOffset(targetB) && TypePromotionUtils.IsDate(targetA))
			{
				return -1;
			}
			return 0;
		}

		// Token: 0x0600153D RID: 5437 RVA: 0x0003CF4C File Offset: 0x0003B14C
		private static bool TryHandleEqualityOperatorForEntityOrComplexTypes(ref IEdmTypeReference left, ref IEdmTypeReference right)
		{
			if (left != null && left.IsStructured())
			{
				if (right == null)
				{
					right = left;
					return true;
				}
				if (!right.IsStructured())
				{
					return false;
				}
				if (left.Definition.IsEquivalentTo(right.Definition))
				{
					if (left.IsNullable && !right.IsNullable)
					{
						right = left;
					}
					else
					{
						left = right;
					}
					return true;
				}
				if (TypePromotionUtils.CanConvertTo(null, left, right))
				{
					left = right;
					return true;
				}
				if (TypePromotionUtils.CanConvertTo(null, right, left))
				{
					right = left;
					return true;
				}
				return false;
			}
			else
			{
				if (right == null || !right.IsStructured())
				{
					return false;
				}
				if (left == null)
				{
					left = right;
					return true;
				}
				return false;
			}
		}

		// Token: 0x0600153E RID: 5438 RVA: 0x0003CFEE File Offset: 0x0003B1EE
		private static bool IsSignedIntegralType(IEdmTypeReference typeReference)
		{
			return TypePromotionUtils.GetNumericTypeKind(typeReference) == TypePromotionUtils.NumericTypeKind.SignedIntegral;
		}

		// Token: 0x0600153F RID: 5439 RVA: 0x0003CFF9 File Offset: 0x0003B1F9
		private static bool IsUnsignedIntegralType(IEdmTypeReference typeReference)
		{
			return TypePromotionUtils.GetNumericTypeKind(typeReference) == TypePromotionUtils.NumericTypeKind.UnsignedIntegral;
		}

		// Token: 0x06001540 RID: 5440 RVA: 0x0003D004 File Offset: 0x0003B204
		private static bool IsDate(IEdmTypeReference typeReference)
		{
			IEdmPrimitiveTypeReference edmPrimitiveTypeReference = typeReference.AsPrimitiveOrNull();
			return edmPrimitiveTypeReference != null && edmPrimitiveTypeReference.PrimitiveKind() == EdmPrimitiveTypeKind.Date;
		}

		// Token: 0x06001541 RID: 5441 RVA: 0x0003D028 File Offset: 0x0003B228
		private static bool IsDateTimeOffset(IEdmTypeReference typeReference)
		{
			IEdmPrimitiveTypeReference edmPrimitiveTypeReference = typeReference.AsPrimitiveOrNull();
			return edmPrimitiveTypeReference != null && edmPrimitiveTypeReference.PrimitiveKind() == EdmPrimitiveTypeKind.DateTimeOffset;
		}

		// Token: 0x06001542 RID: 5442 RVA: 0x0003D04C File Offset: 0x0003B24C
		private static bool IsDecimalType(IEdmTypeReference typeReference)
		{
			IEdmPrimitiveTypeReference edmPrimitiveTypeReference = typeReference.AsPrimitiveOrNull();
			return edmPrimitiveTypeReference != null && edmPrimitiveTypeReference.PrimitiveKind() == EdmPrimitiveTypeKind.Decimal;
		}

		// Token: 0x06001543 RID: 5443 RVA: 0x0003D070 File Offset: 0x0003B270
		private static bool IsDoubleOrSingle(IEdmTypeReference typeReference)
		{
			IEdmPrimitiveTypeReference edmPrimitiveTypeReference = typeReference.AsPrimitiveOrNull();
			if (edmPrimitiveTypeReference != null)
			{
				EdmPrimitiveTypeKind edmPrimitiveTypeKind = edmPrimitiveTypeReference.PrimitiveKind();
				return edmPrimitiveTypeKind == EdmPrimitiveTypeKind.Double || edmPrimitiveTypeKind == EdmPrimitiveTypeKind.Single;
			}
			return false;
		}

		// Token: 0x06001544 RID: 5444 RVA: 0x0003D09C File Offset: 0x0003B29C
		private static TypePromotionUtils.NumericTypeKind GetNumericTypeKind(IEdmTypeReference typeReference)
		{
			IEdmPrimitiveTypeReference edmPrimitiveTypeReference = typeReference.AsPrimitiveOrNull();
			if (edmPrimitiveTypeReference == null)
			{
				return TypePromotionUtils.NumericTypeKind.NotNumeric;
			}
			switch (edmPrimitiveTypeReference.PrimitiveDefinition().PrimitiveKind)
			{
			case EdmPrimitiveTypeKind.Byte:
				return TypePromotionUtils.NumericTypeKind.UnsignedIntegral;
			case EdmPrimitiveTypeKind.Decimal:
			case EdmPrimitiveTypeKind.Double:
			case EdmPrimitiveTypeKind.Single:
				return TypePromotionUtils.NumericTypeKind.NotIntegral;
			case EdmPrimitiveTypeKind.Int16:
			case EdmPrimitiveTypeKind.Int32:
			case EdmPrimitiveTypeKind.Int64:
			case EdmPrimitiveTypeKind.SByte:
				return TypePromotionUtils.NumericTypeKind.SignedIntegral;
			}
			return TypePromotionUtils.NumericTypeKind.NotNumeric;
		}

		// Token: 0x040009DD RID: 2525
		internal static readonly KeyValuePair<string, FunctionSignatureWithReturnType> NotFoundKeyValuePair = default(KeyValuePair<string, FunctionSignatureWithReturnType>);

		// Token: 0x040009DE RID: 2526
		private static readonly FunctionSignature[] logicalSignatures = new FunctionSignature[]
		{
			new FunctionSignature(new IEdmPrimitiveTypeReference[]
			{
				EdmCoreModel.Instance.GetBoolean(false),
				EdmCoreModel.Instance.GetBoolean(false)
			}, null),
			new FunctionSignature(new IEdmPrimitiveTypeReference[]
			{
				EdmCoreModel.Instance.GetBoolean(true),
				EdmCoreModel.Instance.GetBoolean(true)
			}, null)
		};

		// Token: 0x040009DF RID: 2527
		private static readonly FunctionSignature[] notSignatures = new FunctionSignature[]
		{
			new FunctionSignature(new IEdmPrimitiveTypeReference[] { EdmCoreModel.Instance.GetBoolean(false) }, null),
			new FunctionSignature(new IEdmPrimitiveTypeReference[] { EdmCoreModel.Instance.GetBoolean(true) }, null)
		};

		// Token: 0x040009E0 RID: 2528
		private static readonly FunctionSignature[] arithmeticSignatures = new FunctionSignature[]
		{
			new FunctionSignature(new IEdmPrimitiveTypeReference[]
			{
				EdmCoreModel.Instance.GetInt32(false),
				EdmCoreModel.Instance.GetInt32(false)
			}, null),
			new FunctionSignature(new IEdmPrimitiveTypeReference[]
			{
				EdmCoreModel.Instance.GetInt32(true),
				EdmCoreModel.Instance.GetInt32(true)
			}, null),
			new FunctionSignature(new IEdmPrimitiveTypeReference[]
			{
				EdmCoreModel.Instance.GetInt64(false),
				EdmCoreModel.Instance.GetInt64(false)
			}, null),
			new FunctionSignature(new IEdmPrimitiveTypeReference[]
			{
				EdmCoreModel.Instance.GetInt64(true),
				EdmCoreModel.Instance.GetInt64(true)
			}, null),
			new FunctionSignature(new IEdmPrimitiveTypeReference[]
			{
				EdmCoreModel.Instance.GetSingle(false),
				EdmCoreModel.Instance.GetSingle(false)
			}, null),
			new FunctionSignature(new IEdmPrimitiveTypeReference[]
			{
				EdmCoreModel.Instance.GetSingle(true),
				EdmCoreModel.Instance.GetSingle(true)
			}, null),
			new FunctionSignature(new IEdmPrimitiveTypeReference[]
			{
				EdmCoreModel.Instance.GetDouble(false),
				EdmCoreModel.Instance.GetDouble(false)
			}, null),
			new FunctionSignature(new IEdmPrimitiveTypeReference[]
			{
				EdmCoreModel.Instance.GetDouble(true),
				EdmCoreModel.Instance.GetDouble(true)
			}, null),
			new FunctionSignature(new IEdmDecimalTypeReference[]
			{
				EdmCoreModel.Instance.GetDecimal(false),
				EdmCoreModel.Instance.GetDecimal(false)
			}, new FunctionSignature.CreateArgumentTypeWithFacets[]
			{
				(int? p, int? s) => EdmCoreModel.Instance.GetDecimal(p, s, false),
				(int? p, int? s) => EdmCoreModel.Instance.GetDecimal(p, s, false)
			}),
			new FunctionSignature(new IEdmDecimalTypeReference[]
			{
				EdmCoreModel.Instance.GetDecimal(true),
				EdmCoreModel.Instance.GetDecimal(true)
			}, new FunctionSignature.CreateArgumentTypeWithFacets[]
			{
				(int? p, int? s) => EdmCoreModel.Instance.GetDecimal(p, s, true),
				(int? p, int? s) => EdmCoreModel.Instance.GetDecimal(p, s, true)
			})
		};

		// Token: 0x040009E1 RID: 2529
		private static readonly FunctionSignature[] AdditionSignatures = TypePromotionUtils.arithmeticSignatures.Concat(TypePromotionUtils.GetAdditionTermporalSignatures()).ToArray<FunctionSignature>();

		// Token: 0x040009E2 RID: 2530
		private static readonly FunctionSignature[] SubtractionSignatures = TypePromotionUtils.arithmeticSignatures.Concat(TypePromotionUtils.GetSubtractionTermporalSignatures()).ToArray<FunctionSignature>();

		// Token: 0x040009E3 RID: 2531
		private static readonly FunctionSignature[] relationalSignatures = new FunctionSignature[]
		{
			new FunctionSignature(new IEdmPrimitiveTypeReference[]
			{
				EdmCoreModel.Instance.GetInt32(false),
				EdmCoreModel.Instance.GetInt32(false)
			}, null),
			new FunctionSignature(new IEdmPrimitiveTypeReference[]
			{
				EdmCoreModel.Instance.GetInt32(true),
				EdmCoreModel.Instance.GetInt32(true)
			}, null),
			new FunctionSignature(new IEdmPrimitiveTypeReference[]
			{
				EdmCoreModel.Instance.GetInt64(false),
				EdmCoreModel.Instance.GetInt64(false)
			}, null),
			new FunctionSignature(new IEdmPrimitiveTypeReference[]
			{
				EdmCoreModel.Instance.GetInt64(true),
				EdmCoreModel.Instance.GetInt64(true)
			}, null),
			new FunctionSignature(new IEdmPrimitiveTypeReference[]
			{
				EdmCoreModel.Instance.GetSingle(false),
				EdmCoreModel.Instance.GetSingle(false)
			}, null),
			new FunctionSignature(new IEdmPrimitiveTypeReference[]
			{
				EdmCoreModel.Instance.GetSingle(true),
				EdmCoreModel.Instance.GetSingle(true)
			}, null),
			new FunctionSignature(new IEdmPrimitiveTypeReference[]
			{
				EdmCoreModel.Instance.GetDouble(false),
				EdmCoreModel.Instance.GetDouble(false)
			}, null),
			new FunctionSignature(new IEdmPrimitiveTypeReference[]
			{
				EdmCoreModel.Instance.GetDouble(true),
				EdmCoreModel.Instance.GetDouble(true)
			}, null),
			new FunctionSignature(new IEdmDecimalTypeReference[]
			{
				EdmCoreModel.Instance.GetDecimal(false),
				EdmCoreModel.Instance.GetDecimal(false)
			}, new FunctionSignature.CreateArgumentTypeWithFacets[]
			{
				(int? p, int? s) => EdmCoreModel.Instance.GetDecimal(p, s, false),
				(int? p, int? s) => EdmCoreModel.Instance.GetDecimal(p, s, false)
			}),
			new FunctionSignature(new IEdmDecimalTypeReference[]
			{
				EdmCoreModel.Instance.GetDecimal(true),
				EdmCoreModel.Instance.GetDecimal(true)
			}, new FunctionSignature.CreateArgumentTypeWithFacets[]
			{
				(int? p, int? s) => EdmCoreModel.Instance.GetDecimal(p, s, true),
				(int? p, int? s) => EdmCoreModel.Instance.GetDecimal(p, s, true)
			}),
			new FunctionSignature(new IEdmStringTypeReference[]
			{
				EdmCoreModel.Instance.GetString(true),
				EdmCoreModel.Instance.GetString(true)
			}, null),
			new FunctionSignature(new IEdmBinaryTypeReference[]
			{
				EdmCoreModel.Instance.GetBinary(true),
				EdmCoreModel.Instance.GetBinary(true)
			}, null),
			new FunctionSignature(new IEdmPrimitiveTypeReference[]
			{
				EdmCoreModel.Instance.GetBoolean(false),
				EdmCoreModel.Instance.GetBoolean(false)
			}, null),
			new FunctionSignature(new IEdmPrimitiveTypeReference[]
			{
				EdmCoreModel.Instance.GetBoolean(true),
				EdmCoreModel.Instance.GetBoolean(true)
			}, null),
			new FunctionSignature(new IEdmPrimitiveTypeReference[]
			{
				EdmCoreModel.Instance.GetGuid(false),
				EdmCoreModel.Instance.GetGuid(false)
			}, null),
			new FunctionSignature(new IEdmPrimitiveTypeReference[]
			{
				EdmCoreModel.Instance.GetGuid(true),
				EdmCoreModel.Instance.GetGuid(true)
			}, null),
			new FunctionSignature(new IEdmPrimitiveTypeReference[]
			{
				EdmCoreModel.Instance.GetDate(false),
				EdmCoreModel.Instance.GetDate(false)
			}, null),
			new FunctionSignature(new IEdmPrimitiveTypeReference[]
			{
				EdmCoreModel.Instance.GetDate(true),
				EdmCoreModel.Instance.GetDate(true)
			}, null),
			new FunctionSignature(new IEdmTemporalTypeReference[]
			{
				EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.DateTimeOffset, false),
				EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.DateTimeOffset, false)
			}, new FunctionSignature.CreateArgumentTypeWithFacets[]
			{
				(int? p, int? s) => EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.DateTimeOffset, p, false),
				(int? p, int? s) => EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.DateTimeOffset, p, false)
			}),
			new FunctionSignature(new IEdmTemporalTypeReference[]
			{
				EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.DateTimeOffset, true),
				EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.DateTimeOffset, true)
			}, new FunctionSignature.CreateArgumentTypeWithFacets[]
			{
				(int? p, int? s) => EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.DateTimeOffset, p, true),
				(int? p, int? s) => EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.DateTimeOffset, p, true)
			}),
			new FunctionSignature(new IEdmTemporalTypeReference[]
			{
				EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.Duration, false),
				EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.Duration, false)
			}, new FunctionSignature.CreateArgumentTypeWithFacets[]
			{
				(int? p, int? s) => EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.Duration, p, false),
				(int? p, int? s) => EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.Duration, p, false)
			}),
			new FunctionSignature(new IEdmTemporalTypeReference[]
			{
				EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.Duration, true),
				EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.Duration, true)
			}, new FunctionSignature.CreateArgumentTypeWithFacets[]
			{
				(int? p, int? s) => EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.Duration, p, true),
				(int? p, int? s) => EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.Duration, p, true)
			}),
			new FunctionSignature(new IEdmTemporalTypeReference[]
			{
				EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.TimeOfDay, false),
				EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.TimeOfDay, false)
			}, new FunctionSignature.CreateArgumentTypeWithFacets[]
			{
				(int? p, int? s) => EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.TimeOfDay, p, false),
				(int? p, int? s) => EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.TimeOfDay, p, false)
			}),
			new FunctionSignature(new IEdmTemporalTypeReference[]
			{
				EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.TimeOfDay, true),
				EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.TimeOfDay, true)
			}, new FunctionSignature.CreateArgumentTypeWithFacets[]
			{
				(int? p, int? s) => EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.TimeOfDay, p, true),
				(int? p, int? s) => EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.TimeOfDay, p, true)
			})
		};

		// Token: 0x040009E4 RID: 2532
		private static readonly FunctionSignature[] negationSignatures = new FunctionSignature[]
		{
			new FunctionSignature(new IEdmPrimitiveTypeReference[] { EdmCoreModel.Instance.GetInt32(false) }, null),
			new FunctionSignature(new IEdmPrimitiveTypeReference[] { EdmCoreModel.Instance.GetInt32(true) }, null),
			new FunctionSignature(new IEdmPrimitiveTypeReference[] { EdmCoreModel.Instance.GetInt64(false) }, null),
			new FunctionSignature(new IEdmPrimitiveTypeReference[] { EdmCoreModel.Instance.GetInt64(true) }, null),
			new FunctionSignature(new IEdmPrimitiveTypeReference[] { EdmCoreModel.Instance.GetSingle(false) }, null),
			new FunctionSignature(new IEdmPrimitiveTypeReference[] { EdmCoreModel.Instance.GetSingle(true) }, null),
			new FunctionSignature(new IEdmPrimitiveTypeReference[] { EdmCoreModel.Instance.GetDouble(false) }, null),
			new FunctionSignature(new IEdmPrimitiveTypeReference[] { EdmCoreModel.Instance.GetDouble(true) }, null),
			new FunctionSignature(new IEdmDecimalTypeReference[] { EdmCoreModel.Instance.GetDecimal(false) }, new FunctionSignature.CreateArgumentTypeWithFacets[]
			{
				(int? p, int? s) => EdmCoreModel.Instance.GetDecimal(p, s, false)
			}),
			new FunctionSignature(new IEdmDecimalTypeReference[] { EdmCoreModel.Instance.GetDecimal(true) }, new FunctionSignature.CreateArgumentTypeWithFacets[]
			{
				(int? p, int? s) => EdmCoreModel.Instance.GetDecimal(p, s, true)
			}),
			new FunctionSignature(new IEdmTemporalTypeReference[] { EdmCoreModel.Instance.GetDuration(false) }, new FunctionSignature.CreateArgumentTypeWithFacets[]
			{
				(int? p, int? s) => EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.Duration, p, false)
			}),
			new FunctionSignature(new IEdmTemporalTypeReference[] { EdmCoreModel.Instance.GetDuration(true) }, new FunctionSignature.CreateArgumentTypeWithFacets[]
			{
				(int? p, int? s) => EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.Duration, p, true)
			})
		};

		// Token: 0x020003C6 RID: 966
		private enum NumericTypeKind
		{
			// Token: 0x04000EFD RID: 3837
			NotNumeric,
			// Token: 0x04000EFE RID: 3838
			NotIntegral,
			// Token: 0x04000EFF RID: 3839
			SignedIntegral,
			// Token: 0x04000F00 RID: 3840
			UnsignedIntegral
		}
	}
}
