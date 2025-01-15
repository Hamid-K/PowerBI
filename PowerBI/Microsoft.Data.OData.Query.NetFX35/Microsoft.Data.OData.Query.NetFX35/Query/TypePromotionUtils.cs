using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Edm;
using Microsoft.Data.Edm.Library;
using Microsoft.Data.Experimental.OData.Metadata;
using Microsoft.Data.OData;

namespace Microsoft.Data.Experimental.OData.Query
{
	// Token: 0x02000033 RID: 51
	internal static class TypePromotionUtils
	{
		// Token: 0x06000116 RID: 278 RVA: 0x00005FB0 File Offset: 0x000041B0
		internal static bool PromoteOperandTypes(BinaryOperatorKind operatorKind, ref IEdmTypeReference left, ref IEdmTypeReference right)
		{
			if (left == null && right == null)
			{
				return true;
			}
			FunctionSignature[] functionSignatures = TypePromotionUtils.GetFunctionSignatures(operatorKind);
			IEdmTypeReference[] array = new IEdmTypeReference[] { left, right };
			bool flag = TypePromotionUtils.FindBestSignature(functionSignatures, array) == 1;
			if (flag)
			{
				left = array[0];
				right = array[1];
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

		// Token: 0x06000117 RID: 279 RVA: 0x0000600C File Offset: 0x0000420C
		internal static bool PromoteOperandType(UnaryOperatorKind operatorKind, ref IEdmTypeReference typeReference)
		{
			if (typeReference == null)
			{
				return true;
			}
			FunctionSignature[] functionSignatures = TypePromotionUtils.GetFunctionSignatures(operatorKind);
			IEdmTypeReference[] array = new IEdmTypeReference[] { typeReference };
			bool flag = TypePromotionUtils.FindBestSignature(functionSignatures, array) == 1;
			if (flag)
			{
				typeReference = array[0];
			}
			return flag;
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00006048 File Offset: 0x00004248
		internal static FunctionSignature FindBestFunctionSignature(FunctionSignature[] functions, IEdmTypeReference[] argumentTypes)
		{
			List<FunctionSignature> list = new List<FunctionSignature>(functions.Length);
			foreach (FunctionSignature functionSignature in functions)
			{
				if (functionSignature.ArgumentTypes.Length == argumentTypes.Length)
				{
					bool flag = true;
					for (int j = 0; j < functionSignature.ArgumentTypes.Length; j++)
					{
						if (!TypePromotionUtils.CanPromoteTo(argumentTypes[j], functionSignature.ArgumentTypes[j]))
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
					if (k != l && TypePromotionUtils.MatchesArgumentTypesBetterThan(argumentTypes, list[l].ArgumentTypes, list[k].ArgumentTypes))
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

		// Token: 0x06000119 RID: 281 RVA: 0x00006154 File Offset: 0x00004354
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

		// Token: 0x0600011A RID: 282 RVA: 0x000061C4 File Offset: 0x000043C4
		internal static bool CanConvertTo(IEdmTypeReference sourceReference, IEdmTypeReference targetReference)
		{
			if (sourceReference.IsEquivalentTo(targetReference))
			{
				return true;
			}
			if (targetReference.IsODataComplexTypeKind() || targetReference.IsODataEntityTypeKind())
			{
				return ((IEdmStructuredType)targetReference.Definition).IsAssignableFrom((IEdmStructuredType)sourceReference.Definition);
			}
			if (TypePromotionUtils.IsOpenPropertyType(targetReference))
			{
				return true;
			}
			IEdmPrimitiveTypeReference edmPrimitiveTypeReference = sourceReference.AsPrimitiveOrNull();
			IEdmPrimitiveTypeReference edmPrimitiveTypeReference2 = targetReference.AsPrimitiveOrNull();
			return edmPrimitiveTypeReference != null && edmPrimitiveTypeReference2 != null && MetadataUtilsCommon.CanConvertPrimitiveTypeTo(edmPrimitiveTypeReference.PrimitiveDefinition(), edmPrimitiveTypeReference2.PrimitiveDefinition());
		}

		// Token: 0x0600011B RID: 283 RVA: 0x0000623C File Offset: 0x0000443C
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
			case BinaryOperatorKind.Subtract:
			case BinaryOperatorKind.Multiply:
			case BinaryOperatorKind.Divide:
			case BinaryOperatorKind.Modulo:
				return TypePromotionUtils.arithmeticSignatures;
			default:
				throw new ODataException(Strings.General_InternalError(InternalErrorCodes.TypePromotionUtils_GetFunctionSignatures_Binary_UnreachableCodepath));
			}
		}

		// Token: 0x0600011C RID: 284 RVA: 0x000062AC File Offset: 0x000044AC
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

		// Token: 0x0600011D RID: 285 RVA: 0x00006300 File Offset: 0x00004500
		private static int FindBestSignature(FunctionSignature[] signatures, IEdmTypeReference[] argumentTypes)
		{
			List<FunctionSignature> list = Enumerable.ToList<FunctionSignature>(Enumerable.Where<FunctionSignature>(signatures, (FunctionSignature signature) => TypePromotionUtils.IsApplicable(signature, argumentTypes)));
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
				return TypePromotionUtils.FindBestSignature(signatures, argumentTypes);
			}
			return num;
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00006434 File Offset: 0x00004634
		private static bool IsApplicable(FunctionSignature signature, IEdmTypeReference[] argumentTypes)
		{
			if (signature.ArgumentTypes.Length != argumentTypes.Length)
			{
				return false;
			}
			for (int i = 0; i < argumentTypes.Length; i++)
			{
				if (!TypePromotionUtils.CanPromoteTo(argumentTypes[i], signature.ArgumentTypes[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00006474 File Offset: 0x00004674
		private static bool CanPromoteTo(IEdmTypeReference sourceType, IEdmTypeReference targetType)
		{
			if (sourceType == null)
			{
				return targetType.IsNullable;
			}
			if (sourceType.IsEquivalentTo(targetType))
			{
				return true;
			}
			if (TypePromotionUtils.CanConvertTo(sourceType, targetType))
			{
				return true;
			}
			if (sourceType.IsNullable && targetType.IsODataValueType())
			{
				IEdmTypeReference edmTypeReference = sourceType.Definition.ToTypeReference(false);
				if (TypePromotionUtils.CanConvertTo(edmTypeReference, targetType))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000120 RID: 288 RVA: 0x000064CC File Offset: 0x000046CC
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

		// Token: 0x06000121 RID: 289 RVA: 0x0000657C File Offset: 0x0000477C
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

		// Token: 0x06000122 RID: 290 RVA: 0x000065BC File Offset: 0x000047BC
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
			bool flag = TypePromotionUtils.CanConvertTo(targetA, targetB);
			bool flag2 = TypePromotionUtils.CanConvertTo(targetB, targetA);
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
				return 1;
			}
			if (TypePromotionUtils.IsDecimalType(targetB) && TypePromotionUtils.IsDoubleOrSingle(targetA))
			{
				return -1;
			}
			if (!TypePromotionUtils.IsOpenPropertyType(targetA) && TypePromotionUtils.IsOpenPropertyType(targetB))
			{
				return 1;
			}
			if (!TypePromotionUtils.IsOpenPropertyType(targetB) && TypePromotionUtils.IsOpenPropertyType(targetA))
			{
				return -1;
			}
			return 0;
		}

		// Token: 0x06000123 RID: 291 RVA: 0x000066A3 File Offset: 0x000048A3
		private static bool IsSignedIntegralType(IEdmTypeReference typeReference)
		{
			return TypePromotionUtils.GetNumericTypeKind(typeReference) == TypePromotionUtils.NumericTypeKind.SignedIntegral;
		}

		// Token: 0x06000124 RID: 292 RVA: 0x000066AE File Offset: 0x000048AE
		private static bool IsUnsignedIntegralType(IEdmTypeReference typeReference)
		{
			return TypePromotionUtils.GetNumericTypeKind(typeReference) == TypePromotionUtils.NumericTypeKind.UnsignedIntegral;
		}

		// Token: 0x06000125 RID: 293 RVA: 0x000066BC File Offset: 0x000048BC
		private static bool IsDecimalType(IEdmTypeReference typeReference)
		{
			IEdmPrimitiveTypeReference edmPrimitiveTypeReference = typeReference.AsPrimitiveOrNull();
			return edmPrimitiveTypeReference != null && edmPrimitiveTypeReference.PrimitiveKind() == EdmPrimitiveTypeKind.Decimal;
		}

		// Token: 0x06000126 RID: 294 RVA: 0x000066E0 File Offset: 0x000048E0
		private static bool IsOpenPropertyType(IEdmTypeReference typeReference)
		{
			IEdmPrimitiveTypeReference edmPrimitiveTypeReference = typeReference.AsPrimitiveOrNull();
			return edmPrimitiveTypeReference != null && edmPrimitiveTypeReference.PrimitiveKind() == EdmPrimitiveTypeKind.None;
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00006704 File Offset: 0x00004904
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

		// Token: 0x06000128 RID: 296 RVA: 0x00006730 File Offset: 0x00004930
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

		// Token: 0x0400015E RID: 350
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

		// Token: 0x0400015F RID: 351
		private static readonly FunctionSignature[] notSignatures = new FunctionSignature[]
		{
			new FunctionSignature(new IEdmTypeReference[] { EdmCoreModel.Instance.GetBoolean(false) }),
			new FunctionSignature(new IEdmTypeReference[] { EdmCoreModel.Instance.GetBoolean(true) })
		};

		// Token: 0x04000160 RID: 352
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

		// Token: 0x04000161 RID: 353
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
				EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.DateTime, false),
				EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.DateTime, false)
			}),
			new FunctionSignature(new IEdmTypeReference[]
			{
				EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.DateTime, true),
				EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.DateTime, true)
			})
		};

		// Token: 0x04000162 RID: 354
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
			new FunctionSignature(new IEdmTypeReference[] { EdmCoreModel.Instance.GetDecimal(true) })
		};

		// Token: 0x02000034 RID: 52
		private enum NumericTypeKind
		{
			// Token: 0x04000164 RID: 356
			NotNumeric,
			// Token: 0x04000165 RID: 357
			NotIntegral,
			// Token: 0x04000166 RID: 358
			SignedIntegral,
			// Token: 0x04000167 RID: 359
			UnsignedIntegral
		}
	}
}
