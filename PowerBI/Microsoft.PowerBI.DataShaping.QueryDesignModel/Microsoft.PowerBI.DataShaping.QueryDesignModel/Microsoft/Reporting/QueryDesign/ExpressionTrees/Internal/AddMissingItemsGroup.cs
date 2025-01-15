using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.DataShaping.Common;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x0200015F RID: 351
	internal sealed class AddMissingItemsGroup : IAddMissingItemsGroupItem, IEquatable<IAddMissingItemsGroupItem>
	{
		// Token: 0x0600141E RID: 5150 RVA: 0x0003A670 File Offset: 0x00038870
		internal AddMissingItemsGroup(IEnumerable<QueryExpression> keys)
		{
			this._keys = keys.ToReadOnlyCollection<QueryExpression>();
		}

		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x0600141F RID: 5151 RVA: 0x0003A684 File Offset: 0x00038884
		public ReadOnlyCollection<QueryExpression> Keys
		{
			get
			{
				return this._keys;
			}
		}

		// Token: 0x06001420 RID: 5152 RVA: 0x0003A68C File Offset: 0x0003888C
		public override bool Equals(object obj)
		{
			return this.Equals(obj as IAddMissingItemsGroupItem);
		}

		// Token: 0x06001421 RID: 5153 RVA: 0x0003A69C File Offset: 0x0003889C
		public bool Equals(IAddMissingItemsGroupItem other)
		{
			AddMissingItemsGroup addMissingItemsGroup = other as AddMissingItemsGroup;
			return addMissingItemsGroup != null && this.Keys.SequenceEqual(addMissingItemsGroup.Keys);
		}

		// Token: 0x06001422 RID: 5154 RVA: 0x0003A6C6 File Offset: 0x000388C6
		public override int GetHashCode()
		{
			return Hashing.CombineHash<QueryExpression>(this._keys, null);
		}

		// Token: 0x04000B02 RID: 2818
		private readonly ReadOnlyCollection<QueryExpression> _keys;
	}
}
