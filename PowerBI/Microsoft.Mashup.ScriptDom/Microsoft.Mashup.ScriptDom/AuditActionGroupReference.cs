using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200043E RID: 1086
	[Serializable]
	internal class AuditActionGroupReference : AuditSpecificationDetail
	{
		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x060031A6 RID: 12710 RVA: 0x0016F7AC File Offset: 0x0016D9AC
		// (set) Token: 0x060031A7 RID: 12711 RVA: 0x0016F7B4 File Offset: 0x0016D9B4
		public AuditActionGroup Group
		{
			get
			{
				return this._group;
			}
			set
			{
				this._group = value;
			}
		}

		// Token: 0x060031A8 RID: 12712 RVA: 0x0016F7BD File Offset: 0x0016D9BD
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060031A9 RID: 12713 RVA: 0x0016F7C9 File Offset: 0x0016D9C9
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001E7E RID: 7806
		private AuditActionGroup _group;
	}
}
