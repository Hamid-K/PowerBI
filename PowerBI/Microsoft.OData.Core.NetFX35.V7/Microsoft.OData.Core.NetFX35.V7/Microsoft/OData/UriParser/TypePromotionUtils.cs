using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.OData.Edm;
using Microsoft.OData.Metadata;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000189 RID: 393
	internal static class TypePromotionUtils
	{
		// Token: 0x06000FE2 RID: 4066 RVA: 0x0002C330 File Offset: 0x0002A530
		internal static void GetTypeFacets(IEdmTypeReference type, out int? precision, out int? scale)
		{
			precision = default(int?);
			scale = default(int?);
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

		// Token: 0x06000FE3 RID: 4067 RVA: 0x0002C388 File Offset: 0x0002A588
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
				return string.Equals(left.FullName(), right.FullName(), 4);
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

		// Token: 0x06000FE4 RID: 4068 RVA: 0x0002C50C File Offset: 0x0002A70C
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

		// Token: 0x06000FE5 RID: 4069 RVA: 0x0002C55C File Offset: 0x0002A75C
		internal static FunctionSignatureWithReturnType FindBestFunctionSignature(FunctionSignatureWithReturnType[] functions, SingleValueNode[] argumentNodes)
		{
			IEdmTypeReference[] array = Enumerable.ToArray<IEdmTypeReference>(Enumerable.Select<SingleValueNode, IEdmTypeReference>(argumentNodes, (SingleValueNode s) => s.TypeReference));
			List<FunctionSignatureWithReturnType> list = new List<FunctionSignatureWithReturnType>(functions.Length);
			foreach (FunctionSignatureWithReturnType functionSignatureWithReturnType in functions)
			{
				if (functionSignatureWithReturnType.ArgumentTypes.Length == array.Length)
				{
					bool flag = true;
					for (int j = 0; j < functionSignatureWithReturnType.ArgumentTypes.Length; j++)
					{
						if (!TypePromotionUtils.CanPromoteNodeTo(argumentNodes[j], array[j], functionSignatureWithReturnType.ArgumentTypes[j]))
						{
							flag = false;
							break;
						}
					}
					if (flag)
					{
						list.Add(functionSignatureWithReturnType);
					}
				}
			}
			if (list.Count == 0)
			{
				return null;
			}
			if (list.Count == 1)
			{
				return list[0];
			}
			int num = -1;
			for (int k = 0; k < list.Count; k++)
			{
				bool flag2 = true;
				for (int l = 0; l < list.Count; l++)
				{
					if (k != l && TypePromotionUtils.MatchesArgumentTypesBetterThan(array, list[l].ArgumentTypes, list[k].ArgumentTypes))
					{
						flag2 = false;
						break;
					}
				}
				if (flag2)
				{
					if (num != -1)
					{
						return null;
					}
					num = k;
				}
			}
			if (num == -1)
			{
				return null;
			}
			return list[num];
		}

		// Token: 0x06000FE6 RID: 4070 RVA: 0x0002C6A0 File Offset: 0x0002A8A0
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "One method to describe all rules around converts.")]
		internal static bool CanConvertTo(SingleValueNode sourceNodeOrNull, IEdmTypeReference sourceReference, IEdmTypeReference targetReference)
		{
			if (sourceReference.IsEquivalentTo(targetReference))
			{
				return true;
			}
			if (targetReference.IsODataComplexTypeKind() || targetReference.IsODataEntityTypeKind())
			{
				return ((IEdmStructuredType)targetReference.Definition).IsAssignableFrom((IEdmStructuredType)sourceReference.Definition);
			}
			if (sourceReference.IsEnum() && targetReference.IsEnum())
			{
				return sourceReference.Definition.IsEquivalentTo(targetReference.Definition) && (targetReference.IsNullable() || !sourceReference.IsNullable());
			}
			IEdmPrimitiveTypeReference edmPrimitiveTypeReference = sourceReference.AsPrimitiveOrNull();
			IEdmPrimitiveTypeReference edmPrimitiveTypeReference2 = targetReference.AsPrimitiveOrNull();
			return edmPrimitiveTypeReference != null && edmPrimitiveTypeReference2 != null && MetadataUtilsCommon.CanConvertPrimitiveTypeTo(sourceNodeOrNull, edmPrimitiveTypeReference.PrimitiveDefinition(), edmPrimitiveTypeReference2.PrimitiveDefinition());
		}

		// Token: 0x06000FE7 RID: 4071 RVA: 0x0002C745 File Offset: 0x0002A945
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

		// Token: 0x06000FE8 RID: 4072 RVA: 0x0002C74E File Offset: 0x0002A94E
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

		// Token: 0x06000FE9 RID: 4073 RVA: 0x0002C758 File Offset: 0x0002A958
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

		// Token: 0x06000FEA RID: 4074 RVA: 0x0002C7CF File Offset: 0x0002A9CF
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

		// Token: 0x06000FEB RID: 4075 RVA: 0x0002C7F8 File Offset: 0x0002A9F8
		private static int FindBestSignature(FunctionSignature[] signatures, SingleValueNode[] argumentNodes, IEdmTypeReference[] argumentTypes, out FunctionSignature bestMatch)
		{
			bestMatch = null;
			List<FunctionSignature> list = Enumerable.ToList<FunctionSignature>(Enumerable.Where<FunctionSignature>(signatures, (FunctionSignature signature) => TypePromotionUtils.IsApplicable(signature, argumentNodes, argumentTypes)));
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

		// Token: 0x06000FEC RID: 4076 RVA: 0x0002C940 File Offset: 0x0002AB40
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

		// Token: 0x06000FED RID: 4077 RVA: 0x0002C984 File Offset: 0x0002AB84
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

		// Token: 0x06000FEE RID: 4078 RVA: 0x0002C9DC File Offset: 0x0002ABDC
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

		// Token: 0x06000FEF RID: 4079 RVA: 0x0002CA8C File Offset: 0x0002AC8C
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

		// Token: 0x06000FF0 RID: 4080 RVA: 0x0002CACC File Offset: 0x0002ACCC
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

		// Token: 0x06000FF1 RID: 4081 RVA: 0x0002CBB8 File Offset: 0x0002ADB8
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

		// Token: 0x06000FF2 RID: 4082 RVA: 0x0002CC5A File Offset: 0x0002AE5A
		private static bool IsSignedIntegralType(IEdmTypeReference typeReference)
		{
			return TypePromotionUtils.GetNumericTypeKind(typeReference) == TypePromotionUtils.NumericTypeKind.SignedIntegral;
		}

		// Token: 0x06000FF3 RID: 4083 RVA: 0x0002CC65 File Offset: 0x0002AE65
		private static bool IsUnsignedIntegralType(IEdmTypeReference typeReference)
		{
			return TypePromotionUtils.GetNumericTypeKind(typeReference) == TypePromotionUtils.NumericTypeKind.UnsignedIntegral;
		}

		// Token: 0x06000FF4 RID: 4084 RVA: 0x0002CC70 File Offset: 0x0002AE70
		private static bool IsDate(IEdmTypeReference typeReference)
		{
			IEdmPrimitiveTypeReference edmPrimitiveTypeReference = typeReference.AsPrimitiveOrNull();
			return edmPrimitiveTypeReference != null && edmPrimitiveTypeReference.PrimitiveKind() == EdmPrimitiveTypeKind.Date;
		}

		// Token: 0x06000FF5 RID: 4085 RVA: 0x0002CC94 File Offset: 0x0002AE94
		private static bool IsDateTimeOffset(IEdmTypeReference typeReference)
		{
			IEdmPrimitiveTypeReference edmPrimitiveTypeReference = typeReference.AsPrimitiveOrNull();
			return edmPrimitiveTypeReference != null && edmPrimitiveTypeReference.PrimitiveKind() == EdmPrimitiveTypeKind.DateTimeOffset;
		}

		// Token: 0x06000FF6 RID: 4086 RVA: 0x0002CCB8 File Offset: 0x0002AEB8
		private static bool IsDecimalType(IEdmTypeReference typeReference)
		{
			IEdmPrimitiveTypeReference edmPrimitiveTypeReference = typeReference.AsPrimitiveOrNull();
			return edmPrimitiveTypeReference != null && edmPrimitiveTypeReference.PrimitiveKind() == EdmPrimitiveTypeKind.Decimal;
		}

		// Token: 0x06000FF7 RID: 4087 RVA: 0x0002CCDC File Offset: 0x0002AEDC
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

		// Token: 0x06000FF8 RID: 4088 RVA: 0x0002CD08 File Offset: 0x0002AF08
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

		// Token: 0x04000885 RID: 2181
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

		// Token: 0x04000886 RID: 2182
		private static readonly FunctionSignature[] notSignatures = new FunctionSignature[]
		{
			new FunctionSignature(new IEdmPrimitiveTypeReference[] { EdmCoreModel.Instance.GetBoolean(false) }, null),
			new FunctionSignature(new IEdmPrimitiveTypeReference[] { EdmCoreModel.Instance.GetBoolean(true) }, null)
		};

		// Token: 0x04000887 RID: 2183
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

		// Token: 0x04000888 RID: 2184
		private static readonly FunctionSignature[] AdditionSignatures = Enumerable.ToArray<FunctionSignature>(Enumerable.Concat<FunctionSignature>(TypePromotionUtils.arithmeticSignatures, TypePromotionUtils.GetAdditionTermporalSignatures()));

		// Token: 0x04000889 RID: 2185
		private static readonly FunctionSignature[] SubtractionSignatures = Enumerable.ToArray<FunctionSignature>(Enumerable.Concat<FunctionSignature>(TypePromotionUtils.arithmeticSignatures, TypePromotionUtils.GetSubtractionTermporalSignatures()));

		// Token: 0x0400088A RID: 2186
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

		// Token: 0x0400088B RID: 2187
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

		// Token: 0x020002DD RID: 733
		private enum NumericTypeKind
		{
			// Token: 0x04000C24 RID: 3108
			NotNumeric,
			// Token: 0x04000C25 RID: 3109
			NotIntegral,
			// Token: 0x04000C26 RID: 3110
			SignedIntegral,
			// Token: 0x04000C27 RID: 3111
			UnsignedIntegral
		}
	}
}
