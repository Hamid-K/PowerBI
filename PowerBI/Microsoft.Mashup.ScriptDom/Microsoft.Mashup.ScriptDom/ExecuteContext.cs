using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020001AE RID: 430
	[Serializable]
	internal class ExecuteContext : TSqlFragment
	{
		// Token: 0x1700009D RID: 157
		// (get) Token: 0x0600221B RID: 8731 RVA: 0x0015F078 File Offset: 0x0015D278
		// (set) Token: 0x0600221C RID: 8732 RVA: 0x0015F080 File Offset: 0x0015D280
		public ScalarExpression Principal
		{
			get
			{
				return this._principal;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._principal = value;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x0600221D RID: 8733 RVA: 0x0015F090 File Offset: 0x0015D290
		// (set) Token: 0x0600221E RID: 8734 RVA: 0x0015F098 File Offset: 0x0015D298
		public ExecuteAsOption Kind
		{
			get
			{
				return this._kind;
			}
			set
			{
				this._kind = value;
			}
		}

		// Token: 0x0600221F RID: 8735 RVA: 0x0015F0A1 File Offset: 0x0015D2A1
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002220 RID: 8736 RVA: 0x0015F0AD File Offset: 0x0015D2AD
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Principal != null)
			{
				this.Principal.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001A0E RID: 6670
		private ScalarExpression _principal;

		// Token: 0x04001A0F RID: 6671
		private ExecuteAsOption _kind;
	}
}
