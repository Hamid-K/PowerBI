using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.DataShaping.Common;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x02000161 RID: 353
	internal sealed class AddMissingItemsRollup : IAddMissingItemsGroupItem, IEquatable<IAddMissingItemsGroupItem>
	{
		// Token: 0x06001429 RID: 5161 RVA: 0x0003A787 File Offset: 0x00038987
		internal AddMissingItemsRollup(IEnumerable<AddMissingItemsGroupWithSubtotal> groups, IReadOnlyList<QueryExpression> contextTables)
		{
			this.Groups = groups.ToReadOnlyCollection<AddMissingItemsGroupWithSubtotal>();
			this.ContextTables = contextTables;
		}

		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x0600142A RID: 5162 RVA: 0x0003A7A2 File Offset: 0x000389A2
		internal ReadOnlyCollection<AddMissingItemsGroupWithSubtotal> Groups { get; }

		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x0600142B RID: 5163 RVA: 0x0003A7AA File Offset: 0x000389AA
		internal IReadOnlyList<QueryExpression> ContextTables { get; }

		// Token: 0x0600142C RID: 5164 RVA: 0x0003A7B2 File Offset: 0x000389B2
		public override bool Equals(object obj)
		{
			return this.Equals(obj as IAddMissingItemsGroupItem);
		}

		// Token: 0x0600142D RID: 5165 RVA: 0x0003A7C0 File Offset: 0x000389C0
		public bool Equals(IAddMissingItemsGroupItem other)
		{
			AddMissingItemsRollup addMissingItemsRollup = other as AddMissingItemsRollup;
			return addMissingItemsRollup != null && this.Groups.SequenceEqual(addMissingItemsRollup.Groups) && this.ContextTables.SequenceEqualReadOnly(addMissingItemsRollup.ContextTables);
		}

		// Token: 0x0600142E RID: 5166 RVA: 0x0003A7FF File Offset: 0x000389FF
		public override int GetHashCode()
		{
			return Hashing.CombineHash(Hashing.CombineHash<AddMissingItemsGroupWithSubtotal>(this.Groups, null), Hashing.CombineHashReadonly<QueryExpression>(this.ContextTables, null));
		}
	}
}
