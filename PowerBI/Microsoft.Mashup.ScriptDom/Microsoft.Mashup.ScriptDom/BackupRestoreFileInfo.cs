using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000356 RID: 854
	[Serializable]
	internal class BackupRestoreFileInfo : TSqlFragment
	{
		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06002C27 RID: 11303 RVA: 0x00169D31 File Offset: 0x00167F31
		public IList<ValueExpression> Items
		{
			get
			{
				return this._items;
			}
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06002C28 RID: 11304 RVA: 0x00169D39 File Offset: 0x00167F39
		// (set) Token: 0x06002C29 RID: 11305 RVA: 0x00169D41 File Offset: 0x00167F41
		public BackupRestoreItemKind ItemKind
		{
			get
			{
				return this._itemKind;
			}
			set
			{
				this._itemKind = value;
			}
		}

		// Token: 0x06002C2A RID: 11306 RVA: 0x00169D4A File Offset: 0x00167F4A
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002C2B RID: 11307 RVA: 0x00169D58 File Offset: 0x00167F58
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			int i = 0;
			int count = this.Items.Count;
			while (i < count)
			{
				this.Items[i].Accept(visitor);
				i++;
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001CEF RID: 7407
		private List<ValueExpression> _items = new List<ValueExpression>();

		// Token: 0x04001CF0 RID: 7408
		private BackupRestoreItemKind _itemKind;
	}
}
