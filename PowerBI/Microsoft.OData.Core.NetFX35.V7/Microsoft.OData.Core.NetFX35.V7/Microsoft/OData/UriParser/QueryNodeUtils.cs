using System;
using System.Collections.Generic;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000122 RID: 290
	internal static class QueryNodeUtils
	{
		// Token: 0x06000D72 RID: 3442 RVA: 0x0002807C File Offset: 0x0002627C
		internal static IEdmPrimitiveTypeReference GetBinaryOperatorResultType(IEdmPrimitiveTypeReference left, IEdmPrimitiveTypeReference right, BinaryOperatorKind operatorKind)
		{
			EdmPrimitiveTypeKind edmPrimitiveTypeKind;
			if (QueryNodeUtils.additionalMap.TryGetValue(new QueryNodeUtils.Tuple<BinaryOperatorKind, EdmPrimitiveTypeKind, EdmPrimitiveTypeKind>(operatorKind, left.PrimitiveKind(), right.PrimitiveKind()), ref edmPrimitiveTypeKind))
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

		// Token: 0x06000D73 RID: 3443 RVA: 0x00028120 File Offset: 0x00026320
		// Note: this type is marked as 'beforefieldinit'.
		static QueryNodeUtils()
		{
			Dictionary<QueryNodeUtils.Tuple<BinaryOperatorKind, EdmPrimitiveTypeKind, EdmPrimitiveTypeKind>, EdmPrimitiveTypeKind> dictionary = new Dictionary<QueryNodeUtils.Tuple<BinaryOperatorKind, EdmPrimitiveTypeKind, EdmPrimitiveTypeKind>, EdmPrimitiveTypeKind>(EqualityComparer<QueryNodeUtils.Tuple<BinaryOperatorKind, EdmPrimitiveTypeKind, EdmPrimitiveTypeKind>>.Default);
			dictionary.Add(new QueryNodeUtils.Tuple<BinaryOperatorKind, EdmPrimitiveTypeKind, EdmPrimitiveTypeKind>(BinaryOperatorKind.Add, EdmPrimitiveTypeKind.DateTimeOffset, EdmPrimitiveTypeKind.Duration), EdmPrimitiveTypeKind.DateTimeOffset);
			dictionary.Add(new QueryNodeUtils.Tuple<BinaryOperatorKind, EdmPrimitiveTypeKind, EdmPrimitiveTypeKind>(BinaryOperatorKind.Add, EdmPrimitiveTypeKind.Duration, EdmPrimitiveTypeKind.DateTimeOffset), EdmPrimitiveTypeKind.DateTimeOffset);
			dictionary.Add(new QueryNodeUtils.Tuple<BinaryOperatorKind, EdmPrimitiveTypeKind, EdmPrimitiveTypeKind>(BinaryOperatorKind.Add, EdmPrimitiveTypeKind.Date, EdmPrimitiveTypeKind.Duration), EdmPrimitiveTypeKind.Date);
			dictionary.Add(new QueryNodeUtils.Tuple<BinaryOperatorKind, EdmPrimitiveTypeKind, EdmPrimitiveTypeKind>(BinaryOperatorKind.Add, EdmPrimitiveTypeKind.Duration, EdmPrimitiveTypeKind.Date), EdmPrimitiveTypeKind.Date);
			dictionary.Add(new QueryNodeUtils.Tuple<BinaryOperatorKind, EdmPrimitiveTypeKind, EdmPrimitiveTypeKind>(BinaryOperatorKind.Add, EdmPrimitiveTypeKind.Duration, EdmPrimitiveTypeKind.Duration), EdmPrimitiveTypeKind.Duration);
			dictionary.Add(new QueryNodeUtils.Tuple<BinaryOperatorKind, EdmPrimitiveTypeKind, EdmPrimitiveTypeKind>(BinaryOperatorKind.Subtract, EdmPrimitiveTypeKind.DateTimeOffset, EdmPrimitiveTypeKind.Duration), EdmPrimitiveTypeKind.DateTimeOffset);
			dictionary.Add(new QueryNodeUtils.Tuple<BinaryOperatorKind, EdmPrimitiveTypeKind, EdmPrimitiveTypeKind>(BinaryOperatorKind.Subtract, EdmPrimitiveTypeKind.DateTimeOffset, EdmPrimitiveTypeKind.DateTimeOffset), EdmPrimitiveTypeKind.Duration);
			dictionary.Add(new QueryNodeUtils.Tuple<BinaryOperatorKind, EdmPrimitiveTypeKind, EdmPrimitiveTypeKind>(BinaryOperatorKind.Subtract, EdmPrimitiveTypeKind.Date, EdmPrimitiveTypeKind.Duration), EdmPrimitiveTypeKind.Date);
			dictionary.Add(new QueryNodeUtils.Tuple<BinaryOperatorKind, EdmPrimitiveTypeKind, EdmPrimitiveTypeKind>(BinaryOperatorKind.Subtract, EdmPrimitiveTypeKind.Date, EdmPrimitiveTypeKind.Date), EdmPrimitiveTypeKind.Duration);
			dictionary.Add(new QueryNodeUtils.Tuple<BinaryOperatorKind, EdmPrimitiveTypeKind, EdmPrimitiveTypeKind>(BinaryOperatorKind.Subtract, EdmPrimitiveTypeKind.Duration, EdmPrimitiveTypeKind.Duration), EdmPrimitiveTypeKind.Duration);
			QueryNodeUtils.additionalMap = dictionary;
		}

		// Token: 0x04000725 RID: 1829
		private static Dictionary<QueryNodeUtils.Tuple<BinaryOperatorKind, EdmPrimitiveTypeKind, EdmPrimitiveTypeKind>, EdmPrimitiveTypeKind> additionalMap;

		// Token: 0x020002D0 RID: 720
		private class Tuple<T1, T2, T3>
		{
			// Token: 0x060018F3 RID: 6387 RVA: 0x000490D6 File Offset: 0x000472D6
			internal Tuple(T1 first, T2 second, T3 third)
			{
				this.First = first;
				this.Second = second;
				this.Third = third;
			}

			// Token: 0x1700057B RID: 1403
			// (get) Token: 0x060018F4 RID: 6388 RVA: 0x000490F3 File Offset: 0x000472F3
			// (set) Token: 0x060018F5 RID: 6389 RVA: 0x000490FB File Offset: 0x000472FB
			public T1 First { get; private set; }

			// Token: 0x1700057C RID: 1404
			// (get) Token: 0x060018F6 RID: 6390 RVA: 0x00049104 File Offset: 0x00047304
			// (set) Token: 0x060018F7 RID: 6391 RVA: 0x0004910C File Offset: 0x0004730C
			public T2 Second { get; private set; }

			// Token: 0x1700057D RID: 1405
			// (get) Token: 0x060018F8 RID: 6392 RVA: 0x00049115 File Offset: 0x00047315
			// (set) Token: 0x060018F9 RID: 6393 RVA: 0x0004911D File Offset: 0x0004731D
			public T3 Third { get; private set; }
		}
	}
}
