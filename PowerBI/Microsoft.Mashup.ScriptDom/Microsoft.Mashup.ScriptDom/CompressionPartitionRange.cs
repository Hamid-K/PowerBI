using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000344 RID: 836
	[Serializable]
	internal class CompressionPartitionRange : TSqlFragment
	{
		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06002BA6 RID: 11174 RVA: 0x001693D9 File Offset: 0x001675D9
		// (set) Token: 0x06002BA7 RID: 11175 RVA: 0x001693E1 File Offset: 0x001675E1
		public ScalarExpression From
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

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06002BA8 RID: 11176 RVA: 0x001693F1 File Offset: 0x001675F1
		// (set) Token: 0x06002BA9 RID: 11177 RVA: 0x001693F9 File Offset: 0x001675F9
		public ScalarExpression To
		{
			get
			{
				return this._to;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._to = value;
			}
		}

		// Token: 0x06002BAA RID: 11178 RVA: 0x00169409 File Offset: 0x00167609
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002BAB RID: 11179 RVA: 0x00169415 File Offset: 0x00167615
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.From != null)
			{
				this.From.Accept(visitor);
			}
			if (this.To != null)
			{
				this.To.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001CC3 RID: 7363
		private ScalarExpression _from;

		// Token: 0x04001CC4 RID: 7364
		private ScalarExpression _to;
	}
}
