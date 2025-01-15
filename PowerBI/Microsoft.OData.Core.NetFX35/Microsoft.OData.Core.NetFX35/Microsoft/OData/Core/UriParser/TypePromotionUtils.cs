using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.OData.Core.Metadata;
using Microsoft.OData.Core.UriParser.Semantic;
using Microsoft.OData.Core.UriParser.TreeNodeKinds;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Library;

namespace Microsoft.OData.Core.UriParser
{
	// Token: 0x0200028C RID: 652
	internal static class TypePromotionUtils
	{
		// Token: 0x0600164E RID: 5710 RVA: 0x0004C6D4 File Offset: 0x0004A8D4
		internal static bool PromoteOperandTypes(BinaryOperatorKind operatorKind, SingleValueNode leftNode, SingleValueNode rightNode, out IEdmTypeReference left, out IEdmTypeReference right)
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
			FunctionSignature[] functionSignatures = TypePromotionUtils.GetFunctionSignatures(operatorKind);
			SingleValueNode[] array = new SingleValueNode[] { leftNode, rightNode };
			IEdmTypeReference[] array2 = new IEdmTypeReference[] { left, right };
			bool flag = TypePromotionUtils.FindBestSignature(functionSignatures, array, array2) == 1;
			if (flag)
			{
				left = array2[0];
				right = array2[1];
				if (left == null)
				{
					left = right;
				}
				else if (right == null)
				{
					right = left;
				}
			}
			return flag;
		}

		// Token: 0x0600164F RID: 5711 RVA: 0x0004C83C File Offset: 0x0004AA3C
		internal static bool PromoteOperandType(UnaryOperatorKind operatorKind, ref IEdmTypeReference typeReference)
		{
			if (typeReference == null)
			{
				return true;
			}
			FunctionSignature[] functionSignatures = TypePromotionUtils.GetFunctionSignatures(operatorKind);
			IEdmTypeReference[] array = new IEdmTypeReference[] { typeReference };
			FunctionSignature[] array2 = functionSignatures;
			SingleValueNode[] array3 = new SingleValueNode[1];
			bool flag = TypePromotionUtils.FindBestSignature(array2, array3, array) == 1;
			if (flag)
			{
				typeReference = array[0];
			}
			return flag;
		}

		// Token: 0x06001650 RID: 5712 RVA: 0x0004C88C File Offset: 0x0004AA8C
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

		// Token: 0x06001651 RID: 5713 RVA: 0x0004C9CC File Offset: 0x0004ABCC
		internal static FunctionSignature FindExactFunctionSignature(FunctionSignature[] functions, IEdmTypeReference[] argumentTypes)
		{
			foreach (FunctionSignature functionSignature in functions)
			{
				bool flag = true;
				if (functionSignature.ArgumentTypes.Length == argumentTypes.Length)
				{
					for (int j = 0; j < argumentTypes.Length; j++)
					{
						IEdmTypeReference edmTypeReference = functionSignature.ArgumentTypes[j];
						IEdmTypeReference edmTypeReference2 = argumentTypes[j];
						if (!edmTypeReference2.IsODataPrimitiveTypeKind())
						{
							flag = false;
							break;
						}
						if (!edmTypeReference2.IsEquivalentTo(edmTypeReference))
						{
							flag = false;
							break;
						}
					}
					if (flag)
					{
						return functionSignature;
					}
				}
			}
			return null;
		}

		// Token: 0x06001652 RID: 5714 RVA: 0x0004CA3C File Offset: 0x0004AC3C
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

