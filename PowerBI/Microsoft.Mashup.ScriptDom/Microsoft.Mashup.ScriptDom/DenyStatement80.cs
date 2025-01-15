using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000251 RID: 593
	[Serializable]
	internal class DenyStatement80 : SecurityStatementBody80
	{
		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x0600260F RID: 9743 RVA: 0x00163A83 File Offset: 0x00161C83
		// (set) Token: 0x06002610 RID: 9744 RVA: 0x00163A8B File Offset: 0x00161C8B
		public bool CascadeOption
		{
			get
			{
				return this._cascadeOption;
			}
			set
			{
				this._cascadeOption = value;
			}
		}

		// Token: 0x06002611 RID: 9745 RVA: 0x00163A94 File Offset: 0x00161C94
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002612 RID: 9746 RVA: 0x00163AA0 File Offset: 0x00161CA0
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001B3A RID: 6970
		private bool _cascadeOption;
	}
}
