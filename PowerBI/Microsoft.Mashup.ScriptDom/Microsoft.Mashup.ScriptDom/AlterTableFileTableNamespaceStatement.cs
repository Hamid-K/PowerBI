using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000281 RID: 641
	[Serializable]
	internal class AlterTableFileTableNamespaceStatement : AlterTableStatement
	{
		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06002715 RID: 10005 RVA: 0x00164B4A File Offset: 0x00162D4A
		// (set) Token: 0x06002716 RID: 10006 RVA: 0x00164B52 File Offset: 0x00162D52
		public bool IsEnable
		{
			get
			{
				return this._isEnable;
			}
			set
			{
				this._isEnable = value;
			}
		}

		// Token: 0x06002717 RID: 10007 RVA: 0x00164B5B File Offset: 0x00162D5B
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002718 RID: 10008 RVA: 0x00164B67 File Offset: 0x00162D67
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (base.SchemaObjectName != null)
			{
				base.SchemaObjectName.Accept(visitor);
			}
		}

		// Token: 0x04001B7F RID: 7039
		private bool _isEnable;
	}
}
