using System;
using System.Linq;
using Microsoft.OData.Core.UriParser.Semantic;

namespace Microsoft.OData.Core.UriParser.Parsers
{
	// Token: 0x020001F6 RID: 502
	internal sealed class ExpandDepthAndCountValidator
	{
		// Token: 0x06001263 RID: 4707 RVA: 0x00042303 File Offset: 0x00040503
		internal ExpandDepthAndCountValidator(int maxDepth, int maxCount)
		{
			this.maxDepth = maxDepth;
			this.maxCount = maxCount;
		}

		// Token: 0x06001264 RID: 4708 RVA: 0x00042319 File Offset: 0x00040519
		internal void Validate(SelectExpandClause expandTree)
		{
			this.currentCount = 0;
			this.EnsureMaximumCountAndDepthAreNotExceeded(expandTree, 0);
		}

		// Token: 0x06001265 RID: 4709 RVA: 0x00042354 File Offset: 0x00040554
		private void EnsureMaximumCountAndDepthAreNotExceeded(SelectExpandClause expandTree, int currentDepth)
		{
			if (currentDepth > this.maxDepth)
			{
				throw ExceptionUtil.CreateBadRequestError(Strings.UriParser_ExpandDepthExceeded(currentDepth, this.maxDepth));
			}
			foreach (SelectItem selectItem in Enumerable.Where<SelectItem>(expandTree.SelectedItems, (SelectItem I) => I.GetType() == typeof(ExpandedNavigationSelectItem)))
			{
				ExpandedNavigationSelectItem expandedNavigationSelectItem = (ExpandedNavigationSelectItem)selectItem;
				this.currentCount++;
				if (this.currentCount > this.maxCount)
				{
					throw ExceptionUtil.CreateBadRequestError(Strings.UriParser_ExpandCountExceeded(this.currentCount, this.maxCount));
				}
				this.EnsureMaximumCountAndDepthAreNotExceeded(expandedNavigationSelectItem.SelectAndExpand, currentDepth + 1);
			}
			this.currentCount += Enumerable.Count<SelectItem>(Enumerable.Where<SelectItem>(expandTree.SelectedItems, (SelectItem I) => I.GetType() == typeof(ExpandedReferenceSelectItem)));
			if (this.currentCount > this.maxCount)
			{
				throw ExceptionUtil.CreateBadRequestError(Strings.UriParser_ExpandCountExceeded(this.currentCount, this.maxCount));
			}
		}

		// Token: 0x040007EC RID: 2028
		private readonly int maxDepth;

		// Token: 0x040007ED RID: 2029
		private readonly int maxCount;

		// Token: 0x040007EE RID: 2030
		private int currentCount;
	}
}
