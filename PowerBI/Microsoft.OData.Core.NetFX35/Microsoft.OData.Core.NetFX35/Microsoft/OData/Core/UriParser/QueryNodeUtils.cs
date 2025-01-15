using System;
using System.Collections.Generic;
using Microsoft.OData.Core.UriParser.TreeNodeKinds;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Library;

namespace Microsoft.OData.Core.UriParser
{
	// Token: 0x02000219 RID: 537
	internal static class QueryNodeUtils
	{
		// Token: 0x06001397 RID: 5015 RVA: 0x00048328 File Offset: 0x00046528
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

		// Token: 0x06001398 RID: 5016 RVA: 0x000483CC File Offset: 0x000465CC
		// Note: this type is marked as 'beforefieldinit'.
		static QueryNodeUtils()
		{
			Dictionary<QueryNodeUtils.Tuple<BinaryOperatorKind, EdmPrimitiveTypeKind, EdmPrimitiveTypeKind>, EdmPrimitiveTypeKind> dictionary = new Dictionary<QueryNodeUtils.Tuple<BinaryOperatorKind, EdmPrimitiveTypeKind, EdmPrimitiveTypeKind>, EdmPrimitiveTypeKind>(EqualityComparer<QueryNodeUtils.Tuple<BinaryOperatorKind, EdmPrimitiveTypeKind, EdmPrimitiveTypeKind>>.Default);
			dictionary.Add(new QueryNodeUtils.Tuple<BinaryOperatorKind, EdmPrimitiveTypeKind, EdmPrimitiveTypeKind>(BinaryOperatorKind.Add, EdmPrimitiveTypeKind.DateTimeOffset, EdmPrimitiveTypeKind.Duration), EdmPrimitiveTypeKind.DateTimeOffset);
			dictionary.Add(new QueryNodeUtils.Tuple<BinaryOperatorKind, EdmPrimitiveTypeKind, EdmPrimitiveTypeKind>(BinaryOperatorKind.Add, EdmPrimitiveTypeKind.Duration, EdmPrimitiveTypeKind.DateTimeOffset), EdmPrimitiveTypeKind.DateTimeOffset);
			dictionary.Add(new QueryNodeUtils.Tuple<BinaryOperatorKind, EdmPrimitiveTypeKind, EdmPrimitiveTypeKind>(BinaryOperatorKind.Subtract, EdmPrimitiveTypeKind.DateTimeOffset, EdmPrimitiveTypeKind.Duration), EdmPrimitiveTypeKind.DateTimeOffset);
			dictionary.Add(new QueryNodeUtils.Tuple<BinaryOperatorKind, EdmPrimitiveTypeKind, EdmPrimitiveTypeKind>(BinaryOperatorKind.Subtract, EdmPrimitiveTypeKind.DateTimeOffset, EdmPrimitiveTypeKind.DateTimeOffset), EdmPrimitiveTypeKind.Duration);
			dictionary.Add(new QueryNodeUtils.Tuple<BinaryOperatorKind, EdmPrimitiveTypeKind, EdmPrimitiveTypeKind>(BinaryOperatorKind.Add, EdmPrimitiveTypeKind.Date, EdmPrimitiveTypeKind.Duration), EdmPrimitiveTypeKind.DateTimeOffset);
			dictionary.Add(new QueryNodeUtils.Tuple<BinaryOperatorKind, EdmPrimitiveTypeKind, EdmPrimitiveTypeKind>(BinaryOperatorKind.Add, EdmPrimitiveTypeKind.Duration, EdmPrimitiveTypeKind.Date), EdmPrimitiveTypeKind.DateTimeOffset);
			dictionary.Add(new QueryNodeUtils.Tuple<BinaryOperatorKind, EdmPrimitiveTypeKind, EdmPrimitiveTypeKind>(BinaryOperatorKind.Subtract, EdmPrimitiveTypeKind.Date, EdmPrimitiveTypeKind.Duration), EdmPrimitiveTypeKind.DateTimeOffset);
			dictionary.Add(new QueryNodeUtils.Tuple<BinaryOperatorKind, EdmPrimitiveTypeKind, EdmPrimitiveTypeKind>(BinaryOperatorKind.Subtract, EdmPrimitiveTypeKind.Date, EdmPrimitiveTypeKind.Date), EdmPrimitiveTypeKind.Duration);
			QueryNodeUtils.additionalMap = dictionary;
		}

		// Token: 0x0400084E RID: 2126
		private static Dictionary<QueryNodeUtils.Tuple<BinaryOperatorKind, EdmPrimitiveTypeKind, EdmPrimitiveTypeKind>, EdmPrimitiveTypeKind> additionalMap;

		// Token: 0x0200021A RID: 538
		private class Tuple<T1, T2, T3>
		{
			// Token: 0x06001399 RID: 5017 RVA: 0x00048473 File Offset: 0x00046673
			internal Tuple(T1 first, T2 second, T3 third)
			{
				this.First = first;
				this.Second = second;
				this.Third = third;
			}

			// Token: 0x170003DB RID: 987
			// (get) Token: 0x0600139A RID: 5018 RVA: 0x00048490 File Offset: 0x00046690
			// (set) Token: 0x0600139B RID: 5019 RVA: 0x00048498 File Offset: 0x00046698
			public T1 First { get; private set; }

			// Token: 0x170003DC RID: 988
			// (get) Token: 0x0600139C RID: 5020 RVA: 0x000484A1 File Offset: 0x000466A1
			// (set) Token: 0x0600139D RID: 5021 RVA: 0x000484A9 File Offset: 0x000466A9
			public T2 Second { get; private set; }

			// Token: 0x170003DD RID: 989
			// (get) Token: 0x0600139E RID: 5022 RVA: 0x000484B2 File Offset: 0x000466B2
			// (set) Token: 0x0600139F RID: 5023 RVA: 0x000484BA File Offset: 0x000466BA
			public T3 Third { get; private set; }
		}
	}
}