		// Token: 0x06001653 RID: 5715 RVA: 0x0004CE48 File Offset: 0x0004B048
		private static IEnumerable<FunctionSignature> GetAdditionTermporalSignatures()
		{
			yield return new FunctionSignature(new IEdmTypeReference[]
			{
				EdmCoreModel.Instance.GetDateTimeOffset(false),
				EdmCoreModel.Instance.GetDuration(false)
			});
			yield return new FunctionSignature(new IEdmTypeReference[]
			{
				EdmCoreModel.Instance.GetDateTimeOffset(true),
				EdmCoreModel.Instance.GetDuration(true)
			});
			yield return new FunctionSignature(new IEdmTypeReference[]
			{
				EdmCoreModel.Instance.GetDuration(false),
				EdmCoreModel.Instance.GetDateTimeOffset(false)
			});
			yield return new FunctionSignature(new IEdmTypeReference[]
			{
				EdmCoreModel.Instance.GetDuration(true),
				EdmCoreModel.Instance.GetDateTimeOffset(true)
			});
			yield return new FunctionSignature(new IEdmTypeReference[]
			{
				EdmCoreModel.Instance.GetDuration(false),
				EdmCoreModel.Instance.GetDuration(false)
			});
			yield return new FunctionSignature(new IEdmTypeReference[]
			{
				EdmCoreModel.Instance.GetDuration(true),
				EdmCoreModel.Instance.GetDuration(true)
			});
			yield return new FunctionSignature(new IEdmTypeReference[]
			{
				EdmCoreModel.Instance.GetDate(false),
				EdmCoreModel.Instance.GetDuration(false)
			});
			yield return new FunctionSignature(new IEdmTypeReference[]
			{
				EdmCoreModel.Instance.GetDate(true),
				EdmCoreModel.Instance.GetDuration(true)
			});
			yield return new FunctionSignature(new IEdmTypeReference[]
			{
				EdmCoreModel.Instance.GetDuration(false),
				EdmCoreModel.Instance.GetDate(false)
			});
			yield return new FunctionSignature(new IEdmTypeReference[]
			{
				EdmCoreModel.Instance.GetDuration(true),
				EdmCoreModel.Instance.GetDate(true)
			});
			yield break;
		}

		// Token: 0x06001654 RID: 5716 RVA: 0x0004D1C4 File Offset: 0x0004B3C4
		private static IEnumerable<FunctionSignature> GetSubtractionTermporalSignatures()
		{
			yield return new FunctionSignature(new IEdmTypeReference[]
			{
				EdmCoreModel.Instance.GetDateTimeOffset(false),
				EdmCoreModel.Instance.GetDuration(false)
			});
			yield return new FunctionSignature(new IEdmTypeReference[]
			{
				EdmCoreModel.Instance.GetDateTimeOffset(true),
				EdmCoreModel.Instance.GetDuration(true)
			});
			yield return new FunctionSignature(new IEdmTypeReference[]
			{
				EdmCoreModel.Instance.GetDuration(false),
				EdmCoreModel.Instance.GetDuration(false)
			});
			yield return new FunctionSignature(new IEdmTypeReference[]
			{
				EdmCoreModel.Instance.GetDuration(true),
				EdmCoreModel.Instance.GetDuration(true)
			});
			yield return new FunctionSignature(new IEdmTypeReference[]
			{
				EdmCoreModel.Instance.GetDateTimeOffset(false),
				EdmCoreModel.Instance.GetDateTimeOffset(false)
			});
			yield return new FunctionSignature(new IEdmTypeReference[]
			{
				EdmCoreModel.Instance.GetDateTimeOffset(true),
				EdmCoreModel.Instance.GetDateTimeOffset(true)
			});
			yield return new FunctionSignature(new IEdmTypeReference[]
			{
				EdmCoreModel.Instance.GetDate(false),
				EdmCoreModel.Instance.GetDuration(false)
			});
			yield return new FunctionSignature(new IEdmTypeReference[]
			{
				EdmCoreModel.Instance.GetDate(true),
				EdmCoreModel.Instance.GetDuration(true)
			});
			yield return new FunctionSignature(new IEdmTypeReference[]
			{
				EdmCoreModel.Instance.GetDate(false),
				EdmCoreModel.Instance.GetDate(false)
			});
			yield return new FunctionSignature(new IEdmTypeReference[]
			{
				EdmCoreModel.Instance.GetDate(true),
				EdmCoreModel.Instance.GetDate(true)
			});
			yield break;
		}

