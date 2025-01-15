using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000246 RID: 582
	[Serializable]
	internal abstract class SecurityStatement : TSqlStatement
	{
		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x060025C7 RID: 9671 RVA: 0x00163442 File Offset: 0x00161642
		public IList<Permission> Permissions
		{
			get
			{
				return this._permissions;
			}
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x060025C8 RID: 9672 RVA: 0x0016344A File Offset: 0x0016164A
		// (set) Token: 0x060025C9 RID: 9673 RVA: 0x00163452 File Offset: 0x00161652
		public SecurityTargetObject SecurityTargetObject
		{
			get
			{
				return this._securityTargetObject;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._securityTargetObject = value;
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x060025CA RID: 9674 RVA: 0x00163462 File Offset: 0x00161662
		public IList<SecurityPrincipal> Principals
		{
			get
			{
				return this._principals;
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x060025CB RID: 9675 RVA: 0x0016346A File Offset: 0x0016166A
		// (set) Token: 0x060025CC RID: 9676 RVA: 0x00163472 File Offset: 0x00161672
		public Identifier AsClause
		{
			get
			{
				return this._asClause;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._asClause = value;
			}
		}

		// Token: 0x060025CD RID: 9677 RVA: 0x00163484 File Offset: 0x00161684
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			int i = 0;
			int count = this.Permissions.Count;
			while (i < count)
			{
				this.Permissions[i].Accept(visitor);
				i++;
			}
			if (this.SecurityTargetObject != null)
			{
				this.SecurityTargetObject.Accept(visitor);
			}
			int j = 0;
			int count2 = this.Principals.Count;
			while (j < count2)
			{
				this.Principals[j].Accept(visitor);
				j++;
			}
			if (this.AsClause != null)
			{
				this.AsClause.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001B23 RID: 6947
		private List<Permission> _permissions = new List<Permission>();

		// Token: 0x04001B24 RID: 6948
		private SecurityTargetObject _securityTargetObject;

		// Token: 0x04001B25 RID: 6949
		private List<SecurityPrincipal> _principals = new List<SecurityPrincipal>();

		// Token: 0x04001B26 RID: 6950
		private Identifier _asClause;
	}
}
