using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000298 RID: 664
	[Serializable]
	internal class CreateSynonymStatement : TSqlStatement
	{
		// Token: 0x17000234 RID: 564
		// (get) Token: 0x0600279D RID: 10141 RVA: 0x001653FF File Offset: 0x001635FF
		// (set) Token: 0x0600279E RID: 10142 RVA: 0x00165407 File Offset: 0x00163607
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

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x0600279F RID: 10143 RVA: 0x00165417 File Offset: 0x00163617
		// (set) Token: 0x060027A0 RID: 10144 RVA: 0x0016541F File Offset: 0x0016361F
		public SchemaObjectName ForName
		{
			get
			{
				return this._forName;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._forName = value;
			}
		}

		// Token: 0x060027A1 RID: 10145 RVA: 0x0016542F File Offset: 0x0016362F
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060027A2 RID: 10146 RVA: 0x0016543B File Offset: 0x0016363B
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Name != null)
			{
				this.Name.Accept(visitor);
			}
			if (this.ForName != null)
			{
				this.ForName.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001BA5 RID: 7077
		private SchemaObjectName _name;

		// Token: 0x04001BA6 RID: 7078
		private SchemaObjectName _forName;
	}
}
