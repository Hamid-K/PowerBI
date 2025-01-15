using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200030B RID: 779
	[Serializable]
	internal class SetFipsFlaggerCommand : SetCommand
	{
		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06002A22 RID: 10786 RVA: 0x00167CA2 File Offset: 0x00165EA2
		// (set) Token: 0x06002A23 RID: 10787 RVA: 0x00167CAA File Offset: 0x00165EAA
		public FipsComplianceLevel ComplianceLevel
		{
			get
			{
				return this._complianceLevel;
			}
			set
			{
				this._complianceLevel = value;
			}
		}

		// Token: 0x06002A24 RID: 10788 RVA: 0x00167CB3 File Offset: 0x00165EB3
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002A25 RID: 10789 RVA: 0x00167CBF File Offset: 0x00165EBF
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001C4F RID: 7247
		private FipsComplianceLevel _complianceLevel;
	}
}
