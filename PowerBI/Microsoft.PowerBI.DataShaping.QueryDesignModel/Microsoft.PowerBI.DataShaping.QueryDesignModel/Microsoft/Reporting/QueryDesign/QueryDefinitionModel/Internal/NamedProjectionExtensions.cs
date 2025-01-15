using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;

namespace Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal
{
	// Token: 0x020000E3 RID: 227
	internal static class NamedProjectionExtensions
	{
		// Token: 0x06000DD7 RID: 3543 RVA: 0x000235BC File Offset: 0x000217BC
		internal static bool IsCalculatedInMeasureContext(this INamedProjection namedProjection)
		{
			GroupDetail groupDetail = namedProjection as GroupDetail;
			return groupDetail != null && groupDetail.CalculateInMeasureContext;
		}

		// Token: 0x06000DD8 RID: 3544 RVA: 0x000235DB File Offset: 0x000217DB
		internal static IEnumerable<string> GetNames(this IEnumerable<INamedItem> items)
		{
			return items.Select((INamedItem i) => i.Name);
		}

		// Token: 0x06000DD9 RID: 3545 RVA: 0x00023602 File Offset: 0x00021802
		internal static IEnumerable<QueryExpression> GetExpressions(this IEnumerable<INamedProjection> items)
		{
			return items.Select((INamedProjection i) => i.Expression);
		}

		// Token: 0x06000DDA RID: 3546 RVA: 0x0002362C File Offset: 0x0002182C
		internal static TNamedProjection FindByExpression<TNamedProjection>(this IEnumerable<TNamedProjection> projections, QueryExpression expression) where TNamedProjection : class, INamedProjection
		{
			foreach (TNamedProjection tnamedProjection in projections)
			{
				if (expression.Equals(tnamedProjection.Expression))
				{
					return tnamedProjection;
				}
			}
			return default(TNamedProjection);
		}

		// Token: 0x06000DDB RID: 3547 RVA: 0x00023690 File Offset: 0x00021890
		internal static IEnumerable<string> GetNames(this IEnumerable<Group> items)
		{
			return items.Cast<INamedItem>().GetNames();
		}

		// Token: 0x06000DDC RID: 3548 RVA: 0x0002369D File Offset: 0x0002189D
		internal static IEnumerable<string> GetNames(this IEnumerable<GroupKey> items)
		{
			return items.Cast<INamedItem>().GetNames();
		}

		// Token: 0x06000DDD RID: 3549 RVA: 0x000236AA File Offset: 0x000218AA
		internal static IEnumerable<QueryExpression> GetExpressions(this IEnumerable<GroupKey> items)
		{
			return items.Cast<INamedProjection>().GetExpressions();
		}

		// Token: 0x06000DDE RID: 3550 RVA: 0x000236B7 File Offset: 0x000218B7
		internal static IEnumerable<QueryExpression> GetExpressions(this IEnumerable<GroupDetail> items)
		{
			return items.Cast<INamedProjection>().GetExpressions();
		}
	}
}