		// Token: 0x06001655 RID: 5717 RVA: 0x0004D1DC File Offset: 0x0004B3DC
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

		// Token: 0x06001656 RID: 5718 RVA: 0x0004D258 File Offset: 0x0004B458
		private static FunctionSignature[] GetFunctionSignatures(UnaryOperatorKind operatorKind)
		{
			switch (operatorKind)
			{
			case UnaryOperatorKind.Negate:
				return TypePromotionUtils.negationSignatures;
			case UnaryOperatorKind.Not:
				return TypePromotionUtils.notSignatures;
			default:
				throw new ODataException(Strings.General_InternalError(InternalErrorCodes.TypePromotionUtils_GetFunctionSignatures_Unary_UnreachableCodepath));
			}
		}

		// Token: 0x06001657 RID: 5719 RVA: 0x0004D2B0 File Offset: 0x0004B4B0
		private static int FindBestSignature(FunctionSignature[] signatures, SingleValueNode[] argumentNodes, IEdmTypeReference[] argumentTypes)
		{
			List<FunctionSignature> list = Enumerable.ToList<FunctionSignature>(Enumerable.Where<FunctionSignature>(signatures, (FunctionSignature signature) => TypePromotionUtils.IsApplicable(signature, argumentNodes, argumentTypes)));
			if (list.Count > 1)
			{
				list = TypePromotionUtils.FindBestApplicableSignatures(list, argumentTypes);
			}
			int num = list.Count;
			if (num == 1)
			{
				FunctionSignature functionSignature = list[0];
				for (int i = 0; i < argumentTypes.Length; i++)
				{
					argumentTypes[i] = functionSignature.ArgumentTypes[i];
				}
				num = 1;
			}
			else if (num > 1 && argumentTypes.Length == 2 && num == 2 && list[0].ArgumentTypes[0].Definition.IsEquivalentTo(list[1].ArgumentTypes[0].Definition))
			{
				FunctionSignature functionSignature2 = (list[0].ArgumentTypes[0].IsNullable ? list[0] : list[1]);
				argumentTypes[0] = functionSignature2.ArgumentTypes[0];
				argumentTypes[1] = functionSignature2.ArgumentTypes[1];
				return TypePromotionUtils.FindBestSignature(signatures, argumentNodes, argumentTypes);
			}
			return num;
		}

		// Token: 0x06001658 RID: 5720 RVA: 0x0004D3F4 File Offset: 0x0004B5F4
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

		// Token: 0x06001659 RID: 5721 RVA: 0x0004D438 File Offset: 0x0004B638
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

		// Token: 0x0600165A RID: 5722 RVA: 0x0004D490 File Offset: 0x0004B690
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

		// Token: 0x0600165B RID: 5723 RVA: 0x0004D540 File Offset: 0x0004B740
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

		// Token: 0x0600165C RID: 5724 RVA: 0x0004D580 File Offset: 0x0004B780
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

		// Token: 0x0600165D RID: 5725 RVA: 0x0004D66C File Offset: 0x0004B86C
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

		// Token: 0x0600165E RID: 5726 RVA: 0x0004D70E File Offset: 0x0004B90E
		private static bool IsSignedIntegralType(IEdmTypeReference typeReference)
		{
			return TypePromotionUtils.GetNumericTypeKind(typeReference) == TypePromotionUtils.NumericTypeKind.SignedIntegral;
		}

		// Token: 0x0600165F RID: 5727 RVA: 0x0004D719 File Offset: 0x0004B919
		private static bool IsUnsignedIntegralType(IEdmTypeReference typeReference)
		{
			return TypePromotionUtils.GetNumericTypeKind(typeReference) == TypePromotionUtils.NumericTypeKind.UnsignedIntegral;
		}

