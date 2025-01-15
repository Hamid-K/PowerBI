using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020001C8 RID: 456
	[Serializable]
	internal class ExecuteAsProcedureOption : ProcedureOption
	{
		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060022BE RID: 8894 RVA: 0x0015FC9F File Offset: 0x0015DE9F
		// (set) Token: 0x060022BF RID: 8895 RVA: 0x0015FCA7 File Offset: 0x0015DEA7
		public ExecuteAsClause ExecuteAs
		{
			get
			{
				return this._executeAs;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._executeAs = value;
			}
		}

		// Token: 0x060022C0 RID: 8896 RVA: 0x0015FCB7 File Offset: 0x0015DEB7
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060022C1 RID: 8897 RVA: 0x0015FCC3 File Offset: 0x0015DEC3
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.ExecuteAs != null)
			{
				this.ExecuteAs.Accept(visitor);
			}
		}

		// Token: 0x04001A40 RID: 6720
		private ExecuteAsClause _executeAs;
	}
}
