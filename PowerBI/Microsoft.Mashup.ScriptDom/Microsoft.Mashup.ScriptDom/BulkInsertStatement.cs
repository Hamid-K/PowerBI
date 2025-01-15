using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000358 RID: 856
	[Serializable]
	internal class BulkInsertStatement : BulkInsertBase
	{
		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06002C32 RID: 11314 RVA: 0x00169E31 File Offset: 0x00168031
		// (set) Token: 0x06002C33 RID: 11315 RVA: 0x00169E39 File Offset: 0x00168039
		public IdentifierOrValueExpression From
		{
			get
			{
				return this._from;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._from = value;
			}
		}

		// Token: 0x06002C34 RID: 11316 RVA: 0x00169E49 File Offset: 0x00168049
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002C35 RID: 11317 RVA: 0x00169E58 File Offset: 0x00168058
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (base.To != null)
			{
				base.To.Accept(visitor);
			}
			if (this.From != null)
			{
				this.From.Accept(visitor);
			}
			int i = 0;
			int count = base.Options.Count;
			while (i < count)
			{
				base.Options[i].Accept(visitor);
				i++;
			}
		}

		// Token: 0x04001CF3 RID: 7411
		private IdentifierOrValueExpression _from;
	}
}