		// Token: 0x06001660 RID: 5728 RVA: 0x0004D724 File Offset: 0x0004B924
		private static bool IsDate(IEdmTypeReference typeReference)
		{
			IEdmPrimitiveTypeReference edmPrimitiveTypeReference = typeReference.AsPrimitiveOrNull();
			return edmPrimitiveTypeReference != null && edmPrimitiveTypeReference.PrimitiveKind() == EdmPrimitiveTypeKind.Date;
		}

		// Token: 0x06001661 RID: 5729 RVA: 0x0004D748 File Offset: 0x0004B948
		private static bool IsDateTimeOffset(IEdmTypeReference typeReference)
		{
			IEdmPrimitiveTypeReference edmPrimitiveTypeReference = typeReference.AsPrimitiveOrNull();
			return edmPrimitiveTypeReference != null && edmPrimitiveTypeReference.PrimitiveKind() == EdmPrimitiveTypeKind.DateTimeOffset;
		}

		// Token: 0x06001662 RID: 5730 RVA: 0x0004D76C File Offset: 0x0004B96C
		private static bool IsDecimalType(IEdmTypeReference typeReference)
		{
			IEdmPrimitiveTypeReference edmPrimitiveTypeReference = typeReference.AsPrimitiveOrNull();
			return edmPrimitiveTypeReference != null && edmPrimitiveTypeReference.PrimitiveKind() == EdmPrimitiveTypeKind.Decimal;
		}

		// Token: 0x06001663 RID: 5731 RVA: 0x0004D790 File Offset: 0x0004B990
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

		// Token: 0x06001664 RID: 5732 RVA: 0x0004D7BC File Offset: 0x0004B9BC
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

		// Token: 0x040009D6 RID: 2518
		private static readonly FunctionSignature[] logicalSignatures = new FunctionSignature[]
		{
			new FunctionSignature(new IEdmTypeReference[]
			{
				EdmCoreModel.Instance.GetBoolean(false),
				EdmCoreModel.Instance.GetBoolean(false)
			}),
			new FunctionSignature(new IEdmTypeReference[]
			{
				EdmCoreModel.Instance.GetBoolean(true),
				EdmCoreModel.Instance.GetBoolean(true)
			})
		};

		// Token: 0x040009D7 RID: 2519
		private static readonly FunctionSignature[] notSignatures = new FunctionSignature[]
		{
			new FunctionSignature(new IEdmTypeReference[] { EdmCoreModel.Instance.GetBoolean(false) }),
			new FunctionSignature(new IEdmTypeReference[] { EdmCoreModel.Instance.GetBoolean(true) })
		};

		// Token: 0x040009D8 RID: 2520
		private static readonly FunctionSignature[] arithmeticSignatures = new FunctionSignature[]
		{
			new FunctionSignature(new IEdmTypeReference[]
			{
				EdmCoreModel.Instance.GetInt32(false),
				EdmCoreModel.Instance.GetInt32(false)
			}),
			new FunctionSignature(new IEdmTypeReference[]
			{
				EdmCoreModel.Instance.GetInt32(true),
				EdmCoreModel.Instance.GetInt32(true)
			}),
			new FunctionSignature(new IEdmTypeReference[]
			{
				EdmCoreModel.Instance.GetInt64(false),
				EdmCoreModel.Instance.GetInt64(false)
			}),
			new FunctionSignature(new IEdmTypeReference[]
			{
				EdmCoreModel.Instance.GetInt64(true),
				EdmCoreModel.Instance.GetInt64(true)
			}),
			new FunctionSignature(new IEdmTypeReference[]
			{
				EdmCoreModel.Instance.GetSingle(false),
				EdmCoreModel.Instance.GetSingle(false)
			}),
			new FunctionSignature(new IEdmTypeReference[]
			{
				EdmCoreModel.Instance.GetSingle(true),
				EdmCoreModel.Instance.GetSingle(true)
			}),
			new FunctionSignature(new IEdmTypeReference[]
			{
				EdmCoreModel.Instance.GetDouble(false),
				EdmCoreModel.Instance.GetDouble(false)
			}),
			new FunctionSignature(new IEdmTypeReference[]
			{
				EdmCoreModel.Instance.GetDouble(true),
				EdmCoreModel.Instance.GetDouble(true)
			}),
			new FunctionSignature(new IEdmTypeReference[]
			{
				EdmCoreModel.Instance.GetDecimal(false),
				EdmCoreModel.Instance.GetDecimal(false)
			}),
			new FunctionSignature(new IEdmTypeReference[]
			{
				EdmCoreModel.Instance.GetDecimal(true),
				EdmCoreModel.Instance.GetDecimal(true)
			})
		};

