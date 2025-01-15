using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000249 RID: 585
	[Serializable]
	internal class RevokeStatement : SecurityStatement
	{
		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x060025D9 RID: 9689 RVA: 0x00163691 File Offset: 0x00161891
		// (set) Token: 0x060025DA RID: 9690 RVA: 0x00163699 File Offset: 0x00161899
		public bool GrantOptionFor
		{
			get
			{
				return this._grantOptionFor;
			}
			set
			{
				this._grantOptionFor = value;
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x060025DB RID: 9691 RVA: 0x001636A2 File Offset: 0x001618A2
		// (set) Token: 0x060025DC RID: 9692 RVA: 0x001636AA File Offset: 0x001618AA
		public bool CascadeOption
		{
			get
			{
				return this._cascadeOption;
			}
			set
			{
				this._cascadeOption = value;
			}
		}

		// Token: 0x060025DD RID: 9693 RVA: 0x001636B3 File Offset: 0x001618B3
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060025DE RID: 9694 RVA: 0x001636C0 File Offset: 0x001618C0
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			int i = 0;
			int count = base.Permissions.Count;
			while (i < count)
			{
				base.Permissions[i].Accept(visitor);
				i++;
			}
			if (base.SecurityTargetObject != null)
			{
				base.SecurityTargetObject.Accept(visitor);
			}
			int j = 0;
			int count2 = base.Principals.Count;
			while (j < count2)
			{
				base.Principals[j].Accept(visitor);
				j++;
			}
			if (base.AsClause != null)
			{
				base.AsClause.Accept(visitor);
			}
		}

		// Token: 0x04001B29 RID: 6953
		private bool _grantOptionFor;

		// Token: 0x04001B2A RID: 6954
		private bool _cascadeOption;
	}
}
