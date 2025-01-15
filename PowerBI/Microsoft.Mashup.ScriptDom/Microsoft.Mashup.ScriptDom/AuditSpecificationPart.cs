using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200043A RID: 1082
	[Serializable]
	internal class AuditSpecificationPart : TSqlFragment
	{
		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x06003191 RID: 12689 RVA: 0x0016F645 File Offset: 0x0016D845
		// (set) Token: 0x06003192 RID: 12690 RVA: 0x0016F64D File Offset: 0x0016D84D
		public bool IsDrop
		{
			get
			{
				return this._isDrop;
			}
			set
			{
				this._isDrop = value;
			}
		}

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x06003193 RID: 12691 RVA: 0x0016F656 File Offset: 0x0016D856
		// (set) Token: 0x06003194 RID: 12692 RVA: 0x0016F65E File Offset: 0x0016D85E
		public AuditSpecificationDetail Details
		{
			get
			{
				return this._details;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._details = value;
			}
		}

		// Token: 0x06003195 RID: 12693 RVA: 0x0016F66E File Offset: 0x0016D86E
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06003196 RID: 12694 RVA: 0x0016F67A File Offset: 0x0016D87A
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Details != null)
			{
				this.Details.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001E78 RID: 7800
		private bool _isDrop;

		// Token: 0x04001E79 RID: 7801
		private AuditSpecificationDetail _details;
	}
}
