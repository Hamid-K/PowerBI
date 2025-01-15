using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000321 RID: 801
	[Serializable]
	internal class AlterDatabaseModifyNameStatement : AlterDatabaseStatement
	{
		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06002AB2 RID: 10930 RVA: 0x0016859D File Offset: 0x0016679D
		// (set) Token: 0x06002AB3 RID: 10931 RVA: 0x001685A5 File Offset: 0x001667A5
		public Identifier NewDatabaseName
		{
			get
			{
				return this._newDatabaseName;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._newDatabaseName = value;
			}
		}

		// Token: 0x06002AB4 RID: 10932 RVA: 0x001685B5 File Offset: 0x001667B5
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002AB5 RID: 10933 RVA: 0x001685C1 File Offset: 0x001667C1
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.NewDatabaseName != null)
			{
				this.NewDatabaseName.Accept(visitor);
			}
		}

		// Token: 0x04001C7A RID: 7290
		private Identifier _newDatabaseName;
	}
}
