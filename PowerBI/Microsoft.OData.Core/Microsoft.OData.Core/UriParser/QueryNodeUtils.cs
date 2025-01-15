using System;
using System.Collections.Generic;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000167 RID: 359
	internal static class QueryNodeUtils
	{
		// Token: 0x0600124A RID: 4682 RVA: 0x000379B0 File Offset: 0x00035BB0
		internal static IEdmPrimitiveTypeReference GetBinaryOperatorResultType(IEdmPrimitiveTypeReference left, IEdmPrimitiveTypeReference right, BinaryOperatorKind operatorKind)
		{
			EdmPrimitiveTypeKind edmPrimitiveTypeKind;
			if (QueryNodeUtils.additionalMap.TryGetValue(new Tuple<BinaryOperatorKind, EdmPrimitiveTypeKind, EdmPrimitiveTypeKind>(operatorKind, left.PrimitiveKind(), right.PrimitiveKind()), out edmPrimitiveTypeKind))
			{
				return EdmCoreModel.Instance.GetPrimitive(edmPrimitiveTypeKind, left.IsNullable);
			}
			switch (operatorKind)
			{
			case BinaryOperatorKind.Or:
			case BinaryOperatorKind.And:
			case BinaryOperatorKind.Equal:
			case BinaryOperatorKind.NotEqual:
			case BinaryOperatorKind.GreaterThan:
			case BinaryOperatorKind.GreaterThanOrEqual:
			case BinaryOperatorKind.LessThan:
			case BinaryOperatorKind.LessThanOrEqual:
			case BinaryOperatorKind.Has:
				return EdmCoreModel.Instance.GetBoolean(left.IsNullable);
			case BinaryOperatorKind.Add:
			case BinaryOperatorKind.Subtract:
			case BinaryOperatorKind.Multiply:
			case BinaryOperatorKind.Divide:
			case BinaryOperatorKind.Modulo:
				return left;
			default:
				throw new ODataException(Strings.General_InternalError(InternalErrorCodes.QueryNodeUtils_BinaryOperatorResultType_UnreachableCodepath));
			}
		}

		// Token: 0x04000846 RID: 2118
		private static Dictionary<Tuple<BinaryOperatorKind, EdmPrimitiveTypeKind, EdmPrimitiveTypeKind>, EdmPrimitiveTypeKind> additionalMap = new Dictionary<Tuple<BinaryOperatorKind, EdmPrimitiveTypeKind, EdmPrimitiveTypeKind>, EdmPrimitiveTypeKind>(EqualityComparer<Tuple<BinaryOperatorKind, EdmPrimitiveTypeKind, EdmPrimitiveTypeKind>>.Default)
		{
			{
				new Tuple<BinaryOperatorKind, EdmPrimitiveTypeKind, EdmPrimitiveTypeKind>(BinaryOperatorKind.Add, EdmPrimitiveTypeKind.DateTimeOffset, EdmPrimitiveTypeKind.Duration),
				EdmPrimitiveTypeKind.DateTimeOffset
			},
			{
				new Tuple<BinaryOperatorKind, EdmPrimitiveTypeKind, EdmPrimitiveTypeKind>(BinaryOperatorKind.Add, EdmPrimitiveTypeKind.Duration, EdmPrimitiveTypeKind.DateTimeOffset),
				EdmPrimitiveTypeKind.DateTimeOffset
			},
			{
				new Tuple<BinaryOperatorKind, EdmPrimitiveTypeKind, EdmPrimitiveTypeKind>(BinaryOperatorKind.Add, EdmPrimitiveTypeKind.Date, EdmPrimitiveTypeKind.Duration),
				EdmPrimitiveTypeKind.Date
			},
			{
				new Tuple<BinaryOperatorKind, EdmPrimitiveTypeKind, EdmPrimitiveTypeKind>(BinaryOperatorKind.Add, EdmPrimitiveTypeKind.Duration, EdmPrimitiveTypeKind.Date),
				EdmPrimitiveTypeKind.Date
			},
			{
				new Tuple<BinaryOperatorKind, EdmPrimitiveTypeKind, EdmPrimitiveTypeKind>(BinaryOperatorKind.Add, EdmPrimitiveTypeKind.Duration, EdmPrimitiveTypeKind.Duration),
				EdmPrimitiveTypeKind.Duration
			},
			{
				new Tuple<BinaryOperatorKind, EdmPrimitiveTypeKind, EdmPrimitiveTypeKind>(BinaryOperatorKind.Subtract, EdmPrimitiveTypeKind.DateTimeOffset, EdmPrimitiveTypeKind.Duration),
				EdmPrimitiveTypeKind.DateTimeOffset
			},
			{
				new Tuple<BinaryOperatorKind, EdmPrimitiveTypeKind, EdmPrimitiveTypeKind>(BinaryOperatorKind.Subtract, EdmPrimitiveTypeKind.DateTimeOffset, EdmPrimitiveTypeKind.DateTimeOffset),
				EdmPrimitiveTypeKind.Duration
			},
			{
				new Tuple<BinaryOperatorKind, EdmPrimitiveTypeKind, EdmPrimitiveTypeKind>(BinaryOperatorKind.Subtract, EdmPrimitiveTypeKind.Date, EdmPrimitiveTypeKind.Duration),
				EdmPrimitiveTypeKind.Date
			},
			{
				new Tuple<BinaryOperatorKind, EdmPrimitiveTypeKind, EdmPrimitiveTypeKind>(BinaryOperatorKind.Subtract, EdmPrimitiveTypeKind.Date, EdmPrimitiveTypeKind.Date),
				EdmPrimitiveTypeKind.Duration
			},
			{
				new Tuple<BinaryOperatorKind, EdmPrimitiveTypeKind, EdmPrimitiveTypeKind>(BinaryOperatorKind.Subtract, EdmPrimitiveTypeKind.Duration, EdmPrimitiveTypeKind.Duration),
				EdmPrimitiveTypeKind.Duration
			}
		};
	}
}
