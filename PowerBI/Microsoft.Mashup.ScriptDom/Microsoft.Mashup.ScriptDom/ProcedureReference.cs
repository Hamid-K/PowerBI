using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020001C4 RID: 452
	[Serializable]
	internal class ProcedureReference : TSqlFragment
	{
		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060022A0 RID: 8864 RVA: 0x0015FA71 File Offset: 0x0015DC71
		// (set) Token: 0x060022A1 RID: 8865 RVA: 0x0015FA79 File Offset: 0x0015DC79
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

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060022A2 RID: 8866 RVA: 0x0015FA89 File Offset: 0x0015DC89
		// (set) Token: 0x060022A3 RID: 8867 RVA: 0x0015FA91 File Offset: 0x0015DC91
		public Literal Number
		{
			get
			{
				return this._number;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._number = value;
			}
		}

		// Token: 0x060022A4 RID: 8868 RVA: 0x0015FAA1 File Offset: 0x0015DCA1
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060022A5 RID: 8869 RVA: 0x0015FAAD File Offset: 0x0015DCAD
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Name != null)
			{
				this.Name.Accept(visitor);
			}
			if (this.Number != null)
			{
				this.Number.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001A36 RID: 6710
		private SchemaObjectName _name;

		// Token: 0x04001A37 RID: 6711
		private Literal _number;
	}
}
