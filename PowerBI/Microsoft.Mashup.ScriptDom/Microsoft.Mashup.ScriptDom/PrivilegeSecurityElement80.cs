using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000255 RID: 597
	[Serializable]
	internal class PrivilegeSecurityElement80 : SecurityElement80
	{
		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06002626 RID: 9766 RVA: 0x00163B6C File Offset: 0x00161D6C
		public IList<Privilege80> Privileges
		{
			get
			{
				return this._privileges;
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x06002627 RID: 9767 RVA: 0x00163B74 File Offset: 0x00161D74
		// (set) Token: 0x06002628 RID: 9768 RVA: 0x00163B7C File Offset: 0x00161D7C
		public SchemaObjectName SchemaObjectName
		{
			get
			{
				return this._schemaObjectName;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._schemaObjectName = value;
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06002629 RID: 9769 RVA: 0x00163B8C File Offset: 0x00161D8C
		public IList<Identifier> Columns
		{
			get
			{
				return this._columns;
			}
		}

		// Token: 0x0600262A RID: 9770 RVA: 0x00163B94 File Offset: 0x00161D94
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600262B RID: 9771 RVA: 0x00163BA0 File Offset: 0x00161DA0
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			int i = 0;
			int count = this.Privileges.Count;
			while (i < count)
			{
				this.Privileges[i].Accept(visitor);
				i++;
			}
			if (this.SchemaObjectName != null)
			{
				this.SchemaObjectName.Accept(visitor);
			}
			int j = 0;
			int count2 = this.Columns.Count;
			while (j < count2)
			{
				this.Columns[j].Accept(visitor);
				j++;
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001B40 RID: 6976
		private List<Privilege80> _privileges = new List<Privilege80>();

		// Token: 0x04001B41 RID: 6977
		private SchemaObjectName _schemaObjectName;

		// Token: 0x04001B42 RID: 6978
		private List<Identifier> _columns = new List<Identifier>();
	}
}
