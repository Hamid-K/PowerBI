using System;
using System.Linq;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000150 RID: 336
	internal sealed class ExpandDepthAndCountValidator
	{
		// Token: 0x0600116E RID: 4462 RVA: 0x000312F2 File Offset: 0x0002F4F2
		internal ExpandDepthAndCountValidator(int maxDepth, int maxCount)
		{
			this.maxDepth = maxDepth;
			this.maxCount = maxCount;
		}

		// Token: 0x0600116F RID: 4463 RVA: 0x00031308 File Offset: 0x0002F508
		internal void Validate(SelectExpandClause expandTree)
		{
			this.currentCount = 0;
			this.EnsureMaximumCountAndDepthAreNotExceeded(expandTree, 0);
		}

		// Token: 0x06001170 RID: 4464 RVA: 0x0003131C File Offset: 0x0002F51C
		private void EnsureMaximumCountAndDepthAreNotExceeded(SelectExpandClause expandTree, int currentDepth)
		{
			if (currentDepth > this.maxDepth)
			{
				throw ExceptionUtil.CreateBadRequestError(Strings.UriParser_ExpandDepthExceeded(currentDepth, this.maxDepth));
			}
			foreach (SelectItem selectItem in expandTree.SelectedItems.Where((SelectItem I) => I.GetType() == typeof(ExpandedNavigationSelectItem)))
			{
				ExpandedNavigationSelectItem expandedNavigationSelectItem = (ExpandedNavigationSelectItem)selectItem;
				this.currentCount++;
				if (this.currentCount > this.maxCount)
				{
					throw ExceptionUtil.CreateBadRequestError(Strings.UriParser_ExpandCountExceeded(this.currentCount, this.maxCount));
				}
				this.EnsureMaximumCountAndDepthAreNotExceeded(expandedNavigationSelectItem.SelectAndExpand, currentDepth + 1);
			}
			this.currentCount += expandTree.SelectedItems.Where((SelectItem I) => I.GetType() == typeof(ExpandedReferenceSelectItem)).Count<SelectItem>();
			if (this.currentCount > this.maxCount)
			{
				throw ExceptionUtil.CreateBadRequestError(Strings.UriParser_ExpandCountExceeded(this.currentCount, this.maxCount));
			}
		}

		// Token: 0x0400080A RID: 2058
		private readonly int maxDepth;

		// Token: 0x0400080B RID: 2059
		private readonly int maxCount;

		// Token: 0x0400080C RID: 2060
		private int currentCount;
	}
}
