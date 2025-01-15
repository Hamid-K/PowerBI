using System;
using System.Collections.Generic;
using Microsoft.DataShaping.Common;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x02000160 RID: 352
	internal sealed class AddMissingItemsGroupWithSubtotal : IAddMissingItemsGroupItem, IEquatable<IAddMissingItemsGroupItem>
	{
		// Token: 0x06001423 RID: 5155 RVA: 0x0003A6D4 File Offset: 0x000388D4
		internal AddMissingItemsGroupWithSubtotal(AddMissingItemsGroup group, QueryExpression subtotalIndicator, IReadOnlyList<QueryExpression> contextTables)
		{
			this.Group = group;
			this.SubtotalIndicator = subtotalIndicator;
			this.ContextTables = contextTables;
		}

		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x06001424 RID: 5156 RVA: 0x0003A6F1 File Offset: 0x000388F1
		public AddMissingItemsGroup Group { get; }

		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x06001425 RID: 5157 RVA: 0x0003A6F9 File Offset: 0x000388F9
		public QueryExpression SubtotalIndicator { get; }

		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x06001426 RID: 5158 RVA: 0x0003A701 File Offset: 0x00038901
		public IReadOnlyList<QueryExpression> ContextTables { get; }

		// Token: 0x06001427 RID: 5159 RVA: 0x0003A70C File Offset: 0x0003890C
		public bool Equals(IAddMissingItemsGroupItem other)
		{
			AddMissingItemsGroupWithSubtotal addMissingItemsGroupWithSubtotal = other as AddMissingItemsGroupWithSubtotal;
			return addMissingItemsGroupWithSubtotal != null && (this.Group.Equals(addMissingItemsGroupWithSubtotal.Group) && this.SubtotalIndicator.Equals(addMissingItemsGroupWithSubtotal.SubtotalIndicator)) && this.ContextTables.SequenceEqualReadOnly(addMissingItemsGroupWithSubtotal.ContextTables);
		}

		// Token: 0x06001428 RID: 5160 RVA: 0x0003A75E File Offset: 0x0003895E
		public override int GetHashCode()
		{
			return Hashing.CombineHash(this.Group.GetHashCode(), this.SubtotalIndicator.GetHashCode(), Hashing.GetHashCode<IReadOnlyList<QueryExpression>>(this.ContextTables, null));
		}
	}
}
