using System;
using System.Collections.Generic;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.Data.Contracts.ModelParameters
{
	// Token: 0x020000EA RID: 234
	public sealed class ParameterMapping
	{
		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x0600062D RID: 1581 RVA: 0x0000CED3 File Offset: 0x0000B0D3
		public string ParameterName { get; }

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x0600062E RID: 1582 RVA: 0x0000CEDB File Offset: 0x0000B0DB
		// (set) Token: 0x0600062F RID: 1583 RVA: 0x0000CEE3 File Offset: 0x0000B0E3
		public ISet<ResolvedQueryLiteralExpression> Values { get; private set; }

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000630 RID: 1584 RVA: 0x0000CEEC File Offset: 0x0000B0EC
		public bool IsListType { get; }

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06000631 RID: 1585 RVA: 0x0000CEF4 File Offset: 0x0000B0F4
		// (set) Token: 0x06000632 RID: 1586 RVA: 0x0000CEFC File Offset: 0x0000B0FC
		public bool IsSelectAllFilter { get; private set; }

		// Token: 0x06000633 RID: 1587 RVA: 0x0000CF05 File Offset: 0x0000B105
		public ParameterMapping(string parameterName, ISet<ResolvedQueryLiteralExpression> values, bool isListType, bool isSelectAllFilter)
		{
			this.ParameterName = parameterName;
			this.Values = values;
			this.IsListType = isListType;
			this.IsSelectAllFilter = isSelectAllFilter;
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x0000CF2A File Offset: 0x0000B12A
		public void IntersectWith(ISet<ResolvedQueryLiteralExpression> resolvedQueryLiteralExpressions, bool isSelectAllFilter)
		{
			if (!this.IsSelectAllFilter && !isSelectAllFilter)
			{
				this.Values.IntersectWith(resolvedQueryLiteralExpressions);
				return;
			}
			if (this.IsSelectAllFilter && !isSelectAllFilter)
			{
				this.Values = resolvedQueryLiteralExpressions;
				this.IsSelectAllFilter = false;
			}
		}
	}
}
