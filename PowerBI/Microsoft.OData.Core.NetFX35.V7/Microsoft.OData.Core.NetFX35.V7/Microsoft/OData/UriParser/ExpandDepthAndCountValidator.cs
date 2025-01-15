using System;
using System.Linq;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200010C RID: 268
	internal sealed class ExpandDepthAndCountValidator
	{
		// Token: 0x06000CB9 RID: 3257 RVA: 0x00022EC2 File Offset: 0x000210C2
		internal ExpandDepthAndCountValidator(int maxDepth, int maxCount)
		{
			this.maxDepth = maxDepth;
			this.maxCount = maxCount;
		}

		// Token: 0x06000CBA RID: 3258 RVA: 0x00022ED8 File Offset: 0x000210D8
		internal void Validate(SelectExpandClause expandTree)
		{
			this.currentCount = 0;
			this.EnsureMaximumCountAndDepthAreNotExceeded(expandTree, 0);
		}

		// Token: 0x06000CBB RID: 3259 RVA: 0x00022EEC File Offset: 0x000210EC
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

		// Token: 0x040006EC RID: 1772
		private readonly int maxDepth;

		// Token: 0x040006ED RID: 1773
		private readonly int maxCount;

		// Token: 0x040006EE RID: 1774
		private int currentCount;
	}
}
