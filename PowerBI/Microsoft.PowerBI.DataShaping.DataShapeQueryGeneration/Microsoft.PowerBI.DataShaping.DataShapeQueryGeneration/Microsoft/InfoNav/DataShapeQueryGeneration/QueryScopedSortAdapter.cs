using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x020000C0 RID: 192
	internal sealed class QueryScopedSortAdapter
	{
		// Token: 0x060006FB RID: 1787 RVA: 0x0001A4AD File Offset: 0x000186AD
		internal QueryScopedSortAdapter()
		{
			this._unwrappedScopedSortExpressions = new Dictionary<int, global::System.ValueTuple<DsqSortKey, IReadOnlyList<ExpressionNode>>>();
			this._unwrappedAndAppliedSorts = new Dictionary<int, bool>();
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x0001A4CC File Offset: 0x000186CC
		internal void AddSortKey(int orderByIndex, DsqSortKey sortKey)
		{
			DsqSortKey dsqSortKey;
			IReadOnlyList<ExpressionNode> readOnlyList;
			if (this.TryUnwrapScopedSortBy(sortKey, out dsqSortKey, out readOnlyList))
			{
				this._unwrappedScopedSortExpressions.Add(orderByIndex, new global::System.ValueTuple<DsqSortKey, IReadOnlyList<ExpressionNode>>(dsqSortKey, readOnlyList));
			}
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x0001A4F9 File Offset: 0x000186F9
		internal bool IsScopedSortKey(int orderByIndex)
		{
			return this._unwrappedScopedSortExpressions.ContainsKey(orderByIndex);
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x0001A508 File Offset: 0x00018708
		internal bool TryGetScopedSortKeyForGroup(int orderByIndex, DsqSortKey wrappedScopedSortKey, IReadOnlyList<QueryGroupKey> groupKeys, out DsqSortKey sortKeyToApply)
		{
			bool flag;
			if (!this._unwrappedAndAppliedSorts.TryGetValue(orderByIndex, out flag))
			{
				global::System.ValueTuple<DsqSortKey, IReadOnlyList<ExpressionNode>> valueTuple;
				if (this._unwrappedScopedSortExpressions.TryGetValue(orderByIndex, out valueTuple))
				{
					if (groupKeys.Select((QueryGroupKey k) => k.Expression).SetEquals(valueTuple.Item2))
					{
						this._unwrappedAndAppliedSorts.Add(orderByIndex, true);
						sortKeyToApply = valueTuple.Item1;
						return true;
					}
				}
				this._unwrappedAndAppliedSorts.Add(orderByIndex, false);
				sortKeyToApply = wrappedScopedSortKey;
				return true;
			}
			if (flag)
			{
				sortKeyToApply = null;
				return false;
			}
			sortKeyToApply = wrappedScopedSortKey;
			return true;
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x0001A5A0 File Offset: 0x000187A0
		private bool TryUnwrapScopedSortBy(DsqSortKey sortKey, out DsqSortKey unwrappedExpression, out IReadOnlyList<ExpressionNode> scopes)
		{
			DsqSortKeyExpression dsqSortKeyExpression = sortKey as DsqSortKeyExpression;
			if (dsqSortKeyExpression != null && dsqSortKeyExpression.IsScoped)
			{
				FunctionCallExpressionNode functionCallExpressionNode = dsqSortKeyExpression.Expression as FunctionCallExpressionNode;
				if (functionCallExpressionNode != null && !(functionCallExpressionNode.Descriptor.Name != "Evaluate"))
				{
					ExpressionNode expressionNode = functionCallExpressionNode.Arguments[0];
					FunctionCallExpressionNode functionCallExpressionNode2 = functionCallExpressionNode.Arguments[1] as FunctionCallExpressionNode;
					if (functionCallExpressionNode2 == null || functionCallExpressionNode2.Descriptor.Name != "Scope")
					{
						unwrappedExpression = null;
						scopes = null;
						return false;
					}
					unwrappedExpression = dsqSortKeyExpression.CloneWithOverrides(expressionNode);
					scopes = functionCallExpressionNode2.Arguments;
					return true;
				}
			}
			unwrappedExpression = null;
			scopes = null;
			return false;
		}

		// Token: 0x040003AE RID: 942
		[global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "UnwrappedSortKey", "Scopes" })]
		private readonly IDictionary<int, global::System.ValueTuple<DsqSortKey, IReadOnlyList<ExpressionNode>>> _unwrappedScopedSortExpressions;

		// Token: 0x040003AF RID: 943
		private readonly IDictionary<int, bool> _unwrappedAndAppliedSorts;
	}
}