		// Token: 0x040009D9 RID: 2521
		private static readonly FunctionSignature[] AdditionSignatures = Enumerable.ToArray<FunctionSignature>(Enumerable.Concat<FunctionSignature>(TypePromotionUtils.arithmeticSignatures, TypePromotionUtils.GetAdditionTermporalSignatures()));

		// Token: 0x040009DA RID: 2522
		private static readonly FunctionSignature[] SubtractionSignatures = Enumerable.ToArray<FunctionSignature>(Enumerable.Concat<FunctionSignature>(TypePromotionUtils.arithmeticSignatures, TypePromotionUtils.GetSubtractionTermporalSignatures()));

		// Token: 0x040009DB RID: 2523
		private static readonly FunctionSignature[] relationalSignatures = new FunctionSignature[]
		{
			new FunctionSignature(new IEdmTypeReference[]
			{
				EdmCoreModel.Instance.GetInt32(false),
				EdmCoreModel.Instance.GetInt32(false)
			}),
			new FunctionSignature(new IEdmTypeReference[]
			{
				EdmCoreModel.Instance.GetInt32(true),
				EdmCoreModel.Instance.GetInt32(true)
			}),
			new FunctionSignature(new IEdmTypeReference[]
			{
				EdmCoreModel.Instance.GetInt64(false),
				EdmCoreModel.Instance.GetInt64(false)
			}),
			new FunctionSignature(new IEdmTypeReference[]
			{
				EdmCoreModel.Instance.GetInt64(true),
				EdmCoreModel.Instance.GetInt64(true)
			}),
			new FunctionSignature(new IEdmTypeReference[]
			{
				EdmCoreModel.Instance.GetSingle(false),
				EdmCoreModel.Instance.GetSingle(false)
			}),
			new FunctionSignature(new IEdmTypeReference[]
			{
				EdmCoreModel.Instance.GetSingle(true),
				EdmCoreModel.Instance.GetSingle(true)
			}),
			new FunctionSignature(new IEdmTypeReference[]
			{
				EdmCoreModel.Instance.GetDouble(false),
				EdmCoreModel.Instance.GetDouble(false)
			}),
			new FunctionSignature(new IEdmTypeReference[]
			{
				EdmCoreModel.Instance.GetDouble(true),
				EdmCoreModel.Instance.GetDouble(true)
			}),
			new FunctionSignature(new IEdmTypeReference[]
			{
				EdmCoreModel.Instance.GetDecimal(false),
				EdmCoreModel.Instance.GetDecimal(false)
			}),
			new FunctionSignature(new IEdmTypeReference[]
			{
				EdmCoreModel.Instance.GetDecimal(true),
				EdmCoreModel.Instance.GetDecimal(true)
			}),
			new FunctionSignature(new IEdmTypeReference[]
			{
				EdmCoreModel.Instance.GetString(true),
				EdmCoreModel.Instance.GetString(true)
			}),
			new FunctionSignature(new IEdmTypeReference[]
			{
				EdmCoreModel.Instance.GetBinary(true),
				EdmCoreModel.Instance.GetBinary(true)
			}),
			new FunctionSignature(new IEdmTypeReference[]
			{
				EdmCoreModel.Instance.GetBoolean(false),
				EdmCoreModel.Instance.GetBoolean(false)
			}),
			new FunctionSignature(new IEdmTypeReference[]
			{
				EdmCoreModel.Instance.GetBoolean(true),
				EdmCoreModel.Instance.GetBoolean(true)
			}),
			new FunctionSignature(new IEdmTypeReference[]
			{
				EdmCoreModel.Instance.GetGuid(false),
				EdmCoreModel.Instance.GetGuid(false)
			}),
			new FunctionSignature(new IEdmTypeReference[]
			{
				EdmCoreModel.Instance.GetGuid(true),
				EdmCoreModel.Instance.GetGuid(true)
			}),
			new FunctionSignature(new IEdmTypeReference[]
			{
				EdmCoreModel.Instance.GetDate(false),
				EdmCoreModel.Instance.GetDate(false)
			}),
			new FunctionSignature(new IEdmTypeReference[]
			{
				EdmCoreModel.Instance.GetDate(true),
				EdmCoreModel.Instance.GetDate(true)
			}),
			new FunctionSignature(new IEdmTypeReference[]
			{
				EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.DateTimeOffset, false),
				EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.DateTimeOffset, false)
			}),
			new FunctionSignature(new IEdmTypeReference[]
			{
				EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.DateTimeOffset, true),
				EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.DateTimeOffset, true)
			}),
			new FunctionSignature(new IEdmTypeReference[]
			{
				EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.Duration, false),
				EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.Duration, false)
			}),
			new FunctionSignature(new IEdmTypeReference[]
			{
				EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.Duration, true),
				EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.Duration, true)
			}),
			new FunctionSignature(new IEdmTypeReference[]
			{
				EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.TimeOfDay, false),
				EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.TimeOfDay, false)
			}),
			new FunctionSignature(new IEdmTypeReference[]
			{
				EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.TimeOfDay, true),
				EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.TimeOfDay, true)
			})
		};

		// Token: 0x040009DC RID: 2524
		private static readonly FunctionSignature[] negationSignatures = new FunctionSignature[]
		{
			new FunctionSignature(new IEdmTypeReference[] { EdmCoreModel.Instance.GetInt32(false) }),
			new FunctionSignature(new IEdmTypeReference[] { EdmCoreModel.Instance.GetInt32(true) }),
			new FunctionSignature(new IEdmTypeReference[] { EdmCoreModel.Instance.GetInt64(false) }),
			new FunctionSignature(new IEdmTypeReference[] { EdmCoreModel.Instance.GetInt64(true) }),
			new FunctionSignature(new IEdmTypeReference[] { EdmCoreModel.Instance.GetSingle(false) }),
			new FunctionSignature(new IEdmTypeReference[] { EdmCoreModel.Instance.GetSingle(true) }),
			new FunctionSignature(new IEdmTypeReference[] { EdmCoreModel.Instance.GetDouble(false) }),
			new FunctionSignature(new IEdmTypeReference[] { EdmCoreModel.Instance.GetDouble(true) }),
			new FunctionSignature(new IEdmTypeReference[] { EdmCoreModel.Instance.GetDecimal(false) }),
			new FunctionSignature(new IEdmTypeReference[] { EdmCoreModel.Instance.GetDecimal(true) }),
			new FunctionSignature(new IEdmTypeReference[] { EdmCoreModel.Instance.GetDuration(false) }),
			new FunctionSignature(new IEdmTypeReference[] { EdmCoreModel.Instance.GetDuration(true) })
		};

		// Token: 0x0200028D RID: 653
		private enum NumericTypeKind
		{
			// Token: 0x040009DF RID: 2527
			NotNumeric,
			// Token: 0x040009E0 RID: 2528
			NotIntegral,
			// Token: 0x040009E1 RID: 2529
			SignedIntegral,
			// Token: 0x040009E2 RID: 2530
			UnsignedIntegral
		}
	}
}
