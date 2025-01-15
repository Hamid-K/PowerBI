using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x02000190 RID: 400
	internal sealed class RollupGroupItem : IGroupItem, IEquatable<IGroupItem>
	{
		// Token: 0x06001556 RID: 5462 RVA: 0x0003BE7A File Offset: 0x0003A07A
		internal RollupGroupItem(IEnumerable<CompositeKeyGroupItem> groupItems)
		{
			this._groupItems = ArgumentValidation.CheckNotNullOrEmpty<CompositeKeyGroupItem>(groupItems, "groupItems").ToReadOnlyCollection<CompositeKeyGroupItem>();
		}

		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x06001557 RID: 5463 RVA: 0x0003BE98 File Offset: 0x0003A098
		public ReadOnlyCollection<CompositeKeyGroupItem> GroupItems
		{
			get
			{
				return this._groupItems;
			}
		}

		// Token: 0x06001558 RID: 5464 RVA: 0x0003BEA0 File Offset: 0x0003A0A0
		public IEnumerable<KeyValuePair<string, QueryExpression>> GetGroupKeys()
		{
			return this._groupItems.SelectMany((CompositeKeyGroupItem g) => g.GetGroupKeys());
		}

		// Token: 0x06001559 RID: 5465 RVA: 0x0003BECC File Offset: 0x0003A0CC
		public override bool Equals(object obj)
		{
			return this.Equals(obj as IGroupItem);
		}

		// Token: 0x0600155A RID: 5466 RVA: 0x0003BEDC File Offset: 0x0003A0DC
		public bool Equals(IGroupItem other)
		{
			RollupGroupItem rollupGroupItem = other as RollupGroupItem;
			return rollupGroupItem != null && rollupGroupItem.GroupItems.SequenceEqual(this.GroupItems);
		}

		// Token: 0x0600155B RID: 5467 RVA: 0x0003BF06 File Offset: 0x0003A106
		public override int GetHashCode()
		{
			return this._groupItems[0].GetHashCode();
		}

		// Token: 0x04000B62 RID: 2914
		private readonly ReadOnlyCollection<CompositeKeyGroupItem> _groupItems;
	}
}
