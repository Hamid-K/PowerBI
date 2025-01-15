using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020003E0 RID: 992
	[Serializable]
	internal class DropTypeStatement : TSqlStatement
	{
		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x06002F87 RID: 12167 RVA: 0x0016D6F4 File Offset: 0x0016B8F4
		// (set) Token: 0x06002F88 RID: 12168 RVA: 0x0016D6FC File Offset: 0x0016B8FC
		public SchemaObjectName Name
		{
			get
			{
				return this._name;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._name = value;
			}
		}

		// Token: 0x06002F89 RID: 12169 RVA: 0x0016D70C File Offset: 0x0016B90C
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002F8A RID: 12170 RVA: 0x0016D718 File Offset: 0x0016B918
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Name != null)
			{
				this.Name.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001DED RID: 7661
		private SchemaObjectName _name;
	}
}
