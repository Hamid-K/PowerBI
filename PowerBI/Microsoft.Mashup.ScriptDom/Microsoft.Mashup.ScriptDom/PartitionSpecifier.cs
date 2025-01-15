using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020002A7 RID: 679
	[Serializable]
	internal class PartitionSpecifier : TSqlFragment
	{
		// Token: 0x17000248 RID: 584
		// (get) Token: 0x060027EC RID: 10220 RVA: 0x00165971 File Offset: 0x00163B71
		// (set) Token: 0x060027ED RID: 10221 RVA: 0x00165979 File Offset: 0x00163B79
		public ScalarExpression Number
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

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x060027EE RID: 10222 RVA: 0x00165989 File Offset: 0x00163B89
		// (set) Token: 0x060027EF RID: 10223 RVA: 0x00165991 File Offset: 0x00163B91
		public bool All
		{
			get
			{
				return this._all;
			}
			set
			{
				this._all = value;
			}
		}

		// Token: 0x060027F0 RID: 10224 RVA: 0x0016599A File Offset: 0x00163B9A
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060027F1 RID: 10225 RVA: 0x001659A6 File Offset: 0x00163BA6
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Number != null)
			{
				this.Number.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001BB9 RID: 7097
		private ScalarExpression _number;

		// Token: 0x04001BBA RID: 7098
		private bool _all;
	}
}
