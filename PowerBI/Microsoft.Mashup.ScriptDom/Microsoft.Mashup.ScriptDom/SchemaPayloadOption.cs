using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200038E RID: 910
	[Serializable]
	internal class SchemaPayloadOption : PayloadOption
	{
		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06002D9A RID: 11674 RVA: 0x0016B609 File Offset: 0x00169809
		// (set) Token: 0x06002D9B RID: 11675 RVA: 0x0016B611 File Offset: 0x00169811
		public bool IsStandard
		{
			get
			{
				return this._isStandard;
			}
			set
			{
				this._isStandard = value;
			}
		}

		// Token: 0x06002D9C RID: 11676 RVA: 0x0016B61A File Offset: 0x0016981A
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002D9D RID: 11677 RVA: 0x0016B626 File Offset: 0x00169826
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001D61 RID: 7521
		private bool _isStandard;
	}
}
